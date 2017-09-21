using DapperExtension.Core;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ViewModel;

namespace Repository
{
    public class ChapterRedirectRepo
    {
        protected IDbManage DbManage { get; private set; }

        public ChapterRedirectRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public ChapterRedirect Get(int novelId, int status)
        {
            string sql = "select top 1 * from [dbo].[ChapterRedirect] with (nolock) where novelId = @novelId and status = @status order by id desc";

            return DbManage.Query<ChapterRedirect>(sql, new { novelId = novelId, status = status }).FirstOrDefault();
        }
    }
}