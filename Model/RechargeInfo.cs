using Model.Common;
using System;

namespace Model
{
    [Serializable]
    public class RechargeInfo : IPromotionLogInfo
    {
        #region Fields

        private int _id;
        private string _merchantId = String.Empty;
        private string _orderId = String.Empty;
        private string _fOrderId = String.Empty;
        private string _name = String.Empty;
        private int _clientId = 0;
        private int _userId = 0;
        private string _userName = String.Empty;
        private string _nickName = String.Empty;
        private int _fee = 0;
        private int _rebateFee = 0;
        private string _rebateExpression = String.Empty;
        private int _balance = 0;
        private decimal _cash;
        private int _integral = 0;
        private int _payType = 0;
        private string _payMobile = String.Empty;
        private string _spNumber = String.Empty;
        private string _sMSOrder = String.Empty;
        private string _merchantPrivate = String.Empty;
        private string _merchantExpand = String.Empty;
        private string _brief = String.Empty;
        private string _userAgent = String.Empty;
        private string _version = String.Empty;
        private string _imei = String.Empty;
        private string _imsi = String.Empty;
        private string _channelId = String.Empty;
        private int _sourceType = 0;
        private string _mobile = String.Empty;
        private string _netType = String.Empty;
        private string _province = String.Empty;
        private string _city = String.Empty;
        private string _referUrl = String.Empty;
        private string _iPAddress = String.Empty;
        private string _remoteHost = String.Empty;
        private int _status = 0;
        private DateTime _completeTime;
        private DateTime _addTime;
        private string _promotionCode = String.Empty;

        #endregion

        #region Contructors

        public RechargeInfo()
        {

        }

        public RechargeInfo
        (
            int id,
            string merchantId,
            string orderId,
            string fOrderId,
            string name,
            int clientId,
            int userId,
            string userName,
            string nickName,
            int fee,
            int rebateFee,
            string rebateExpression,
            int balance,
            decimal cash,
            int integral,
            int payType,
            string payMobile,
            string spNumber,
            string sMSOrder,
            string merchantPrivate,
            string merchantExpand,
            string brief,
            string userAgent,
            string version,
            string imei,
            string imsi,
            string channelId,
            int sourceType,
            string mobile,
            string netType,
            string province,
            string city,
            string referUrl,
            string iPAddress,
            string remoteHost,
            int status,
            DateTime completeTime,
            DateTime addTime,
            string promotionCode
        )
        {
            _id = id;
            _merchantId = merchantId;
            _orderId = orderId;
            _fOrderId = fOrderId;
            _name = name;
            _clientId = clientId;
            _userId = userId;
            _userName = userName;
            _nickName = nickName;
            _fee = fee;
            _rebateFee = rebateFee;
            _rebateExpression = rebateExpression;
            _balance = balance;
            _cash = cash;
            _integral = integral;
            _payType = payType;
            _payMobile = payMobile;
            _spNumber = spNumber;
            _sMSOrder = sMSOrder;
            _merchantPrivate = merchantPrivate;
            _merchantExpand = merchantExpand;
            _brief = brief;
            _userAgent = userAgent;
            _version = version;
            _imei = imei;
            _imsi = imsi;
            _channelId = channelId;
            _sourceType = sourceType;
            _mobile = mobile;
            _netType = netType;
            _province = province;
            _city = city;
            _referUrl = referUrl;
            _iPAddress = iPAddress;
            _remoteHost = remoteHost;
            _status = status;
            _completeTime = completeTime;
            _addTime = addTime;
            _promotionCode = promotionCode;

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
        /// MerchantId
        /// </summary>
        public string MerchantId
        {
            get { return _merchantId; }
            set { _merchantId = value; }
        }

        /// <summary>
        /// OrderId
        /// </summary>
        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        /// <summary>
        /// FOrderId
        /// </summary>
        public string FOrderId
        {
            get { return _fOrderId; }
            set { _fOrderId = value; }
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        /// NickName
        /// </summary>
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }

        /// <summary>
        /// Fee
        /// </summary>
        public int Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        /// <summary>
        /// RebateFee
        /// </summary>
        public int RebateFee
        {
            get { return _rebateFee; }
            set { _rebateFee = value; }
        }

        /// <summary>
        /// RebateExpression
        /// </summary>
        public string RebateExpression
        {
            get { return _rebateExpression; }
            set { _rebateExpression = value; }
        }

        /// <summary>
        /// Balance
        /// </summary>
        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        /// <summary>
        /// Cash
        /// </summary>
        public decimal Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        /// <summary>
        /// Integral
        /// </summary>
        public int Integral
        {
            get { return _integral; }
            set { _integral = value; }
        }

        /// <summary>
        /// PayType
        /// </summary>
        public int PayType
        {
            get { return _payType; }
            set { _payType = value; }
        }

        /// <summary>
        /// PayMobile
        /// </summary>
        public string PayMobile
        {
            get { return _payMobile; }
            set { _payMobile = value; }
        }

        /// <summary>
        /// SpNumber
        /// </summary>
        public string SpNumber
        {
            get { return _spNumber; }
            set { _spNumber = value; }
        }

        /// <summary>
        /// SMSOrder
        /// </summary>
        public string SMSOrder
        {
            get { return _sMSOrder; }
            set { _sMSOrder = value; }
        }

        /// <summary>
        /// MerchantPrivate
        /// </summary>
        public string MerchantPrivate
        {
            get { return _merchantPrivate; }
            set { _merchantPrivate = value; }
        }

        /// <summary>
        /// MerchantExpand
        /// </summary>
        public string MerchantExpand
        {
            get { return _merchantExpand; }
            set { _merchantExpand = value; }
        }

        /// <summary>
        /// Brief
        /// </summary>
        public string Brief
        {
            get { return _brief; }
            set { _brief = value; }
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
        /// UserLog: Version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
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
        /// WebLog: IP.
        /// </summary>
        public string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
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
        /// Status
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// CompleteTime
        /// </summary>
        public DateTime CompleteTime
        {
            get { return _completeTime; }
            set { _completeTime = value; }
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
        /// PromotionCode
        /// </summary>
        public string PromotionCode
        {
            get { return _promotionCode; }
            set { _promotionCode = value; }
        }

        #endregion
    }
}

