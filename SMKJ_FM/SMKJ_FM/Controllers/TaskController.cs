using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMKJ_FM.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProductLineID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="Status"></param>
        /// <param name="PackageRuleID"></param>
        /// <param name="ProductID"></param>
        /// <param name="PackageSpecID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="PlanAmount"></param>
        /// <param name="TaskAmount"></param>
        /// <param name="DoneAmount"></param>
        /// <param name="CreateUserID"></param>
        /// <param name="ProductDate"></param>
        /// <param name="InvalidDate"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public JsonResult Insert(string ID, string ProductLineID, string TaskCode, string PackageRuleID
            , string ProductID, string BatchNo, string TaskAmount
            , string CreateUserID, string ProductDate, string InvalidDate, string Remark)
        {
            return Save(ID, ProductLineID, TaskCode, PackageRuleID
            , ProductID, BatchNo, TaskAmount
            , CreateUserID, ProductDate, InvalidDate, Remark);
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProductLineID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="Status"></param>
        /// <param name="PackageRuleID"></param>
        /// <param name="ProductID"></param>
        /// <param name="PackageSpecID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="PlanAmount"></param>
        /// <param name="TaskAmount"></param>
        /// <param name="DoneAmount"></param>
        /// <param name="CreateUserID"></param>
        /// <param name="ProductDate"></param>
        /// <param name="InvalidDate"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public JsonResult Update(string ID, string ProductLineID, string TaskCode, string PackageRuleID
            , string ProductID, string BatchNo, string TaskAmount
            , string CreateUserID, string ProductDate, string InvalidDate, string Remark)
        {
            return Save( ID,  ProductLineID,  TaskCode,  PackageRuleID
            ,  ProductID,  BatchNo,  TaskAmount
            ,  CreateUserID,  ProductDate,  InvalidDate,  Remark);
        }

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProductLineID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="Status"></param>
        /// <param name="PackageRuleID"></param>
        /// <param name="ProductID"></param>
        /// <param name="PackageSpecID"></param>
        /// <param name="BatchNo"></param>
        /// <param name="PlanAmount"></param>
        /// <param name="TaskAmount"></param>
        /// <param name="DoneAmount"></param>
        /// <param name="CreateUserID"></param>
        /// <param name="ProductDate"></param>
        /// <param name="InvalidDate"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        private JsonResult Save(string ID, string ProductLineID, string TaskCode,  string PackageRuleID
            , string ProductID, string BatchNo,  string TaskAmount
            , string CreateUserID, string ProductDate, string InvalidDate, string Remark)
        {
            Models.Task task = new Models.Task();
            task.ID = ID;
            task.ProductLine = new Models.ProductLine();
            task.ProductLine.ID = string.IsNullOrEmpty(ProductLineID) ? new Guid() : new Guid(ProductLineID);
            task.TaskCode = TaskCode;
            task.PackageRule = new Models.PackageRule();
            task.PackageRule.ID = PackageRuleID;
            task.Product = new Models.Product();
            task.Product.ID = ProductID;
            task.PackageSpec = new Models.PackageSpecific();
            task.BatchNo = BatchNo;
            task.TaskAmount = long.Parse(TaskAmount);
            task.CreateUser = new Models.User();
            task.CreateUser.ID = CreateUserID;
            task.ProductDate = ProductDate;
            task.InvalidDate = InvalidDate;
            task.Remark = Remark;
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Save(task, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <returns></returns>
        public JsonResult Select(string TaskCode, string ProductID, string ProductLineID, string BatchNo,string Status,string page,string rows)
        {
            SysInfo.DatagridPage<Models.Task> rst = new SysInfo.DatagridPage<Models.Task>();
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.SysSetting.DBCCN))
            {
                List<SysInfo.Param> plist = new List<SysInfo.Param>();
                plist.Add(new SysInfo.Param("@TaskCode", string.IsNullOrEmpty(TaskCode)?string.Empty:TaskCode));
                plist.Add(new SysInfo.Param("@ProductID", string.IsNullOrEmpty(ProductID)?string.Empty:ProductID));
                plist.Add(new SysInfo.Param("@ProductLineID", string.IsNullOrEmpty(ProductLineID)?string.Empty:ProductLineID));
                plist.Add(new SysInfo.Param("@BatchNo", string.IsNullOrEmpty(BatchNo)?string.Empty:BatchNo));
                plist.Add(new SysInfo.Param("@Status", string.IsNullOrEmpty(Status)?string.Empty:Status));
                string msg;
                dal.Select(plist, int.Parse(page), int.Parse(rows), out rst.rows, out rst.total, out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/type";
            return jr;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult Delete(string ID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 审核任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AuditUserID"></param>
        /// <returns></returns>
        public JsonResult Audit(string ID, string AuditUserID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Audit(ID, AuditUserID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 返工任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AuditUserId"></param>
        /// <returns></returns>
        public JsonResult UnDo(string ID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.UnDo(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
