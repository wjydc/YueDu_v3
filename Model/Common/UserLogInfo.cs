using System;

namespace Model.Common
{
    public class UserLogInfo : ILogInfo
    {
        #region Fields

        private int _id;
        private string _cookieId = String.Empty;
        private int _userId = 0;
        private string _userName = String.Empty;
        private int _clientId = 0;
        private string _version = String.Empty;
        private string _userAgent = String.Empty;
        private string _imei = String.Empty;
        private string _imsi = String.Empty;
        private string _channelId = String.Empty;
        private int _sourceType = 0;
        private int _startupCount = 0;
        private DateTime _recentStartupTime;
        private int _onlineTime = 0;
        private string _mobile = String.Empty;
        private string _netType = String.Empty;
        private string _province = String.Empty;
        private string _city = String.Empty;
        private string _referUrl = String.Empty;
        private string _url = String.Empty;
        private string _ipAddress = String.Empty;
        private string _remoteHost = String.Empty;
        private DateTime _addTime;

        #endregion

        #region Contructors

        public UserLogInfo()
        {

        }

        public UserLogInfo
        (
            int id,
            string cookieId,
            int userId,
            string userName,
            int clientId,
            string version,
            string userAgent,
            string imei,
            string imsi,
            string channelId,
            int sourceType,
            int startupCount,
            DateTime recentStartupTime,
            int onlineTime,
            string mobile,
            string netType,
            string province,
            string city,
            string referUrl,
            string url,
            string ipAddress,
            string remoteHost,
            DateTime addTime
        )
        {
            _id = id;
            _cookieId = cookieId;
            _userId = userId;
            _userName = userName;
            _clientId = clientId;
            _version = version;
            _userAgent = userAgent;
            _imei = imei;
            _imsi = imsi;
            _channelId = channelId;
            _sourceType = sourceType;
            _startupCount = startupCount;
            _recentStartupTime = recentStartupTime;
            _onlineTime = onlineTime;
            _mobile = mobile;
            _netType = netType;
            _province = province;
            _city = city;
            _referUrl = referUrl;
            _url = url;
            _ipAddress = ipAddress;
            _remoteHost = remoteHost;
            _addTime = addTime;

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// CookieId
        /// </summary>
        public string CookieId
        {
            get { return _cookieId; }
            set { _cookieId = value; }
        }

        /// <summary>
        /// ClientLog: UserId.
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// UserLog: UserName.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// UserLog: ClientId.
        /// </summary>
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        /// <summary>
        /// UserLog: Version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// UserLog: User Agent.
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        /// UserLog: IMEI.
        /// </summary>
        public string IMEI
        {
            get { return _imei; }
            set { _imei = value; }
        }

        /// <summary>
        /// UserLog: IMSI.
        /// </summary>
        public string IMSI
        {
            get { return _imsi; }
            set { _imsi = value; }
        }

        /// <summary>
        /// UserLog: ChannelId.
        /// </summary>
        public string ChannelId
        {
            get { return _channelId; }
            set { _channelId = value; }
        }

        /// <summary>
        /// UserLog: SourceType.
        /// </summary>
        public int SourceType
        {
            get { return _sourceType; }
            set { _sourceType = value; }
        }

        /// <summary>
        /// StartupCount
        /// </summary>
        public int StartupCount
        {
            get { return _startupCount; }
            set { _startupCount = value; }
        }

        /// <summary>
        /// RecentStartupTime
        /// </summary>
        public DateTime RecentStartupTime
        {
            get { return _recentStartupTime; }
            set { _recentStartupTime = value; }
        }

        /// <summary>
        /// OnlineTime
        /// </summary>
        public int OnlineTime
        {
            get { return _onlineTime; }
            set { _onlineTime = value; }
        }

        /// <summary>
        /// WebLog: Mobile/IP.
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        /// <summary>
        /// WebLog: Mobile/WIFI.
        /// </summary>
        public string NetType
        {
            get { return _netType; }
            set { _netType = value; }
        }

        /// <summary>
        /// WebLog: Province.
        /// </summary>
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        /// <summary>
        /// WebLog: City.
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// WebLog: ReferUrl.
        /// </summary>
        public string ReferUrl
        {
            get { return _referUrl; }
            set { _referUrl = value; }
        }

        /// <summary>
        /// Url.
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// WebLog: IP.
        /// </summary>
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>
        /// WebLog: Remote Host.
        /// </summary>
        public string RemoteHost
        {
            get { return _remoteHost; }
            set { _remoteHost = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }

        #endregion
    }
}
