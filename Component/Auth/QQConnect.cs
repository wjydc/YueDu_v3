using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utility;

namespace Component.Auth
{
    public class QQConnect
    {
        private string AppId = "";
        private string AppKey = "";
        private string State = "";
        private string CallbackUrl = "";
        private string AuthorizationCode_Url = "https://graph.qq.com/oauth2.0/authorize";
        private string AccessToken_Url = "https://graph.qq.com/oauth2.0/token";
        private string OpenId_Url = "https://graph.qq.com/oauth2.0/me";
        private string UserInfo_Url = "https://openmobile.qq.com/{0}?access_token={1}&oauth_consumer_key={2}&openid={3}";

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

        private string _unionId;

        public string UnionId
        {
            get { return _unionId; }
            set { _unionId = value; }
        }

        public QQConnect(string appId, string appKey, string state, string callbackUrl)
        {
            AppId = appId;
            AppKey = appKey;
            State = state;
            CallbackUrl = callbackUrl;
        }

        public string GetAuthorizationCodeUrl(string scope)
        {
            if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(State) || string.IsNullOrEmpty(scope) || string.IsNullOrEmpty(CallbackUrl)) return "";

            return string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}&state={3}&scope={4}", AuthorizationCode_Url, AppId, CallbackUrl, State, scope);
        }

        public bool GetAccessTokenByCode(string code)
        {
            if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(code) || string.IsNullOrEmpty(CallbackUrl)) return false;

            bool flag = false;
            string result = string.Empty;
            string url = string.Format("{0}?grant_type=authorization_code&client_id={1}&client_secret={2}&code={3}&state={4}&redirect_uri={5}", AccessToken_Url, AppId, AppKey, code, State, CallbackUrl);

            if (flag = HttpHelper.Send(url, "Get", out result))
            {
                var pattern = @"access_token=(([\d|a-zA-Z]*))";

                if (Regex.IsMatch(result, pattern))
                {
                    AccessToken = Regex.Match(result, pattern).Groups[1].Value;
                }
            }

            return flag;
        }

        public bool GetOpenIdByToken(int union = 0)
        {
            if (string.IsNullOrEmpty(AccessToken)) return false;

            bool flag = false;
            string result = string.Empty;
            string url = string.Format("{0}?access_token={1}", OpenId_Url, AccessToken);
            if (union == 1)
            {
                url += "&unionid=1";
            }

            if (flag = HttpHelper.Send(url, "Get", out result))
            {
                string pattern = @"\""openid\"":\""([\d|a-zA-Z]+)\""";

                if (Regex.IsMatch(result, pattern))
                {
                    OpenId = Regex.Match(result, pattern).Groups[1].Value;
                }

                if (union == 1)
                {
                    pattern = @"\""unionid\"":\""([\d|a-zA-Z]+)\""";

                    if (Regex.IsMatch(result, pattern))
                    {
                        UnionId = Regex.Match(result, pattern).Groups[1].Value;
                    }
                }
            }

            return flag;
        }

        public bool GetUserInfo(out string result, out UserInfo userInfo)
        {
            result = string.Empty;
            userInfo = null;
            if (string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(OpenId)) return false;

            bool flag = false;
            string url = string.Format(UserInfo_Url, "user/get_simple_userinfo", AccessToken, AppId, OpenId);

            if (flag = HttpHelper.Send(url, "Get", out result))
            {
                userInfo = JsonHelper.Deserialize<UserInfo>(result);
            }

            return flag;
        }

        public class UserInfo
        {
            private string _ret;

            public string ret
            {
                get { return _ret; }
                set { _ret = value; }
            }

            private string _msg;

            public string msg
            {
                get { return _msg; }
                set { _msg = value; }
            }

            private string _nickname;

            public string nickname
            {
                get { return _nickname; }
                set { _nickname = value; }
            }

            private string _figureurl_qq_1;
            /// <summary>
            /// 40 X 40
            /// </summary>
            public string figureurl_qq_1
            {
                get { return _figureurl_qq_1; }
                set { _figureurl_qq_1 = value; }
            }

            private string _figureurl_qq_2;
            /// <summary>
            /// 100 X 100
            /// </summary>
            public string figureurl_qq_2
            {
                get { return _figureurl_qq_2; }
                set { _figureurl_qq_2 = value; }
            }
        }
    }
}