using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMKJ_FM.Controllers
{
    public class ProductLineController : Controller
    {
        //
        // GET: /ProductLine/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 检索生产线
        /// </summary>
        /// <param name="LineCode"></param>
        /// <param name="LineName"></param>
        /// <param name="ManageUser"></param>
        /// <param name="WorkShop"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string LineCode, string LineName, string ManageUser, string WorkShop, string Status,string page,string rows)
        {
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@LineCode", string.IsNullOrEmpty(LineCode)?string.Empty:LineCode));
            pList.Add(new SysInfo.Param("@LineName", string.IsNullOrEmpty(LineName)?string.Empty:LineName));
            pList.Add(new SysInfo.Param("@ManageUser",string.IsNullOrEmpty( ManageUser)?string.Empty:ManageUser));
            pList.Add(new SysInfo.Param("@WorkShop", string.IsNullOrEmpty(WorkShop)?string.Empty:WorkShop));
            pList.Add(new SysInfo.Param("@Status", string.IsNullOrEmpty(Status)?string.Empty:Status));

            SysInfo.DatagridPage<Models.ProductLine> rst=new SysInfo.DatagridPage<Models.ProductLine>();

            using (DAL.ProductLineDAL dal = new DAL.ProductLineDAL(SysInfo.SysSetting.DBCCN))
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
        /// <param name="ID"></param>
        /// <param name="WorkComputerID"></param>
        /// <param name="LineCode"></param>
        /// <param name="LineName"></param>
        /// <param name="Status"></param>
        /// <param name="ManageUser"></param>
        /// <param name="Remark"></param>
        /// <param name="WorkShop"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string ID, string WorkComputerID, string LineCode, string LineName, string Status, string ManageUser, string Remark, string WorkShop)
        {
            Models.ProductLine pl = new Models.ProductLine();
            pl.ID = new Guid();
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                pl.Work_Computer = dal.Get(WorkComputerID);
            }
            if (pl.Work_Computer == null)
            {
                SysInfo.Message msg = new SysInfo.Message();
                msg.Success = false;
                msg.Msg = "保存失败!工控机不存在!";
                JsonResult jr = Json(msg);
                jr.ContentType = "text/html";
                return jr;
            }
            pl.LineCode = LineCode;
            pl.LineName = LineName;
            pl.Status = (Models.StatusEnum)short.Parse(Status);
            pl.ManageUser = ManageUser;
            pl.Remark = Remark;
            pl.WorkShop = WorkShop;

            return Save(pl);
        }

        public JsonResult Update(string ID, string WorkComputerID, string LineCode, string LineName, string Status, string ManageUser, string Remark, string WorkShop)
        {
            Models.ProductLine pl = new Models.ProductLine();
            pl.ID =  new Guid(ID);
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                pl.Work_Computer = dal.Get(WorkComputerID);
            }
            if (pl.Work_Computer == null)
            {
                SysInfo.Message msg = new SysInfo.Message();
                msg.Success = false;
                msg.Msg = "保存失败!工控机不存在!";
                JsonResult jr = Json(msg);
                jr.ContentType = "text/html";
                return jr;
            }
            pl.LineCode = LineCode;
            pl.LineName = LineName;
            pl.Status = (Models.StatusEnum)short.Parse(Status);
            pl.ManageUser = ManageUser;
            pl.Remark = Remark;
            pl.WorkShop = WorkShop;

            return Save(pl);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult Save(Models.ProductLine obj)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.ProductLineDAL dal = new DAL.ProductLineDAL(SysInfo.SysSetting.DBCCN))
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
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.ProductLineDAL dal = new DAL.ProductLineDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }


        /// <summary>
        /// 获取工位
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWorkCenterByProductLineID(string ID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(SysInfo.SysSetting.DBCCN))
            {
                List<Models.WorkCenter> WcList;
                msg.Success = dal.GetProductLineWorkCenter(ID, out WcList);
                if (msg.Success)
                {
                    msg.Obj = WcList;
                }
                else
                {
                    msg.Msg = "工位读取失败!";
                }
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }


        [HttpGet]
        public ViewResult NewWorkCenterWindow(string LineID)
        {
            Des des=new Des();
            Models.ProductLine line = null; ;
            using (DAL.ProductLineDAL dal = new DAL.ProductLineDAL(SysInfo.SysSetting.DBCCN))
            {
                line = dal.Get(LineID);
            }
            ViewBag.Str = "［车间］" + line.WorkShop + "   "+"［流水线］"+line.LineCode+"-"+line.LineName;
            ViewBag.P1 = line.ID.ToString();
            ViewBag.P2 = des.EncryStrHex(SysInfo.SysSetting.DBCCN, "0125");
            return View("WorkCenter");
        }
    }
}
