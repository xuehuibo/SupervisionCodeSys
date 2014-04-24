using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProductCategory
    {
        public string ID
        {
            get;
            set;
        }

        public string CategoryCode
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        public GetCodeTypeEnum GetCodeType
        {
            get;
            set;
        }

        public string GetCodeTypeStr
        {
            get
            {
                return GetCodeType.ToString();
            }
        }

        public List<DetailType> DetailTypes
        {
            get;
            set;
        }
    }
    public enum GetCodeTypeEnum
    {
        包装码区分最小包装 = 0,
        包装码没有区分 = 1,
        包装码区分全部级别 = 2
    }
}

