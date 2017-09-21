using DapperExtension.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ChannelCompanyRepo
    {
        protected IDbManage DbManage { get; private set; }

        public ChannelCompanyRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public int GetCount(int id, string channelId)
        {
            return DbManage.ExecuteScalar<int>("select count(1) from dbo.ChannelCompany a with(nolock) inner join dbo.Channel b with(nolock) on a.Id = b.CompanyId where a.Status = 1 and b.Status = 1 and a.Id = @Id and b.ChannelCode = @ChannelCode", new { Id = id, ChannelCode = channelId });
        }

        public string GetRecentReadDefaultChannelId(int id)
        {
            return DbManage.ExecuteScalar<string>("select top 1 b.ChannelCode from dbo.ChannelCompany a with(nolock) inner join dbo.Channel b with(nolock) on a.Id = b.CompanyId where a.Status = 1 and b.Status = 1 and a.Id = @Id order by b.id asc", new { Id = id });
        }
    }
}