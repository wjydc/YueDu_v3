using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ExtendChapterService : BaseService, IExtendChapterService
    {
        public ExtendChapter GetExtendChapter(int novelId, int chapterCode)
        {
            if (novelId <= 0 || chapterCode < 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ExtendChapterRepo(conn);
                var model = repo.GetExtendChapter(novelId, chapterCode);
                return model;
            }
        }
    }
}