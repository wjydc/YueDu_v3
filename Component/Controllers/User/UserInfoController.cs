using Component.Base;
using Component.Controllers.Log;
using Model.Common;
using Service;
using System;
using System.Web.Mvc;
using Utility;

namespace Component.Controllers.User
{
    public class UserInfoController : CreateUserController
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            bool isMicroMessenger = StringHelper.GetUserAgent().ToLower().Contains("micromessenger");
            if (isMicroMessenger && SiteSection.Auth.IsAutoLoginInMicroMessenger && !IsLogin())
            {
                string url = "/wechat/autologin";
                filterContext.Result = new RedirectResult(url);
                return;
            }
        }
    }
}