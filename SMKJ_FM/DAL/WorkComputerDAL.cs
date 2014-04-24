using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class WorkComputerDAL:BaseDAL<Models.WorkComputer>
    {
        public WorkComputerDAL(string DBConnection)
            : base(DBConnection) { }
        public override bool Save(Models.WorkComputer obj, out string msg)
        {
            msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@ID", string.IsNullOrEmpty(obj.ID) ? string.Empty : obj.ID));
            pList.Add(new SysInfo.Param("@WorkComputerName", string.IsNullOrEmpty(obj.WorkComputerName) ? string.Empty : obj.WorkComputerName));
            pList.Add(new SysInfo.Param("@ComputerIP", string.IsNullOrEmpty(obj.ComputerIP) ? string.Empty : obj.ComputerIP));
            pList.Add(new SysInfo.Param("@HardwardConfig",string.IsNullOrEmpty(obj.HardwareConfig)?string.Empty:obj.HardwareConfig));
            pList.Add(new SysInfo.Param("@msg", string.Empty, SqlDbType.NVarChar, 100, ParameterDirection.Output));
            SqlParameter []ps;
            BuildParam(out ps,pList.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SaveWorkComputer", ps, out i);
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
            msg=SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
            SqlParameter[] ps;
            List<SysInfo.Param> pList=new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@ID", id));
            pList.Add(new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output));
            BuildParam(out ps,pList.ToArray());
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelWorkComputer", ps, out i);
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

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.WorkComputer> rst, out int total, out string msg)
        {
            SqlParameter[] ps;
            rst = new List<Models.WorkComputer>();
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListWorkComputer", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.WorkComputer wc = new Models.WorkComputer();
                        wc.ID = ConvertToString(row["ID"]);
                        wc.WorkComputerName = ConvertToString(row["WorkComputerName"]);
                        wc.ComputerIP = ConvertToString(row["ComputerIP"]);
                        wc.HardwareConfig = ConvertToString(row["HardwareConfig"]);
                        wc.CreateDate = ConvertToDatetime(row["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        rst.Add(wc);
                    }
                    total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
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

        public override Models.WorkComputer Get(string ID)
        {
            Models.WorkComputer wc = new Models.WorkComputer();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetWorkComputer", new SqlParameter[] { new SqlParameter("@ID", ID) });
                if (ds.Tables.Count > 0)
                {
                    wc.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    wc.WorkComputerName = ConvertToString(ds.Tables[0].Rows[0]["WorkComputerName"]);
                    wc.ComputerIP = ConvertToString(ds.Tables[0].Rows[0]["ComputerIP"]);
                    wc.HardwareConfig = ConvertToString(ds.Tables[0].Rows[0]["HardwareConfig"]);
                    wc.CreateDate = ConvertToDatetime(ds.Tables[0].Rows[0]["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    return wc;
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
    }
}