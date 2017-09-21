using Component.Base;
using Component.Filter;
using Service.Core;
using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Utility;

namespace YueDu
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.RemoveAt(0);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LogHelper.Initialise();
            AutoMapperBootStrapper.Instance.Initialise();
            AutofacBootStrapper.Instance.Initialise(Assembly.GetExecutingAssembly());

            ThreadPool.QueueUserWorkItem((a) =>
            {
                string error = "";

                while (true)
                {
                    //判断一下队列中是否有数据
                    if (ExceptionFilter.execptionQueue.Count > 0)
                    {
                        if (ExceptionFilter.execptionQueue.TryDequeue(out error))
                        {
                            LogHelper.Error(error);
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        //如果队列中没有数据，休息
                        Thread.Sleep(3000);
                    }
                }
            });
        }

        protected void Application_Stop()
        {
            CurrentContext.ClearUser();
        }
    }
}