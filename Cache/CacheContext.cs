using Cache.LocalCache;
using Cache.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
	public enum CacheTypeEnum
	{
		LocalCache = 1, Redis = 2
	}

	public class CacheContext
	{
		private ICacheStrategy strategy;

		/// <summary>
		/// 选择为redis缓存时,请指定操作哪个数据库
		/// </summary>
		/// <param name="cacheType"></param>
		/// <param name="dbNum"></param>
		public CacheContext(CacheTypeEnum cacheType, int dbNum = -1)
		{
			if (cacheType == CacheTypeEnum.LocalCache)
			{
				this.strategy = new LocalCacheStrategy();
			}
			else
			{
				this.strategy = new RedisStrategy(dbNum);
			}
		}

		public bool IsExistsCache(string key)
		{
			return strategy.IsExistsCache(key);
		}

		public T GetCache<T>(string key)
		{
			return strategy.GetCache<T>(key);
		}

		public bool SetCache<T>(string key, T value)
		{
			return strategy.SetCache<T>(key, value);
		}

		public bool SetCache<T>(string key, T value, TimeSpan slidingExpiration)
		{
			return strategy.SetCache<T>(key, value, slidingExpiration);
		}

		public bool SetCache<T>(string key, T value, DateTime absoluteExpiration)
		{
			return strategy.SetCache<T>(key, value, absoluteExpiration);
		}

		public bool RemoveCache(string key)
		{
			return strategy.RemoveCache(key);
		}
	}
}