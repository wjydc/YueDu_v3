using Com.Alipay;
using Component.Base;
using Component.Controllers.User;
using Component.Filter;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace YueDu.Controllers
{
    public class AlipayController : UserInfoController
    {
        private IRechargeService _rechargeService;

        public AlipayController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }

        [Authorization]
        public void Index()
        {
            ErrorMessage errorMessage = ErrorMessage.失败;

            string url = StringHelper.GetHost();
            if (string.IsNullOrEmpty(url))
            {
                errorMessage = ErrorMessage.订单初始化失败;
                return;
            }

            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(1, out errorMessage, out payType, out orderId, out money, out userType))
            {
                //支付宝网关地址
                string GATEWAY_NEW = "http://wappaygw.alipay.com/service/rest.htm?";

                #region

                //返回格式, 必填，不需要修改
                string format = "xml";

                //返回格式, 必填，不需要修改
                string v = "2.0";

                //请求号, 必填，须保证每次请求都是唯一
                string req_id = DateTime.Now.ToString("yyyyMMddHHmmss");

                //req_data详细信息

                //服务器异步通知页面路径 ,需http://格式的完整路径，不允许加?id=123这类自定义参数
                string notify_url = string.Format("http://{0}/alipay/notify", url);

                //页面跳转同步通知页面路径, 需http://格式的完整路径，不允许加?id=123这类自定义参数
                string call_back_url = string.Format("http://{0}/alipay/result", url);

                //操作中断返回地址, 用户付款中途退出返回商户的地址。需http://格式的完整路径，不允许加?id=123这类自定义参数
                string merchant_url = string.Format("http://{0}/order/recharge", url);

                //卖家支付宝帐户
                string seller_email = Config.SellerEmail;

                //商户网站订单系统中唯一订单号，必填

                //订单名称 必填
                string subject = GetName(money);

                //付款金额（单位：元） 必填
                string total_fee = GetFee(money);

                //请求业务参数详细 必填
                string req_dataToken = "<direct_trade_create_req><notify_url>" + notify_url + "</notify_url><call_back_url>" + call_back_url + "</call_back_url><seller_account_name>" + seller_email + "</seller_account_name><out_trade_no>" + orderId + "</out_trade_no><subject>" + subject + "</subject><total_fee>" + total_fee + "</total_fee><merchant_url>" + merchant_url + "</merchant_url></direct_trade_create_req>";

                //把请求参数打包成数组
                Dictionary<string, string> sParaTempToken = new Dictionary<string, string>();
                sParaTempToken.Add("partner", Config.Partner);
                sParaTempToken.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTempToken.Add("sec_id", Config.Sign_type.ToUpper());
                sParaTempToken.Add("service", "alipay.wap.trade.create.direct");
                sParaTempToken.Add("format", format);
                sParaTempToken.Add("v", v);
                sParaTempToken.Add("req_id", req_id);
                sParaTempToken.Add("req_data", req_dataToken);

                //建立请求
                string sHtmlTextToken = Submit.BuildRequest(GATEWAY_NEW, sParaTempToken);
                //URLDECODE返回的信息
                Encoding code = Encoding.GetEncoding(Config.Input_charset);
                sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);

                //解析远程模拟提交后返回的信息
                Dictionary<string, string> dicHtmlTextToken = Submit.ParseResponse(sHtmlTextToken);

                //获取token
                string request_token = dicHtmlTextToken["request_token"];

                #endregion

                //业务详细 必填
                string req_data = "<auth_and_execute_req><request_token>" + request_token + "</request_token></auth_and_execute_req>";

                //把请求参数打包成数组
                Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
                sParaTemp.Add("partner", Config.Partner);
                sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTemp.Add("sec_id", Config.Sign_type.ToUpper());
                sParaTemp.Add("service", "alipay.wap.auth.authAndExecute");
                sParaTemp.Add("format", format);
                sParaTemp.Add("v", v);
                sParaTemp.Add("req_data", req_data);

                //建立请求
                string sHtmlText = Submit.BuildRequest(GATEWAY_NEW, sParaTemp, "get", "确认");
                Response.Write(sHtmlText);
            }
        }

        [ValidateInput(false)]
        public void Notify()
        {
            string errorMessage = "fail";

            Dictionary<string, string> sPara = GetParamPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Com.Alipay.Notify aliNotify = new Com.Alipay.Notify();
                bool verifyResult = aliNotify.VerifyNotify(sPara, Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    //解密（如果是RSA签名需要解密，如果是MD5签名则下面一行清注释掉）
                    sPara = aliNotify.Decrypt(sPara);

                    //XML解析notify_data数据
                    try
                    {
                        XmlHelper xmlHelper = new XmlHelper(sPara["notify_data"]);
                        if (xmlHelper != null)
                        {
                            //商户订单号
                            string out_trade_no = xmlHelper.GetNodeString("/notify/out_trade_no");
                            //支付宝交易号
                            string trade_no = xmlHelper.GetNodeString("/notify/trade_no");
                            //交易状态
                            string trade_status = xmlHelper.GetNodeString("/notify/trade_status");

                            string buyer_email = xmlHelper.GetNodeString("/notify/buyer_email");

                            string seller_id = xmlHelper.GetNodeString("/notify/seller_id");

                            decimal total_fee = xmlHelper.GetNodeDecimal("/notify/total_fee");

                            if (string.Compare(Com.Alipay.Config.Partner, seller_id, true) == 0 && (string.Compare(trade_status, "TRADE_FINISHED", true) == 0 || string.Compare(trade_status, "TRADE_SUCCESS", true) == 0))
                            {
                                int result = 0;
                                RechargeInfo model = new RechargeInfo();
                                model.OrderId = out_trade_no;
                                model.FOrderId = trade_no;
                                model.Cash = total_fee * 100;
                                model.PayMobile = "";
                                _rechargeService.Completed(model, out result);     //1：成功 0：失败
                                if (result == (int)ErrorMessage.成功)
                                {
                                    errorMessage = "success";
                                }
                            }
                            else
                            {
                                errorMessage = trade_status;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                errorMessage = "无通知参数";
            }

            this.Response.Write(errorMessage);
        }

        public ActionResult Result()
        {
            Dictionary<string, string> sPara = GetParamGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Com.Alipay.Notify aliNotify = new Com.Alipay.Notify();
                bool verifyResult = aliNotify.VerifyReturn(sPara, Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    //商户订单号
                    string out_trade_no = Request.QueryString["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request.QueryString["trade_no"];

                    //交易状态
                    string result = Request.QueryString["result"];

                    if (string.Compare(result, "success", true) == 0)
                    {
                        return Redirect("/recentread/index".GetChannelRouteUrl(RouteChannelId));
                    }

                    //Response.Write("验证成功<br />");
                }
                else//验证失败
                {
                    //Response.Write("验证失败");
                }
            }
            else
            {
                //Response.Write("无返回参数");
            }

            return Redirect("/order/recharge".GetChannelRouteUrl(RouteChannelId));
        }

        /// <summary>
        /// 获取订单名称
        /// </summary>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        private string GetName(int money)
        {
            return string.Concat("充值", GetFee(money), "元");
        }

        /// <summary>
        /// 获取充值金额
        /// </summary>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        private string GetFee(int money)
        {
            return StringHelper.ToString(((decimal)money / (decimal)100));
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetParamPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll = Request.Form;

            string[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public Dictionary<string, string> GetParamGet()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll = Request.QueryString;

            string[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}