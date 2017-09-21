using Model;
using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LogService : BaseService, ILogService
    {
        public int CreateUserInfo(UserLogInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.CreateUserInfo(model);
            }
        }

        public int ChapterReadLog(ChapterLogInfo chapterLogInfo, NovelReadRecordInfo novelReadRecordInfo)
        {
            if (chapterLogInfo == null || novelReadRecordInfo == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.ChapterReadLog(chapterLogInfo, novelReadRecordInfo);
            }
        }

        public int ChapterReadLogSync(ChapterLogInfo chapterLogInfo, NovelReadRecordInfo novelReadRecordInfo)
        {
            if (chapterLogInfo == null || novelReadRecordInfo == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.ChapterReadLogSync(chapterLogInfo, novelReadRecordInfo);
            }
        }

        public int ChapterReadLogLocalSync(NovelReadRecordInfo novelReadRecordInfo)
        {
            if (novelReadRecordInfo == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.ChapterReadLogLocalSync(novelReadRecordInfo);
            }
        }

        public NovelReadRecordInfo GetRecentChapter(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapter(userName);
            }
        }

        public NovelReadRecordInfo GetRecentChapter(string userName, int novelId)
        {
            if (string.IsNullOrEmpty(userName) || novelId <= 0) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapter(userName, novelId);
            }
        }

        public NovelReadRecordInfo GetRecentChapterByType(string userName, int contentType)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapterByType(userName, contentType);
            }
        }

        public NovelReadRecordInfo GetRecentChapterExceptType(string userName, int exceptContentType)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapterExceptType(userName, exceptContentType);
            }
        }

        public IEnumerable<NovelReadRecordInfo> GetRecentChapterList(string userName, int top = 5)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapterList(userName, top);
            }
        }

        public IEnumerable<NovelReadRecordInfo> GetRecentChapterListByType(string userName, int contentType, int top = 5)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapterListByType(userName, contentType, top);
            }
        }

        public IEnumerable<NovelReadRecordInfo> GetRecentChapterListExceptType(string userName, int exceptContentType, int top = 5)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.GetRecentChapterListExceptType(userName, exceptContentType, top);
            }
        }

        public int NovelReadLog(NovelLogInfo model)
        {
            if (model == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.LogRepo(conn);
                return repo.NovelReadLog(model);
            }
        }
    }
}