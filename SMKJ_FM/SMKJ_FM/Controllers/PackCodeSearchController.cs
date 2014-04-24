using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMKJ_FM.Controllers
{
    public class PackCodeSearchController : Controller
    {
        //
        // GET: /PackRelation/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询监管码
        /// </summary>
        /// <param name="PackCode"></param>
        /// <param name="ParentCode"></param>
        /// <param name="TaskCode"></param>
        /// <param name="BatchNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string PackCode, string ParentCode, string TaskCode, string BatchNo,string page,string rows)
        {
            SysInfo.DatagridPage<Models.PackageCode> rst = new SysInfo.DatagridPage<Models.PackageCode>();
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@PackCode", string.IsNullOrEmpty(PackCode) ? string.Empty : PackCode));
            plist.Add(new SysInfo.Param("@ParentCode", string.IsNullOrEmpty(ParentCode) ? string.Empty : ParentCode));
            plist.Add(new SysInfo.Param("@TaskCode", string.IsNullOrEmpty(TaskCode) ? string.Empty : TaskCode));
            plist.Add(new SysInfo.Param("@BatchNo", string.IsNullOrEmpty(BatchNo) ? string.Empty : BatchNo));
            using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.SysSetting.DBCCN))
            {
                string msg;
                dal.Select(plist, int.Parse(page), int.Parse(rows), out rst.rows, out rst.total, out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
