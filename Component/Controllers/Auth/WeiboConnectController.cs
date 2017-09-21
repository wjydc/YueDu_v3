using Component.Auth;
using Model;
using Model.Common;
using System;
using Utility;

namespace Component.Controllers.Auth
{
    public class WeiboConnectController : ConnectController
    {
        #region

        protected string Login(string AppId, string AppKey, string CallbackUrl)
        {
            string url = "";

            currentUser.State = Guid.NewGuid().ToString("N");

            SetReturnUrl(currentUser.State, ReturnUrl);

            WeiboConnect connect = new WeiboConnect(AppId, AppKey, currentUser.State, UrlParameterHelper.UrlEncode(CallbackUrl));
            if (connect != null)
            {
                SaveUserInfo(currentUser);
                url = connect.GetAuthorizationCodeUrl("email");
            }

            return url;
        }

        protected string Notify(string AppId, string AppKey, string CallbackUrl)
        {
            string url = "";

            if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(State) && string.Compare(State, currentUser.State, true) == 0)
            {
                WeiboConnect connect = new WeiboConnect(AppId, AppKey, currentUser.State, UrlParameterHelper.UrlEncode(CallbackUrl));
                if (connect != null && connect.GetAccessTokenByCode(Code)
                    && !string.IsNullOrEmpty(connect.AccessToken) && !string.IsNullOrEmpty(connect.OpenId))
                {
                    currentUser.Token = connect.AccessToken;
                    currentUser.OpenId = connect.OpenId;
                    string result = string.Empty;
                    WeiboConnect.UserInfo model = new WeiboConnect.UserInfo();
                    if (connect.GetUserInfo(out result, out model) && !string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(model.id))
                    {
                        currentUser.NickName = model.screen_name;
                        string nickName = string.Empty;

                        AccessUsersInfo accessUsersInfo = new AccessUsersInfo();
                        accessUsersInfo = GetClientLogInfo(accessUsersInfo) as AccessUsersInfo;
                        accessUsersInfo.AccessToken = currentUser.Token;
                        accessUsersInfo.UserName = "";
                        accessUsersInfo.NickName = currentUser.NickName;
                        accessUsersInfo.Email = "";
                        accessUsersInfo.Icon = model.profile_image_url;
                        accessUsersInfo.Phone = "";
                        accessUsersInfo.Platform = Constants.AccessUserPlatform.weibo.ToString();
                        accessUsersInfo.OpenId = currentUser.OpenId;
                        accessUsersInfo.AccessToken = currentUser.Token;
                        accessUsersInfo.LoginInvalidTime = DateTime.Now;
                        accessUsersInfo.RecentLoginTime = DateTime.Now;
                        accessUsersInfo.AddTime = DateTime.Now;
                        accessUsersInfo.Status = (int)Constants.Status.yes;
                        accessUsersInfo.UnionId = "";
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