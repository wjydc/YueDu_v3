using Model.Config;
using System.Xml;

namespace Component.Config
{
    public class PaySectionHandler : BaseSectionHandler
    {
        public override object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            IPaySection model = null;

            if (section != null)
            {
                model = new PaySection();

                XmlNode node = null;

                #region

                if (GetSingleNodeValue(section, "/PaySection/NowPay", out node))
                {
                    model.NowPay.AppId = GetSingleNodeAttributeValue(node, "item[@key=\"AppId\"]", "value");
                    model.NowPay.AppKey = GetSingleNodeAttributeValue(node, "item[@key=\"AppKey\"]", "value");
                    model.NowPay.WeChatAppId = GetSingleNodeAttributeValue(node, "item[@key=\"WeChatAppId\"]", "value");
                    model.NowPay.WeChatAppKey = GetSingleNodeAttributeValue(node, "item[@key=\"WeChatAppKey\"]", "value");
                    model.NowPay.WeChatQrCodeAppId = GetSingleNodeAttributeValue(node, "item[@key=\"WeChatQrCodeAppId\"]", "value");
                    model.NowPay.WeChatQrCodeAppKey = GetSingleNodeAttributeValue(node, "item[@key=\"WeChatQrCodeAppKey\"]", "value");
                    model.NowPay.CallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"CallbackUrl\"]", "value");
                    model.NowPay.NewCallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"NewCallbackUrl\"]", "value");
                }
                if (GetSingleNodeValue(section, "/PaySection/WxPay", out node))
                {
                    model.WxPay.AppId = GetSingleNodeAttributeValue(node, "item[@key=\"AppId\"]", "value");
                    model.WxPay.MchId = GetSingleNodeAttributeValue(node, "item[@key=\"MchId\"]", "value");
                    model.WxPay.AppKey = GetSingleNodeAttributeValue(node, "item[@key=\"AppKey\"]", "value");
                    model.WxPay.AppSecret = GetSingleNodeAttributeValue(node, "item[@key=\"AppSecret\"]", "value");
                    model.WxPay.CallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"CallbackUrl\"]", "value");
                    model.WxPay.LogLevel = GetSingleNodeAttributeValue(node, "item[@key=\"LogLevel\"]", "value");
                }
                if (GetSingleNodeValue(section, "/PaySection/WxPay2", out node))
                {
                    model.WxPay2.AppId = GetSingleNodeAttributeValue(node, "item[@key=\"AppId\"]", "value");
                    model.WxPay2.MchId = GetSingleNodeAttributeValue(node, "item[@key=\"MchId\"]", "value");
                    model.WxPay2.AppKey = GetSingleNodeAttributeValue(node, "item[@key=\"AppKey\"]", "value");
                    model.WxPay2.AppSecret = GetSingleNodeAttributeValue(node, "item[@key=\"AppSecret\"]", "value");
                    model.WxPay2.CallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"CallbackUrl\"]", "value");
                    model.WxPay2.LogLevel = GetSingleNodeAttributeValue(node, "item[@key=\"LogLevel\"]", "value");
                }
                if (GetSingleNodeValue(section, "/PaySection/Alipay", out node))
                {
                    model.Alipay.SellerEmail = GetSingleNodeAttributeValue(node, "item[@key=\"SellerEmail\"]", "value");
                    model.Alipay.Partner = GetSingleNodeAttributeValue(node, "item[@key=\"Partner\"]", "value");
                    model.Alipay.Key = GetSingleNodeAttributeValue(node, "item[@key=\"Key\"]", "value");
                    model.Alipay.PrivateKey = GetSingleNodeAttributeValue(node, "item[@key=\"PrivateKey\"]", "value");
                    model.Alipay.PublicKey = GetSingleNodeAttributeValue(node, "item[@key=\"PublicKey\"]", "value");
                    model.Alipay.InputCharset = GetSingleNodeAttributeValue(node, "item[@key=\"InputCharset\"]", "value");
                    model.Alipay.SignType = GetSingleNodeAttributeValue(node, "item[@key=\"SignType\"]", "value");
                }

                #endregion
            }

            return model;
        }
    }
}