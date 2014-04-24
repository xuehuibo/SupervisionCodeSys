using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SysInfo
{
    public delegate bool ScanCodeDelegate(Models.WorkCenter WorkCenter, string[] Codes);//扫描器触发处理程序

    public delegate bool LingxiangDelegate(Models.WorkCenter WorkCenter);//零箱操作

    public delegate bool PinxiangDelegate(Models.WorkCenter WorkCenter);//拼箱操作

    public delegate bool SanzhuangDelegate(Models.WorkCenter WorkCenter);//散装操作
}
