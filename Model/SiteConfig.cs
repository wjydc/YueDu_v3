using System;

namespace Model
{
    ///<summary>
    ///表SiteConfig的实体类
    ///</summary>
    public class SiteConfig
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// channelCompanyId
        /// </summary>
        public int ChannelCompanyId { get; set; }

        /// <summary>
        /// siteName
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// metaKeywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// metaDescription
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// qq
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// weChat
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// weChatPubId
        /// </summary>
        public string WeChatPubId { get; set; }

        /// <summary>
        /// followChapter
        /// </summary>
        public int FollowChapter { get; set; }

        /// <summary>
        /// followLinkUrl
        /// </summary>
        public string FollowLinkUrl { get; set; }

        /// <summary>
        /// statsCode
        /// </summary>
        public string StatsCode { get; set; }

        /// <summary>
        /// qrCode
        /// </summary>
        public string QrCode { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// addTime
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// updateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}