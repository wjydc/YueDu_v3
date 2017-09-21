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

namespace Repository
{
    public class LogRepo
    {
        protected IDbManage DbManage { get; private set; }

        public LogRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public int CreateUserInfo(UserLogInfo model)
        {
            string spName = "Wap.UserLog_Create";

            var p = new DynamicParameters();
            p.Add("@CookieId", model.CookieId);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@ClientId", model.ClientId);
            p.Add("@Version", model.Version);
            p.Add("@UserAgent", model.UserAgent);
            p.Add("@IMEI", model.IMEI);
            p.Add("@IMSI", model.IMSI);
            p.Add("@ChannelId", model.ChannelId);
            p.Add("@SourceType", model.SourceType);
            p.Add("@StartupCount", model.StartupCount);
            p.Add("@RecentStartupTime", model.RecentStartupTime);
            p.Add("@OnlineTime", model.OnlineTime);
            p.Add("@Mobile", model.Mobile);
            p.Add("@NetType", model.NetType);
            p.Add("@Province", model.Province);
            p.Add("@City", model.City);
            p.Add("@ReferUrl", model.ReferUrl);
            p.Add("@Url", model.Url);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@RemoteHost", model.RemoteHost);
            p.Add("@AddTime", model.AddTime);

            return DbManage.Execute(spName, p, CommandType.StoredProcedure);
        }

        public int ChapterReadLog(ChapterLogInfo chapterLogInfo, NovelReadRecordInfo novelReadRecordInfo)
        {
            string spName = "Wap.Chapter_Read";

            var p = new DynamicParameters();
            p.Add("@NovelId", chapterLogInfo.NovelId);
            p.Add("@ChapterId", chapterLogInfo.ChapterId);
            p.Add("@ChapterCode", chapterLogInfo.ChapterCode);
            p.Add("@Position", novelReadRecordInfo.Position);
            p.Add("@RecentReadTime", novelReadRecordInfo.RecentReadTime);
            p.Add("@CookieId", chapterLogInfo.CookieId);
            p.Add("@UserId", chapterLogInfo.UserId);
            p.Add("@UserName", chapterLogInfo.UserName);
            p.Add("@ClientId", chapterLogInfo.ClientId);
            p.Add("@Version", chapterLogInfo.Version);
            p.Add("@UserAgent", chapterLogInfo.UserAgent);
            p.Add("@IMEI", chapterLogInfo.IMEI);
            p.Add("@IMSI", chapterLogInfo.IMSI);
            p.Add("@ChannelId", chapterLogInfo.ChannelId);
            p.Add("@SourceType", chapterLogInfo.SourceType);
            p.Add("@Mobile", chapterLogInfo.Mobile);
            p.Add("@NetType", chapterLogInfo.NetType);
            p.Add("@Province", chapterLogInfo.Province);
            p.Add("@City", chapterLogInfo.City);
            p.Add("@ReferUrl", chapterLogInfo.ReferUrl);
            p.Add("@IPAddress", chapterLogInfo.IPAddress);
            p.Add("@RemoteHost", chapterLogInfo.RemoteHost);
            p.Add("@AddTime", chapterLogInfo.AddTime);
            //p.Add("@PromotionCode", model.PromotionCode);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        public int ChapterReadLogSync(ChapterLogInfo chapterLogInfo, NovelReadRecordInfo novelReadRecordInfo)
        {
            string spName = "Wap.Chapter_ReadSync";

            var p = new DynamicParameters();
            p.Add("@NovelId", chapterLogInfo.NovelId);
            p.Add("@ChapterId", chapterLogInfo.ChapterId);
            p.Add("@ChapterCode", chapterLogInfo.ChapterCode);
            p.Add("@Position", novelReadRecordInfo.Position);
            p.Add("@RecentReadTime", novelReadRecordInfo.RecentReadTime);
            p.Add("@CookieId", chapterLogInfo.CookieId);
            p.Add("@UserId", chapterLogInfo.UserId);
            p.Add("@UserName", chapterLogInfo.UserName);
            p.Add("@ClientId", chapterLogInfo.ClientId);
            p.Add("@Version", chapterLogInfo.Version);
            p.Add("@UserAgent", chapterLogInfo.UserAgent);
            p.Add("@IMEI", chapterLogInfo.IMEI);
            p.Add("@IMSI", chapterLogInfo.IMSI);
            p.Add("@ChannelId", chapterLogInfo.ChannelId);
            p.Add("@SourceType", chapterLogInfo.SourceType);
            p.Add("@Mobile", chapterLogInfo.Mobile);
            p.Add("@NetType", chapterLogInfo.NetType);
            p.Add("@Province", chapterLogInfo.Province);
            p.Add("@City", chapterLogInfo.City);
            p.Add("@ReferUrl", chapterLogInfo.ReferUrl);
            p.Add("@IPAddress", chapterLogInfo.IPAddress);
            p.Add("@RemoteHost", chapterLogInfo.RemoteHost);
            p.Add("@AddTime", chapterLogInfo.AddTime);
            p.Add("@PromotionCode", chapterLogInfo.PromotionCode);
            p.Add("@RouteChannelId", novelReadRecordInfo.RouteChannelId);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        public int ChapterReadLogLocalSync(NovelReadRecordInfo novelReadRecordInfo)
        {
            string spName = "Wap.NovelReadRecord_LocalSync";

            var p = new DynamicParameters();
            p.Add("@NovelId", novelReadRecordInfo.NovelId);
            p.Add("@ChapterId", novelReadRecordInfo.ChapterId);
            p.Add("@ChapterCode", novelReadRecordInfo.ChapterCode);
            p.Add("@Position", novelReadRecordInfo.Position);
            p.Add("@RecentReadTime", novelReadRecordInfo.RecentReadTime);
            p.Add("@CookieId", novelReadRecordInfo.CookieId);
            p.Add("@UserId", novelReadRecordInfo.UserId);
            p.Add("@UserName", novelReadRecordInfo.UserName);
            p.Add("@ClientId", novelReadRecordInfo.ClientId);
            p.Add("@Version", novelReadRecordInfo.Version);
            p.Add("@UserAgent", novelReadRecordInfo.UserAgent);
            p.Add("@IMEI", novelReadRecordInfo.IMEI);
            p.Add("@IMSI", novelReadRecordInfo.IMSI);
            p.Add("@ChannelId", novelReadRecordInfo.ChannelId);
            p.Add("@SourceType", novelReadRecordInfo.SourceType);
            p.Add("@AddTime", novelReadRecordInfo.AddTime);
            p.Add("@RouteChannelId", novelReadRecordInfo.RouteChannelId);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }

        public NovelReadRecordInfo GetRecentChapter(string userName)
        {
            string sql = @"Select top 1 a.Id,
                                a.NovelId,
                                a.ChapterId,
                                a.ChapterCode,
                                a.Position,
                                a.RecentReadTime,
                                a.CookieId,
                                a.UserId,
                                a.UserName,
                                a.ClientId,
                                a.Version,
                                a.UserAgent,
                                a.IMEI,
                                a.IMSI,
                                a.ChannelId,
                                a.SourceType,
                                a.AddTime,
                                b.ChapterName,
					            c.title as novelTitle,
					            c.ContentType as novelContentType
                            From Users.NovelReadRecord a with(nolock)
                                INNER JOIN dbo.Chapter b with(nolock) on a.ChapterCode = b.ChapterCode and a.NovelId = b.NovelId
					            INNER JOIN dbo.Novel c with(nolock) on a.NovelId = c.Id
                            Where a.UserName = @UserName
                            Order By a.RecentReadTime desc, a.id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName }).FirstOrDefault();
        }

        public NovelReadRecordInfo GetRecentChapter(string userName, int novelId)
        {
            string sql = @"Select top 1 Id,
                                NovelId,
                                ChapterId,
                                ChapterCode,
                                Position,
                                RecentReadTime,
                                CookieId,
                                UserId,
                                UserName,
                                ClientId,
                                Version,
                                UserAgent,
                                IMEI,
                                IMSI,
                                ChannelId,
                                SourceType,
                                AddTime
                            From Users.NovelReadRecord with(nolock)
                            Where UserName = @UserName
                            And NovelId = @NovelId
                            Order By Id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName, NovelId = novelId }).FirstOrDefault();
        }

        public NovelReadRecordInfo GetRecentChapterByType(string userName, int contentType)
        {
            string sql = @"Select top 1 a.Id,
                                a.NovelId,
                                a.ChapterId,
                                a.ChapterCode,
                                a.Position,
                                a.RecentReadTime,
                                a.CookieId,
                                a.UserId,
                                a.UserName,
                                a.ClientId,
                                a.Version,
                                a.UserAgent,
                                a.IMEI,
                                a.IMSI,
                                a.ChannelId,
                                a.SourceType,
                                a.AddTime,
                                b.ChapterName,
					            c.title as novelTitle,
					            c.ContentType as novelContentType
                            From Users.NovelReadRecord a with(nolock)
                                INNER JOIN dbo.Chapter b with(nolock) on a.ChapterCode = b.ChapterCode and a.NovelId = b.NovelId
					            INNER JOIN dbo.Novel c with(nolock) on a.NovelId = c.Id
                            Where a.UserName = @UserName and c.ContentType = @ContentType
                            Order By a.RecentReadTime desc, a.id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName, ContentType = contentType }).FirstOrDefault();
        }

        public NovelReadRecordInfo GetRecentChapterExceptType(string userName, int exceptContentType)
        {
            string sql = @"Select top 1 a.Id,
                                a.NovelId,
                                a.ChapterId,
                                a.ChapterCode,
                                a.Position,
                                a.RecentReadTime,
                                a.CookieId,
                                a.UserId,
                                a.UserName,
                                a.ClientId,
                                a.Version,
                                a.UserAgent,
                                a.IMEI,
                                a.IMSI,
                                a.ChannelId,
                                a.SourceType,
                                a.AddTime,
                                b.ChapterName,
					            c.title as novelTitle,
					            c.ContentType as novelContentType
                            From Users.NovelReadRecord a with(nolock)
                                INNER JOIN dbo.Chapter b with(nolock) on a.ChapterCode = b.ChapterCode and a.NovelId = b.NovelId
					            INNER JOIN dbo.Novel c with(nolock) on a.NovelId = c.Id
                            Where a.UserName = @UserName and c.ContentType != @ExceptContentType
                            Order By a.RecentReadTime desc, a.id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName, ExceptContentType = exceptContentType }).FirstOrDefault();
        }

        public IEnumerable<NovelReadRecordInfo> GetRecentChapterList(string userName, int top)
        {
            string sql = @"Select top(@top) a.Id,
                                a.NovelId,
                                a.ChapterId,
                                a.ChapterCode,
                                a.Position,
                                a.RecentReadTime,
                                a.CookieId,
                                a.UserId,
                                a.UserName,
                                a.ClientId,
                                a.Version,
                                a.UserAgent,
                                a.IMEI,
                                a.IMSI,
                                a.ChannelId,
                                a.SourceType,
                                a.AddTime,
                                b.ChapterName,
					            c.title as novelTitle,
					            c.ContentType as novelContentType
                            From Users.NovelReadRecord a with(nolock)
                                INNER JOIN dbo.Chapter b with(nolock) on a.ChapterCode = b.ChapterCode and a.NovelId = b.NovelId
					            INNER JOIN dbo.Novel c with(nolock) on a.NovelId = c.Id
                            Where a.UserName = @UserName
                            Order By a.RecentReadTime desc, a.id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName, top = top });
        }

        public IEnumerable<NovelReadRecordInfo> GetRecentChapterListByType(string userName, int contentType, int top)
        {
            string sql = @"Select top(@top) a.Id,
                                a.NovelId,
                                a.ChapterId,
                                a.ChapterCode,
                                a.Position,
                                a.RecentReadTime,
                                a.CookieId,
                                a.UserId,
                                a.UserName,
                                a.ClientId,
                                a.Version,
                                a.UserAgent,
                                a.IMEI,
                                a.IMSI,
                                a.ChannelId,
                                a.SourceType,
                                a.AddTime,
                                b.ChapterName,
					            c.title as novelTitle,
					            c.ContentType as novelContentType
                            From Users.NovelReadRecord a with(nolock)
                                INNER JOIN dbo.Chapter b with(nolock) on a.ChapterCode = b.ChapterCode and a.NovelId = b.NovelId
					            INNER JOIN dbo.Novel c with(nolock) on a.NovelId = c.Id
                            Where a.UserName = @UserName and c.ContentType = @ContentType
                            Order By a.RecentReadTime desc, a.id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName, ContentType = contentType, top = top });
        }

        public IEnumerable<NovelReadRecordInfo> GetRecentChapterListExceptType(string userName, int exceptContentType, int top)
        {
            string sql = @"Select top(@top) a.Id,
                                a.NovelId,
                                a.ChapterId,
                                a.ChapterCode,
                                a.Position,
                                a.RecentReadTime,
                                a.CookieId,
                                a.UserId,
                                a.UserName,
                                a.ClientId,
                                a.Version,
                                a.UserAgent,
                                a.IMEI,
                                a.IMSI,
                                a.ChannelId,
                                a.SourceType,
                                a.AddTime,
                                b.ChapterName,
					            c.title as novelTitle,
					            c.ContentType as novelContentType
                            From Users.NovelReadRecord a with(nolock)
                                INNER JOIN dbo.Chapter b with(nolock) on a.ChapterCode = b.ChapterCode and a.NovelId = b.NovelId
					            INNER JOIN dbo.Novel c with(nolock) on a.NovelId = c.Id
                            Where a.UserName = @UserName and c.ContentType != @ExceptContentType
                            Order By a.RecentReadTime desc, a.id desc";
            return DbManage.Query<NovelReadRecordInfo>(sql, new { UserName = userName, ExceptContentType = exceptContentType, top = top });
        }

        public int NovelReadLog(NovelLogInfo model)
        {
            string spName = "Wap.Novel_Log";

            var p = new DynamicParameters();
            p.Add("@NovelId", model.NovelId);
            p.Add("@CookieId", model.CookieId);
            p.Add("@UserId", model.UserId);
            p.Add("@UserName", model.UserName);
            p.Add("@ClientId", model.ClientId);
            p.Add("@Version", model.Version);
            p.Add("@UserAgent", model.UserAgent);
            p.Add("@IMEI", model.IMEI);
            p.Add("@IMSI", model.IMSI);
            p.Add("@ChannelId", model.ChannelId);
            p.Add("@SourceType", model.SourceType);
            p.Add("@Mobile", model.Mobile);
            p.Add("@NetType", model.NetType);
            p.Add("@Province", model.Province);
            p.Add("@City", model.City);
            p.Add("@ReferUrl", model.ReferUrl);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@RemoteHost", model.RemoteHost);
            p.Add("@AddTime", model.AddTime);
            //p.Add("@PromotionCode", model.PromotionCode);
            p.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute(spName, p, CommandType.StoredProcedure);

            return p.Get<int>("@ROut");
        }
    }
}