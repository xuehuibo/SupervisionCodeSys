using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ProductLine
    {
        public Guid ID;

        public WorkComputer Work_Computer
        {
            get;
            set;
        }

        public string LineCode
        {
            get;
            set;
        }

        public string LineName
        {
            get;
            set;
        }

        public Models.StatusEnum Status
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

        public string ManageUser
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        public string WorkShop
        {
            get;
            set;
        }
    }
}