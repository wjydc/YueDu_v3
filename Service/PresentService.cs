using Repository;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
    public class PresentService : BaseService, IPresentService
    {
        public IEnumerable<PresentView> GetUserCosumePagerList(string columns, string table, string where, string orderBy, int pageIndex, int pageSize, out int rowCount, object param)
        {
            IEnumerable<PresentView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new NovelPropsUserConsumeLogRepo(conn);

                list = repo.GetPagerList(columns, table, where, orderBy, pageIndex, pageSize, out rowCount, param);
            }

            return list;
        }

        public IEnumerable<ConsumeView> GetConsumeList(string columns, string table, string where, string orderBy, int pageIndex, int pageSize, out int rowCount, object param)
        {
            IEnumerable<ConsumeView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new NovelPropsUserConsumeLogRepo(conn);

                list = repo.GetConsumeList(columns, table, where, orderBy, pageIndex, pageSize, out rowCount, param);
            }

            return list;
        }

        public int Add(PresentView present)
        {
            if (present == null) return 0;

            using (var conn = DbConnection(DbOperation.Write))
            {
                var repo = new NovelPropsUserConsumeLogRepo(conn);
                return repo.Add(present);
            }
        }

        public IEnumerable<NovelPropsView> GetNovelPropsPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param)
        {
            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new NovelPropsRepo(conn);
                return repo.GetPagerList(where, orderby, pageIndex, pageSize, out  rowCount, param);
            }
        }

        public int GetUserFeeByNovel(string userName, int novelId)
        {
            if (string.IsNullOrEmpty(userName) || novelId <= 0) return 0;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new NovelUserConsumeRepo(conn);

                return repo.GetUserFeeByNovel(userName, novelId);
            }
        }

        public IEnumerable<PresentView> GetConsumeRankPagerList(string columns, string table, string where, string orderBy, out int rowCount, int pageIndex = 1, int pageSize = 3, object param = null)
        {
            IEnumerable<PresentView> list = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new NovelUserConsumeRepo(conn);

                list = repo.GetPagerList(columns, table, where, orderBy, out rowCount, pageIndex, pageSize, param);
            }

            return list;
        }
    }
}