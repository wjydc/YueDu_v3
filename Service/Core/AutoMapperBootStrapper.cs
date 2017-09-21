using AutoMapper;
using Model;
using System.Collections.Generic;
using Utility;
using ViewModel;

namespace Service.Core
{
    public class AutoMapperBootStrapper
    {
        private static AutoMapperBootStrapper _strapper;

        public static AutoMapperBootStrapper Instance
        {
            get { return _strapper ?? (_strapper = new AutoMapperBootStrapper()); }
        }

        public void Initialise()
        {
            Mapper.CreateMap<NovelClass, NovelClassView>();
            Mapper.CreateMap<Novel, BookDetailView>();
            Mapper.CreateMap<NovelRecentReadLog, NovelRecentReadListView>();
            Mapper.CreateMap<NovelReadInfo, NovelRecentReadView>();
            Mapper.CreateMap<NovelReadRecordInfo, NovelRecentReadView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NovelId))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.NovelContentType))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.NovelTitle))
                .ForMember(dest => dest.ReadTime, opt => opt.MapFrom(src => src.RecentReadTime));
        }
    }

    public static class AutoMapperExtension
    {
        public static BookDetailView ToBookDetailView(this Novel model)
        {
            return model != null ? Mapper.Map<BookDetailView>(model) : null;
        }

        public static List<NovelClassView> ToNovelClassViewList(this List<NovelClass> list)
        {
            return !list.IsNullOrEmpty<NovelClass>() ? Mapper.Map<List<NovelClass>, List<NovelClassView>>(list) : null;
        }

        public static NovelRecentReadListView ToNovelRecentReadListView(this NovelRecentReadLog model)
        {
            return model != null ? Mapper.Map<NovelRecentReadListView>(model) : null;
        }

        public static NovelRecentReadView ToNovelRecentReadView(this NovelReadInfo model)
        {
            return model != null ? Mapper.Map<NovelRecentReadView>(model) : null;
        }

        public static List<NovelRecentReadView> ToNovelRecentReadViewList(this List<NovelReadRecordInfo> list)
        {
            return !list.IsNullOrEmpty<NovelReadRecordInfo>() ? Mapper.Map<List<NovelReadRecordInfo>, List<NovelRecentReadView>>(list) : null;
        }
    }
}