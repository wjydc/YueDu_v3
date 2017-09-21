using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class AuthSection : IAuthSection
    {
        public IQQ QQ { get; set; }

        public IWeibo Weibo { get; set; }

        public IWeChat WeChat { get; set; }

        public AuthSection()
        {
            QQ = new QQ();
            Weibo = new Weibo();
            WeChat = new WeChat();
        }
    }

    public class QQ : IQQ
    {
        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string CallbackUrl { get; set; }
    }

    public class Weibo : IWeibo
    {
        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string CallbackUrl { get; set; }
    }

    public class WeChat : IWeChat
    {
        public string QrCodeAppId { get; set; }

        public string QrCodeAppKey { get; set; }

        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string CallbackUrl { get; set; }
    }
}