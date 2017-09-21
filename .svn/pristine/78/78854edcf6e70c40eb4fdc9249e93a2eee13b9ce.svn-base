using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RechargeFeeConfigService : BaseService, IRechargeFeeConfigService
    {
        public IEnumerable<RechargeFeeConfigInfo> GetAll(string columns = "*", string where = "", string orderBy = "")
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.RechargeFeeConfigRepo(conn);
                return repo.GetAll(columns, where, orderBy);
            }
        }
    }
}