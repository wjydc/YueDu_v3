using Component.Auth;
using Model;
using Model.Common;
using System;
using Utility;

namespace Component.Controllers.Auth
{
    public class QQConnectController : ConnectController
    {
        #region

        protected string Login(string AppId, string AppKey, string CallbackUrl)
        {
            string url = "";

            currentUser.State = Guid.NewGuid().ToString("N");

            SetReturnUrl(currentUser.State, ReturnUrl);

            QQConnect connect = new QQConnect(AppId, AppKey, currentUser.State, UrlParameterHelper.UrlEncode(CallbackUrl));
            if (connect != null)
            {
                SaveUserInfo(currentUser);
                url = connect.GetAuthorizationCodeUrl("get_user_info");
            }

            return url;
        }

        protected string Notify(string AppId, string AppKey, string CallbackUrl, int union = 0)
        {
            string url = "";

            if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(State) && string.Compare(State, currentUser.State, true) == 0)
            {
                QQConnect connect = new QQConnect(AppId, AppKey, State, UrlParameterHelper.UrlEncode(CallbackUrl));

                if (connect != null && connect.GetAccessTokenByCode(Code) && connect.GetOpenIdByToken(union)
                    && !string.IsNullOrEmpty(connect.AccessToken) && !string.IsNullOrEmpty(connect.OpenId))
                {
                    currentUser.Token = connect.AccessToken;
                    currentUser.OpenId = connect.OpenId;
                    string result = string.Empty;
                    QQConnect.UserInfo model = new QQConnect.UserInfo();
                    if (connect.GetUserInfo(out result, out model) && !string.IsNullOrEmpty(result) && string.Compare(model.ret, "0", true) == 0)
                    {
                        currentUser.NickName = model.nickname;
                        string nickName = string.Empty;

                        AccessUsersInfo accessUsersInfo = new AccessUsersInfo();
                        accessUsersInfo = GetClientLogInfo(accessUsersInfo) as AccessUsersInfo;
                        accessUsersInfo.AccessToken = currentUser.Token;
                        accessUsersInfo.UserName = "";
                        accessUsersInfo.NickName = currentUser.NickName;
                        accessUsersInfo.Email = "";
                        accessUsersInfo.Icon = model.figureurl_qq_2;
                        accessUsersInfo.Phone = "";
                        accessUsersInfo.Platform = Constants.AccessUserPlatform.qq.ToString();
                        accessUsersInfo.OpenId = currentUser.OpenId;
                        accessUsersInfo.AccessToken = currentUser.Token;
                        accessUsersInfo.LoginInvalidTime = DateTime.Now;
                        accessUsersInfo.RecentLoginTime = DateTime.Now;
                        accessUsersInfo.AddTime = DateTime.Now;
                        accessUsersInfo.Status = (int)Constants.Status.yes;
                        if (union == 1)
                        {
                            accessUsersInfo.UnionId = connect.UnionId;
                        }
                        Users users = new Users();
                        users.Fee = 0;
                        users.UserName = CreateUserName(out nickName);
                        users.Icon = "";
                        users.Phone = "";
                        int rel = 0;
                        if ((rel = _accessUsersService.Register(accessUsersInfo, users)) == (int)ErrorMessage.成功)
                        {
                            LoginedAccessUsers loginedAccessUsers = _accessUsersService.AccessLogin(accessUsersInfo);
                            if (loginedAccessUsers != null && loginedAccessUsers.Id > 0)
                            {
                                currentUser.UserId = loginedAccessUsers.Id;
                                currentUser.UserName = loginedAccessUsers.UserName;
                                currentUser.NickName = loginedAccessUsers.NickName;
                                SaveUserInfo(currentUser);

                                //ChapterReadLogSync(loginedAccessUsers.UserName, loginedAccessUsers.Id);

                                url = GetReturnUrl(State);
                            }
                        }
                    }
                }
            }

            return url;
        }

        #endregion
    }
}