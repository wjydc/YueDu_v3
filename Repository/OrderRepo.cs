using Dapper;
using DapperExtension.Core;
using Model.Common;
using System.Data;
using System.Linq;

namespace Repository
{
    public class OrderRepo
    {
        protected IDbManage DbManage { get; private set; }

        public OrderRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        #region

        /// <summary>
        /// 是否按本或按章订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int IsReadChapter(ChapterOrderInfo model)
        {
            return IsOrder(model, 3, 0);
        }

        /// <summary>
        /// 是否按本订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int IsOrderNovel(ChapterOrderInfo model)
        {
            return IsOrder(model, 2, 0);
        }

        private int IsOrder(ChapterOrderInfo model, int funcType, int clientType)
        {
            string spName = "Users.Novel_IsRead";

            var p = new DynamicParameters();
            p.Add("@FuncType", funcType);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@NovelId", model.NovelId);
            p.Add("@ChapterCode", model.ChapterCode);
            p.Add("@ClientId", model.ClientId);
            p.Add("@ClientType", clientType);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        #endregion

        #region

        /// <summary>
        /// 按章订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrderChapter(ChapterOrderInfo model)
        {
            return OrderChapter(model, 0);
        }

        private int OrderChapter(ChapterOrderInfo model, int clientType)
        {
            string spName = "Users.Chapter_Order";

            var p = new DynamicParameters();
            p.Add("@NovelId", model.NovelId);
            p.Add("@NovelName", model.NovelName);
            p.Add("@Fee", model.Fee);
            p.Add("@ChapterId", model.ChapterId);
            p.Add("@ChapterName", model.ChapterName);
            p.Add("@OrderTime", model.OrderTime);
            p.Add("@Status", model.Status);
            p.Add("@PayChannel", model.PayChannel);
            p.Add("@OrderCode", model.OrderCode);
            p.Add("@ChapterCode", model.ChapterCode);
            p.Add("@AutoBuy", model.AutoBuy);
            p.Add("@Rebate", model.Rebate);
            p.Add("@RebateFee", model.RebateFee);
            p.Add("@Balance", model.Balance);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@ClientId", model.ClientId);
            p.Add("@Version", model.Version);
            p.Add("@UserAgent", model.UserAgent);
            p.Add("@IMEI", model.IMEI);
            p.Add("@IMSI", model.IMSI);
            p.Add("@ChannelId", model.ChannelId);
            p.Add("@SourceType", model.SourceType);
            p.Add("@Mobile", model.Mobile);
            p.Add("@NetType", model.NetType);
            p.Add("@Province", model.Province);
            p.Add("@City", model.City);
            p.Add("@ReferUrl", model.ReferUrl);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@RemoteHost", model.RemoteHost);
            p.Add("@BindFee", model.BindFee);
            p.Add("@Cash", model.Cash);
            p.Add("@Integral", model.Integral);
            p.Add("@RebateExpression", model.RebateExpression);
            p.Add("@FeeId", model.FeeId);
            p.Add("@WordSize", model.WordSize);
            p.Add("@FeeType", model.FeeType);
            p.Add("@ClientType", clientType);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        #endregion

        #region

        /// <summary>
        /// 按本订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrderNovel(NovelOrderInfo model)
        {
            return OrderNovel(model, 0);
        }

        private int OrderNovel(NovelOrderInfo model, int clientType)
        {
            string spName = "Users.Novel_Order";

            var p = new DynamicParameters();
            p.Add("@NovelId", model.NovelId);
            p.Add("@NovelName", model.NovelName);
            p.Add("@Fee", model.Fee);
            p.Add("@OrderTime", model.OrderTime);
            p.Add("@Status", model.Status);
            p.Add("@PayChannel", model.PayChannel);
            p.Add("@OrderCode", model.OrderCode);
            p.Add("@Rebate", model.Rebate);
            p.Add("@RebateFee", model.RebateFee);
            p.Add("@FeeId", model.FeeId);
            p.Add("@Balance", model.Balance);
            p.Add("@BindFee", model.BindFee);
            p.Add("@Cash", model.Cash);
            p.Add("@Integral", model.Integral);
            p.Add("@RebateExpression", model.RebateExpression);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@ClientId", model.ClientId);
            p.Add("@Version", model.Version);
            p.Add("@UserAgent", model.UserAgent);
            p.Add("@IMEI", model.IMEI);
            p.Add("@IMSI", model.IMSI);
            p.Add("@ChannelId", model.ChannelId);
            p.Add("@SourceType", model.SourceType);
            p.Add("@Mobile", model.Mobile);
            p.Add("@NetType", model.NetType);
            p.Add("@Province", model.Province);
            p.Add("@City", model.City);
            p.Add("@ReferUrl", model.ReferUrl);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@RemoteHost", model.RemoteHost);
            p.Add("@WordSize", model.WordSize);
            p.Add("@FeeType", model.FeeType);
            p.Add("@ClientType", clientType);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        #endregion

        #region

        /// <summary>
        /// 包月订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrderPackage(PackageOrderInfo model)
        {
            return OrderPackage(model, 0);
        }

        private int OrderPackage(PackageOrderInfo model, int clientType)
        {
            string spName = "Users.Package_Order";

            var p = new DynamicParameters();
            p.Add("@PackageId", model.PackageId);
            p.Add("@PackageTitle", model.PackageTitle);
            p.Add("@Fee", model.Fee);
            p.Add("@OrderTime", model.OrderTime);
            p.Add("@Status", model.Status);
            p.Add("@PayChannel", model.PayChannel);
            p.Add("@OrderCode", model.OrderCode);
            p.Add("@Rebate", model.Rebate);
            p.Add("@RebateFee", model.RebateFee);
            p.Add("@FeeId", model.FeeId);
            p.Add("@Balance", model.Balance);
            p.Add("@BindFee", model.BindFee);
            p.Add("@Cash", model.Cash);
            p.Add("@Integral", model.Integral);
            p.Add("@RebateExpression", model.RebateExpression);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@ClientId", model.ClientId);
            p.Add("@Version", model.Version);
            p.Add("@UserAgent", model.UserAgent);
            p.Add("@IMEI", model.IMEI);
            p.Add("@IMSI", model.IMSI);
            p.Add("@ChannelId", model.ChannelId);
            p.Add("@SourceType", model.SourceType);
            p.Add("@Mobile", model.Mobile);
            p.Add("@NetType", model.NetType);
            p.Add("@Province", model.Province);
            p.Add("@City", model.City);
            p.Add("@ReferUrl", model.ReferUrl);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@RemoteHost", model.RemoteHost);
            p.Add("@BeginTime", model.BeginTime);
            p.Add("@EndTime", model.EndTime);
            p.Add("@CancelTime", model.CancelTime);
            p.Add("@AutoRenew", model.AutoRenew);
            p.Add("@OrderContentType", model.OrderContentType);
            p.Add("@ClientType", clientType);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        #endregion

        #region

        /// <summary>
        /// 获取包月信息
        /// </summary>
        /// <returns></returns>
        public PackageOrderInfo GetPackageOrder(string where)
        {
            if (string.IsNullOrEmpty(where)) return null;

            string sql = "SELECT TOP 1 * FROM Users.PackageOrder WHERE 1=1 " + where;

            return DbManage.Query<PackageOrderInfo>(sql).FirstOrDefault();
        }

        #endregion
    }
}