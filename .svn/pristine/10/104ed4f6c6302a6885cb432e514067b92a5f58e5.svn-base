using Model;
using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace Service
{
    public class ChapterService : BaseService, IChapterService
    {
        public IEnumerable<ChapterView> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param)
        {
            IEnumerable<ChapterView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ChapterRepo(conn);
                list = repo.GetPagerList(columns, where, orderBy, out rowCount, pageIndex, pageSize, param);
            }

            return list;
        }

        /// <summary>
        /// 获取最新章节
        /// </summary>
        /// <param name="novelId"></param>
        /// <returns></returns>
        public ChapterView GetLatestChapter(int novelId, int status = 1)
        {
            if (novelId <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ChapterRepo(conn);
                return repo.GetLatestChapter(novelId, status);
            }
        }

        /// <summary>
        /// 获取章节信息（上一章/本章/下一章）
        /// </summary>
        /// <param name="novelId">小说Id</param>
        /// <param name="chapterCode">章节编号</param>
        /// <param name="status">状态</param>
        /// <param name="direction">向前/当前/向后</param>
        /// <param name="tChapterCode">章节编号（上一章/本章/下一章）</param>
        /// <returns></returns>
        public Chapter GetChapter(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, out int tChapterCode, int status = 1)
        {
            tChapterCode = chapterCode;

            if (novelId <= 0 || chapterCode < 0) return null;

            Chapter model = GetChapter(novelId, chapterCode, direction, status);
            if (!model.IsNullOrEmpty<Chapter>() && model.Id > 0)
            {
                tChapterCode = model.ChapterCode;
            }

            return model;
        }

        /// <summary>
        /// 获取章节信息（上一章/本章/下一章）
        /// </summary>
        /// <param name="novelId">小说Id</param>
        /// <param name="chapterCode">章节编号</param>
        /// <param name="direction">向前/当前/向后</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public Chapter GetChapter(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, int status = 1)
        {
            if (novelId <= 0 || chapterCode < 0) return null;

            Chapter model = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                using (var tran = BeginTransaction(conn))
                {
                    var repo = new Repository.ChapterRepo(conn, tran);
                    if ((model = repo.GetChapter(novelId, chapterCode, direction, status)).IsNullOrEmpty<Chapter>() && direction != Constants.Novel.ChapterDirection.none)
                    {
                        model = repo.GetTopChapter(novelId, chapterCode, direction, status);
                    }

                    tran.Commit();
                }
            }

            return model;
        }

        public ChapterCodeRange GetChapterCodeRange(int novelId, int status = 1)
        {
            if (novelId <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ChapterRepo(conn);
                var model = repo.GetChapterCodeRange(novelId, status);
                return model;
            }
        }

        /// <summary>
        /// 获取章节阅读信息
        /// </summary>
        /// <param name="novelId"></param>
        /// <param name="chapterCode"></param>
        /// <returns></returns>
        public ChapterDetail GetChapterView(int novelId, int chapterCode = 0)
        {
            if (novelId <= 0 || chapterCode < 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.ChapterRepo(conn);
                var model = repo.GetChapterView(novelId, chapterCode);
                return model;
            }
        }
    }
}