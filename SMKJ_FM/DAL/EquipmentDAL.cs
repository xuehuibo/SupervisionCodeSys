using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class EquipmentDAL:BaseDAL<Models.Equipment>
    {
        public EquipmentDAL(string DBConnection)
            : base(DBConnection) { }
        public override bool Save(Models.Equipment obj, out string msg)
        {
            msg=SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
            List<SysInfo.Param> Plist = new List<SysInfo.Param>();
            Plist.Add(new SysInfo.Param("@ID", obj.ID,SqlDbType.UniqueIdentifier));
            Plist.Add(new SysInfo.Param("@EquipmentCode", string.IsNullOrEmpty(obj.EquipmentCode) ? "" : obj.EquipmentCode));
            Plist.Add(new SysInfo.Param("@IP", string.IsNullOrEmpty(obj.IP) ? "" : obj.IP));
            Plist.Add(new SysInfo.Param("@Port", string.IsNullOrEmpty(obj.Port) ? "" : obj.Port));
            Plist.Add(new SysInfo.Param("@BaudRate", string.IsNullOrEmpty(obj.BaudRate) ? "" : obj.BaudRate));
            Plist.Add(new SysInfo.Param("@DataBits", string.IsNullOrEmpty(obj.DataBits) ? "" : obj.DataBits));
            Plist.Add(new SysInfo.Param("@StopBits", (short)(obj.StopBits),SqlDbType.SmallInt));
            Plist.Add(new SysInfo.Param("@Parity",(short)(obj.Parity),SqlDbType.SmallInt));
            Plist.Add(new SysInfo.Param("@PropertyObj", string.IsNullOrEmpty(obj.PropertyObj) ? "" : obj.PropertyObj));
            Plist.Add(new SysInfo.Param("@Remark", string.IsNullOrEmpty(obj.Remark) ? "" : obj.Remark));
            Plist.Add(new SysInfo.Param("@EquipmentSettingID", string.IsNullOrEmpty(obj.Setting.ID) ? "" : obj.Setting.ID));
            Plist.Add(new SysInfo.Param("@msg", string.Empty, SqlDbType.NVarChar, 100, ParameterDirection.Output));

            SqlParameter[] ps;
            BuildParam(out ps, Plist.ToArray());

            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SaveEquipment", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }

        }

        public override bool Delete(string id, out string msg)
        {
            msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
            List<SysInfo.Param> Plist=new List<SysInfo.Param>();
            Plist.Add(new SysInfo.Param("@ID",id));
            Plist.Add(new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output));
            SqlParameter[] ps;
            BuildParam(out ps,Plist.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelEquipment",ps,out i);
                msg = ConvertToString(ps.Last().Value);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);

                return false;
            }
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.Equipment> rst, out int total, out string msg)
        {
            SqlParameter[] ps;
            total = 0;
            rst = new List<Models.Equipment>();
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListEquipment", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Equipment equ = new Models.Equipment();
                        equ.ID = new Guid(ConvertToString(row["ID"]));
                        equ.EquipmentCode = ConvertToString(row["EquipmentCode"]);
                        using (DAL.EquipmentSettingDAL dal = new EquipmentSettingDAL(ConStr))
                        {
                            equ.Setting =dal.Get( ConvertToString(row["EquipmentSettingID"]));
                        }
                        equ.IP = ConvertToString(row["IP"]);
                        equ.Port = ConvertToString(row["Port"]);
                        equ.BaudRate = ConvertToString(row["BaudRate"]);
                        equ.DataBits = ConvertToString(row["DataBits"]);
                        equ.StopBits = (Models.StopBitsEnum)ConvertToShort(row["StopBits"]);
                        equ.Parity = (Models.ParityEnum)ConvertToShort(row["Parity"]);
                        equ.PropertyObj = ConvertToString(row["PropertyObj"]);
                        equ.Remark = ConvertToString(row["Remark"]);
                        rst.Add(equ);
                    }
                    total = ConvertToInt( ds.Tables[1].Rows[0]["total"]);
                }
                return true;

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }

        public override Models.Equipment Get(string ID)
        {
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetEquipment", new SqlParameter[] { 
                    new SqlParameter("@ID",ID)
                });
                if (ds.Tables.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    Models.Equipment equ = new Models.Equipment();
                    equ.ID = new Guid(ConvertToString(row["ID"]));
                    equ.EquipmentCode = ConvertToString(row["EquipmentCode"]);
                    using (DAL.EquipmentSettingDAL dal = new EquipmentSettingDAL(ConStr))
                    {
                        equ.Setting = dal.Get(ConvertToString(row["EquipmentSettingID"]));
                    }
                    equ.IP = ConvertToString(row["IP"]);
                    equ.Port = ConvertToString(row["Port"]);
                    equ.BaudRate = ConvertToString(row["BaudRate"]);
                    equ.DataBits = ConvertToString(row["DataBits"]);
                    equ.StopBits =(Models.StopBitsEnum) ConvertToShort(row["StopBits"]);
                    equ.Parity = (Models.ParityEnum)ConvertToShort(row["Parity"]);
                    equ.PropertyObj = ConvertToString(row["PropertyObj"]);
                    equ.Remark = ConvertToString(row["Remark"]);
                    return equ;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 查询工位设备
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public List<Models.Equipment> GetEquipmentByWorkCenterID(Guid WorkCenterID)
        {
            List<Models.Equipment> eList = new List<Models.Equipment>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetEquipmentByWorkCenterID", new SqlParameter[]{
                    new SqlParameter("@WorkCenterID",WorkCenterID)
                });
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        eList.Add(Get(ConvertToString(row["ID"])));
                    }
                }
                return eList;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }

    }
}