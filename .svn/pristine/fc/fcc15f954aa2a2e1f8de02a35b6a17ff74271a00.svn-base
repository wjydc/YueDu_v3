using Model;
using Model.Common;
using Service;
using System;
using System.Collections.Generic;
using Utility;
using ViewModel;

namespace Component.Base
{
    public class ChapterContext
    {
        #region

        #region

        private const string RedirectKey = "readchapter_key";
        private static Random rd = new Random();

        public static string CreateRedirectToken(int id, int code, Constants.Novel.ChapterDirection direction, out int timeStamp, out int random)
        {
            timeStamp = 0;
            random = 0;

            if (id <= 0 || code < 0) return "";

            timeStamp = StringHelper.ConvertTimeStamp(DateTime.Now);
            random = rd.Next(1000, 10000);
            string txt = string.Concat(code, "_", StringHelper.ToString(direction), "_", StringHelper.ToString(id), "_", string.Concat(random, timeStamp));
            string result = SecurityHelper.EncryptBase64XorBase64Url(txt, RedirectKey);
            if (!string.IsNullOrEmpty(result))
            {
                result = UrlParameterHelper.UrlEncode(result);
            }

            return result;
        }

        public static bool VerifyRedirectToken(string token, string id, string timeStamp, string random, out int code, out Constants.Novel.ChapterDirection direction, int timeout = 0)
        {
            code = 0;
            direction = Constants.Novel.ChapterDirection.none;

            if (string.IsNullOrEmpty(token) ||
                string.IsNullOrEmpty(id) ||
                string.IsNullOrEmpty(timeStamp) ||
                string.IsNullOrEmpty(random))
            {
                return false;
            }

            bool flag = false;

            try
            {
                token = UrlParameterHelper.UrlDecode(token);
                string txt = SecurityHelper.DecryptBase64XorBase64Url(token, RedirectKey);
                if (!string.IsNullOrEmpty(txt))
                {
                    string[] list = txt.Split('_');
                    if (!StringHelper.IsNullOrEmpty(list))
                    {
                        if (timeout == 0 || ((DateTime.Now - StringHelper.ConvertDateTime(timeStamp)).TotalMinutes < timeout))
                        {
                            code = StringHelper.ToInt(list[0]);
                            if (list.Length <= 3)
                            {
                                flag = (string.Compare(id, list[1], true) == 0 &&
                                    string.Compare(string.Concat(random, timeStamp), list[2], true) == 0);
                            }
                            else
                            {
                                flag = (EnumHelper.TryParsebyName<Constants.Novel.ChapterDirection>(list[1], out direction) &&
                                    string.Compare(id, list[2], true) == 0 &&
                                    string.Compare(string.Concat(random, timeStamp), list[3], true) == 0);
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                //if (!flag)
                //{
                //    code = 0;
                //    direction = Constants.ChapterDirection.none;
                //    flag = true;
                //}
            }

            return flag;
        }

        #endregion

        #region

        public static bool IsToken = true;

        public static string GetUrl(string url, int id, int code = 0, Constants.Novel.ChapterDirection direction = Constants.Novel.ChapterDirection.none, string channelId = "")
        {
            if (string.IsNullOrEmpty(url) || id <= 0 || code < 0)
            {
                return "";
            }

            if (!string.IsNullOrEmpty(channelId))
            {
                url = url.GetChannelRouteUrl(channelId);
            }

            IDictionary<string, object> dict = new Dictionary<string, object>();

            if (IsToken)
            {
                int timeStamp = 0;
                int random = 0;

                string token = CreateRedirectToken(id, code, direction, out timeStamp, out random);
                if (!string.IsNullOrEmpty(token))
                {
                    dict.Add("novelId", id);
                    dict.Add("t", token);
                    dict.Add("s", timeStamp);
                    dict.Add("r", random);
                }
            }
            else
            {
                dict.Add("novelId", id);
                dict.Add("chapterCode", code);
                dict.Add("direction", StringHelper.ToString(direction));
            }

            return StringHelper.SpliceUrl(url, dict);
        }

        #region

        public static bool VerifyRedirect(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, out string hideUrl, bool IsRedirectWechat = false)
        {
            hideUrl = "";

            bool flag = false;

            if (//StringHelper.GetUserAgent().ToLower().Contains("micromessenger") &&
                novelId > 0 &&
                IsRedirectWechat)
            {
                long timeout = 86400;   //单位：秒，24小时
                string cookieName = string.Concat("cdwh_", novelId);
                string cookieValue = CookieHelper.Get(cookieName);
                if (string.IsNullOrEmpty(cookieValue) || (DateTime.Now - StringHelper.ToDateTime(cookieValue)).TotalSeconds > timeout)
                {
                    int curChapterCode = chapterCode;
                    if (direction == Constants.Novel.ChapterDirection.next)
                    {
                        curChapterCode += 1;
                    }
                    else if (direction == Constants.Novel.ChapterDirection.pre)
                    {
                        curChapterCode -= 1;
                    }

                    IChapterRedirectService service = DataContext.ResolveService<IChapterRedirectService>();
                    ChapterRedirect model = service.Get(novelId);
                    if (flag = (model != null && model.Id > 0 && model.ChapterCode <= curChapterCode && !string.IsNullOrEmpty(model.HideChapterUrl)))
                    {
                        CookieHelper.Set(cookieName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.AddSeconds(timeout));
                        hideUrl = model.HideChapterUrl;
                    }
                }
            }

            return flag;
        }

        #endregion

        #endregion

        #endregion

        #region

        private const string RangeKey = "code";

        public static string GetRangeToken(int minChapterCode, int maxChapterCode)
        {
            if (minChapterCode < 0 || minChapterCode > maxChapterCode) return "";

            string result = string.Empty;

            try
            {
                int timeStamp = StringHelper.ConvertTimeStamp(DateTime.Now);
                string txt = string.Concat(minChapterCode, "_", maxChapterCode, "@", timeStamp);
                result = SecurityHelper.EncryptBase64XorBase64Url(txt, RangeKey);
                if (!string.IsNullOrEmpty(result))
                {
                    result = UrlParameterHelper.UrlEncode(result);
                }
            }
            catch { }

            return result;
        }

        public static bool VerifyRangeToken(string value, out int minChapterCode, out int maxChapterCode, int timeout = 0)
        {
            minChapterCode = 0;
            maxChapterCode = 0;

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            bool flag = false;

            try
            {
                value = UrlParameterHelper.UrlDecode(value);
                string txt = SecurityHelper.DecryptBase64XorBase64Url(value, RangeKey);
                if (!string.IsNullOrEmpty(txt))
                {
                    string[] list = txt.Split('@');
                    if (!StringHelper.IsNullOrEmpty(list) && list.Length == 2)
                    {
                        string timeStamp = list[1];
                        if (!string.IsNullOrEmpty(timeStamp))
                        {
                            if (timeout == 0 || ((DateTime.Now - StringHelper.ConvertDateTime(timeStamp)).TotalMinutes < timeout))
                            {
                                if (!string.IsNullOrEmpty(list[0]))
                                {
                                    string[] chapterCodeList = list[0].Split('_');
                                    if (!StringHelper.IsNullOrEmpty(chapterCodeList))
                                    {
                                        minChapterCode = StringHelper.ToInt(chapterCodeList[0]);
                                        maxChapterCode = StringHelper.ToInt(chapterCodeList[1]);
                                        flag = (minChapterCode <= maxChapterCode && minChapterCode >= 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
            }

            return flag;
        }

        #endregion

        #region

        private const string ReplyTextKey = "rptext";

        public static string GetReplyText(string replyText)
        {
            if (string.IsNullOrEmpty(replyText)) return "";

            string result = string.Empty;

            try
            {
                result = SecurityHelper.EncryptBase64XorBase64Url(replyText, ReplyTextKey);
                if (!string.IsNullOrEmpty(result))
                {
                    result = UrlParameterHelper.UrlEncode(result);
                }
            }
            catch { }

            return result;
        }

        public static bool VerifyReplyText(string value, out string replyText)
        {
            replyText = "";

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            bool flag = false;

            try
            {
                value = UrlParameterHelper.UrlDecode(value);
                replyText = SecurityHelper.DecryptBase64XorBase64Url(value, ReplyTextKey);
                flag = !string.IsNullOrEmpty(replyText);
            }
            catch { }
            finally
            {
            }

            return flag;
        }

        #endregion
    }
}