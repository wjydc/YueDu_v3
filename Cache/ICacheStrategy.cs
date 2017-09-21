using Cache.LocalCache;
using Cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
	public interface ICacheStrategy
	{
		/// <summary>
		/// 查询指定缓存是否存在
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>true:存在，false:不存在</returns>
		bool IsExistsCache(string key);

		/// <summary>
		/// 获取缓存中的数据
		/// </summary>
		/// <param name="key">键</param>
		T GetCache<T>(string key);

		/// <summary>
		/// 设置数据缓存
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		bool SetCache<T>(string key, T value);

		/// <summary>
		/// 设置数据缓存（滑动过期时间）
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		/// <param name="slidingExpiration">滑动过期时间</param>
		bool SetCache<T>(string key, T value, TimeSpan slidingExpiration);

		/// <summary>
		/// 设置数据缓存(绝对过期时间)
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		/// <param name="absoluteExpiration">绝对过期时间</param>
		bool SetCache<T>(string key, T value, DateTime absoluteExpiration);

		/// <summary>
		/// 删除指定数据缓存
		/// </summary>
		/// <param name="key">键</param>
		bool RemoveCache(string key);
	}
}