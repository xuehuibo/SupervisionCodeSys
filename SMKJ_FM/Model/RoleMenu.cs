using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class RoleMenu
    {

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单状态
        /// </summary>
        public string Allowed
        {
            get;
            set;
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string XgDate
        {
            get;
            set;
        }
        /// <summary>
        /// 权限码
        /// </summary>
        public string RightFlag
        {
            get
            {
                return string.Concat(new string[] {
                    Insert=="Y"?"1":"0",
                    Delete=="Y"?"1":"0",
                    Update=="Y"?"1":"0",
                    Select=="Y"?"1":"0",
                    Approve=="Y"?"1":"0"
                });
            }
            set
            {
                if (value.Length == 5)
                {
                    Insert = value[0] == '1' ? "Y" : "N";
                    Delete = value[1] == '1' ? "Y" : "N";
                    Update = value[2] == '1' ? "Y" : "N";
                    Select = value[3] == '1' ? "Y" : "N";
                    Approve = value[4] == '1' ? "Y" : "N";
                }
                else
                {
                    Insert ="N";
                    Delete = "N";
                    Update = "N";
                    Select = "N";
                    Approve = "N";
                }
            }
        }
        /*****增删改查审核权限*****/
        /// <summary>
        /// 增加权限
        /// </summary>
        public string Insert
        {
            get;
            set;
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        public string Delete
        {
            get;
            set;
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        public string Update
        {
            get;
            set;
        }
        /// <summary>
        /// 查询权限
        /// </summary>
        public string Select
        {
            get;
            set;
        }
        /// <summary>
        /// 审核权限
        /// </summary>
        public string Approve
        {
            get;
            set;
        }



        /// <summary>
        /// 展开状态
        /// </summary>
        public string state
        {
            get;
            set;
        }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<RoleMenu> children
        {
            get;
            set;
        }
    }
}