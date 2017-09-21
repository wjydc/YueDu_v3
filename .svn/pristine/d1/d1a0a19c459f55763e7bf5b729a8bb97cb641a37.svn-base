using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NovelClassService : BaseService, INovelClassService
    {
        public IEnumerable<NovelClass> GetAll(int contentType, int status = 1)
        {
            IEnumerable<NovelClass> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.NovelClassRepo(conn);
                list = repo.GetAll(contentType, status);
            }

            return list;
        }
    }
}