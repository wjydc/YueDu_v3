using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public interface ISiteSection
    {
        IHtml Html { get; set; }

        IService Service { get; set; }

        IAuth Auth { get; set; }

        IPay Pay { get; set; }

        ICP CP { get; set; }

        IClass Class { get; set; }

        INovel Novel { get; set; }

        IAudio Audio { get; set; }

        IChapter Chapter { get; set; }

        IComment Comment { get; set; }

        IDownload Download { get; set; }

        IFilter Filter { get; set; }
    }

    public interface IHtml
    {
        string SiteName { get; set; }

        string SiteTitle { get; set; }

        string FeeName { get; set; }

        string MetaKeywords { get; set; }

        string MetaDescription { get; set; }

        string WeChatQrCode { get; set; }
    }

    public interface IService
    {
        string WorkHour { get; set; }

        string Phone { get; set; }

        string Email { get; set; }

        string QQ { get; set; }

        string WeChat { get; set; }

        string WeChatPubId { get; set; }
    }

    public interface IAuth
    {
        bool IsShowQQ { get; set; }

        bool IsShowWeibo { get; set; }

        bool IsShowWeChat { get; set; }

        bool IsAutoLoginInMicroMessenger { get; set; }
    }

    public interface IPay
    {
        bool IsShowFailTip { get; set; }

        string WxPayResultFailUrl { get; set; }
    }

    public interface ICP
    {
        bool IsShowAllNovel { get; set; }

        bool IsShowAllAudio { get; set; }

        bool IsShowAllCartoon { get; set; }
    }

    public interface IClass
    {
        bool IsShowMale { get; set; }

        bool IsShowFemale { get; set; }

        bool IsShowAudio { get; set; }

        bool IsShowCartoon { get; set; }
    }

    public interface INovel
    {
        int ChapterWordSizeFee { get; set; }
    }

    public interface IAudio
    {
        int AllPackageFee { get; set; }
    }

    public interface IChapter
    {
        bool IsRedirectWeChat { get; set; }
    }

    public interface IComment
    {
        bool IsOpen { get; set; }

        bool IsReverseSort { get; set; }
    }

    public interface IDownload
    {
        bool IsShowApp { get; set; }
    }

    public interface IFilter
    {
        int BookCPID { get; set; }
    }
}