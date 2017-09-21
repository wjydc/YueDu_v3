using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISiteConfigService : IBaseService
    {
        SiteConfig GetModel(string channelId, int status = 1);
    }
}