using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class EquipmentSettingDAL:BaseDAL<Models.EquipmentSetting>
    {
        public EquipmentSettingDAL(string DBConnection)
            : base(DBConnection) { }

        public override bool Save(Models.EquipmentSetting obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, out List<Models.EquipmentSetting> rst)
        {
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            rst = new List<Models.EquipmentSetting>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListEquipmentSetting", ps);
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
                        setting.Status =ConvertToShort(row["Status"]);
                        rst.Add(setting);
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

        public override Models.EquipmentSetting Get(string ID)
        {
            Models.EquipmentSetting es = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetEquipmentSetting", new SqlParameter[] { 
                    new SqlParameter("@ID",ID)
                });
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    es = new Models.EquipmentSetting();
                    es.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    es.Category = (Models.EquipmentSettingCategoryEnum)ConvertToShort(ds.Tables[0].Rows[0]["Category"]);
                    es.InOutType = (Models.EquipmentSettingInOutType)ConvertToShort(ds.Tables[0].Rows[0]["InOutType"]);
                    es.DriverType = (Models.EquipmentSettingDriverType)ConvertToShort(ds.Tables[0].Rows[0]["DriverType"]);
                    es.Name = ConvertToString(ds.Tables[0].Rows[0]["Name"]);
                    es.FullClassName = ConvertToString(ds.Tables[0].Rows[0]["FullClassName"]);
                    es.PropertyObj = ConvertToString(ds.Tables[0].Rows[0]["PropertyObj"]);
                    es.Status =ConvertToShort(ds.Tables[0].Rows[0]["Status"]);
                }
                return es;
            }
            catch
            {
                throw;
            }
        }
    }
}
