using Model;
using Model.Common;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Service
{
	public interface IBookService : IBaseService
	{
		IEnumerable<NovelClass> GetNovelClassList(int? contentType, int status = 1);

		IEnumerable<NovelView> GetPagerList(string where, string orderBy, out int rowCount, int pageIndex, int pageSize, object param, string table = "[dbo].[Novel] as n inner join [dbo].[NovelClass] nc on nc.Id = n.ClassId", string columns = "n.Id, n.Title, n.LargeCover, n.ThumbCover, n.SmallCover, n.UpdateStatus, nc.ClassName");

		Novel GetNovel(int novelId, int status = 1);

		AuthorNotice GetAuthorNotice(string columns, string where, string orderby, object param);

		CP GetCP(string columns, string where, string orderby, object param);
	}
}