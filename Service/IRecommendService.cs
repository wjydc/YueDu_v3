using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public interface IRecommendService : IBaseService
    {
        IEnumerable<RecommendView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status = 1);
    }
}
