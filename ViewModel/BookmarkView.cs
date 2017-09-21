using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class BookmarkView
	{
		/// <summary>
		/// NovelId
		/// </summary>
		public int NovelId { get; set; }

		/// <summary>
		/// novelTotalCount
		/// </summary>
		public int NovelTotalCount { get; set; }

		/// <summary>
		/// novelCount
		/// </summary>
		public int NovelCount { get; set; }

		/// <summary>
		/// novelInsertCount
		/// </summary>
		public int NovelInsertCount { get; set; }

		/// <summary>
		/// novelUpdateCount
		/// </summary>
		public int NovelUpdateCount { get; set; }

		/// <summary>
		/// userId
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// userName
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// clientId
		/// </summary>
		public int ClientId { get; set; }

		/// <summary>
		/// version
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// userAgent
		/// </summary>
		public string UserAgent { get; set; }

		/// <summary>
		/// imei
		/// </summary>
		public string IMEI { get; set; }

		/// <summary>
		/// imsi
		/// </summary>
		public string IMSI { get; set; }

		/// <summary>
		/// channelId
		/// </summary>
		public string ChannelId { get; set; }

		/// <summary>
		/// sourceType
		/// </summary>
		public int SourceType { get; set; }

		/// <summary>
		/// mobile
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// netType
		/// </summary>
		public string NetType { get; set; }

		/// <summary>
		/// province
		/// </summary>
		public string Province { get; set; }

		/// <summary>
		/// city
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// referUrl
		/// </summary>
		public string ReferUrl { get; set; }

		/// <summary>
		/// iPAddress
		/// </summary>
		public string IPAddress { get; set; }

		/// <summary>
		/// remoteHost
		/// </summary>
		public string RemoteHost { get; set; }

		/// <summary>
		/// updateTime
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// addTime
		/// </summary>
		public DateTime AddTime { get; set; }

		/// <summary>
		/// HistoryCount
		/// </summary>
		public int HistoryCount { get; set; }

		/// <summary>
		/// 小说标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 章节标题
		/// </summary>
		public string ChapterName { get; set; }

		public string LargeCover { get; set; }
		public string SmallCover { get; set; }
		public string ThumbCover { get; set; }
	}
}