using Component.Base;
using Component.Controllers.Novel;
using Component.Controllers.User;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Utility;
using ViewModel;
using YueDu.App_Code;

namespace YueDu.Controllers
{
    public class HomeController : UserInfoController
    {
        private IRecommendService _recommendService;
        private IADService _adService;
        private IPackageService _packageService;
        private IBookService _bookService;
        private IPromotionLinkService _promotionLinkService;
        private INovelPromotionChannelService _novelPromotionChannelService;
        private IChapterService _chapterService;
        private IUsersService _usersService;

        public HomeController(IRecommendService recommendService, IADService adService, IPackageService packageService, IBookService bookService, IPromotionLinkService promotionLinkService, INovelPromotionChannelService novelPromotionChannelService, IChapterService chapterService, IUsersService usersService)
        {
            _recommendService = recommendService;
            _adService = adService;
            _packageService = packageService;
            _bookService = bookService;
            _promotionLinkService = promotionLinkService;
            _novelPromotionChannelService = novelPromotionChannelService;
            _chapterService = chapterService;
            _usersService = usersService;
        }

        public ActionResult Index(Constants.Novel.ClassSpeType cst = Constants.Novel.ClassSpeType.female)
        {
            int timeOut = 30;

            if (SiteSection.Class.IsShowMale && !SiteSection.Class.IsShowFemale)
            {
                cst = Constants.Novel.ClassSpeType.male;
            }

            dynamic recClass = GetRecClass(cst);

            //首页-热门推荐
            var hotRec = DataContext.TryCache<IEnumerable<RecommendView>>(string.Format("{0}_BookIndex_HotRecommend", cst), () =>
            {
                return GetRecList(recClass.HotRecommend, 7);
            }, timeOut);

            //首页-潜力新作
            var newRec = DataContext.TryCache<IEnumerable<RecommendView>>(string.Format("{0}_BookIndex_PotentialNew", cst), () =>
            {
                return GetRecList(recClass.PotentialNew, 6);
            }, timeOut);

            //首页-主编力推
            var authorRec = DataContext.TryCache<IEnumerable<RecommendView>>(string.Format("{0}_BookIndex_EditorRecommend", cst), () =>
            {
                return GetRecList(recClass.EditorRecommend, 3);
            }, timeOut);

            //首页-热销专区
            var sellRec = DataContext.TryCache<IEnumerable<RecommendView>>(string.Format("{0}_BookIndex_HotSale", cst), () =>
            {
                return GetRecList(recClass.HotSale, 6);
            }, timeOut);

            //首页-听书专区
            var listenRec = DataContext.TryCache<IEnumerable<RecommendView>>("BookIndex_ListenRec", () =>
            {
                return GetRecList(RecSection.BookIndex.ListenRec, 3);
            }, timeOut);

            //首页-顶端广告位
            var headAD = DataContext.TryCache<IEnumerable<AD>>(string.Format("{0}_BookIndex_HeaderAD", cst), () =>
            {
                return GetADList(recClass.HeaderAD, 4);
            }, timeOut);

            //首页-中间广告位
            var middleAD = DataContext.TryCache<IEnumerable<AD>>(string.Format("{0}_BookIndex_MiddleAD", cst), () =>
            {
                return GetADList(recClass.MiddleAD, 1);
            }, timeOut);

            //首页-限时免费
            var freeRec = GetPackageList((int)recClass.Free, Constants.PackageType.LimitFree, 3);

            //获取本地最近阅读
            var recentRead = RecentReadContext.First(new string[] { Constants.SecurityKey.NovelRecentRead_CookieName.ToString(), Constants.SecurityKey.CartoonRecentRead_CookieName.ToString() });

            HomeView home = new HomeView()
            {
                HotRecList = new SimpleResponse<IEnumerable<RecommendView>>(!hotRec.IsNullOrEmpty(), hotRec),
                FreeRecList = new SimpleResponse<IEnumerable<PackageView>>(!freeRec.IsNullOrEmpty(), freeRec),
                NewRecList = new SimpleResponse<IEnumerable<RecommendView>>(!newRec.IsNullOrEmpty(), newRec),
                AuthorRecList = new SimpleResponse<IEnumerable<RecommendView>>(!authorRec.IsNullOrEmpty(), authorRec),
                SellRecList = new SimpleResponse<IEnumerable<RecommendView>>(!sellRec.IsNullOrEmpty(), sellRec),
                ListenRecList = new SimpleResponse<IEnumerable<RecommendView>>(!listenRec.IsNullOrEmpty(), listenRec),
                HeadADList = new SimpleResponse<IEnumerable<AD>>(!headAD.IsNullOrEmpty(), headAD),
                MiddleADList = new SimpleResponse<IEnumerable<AD>>(!middleAD.IsNullOrEmpty(), middleAD),
                RecentRead = new SimpleResponse<NovelRecentReadView>(!recentRead.IsObjectNullOrEmpty(), recentRead),
                ClassSpeType = cst.ToString()
            };

            return View(home);
        }

        public ActionResult T(int id = 0, string type = "")
        {
            Constants.Novel.PromotionType promotionType = Constants.Novel.PromotionType.none;
            string url = "";

            if (id > 0)
            {
                if (EnumHelper.TryParsebyName<Constants.Novel.PromotionType>(type, out promotionType))
                {
                    if (promotionType == Constants.Novel.PromotionType.pmc)
                    {
                        PromotionLink promotionLink = _promotionLinkService.Detail(id);
                        if (promotionLink != null && promotionLink.Id > 0
                            && !string.IsNullOrEmpty(promotionLink.ChannelCode)
                            && !string.IsNullOrEmpty(promotionLink.PromotionCode))
                        {
                            SetSessionHeaderInfoFromRoute(promotionLink.ChannelCode, promotionLink.PromotionCode);

                            string chapterUrl = "/chapter/detail/".GetChannelRouteUrl(promotionLink.ChannelCode);
                            int chapterCode = promotionLink.EndChapterCode > promotionLink.StartChapterCode ? promotionLink.EndChapterCode : 0;

                            if (promotionLink.FuncType == 1)
                            {
                                IDictionary<string, object> dict = new Dictionary<string, object>();
                                dict.Add("c", ChapterContext.GetRangeToken(promotionLink.EndChapterCode, promotionLink.FollowChapter));
                                dict.Add("rp", ChapterContext.GetReplyText(promotionLink.ReplyKeywords));
                                chapterUrl = "/preview".GetChannelRouteUrl(promotionLink.ChannelCode).SpliceUrl(dict);
                            }

                            url = ChapterContext.GetUrl(chapterUrl, promotionLink.NovelId, chapterCode, Constants.Novel.ChapterDirection.none, channelId: RouteChannelId);
                        }
                    }
                    else if (promotionType == Constants.Novel.PromotionType.rk)
                    {
                        PromotionLink promotionLink = _promotionLinkService.Detail(id);
                        if (promotionLink != null && promotionLink.Id > 0
                            && !string.IsNullOrEmpty(promotionLink.ChannelCode)
                            && !string.IsNullOrEmpty(promotionLink.PromotionCode))
                        {
                            SetSessionHeaderInfoFromRoute(promotionLink.ChannelCode, promotionLink.PromotionCode);

                            string chapterUrl = "/chapter/detail/".GetChannelRouteUrl(promotionLink.ChannelCode);

                            url = ChapterContext.GetUrl(chapterUrl, promotionLink.NovelId, promotionLink.FollowChapter, Constants.Novel.ChapterDirection.none, channelId: RouteChannelId);
                        }
                    }
                }
                else
                {
                    NovelPromotionChannel model = _novelPromotionChannelService.Detail(id);
                    if (model != null && model.NovelId > 0 && model.ChapterCode >= 0)
                    {
                        string chapterUrl = "chapter/detail";
                        string channelId = model.ChannelId;

                        if (!string.IsNullOrEmpty(channelId))
                        {
                            chapterUrl = string.Concat(chapterUrl, "?channelid=", channelId);
                        }
                        url = ChapterContext.GetUrl(chapterUrl, model.NovelId, model.ChapterCode, channelId: RouteChannelId);
                    }
                }
            }

            if (string.IsNullOrEmpty(url))
            {
                url = DataContext.GetErrorUrl(channelId: RouteChannelId);
            }

            return Redirect(url);
        }

        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = UrlParameterHelper.UrlEncode(returnUrl);
            }
            ViewData.Model = returnUrl;

            return View();
        }

        public ActionResult Qt_Login(int qtlu_id = 0)
        {
            string url = "";
            if (qtlu_id > 0)
            {
                UsersView usersInfo = _usersService.GetDetail(qtlu_id);
                if (usersInfo != null && usersInfo.Id > 0)
                {
                    currentUser.UserId = usersInfo.Id;
                    currentUser.UserName = usersInfo.UserName;
                    currentUser.NickName = usersInfo.NickName;
                    SaveUserInfo(currentUser);
                    url = ("/").GetChannelRouteUrl(RouteChannelId);
                }
            }

            if (string.IsNullOrEmpty(url))
            {
                url = DataContext.GetErrorUrl(channelId: RouteChannelId);
            }

            return Redirect(url);
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
            string where = "and a.ClassId = @classId ";
            string orderby = " order by a.onlinetime desc, a.sortid desc ";
            int rowCount = 0;

            return _adService.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, new { classId });
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
                    HotRecommend = RecSection.BookIndex.MaleHotRec,//热门推荐
                    PotentialNew = RecSection.BookIndex.MaleNewRec,//潜力新作
                    EditorRecommend = RecSection.BookIndex.MaleEditorRec,//主编力推
                    HotSale = RecSection.BookIndex.MaleSaleRec,//热销专区
                    Free = RecSection.LimitFree.MaleIndexRec,//限时免费
                    HeaderAD = RecSection.BookIndex.MaleTopAd,//顶部广告位
                    MiddleAD = RecSection.BookIndex.MaleMiddleAd//中间广告位
                };
            else
                return new
                {
                    HotRecommend = RecSection.BookIndex.FemaleHotRec,//热门推荐
                    PotentialNew = RecSection.BookIndex.FemaleNewRec,//潜力新作
                    EditorRecommend = RecSection.BookIndex.FemaleEditorRec,//主编力推
                    HotSale = RecSection.BookIndex.FemaleSaleRec,//热销专区
                    Free = RecSection.LimitFree.FemaleIndexRec,//限时免费
                    HeaderAD = RecSection.BookIndex.FemaleTopAd,//顶部广告位
                    MiddleAD = RecSection.BookIndex.FemaleMiddleAd//中间广告位
                };
        }

        #endregion 辅助方法
    }
}