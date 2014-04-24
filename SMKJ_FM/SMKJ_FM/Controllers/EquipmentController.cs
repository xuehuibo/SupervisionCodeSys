using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMKJ_FM.Controllers
{
    public class EquipmentController : Controller
    {
        //
        // GET: /Equipment/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string EquipmentCode,string EquipmentSettingID,string page,string rows)
        {
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@EquipmentCode", string.IsNullOrEmpty(EquipmentCode)?string.Empty:EquipmentCode));
            pList.Add(new SysInfo.Param("@EquipmentSettingID", string.IsNullOrEmpty(EquipmentSettingID)?string.Empty:EquipmentSettingID));
            SysInfo.DatagridPage<Models.Equipment> rst=new SysInfo.DatagridPage<Models.Equipment>();
            string msg;
            using (DAL.EquipmentDAL dal = new DAL.EquipmentDAL(SysInfo.SysSetting.DBCCN))
            {
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
        /// <param name="EquipmentCode"></param>
        /// <param name="EquipmentSettingID"></param>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        /// <param name="BaudRate"></param>
        /// <param name="DataBits"></param>
        /// <param name="StopBits"></param>
        /// <param name="Parity"></param>
        /// <param name="PropertyObj"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string ID, string EquipmentCode, string EquipmentSettingID, string IP, string Port,
            string BaudRate, string DataBits, string StopBits, string Parity, string PropertyObj, string Remark)
        {
            Models.Equipment equ = new Models.Equipment();
            equ.ID =new Guid();
            equ.EquipmentCode = EquipmentCode;
            using (DAL.EquipmentSettingDAL sdal = new DAL.EquipmentSettingDAL(SysInfo.SysSetting.DBCCN))
            {
                equ.Setting = sdal.Get(EquipmentSettingID);
            }
            equ.IP = IP;
            equ.Port = Port;
            equ.BaudRate = BaudRate;
            equ.DataBits = DataBits;
            equ.StopBits = (Models.StopBitsEnum)short.Parse(StopBits);
            equ.Parity = (Models.ParityEnum)short.Parse(Parity);
            equ.PropertyObj = PropertyObj;
            equ.Remark = Remark;
            return Save(equ);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="EquipmentCode"></param>
        /// <param name="EquipmentSettingID"></param>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        /// <param name="BaudRate"></param>
        /// <param name="DataBits"></param>
        /// <param name="StopBits"></param>
        /// <param name="Parity"></param>
        /// <param name="PropertyObj"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string ID, string EquipmentCode, string EquipmentSettingID, string IP, string Port,
            string BaudRate, string DataBits, string StopBits, string Parity, string PropertyObj, string Remark)
        {
            Models.Equipment equ = new Models.Equipment();
            equ.ID = new Guid(ID);
            equ.EquipmentCode = EquipmentCode;
            using (DAL.EquipmentSettingDAL sdal = new DAL.EquipmentSettingDAL(SysInfo.SysSetting.DBCCN))
            {
                equ.Setting = sdal.Get(EquipmentSettingID);
            }
            equ.IP = IP;
            equ.Port = Port;
            equ.BaudRate = BaudRate;
            equ.DataBits = DataBits;
            equ.StopBits = (Models.StopBitsEnum)short.Parse(StopBits);
            equ.Parity = (Models.ParityEnum)short.Parse(Parity);
            equ.PropertyObj = PropertyObj;
            equ.Remark = Remark;
            return Save(equ);
        }
        /// <summary>
        /// 保存设备
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult Save(Models.Equipment obj)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.EquipmentDAL dal = new DAL.EquipmentDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success=dal.Save(obj, out msg.Msg);
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
            using (DAL.EquipmentDAL dal = new DAL.EquipmentDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
