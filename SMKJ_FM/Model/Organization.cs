using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// 组织
    /// </summary>
    public class Organization
    {
        public Organization()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id">组织ID 如果为空ID将置-1</param>
        /// <param name="_orgCode">组织代码</param>
        /// <param name="_orgName">组织名称</param>
        /// <param name="_orgType">组织类型</param>
        /// <param name="_orgStatus">组织状态</param>
        public Organization(string _id, string _orgCode, string _orgName, string _orgType, string _orgStatus)
        {
            ID = _id;
            OrgCode = _orgCode;
            OrgName = _orgName;
            OrgType = (OrgTypeEnum)short.Parse(_orgType);
            Status = (StatusEnum)short.Parse(_orgStatus);

        }

        public string ID
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

        #region 组织在combotree中的属性
        /// <summary>
        /// 
        /// </summary>
        public string id
        {
            get
            {
                return OrgCode;
            }
        }

        public string text
        {
            get
            {
                return OrgName;
            }
        }
        #endregion
     
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织类型
        /// </summary>
        public OrgTypeEnum OrgType
        {
            get;
            set;
        }

        /// <summary>
        /// 组织类型枚举
        /// </summary>
        public string OrgTypeE
        {
            get
            {
                return OrgType.ToString();
            }
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
        public String StatusStr
        {
            get
            {
                return ((StatusEnum)Status).ToString();
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

        /// <summary>
        /// 下属组织
        /// </summary>
        public List<Organization> children
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

    }

    /// <summary>
    /// 组织类型枚举
    /// </summary>
    public enum OrgTypeEnum
    {
        系统组织 = 0,
        管理部门 = 1,
        营业部门 = 2,
        财务部门 = 3,
        其他部门 = 4
    }
}