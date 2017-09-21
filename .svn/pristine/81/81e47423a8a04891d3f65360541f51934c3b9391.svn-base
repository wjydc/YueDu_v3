using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Component.Base
{
    public class ChapterRedirectManage
    {
        private int _novelId = 0;

        public int NovelId
        {
            get { return _novelId; }
        }

        private int _chapterCode = 0;

        public int ChapterCode
        {
            get { return _chapterCode; }
            protected set { _chapterCode = value; }
        }

        private Constants.Novel.ChapterDirection _chapterDirection = Constants.Novel.ChapterDirection.none;

        public Constants.Novel.ChapterDirection ChapterDirection
        {
            get { return _chapterDirection; }
        }

        public ChapterRedirectManage(int chapterRedirectTimeout = 0)
        {
            string nid = UrlParameterHelper.GetParams("novelId");
            if (!string.IsNullOrEmpty(nid)
                     && !VerifyRedirectToken(nid, out _chapterCode, out _chapterDirection, chapterRedirectTimeout) || _chapterCode < 0)
            {
                _chapterCode = 0;
                _chapterDirection = Constants.Novel.ChapterDirection.none;
            }

            _novelId = StringHelper.ToInt(nid);
        }

        #region

        protected bool VerifyRedirectToken(string novelId, out int code, out Constants.Novel.ChapterDirection direction, int timeout = 0)
        {
            if (ChapterContext.IsToken)
            {
                code = 0;
                direction = Constants.Novel.ChapterDirection.none;
                string token = UrlParameterHelper.GetParams("t");
                string timeStamp = UrlParameterHelper.GetParams("s");
                string random = UrlParameterHelper.GetParams("r");

                if (string.IsNullOrEmpty(token) ||
                    string.IsNullOrEmpty(timeStamp) ||
                    string.IsNullOrEmpty(random))
                {
                    return false;
                }

                return ChapterContext.VerifyRedirectToken(token, novelId, timeStamp, random, out code, out direction, timeout);
            }
            else
            {
                code = StringHelper.ToInt(UrlParameterHelper.GetParams("chapterCode"));
                if (!EnumHelper.TryParsebyName<Constants.Novel.ChapterDirection>(UrlParameterHelper.GetParams("direction"), out direction))
                {
                    direction = Constants.Novel.ChapterDirection.none;
                }
                return true;
            }
        }

        #endregion
    }
}