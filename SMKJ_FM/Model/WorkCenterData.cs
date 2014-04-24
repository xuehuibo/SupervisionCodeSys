using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class WorkCenterData
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public string TaskID
        {
            get;
            set;
        }

        /// <summary>
        /// 工位ID
        /// </summary>
        public Guid WorkCenterID
        {
            get;
            set;
        }

        /// <summary>
        /// 已扫描数量
        /// </summary>
        public long CurrentNum
        {
            get;
            set;
        }

        /// <summary>
        /// 已完成数量
        /// </summary>
        public long FinishedNum
        {
            get;
            set;
        }

        /// <summary>
        /// 已包装数量
        /// </summary>
        public long PackageNum
        {
            get;
            set;
        }

        /// <summary>
        /// 剔除数量
        /// </summary>
        public long KickoutNum
        {
            get;
            set;
        }

    }
}
