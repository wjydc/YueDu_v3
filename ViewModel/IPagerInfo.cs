using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public interface IPagerInfo
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        object Parameters { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页多少条
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int RowCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }
    }
}