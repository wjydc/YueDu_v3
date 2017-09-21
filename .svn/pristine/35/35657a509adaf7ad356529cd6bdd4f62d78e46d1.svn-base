using Dapper;
using DapperExtension.Core;
using Model;
using System.Collections.Generic;
using System.Data;
using ViewModel;

namespace Repository
{
    public class NovelCommentRepo : Repository<NovelComment>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelCommentRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        //书评列表
        public IEnumerable<CommentView> GetPagerList(string columns, string table, string where, string orderby, out int rowCount, int pageIndex, int pageSize, object param)
        {
            var result = DbManage.GetPagerList<CommentView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
            return result;
        }

        public int Send(CommentView commentInfo)
        {
            var param = new DynamicParameters();

            param.Add("@AuthorId", commentInfo.AuthorId);
            param.Add("@NovelId", commentInfo.NovelId);
            param.Add("@Message", commentInfo.Message);
            param.Add("@GoodCount", commentInfo.GoodCount);
            param.Add("@BadCount", commentInfo.BadCount);
            param.Add("@ReplyCount", commentInfo.ReplyCount);
            param.Add("@UserId", commentInfo.UserId);
            param.Add("@UserName", commentInfo.UserName);
            param.Add("@ClientId", commentInfo.ClientId);
            param.Add("@Version", commentInfo.Version);
            param.Add("@UserAgent", commentInfo.UserAgent);
            param.Add("@IMEI", commentInfo.IMEI);
            param.Add("@IMSI", commentInfo.IMSI);
            param.Add("@ChannelId", commentInfo.ChannelId);
            param.Add("@SourceType", commentInfo.SourceType);
            param.Add("@Status", commentInfo.Status);
            param.Add("@Creator", commentInfo.Creator);
            param.Add("@UpdateTime", commentInfo.UpdateTime);
            param.Add("@AddTime", commentInfo.AddTime);
            param.Add("@PropsId", commentInfo.PropsId);
            param.Add("@PropsCount", commentInfo.PropsCount);
            param.Add("@SortId", commentInfo.SortId);
            param.Add("@UserType", commentInfo.UserType);
            param.Add("@Title", commentInfo.Title);
            param.Add("@Description", commentInfo.Description);
            param.Add("@CommentId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("[Wap].[NovelComment_Send]", param, CommandType.StoredProcedure);

            return param.Get<int>("@ROut");
        }
    }
}