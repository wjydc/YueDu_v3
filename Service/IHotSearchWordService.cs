using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IHotSearchWordService:IBaseService
    {
        /// <summary>
        /// 获取所有热搜列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<HotSearchWord> GetAll();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<HotSearchWord> GetPagerList(out int rowCount, int pageIndex = 1, int pageSize = 10);

    }
}
