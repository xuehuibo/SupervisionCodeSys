using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Product
    {
        public string ID;

        public string ProductCode
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }

        public string SubNo
        {
            get;
            set;
        }

        public string SubName
        {
            get;
            set;
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

        public string ProductType
        {
            get;
            set;
        }

        public string Spec
        {
            get;
            set;
        }

        public string PermitNo
        {
            get;
            set;
        }

        public string PackSpec
        {
            get;
            set;
        }

        public ProductCategory Product_Category
        {
            get;
            set;
        }

        public DetailType Detail_Type
        {
            get;
            set;
        }

        public short CodeLen
        {
            get;
            set;
        }

        public int DocID
        {
            get;
            set;
        }

        public short ValidTime
        {
            get;
            set;
        }

        public ValidTimeUnitEnum ValidTimeUnit
        {
            get;
            set;
        }
        public string ValidTimeUnitStr
        {
            get
            {
                return ValidTimeUnit.ToString();
            }
        }

        public string PackUnit
        {
            get;
            set;
        }

        public string CreateUserID
        {
            get;
            set;
        }

        public string CreateUserName
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

    }

    public enum ValidTimeUnitEnum
    {
        天=0,
        月=1,
        年=2
    }
}