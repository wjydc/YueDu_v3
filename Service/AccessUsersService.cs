using Model;
using Model.Common;
using Service.Base;

namespace Service
{
    public class AccessUsersService : BaseService, IAccessUsersService
    {
        public int Register(AccessUsersInfo oAccessUsersInfo, Users oUsers)
        {
            if (oAccessUsersInfo == null || oUsers == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.AccessUsersRepo(conn);
                return repo.Register(oAccessUsersInfo, oUsers);
            }
        }

        public LoginedAccessUsers AccessLogin(AccessUsersInfo oAccessUsersInfo)
        {
            if (oAccessUsersInfo == null) return null;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.AccessUsersRepo(conn);
                return repo.AccessLogin(oAccessUsersInfo);
            }
        }

        public LoginedUsers RegisterLogin(AccessUsersInfo oAccessUsersInfo, Users oUsers, out int result)
        {
            result = 0;
            if (oAccessUsersInfo == null || oUsers == null) return null;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.AccessUsersRepo(conn);
                return repo.RegisterLogin(oAccessUsersInfo, oUsers, out result);
            }
        }
    }
}