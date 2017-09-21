using Model;
using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class InterfaceErrorLogService : BaseService, IInterfaceErrorLogService
    {
        public InterfaceErrorLog Get(long id)
        {
            if (id <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.InterfaceErrorLogRepo(conn);
                return repo.Get(id);
            }
        }

        public long Add(InterfaceErrorLog model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.InterfaceErrorLogRepo(conn);
                return repo.Insert(model);
            }
        }

        public bool Update(InterfaceErrorLog model)
        {
            if (model == null) return false;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.InterfaceErrorLogRepo(conn);
                return repo.Update(model);
            }
        }

        public bool Delete(InterfaceErrorLog model)
        {
            if (model == null) return false;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.InterfaceErrorLogRepo(conn);
                return repo.Delete(model);
            }
        }

        public IEnumerable<InterfaceErrorLog> GetPagerList(string where, out int rowCount, int pageIndex = 1, int pageSize = 10)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.InterfaceErrorLogRepo(conn);
                return repo.GetPagerList(where, out rowCount, pageIndex, pageSize);
            }
        }
    }
}