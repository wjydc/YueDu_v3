using Com.NowPay;
using Component.Controllers.User;
using Model;
using Model.Common;
using Service;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace YueDu.Controllers
{
    public class NotifyController : UserInfoController
    {
        private IRechargeService _rechargeService;

        public NotifyController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }

        #region 微信支付

        [ValidateInput(false)]
        [Route("user/order/wechat/notify.aspx")]
        public void WxPayNotify()
        {
            Stream stream = Request.InputStream;
            Com.WxPayAPI.ResultNotify nativeNatify = new Com.WxPayAPI.ResultNotify(_rechargeService, stream);

            Com.WxPayAPI.WxPayData data = null;
            nativeNatify.ProcessNotify(out data);
            if (data != null)
            {
                string xml = data.ToXml();
                if (!string.IsNullOrEmpty(xml))
                {
                    Response.Write(xml);
                }
            }
        }

        [ValidateInput(false)]
        [Route("user/order/wechat/notify1")]
        public void WxPayNotify1()
        {
            Stream stream = Request.InputStream;
            Com.WxPayAPI2.ResultNotify nativeNatify = new Com.WxPayAPI2.ResultNotify(_rechargeService, stream);

            Com.WxPayAPI2.WxPayData data = null;
            nativeNatify.ProcessNotify(out data);
            if (data != null)
            {
                string xml = data.ToXml();
                if (!string.IsNullOrEmpty(xml))
                {
                    Response.Write(xml);
                }
            }
        }

        #endregion 微信支付

        #region 现在支付以及现在支付-手机网页（异步通知）

        [Route("user/order/paynow/notify")]
        public void NowPayNotify()
        {
            string errorMessage = "fail";

            try
            {
                string appKey = NowPayConfig.AppKey;

                SortedDictionary<string, string> sPara = GetRequestPost();
                if (sPara.Count > 0)
                {
                    if (string.Compare(GetSign(appKey, sPara), sPara["signature"], true) == 0)
                    {
                        string mhtOrderNo = sPara["mhtOrderNo"];
                        decimal mhtOrderAmt = StringHelper.ToDecimal(sPara["mhtOrderAmt"]);
                        string tradeStatus = sPara["tradeStatus"];
                        if (string.Compare(tradeStatus, "A001", true) == 0)
                        {
                            int result = 0;
                            RechargeInfo model = new RechargeInfo();
                            model.OrderId = mhtOrderNo;
                            model.FOrderId = "";
                            model.Cash = mhtOrderAmt;
                            model.PayMobile = "";
                            _rechargeService.Completed(model, out result);     //1：成功 0：失败
                            if (result == (int)ErrorMessage.成功)
                            {
                                errorMessage = "success";
                            }
                        }
                    }
                }
            }
            catch { }

            Response.Write(errorMessage);
        }

        [Route("user/order/paynow/result")]
        public ActionResult NowPayResult()
        {
            return Redirect("/recentread/index".GetChannelRouteUrl(RouteChannelId));
        }

        #endregion 现在支付以及现在支付-手机网页（异步通知）

        #region 现在支付-公众号（异步通知）

        [Route("user/order/paynow/notify1")]
        public void NowPayNotify1()
        {
            string errorMessage = "fail";

            try
            {
                string appKey = NowPayConfig.WeChatAppKey;

                SortedDictionary<string, string> sPara = GetRequestPost();
                if (sPara.Count > 0)
                {
                    if (string.Compare(GetSign(appKey, sPara), sPara["signature"], true) == 0)
                    {
                        string mhtOrderNo = sPara["mhtOrderNo"];
                        decimal mhtOrderAmt = StringHelper.ToDecimal(sPara["mhtOrderAmt"]);
                        string tradeStatus = sPara["tradeStatus"];
                        if (string.Compare(tradeStatus, "A001", true) == 0)
                        {
                            int result = 0;
                            RechargeInfo model = new RechargeInfo();
                            model.OrderId = mhtOrderNo;
                            model.FOrderId = "";
                            model.Cash = mhtOrderAmt;
                            model.PayMobile = "";
                            _rechargeService.Completed(model, out result);     //1：成功 0：失败
                            if (result == (int)ErrorMessage.成功)
                            {
                                errorMessage = "success";
                            }
                        }
                    }
                }
            }
            catch { }

            Response.Write(errorMessage);
        }

        [Route("user/order/paynow/result1")]
        public ActionResult NowPayResult1()
        {
            return Redirect("/recentread/index".GetChannelRouteUrl(RouteChannelId));
        }

        #endregion 现在支付-公众号（异步通知）

        #region 现在支付-扫码（异步通知）

        [Route("user/order/paynow/notify3")]
        public void NowPayNotify3()
        {
            string errorMessage = "fail";

            try
            {
                string appKey = NowPayConfig.WeChatQrCodeAppKey;

                SortedDictionary<string, string> sPara = GetRequestPost();
                if (sPara.Count > 0)
                {
                    if (string.Compare(GetSign(appKey, sPara), sPara["signature"], true) == 0)
                    {
                        string mhtOrderNo = sPara["mhtOrderNo"];
                        string tradeStatus = sPara["tradeStatus"];
                        if (string.Compare(tradeStatus, "A001", true) == 0)
                        {
                            int result = 0;
                            RechargeInfo model = new RechargeInfo();
                            model.OrderId = mhtOrderNo;
                            model.FOrderId = "";
                            model.Cash = 0;
                            model.PayMobile = "";
                            _rechargeService.Completed(model, out result);     //1：成功 0：失败
                            if (result == (int)ErrorMessage.成功)
                            {
                                errorMessage = "success";
                            }
                        }
                    }
                }
            }
            catch { }

            Response.Write(errorMessage);
        }

        [Route("user/order/paynow/result3")]
        public ActionResult NowPayResult3()
        {
            return Redirect("/recentread/index".GetChannelRouteUrl(RouteChannelId));
        }

        #endregion 现在支付-扫码（异步通知）

        #region

        private string GetSign(string appKey, SortedDictionary<string, string> dict)
        {
            if (dict == null || dict.Count == 0) return "";

            string sign = string.Empty;
            IList<string> list = new List<string>();
            list.Add("signType");
            list.Add("signature");
            foreach (string key in dict.Keys)
            {
                if (list.Contains(key) || string.IsNullOrEmpty(dict[key])) continue;

                sign += string.Concat(key, "=", dict[key], "&");
            }

            return SecurityHelper.EncryptMD5(string.Concat(sign.TrimEnd('&'), "&", SecurityHelper.EncryptMD5(appKey).ToLower())).ToLower();
        }

        #endregion

        #region

        public SortedDictionary<string, string> GetRequestPost()
        {
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();

            string value = string.Empty;
            using (Stream stream = Request.InputStream)
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    value = reader.ReadToEnd();
                }
            }

            if (!string.IsNullOrEmpty(value))
            {
                Regex reg = new Regex("(?<name>[^=|&]*)=(?<value>[^&]*)", RegexOptions.IgnoreCase);
                if (reg.IsMatch(value))
                {
                    foreach (Match item in reg.Matches(value))
                    {
                        sArray.Add(item.Groups["name"].Value, HttpUtility.UrlDecode(item.Groups["value"].Value, Encoding.UTF8));
                    }
                }
            }

            return sArray;
        }

        #endregion

        //支付失败
        public ActionResult PayError()
        {
            return View();
        }
        //联系客服
        public ActionResult Contact()
        {
            return View();
        }
    }
}