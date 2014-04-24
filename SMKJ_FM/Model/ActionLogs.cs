using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class ActionLogs
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        public LoginUser User
        {
            get;
            set;
        }
        /// <summary>
        /// 模块
        /// </summary>
        public string Controller
        {
            get;
            set;
        }
        /// <summary>
        /// 动作
        /// </summary>
        public string Action
        {
            get;
            set;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success
        {
            get;
            set;
        }
        /// <summary>
        /// 信息
        /// </summary>
        public string info
        {
            get;
            set;
        }
    }
}