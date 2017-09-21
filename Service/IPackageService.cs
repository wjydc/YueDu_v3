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
	public interface IPackageService : IBaseService
	{
		PackageView GetNovelPackage(Constants.PackageType packageType, int novelId);

        IEnumerable<PackageView> GetPagerList(Constants.PackageType packageType, string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status = 1, int showType = 1);
	}
}