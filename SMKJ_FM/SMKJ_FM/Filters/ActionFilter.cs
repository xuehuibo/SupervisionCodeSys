using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysInfo;
using DAL;
using System.Configuration;
using SMKJ_FM.Filters;
using Models;
namespace SMKJ_FM.Filters
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple=false)]
    public class ActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 操作前过滤
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Message message = new Message();
            message.Success = RightJudger.Judge(new ActionExecutingContextInfo(filterContext), ref message.Msg);//判断权限
            if (!message.Success)
            {
                //没有权限
                JsonResult jr = new JsonResult();
                jr.Data = message;
                jr.ContentType = "text/html";
                filterContext.Result = jr;
            }
        }

        /// <summary>
        /// 操作后过滤
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            using (ActionLogsDAL dal = new ActionLogsDAL(SysInfo.SysSetting.DBCCN))
            {
                ResultExecutedContextInfo fi = new ResultExecutedContextInfo(filterContext);
                switch (filterContext.Result.GetType().Name)
                {
                    case "ViewResult":
                        break;
                    case "JsonResult":
                        if (fi.DataType.StartsWith("Message"))
                        {
                            if (fi.User != null)
                            {
                                //message类型 记录入数据库
                                ActionLogs log = new ActionLogs();
                                log.User = fi.User;
                                log.Controller = fi.ControllerName;
                                log.Action = fi.ActionName;
                                log.success = ((Message)fi.Data).Success;
                                log.info = ((Message)fi.Data).Msg;
                                string msg;
                                if (!dal.Save(log, out msg))
                                {
                                    LogWriter.WriteErrorLog(dal.GetType().ToString(), "Save", msg);
                                }
                            }
                        }
                        break;
                    case "RedirectResult":
                        break;

                }
            }
        }
    }

    /// <summary>
    /// ActionExecutingContext信息
    /// </summary>
    class ActionExecutingContextInfo
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName
        {
            get;
            set;
        }

        /// <summary>
        /// 模块名
        /// </summary>
        public string Module
        {
            get;
            set;
        }

        /// <summary>
        /// Controller名
        /// </summary>
        public string ControllerName
        {
            get;
            set;
        }

        /// <summary>
        /// Action名
        /// </summary>
        public string ActionName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户
        /// </summary>
        public LoginUser User
        {
            get;set;
        }

        /// <summary>
        /// 初始化Filter信息
        /// </summary>
        /// <param name="filterContext"></param>
        public ActionExecutingContextInfo(ActionExecutingContext filterContext)
        {
            DomainName = filterContext.HttpContext.Request.Url.Authority;
            Module = filterContext.HttpContext.Request.Url.Segments[0];
            User = filterContext.HttpContext.Session["User"] as LoginUser;
            ControllerName = filterContext.RouteData.Values["controller"].ToString();
            ActionName = filterContext.RouteData.Values["action"].ToString();
            if (User == null && ActionName =="Upload")
            {
                using (UserLoginDAL dal = new UserLoginDAL(SysInfo.SysSetting.DBCCN))
                {
                    try
                    {
                        User = dal.Get(filterContext.HttpContext.Request["UserID"].ToString());
                    }
                    catch { }
                }
            }
        }
    }

    /// <summary>
    /// ResultExecutedContext信息
    /// </summary>
    class ResultExecutedContextInfo
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName
        {
            get;
            set;
        }

        /// <summary>
        /// 模块名
        /// </summary>
        public string Module
        {
            get;
            set;
        }

        /// <summary>
        /// Controller名
        /// </summary>
        public string ControllerName
        {
            get;
            set;
        }

        /// <summary>
        /// Action名
        /// </summary>
        public string ActionName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户
        /// </summary>
        public LoginUser User
        {
            get;
            set;
        }

        /// <summary>
        /// jsonresult 返回的信息的类型
        /// </summary>
        public string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// jsonresult 中返回的信息
        /// </summary>
        public object Data
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化Filter信息
        /// </summary>
        /// <param name="filterContext"></param>
        public ResultExecutedContextInfo(ResultExecutedContext filterContext)
        {
            DomainName = filterContext.HttpContext.Request.Url.Authority;
            Module = filterContext.HttpContext.Request.Url.Segments[0];
            User = filterContext.HttpContext.Session["User"] as LoginUser;
            if (filterContext.Result.GetType().Name == "JsonResult")
            {
                //获取返回数据的类型
                Data = ((JsonResult)filterContext.Result).Data ;
                DataType=Data.GetType().Name;

            }
            ControllerName = filterContext.RouteData.Values["controller"].ToString();
            ActionName = filterContext.RouteData.Values["action"].ToString();
            if (User == null && ActionName=="Upload")
            {
                using (UserLoginDAL dal = new UserLoginDAL(SysInfo.SysSetting.DBCCN))
                {
                    try
                    {
                        User = dal.Get(filterContext.HttpContext.Request["UserID"].ToString());
                    }
                    catch
                    {

                    }
                }
            }
        }
    }

    /// <summary>
    /// 权限判断
    /// </summary>
    class RightJudger
    {
        /// <summary>
        /// 忽略判断权限 的操作名称列表
        /// </summary>
        private static string[] IGNORE_RIGHTJUDGE_OPERATIONS_LIST = new string[] {
            "Home/*",
            "Users/Login",
            "Users/Logoff",
            "Users/ChangePassword",
            "*/Index",
            "RoleMenu/*",
            "SysRes/*"
        };

        /// <summary>
        /// 判断是否需要权限判断
        /// </summary>
        /// <param name="_RightName"></param>
        /// <returns></returns>
        private static bool NeedJudge(ActionExecutingContextInfo fi)
        {
            int ok = 0;
            foreach (string str in IGNORE_RIGHTJUDGE_OPERATIONS_LIST)
            {
                ok = 0;
                string[] strs = str.Split('/');
                if (fi.ControllerName == strs[0]||strs[0]=="*")
                {
                    ok++;
                }
                if (fi.ActionName == strs[1] || strs[1] == "*")
                {
                    ok++;
                }
                if (ok == 2)
                {
                    break;
                }
                else
                {
                    ok = 1;
                }
            }

            return !(ok == 2);
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="action">Action名称</param>
        /// <returns></returns>
        public static bool Judge(ActionExecutingContextInfo fi,ref string msg)
        {
            if (!NeedJudge(fi))
            {
                //操作在忽略列表中
                return true;
            }
            if (fi.User == null )
            {//登陆超时
                msg = SysInfo.SysMessageTxt.SYS_LOGIN_TIMEOUT;
                return false;
            }
                
                bool ok =FindRight(fi.User.ID, fi.ControllerName, fi.ActionName);
                if (!ok)
                {
                    msg = SysInfo.SysMessageTxt.SYS_NO_RIGHT;
                }
                return ok;
        }


        /// <summary>
        /// 检索菜单找到对应的权限选项
        /// </summary>
        /// <returns></returns>
        private static bool FindRight(string userID, string controller, string action)
        {
            string rightFlag;
            using (DAL.RoleDAL dal = new RoleDAL(SysInfo.SysSetting.DBCCN))
            {
                rightFlag = dal.GetRightFlag(userID, controller);
            }
            return GetRightFlag(rightFlag, action);
        }

        /// <summary>
        /// 读出对应的权限标志，如果没有则返回null
        /// </summary>
        /// <param name="menu">菜单</param>
        /// <param name="action">动作</param>
        /// <returns></returns>
        private static bool GetRightFlag(string  RightFlag, string action)
        {
            switch (action)
            {
                case "Upload"://上传判断增加权限
                case "Insert":
                    return RightFlag.Substring(0, 1).Equals("1");

                case "Delete":
                    return RightFlag.Substring(1, 1).Equals("1");

                case "Update":
                    return RightFlag.Substring(2, 1).Equals("1");

                case "Select":
                    return RightFlag.Substring(3, 1).Equals("1");
                case "Approve"://增删改差以外的权限 判断审核权限
                default:
                    return RightFlag.Substring(4, 1).Equals("1");
            }
        }
    }
}

