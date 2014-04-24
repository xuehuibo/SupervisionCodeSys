using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysInfo;
using DAL;
using System.Configuration;
using Models;
namespace SMKJ_FM.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        /// <summary>
        /// 获取主容器
        /// </summary>
        /// <returns></returns>
        public ViewResult MainTabs()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单栏
        /// </summary>
        /// <returns></returns>
        public ViewResult Menus()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenusTree()
        {
            LoginUser user = Session["User"] as LoginUser;
            JsonResult jr = Json(user.UserRole.RoleMenu);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
