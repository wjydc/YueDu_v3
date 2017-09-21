using Component.Base;
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
    public class ChapterPreviewController : ChapterPagerController
    {
        #region

        /// <summary>
        /// 获取分页地址
        /// 判断是否有上一章和下一章
        /// </summary>
        /// <param name="url">url为preview.aspx、play.aspx或detail.aspx</param>
        /// <param name="chapterCode"></param>
        /// <param name="replyText"></param>
        /// <param name="isPreChapterCode"></param>
        /// <param name="isNextChapterCode"></param>
        /// <returns></returns>
        protected string GetChapterPager(string url, int chapterCode, out string replyText, out bool isPreChapterCode, out bool isNextChapterCode)
        {
            url = GetChapterPager(url, chapterCode, out isPreChapterCode, out isNextChapterCode);

            return GetReplyTextUrl(url, out replyText);
        }

        private string GetReplyTextUrl(string url, out string replyText)
        {
            replyText = "";
            string rp = UrlParameterHelper.GetParams("rp");

            if (!string.IsNullOrEmpty(rp))
            {
                ChapterContext.VerifyReplyText(rp, out replyText);
                IDictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("rp", rp);
                url = StringHelper.SpliceUrl(url, dict);
            }

            return url;
        }

        #endregion

        protected override void SetChapterCodeRange()
        {
            string MinMaxChapterCode = UrlParameterHelper.GetParams("cc");
            if (!string.IsNullOrEmpty(MinMaxChapterCode))
            {
                int min = 0;
                int max = 0;
                string[] chapterCode = MinMaxChapterCode.Split('_');
                if (chapterCode != null && chapterCode.Length == 2
                    && int.TryParse(chapterCode[0], out min) && min >= 0
                    && int.TryParse(chapterCode[1], out max) && max >= 0)
                {
                    SetChapterCode(min, max);
                }
            }
        }

        #region 章节是否可读

        protected bool IsRead(IOrderService service, string userName = "")
        {
            if (NovelId <= 0 || ChapterCode < 0) return false;

            ChapterOrderInfo model = new ChapterOrderInfo();
            model.UserName = string.IsNullOrEmpty(userName) ? currentUser.UserName : userName;
            model.NovelId = NovelId;
            model.ChapterCode = ChapterCode;

            return (service.IsReadChapter(model) == (int)ErrorMessage.成功);
        }

        #endregion
    }
}