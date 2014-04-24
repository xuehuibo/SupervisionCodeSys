using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace SysInfo
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class LogWriter
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="msg">信息</param>
        public static void WriteErrorLog(string module,string action ,string msg)
        {
            File.AppendAllText(@"/log.txt",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "#" + module + "#" + action + "#" + msg+"\r\n",
                System.Text.Encoding.UTF8);
        }
    }
}