using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ADRepo
    {
        protected IDbManage DbManage { get; private set; }

        public ADRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public IEnumerable<AD> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param, int status)
        {
            string columns = "a.Title, a.ClassId, a.FunctionType, a.Cover, a.FId, a.FCode, a.Url,a.ShortSummary, d.ClassName as novelClassName,c.Title as NovelTitle,c.Author,c.LargeCover,c.SmallCover,c.ThumbCover,c.UpdateStatus,c.Tags,c.Hits as novelHits,c.CommentCount,c.FavCount,c.UserId,c.RewardFee,c.RecentChapterName,c.WordSize,c.ShortWordSize,c.FNovelId,c.RecentChapterUpdateTime";
            string table = string.Format("dbo.AD a with (nolock) INNER JOIN dbo.ADClass b ON a.ClassId = b.ID Left JOIN dbo.Novel c with (nolock) ON c.id = a.FId AND c.Status = {0} Left JOIN dbo.NovelClass d ON d.id = c.ClassId ", status);
            where += string.Format(" and GETDATE() > a.OnlineTime AND GETDATE() < a.OfflineTime and a.status = {0} ", status);

            return DbManage.GetPagerList<AD>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }
    }
}