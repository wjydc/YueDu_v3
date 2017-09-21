using Model.Common;
using Service.Base;

namespace Service
{
    public interface IOrderService : IBaseService
    {
        /// <summary>
        /// 是否按本或按章订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int IsReadChapter(ChapterOrderInfo model);

        /// <summary>
        /// 是否按本订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int IsOrderNovel(ChapterOrderInfo model);

        /// <summary>
        /// 按章订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int OrderChapter(ChapterOrderInfo model);

        /// <summary>
        /// 按本订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int OrderNovel(NovelOrderInfo model);

        /// <summary>
        /// 包月订购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int OrderPackage(PackageOrderInfo model);

        /// <summary>
        /// 获取包月信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        PackageOrderInfo GetPackageOrder(string where);
    }
}