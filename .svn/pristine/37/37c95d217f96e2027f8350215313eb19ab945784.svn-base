using Model;
using Model.Common;
using Service;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;
using ViewModel;

namespace Component.Base
{
    public class RecentReadContext
    {
        #region 最近阅读记录

        /// <summary>
        /// 获取最近阅读记录
        /// </summary>
        /// <param name="cookieNameList"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NovelRecentReadView First(string[] cookieNameList, int id = 0)
        {
            if (!StringHelper.IsNullOrEmpty(cookieNameList))
            {
                NovelRecentReadView model = null, temp = null;

                foreach (string cookieName in cookieNameList)
                {
                    if (!string.IsNullOrEmpty(cookieName))
                    {
                        temp = First(cookieName, id);
                        if (temp != null && temp.Id > 0)
                        {
                            if (model == null || (model != null && model.ReadTime < temp.ReadTime))
                            {
                                model = temp;
                            }
                        }
                    }
                }

                return model;
            }

            return null;
        }

        /// <summary>
        /// 获取最近阅读记录
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NovelRecentReadView First(string cookieName, int id = 0)
        {
            if (!string.IsNullOrEmpty(cookieName))
            {
                string host = StringHelper.GetHost();

                NovelRecentReadListView readLog = Get(cookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);

                //if (readLog != null && readLog.List != null && readLog.List.Count > 0 && readLog.Id == id)
                if (readLog != null && readLog.List != null && readLog.List.Count > 0)
                {
                    return readLog.List.OrderByDescending<NovelRecentReadView, System.DateTime>(t => t.ReadTime).First<NovelRecentReadView>();
                }
            }

            return null;
        }

        public static NovelRecentReadListView Get(string cookieName, int id = 0)
        {
            string host = StringHelper.GetHost();

            NovelRecentReadListView readLog = Get(cookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);

            //if (readLog != null && readLog.List != null && readLog.List.Count > 0 && string.Compare(readLog.Host, Host, true) == 0 && readLog.Id == id)
            if (readLog != null && readLog.List != null && readLog.List.Count > 0 && string.Compare(readLog.Host, host, true) == 0)
            {
                return readLog;
            }

            return null;
        }

        /// <summary>
        /// 保存最近阅读记录
        /// </summary>
        /// <param name="novelInfo"></param>
        /// <param name="chapterTitle"></param>
        /// <param name="chapterCode"></param>
        /// <param name="routeChannelId"></param>
        /// <param name="id"></param>
        public static void Save(Novel novelInfo, string chapterTitle, int chapterCode, string routeChannelId = "", int id = 0)
        {
            NovelRecentReadView readInfo = new NovelRecentReadView();
            readInfo.Id = novelInfo.Id;
            readInfo.ChapterName = chapterTitle;
            readInfo.ChapterCode = chapterCode;
            readInfo.ContentType = novelInfo.ContentType;
            readInfo.Title = novelInfo.Title;
            readInfo.ReadTime = DateTime.Now;
            readInfo.RouteChannelId = routeChannelId;
            Save(GetCookieName(novelInfo.ContentType), readInfo, 5, id);
        }

        public static string GetCookieName(int contentType)
        {
            return contentType == 0 ? Constants.SecurityKey.NovelRecentRead_CookieName.ToString() : Constants.SecurityKey.CartoonRecentRead_CookieName.ToString();
        }

        public static void Save(string cookieName, NovelRecentReadView readInfo, int recordCount, int id = 0)
        {
            if (readInfo == null || readInfo.Id <= 0) return;

            string host = StringHelper.GetHost();

            NovelRecentReadListView readLog = Get(cookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);

            //if (readLog == null || string.Compare(readLog.Host, Host, true) != 0 || readLog.Id != id)
            if (readLog == null || string.Compare(readLog.Host, host, true) != 0)
            {
                readLog = new NovelRecentReadListView();
                readLog.Id = id;
                readLog.Host = host;
                readLog.List = new List<NovelRecentReadView>();
            }

            //if (readLog != null && string.Compare(readLog.Host, Host, true) == 0 && readLog.Id == id)
            if (readLog != null && string.Compare(readLog.Host, host, true) == 0)
            {
                if (readLog.List == null) readLog.List = new List<NovelRecentReadView>();

                if (readLog.List != null)
                {
                    if (readLog.List.Count > 0)
                    {
                        IEnumerable<NovelRecentReadView> list = readLog.List.Where<NovelRecentReadView>(t => t.Id != readInfo.Id);
                        if (list != null)
                        {
                            readLog.List = list.Count<NovelRecentReadView>() > 0 ? list.ToList<NovelRecentReadView>() : new List<NovelRecentReadView>();
                        }
                    }
                }

                readLog.List.Add(readInfo);

                while (readLog.List.Count > recordCount)
                {
                    readLog.List.RemoveAt(0);
                }
            }

            CookieHelper<NovelRecentReadListView>.Set(readLog, cookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV, DateTime.Now.AddMonths(1));
        }

        private static NovelRecentReadListView Get(string cookieName, string key, string IV)
        {
            NovelRecentReadListView readList = null;

            string value = "";
            if (CookieHelper.Get(cookieName, key, IV, out value))
            {
                readList = SerializeHelper.DeSerialize<NovelRecentReadListView>(value);
                if (readList == null || readList.List.IsNullOrEmpty<NovelRecentReadView>())
                {
                    NovelRecentReadLog readLog = SerializeHelper.DeSerialize<NovelRecentReadLog>(value);
                    if (readLog != null && !readLog.List.IsNullOrEmpty<NovelReadInfo>())
                    {
                        readList = readLog.ToNovelRecentReadListView();
                    }
                }
            }

            return readList;
        }

        #endregion 最近阅读记录

        #region 最近阅读记录

        /// <summary>
        /// 获取最近阅读记录
        /// </summary>
        /// <param name="cookieNameList"></param>
        /// <param name="logService"></param>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NovelRecentReadView First(string[] cookieNameList, ILogService logService, string userName, int id = 0)
        {
            if (logService != null && !string.IsNullOrEmpty(userName))
            {
                NovelReadRecordInfo recordInfo = logService.GetRecentChapter(userName);
                if (recordInfo != null && recordInfo.Id > 0)
                {
                    NovelRecentReadView readInfo = new NovelRecentReadView();
                    readInfo.Id = recordInfo.NovelId;
                    readInfo.ChapterName = recordInfo.ChapterName;
                    readInfo.ChapterCode = recordInfo.ChapterCode;
                    readInfo.ContentType = recordInfo.NovelContentType;
                    readInfo.Title = recordInfo.NovelTitle;
                    readInfo.ReadTime = recordInfo.RecentReadTime;
                    readInfo.RouteChannelId = recordInfo.RouteChannelId;
                    return readInfo;
                }
            }

            return First(cookieNameList, id);
        }

        /// <summary>
        /// 获取最近阅读记录
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NovelRecentReadView First(string cookieName, ILogService logService, string userName, int id = 0)
        {
            if (logService != null && !string.IsNullOrEmpty(userName))
            {
                NovelReadRecordInfo recordInfo = GetRecentChapter(cookieName, logService, userName);
                if (recordInfo != null && recordInfo.Id > 0)
                {
                    NovelRecentReadView readInfo = new NovelRecentReadView();
                    readInfo.Id = recordInfo.NovelId;
                    readInfo.ChapterName = recordInfo.ChapterName;
                    readInfo.ChapterCode = recordInfo.ChapterCode;
                    readInfo.ContentType = recordInfo.NovelContentType;
                    readInfo.Title = recordInfo.NovelTitle;
                    readInfo.ReadTime = recordInfo.RecentReadTime;
                    readInfo.RouteChannelId = recordInfo.RouteChannelId;
                    return readInfo;
                }
            }

            return First(cookieName, id);
        }

        public static NovelReadRecordInfo GetRecentChapter(string cookieName, ILogService logService, string userName)
        {
            NovelReadRecordInfo model = null;
            if (string.Compare(Constants.SecurityKey.NovelRecentRead_CookieName.ToString(), cookieName, true) == 0)
            {
                model = logService.GetRecentChapterByType(userName, 0);
            }
            else
            {
                model = logService.GetRecentChapterExceptType(userName, 0);
            }
            return model;
        }

        public static NovelRecentReadListView Get(string cookieName, ILogService logService, string userName, int id = 0)
        {
            if (logService != null && !string.IsNullOrEmpty(userName))
            {
                IEnumerable<NovelReadRecordInfo> recordList = GetRecentChapterList(cookieName, logService, userName);
                if (!recordList.IsNullOrEmpty<NovelReadRecordInfo>())
                {
                    NovelRecentReadListView readList = new NovelRecentReadListView();
                    readList.Id = id;
                    readList.Host = "";

                    #region

                    IList<NovelRecentReadView> list = new List<NovelRecentReadView>();
                    NovelRecentReadView readInfo = null;
                    foreach (NovelReadRecordInfo recordInfo in recordList)
                    {
                        readInfo = new NovelRecentReadView();
                        readInfo.Id = recordInfo.NovelId;
                        readInfo.ChapterName = recordInfo.ChapterName;
                        readInfo.ChapterCode = recordInfo.ChapterCode;
                        readInfo.ContentType = recordInfo.NovelContentType;
                        readInfo.Title = recordInfo.NovelTitle;
                        readInfo.ReadTime = recordInfo.RecentReadTime;
                        readInfo.RouteChannelId = recordInfo.RouteChannelId;
                        list.Add(readInfo);
                    }

                    readList.List = StringHelper.Reverse<NovelRecentReadView>(list);

                    //readList.List = StringHelper.Reverse<NovelRecentReadView>(recordList.ToList().ToNovelRecentReadViewList());

                    #endregion 最近阅读记录

                    return readList;
                }
            }

            return Get(cookieName, id);
        }

        public static IEnumerable<NovelReadRecordInfo> GetRecentChapterList(string cookieName, ILogService logService, string userName)
        {
            IEnumerable<NovelReadRecordInfo> list = null;
            if (string.Compare(Constants.SecurityKey.NovelRecentRead_CookieName.ToString(), cookieName, true) == 0)
            {
                list = logService.GetRecentChapterListByType(userName, 0);
            }
            else
            {
                list = logService.GetRecentChapterListExceptType(userName, 0);
            }
            return list;
        }

        #endregion 最近阅读记录
    }
}