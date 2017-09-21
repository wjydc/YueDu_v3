using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Component.Base
{
    public class ChapterPagerManage : ChapterRedirectManage
    {
        private int _minChapterCode = 0;

        public int MinChapterCode
        {
            get { return _minChapterCode; }
        }

        private int _maxChapterCode = 0;

        public int MaxChapterCode
        {
            get { return _maxChapterCode; }
        }

        private string _chapterCodeRange = string.Empty;

        public string ChapterCodeRange
        {
            get { return _chapterCodeRange; }
        }

        public int CurChapterCode = 0;

        public ChapterPagerManage(Func<int, ChapterCodeRange> func, bool isCustomRange = false, int chapterCodeRangeTimeout = 0, int chapterRedirectTimeout = 0)
            : base(chapterRedirectTimeout)
        {
            if (NovelId <= 0) return;

            string chapterCodeRange = string.Empty;
            if (isCustomRange && !string.IsNullOrEmpty(chapterCodeRange = UrlParameterHelper.GetParams("cc")))
            {
                int min = 0;
                int max = 0;
                string[] range = chapterCodeRange.Split('_');
                if (!range.IsNullOrEmpty() && range.Length == 2
                    && int.TryParse(range[0], out min) && min >= 0
                    && int.TryParse(range[1], out max) && max >= 0)
                {
                    Set(min, max);
                }
            }
            else
            {
                _chapterCodeRange = UrlParameterHelper.GetParams("c");
                if (!string.IsNullOrEmpty(_chapterCodeRange)
                         && ChapterContext.VerifyRangeToken(_chapterCodeRange, out _minChapterCode, out _maxChapterCode, chapterCodeRangeTimeout))
                {
                    Reset();
                }
                else
                {
                    if (func != null)
                    {
                        ChapterCodeRange range = func(NovelId);
                        if (!range.IsNullOrEmpty<ChapterCodeRange>())
                        {
                            Set(range.MinChapterCode, range.MaxChapterCode);
                        }
                    }
                }
            }
        }

        #region

        public void Set(int minChapterCode, int maxChapterCode)
        {
            if (NovelId <= 0) return;

            _chapterCodeRange = ChapterContext.GetRangeToken(minChapterCode, maxChapterCode);
            if (!string.IsNullOrEmpty(_chapterCodeRange))
            {
                _minChapterCode = minChapterCode;
                _maxChapterCode = maxChapterCode;

                Reset();
            }
        }

        private void Reset()
        {
            if (NovelId <= 0) return;

            if (_maxChapterCode >= _minChapterCode && _minChapterCode >= 0)
            {
                int min = _minChapterCode;
                int max = _maxChapterCode;

                switch (ChapterDirection)
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

        #region

        /// <summary>
        /// 获取分页地址
        /// 判断是否有上一章和下一章
        /// </summary>
        /// <param name="url">url为/chapter/detail或/preview</param>
        /// <param name="isPreChapterCode"></param>
        /// <param name="isNextChapterCode"></param>
        /// <returns></returns>
        public virtual string Get(string url, out bool isPreChapterCode, out bool isNextChapterCode)
        {
            isPreChapterCode = false;
            isNextChapterCode = false;

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(_chapterCodeRange)) return "";

            isPreChapterCode = CurChapterCode > _minChapterCode;
            isNextChapterCode = CurChapterCode < _maxChapterCode;

            return StringHelper.SpliceUrl(url, "c", _chapterCodeRange);
        }

        #endregion

        #region 章节是否可读

        /// <summary>
        /// 章节是否可读
        /// </summary>
        /// <param name="func"></param>
        /// <param name="userName"></param>
        /// <param name="curNovelId"></param>
        /// <returns></returns>
        public bool IsRead(Func<ChapterOrderInfo, int> func, string userName, int curNovelId = 0)
        {
            curNovelId = (curNovelId > 0 ? curNovelId : NovelId);

            if (func == null || curNovelId <= 0 || ChapterCode < 0) return false;

            ChapterOrderInfo model = new ChapterOrderInfo();
            model.UserName = userName;
            model.NovelId = curNovelId;
            model.ChapterCode = CurChapterCode;

            return (func(model) == (int)ErrorMessage.成功);
        }

        #endregion 章节是否可读

        #region 是否订购全本

        /// <summary>
        /// 是否订购全本
        /// </summary>
        /// <param name="func"></param>
        /// <param name="userName"></param>
        /// <param name="curNovelId"></param>
        /// <returns></returns>
        protected bool IsOrder(Func<ChapterOrderInfo, int> func, string userName, int curNovelId = 0)
        {
            curNovelId = (curNovelId > 0 ? curNovelId : NovelId);

            if (func == null || string.IsNullOrEmpty(userName) || NovelId <= 0) return false;

            ChapterOrderInfo model = new ChapterOrderInfo();
            model.UserName = userName;
            model.NovelId = curNovelId;

            return (func(model) == (int)ErrorMessage.成功);
        }

        #endregion 是否订购全本
    }
}