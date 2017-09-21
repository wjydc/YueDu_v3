using DapperExtension.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenerateHitsRepo
    {
        protected IDbManage DbManage { get; private set; }

        public GenerateHitsRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<string> GetPagerList(int status, int pageIndex, int pageSize, out int rowCount)
        {
            return DbManage.GetPagerList<string>("url", "Wap.GenerateHits", " and status = @status ", " order by sortid desc, id desc", out rowCount, pageIndex, pageSize, new { status });
        }
    }
}