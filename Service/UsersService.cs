using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersView GetDetail(string userName, int status = 1)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            UsersView users = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.UsersRepo(conn);
                users = repo.GetDetail(userName, status);
            }

            return users;
        }

        public UsersView GetDetail(int userId, int status = 1)
        {
            if (userId <= 0) return null;

            UsersView users = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.UsersRepo(conn);
                users = repo.GetDetail(userId, status);
            }

            return users;
        }
    }
}