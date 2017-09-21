using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BlackListService : BaseService, IBlackListService
    {
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetCount(string where, object param)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.BlackListRepo(conn);
                return repo.GetCount(where, param);
            }
        }
    }
}