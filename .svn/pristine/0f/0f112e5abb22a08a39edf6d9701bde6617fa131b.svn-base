using Model;
using Model.Common;
using Service;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Utility;

namespace Com.WxPayAPI
{
    /// <summary>
    /// 扫码支付模式一回调处理类
    /// 接收微信支付后台发送的扫码结果，调用统一下单接口并将下单结果返回给微信支付后台
    /// </summary>
    public class NativeNotify : Notify
    {
        protected IRechargeService _rechargeService;

        public NativeNotify(IRechargeService rechargeService, Stream stream)
            : base(stream)
        {
            _rechargeService = rechargeService;
        }

        public override void ProcessNotify(out WxPayData data)
        {
            if (GetNotifyData(out data))
            {
                //Log.Info(this.GetType().ToString(), "ProcessNotify() out_trade_no = " + data.GetValue("out_trade_no").ToString());
                //Log.Info(this.GetType().ToString(), "ProcessNotify() isset openid = " + data.IsSet("openid"));
                //Log.Info(this.GetType().ToString(), "ProcessNotify() openid = " + data.GetValue("openid").ToString());
                //Log.Info(this.GetType().ToString(), "ProcessNotify() isset product_id = " + data.IsSet("product_id"));
                //Log.Info(this.GetType().ToString(), "ProcessNotify() product_id = " + data.GetValue("product_id").ToString());

                //检查openid和product_id是否返回
                if (!data.IsSet("openid") || !data.IsSet("product_id"))
                {
                    data = new WxPayData();
                    data.SetValue("return_code", "FAIL");
                    data.SetValue("return_msg", "回调数据异常");

                    Log.Info(this.GetType().ToString(), "The data WeChat post is error : " + data.ToXml());

                    return;
                }

                //调统一下单接口，获得下单结果
                string openid = data.GetValue("openid").ToString();
                string product_id = data.GetValue("product_id").ToString();
                WxPayData unifiedOrderResult = new WxPayData();
                try
                {
                    unifiedOrderResult = UnifiedOrder(openid, product_id);
                }
                catch   //若在调统一下单接口时抛异常，立即返回结果给微信支付后台
                {
                    data = new WxPayData();
                    data.SetValue("return_code", "FAIL");
                    data.SetValue("return_msg", "统一下单失败");

                    Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + data.ToXml());

                    return;
                }

                //若下单失败，则立即返回结果给微信支付后台
                if (!unifiedOrderResult.IsSet("appid") || !unifiedOrderResult.IsSet("mch_id") || !unifiedOrderResult.IsSet("prepay_id"))
                {
                    data = new WxPayData();
                    data.SetValue("return_code", "FAIL");
                    data.SetValue("return_msg", "统一下单失败");

                    Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + data.ToXml());

                    return;
                }

                #region

                string mhtOrderNo = data.GetValue("out_trade_no").ToString();
                decimal total_fee = StringHelper.ToDecimal(data.GetValue("total_fee"));
                if (!string.IsNullOrEmpty(mhtOrderNo))
                {
                    int result = 0;
                    RechargeInfo model = new RechargeInfo();
                    model.OrderId = mhtOrderNo;
                    model.FOrderId = "";
                    model.Cash = total_fee;
                    model.PayMobile = "";
                    _rechargeService.Completed(model, out result);     //1：成功 0：失败
                    if (result != (int)ErrorMessage.成功)
                    {
                        data = new WxPayData();
                        data.SetValue("return_code", "FAIL");
                        data.SetValue("return_msg", "统一下单失败");

                        Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + data.ToXml());

                        return;
                    }
                }

                #endregion

                //统一下单成功,则返回成功结果给微信支付后台
                data = new WxPayData();
                data.SetValue("return_code", "SUCCESS");
                data.SetValue("return_msg", "OK");
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("mch_id", WxPayConfig.MCHID);
                data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
                data.SetValue("prepay_id", unifiedOrderResult.GetValue("prepay_id"));
                data.SetValue("result_code", "SUCCESS");
                data.SetValue("err_code_des", "OK");
                data.SetValue("sign", data.MakeSign());

                Log.Info(this.GetType().ToString(), "UnifiedOrder success , send data to WeChat : " + data.ToXml());
            }
        }

        private WxPayData UnifiedOrder(string openId, string productId)
        {
            //统一下单
            WxPayData req = new WxPayData();
            req.SetValue("body", "test");
            req.SetValue("attach", "test");
            req.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());
            req.SetValue("total_fee", 1);
            req.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            req.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            req.SetValue("goods_tag", "test");
            req.SetValue("trade_type", "NATIVE");
            req.SetValue("openid", openId);
            req.SetValue("product_id", productId);
            WxPayData result = WxPayApi.UnifiedOrder(req);
            return result;
        }
    }
}