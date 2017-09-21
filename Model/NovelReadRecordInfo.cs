using System;

namespace Model
{
    [Serializable]
    public class NovelReadRecordInfo
    {
        #region Fields

        private int _id;
        private int _novelId = 0;
        private int _chapterId = 0;
        private int _chapterCode = 0;
        private int _position = 0;
        private DateTime _recentReadTime;
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
        private DateTime _addTime;
        private string _routeChannelId = String.Empty;
        private string _novelTitle = String.Empty;
        private int _novelContentType = 0;
        private string _chapterName = String.Empty;

        #endregion Fields

        #region Contructors

        public NovelReadRecordInfo()
        {
        }

        public NovelReadRecordInfo
        (
            int id,
            int novelId,
            int chapterId,
            int chapterCode,
            int position,
            DateTime recentReadTime,
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
            DateTime addTime,
            string routeChannelId,
            string novelTitle,
            int novelContentType,
            string chapterName
        )
        {
            _id = id;
            _novelId = novelId;
            _chapterId = chapterId;
            _chapterCode = chapterCode;
            _position = position;
            _recentReadTime = recentReadTime;
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
            _addTime = addTime;
            _routeChannelId = routeChannelId;
            _novelTitle = novelTitle;
            _novelContentType = novelContentType;
            _chapterName = chapterName;
        }

        #endregion Contructors

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
        /// NovelId
        /// </summary>
        public int NovelId
        {
            get { return _novelId; }
            set { _novelId = value; }
        }

        /// <summary>
        /// ChapterId
        /// </summary>
        public int ChapterId
        {
            get { return _chapterId; }
            set { _chapterId = value; }
        }

        /// <summary>
        /// ChapterCode
        /// </summary>
        public int ChapterCode
        {
            get { return _chapterCode; }
            set { _chapterCode = value; }
        }

        /// <summary>
        /// Position
        /// </summary>
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// RecentReadTime
        /// </summary>
        public DateTime RecentReadTime
        {
            get { return _recentReadTime; }
            set { _recentReadTime = value; }
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
        /// AddTime
        /// </summary>
        public DateTime AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }

        /// <summary>
        /// RouteChannelId
        /// </summary>
        public string RouteChannelId
        {
            get { return _routeChannelId; }
            set { _routeChannelId = value; }
        }

        /// <summary>
        /// NovelTitle
        /// </summary>
        public string NovelTitle
        {
            get { return _novelTitle; }
            set { _novelTitle = value; }
        }

        /// <summary>
        /// NovelContentType
        /// </summary>
        public int NovelContentType
        {
            get { return _novelContentType; }
            set { _novelContentType = value; }
        }

        /// <summary>
        /// ChapterName
        /// </summary>
        public string ChapterName
        {
            get { return _chapterName; }
            set { _chapterName = value; }
        }

        #endregion Public Properties
    }
}