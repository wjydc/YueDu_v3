using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public interface IRecSection
    {
        IBookIndex BookIndex { get; set; }

        IBookDetail BookDetail { get; set; }

        IBookChapterList BookChapterList { get; set; }

        IBookChapterDetail BookChapterDetail { get; set; }

        ILimitFree LimitFree { get; set; }

        IRank Rank { get; set; }

        ICartoonIndex CartoonIndex { get; set; }

        ICartoonDetail CartoonDetail { get; set; }

        ICartoonChapterList CartoonChapterList { get; set; }
    }

    public interface IBookIndex
    {
        /// <summary>
        /// 小说首页-重磅推荐（男频）
        /// </summary>
        int MaleHotRec { get; set; }

        /// <summary>
        /// 小说首页-新书抢鲜（男频）
        /// </summary>
        int MaleNewRec { get; set; }

        /// <summary>
        /// 小说首页-主编推荐（男频）
        /// </summary>
        int MaleEditorRec { get; set; }

        /// <summary>
        /// 小说首页-畅销精品（男频）
        /// </summary>
        int MaleSaleRec { get; set; }

        /// <summary>
        /// 小说首页-重磅推荐（女频）
        /// </summary>
        int FemaleHotRec { get; set; }

        /// <summary>
        /// 小说首页-新书抢鲜（女频）
        /// </summary>
        int FemaleNewRec { get; set; }

        /// <summary>
        /// 小说首页-主编推荐（女频）
        /// </summary>
        int FemaleEditorRec { get; set; }

        /// <summary>
        /// 小说首页-畅销精品（女频）
        /// </summary>
        int FemaleSaleRec { get; set; }

        /// <summary>
        /// 小说首页-顶部广告（男频）
        /// </summary>
        int MaleTopAd { get; set; }

        /// <summary>
        /// 小说首页-中间广告（男频）
        /// </summary>
        int MaleMiddleAd { get; set; }

        /// <summary>
        /// 小说首页-顶部广告（女频）
        /// </summary>
        int FemaleTopAd { get; set; }

        /// <summary>
        /// 小说首页-中间广告（女频）
        /// </summary>
        int FemaleMiddleAd { get; set; }

        /// <summary>
        /// 小说首页-听书专区
        /// </summary>
        int ListenRec { get; set; }
    }

    public interface IBookDetail
    {
        /// <summary>
        /// 书箱详情-猜你喜欢
        /// </summary>
        int GuessRec { get; set; }
    }

    public interface IBookChapterList
    {
        /// <summary>
        /// 章节目录-猜你喜欢
        /// </summary>
        int GuessRec { get; set; }
    }

    public interface IBookChapterDetail
    {
        /// <summary>
        /// 阅读正文-同类推荐阅读
        /// </summary>
        int Rec { get; set; }

        /// <summary>
        /// 阅读正文-推荐阅读
        /// </summary>
        int Ad { get; set; }
    }

    public interface ILimitFree
    {
        /// <summary>
        /// 限时免费-首页推荐（男频）
        /// </summary>
        int MaleIndexRec { get; set; }

        /// <summary>
        /// 限时免费-免费新书（男频）
        /// </summary>
        int MaleNewRec { get; set; }

        /// <summary>
        /// 限时免费-热门免费（男频）
        /// </summary>
        int MaleHotRec { get; set; }

        /// <summary>
        /// 限时免费-首页推荐（女频）
        /// </summary>
        int FemaleIndexRec { get; set; }

        /// <summary>
        /// 限时免费-免费新书（女频）
        /// </summary>
        int FemaleNewRec { get; set; }

        /// <summary>
        /// 限时免费-热门免费（女频）
        /// </summary>
        int FemaleHotRec { get; set; }

        /// <summary>
        /// 限时免费-广告（男频）
        /// </summary>
        int MaleAd { get; set; }

        /// <summary>
        /// 限时免费-广告（女频）
        /// </summary>
        int FemaleAd { get; set; }
    }

    public interface IRank
    {
        /// <summary>
        /// 排行（男频）
        /// </summary>
        int MaleRec { get; set; }

        /// <summary>
        /// 排行（女频）
        /// </summary>
        int FemaleRec { get; set; }
    }

    public interface ICartoonIndex
    {
        /// <summary>
        /// 漫画首页-收藏推荐
        /// </summary>
        int FavRec { get; set; }

        /// <summary>
        /// 漫画首页-人气推荐
        /// </summary>
        int PopRec { get; set; }

        /// <summary>
        /// 漫画首页-三次元漫画
        /// </summary>
        int ThreeDRec { get; set; }

        /// <summary>
        /// 漫画首页-顶部广告
        /// </summary>
        int TopAd { get; set; }

        /// <summary>
        /// 漫画首页-中间广告
        /// </summary>
        int MiddleAd { get; set; }
    }

    public interface ICartoonDetail
    {
        /// <summary>
        /// 漫画详情-猜你喜欢
        /// </summary>
        int GuessRec { get; set; }
    }

    public interface ICartoonChapterList
    {
        /// <summary>
        /// 漫画目录-猜你喜欢
        /// </summary>
        int GuessRec { get; set; }
    }
}