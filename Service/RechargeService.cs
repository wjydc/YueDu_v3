using Model;
using Service.Base;
using System.Collections.Generic;

namespace Service
{
    public class RechargeService : BaseService, IRechargeService
    {
        public RechargeInfo GetOrder(string orderId, string userName)
        {
            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.RechargeRepo(conn);
                return repo.GetOrder(orderId, userName);
            }
        }

        public RechargeInfo Generate(RechargeInfo model, int feeConfigId, int smsPayType, out int result)
        {
            result = 0;
            if (model == null) return null;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.RechargeRepo(conn);
                return repo.Generate(model, feeConfigId, smsPayType, out result);
            }
        }

        public int Completed(RechargeInfo model, out int result)
        {
            result = 0;
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.RechargeRepo(conn);
                return repo.Completed(model, out result);
            }
        }

        public IEnumerable<RechargeInfo> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 10, object param = null)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.RechargeRepo(conn);
                return repo.GetPagerList(columns, where, orderBy, out rowCount, pageIndex, pageSize, param);
            }
        }
    }
}