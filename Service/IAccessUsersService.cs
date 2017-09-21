using Model;
using Model.Common;
using Service.Base;

namespace Service
{
    public interface IAccessUsersService : IBaseService
    {
        int Register(AccessUsersInfo oAccessUsersInfo, Users oUsers);

        LoginedAccessUsers AccessLogin(AccessUsersInfo oAccessUsersInfo);

        LoginedUsers RegisterLogin(AccessUsersInfo oAccessUsersInfo, Users oUsers, out int result);
    }
}
