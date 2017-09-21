using Component.Base;
using Component.Controllers.Novel;
using Model;
using Model.Common;
using Service;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utility;
using ViewModel;
using YueDu.App_Code;

namespace YueDu.Controllers
{
    public class BookController : DetailController
    {
        private IBookService _bookService;
        private IHotSearchWordService _hotSearchWordService;
        private IRecommendService _recommendService;
        private IBookmarkService _bookmarkService;
        private IPresentService _presentService;
        private IChapterService _chapterService;
        private ICommentService _commentService;
        private IPackageService _packageService;
        private IOrderService _orderService;

        public BookController(IBookService bookService, IHotSearchWordService hotSearchWordService, IRecommendService recommendService, IBookmarkService bookmarkService, IPresentService presentService, IChapterService chapterService, ICommentService commentService, IPackageService packageService, IOrderService orderService)
        {
            _bookService = bookService;
            _hotSearchWordService = hotSearchWordService;
            _recommendService = recommendService;
            _bookmarkService = bookmarkService;
            _presentService = presentService;
            _chapterService = chapterService;
            _commentService = commentService;
            _packageService = packageService;
            _orderService = orderService;
        }

        public ActionResult List(int? cType = 0, int classId = 0, int updateStatus = 0, int feeType = 0, int sort = (int)Constants.Novel.SortBy.newest, int pageIndex = 1, int pageSize = 10)
        {
            string table = " [dbo].[novel] as n join [dbo].[NovelClass] as nc on n.ClassId =nc.Id ";
            string columns = @" n.Id,n.Title,n.Author,n.UpdateStatus,n.ShortWordSize,n.ContentType,n.RecentChapterName,n.RecentChapterUpdateTime,
                                n.LargeCover,n.SmallCover,n.ThumbCover, n.RewardFee, nc.ClassName ";
            string where = string.Empty;
            string orderBy = (sort == (int)Constants.Novel.SortBy.newest) ? " order by n.RecentChapterUpdateTime desc, n.id desc" :
                ((sort == (int)Constants.Novel.SortBy.hot) ? " order by n.hits desc, n.id desc" : " order by n.id desc");

            var novelClassList = _bookService.GetNovelClassList(cType).ToList().ToNovelClassViewList();
            where = GetSearchWhere(cType, classId, updateStatus, feeType);

            int rowCount;
            object param = new
            {
                cType,
                classId,
                updateStatus
            };
            var bookList = _bookService.GetPagerList(where, orderBy, out  rowCount, pageIndex, pageSize, param, table: table, columns: columns);
            BookListView model = new BookListView
            {
                CType = cType,
                ClassId = classId,
                UpdateStatus = updateStatus,
                FeeType = feeType,
                Sort = sort,
                NovelClassList = new SimpleResponse<IEnumerable<NovelClassView>>(!novelClassList.IsNullOrEmpty(), novelClassList),
                BookList = new SimpleResponse<IEnumerable<NovelView>>(!bookList.IsNullOrEmpty(), bookList),
                RowCount = rowCount,
            };
            return View(model);
        }

        /// <summary>
        /// 搜索页
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            return View();
        }

        public JsonResult ChangeHotSearch(int pageIndex = 1, int pageSize = 10)
        {
            int rowCount = 0;
            var list = _hotSearchWordService.GetPagerList(out rowCount, pageIndex, pageSize);
            if (list.Count() < pageSize)
            {
                pageIndex = 0;
                list = _hotSearchWordService.GetPagerList(out rowCount, pageIndex, pageSize);
            }
            return Json(new SimpleResponse<object>(true, new { list = list, pageIndex = pageIndex }));
        }

        /// <summary>
        /// 搜索结果页
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchList(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            string where = GetSearchWhere(null, 0, 0, 0, keyword, Constants.Novel.ShowLocation.searchlist);
            string orderby = " order by n.Hits desc, n.FavCount desc, n.id desc";
            int rowCount;
            var bookList = _bookService.GetPagerList(where, orderby, out  rowCount, pageIndex, pageSize,
                new { keyword = UrlParameterHelper.GetDecodingParams("keyword") },
                "[dbo].[Novel] as n inner join [dbo].[NovelClass] nc on nc.Id = n.ClassId",
                "n.Id, n.Title, n.LargeCover, n.ThumbCover, n.SmallCover, n.UpdateStatus, nc.ClassName,n.Author,n.ShortDescription,n.ShortWordSize,n.ContentType, n.IsHideAuthor");
            ViewBag.TotalCount = rowCount;
            ViewBag.Keyword = keyword;
            var model = new SimpleResponse<IEnumerable<NovelView>>(!bookList.IsNullOrEmpty(), bookList);
            return View(model);
        }

        /// <summary>
        /// 书籍详情
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public async Task<ActionResult> Detail(int novelId = 0)
        {
            string loginUserName = currentUser.UserName;
            Novel novel = null;
            if (novelId > 0)
                novel = _bookService.GetNovel(novelId);

            if (novel == null || novel.Id <= 0)
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));

            BookDetailView view = novel.ToBookDetailView();
            //活动截止时间
            var packageInfo = await Task.Run(() => _packageService.GetNovelPackage(Constants.PackageType.LimitFree, novelId));
            if (packageInfo != null)
                view.PackageEndTime = packageInfo.EndTime;
            else
                view.PackageEndTime = null;

            //是否收藏
            bool isAddMark = await Task.Run(() => _bookmarkService.Exists(novelId, loginUserName));
            view.IsAddMark = isAddMark;
            //是否全本优惠
            view.IsOrder = await Task.Run(() => IsOrder(_orderService));
            //阅读记录
            ILogService logService = DataContext.ResolveService<ILogService>();
            NovelReadRecordInfo readRecord = await Task.Run(() => logService.GetRecentChapter(loginUserName, novelId));
            if (readRecord != null) view.RecentReadChapterCode = readRecord.ChapterCode;

            //作者的话
            string column = "Message";
            string where = "and novelId = @novelId and authorId = @authorId ";
            string orderby = " order by id desc";
            var authorNotice = await Task.Run(() => _bookService.GetAuthorNotice(column, where, orderby, new { novelId = novelId, authorId = novel.UserId }));
            view.AuthorNotice = authorNotice == null ? null : authorNotice.Message;

            //章节信息
            var lastChapter = await Task.Run(() => _chapterService.GetLatestChapter(novelId));
            view.RecentChapterName = lastChapter == null ? null : lastChapter.ChapterName;
            view.RecentChapterUpdateTime = lastChapter == null ? default(DateTime) : lastChapter.OnlineTime;
            view.RecentChapterCode = lastChapter == null ? 0 : lastChapter.ChapterCode;
            int rowCount = 0;
            var chapterList = await Task.Run(() => GetChapterList(out rowCount, novelId, novel.ContentType));
            view.TotalChapterCount = rowCount;
            view.ChapterList = new SimpleResponse<IEnumerable<ChapterView>>(!chapterList.IsNullOrEmpty(), chapterList);

            //评论信息
            int commentCount = 0;
            var commentList = await Task.Run(() => GetCommentList(out commentCount, novel.Id));
            view.CommentCount = commentCount;
            view.CommentList = new SimpleResponse<IEnumerable<CommentView>>(!commentList.IsNullOrEmpty(), commentList);

            //猜你喜欢
            bool IsCartoonType = novel.ContentType == (int)Constants.Novel.ContentType.漫画;
            view.RecClassId = IsCartoonType ? RecSection.CartoonDetail.GuessRec : RecSection.BookDetail.GuessRec;

            bool isNovelAllCPShow = SiteSection.CP.IsShowAllNovel;
            bool isAudioAllCPShow = SiteSection.CP.IsShowAllAudio;
            bool isCartoonAllCPShow = SiteSection.CP.IsShowAllCartoon;

            string cpWhere = "and Id = @cpId and status = @status";
            cpWhere += ((novel.ContentType == (int)Constants.Novel.ContentType.小说 && !isNovelAllCPShow) ||
            (novel.ContentType == (int)Constants.Novel.ContentType.听书 && !isAudioAllCPShow) ||
            (IsCartoonType && !isCartoonAllCPShow)) ? " and displayType & 1 = 1 " : "";

            var cp = await Task.Run(() => _bookService.GetCP("CnName", cpWhere, "order by id ", new { cpId = novel.CPId, status = 1 }));
            view.CPName = cp == null ? null : cp.CnName;

            //记录日志

            NovelLogInfo log = new NovelLogInfo();
            log = GetLogInfo(log) as NovelLogInfo;
            log.CookieId = GetCookieId();
            log.NovelId = novelId;
            log.AddTime = DateTime.Now;
            var service = DataContext.ResolveService<ILogService>();
            await Task.Run(() => service.NovelReadLog(log));

            var model = new SimpleResponse<BookDetailView>(view != null, view);

            if (IsCartoonType)
            {
                return View("/Views/Cartoon/Detail.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 免费专区
        /// </summary>
        /// <returns></returns>
        public ActionResult Free(Constants.Novel.ClassSpeType cst = Constants.Novel.ClassSpeType.female)
        {
            int timeOut = 30;

            dynamic recClass = GetRecClass(cst);

            IEnumerable<RecommendView> newFreeList = DataContext.TryCache<IEnumerable<RecommendView>>(string.Format("{0}_LimitFree_New", cst), () =>
            {
                return GetRecList(recClass.New, 3);
            }, timeOut);

            IEnumerable<RecommendView> hotFreeList = DataContext.TryCache<IEnumerable<RecommendView>>(string.Format("{0}_BookIndex_Hot", cst), () =>
            {
                return GetRecList(recClass.Hot, 3);
            }, timeOut);

            IEnumerable<PackageView> limitFreeList = GetPackageList((int)recClass.Limit, Constants.PackageType.LimitFree, 4);

            BookFreeView free = new BookFreeView()
            {
                NewFreeList = new SimpleResponse<IEnumerable<RecommendView>>(!newFreeList.IsNullOrEmpty(), newFreeList),
                HotFreeList = new SimpleResponse<IEnumerable<RecommendView>>(!hotFreeList.IsNullOrEmpty(), hotFreeList),
                LimitFreeList = new SimpleResponse<IEnumerable<PackageView>>(!limitFreeList.IsNullOrEmpty(), limitFreeList),
            };

            return View(free);
        }

        public ActionResult BeginRead(int novelId, int code)
        {
            string readUrl = ChapterContext.GetUrl("/chapter/detail", novelId, code, channelId: RouteChannelId);
            return Redirect(readUrl);
        }

        #region 辅助方法

        private string GetSearchWhere(int? cType, int classId = 0, int updateStatus = 0, int feeType = 0, string keyword = "", Constants.Novel.ShowLocation showLocation = Constants.Novel.ShowLocation.booklist)
        {
            StringBuilder where = new StringBuilder(" and n.Status = 1 ");

            where.Append(EnvSettings.Novel.Filter(showLocation, prefix: "n."));

            if (cType.HasValue)
            {
                where.Append(" and isnull(n.ContentType,0) =@cType ");
            }

            #region 过滤CP

            where.Append(EnvSettings.Novel.Filter(SiteSection.Filter.BookCPID, "n."));

            #endregion 过滤CP

            #region 构建搜索条件

            if (classId > 0)
            {
                where.Append(" and n.ClassId = @classId");
            }
            if (updateStatus > 0)
            {
                where.Append(" and n.UpdateStatus = @updateStatus");
            }
            if (feeType > 0)
            {
                if (feeType == (int)Constants.Novel.FeeTypeFilter.free)
                    where.Append(" and n.FeeType = 0 ");
                else
                    where.Append(" and n.FeeType <> 0 ");
            }
            if (!keyword.IsClearBlankNullOrEmpty(out keyword))
            {
                where.Append(" and (charindex(@keyword,n.Title)>0 or charindex(@keyword,n.Author)>0 or charindex(@keyword,n.Tags)>0 ");

                if (keyword.ToInt() > 0)
                {
                    where.Append(" or n.Id =@keyword ");
                }

                where.Append(" ) ");
            }

            #endregion 构建搜索条件

            return where.ToString();
        }

        /// <summary>
        /// 推荐位
        /// </summary>
        private IEnumerable<RecommendView> GetRecList(int classId = 0, int pageSize = 10, int pageIndex = 1)
        {
            string where = " and r.RecClassId = @classId ";
            string orderby = " order by r.onlinetime desc, r.sortid desc ";
            int rowCount = 0;

            return _recommendService.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, new { classId });
        }

        private IEnumerable<PackageView> GetPackageList(int packageId = 0, Constants.PackageType packageType = Constants.PackageType.LimitFree, int pageSize = 10, int pageIndex = 1)
        {
            string where = "AND p.id = @packageId ";
            string orderby = " order by pn.sortid desc, pn.id asc ";
            int rowCount = 0;

            return _packageService.GetPagerList(packageType, where, orderby, pageIndex, pageSize, out rowCount, new { packageId });
        }

        private dynamic GetRecClass(Constants.Novel.ClassSpeType speType)
        {
            if (speType == Constants.Novel.ClassSpeType.male)
                return new
                {
                    New = RecSection.LimitFree.MaleNewRec,
                    Hot = RecSection.LimitFree.MaleHotRec,
                    Limit = RecSection.LimitFree.MaleIndexRec
                };
            else
                return new
                {
                    New = RecSection.LimitFree.FemaleNewRec,
                    Hot = RecSection.LimitFree.FemaleHotRec,
                    Limit = RecSection.LimitFree.FemaleIndexRec
                };
        }

        private IEnumerable<ChapterView> GetChapterList(out int rowCount, int novelId, int contentType)
        {
            int pageIndex = 1;
            int pageSize = 2;
            string columns = " Id ,NovelId,VolumeName,ChapterName,ChapterCode,OnlineTime,WordSize";
            string where = " and NovelId = @NovelId and status = 1 and OnlineTime < @OnlineTime ";
            string orderBy = "order by chaptercode asc, id asc";

            object param = new
            {
                NovelId = novelId,
                OnlineTime = DateTime.Now
            };
            if (contentType == (int)Constants.Novel.ContentType.漫画)
            {
                pageSize = 10;
            }

            return _chapterService.GetPagerList(columns, where, orderBy, out  rowCount, pageIndex, pageSize, param);
        }

        private IEnumerable<CommentView> GetCommentList(out int rowCount, int novelId)
        {
            string columns = "c.Id ,c.NovelId,isnull(c.ReplyCount,0) as ReplyCount,c.Message,isnull( c.PropsId,0) as PropsId, c.PropsCount,c.AddTime,c.UserType, u.NickName as UserNickName,u.Icon as UserIcon,p.Icon as PropsIcon ";
            string table = " Users.NovelComment as c join dbo.Users as u on c.UserId =u.Id left join dbo.NovelProps p on c.PropsId=p.id ";
            string where = " and c.Status = 1 and c.NovelId = @novelId  and u.Status = 1 ";
            string orderBy = string.Format(" order by isnull(c.Sortid,0) desc, c.Addtime {0}, c.Id {0} ", SiteSection.Comment.IsReverseSort ? "desc" : "asc");

            return _commentService.GetCommentPagerList(columns, table, where, orderBy, out  rowCount, 1, 3, new { novelId });
        }

        #endregion 辅助方法
    }
}