using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class EquipmentSettingDAL:BaseDAL<Models.EquipmentSetting>
    {
        public EquipmentSettingDAL(string DBConnection)
            : base(DBConnection) { }
        public override bool Save(Models.EquipmentSetting obj, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.EquipmentSetting> rst, out int total, out string msg)
        {
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            SqlParameter [] ps;
            BuildParam(out ps,param.ToArray());
            rst = new List<Models.EquipmentSetting>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListEquipmentSetting", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.EquipmentSetting setting = new Models.EquipmentSetting();
                        setting.ID = ConvertToString(row["ID"]);
                        setting.Category = (Models.EquipmentSettingCategoryEnum)ConvertToShort(row["Category"]);
                        setting.InOutType = (Models.EquipmentSettingInOutType)ConvertToShort(row["InOutType"]);
                        setting.DriverType = (Models.EquipmentSettingDriverType)ConvertToShort(row["DriverType"]);
                        setting.Name = ConvertToString(row["Name"]);
                        setting.FullClassName = ConvertToString(row["FullClassName"]);
                        setting.PropertyObj = ConvertToString(row["PropertyObj"]);
                        setting.Status = (Models.StatusEnum)ConvertToShort(row["Status"]);
                        rst.Add(setting);
                    }
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

        public override Models.EquipmentSetting Get(string ID)
        {
            Models.EquipmentSetting equipmentSetting = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetEquipmentSetting", new SqlParameter[]{
                    new SqlParameter("@ID",ID)
                });
                if (ds.Tables.Count > 0)
                {
                    equipmentSetting = new Models.EquipmentSetting();
                    equipmentSetting.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    equipmentSetting.Category = (Models.EquipmentSettingCategoryEnum)ConvertToShort(ds.Tables[0].Rows[0]["Category"]);
                    equipmentSetting.InOutType = (Models.EquipmentSettingInOutType)ConvertToShort(ds.Tables[0].Rows[0]["InOutType"]);
                    equipmentSetting.DriverType = (Models.EquipmentSettingDriverType)ConvertToShort(ds.Tables[0].Rows[0]["DriverType"]);
                    equipmentSetting.Name = ConvertToString(ds.Tables[0].Rows[0]["Name"]);
                    equipmentSetting.FullClassName = ConvertToString(ds.Tables[0].Rows[0]["FullClassName"]);
                    equipmentSetting.PropertyObj = ConvertToString(ds.Tables[0].Rows[0]["PropertyObj"]);
                    equipmentSetting.Status = (Models.StatusEnum)ConvertToShort(ds.Tables[0].Rows[0]["Status"]);
                }
                return equipmentSetting;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }
    }
}