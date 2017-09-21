using Model;
using Model.Common;
using Service;
using System.IO;
using System.Web;
using System.Web.UI;
using Utility;

namespace Com.WxPayAPI
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        protected IRechargeService _rechargeService;

        public ResultNotify(IRechargeService rechargeService, Stream stream)
            : base(stream)
        {
            _rechargeService = rechargeService;
        }

        public override void ProcessNotify(out WxPayData data)
        {
            if (GetNotifyData(out data))
            {
                //检查支付结果中transaction_id是否存在
                if (!data.IsSet("transaction_id"))
                {
                    //若transaction_id不存在，则立即返回结果给微信支付后台
                    data = new WxPayData();
                    data.SetValue("return_code", "FAIL");
                    data.SetValue("return_msg", "支付结果中微信订单号不存在");

                    Log.Error(this.GetType().ToString(), "The Pay result is error : " + data.ToXml());

                    return;
                }

                string transaction_id = data.GetValue("transaction_id").ToString();

                //查询订单，判断订单真实性
                if (!QueryOrder(transaction_id))
                {
                    //若订单查询失败，则立即返回结果给微信支付后台
                    data = new WxPayData();
                    data.SetValue("return_code", "FAIL");
                    data.SetValue("return_msg", "订单查询失败");

                    Log.Error(this.GetType().ToString(), "Order query failure : " + data.ToXml());
                }
                //查询订单成功
                else
                {
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
                            //失败
                            data = new WxPayData();
                            data.SetValue("return_code", "FAIL");
                            data.SetValue("return_msg", "订单查询失败");

                            Log.Error(this.GetType().ToString(), "Order query failure : " + data.ToXml());

                            return;
                        }
                    }

                    //成功
                    data.SetValue("return_code", "SUCCESS");
                    data.SetValue("return_msg", "OK");

                    Log.Info(this.GetType().ToString(), "order query success : " + data.ToXml());
                }
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}