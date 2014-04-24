using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProductRes
    {
        public ProductRes()
        {
        }

        public ProductRes(short LevelNo, string CodeVersion, string ProductResCode, string PackageSpecID, string ProductID)
        {
            this.LevelNo = LevelNo;
            this.CodeVersion = CodeVersion;
            this.ProductResCode = ProductResCode;
            this.PackageSpec = new PackageSpecific();
            this.PackageSpec.ID = PackageSpecID;
            this.ProductID = ProductID;
        }
       
        /// <summary>
        /// 资源码ID
        /// </summary>
        public string ID;

        /// <summary>
        /// 资源码级别
        /// </summary>
        public short LevelNo
        {
            get;
            set;
        }

        /// <summary>
        /// 码版本
        /// </summary>
        public string CodeVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 资源码
        /// </summary>
        public string ProductResCode
        {
            get;
            set;
        }

        /// <summary>
        /// 包装规格ID
        /// </summary>
        public PackageSpecific PackageSpec
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
    }
}