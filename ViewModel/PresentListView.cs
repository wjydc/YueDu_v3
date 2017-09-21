using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class PresentListView
	{
		public int NovelId { get; set; }
		public int RewardFee { get; set; }
		public string UserIcon { get; set; }
		public string UserRankInfo { get; set; }
		public SimpleResponse<IEnumerable<PresentView>> RankList { get; set; }
		public SimpleResponse<IEnumerable<PresentView>> RecentPresentList { get; set; }
		public SimpleResponse<IEnumerable<NovelPropsView>> NovelPropsList { get; set; }
	}
}