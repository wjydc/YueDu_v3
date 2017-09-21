using Model;
using Service.Base;
using System.Collections.Generic;
using ViewModel;

namespace Service
{
    public interface ICommentService : IBaseService
    {
        //书评列表
        IEnumerable<CommentView> GetCommentPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param);

        //书评回复列表
        IEnumerable<BookCommentReplyView> GetCommentReplyPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param);

        //发送评论
        int Send(CommentView commentInfo);

        //发送评论回复
        int Reply(NovelCommentReply reply);
    }
}