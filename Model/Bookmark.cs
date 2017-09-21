using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Model.Common;

namespace Model
{
    [Table("Users.Bookmark")]
    public class Bookmark : IClientLogInfo
    {
        [IdentityKey]
        public int Id { get; set; }
        public int NovelId { get; set; }//小说ID
        public int ChapterId { get; set; }//章节ID
        public int ChapterCode { get; set; }//章节号（从0开始）
        public int Position { get; set; }//阅读位置
        public DateTime RecentReadTime { get; set; }//最后阅读时间
        public int UserId { get; set; }//用户ID
        public string UserName { get; set; }//用户名
        public int ClientId { get; set; }//客户端ID
        public string Version { get; set; }
        public string UserAgent { get; set; }
        public string IMEI { get; set; }
        public string IMSI { get; set; }
        public string ChannelId { get; set; }//渠道ID
        public int SourceType { get; set; }//来源
        public DateTime UpdateTime { get; set; }
        public DateTime AddTime { get; set; }
    }
}
