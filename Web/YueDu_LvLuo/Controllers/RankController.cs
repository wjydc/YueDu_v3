using Component.Base;
using Component.Controllers.User;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using ViewModel;
using YueDu.App_Code;

namespace YueDu.Controllers
{
    public class RankController : UserInfoController
    {
        private IBookService _bookService;
        private IRecommendService _recommendService;

        public RankController(IBookService bookService, IRecommendService recommendService)
        {
            _bookService = bookService;
            _recommendService = recommendService;
        }

        public ActionResult List()
        {
            int timeOut = 60;

            //综合榜-点击排行榜
            var clickList = DataContext.TryCache<IEnumerable<NovelView>>("Rank_Hits", () =>
            {
                return GetBookList("order by n.hits desc");
            }, timeOut);

            //综合榜-收藏排行榜
            var favoriteList = DataContext.TryCache<IEnumerable<NovelView>>("Rank_Fav", () =>
            {
                return GetBookList("order by n.favcount desc");
            }, timeOut);

            //综合榜-完本排行榜
            var finishList = DataContext.TryCache<IEnumerable<NovelView>>("Rank_UpdateStatus_Hits", () =>
            {
                return GetBookList("order by n.hits desc", " and n.UpdateStatus=1 ");
            }, timeOut);

            //综合榜-打赏排行榜
            var rewardList = DataContext.TryCache<IEnumerable<NovelView>>("Rank_RewardFee", () =>
            {
                return GetBookList("order by n.rewardfee desc");
            }, timeOut);

            //综合榜-新书排行榜
            var newList = DataContext.TryCache<IEnumerable<NovelView>>("Rank_New", () =>
            {
                return GetBookList("order by n.id desc");
            }, timeOut);

            //综合榜-字数排行榜
            var wordSizeList = DataContext.TryCache<IEnumerable<NovelView>>("Rank_WordSize", () =>
            {
                return GetBookList("order by n.wordsize desc");
            }, timeOut);

            //女生榜
            var girlsList = DataContext.TryCache<IEnumerable<RecommendView>>("Rank_Female", () =>
            {
                return GetRecommendList(RecSection.Rank.FemaleRec);
            }, timeOut);

            //男生榜
            var boysList = DataContext.TryCache<IEnumerable<RecommendView>>("Rank_Male", () =>
            {
                return GetRecommendList(RecSection.Rank.MaleRec);
            }, timeOut);

            RankView rank = new RankView()
            {
                BoysList = new SimpleResponse<IEnumerable<RecommendView>>(!boysList.IsNullOrEmpty(), boysList),
                ClickList = new SimpleResponse<IEnumerable<NovelView>>(!clickList.IsNullOrEmpty(), clickList),
                FavoriteList = new SimpleResponse<IEnumerable<NovelView>>(!favoriteList.IsNullOrEmpty(), favoriteList),
                FinishList = new SimpleResponse<IEnumerable<NovelView>>(!finishList.IsNullOrEmpty(), finishList),
                GirlsList = new SimpleResponse<IEnumerable<RecommendView>>(!girlsList.IsNullOrEmpty(), girlsList),
                NewList = new SimpleResponse<IEnumerable<NovelView>>(!newList.IsNullOrEmpty(), newList),
                RewardList = new SimpleResponse<IEnumerable<NovelView>>(!rewardList.IsNullOrEmpty(), rewardList),
                WordSizeList = new SimpleResponse<IEnumerable<NovelView>>(!wordSizeList.IsNullOrEmpty(), wordSizeList)
            };

            return View(rank);
        }

        /// <summary>
        /// 获取书籍榜单列表
        /// </summary>
        /// <param name="orderby"></param>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private IEnumerable<NovelView> GetBookList(string orderby, string where = "", int pageIndex = 1, int pageSize = 10)
        {
            int rowCount = 0;
            string columns = "n.Id,n.Title,n.ThumbCover";
            string strWhere = " and n.ContentType=0 and n.status =1";//类别为小说

            strWhere += EnvSettings.Novel.Filter(Constants.Novel.ShowLocation.ranklist, prefix: "n.");

            #region 过滤CP

            strWhere += EnvSettings.Novel.Filter(SiteSection.Filter.BookCPID, "n.");

            #endregion 过滤CP

            if (!string.IsNullOrEmpty(where))
                strWhere += where;
            return _bookService.GetPagerList(strWhere, orderby, out rowCount, pageIndex, pageSize, null, "[dbo].[Novel] as n ", columns);
        }

        /// <summary>
        /// 获取推荐位列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private IEnumerable<RecommendView> GetRecommendList(int classId = 0, int pageSize = 10, int pageIndex = 1)
        {
            int rowCount = 0;
            string where = " and r.RecClassId = @classId ";
            string orderby = " order by r.onlinetime desc, r.sortid desc ";

            return _recommendService.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, new { classId });
        }
    }
}