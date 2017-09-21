using Service.Base;
using System.Collections.Generic;
using ViewModel;

namespace Service
{
    public interface IPresentService : IBaseService
    {
        /// <summary>
        /// 获取用户打赏金额
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="novelId"></param>
        /// <returns></returns>
        int GetUserFeeByNovel(string userName, int novelId);

        /// <summary>
        /// 获取打赏的用户排行榜
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<PresentView> GetConsumeRankPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 3, object param = null);

        IEnumerable<ConsumeView> GetConsumeList(string columns, string table, string where, string orderBy, int pageIndex, int pageSize, out int rowCount, object param);

        /// <summary>
        /// 获取打赏的用户列表
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<PresentView> GetUserCosumePagerList(string columns, string table, string where, string orderBy, int pageIndex, int pageSize, out int rowCount, object param);

        /// <summary>
        /// 获取小说道具列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<NovelPropsView> GetNovelPropsPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param);

        int Add(PresentView present);
    }
}