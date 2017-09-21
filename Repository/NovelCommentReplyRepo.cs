using Dapper;
using DapperExtension.Core;
using Model;
using System.Collections.Generic;
using System.Data;
using ViewModel;

namespace Repository
{
    public class NovelCommentReplyRepo : Repository<NovelCommentReply>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelCommentReplyRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        /// 书评回复列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookCommentReplyView> GetPagerList(string columns, string table, string where, string orderby, out int rowCount, int pageIndex, int pageSize, object param)
        {
            var result = DbManage.GetPagerList<BookCommentReplyView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
            return result;
        }

        /// <summary>
        /// 评论回复
        /// </summary>
        /// <returns></returns>
        public int Reply(NovelCommentReply model)
        {
            var p = new DynamicParameters();

            p.Add("Message", model.Message);
            p.Add("GoodCount", model.GoodCount);
            p.Add("BadCount", model.BadCount);
            p.Add("ToUserId", model.ToUserId);
            p.Add("ToUserName", model.ToUserName);
            p.Add("ToNickName", model.ToNickName);
            p.Add("UserId", model.UserId);
            p.Add("UserName", model.UserName);
            p.Add("ReplyId", model.ReplyId);
            p.Add("ReplyPath", model.ReplyPath);
            p.Add("ReplyLevel", model.ReplyLevel);
            p.Add("ClientId", model.ClientId);
            p.Add("Version", model.Version);
            p.Add("UserAgent", model.UserAgent);
            p.Add("IMEI", model.IMEI);
            p.Add("IMSI", model.IMSI);
            p.Add("ChannelId", model.ChannelId);
            p.Add("SourceType", model.SourceType);
            p.Add("Status", model.Status);
            p.Add("Creator", model.Creator);
            p.Add("CommentId", model.CommentId);
            p.Add("ToReplyId", model.ToReplyId);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("Wap.NovelComment_Reply", p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }
    }
}