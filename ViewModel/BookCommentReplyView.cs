using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class BookCommentReplyView
	{
		public int Id { get; set; }

		/// <summary>
		/// message
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// userId
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// userName
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// addTime
		/// </summary>
		public DateTime AddTime { get; set; }

		/// <summary>
		/// UserNickName
		/// </summary>
		public string UserNickName { get; set; }

		/// <summary>
		/// UserIcon
		/// </summary>
		public string UserIcon { get; set; }

		/// <summary>
		/// ToNickName
		/// </summary>
		public string ToNickName { get; set; }

		/// <summary>
		/// 回复的评论ID
		/// </summary>
		public int ReplyId { get; set; }
	}
}