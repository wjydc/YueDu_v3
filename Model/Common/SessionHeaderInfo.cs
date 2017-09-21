using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    [Serializable]
    public class SessionHeaderInfo : ISessionHeaderInfo
    {
        private string _channelId = "";

        /// <summary>
        /// 渠道号
        /// </summary>
        public string ChannelId
        {
            get
            {
                if (string.IsNullOrEmpty(_channelId) || _channelId == "0")
                {
                    return "";
                }

                return _channelId;
            }
            set { _channelId = value; }
        }

        private string _routeChannelId = "";

        /// <summary>
        /// 渠道号（路由）
        /// </summary>
        public string RouteChannelId
        {
            get { return _routeChannelId; }
            set { _routeChannelId = value; }
        }

        private string _promotionCode = "";

        /// <summary>
        /// 推广编号
        /// </summary>
        public string PromotionCode
        {
            get { return _promotionCode; }
            set { _promotionCode = value; }
        }

        private string _headerId = "";

        public string HeaderId
        {
            get { return _headerId; }
            set { _headerId = value; }
        }

        public SessionHeaderInfo(string headerId = "")
        {
            this._headerId = headerId;
        }
    }
}