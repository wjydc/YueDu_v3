using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;

namespace Service
{
    public interface IChannelCompanyService : IBaseService
    {
        string GetRecentReadChannelId(int id, string channelId);

        string GetRecentReadChannelId(int id, string channelId, out Constants.Novel.StatisticType statisticType);
    }
}