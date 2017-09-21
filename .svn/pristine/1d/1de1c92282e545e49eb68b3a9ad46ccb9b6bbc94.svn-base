using Component.Base;
using Component.Controllers.User;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using Utility;

namespace Component.Controllers.Pay
{
    public class PrePayController : LoginedController
    {
        protected IRechargeService _rechargeService;

        #region

        /// <summary>
        /// 绑定充值类型
        /// </summary>
        /// <param name="payType">充值类型</param>
        /// <param name="money">金额（单位：分）</param>
        /// <param name="url">支付地址</param>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="fPayType">充值类型（第三方）</param>
        /// <param name="feeConfigId">资费配置Id（赠送）</param>
        /// <param name="brief">备注</param>
        /// <param name="userType"></param>
        protected virtual void BindPayType(int payType, int money, out string url, out ErrorMessage errorMessage, string fPayType = "", int feeConfigId = 0, string brief = "", int userType = 0)
        {
            url = string.Empty;
            errorMessage = ErrorMessage.失败;

            string merId = string.Empty;
            if (GetPayOrderInfo(payType, money, out merId, out url, fPayType, userType))
            {
                string otn = string.Empty;
                if (GetOrderToken(payType, merId, money, out otn, feeConfigId, brief, userType))
                {
                    url = url.SpliceUrl("otn", otn);
                    if (!string.IsNullOrEmpty(url))
                    {
                        errorMessage = ErrorMessage.成功;
                    }
                }
                else
                {
                    errorMessage = ErrorMessage.生成订单号失败;
                }
            }
            else
            {
                errorMessage = ErrorMessage.支付参数错误;
            }
        }

        /// <summary>
        /// 获取充值信息
        /// </summary>
        /// <param name="payType"></param>
        /// <param name="fPayType"></param>
        /// <param name="money"></param>
        /// <param name="merId"></param>
        /// <param name="url"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        protected virtual bool GetPayOrderInfo(int payType, int money, out string merId, out string url, string fPayType = "", int userType = 0)
        {
            merId = string.Empty;
            url = string.Empty;

            if (money > 0 && payType > 0)
            {
                IDictionary<string, object> paramList = null;

                switch (payType)
                {
                    case (int)Constants.PayType.paynow:
                    case (int)Constants.PayType.paynow_alipay:
                    case (int)Constants.PayType.paynow_wechat:
                    case (int)Constants.PayType.paynow_bank:
                    case (int)Constants.PayType.paynow_pointcard:
                    case (int)Constants.PayType.paynow_rechargecard:
                        if (!string.IsNullOrEmpty(fPayType))
                        {
                            paramList = new Dictionary<string, object>();
                            paramList.Add("pt", fPayType);
                        }
                        break;
                }

                return GetPayOrderInfo(payType, money, out merId, out url, paramList, userType);
            }

            return false;
        }

        /// <summary>
        /// 获取充值信息
        /// </summary>
        /// <param name="payType"></param>
        /// <param name="paramList"></param>
        /// <param name="money"></param>
        /// <param name="merId"></param>
        /// <param name="url"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        protected virtual bool GetPayOrderInfo(int payType, int money, out string merId, out string url, IDictionary<string, object> paramList = null, int userType = 0)
        {
            merId = string.Empty;
            url = string.Empty;

            if (money > 0 && payType > 0)
            {
                switch (payType)
                {
                    case (int)Constants.PayType.wechat:
                        merId = "3333";
                        url = string.Format("/user/order/wechat/js/open.aspx?ut={0}&money={1}", userType, money);
                        break;

                    case (int)Constants.PayType.wechat_qr:
                        merId = "7777";
                        url = string.Format("/WxPay/Index1?ut={0}&money={1}", userType, money);
                        break;

                    case (int)Constants.PayType.paynow:
                    case (int)Constants.PayType.paynow_alipay:
                    case (int)Constants.PayType.paynow_wechat:
                    case (int)Constants.PayType.paynow_bank:
                    case (int)Constants.PayType.paynow_pointcard:
                    case (int)Constants.PayType.paynow_rechargecard:
                        merId = "5555";
                        url = string.Format("/NowPay/Open?ut={0}&money={1}", userType, money);
                        break;

                    case (int)Constants.PayType.paynow_wechatqr:
                        merId = "5555";
                        url = string.Format("/NowPay/Index3?ut={0}&money={1}", userType, money);
                        break;

                    case (int)Constants.PayType.alipay:
                        merId = "1111";
                        url = string.Format("/Alipay/Index?ut={0}&money={1}", userType, money);
                        break;

                    default:
                        merId = "";
                        url = "";
                        break;
                }

                if (!paramList.IsNullOrEmpty<string, object>())
                {
                    url = url.SpliceUrl(paramList);
                }

                return (!string.IsNullOrEmpty(merId) && payType > 0 && !string.IsNullOrEmpty(url));
            }

            return false;
        }

        private bool GetOrderToken(int payType, string merId, int money, out string otn, int feeConfigId = 0, string brief = "", int userType = 0)
        {
            otn = "";

            string orderId = "";
            if (GetOrderId(payType, merId, money, out orderId, feeConfigId, brief, userType))
            {
                otn = PayContext.EncryptOrderId(orderId, money);
            }

            return !string.IsNullOrEmpty(otn);
        }

        #endregion

        #region 生成订单号

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="payType">充值类型</param>
        /// <param name="merId">商户号</param>
        /// <param name="money">金额（单位：分）</param>
        /// <param name="orderId">订单号</param>
        /// <param name="feeConfigId">资费配置Id（赠送）</param>
        /// <param name="brief">备注</param>
        /// <param name="userType"></param>
        /// <returns></returns>
        protected virtual bool GetOrderId(int payType, string merId, int money, out string orderId, int feeConfigId = 0, string brief = "", int userType = 0)
        {
            orderId = string.Empty;

            string name = string.Empty;
            int fee = 0;

            if (!string.IsNullOrEmpty(merId) && payType > 0
                && GetOrderFee(payType, money, out name, out fee, userType))
            {
                try
                {
                    RechargeInfo model = new RechargeInfo();
                    model = GetLogInfo(model) as RechargeInfo;
                    model.MerchantId = merId;
                    model.FOrderId = "";
                    model.Name = name;
                    model.UserId = 0;
                    model.UserName = currentUser.UserName;
                    model.NickName = "";
                    model.Fee = fee;
                    model.RebateFee = 0;
                    model.RebateExpression = "";
                    model.Balance = 0;
                    model.Cash = fee;
                    model.Integral = 0;
                    model.PayType = payType;
                    model.PayMobile = "";
                    model.SpNumber = "";
                    model.SMSOrder = "";
                    model.MerchantPrivate = "";
                    model.MerchantExpand = "";
                    model.Brief = brief;
                    model.Status = (int)Constants.Status.no;
                    model.CompleteTime = DateTime.Now;
                    model.AddTime = DateTime.Now;
                    model.PromotionCode = HeaderInfo.PromotionCode;

                    int result = 0;
                    RechargeInfo rechargeInfo = _rechargeService.Generate(model, (userType == 0 ? feeConfigId : 0), (int)Constants.PayType.sms, out result);
                    if (result == (int)ErrorMessage.成功 && rechargeInfo != null)
                    {
                        orderId = rechargeInfo.OrderId;
                    }
                }
                catch (Exception ex)
                {
                    Log(ex, "");
                }
            }

            return (!string.IsNullOrEmpty(orderId));
        }

        #region 获取订单金额

        /// <summary>
        /// 获取订单金额
        /// </summary>
        /// <param name="payType">充值类型</param>
        /// <param name="money">金额（单位：分）</param>
        /// <param name="name">订单名称</param>
        /// <param name="fee">订单金额（虚拟币=单位：分）</param>
        /// <param name="userType"></param>
        /// <returns></returns>
        protected virtual bool GetOrderFee(int payType, int money, out string name, out int fee, int userType = 0)
        {
            name = string.Empty;
            fee = 0;

            if (userType == 0)
            {
                fee = money;
            }
            else
            {
                switch (payType)
                {
                    case (int)Constants.PayType.alipay:
                    case (int)Constants.PayType.wechat_qr:
                    case (int)Constants.PayType.wechat:
                        fee = 1;
                        break;

                    case (int)Constants.PayType.paynow:
                    case (int)Constants.PayType.paynow_alipay:
                    case (int)Constants.PayType.paynow_wechat:
                    case (int)Constants.PayType.paynow_bank:
                    case (int)Constants.PayType.paynow_pointcard:
                    case (int)Constants.PayType.paynow_rechargecard:
                        fee = 10;
                        break;
                }
            }

            if (fee > 0)
            {
                name = string.Concat("充值", ((decimal)fee / (decimal)100), "元");
            }

            return (!string.IsNullOrEmpty(name) && fee > 0);
        }

        #endregion

        #region 订单扩展（私有）信息

        /// <summary>
        /// 扩展信息
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMerExpand()
        {
            return string.Empty;
        }

        /// <summary>
        /// 私有信息
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMerPriv()
        {
            return GetMerPriv(5, 20);
        }

        /// <summary>
        /// 私有信息
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected string GetMerPriv(int startIndex, int length)
        {
            string value = Guid.NewGuid().ToString("N");

            if (!string.IsNullOrEmpty(value))
            {
                value = value.ToUpper();

                if (value.Length >= startIndex + length)
                {
                    value = value.Substring(startIndex, length);
                }
                else
                {
                    value = string.Empty;
                }
            }

            return value;
        }

        #endregion

        #endregion
    }
}