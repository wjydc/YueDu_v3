using Component.Base;
using Component.Controllers.User;
using Component.Filter;
using Model;
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
        private IChannelCompanyService _channelCompanyService;
        private IPromotionLinkService _promotionLinkService;

        public RecentReadController(ILogService logService, IChannelCompanyService channelCompanyService, IPromotionLinkService promotionLinkService)
        {
            _logService = logService;
            _channelCompanyService = channelCompanyService;
            _promotionLinkService = promotionLinkService;
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

        public ActionResult Open(int cType = 0, int ccid = 0)
        {
            Constants.Novel.StatisticType statisticType = Constants.Novel.StatisticType.none;
            string channelId = _channelCompanyService.GetRecentReadChannelId(ccid, RouteChannelId, out statisticType);

            if (IsRouteChannel)
            {
                string promotionCode = HeaderInfo.PromotionCode;

                if (statisticType == Constants.Novel.StatisticType.promotion)
                {
                    PromotionLink promote = _promotionLinkService.Detail("and ChannelCode = @channelId and PromotionCode = @promotionCode", new { channelId, promotionCode });
                    if (promote != null && promote.Id > 0 && !string.IsNullOrEmpty(promotionCode))
                    {
                        #region 统计推广码关注人数

                        _promotionLinkService.Update(" FollowCount = ISNULL(FollowCount, 0) + 1 ", promote.Id);

                        #endregion 统计推广码关注人数
                    }
                    else
                    {
                    }
                }
            }

            string url = "/".GetChannelRouteUrl(channelId);
            NovelRecentReadListView readLog = RecentReadContext.Get(RecentReadContext.GetCookieName(cType), _logService, currentUser.UserName, currentUser.UserId);
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