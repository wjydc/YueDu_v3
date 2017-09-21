using Component.Filter;
using System.Web.Mvc;
using YueDu.Controllers;

namespace YueDu
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new ActionFilter());
            filters.Add(new ExceptionFilter(), 1);
        }
    }
}