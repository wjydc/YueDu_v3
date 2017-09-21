namespace Model.Common
{
    public class HeaderInfo : IHeaderInfo
    {
        private string _clientVersion = "";

        /// <summary>
        /// 客户端版本
        /// </summary>
        public string ClientVersion
        {
            get { return _clientVersion; }
            set { _clientVersion = value; }
        }

        private int _clientId = 0;

        /// <summary>
        /// 客户端ID
        /// </summary>
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        private string _channelId = "";

        /// <summary>
        /// 渠道号
        /// </summary>
        public string ChannelId
        {
            get
            {
                _channelId = string.IsNullOrEmpty(RouteChannelId) ? _channelId : RouteChannelId;

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

        private string _tel = "";

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Tel
        {
            get
            {
                if (string.IsNullOrEmpty(_tel) || _tel == "0")
                {
                    return "";
                }

                return _tel;
            }
            set { _tel = value; }
        }

        private string _imsi = "";

        /// <summary>
        /// IMSI
        /// </summary>
        public string IMSI
        {
            get { return _imsi; }
            set { _imsi = value; }
        }

        private string _imei = "";

        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEI
        {
            get { return _imei; }
            set { _imei = value; }
        }

        private string _userAgent = "";

        /// <summary>
        /// User Agent
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        private int _sourceType = 0;

        /// <summary>
        /// 来源
        /// </summary>
        public int SourceType
        {
            get { return _sourceType; }
            set { _sourceType = value; }
        }
    }
}