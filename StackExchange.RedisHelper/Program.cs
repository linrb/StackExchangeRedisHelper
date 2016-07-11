using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.RedisHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> userList =new List<User>() {
                new User { UserName = "chengjun", PassWord = "qwerty" }, 
                new User { UserName = "chengjun1", PassWord = "qwerty" },
                new User { UserName = "chengjun2", PassWord = "qwerty" },
                new User { UserName = "chengjun3", PassWord = "qwerty" },
                new User { UserName = "chengjun5", PassWord = "qwerty" },
                new User { UserName = "chengjun6", PassWord = "qwerty" },
                new User { UserName = "chengjun7", PassWord = "qwerty" },
                new User { UserName = "chengjun8", PassWord = "qwerty" }
            };
            var stackExchangeHelper  = new StackExchangeHelper();
            stackExchangeHelper.Set("xxoo_cus", userList);
            var getUserListFromRedis = stackExchangeHelper.Get<List<User>>("xxoo_cus");
        }
    }
    [Serializable]
    public class User
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

}
