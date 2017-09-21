using Component.Base;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Component.Controllers.Novel
{
    public class ChapterPagerController : ChapterRedirectController
    {
        #region

        protected int ChapterCodeRangeTimeout = 0;
        private string MinMaxChapterCode = string.Empty;
        private int MinChapterCode = 0;
        private int MaxChapterCode = 0;

        protected void InitializeChapterPager()
        {
            MinMaxChapterCode = UrlParameterHelper.GetParams("c");

            if (!string.IsNullOrEmpty(MinMaxChapterCode) && ChapterContext.VerifyRangeToken(MinMaxChapterCode, out MinChapterCode, out MaxChapterCode, ChapterCodeRangeTimeout) && MinChapterCode >= 0 && MaxChapterCode >= MinChapterCode)
            {
                ResetChapterCode(ChapterDirection);
            }
            else
            {
                SetChapterCodeRange();
            }
        }

        #endregion

        #region

        /// <summary>
        /// 获取分页地址
        /// 判断是否有上一章和下一章
        /// </summary>
        /// <param name="url">url为preview.aspx、play.aspx或detail.aspx</param>
        /// <param name="chapterCode"></param>
        /// <param name="isPreChapterCode"></param>
        /// <param name="isNextChapterCode"></param>
        /// <returns></returns>
        protected virtual string GetChapterPager(string url, int chapterCode, out bool isPreChapterCode, out bool isNextChapterCode)
        {
            isPreChapterCode = chapterCode > MinChapterCode;
            isNextChapterCode = chapterCode < MaxChapterCode;

            return string.Format("{0}?c={1}", url, MinMaxChapterCode);
        }

        #endregion

        #region

        protected virtual void SetChapterCodeRange()
        {
        }

        protected void SetChapterCode(int minChapterCode, int maxChapterCode)
        {
            MinMaxChapterCode = ChapterContext.GetRangeToken(minChapterCode, maxChapterCode);
            if (!string.IsNullOrEmpty(MinMaxChapterCode))
            {
                MinChapterCode = minChapterCode;
                MaxChapterCode = maxChapterCode;

                ResetChapterCode(ChapterDirection);
            }
        }

        private void ResetChapterCode(Constants.Novel.ChapterDirection chapterDirection)
        {
            if (MaxChapterCode >= MinChapterCode && MinChapterCode >= 0)
            {
                int min = MinChapterCode;
                int max = MaxChapterCode;

                switch (chapterDirection)
                {
                    case Constants.Novel.ChapterDirection.pre:
                        min += 1;
                        max += 1;
                        break;

                    case Constants.Novel.ChapterDirection.next:
                        min = ((min > 0) ? (min - 1) : 0);
                        max = ((max > 0) ? (max - 1) : 0);
                        break;
                }

                ChapterCode = ChapterCode < min ? min : ChapterCode;
                ChapterCode = ChapterCode > max ? max : ChapterCode;
            }
        }

        #endregion

        #region 阅读界面显示相关（白天/黑夜模式，用户设置字体）  章节阅读，章节订购，全本订购

        /// <summary>
        /// 阅读界面显示相关（白天/黑夜模式，用户设置字体）  章节阅读，章节订购，全本订购
        /// </summary>
        public void SetReadMode()
        {
            string readMode = CookieHelper.Get("readMode");
            ViewBag.ReadMode = readMode.ToInt() == 0 ? 0 : 1;//0白天模式  1黑夜模式

            string readFontSize = CookieHelper.Get("readFontSize");//字体大小
            ViewBag.FontSize = string.IsNullOrEmpty(readFontSize) ? "7" : readFontSize;
        }

        #endregion
    }
}