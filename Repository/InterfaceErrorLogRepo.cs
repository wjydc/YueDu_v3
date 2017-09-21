using DapperExtension.Core;
using Model.Common;
using System.Collections.Generic;
using System.Data;

namespace Repository
{
    public class InterfaceErrorLogRepo : Repository<InterfaceErrorLog>
    {
        protected IDbManage DbManage { get; private set; }

        public InterfaceErrorLogRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<InterfaceErrorLog> GetPagerList(string where, out int rowCount, int pageIndex = 1, int pageSize = 10)
        {
            string columns = "Id, Module, Method, Error, Brief, AddTime";
            string table = "[dbo].[InterfaceErrorLog] with (nolock)";
            string orderBy = "order by id desc";
            return DbManage.GetPagerList<InterfaceErrorLog>(columns, table, where, orderBy, out rowCount, pageIndex, pageSize);
        }
    }
}