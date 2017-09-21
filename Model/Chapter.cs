using Dapper.Contrib.Extensions;
using System;

namespace Model
{
    [Table("dbo.Chapter")]
    ///<summary>
    ///表Chapter的实体类
    ///</summary>
    public class Chapter
    {
        [IdentityKey]
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// novelId
        /// </summary>
        public int NovelId { get; set; }

        /// <summary>
        /// volumeName
        /// </summary>
        public string VolumeName { get; set; }

        /// <summary>
        /// chapterName
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// chapterCode
        /// </summary>
        public int ChapterCode { get; set; }

        /// <summary>
        /// fileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// wordSize
        /// </summary>
        public long WordSize { get; set; }

        /// <summary>
        /// isFree
        /// </summary>
        public int IsFree { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        public int Fee { get; set; }

        /// <summary>
        /// bindFee
        /// </summary>
        public int BindFee { get; set; }

        /// <summary>
        /// fChapterId
        /// </summary>
        public string FChapterId { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public int Status { get; set; }

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
        /// onlineTime
        /// </summary>
        public DateTime OnlineTime { get; set; }

        /// <summary>
        /// notice
        /// </summary>
        public string Notice { get; set; }

    }
}
