using Com.NowPay;
using Com.WxPayAPI;
using Component.Base;
using Component.Controllers.User;
using Component.Filter;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace YueDu.Controllers
{
    public class NowPayController : UserInfoController
    {
        private IRechargeService _rechargeService;

        public NowPayController(IRechargeService rechargeServce)
        {
            _rechargeService = rechargeServce;
        }

        #region

        [Authorization]
        public ActionResult Open()
        {
            string url = "";
            bool isMicromessenger = StringHelper.GetUserAgent().ToLower().Contains("micromessenger");

            int payType = 0;
            string pt = UrlParameterHelper.GetParams("pt");
            if (!string.IsNullOrEmpty(pt) && int.TryParse(pt, out payType) && payType == 13)
            {
                if (isMicromessenger) //微信支付（公众号）
                {
                    url = "/user/order/paynow/open1";
                }
                else //微信支付（手机网页）
                {
                    url = "/user/order/paynow/index2";
                }
            }
            else //其它支付
            {
                url = "/user/order/paynow/index";
            }

            return Redirect(GetPayUrl(url, pt));
        }

        private string GetPayUrl(string url, string pt)
        {
            string userType = UrlParameterHelper.GetParams("ut");
            string money = UrlParameterHelper.GetParams("money");
            string otn = UrlParameterHelper.GetParams("otn");
            //string returnUrl = UrlParameterHelper.GetDecodingParams("returnUrl");
            //string state = "";
            //if (!string.IsNullOrEmpty(returnUrl))
            //{
            //    state = Guid.NewGuid().ToString("N");
            //    ReturnUrlHelper.SetSession("NowPay_ReturnUrl", state, returnUrl);
            //}
            //return string.Format("{0}?ut={1}&money={2}&pt={3}&otn={4}&st={5}", url, userType, money, pt, otn, state);
            return string.Format("{0}?ut={1}&money={2}&pt={3}&otn={4}", url, userType, money, pt, otn);
        }

        #endregion

        #region 现在支付以及现在支付-手机网页

        #region 现在支付

        [Route("user/order/paynow/index")]
        [Authorization]
        public ActionResult Index()
        {
            ErrorMessage errorMessage = ErrorMessage.失败;

            string url = StringHelper.GetHost();
            if (string.IsNullOrEmpty(url))
            {
                errorMessage = ErrorMessage.订单初始化失败;

                return Redirect(string.Format("/error/index?errCode={0}&returnUrl=", (int)errorMessage).GetChannelRouteUrl(RouteChannelId));
            }

            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(10, out errorMessage, out payType, out orderId, out money, out userType))
            {
                string appId = NowPayConfig.AppId;
                string appKey = NowPayConfig.AppKey;
                string callbackUrl = NowPayConfig.CallbackUrl;

                string notifyUrl = string.Format("http://{0}/user/order/paynow/notify", url);
                string frontNotifyUrl = string.Format("http://{0}/user/order/paynow/result", url);

                SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
                dict.Add("funcode", "WP001");                                               //功能码(定值：WP001)(必填)
                dict.Add("appId", appId);                                                //商户应用唯一标识(字符，长度1-40)(必填)
                dict.Add("mhtOrderNo", orderId);                                         //商户订单号(字符，长度1-40)(必填)
                dict.Add("mhtOrderName", GetName(money));                                    //商户商品名称(字符，长度1-40)(必填)
                dict.Add("mhtOrderType", "01");                                             //商户交易类型(01普通消费)(字符，长度1-40)(必填)
                dict.Add("mhtCurrencyType", "156");                                         //商户订单币种类型(156人民币)(字符，长度3)(必填)
                dict.Add("mhtOrderAmt", StringHelper.ToString(money));                      //商户订单交易金额(单位(人民币)：分)(数字，长度22)(必填)
                dict.Add("mhtOrderDetail", "充值");                                         //商户订单详情(字符，长度0-200)(必填)
                dict.Add("mhtOrderTimeOut", "3600");                                        //商户订单超时时间(60~3600秒，默认3600)(数字，长度4)(必填)
                dict.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));     //商户订单开始时间 yyyyMMddHHmmss(字符，长度14)(必填)
                dict.Add("notifyUrl", notifyUrl);                                           //商户后台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                dict.Add("frontNotifyUrl", frontNotifyUrl);                                 //商户前台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                dict.Add("mhtCharset", "UTF-8");                                            //商户字符编码(字符，长度1-16)(必填)
                dict.Add("deviceType", "06");                                               //设备类型(06手机网页)(字符，长度2)(必填)

                #region 支付宝

                //if (payType == 12)
                //{
                //    bool isMicromessenger = StringHelper.GetUserAgent().ToLower().Contains("micromessenger");

                //    if (isMicromessenger)
                //    {
                //        dict.Add("outputType", "4");                                                //微信
                //    }
                //    else
                //    {
                //        dict.Add("outputType", "0");                                                //手机网页
                //    }

                //    callbackUrl = NowPayConfig.NewCallbackUrl;
                //}

                #endregion

                dict.Add("payChannelType", payType.ToString());                            //用户所选渠道类型(如果为空则直接使用现在支付收银台页面，如果不为空则直接跳转至支付渠道（银联或者支付宝等）)(字符，长度2)
                dict.Add("mhtReserved", "");                                                //商户保留域(给商户使用的字段，商户可以对交易进行标记，现在支付将原样返回)(字符，长度100)
                dict.Add("consumerId", StringHelper.ToString(currentUser.UserId));      //消费者ID(消费者在商户系统的ID，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                dict.Add("consumerName", "");                                               //消费者名称(消费者在商户系统的名称，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                dict.Add("mhtSignType", "MD5");                                             //商户签名方法(定值：MD5)(必填)
                dict.Add("mhtSignature", GetSign(appKey, dict));                                    //商户数据签名(MAX64TEXT)(必填)

                return Redirect(string.Concat(callbackUrl, "?", GetPostData(dict)));
            }

            return Redirect(string.Format("/error/index?errCode={0}&returnUrl=", (int)errorMessage).GetChannelRouteUrl(RouteChannelId));
        }

        #endregion 现在支付

        #region 现在支付-手机网页

        [Route("user/order/paynow/index2")]
        [Authorization]
        public ActionResult Index2()
        {
            string result = "";

            ErrorMessage errorMessage = ErrorMessage.失败;

            string url = StringHelper.GetHost();
            if (string.IsNullOrEmpty(url))
            {
                errorMessage = ErrorMessage.订单初始化失败;

                return Redirect(string.Format("/error/index?errCode={0}&returnUrl=", (int)errorMessage).GetChannelRouteUrl(RouteChannelId));
            }

            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(10, out errorMessage, out payType, out orderId, out money, out userType)
                && payType == 13 && !StringHelper.GetUserAgent().ToLower().Contains("micromessenger"))
            {
                string appId = NowPayConfig.AppId;
                string appKey = NowPayConfig.AppKey;
                string callbackUrl = NowPayConfig.NewCallbackUrl;

                string notifyUrl = string.Format("http://{0}/user/order/paynow/notify", url);
                string frontNotifyUrl = string.Format("http://{0}/user/order/paynow/result", url);

                SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
                dict.Add("funcode", "WP001");                                               //功能码(定值：WP001)(必填)
                dict.Add("appId", appId);                                                //商户应用唯一标识(字符，长度1-40)(必填)
                dict.Add("mhtOrderNo", orderId);                                         //商户订单号(字符，长度1-40)(必填)
                dict.Add("mhtOrderName", GetName(money));                                    //商户商品名称(字符，长度1-40)(必填)
                dict.Add("mhtOrderType", "01");                                             //商户交易类型(01普通消费)(字符，长度1-40)(必填)
                dict.Add("mhtCurrencyType", "156");                                         //商户订单币种类型(156人民币)(字符，长度3)(必填)
                dict.Add("mhtOrderAmt", StringHelper.ToString(money));                      //商户订单交易金额(单位(人民币)：分)(数字，长度22)(必填)
                dict.Add("mhtOrderDetail", "充值");                                         //商户订单详情(字符，长度0-200)(必填)
                dict.Add("mhtOrderTimeOut", "3600");                                        //商户订单超时时间(60~3600秒，默认3600)(数字，长度4)(必填)
                dict.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));     //商户订单开始时间 yyyyMMddHHmmss(字符，长度14)(必填)
                dict.Add("notifyUrl", notifyUrl);                                           //商户后台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                dict.Add("frontNotifyUrl", frontNotifyUrl);                                 //商户前台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                dict.Add("mhtCharset", "UTF-8");                                            //商户字符编码(字符，长度1-16)(必填)
                dict.Add("deviceType", "06");                                               //设备类型(06手机网页，0600微信公众号)(字符，长度2)(必填)
                dict.Add("payChannelType", "1301");                                         //用户所选渠道类型(如果为空则直接使用现在支付收银台页面，如果不为空则直接跳转至支付渠道（银联或者支付宝等）)(字符，长度2)
                dict.Add("mhtReserved", "");                                                //商户保留域(给商户使用的字段，商户可以对交易进行标记，现在支付将原样返回)(字符，长度100)
                dict.Add("consumerId", StringHelper.ToString(currentUser.UserId));      //消费者ID(消费者在商户系统的ID，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                dict.Add("consumerName", "");                                               //消费者名称(消费者在商户系统的名称，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                dict.Add("mhtSignType", "MD5");                                             //商户签名方法(定值：MD5)(必填)
                dict.Add("mhtSignature", GetSign(appKey, dict));                                    //商户数据签名(MAX64TEXT)(必填)

                HttpSendInfo sendInfo = new HttpSendInfo();
                sendInfo.Method = "POST";
                sendInfo.Url = callbackUrl;
                sendInfo.Data = GetPostData(dict);
                HttpReceiveInfo receiveInfo = new HttpReceiveInfo();
                if (HttpHelper.Send(sendInfo, out receiveInfo))
                {
                    //报文日志
                    //Log(this.Server.UrlDecode(sendInfo.Data));

                    result = GetTnUrl(receiveInfo.Result);
                }
            }

            ViewData.Model = UrlParameterHelper.UrlDecode(result);
            ViewBag.OrderId = orderId;

            return View();
        }

        private string GetTnUrl(string result)
        {
            if (string.IsNullOrEmpty(result)) return "";

            //响应日志
            //Log(this.Server.UrlDecode(result));

            string url = string.Empty;

            Regex reg = new Regex("(?<begin>[?|&])tn=(?<param>[^&]*)(?<end>[&]?)", RegexOptions.IgnoreCase);
            if (reg.IsMatch(result))
            {
                Match match = reg.Match(result);
                url = match.Groups["param"].Value;
            }

            return url;
        }

        #endregion 现在支付-手机网页

        #endregion

        #region 现在支付-公众号

        [Route("user/order/paynow/open1")]
        [Authorization]
        public ActionResult Open1()
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

                JsApiPay jsApiPay = new JsApiPay(host, path, queryString);
                jsApiPay.GetOpenidAndAccessToken(state, out url, code);
                if (string.IsNullOrEmpty(url))
                {
                    string openid = jsApiPay.OpenId;

                    if (!string.IsNullOrEmpty(openid) && !string.IsNullOrEmpty(query))
                    {
                        IDictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add("openid", openid);
                        url = StringHelper.SpliceUrl(string.Concat("/user/order/paynow/index1", query), dict);
                    }
                }
            }
            catch
            {
                url = "/error/notfound".GetChannelRouteUrl(RouteChannelId);
            }

            return Redirect(url);
        }

        [Route("user/order/paynow/index1")]
        [Authorization]
        public ActionResult Index1()
        {
            string result = "";

            ErrorMessage errorMessage = ErrorMessage.失败;

            string url = StringHelper.GetHost();
            if (string.IsNullOrEmpty(url))
            {
                errorMessage = ErrorMessage.订单初始化失败;

                return Redirect(string.Format("/error/index?errCode={0}&returnUrl=", (int)errorMessage).GetChannelRouteUrl(RouteChannelId));
            }

            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(10, out errorMessage, out payType, out orderId, out money, out userType)
                && payType == 13 && StringHelper.GetUserAgent().ToLower().Contains("micromessenger"))
            {
                string OpenId = UrlParameterHelper.GetParams("OpenId");
                if (!string.IsNullOrEmpty(OpenId))
                {
                    string appId = NowPayConfig.WeChatAppId;
                    string appKey = NowPayConfig.WeChatAppKey;
                    string callbackUrl = NowPayConfig.NewCallbackUrl;

                    string notifyUrl = string.Format("http://{0}/user/order/paynow/notify1", url);
                    string frontNotifyUrl = string.Format("http://{0}/user/order/paynow/result1", url);

                    SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
                    dict.Add("funcode", "WP001");                                               //功能码(定值：WP001)(必填)
                    dict.Add("appId", appId);                                                //商户应用唯一标识(字符，长度1-40)(必填)
                    dict.Add("mhtOrderNo", orderId);                                         //商户订单号(字符，长度1-40)(必填)
                    dict.Add("mhtOrderName", GetName(money));                                    //商户商品名称(字符，长度1-40)(必填)
                    dict.Add("mhtOrderType", "01");                                             //商户交易类型(01普通消费)(字符，长度1-40)(必填)
                    dict.Add("mhtCurrencyType", "156");                                         //商户订单币种类型(156人民币)(字符，长度3)(必填)
                    dict.Add("mhtOrderAmt", StringHelper.ToString(money));                      //商户订单交易金额(单位(人民币)：分)(数字，长度22)(必填)
                    dict.Add("mhtOrderDetail", "充值");                                         //商户订单详情(字符，长度0-200)(必填)
                    dict.Add("mhtOrderTimeOut", "3600");                                        //商户订单超时时间(60~3600秒，默认3600)(数字，长度4)(必填)
                    dict.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));     //商户订单开始时间 yyyyMMddHHmmss(字符，长度14)(必填)
                    dict.Add("notifyUrl", notifyUrl);                                           //商户后台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                    dict.Add("frontNotifyUrl", frontNotifyUrl);                                 //商户前台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                    dict.Add("mhtCharset", "UTF-8");                                            //商户字符编码(字符，长度1-16)(必填)
                    dict.Add("deviceType", "0600");                                             //设备类型(06手机网页，0600微信公众号)(字符，长度2)(必填)
                    dict.Add("payChannelType", "13");                                           //用户所选渠道类型(如果为空则直接使用现在支付收银台页面，如果不为空则直接跳转至支付渠道（银联或者支付宝等）)(字符，长度2)
                    dict.Add("outputType", "1");                                                //输出类型（返回凭证 = 1）
                    dict.Add("mhtReserved", "");                                                //商户保留域(给商户使用的字段，商户可以对交易进行标记，现在支付将原样返回)(字符，长度100)
                    dict.Add("mhtSubOpenId", OpenId);
                    dict.Add("mhtSubAppId", WxPayConfig.APPID);
                    dict.Add("consumerId", StringHelper.ToString(currentUser.UserId));      //消费者ID(消费者在商户系统的ID，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                    dict.Add("consumerName", "");                                               //消费者名称(消费者在商户系统的名称，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                    dict.Add("mhtSignType", "MD5");                                             //商户签名方法(定值：MD5)(必填)
                    dict.Add("mhtSignature", GetSign(appKey, dict));                                    //商户数据签名(MAX64TEXT)(必填)

                    HttpSendInfo sendInfo = new HttpSendInfo();
                    sendInfo.Method = "POST";
                    sendInfo.Url = callbackUrl;
                    sendInfo.Data = GetPostData(dict);
                    HttpReceiveInfo receiveInfo = new HttpReceiveInfo();
                    if (HttpHelper.Send(sendInfo, out receiveInfo))
                    {
                        //报文日志
                        //Log(this.Server.UrlDecode(sendInfo.Data));

                        result = GetWxJsApiParam(receiveInfo.Result);
                    }
                }
            }

            ViewData.Model = result;

            return View();
        }

        private string GetWxJsApiParam(string result)
        {
            if (string.IsNullOrEmpty(result)) return "";

            //响应日志
            //Log(this.Server.UrlDecode(result));

            string json = "";
            SortedDictionary<string, object> sdict = new SortedDictionary<string, object>();

            Regex reg = new Regex("(?<begin>[?|&]?)responseCode=(?<param>[^?|&]*)(?<end>[&]?)", RegexOptions.IgnoreCase);
            if (reg.IsMatch(result))
            {
                Match match = reg.Match(result);
                string responseCode = match.Groups["param"].Value;
                if (string.Compare(responseCode, "A001", true) == 0)
                {
                    reg = new Regex("(?<begin>[?|&]?)tn=(?<param>[^?|&]*)(?<end>[&]?)", RegexOptions.IgnoreCase);
                    if (reg.IsMatch(result))
                    {
                        match = reg.Match(result);
                        string tn = match.Groups["param"].Value;
                        if (!string.IsNullOrEmpty(tn))
                        {
                            tn = this.Server.UrlDecode(tn);
                            reg = new Regex("(?<begin>[?|&]?)(?<paramname>[^=]*)=(?<paramvalue>[^?|&]*)(?<end>[&]?)", RegexOptions.IgnoreCase);
                            if (reg.IsMatch(tn))
                            {
                                string paramName = "";
                                string paramValue = "";
                                MatchCollection mc = reg.Matches(tn);
                                foreach (Match m in mc)
                                {
                                    paramName = m.Groups["paramname"].Value;
                                    paramValue = m.Groups["paramvalue"].Value;

                                    switch (paramName)
                                    {
                                        case "wxAppId":
                                            sdict.Add("appId", paramValue);
                                            break;

                                        case "timeStamp":
                                            sdict.Add("timeStamp", paramValue);
                                            break;

                                        case "nonceStr":
                                            sdict.Add("nonceStr", paramValue);
                                            break;

                                        case "prepay_id":
                                            sdict.Add("package", "prepay_id=" + paramValue + "");
                                            break;

                                        case "paySign":
                                            sdict.Add("paySign", paramValue);
                                            break;
                                    }
                                }

                                sdict.Add("signType", "MD5");
                            }
                        }
                    }
                }
            }

            if (!sdict.IsNullOrEmpty<string, object>())
            {
                json = JsonHelper.Serialize(sdict);
            }

            return json;
        }

        #endregion 现在支付-公众号

        #region 现在支付-扫码

        [Authorization]
        public ActionResult Index3()
        {
            string result = "";

            ErrorMessage errorMessage = ErrorMessage.失败;

            string url = StringHelper.GetHost();
            if (string.IsNullOrEmpty(url))
            {
                errorMessage = ErrorMessage.订单初始化失败;

                return Redirect(string.Format("/error/index?errCode={0}&returnUrl=", (int)errorMessage).GetChannelRouteUrl(RouteChannelId));
            }

            int payType = 0;
            string orderId = "";
            int money = 0;
            int userType = 0;
            if (PayContext.VerifyPayOrderInfo(10, out errorMessage, out payType, out orderId, out money, out userType)
                && payType == 13 && StringHelper.GetUserAgent().ToLower().Contains("micromessenger"))
            {
                string OpenId = UrlParameterHelper.GetParams("OpenId");
                if (!string.IsNullOrEmpty(OpenId))
                {
                    string appId = NowPayConfig.WeChatQrCodeAppId;
                    string appKey = NowPayConfig.WeChatQrCodeAppKey;
                    string callbackUrl = NowPayConfig.NewCallbackUrl;

                    string notifyUrl = string.Format("http://{0}/user/order/paynow/notify", url);
                    string frontNotifyUrl = string.Format("http://{0}/user/order/paynow/result", url);

                    SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
                    dict.Add("funcode", "WP001");                                               //功能码(定值：WP001)(必填)
                    dict.Add("appId", appId);                                                //商户应用唯一标识(字符，长度1-40)(必填)
                    dict.Add("mhtOrderNo", orderId);                                         //商户订单号(字符，长度1-40)(必填)
                    dict.Add("mhtOrderName", GetName(money));                                    //商户商品名称(字符，长度1-40)(必填)
                    dict.Add("mhtOrderType", "01");                                             //商户交易类型(01普通消费)(字符，长度1-40)(必填)
                    dict.Add("mhtCurrencyType", "156");                                         //商户订单币种类型(156人民币)(字符，长度3)(必填)
                    dict.Add("mhtOrderAmt", StringHelper.ToString(money));                      //商户订单交易金额(单位(人民币)：分)(数字，长度22)(必填)
                    dict.Add("mhtOrderDetail", "充值");                                         //商户订单详情(字符，长度0-200)(必填)
                    dict.Add("mhtOrderTimeOut", "3600");                                        //商户订单超时时间(60~3600秒，默认3600)(数字，长度4)(必填)
                    dict.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));     //商户订单开始时间 yyyyMMddHHmmss(字符，长度14)(必填)
                    dict.Add("notifyUrl", notifyUrl);                                           //商户后台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                    dict.Add("frontNotifyUrl", frontNotifyUrl);                                 //商户前台通知URL(HTTP协议或者HTTPS协议，POST方式提交报文)(字符，长度1-200)(必填)
                    dict.Add("mhtCharset", "UTF-8");                                            //商户字符编码(字符，长度1-16)(必填)
                    dict.Add("deviceType", "08");                                               //设备类型(06手机网页)(字符，长度2)(必填)
                    dict.Add("outputType", "1");                                                //输出类型（二维码 = 1）
                    dict.Add("payChannelType", payType.ToString());                          //用户所选渠道类型(如果为空则直接使用现在支付收银台页面，如果不为空则直接跳转至支付渠道（银联或者支付宝等）)(字符，长度2)
                    dict.Add("mhtReserved", "");                                                //商户保留域(给商户使用的字段，商户可以对交易进行标记，现在支付将原样返回)(字符，长度100)
                    dict.Add("consumerId", StringHelper.ToString(currentUser.UserId));      //消费者ID(消费者在商户系统的ID，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                    dict.Add("consumerName", "");                                               //消费者名称(消费者在商户系统的名称，非必填，但是推荐填写，以便于辅助数据分析)(字符，长度40)
                    dict.Add("mhtSignType", "MD5");                                             //商户签名方法(定值：MD5)(必填)
                    dict.Add("mhtSignature", GetSign(appKey, dict));                                    //商户数据签名(MAX64TEXT)(必填)

                    HttpSendInfo sendInfo = new HttpSendInfo();
                    sendInfo.Method = "POST";
                    sendInfo.Url = callbackUrl;
                    sendInfo.Data = GetPostData(dict);
                    HttpReceiveInfo receiveInfo = new HttpReceiveInfo();
                    if (HttpHelper.Send(sendInfo, out receiveInfo))
                    {
                        //报文日志
                        //Log(this.Server.UrlDecode(sendInfo.Data));

                        result = GetTnUrl3(receiveInfo.Result);
                    }
                }
            }

            ViewData.Model = result;
            ViewBag.OrderId = orderId;

            return View();
        }

        private string GetTnUrl3(string result)
        {
            if (string.IsNullOrEmpty(result)) return "";

            //响应日志
            //Log(this.Server.UrlDecode(result));

            string url = string.Empty;

            Regex reg = new Regex("(?<begin>[?|&])tn=(?<param>[^&]*)(?<end>[&]?)", RegexOptions.IgnoreCase);
            if (reg.IsMatch(result))
            {
                Match match = reg.Match(result);
                url = "/WxPay/MakeQRCode?data=" + match.Groups["param"].Value;
            }

            return url;
        }

        #endregion

        #region

        private string GetPostData(SortedDictionary<string, string> dict)
        {
            if (dict == null || dict.Count == 0) return "";

            string data = string.Empty;
            foreach (string key in dict.Keys)
            {
                //if (string.IsNullOrEmpty(dict[key])) continue;

                data += string.Concat(key, "=", HttpUtility.UrlEncode(dict[key], Encoding.UTF8), "&");
            }

            return data.TrimEnd('&');
        }

        private string GetSign(string appKey, SortedDictionary<string, string> dict)
        {
            if (dict == null || dict.Count == 0) return "";

            string sign = string.Empty;
            IList<string> list = new List<string>();
            list.Add("funcode");
            list.Add("deviceType");
            list.Add("mhtSignType");
            list.Add("mhtSignature");
            foreach (string key in dict.Keys)
            {
                if (list.Contains(key) || string.IsNullOrEmpty(dict[key])) continue;

                sign += string.Concat(key, "=", dict[key], "&");
            }

            return SecurityHelper.EncryptMD5(string.Concat(sign.TrimEnd('&'), "&", SecurityHelper.EncryptMD5(appKey).ToLower())).ToLower();
        }

        /// <summary>
        /// 获取订单名称
        /// </summary>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        private string GetName(int money)
        {
            return string.Concat("充值", ((decimal)money / (decimal)100), "元");
        }

        #endregion
    }
}