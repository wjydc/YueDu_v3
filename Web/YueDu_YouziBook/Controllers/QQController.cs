using Component.Controllers.Auth;
using Service;
using System.Web.Mvc;

namespace YueDu.Controllers
{
    public class QQController : QQConnectController
    {
        public QQController(IAccessUsersService accessUsersService)
        {
            _accessUsersService = accessUsersService;
        }

        #region

        public ActionResult Login()
        {
            string url = Login(AuthSection.QQ.AppId, AuthSection.QQ.AppKey, GetCallbackUrl(AuthSection.QQ.CallbackUrl));

            return AuthDefaultRedirect(url);
        }

        [Route("user/auth/qq/notify.aspx")]
        public ActionResult Notify()
        {
            string url = Notify(AuthSection.QQ.AppId, AuthSection.QQ.AppKey, GetCallbackUrl(AuthSection.QQ.CallbackUrl));

            return AuthSuccessRedirect(url);
        }

        #endregion QQ
    }
}