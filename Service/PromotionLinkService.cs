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
    public class PromotionLinkService : BaseService, IPromotionLinkService
    {
        public PromotionLink Detail(int id = 0)
        {
            if (id <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new PromotionLinkRepo(conn);
                return repo.Detail(id);
            }
        }

        public int Update(string column, int id)
        {
            if (id <= 0 || string.IsNullOrEmpty(column)) return 0;

            using (var conn = DbConnection(DbOperation.Read)) 
            {
                var repo = new PromotionLinkRepo(conn);
                return repo.Update(column, id);
            }
        }

        public PromotionLink Detail( string where, object param = null)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new PromotionLinkRepo(conn);
                return repo.Detail(where, param);
            }
        }
    }
}