using Dapper;
using DapperExtension.Core;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Repository
{
    public class AccessUsersRepo
    {
        protected IDbManage DbManage { get; private set; }

        public AccessUsersRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        #region

        /// <summary>
        /// 第三方注册
        /// </summary>
        /// <param name="oAccessUsersInfo"></param>
        /// <param name="oUsers"></param>
        /// <returns></returns>
        public int Register(AccessUsersInfo oAccessUsersInfo, Users oUsers)
        {
            string spName = "Wap.AccessUsers_Register";

            var p = new DynamicParameters();
            p.Add("@ClientId", oAccessUsersInfo.ClientId);
            p.Add("@UserId", oAccessUsersInfo.UserId);
            p.Add("@UserName", oAccessUsersInfo.UserName);
            p.Add("@Platform", oAccessUsersInfo.Platform);
            p.Add("@OpenId", oAccessUsersInfo.OpenId);
            p.Add("@AccessToken", oAccessUsersInfo.AccessToken);
            p.Add("@NickName", oAccessUsersInfo.NickName);
            p.Add("@Icon", oAccessUsersInfo.Icon);
            p.Add("@Phone", oAccessUsersInfo.Phone);
            p.Add("@Email", oAccessUsersInfo.Email);
            p.Add("@RecentLoginTime", oAccessUsersInfo.RecentLoginTime);
            p.Add("@IsLogin", oAccessUsersInfo.IsLogin);
            p.Add("@LoginInvalidTime", oAccessUsersInfo.LoginInvalidTime);
            p.Add("@UserAgent", oAccessUsersInfo.UserAgent);
            p.Add("@IMSI", oAccessUsersInfo.IMSI);
            p.Add("@IMEI", oAccessUsersInfo.IMEI);
            p.Add("@Version", oAccessUsersInfo.Version);
            p.Add("@ChannelId", oAccessUsersInfo.ChannelId);
            p.Add("@SourceType", oAccessUsersInfo.SourceType);
            p.Add("@Status", oAccessUsersInfo.Status);
            p.Add("@AddTime", oAccessUsersInfo.AddTime);
            p.Add("@Fee", oUsers.Fee);
            p.Add("@GuestUserName", oUsers.UserName);
            p.Add("@UserIcon", oUsers.Icon);
            p.Add("@UserPhone", oUsers.Phone);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        /// <summary>
        /// 第三方登录（手机号码）
        /// </summary>
        /// <param name="oAccessUsersInfo"></param>
        /// <returns></returns>
        public LoginedAccessUsers AccessLogin(AccessUsersInfo oAccessUsersInfo)
        {
            string sql = @"select a.openid,b.*
                            from dbo.accessusers a inner join dbo.users b on a.userid = b.id and a.status = 1 and b.status = 1
                            where a.OpenId = @OpenId and a.AccessToken = @AccessToken and a.Platform = @Platform";
            var p = new
            {
                OpenId = oAccessUsersInfo.OpenId,
                AccessToken = oAccessUsersInfo.AccessToken,
                Platform = oAccessUsersInfo.Platform
            };

            return DbManage.Query<LoginedAccessUsers>(sql, p, CommandType.Text).FirstOrDefault();
        }

        #endregion

        #region

        /// <summary>
        /// 第三方注册登录
        /// </summary>
        /// <param name="oAccessUsersInfo"></param>
        /// <param name="oUsers"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public LoginedUsers RegisterLogin(AccessUsersInfo oAccessUsersInfo, Users oUsers, out int result)
        {
            string spName = "Wap.AccessUsers_Register_Union";

            var p = new DynamicParameters();
            p.Add("@ClientId", oAccessUsersInfo.ClientId);
            p.Add("@UserId", oAccessUsersInfo.UserId);
            p.Add("@UserName", oAccessUsersInfo.UserName);
            p.Add("@Platform", oAccessUsersInfo.Platform);
            p.Add("@OpenId", oAccessUsersInfo.OpenId);
            p.Add("@AccessToken", oAccessUsersInfo.AccessToken);
            p.Add("@NickName", oAccessUsersInfo.NickName);
            p.Add("@Icon", oAccessUsersInfo.Icon);
            p.Add("@Phone", oAccessUsersInfo.Phone);
            p.Add("@Email", oAccessUsersInfo.Email);
            p.Add("@RecentLoginTime", oAccessUsersInfo.RecentLoginTime);
            p.Add("@IsLogin", oAccessUsersInfo.IsLogin);
            p.Add("@LoginInvalidTime", oAccessUsersInfo.LoginInvalidTime);
            p.Add("@UserAgent", oAccessUsersInfo.UserAgent);
            p.Add("@IMSI", oAccessUsersInfo.IMSI);
            p.Add("@IMEI", oAccessUsersInfo.IMEI);
            p.Add("@Version", oAccessUsersInfo.Version);
            p.Add("@ChannelId", oAccessUsersInfo.ChannelId);
            p.Add("@SourceType", oAccessUsersInfo.SourceType);
            p.Add("@Status", oAccessUsersInfo.Status);
            p.Add("@AddTime", oAccessUsersInfo.AddTime);
            p.Add("@Fee", oUsers.Fee);
            p.Add("@GuestUserName", oUsers.UserName);
            p.Add("@UserIcon", oUsers.Icon);
            p.Add("@UserPhone", oUsers.Phone);
            p.Add("@UnionId", oAccessUsersInfo.UnionId);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            LoginedUsers loginedUsers = DbManage.Query<LoginedUsers>(spName, p, CommandType.StoredProcedure).FirstOrDefault();

            result = p.Get<int>("@ROut");

            return loginedUsers;
        }

        #endregion
    }
}