﻿using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class BookDetailView
	{
		/// <summary>
		/// id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 小说标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 小说封面（大）
		/// </summary>
		public string LargeCover { get; set; }

		/// <summary>
		/// smallCover
		/// </summary>
		public string SmallCover { get; set; }

		/// <summary>
		/// thumbCover
		/// </summary>
		public string ThumbCover { get; set; }

		/// <summary>
		/// classId
		/// </summary>
		public int ClassId { get; set; }

		/// <summary>
		/// author
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// tags
		/// </summary>
		public string Tags { get; set; }

		/// <summary>
		/// shortDescription
		/// </summary>
		public string ShortDescription { get; set; }

		/// <summary>
		/// description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// updateStatus
		/// </summary>
		public int UpdateStatus { get; set; }

		/// <summary>
		/// hits
		/// </summary>
		public long Hits { get; set; }

		/// <summary>
		/// 总评论数
		/// </summary>
		public int CommentCount { get; set; }

		/// <summary>
		/// favCount
		/// </summary>
		public int FavCount { get; set; }

		/// <summary>
		/// buyCount
		/// </summary>
		public int BuyCount { get; set; }

		/// <summary>
		/// downloadCount
		/// </summary>
		public int DownloadCount { get; set; }

		/// <summary>
		/// filePath
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// fileChapterTitle
		/// </summary>
		public string FileChapterTitle { get; set; }

		/// <summary>
		/// fileName
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// fileChapterPosition
		/// </summary>
		public string FileChapterPosition { get; set; }

		/// <summary>
		/// shareType
		/// </summary>
		public int ShareType { get; set; }

		/// <summary>
		/// feeType
		/// </summary>
		public int FeeType { get; set; }

		/// <summary>
		/// chapterFee
		/// </summary>
		public int ChapterFee { get; set; }

		/// <summary>
		/// fee
		/// </summary>
		public int Fee { get; set; }

		/// <summary>
		/// bindChapterFeeTotalChapterCount
		/// </summary>
		public int BindChapterFee { get; set; }

		/// <summary>
		/// bindFee
		/// </summary>
		public int BindFee { get; set; }

		/// <summary>
		/// rewardFee
		/// </summary>
		public long RewardFee { get; set; }

		/// <summary>
		/// freeChapterCount
		/// </summary>
		public int FreeChapterCount { get; set; }

		/// <summary>
		/// 总章节数
		/// </summary>
		public int TotalChapterCount { get; set; }

		/// <summary>
		/// firstChapterId
		/// </summary>
		public int FirstChapterId { get; set; }

		/// <summary>
		/// recentChapterId
		/// </summary>
		public int RecentChapterId { get; set; }

		/// <summary>
		/// recentChapterCode
		/// </summary>
		public int RecentChapterCode { get; set; }

		/// <summary>
		/// 最新章节名称
		/// </summary>
		public string RecentChapterName { get; set; }

		/// <summary>
		/// recentChapterUpdateTime
		/// </summary>
		public DateTime RecentChapterUpdateTime { get; set; }

		/// <summary>
		/// wordSize
		/// </summary>
		public long WordSize { get; set; }

		/// <summary>
		/// shortWordSize
		/// </summary>
		public string ShortWordSize { get; set; }

		/// <summary>
		/// userConsume
		/// </summary>
		public int UserConsume { get; set; }

		/// <summary>
		/// fNovelId
		/// </summary>
		public string FNovelId { get; set; }

		/// <summary>
		/// userId
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// cPId
		/// </summary>
		public int CPId { get; set; }

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
		/// txtStatus
		/// </summary>
		public int TxtStatus { get; set; }

		/// <summary>
		/// txtCompleteTime
		/// </summary>
		public DateTime TxtCompleteTime { get; set; }

		/// <summary>
		/// contentType
		/// </summary>
		public int ContentType { get; set; }

		/// <summary>
		/// authorFee
		/// </summary>
		public int AuthorFee { get; set; }

		/// <summary>
		/// authorMonthFee
		/// </summary>
		public int AuthorMonthFee { get; set; }

		/// <summary>
		/// authorRewardRate
		/// </summary>
		public decimal AuthorRewardRate { get; set; }

		/// <summary>
		/// authorOrderRate
		/// </summary>
		public decimal AuthorOrderRate { get; set; }

		/// <summary>
		/// authorContractLevel
		/// </summary>
		public int AuthorContractLevel { get; set; }

		/// <summary>
		/// classSpeType
		/// </summary>
		public int ClassSpeType { get; set; }

		/// <summary>
		/// readDirection
		/// </summary>
		public int ReadDirection { get; set; }

		/// <summary>
		/// copyrightStatus
		/// </summary>
		public int CopyrightStatus { get; set; }

		/// <summary>
		/// cartoonType
		/// </summary>
		public int CartoonType { get; set; }

		public bool IsAddMark { get; set; }

		public string AuthorNotice { get; set; }

		public SimpleResponse<IEnumerable<ChapterView>> ChapterList { get; set; }

		public string CPName { get; set; }//漫画详情-来源

		public SimpleResponse<IEnumerable<CommentView>> CommentList { get; set; }

		public bool IsOrder { get; set; }

		/// <summary>
		/// 推荐位-猜你喜欢
		/// </summary>
		public int RecClassId { get; set; }

		public DateTime? PackageEndTime { get; set; }
		public int RecentReadChapterCode { get; set; }

        public int IsHideAuthor { get; set; }
	}
}