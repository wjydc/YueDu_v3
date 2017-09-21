using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PagerInfo : IPagerInfo
    {
        public string Url { get; set; }

        public object Parameters { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int PageCount { get { return (int)Math.Ceiling((double)RowCount / PageSize); } }

        public PagerInfo()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        public PagerInfo(int rowCount, int pageIndex = 1, int pageSize = 10)
        {
            RowCount = rowCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PagerInfo(int rowCount, string url = "", object parameters = null, int pageIndex = 1, int pageSize = 10)
        {
            RowCount = rowCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Url = url;
            Parameters = parameters;
        }
    }
}