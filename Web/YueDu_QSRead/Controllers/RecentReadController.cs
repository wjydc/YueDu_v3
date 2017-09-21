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
        private IChannelCompanyService _channelCompanyService;

        public RecentReadController(IChannelCompanyService channelCompanyService)
        {
            _channelCompanyService = channelCompanyService;
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
            NovelRecentReadListView readLog = RecentReadContext.Get(RecentReadContext.GetCookieName(cType), currentUser.UserId);
            return View(new SimpleResponse<NovelRecentReadListView>((!readLog.IsNullOrEmpty<NovelRecentReadListView>() && !readLog.List.IsNullOrEmpty<IList<NovelRecentReadView>>()), readLog));
        }

        public ActionResult Open(int cType = 0, int? cid = 0)
        {
            int channelCompanyId = cid.HasValue ? cid.ToInt() : 0;
            string channelId = _channelCompanyService.GetRecentReadChannelId(channelCompanyId, RouteChannelId);

            string url = "/".GetChannelRouteUrl(channelId);
            NovelRecentReadListView readLog = RecentReadContext.Get(RecentReadContext.GetCookieName(cType), currentUser.UserId);
            if (!readLog.IsNullOrEmpty<NovelRecentReadListView>() && !readLog.List.IsNullOrEmpty<IList<NovelRecentReadView>>())
            {
                NovelRecentReadView info = readLog.List.Reverse().FirstOrDefault();
                if (info != null && info.Id > 0)
                {
                    url = ChapterContext.GetUrl("/chapter/detail", info.Id, info.ChapterCode, channelId: channelId);
                }
            }
            return Redirect(url);
        }
    }
}