using Component.Controllers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Model.Common;

namespace YueDu.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int errCode = 0, string returnUrl = "")
        {
            string errorMsg = "";

            if (!string.IsNullOrEmpty(returnUrl))
                returnUrl = UrlParameterHelper.UrlDecode(returnUrl);

            ErrorMessage errorMessage = ErrorMessage.失败;
            if (EnumHelper.TryParsebyValue<ErrorMessage>(errCode, out errorMessage))
            {
                errorMsg = errorMessage.ToString();

                switch (errorMessage)
                {
                    case ErrorMessage.支付参数错误:

                        errorMsg = "( >﹏< )~支付参数出错了";
                        break;

                    case ErrorMessage.未知错误:

                        errorMsg = "( >﹏< )~页面出错了";
                        break;

                    case ErrorMessage.用户不存在:

                        errorMsg = "( >﹏< )~找不到用户信息";
                        break;

                    case ErrorMessage.小说不存在:

                        errorMsg = "( >﹏< )~该书籍不存在或已下架";
                        break;

                    case ErrorMessage.小说章节不存在:

                        errorMsg = "( >﹏< )~本章节不存在或已下架";
                        break;

                    case ErrorMessage.该章节统计字数有误无法购买:
                    case ErrorMessage.该小说无法按章计费:
                    case ErrorMessage.该小说无法按本计费:

                        errorMsg = "( >﹏< )~计费错误，请返回重新购买";
                        break;

                    case ErrorMessage.生成订单号失败:

                        errorMsg = "( >﹏< )~订单生成失败，返回再试试吧";
                        break;

                    default:

                        errorMsg = "404";
                        break;
                }
            }

            ViewBag.ErrorMessage = errorMsg;
            ViewBag.ReturnUrl = returnUrl;

            return View("/Views/Shared/Error.cshtml");
        }

        public ActionResult Index1(string errMessage = "", string returnUrl = "")
        {
            ViewBag.ErrorMessage = UrlParameterHelper.UrlDecode(errMessage);
            ViewBag.ReturnUrl = UrlParameterHelper.UrlDecode(returnUrl);

            return View("/Views/Shared/Error.cshtml");
        }

        public ActionResult NotFound()
        {
            ViewBag.ErrorMessage = "404";
            ViewBag.ReturnUrl = "";

            return View("/Views/Shared/Error.cshtml");
        }
    }
}