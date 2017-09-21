using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	/// <summary>
	///小说视图模型
	/// </summary>
	public class NovelView
	{
		public int Id { get; set; }

		/// <summary>
		/// 小说标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 作者
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// 分类名
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// 连载状态
		/// </summary>
		public int UpdateStatus { get; set; }

		/// <summary>
		/// 字数
		/// </summary>
		public string ShortWordSize { get; set; }

		/// <summary>
		/// 小说类型（音频、小说）
		/// </summary>
		public int ContentType { get; set; }

		/// <summary>
		/// 最新章节
		/// </summary>
		public string RecentChapterName { get; set; }

		/// <summary>
		/// 捧场总数
		/// </summary>
		public long RewardFee { get; set; }

		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime RecentChapterUpdateTime { get; set; }

		/// <summary>
		/// 大图
		/// </summary>
		public string LargeCover { get; set; }

		/// <summary>
		/// 小图
		/// </summary>
		public string SmallCover { get; set; }

		/// <summary>
		/// 缩略图
		/// </summary>
		public string ThumbCover { get; set; }

		/// <summary>
		/// 标签
		/// </summary>
		public string Tags { get; set; }

		/// <summary>
		/// 概述
		/// </summary>
		public string ShortDescription { get; set; }

        /// <summary>
        /// 是否隐藏作者
        /// </summary>
        public int IsHideAuthor { get; set; }
	}
}