using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysInfo
{
    public class SysMessageTxt
    {
        /// <summary>
        /// 系统故障
        /// </summary>
        public static string SYS_ERROR_TXT = "系统故障！请联系管理员！";

        /// <summary>
        /// 登录超时
        /// </summary>
        public static string SYS_LOGIN_TIMEOUT = "用户登陆超时！请重新<a href=''>登录</a>！";

        /// <summary>
        /// 保存失败
        /// </summary>
        public static string SYS_SAVE_FAILED = "保存失败！";

        /// <summary>
        /// 保存成功
        /// </summary>
        public static string SYS_SAVE_SUCCESS = "保存成功！";

        /// <summary>
        /// 登陆成功
        /// </summary>
        public static string SYS_LOGIN_SUCCESS = "登陆成功！";

        /// <summary>
        /// 登陆失败
        /// </summary>
        public static string SYS_LOGIN_FAILED = "登陆失败！用户名或密码错误！";

        /// <summary>
        /// 检索成功
        /// </summary>
        public static string SYS_SEARCH_SUCCESS = "检索成功!";

        /// <summary>
        /// 检索失败
        /// </summary>
        public static string SYS_SEARCH_FAILED = "检索失败!";
        /// <summary>
        /// 删除失败
        /// </summary>
        public static string SYS_DELETE_FAILED = "删除失败!";

        /// <summary>
        /// 删除成功
        /// </summary>
        public static string SYS_DELETE_SUCCESS = "删除成功!";

        /// <summary>
        /// 没有权限
        /// </summary>
        public static string SYS_NO_RIGHT = "没有该操作权限!";
    }
}