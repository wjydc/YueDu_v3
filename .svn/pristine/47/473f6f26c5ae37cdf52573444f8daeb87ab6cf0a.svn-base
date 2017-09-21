using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IGenerateHitsService : IBaseService
    {
        IEnumerable<string> GetPagerList(int pageIndex, int pageSize, out int rowCount, int status = 1);
    }
}