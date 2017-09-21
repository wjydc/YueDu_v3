using Dapper;
using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace Repository
{
    public class BookmarkRepo : Repository<Bookmark>
    {
        protected IDbManage DbManage { get; private set; }

        public BookmarkRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        /// <summary>
        /// 收藏列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookmarkView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param)
        {
            string columns = "n.Id as NovelId, n.Title, n.ShortDescription,n.LargeCover,n.SmallCover,n.ThumbCover, b.Position, b.ChapterCode, b.RecentReadTime, u.nickname as authorname, n.RecentChapterName as ChapterName  ";
            string table = "Users.Bookmark b with (nolock) inner join dbo.Novel n with (nolock) on b.Novelid = n.id inner join dbo.Users u on u.id = b.userid ";

            return DbManage.GetPagerList<BookmarkView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <returns></returns>
        public int Add(Bookmark bookmark, BookmarkLogView bookmarkLog)
        {
            var p = new DynamicParameters();

            p.Add("@NovelId", bookmark.NovelId);
            p.Add("@ChapterId", bookmark.ChapterId);
            p.Add("@ChapterCode", bookmark.ChapterCode);
            p.Add("@Position", bookmark.Position);
            p.Add("@RecentReadTime", bookmark.RecentReadTime);
            p.Add("@UserId", bookmark.UserId);
            p.Add("@UserName", bookmark.UserName);
            p.Add("@ClientId", bookmark.ClientId);
            p.Add("@Version", bookmark.Version);
            p.Add("@UserAgent", bookmark.UserAgent);
            p.Add("@IMEI", bookmark.IMEI);
            p.Add("@IMSI", bookmark.IMSI);
            p.Add("@ChannelId", bookmark.ChannelId);
            p.Add("@SourceType", bookmark.SourceType);
            p.Add("@Mobile", bookmarkLog.Mobile);
            p.Add("@NetType", bookmarkLog.NetType);
            p.Add("@Province", bookmarkLog.Province);
            p.Add("@City", bookmarkLog.City);
            p.Add("@ReferUrl", bookmarkLog.ReferUrl);
            p.Add("@IPAddress", bookmarkLog.IPAddress);
            p.Add("@RemoteHost", bookmarkLog.RemoteHost);
            p.Add("@HistoryCount", bookmarkLog.HistoryCount);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("Wap.Bookmark_Add", p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        /// <summary>
        /// 检查是否已收藏
        /// </summary>
        /// <returns></returns>
        public int Exists(int NovelId, string UserName)
        {
            string strsql = "select count(1) from Users.Bookmark where NovelId = @NovelId and UserName = @UserName ";

            return DbManage.ExecuteScalar<int>(strsql, new { NovelId = NovelId, UserName = UserName }, CommandType.Text);
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <returns></returns>
        public int DeleteByNovelId(int novelId, string userName)
        {
            var p = new DynamicParameters();

            p.Add("@NovelId", novelId);
            p.Add("@UserId", 0);
            p.Add("@UserName", userName);

            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("[Wap].[Bookmark_Delete]", p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }
    }
}