using Component.Base;
using Component.Controllers.Pay;
using Component.Controllers.User;
using Component.Filter;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace YueDu.Controllers
{
    public class WxPayController : UserInfoController
    {
        private IRechargeService _rechargeService;

        public WxPayController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }

        [Route("user/order/wechat/js/open.aspx")]
        [Authorization]
        public ActionResult Open()
        {
            string url = "";

            try
            {
                string host = StringHelper.GetHost();
                string path = Request.Path;
                string queryString = Request.Url.Query;
                string code = UrlParameterHelper.GetQueryString("code");
                string query = "";
                string state = "";
                if (!string.IsNullOrEmpty(code))
                {
                    query = UrlParameterHelper.GetDecodingParams("state");
                }
                else
                {
                    state = UrlParameterHelper.UrlEncode(queryString);
                }

                Com.WxPayAPI.JsApiPay jsApiPay = new Com.WxPayAPI.JsApiPay(host, path, queryString);
                jsApiPay.GetOpenidAndAccessToken(state, out url, code);

                string openid = jsApiPay.OpenId;

                if (string.IsNullOrEmpty(url))
                {
                    if (!string.IsNullOrEmpty(openid) && !string.IsNullOrEmpty(query))
                    {
                        IDictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add("openid", openid);
                        url = StringHelper.SpliceUrl(string.Concat("/user/order/wechat/js/index.aspx", query), dict);
                    }
                }
            }
            catch
            {
                url = "/error/notfound".GetChannelRouteUrl(RouteChannelId);
            }

            return Redirect(url);
        }

        [Route("user/order/wechat/js/index.aspx")]
        [Authorization]
        public ActionResult Index()
        {
            Com.WxPayAPI.Log.Info(this.GetType().ToString(), "WxPayController Index");

            string wxJsApiParam = "";

            ErrorMessage errorMessage = ErrorMessage.失败;
            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(1, out errorMessage, out payType, out orderId, out money, out userType))
            {
                string host = StringHelper.GetHost();
                string path = Request.Path;
                string queryString = Request.Url.Query;
                Com.WxPayAPI.JsApiPay jsApiPay = new Com.WxPayAPI.JsApiPay(host, path, queryString);

                string openid = UrlParameterHelper.GetParams("openid");

                //检测是否给当前页面传递了相关参数
                if (string.IsNullOrEmpty(openid) || money <= 0)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                    Com.WxPayAPI.Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");

                    return View();
                }

                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                jsApiPay.OpenId = openid;
                jsApiPay.Total_Fee = money;

                //JSAPI支付预处理
                try
                {
                    string body = string.Concat(SiteSection.Html.SiteName, "-", SiteSection.Html.FeeName);
                    Com.WxPayAPI.WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(body, orderId);
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数

                    Com.WxPayAPI.Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                    //在页面上显示订单信息
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");
                }
                catch
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                }
            }

            ViewData.Model = wxJsApiParam;

            return View();
        }

        [Authorization]
        public ActionResult Index1()
        {
            Com.WxPayAPI2.Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            string url = "";

            ErrorMessage errorMessage = ErrorMessage.失败;
            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(1, out errorMessage, out payType, out orderId, out money, out userType))
            {
                Com.WxPayAPI2.WxPayData data = new Com.WxPayAPI2.WxPayData();
                data.SetValue("body", string.Concat(SiteSection.Html.SiteName, "-", SiteSection.Html.FeeName));//商品描述
                data.SetValue("attach", "");//附加数据
                data.SetValue("out_trade_no", orderId);//随机字符串
                data.SetValue("total_fee", money);//总金额
                data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
                data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
                data.SetValue("goods_tag", "");//商品标记
                data.SetValue("trade_type", "NATIVE");//交易类型
                data.SetValue("product_id", "1");//商品ID

                Com.WxPayAPI2.WxPayData result = Com.WxPayAPI2.WxPayApi.UnifiedOrder(data);//调用统一下单接口
                if (string.Compare(result.GetValue("return_code").ToString(), "SUCCESS", true) == 0)
                {
                    string code_url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接

                    Com.WxPayAPI2.Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + code_url);

                    if (!string.IsNullOrEmpty(code_url))
                    {
                        url = "/WxPay/MakeQRCode?data=" + HttpUtility.UrlEncode(code_url);
                    }
                }
            }

            ViewData.Model = url;
            ViewBag.OrderId = orderId;

            return View();
        }

        [Authorization]
        public void MakeQrCode(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                byte[] buffer = Com.WxPayAPI.QrCodeMaker.PNG(data, 0, 4);

                if (buffer != null && buffer.Length > 0)
                {
                    //输出二维码图片
                    Response.BinaryWrite(buffer);
                    Response.End();
                }
            }
        }
    }
}