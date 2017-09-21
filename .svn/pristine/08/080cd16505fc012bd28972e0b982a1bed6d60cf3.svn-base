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
    public class RechargeTypeRepo : Repository<RechargeType>
    {
        protected IDbManage DbManage { get; private set; }

        public RechargeTypeRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        ///获取所有支付类型
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<RechargeType> GetAll(string where, string orderBy = "")
        {
            return DbManage.Query<RechargeType>(string.Format("select * from users.RechargeType with (nolock) where 1=1 {0} {1}", where, orderBy));
        }
    }
}