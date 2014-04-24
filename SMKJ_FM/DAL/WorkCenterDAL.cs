using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class WorkCenterDAL:BaseDAL<Models.WorkCenter>,IDisposable
    {
        public WorkCenterDAL(string DBConnection)
            : base(DBConnection) { }

        public override bool Save(Models.WorkCenter obj, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.WorkCenter> rst, out int total, out string msg)
        {
            throw new NotImplementedException();
        }

        public override Models.WorkCenter Get(string ID)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取指定生产线工位
        /// </summary>
        /// <param name="ProductLineID"></param>
        /// <returns></returns>
        public bool GetProductLineWorkCenter(string ProductLineID, out List<Models.WorkCenter> workCenterList)
        {
            workCenterList = new List<Models.WorkCenter>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetWorkCenterByProductLineID", new SqlParameter[]{
                    new SqlParameter("@ProductLineID",ProductLineID)
                });
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.WorkCenter wc = new Models.WorkCenter();
                        wc.ID = new Guid(ConvertToString(row["ID"]));
                        wc.ProductLineID = new Guid(ConvertToString(row["ProductLineID"]));
                        wc.WorkCenterCode = ConvertToString(row["WorkCenterCode"]);
                        wc.LevelNo = ConvertToShort(row["LevelNo"]);
                        wc.Remark = ConvertToString(row["Remark"]);
                        wc.PropertyObj = ConvertToString(row["PropertyObj"]);
                        wc.X = ConvertToString(row["X"]);
                        wc.Y = ConvertToString(row["Y"]);
                        wc.PreWorkCenterID = new Guid(ConvertToString(row["PreWorkCenterID"]));
                        wc.PostWorkCenterID = new Guid(ConvertToString(row["PostWorkCenterID"]));
                        using (DAL.EquipmentDAL dal = new EquipmentDAL(ConStr))
                        {
                            wc.Equipments = dal.GetEquipmentByWorkCenterID(wc.ID);
                        }
                        workCenterList.Add(wc);
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

        /// <summary>
        /// 获取工位数据
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="WorkcenterID"></param>
        /// <returns></returns>
        public Models.WorkCenterData GetWorkCenterData(string TaskID, Guid WorkcenterID)
        {
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@TaskId", TaskID));
            plist.Add(new SysInfo.Param("@WorkCenterID", WorkcenterID, System.Data.SqlDbType.UniqueIdentifier));
            SqlParameter[] ps;
            BuildParam(out ps, plist.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetWorkCenterData", ps);
                if (ds.Tables.Count > 0)
                {
                    Models.WorkCenterData workCenterData = new Models.WorkCenterData();
                    workCenterData.TaskID = ConvertToString(ds.Tables[0].Rows[0]["TaskID"]);
                    workCenterData.WorkCenterID = new Guid(ConvertToString(ds.Tables[0].Rows[0]["WorkCenterId"]));
                    workCenterData.CurrentNum = ConvertToLong(ds.Tables[0].Rows[0]["CurrentNum"]);
                    workCenterData.FinishedNum = ConvertToLong(ds.Tables[0].Rows[0]["FinishedNum"]);
                    workCenterData.PackageNum = ConvertToLong(ds.Tables[0].Rows[0]["PackageNum"]);
                    workCenterData.KickoutNum = ConvertToLong(ds.Tables[0].Rows[0]["KickoutNum"]);
                    return workCenterData;
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
        /// 保存工位数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveWorkCenterData(Models.WorkCenterData data)
        {
            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.AppendFormat("update WorkCenterData set CurrentNum={0},FinishedNum={1},PackageNum={2} where TaskID='{3}' and WorkCenterId='{4}'",
                new object[]{data.CurrentNum.ToString(),data.FinishedNum.ToString(),data.PackageNum.ToString(),data.TaskID,data.WorkCenterID.ToString()}
                );
            try
            {
                int i = SqlEngine.ExecuteSql(StrBuilder.ToString());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("系统异常！");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }
    }
}