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
    public interface IUsersService : IBaseService
    {
        UsersView GetDetail(string userName, int status = 1);

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        UsersView GetDetail(int userId, int status = 1);
    }
}