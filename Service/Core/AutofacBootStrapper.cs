using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace Service.Core
{
    public class AutofacBootStrapper
    {
        // Controller Assembly
        private Assembly _controllerAssemblies;

        // 单例对象
        private static AutofacBootStrapper _strapper;

        public static AutofacBootStrapper Instance
        {
            get { return _strapper ?? (_strapper = new AutofacBootStrapper()); }
        }

        /// <summary>
        /// IoC Container for Autofac.
        /// </summary>
        public IContainer AutofacContainer { get; set; }

        public void Initialise(Assembly controllerAssemblies)
        {
            _controllerAssemblies = controllerAssemblies;
            BuildContainer();
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            #region 注册服务层

            builder.RegisterType<InterfaceErrorLogService>().As<IInterfaceErrorLogService>().SingleInstance();
            builder.RegisterType<SiteConfigService>().As<ISiteConfigService>().SingleInstance();
            builder.RegisterType<LogService>().As<ILogService>().SingleInstance();
            builder.RegisterType<BlackListService>().As<IBlackListService>().SingleInstance();
            builder.RegisterType<GenerateHitsService>().As<IGenerateHitsService>().SingleInstance();

            builder.RegisterType<RecommendService>().As<IRecommendService>().InstancePerRequest();
            builder.RegisterType<ADService>().As<IADService>().InstancePerRequest();
            builder.RegisterType<NovelClassService>().As<INovelClassService>().InstancePerRequest();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerRequest();
            builder.RegisterType<ChapterService>().As<IChapterService>().InstancePerRequest();
            builder.RegisterType<ChapterRedirectService>().As<IChapterRedirectService>().SingleInstance();
            builder.RegisterType<ExtendChapterService>().As<IExtendChapterService>().InstancePerRequest();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerRequest();
            builder.RegisterType<PackageService>().As<IPackageService>().InstancePerRequest();
            builder.RegisterType<PromotionLinkService>().As<IPromotionLinkService>().InstancePerRequest();
            builder.RegisterType<NovelPromotionChannelService>().As<INovelPromotionChannelService>().InstancePerRequest();

            builder.RegisterType<HotSearchWordService>().As<IHotSearchWordService>().InstancePerRequest();

            builder.RegisterType<ChannelCompanyService>().As<IChannelCompanyService>().InstancePerRequest();

            builder.RegisterType<AccessUsersService>().As<IAccessUsersService>().InstancePerRequest();
            builder.RegisterType<UsersService>().As<IUsersService>().InstancePerRequest();
            builder.RegisterType<LoginService>().As<ILoginService>().SingleInstance();
            builder.RegisterType<BookmarkService>().As<IBookmarkService>().InstancePerRequest();
            builder.RegisterType<PresentService>().As<IPresentService>().InstancePerRequest();
            builder.RegisterType<RechargeFeeConfigService>().As<IRechargeFeeConfigService>().InstancePerRequest();
            builder.RegisterType<RechargeService>().As<IRechargeService>().InstancePerRequest();
            builder.RegisterType<RechargeTypeService>().As<IRechargeTypeService>().InstancePerRequest();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();
            builder.RegisterType<QrCodeService>().As<IQrCodeService>().InstancePerRequest();

            #endregion 注册服务层

            #region 注册控制器

            builder.RegisterControllers(controllerAssemblies: _controllerAssemblies);

            #endregion 注册控制器

            AutofacContainer = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacContainer));
        }
    }
}