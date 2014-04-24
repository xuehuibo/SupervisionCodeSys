using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class Role
    {
        public Role()
        {
        }

        public Role(string _ID,string _RoleCode,string _RoleName,string _Status )
        {
            this.ID = string.IsNullOrEmpty(_ID)?"":_ID;
            this.RoleCode = _RoleCode;
            this.RoleName = _RoleName;
            this.Status =(StatusEnum)short.Parse( _Status);
        }
        public string ID
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
        /// 状态
        /// </summary>
        public StatusEnum Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态枚举
        /// </summary>
        public string StatusStr
        {
            get
            {
                return ((StatusEnum)Status).ToString();
            }
        }
        /// <summary>
        /// 角色菜单
        /// </summary>
        public List<Menus> RoleMenu
        {
            get;
            set;
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