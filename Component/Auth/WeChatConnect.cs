using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utility;

namespace Component.Auth
{
    public class WeChatConnect
    {
        private string AppId = "";
        private string AppKey = "";
        private string State = "";
        private string CallbackUrl = "";
        private string AuthorizationCode_QrCode_Url = "https://open.weixin.qq.com/connect/qrconnect";
        private string AuthorizationCode_Auth_Url = "https://open.weixin.qq.com/connect/oauth2/authorize";
        private string AccessToken_Url = "https://api.weixin.qq.com/sns/oauth2/access_token";
        private string UserInfo_Url = "https://api.weixin.qq.com/sns/{0}?access_token={1}&openid={2}";

        private string _microMessengerScope = "snsapi_userinfo";

        /// <summary>
        /// 微信环境下是否显示授权界面
        /// snsapi_userinfo：显示（默认）
        /// snsapi_base：跳过
        /// </summary>
        public string MicroMessengerScope
        {
            get { return _microMessengerScope; }
            set { _microMessengerScope = value; }
        }

        private string _accessToken;

        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        private string _openId;

        public string OpenId
        {
            get { return _openId; }
            set { _openId = value; }
        }

        private bool IsMicroMessenger = false;

        public WeChatConnect(string qrcodeAppId, string qrcodeAppKey, string authAppId, string authAppKey, string state, string callbackUrl)
        {
            IsMicroMessenger = StringHelper.GetUserAgent().ToLower().Contains("micromessenger");

            if (IsMicroMessenger)
            {
                AppId = authAppId;
                AppKey = authAppKey;
            }
            else
            {
                AppId = qrcodeAppId;
                AppKey = qrcodeAppKey;
            }

            State = state;
            CallbackUrl = callbackUrl;
        }

        public string GetAuthorizationCodeUrl(string scope)
        {
            if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(State) || string.IsNullOrEmpty(scope) || string.IsNullOrEmpty(CallbackUrl)) return "";

            if (IsMicroMessenger)
            {
                return string.Format("{0}?appid={1}&redirect_uri={2}&response_type=code&scope={4}&state={3}#wechat_redirect", AuthorizationCode_Auth_Url, AppId, CallbackUrl, State, MicroMessengerScope);
            }
            else
            {
                return string.Format("{0}?response_type=code&appid={1}&redirect_uri={2}&state={3}&scope={4}", AuthorizationCode_QrCode_Url, AppId, CallbackUrl, State, scope);
            }
        }

        public bool GetAccessTokenByCode(string code)
        {
            if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(code) || string.IsNullOrEmpty(CallbackUrl)) return false;

            bool flag = false;
            string result = string.Empty;
            string url = string.Format("{0}?grant_type=authorization_code&appid={1}&secret={2}&code={3}", AccessToken_Url, AppId, AppKey, code);

            if (!IsMicroMessenger)
            {
                url += "&redirect_uri=" + CallbackUrl;
            }

            if (flag = HttpHelper.Send(url, "Get", out result))
            {
                TokenInfo tokenInfo = JsonHelper.Deserialize<TokenInfo>(result);
                if (tokenInfo != null)
                {
                    AccessToken = tokenInfo.access_token;
                }
            }

            return flag;
        }

        public bool GetUserInfo(out string result, out UserInfo userInfo)
        {
            result = string.Empty;
            userInfo = null;
            if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(AccessToken)) return false;

            bool flag = false;
            string url = string.Format(UserInfo_Url, "userinfo", AccessToken, AppId, OpenId);

            if (IsMicroMessenger)
            {
                url += "&lang=zh_CN";
            }

            if (flag = HttpHelper.Send(url, "Get", out result))
            {
                userInfo = JsonHelper.Deserialize<UserInfo>(result);
                if (userInfo != null)
                {
                    OpenId = userInfo.openid;
                }
            }

            return flag;
        }

        public class TokenInfo
        {
            private string _errcode;

            public string errcode
            {
                get { return _errcode; }
                set { _errcode = value; }
            }

            private string _errmsg;

            public string errmsg
            {
                get { return _errmsg; }
                set { _errmsg = value; }
            }

            private string _access_token;

            public string access_token
            {
                get { return _access_token; }
                set { _access_token = value; }
            }

            private string _expires_in;

            public string expires_in
            {
                get { return _expires_in; }
                set { _expires_in = value; }
            }

            private string _refresh_token;

            public string refresh_token
            {
                get { return _refresh_token; }
                set { _refresh_token = value; }
            }

            private string _scope;

            public string scope
            {
                get { return _scope; }
                set { _scope = value; }
            }

            private string _unionid;

            public string unionid
            {
                get { return _unionid; }
                set { _unionid = value; }
            }
        }

        public class UserInfo
        {
            private string _errcode;

            public string errcode
            {
                get { return _errcode; }
                set { _errcode = value; }
            }

            private string _errmsg;

            public string errmsg
            {
                get { return _errmsg; }
                set { _errmsg = value; }
            }

            private string _openid;

            public string openid
            {
                get { return _openid; }
                set { _openid = value; }
            }

            private string _nickname;

            public string nickname
            {
                get { return _nickname; }
                set { _nickname = value; }
            }

            private string _headimgurl;
            /// <summary>
            /// 头像
            /// </summary>
            public string headimgurl
            {
                get { return _headimgurl; }
                set { _headimgurl = value; }
            }

            private string _unionid;

            public string unionid
            {
                get { return _unionid; }
                set { _unionid = value; }
            }
        }
    }
}
