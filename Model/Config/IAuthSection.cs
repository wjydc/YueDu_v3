using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public interface IAuthSection
    {
        IQQ QQ { get; set; }

        IWeibo Weibo { get; set; }

        IWeChat WeChat { get; set; }
    }

    public interface IQQ
    {
        string AppId { get; set; }

        string AppKey { get; set; }

        string CallbackUrl { get; set; }
    }

    public interface IWeibo
    {
        string AppId { get; set; }

        string AppKey { get; set; }

        string CallbackUrl { get; set; }
    }

    public interface IWeChat
    {
        string QrCodeAppId { get; set; }

        string QrCodeAppKey { get; set; }

        string AppId { get; set; }

        string AppKey { get; set; }

        string CallbackUrl { get; set; }
    }
}