using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public class RecommendService : BaseService, IRecommendService
    {
        public IEnumerable<RecommendView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status = 1)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.RecommendRepo(conn);
                return repo.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, param, status);
            }
        }
    }
}