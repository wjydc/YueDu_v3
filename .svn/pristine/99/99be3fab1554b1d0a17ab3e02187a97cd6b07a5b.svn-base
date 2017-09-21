using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SiteConfigService : BaseService, ISiteConfigService
    {
        public SiteConfig GetModel(string channelId, int status = 1)
        {
            if (string.IsNullOrEmpty(channelId)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.SiteConfigRepo(conn);
                return repo.GetModel(channelId, status);
            }
        }
    }
}