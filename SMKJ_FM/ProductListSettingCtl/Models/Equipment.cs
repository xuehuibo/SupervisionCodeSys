using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        private string stopBits;
        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBits
        {
            get
            {
                if (string.IsNullOrEmpty(stopBits))
                {
                    return "1";
                }
                else
                {
                    return stopBits;
                }
            }
            set
            {
                stopBits = value;
            }
        }

        private string parity;
        /// <summary>
        /// 校验位
        /// </summary>
        public string Parity
        {
            get
            {
                if (string.IsNullOrEmpty(parity))
                {
                    return "0";
                }
                else
                {
                    return parity;
                }
            }
            set
            {
                parity = value;
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
}