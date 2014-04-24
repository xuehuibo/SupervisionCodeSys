using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Models;
namespace SMKJ_FM.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: /Organization/

        public ViewResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询组织
        /// </summary>
        /// <param name="OrgCode">组织编码</param>
        /// <param name="OrgName">组织名称</param>
        /// <param name="OrgType">组织类型</param>
        /// <param name="Status">状态</param>
        /// <param name="XgDateBegin">修改时间开始</param>
        /// <param name="XgDateEnd">修改时间结束</param>
        /// <param name="page">页号</param>
        /// <param name="size">每页数据条数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string OrgCode,string OrgName,string OrgType,string Status,string XgDateBegin,string XgDateEnd, string page, string rows)
        {
            List<SysInfo.Param> paramList = new List<SysInfo.Param>();
            #region 组装参数
            paramList.Add(new SysInfo.Param("@OrgCode", OrgCode==null?"":OrgCode));
            paramList.Add(new SysInfo.Param("@OrgName", OrgName==null?"":OrgName));
            paramList.Add(new SysInfo.Param("@OrgType", OrgType==null?"":OrgType));
            paramList.Add(new SysInfo.Param("@Status", Status==null?"":Status));
            paramList.Add(new SysInfo.Param("@XgDateBegin", XgDateBegin==null?"":XgDateBegin));
            paramList.Add(new SysInfo.Param("@XgDateEnd", XgDateEnd==null?"":XgDateEnd));
	        #endregion
            JsonResult jrst;
            string msg;
            SysInfo.DatagridPage<Organization> dgPage = new SysInfo.DatagridPage<Organization>();
            using (DAL.OrganizationDAL dal =
                new DAL.OrganizationDAL(SysInfo.SysSetting.DBCCN))
            {

                dal.Select(paramList, int.Parse(page), int.Parse(rows), out dgPage.rows, out dgPage.total, out msg);
            }
            jrst = Json(dgPage);
            jrst.ContentType = "text/html";
            return jrst;
        }

        /// <summary>
        ///  新增组织
        /// </summary>
        /// <param name="ID">组织ID</param>
        /// <param name="OrgCode">组织编码</param>
        /// <param name="OrgName">组织名称</param>
        /// <param name="OrgType">组织类型</param>
        /// <param name="Status">组织状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string ID, string OrgCode,string OrgName , string OrgType, string Status)
        {
            Organization org = new Organization(ID, OrgCode, OrgName, OrgType, Status);
            return Save( org);
        }

        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="ID">组织ID</param>
        /// <param name="OrgCode">组织编码</param>
        /// <param name="OrgName">组织名称</param>
        /// <param name="OrgType">组织类型</param>
        /// <param name="Status">组织状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string ID, string OrgCode, string OrgName, string OrgType, string Status)
        {
            Organization org = new Organization(ID, OrgCode, OrgName, OrgType, Status);
            return Save(org);
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="ID">组织ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string ID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.OrganizationDAL dal = new DAL.OrganizationDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jrs = Json(msg);
            jrs.ContentType = "text/html";
            return jrs;
        }


        /// <summary>
        /// 保存Org对象
        /// </summary>
        /// <param name="ID">对象ID</param>
        /// <param name="OrgCode">组织编码</param>
        /// <param name="OrgName">组织名称</param>
        /// <param name="OrgType">组织类型</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public JsonResult Save(Models.Organization org)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.OrganizationDAL dal = new DAL.OrganizationDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Save(org, out msg.Msg);
            }
            JsonResult jrs = Json(msg);
            jrs.ContentType = "text/html";
            return jrs;
        }
    }
}
