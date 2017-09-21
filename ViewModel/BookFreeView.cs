using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 免费专区视图
    /// </summary>
    public class BookFreeView
    {
        /// <summary>
        /// 免费新书
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> NewFreeList { get; set; }

        /// <summary>
        /// 热门新书
        /// </summary>
        public SimpleResponse<IEnumerable<RecommendView>> HotFreeList { get; set; }

        /// <summary>
        /// 限时免费
        /// </summary>
        public SimpleResponse<IEnumerable<PackageView>> LimitFreeList { get; set; }
    }
}
