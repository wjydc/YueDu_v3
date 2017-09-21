using System.Web.Optimization;

namespace YueDu
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqval").Include(
                        "~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));

            //libflexible
            bundles.Add(new ScriptBundle("~/bundles/libflexible/css").Include(
                        "~/Content/plugin/libflexible/flexible_css.debug.js"));
            bundles.Add(new ScriptBundle("~/bundles/libflexible").Include(
                        "~/Content/plugin/libflexible/flexible.debug.js", "~/Content/plugin/libflexible/makegrid.debug.js"));

            //dist-swiper
            bundles.Add(new ScriptBundle("~/bundles/dist").Include(
                        "~/Content/plugIn/dist/js/swiper.min.js"));
            //echo
            bundles.Add(new ScriptBundle("~/bundles/echo").Include(
                        "~/Content/plugIn/echo/echo.min.js"));
            //audio
            bundles.Add(new ScriptBundle("~/bundles/audio").Include(
                        "~/Content/listening/js/jqmobi.js"));
            //audio
            bundles.Add(new ScriptBundle("~/bundles/audioplay").Include(
                        "~/Content/listening/js/tsall.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //公共消息提示
            bundles.Add(new ScriptBundle("~/bundles/tips").Include(
                        "~/Content/scripts/commonTips.js"));

            //书籍阅读、章节订购、全本订购
            bundles.Add(new ScriptBundle("~/bundles/bookReading").Include(
                        "~/Content/scripts/jquery.cookie.js", "~/Content/scripts/readMode.js"));

            //搜索结果
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                        "~/Content/scripts/jquery.cookie.js", "~/Content/scripts/bookSearch.js"));

            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                        "~/Content/scripts/jquery.cookie.js"));

            //书籍阅读、章节订购、全本订购
            bundles.Add(new ScriptBundle("~/bundles/goBack").Include(
                        "~/Content/scripts/jquery.cookie.js", "~/Content/scripts/goBack.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            //common
            bundles.Add(new StyleBundle("~/Content/common").Include(
                      "~/Content/css/common.css"));

            //首页
            bundles.Add(new StyleBundle("~/Content/index").Include(
                      "~/Content/css/index.css", "~/Content/plugin/dist/css/swiper.min.css", "~/Content/css/mask.css"));

            //书籍详情
            bundles.Add(new StyleBundle("~/Content/bookdetail").Include(
                      "~/Content/css/read_detail.css", "~/Content/css/mask.css"));
            //书籍目录
            bundles.Add(new StyleBundle("~/Content/directory").Include(
                      "~/Content/css/directory.css"));

            //排行
            bundles.Add(new StyleBundle("~/Content/ranklist").Include(
                      "~/Content/css/RankingList.css"));

            //搜索
            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Content/css/login.css"));

            //免费专区
            bundles.Add(new StyleBundle("~/Content/additional").Include(
                      "~/Content/css/additional.css"));

            //书籍评论
            bundles.Add(new StyleBundle("~/Content/bookComment").Include(
                      "~/Content/css/read_detail.css", "~/Content/css/book_Comment.css"));

            //个人中心
            bundles.Add(new StyleBundle("~/Content/userCenter").Include(
                      "~/Content/css/personalcenter.css"));

            //漫画专区
            bundles.Add(new StyleBundle("~/Content/cartoonIndex").Include(
                      "~/Content/css/index.css", "~/Content/css/additional.css", "~/Content/plugIn/dist/css/swiper.min.css"));

            //漫画详情
            bundles.Add(new StyleBundle("~/Content/cartoonDetail").Include(
                      "~/Content/css/read_detail.css", "~/Content/css/additional.css"));

            //小说章节正文
            bundles.Add(new StyleBundle("~/Content/chapterDetail").Include(
                      "~/Content/css/reading.css"));

            bundles.Add(new StyleBundle("~/Content/bookOrder").Include(
                      "~/Content/css/buying.css", "~/Content/css/reading.css"));

            //消费记录
            bundles.Add(new StyleBundle("~/Content/ConsumeRecords").Include(
                      "~/Content/css/rechargecenter.css"));

            //漫画正文
            bundles.Add(new StyleBundle("~/Content/cartoonChapterDetail").Include(
                      "~/Content/css/comic.css", "~/Content/css/buying.css"));

            //微信扫码支付
            bundles.Add(new StyleBundle("~/Content/WxPay").Include(
                      "~/Content/css/wechart.css"));

            //听书正文
            bundles.Add(new StyleBundle("~/Content/Audio").Include(
                      "~/Content/listening/css/listening.css"));

            //推广
            bundles.Add(new StyleBundle("~/Content/preview").Include(
                     "~/Content/css/promote.css"));

            //充值失败
            bundles.Add(new StyleBundle("~/Content/wxIndex").Include(
                     "~/Content/wx/css/index.css"));
            //支付失败
            bundles.Add(new StyleBundle("~/Content/payError").Include(
                      "~/Content/css/common.css", "~/Content/css/payerror.css"));
        }
    }
}