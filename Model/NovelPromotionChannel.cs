using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Users.NovelPromotionChannel")]
    public class NovelPromotionChannel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// novelId
        /// </summary>
        public int NovelId { get; set; }

        /// <summary>
        /// chapterCode
        /// </summary>
        public int ChapterCode { get; set; }

        /// <summary>
        /// channelId
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// channelName
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// siteId
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// siteName
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// hits
        /// </summary>
        public long Hits { get; set; }

        /// <summary>
        /// sortId
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// updateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// addTime
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// classId
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// wapHost
        /// </summary>
        public string WapHost { get; set; }
        
    }
}
