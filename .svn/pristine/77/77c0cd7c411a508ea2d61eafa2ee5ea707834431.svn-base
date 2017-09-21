using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Cache.LocalCache
{
	internal class LocalCacheHelper
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
		public object GetCache(string cacheKey)
		{
			return HttpRuntime.Cache[cacheKey];
		}

		/// <summary>
		/// 设置数据缓存
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <param name="value">值</param>
		public void SetCache(string cacheKey, object value)
		{
			HttpRuntime.Cache.Insert(cacheKey, value);
		}

		/// <summary>
		/// 设置数据缓存（滑动过期时间）
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <param name="value">值</param>
		/// <param name="Timeout">滑动过期时间</param>
		public void SetCache(string cacheKey, object value, TimeSpan Timeout)
		{
			HttpRuntime.Cache.Insert(cacheKey, value, null, DateTime.MaxValue, Timeout, CacheItemPriority.NotRemovable, null);
		}

		/// <summary>
		/// 设置数据缓存(绝对过期时间)
		/// </summary>
		/// <param name="cacheKey">键</param>
		/// <param name="value">值</param>
		/// <param name="absoluteExpiration">绝对过期时间</param>
		/// <param name="slidingExpiration">滑动过期时间</param>
		public void SetCache(string cacheKey, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
		{
			HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, slidingExpiration);
		}

		/// <summary>
		/// 删除指定数据缓存
		/// </summary>
		/// <param name="cacheKey">键</param>
		public void RemoveCache(string cacheKey)
		{
			HttpRuntime.Cache.Remove(cacheKey);
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