using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class HeaderView
	{
		public HeaderView(string title, bool showUser = true, bool showIndex = true, bool showBookmark = true)
		{
			this.Title = title;
			this.ShowUser = showUser;
			this.ShowIndex = showIndex;
			this.ShowBookmark = showBookmark;
		}

		/// <summary>
		/// 当前页面标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 是否显示个人中心，默认显示
		/// </summary>
		public bool ShowUser { get; set; }

		/// <summary>
		/// 是否显示首页图标，默认显示
		/// </summary>
		public bool ShowIndex { get; set; }

		public bool ShowBookmark { get; set; }
	}
}