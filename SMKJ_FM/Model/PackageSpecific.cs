using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class PackageSpecific
    {
        public string ID;

        /// <summary>
        /// 包装比例
        /// </summary>
        public string PackageRatio
        {
            get;
            set;
        }

        /// <summary>
        /// 包装规格
        /// </summary>
        public string PackageSpec
        {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID
        {
            get;
            set;
        }


        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}