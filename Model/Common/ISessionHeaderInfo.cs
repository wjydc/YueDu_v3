using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public interface ISessionHeaderInfo
    {
        string ChannelId { set; get; }
        string RouteChannelId { set; get; }
        string PromotionCode { set; get; }
        string HeaderId { set; get; }
    }
}