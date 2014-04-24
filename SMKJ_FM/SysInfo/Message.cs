using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysInfo
{
    /// <summary>
    /// 通讯消息类
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Success;

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg;

        /// <summary>
        /// 结果
        /// </summary>
        public object Obj;
    }
}