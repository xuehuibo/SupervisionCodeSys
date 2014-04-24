using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysInfo
{
    public class SysSetting
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="name">连接名称</param>
        /// <returns></returns>
        public static string DBConnection(string name)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static string DBCCN
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DBCCN"].ConnectionString;
            }
        }
    }
}