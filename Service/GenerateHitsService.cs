using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GenerateHitsService : BaseService, IGenerateHitsService
    {
        public IEnumerable<string> GetPagerList(int pageIndex, int pageSize, out int rowCount, int status = 1)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.GenerateHitsRepo(conn);
                return repo.GetPagerList(status, pageIndex, pageSize, out rowCount);
            }
        }
    }
}