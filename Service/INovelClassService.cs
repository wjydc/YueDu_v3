using Model;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public interface INovelClassService : IBaseService
	{
		IEnumerable<NovelClass> GetAll(int contentType, int status = 1);
	}
}