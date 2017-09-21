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
using ViewModel;

namespace Repository
{
    public class NovelRepo : Repository<Novel>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public Novel GetDetail(int novelId, int status)
        {
            string sql = " select * from [dbo].[Novel] with (nolock) where Id = @novelId and Status = @status   ";
            return DbManage.Query<Novel>(sql, new { novelId, status }, CommandType.Text).FirstOrDefault();
        }

        public IEnumerable<NovelView> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 10, string table = "[dbo].[Novel] as n with (nolock) inner join [dbo].[NovelClass] nc on nc.Id = n.ClassId", object param = null)
        {
            return DbManage.GetPagerList<NovelView>(columns, table, where, orderBy, out rowCount, pageIndex, pageSize, param);
        }

        public void AddNovelLog(NovelLogInfo logInfo)
        {
            var param = new DynamicParameters();

            param.Add("@NovelId", logInfo.NovelId);
            param.Add("@CookieId", logInfo.CookieId);
            param.Add("@ClientId", logInfo.ClientId);
            param.Add("@UserId", logInfo.UserId);
            param.Add("@UserName", logInfo.UserName);
            param.Add("@Version", logInfo.Version);
            param.Add("@UserAgent", logInfo.UserAgent);
            param.Add("@IMEI", logInfo.IMEI);
            param.Add("@IMSI", logInfo.IMSI);
            param.Add("@ChannelId", logInfo.ChannelId);
            param.Add("@SourceType", logInfo.SourceType);
            param.Add("@Mobile", logInfo.Mobile);
            param.Add("@NetType", logInfo.NetType);
            param.Add("@Province", logInfo.Province);
            param.Add("@City", logInfo.City);
            param.Add("@ReferUrl", logInfo.ReferUrl);
            param.Add("@IPAddress", logInfo.IPAddress);
            param.Add("@RemoteHost", logInfo.RemoteHost);
            param.Add("@AddTime", logInfo.AddTime);

            param.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("Wap.Novel_Log", param, CommandType.StoredProcedure);
        }
    }
}