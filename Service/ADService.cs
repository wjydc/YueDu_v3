using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ADService : BaseService, IADService
    {
        public IEnumerable<AD> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status = 1)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ADRepo(conn);
                return repo.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, param, status);
            }
        }
    }
}