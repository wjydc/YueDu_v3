using DapperExtension.Core;
using Model;
using System.Collections.Generic;
using System.Data;
using ViewModel;

namespace Repository
{
    public class RecommendRepo : Repository<Recommend>
    {
        protected IDbManage DbManage { get; private set; }

        public RecommendRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        //推荐位列表
        public IEnumerable<RecommendView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status)
        {
            string columns = "r.Id, r.FuncType, r.FId, r.RecClassId, r.RecTitle, r.RecCover, r.RecCornerMark, r.RecBg, r.RecDescription, r.RecTags, r.Font ,nc.ClassName, n.id as NovelId, n.Title as NovelTitle, n.Author, n.LargeCover, n.SmallCover, n.ThumbCover, n.UpdateStatus, n.Tags, n.Hits, n.CommentCount, n.FavCount, n.recentchaptername, n.recentchapterupdatetime, n.RecentChapterCode, n.wordsize, n.shortwordsize, n.fnovelid, n.userid, n.RewardFee, n.shortdescription, c.DisplayName as RecClassName";
            string table = string.Format(" dbo.Recommend r with (nolock) inner join dbo.RecommendClass c on r.RecClassId = c.id inner join dbo.Novel n with (nolock) on r.fid = n.id and n.status = {0} inner join dbo.NovelClass nc on n.classid=nc.id  ", status);
            where += string.Format(" and getdate() > r.onlinetime and getdate() < r.offlinetime and r.status = {0}", status);
            return DbManage.GetPagerList<RecommendView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }
    }
}