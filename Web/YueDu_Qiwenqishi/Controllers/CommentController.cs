using Component.Base;
using Component.Controllers.User;
using Component.Filter;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    public class CommentController : UserInfoController
    {
        private IBookService _bookService;
        private ICommentService _commentService;

        public CommentController(IBookService bookService, ICommentService commentService)
        {
            _bookService = bookService;
            _commentService = commentService;
        }

        //小说评论
        public ActionResult List(int novelId, int pageIndex = 1, int pageSize = 10)
        {
            var novel = _bookService.GetNovel(novelId);
            if (novel == null || novel.Id <= 0)
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.小说不存在, channelId: RouteChannelId));

            ViewBag.NovelId = novelId;
            ViewBag.NovelTitle = novel.Title;
            string columns = "c.Id ,c.NovelId,isnull(c.ReplyCount,0) as ReplyCount,c.Message,isnull( c.PropsId,0) as PropsId, c.PropsCount,c.AddTime,c.UserType, u.NickName as UserNickName,u.Icon as UserIcon,p.Icon as PropsIcon ";
            string table = " Users.NovelComment as c join dbo.Users as u on c.UserId =u.Id left join dbo.NovelProps p on c.PropsId=p.id ";
            string where = " and c.Status = 1 and c.NovelId = @novelId  and u.Status = 1 ";
            string orderBy = string.Format(" order by isnull(c.Sortid,0) desc, c.Addtime {0}, c.Id {0} ", SiteSection.Comment.IsReverseSort ? "desc" : "asc");
  
            int rowCount;
            var commentList = _commentService.GetCommentPagerList(columns, table, where, orderBy, out  rowCount, pageIndex, pageSize, new { novelId });
            ViewBag.RowCount = rowCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.IsOpen = SiteSection.Comment.IsOpen;
            var model = new SimpleResponse<IEnumerable<CommentView>>(!commentList.IsNullOrEmpty(), commentList);
            return View(model);
        }

        //评论详情
        public ActionResult Detail(int commentId = 0, int pageIndex = 1, int pageSize = 10)
        {
            //获取评论的详情
            int RowCount;
            var commentDetail = _commentService.GetCommentPagerList("c.Id,c.Addtime, c.Message,u.Id as UserId, u.UserName, u.NickName as UserNickName, u.Icon as UserIcon", " Users.NovelComment as c join dbo.Users as u on c.UserId =u.Id ", " and c.Status = 1 and c.Id = @commentId  and u.Status = 1", " order by c.Sortid desc, c.Addtime desc, c.Id desc ", out RowCount, 1, 1, new { commentId }).FirstOrDefault();
            if (commentDetail == null || commentDetail.Id <= 0)
                return Redirect(DataContext.GetErrorUrl(ErrorMessage.评论不存在, channelId: RouteChannelId));

            //获取回复列表
            string table = " [Users].[NovelCommentReply] cr inner join dbo.Users u on cr.UserId = u.Id ";
            string where = " and cr.Status = 1 and cr.CommentId = @commentId and u.Status = 1 ";
            string orderBy = " order by cr.AddTime asc";

            int rowCount;
            var commentReplyList = _commentService.GetCommentReplyPagerList("cr.Id, cr.Message, cr.AddTime,cr.ReplyId, cr.ToNickName,u.Id as UserId,u.UserName, u.NickName as UserNickName, u.Icon as UserIcon ", table, where, orderBy, out  rowCount, pageIndex, pageSize, new { commentId });

            CommentDetailView detail = new CommentDetailView()
            {
                IsOpen = SiteSection.Comment.IsOpen,
                RowCount = rowCount,
                PageIndex = pageIndex,
                commentDetail = new SimpleResponse<CommentView>(!commentDetail.IsObjectNullOrEmpty(), commentDetail),
                replyList = new SimpleResponse<IEnumerable<BookCommentReplyView>>(!commentReplyList.IsNullOrEmpty(), commentReplyList)
            };

            return View(detail);
        }

        //回复他人评论 (登录后才能回复)
        [Authorization("您尚未登录,请登录后重新操作", ErrorMessage.用户名为空)]
        public ActionResult Reply(string message = "", int commentId = 0, int replyId = 0)
        {
            int result = (int)ErrorMessage.失败;
            bool isOpen = SiteSection.Comment.IsOpen;
            if (isOpen && commentId > 0)
            {
                NovelCommentReply reply = new NovelCommentReply();
                reply = GetClientLogInfo(reply) as NovelCommentReply;
                reply.UserId = currentUser.UserId;
                reply.UserName = currentUser.UserName;
                reply.Message = StringHelper.HtmlEncode(message);
                reply.GoodCount = 0;
                reply.BadCount = 0;
                reply.ToUserId = 0;
                reply.ToUserName = "";
                reply.ToNickName = "";
                reply.ReplyId = replyId;
                reply.ReplyPath = "0";
                reply.ReplyLevel = replyId == 0 ? 1 : 2;
                reply.Status = (int)Constants.Status.yes;
                reply.Creator = "";
                reply.CommentId = commentId;
                reply.ToReplyId = replyId;

                result = _commentService.Reply(reply);
            }

            return Json(new ComplexResponse<string>(result));
        }

        //发送评论 (登录后才能评论)
        [Authorization("您尚未登录,请登录后重新操作", ErrorMessage.用户名为空)]
        public ActionResult Send(string message, int novelId)
        {
            int result = (int)ErrorMessage.失败;
            bool isOpen = SiteSection.Comment.IsOpen;
            if (isOpen && novelId > 0)
            {
                CommentView comment = new CommentView();
                comment = GetClientLogInfo(comment) as CommentView;
                comment.AuthorId = 0;
                comment.UserId = currentUser.UserId;
                comment.UserName = currentUser.UserName;
                comment.Message = StringHelper.HtmlEncode(message);
                comment.NovelId = novelId;
                comment.Status = (int)Constants.Status.yes;
                comment.Creator = "";
                comment.UpdateTime = DateTime.Now;
                comment.AddTime = DateTime.Now;
                comment.PropsId = 0;
                comment.PropsCount = 0;
                comment.SortId = 0;
                comment.UserType = 0;
                comment.Title = "";
                comment.Description = "";

                result = _commentService.Send(comment);
            }

            return Json(new ComplexResponse<string>(result));
        }
    }
}