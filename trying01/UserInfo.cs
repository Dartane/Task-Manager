using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trying01
{
    public class UserInfo
    {
        public static string UserLogin {  get; set; }
        
        public UserInfo(string userlog)
        {
            UserLogin = userlog;
        }
        public static string Vivod()
        {

            return UserLogin;
        }
    }
}
