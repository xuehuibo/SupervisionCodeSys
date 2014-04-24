using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Configuration;
using SysInfo;
using Models;
namespace SMKJ_FM.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View();
        }


        /// <summary>
        /// 检索用户
        /// </summary>
        /// <param name="UserCode">用户编码</param>
        /// <param name="UserName">用户名称</param>
        /// <param name="Role">角色</param>
        /// <param name="OrgCode">组织</param>
        /// <param name="XgDateBegin">修改时间开始</param>
        /// <param name="XgDateEnd">修改时间结束</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页号</param>
        /// <param name="rows">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string UserCode, string UserName, string RoleCode, string OrgCode, string XgDateBegin, string XgDateEnd,
            string Status, string page, string rows)
        {
            #region 组装参数
            List<Param> paramList = new List<Param>();
            paramList.Add(new Param("@UserCode", string.IsNullOrEmpty(UserCode)?"":UserCode));
            paramList.Add(new Param("@UserName", string.IsNullOrEmpty(UserName)?"":UserCode));
            paramList.Add(new Param("@RoleCode", string.IsNullOrEmpty(RoleCode)?"":RoleCode));
            paramList.Add(new Param("@OrgCode", string.IsNullOrEmpty(OrgCode)?"":OrgCode));
            paramList.Add(new Param("@XgDateBegin", string.IsNullOrEmpty(XgDateBegin)?"":XgDateBegin));
            paramList.Add(new Param("@XgDateEnd", string.IsNullOrEmpty(XgDateEnd)?"":XgDateEnd));
            paramList.Add(new Param("@Status", Status==null?"":Status));
            #endregion
            SysInfo.DatagridPage<User> rstPage = new DatagridPage<User>();
            string msg;
            using (UserDAL dal = new UserDAL(SysSetting.DBCCN))
            {
                dal.Select(paramList,int.Parse(page),int.Parse(rows), out rstPage.rows, out rstPage.total, out msg);
            }

            JsonResult js = Json(rstPage);
            js.ContentType = "text/html";
            return js;

        }


        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="UserCode">用户编码</param>
        /// <param name="UserName">用户名称</param>
        /// <param name="RoleCode">角色编码</param>
        /// <param name="OrgCode">组织编码</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string ID,string UserCode,string UserName,string RoleCode,string OrgCode,string Status)
        {
            Models.User user = new User(ID, UserCode, UserName, OrgCode, RoleCode, Status);
            return Save(user);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="UserCode">用户编码</param>
        /// <param name="UserName">用户名称</param>
        /// <param name="RoleCode">角色编码</param>
        /// <param name="OrgCode">组织编码</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string ID, string UserCode, string UserName, string RoleCode, string OrgCode, string Status)
        {
            Models.User user = new User(ID, UserCode, UserName, OrgCode, RoleCode, Status);
            return Save(user);
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private JsonResult Save(Models.User user)
        {
            Message msg = new Message();
            using (DAL.UserDAL dal = new UserDAL(SysSetting.DBCCN))
            {
                msg.Success = dal.Save(user, out msg.Msg);
            }
            JsonResult js = Json(msg);
            js.ContentType = "text/html";
            return js;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string ID)
        {
            SysInfo.Message msg = new Message();
            using (DAL.UserDAL dal = new UserDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string userCode, string uPassword, string orgCode)
        {
            using (UserLoginDAL dal = new UserLoginDAL(SysInfo.SysSetting.DBCCN))
            {
                Message msg = new Message();
                LoginUser user;
                user = dal.Login(userCode, uPassword, orgCode, out msg.Success, out msg.Msg);
                msg.Obj = user;

                if (msg.Success)
                {
                    Session.Add("User", msg.Obj);
                }
                JsonResult jr = Json(msg);
                jr.ContentType = "text/html";
                return jr;
            }
        }


        /// <summary>
        /// 用户注销
        /// </summary>
        /// <returns></returns>
        public RedirectResult Logoff()
        {
            try
            {
                Session.Remove("User");
            }
            catch
            {

            }
            return Redirect("~/Home");
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangePassword(string oldPwd, string newPwd)
        {
            Message msg = new Message();
            JsonResult jr;
            string userId = ((LoginUser)Session["User"]).ID;
            using (UserLoginDAL dal = new UserLoginDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.ChangePassword(userId, oldPwd, newPwd, out msg.Msg);
            }
            jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
