using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Model.Config
{
    public class SiteSection : ISiteSection
    {
        public IHtml Html { get; set; }

        public IService Service { get; set; }

        public IAuth Auth { get; set; }

        public IPay Pay { get; set; }

        public ICP CP { get; set; }

        public IClass Class { get; set; }

        public INovel Novel { get; set; }

        public IAudio Audio { get; set; }

        public IChapter Chapter { get; set; }

        public IComment Comment { get; set; }

        public IDownload Download { get; set; }

        public IFilter Filter { get; set; }

        public SiteSection()
        {
            Html = new Html();
            Service = new Service();
            Auth = new Auth();
            Pay = new Pay();
            CP = new CP();
            Class = new Class();
            Novel = new Novel();
            Audio = new Audio();
            Chapter = new Chapter();
            Comment = new Comment();
            Download = new Download();
            Filter = new Filter();
            Service.WorkHour = "周一到周五 9:00-18:00";
            Html.WeChatQrCode = "/content/img/weixin.jpg";
        }
    }

    public class Html : IHtml
    {
        public string SiteName { get; set; }

        public string SiteTitle { get; set; }

        public string FeeName { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string WeChatQrCode { get; set; }
    }

    public class Service : IService
    {
        public string WorkHour { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string QQ { get; set; }

        public string WeChat { get; set; }

        public string WeChatPubId { get; set; }
    }

    public class Auth : IAuth
    {
        public bool IsShowQQ { get; set; }

        public bool IsShowWeibo { get; set; }

        public bool IsShowWeChat { get; set; }

        public bool IsAutoLoginInMicroMessenger { get; set; }
    }

    public class Pay : IPay
    {
        public bool IsShowFailTip { get; set; }

        public string WxPayResultFailUrl { get; set; }
    }

    public class CP : ICP
    {
        public bool IsShowAllNovel { get; set; }

        public bool IsShowAllAudio { get; set; }

        public bool IsShowAllCartoon { get; set; }
    }

    public class Class : IClass
    {
        public bool IsShowMale { get; set; }

        public bool IsShowFemale { get; set; }

        public bool IsShowAudio { get; set; }

        public bool IsShowCartoon { get; set; }
    }

    public class Novel : INovel
    {
        public int ChapterWordSizeFee { get; set; }
    }

    public class Audio : IAudio
    {
        public int AllPackageFee { get; set; }
    }

    public class Chapter : IChapter
    {
        public bool IsRedirectWeChat { get; set; }
    }

    public class Comment : IComment
    {
        public bool IsOpen { get; set; }

        public bool IsReverseSort { get; set; }
    }

    public class Download : IDownload
    {
        public bool IsShowApp { get; set; }
    }

    public class Filter : IFilter
    {
        public int BookCPID { get; set; }
    }
}