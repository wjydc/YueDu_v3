using Component.Controllers.User;
using Component.Filter;
using Model;
using Model.Common;
using Service;
using System.Collections.Generic;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    public class MarkController : UserInfoController
    {
        private IBookmarkService _bookmarkService;

        public MarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [Authorization]
        public ActionResult List(int ct = 0, int pageIndex = 1, int pageSize = 10)
        {
            int rowCount = 0;
            string where;
            //if (ct == (int)Constants.Novel.ContentType.漫画)
            //    where = " and n.status = 1 and u.status = 1 and b.UserName = @userName and isnull(n.contenttype,0) = 2 ";
            //else
            //    where = " and n.status = 1 and u.status = 1 and b.UserName = @userName and (isnull(n.contenttype,0) =0 or isnull(n.contenttype,0) = 1 )";

            if (ct == (int)Constants.Novel.ContentType.小说)
                where = " and n.status = 1 and u.status = 1 and b.UserName = @userName and isnull(n.contenttype,0) = 0  ";
            else
                where = " and n.status = 1 and u.status = 1 and b.UserName = @userName and isnull(n.contenttype,0) > 0 ";

            string orderby = " order by b.UpdateTime desc, b.id desc ";

            var list = _bookmarkService.GetPagerList(where, orderby, pageIndex, pageSize, out rowCount, new { userName = currentUser.UserName, });

            var model = new SimpleResponse<IEnumerable<BookmarkView>>(!list.IsNullOrEmpty(), list);

            ViewBag.CType = ct;
            ViewBag.RowCount = rowCount;

            return View(model);
        }

        [HttpPost]
        [Authorization("抱歉,您还未登录", ErrorMessage.用户名为空)]
        public JsonResult Delete(int[] ids)
        {
            int result = (int)ErrorMessage.失败;

            if (_bookmarkService.Delete(ids, currentUser.UserName))
            {
                result = (int)ErrorMessage.成功;
            }

            return Json(new ComplexResponse<string>(result));
        }

        [HttpPost]
        [Authorization("抱歉,您还未登录", ErrorMessage.用户名为空)]
        public JsonResult Add(int novelId = 0)
        {
            string userName = currentUser.UserName;
            int result = (int)ErrorMessage.失败;

            if (string.IsNullOrEmpty(userName))
            {
                result = (int)ErrorMessage.用户名为空;
            }
            else if (_bookmarkService.Exists(novelId, userName))
            {
                result = (int)ErrorMessage.已收藏;
            }
            else
            {
                Bookmark bookmark = new Bookmark();
                bookmark = GetClientLogInfo(bookmark) as Bookmark;
                bookmark.UserName = userName;
                bookmark.NovelId = novelId;
                bookmark.ChapterId = 0;
                bookmark.ChapterCode = 0;
                bookmark.Position = 0;
                bookmark.RecentReadTime = System.DateTime.Now;

                BookmarkLogView bookmarkLogView = new BookmarkLogView();
                bookmarkLogView = GetLogInfo(bookmarkLogView) as BookmarkLogView;
                bookmarkLogView.HistoryCount = 100;

                result = _bookmarkService.Add(bookmark, bookmarkLogView);
            }

            return Json(new ComplexResponse<string>(result));
        }

        /// <summary>
        /// 是否已收藏
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Exists(int id)
        {
            string userName = currentUser.UserName;
            int result = (int)ErrorMessage.失败;

            if (_bookmarkService.Exists(id, userName))
            {
                result = (int)ErrorMessage.成功;
            }

            return Json(new ComplexResponse<string>(result));
        }
    }
}