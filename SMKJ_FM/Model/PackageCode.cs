using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    /// <summary>
    /// 监管码
    /// </summary>
    public class PackageCode
    {
        /// <summary>
        /// 监管码
        /// </summary>
        public string PackCode
        {
            get;
            set;
        }
        /// <summary>
        /// 包装规格ID
        /// </summary>
        public string PackSpecID
        {
            get;
            set;
        }
        /// <summary>
        /// 包装级别ID
        /// </summary>
        public short LevelNo
        {
            get;
            set;
        }
        /// <summary>
        /// 码状态
        /// </summary>
        public CodeStatuFlag Status
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
        /// <summary>
        /// 父级码
        /// </summary>
        public string ParentPackCode
        {
            get;
            set;
        }

        /// <summary>
        /// 打印数量
        /// </summary>
        public short PrintNum
        {
            get;
            set;
        }

        /// <summary>
        /// 任务ID
        /// </summary>
        public string TaskID
        {
            get;
            set;
        }

        /// <summary>
        /// 打印批号
        /// </summary>
        public string PrintBatchNo
        {
            get;
            set;
        }

        /// <summary>
        /// 包装标志
        /// </summary>
        public CodePackEnum PackFlag
        {
            get;
            set;
        }

        public string PackFlagStr
        {
            get
            {
                return PackFlag.ToString();
            }
        }

        /// <summary>
        /// 打印标志
        /// </summary>
        public CodePrintFlag PrintFlag
        {
            get;
            set;
        }

        public string PrintFlagStr
        {
            get
            {
                return PrintFlag.ToString();
            }
        }
        /// <summary>
        /// 包装组编号
        /// </summary>
        public string PackGroupNo
        {
            get;
            set;
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string EquipmentID
        {
            get;
            set;
        }

        /// <summary>
        /// 扫描编号
        /// </summary>
        public string ScanNO
        {
            get;
            set;
        }

        /// <summary>
        /// 托盘编号
        /// </summary>
        public string BoxNo
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 工位ID
        /// </summary>
        public string WorkCenterID
        {
            get;
            set;
        }

        /// <summary>
        /// 双工位ID
        /// </summary>
        public string DoubleWorkCenter
        {
            get;
            set;
        }

        /// <summary>
        /// 包装任务ID
        /// </summary>
        public string PackTaskID
        {
            get;
            set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 导出
        /// </summary>
        public string ExportGuid
        {
            get;
            set;
        }

        /// <summary>
        /// 任务锁定
        /// </summary>
        public short LockedByTask
        {
            get;
            set;
        }

        /// <summary>
        /// 替换码
        /// </summary>
        public string ReplaceCode
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

    /// <summary>
    /// 包装标志
    /// </summary>
    public enum CodePackEnum
    {
        整箱=1,
        零箱=2,
        散装=3,
        拼箱=4
    }

    /// <summary>
    /// 打印标志
    /// </summary>
    public enum CodePrintFlag
    {
        未打印=0,
        已打印=1
    }

    /// <summary>
    /// 码状态
    /// </summary>
    public enum CodeStatuFlag
    {
        未使用=0,
        已使用=1,
        散装=2,
        作废=-1
    }
}