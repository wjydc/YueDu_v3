using Model;
using Model.Common;
using Service.Base;
using System.Collections.Generic;
using ViewModel;

namespace Service
{
    public class BookService : BaseService, IBookService
    {
        public IEnumerable<NovelClass> GetNovelClassList(int? contentType, int status = 1)
        {
            IEnumerable<NovelClass> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.NovelClassRepo(conn);
                list = repo.GetAll(contentType, status);
            }

            return list;
        }

        public IEnumerable<NovelView> GetPagerList(string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param, string table = "[dbo].[Novel] as n ", string columns = "*")
        {
            IEnumerable<NovelView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.NovelRepo(conn);

                list = repo.GetPagerList(columns, where, orderBy, out rowCount, pageIndex, pageSize, table, param);
            }

            return list;
        }

        public Novel GetNovel(int novelId, int status = 1)
        {
            Novel novel = null;

            if (novelId > 0)
            {
                using (var conn = DbConnection(DbOperation.Read))
                {
                    var repo = new Repository.NovelRepo(conn);
                    novel = repo.GetDetail(novelId, status);
                }
            }

            return novel;
        }

        public AuthorNotice GetAuthorNotice(string columns, string where, string orderby, object param)
        {
            AuthorNotice authorNotice = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.AuthorNoticeRepo(conn);
                authorNotice = repo.GetAuthorNotice(columns, where, orderby, param);
            }

            return authorNotice;
        }

        /// <summary>
        /// 漫画详情页-来源
        /// </summary>
        public CP GetCP(string columns, string where, string orderby, object param)
        {
            CP cp = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.CPRepo(conn);
                cp = repo.Detail(columns, where, orderby, param);
            }

            return cp;
        }
    }
}