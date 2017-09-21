using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPromotionLinkService : IBaseService
    {
        PromotionLink Detail(int id = 0);

        int Update(string column, int id);

        PromotionLink Detail(string where, object param = null);
    }
}