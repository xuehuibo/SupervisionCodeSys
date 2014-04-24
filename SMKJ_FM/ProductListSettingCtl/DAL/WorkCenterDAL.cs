using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    class WorkCenterDAL:BaseDAL<Models.WorkCenter>
    {
        public WorkCenterDAL(string DBConnection)
            : base(DBConnection) { }
        public override bool Save(Models.WorkCenter obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, out List<Models.WorkCenter> rst)
        {
            rst = new List<Models.WorkCenter>();
            
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetWorkCenterByProductLineID", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.WorkCenter workCenter = new Models.WorkCenter();
                        workCenter.ID = new Guid(ConvertToString(row["ID"]));
                        workCenter.ProductLineID =new Guid( ConvertToString(row["ProductLineID"]));
                        workCenter.WorkCenterCode = ConvertToString(row["WorkCenterCode"]);
                        workCenter.LevelNo = ConvertToShort(row["LevelNo"]);
                        workCenter.Remark = ConvertToString(row["Remark"]);
                        workCenter.PreWorkCenterID = new Guid(ConvertToString(row["PreWorkCenterID"]));
                        workCenter.PostWorkCenterID = new Guid(ConvertToString(row["PostWorkCenterID"]));
                        using (DAL.EquipmentDAL dal = new EquipmentDAL(ConStr))
                        {
                            List<SysInfo.Param> plist = new List<SysInfo.Param>();
                            plist.Add(new SysInfo.Param("@WorkCenterID", workCenter.ID,SqlDbType.UniqueIdentifier));
                            if (!dal.SelectByWorkCenterID(plist, out workCenter.Equipments))
                            {
                                throw new Exception("工位读取错误!");
                            }
                        }

                        rst.Add(workCenter);
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

        public override Models.WorkCenter Get(string ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存工位列表
        /// </summary>
        /// <param name="WorkCenterList"></param>
        /// <returns></returns>
        public bool SaveList(Guid ProductLineID,List<Models.WorkCenter> WorkCenterList,List<Models.WorkCenter> RemoveWorkCenterList, out string msg)
        {
            int i = 0;
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            msg="流水线配置保存失败!";
            SqlParameter[] ps;
            
            try
            {
                #region 删除工位
                foreach (Models.WorkCenter WC in RemoveWorkCenterList)
                {
                    DeleteWorkCenterAndEquipment(WC.ID);
                }
                #endregion

                foreach (Models.WorkCenter WC in WorkCenterList)
                {
                    if (WC.ID!=Guid.Empty)
                    {
                        #region 清除工位设备
                        SqlEngine.RunProcedure("PROC_DelWorkCenterEquipment", new IDataParameter[] { new SqlParameter("@WorkCenterID", WC.ID) }, out i);
                        #endregion
                    }
                    WC.ProductLineID=ProductLineID;

                    #region 保存工位
                    plist.Clear();
                    plist.Add(new SysInfo.Param("@ID",WC.ID,SqlDbType.UniqueIdentifier,ParameterDirection.InputOutput));
                    plist.Add(new SysInfo.Param("@ProductLineID",WC.ProductLineID,SqlDbType.UniqueIdentifier));
                    plist.Add(new SysInfo.Param("@WorkCenterCode",WC.WorkCenterCode));
                    plist.Add(new SysInfo.Param("@LevelNo",WC.LevelNo,SqlDbType.SmallInt));
                    plist.Add(new SysInfo.Param("@Remark",string.IsNullOrEmpty(WC.Remark)?string.Empty:WC.Remark));
                    plist.Add(new SysInfo.Param("@PropertyObj",string.IsNullOrEmpty(WC.PropertyObj)?string.Empty:WC.PropertyObj));
                    plist.Add(new SysInfo.Param("@PreWorkCenterID",WC.PreWorkCenterID,SqlDbType.UniqueIdentifier));
                    plist.Add(new SysInfo.Param("@PostWorkCenterID",WC.PostWorkCenterID,SqlDbType.UniqueIdentifier));
                    BuildParam(out ps,plist.ToArray());
                    SqlEngine.RunProcedure("PROC_SaveWorkCenter",ps,out i);
                    if (WC.ID==Guid.Empty)
                    {
                        WC.ID =new Guid(ConvertToString(ps[0].Value));
                    }
                    #endregion

                    #region 保存工位设备
                    using (EquipmentDAL dal = new EquipmentDAL(ConStr))
                    {
                        dal.SaveWorkCenterEquipment(WC.ID, WC.Equipments);
                    }
                   

                    #endregion
                }
                msg = "保存流水线成功!";
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除工位及该工位上的设备
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public bool DeleteWorkCenterAndEquipment(Guid WorkCenterID)
        {
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelWorkCenterAndEquipment", new SqlParameter[] { 
                    new SqlParameter("@WorkCenterID",WorkCenterID)
                }, out i);
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
