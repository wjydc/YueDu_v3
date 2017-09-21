using Component.Base;
using Model.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Utility;

namespace Component.Filter
{
    public enum ContentType
    {
        url, json, str
    };

    public class Authorization : FilterAttribute, IAuthorizationFilter
    {
        #region 用户信息

        private string Host = "";

        private ICurrentUser currentUser;

        private void GetUserInfo()
        {
            currentUser = CurrentContext.GetUser();
            if (currentUser == null)
            {
                currentUser = new CurrentUser(Host);
                CurrentContext.SaveUser(currentUser);
            }
        }

        private bool IsLogin()
        {
            if (currentUser != null && !string.IsNullOrEmpty(Host) && string.Compare(currentUser.LoginedId, Host, true) != 0)
            {
                DataContext.ErrorLog("Authentication", "IsLogin", "", "IsLogin：Host=" + Host + "&Id=" + currentUser.LoginedId);

                currentUser = null;
                CurrentContext.ClearUser();
            }

            return currentUser != null && !string.IsNullOrEmpty(currentUser.UserName);
            //&& loginedUserInfo.UserId > 0;
        }

        #endregion 用户信息

        public bool IsAjaxOnly { private get; set; }

        public ContentType CType { private get; set; }

        public string Content { private get; set; }

        public int ErrorCode { private get; set; }

        public string Message { private get; set; }

        /// <summary>
        /// 鉴权失败时跳转
        /// </summary>
        /// <param name="isAjaxOnly">是否仅支持AJAX请求</param>
        public Authorization(bool isAjaxOnly = false)
        {
            CType = ContentType.url;
            IsAjaxOnly = isAjaxOnly;
        }

        /// <summary>
        /// 鉴权失败时返回指定内容
        /// </summary>
        /// <param name="content">返回的内容</param>
        /// <param name="isAjaxOnly">是否仅支持AJAX请求</param>
        public Authorization(string content, bool isAjaxOnly = false)
        {
            CType = ContentType.str;
            Content = content;
            IsAjaxOnly = isAjaxOnly;
        }

        /// <summary>
        /// 鉴权失败时返回包含错误码和错误信息的json数据
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorCode">错误码</param>
        /// <param name="content">返回的内容</param>
        /// <param name="isAjaxOnly">是否仅支持AJAX请求</param>
        public Authorization(string message, ErrorMessage errorCode, string content = "", bool isAjaxOnly = false)
        {
            CType = ContentType.json;
            Message = message;
            ErrorCode = (int)errorCode;
            Content = content;
            IsAjaxOnly = isAjaxOnly;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            #region

            if (IsAjaxOnly && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult { Content = "" };
                return;
            }

            #endregion

            #region

            Host = StringHelper.GetHost(Host);
            GetUserInfo();

            #endregion

            if (!IsLogin())
            {
                switch (CType)
                {
                    case ContentType.url:
                        string url = StringHelper.GetReturnUrl("/login", channelId: GetChannelId(filterContext, "channelId"));
                        filterContext.Result = new RedirectResult(url);
                        break;

                    case ContentType.json:
                        var responseData = new ComplexResponse<string>(ErrorCode, Message, Content);
                        filterContext.Result = new JsonResult { Data = responseData };
                        break;

                    case ContentType.str:
                        filterContext.Result = new ContentResult { Content = Content };
                        break;
                }
            }
        }

        private string GetChannelId(AuthorizationContext filterContext, string name)
        {
            if (string.IsNullOrEmpty(name)) return "";

            string value = string.Empty;

            try
            {
                value = StringHelper.FilterIllegalParameter(filterContext.HttpContext.Request.Headers[name]);
            }
            catch { }

            if (string.IsNullOrEmpty(value))
            {
                try
                {
                    value = UrlParameterHelper.GetParams(name);
                }
                catch { }
            }

            if (string.IsNullOrEmpty(value))
            {
                try
                {
                    RouteData data = filterContext.RouteData;
                    if (data != null)
                    {
                        RouteValueDictionary values = data.Values;
                        if (values != null && values.Count > 0)
                        {
                            value = StringHelper.ToString(values[name]);
                        }
                    }
                }
                catch { }
            }

            return value;
        }
    }
}