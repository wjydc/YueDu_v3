using Component.Base;
using Model.Config;

namespace Com.NowPay
{
    public class NowPayConfig
    {
        static NowPayConfig()
        {
            IPaySection paySection = DataContext.GetPaySection();

            if (paySection != null)
            {
                AppId = paySection.NowPay.AppId;
                AppKey = paySection.NowPay.AppKey;
                WeChatAppId = paySection.NowPay.WeChatAppId;
                WeChatAppKey = paySection.NowPay.WeChatAppKey;
                WeChatQrCodeAppId = paySection.NowPay.WeChatQrCodeAppId;
                WeChatQrCodeAppKey = paySection.NowPay.WeChatQrCodeAppKey;
                CallbackUrl = paySection.NowPay.CallbackUrl;
                NewCallbackUrl = paySection.NowPay.NewCallbackUrl;
            }
        }

        public static string AppId { get; set; }

        public static string AppKey { get; set; }

        public static string WeChatAppId { get; set; }

        public static string WeChatAppKey { get; set; }

        public static string WeChatQrCodeAppId { get; set; }

        public static string WeChatQrCodeAppKey { get; set; }

        public static string CallbackUrl { get; set; }

        public static string NewCallbackUrl { get; set; }
    }
}