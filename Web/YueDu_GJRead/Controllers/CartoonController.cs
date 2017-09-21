using Component.Base;
using Component.Controllers.User;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utility;
using ViewModel;
using YueDu.App_Code;

namespace YueDu.Controllers
{
    public class CartoonController : UserInfoController
    {
        private IADService _adService;
        private IBookService _bookService;
        private IRecommendService _recommendService;

        public CartoonController(IADService adService, IBookService bookService, IRecommendService recommendService)
        {
            _adService = adService;
            _bookService = bookService;
            _recommendService = recommendService;
        }

        public ActionResult Index()
        {
            int timeOut = 30;

            //首页-收藏 - 推荐位
            var FavRec = DataContext.TryCache<IEnumerable<RecommendView>>("CartoonIndex_Fav", () =>
            {
                return GetRecList(RecSection.CartoonIndex.FavRec, 3);
            }, timeOut);

            //首页-人气排行
            var HotRec = DataContext.TryCache<IEnumerable<RecommendView>>("CartoonIndex_Pop", () =>
            {
                return GetRecList(RecSection.CartoonIndex.PopRec, 4);
            }, timeOut);

            //首页-三次元漫画
            var ThreeRec = DataContext.TryCache<IEnumerable<RecommendView>>("CartoonIndex_ThreeD", () =>
            {
                return GetRecList(RecSection.CartoonIndex.ThreeDRec, 6);
            }, timeOut);

            //首页-顶端广告位
            var headAD = DataContext.TryCache<IEnumerable<AD>>("CartoonIndex_Top", () =>
            {
                return GetADList(RecSection.CartoonIndex.TopAd, 4);
            }, timeOut);

            //首页-中间广告位
            var middleAD = DataContext.TryCache<IEnumerable<AD>>("CartoonIndex_Middle", () =>
            {
                return GetADList(RecSection.CartoonIndex.MiddleAd, 1);
            }, timeOut);

            CartoonView index = new CartoonView()
            {
                FavRecList = new SimpleResponse<IEnumerable<RecommendView>>(!FavRec.IsNullOrEmpty(), FavRec),
                HotRecList = new SimpleResponse<IEnumerable<RecommendView>>(!HotRec.IsNullOrEmpty(), HotRec),
                ThreeRecList = new SimpleResponse<IEnumerable<RecommendView>>(!ThreeRec.IsNullOrEmpty(), ThreeRec),
                HeadADList = new SimpleResponse<IEnumerable<AD>>(!headAD.IsNullOrEmpty(), headAD),
                MiddleADList = new SimpleResponse<IEnumerable<AD>>(!middleAD.IsNullOrEmpty(), middleAD)
            };
            return View(index);
        }

        /// <summary>
        /// 排行
        /// </summary>
        /// <returns></returns>
        public ActionResult Rank()
        {
            int timeOut = 60;

            //人气榜
            var popularityList = DataContext.TryCache<IEnumerable<NovelView>>("CartoonRank_Hits", () =>
            {
                return GetRankList("order by n.hits desc");
            }, timeOut);

            //新书榜
            var newList = DataContext.TryCache<IEnumerable<NovelView>>("CartoonRank_New", () =>
            {
                return GetRankList("order by n.id desc");
            }, timeOut);

            CartoonRankView rankView = new CartoonRankView()
            {
                New = new SimpleResponse<IEnumerable<NovelView>>(!newList.IsNullOrEmpty(), newList),
                Popularity = new SimpleResponse<IEnumerable<NovelView>>(!popularityList.IsNullOrEmpty(), popularityList)
            };

            return View(rankView);
        }

        /// <summary>
        /// CP专区
        /// </summary>
        /// <returns></returns>
        public ActionResult CP()
        {
            int timeOut = 30;

            IEnumerable<RecommendView> recommendList = DataContext.TryCache<IEnumerable<RecommendView>>("CartoonIndex_ThreeD_More", () =>
            {
                return GetRecList(RecSection.CartoonIndex.ThreeDRec, 10);
            }, timeOut);

            var model = new SimpleResponse<IEnumerable<RecommendView>>(!recommendList.IsNullOrEmpty(), recommendList);

            return View(model);
        }

        #region 辅助方法

        private IEnumerable<RecommendView> GetRecList(int classId = 0, int pageSize = 10, int pageIndex = 1)
        {
            string where = " and r.RecClassId = @classId ";
            string orderby = " order by r.onlinetime desc, r.sortid desc ";
            int rowCount = 0;

            return _recommendService.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, new { classId });
        }

        private IEnumerable<AD> GetADList(int classId = 0, int pageSize = 10, int pageIndex = 1)
        {
            string where = "and a.ClassId = @classId";
            string orderby = " order by a.onlinetime desc, a.sortid desc ";
            int rowCount = 0;

            return _adService.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, new { classId });
        }

        private IEnumerable<NovelView> GetRankList(string orderby, int pageSize = 10, int pageIndex = 1)
        {
            string columns = "n.Id, n.Title, n.LargeCover, n.ThumbCover, n.SmallCover, n.UpdateStatus,n.ShortDescription, nc.ClassName",
                   where = " and n.ContentType = @contentType and n.status = @status and nc.status = @status";//类别为漫画
            int rowCount = 0;

            return _bookService.GetPagerList(where, orderby, out rowCount, pageIndex, pageSize, new { contentType = (int)Constants.Novel.ContentType.漫画, status = 1 }, "[dbo].[Novel] as n inner join [dbo].[NovelClass] nc on nc.Id = n.ClassId", columns);
        }

        #endregion 辅助方法
    }
}