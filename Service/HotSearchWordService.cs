using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class HotSearchWordService : BaseService, IHotSearchWordService
    {
        /// <summary>
        /// 获取所有热搜列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HotSearchWord> GetAll()
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.HotSearchWordRepo(conn);
                return repo.GetAll();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<HotSearchWord> GetPagerList(out int rowCount, int pageIndex = 1, int pageSize = 10)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.HotSearchWordRepo(conn);
                return repo.GetPagerList(out rowCount, pageIndex, pageSize);
            }
        }
    }
}