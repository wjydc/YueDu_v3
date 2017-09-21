using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	///<summary>
	///表NovelClass的实体类
	///</summary>
	[Table("dbo.NovelClass")]
	public class NovelClass
	{
		/// <summary>
		/// id
		/// </summary>
		[IdentityKey]
		public int Id { get; set; }

		/// <summary>
		/// className
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// icon
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// thumbIcon
		/// </summary>
		public string ThumbIcon { get; set; }

		/// <summary>
		/// font
		/// </summary>
		public string Font { get; set; }

		/// <summary>
		/// parentId
		/// </summary>
		public int ParentId { get; set; }

		/// <summary>
		/// parentPath
		/// </summary>
		public string ParentPath { get; set; }

		/// <summary>
		/// level
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		/// linkUrl
		/// </summary>
		public string LinkUrl { get; set; }

		/// <summary>
		/// speType
		/// </summary>
		public int SpeType { get; set; }

		/// <summary>
		/// novelCount
		/// </summary>
		public int NovelCount { get; set; }

		/// <summary>
		/// sortId
		/// </summary>
		public int SortId { get; set; }

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
		/// qrCodeId
		/// </summary>
		public int QrCodeId { get; set; }

		public int ContentType { get; set; }
	}
}