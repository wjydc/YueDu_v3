using Component.Controllers;
using Component.Controllers.User;
using Model.Common;
using Service;
using System.Collections.Generic;
using System.Web.Mvc;
using Utility;
using ViewModel;

namespace YueDu.Controllers
{
    public class RecommendController : UserInfoController
    {
        private IRecommendService _recommendService;

        public RecommendController(IRecommendService recommendService)
        {
            _recommendService = recommendService;
        }

        [ChildActionOnly]
        public ActionResult GuessLike(int classId)
        {
            string where = " and r.RecClassId = @classId ";
            string orderby = " order by r.onlinetime desc, r.sortid desc ";
            int rowCount = 0;

            var recommendList = _recommendService.GetPagerList(where, orderby, 1, 3, out rowCount, new { classId });
            var model = new SimpleResponse<IEnumerable<RecommendView>>(!recommendList.IsObjectNullOrEmpty(), recommendList);

            return PartialView(model);
        }
    }
}