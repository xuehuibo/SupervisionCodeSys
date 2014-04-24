using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SysInfo;
using System.Data;
using Models;
namespace DAL
{
    public class UserDAL:BaseDAL<Models.User>
    {
        public UserDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Save(Models.User obj, out string msg)
        {
            int i = 0;
            SqlParameter[] ps;
            BuildParam(out ps, new Param[] { 
                new Param("@ID",obj.ID),
                new Param("@UserCode",obj.UserCode),
                new Param("@UserName",obj.UserName),
                new Param("@RoleCode",obj.RoleCode),
                new Param("@OrgCode",obj.OrgCode),
                new Param("@Status",(short)obj.Status,SqlDbType.SmallInt),
                new Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
            });

            try
            {
                SqlEngine.RunProcedure("PROC_SaveUser", ps, out i);
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
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED ;
                return false;
            }

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Delete(string id, out string msg)
        {
            SqlParameter[] ps;
            BuildParam(out ps, new Param[] { 
                new Param("@id",id),
                new Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
            });
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_DelUser", ps, out i);
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
        /// 检索用户
        /// </summary>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rst"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Select(List<Param> param, int page, int size, out List<User> rst, out int total, out string msg)
        {
            rst = new List<User>();
            SqlParameter[] ps;
            DataSet ds;
            total = 0;
            msg = SysMessageTxt.SYS_SEARCH_FAILED;
            BuildParam(out ps, param.ToArray());
            try
            {
                ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListUser", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        User u = new User();
                        u.ID = ConvertToInt(row["ID"]).ToString();
                        u.UserCode = ConvertToString(row["UserCode"]);
                        u.UserName = ConvertToString(row["UserName"]);
                        u.RoleCode = ConvertToString(row["RoleCode"]);
                        u.RoleName = ConvertToString(row["RoleName"]);
                        u.OrgCode = ConvertToString(row["OrgCode"]);
                        u.OrgName = ConvertToString(row["OrgName"]);
                        u.Status = (StatusEnum)ConvertToShort(row["Status"]);
                        u.XgDate = ConvertToDatetime(row["XgDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        rst.Add(u);
                    }
                    total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
                    msg = SysMessageTxt.SYS_SEARCH_SUCCESS;
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



        public override User Get(string ID)
        {
            Models.User user = new User();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetUser", new SqlParameter[] { new SqlParameter("@ID", ID) });
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    user.ID = ConvertToString(ds.Tables[0].Rows[0]["id"]);
                    user.UserCode = ConvertToString(ds.Tables[0].Rows[0]["UserCode"]);
                    user.UserName = ConvertToString(ds.Tables[0].Rows[0]["UserName"]);
                    user.RoleCode = ConvertToString(ds.Tables[0].Rows[0]["RoleCode"]);
                    user.RoleName = ConvertToString(ds.Tables[0].Rows[0]["RoleName"]);
                    user.OrgCode = ConvertToString(ds.Tables[0].Rows[0]["OrgCode"]);
                    user.OrgName = ConvertToString(ds.Tables[0].Rows[0]["OrgName"]);
                    user.Status = (Models.StatusEnum)ConvertToShort(ds.Tables[0].Rows[0]["Status"]);
                    user.XgDate = ConvertToDatetime(ds.Tables[0].Rows[0]["XgDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return user;
        }
    }
}