using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.RedisHelper
{
    public class StackExchangeConn
    {
        private static ConnectionMultiplexer _connection;
        private static readonly object SyncObject = new object();
        public static ConnectionMultiplexer GetFactionConn
        {
            get
            {
                if (_connection == null || !_connection.IsConnected)
                {
                    lock (SyncObject)
                    {
                        var configurationOptions = new ConfigurationOptions()
                        {
                            Password = PubConstant.RedisPass,
                            EndPoints = { { PubConstant.RedisIp, Convert.ToInt32(PubConstant.RedisPort) } }
                        };
                        //"192.168.100.37,password=myRedis";
                        _connection = ConnectionMultiplexer.Connect(configurationOptions);
                    }
                }
                return _connection;
            }
        }
    }
}
