using Component.Base;
using Component.Filter;
using Model;
using Model.Common;
using Service.Core;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace Component.Controllers
{
    [BaseExceptionFilter(Order = 2)]
    public class BaseController : Controller
    {
        #region

        protected string Host = "";

        #endregion

        private IDictionary<string, object> RouteParams = new Dictionary<string, object>();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            #region

            RouteParams = RouteDataHelper.Init(requestContext);

            #endregion

            Host = StringHelper.GetHost(Host);
        }

        #region Header

        protected enum ParamType
        {
            none, header, route, param
        };

        /// <summary>
        /// 获取头信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="paramType"></param>
        /// <returns></returns>
        protected bool IsHeader(string name, out string value, out ParamType paramType)
        {
            value = "";
            paramType = ParamType.none;

            if (string.IsNullOrEmpty(name)) return false;

            try
            {
                value = StringHelper.FilterIllegalParameter(HttpContext.Request.Headers[name]);
                if (!string.IsNullOrEmpty(value))
                {
                    paramType = ParamType.header;
                }
            }
            catch { }

            if (string.IsNullOrEmpty(value))
            {
                try
                {
                    value = GetRouteData(name);
                    if (!string.IsNullOrEmpty(value))
                    {
                        paramType = ParamType.route;
                    }
                }
                catch { }
            }

            if (string.IsNullOrEmpty(value))
            {
                try
                {
                    value = UrlParameterHelper.GetParams(name);
                    if (!string.IsNullOrEmpty(value))
                    {
                        paramType = ParamType.param;
                    }
                }
                catch { }
            }

            return (!string.IsNullOrEmpty(value) && paramType != ParamType.none);
        }

        /// <summary>
        /// 获取头信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected string GetHeader(string name)
        {
            if (string.IsNullOrEmpty(name)) return "";

            string value = string.Empty;
            ParamType paramType = ParamType.none;
            if (IsHeader(name, out value, out paramType))
            {
                return value;
            }

            return "";
        }

        #endregion

        #region GetRouteData

        protected string GetRouteData(string name)
        {
            string value = "";

            if (!string.IsNullOrEmpty(name) && !RouteParams.IsNullOrEmpty<string, object>())
            {
                value = RouteDataHelper.Get(RouteParams, name);
            }

            return value;
        }

        #endregion

        #region SourceType

        protected int GetSourceType()
        {
            return (int)Constants.Version.wap;
        }

        #endregion
    }
}