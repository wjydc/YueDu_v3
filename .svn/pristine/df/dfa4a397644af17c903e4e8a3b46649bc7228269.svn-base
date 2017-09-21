using Component.Base;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Controllers.Novel
{
    public class DetailController : ChapterRedirectController
    {
        #region 是否订购全本

        /// <summary>
        /// 是否订购全本
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        protected bool IsOrder(IOrderService service, string userName = "")
        {
            userName = string.IsNullOrEmpty(userName) ? currentUser.UserName : userName;
            if (string.IsNullOrEmpty(userName) || NovelId <= 0) return false;

            ChapterOrderInfo model = new ChapterOrderInfo();
            model.UserName = userName;
            model.NovelId = NovelId;

            return (service.IsOrderNovel(model) == (int)ErrorMessage.成功);
        }

        #endregion 是否订购全本
    }
}