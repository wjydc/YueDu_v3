using Component.Controllers.Auth;
using Service;
using System.Web.Mvc;

namespace YueDu.Controllers
{
    public class WeiboController : WeiboConnectController
    {
        public WeiboController(IAccessUsersService accessUsersService)
        {
            _accessUsersService = accessUsersService;
        }

        #region

        public ActionResult Login()
        {
            string url = Login(AuthSection.Weibo.AppId, AuthSection.Weibo.AppKey, GetCallbackUrl(AuthSection.Weibo.CallbackUrl));

            return AuthDefaultRedirect(url);
        }

        [Route("user/auth/weibo/notify.aspx")]
        public ActionResult Notify()
        {
            string url = Notify(AuthSection.Weibo.AppId, AuthSection.Weibo.AppKey, GetCallbackUrl(AuthSection.Weibo.CallbackUrl));

            return AuthSuccessRedirect(url);
        }

        #endregion Weibo
    }
}