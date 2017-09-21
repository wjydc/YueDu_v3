using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public class BookmarkService : BaseService, IBookmarkService
    {
        public IEnumerable<BookmarkView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.BookmarkRepo(conn);
                return repo.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, param);
            }
        }

        public int Add(Bookmark bookmark, BookmarkLogView bookmarkLog)
        {
            if (bookmark == null || bookmarkLog == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.BookmarkRepo(conn);
                return repo.Add(bookmark, bookmarkLog);
            }
        }

        public bool Exists(int NovelId, string UserName)
        {
            if (NovelId <= 0 || string.IsNullOrEmpty(UserName)) return false;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.BookmarkRepo(conn);
                return repo.Exists(NovelId, UserName) > 0;
            }
        }

        public bool Delete(IEnumerable<int> novelIds, string userName)
        {
            if (string.IsNullOrEmpty(userName)
                || novelIds == null
                || novelIds.Count<int>() <= 0)
                return false;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.BookmarkRepo(conn);
                foreach (var id in novelIds)
                {
                    repo.DeleteByNovelId(id, userName);
                }
                return true;
            }
        }
    }
}