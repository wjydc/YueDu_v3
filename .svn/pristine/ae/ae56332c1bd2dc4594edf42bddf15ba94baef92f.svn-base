using Component.Base;
using Component.Controllers.User;
using Model.Common;
using System;
using System.Collections.Generic;
using Utility;

namespace Component.Controllers.Novel
{
    public class ChapterRedirectController : UserInfoController
    {
        protected int NovelId = 0;
        protected int ChapterCode = 0;
        protected Constants.Novel.ChapterDirection ChapterDirection = Constants.Novel.ChapterDirection.none;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            NovelId = StringHelper.ToInt(UrlParameterHelper.GetParams("novelId"));

            if (!VerifyRedirectToken(StringHelper.ToString(NovelId), out ChapterCode, out ChapterDirection) || NovelId <= 0 || ChapterCode < 0)
            {
                ChapterCode = 0;
                ChapterDirection = Constants.Novel.ChapterDirection.none;
            }
        }

        #region

        protected bool VerifyRedirectToken(string novelId, out int code, out Constants.Novel.ChapterDirection direction)
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

                return ChapterContext.VerifyRedirectToken(token, novelId, timeStamp, random, out code, out direction);
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

        #region

        protected static int GetFee(long wordSize, decimal avgPrice = 5)
        {
            return GetFee((decimal)wordSize, avgPrice);
        }

        protected static int GetFee(decimal wordSize, decimal avgPrice = 5)
        {
            if (wordSize > 0)
            {
                try
                {
                    decimal d = wordSize * avgPrice / (decimal)1000;

                    return (int)Math.Round(d, MidpointRounding.AwayFromZero);
                }
                catch { }
            }

            return 0;
        }

        #endregion

        #region

        protected int GetChapterWordSizeFee(int value)
        {
            int result = value > 0 ? value : SiteSection.Novel.ChapterWordSizeFee;
            result = result > 0 ? result : 5;
            return result;
        }

        #endregion
    }
}