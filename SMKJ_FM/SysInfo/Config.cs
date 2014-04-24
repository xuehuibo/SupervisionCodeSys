using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SysInfo
{
    public class Config
    {
        /// <summary>
        /// 系统库连接字符串
        /// </summary>
        public static string DBCCN
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string WebURL
        {
            get;
            set;
        }
        /// <summary>
        /// api入口
        /// </summary>
        public static string API_GetDBConnection
        {
            get
            {
                return "/api/ProductForm/GetDBConnection";
            }
        }

        public static string API_TestConfig
        {
            get
            {
                return "/api/ProductForm/TestConfig";
            }
        }

        /// <summary>
        /// 计算机名称
        /// </summary>
        public static string WorkComputerName
        {
            get
            {
                return System.Net.Dns.GetHostName().ToUpper();
            }
        }
    }
}
