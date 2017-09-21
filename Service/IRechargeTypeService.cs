using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRechargeTypeService : IBaseService
    {
        IEnumerable<RechargeType> GetAll(string where, string orderBy = "");
    }
}