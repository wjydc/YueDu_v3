using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    [Serializable]
    public class NovelRecentReadView
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Cover { get; set; }

        public int ContentType { get; set; }

        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public string Author { get; set; }

        public string ChapterName { get; set; }

        public int ChapterCode { get; set; }

        public DateTime RecentUpdateTime { get; set; }

        public DateTime ReadTime { get; set; }

        public string RouteChannelId { get; set; }
    }

    [Serializable]
    public class NovelRecentReadListView
    {
        public int Id { get; set; }

        public string Host { get; set; }

        public IList<NovelRecentReadView> List { get; set; }
    }
}