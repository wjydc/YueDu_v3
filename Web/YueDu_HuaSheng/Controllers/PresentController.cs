using Component.Controllers.User;
using Component.Filter;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    public class PresentController : UserInfoController
    {
        private IPresentService _presentService;
        private ICommentService _commentService;
        private IUsersService _usersService;

        public PresentController(IPresentService presentService, ICommentService commentService, IUsersService usersService)
        {
            _presentService = presentService;
            _commentService = commentService;
            _usersService = usersService;
        }

        // 打赏
        [ChildActionOnly]
        public ActionResult List(int novelId, int rewardFee)
        {
            //打赏排行
            var rankList = GetRankList(novelId);
            //用户排行
            string userIcon = null;
            string userRankInfo = null;
            string userName = currentUser.UserName;
            if (string.IsNullOrEmpty(userName))
            {
                userRankInfo = "未登录";
            }
            else
            {
                int userFee = _presentService.GetUserFeeByNovel(userName, novelId);
                userRankInfo = userFee == 0 ? "未上榜" : (userFee > 10000 ? (userFee / 10000.00).ToString() + "万" : userFee.ToString()) + SiteSection.Html.FeeName;
                var userInfo = _usersService.GetDetail(currentUser.UserName);
                userIcon = userInfo == null ? null : userInfo.Icon;
            }
            //最新打赏
            var recentPresentList = GetRecentPresentList(novelId);
            //礼物列表
            int novelPropsRowCount;
            var novelPropsList = _presentService.GetNovelPropsPagerList(" and Status = 1", " order by Fee desc,AddTime desc", 1, 10, out novelPropsRowCount, null);

            var model = new PresentListView
            {
                NovelId = novelId,
                RewardFee = rewardFee,
                UserIcon = userIcon,
                UserRankInfo = userRankInfo,
                RankList = new SimpleResponse<IEnumerable<PresentView>>(!rankList.IsNullOrEmpty(), rankList),
                RecentPresentList = new SimpleResponse<IEnumerable<PresentView>>(!recentPresentList.IsNullOrEmpty(), recentPresentList),
                NovelPropsList = new SimpleResponse<IEnumerable<NovelPropsView>>(!novelPropsList.IsNullOrEmpty(), novelPropsList)
            };

            return PartialView(model);
        }

        [HttpPost]
        [Authorization("您尚未登录,请登录后重新操作", ErrorMessage.用户名为空)]
        public ActionResult Add(PresentView present)
        {
            int code = (int)ErrorMessage.失败;
            ComplexResponse<string> result;
            if (present.PropsId > 0 && present.PropsCount > 0)
            {
                //添加操作
                PresentView presentInfo = new PresentView();
                presentInfo = GetLogInfo(presentInfo) as PresentView;
                presentInfo.UserId = currentUser.UserId;
                presentInfo.UserName = currentUser.UserName;
                presentInfo.NovelId = present.NovelId;
                presentInfo.BindFee = 0;
                presentInfo.Fee = 0;
                presentInfo.FeeType = 0;
                presentInfo.PropsCount = present.PropsCount;
                presentInfo.PropsId = present.PropsId;

                int addResult = _presentService.Add(presentInfo);
                if (addResult == (int)ErrorMessage.成功)
                {
                    //ToDo
                    CommentView commentInfo = new CommentView();
                    commentInfo = GetClientLogInfo(commentInfo) as CommentView;
                    commentInfo.AuthorId = 0;
                    commentInfo.UserId = currentUser.UserId;
                    commentInfo.UserName = currentUser.UserName;
                    commentInfo.Message = StringHelper.HtmlEncode(present.Message);
                    commentInfo.NovelId = present.NovelId;
                    commentInfo.Status = (int)Constants.Status.yes;
                    commentInfo.Creator = "";
                    commentInfo.UpdateTime = DateTime.Now;
                    commentInfo.AddTime = DateTime.Now;
                    commentInfo.PropsId = present.PropsId;
                    commentInfo.PropsCount = present.PropsCount;
                    commentInfo.Title = "";
                    commentInfo.Description = "";

                    _commentService.Send(commentInfo);
                }

                code = addResult;
            }
            else code = (int)ErrorMessage.道具数量为0;

            result = new ComplexResponse<string>(code);

            return Json(result);
        }

        #region 辅助方法

        private IEnumerable<PresentView> GetRecentPresentList(int novelId, int userStatus = 1)
        {
            string columns = "a.NovelId, a.PropsCount,b.Icon as PropsIcon, b.Name,u.NickName as UserNickName";
            string table = " Log.NovelPropsUserConsumeLog a inner join dbo.NovelProps b on a.PropsId = b.Id inner join dbo.Users u on a.UserId = u.Id ";
            string where = userStatus == 0 ? " and b.status=1 and a.Novelid = @novelId " : " and b.status=1 and a.Novelid = @novelId and u.status = 1 ";
            string orderby = " order by a.addtime desc ,a.id desc ";
            int rowCount;
            var list = _presentService.GetUserCosumePagerList(columns, table, where, orderby, 1, 3, out rowCount, new { novelId });
            return list;
        }

        private IEnumerable<PresentView> GetRankList(int novelId, int userStatus = 1)
        {
            string columns = " u.icon as UserIcon,u.NickName as UserNickName";
            string table = "Rank.NovelUserConsume c inner join dbo.Users u on c.UserId= u.id  ";
            string where = userStatus == 0 ? " and c.Novelid = @novelId  " : " and c.Novelid = @novelId  and u.status = 1 ";
            string orderby = "order by c.fee desc,c.id desc";
            int rowCount;
            var list = _presentService.GetConsumeRankPagerList(columns, table, where, orderby, out rowCount, 1, 3, new { novelId });
            return list;
        }

        #endregion 辅助方法
    }
}