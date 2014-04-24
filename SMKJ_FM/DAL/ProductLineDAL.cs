using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ProductLineDAL:BaseDAL<Models.ProductLine>
    {
        public ProductLineDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Save(Models.ProductLine obj, out string msg)
        {
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@ID", obj.ID,SqlDbType.UniqueIdentifier));
            pList.Add(new SysInfo.Param("@WorkComputerID", obj.Work_Computer.ID));
            pList.Add(new SysInfo.Param("@LineCode", obj.LineCode));
            pList.Add(new SysInfo.Param("@LineName", obj.LineName));
            pList.Add(new SysInfo.Param("@Status", (short)(obj.Status), SqlDbType.SmallInt));
            pList.Add(new SysInfo.Param("@ManageUser", obj.ManageUser));
            pList.Add(new SysInfo.Param("@Remark", obj.Remark));
            pList.Add(new SysInfo.Param("@WorkShop", obj.WorkShop));
            pList.Add(new SysInfo.Param("@msg", string.Empty, SqlDbType.NVarChar, 100, ParameterDirection.Output));
            SqlParameter[] ps;
            BuildParam(out ps, pList.ToArray());
            msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SaveProductLine", ps, out i);
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Delete(string id, out string msg)
        {
            SysInfo.Param[] pList = { 
                                    new SysInfo.Param("@ID",id),
                                    new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter[] ps;
            BuildParam(out ps, pList);
            msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelProductLine", ps, out i);
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rst"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.ProductLine> rst, out int total, out string msg)
        {
            rst = new List<Models.ProductLine>();
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            SqlParameter [] ps;
            BuildParam(out ps,param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListProductLine", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.ProductLine line = new Models.ProductLine();
                        line.ID = new Guid(ConvertToString(row["ID"]));
                        line.LineCode = ConvertToString(row["LineCode"]);
                        line.LineName = ConvertToString(row["LineName"]);
                        line.ManageUser = ConvertToString(row["ManageUser"]);
                        line.WorkShop = ConvertToString(row["WorkShop"]);
                        line.Status = (Models.StatusEnum)ConvertToShort(row["Status"]);
                        using (DAL.WorkComputerDAL dal = new WorkComputerDAL(ConStr))
                        {
                            line.Work_Computer = dal.Get(ConvertToString(row["WorkComputerID"]));
                        }
                        line.Remark = ConvertToString(row["Remark"]);
                        rst.Add(line);

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

        /// <summary>
        /// 读取生产线
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override Models.ProductLine Get(string ID)
        {
            Models.ProductLine PL = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetProductLine", new SqlParameter[] {
                    new SqlParameter("@ID",ID)
                });
                if (ds.Tables.Count > 0)
                {
                    PL = new Models.ProductLine();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PL.ID = new Guid(ConvertToString(row["ID"]));
                        using (DAL.WorkComputerDAL dal = new WorkComputerDAL(ConStr))
                        {
                            PL.Work_Computer = dal.Get(ConvertToString(row["WorkComputerID"]));
                        }
                        PL.LineCode = ConvertToString(row["LineCode"]);
                        PL.LineName = ConvertToString(row["LineName"]);
                        PL.Status = (Models.StatusEnum)ConvertToShort(row["Status"]);
                        PL.ManageUser = ConvertToString(row["ManageUser"]);
                        PL.Remark = ConvertToString(row["Remark"]);
                        PL.WorkShop = ConvertToString(row["WorkShop"]);
                    }
                }
                return PL;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 查找指定工控机的生产线
        /// </summary>
        /// <param name="WorkComputerName"></param>
        /// <returns></returns>
        public List<Models.ProductLine> GetProductLineByWorkComputerName(string WorkComputerName)
        {
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetProductLineByWorkComputerName", new SqlParameter[] { 
                    new SqlParameter("WorkComputerName",WorkComputerName)
                });
                if (ds.Tables.Count > 0)
                {
                    List<Models.ProductLine> Lines = new List<Models.ProductLine>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.ProductLine line = new Models.ProductLine();
                        line.ID = new Guid(ConvertToString(row["ID"]));
                        line.LineCode = ConvertToString(row["LineCode"]);
                        line.LineName = ConvertToString(row["LineName"]);
                        line.ManageUser = ConvertToString(row["ManageUser"]);
                        line.WorkShop = ConvertToString(row["WorkShop"]);
                        using (DAL.WorkComputerDAL dal = new WorkComputerDAL(ConStr))
                        {
                            line.Work_Computer = dal.Get(ConvertToString(row["WorkComputerID"]));
                        }
                        Lines.Add(line);
                    }
                    return Lines;
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