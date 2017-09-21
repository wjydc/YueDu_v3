using Component.Base;
using Model.Common;
using Model.Config;
using Service;
using Utility;

namespace Component.Controllers.User
{
    public class HeaderInfoController : BaseController
    {
        #region 头信息

        protected IHeaderInfo HeaderInfo = new HeaderInfo();
        private ISessionHeaderInfo SessionHeaderInfo;

        /// <summary>
        /// User Agent
        /// </summary>
        private string GetUserAgent()
        {
            string value = GetHeader(Constants.HttpHeader.USER_AGENT);

            if (string.IsNullOrEmpty(value))
            {
                if (this.Request.UserAgent != null)
                {
                    value = StringHelper.GetUserAgent(300);
                }
            }

            return value;
        }

        #endregion 头信息

        #region

        private ISiteSection _siteSection = null;

        protected ISiteSection SiteSection
        {
            get
            {
                if (_siteSection == null)
                {
                    ISiteSection siteSection = DataContext.GetSiteSection();

                    if (!string.IsNullOrEmpty(RouteChannelId))
                    {
                        Model.SiteConfig siteConfig = DataContext.TryCache<Model.SiteConfig>("SiteConfig_" + RouteChannelId, () =>
                        {
                            ISiteConfigService service = DataContext.ResolveService<ISiteConfigService>();
                            return service.GetModel(RouteChannelId);
                        }, 60);

                        if (!siteConfig.IsNullOrEmpty<Model.SiteConfig>())
                        {
                            _siteSection = new SiteSection();

                            _siteSection.Html.SiteName = siteConfig.SiteName;
                            _siteSection.Html.SiteTitle = siteConfig.SiteName;
                            _siteSection.Html.FeeName = siteSection.Html.FeeName;
                            _siteSection.Html.MetaKeywords = siteConfig.MetaKeywords;
                            _siteSection.Html.MetaDescription = siteConfig.MetaDescription;
                            _siteSection.Html.WeChatQrCode = siteConfig.QrCode;

                            //_siteSection.Service.WorkHour = "";
                            _siteSection.Service.Phone = siteConfig.Phone;
                            _siteSection.Service.QQ = siteConfig.QQ;
                            _siteSection.Service.Email = siteConfig.Email;
                            _siteSection.Service.WeChat = siteConfig.WeChat;
                            _siteSection.Service.WeChatPubId = siteConfig.WeChatPubId;

                            _siteSection.Auth.IsShowQQ = siteSection.Auth.IsShowQQ;
                            _siteSection.Auth.IsShowWeibo = siteSection.Auth.IsShowWeibo;
                            _siteSection.Auth.IsShowWeChat = siteSection.Auth.IsShowWeChat;
                            _siteSection.Auth.IsAutoLoginInMicroMessenger = siteSection.Auth.IsAutoLoginInMicroMessenger;

                            _siteSection.Pay.IsShowFailTip = siteSection.Pay.IsShowFailTip;
                            _siteSection.Pay.WxPayResultFailUrl = siteSection.Pay.WxPayResultFailUrl;

                            _siteSection.CP.IsShowAllNovel = siteSection.CP.IsShowAllNovel;
                            _siteSection.CP.IsShowAllAudio = siteSection.CP.IsShowAllAudio;
                            _siteSection.CP.IsShowAllCartoon = siteSection.CP.IsShowAllCartoon;

                            _siteSection.Class.IsShowMale = siteSection.Class.IsShowMale;
                            _siteSection.Class.IsShowFemale = siteSection.Class.IsShowFemale;
                            _siteSection.Class.IsShowAudio = siteSection.Class.IsShowAudio;
                            _siteSection.Class.IsShowCartoon = siteSection.Class.IsShowCartoon;

                            _siteSection.Novel.ChapterWordSizeFee = siteSection.Novel.ChapterWordSizeFee;

                            _siteSection.Audio.AllPackageFee = siteSection.Audio.AllPackageFee;

                            _siteSection.Chapter.IsRedirectWeChat = siteSection.Chapter.IsRedirectWeChat;

                            _siteSection.Comment.IsOpen = siteSection.Comment.IsOpen;
                            _siteSection.Comment.IsReverseSort = siteSection.Comment.IsReverseSort;

                            _siteSection.Download.IsShowApp = siteSection.Download.IsShowApp;

                            _siteSection.Filter.BookCPID = siteSection.Filter.BookCPID;
                        }
                    }

                    _siteSection = _siteSection ?? siteSection;
                }

                return _siteSection;
            }
        }

        private IRecSection _recSection = null;

        protected IRecSection RecSection
        {
            get
            {
                if (_recSection == null)
                {
                    _recSection = DataContext.GetRecSection();
                }

                return _recSection;
            }
        }

        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            SetSessionHeaderInfo();

            ViewBag.SiteSection = SiteSection;

            ViewBag.ChannelId = RouteChannelId;
        }

        #region RouteChannelId

        protected string RouteChannelId
        {
            get
            {
                return HeaderInfo.RouteChannelId;
            }
        }

        #endregion

        #region

        #region

        #region SetSessionHeaderInfo

        protected void SetSessionHeaderInfo()
        {
            ReadSessionHeaderInfo();

            string channelId = "";
            ParamType paramType = ParamType.none;
            if (IsHeader(Constants.HttpHeader.CHANNEL_ID, out channelId, out paramType))
            {
                if (paramType == ParamType.route)
                {
                    SaveSessionHeaderInfoFromRoute(channelId);
                }
                else
                {
                    #region HeaderInfo Session

                    string promotionCode = UrlParameterHelper.GetParams("pmc");
                    if (!string.IsNullOrEmpty(promotionCode))
                    {
                        SaveSessionHeaderInfo(channelId, promotionCode);
                    }
                    else
                    {
                        SaveSessionHeaderInfo(channelId, "");
                    }

                    #endregion HeaderInfo Session
                }
            }

            #region

            SetHeaderInfo();

            #endregion
        }

        protected void SetSessionHeaderInfo(string channelId)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                #region HeaderInfo Session

                ReadSessionHeaderInfo();
                SaveSessionHeaderInfo(channelId, "");

                #endregion
            }

            #region

            SetHeaderInfo();

            #endregion
        }

        protected void SetSessionHeaderInfo(string channelId, string promotionCode)
        {
            if (!string.IsNullOrEmpty(channelId) && !string.IsNullOrEmpty(promotionCode))
            {
                #region HeaderInfo Session

                ReadSessionHeaderInfo();
                SaveSessionHeaderInfo(channelId, promotionCode);

                #endregion
            }

            #region

            SetHeaderInfo();

            #endregion
        }

        #endregion

        #region SetSessionHeaderInfoFromRoute

        protected void SetSessionHeaderInfoFromRoute(string channelId)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                #region HeaderInfo Session

                ReadSessionHeaderInfo();
                SaveSessionHeaderInfoFromRoute(channelId);

                #endregion
            }

            #region

            SetHeaderInfo();

            #endregion
        }

        protected void SetSessionHeaderInfoFromRoute(string channelId, string promotionCode)
        {
            if (!string.IsNullOrEmpty(channelId) && !string.IsNullOrEmpty(promotionCode))
            {
                #region HeaderInfo Session

                ReadSessionHeaderInfo();
                SaveSessionHeaderInfoFromRoute(channelId, promotionCode);

                #endregion
            }

            #region

            SetHeaderInfo();

            #endregion
        }

        #endregion

        #endregion

        #region

        #region ReadSessionHeaderInfo

        private void ReadSessionHeaderInfo()
        {
            SessionHeaderInfo = SessionCookieHelper<ISessionHeaderInfo>.Get(Constants.SecurityKey.HeaderInfo_SessionName, Constants.SecurityKey.HeaderInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);

            if (SessionHeaderInfo != null && !string.IsNullOrEmpty(Host) && string.Compare(SessionHeaderInfo.HeaderId, Host, true) != 0)
            {
                SessionHeaderInfo = null;
                SessionCookieHelper<ISessionHeaderInfo>.Clear(Constants.SecurityKey.HeaderInfo_SessionName, Constants.SecurityKey.HeaderInfo_CookieName);
            }
        }

        #endregion

        #region SaveSessionHeaderInfo

        private void SaveSessionHeaderInfo(string channelId)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                if (SessionHeaderInfo == null)
                {
                    SessionHeaderInfo = new Model.Common.SessionHeaderInfo(Host);
                }
                SessionHeaderInfo.ChannelId = channelId;
            }

            SessionCookieHelper<ISessionHeaderInfo>.Save(SessionHeaderInfo, Constants.SecurityKey.HeaderInfo_SessionName, Constants.SecurityKey.HeaderInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);
        }

        private void SaveSessionHeaderInfo(string channelId, string promotionCode)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                if (SessionHeaderInfo == null)
                {
                    SessionHeaderInfo = new Model.Common.SessionHeaderInfo(Host);
                }
                SessionHeaderInfo.ChannelId = channelId;
                SessionHeaderInfo.PromotionCode = promotionCode;
            }

            SessionCookieHelper<ISessionHeaderInfo>.Save(SessionHeaderInfo, Constants.SecurityKey.HeaderInfo_SessionName, Constants.SecurityKey.HeaderInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);
        }

        #endregion

        #region SaveSessionHeaderInfoFromRoute

        private void SaveSessionHeaderInfoFromRoute(string channelId)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                if (SessionHeaderInfo == null)
                {
                    SessionHeaderInfo = new Model.Common.SessionHeaderInfo(Host);
                }
                SetSessionRouteChannelInfo(channelId);
            }

            SessionCookieHelper<ISessionHeaderInfo>.Save(SessionHeaderInfo, Constants.SecurityKey.HeaderInfo_SessionName, Constants.SecurityKey.HeaderInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);
        }

        private void SaveSessionHeaderInfoFromRoute(string channelId, string promotionCode)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                if (SessionHeaderInfo == null)
                {
                    SessionHeaderInfo = new Model.Common.SessionHeaderInfo(Host);
                }
                SetSessionRouteChannelInfo(channelId);
                SessionHeaderInfo.PromotionCode = promotionCode;
            }

            SessionCookieHelper<ISessionHeaderInfo>.Save(SessionHeaderInfo, Constants.SecurityKey.HeaderInfo_SessionName, Constants.SecurityKey.HeaderInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);
        }

        #endregion

        #region SetChannelInfo

        protected static readonly bool IsRouteChannel = ConfigHelper.GetBool("RouteChannelEnabled");

        private void SetSessionRouteChannelInfo(string channelId)
        {
            if (!string.IsNullOrEmpty(channelId))
            {
                if (IsRouteChannel)
                {
                    SessionHeaderInfo.RouteChannelId = channelId;
                }
                else
                {
                    SessionHeaderInfo.RouteChannelId = "";
                    SessionHeaderInfo.ChannelId = channelId;
                }
            }
        }

        #endregion

        #region SetHeaderInfo

        private void SetHeaderInfo()
        {
            if (SessionHeaderInfo != null)
            {
                HeaderInfo.ChannelId = SessionHeaderInfo.ChannelId;
                HeaderInfo.RouteChannelId = SessionHeaderInfo.RouteChannelId;
                HeaderInfo.PromotionCode = SessionHeaderInfo.PromotionCode;
            }

            HeaderInfo.ClientVersion = "";
            HeaderInfo.ClientId = 0;
            HeaderInfo.IMSI = StringHelper.SubString(StringHelper.GetUCIMSI(), 100, false);
            HeaderInfo.IMEI = "";
            HeaderInfo.UserAgent = GetUserAgent();
            HeaderInfo.Tel = StringHelper.GetMobile();
            HeaderInfo.SourceType = GetSourceType();
        }

        #endregion

        #endregion

        #endregion
    }
}