using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : BaseService, IOrderService
    {
        /// <summary>
        /// 是否按本或按章订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int IsReadChapter(ChapterOrderInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.OrderRepo(conn);
                return repo.IsReadChapter(model);
            }
        }

        /// <summary>
        /// 是否按本订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int IsOrderNovel(ChapterOrderInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.OrderRepo(conn);
                return repo.IsOrderNovel(model);
            }
        }

        /// <summary>
        /// 按章订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrderChapter(ChapterOrderInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.OrderRepo(conn);
                return repo.OrderChapter(model);
            }
        }

        /// <summary>
        /// 按本订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrderNovel(NovelOrderInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.OrderRepo(conn);
                return repo.OrderNovel(model);
            }
        }

        /// <summary>
        /// 包月订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrderPackage(PackageOrderInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.OrderRepo(conn);
                return repo.OrderPackage(model);
            }
        }

        /// <summary>
        /// 获取包月信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public PackageOrderInfo GetPackageOrder(string where)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.OrderRepo(conn);
                return repo.GetPackageOrder(where);
            }
        }
    }
}