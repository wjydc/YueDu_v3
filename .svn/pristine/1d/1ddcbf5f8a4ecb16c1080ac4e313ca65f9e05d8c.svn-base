using Model;
using Model.Common;
using System;
using System.Collections.Generic;

namespace ViewModel
{
    /// <summary>
    /// 章节详情
    /// </summary>
    public class ChapterDetail
    {
        /// <summary>
        /// 小说ID
        /// </summary>
        public int NovelId { get; set; }

        /// <summary>
        /// 小说标题
        /// </summary>
        public string NovelTitle { get; set; }

        /// <summary>
        /// 小说作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 小说根路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 小说类别
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// CP
        /// </summary>
        public int CPId { get; set; }

        /// <summary>
        /// 计费类型
        /// </summary>
        public int FeeType { get; set; }

        /// <summary>
        /// 免费章节数
        /// </summary>
        public int FreeChapterCount { get; set; }

        /// <summary>
        /// 总章节数
        /// </summary>
        public int TotalChapterCount { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public int ContentType { get; set; }

        /// <summary>
        /// 章节Id
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// 章节标题
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// 章节序号
        /// </summary>
        public int ChapterCode { get; set; }

        /// <summary>
        /// 章节上架时间
        /// </summary>
        public DateTime OnlineTime { get; set; }

        /// <summary>
        /// 章节字数
        /// </summary>
        public int WordSize { get; set; }

        /// <summary>
        /// 章节文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public int IsFree { get; set; }

        /// <summary>
        /// 章节价格
        /// </summary>
        public int Fee { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        public string ChapterContent { get; set; }

        /// <summary>
        /// 是否第一章
        /// </summary>
        public bool IsFirstChapter { get; set; }

        /// <summary>
        /// 是否最后一章
        /// </summary>
        public bool IsLastChapter { get; set; }

        /// <summary>
        /// 上一章链接
        /// </summary>
        public string PreChapterUrl { get; set; }

        /// <summary>
        /// 下一章链接
        /// </summary>
        public string NextChapterUrl { get; set; }
    }

    /// <summary>
    /// 章节详情
    /// </summary>
    public class ChapterDetailView
    {
        public int UserBalance { get; set; }

        /// <summary>
        /// 小说详情
        /// </summary>
        public SimpleResponse<Novel> Novel { get; set; }

        /// <summary>
        /// 章节详情
        /// </summary>
        public SimpleResponse<Chapter> Chapter { get; set; }

        /// <summary>
        /// 千字价格
        /// </summary>
        public int ChapterWordSizeFee { get; set; }

        /// <summary>
        /// 章节价格
        /// </summary>
        public int ChapterFee { get; set; }

        /// <summary>
        /// 按本价格
        /// </summary>
        public int NovelFee { get; set; }

        /// <summary>
        /// 章节正文
        /// </summary>
        public string ChapterContent { get; set; }

        /// <summary>
        /// 是否存在上一章
        /// </summary>
        public bool IsPreChapterCode { get; set; }

        /// <summary>
        /// 是否存在下一章
        /// </summary>
        public bool IsNextChapterCode { get; set; }

        /// <summary>
        /// 上一章
        /// </summary>
        public string PreChapterUrl { get; set; }

        /// <summary>
        /// 下一章
        /// </summary>
        public string NextChapterUrl { get; set; }

        /// <summary>
        /// 推荐阅读-同类小说推荐
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> RecommendList { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsMark { get; set; }

        /// <summary>
        /// 漫画路径
        /// </summary>
        public SimpleResponse<IEnumerable<ExtendChapterView>> ExtendChapterList { get; set; }

        /// <summary>
        /// 听书路径
        /// </summary>
        public string ChapterAudioUrl { get; set; }

        /// <summary>
        /// 推荐阅读
        /// </summary>
        public SimpleResponse<IEnumerable<AD>> AdList { get; set; }

        public int ShowQrCodeMinChapterCode { get; set; }

        /// <summary>
        /// 回复关键字
        /// </summary>
        public string ReplyText { get; set; }

        public string HitUrl { get; set; }
    }
}