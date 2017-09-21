using Model;
using Model.Common;
using Service.Base;
using System.Collections.Generic;
using ViewModel;

namespace Service
{
    public interface IChapterService : IBaseService
    {
        IEnumerable<ChapterView> GetPagerList(string columns, string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param);

        /// <summary>
        /// 获取最新章节
        /// </summary>
        /// <param name="novelId"></param>
        /// <returns></returns>
        ChapterView GetLatestChapter(int novelId, int status=1);

        /// <summary>
        /// 获取章节信息（上一章/本章/下一章）
        /// </summary>
        /// <param name="novelId">小说Id</param>
        /// <param name="chapterCode">章节编号</param>
        /// <param name="status">状态</param>
        /// <param name="direction">向前/当前/向后</param>
        /// <param name="tChapterCode">章节编号（上一章/本章/下一章）</param>
        /// <returns></returns>
        Chapter GetChapter(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, out int tChapterCode, int status = 1);

        /// <summary>
        /// 获取章节信息（上一章/本章/下一章）
        /// </summary>
        /// <param name="novelId">小说Id</param>
        /// <param name="chapterCode">章节编号</param>
        /// <param name="direction">向前/当前/向后</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        Chapter GetChapter(int novelId, int chapterCode, Constants.Novel.ChapterDirection direction, int status = 1);

        /// <summary>
        /// 获取最小章节和最大章节
        /// </summary>
        /// <param name="novelId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ChapterCodeRange GetChapterCodeRange(int novelId, int status = 1);

        /// <summary>
        /// 获取章节阅读信息
        /// </summary>
        /// <param name="novelId"></param>
        /// <param name="chapterCode"></param>
        /// <returns></returns>
        ChapterDetail GetChapterView(int novelId, int chapterCode = 0);
    }
}