using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.RedisHelper
{
    public class StackExchangeHelper
    {
        private  static IDatabase cache;
        public StackExchangeHelper()
        {
            cache = StackExchangeConn.GetFactionConn.GetDatabase();
        }
        /// <summary>
        /// 添加string类型数据到redis
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool StringSet(string key, string value)
        {
            return cache.StringSet(key, value);
        }
        /// <summary>
        /// 从redis中获取string类型数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return cache.StringGet(key);
        }
        /// <summary>
        /// 从redis中获取Int类型数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public int IntGet(string key)
        {
            try
            {
                return Convert.ToInt32(StringGet(key));
            }
            catch (Exception)
            {
                return (-9999);
            }
        }
        /// <summary>
        /// 根据key获取指定类型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return Deserialize<T>(cache.StringGet(key));
        }
        /// <summary>
        /// 根据key获取object类型数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return Deserialize<object>(cache.StringGet(key));
        }
        /// <summary>
        /// 将数据添加到redis中
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Set(string key, object value)
        {
            cache.StringSet(key, Serialize(value));
        }
        static byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}
