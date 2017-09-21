using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	/// <summary>
	/// 评论详情视图
	/// </summary>
	public class CommentDetailView
	{
		public bool IsOpen { get; set; }
		public int PageIndex { get; set; }
		public int RowCount { get; set; }

		/// <summary>
		/// 回复列表
		/// </summary>
		public SimpleResponse<IEnumerable<BookCommentReplyView>> replyList { get; set; }

		/// <summary>
		///
		/// </summary>
		public SimpleResponse<CommentView> commentDetail { get; set; }
	}
}