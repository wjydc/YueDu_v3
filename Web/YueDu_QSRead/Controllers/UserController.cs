using Component.Base;
using Component.Controllers.User;
using Model.Common;
using Service;
using System.Collections.Generic;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    public class UserController : LoginedController
    {
        private IUsersService _usersService;
        private IPresentService _presentService;

        public UserController(IUsersService usersService, IPresentService presentService)
        {
            _usersService = usersService;
            _presentService = presentService;
        }

        public ActionResult Detail()
        {
            var userInfo = _usersService.GetDetail(currentUser.UserName);
            var model = new SimpleResponse<UsersView>(userInfo != null && userInfo.Id > 0, userInfo);

            return View(model);
        }

        public ActionResult ConsumeRecords(int pageIndex = 1, int pageSize = 10)
        {
            string columns = "a.AddTime,a.PropsCount,a.Fee,b.Name as PropsName,n.Title as NovelTitle";
            string table = @"Log.NovelPropsUserConsumeLog a
inner join dbo.NovelProps b on a.PropsId = b.Id
inner join dbo.Users u on a.UserId = u.Id
inner join dbo.Novel n on a.NovelId=n.Id";
            string where = " and a.UserId=@UserId";
            string orderBy = " order by a.AddTime desc";
            int rowCount = 0;
            IEnumerable<ConsumeView> list = _presentService.GetConsumeList(columns, table, where, orderBy, pageIndex, pageSize, out rowCount, new { UserId = currentUser.UserId });
            ViewBag.rowCount = rowCount;
            return View(new SimpleResponse<IEnumerable<ConsumeView>>(!StringHelper.IsNullOrEmpty(list), list));
        }

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //TODO:退出逻辑
            CurrentContext.ClearUser();
            string channelId = RouteChannelId;
            return Redirect(("/").GetChannelRouteUrl(channelId));
        }
    }
}