using Component.Controllers.Auth;
using Service;
using System.Web.Mvc;

namespace YueDu.Controllers
{
    public class WeChatController : WeChatConnectController
    {
        public WeChatController(IAccessUsersService accessUsersService)
        {
            _accessUsersService = accessUsersService;
        }

        #region

        public ActionResult AutoLogin()
        {
            string url = AutoLogin(AuthSection.WeChat.QrCodeAppId, AuthSection.WeChat.QrCodeAppKey, AuthSection.WeChat.AppId, AuthSection.WeChat.AppKey, GetCallbackUrl(AuthSection.WeChat.CallbackUrl));

            return AuthDefaultRedirect(url);
        }

        public ActionResult Login()
        {
            string url = Login(AuthSection.WeChat.QrCodeAppId, AuthSection.WeChat.QrCodeAppKey, AuthSection.WeChat.AppId, AuthSection.WeChat.AppKey, GetCallbackUrl(AuthSection.WeChat.CallbackUrl));

            return AuthDefaultRedirect(url);
        }

        [Route("user/auth/wechat/notify.aspx")]
        public ActionResult Notify()
        {
            string url = Notify(AuthSection.WeChat.QrCodeAppId, AuthSection.WeChat.QrCodeAppKey, AuthSection.WeChat.AppId, AuthSection.WeChat.AppKey, GetCallbackUrl(AuthSection.WeChat.CallbackUrl));

            return AuthSuccessRedirect(url);
        }

        public ActionResult ScanLogin()
        {
            return View();
        }

        #endregion WeChat
    }
}