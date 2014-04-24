using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// 工位
    /// </summary>
    public class WorkCenter
    {
        public Guid ID;

        public Guid ProductLineID
        {
            get;
            set;
        }

        public string WorkCenterCode
        {
            get;
            set;
        }

        public short LevelNo
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        public string PropertyObj
        {
            get;
            set;
        }

        public string X
        {
            get;
            set;
        }
        public string Y
        {
            get;
            set;
        }

        public Guid PreWorkCenterID
        {
            get;
            set;
        }

        public Guid PostWorkCenterID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备表
        /// </summary>
        public List<Equipment> Equipments;

        /// <summary>
        /// 工位数据
        /// </summary>
        public WorkCenterData Data
        {
            get;
            set;
        }
    }
}