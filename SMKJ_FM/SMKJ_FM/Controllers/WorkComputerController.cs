using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMKJ_FM.Controllers
{
    public class WorkComputerController : Controller
    {
        //
        // GET: /WorkComputer/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="WorkComputerName"></param>
        /// <param name="ComputerIP"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string WorkComputerName, string ComputerIP,string page,string rows)
        {
            SysInfo.DatagridPage<Models.WorkComputer> rst = new SysInfo.DatagridPage<Models.WorkComputer>();
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@WorkComputerName", WorkComputerName));
            pList.Add(new SysInfo.Param("@ComputerIP", ComputerIP));
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                string msg;
                dal.Select(pList, int.Parse(page), int.Parse(rows), out rst.rows, out rst.total, out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="WorkComputerName"></param>
        /// <param name="ComputerIP"></param>
        /// <param name="HardwareConfig"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string ID,string WorkComputerName, string ComputerIP, string HardwareConfig)
        {
            Models.WorkComputer wc = new Models.WorkComputer();
            wc.ID = ID;
            wc.WorkComputerName = WorkComputerName;
            wc.ComputerIP = ComputerIP;
            wc.HardwareConfig = HardwareConfig;
            return Save(wc);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="WorkComputerName"></param>
        /// <param name="ComputerIP"></param>
        /// <param name="HardwareConfig"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string ID, string WorkComputerName, string ComputerIP, string HardwareConfig)
        {
            Models.WorkComputer wc = new Models.WorkComputer();
            wc.ID = ID;
            wc.WorkComputerName = WorkComputerName;
            wc.ComputerIP = ComputerIP;
            wc.HardwareConfig = HardwareConfig;
            return Save(wc);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult Save(Models.WorkComputer obj)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Save(obj, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string ID)
        {
            SysInfo.Message msg=new SysInfo.Message();
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
