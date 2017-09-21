using Component.Auth;
using Model;
using Model.Common;
using System;
using Utility;

namespace Component.Controllers.Auth
{
    public class WeChatConnectController : ConnectController
    {
        #region

        protected string AutoLogin(string QrCodeAppId, string QrCodeAppKey, string AuthAppId, string AuthAppKey, string CallbackUrl)
        {
            string url = "";

            currentUser.State = Guid.NewGuid().ToString("N");

            SetReturnUrl(currentUser.State, "/".GetChannelRouteUrl(RouteChannelId));

            WeChatConnect connect = new WeChatConnect(QrCodeAppId, QrCodeAppKey, AuthAppId, AuthAppKey, currentUser.State, UrlParameterHelper.UrlEncode(CallbackUrl));
            if (connect != null)
            {
                SaveUserInfo(currentUser);
                connect.MicroMessengerScope = "snsapi_base";
                url = connect.GetAuthorizationCodeUrl("snsapi_login");
            }

            return url;
        }

        protected string Login(string QrCodeAppId, string QrCodeAppKey, string AuthAppId, string AuthAppKey, string CallbackUrl)
        {
            string url = "";

            currentUser.State = Guid.NewGuid().ToString("N");

            SetReturnUrl(currentUser.State, ReturnUrl);

            WeChatConnect connect = new WeChatConnect(QrCodeAppId, QrCodeAppKey, AuthAppId, AuthAppKey, currentUser.State, UrlParameterHelper.UrlEncode(CallbackUrl));
            if (connect != null)
            {
                SaveUserInfo(currentUser);
                url = connect.GetAuthorizationCodeUrl("snsapi_login");
            }

            return url;
        }

        protected string Notify(string QrCodeAppId, string QrCodeAppKey, string AuthAppId, string AuthAppKey, string CallbackUrl)
        {
            string url = "";

            if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(State) && string.Compare(State, currentUser.State, true) == 0)
            {
                WeChatConnect connect = new WeChatConnect(QrCodeAppId, QrCodeAppKey, AuthAppId, AuthAppKey, State, UrlParameterHelper.UrlEncode(CallbackUrl));

                if (connect != null && connect.GetAccessTokenByCode(Code)
                     && !string.IsNullOrEmpty(connect.AccessToken))
                {
                    currentUser.Token = connect.AccessToken;
                    string result = string.Empty;

                    WeChatConnect.UserInfo model = new WeChatConnect.UserInfo();
                    if (connect.GetUserInfo(out result, out model)
                         && !string.IsNullOrEmpty(connect.OpenId) && !string.IsNullOrEmpty(result) && string.IsNullOrEmpty(model.errcode))
                    {
                        currentUser.OpenId = connect.OpenId;
                        currentUser.NickName = model.nickname;
                        string nickName = string.Empty;

                        AccessUsersInfo accessUsersInfo = new AccessUsersInfo();
                        accessUsersInfo = GetClientLogInfo(accessUsersInfo) as AccessUsersInfo;
                        accessUsersInfo.AccessToken = currentUser.Token;
                        accessUsersInfo.UserName = "";
                        accessUsersInfo.NickName = currentUser.NickName;
                        accessUsersInfo.Email = "";
                        accessUsersInfo.Icon = model.headimgurl;
                        accessUsersInfo.Phone = "";
                        accessUsersInfo.Platform = Constants.AccessUserPlatform.wechat.ToString();
                        accessUsersInfo.OpenId = currentUser.OpenId;
                        accessUsersInfo.AccessToken = currentUser.Token;
                        accessUsersInfo.LoginInvalidTime = DateTime.Now;
                        accessUsersInfo.RecentLoginTime = DateTime.Now;
                        accessUsersInfo.AddTime = DateTime.Now;
                        accessUsersInfo.Status = (int)Constants.Status.yes;
                        accessUsersInfo.UnionId = model.unionid;
                        Users users = new Users();
                        users.Fee = 0;
                        users.UserName = CreateUserName(out nickName);
                        users.Icon = "";
                        users.Phone = "";
                        int rel = 0;
                        LoginedUsers loginedUsers = null;
                        if ((loginedUsers = _accessUsersService.RegisterLogin(accessUsersInfo, users, out rel)) != null && rel == (int)ErrorMessage.成功)
                        {
                            currentUser.UserId = loginedUsers.Id;
                            currentUser.UserName = loginedUsers.UserName;
                            currentUser.NickName = loginedUsers.NickName;
                            SaveUserInfo(currentUser);

                            //ChapterReadLogSync(loginedUsers.UserName, loginedUsers.Id);

                            url = GetReturnUrl(State);
                        }
                    }
                }
            }

            return url;
        }

        #endregion WeChat
    }
}