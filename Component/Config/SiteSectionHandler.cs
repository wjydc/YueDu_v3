using Model.Config;
using System.Xml;
using Utility;

namespace Component.Config
{
    public class SiteSectionHandler : BaseSectionHandler
    {
        public override object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            ISiteSection model = null;

            if (section != null)
            {
                model = new SiteSection();

                XmlNode node = null;

                #region

                if (GetSingleNodeValue(section, "/SiteSection", out node))
                {
                    model.Html.SiteName = GetSingleNodeAttributeValue(node, "item[@key=\"SiteName\"]", "value");
                    model.Html.SiteTitle = GetSingleNodeAttributeValue(node, "item[@key=\"SiteTitle\"]", "value");
                    model.Html.FeeName = GetSingleNodeAttributeValue(node, "item[@key=\"FeeName\"]", "value");
                    model.Html.MetaKeywords = GetSingleNodeAttributeValue(node, "item[@key=\"MetaKeywords\"]", "value");
                    model.Html.MetaDescription = GetSingleNodeAttributeValue(node, "item[@key=\"MetaDescription\"]", "value");
                }
                if (GetSingleNodeValue(section, "/SiteSection/Service", out node))
                {
                    model.Service.WorkHour = GetSingleNodeAttributeValue(node, "item[@key=\"WorkHour\"]", "value");
                    model.Service.Phone = GetSingleNodeAttributeValue(node, "item[@key=\"Phone\"]", "value");
                    model.Service.Email = GetSingleNodeAttributeValue(node, "item[@key=\"Email\"]", "value");
                    model.Service.QQ = GetSingleNodeAttributeValue(node, "item[@key=\"QQ\"]", "value");
                    model.Service.WeChat = GetSingleNodeAttributeValue(node, "item[@key=\"WeChat\"]", "value");
                    model.Service.WeChatPubId = GetSingleNodeAttributeValue(node, "item[@key=\"WeChatPubId\"]", "value");
                }
                if (GetSingleNodeValue(section, "/SiteSection/Auth/QQ", out node))
                {
                    model.Auth.IsShowQQ = GetSingleNodeAttributeValue(node, "item[@key=\"IsShow\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Auth/Weibo", out node))
                {
                    model.Auth.IsShowWeibo = GetSingleNodeAttributeValue(node, "item[@key=\"IsShow\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Auth/WeChat", out node))
                {
                    model.Auth.IsShowWeChat = GetSingleNodeAttributeValue(node, "item[@key=\"IsShow\"]", "value").ToBool();
                    model.Auth.IsAutoLoginInMicroMessenger = GetSingleNodeAttributeValue(node, "item[@key=\"IsAutoLoginInMicroMessenger\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Pay", out node))
                {
                    model.Pay.IsShowFailTip = GetSingleNodeAttributeValue(node, "item[@key=\"IsShowFailTip\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Pay/WxPay", out node))
                {
                    model.Pay.WxPayResultFailUrl = GetSingleNodeAttributeValue(node, "item[@key=\"ResultFailUrl\"]", "value");
                }
                if (GetSingleNodeValue(section, "/SiteSection/CP", out node))
                {
                    model.CP.IsShowAllNovel = GetSingleNodeAttributeValue(node, "item[@key=\"IsShowAllNovel\"]", "value").ToBool();
                    model.CP.IsShowAllAudio = GetSingleNodeAttributeValue(node, "item[@key=\"IsShowAllAudio\"]", "value").ToBool();
                    model.CP.IsShowAllCartoon = GetSingleNodeAttributeValue(node, "item[@key=\"IsShowAllCartoon\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Class/SpeType", out node))
                {
                    model.Class.IsShowMale = GetSingleNodeAttributeValue(node, "item[@key=\"IsShowMale\"]", "value").ToBool();
                    model.Class.IsShowFemale = GetSingleNodeAttributeValue(node, "item[@key=\"IsShowFemale\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Class/Audio", out node))
                {
                    model.Class.IsShowAudio = GetSingleNodeAttributeValue(node, "item[@key=\"IsShow\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Class/Cartoon", out node))
                {
                    model.Class.IsShowCartoon = GetSingleNodeAttributeValue(node, "item[@key=\"IsShow\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Novel", out node))
                {
                    model.Novel.ChapterWordSizeFee = GetSingleNodeAttributeValue(node, "item[@key=\"ChapterWordSizeFee\"]", "value").ToInt();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Audio", out node))
                {
                    model.Audio.AllPackageFee = GetSingleNodeAttributeValue(node, "item[@key=\"AllPackageFee\"]", "value").ToInt();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Chapter", out node))
                {
                    model.Chapter.IsRedirectWeChat = GetSingleNodeAttributeValue(node, "item[@key=\"IsRedirectWechat\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Comment", out node))
                {
                    model.Comment.IsOpen = GetSingleNodeAttributeValue(node, "item[@key=\"IsOpen\"]", "value").ToBool();
                    model.Comment.IsReverseSort = GetSingleNodeAttributeValue(node, "item[@key=\"IsReverseSort\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Download/App", out node))
                {
                    model.Download.IsShowApp = GetSingleNodeAttributeValue(node, "item[@key=\"IsShow\"]", "value").ToBool();
                }
                if (GetSingleNodeValue(section, "/SiteSection/Filter/Book", out node))
                {
                    model.Filter.BookCPID = GetSingleNodeAttributeValue(node, "item[@key=\"CPID\"]", "value").ToInt();
                }

                #endregion
            }

            return model;
        }
    }
}