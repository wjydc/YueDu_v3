using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class BookListView
	{
		public int? CType { get; set; }
		public int ClassId { get; set; }
		public int UpdateStatus { get; set; }
		public int FeeType { get; set; }
		public int Sort { get; set; }
		public SimpleResponse<IEnumerable<NovelClassView>> NovelClassList { get; set; }
		public SimpleResponse<IEnumerable<NovelView>> BookList { get; set; }

		public int RowCount { get; set; }
	}

	/// <summary>
	/// 书库筛选类型实体
	/// </summary>
	public class BookListFilterInfo
	{
		/// <summary>
		/// 类型  null全部 0小说 1听书 2漫画
		/// </summary>
		public int? cType { get; set; }

		/// <summary>
		/// 分类
		/// </summary>
		public int? classId { get; set; }

		/// <summary>
		/// 进度   null全部 连载 完结
		/// </summary>
		public int? updateStatus { get; set; }

		/// <summary>
		/// 收费类型    null全部 免费小说 付费小说
		/// </summary>
		public int feeType { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		public int sort { get; set; }
	}
}