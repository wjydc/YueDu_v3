using Component.Base;
using Component.Controllers.Novel;
using Component.Filter;
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
    public class PreOrderController : ChapterDetailController
    {
        private IBookService _bookService;
        private IRecommendService _recommendService;
        private IUsersService _usersService;
        private IBookmarkService _bookmarkService;
        private IOrderService _orderService;

        public PreOrderController(IBookService bookService, IChapterService chapterService, IRecommendService recommendService, IUsersService usersService, IBookmarkService bookmarkService, IOrderService orderService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
            _recommendService = recommendService;
            _usersService = usersService;
            _bookmarkService = bookmarkService;
            _orderService = orderService;
        }

        /// <summary>
        /// 按章订购页
        /// </summary>
        /// <returns></returns>
        [Authorization]
        public ActionResult Chapter()
        {
            SetReadMode();
            Novel novel = _bookService.GetNovel(NovelId);
            if (novel != null && novel.Id > 0)
            {
                if (novel.FeeType == (int)Constants.Novel.FeeType.novel)
                {
                    return Redirect(DataContext.GetErrorUrl(ErrorMessage.该小说无法按章计费, channelId: RouteChannelId));
                }

                InitializeChapterPager();

                Chapter chapter = _chapterService.GetChapter(NovelId, ChapterCode, ChapterDirection, out ChapterCode);
                if (chapter != null && chapter.Id > 0 && IsRead(_orderService))
                {
                    return Redirect(ChapterContext.GetUrl("/chapter/detail", chapter.NovelId, chapter.ChapterCode, channelId: RouteChannelId));
                }

                if (chapter != null && chapter.Id > 0)
                {
                    int i = 0;
                    if (int.TryParse(StringHelper.ToString(SessionHelper.Get("ancp")), out i) && i == NovelId)
                    {
                        return Redirect(ChapterContext.GetUrl("/order/chapter", chapter.NovelId, chapter.ChapterCode, channelId: RouteChannelId));
                    }

                    #region

                    bool isPreChapterCode = false;
                    bool isNextChapterCode = false;
                    string url = GetChapterPager("/chapter/detail", ChapterCode, out isPreChapterCode, out isNextChapterCode);

                    #endregion

                    bool isMark = _bookmarkService.Exists(NovelId, currentUser.UserName);

                    // 千字价格
                    int chapterWordSizeFee = GetChapterWordSizeFee(novel.ChapterWordSizeFee);
                    ChapterDetailView detailView = new ChapterDetailView()
                    {
                        Novel = new SimpleResponse<Novel>(true, novel),
                        Chapter = new SimpleResponse<Chapter>(true, chapter),
                        ChapterWordSizeFee = chapterWordSizeFee,
                        ChapterFee = GetFee(chapter.WordSize, chapterWordSizeFee),
                        NovelFee = GetFee((decimal)(novel.WordSize * 0.95f), chapterWordSizeFee),
                        ChapterContent = (novel.ContentType == (int)Constants.Novel.ContentType.小说) ? StringHelper.CutString(FileHelper.ReadFile(FileHelper.MergePath("\\", new string[] { novel.FilePath, chapter.FileName }), chapter.ChapterName), 100, true) : "",
                        IsPreChapterCode = isPreChapterCode,
                        IsNextChapterCode = isNextChapterCode,
                        PreChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.pre, channelId: RouteChannelId),
                        NextChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.next, channelId: RouteChannelId),
                        UserBalance = GetUserBalance(),
                        IsMark = isMark
                    };

                    if (novel.ContentType == (int)Constants.Novel.ContentType.漫画)
                    {
                        return View("/views/cartoonchapter/order.cshtml", detailView);
                    }
                    else if (novel.ContentType == (int)Constants.Novel.ContentType.听书)
                    {
                        return View("/views/audiochapter/order.cshtml", detailView);
                    }
                    else
                    {
                        return View("/views/chapter/order.cshtml", detailView);
                    }
                }
                else
                {
                    //章节不存在
                    return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说章节不存在, channelId: RouteChannelId));
                }
            }
            else
            {
                //小说不存在
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));
            }
        }

        /// <summary>
        /// 按本订购页
        /// </summary>
        /// <returns></returns>
        [Authorization]
        public ActionResult Novel()
        {
            SetReadMode();
            Novel novel = _bookService.GetNovel(NovelId);
            if (novel != null && novel.Id > 0)
            {
                if (novel.FeeType == (int)Constants.Novel.FeeType.chapter)
                {
                    return Redirect(DataContext.GetErrorUrl(ErrorMessage.该小说无法按本计费, channelId: RouteChannelId));
                }

                InitializeChapterPager();

                Chapter chapter = _chapterService.GetChapter(NovelId, ChapterCode, ChapterDirection, out ChapterCode);
                if (chapter != null && chapter.Id > 0 && IsOrder(_orderService))
                {
                    return Redirect(ChapterContext.GetUrl("/chapter/detail", chapter.NovelId, chapter.ChapterCode, channelId: RouteChannelId));
                }

                if (chapter != null && chapter.Id > 0)
                {
                    #region

                    bool isPreChapterCode = false;
                    bool isNextChapterCode = false;
                    string url = GetChapterPager("/chapter/detail", ChapterCode, out isPreChapterCode, out isNextChapterCode);

                    #endregion

                    bool isMark = _bookmarkService.Exists(NovelId, currentUser.UserName);

                    // 千字价格
                    int chapterWordSizeFee = GetChapterWordSizeFee(novel.ChapterWordSizeFee);
                    ChapterDetailView detailView = new ChapterDetailView()
                    {
                        Novel = new SimpleResponse<Novel>(true, novel),
                        Chapter = new SimpleResponse<Chapter>(true, chapter),
                        ChapterWordSizeFee = chapterWordSizeFee,
                        ChapterFee = GetFee(chapter.WordSize, chapterWordSizeFee),
                        NovelFee = GetFee((decimal)(novel.WordSize * 0.95f), chapterWordSizeFee),
                        ChapterContent = (novel.ContentType == (int)Constants.Novel.ContentType.小说) ? StringHelper.CutString(FileHelper.ReadFile(FileHelper.MergePath("\\", new string[] { novel.FilePath, chapter.FileName }), chapter.ChapterName), 100, true) : "",
                        IsPreChapterCode = isPreChapterCode,
                        IsNextChapterCode = isNextChapterCode,
                        PreChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.pre, channelId: RouteChannelId),
                        NextChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.next, channelId: RouteChannelId),
                        UserBalance = GetUserBalance(),
                        IsMark = isMark
                    };

                    if (novel.ContentType == (int)Constants.Novel.ContentType.漫画)
                    {
                        return View("/views/cartoon/order.cshtml", detailView);
                    }
                    else if (novel.ContentType == (int)Constants.Novel.ContentType.听书)
                    {
                        return View("/views/audio/order.cshtml", detailView);
                    }
                    else
                    {
                        return View("/views/book/order.cshtml", detailView);
                    }
                }
                else
                {
                    //章节不存在
                    return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说章节不存在, channelId: RouteChannelId));
                }
            }
            else
            {
                //小说不存在
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));
            }
        }

        /// <summary>
        /// 听书包月页
        /// </summary>
        /// <returns></returns>
        public ActionResult AllPackage()
        {
            IEnumerable<RecommendView> recList = GetRecList(RecSection.BookIndex.ListenRec, 10);

            //获取包月信息
            string where = string.Format("and status = {0} AND PackageId = 0  AND OrderContentType & 2 = 2 AND begintime <= getdate() AND endtime >= getdate() AND UserId = {1}", (int)Constants.Status.yes, currentUser.UserId);
            PackageOrderInfo packageOrderInfo = _orderService.GetPackageOrder(where);

            AudioView audioView = new AudioView()
            {
                HotRecList = new SimpleResponse<IEnumerable<RecommendView>>(!recList.IsNullOrEmpty(), recList),
                UserBalance = GetUserBalance(),
                AllAudioFee = SiteSection.Audio.AllPackageFee,
                IsPackageOrder = !packageOrderInfo.IsNullOrEmpty() && packageOrderInfo.Id > 0
            };

            return View("/views/audio/allpackage.cshtml", audioView);
        }

        private int GetUserBalance()
        {
            UsersView userInfo = _usersService.GetDetail(currentUser.UserName, (int)Constants.Status.yes);
            if (!userInfo.IsNullOrEmpty<UsersView>() && userInfo.Id > 0)
            {
                return userInfo.Fee;
            }

            return 0;
        }

        private IEnumerable<RecommendView> GetRecList(int classId = 0, int pageSize = 10, int pageIndex = 1)
        {
            string where = " and r.recclassid = @ClassId ";
            string orderBy = " order by r.onlinetime desc, r.sortid desc, r.id desc";
            int recRowCount = 0;
            object param = new
            {
                ClassId = classId
            };

            return _recommendService.GetPagerList(where, orderBy, pageIndex, pageSize, out  recRowCount, param);
        }
    }
}