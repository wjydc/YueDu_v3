using Model;
using Model.Common;
using Service.Base;
using System.Collections.Generic;

namespace Service
{
    public interface ILogService : IBaseService
    {
        int CreateUserInfo(UserLogInfo model);

        int ChapterReadLog(ChapterLogInfo chapterLogInfo, NovelReadRecordInfo novelReadRecordInfo);

        int ChapterReadLogSync(ChapterLogInfo chapterLogInfo, NovelReadRecordInfo novelReadRecordInfo);

        int ChapterReadLogLocalSync(NovelReadRecordInfo novelReadRecordInfo);

        NovelReadRecordInfo GetRecentChapter(string userName);

        NovelReadRecordInfo GetRecentChapter(string userName, int novelId);

        NovelReadRecordInfo GetRecentChapterByType(string userName, int contentType);

        NovelReadRecordInfo GetRecentChapterExceptType(string userName, int exceptContentType);

        IEnumerable<NovelReadRecordInfo> GetRecentChapterList(string userName, int top = 5);

        IEnumerable<NovelReadRecordInfo> GetRecentChapterListByType(string userName, int contentType, int top = 5);

        IEnumerable<NovelReadRecordInfo> GetRecentChapterListExceptType(string userName, int exceptContentType, int top = 5);

        int NovelReadLog(NovelLogInfo model);
    }
}