using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Component.Base
{
    public class ChapterPreviewManage : ChapterDetailManage
    {
        public ChapterPreviewManage(Service.IChapterService service, bool isCustomRange = true)
            : base(service, isCustomRange)
        {
        }

        #region

        /// <summary>
        /// 获取分页地址
        /// 判断是否有上一章和下一章
        /// </summary>
        /// <param name="url">url为/preview</param>
        /// <param name="replyText"></param>
        /// <param name="isPreChapterCode"></param>
        /// <param name="isNextChapterCode"></param>
        /// <returns></returns>
        public string Get(string url, out string replyText, out bool isPreChapterCode, out bool isNextChapterCode)
        {
            url = Get(url, out isPreChapterCode, out isNextChapterCode);

            return GetReplyTextUrl(url, out replyText);
        }

        private string GetReplyTextUrl(string url, out string replyText)
        {
            replyText = "";

            string rp = UrlParameterHelper.GetParams("rp");
            if (!string.IsNullOrEmpty(rp) && ChapterContext.VerifyReplyText(rp, out replyText))
            {
                url = StringHelper.SpliceUrl(url, "rp", rp);
            }

            return url;
        }

        #endregion
    }
}