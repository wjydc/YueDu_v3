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
    public class SiteConfigRepo : Repository<SiteConfig>
    {
        protected IDbManage DbManage { get; private set; }

        public SiteConfigRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public SiteConfig GetModel(string channelId, int status)
        {
            string sql = "select a.* from Market.SiteConfig a with (nolock) inner join dbo.Channel b with (nolock) on a.ChannelCompanyId = b.CompanyRootId where a.status = @status and b.status = @status and b.channelCode = @channelId";
            return DbManage.Query<SiteConfig>(sql, new { status, channelId }).FirstOrDefault();
        }
    }
}