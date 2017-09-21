using Cache.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Cache
{
	public class RedisStrategy : ICacheStrategy
	{
		private int DbNum { get; set; }
		private readonly ConnectionMultiplexer _conn;
		public string CustomKey;//key 的前缀

		#region 构造函数

		public RedisStrategy(int dbNum = 0)
			: this(dbNum, null)
		{
		}

		public RedisStrategy(int dbNum, string readWriteHosts)
		{
			DbNum = dbNum;
			_conn = string.IsNullOrWhiteSpace(readWriteHosts) ? RedisConnection.Instance :
				RedisConnection.GetConnectionMultiplexer(readWriteHosts);
		}

		#endregion 构造函数

		#region 辅助方法

		/// <summary>
		/// 为key添加前缀 方便存取
		/// </summary>
		/// <param name="oldKey"></param>
		/// <returns></returns>
		private string AddSysCustomKey(string oldKey)
		{
			var prefixKey = CustomKey ?? RedisConnection.SysCustomKey;
			return prefixKey + oldKey;
		}

		private T Do<T>(Func<IDatabase, T> func)
		{
			var database = _conn.GetDatabase(DbNum);
			return func(database);
		}

		private string ConvertJson<T>(T value)
		{
			string result = value is string ? value.ToString() : JsonHelper.Serialize(value);
			return result;
		}

		private T ConvertObj<T>(RedisValue value)
		{
			return JsonHelper.Deserialize<T>(value);
		}

		private List<T> ConvetList<T>(RedisValue[] values)
		{
			List<T> result = new List<T>();
			foreach (var item in values)
			{
				var model = ConvertObj<T>(item);
				result.Add(model);
			}
			return result;
		}

		private RedisKey[] ConvertRedisKeys(List<string> redisKeys)
		{
			return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
		}

		#endregion 辅助方法

		public bool IsExistsCache(string key)
		{
			key = AddSysCustomKey(key);
			return Do(db => db.KeyExists(key));
		}

		public T GetCache<T>(string key)
		{
			key = AddSysCustomKey(key);
			RedisValue value = Do(db => db.StringGet(key));
			return ConvertObj<T>(value);
		}

		public bool SetCache<T>(string key, T value)
		{
			key = AddSysCustomKey(key);
			string jsonValue = ConvertJson(value);
			return Do(db => db.StringSet(key, jsonValue));
		}

		public bool SetCache<T>(string key, T value, TimeSpan slidingExpiration)
		{
			key = AddSysCustomKey(key);
			string jsonValue = ConvertJson(value);
			return Do(db => db.StringSet(key, jsonValue, slidingExpiration));
		}

		public bool SetCache<T>(string key, T value, DateTime absoluteExpiration)
		{
			TimeSpan ts = absoluteExpiration - DateTime.Now;
			key = AddSysCustomKey(key);
			string jsonValue = ConvertJson(value);
			return Do(db => db.StringSet(key, jsonValue, ts));
		}

		public bool RemoveCache(string key)
		{
			key = AddSysCustomKey(key);
			return Do(db => db.KeyDelete(key));
		}
	}
}