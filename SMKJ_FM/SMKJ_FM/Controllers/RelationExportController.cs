using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SMKJ_FM.Controllers
{
    public class RelationExportController : Controller
    {
        //
        // GET: /PackRelation/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="BatchNo"></param>
        /// <param name="ProductLineID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string ProductID, string TaskCode, string BatchNo, string ProductLineID,string EndDateBegin,string EndDateEnd,string page, string rows)
        {
            SysInfo.DatagridPage<Models.Task> rst = new SysInfo.DatagridPage<Models.Task>();
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.SysSetting.DBCCN))
            {
                List<SysInfo.Param> plist = new List<SysInfo.Param>();
                plist.Add(new SysInfo.Param("@TaskCode", string.IsNullOrEmpty(TaskCode) ? string.Empty : TaskCode));
                plist.Add(new SysInfo.Param("@ProductID", string.IsNullOrEmpty(ProductID) ? string.Empty : ProductID));
                plist.Add(new SysInfo.Param("@ProductLineID", string.IsNullOrEmpty(ProductLineID) ? string.Empty : ProductLineID));
                plist.Add(new SysInfo.Param("@BatchNo", string.IsNullOrEmpty(BatchNo) ? string.Empty : BatchNo));
                plist.Add(new SysInfo.Param("@EndDateBegin",string.IsNullOrEmpty(EndDateBegin)?string.Empty:EndDateBegin));
                plist.Add(new SysInfo.Param("@EndDateEnd", string.IsNullOrEmpty(EndDateEnd) ? string.Empty : EndDateEnd));
                string msg;
                dal.SelectExportTask(plist, int.Parse(page), int.Parse(rows), out rst.rows, out rst.total, out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/type";
            return jr;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="IDArray"></param>
        /// <returns></returns>
        [HttpPost]
        public string Export(string IDArray)
        {
            string[] IDs = IDArray.Split(';');
            using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.SysSetting.DBCCN))
            {
                for (int i = 0; i < IDs.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(IDs[i]))
                    {
                        dal.ExportRelation(IDs[i], Server.MapPath("/Content/RelationModel.xml"), Server.MapPath("/"));
                    }
                }
            }
            return Server.MapPath("/Export/Relation/");
        }
    }
}
