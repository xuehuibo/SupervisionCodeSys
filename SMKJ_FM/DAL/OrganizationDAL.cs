using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Models;
using SysInfo;

namespace DAL
{
    public class OrganizationDAL : BaseDAL<Organization>
    {
        public OrganizationDAL(String DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存组织
        /// </summary>
        /// <param name="obj">组织对象</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public override bool Save(Organization obj, out string msg)
        {
            try
            {
                int i = 0;
                SqlParameter[] ps;
                BuildParam(out ps, new SysInfo.Param[] { 
                    new SysInfo.Param("@ID",obj.ID),
                    new SysInfo.Param("@OrgCode",obj.OrgCode),
                    new SysInfo.Param("@OrgName",obj.OrgName),
                    new SysInfo.Param("@OrgType",(short)(obj.OrgType),SqlDbType.SmallInt),
                    new SysInfo.Param("@Status",(short)(obj.Status),SqlDbType.SmallInt),
                    new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100, ParameterDirection.Output)
                });
                SqlEngine.RunProcedure("PROC_SaveOrg",
                    ps,
                    out i
                );
                    msg=ConvertToString(ps.Last().Value);
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
        /// 删除组织
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public override bool Delete(string id, out string msg)
        {
            try
            {
                int i = 0;
                SqlParameter[] ps;
                BuildParam(out ps, new SysInfo.Param[] { 
                    new SysInfo.Param("@ID",id),new SysInfo.Param("@msg",null,SqlDbType.NVarChar,100, ParameterDirection.Output)
                });
                SqlEngine.RunProcedure("[PROC_DelOrg]", ps, out i);
                if (i > 0)
                {
                    msg = SysInfo.SysMessageTxt.SYS_DELETE_SUCCESS;
                    return true;
                }
                else
                {
                    msg =ConvertToString(ps.Last().Value);
                    return false;
                }
            }
            catch(Exception ex)
            {
                msg = SysInfo.SysMessageTxt.SYS_DELETE_FAILED;
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 检索组织
        /// </summary>
        /// <param name="param">参数列表</param>
        /// <param name="rst">返回结果</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public override bool Select(List<Param> param, int page, int size, out List<Organization> rst, out int total, out string msg)
        {
            rst = new List<Models.Organization>();
            List<Models.Organization> tmpList = new List<Models.Organization>();
              SqlParameter[] sqlParam;
              if (param.Count == 0)
              {
                  param.Add(new SysInfo.Param("@OrgCode",string.Empty));
                  param.Add(new SysInfo.Param("@OrgName", string.Empty));
                  param.Add(new SysInfo.Param("@OrgType", string.Empty));
                  param.Add(new SysInfo.Param("@Status", string.Empty));
                  param.Add(new SysInfo.Param("@XgDateBegin", string.Empty));
                  param.Add(new SysInfo.Param("@XgdateEnd", string.Empty));
              }
            BuildParam(out sqlParam,param.ToArray());

            total = 0;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet( size,page,CommandType.StoredProcedure, "PROC_ListOrg",sqlParam);
                #region 读取全部组织
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Organization org = new Models.Organization();
                        org.ID = ConvertToInt(row["id"]).ToString();
                        org.OrgCode = ConvertToString(row["OrgCode"]);
                        org.OrgName = ConvertToString(row["OrgName"]);
                        org.OrgType = (Models.OrgTypeEnum)ConvertToShort(row["OrgType"]);
                        org.Status = (StatusEnum)ConvertToShort(row["Status"]);
                        org.XgDate = ConvertToDatetime(row["XgDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        org.children = new List<Models.Organization>();
                        tmpList.Add(org);
                    }
                }
                #endregion

                #region 系统组织
                if (tmpList.Where(o => o.OrgCode == "SYS").Count() > 0)
                {
                    Organization org=tmpList.Where(o => o.OrgCode == "SYS").First();
                    rst.Add(org);
                    tmpList.Remove(org);
                }
                #endregion
                int len=2;
                if (tmpList.Count > 0)
                {
                    tmpList.Sort(new Org_Sort_ByOrgCodeLen());
                    len = tmpList.First().OrgCode.Length;//获取最顶级组织编号长度
                }
                #region 其他组织
                List<Organization> list = tmpList.Where(o => o.OrgCode.Length == len).ToList();
                list.Sort(new Org_Sort_ByOrgCodeLen());
                rst.AddRange(list);
                BuildOrgTree(rst, tmpList.Where(o => o.OrgCode.Length > 2).ToList());
                #endregion
                msg = SysInfo.SysMessageTxt.SYS_SEARCH_SUCCESS;
                total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
                return true;


            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
                total = 0;
                return false;
            }

        }

        /// <summary>
        /// 构造菜单树
        /// </summary>
        /// <param name="parentMenu">父级组织</param>
        /// <param name="menuList">菜单列表</param>
        private void BuildOrgTree(List<Organization> parentMenu, List<Organization> menuList)
        {

            foreach (Models.Organization org in parentMenu)
            {
                string pOrgCode = org.OrgCode;
                List<Models.Organization> chd = menuList.Where(m => m.OrgCode.Length == pOrgCode.Length + 2
                    && m.OrgCode.StartsWith(pOrgCode)).ToList<Models.Organization>();
                chd.Sort(new Org_Sort_ByOrgCodeLen());
                org.children.AddRange(chd);

                if (org.children.Count > 0)
                {
                    org.children.Sort(new Org_Sort_ByOrgCodeLen());
                    org.state = "closed";
                    BuildOrgTree(org.children, menuList);
                }
            }
        }


        public override Organization Get(string ID)
        {
            throw new NotImplementedException();
        }
    }

    public class Org_Sort_ByOrgCodeLen : IComparer<Models.Organization>
    {
        #region IComparer<Organization> 成员

        public int Compare(Models.Organization x, Models.Organization y)
        {
            if (x != null && y != null)
            {
                if (x.OrgCode.Length == y.OrgCode.Length)
                {
                    return string.Compare(x.OrgCode, y.OrgCode);
                }
                else
                {
                    return x.OrgCode.Length > y.OrgCode.Length ? 1 : -1;
                }
                
            }
            return 0;
        }

        #endregion
    }
}