using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utility;

namespace Component.Auth
{
    public class WeiboConnect
    {
        private string AppId = "";
        private string AppKey = "";
        private string State = "";
        private string CallbackUrl = "";
        private string AuthorizationCode_Url = "https://api.weibo.com/oauth2/authorize";
        private string AccessToken_Url = "https://api.weibo.com/oauth2/access_token";
        private string UserInfo_Url = "https://api.weibo.com/2/users/show.json";

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

        public WeiboConnect(string appId, string appKey, string state, string callbackUrl)
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

            HttpSendInfo sendInfo = new HttpSendInfo();
            sendInfo.Url = AccessToken_Url;
            sendInfo.Method = "Post";
            sendInfo.ContentType = "application/x-www-form-urlencoded";
            sendInfo.Data = string.Format("grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}", AppId, AppKey, code, CallbackUrl);
            sendInfo.IsHttps = true;
            HttpReceiveInfo receiverInfo = new HttpReceiveInfo();
            if (flag = HttpHelper.Send(sendInfo, out receiverInfo) && !string.IsNullOrEmpty(receiverInfo.Result))
            {
                try
                {
                    TokenInfo tokenInfo = JsonHelper.Deserialize<TokenInfo>(receiverInfo.Result);
                    if (tokenInfo != null)
                    {
                        AccessToken = tokenInfo.access_token;
                        OpenId = tokenInfo.uid;
                    }
                }
                catch { }
            }

            return flag;
        }

        public bool GetUserInfo(out string result, out UserInfo userInfo)
        {
            result = string.Empty;
            userInfo = null;
            if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(OpenId)) return false;

            bool flag = false;
            string url = string.Format("{0}?access_token={1}&uid={2}", UserInfo_Url, AccessToken, OpenId);

            HttpSendInfo sendInfo = new HttpSendInfo();
            sendInfo.Url = url;
            sendInfo.Method = "Get";
            sendInfo.ContentType = "application/x-www-form-urlencoded";
            sendInfo.Data = "";
            sendInfo.IsHttps = true;
            HttpReceiveInfo receiverInfo = new HttpReceiveInfo();
            if (flag = HttpHelper.Send(sendInfo, out receiverInfo) && !string.IsNullOrEmpty(receiverInfo.Result))
            {
                result = receiverInfo.Result;
                userInfo = JsonHelper.Deserialize<UserInfo>(result);
            }

            return flag;
        }

        public class TokenInfo
        {
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

            private string _remind_in;

            public string remind_in
            {
                get { return _remind_in; }
                set { _remind_in = value; }
            }

            private string _uid;

            public string uid
            {
                get { return _uid; }
                set { _uid = value; }
            }
        }

        public class UserInfo
        {
            private string _id;

            public string id
            {
                get { return _id; }
                set { _id = value; }
            }

            private string _screen_name;
            /// <summary>
            /// 昵称
            /// </summary>
            public string screen_name
            {
                get { return _screen_name; }
                set { _screen_name = value; }
            }

            private string _profile_image_url;
            /// <summary>
            /// 头像
            /// </summary>
            public string profile_image_url
            {
                get { return _profile_image_url; }
                set { _profile_image_url = value; }
            }
        }
    }
}
