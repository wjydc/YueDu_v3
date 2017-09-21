using System.Configuration;
using System.Xml;

namespace Component.Config
{
    public class BaseSectionHandler : IConfigurationSectionHandler
    {
        #region

        protected bool GetSingleNodeValue(XmlNode section, string xmlPath, out XmlNode node)
        {
            node = null;
            if (string.IsNullOrEmpty(xmlPath)) return false;

            return ((node = section.SelectSingleNode(xmlPath)) != null);
        }

        protected string GetSingleNodeAttributeValue(XmlNode section, string xmlPath, string attributeName)
        {
            if (string.IsNullOrEmpty(xmlPath)) return "";

            string value = "";

            XmlNode node = section.SelectSingleNode(xmlPath);
            if (node != null)
            {
                XmlAttribute attr = node.Attributes[attributeName];
                if (attr != null)
                {
                    value = attr.Value;
                }
            }

            return value;
        }

        #endregion

        public virtual object Create(object parent, object configContext, XmlNode section)
        {
            return null;
        }
    }
}