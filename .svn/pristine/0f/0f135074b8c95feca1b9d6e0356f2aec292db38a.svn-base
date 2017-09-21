using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	[Table("dbo.NovelProps")]
	public class NovelProps
	{
		[IdentityKey]
		public int Id { get; set; }

		public string Name { get; set; }//道具名
		public string Description { get; set; }//描述
		public int Fee { get; set; }
		public int BindFee { get; set; }
		public int FeeType { get; set; }
		public int SortId { get; set; }
		public int Status { get; set; }
		public string Creator { get; set; }
		public DateTime UpdateTime { get; set; }
		public DateTime AddTime { get; set; }
		public string Icon { get; set; }
		public string LargeIcon { get; set; }
		public string SmallIcon { get; set; }
	}
}