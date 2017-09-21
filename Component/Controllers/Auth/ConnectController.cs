using Component.Base;
using Component.Controllers.User;
using Model;
using Model.Common;
using Model.Config;
using Service;
using System;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace Component.Controllers.Auth
{
    public class ConnectController : CreateUserController
    {
        protected IAccessUsersService _accessUsersService;

        #region

        private IAuthSection _authSection = null;

        protected IAuthSection AuthSection
        {
            get
            {
                if (_authSection == null)
                {
                    _authSection = DataContext.GetAuthSection();
                }

                return _authSection;
            }
        }

        #endregion

        #region

        protected string DefaultUrl = "/";
        protected string SuccessUrl = "/user/detail";

        protected RedirectResult AuthDefaultRedirect(string url)
        {
            return AuthRedirect(url, DefaultUrl);
        }

        protected RedirectResult AuthSuccessRedirect(string url)
        {
            return AuthRedirect(url, SuccessUrl);
        }

        private RedirectResult AuthRedirect(string url, string defaultUrl)
        {
            return Redirect(string.IsNullOrEmpty(url) ? defaultUrl : url);
        }

        protected string GetCallbackUrl(string callbackUrl)
        {
            if (!string.IsNullOrEmpty(Host) && !string.IsNullOrEmpty(callbackUrl))
            {
                string scheme = Request.Url.Scheme;
                callbackUrl = FileHelper.MergePath("/", new string[] { string.Concat(scheme, "://", Host), callbackUrl });
            }

            return callbackUrl;
        }

        #endregion

        protected string Code = "";
        protected string State = "";
        protected string ReturnUrl = "";

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            Code = UrlParameterHelper.GetParams("code");
            State = UrlParameterHelper.GetParams("state");
            ReturnUrl = UrlParameterHelper.GetDecodingParams("returnurl");
        }

        #region

        protected string GetReturnUrl(string state)
        {
            return ReturnUrlHelper.GetSession(Constants.SecurityKey.LoginedReturnUrl_SessionName, state);
        }

        protected void SetReturnUrl(string state, string returnUrl)
        {
            ReturnUrlHelper.SetSession(Constants.SecurityKey.LoginedReturnUrl_SessionName, state, returnUrl);
        }

        #endregion

        #region 生成用户名

        private static readonly object Locker = new object();
        private static Random rd = new Random();

        protected string CreateUserName(out string nickName)
        {
            lock (Locker)
            {
                nickName = string.Concat(StringHelper.ConvertMilliseconds(DateTime.Now), rd.Next(10, 100));

                return string.Concat("i", nickName);
            }
        }

        #endregion

        #region

        protected void ChapterReadLogSync(string userName, int userId = 0)
        {
            if (string.IsNullOrEmpty(userName)) return;

            ChapterReadLogSync(userName, new string[] { Constants.SecurityKey.NovelRecentRead_CookieName.ToString(), Constants.SecurityKey.CartoonRecentRead_CookieName.ToString() }, userId);
        }

        protected void ChapterReadLogSync(string userName, string[] cookieNameList, int userId = 0)
        {
            if (string.IsNullOrEmpty(userName) || cookieNameList.IsNullOrEmpty()) return;

            foreach (string cookieName in cookieNameList)
            {
                ChapterReadLogSync(userName, cookieName, userId);
            }
        }

        protected void ChapterReadLogSync(string userName, string cookieName, int userId = 0)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(cookieName)) return;

            try
            {
                NovelRecentReadListView readLog = RecentReadContext.Get(cookieName, userId);
                if (readLog != null && !readLog.List.IsNullOrEmpty<NovelRecentReadView>())
                {
                    ILogService service = DataContext.ResolveService<ILogService>();

                    NovelReadRecordInfo novelReadRecordInfo = null;
                    foreach (NovelRecentReadView item in readLog.List)
                    {
                        novelReadRecordInfo = new NovelReadRecordInfo();
                        novelReadRecordInfo.AddTime = DateTime.Now;
                        novelReadRecordInfo.ChannelId = "";
                        novelReadRecordInfo.ChapterCode = item.ChapterCode;
                        novelReadRecordInfo.ChapterId = 0;
                        novelReadRecordInfo.ClientId = 0;
                        novelReadRecordInfo.CookieId = "";
                        novelReadRecordInfo.IMEI = "";
                        novelReadRecordInfo.IMSI = "";
                        novelReadRecordInfo.NovelId = item.Id;
                        novelReadRecordInfo.Position = 0;
                        novelReadRecordInfo.RecentReadTime = item.ReadTime;
                        novelReadRecordInfo.RouteChannelId = item.RouteChannelId;
                        novelReadRecordInfo.SourceType = (int)Constants.Version.wap;
                        novelReadRecordInfo.UserAgent = "";
                        novelReadRecordInfo.UserId = userId;
                        novelReadRecordInfo.UserName = userName;
                        novelReadRecordInfo.Version = "";
                        service.ChapterReadLogLocalSync(novelReadRecordInfo);
                    }
                }
            }
            catch { }
        }

        #endregion
    }
}