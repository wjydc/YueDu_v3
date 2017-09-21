using Component.Base;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utility;

namespace Component.Handler
{
    public class FilterModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpRequest request = application.Request;
            HttpResponse response = application.Response;

            string IP = GetIP(request);

            if (!string.IsNullOrEmpty(IP))
            {
                IBlackListService service = DataContext.ResolveService<IBlackListService>();
                int count = service.GetCount(" and Status = 1 and TypeName = 'IP' and TypeValue = @TypeValue ", new { TypeValue = IP });
                if (count > 0)
                {
                    response.ContentEncoding = Encoding.UTF8;
                    response.ContentType = "text/html";
                    response.Write("抱歉！暂时无法访问，请稍候再试。");
                    application.CompleteRequest();
                }
            }
        }

        private string GetIP(HttpRequest request)
        {
            string result = "";

            if (request != null)
            {
                result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(result))
                {
                    result = request.ServerVariables["REMOTE_ADDR"];
                }
            }

            return result;
        }
    }
}