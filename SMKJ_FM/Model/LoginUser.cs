using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class LoginUser
    {
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户角色
        /// </summary>
        public Role UserRole
        {
            get;
            set;
        }

        /// <summary>
        /// 用户组织
        /// </summary>
        public Organization UserOrg
        {
            get;
            set;
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UPassword
        {
            get;
            set;
        }
    }
}