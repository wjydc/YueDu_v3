using DapperExtension.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class NovelUserConsumeRepo : Repository<NovelUserConsumeRepo>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelUserConsumeRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public int GetUserFeeByNovel(string userName, int novelId)
        {
            string sql = " SELECT [Fee] FROM [Rank].[NovelUserConsume] with (nolock) where UserName = @userName And NovelId = @novelId ";
            return DbManage.ExecuteScalar<int>(sql, new { userName, novelId });
        }

        public IEnumerable<PresentView> GetPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param)
        {
            return DbManage.GetPagerList<PresentView>(columns, table, where, orderBy, out rowCount, pageIndex, pageSize, param);
        }
    }
}