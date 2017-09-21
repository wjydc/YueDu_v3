using DapperExtension.Core;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class NovelPropsRepo : Repository<NovelProps>
    {
        protected IDbManage DbManage { get; private set; }

        public NovelPropsRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        //道具列表
        public IEnumerable<NovelPropsView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param)
        {
            string columns = " [Id],[Name],[Description],[Fee], [FeeType],[AddTime], [Icon],[LargeIcon],[SmallIcon]";
            string table = " dbo.NovelProps with (nolock) ";

            return DbManage.GetPagerList<NovelPropsView>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);
        }
    }
}