using Component.Base;
using Component.Controllers.Novel;
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
    public class ChapterController : ChapterDetailController
    {
        private IBookService _bookService;
        private IRecommendService _recommendService;
        private IBookmarkService _bookmarkService;
        private IExtendChapterService _extendChapterService;
        private IADService _adService;
        private IOrderService _orderService;

        public ChapterController(IBookService bookService, IChapterService chapterService, IRecommendService recommendService, IBookmarkService bookmarkService, IExtendChapterService extendChapterService, IADService adService, IOrderService orderService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
            _recommendService = recommendService;
            _bookmarkService = bookmarkService;
            _extendChapterService = extendChapterService;
            _adService = adService;
            _orderService = orderService;
        }

        #region 章节列表

        /// <summary>
        /// 章节目录/漫画目录
        /// </summary>
        public ActionResult List(int? novelId = 0, int sort = 0, int pageIndex = 1, int pageSize = 10)
        {
            if (!novelId.HasValue || novelId <= 0)
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));

            int nid = novelId.ToInt();

            var novelInfo = _bookService.GetNovel(nid);

            if (novelInfo == null || novelInfo.Id <= 0)
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));

            int contentType = novelInfo.ContentType;

            int rowCount = 0;
            if (novelInfo.ContentType == (int)Constants.Novel.ContentType.漫画)
                pageSize = 20;
            var chapterList = GetChapterList(out rowCount, nid, sort, pageIndex, pageSize);

            IPagerInfo pageInfo = new PagerInfo(rowCount, pageIndex, pageSize);

            NovelChapterView chapterInfo = new NovelChapterView
            {
                NovelInfo = new SimpleResponse<Novel>(novelInfo != null && novelInfo.Id > 0, novelInfo),
                ChapterList = new SimpleResponse<IEnumerable<ChapterView>>(!chapterList.IsNullOrEmpty(), chapterList),
                PageInfo = pageInfo,
                RecClassId = contentType == (int)Constants.Novel.ContentType.漫画 ? RecSection.CartoonChapterList.GuessRec : RecSection.BookChapterList.GuessRec, //猜你喜欢-推荐位id
                Sort = sort
            };

            if (novelInfo.ContentType == (int)Constants.Novel.ContentType.漫画)
                return View("/Views/CartoonChapter/List.cshtml", chapterInfo);
            else
                return View(chapterInfo);
        }

        private IEnumerable<ChapterView> GetChapterList(out int rowCount, int novelId = 0, int sort = 0, int pageIndex = 1, int pageSize = 10)
        {
            string columns = " Id ,NovelId,VolumeName,ChapterName,ChapterCode,OnlineTime,WordSize";
            string where = " and NovelId = @NovelId and status = 1 and OnlineTime <@OnlineTime ";
            string orderBy = (sort == (int)Constants.SortDirection.desc) ? " order by chaptercode desc, id desc" : "order by chaptercode asc, id asc";

            object param = new
            {
                NovelId = novelId,
                OnlineTime = DateTime.Now
            };

            var result = _chapterService.GetPagerList(columns, where, orderBy, out  rowCount, pageIndex, pageSize, param);

            return result;
        }

        #endregion 章节列表

        #region

        private IEnumerable<AD> GetADList(int classId, int novelId, int pageSize)
        {
            string where = "and a.ClassId = @classId and a.fid <> @novelId";
            string orderby = " order by a.onlinetime desc, a.sortid desc ";

            int adIndex = UrlParameterHelper.GetParams("dx").ToInt();
            adIndex = (adIndex > 0) ? adIndex : 1;

            int rowCount = 0;

            var adList = DataContext.TryCache<IEnumerable<AD>>(string.Format("{0}{1}{2}_ChapterDetail_AD", classId, pageSize, adIndex), () =>
            {
                return _adService.GetPagerList(where, orderby, adIndex, pageSize, out rowCount, new { classId, novelId });
            }, 60);

            rowCount = DataContext.TryCache<int>(string.Format("{0}{1}{2}_ChapterDetail_AD_RowCount", classId, pageSize, adIndex), () =>
            {
                return rowCount;
            }, 60);

            if (!adList.IsNullOrEmpty<AD>())
            {
                ViewBag.AdIndex = (adIndex >= (int)Math.Ceiling((double)rowCount / pageSize)) ? 1 : adIndex + 1;
            }

            return adList;
        }

        #endregion

        #region

        public void Read()
        {
            string userName = currentUser.UserName;
            if (!string.IsNullOrEmpty(userName) && NovelId > 0)
            {
                ILogService service = DataContext.ResolveService<ILogService>();
                NovelReadRecordInfo model = service.GetRecentChapter(userName, NovelId);
                if (model != null && model.Id > 0)
                {
                    ChapterCode = model.ChapterCode;
                }
            }

            Redirect(ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, ChapterDirection, channelId: RouteChannelId));
        }

        #endregion

        #region 正文

        public ActionResult Detail()
        {
            SetReadMode();
            Novel novel = _bookService.GetNovel(NovelId);
            if (novel != null && novel.Id > 0)
            {
                InitializeChapterPager();

                Chapter chapter = _chapterService.GetChapter(NovelId, ChapterCode, ChapterDirection, out ChapterCode);
                if (chapter != null && chapter.Id > 0 && IsRead(_orderService))
                {
                    #region

                    bool isPreChapterCode = false;
                    bool isNextChapterCode = false;
                    string url = GetChapterPager("/chapter/detail", ChapterCode, out isPreChapterCode, out isNextChapterCode);

                    #endregion

                    #region 推荐阅读

                    //推荐阅读 - 轮循广告
                    int adClassId = RecSection.BookChapterDetail.Ad;
                    IEnumerable<AD> adList = GetADList(adClassId, novel.Id, 3);

                    #endregion

                    //阅读记录
                    ReadLog(currentUser.UserName, chapter.NovelId, chapter.Id, chapter.ChapterCode);

                    //最近阅读日志
                    RecentReadContext.Save(novel, chapter.ChapterName, ChapterCode, RouteChannelId, currentUser.UserId);

                    //是否收藏
                    bool isMark = _bookmarkService.Exists(NovelId, currentUser.UserName);

                    //漫画目录
                    IEnumerable<ExtendChapterView> extendChapterList = GetExtendChapterList(novel);

                    // 千字价格
                    int chapterWordSizeFee = GetChapterWordSizeFee(novel.ChapterWordSizeFee);
                    //正文中插入用户信息
                    string encryptInfo = string.IsNullOrEmpty(currentUser.UserName) ? null : string.Concat("14", ";", currentUser.UserName.Substring(1));

                    ChapterDetailView detailView = new ChapterDetailView()
                    {
                        Novel = new SimpleResponse<Novel>(true, novel),
                        Chapter = new SimpleResponse<Chapter>(true, chapter),
                        ChapterFee = GetFee(chapter.WordSize, chapterWordSizeFee),
                        ChapterContent = StringHelper.ConvertAndSignTxt((novel.ContentType == (int)Constants.Novel.ContentType.小说) ? FileHelper.ReadFile(FileHelper.MergePath("\\", new string[] { novel.FilePath, chapter.FileName }), chapter.ChapterName) : "", Constants.SecurityKey.userAESId, encryptInfo),
                        IsPreChapterCode = isPreChapterCode,
                        IsNextChapterCode = isNextChapterCode,
                        PreChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.pre, channelId: RouteChannelId),
                        NextChapterUrl = ChapterContext.GetUrl(url, NovelId, ChapterCode, Constants.Novel.ChapterDirection.next, channelId: RouteChannelId),
                        AdList = new SimpleResponse<IEnumerable<AD>>(!adList.IsNullOrEmpty<AD>(), adList),
                        IsMark = isMark,
                        ExtendChapterList = new SimpleResponse<IEnumerable<ExtendChapterView>>(!extendChapterList.IsNullOrEmpty<ExtendChapterView>(), extendChapterList),
                        ChapterAudioUrl = GetAudioUrl(novel.ContentType, chapter.FileName),
                        HitUrl = GetHitUrl(),
                        ShowQrCodeMinChapterCode = StringHelper.ToInt(UrlParameterHelper.GetParams("qrdx"))
                    };

                    if (novel.ContentType == (int)Constants.Novel.ContentType.漫画)
                    {
                        return View("/views/cartoonchapter/detail.cshtml", detailView);
                    }
                    else if (novel.ContentType == (int)Constants.Novel.ContentType.听书)
                    {
                        return View("/views/audiochapter/detail.cshtml", detailView);
                    }
                    else
                    {
                        return View(detailView);
                    }
                }
                else
                {
                    if (novel.FeeType == (int)Constants.Novel.FeeType.chapter || novel.FeeType == (int)Constants.Novel.FeeType.novelchapter)
                    {
                        return Redirect(ChapterContext.GetUrl("/preorder/chapter", NovelId, ChapterCode, channelId: RouteChannelId));
                    }
                    else if (novel.FeeType == (int)Constants.Novel.FeeType.novel)
                    {
                        return Redirect(ChapterContext.GetUrl("/preorder/novel", NovelId, ChapterCode, channelId: RouteChannelId));
                    }
                }
            }

            return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));
        }

        protected IEnumerable<ExtendChapterView> GetExtendChapterList(Novel novel)
        {
            List<ExtendChapterView> filePathList = new List<ExtendChapterView>();
            if (novel.ContentType == (int)Constants.Novel.ContentType.漫画)
            {
                ExtendChapter extendChapterInfo = _extendChapterService.GetExtendChapter(NovelId, ChapterCode);
                if (!extendChapterInfo.IsNullOrEmpty() && extendChapterInfo.Id > 0)
                {
                    string[] files = FileHelper.GetFiles(FileHelper.MapPath(FileHelper.MergePath("/", new string[] { novel.FilePath, extendChapterInfo.ChapterPath, extendChapterInfo.MediumPath1 })));
                    if (!StringHelper.IsNullOrEmpty(files))
                    {
                        IList<string> list = files.OrderBy<string, string>(t => t).ToList<string>();
                        if (!StringHelper.IsNullOrEmpty(list))
                        {
                            string rootDir = Server.MapPath(novel.FilePath);
                            foreach (string item in list)
                            {
                                filePathList.Add(new ExtendChapterView()
                                {
                                    FilePath = StringHelper.GetPrefixUrl(FileHelper.MergePath("/", new string[] { novel.FilePath, item.Replace(rootDir, "").Replace("\\", "/") }), StringHelper.CartoonPrefixUrl)
                                });
                            }
                        }
                    }
                }
            }
            return filePathList;
        }

        protected string GetAudioUrl(int contentType, string fileName)
        {
            string audioUrl = "";
            if (contentType == (int)Constants.Novel.ContentType.听书)
            {
                audioUrl = StringHelper.GetPrefixUrl(fileName, StringHelper.AudioPrefixUrl);
            }
            return audioUrl.IsNullOrEmpty() ? "#" : audioUrl;
        }

        #endregion

        #region

        protected string GetHitUrl()
        {
            string url = "";

            int index = StringHelper.ToInt(SessionHelper.Get(Constants.SecurityKey.Hits_SessionName));

            int rowCount = 0;
            IGenerateHitsService service = DataContext.ResolveService<IGenerateHitsService>();
            IEnumerable<string> list = service.GetPagerList(index + 1, 1, out rowCount);
            if (!StringHelper.IsNullOrEmpty<string>(list))
            {
                url = list.FirstOrDefault();

                SessionHelper.Set(Constants.SecurityKey.Hits_SessionName, index + 1);
            }

            return url;
        }

        #endregion
    }
}