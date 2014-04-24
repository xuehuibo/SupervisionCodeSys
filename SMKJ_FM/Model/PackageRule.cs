using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class PackageRule
    {
        public string ID;
        public string PackageRuleName
        {
            get;
            set;
        }
        public int LevelCount
        {
            get
            {
                return PackageRule_Item==null?0:PackageRule_Item.Count;
            }
        }
        public StatusEnum Status
        {
            get;
            set;
        }
        public string StatusStr
        {
            get
            {
                return Status.ToString();
            }
        }
        public string CascadeCode
        {
            get;
            set;
        }
        public User CreateUser
        {
            get;
            set;
        }
        public string CreateDate
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public List<PackageRuleItem> PackageRule_Item
        {
            get;
            set;
        }
    }

    public class PackageRuleItem
    {
        public string PackageRuleID
        {
            get;
            set;
        }
        public short LevelNo
        {
            get;
            set;
        }
        public short Amount
        {
            get;
            set;
        }
        public short PrintAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 打印模板id
        /// </summary>
        public string PrintTemplateID
        {
            get;
            set;
        }
        public string PackCodeRuleID
        {
            get;
            set;
        }
        public string CodeAmount
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
    }
}