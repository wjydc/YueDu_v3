using Component.Base;
using Component.Controllers.Log;
using Model.Common;
using Service;
using System;
using Utility;

namespace Component.Controllers.User
{
    public class CreateUserController : ErrorLogController
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            CreateUserLog();
            GetUserInfo();
        }

        #region 用户日志

        protected void CreateUserLog()
        {
            object obj = SessionHelper.Get(Constants.SecurityKey.UserId_SessionName);

            if (obj != null && !StringHelper.IsObjectNullOrEmpty(obj))
            {
            }
            else
            {
                string cookieId = GetCookieId();

                try
                {
                    UserLogInfo logInfo = new UserLogInfo();
                    logInfo = GetLogInfo(logInfo) as UserLogInfo;
                    logInfo.AddTime = DateTime.Now;
                    logInfo.CookieId = cookieId;
                    logInfo.OnlineTime = 0;
                    logInfo.RecentStartupTime = DateTime.Now;
                    logInfo.StartupCount = 0;
                    logInfo.Url = StringHelper.ToString(this.Request.Url);
                    ILogService log = DataContext.ResolveService<ILogService>();
                    log.CreateUserInfo(logInfo);
                }
                catch { }
                finally
                {
                    SessionHelper.Set(Constants.SecurityKey.UserId_SessionName, cookieId);
                }
            }
        }

        /// <summary>
        /// 获取CookieId
        /// </summary>
        /// <returns></returns>
        protected string GetCookieId()
        {
            //return StringHelper.GetCookieId(Constants.SecurityKey.UserLog_CookieName,
            //    Constants.SecurityKey.UserLog_Cookie_Id,
            //    StringHelper.ToString(SessionHelper.Get(Constants.SecurityKey.UserId_SessionName)),
            //    DateTime.Now.AddDays(1));

            return StringHelper.GetCookieId(Constants.SecurityKey.UserLog_CookieName, Constants.SecurityKey.UserLog_Cookie_Id, DateTime.Now.AddDays(1));
        }

        #endregion 用户日志

        #region 用户信息

        protected ICurrentUser currentUser;

        private void GetUserInfo()
        {
            currentUser = CurrentContext.GetUser();
            if (currentUser == null)
            {
                currentUser = new CurrentUser(Host);
                SaveUserInfo(currentUser);
            }
            else
            {
                if (currentUser.LoginTime.Date != DateTime.Now.Date)
                {
                    ILoginService service = DataContext.ResolveService<ILoginService>();
                    currentUser.LoginTime = service.UpdateLoginInfo(currentUser.UserName);
                    SaveUserInfo(currentUser);
                }
            }
        }

        protected void SaveUserInfo(ICurrentUser currentUser)
        {
            CurrentContext.SaveUser(currentUser);
        }

        /// <summary>
        /// 判断是否已登录
        /// true：已登录
        /// false：未登录
        /// </summary>
        /// <returns></returns>
        protected bool IsLogin()
        {
            if (currentUser != null && !string.IsNullOrEmpty(Host) && string.Compare(currentUser.LoginedId, Host, true) != 0)
            {
                Log("IsLogin：Host=" + Host + "&Id=" + currentUser.LoginedId);

                currentUser = null;
                CurrentContext.ClearUser();
            }

            return currentUser != null && !string.IsNullOrEmpty(currentUser.UserName);
            //&& loginedUserInfo.UserId > 0;
        }

        #endregion 用户信息
    }
}