using DapperExtension.Core;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ViewModel;

namespace Repository
{
    public class ChapterRepo : Repository<Chapter>
    {
        protected IDbManage DbManage { get; private set; }

        public ChapterRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<ChapterView> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 10, object param = null)
        {
            string table = "[dbo].[Chapter] with (nolock)";
            return DbManage.GetPagerList<ChapterView>(columns, table, where, orderBy, out rowCount, pageIndex, pageSize, param);
        }

        /// <summary>
        /// 获取最新章节
        /// </summary>
        /// <param name="novelId"></param>
        /// <returns></returns>
        public ChapterView GetLatestChapter(int novelId, int status)
        {
            string sql = "select top 1 c.ChapterName,c.ChapterCode,c.OnlineTime from dbo.Chapter c with (nolock) where c.Status = @Status and c.NovelId = @NovelId and c.OnlineTime < @OnlineTime order by c.Id desc";
            return DbManage.Query<ChapterView>(sql, new { NovelId = novelId, Status = status, OnlineTime = DateTime.Now }).FirstOrDefault();
        }

        #region 获取章节信息（上一章/本章/下一章）

        /// <summary>
        /// 获取章节信息（上一章/本章/下一章）
        /// </summary>
        /// <param name="novelId">小说Id</param>
        /// <param name="chapterCode">章节编号</param>
        /// <param name="status">状态</param>
        /// <param name="direction">向前/当前/向后</param>
        /// <returns></returns>
        public Chapter GetChapter(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, int status)
        {
            if (novelId <= 0 || chapterCode < 0) return null;

            string sql = "select * from dbo.Chapter with (nolock) where NovelId = @NovelId and ChapterCode = @ChapterCode and Status = @Status and OnlineTime < @OnlineTime";

            switch (direction)
            {
                case Constants.Novel.ChapterDirection.pre:
                    chapterCode = chapterCode > 0 ? (chapterCode - 1) : 0;
                    break;

                case Constants.Novel.ChapterDirection.next:
                    chapterCode += 1;
                    break;
            }

            return DbManage.Query<Chapter>(sql, new { NovelId = novelId, ChapterCode = chapterCode, Status = status, OnlineTime = DateTime.Now }).FirstOrDefault();
        }

        /// <summary>
        /// 获取章节信息（上一章/本章/下一章）
        /// </summary>
        /// <param name="novelId">小说Id</param>
        /// <param name="chapterCode">章节编号</param>
        /// <param name="direction">向前/当前/向后</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public Chapter GetTopChapter(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, int status)
        {
            if (novelId <= 0 || chapterCode < 0 || direction == Constants.Novel.ChapterDirection.none) return null;

            string sql = "select top 1 * from dbo.Chapter with (nolock) where NovelId = @NovelId and Status = @Status and OnlineTime < @OnlineTime";

            switch (direction)
            {
                case Constants.Novel.ChapterDirection.pre:
                    sql += " and ChapterCode < @ChapterCode order by chaptercode desc";
                    break;

                case Constants.Novel.ChapterDirection.next:
                    sql += " and ChapterCode > @ChapterCode order by chaptercode asc";
                    break;
            }

            return DbManage.Query<Chapter>(sql, new { NovelId = novelId, ChapterCode = chapterCode, Status = status, OnlineTime = DateTime.Now }).FirstOrDefault();
        }

        #endregion 获取章节信息（上一章/本章/下一章）

        #region 获取第一章和最后一章

        /// <summary>
        /// 获取第一章和最后一章
        /// </summary>
        /// <param name="novelId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ChapterCodeRange GetChapterCodeRange(int novelId, int status)
        {
            string sql = "SELECT min(ChapterCode) as minChapterCode, max(ChapterCode) as maxChapterCode from dbo.Chapter with (nolock) where NovelId = @NovelId and Status = @Status and OnlineTime < @OnlineTime";

            return DbManage.Query<ChapterCodeRange>(sql, new { NovelId = novelId, Status = status, OnlineTime = DateTime.Now }).FirstOrDefault();
        }

        #endregion 获取第一章和最后一章

        /// <summary>
        /// 获取章节阅读信息
        /// </summary>
        /// <param name="novelId"></param>
        /// <param name="chapterCode"></param>
        /// <returns></returns>
        public ChapterDetail GetChapterView(int novelId, int chapterCode)
        {
            string sql = @"select n.Title as NovelTitle,n.Id as NovelId,n.Author,n.CPId,n.FeeType,n.FreeChapterCount,n.FilePath,n.ClassId,n.FreeChapterCount,n.TotalChapterCount,n.ContentType,
c.Id as ChapterId,c.ChapterName,c.ChapterCode,c.OnlineTime,c.WordSize,c.FileName,c.IsFree,c.Fee
from Novel n with (nolock) left join Chapter c with (nolock) on n.Id=c.NovelId where c.Status=1 and c.OnlineTime<=@OnlineTime and n.Id=@novelId and c.ChapterCode=@chapterCode";
            ChapterDetail view = DbManage.Query<ChapterDetail>(sql, new { novelId = novelId, chapterCode = chapterCode, OnlineTime = DateTime.Now }).FirstOrDefault();
            return view;
        }
    }
}