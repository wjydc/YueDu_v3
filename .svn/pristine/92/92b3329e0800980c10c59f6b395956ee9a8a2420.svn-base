using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;

namespace Service
{
    public class ChannelCompanyService : BaseService, IChannelCompanyService
    {
        public string GetRecentReadChannelId(int id, string channelId)
        {
            if (id <= 0) return channelId;

            using (var conn = DbConnection(DbOperation.Read))
            {
                using (var tran = BeginTransaction(conn))
                {
                    var repo = new Repository.ChannelCompanyRepo(conn, tran);
                    if (!string.IsNullOrEmpty(channelId) && repo.GetCount(id, channelId) > 0)
                    {
                    }
                    else
                    {
                        channelId = repo.GetRecentReadDefaultChannelId(id);
                    }
                }
            }

            return channelId;
        }

        public string GetRecentReadChannelId(int id, string channelId, out Constants.Novel.StatisticType statisticType)
        {
            statisticType = Constants.Novel.StatisticType.none;

            if (id <= 0) return channelId;

            using (var conn = DbConnection(DbOperation.Read))
            {
                using (var tran = BeginTransaction(conn))
                {
                    var repo = new Repository.ChannelCompanyRepo(conn, tran);
                    if (!string.IsNullOrEmpty(channelId) && repo.GetCount(id, channelId) > 0)
                    {
                        statisticType = Constants.Novel.StatisticType.promotion;
                    }
                    else
                    {
                        statisticType = Constants.Novel.StatisticType.menu;
                        channelId = repo.GetRecentReadDefaultChannelId(id);
                    }

                    tran.Commit();
                }
            }

            return channelId;
        }
    }
}