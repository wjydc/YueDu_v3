using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	[Table("Log.NovelPropsUserConsumeLog")]
	public class NovelPropsUserConsumeLog
	{
		[IdentityKey]
		public int Id { get; set; }

		public int NovelId { get; set; }

		public int PropsId { get; set; }
		public int PropsCount { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public int Fee { get; set; }
		public int BindFee { get; set; }
		public DateTime UpdateTime { get; set; }

		public DateTime AddTime { get; set; }
	}
}