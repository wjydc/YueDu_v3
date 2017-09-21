using DapperExtension.Core;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class PackageRepo : Repository<Package>
    {
        protected IDbManage DbManage { get; private set; }

        public PackageRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        //推荐位列表
        public IEnumerable<PackageView> GetPagerList(Constants.PackageType packageType, string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status, int showType)
        {
            string columns = "pn.*, n.Title AS NovelTitle, n.ShortDescription AS NovelShortDesc, n.LargeCover, n.ThumbCover, n.SmallCover, n.UpdateStatus, nc.ClassName, n.ShortWordSize, p.BeginTime, p.EndTime, p.AutoOpenTime, p.AutoCloseTime";
            string table = string.Format(" users.PackageNovel pn with (nolock) INNER JOIN dbo.Novel n with (nolock) ON pn.NovelId = n.Id and n.status = {0} INNER JOIN Users.Package p with (nolock) ON p.Id = pn.PackageId INNER JOIN dbo.NovelClass nc ON nc.Id = n.ClassId ", status);
            where += string.Format("AND pn.Status = {0} AND p.Status = {0} AND p.ShowType = {1} ", status, showType);

            switch (packageType)
            {
                case Constants.PackageType.LimitFree:
                    where += " AND GETDATE() BETWEEN p.BeginTime AND p.EndTime ";
                    break;

                //case Constants.PackageType.Topic:
                //case Constants.PackageType.Free:
                //case Constants.PackageType.Rebate:
                //    where += " AND ((AutoTurn = 1 AND GETDATE() BETWEEN AutoOpenTime AND AutoCloseTime) OR AutoTurn = 0) ";
                //    break;
                default:
                    break;
            }

            return DbManage.GetPagerList<PackageView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }

        public PackageView GetNovelPackage(Constants.PackageType packageType, int novelId)
        {
            StringBuilder sql = new StringBuilder(" select pn.*,  p.BeginTime, p.EndTime, p.AutoOpenTime, p.AutoCloseTime");
            sql.Append(" from users.PackageNovel pn with (nolock) INNER JOIN Users.Package p with (nolock) ON p.Id = pn.PackageId ");
            string where = " where pn.status = 1 and p.status = 1 and pn.novelId = @novelId ";
            switch (packageType)
            {
                case Constants.PackageType.LimitFree:
                    where += " AND GETDATE() BETWEEN BeginTime AND EndTime ";
                    break;

                case Constants.PackageType.Topic:
                case Constants.PackageType.Free:
                case Constants.PackageType.Rebate:
                    where += " AND ((AutoTurn = 1 AND GETDATE() BETWEEN AutoOpenTime AND AutoCloseTime) OR AutoTurn = 0) ";
                    break;
            }

            sql.Append(where);
            return DbManage.Query<PackageView>(sql.ToString(), new { novelId }, CommandType.Text).FirstOrDefault();
        }
    }
}