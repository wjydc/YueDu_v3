﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	///<summary>
	///表NovelComment的实体类
	///</summary>
	[Table("Users.NovelComment")]
	public class NovelComment
	{
		/// <summary>
		/// id
		/// </summary>
		[IdentityKey]
		public int Id { get; set; }

		/// <summary>
		/// novelId
		/// </summary>
		public int NovelId { get; set; }

		/// <summary>
		/// message
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// goodCount
		/// </summary>
		public int GoodCount { get; set; }

		/// <summary>
		/// badCount
		/// </summary>
		public int BadCount { get; set; }

		/// <summary>
		/// replyCount
		/// </summary>
		public int ReplyCount { get; set; }

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
		/// status
		/// </summary>
		public int Status { get; set; }

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
		/// authorId
		/// </summary>
		public int AuthorId { get; set; }

		/// <summary>
		/// propsId
		/// </summary>
		public int PropsId { get; set; }

		/// <summary>
		/// propsCount
		/// </summary>
		public int PropsCount { get; set; }

		/// <summary>
		/// sortId
		/// </summary>
		public int SortId { get; set; }

		/// <summary>
		/// userType
		/// </summary>
		public int UserType { get; set; }

		/// <summary>
		/// title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// description
		/// </summary>
		public string Description { get; set; }
	}
}