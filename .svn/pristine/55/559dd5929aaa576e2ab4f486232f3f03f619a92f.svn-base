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
    public class CPRepo : Repository<AuthorNotice>
    {
        protected IDbManage DbManage { get; private set; }

        public CPRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        /// CP(来源)
        /// </summary>
        public CP Detail(string columns, string where, string orderby, object param)
        {
            string sql = string.Format("select {0} from [dbo].[CP] with (nolock) where (1=1) {1} {2} ", columns, where, orderby);
            return DbManage.Query<CP>(sql, param).FirstOrDefault();
        }
    }
}