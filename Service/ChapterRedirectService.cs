using Model;
using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace Service
{
    public class ChapterRedirectService : BaseService, IChapterRedirectService
    {
        public ChapterRedirect Get(int novelId, int status = 1)
        {
            if (novelId <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ChapterRedirectRepo(conn);
                return repo.Get(novelId, status);
            }
        }
    }
}