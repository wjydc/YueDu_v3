using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Utility
{
    public class XmlHelper
    {
        #region

        protected XmlDocument doc;

        public XmlHelper(string xml)
        {
            try
            {
                if (!string.IsNullOrEmpty(xml))
                {
                    this.doc = new XmlDocument();
                    doc.LoadXml(xml);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据XML节点获取节点中的内容
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public string GetNodeString(string name)
        {
            if (doc == null)
            {
                return string.Empty;
            }

            XmlNode subNode = doc.SelectSingleNode(name);

            if (subNode == null)
            {
                return string.Empty;
            }
            else
            {
                return subNode.InnerText;
            }
        }

        /// <summary>
        /// 根据节点名称返回节点中的内容（decimal类型）
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public decimal GetNodeDecimal(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;

            decimal result = 0;
            if (decimal.TryParse(GetNodeString(name), out result))
            {
                return result;
            }

            return 0;
        }

        #endregion

        public static T Deserialize<T>(string xml, Encoding encoding) where T : class
        {
            T t = default(T);

            if (!string.IsNullOrEmpty(xml) && encoding != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(xml)))
                {
                    using (StreamReader streamReader = new StreamReader(memoryStream, encoding))
                    {
                        t = xmlSerializer.Deserialize(streamReader).To<T>();
                    }
                }
            }

            return t;
        }

        public static T DeserializeFromFile<T>(string xmlPath, Encoding encoding) where T : class
        {
            T t = default(T);

            if (!string.IsNullOrEmpty(xmlPath) && encoding != null)
            {
                string xml = File.ReadAllText(xmlPath, encoding);
                if (!string.IsNullOrEmpty(xml))
                {
                    t = Deserialize<T>(xml, encoding);
                }
            }

            return t;
        }
    }
}