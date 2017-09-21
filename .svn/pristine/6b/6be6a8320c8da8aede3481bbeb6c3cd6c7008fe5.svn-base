using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RechargeTypeService : BaseService, IRechargeTypeService
    {
        public IEnumerable<RechargeType> GetAll(string where, string orderBy = "")
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.RechargeTypeRepo(conn);
                return repo.GetAll(where, orderBy);
            }
        }
    }
}