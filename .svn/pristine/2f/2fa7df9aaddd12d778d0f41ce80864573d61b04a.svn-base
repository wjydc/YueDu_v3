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
    public class HotSearchWordRepo : Repository<HotSearchWord>
    {
        protected IDbManage DbManage { get; private set; }

        public HotSearchWordRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        /// 热搜列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HotSearchWord> GetPagerList(out int rowCount, int pageIndex = 1, int pageSize = 10)
        {
            string orderby = " order by id desc ";
            string columns = "Id, Name, Icon, Description";
            string table = "dbo.HotSearchWord with (nolock)";
            return DbManage.GetPagerList<HotSearchWord>(columns, table, "", orderby, out rowCount, pageIndex, pageSize);
        }
    }
}