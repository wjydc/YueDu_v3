using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Cache
{
	public class LocalCacheStrategy : ICacheStrategy
	{
		/// <summary>
		/// 查询指定缓存是否存在
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <returns>true:存在，false:不存在</returns>
		public bool IsExistsCache(string cacheKey)
		{
			if (HttpRuntime.Cache[cacheKey] == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 获取缓存中的数据
		/// </summary>
		/// <param name="cacheKey">键</param>
		public T GetCache<T>(string cacheKey)
		{
			return (T)HttpRuntime.Cache[cacheKey];
		}

		/// <summary>
		/// 设置数据缓存
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <param name="value">值</param>
		public bool SetCache<T>(string cacheKey, T value)
		{
			HttpRuntime.Cache.Insert(cacheKey, value);
			return HttpRuntime.Cache[cacheKey] != null;
		}

		/// <summary>
		/// 设置数据缓存（滑动过期时间）
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <param name="value">值</param>
		/// <param name="Timeout">滑动过期时间</param>
		public bool SetCache<T>(string cacheKey, T value, TimeSpan slidingExpiration)
		{
			HttpRuntime.Cache.Insert(cacheKey, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration, CacheItemPriority.NotRemovable, null);
			return HttpRuntime.Cache[cacheKey] != null;
		}

		/// <summary>
		/// 设置数据缓存(绝对过期时间)
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <param name="value">值</param>
		/// <param name="absoluteExpiration">绝对过期时间</param>
		public bool SetCache<T>(string cacheKey, T value, DateTime absoluteExpiration)
		{
			HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
			return HttpRuntime.Cache[cacheKey] != null;
		}

		/// <summary>
		/// 删除指定数据缓存
		/// </summary>
		/// <param name="cacheKey">键</param>
		public bool RemoveCache(string cacheKey)
		{
			HttpRuntime.Cache.Remove(cacheKey);
			return HttpRuntime.Cache[cacheKey] == null;
		}

		/// <summary>
		/// 移除全部缓存
		/// </summary>
		public void RemoveAllCache()
		{
			IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
			while (CacheEnum.MoveNext())
			{
				HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
			}
		}
	}
}