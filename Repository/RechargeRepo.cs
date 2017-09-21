using Dapper;
using DapperExtension.Core;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RechargeRepo
    {
        protected IDbManage DbManage { get; private set; }

        public RechargeRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        #region 查询订单状态

        private const string SQL_SELECT_GETORDER = "SELECT TOP 1 a.*, b.fee as userfee FROM Users.Recharge a inner join dbo.Users b on a.Userid = b.Id WHERE a.OrderId = @OrderId and a.UserName = @UserName";

        /// <summary>
        /// 查询订单状态
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public RechargeInfo GetOrder(string orderId, string userName)
        {
            var p = new
            {
                OrderId = orderId,
                UserName = userName
            };

            return DbManage.Query<RechargeInfo>(SQL_SELECT_GETORDER, p).FirstOrDefault();
        }

        #endregion 查询订单状态

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="model">订单</param>
        /// <param name="feeConfigId">计费点ID</param>
        /// <param name="smsPayType">短信支付方式(int)Constants.PayType.sms</param>
        /// <param name="result">成功 = 1，失败 = 0...</param>
        /// <returns></returns>
        public RechargeInfo Generate(RechargeInfo model, int feeConfigId, int smsPayType, out int result)
        {
            string spName = "Wap.Recharge_Generate";

            var p = new DynamicParameters();
            p.Add("@MerchantId", model.MerchantId);
            p.Add("@FOrderId", model.FOrderId);
            p.Add("@ClientID", model.ClientId);
            p.Add("@Name", model.Name);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@NickName", model.NickName);
            p.Add("@Fee", model.Fee);
            p.Add("@RebateFee", model.RebateFee);
            p.Add("@RebateExpression", model.RebateExpression);
            p.Add("@Balance", model.Balance);
            p.Add("@Cash", model.Cash);
            p.Add("@Integral", model.Integral);
            p.Add("@PayType", model.PayType);
            p.Add("@PayMobile", model.PayMobile);
            p.Add("@SpNumber", model.SpNumber);
            p.Add("@SMSOrder", model.SMSOrder);
            p.Add("@MerchantPrivate", model.MerchantPrivate);
            p.Add("@MerchantExpand", model.MerchantExpand);
            p.Add("@Brief", model.Brief);
            p.Add("@Version", model.Version);
            p.Add("@Province", model.Province);
            p.Add("@City", model.City);
            p.Add("@ReferUrl", model.ReferUrl);
            p.Add("@UserAgent", model.UserAgent);
            p.Add("@IMEI", model.IMEI);
            p.Add("@IMSI", model.IMSI);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@RemoteHost", model.RemoteHost);
            p.Add("@Mobile", model.Mobile);
            p.Add("@NetType", model.NetType);
            p.Add("@ChannelId", model.ChannelId);
            p.Add("@SourceType", model.SourceType);
            p.Add("@Status", model.Status);
            p.Add("@CompleteTime", model.CompleteTime);
            p.Add("@AddTime", model.AddTime);
            p.Add("@FeeConfigId", feeConfigId);
            p.Add("@SMSPayType", smsPayType);
            p.Add("@PromotionCode", model.PromotionCode);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            RechargeInfo rechargeInfo = DbManage.Query<RechargeInfo>(spName, p, CommandType.StoredProcedure).FirstOrDefault();

            result = p.Get<int>("@ROut");

            return rechargeInfo;
        }

        /// <summary>
        /// 充值完成
        /// </summary>
        /// <param name="model"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public int Completed(RechargeInfo model, out int result)
        {
            string spName = "Wap.Recharge_Completed";

            var p = new DynamicParameters();
            p.Add("@OrderId", model.OrderId);
            p.Add("@FOrderId", model.FOrderId);
            p.Add("@Cash", model.Cash);
            p.Add("@PayMobile", model.PayMobile);
            p.Add("@Brief", model.Brief);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var value = DbManage.Execute(spName, p, CommandType.StoredProcedure);

            result = p.Get<int>("@ROut");

            return value;
        }

        public IEnumerable<RechargeInfo> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 10, object param = null)
        {
            return DbManage.GetPagerList<RechargeInfo>(columns, "[Users].[Recharge]", where, orderBy, out rowCount, pageIndex, pageSize, param);
        }
    }
}