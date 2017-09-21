using Model.Common;
using Utility;

namespace Component.Base
{
    public class CurrentContext
    {
        public static ICurrentUser GetUser()
        {
            return SessionCookieHelper<ICurrentUser>.Get(Constants.SecurityKey.LoginedUserInfo_SessionName, Constants.SecurityKey.LoginedUserInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);
        }

        public static void SaveUser(ICurrentUser user)
        {
            SessionCookieHelper<ICurrentUser>.Save(user, Constants.SecurityKey.LoginedUserInfo_SessionName, Constants.SecurityKey.LoginedUserInfo_CookieName, Constants.SecurityKey.key, Constants.SecurityKey.IV);
        }

        public static void ClearUser()
        {
            SessionCookieHelper<ICurrentUser>.Clear(Constants.SecurityKey.LoginedUserInfo_SessionName, Constants.SecurityKey.LoginedUserInfo_CookieName);
        }
    }
}