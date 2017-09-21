using Model;
using Repository;
using Service.Base;
using Service.Core;
using System.Collections.Generic;
using System.Linq;
using ViewModel;

namespace Service
{
    public class CommentService : BaseService, ICommentService
    {
        public IEnumerable<CommentView> GetCommentPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param)
        {
            IEnumerable<CommentView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.NovelCommentRepo(conn);
                list = repo.GetPagerList(columns, table, where, orderBy, out rowCount, pageIndex, pageSize, param);
            }

            return list;
        }

        public IEnumerable<BookCommentReplyView> GetCommentReplyPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param)
        {
            IEnumerable<BookCommentReplyView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.NovelCommentReplyRepo(conn);
                list = repo.GetPagerList(columns, table, where, orderBy, out rowCount, pageIndex, pageSize, param);
            }

            return list;
        }

        public int Send(CommentView commentInfo)
        {
            if (commentInfo == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new NovelCommentRepo(conn);
                return repo.Send(commentInfo);
            }
        }

        public int Reply(NovelCommentReply reply)
        {
            if (reply == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new NovelCommentReplyRepo(conn);
                return repo.Reply(reply);
            }
        }
    }
}