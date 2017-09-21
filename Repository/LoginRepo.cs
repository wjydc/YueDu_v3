using DapperExtension.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LoginRepo
    {
        protected IDbManage DbManage { get; private set; }

        public LoginRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public DateTime UpdateLoginInfo(string userName, int status)
        {
            string sql = "declare @curtime datetime = getdate();update [dbo].[Users] set logincount = isnull(logincount, 0) + 1, recentlogintime = @curtime where username = @userName and status = @status;select @curtime";

            return DbManage.ExecuteScalar<DateTime>(sql, new { userName, status });
        }
    }
}