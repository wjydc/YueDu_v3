using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Common
{
    #region

    public interface IClientLogInfo : IUserLogInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        int UserId { set; get; }
    }

    public interface IWebLogInfo
    {
        /// <summary>
        /// 手机号码/IP
        /// </summary>
        string Mobile { set; get; }
        /// <summary>
        /// mobile/WIFI
        /// </summary>
        string NetType { set; get; }
        /// <summary>
        /// 省份
        /// </summary>
        string Province { set; get; }
        /// <summary>
        /// 城市
        /// </summary>
        string City { set; get; }
        /// <summary>
        /// 相关网址
        /// </summary>
        string ReferUrl { set; get; }
        /// <summary>
        /// IP
        /// </summary>
        string IPAddress { set; get; }
        /// <summary>
        /// Remote Host
        /// </summary>
        string RemoteHost { set; get; }
    }

    public interface ILogInfo : IClientLogInfo, IWebLogInfo
    {

    }

    public interface IPromotionLogInfo : ILogInfo
    {
        /// <summary>
        /// 推广编号
        /// </summary>
        string PromotionCode { set; get; }
    }

    #endregion

    #region

    public interface IUserLogInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { set; get; }
        /// <summary>
        /// 客户端Id
        /// </summary>
        int ClientId { set; get; }
        /// <summary>
        /// 版本
        /// </summary>
        string Version { set; get; }
        /// <summary>
        /// User Agent
        /// </summary>
        string UserAgent { set; get; }
        /// <summary>
        /// IMEI
        /// </summary>
        string IMEI { set; get; }
        /// <summary>
        /// IMSI
        /// </summary>
        string IMSI { set; get; }
        /// <summary>
        /// 渠道号
        /// </summary>
        string ChannelId { set; get; }
        /// <summary>
        /// 来源
        /// </summary>
        int SourceType { set; get; }
    }

    #endregion
}
