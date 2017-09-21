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
    public class QrCodeService : BaseService, IQrCodeService
    {
        public QrCodeView GetDetail(int classId, int status = 1)
        {
            if (classId <= 0) return null;

            QrCodeView qrCode = null;

            using (var conn = DbConnection(DbOperation.Read))
            {
                var repo = new Repository.QrCodeRepo(conn);
                qrCode = repo.GetDetail(classId, status);
            }

            return qrCode;
        }
    }
}