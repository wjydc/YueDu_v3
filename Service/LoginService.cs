using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LoginService : BaseService, ILoginService
    {
        public DateTime UpdateLoginInfo(string userName, int status = 1)
        {
            if (string.IsNullOrEmpty(userName)) return Convert.ToDateTime("01/01/1900");

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new Repository.LoginRepo(conn);
                return repo.UpdateLoginInfo(userName, status);
            }
        }
    }
}