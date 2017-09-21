using System.Web.Mvc;
using Utility;

namespace Component.Controllers.User
{
    public class LoginedController : UserInfoController
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!IsLogin())
            {
                string url = StringHelper.GetReturnUrl("/login", channelId: RouteChannelId);
                filterContext.Result = new RedirectResult(url);
                return;
            }
        }
    }
}