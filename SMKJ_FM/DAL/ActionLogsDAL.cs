using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 系统操作日志操作类
    /// </summary>
    public class ActionLogsDAL : BaseDAL<Models.ActionLogs>
    {
        public ActionLogsDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="obj">日志内容</param>
        /// <param name="msg">返回信息</param>
        /// <returns>是否成功</returns>
        public override bool Save(Models.ActionLogs obj, out string msg)
        {

            try
            {
                int i = 0;
                SqlEngine.RunProcedure("PROC_SaveSysLogs", new SqlParameter[]{
                        SqlEngine.MakeParam("@AUser",obj.User.UserCode,System.Data.ParameterDirection.Input),
                        SqlEngine.MakeParam("@Controller",obj.Controller,System.Data.ParameterDirection.Input),
                        SqlEngine.MakeParam("@Action",obj.Action,System.Data.ParameterDirection.Input),
                        SqlEngine.MakeParam("@Success",obj.success,System.Data.SqlDbType.Bit,System.Data.ParameterDirection.Input),
                        SqlEngine.MakeParam("@SysInfo",obj.info,System.Data.ParameterDirection.Input)
                    }, 
                    out i);
                if (i > 0)
                {
                    msg = SysInfo.SysMessageTxt.SYS_SAVE_SUCCESS;
                    return true;
                }
                else
                {
                    msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
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


        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.ActionLogs> rst, out int total, out string msg)
        {
            throw new NotImplementedException();
        }

        public override Models.ActionLogs Get(string ID)
        {
            throw new NotImplementedException();
        }
    }
}