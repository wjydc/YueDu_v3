using Model;
using Model.Common;
using Service.Base;
using System.Collections.Generic;

namespace Service
{
    public interface IRechargeService : IBaseService
    {
        /// <summary>
        /// 查询订单状态
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        RechargeInfo GetOrder(string orderId, string userName);

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="model">订单</param>
        /// <param name="feeConfigId">计费点ID</param>
        /// <param name="smsPayType">短信支付方式(int)Constants.PayType.sms</param>
        /// <param name="result">成功 = 1，失败 = 0...</param>
        /// <returns></returns>
        RechargeInfo Generate(RechargeInfo model, int feeConfigId, int smsPayType, out int result);

        /// <summary>
        /// 充值完成
        /// </summary>
        /// <param name="model"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        int Completed(RechargeInfo model, out int result);

        IEnumerable<RechargeInfo> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 10, object param = null);
    }
}