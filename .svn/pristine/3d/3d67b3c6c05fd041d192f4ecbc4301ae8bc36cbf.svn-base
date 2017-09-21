using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NovelPromotionChannelRepo : Repository<NovelPromotionChannel>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelPromotionChannelRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public NovelPromotionChannel Detail(int id, int status)
        {
            string sql = " select Id, Name, Description, NovelId, ChapterCode, ChannelId, ChannelName, SiteId, SiteName, Hits, AddTime, Status, ClassId, WapHost from [Users].[NovelPromotionChannel] where id = @id and status = @status ";
            return DbManage.Query<NovelPromotionChannel>(sql, new { id, status }).FirstOrDefault();
        }
    }
}