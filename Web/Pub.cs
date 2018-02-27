using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web
{
    public static class Pub
    {
        /// <summary>
        /// redis服务器地址
        /// </summary>
        public static string IpRedisAddress = ConfigurationManager.AppSettings["IpRedis"];
     
    }
}