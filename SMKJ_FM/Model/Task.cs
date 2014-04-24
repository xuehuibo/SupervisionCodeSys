using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Task
    {
        public string ID
        {
            get;
            set;
        }

        public Models.ProductLine ProductLine
        {
            get;
            set;
        }

        public string TaskCode
        {
            get;
            set;
        }

        public TaskStatus Status
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
        public Models.PackageRule PackageRule
        {
            get;
            set;
        }
        public Models.Product Product
        {
            get;
            set;
        }
        public Models.PackageSpecific PackageSpec
        {
            get;
            set;
        }
        public string BatchNo
        {
            get;
            set;
        }
        public long PlanAmount
        {
            get;
            set;
        }
        public long TaskAmount
        {
            get;
            set;
        }
        public long DoneAmount
        {
            get;
            set;
        }
        public Models.User CreateUser
        {
            get;
            set;
        }
        public string CreateDate
        {
            get;
            set;
        }
        public string StartDate
        {
            get;
            set;
        }

        public string EndDate
        {
            get;
            set;
        }
        public string ProductDate
        {
            get;
            set;
        }
        public string InvalidDate
        {
            get;
            set;
        }
        public Models.User AuditUser
        {
            get;
            set;
        }
        public string AuditDate
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

    public enum TaskStatus
    {
        未审核=0,
        已审核=1,
        运行中=2,
        已暂停=3,
        已结束=4,
        已返工=5
    }
}