using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class EquipmentSetting
    {
        public string ID;
        /// <summary>
        /// 设备类型
        /// </summary>
        public EquipmentSettingCategoryEnum Category
        {
            get;
            set;
        }
        public string CategoryStr
        {
            get
            {
                return Category.ToString();
            }
        }
        /// <summary>
        /// 输入输出类型
        /// </summary>
        public EquipmentSettingInOutType InOutType
        {
            get;
            set;
        }
        public string InOutTypeStr
        {
            get
            {
                return InOutType.ToString();
            }
        }
        /// <summary>
        /// 驱动类型
        /// </summary>
        public EquipmentSettingDriverType DriverType
        {
            get;
            set;
        }
        public string DriverTypeStr
        {
            get
            {
                return DriverType.ToString();
            }
        }
        /// <summary>
        /// 调用类名称
        /// </summary>
        public string FullClassName
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

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
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
    }

    public enum EquipmentSettingCategoryEnum
    {
        电眼=0,
        工位显示器=2,
        条码扫描器=1,
        打印机=3,
        剔除设备=4
    }

    public enum EquipmentSettingInOutType
    {
        输入设备=0,
        输出设备=1
    }

    public enum EquipmentSettingDriverType{
        串口=0,
        网口=1,
        并口=2,
        驱动=3
    }
}