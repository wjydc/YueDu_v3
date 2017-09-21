using Model.Config;
using System.Xml;

namespace Component.Config
{
    public class AuthSectionHandler : BaseSectionHandler
    {
        public override object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            IAuthSection model = null;

            if (section != null)
            {
                model = new AuthSection();

                XmlNode node = null;

                #region

                if (GetSingleNodeValue(section, "/AuthSection/QQ", out node))
                {
                    model.QQ.AppId = GetSingleNodeAttributeValue(node, "item[@key=\"AppId\"]", "value");
                    model.QQ.AppKey = GetSingleNodeAttributeValue(node, "item[@key=\"AppKey\"]", "value");
                    model.QQ.CallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"CallbackUrl\"]", "value");
                }
                if (GetSingleNodeValue(section, "/AuthSection/Weibo", out node))
                {
                    model.Weibo.AppId = GetSingleNodeAttributeValue(node, "item[@key=\"AppId\"]", "value");
                    model.Weibo.AppKey = GetSingleNodeAttributeValue(node, "item[@key=\"AppKey\"]", "value");
                    model.Weibo.CallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"CallbackUrl\"]", "value");
                }
                if (GetSingleNodeValue(section, "/AuthSection/WeChat", out node))
                {
                    model.WeChat.QrCodeAppId = GetSingleNodeAttributeValue(node, "item[@key=\"QrCodeAppId\"]", "value");
                    model.WeChat.QrCodeAppKey = GetSingleNodeAttributeValue(node, "item[@key=\"QrCodeAppKey\"]", "value");
                    model.WeChat.AppId = GetSingleNodeAttributeValue(node, "item[@key=\"AppId\"]", "value");
                    model.WeChat.AppKey = GetSingleNodeAttributeValue(node, "item[@key=\"AppKey\"]", "value");
                    model.WeChat.CallbackUrl = GetSingleNodeAttributeValue(node, "item[@key=\"CallbackUrl\"]", "value");
                }

                #endregion
            }

            return model;
        }
    }
}