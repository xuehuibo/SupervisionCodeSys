using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Ports;

namespace Models
{
    public class Equipment
    {
        public Guid ID;

        /// <summary>
        /// 设备设置
        /// </summary>
        public EquipmentSetting Setting
        {
            get;
            set;
        }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode
        {
            get;
            set;
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get;
            set;
        }

        /// <summary>
        /// 端口
        /// </summary>
        public string Port
        {
            get;
            set;
        }

        private string baudRate;
        /// <summary>
        /// 波特率
        /// </summary>
        public string BaudRate
        {
            get
            {
                if (string.IsNullOrEmpty(baudRate))
                {
                    return "9600";
                }
                else
                {
                    return baudRate;
                }
            }
            set
            {
                baudRate = value;
            }
        }

        private string dataBits;
        /// <summary>
        /// 数据位
        /// </summary>
        public string DataBits
        {
            get
            {
                if (string.IsNullOrEmpty(dataBits))
                {
                    return "8";
                }
                else
                {
                    return dataBits;
                }
            }
            set
            {
                dataBits = value;
            }
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBitsEnum StopBits
        {
            get;
            set;
        }

        public string StopBitsStr
        {
            get
            {
                return StopBits.ToString();
            }
        }

        /// <summary>
        /// 校验位
        /// </summary>
        public ParityEnum Parity
        {
            get;
            set;
        }
        public string ParityStr
        {
            get
            {
                return Parity.ToString();
            }
        }

        public int Delay
        {
            get;
            set;
        }
        /// <summary>
        /// 参数对象
        /// </summary>
        public string PropertyObj
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

    /// <summary>
    /// 
    /// </summary>
    public enum StopBitsEnum
    {
        使用一个停止位=1,
        使用两个停止位=2
    }

    public enum ParityEnum
    {
        不发生奇偶校验检查=0,
        将奇偶校验位保留为1 = 1,
        设置奇偶校验位使位数等于偶数=2,
        设置奇偶校验位使位数等于奇数=3,
        将奇偶校验位保留为0=4
    }
}