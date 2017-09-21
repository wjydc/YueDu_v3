using System.Web.Mvc;
using Utility;

namespace Component.Filter
{
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //var actionParameters = filterContext.ActionDescriptor.GetParameters();
            //foreach (var p in actionParameters)
            //{
            //    if (p.ParameterType == typeof(string)) //参数为字符串时可能会被sql注入和xss攻击
            //    {
            //        object parameterValue = filterContext.ActionParameters[p.ParameterName];

            //        if (StringHelper.IsObjectNullOrEmpty(parameterValue))
            //        {
            //            string inputValue = StringHelper.ToString(parameterValue);
            //            //if (!StringHelper.SqlInjectCheck(inputValue))
            //            //{
            //            //    filterContext.Result = new RedirectResult("/error.html");
            //            //    //filterContext.Result = new HttpNotFoundResult(" 您的路径中含有非法字符！ ");
            //            //}
            //            //filterContext.ActionParameters[p.ParameterName] = inputValue.SqlInjectFilter();
            //        }
            //    }
            //}
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            //var controllerName = filterContext.RouteData.Values["controller"].ToString();
            //var actionName = filterContext.RouteData.Values["action"].ToString();
        }
    }
}