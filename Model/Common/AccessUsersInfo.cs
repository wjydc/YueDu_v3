using System;

namespace Model.Common
{
    [Serializable]
    public class AccessUsersInfo : IClientLogInfo
    {
        #region Fields

        private int _id;
        private int _clientId = 0;
        private int _userId = 0;
        private string _userName = String.Empty;
        private string _platform = String.Empty;
        private string _openId = String.Empty;
        private string _accessToken = String.Empty;
        private string _nickName = String.Empty;
        private string _icon = String.Empty;
        private string _phone = String.Empty;
        private string _email = String.Empty;
        private DateTime _recentLoginTime;
        private int _isLogin = 0;
        private int _confirmLogin = 0;
        private DateTime _loginInvalidTime;
        private string _userAgent = String.Empty;
        private string _imsi = String.Empty;
        private string _imei = String.Empty;
        private string _version = String.Empty;
        private string _channelId = String.Empty;
        private int _sourceType = 0;
        private int _status = 0;
        private DateTime _addTime;
        private string _unionId = string.Empty;

        #endregion

        #region Contructors

        public AccessUsersInfo()
        {

        }

        public AccessUsersInfo
        (
            int id,
            int clientId,
            int userId,
            string userName,
            string platform,
            string openId,
            string accessToken,
            string nickName,
            string icon,
            string phone,
            string email,
            DateTime recentLoginTime,
            int isLogin,
            int confirmLogin,
            DateTime loginInvalidTime,
            string userAgent,
            string imsi,
            string imei,
            string version,
            string channelId,
            int sourceType,
            int status,
            DateTime addTime,
            string unionId
        )
        {
            _id = id;
            _clientId = clientId;
            _userId = userId;
            _userName = userName;
            _platform = platform;
            _openId = openId;
            _accessToken = accessToken;
            _nickName = nickName;
            _icon = icon;
            _phone = phone;
            _email = email;
            _recentLoginTime = recentLoginTime;
            _isLogin = isLogin;
            _confirmLogin = confirmLogin;
            _loginInvalidTime = loginInvalidTime;
            _userAgent = userAgent;
            _imsi = imsi;
            _imei = imei;
            _version = version;
            _channelId = channelId;
            _sourceType = sourceType;
            _status = status;
            _addTime = addTime;
            _unionId = unionId;
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
        /// UserLog: ClientId.
        /// </summary>
        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
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
        /// Platform
        /// </summary>
        public string Platform
        {
            get { return _platform; }
            set { _platform = value; }
        }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId
        {
            get { return _openId; }
            set { _openId = value; }
        }

        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        /// <summary>
        /// NickName
        /// </summary>
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        /// <summary>
        /// Phone
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// RecentLoginTime
        /// </summary>
        public DateTime RecentLoginTime
        {
            get { return _recentLoginTime; }
            set { _recentLoginTime = value; }
        }

        /// <summary>
        /// IsLogin
        /// </summary>
        public int IsLogin
        {
            get { return _isLogin; }
            set { _isLogin = value; }
        }

        /// <summary>
        /// ConfirmLogin
        /// </summary>
        public int ConfirmLogin
        {
            get { return _confirmLogin; }
            set { _confirmLogin = value; }
        }

        /// <summary>
        /// LoginInvalidTime
        /// </summary>
        public DateTime LoginInvalidTime
        {
            get { return _loginInvalidTime; }
            set { _loginInvalidTime = value; }
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
        /// UserLog: IMSI.
        /// </summary>
        public string IMSI
        {
            get { return _imsi; }
            set { _imsi = value; }
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
        /// UserLog: Version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
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
        /// Status
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }

        /// <summary>
        /// UnionId
        /// </summary>
        public string UnionId
        {
            get { return _unionId; }
            set { _unionId = value; }
        }

        #endregion
    }
}
