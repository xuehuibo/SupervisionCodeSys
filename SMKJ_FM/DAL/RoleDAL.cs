using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Models;

namespace DAL
{
    public class RoleDAL : BaseDAL<Models.Role>
    {
        public RoleDAL(string DBConnection)
            : base(DBConnection) { }
        /// <summary>
        /// 保存Role对象
        /// </summary>
        /// <param name="obj">Role对象</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public override bool Save(Models.Role obj, out string msg)
        {
            int i = 0;
            SqlParameter[] ps;
            BuildParam(out ps, new SysInfo.Param[] {
                new SysInfo.Param("@id",obj.ID),
                new SysInfo.Param("@RoleCode",obj.RoleCode),
                new SysInfo.Param("@RoleName",obj.RoleName),
                new SysInfo.Param("@Status",(short)obj.Status,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,System.Data.ParameterDirection.Output)
            });
            try
            {
                SqlEngine.RunProcedure("PROC_SaveRole", ps, out i);
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
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 删除Role对象
        /// </summary>
        /// <param name="id">Role对象Id</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public override bool Delete(string id, out string msg)
        {
            SqlParameter[] ps;
            BuildParam(out ps, new SysInfo.Param[] {
                new SysInfo.Param("@ID",id),
                new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
            });
            try
            {
                int i = 0;
                SqlEngine.RunProcedure("PROC_DelRole", ps, out i);
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
                msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 查询Role对象
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="page">页号</param>
        /// <param name="size">页大小</param>
        /// <param name="rst">返回结果</param>
        /// <param name="total">返回总结果数</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.Role> rst, out int total, out string msg)
        {
            if (param.Count == 0)
            {
                param.Add(new SysInfo.Param("@RoleCode", string.Empty));
                param.Add(new SysInfo.Param("@RoleName", string.Empty));
                param.Add(new SysInfo.Param("@Status",string.Empty));
                param.Add(new SysInfo.Param("@XgDateBegin", string.Empty));
                param.Add(new SysInfo.Param("@XgDateEnd", string.Empty));
            }
            rst = new List<Models.Role>();
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            total = 0;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListRole", ps);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Role role = new Role();
                        role.ID = ConvertToString(row["id"]);
                        role.RoleCode = ConvertToString(row["RoleCode"]);
                        role.RoleName = ConvertToString(row["RoleName"]);
                        role.Status = (StatusEnum)ConvertToShort(row["Status"]);
                        role.XgDate = ConvertToDatetime(row["XgDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        rst.Add(role);
                    }
                }
                total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);//总记录数
                msg = SysInfo.SysMessageTxt.SYS_SEARCH_SUCCESS;
                return true;
            }
            catch(Exception ex)
            {
                msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
                WriteLog(ex.Message);
                total = 0;
                return false;
            }
        }


        public override Role Get(string ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定用户对指定功能的权限标志
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetRightFlag(string userID,string controller)
        {
            string rightFlag = "00000";
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetRoleRight", new SqlParameter[] {
                    new SqlParameter("@UserID",userID),
                    new SqlParameter("@EnterPath",controller)
                });
                if (ds.Tables.Count > 0)
                {
                    rightFlag = ConvertToString(ds.Tables[0].Rows[0]["RightFlag"]);
                }
                return rightFlag;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return rightFlag;
            }
        }
    }
}