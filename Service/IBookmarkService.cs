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
    public interface IBookmarkService : IBaseService
    {
        IEnumerable<BookmarkView> GetPagerList(string where, string orderby, int pageIndex, int pageSize, out int rowCount, object param);

        int Add(Bookmark bookmark, BookmarkLogView bookmarkLog);

        bool Exists(int NovelId, string UserName);

        bool Delete(IEnumerable<int> novelIds, string userName);
    }
}