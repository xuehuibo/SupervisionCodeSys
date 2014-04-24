using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
namespace SMKJ_FM.Controllers
{
    public class RoleMenuController : Controller
    {
        /// <summary>
        /// 查询角色菜单
        /// </summary>
        /// <param name="RoleCode">角色编码</param>
        /// <param name="page">页号</param>
        /// <param name="rows">页大小</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string RoleCode,string page,string rows)
        {
            SysInfo.DatagridPage<RoleMenu> pg=new SysInfo.DatagridPage<RoleMenu>();
            using (DAL.RoleMenuDAL dal = new DAL.RoleMenuDAL(SysInfo.SysSetting.DBCCN))
            {
                List<SysInfo.Param> pList=new List<SysInfo.Param>();
                pList.Add(new SysInfo.Param("@RoleCode",RoleCode));
                string msg;
                dal.Select(pList, int.Parse(page), int.Parse(rows), out pg.rows, out pg.total,out  msg);
            }
            JsonResult js = Json(pg);
            js.ContentType = "text/html";
            return js;
        }

        [HttpPost]
        public JsonResult Save(string roleCode,string jsonStr)
        {
            RoleMenu[] objs = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<RoleMenu[]>(jsonStr);
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.RoleMenuDAL dal = new DAL.RoleMenuDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Save(roleCode,objs, out msg.Msg);
            }
            JsonResult js = Json(msg);
            js.ContentType = "text/html";
            return js;
        }
    }
}
