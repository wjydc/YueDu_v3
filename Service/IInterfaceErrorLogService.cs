using Model.Common;
using Service.Base;
using System.Collections.Generic;

namespace Service
{
    public interface IInterfaceErrorLogService : IBaseService
    {
        InterfaceErrorLog Get(long id);

        long Add(InterfaceErrorLog model);

        bool Update(InterfaceErrorLog model);

        bool Delete(InterfaceErrorLog model);

        IEnumerable<InterfaceErrorLog> GetPagerList(string where, out int rowCount, int pageIndex = 1, int pageSize = 10);
    }
}