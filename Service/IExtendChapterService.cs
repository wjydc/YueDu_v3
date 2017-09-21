using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IExtendChapterService
    {
        ExtendChapter GetExtendChapter(int novelId, int chapterCode);
    }
}
