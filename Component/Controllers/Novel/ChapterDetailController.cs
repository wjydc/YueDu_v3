using Component.Base;
using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Component.Controllers.Novel
{
    public class ChapterDetailController : ChapterPagerController
    {
        protected IChapterService _chapterService;

        #region 章节详情日志，小说阅读记录

        /// <summary>
        /// 章节详情日志，小说阅读记录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="novelId"></param>
        /// <param name="chapterId"></param>
        /// <param name="chapterCode"></param>
        protected void ReadLog(string userName, int novelId, int chapterId, int chapterCode)
        {
            ChapterLogInfo model = new ChapterLogInfo();
            model = GetLogInfo(model) as ChapterLogInfo;
            model.CookieId = GetCookieId();
            model.NovelId = novelId;
            model.ChapterId = chapterId;
            model.ChapterCode = chapterCode;
            model.AddTime = DateTime.Now;
            model.UserName = userName;

            NovelReadRecordInfo novelReadRecordInfo = new NovelReadRecordInfo();
            novelReadRecordInfo.Position = 0;
            novelReadRecordInfo.RecentReadTime = System.DateTime.Now;

            ILogService service = DataContext.ResolveService<ILogService>();
            service.ChapterReadLog(model, novelReadRecordInfo);
        }

        #endregion 章节详情日志，小说阅读记录

        #region 获取最大/最小章节

        protected override void SetChapterCodeRange()
        {
            ChapterCodeRange chapterCodeRange = _chapterService.GetChapterCodeRange(NovelId);
            if (!chapterCodeRange.IsNullOrEmpty<ChapterCodeRange>())
            {
                SetChapterCode(chapterCodeRange.MinChapterCode, chapterCodeRange.MaxChapterCode);
            }
        }

        #endregion 获取最大/最小章节

        #region 章节是否可读

        /// <summary>
        /// 章节是否可读
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        protected bool IsRead(IOrderService service, string userName = "")
        {
            if (NovelId <= 0 || ChapterCode < 0) return false;

            ChapterOrderInfo model = new ChapterOrderInfo();
            model.UserName = string.IsNullOrEmpty(userName) ? currentUser.UserName : userName;
            model.NovelId = NovelId;
            model.ChapterCode = ChapterCode;

            return (service.IsReadChapter(model) == (int)ErrorMessage.成功);
        }

        #endregion 章节是否可读

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