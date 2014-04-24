using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class TaskDAL:BaseDAL<Models.Task>
    {
        public TaskDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Save(Models.Task obj, out string msg)
        {
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@ID", obj.ID));
            plist.Add(new SysInfo.Param("@ProductLineID", 
                obj.ProductLine.ID,System.Data.SqlDbType.UniqueIdentifier));
            plist.Add(new SysInfo.Param("@TaskCode", obj.TaskCode));
            plist.Add(new SysInfo.Param("@Status", obj.Status, System.Data.SqlDbType.SmallInt));
            plist.Add(new SysInfo.Param("@PackageRuleID", obj.PackageRule.ID));
            plist.Add(new SysInfo.Param("@ProductID", obj.Product.ID));
            plist.Add(new SysInfo.Param("@BatchNo", obj.BatchNo));
            plist.Add(new SysInfo.Param("@PlanAmount", obj.PlanAmount, System.Data.SqlDbType.BigInt));
            plist.Add(new SysInfo.Param("@TaskAmount", obj.TaskAmount, System.Data.SqlDbType.BigInt));
            plist.Add(new SysInfo.Param("@DoneAmount", obj.DoneAmount, System.Data.SqlDbType.BigInt));
            plist.Add(new SysInfo.Param("@CreateUserID", obj.CreateUser.ID));
            plist.Add(new SysInfo.Param("@ProductDate", string.IsNullOrEmpty(obj.ProductDate)?string.Empty:obj.ProductDate));
            plist.Add(new SysInfo.Param("@InvalidDate", string.IsNullOrEmpty(obj.InvalidDate)?string.Empty:obj.InvalidDate));
            plist.Add(new SysInfo.Param("@Remark", obj.Remark));
            plist.Add(new SysInfo.Param("@msg", string.Empty, System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Output));
            SqlParameter[] ps;
            BuildParam(out ps, plist.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SaveTask", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return i > 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                return false;
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Delete(string id, out string msg)
        {
            try
            {
                SysInfo.Param[] plist = { 
                                        new SysInfo.Param("@ID",id),
                                        new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,ParameterDirection.Output)
                                        };
                SqlParameter[] ps;
                BuildParam(out ps, plist);
                int i;
                SqlEngine.RunProcedure("PROC_DelTask", ps, out i);
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
                msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
                return false;
            }
        }

        /// <summary>
        /// 检索任务
        /// </summary>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rst"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.Task> rst, out int total, out string msg)
        {
            rst = new List<Models.Task>();
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListTask", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Task task = new Models.Task();
                        task.ID = ConvertToString(row["ID"]);
                        using (DAL.ProductLineDAL dal = new ProductLineDAL(ConStr))
                        {
                            task.ProductLine = dal.Get(ConvertToString(row["ProductLineID"]));
                        }
                        task.TaskCode = ConvertToString(row["TaskCode"]);
                        task.Status = (Models.TaskStatus)ConvertToShort(row["Status"]);
                        using (DAL.PackageRuleDAL dal = new PackageRuleDAL(ConStr))
                        {
                            task.PackageRule = dal.Get(ConvertToString(row["PackageRuleID"]));
                        }
                        using (DAL.ProductDAL dal = new ProductDAL(ConStr))
                        {
                            task.Product = dal.Get(ConvertToString(row["ProductID"]));
                            task.PackageSpec = dal.GetPackageSpecific(ConvertToString(row["PackageSpecID"]));
                        }
                        task.BatchNo = ConvertToString(row["BatchNo"]);
                        task.PlanAmount = ConvertToLong(row["PlanAmount"]);
                        task.TaskAmount = ConvertToLong(row["TaskAmount"]);
                        task.DoneAmount = ConvertToLong(row["DoneAmount"]);
                        using (DAL.UserDAL dal = new UserDAL(ConStr))
                        {
                            task.CreateUser = dal.Get(ConvertToString(row["CreateUserID"]));
                            task.AuditUser = dal.Get(ConvertToString(row["AuditUserID"]));
                        }
                        task.CreateDate = ConvertToDatetime(row["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        if (!Convert.IsDBNull(row["StartDate"]))
                        {
                            task.StartDate = ConvertToDatetime(row["StartDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["EndDate"]))
                        {
                            task.EndDate = ConvertToDatetime(row["EndDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["ProductDate"]))
                        {
                            task.ProductDate = ConvertToDatetime(row["ProductDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["InvalidDate"]))
                        {
                            task.InvalidDate = ConvertToDatetime(row["InvalidDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["AuditDate"]))
                        {
                            task.AuditDate = ConvertToDatetime(row["AuditDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        task.Remark = ConvertToString(row["Remark"]);
                        rst.Add(task);
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

        /// <summary>
        /// 将要导出的任务
        /// </summary>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rst"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SelectExportTask(List<SysInfo.Param> param, int page, int size, out List<Models.Task> rst, out int total, out string msg)
        {
            rst = new List<Models.Task>();
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListExportRelationTask", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Task task = new Models.Task();
                        task.ID = ConvertToString(row["ID"]);
                        using (DAL.ProductLineDAL dal = new ProductLineDAL(ConStr))
                        {
                            task.ProductLine = dal.Get(ConvertToString(row["ProductLineID"]));
                        }
                        task.TaskCode = ConvertToString(row["TaskCode"]);
                        task.Status = (Models.TaskStatus)ConvertToShort(row["Status"]);
                        using (DAL.PackageRuleDAL dal = new PackageRuleDAL(ConStr))
                        {
                            task.PackageRule = dal.Get(ConvertToString(row["PackageRuleID"]));
                        }
                        using (DAL.ProductDAL dal = new ProductDAL(ConStr))
                        {
                            task.Product = dal.Get(ConvertToString(row["ProductID"]));
                            task.PackageSpec = dal.GetPackageSpecific(ConvertToString(row["PackageSpecID"]));
                        }
                        task.BatchNo = ConvertToString(row["BatchNo"]);
                        task.PlanAmount = ConvertToLong(row["PlanAmount"]);
                        task.TaskAmount = ConvertToLong(row["TaskAmount"]);
                        task.DoneAmount = ConvertToLong(row["DoneAmount"]);
                        using (DAL.UserDAL dal = new UserDAL(ConStr))
                        {
                            task.CreateUser = dal.Get(ConvertToString(row["CreateUserID"]));
                            task.AuditUser = dal.Get(ConvertToString(row["AuditUserID"]));
                        }
                        task.CreateDate = ConvertToDatetime(row["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        if (!Convert.IsDBNull(row["StartDate"]))
                        {
                            task.StartDate = ConvertToDatetime(row["StartDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["EndDate"]))
                        {
                            task.EndDate = ConvertToDatetime(row["EndDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["ProductDate"]))
                        {
                            task.ProductDate = ConvertToDatetime(row["ProductDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["InvalidDate"]))
                        {
                            task.InvalidDate = ConvertToDatetime(row["InvalidDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (!Convert.IsDBNull(row["AuditDate"]))
                        {
                            task.AuditDate = ConvertToDatetime(row["AuditDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        task.Remark = ConvertToString(row["Remark"]);
                        rst.Add(task);
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

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override Models.Task Get(string ID)
        {
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetTask",
                    new SqlParameter[] { new SqlParameter("ID", ID) });
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Models.Task task = new Models.Task();
                    task.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    using (DAL.ProductLineDAL dal = new ProductLineDAL(ConStr))
                    {
                        task.ProductLine = dal.Get(ConvertToString(ds.Tables[0].Rows[0]["ProductLineID"]));
                    }
                    task.TaskCode = ConvertToString(ds.Tables[0].Rows[0]["TaskCode"]);
                    task.Status = (Models.TaskStatus)ConvertToShort(ds.Tables[0].Rows[0]["Status"]);
                    using (DAL.PackageRuleDAL dal = new PackageRuleDAL(ConStr))
                    {
                        task.PackageRule = dal.Get(ConvertToString(ds.Tables[0].Rows[0]["PackageRuleID"]));
                    }
                    using (DAL.ProductDAL dal = new ProductDAL(ConStr))
                    {
                        task.Product = dal.Get(ConvertToString(ds.Tables[0].Rows[0]["ProductID"]));
                        task.PackageSpec = dal.GetPackageSpecific(ConvertToString(ds.Tables[0].Rows[0]["PackageSpecID"]));
                    }
                    task.BatchNo = ConvertToString(ds.Tables[0].Rows[0]["BatchNo"]);
                    task.PlanAmount = ConvertToLong(ds.Tables[0].Rows[0]["PlanAmount"]);
                    task.TaskAmount = ConvertToLong(ds.Tables[0].Rows[0]["TaskAmount"]);
                    task.DoneAmount = ConvertToLong(ds.Tables[0].Rows[0]["DoneAmount"]);
                    using (DAL.UserDAL dal = new UserDAL(ConStr))
                    {
                        task.CreateUser = dal.Get(ConvertToString(ds.Tables[0].Rows[0]["CreateUserID"]));
                        task.AuditUser = dal.Get(ConvertToString(ds.Tables[0].Rows[0]["AuditUserID"]));
                    }
                    task.CreateDate = ConvertToDatetime(ds.Tables[0].Rows[0]["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    task.StartDate = ConvertToDatetime(ds.Tables[0].Rows[0]["StartDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    task.EndDate = ConvertToDatetime(ds.Tables[0].Rows[0]["EndDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    task.ProductDate = ConvertToDatetime(ds.Tables[0].Rows[0]["ProductDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    task.InvalidDate = ConvertToDatetime(ds.Tables[0].Rows[0]["InvalidDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    task.AuditDate = ConvertToDatetime(ds.Tables[0].Rows[0]["AuditDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    task.Remark = ConvertToString(ds.Tables[0].Rows[0]["Remark"]);
                    return task;
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
        /// 审核任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AuditUserID"></param>
        /// <returns></returns>
        public bool Audit(string ID, string AuditUserID,out string msg)
        {
            SysInfo.Param[] plist = { 
                                    new SysInfo.Param("@ID",ID),
                                    new SysInfo.Param("@AuditUserID",AuditUserID),
                                    new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter[] ps;
            BuildParam(out ps, plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_AuditTask", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return i > 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = "审核失败!";
                return false;
            }
        }

        /// <summary>
        /// 返工任务
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UnDo(string ID, out string msg)
        {
            SysInfo.Param[] plist = { 
                                    new SysInfo.Param("@ID",ID),
                                    new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter[] ps;
            BuildParam(out ps,plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_UnDoTask", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                bool ok=false;
                using (DAL.PackageCodeDAL dal = new PackageCodeDAL(ConStr))
                {
                    ok = dal.UnDo(ID);
                }
                msg += ok ? string.Empty : "码返工失败!";
                return i > 0&&ok;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = "返工失败!";
                return false;
            }
        }

        /// <summary>
        /// 获取指定工控机的任务
        /// </summary>
        /// <param name="WorkComputerName"></param>
        /// <returns></returns>
        public List<Models.Task> GetTaskByWorkComputerName(string WorkComputerName)
        {
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetTaskByWorkComputerName", new SqlParameter[] { 
                    new SqlParameter("@WorkComputerName",WorkComputerName)
                });
                if (ds.Tables.Count > 0)
                {
                    List<Models.Task> tasks = new List<Models.Task>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Task task = new Models.Task();
                        task.ID = ConvertToString(row["ID"]);
                        using (DAL.ProductLineDAL dal = new ProductLineDAL(ConStr))
                        {
                            task.ProductLine = dal.Get(ConvertToString(row["ProductLineID"]));
                        }
                        task.TaskCode = ConvertToString(row["TaskCode"]);
                        task.Status = (Models.TaskStatus)ConvertToShort(row["Status"]);
                        using (DAL.PackageRuleDAL dal = new PackageRuleDAL(ConStr))
                        {
                            task.PackageRule = dal.Get(ConvertToString(row["PackageRuleID"]));
                        }
                        using (DAL.ProductDAL dal = new ProductDAL(ConStr))
                        {
                            task.Product = dal.Get(ConvertToString(row["ProductID"]));
                            task.PackageSpec = dal.GetPackageSpecific(ConvertToString(row["PackageSpecID"]));
                        }
                        task.BatchNo = ConvertToString(row["BatchNo"]);
                        task.PlanAmount = ConvertToLong(row["PlanAmount"]);
                        task.TaskAmount = ConvertToLong(row["TaskAmount"]);
                        task.DoneAmount = ConvertToLong(row["DoneAmount"]);
                        tasks.Add(task);
                    }
                    return tasks;
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
        /// 开启任务
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool StartTask(string taskID,out string msg)
        {
            SysInfo.Param[] plist = { 
                                    new SysInfo.Param("@ID",taskID),
                                    new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter[] ps;
            BuildParam(out ps,plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_StartTask", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return i > 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 结束任务
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool StopTask(string taskID,out string msg)
        {
            SysInfo.Param[] plist ={
                                  new SysInfo.Param("@ID",taskID),
                                  new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
                                  };
            SqlParameter[] ps;
            BuildParam(out ps, plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_StopTask", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return i > 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = ex.Message;
                return false;
            }
        }
    }
}