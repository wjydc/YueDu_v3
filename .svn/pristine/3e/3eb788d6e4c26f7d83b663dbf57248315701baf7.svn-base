using Component.Base;
using Component.Controllers.Novel;
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
    public class PreviewController : ChapterPreviewController
    {
        private IBookService _bookService;
        private IChapterService _chapterService;
        private IOrderService _orderService;

        public PreviewController(IBookService bookService, IChapterService chapterService, IOrderService orderService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            #region

            string absoluteUrl = "";
            if (EnvSettings.Domain.IsInvalid(out absoluteUrl))
            {
                return Redirect(absoluteUrl);
            }

            #endregion

            Novel novel = _bookService.GetNovel(NovelId);
            if (novel != null && novel.Id > 0)
            {
                InitializeChapterPager();
                bool IsNovel = novel.ContentType == (int)Constants.Novel.ContentType.小说;

                if (IsNovel)
                {
                    Chapter chapter = _chapterService.GetChapter(NovelId, ChapterCode, ChapterDirection, out ChapterCode);

                    if (chapter != null && chapter.Id > 0 && IsRead(_orderService))
                    {
                        // 分页
                        string replyText = null;
                        bool isPreChapterCode = false;
                        bool isNextChapterCode = false;
                        string url = GetChapterPager("/preview", ChapterCode, out replyText, out isPreChapterCode, out isNextChapterCode);

                        //小说阅读记录
                        ReadLog(currentUser.UserName, NovelId, chapter.Id, ChapterCode);

                        //最近阅读日志
                        RecentReadContext.Save(novel, chapter.ChapterName, ChapterCode, RouteChannelId, currentUser.UserId);

                        ChapterDetailView detailView = new ChapterDetailView()
                        {
                            Novel = new SimpleResponse<Novel>(true, novel),
                            Chapter = new SimpleResponse<Chapter>(true, chapter),
                            ChapterContent = IsNovel ? FileHelper.ReadFile(FileHelper.MergePath("\\", new string[] { novel.FilePath, chapter.FileName }), chapter.ChapterName) : "",
                            IsPreChapterCode = isPreChapterCode,
                            IsNextChapterCode = isNextChapterCode,
                            PreChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.pre, channelId: RouteChannelId),
                            NextChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.next, channelId: RouteChannelId),
                            ReplyText = replyText
                        };

                        return View(detailView);
                    }
                }
            }

            return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));
        }

        #region 章节详情日志，小说阅读记录

        /// <summary>
        /// 章节详情日志，小说阅读记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="novelId"></param>
        /// <param name="chapterId"></param>
        /// <param name="chapterCode"></param>
        protected void ReadLog(string userName, int novelId, int chapterId, int chapterCode)
        {
            ChapterLogInfo model = new ChapterLogInfo();
            model = GetLogInfo(model) as ChapterLogInfo;
            model.CookieId = GetCookieId();
            model.NovelId = novelId;
            model.ChapterId = chapterId;
            model.ChapterCode = chapterCode;
            model.AddTime = DateTime.Now;
            model.UserName = userName;

            NovelReadRecordInfo novelReadRecordInfo = new NovelReadRecordInfo();
            novelReadRecordInfo.Position = 0;
            novelReadRecordInfo.RecentReadTime = System.DateTime.Now;

            ILogService service = DataContext.ResolveService<ILogService>();
            service.ChapterReadLog(model, novelReadRecordInfo);
        }

        #endregion 章节详情日志，小说阅读记录
    }
}