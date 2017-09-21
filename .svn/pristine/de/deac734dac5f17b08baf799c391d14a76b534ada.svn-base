using Model;
using Repository;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NovelPromotionChannelService : BaseService, INovelPromotionChannelService
    {
        public NovelPromotionChannel Detail(int id = 0, int status = 1)
        {
            if (id <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new NovelPromotionChannelRepo(conn);
                return repo.Detail(id, status);
            }
        }
    }
}