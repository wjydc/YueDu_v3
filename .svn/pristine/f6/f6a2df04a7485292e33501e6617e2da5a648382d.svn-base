using System.Web.Mvc;

namespace Component.Filter
{
    public class BaseExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private bool IsAjax(ExceptionContext filterContext)
        {
            //return (System.Web.HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest");
            return filterContext.HttpContext.Request.IsAjaxRequest();
        }

        public void OnException(ExceptionContext filterContext)
        {
            ////记录日志
            //string msg = filterContext.Exception.Message;
            ////Log  filterContext.Exception.StackTrace, filterContext.Exception;

            //if (IsAjax(filterContext))
            //{
            //    filterContext.Result = new ContentResult { Content = "服务器内部错误", ContentType = "application/json" };
            //    //将状态码更新为200，否则在Web.config中配置了CustomerError后，Ajax会返回500错误导致页面不能正确显示错误信息
            //    filterContext.HttpContext.Response.StatusCode = 200;
            //    filterContext.ExceptionHandled = true;
            //}
            //else
            //{
            //    var result = new ContentResult();
            //    string errorMsg = "<div>页面发生错误：{0}您可以<a href='/'>返回首页</a></div>";
            //    result.Content = string.Format(errorMsg, "");
            //    filterContext.Result = result;
            //    filterContext.HttpContext.Response.StatusCode = 200;
            //    filterContext.ExceptionHandled = true;
            //}
        }
    }
}