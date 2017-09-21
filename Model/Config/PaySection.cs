using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class PaySection : IPaySection
    {
        public INowPay NowPay { get; set; }

        public IWxPay WxPay { get; set; }

        public IWxPay WxPay2 { get; set; }

        public IAlipay Alipay { get; set; }

        public PaySection()
        {
            NowPay = new NowPay();
            WxPay = new WxPay();
            WxPay2 = new WxPay();
            Alipay = new Alipay();
        }
    }

    public class NowPay : INowPay
    {
        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string WeChatAppId { get; set; }

        public string WeChatAppKey { get; set; }

        public string WeChatQrCodeAppId { get; set; }

        public string WeChatQrCodeAppKey { get; set; }

        public string CallbackUrl { get; set; }

        public string NewCallbackUrl { get; set; }
    }

    public class WxPay : IWxPay
    {
        public string AppId { get; set; }

        public string MchId { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string CallbackUrl { get; set; }

        public string LogLevel { get; set; }
    }

    public class Alipay : IAlipay
    {
        public string SellerEmail { get; set; }

        public string Partner { get; set; }

        public string Key { get; set; }

        public string PrivateKey { get; set; }

        public string PublicKey { get; set; }

        public string InputCharset { get; set; }

        public string SignType { get; set; }
    }
}