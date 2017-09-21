using Component.Controllers.Pay;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace YueDu.Controllers
{
    public class RechargeController : PrePayController
    {
        public RechargeController(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }

        public ActionResult Pay(int? pt = 0, int? money = 0, string fpt = "", int? fcid = 0, int? ut = 0)
        {
            string url = string.Empty;
            ErrorMessage errorMessage = ErrorMessage.失败;
            BindPayType(pt.ToInt(), money.ToInt(), out url, out errorMessage, fpt, fcid.ToInt(), userType: ut.ToInt());
            if (errorMessage == ErrorMessage.成功)
            {
                string returnUrl = UrlParameterHelper.GetDecodingParams("returnUrl");
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    url = StringHelper.GetReturnUrl(url, returnUrl);
                }
                return Redirect(url);
            }
            //else if (errorMessage == ErrorMessage.支付参数错误)
            //{
            //
            //}
            //else if (errorMessage == ErrorMessage.生成订单号失败)
            //{
            //
            //}
            else
            {
                return Redirect(string.Format("/error/index?errCode={0}&returnUrl=", (int)errorMessage).GetChannelRouteUrl(RouteChannelId));
            }
        }
    }
}