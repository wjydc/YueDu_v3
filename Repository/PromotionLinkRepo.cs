using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DapperExtension.Core;
using System.Data;

namespace Repository
{
    public class PromotionLinkRepo : Repository<PromotionLink>
    {
        protected IDbManage DbManage { get; private set; }

        public PromotionLinkRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public PromotionLink Detail(int id)
        {
            string sql = " select Id, ChannelLogName, ChannelCompanyId, ChannelCompanyName, ChannelCode, ChannelName, PromotionCode, Hits, RegUserCount, RechargeCount, TotalFee, Cost, ReturnRate, NovelId, StartChapterCode, EndChapterCode, CopywriterStyle, Platform, PlatformName, FansCount, AddTime, Status, FuncType, FollowChapter, ReplyKeywords from [Market].[PromotionLink] with (nolock) where id = @id ";
            return DbManage.Query<PromotionLink>(sql, new { id }).FirstOrDefault();
        }

        public int Update(string column, int id)
        {
            string sql = string.Format(" update Market.PromotionLink set UpdateTime = getdate(), {0} where id = @id", column);

            return DbManage.Execute(sql, new { id }, CommandType.Text);
        }

        public PromotionLink Detail(string where, object param = null)
        {
            string sql = string.Format("select top 1 Id, ChannelCompanyId, ChannelCode, PromotionCode from Market.PromotionLink where 1=1 {0}", where);
            return DbManage.Query<PromotionLink>(sql, param, CommandType.Text).FirstOrDefault();
        }

    }
}