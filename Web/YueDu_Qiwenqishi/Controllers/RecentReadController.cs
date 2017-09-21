using Component.Base;
using Component.Controllers.User;
using Component.Filter;
using Model.Common;
using Newtonsoft.Json;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    /// <summary>
    /// 最近阅读
    /// </summary>
    public class RecentReadController : UserInfoController
    {
        private ILogService _logService;

        public RecentReadController(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// 最近阅读
        /// </summary>
        /// <param name="contentType">0：小说  2：漫画</param>
        /// <returns></returns>
        [Authorization]
        public ActionResult Index(int cType = 0)
        {
            ViewBag.ContentType = cType;
            NovelRecentReadListView readLog = RecentReadContext.Get(RecentReadContext.GetCookieName(cType), _logService, currentUser.UserName, currentUser.UserId);
            return View(new SimpleResponse<NovelRecentReadListView>((!readLog.IsNullOrEmpty<NovelRecentReadListView>() && !readLog.List.IsNullOrEmpty<IList<NovelRecentReadView>>()), readLog));
        }

        public ActionResult Open(int cType = 0)
        {
            string url = "/".GetChannelRouteUrl(RouteChannelId);
            NovelRecentReadListView readLog = RecentReadContext.Get(RecentReadContext.GetCookieName(cType), _logService, currentUser.UserName, currentUser.UserId);
            if (!readLog.IsNullOrEmpty<NovelRecentReadListView>() && !readLog.List.IsNullOrEmpty<IList<NovelRecentReadView>>())
            {
                NovelRecentReadView info = readLog.List.Reverse().FirstOrDefault();
                if (info != null && info.Id > 0)
                {
                    string channelId = string.IsNullOrEmpty(info.RouteChannelId) ? RouteChannelId : info.RouteChannelId;
                    url = ChapterContext.GetUrl("/chapter/detail", info.Id, info.ChapterCode, channelId: channelId);
                }
            }
            return Redirect(url);
        }
    }
}