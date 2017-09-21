using Dapper;
using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class NovelPropsUserConsumeLogRepo : Repository<NovelPropsUserConsumeLog>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelPropsUserConsumeLogRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<PresentView> GetPagerList(string columns, string table, string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param)
        {
            return DbManage.GetPagerList<PresentView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }

        /// <summary>
        /// 获取用户打赏记录列表
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<ConsumeView> GetConsumeList(string columns, string table, string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param)
        {
            return DbManage.GetPagerList<ConsumeView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }

        public int Add(PresentView present)
        {
            var param = new DynamicParameters();

            param.Add("@NovelId", present.NovelId);
            param.Add("@ClientId", present.ClientId);
            param.Add("@UserId", present.UserId);
            param.Add("@UserName", present.UserName);
            param.Add("@Version", present.Version);
            param.Add("@UserAgent", present.UserAgent);
            param.Add("@IMEI", present.IMEI);
            param.Add("@IMSI", present.IMSI);
            param.Add("@ChannelId", present.ChannelId);
            param.Add("@SourceType", present.SourceType);

            param.Add("@Mobile", present.Mobile);
            param.Add("@NetType", present.NetType);
            param.Add("@Province", present.Province);
            param.Add("@City", present.City);
            param.Add("@ReferUrl", present.ReferUrl);
            param.Add("@IPAddress", present.IPAddress);
            param.Add("@RemoteHost", present.RemoteHost);

            param.Add("@Fee", present.Fee);
            param.Add("@BindFee", present.BindFee);
            param.Add("@FeeType", present.FeeType);
            param.Add("@PropsId", present.PropsId);
            param.Add("@PropsCount", present.PropsCount);
            param.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("Wap.Novel_Present", param, CommandType.StoredProcedure);

            return param.Get<int>("@ROut");
        }
    }
}