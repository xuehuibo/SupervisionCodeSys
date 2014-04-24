using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class PackageRuleItemDAL:BaseDAL<Models.PackageRuleItem>
    {
        public PackageRuleItemDAL(string DBConnection)
            : base(DBConnection) { }

        public override bool Save(Models.PackageRuleItem obj, out string msg)
        {
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@PackageRuleID", obj.PackageRuleID));
            plist.Add(new SysInfo.Param("@LevelNo", obj.LevelNo, System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@Amount", obj.Amount, System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@PrintAmount", obj.PrintAmount, System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@PrintTemplateID", string.IsNullOrEmpty(obj.PrintTemplateID)?string.Empty:obj.PrintTemplateID));
            plist.Add(new SysInfo.Param("@Remark", string.IsNullOrEmpty(obj.Remark)?string.Empty:obj.Remark));
            plist.Add(new SysInfo.Param("@msg", string.Empty, System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Output));
            SqlParameter[] ps;
            BuildParam(out ps, plist.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SavePackageRuleItem", ps, out i);
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
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                return false;
            }
        }

        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.PackageRuleItem> rst, out int total, out string msg)
        {
            throw new NotImplementedException();
        }

        public override Models.PackageRuleItem Get(string ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过PackageRuleID检索item
        /// </summary>
        /// <returns></returns>
        public List<Models.PackageRuleItem> SelectByPackageRuleID(string PackageRuleID)
        {
            List<Models.PackageRuleItem> itemsList = new List<Models.PackageRuleItem>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListPackageRuleItem", new SqlParameter[] { 
                    new SqlParameter("@PackageRuleID",PackageRuleID)
                });
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.PackageRuleItem item = new Models.PackageRuleItem();
                        item.PackageRuleID = ConvertToString(row["PackageRuleID"]);
                        item.LevelNo = ConvertToShort(row["LevelNo"]);
                        item.Amount = ConvertToShort(row["Amount"]);
                        item.PrintAmount = ConvertToShort(row["PrintAmount"]);
                        item.PrintTemplateID = ConvertToString(row["PrintTemplateID"]);
                        item.Remark = ConvertToString(row["Remark"]);
                        itemsList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return itemsList;
        }

        /// <summary>
        /// 删除item
        /// </summary>
        /// <param name="PackageRuleID"></param>
        /// <returns></returns>
        public bool DeleteByPackageRuleID(string PackageRuleID,out string msg)
        {
            msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
            SysInfo.Param[] plist = { 
                                new SysInfo.Param("@PackageRuleID",PackageRuleID),
                                new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,ParameterDirection.Output)
                                 };
            SqlParameter[] ps;
            BuildParam(out ps, plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelPackageRuleItemByPackageRuleID", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }
    }
}