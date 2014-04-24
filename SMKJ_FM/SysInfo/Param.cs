using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SysInfo
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public class Param
    {
        public Param()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ParamName">参数名称</param>
        /// <param name="_Value">参数值</param>
        public Param(string _ParamName, string _Value)
        {
            this.ParamName = _ParamName;
            this.Value = _Value;
            this.SqlParamType = SqlDbType.NVarChar;
            this.ParamSize = -1;
            this.ParamDirection = ParameterDirection.Input;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ParamName">参数名称</param>
        /// <param name="_Value">参数值</param>
        /// <param name="_Direction">参数方向</param>
        public Param(string _ParamName, string _Value, ParameterDirection _Direction)
        {
            this.ParamName = _ParamName;
            this.Value = _Value;
            this.SqlParamType = SqlDbType.NVarChar;
            this.ParamSize = -1;
            this.ParamDirection = _Direction;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ParamName">参数名称</param>
        /// <param name="_Value">参数值</param>
        /// <param name="_SqlParamType">参数类型</param>
        public Param(string _ParamName,object _Value,SqlDbType _SqlParamType)
        {
            this.ParamName = _ParamName;
            this.Value = _Value;
            this.SqlParamType=_SqlParamType;
            this.ParamSize = -1;
            this.ParamDirection = ParameterDirection.Input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ParamName">参数名称</param>
        /// <param name="_Value">参数值</param>
        /// <param name="_SqlParamType">参数类型</param>
        /// <param name="_Size">参数长度</param>
        public Param(string _ParamName, string _Value, SqlDbType _SqlParamType, int _Size)
        {
            this.ParamName = _ParamName;
            this.Value = _Value;
            this.SqlParamType = _SqlParamType;
            this.ParamSize = _Size;
            this.ParamDirection = ParameterDirection.Input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ParamName">参数名称</param>
        /// <param name="_Value">参数值</param>
        /// <param name="_SqlParamType">参数类型</param>
        /// <param name="_Size">参数长度</param>
        /// <param name="_Direction">参数方向</param>
        public Param(string _ParamName, string _Value, SqlDbType _SqlParamType, int _Size, ParameterDirection _Direction)
        {
            this.ParamName = _ParamName;
            this.Value = _Value;
            this.SqlParamType = _SqlParamType;
            this.ParamSize = _Size;
            this.ParamDirection = _Direction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ParamName">参数名称</param>
        /// <param name="_Value">参数值</param>
        /// <param name="_SqlParamType">参数类型</param>
        /// <param name="_Direction">参数方向</param>
        public Param(string _ParamName, string _Value, SqlDbType _SqlParamType, ParameterDirection _Direction)
        {
            this.ParamName = _ParamName;
            this.Value = _Value;
            this.SqlParamType = _SqlParamType;
            this.ParamSize = -1;
            this.ParamDirection = _Direction;
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName
        {
            get;
            set;
        }

         /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 参数数据库类型
        /// </summary>
        public SqlDbType SqlParamType
        {
            get;
            set;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int ParamSize
        {
            get;
            set;
        }

        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection ParamDirection
        {
            get;
            set;
        }

        /// <summary>
        /// sqlParameter
        /// </summary>
        public SqlParameter SqlParam
        {
            get
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = this.ParamName;
                p.SqlDbType = this.SqlParamType;
                p.Value = this.Value;
                if (this.ParamSize > 0)
                {
                    p.Size = ParamSize;
                }
                p.Direction = this.ParamDirection;

                return p;
            }
        }

    }
}