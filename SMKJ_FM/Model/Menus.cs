using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Menus
    {
        /// <summary>
        /// 节点的ID   
        /// </summary>
        public string id
        {
            get;
            set;
        }
        /// <summary>
        /// 节点显示的文字
        /// </summary>
        public string text
        {
            get;
            set;
        }
        /// <summary>
        /// 节点状态，有两个值  'open' or 'closed', 默认为'open'. 
        /// 当为‘closed’时说明此节点下有子节点否则此节点为叶子节点
        /// </summary>
        public string state
        {
            get;
            set;
        }
        /// <summary>
        /// 节点中其他属性的集合
        /// </summary>
        public MenuAttributes attributes
        {
            get;
            set;
        }
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<Menus> children
        {
            get;
            set;
        }

        /// <summary>
        /// 权限码
        /// </summary>
        public string RightFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Int16 Status
        {
            get;
            set;
        }

        /// <summary>
        /// 状态枚举
        /// </summary>
        public string StatusE
        {
            get
            {
                return ((StatusEnum)Status).ToString();
            }
        }

        /// <summary>
        /// 修改日期
        /// </summary>
        public string XgDate
        {
            get;
            set;
        }

       
    }
}