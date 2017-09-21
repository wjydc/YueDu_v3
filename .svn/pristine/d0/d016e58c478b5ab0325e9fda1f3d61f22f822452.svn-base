using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class UsersRepo : Repository<Users>
    {
        protected IDbManage DbManage { get; private set; }

        public UsersRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public UsersView GetDetail(string userName, int status)
        {
            string sql = "select Id, UserName, NickName, Icon, Fee from [dbo].[Users] where username = @userName and status = @status";

            return DbManage.Query<UsersView>(sql, new { userName, status }).FirstOrDefault();
        }

        public UsersView GetDetail(int userId, int status)
        {
            string sql = "select Id, UserName, NickName, Icon, Fee from [dbo].[Users] where id = @userId and status = @status";

            return DbManage.Query<UsersView>(sql, new { userId, status }).FirstOrDefault();
        }
    }
}