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
    public class NovelClassRepo : Repository<NovelClass>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelClassRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<NovelClass> GetAll(int? contentType, int status)
        {
            StringBuilder sql = new StringBuilder("select Id,className from [dbo].[NovelClass] where status = @status ");

            if (contentType.HasValue)
            {
                sql.Append(" and isnull(ContentType,0) = @contentType ");

                return DbManage.Query<NovelClass>(sql.ToString(), new { contentType, status });
            }
            else
                return DbManage.Query<NovelClass>(sql.ToString(), new { status });
        }
    }
}