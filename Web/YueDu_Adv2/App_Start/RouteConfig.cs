using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Utility;

namespace YueDu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            //推广
            routes.MapRoute(
                name: "Preview",
                url: "preview/{channelName}/{channelId}",
                defaults: new { controller = "preview", action = "index", channelName = "cid", channelId = UrlParameter.Optional },
                constraints: new { channelName = "cid", channelId = "^(?![a-zA-Z]+$)[0-9a-zA-Z]*" }
            );

            //首页
            routes.MapRoute(
                name: "Index",
                url: "{channelName}/{channelId}",
                defaults: new { controller = "home", action = "index", channelName = "cid", channelId = UrlParameter.Optional },
                constraints: new { channelName = "cid", channelId = "^(?![a-zA-Z]+$)[0-9a-zA-Z]*" }
            );

            //登录
            routes.MapRoute(
                name: "Home",
                url: "{action}/{channelName}/{channelId}",
                defaults: new { controller = "home", channelName = "cid", channelId = UrlParameter.Optional },
                constraints: new { action = "login|t|m", channelName = "cid", channelId = "^(?![a-zA-Z]+$)[0-9a-zA-Z]*" }
            );

            #region

            //对接老项目book的url路径
            routes.MapRoute(
                name: "BookAspx",
                url: "book/{controller}/{action}.aspx",
                defaults: new { controller = "chapter", action = "list" },
                constraints: new { controller = "chapter|mark|present|comment" }
            );

            //对接老项目user的url路径
            routes.MapRoute(
                name: "UserAspx",
                url: "user/{controller}/{action}.aspx",
                defaults: new { controller = "order", action = "chapter" },
                constraints: new { controller = "user|qq|weibo|wechat|alipay|wxpay|nowpay|notify" }
            );

            //对接老项目index的url路径
            routes.MapRoute(
                name: "PreviewAspx",
                url: "Preview.aspx",
                defaults: new { controller = "preview", action = "index" }
            );

            //对接老项目index的url路径
            routes.MapRoute(
                name: "HomeAspx",
                url: "{action}.aspx",
                defaults: new { controller = "home" },
                constraints: new { action = "index|login|t" }
            );

            //对接老项目一级页面aspx的url路径
            routes.MapRoute(
                name: "Aspx",
                url: "{controller}/{action}.aspx",
                defaults: new { controller = "book" },
                constraints: new { controller = "book|rank|preorder|order|recharge" }
            );

            #endregion

            //默认规则
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{channelName}/{channelId}",
                defaults: new { controller = "home", action = "index", channelName = "cid", channelId = UrlParameter.Optional },
                constraints: new { channelName = "cid", channelId = "^(?![a-zA-Z]+$)[0-9a-zA-Z]*" }
            );
        }
    }
}