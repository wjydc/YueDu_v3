using DapperExtension.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BlackListRepo
    {
        protected IDbManage DbManage { get; private set; }

        public BlackListRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetCount(string where, object param)
        {
            string sql = string.Format("select count(1) from [dbo].[BlackList] where (1=1) {0} ", where);
            return DbManage.ExecuteScalar<int>(sql, param);
        }
    }
}