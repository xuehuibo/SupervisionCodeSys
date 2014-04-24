using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace SMKJ_FM.Controllers
{
    public class RoleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增Role对象
        /// </summary>
        /// <param name="ID">Role ID</param>
        /// <param name="RoleCode">Role编码</param>
        /// <param name="RoleName">Role名称</param>
        /// <param name="Status">Role状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string ID, string RoleCode, string RoleName, string Status)
        {
            Role role = new Role(ID, RoleCode, RoleName, Status);
            return Save(role);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string ID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.RoleDAL dal = new DAL.RoleDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success=dal.Delete(ID, out msg.Msg);
            }
            JsonResult jsr = Json(msg);
            jsr.ContentType = "text/html";
            return jsr;

        }

        /// <summary>
        /// 修改Role对象
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="RoleCode"></param>
        /// <param name="RoleName"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string ID, string RoleCode, string RoleName, string Status)
        {
            Role role = new Role(ID, RoleCode, RoleName, Status);
            return Save(role);
        }
        /// <summary>
        /// 查询Role对象
        /// </summary>
        /// <param name="RoleCode">角色编码</param>
        /// <param name="RoleName">角色名称</param>
        /// <param name="Status">角色状态</param>
        /// <param name="XgDateBegin">修改时间开始</param>
        /// <param name="XgDateEnd">修改时间结束</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string RoleCode,string RoleName,string Status,string XgDateBegin,string XgDateEnd,string page,string rows)
        {
            List<SysInfo.Param> paramList=new List<SysInfo.Param>();
            paramList.Add(new SysInfo.Param("@RoleCode",RoleCode==null?"":RoleCode));
            paramList.Add(new SysInfo.Param("@RoleName",RoleName==null?"":RoleName));
            paramList.Add(new SysInfo.Param("@Status",Status==null?"":Status));
            paramList.Add(new SysInfo.Param("@XgDateBegin",XgDateBegin==null?"":XgDateBegin));
            paramList.Add(new SysInfo.Param("@XgDateEnd",XgDateEnd==null?"":XgDateEnd));

            JsonResult jsr;
            string msg;
            SysInfo.DatagridPage<Models.Role> dgpage = new SysInfo.DatagridPage<Models.Role>();

            using(DAL.RoleDAL dal=new DAL.RoleDAL(SysInfo.SysSetting.DBCCN)){
                dal.Select(paramList,int.Parse(page),int.Parse(rows),out dgpage.rows,out dgpage.total,out msg);
            }
            jsr=Json(dgpage);
            jsr.ContentType="text/html";
            return jsr;

        }

        /// <summary>
        /// 保存Role对象
        /// </summary>
        /// <param name="ID">对象ID</param>
        /// <param name="RoleCode">角色编码</param>
        /// <param name="RoleName">角色名称</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        private JsonResult Save(Models.Role role)
        {
            JsonResult jsr;
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.RoleDAL dal = new DAL.RoleDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Save(role, out msg.Msg);
            }
            jsr = Json(msg);
            jsr.ContentType = "text/html";
            return jsr;
        }
    }
}
