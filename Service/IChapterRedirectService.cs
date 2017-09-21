using Model;
using Model.Common;
using Service.Base;
using System.Collections.Generic;
using ViewModel;

namespace Service
{
    public interface IChapterRedirectService : IBaseService
    {
        ChapterRedirect Get(int novelId, int status = 1);
    }
}