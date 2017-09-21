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
    public interface IQrCodeService : IBaseService
    {
        QrCodeView GetDetail(int classId, int status = 1);
    }
}
