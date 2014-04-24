using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class PackageRuleDAL:BaseDAL<Models.PackageRule>
    {
        public PackageRuleDAL(string DBConnection)
            : base(DBConnection) { }
        public override bool Save(Models.PackageRule obj, out string msg)
        {
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@ID", string.IsNullOrEmpty(obj.ID) ? string.Empty : obj.ID));
            plist.Add(new SysInfo.Param("@PackageRuleName", obj.PackageRuleName));
            plist.Add(new SysInfo.Param("@LevelCount", obj.LevelCount, System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@Status", (short)(obj.Status), System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@CascadeCode", obj.CascadeCode));
            plist.Add(new SysInfo.Param("@CreateUserID", obj.CreateUser.ID));
            plist.Add(new SysInfo.Param("@Remark", obj.Remark));
            plist.Add(new SysInfo.Param("@msg", string.Empty, System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Output));
            SqlParameter[] ps;
            BuildParam(out ps, plist.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SavePackageRule", ps, out i);
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
            SysInfo.Param[] plist = { 
                                    new SysInfo.Param("@ID",id),
                                    new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter [] ps;
            BuildParam(out ps, plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelPackageRule", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
                return false;
            }
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.PackageRule> rst, out int total, out string msg)
        {
            rst = new List<Models.PackageRule>();
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListPackageRule", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.PackageRule rule = new Models.PackageRule();
                        rule.ID = ConvertToString(row["ID"]);
                        rule.PackageRuleName = ConvertToString(row["PackageRuleName"]);
                        //rule.LevelCount = ConvertToShort(row["LevelCount"]);
                        rule.Status = (Models.StatusEnum)ConvertToShort(row["Status"]);
                        rule.CascadeCode = ConvertToString(row["CascadeCode"]);
                        using (DAL.UserDAL dal = new UserDAL(ConStr))
                        {
                            rule.CreateUser = dal.Get(ConvertToString(row["CreateUserID"]));
                        }
                        rule.CreateDate = ConvertToDatetime(row["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        rule.Remark = ConvertToString(row["Remark"]);
                        using (DAL.PackageRuleItemDAL dal = new PackageRuleItemDAL(ConStr))
                        {
                            rule.PackageRule_Item = dal.SelectByPackageRuleID(rule.ID);
                        }
                        rst.Add(rule);
                    }
                    total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
                    msg = SysInfo.SysMessageTxt.SYS_SEARCH_SUCCESS;
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

        public override Models.PackageRule Get(string ID)
        {
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetPackageRule", new SqlParameter[] {
                    new SqlParameter("@ID",ID)
                });
                if (ds.Tables.Count > 0)
                {
                    Models.PackageRule rule = new Models.PackageRule();
                    rule.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    rule.PackageRuleName=ConvertToString(ds.Tables[0].Rows[0]["PackageRuleName"]);
                    rule.Status = (Models.StatusEnum)ConvertToShort(ds.Tables[0].Rows[0]["Status"]);
                    rule.CascadeCode = ConvertToString(ds.Tables[0].Rows[0]["CascadeCode"]);
                    using (DAL.UserDAL dal = new UserDAL(ConStr))
                    {
                        rule.CreateUser = dal.Get(ConvertToString(ds.Tables[0].Rows[0]["CreateUserID"]));
                    }
                    rule.CreateDate = ConvertToDatetime(ds.Tables[0].Rows[0]["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    rule.Remark = ConvertToString(ds.Tables[0].Rows[0]["Remark"]);
                    using (DAL.PackageRuleItemDAL dal = new PackageRuleItemDAL(ConStr))
                    {
                        rule.PackageRule_Item = dal.SelectByPackageRuleID(rule.ID);
                    }
                    return rule;
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

        public bool Save(Models.PackageRule obj, out string ID, out string msg)
        {
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@ID", string.IsNullOrEmpty(obj.ID) ? string.Empty : obj.ID,System.Data.SqlDbType.NVarChar,100, ParameterDirection.InputOutput));
            plist.Add(new SysInfo.Param("@PackageRuleName", obj.PackageRuleName));
            plist.Add(new SysInfo.Param("@LevelCount", obj.LevelCount, System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@Status", (short)(obj.Status), System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@CascadeCode", obj.CascadeCode));
            plist.Add(new SysInfo.Param("@CreateUserID", obj.CreateUser.ID));
            plist.Add(new SysInfo.Param("@Remark", obj.Remark));
            plist.Add(new SysInfo.Param("@msg", string.Empty, System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Output));
            SqlParameter[] ps;
            BuildParam(out ps, plist.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SavePackageRule", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                if (i > 0)
                {
                    ID = ConvertToString(ps.First().Value);
                    return true;
                }
                else
                {
                    ID = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                ID = null;
                return false;
            }
        }
    }
}