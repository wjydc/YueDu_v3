using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 首页视图
    /// </summary>
    public class HomeView
    {
        /// <summary>
        /// 首页-热门推荐
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> HotRecList { get; set; }

        /// <summary>
        /// 首页-限时免费
        /// </summary>
        public SimpleResponse<IEnumerable<PackageView>> FreeRecList { get; set; }

        /// <summary>
        /// 首页-潜力新作
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> NewRecList { get; set; }

        /// <summary>
        /// 首页-主编力推
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> AuthorRecList { get; set; }

        /// <summary>
        /// 首页-热销专区
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> SellRecList { get; set; }

        /// <summary>
        /// 首页-听书专区
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> ListenRecList { get; set; }

        /// <summary>
        /// 首页-顶端广告位
        /// </summary>
        public SimpleResponse<IEnumerable<AD>> HeadADList { get; set; }

        /// <summary>
        /// 首页-中间广告位
        /// </summary>
        public SimpleResponse<IEnumerable<AD>> MiddleADList { get; set; }

        /// <summary>
        /// 首页-最近阅读
        /// </summary>
        public SimpleResponse<NovelRecentReadView> RecentRead { get; set; }

        /// <summary>
        /// 男女频
        /// </summary>
        public string ClassSpeType { get; set; }
    }
}