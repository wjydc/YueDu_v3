using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Component.Base
{
    public class PayContext
    {
        #region

        /// <summary>
        /// 加密订单号和金额
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        public static string EncryptOrderId(string orderId, int money)
        {
            if (!string.IsNullOrEmpty(orderId) && money > 0)
            {
                return EncryptOrderId('.', "1s2D536A", orderId, money);
            }

            return "";
        }

        #region

        /// <summary>
        /// 加密订单号和金额
        /// </summary>
        /// <param name="separator">订单号和金额分割符号</param>
        /// <param name="key">加密Key</param>
        /// <param name="orderId">订单号</param>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        public static string EncryptOrderId(char separator, string key, string orderId, int money)
        {
            if (!string.IsNullOrEmpty(orderId) && money > 0)
            {
                return SecurityHelper.EncryptBase64XorBase64Url(string.Concat(orderId, separator, money), key);
            }

            return "";
        }

        #endregion

        #endregion

        #region

        /// <summary>
        /// 验证充值订单信息
        /// </summary>
        /// <param name="minMoney">最低充值金额（如：0.1元、0.01元）</param>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="payType">充值类型</param>
        /// <param name="orderId">订单号</param>
        /// <param name="money">充值金额</param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static bool VerifyPayOrderInfo(int minMoney, out ErrorMessage errorMessage, out int payType, out string orderId, out int money, out int userType)
        {
            errorMessage = ErrorMessage.成功;

            payType = 0;
            orderId = string.Empty;
            money = StringHelper.ToInt(UrlParameterHelper.GetParams("money"));      //单位：分
            userType = StringHelper.ToInt(UrlParameterHelper.GetParams("ut"));      //默认：至少20元；大于0：任意金额；

            string pt = UrlParameterHelper.GetParams("pt");
            string otn = UrlParameterHelper.GetParams("otn");

            int orderMoney = 0;
            if (!VerifyOrderId(otn, money, out orderId, out orderMoney))
            {
                errorMessage = ErrorMessage.支付参数错误;
            }
            else if (!string.IsNullOrEmpty(pt) && int.TryParse(pt, out payType) && payType <= 0)
            {
                errorMessage = ErrorMessage.支付类型为空;
            }
            else if (money <= 0)
            {
                errorMessage = ErrorMessage.金额为空;
            }
            else if (money <= 1999)
            {
                errorMessage = ErrorMessage.最低充值20元;
            }
            else
            {
                money = (userType == 0 ? money : minMoney);
            }

            return (errorMessage == ErrorMessage.成功);
        }

        #region

        /// <summary>
        /// 验证订单号和金额
        /// </summary>
        /// <param name="otn">加密串</param>
        /// <param name="paramMoney">金额（Url参数）</param>
        /// <param name="orderId">订单号</param>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        public static bool VerifyOrderId(string otn, int paramMoney, out string orderId, out int money)
        {
            orderId = string.Empty;
            money = 0;

            if (!string.IsNullOrEmpty(otn))
            {
                return (VerifyOrderId(otn, '.', "1s2D536A", out orderId, out money) && money == paramMoney);
            }

            return false;
        }

        /// <summary>
        /// 验证订单号和金额
        /// </summary>
        /// <param name="otn">加密串</param>
        /// <param name="separator">订单号和金额分割符号</param>
        /// <param name="key">加密Key</param>
        /// <param name="orderId">订单号</param>
        /// <param name="money">金额（单位：分）</param>
        /// <returns></returns>
        public static bool VerifyOrderId(string otn, char separator, string key, out string orderId, out int money)
        {
            orderId = string.Empty;
            money = 0;

            bool flag = false;

            if (!string.IsNullOrEmpty(otn))
            {
                otn = SecurityHelper.DecryptBase64XorBase64Url(otn, key);

                if (!string.IsNullOrEmpty(otn))
                {
                    try
                    {
                        string[] list = otn.Split(separator);
                        if (list.Length == 2)
                        {
                            orderId = list[0];
                            money = StringHelper.ToInt(list[1]);

                            flag = (!string.IsNullOrEmpty(orderId) && money > 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        DataContext.ErrorLog("PayContext", "VerifyOrderId", "", string.Concat(ex.Message, " ", ex.StackTrace));
                    }
                }
            }

            return flag;
        }

        #endregion

        #endregion
    }
}