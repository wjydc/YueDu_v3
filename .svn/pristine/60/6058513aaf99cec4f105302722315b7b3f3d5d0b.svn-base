using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RechargeFeeConfigRepo : Repository<RechargeFeeConfigInfo>
    {
        protected IDbManage DbManage { get; private set; }

        public RechargeFeeConfigRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<RechargeFeeConfigInfo> GetAll(string columns = "*", string where = "", string orderBy = "")
        {
            string sql = string.Format("select {0} from Users.RechargeFeeConfig with (nolock) where 1=1 {1} {2}", columns, where, orderBy);
            return DbManage.Query<RechargeFeeConfigInfo>(sql);
        }
    }
}