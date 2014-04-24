using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    class EquipmentDAL:BaseDAL< Models.Equipment>
    {
        public EquipmentDAL(string DBConnection)
            : base(DBConnection) { }
        public override bool Save(Models.Equipment obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, out List<Models.Equipment> rst)
        {
            rst = new List<Models.Equipment>();
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListEquipment", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Equipment eq = new Models.Equipment();
                        eq.ID =new Guid( ConvertToString(row["ID"]));
                        using (DAL.EquipmentSettingDAL dal = new EquipmentSettingDAL(ConStr))
                        {
                            eq.Setting = dal.Get(ConvertToString(row["EquipmentSettingID"]));
                        }
                        eq.EquipmentCode = ConvertToString(row["EquipmentCode"]);
                        eq.IP = ConvertToString(row["IP"]);
                        eq.Port = ConvertToString(row["Port"]);
                        eq.BaudRate = ConvertToString(row["BaudRate"]);
                        eq.DataBits = ConvertToString(row["DataBits"]);
                        eq.StopBits = ConvertToString(row["StopBits"]);
                        eq.Parity = ConvertToString(row["Parity"]);
                        eq.PropertyObj = ConvertToString(row["PropertyObj"]);
                        eq.Remark = ConvertToString(row["Remark"]);
                        rst.Add(eq);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public override Models.Equipment Get(string ID)
        {
            throw new NotImplementedException();
        }

        public bool SelectByWorkCenterID(List<SysInfo.Param> param, out List<Models.Equipment> rst)
        {
            rst = new List<Models.Equipment>();
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetEquipmentByWorkCenterID", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Equipment eq = new Models.Equipment();
                        eq.ID = new Guid(ConvertToString(row["ID"]));
                        using (DAL.EquipmentSettingDAL dal = new EquipmentSettingDAL(ConStr))
                        {
                            eq.Setting = dal.Get(ConvertToString(row["EquipmentSettingID"]));
                        }
                        eq.EquipmentCode = ConvertToString(row["EquipmentCode"]);
                        eq.IP = ConvertToString(row["IP"]);
                        eq.Port = ConvertToString(row["Port"]);
                        eq.BaudRate = ConvertToString(row["BaudRate"]);
                        eq.DataBits = ConvertToString(row["DataBits"]);
                        eq.StopBits = ConvertToString(row["StopBits"]);
                        eq.Parity = ConvertToString(row["Parity"]);
                        eq.Delay = ConvertToInt(row["Delay"]);
                        eq.PropertyObj = ConvertToString(row["PropertyObj"]);
                        eq.Remark = ConvertToString(row["Remark"]);
                        rst.Add(eq);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 保存工位设备
        /// </summary>
        /// <param name="workCenterID"></param>
        /// <param name="equipmentList"></param>
        /// <returns></returns>
        public bool SaveWorkCenterEquipment(Guid workCenterID, List<Models.Equipment> equipmentList)
        {
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            SqlParameter[] ps;
            int i;
            try
            {
                foreach (Models.Equipment equipment in equipmentList)
                {
                    plist.Clear();
                    plist.Add(new SysInfo.Param("@WorkCenterID", workCenterID,SqlDbType.UniqueIdentifier));
                    plist.Add(new SysInfo.Param("@EquipmentID", equipment.ID,SqlDbType.UniqueIdentifier));
                    plist.Add(new SysInfo.Param("@Delay", equipment.Delay, SqlDbType.Int));
                    BuildParam(out ps, plist.ToArray());
                    SqlEngine.RunProcedure("PROC_SaveWorkCenterEquipment", ps, out i);
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
