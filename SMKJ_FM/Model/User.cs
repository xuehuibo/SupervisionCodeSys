using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Models
{
    /// <summary>
    /// 用户设置中 用户显示模板
    /// </summary>
    public class User
    {
        public User()
        {
        }
        public User(string ID,string UserCode,string UserName,string OrgCode,string RoleCode,string Status)
        {
            this.ID = string.IsNullOrEmpty(ID)?"":ID;
            this.UserCode = UserCode;
            this.UserName = UserName;
            this.OrgCode = OrgCode;
            this.RoleCode = RoleCode;
            this.Status = (StatusEnum)short.Parse(Status);
        }
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
        /// 角色编码
        /// </summary>
        public string RoleCode
        {
            get;
            set;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string UPassword
        {
            get;
            set;
        }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrgCode
        {
            get;
            set;
        }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态显示字符串
        /// </summary>
        public string StatusStr
        {
            get
            {
                return Status.ToString();
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string XgDate
        {
            get;
            set;
        }
    }
}