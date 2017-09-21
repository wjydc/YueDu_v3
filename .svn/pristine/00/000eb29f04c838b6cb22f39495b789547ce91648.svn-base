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
    public class AuthorNoticeRepo : Repository<AuthorNotice>
    {
        protected IDbManage DbManage { get; private set; }

        public AuthorNoticeRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        /// 作者的话
        /// </summary>
        public AuthorNotice GetAuthorNotice(string columns, string where, string orderby, object param)
        {
            string sql = string.Format("select {0} from Users.AuthorNotice with (nolock) where(1=1) {1} {2} ", columns, where, orderby);
            return DbManage.Query<AuthorNotice>(sql, param).FirstOrDefault();
        }
    }
}