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
    public class QrCodeRepo : Repository<QrCode>
    {
        protected IDbManage DbManage { get; private set; }

        public QrCodeRepo(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
            : base(dbConnection, dbTransaction)
        {
            DbManage = new DbManage(dbConnection, dbTransaction);
        }

        public QrCodeView GetDetail(int classId, int status)
        {
            string sql = " select top 1 b.Id,b.FuncType,b.CnName,b.EnName,b.Path,b.UpdateTime from [dbo].[NovelClass] a inner join [dbo].[QrCode] b on a.QrCodeId = b.Id where a.Id = @classId and a.status = @status and b.status = @status ";
            return DbManage.Query<QrCodeView>(sql, new { classId = classId, status = status, }, CommandType.Text).FirstOrDefault();
        }
    }
}