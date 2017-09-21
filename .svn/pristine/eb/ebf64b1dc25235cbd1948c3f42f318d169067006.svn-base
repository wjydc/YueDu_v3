using Component.Base;
using Model.Config;
using Utility;

namespace Com.WxPayAPI
{
    /**
    * 	配置账号信息
    */

    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        private static string _appId = "";

        public static string APPID
        {
            get { return _appId; }
            set { _appId = value; }
        }

        private static string _mchId = "";

        public static string MCHID
        {
            get { return _mchId; }
            set { _mchId = value; }
        }

        private static string _key = "";

        public static string KEY
        {
            get { return _key; }
            set { _key = value; }
        }

        private static string _appSecret = "";

        public static string APPSECRET
        {
            get { return _appSecret; }
            set { _appSecret = value; }
        }

        //=======【证书路径设置】=====================================
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        private static string _sslcert_path = "";

        public static string SSLCERT_PATH
        {
            get { return _sslcert_path; }
            set { _sslcert_path = value; }
        }

        private static string _sslcert_password = "";

        public static string SSLCERT_PASSWORD
        {
            get { return _sslcert_password; }
            set { _sslcert_password = value; }
        }

        //=======【支付结果通知url】=====================================
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        private static string _notify_url = "";

        public static string NOTIFY_URL
        {
            get { return _notify_url; }
            set { _notify_url = value; }
        }

        //=======【商户系统后台机器IP】=====================================
        /* 此参数可手动配置也可在程序中自动获取
        */
        private static string _ip = "";

        public static string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        private static string _proxy_url = "";

        public static string PROXY_URL
        {
            get { return _proxy_url; }
            set { _proxy_url = value; }
        }

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        private static int _report_levenl = 0;

        public static int REPORT_LEVENL
        {
            get { return _report_levenl; }
            set { _report_levenl = value; }
        }

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        private static int _log_levenl = 0;

        public static int LOG_LEVENL
        {
            get { return _log_levenl; }
            set { _log_levenl = value; }
        }

        static WxPayConfig()
        {
            Log.Info("WxConfig", "初始化参数");

            string host = StringHelper.GetHost();
            if (!string.IsNullOrEmpty(host))
            {
                IPaySection paySection = DataContext.GetPaySection();

                if (paySection != null)
                {
                    //服务器异步通知页面路径
                    string notify_url = string.Concat("http://", host, paySection.WxPay.CallbackUrl);

                    APPID = paySection.WxPay.AppId;
                    MCHID = paySection.WxPay.MchId;
                    KEY = paySection.WxPay.AppKey;
                    APPSECRET = paySection.WxPay.AppSecret;
                    SSLCERT_PATH = "";
                    SSLCERT_PASSWORD = "";
                    NOTIFY_URL = notify_url;
                    IP = "8.8.8.8";
                    PROXY_URL = "";
                    REPORT_LEVENL = 0;
                    LOG_LEVENL = StringHelper.ToInt(paySection.WxPay.LogLevel);
                }
            }
        }
    }
}