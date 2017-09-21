using Model.Common;
using Component.Controllers.User;
using Utility;

namespace Component.Controllers.Log
{
    public class LogInfoController : HeaderInfoController
    {
        #region

        protected IUserLogInfo GetUserLogInfo(IUserLogInfo logInfo)
        {
            if (logInfo == null) return logInfo;

            logInfo.UserName = "";
            logInfo.ClientId = HeaderInfo.ClientId;
            logInfo.Version = HeaderInfo.ClientVersion;
            logInfo.UserAgent = HeaderInfo.UserAgent;
            logInfo.IMSI = HeaderInfo.IMSI;
            logInfo.IMEI = HeaderInfo.IMEI;
            logInfo.ChannelId = HeaderInfo.ChannelId;
            logInfo.SourceType = HeaderInfo.SourceType;

            return logInfo;
        }

        /// <summary>
        /// 包括IUserLogInfo
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        protected IClientLogInfo GetClientLogInfo(IClientLogInfo logInfo)
        {
            if (logInfo == null) return logInfo;

            logInfo.UserId = 0;
            logInfo.UserName = "";
            logInfo.ClientId = HeaderInfo.ClientId;
            logInfo.Version = HeaderInfo.ClientVersion;
            logInfo.UserAgent = HeaderInfo.UserAgent;
            logInfo.IMSI = HeaderInfo.IMSI;
            logInfo.IMEI = HeaderInfo.IMEI;
            logInfo.ChannelId = HeaderInfo.ChannelId;
            logInfo.SourceType = HeaderInfo.SourceType;

            return logInfo;
        }

        protected IWebLogInfo GetWebLogInfo(IWebLogInfo logInfo)
        {
            if (logInfo == null) return logInfo;

            string netType = string.Empty;
            logInfo.Mobile = StringHelper.GetMobileOrIP(out netType);
            logInfo.NetType = netType;
            logInfo.Province = GetHeader(Constants.HttpHeader.PROVINCE);
            logInfo.City = GetHeader(Constants.HttpHeader.CITY);
            logInfo.ReferUrl = StringHelper.GetReferUrl(1000);
            logInfo.IPAddress = StringHelper.GetIP();
            logInfo.RemoteHost = StringHelper.GetRemoteHost();

            return logInfo;
        }

        /// <summary>
        /// 包括IClientLogInfo、IUserLogInfo、IWebLogInfo
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        protected ILogInfo GetLogInfo(ILogInfo logInfo)
        {
            if (logInfo == null) return logInfo;

            IClientLogInfo clientLogInfo = GetClientLogInfo(logInfo);
            if (clientLogInfo != null && clientLogInfo is ILogInfo)
            {
                logInfo = clientLogInfo as ILogInfo;
            }

            IWebLogInfo webLogInfo = GetWebLogInfo(logInfo);
            if (webLogInfo != null && webLogInfo is ILogInfo)
            {
                logInfo = webLogInfo as ILogInfo;
            }

            return logInfo;
        }

        #endregion
    }
}
