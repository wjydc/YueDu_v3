using Component.Controllers.Novel;
using Model;
using Model.Common;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViewModel;
using Utility;
using System;
using Component.Base;
using YueDu.App_Code;
using Component.Filter;

namespace YueDu.Controllers
{
    public class OrderController : ChapterRedirectController
    {
        private IRechargeFeeConfigService _rechargeFeeConfigService;
        private IRechargeService _rechargeService;
        private IBookService _bookService;
        private IChapterService _chapterService;
        private IOrderService _orderService;
        private IRechargeTypeService _rechargeTypeService;
        private IUsersService _usersService;

        public OrderController(IRechargeFeeConfigService rechargeFeeConfigService, IRechargeService rechargeService, IBookService bookService, IChapterService chapterService, IOrderService orderService, IRechargeTypeService rechargeTypeService, IUsersService usersService)
        {
            _rechargeFeeConfigService = rechargeFeeConfigService;
            _rechargeService = rechargeService;
            _bookService = bookService;
            _chapterService = chapterService;
            _orderService = orderService;
            _rechargeTypeService = rechargeTypeService;
            _usersService = usersService;
        }

        [Authorization]
        public ActionResult Recharge()
        {
            IEnumerable<RechargeFeeConfigInfo> configList = _rechargeFeeConfigService.GetAll("*", " and Status=1", " order by SortId desc");
            List<RechargeView> list = new List<RechargeView>();
            List<PayInfo> payList = GetPayTypeList();
            payList.ForEach(x =>
            {
                list.Add(new RechargeView()
                {
                    PayInfo = x,
                    PayItems = configList.ToList().Where(c => c.PayType == x.PayType).ToList()
                });
            });

            var userInfo = _usersService.GetDetail(currentUser.UserName);
            ViewBag.Fee = userInfo == null ? 0 : userInfo.Fee;

            return View(new SimpleResponse<List<RechargeView>>(list.Count > 0, list));
        }

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <returns></returns>
        [Authorization]
        public ActionResult RechargeRecord(int pageIndex = 1, int pageSize = 10)
        {
            int rowCount = 0;
            IEnumerable<RechargeInfo> list = _rechargeService.GetPagerList("Fee,Name,CompleteTime", " and Status=1 and UserId=@UserId", " order by CompleteTime desc", out rowCount, pageIndex, pageSize, new { UserId = currentUser.UserId });

            return View(new SimpleResponse<IEnumerable<RechargeInfo>>(list != null && list.Count() > 0, list));
        }

        #region 辅助方法

        private List<PayInfo> GetPayTypeList()
        {
            List<PayInfo> list = new List<PayInfo>();
            List<RechargeType> typeList = _rechargeTypeService.GetAll(" and Status=1 and ClientName='wap'", "order by SortId desc").ToList();
            typeList.ForEach(x =>
            {
                Model.Common.Constants.PayType tempValue = default(Model.Common.Constants.PayType);
                if (EnumHelper.TryParsebyName<Model.Common.Constants.PayType>(x.Tag, out tempValue))
                {
                    if (!StringHelper.GetUserAgent().ToLower().Contains("micromessenger") && tempValue == Constants.PayType.wechat)
                    {

                    }
                    else
                    {
                        list.Add(new PayInfo(x.Name, (int)tempValue, x.FPayType, x.Description));
                    }
                }
            });

            return list;
        }

        #endregion 辅助方法

        #region 按本/按章订购

        #region 按章订购

        [Authorization]
        public ActionResult Chapter()
        {
            string url = "";

            Novel novelInfo = _bookService.GetNovel(NovelId, (int)Constants.Status.yes);
            if (!novelInfo.IsNullOrEmpty<Novel>() && novelInfo.Id > 0)
            {
                ErrorMessage errorMessage = ErrorMessage.失败;
                switch (novelInfo.FeeType)
                {
                    case (int)Constants.Novel.FeeType.chapter:
                    case (int)Constants.Novel.FeeType.novelchapter:
                        int result = Chapter(novelInfo);
                        if (EnumHelper.TryParsebyValue<ErrorMessage>(result, out errorMessage))
                        {
                            switch (errorMessage)
                            {
                                case ErrorMessage.成功:
                                    SessionHelper.Set("ancp", NovelId);
                                    url = ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.余额不足:
                                    url = StringHelper.GetReturnUrl("/order/recharge", UrlParameterHelper.GetParams("returnUrl"), channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.已购买过该章节:
                                case ErrorMessage.免费章节内不用购买:
                                    url = ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.用户不存在:
                                    url = DataContext.GetErrorUrl(ErrorMessage.用户不存在, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.小说不存在:
                                    url = DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.小说章节不存在:
                                    url = DataContext.GetErrorUrl(ErrorMessage.小说章节不存在, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.该章节统计字数有误无法购买:
                                    url = DataContext.GetErrorUrl(ErrorMessage.该章节统计字数有误无法购买, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.该小说无法按章计费:
                                    url = DataContext.GetErrorUrl(ErrorMessage.该小说无法按章计费, channelId: RouteChannelId);
                                    break;

                                default:
                                    url = DataContext.GetErrorUrl(ErrorMessage.未知错误, channelId: RouteChannelId);
                                    break;
                            }
                        }
                        break;

                    case (int)Constants.Novel.FeeType.novel:
                        url = ChapterContext.GetUrl("/preorder/novel", NovelId, ChapterCode, channelId: RouteChannelId);
                        break;
                }
            }

            return Redirect(url);
        }

        private int Chapter(Novel novelInfo)
        {
            int result = (int)ErrorMessage.失败;

            if (!novelInfo.IsNullOrEmpty<Novel>() && novelInfo.Id > 0
                && (novelInfo.FeeType == (int)Constants.Novel.FeeType.chapter || novelInfo.FeeType == (int)Constants.Novel.FeeType.novelchapter))
            {
                Chapter chapter = _chapterService.GetChapter(NovelId, ChapterCode, ChapterDirection, out ChapterCode);
                if (!chapter.IsNullOrEmpty<Chapter>() && chapter.Id > 0)
                {
                    ChapterOrderInfo model = new ChapterOrderInfo();
                    model = GetLogInfo(model) as ChapterOrderInfo;
                    model.NovelId = NovelId;
                    model.ChapterCode = ChapterCode;
                    model.UserName = currentUser.UserName;
                    model.WordSize = chapter.WordSize;

                    // 千字价格
                    int chapterWordSizeFee = novelInfo.ChapterWordSizeFee;
                    chapterWordSizeFee = chapterWordSizeFee > 0 ? chapterWordSizeFee : SiteSection.Novel.ChapterWordSizeFee;
                    int fee = GetFee(chapter.WordSize, chapterWordSizeFee);
                    model.FeeType = 0;
                    model.Fee = fee;
                    model.Cash = fee;
                    model.Status = (int)Constants.Status.yes;
                    model.OrderTime = DateTime.Now;
                    result = _orderService.OrderChapter(model);
                }
            }

            return result;
        }

        #endregion 按章订购

        #region 按本订购

        [Authorization]
        public ActionResult Novel()
        {
            string url = "";

            Novel novelInfo = _bookService.GetNovel(NovelId, (int)Constants.Status.yes);
            if (!novelInfo.IsNullOrEmpty<Novel>() && novelInfo.Id > 0)
            {
                ErrorMessage errorMessage = ErrorMessage.失败;
                switch (novelInfo.FeeType)
                {
                    case (int)Constants.Novel.FeeType.novel:
                    case (int)Constants.Novel.FeeType.novelchapter:
                        int result = Novel(novelInfo);
                        if (EnumHelper.TryParsebyValue<ErrorMessage>(result, out errorMessage))
                        {
                            switch (errorMessage)
                            {
                                case ErrorMessage.成功:
                                    url = ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.余额不足:
                                    url = StringHelper.GetReturnUrl("/order/recharge", UrlParameterHelper.GetParams("returnUrl"), channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.已购买过该小说:
                                    url = ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.用户不存在:
                                    url = DataContext.GetErrorUrl(ErrorMessage.用户不存在, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.小说不存在:
                                    url = DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.小说章节不存在:
                                    url = DataContext.GetErrorUrl(ErrorMessage.小说章节不存在, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.该小说标价有误无法购买:
                                    url = DataContext.GetErrorUrl(ErrorMessage.该小说标价有误无法购买, channelId: RouteChannelId);
                                    break;

                                case ErrorMessage.该小说无法按本计费:
                                    url = DataContext.GetErrorUrl(ErrorMessage.该小说无法按本计费, channelId: RouteChannelId);
                                    break;

                                default:
                                    url = DataContext.GetErrorUrl(ErrorMessage.未知错误, channelId: RouteChannelId);
                                    break;
                            }
                        }
                        break;

                    case (int)Constants.Novel.FeeType.chapter:
                        url = ChapterContext.GetUrl("/preorder/chapter", NovelId, ChapterCode, channelId: RouteChannelId);
                        break;
                }
            }

            return Redirect(url);
        }

        private int Novel(Novel novelInfo)
        {
            int result = (int)ErrorMessage.失败;

            if (!novelInfo.IsNullOrEmpty<Novel>() && novelInfo.Id > 0
                && (novelInfo.FeeType == (int)Constants.Novel.FeeType.novel || novelInfo.FeeType == (int)Constants.Novel.FeeType.novelchapter))
            {
                int fee = novelInfo.Fee;
                NovelOrderInfo model = new NovelOrderInfo();
                model = GetLogInfo(model) as NovelOrderInfo;
                model.NovelId = NovelId;
                model.UserName = currentUser.UserName;
                model.FeeType = 0;
                model.Fee = fee;
                model.Cash = fee;
                model.Status = 1;
                model.OrderTime = DateTime.Now;
                model.Balance = 0;
                model.BindFee = 0;
                model.FeeId = 0;
                model.Integral = 0;
                model.NovelName = "";
                model.OrderCode = ""; ;
                model.PayChannel = 0;
                model.Rebate = 0;
                model.RebateExpression = "";
                model.RebateFee = 0;
                model.WordSize = 0;
                result = _orderService.OrderNovel(model);
            }

            return result;
        }

        #endregion 按本订购

        #endregion 按本/按章订购

        #region 听书全站包月

        [Authorization]
        public ActionResult AllAudioPackage()
        {
            string url = "";

            ErrorMessage errorMessage = ErrorMessage.失败;
            PackageOrderInfo model = new PackageOrderInfo();
            model = GetLogInfo(model) as PackageOrderInfo;
            model.UserName = currentUser.UserName;
            model.Fee = SiteSection.Audio.AllPackageFee;
            model.Cash = SiteSection.Audio.AllPackageFee;
            model.Status = (int)Constants.Status.yes;
            model.OrderTime = DateTime.Now;
            model.AutoRenew = 0;
            model.Balance = 0;
            model.BeginTime = DateTime.Now;
            model.EndTime = DateTime.Now;
            model.FeeId = 0;
            model.Integral = 0;
            model.OrderCode = "";
            model.OrderContentType = 2;
            model.PackageId = 0;
            model.PackageTitle = "";
            model.PayChannel = 0;
            model.Rebate = 0;
            model.RebateExpression = "";
            model.RebateFee = 0;
            model.CancelTime = DateTime.Now;
            int result = _orderService.OrderPackage(model);
            if (EnumHelper.TryParsebyValue<ErrorMessage>(result, out errorMessage))
            {
                switch (errorMessage)
                {
                    case ErrorMessage.成功:
                        url = NovelId > 0 ? ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, channelId: RouteChannelId) : "/";
                        break;

                    case ErrorMessage.余额不足:
                        url = StringHelper.GetReturnUrl("/order/recharge", UrlParameterHelper.GetParams("returnUrl"), channelId: RouteChannelId);
                        break;

                    case ErrorMessage.已包月:
                        url = ChapterContext.GetUrl("/chapter/detail", NovelId, ChapterCode, channelId: RouteChannelId);
                        break;

                    case ErrorMessage.用户不存在:
                        url = DataContext.GetErrorUrl(ErrorMessage.用户不存在, channelId: RouteChannelId);
                        break;
                }
            }

            return Redirect(url);
        }

        #endregion 听书全站包月

        #region 查询订单是否成功

        [Authorization(isAjaxOnly: true)]
        public JsonResult Result(string orderId)
        {
            string url = "";
            bool flag = false;
            RechargeInfo rechargeInfo = _rechargeService.GetOrder(orderId, currentUser.UserName);
            if (rechargeInfo != null && rechargeInfo.Id > 0)
            {
                if (rechargeInfo.Status == 1)
                {
                    flag = true;
                    url = "/recentread/index".GetChannelRouteUrl(RouteChannelId);
                }
            }
            return Json(new SimpleResponse<object>(flag, new { returnUrl = url }));
        }

        #endregion 查询订单是否成功
    }
}