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
    public class ExtendChapterRepo : Repository<ExtendChapter>
    {
        protected IDbManage DbManage { get; private set; }

        public ExtendChapterRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public ExtendChapter GetExtendChapter(int novelId, int chapterCode)
        {
            string sql = "select id, ChapterPath, MediumPath1 from dbo.ExtendChapter with (nolock) where novelid = @novelid and chaptercode = @chaptercode";
            ExtendChapter extendChapterInfo = DbManage.Query<ExtendChapter>(sql, new { novelId = novelId, chapterCode = chapterCode }).FirstOrDefault();
            return extendChapterInfo;
        }
    }
}