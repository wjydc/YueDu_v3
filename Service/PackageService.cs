using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public class PackageService : BaseService, IPackageService
    {
        public IEnumerable<PackageView> GetPagerList(Constants.PackageType packageType, string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status = 1, int showType = 1)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.PackageRepo(conn);
                return repo.GetPagerList(packageType, where, orderby, pageIndex, pageSize, out rowCount, param, status, showType);
            }
        }

        public PackageView GetNovelPackage(Constants.PackageType packageType, int novelId)
        {
            PackageView result = null;

            if (novelId > 0)
            {
                using (var conn = DbConnection(DbOperation.Read))
                {
                    var repo = new Repository.PackageRepo(conn);
                    result = repo.GetNovelPackage(packageType, novelId);
                }
            }

            return result;
        }
    }
}