using Autofac;
using Cache;
using Model.Common;
using Model.Config;
using Service;
using Service.Core;
using System;
using Utility;

namespace Component.Base
{
    public class DataContext
    {
        public enum DataOperate
        {
            insert, update, delete, select
        };

        #region 操作日志

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="operate"></param>
        /// <param name="data"></param>
        /// <param name="func"></param>
        public static void TryLog(DataOperate operate, string data, Func<bool> func)
        {
            if (func != null)
            {
                if (func())
                {
                    //记录日志
                    OperateLog(operate, data);
                }
            }
        }

        #region 记录日志

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="operate"></param>
        /// <param name="result"></param>
        /// <param name="data"></param>
        private static void OperateLog(DataOperate operate, string data)
        {
            if (string.IsNullOrEmpty(data)) return;
        }

        #endregion 记录日志

        #endregion 操作日志

        #region 错误日志

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="method"></param>
        /// <param name="brief"></param>
        /// <param name="module"></param>
        /// <param name="error"></param>
        public static void ErrorLog(string module, string method, string brief, string error)
        {
            try
            {
                InterfaceErrorLog model = new InterfaceErrorLog();
                model.Method = method;
                model.Brief = brief;
                model.Module = module;
                model.Error = error;
                model.AddTime = DateTime.Now;
                IInterfaceErrorLogService service = ResolveService<IInterfaceErrorLogService>();
                service.Add(model);
            }
            catch { }
        }

        #endregion 错误日志

        #region 错误提示

        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="returnUrl">返回地址（需要UrlEncode）</param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static string GetErrorUrl(ErrorMessage errorMessage = ErrorMessage.未知错误, string returnUrl = "", string channelId = "")
        {
            string url = "/error/index";
            if (!string.IsNullOrEmpty(channelId))
            {
                url = url.GetChannelRouteUrl(channelId);
            }
            return StringHelper.GetReturnUrl(url.SpliceUrl("errCode", StringHelper.ToString((int)errorMessage)), returnUrl);
        }

        #endregion 错误提示

        #region 缓存

        public static TReturn TryCache<TReturn>(string key, Func<TReturn> func, int timeOut = 0)
        {
            TReturn t = default(TReturn);

            bool isOpen = ConfigHelper.GetBool("CacheEnabled");
            CacheContext context = isOpen ? new CacheContext(CacheTypeEnum.LocalCache) : null;

            if (isOpen && !string.IsNullOrEmpty(key))
            {
                try
                {
                    t = context.GetCache<TReturn>(key);
                }
                catch { t = default(TReturn); }
            }

            if (func != null && t.IsDefault<TReturn>())
            {
                t = func();
                if (isOpen && !string.IsNullOrEmpty(key) && !t.IsDefault<TReturn>())
                {
                    Type type = t.GetType();
                    if (type == typeof(string) || type.IsValueType)
                    {
                        if (!StringHelper.IsObjectNullOrEmpty(t))
                        {
                            if (timeOut == 0)
                            {
                                context.SetCache<TReturn>(key, t);
                            }
                            else
                            {
                                context.SetCache<TReturn>(key, t, DateTime.Now.AddSeconds(timeOut));
                                //context.SetCache<TReturn>(key, t, TimeSpan.FromSeconds(timeOut));
                            }
                        }
                    }
                    else
                    {
                        if (timeOut == 0)
                        {
                            context.SetCache<TReturn>(key, t);
                        }
                        else
                        {
                            context.SetCache<TReturn>(key, t, DateTime.Now.AddSeconds(timeOut));
                            //context.SetCache<TReturn>(key, t, TimeSpan.FromSeconds(timeOut));
                        }
                    }
                }
            }

            return t;
        }

        #endregion 缓存

        #region 从容器获取实例

        /// <summary>
        ///  从IoC容器中取出service层实例
        /// </summary>
        /// <typeparam name="T">IService 类型</typeparam>
        /// <param name="serviceName">Service 在IoC注入时的标记名称</param>
        /// <returns>Service 实例</returns>
        public static T ResolveService<T>(string serviceName = "")
        {
            var service = string.IsNullOrEmpty(serviceName) ? AutofacBootStrapper.Instance.AutofacContainer.Resolve<T>()
                : AutofacBootStrapper.Instance.AutofacContainer.ResolveNamed<T>(serviceName);
            if (service != null) return service;

            var msg = string.Format("在获取的Service层：{0} 实例时，出现异常！", serviceName);
            throw new Exception(msg);
        }

        #endregion 从容器获取实例

        #region 配置文件

        public static ISiteSection GetSiteSection()
        {
            return ConfigHelper.GetSection<ISiteSection>("SiteSection");
        }

        public static IRecSection GetRecSection()
        {
            return ConfigHelper.GetSection<IRecSection>("RecSection");
        }

        public static IAuthSection GetAuthSection()
        {
            return ConfigHelper.GetSection<IAuthSection>("AuthSection");
        }

        public static IPaySection GetPaySection()
        {
            return ConfigHelper.GetSection<IPaySection>("PaySection");
        }

        #endregion 配置文件
    }
}