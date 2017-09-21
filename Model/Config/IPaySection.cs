using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public interface IPaySection
    {
        INowPay NowPay { get; set; }

        IWxPay WxPay { get; set; }

        IWxPay WxPay2 { get; set; }

        IAlipay Alipay { get; set; }
    }

    public interface INowPay
    {
        string AppId { get; set; }

        string AppKey { get; set; }

        string WeChatAppId { get; set; }

        string WeChatAppKey { get; set; }

        string WeChatQrCodeAppId { get; set; }

        string WeChatQrCodeAppKey { get; set; }

        string CallbackUrl { get; set; }

        string NewCallbackUrl { get; set; }
    }

    public interface IWxPay
    {
        string AppId { get; set; }

        string MchId { get; set; }

        string AppKey { get; set; }

        string AppSecret { get; set; }

        string CallbackUrl { get; set; }

        string LogLevel { get; set; }
    }

    public interface IAlipay
    {
        string SellerEmail { get; set; }

        string Partner { get; set; }

        string Key { get; set; }

        string PrivateKey { get; set; }

        string PublicKey { get; set; }

        string InputCharset { get; set; }

        string SignType { get; set; }
    }
}