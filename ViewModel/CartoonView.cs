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
    /// 漫画首页视图
    /// </summary>
    public class CartoonView
    {
        /// <summary>
        /// 首页-收藏 - 推荐位
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> FavRecList { get; set; }

        /// <summary>
        /// 首页-人气排行
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> HotRecList { get; set; }

        /// <summary>
        /// 首页-三次元漫画
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> ThreeRecList { get; set; }

        /// <summary>
        /// 首页-顶端广告位
        /// </summary>
        public SimpleResponse<IEnumerable<AD>> HeadADList { get; set; }

        /// <summary>
        /// 首页-中间广告位
        /// </summary>
        public SimpleResponse<IEnumerable<AD>> MiddleADList { get; set; }
    }
}
