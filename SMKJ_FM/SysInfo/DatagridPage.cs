using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysInfo
{
    /// <summary>
    /// 页模型
    /// </summary>
    public class DatagridPage<T>
    {
        public int total;
        public List<T> rows;
    }
}