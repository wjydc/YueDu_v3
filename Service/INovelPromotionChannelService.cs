using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface INovelPromotionChannelService
    {
        NovelPromotionChannel Detail(int id = 0, int status = 1);
    }
}
