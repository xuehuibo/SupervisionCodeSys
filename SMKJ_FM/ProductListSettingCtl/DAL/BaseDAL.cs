using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using SysInfo;
using System.Data.SqlClient;
namespace DAL
{
    /// <summary>
    /// 基础类
    /// </summary>
    /// <typeparam name="T">操作模型Type</typeparam>
     abstract class BaseDAL<T>:SqlServerDll.SqlServerBaseAction
    {
        public BaseDAL(string DBConnection)
            : base(DBConnection) {}

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public void WriteLog(string msg)
        {
            StackTrace st = new StackTrace(true);
            LogWriter.WriteErrorLog(this.GetType().ToString(),
            st.GetFrame(1).GetMethod().Name.ToString(),
            msg);
        }

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="msg">返回信息</param>
        /// <returns>是否成功</returns>
        public abstract bool Save(T obj);

       /// <summary>
       /// 删除对象
       /// </summary>
       /// <param name="id">对象ID</param>
       /// <param name="msg">返回信息</param>
       /// <returns>是否成功</returns>
        public abstract bool Delete(string id);

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="page">页号</param>
        /// <param name="size">页大小</param>
        /// <param name="rst">返回结果集</param>
        /// <param name="msg">返回信息</param>
        /// <returns>是否成功</returns>
        public abstract bool Select(List<SysInfo.Param> param,out List<T> rst);

        /// <summary>
        /// 通过ID获取对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract T Get(string ID);
        /// <summary>
        /// 构造sql参数
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static void BuildParam(out SqlParameter[] sqlParameter,SysInfo.Param[] param)
        {
            List<SqlParameter> rst = new List<SqlParameter>();
            if (param != null)
            {
                foreach (SysInfo.Param p in param)
                {
                    rst.Add(p.SqlParam);
                }
            }
            sqlParameter = rst.ToArray();
        }

    }
}