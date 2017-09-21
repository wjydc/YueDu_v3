using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Base
{
    public class ChapterDetailManage : ChapterPagerManage
    {
        public ChapterDetailManage(Service.IChapterService service, bool isCustomRange = false)
            : base((novelId) => { return service.GetChapterCodeRange(novelId); }, isCustomRange)
        {
        }

        public bool IsRead(Service.IOrderService service, string userName, int curNovelId = 0)
        {
            return IsRead((model) => { return service.IsReadChapter(model); }, userName, curNovelId);
        }

        public bool IsOrder(Service.IOrderService service, string userName, int curNovelId = 0)
        {
            return IsOrder((model) => { return service.IsOrderNovel(model); }, userName, curNovelId);
        }
    }
}