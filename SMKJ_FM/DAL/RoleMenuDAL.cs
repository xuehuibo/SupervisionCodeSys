using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Text;
using System.Collections;

namespace DAL
{
    public class RoleMenuDAL : BaseDAL<Models.RoleMenu>
    {
        public RoleMenuDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存角色菜单
        /// </summary>
        /// <param name="rMenus">需要保存的角色菜单List</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public bool Save(string roleCode,RoleMenu[] rMenus, out string msg)
        {
            StringBuilder strB = new StringBuilder();
            ArrayList sqlStrs = new ArrayList();
            foreach (RoleMenu m in rMenus)
            {
                sqlStrs.Add(" delete from RoleMenu where RoleCode='" + roleCode + "' and MenuCode='" + m.MenuCode + "' ");
                if (m.Allowed == "Y")
                {
                    strB.Append(" INSERT INTO [RoleMenu]([RoleCode],[MenuCode],[MenuName],[RightFlag]) ");
                    strB.Append(" VALUES (");
                    strB.Append("'" + roleCode + "',");
                    strB.Append("'" + m.MenuCode + "',");
                    strB.Append("'" + m.MenuName + "',");
                    strB.Append("'" + m.RightFlag + "') ");
                    sqlStrs.Add(strB.ToString());
                    strB.Clear();
                }
            }
            strB.Append(" update RoleMenu set EnterPath=a.EnterPath,Status=a.Status ");
            strB.Append(" from MenuAll a join  RoleMenu b on a.MenuCode=b.MenuCode ");
            strB.Append(" where RoleCode='"+roleCode+"'");
            sqlStrs.Add(strB.ToString());
            strB.Clear();
            try
            {
               bool ok= SqlEngine.ExecuteSqlTran(sqlStrs);
                sqlStrs.Clear();
                if (ok)
                {
                    msg = SysInfo.SysMessageTxt.SYS_SAVE_SUCCESS;
                }
                else
                {
                    msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                }
                return ok;

            }
            catch (Exception ex)
            {
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                WriteLog(ex.Message);
                return false;
            }

        }
        public override bool Save(RoleMenu obj, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询角色的菜单
        /// </summary>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rst"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.RoleMenu> rst, out int total, out string msg)
        {
            rst = new List<Models.RoleMenu>();
            try
            {
                SqlParameter [] ps;
                BuildParam(out ps,param.ToArray());
                List<Models.RoleMenu> tmplist = new List<Models.RoleMenu>();
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListMenuAll_RoleMenu",ps);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.RoleMenu rMenu = new Models.RoleMenu();
                        rMenu.MenuCode = ConvertToString(row["MenuCode"]);
                        rMenu.MenuName = ConvertToString(row["MenuName"]);
                        rMenu.Allowed = string.IsNullOrEmpty(ConvertToString(row["RightFlag"])) ? "N" : "Y";
                        rMenu.RightFlag = ConvertToString(row["RightFlag"]);
                        rMenu.XgDate = Convert.IsDBNull(row["XgDate"])?"": ConvertToDatetime(row["XgDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        tmplist.Add(rMenu);
                    }
                }
                int len = 2;
                if (tmplist.Count > 0)
                {
                    tmplist.Sort(new RoleMenu_Sort_ByMenuCodeLen());
                    len = tmplist.First().MenuCode.Length;//获取最顶级组织编号长度
                }
                List<Models.RoleMenu> list = tmplist.Where(o => o.MenuCode.Length == len).ToList();
                list.Sort(new RoleMenu_Sort_ByMenuCodeLen());
                rst.AddRange(list);
                BuildOrgTree(rst, tmplist.Where(o => o.MenuCode.Length > 2).ToList());
                msg = SysInfo.SysMessageTxt.SYS_SEARCH_SUCCESS;
                total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
                return true;
            }
            catch (Exception ex)
            {
                msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
                WriteLog(ex.Message);
                total = 0;
                return false;
            }
        }




        /// <summary>
        /// 构造菜单树
        /// </summary>
        /// <param name="parentMenu">父级菜单</param>
        /// <param name="menuList">菜单列表</param>
        private void BuildOrgTree(List<Models.RoleMenu> parentMenu, List<Models.RoleMenu> menuList)
        {

            foreach (Models.RoleMenu rMenu in parentMenu)
            {
                string rMenuCode = rMenu.MenuCode;
                List<Models.RoleMenu> chd = menuList.Where(m => m.MenuCode.Length == rMenuCode.Length + 2
                    && m.MenuCode.StartsWith(rMenuCode)).ToList<Models.RoleMenu>();
                chd.Sort(new RoleMenu_Sort_ByMenuCodeLen());
                if (rMenu.children == null)
                {
                    rMenu.children = new List<RoleMenu>();
                }
                rMenu.children.AddRange(chd);

                if (rMenu.children.Count > 0)
                {
                    rMenu.children.Sort(new RoleMenu_Sort_ByMenuCodeLen());
                    rMenu.state = "closed";
                    BuildOrgTree(rMenu.children, menuList);
                }
            }
        }


        public class RoleMenu_Sort_ByMenuCodeLen : IComparer<RoleMenu>
        {

            #region IComparer<RoleMenu> 成员

            public int Compare(RoleMenu x, RoleMenu y)
            {
                if (x != null && y != null)
                {
                    if (x.MenuCode.Length == y.MenuCode.Length)
                    {
                        return string.Compare(x.MenuCode, y.MenuCode);
                    }
                    else
                    {
                        return x.MenuCode.Length > y.MenuCode.Length ? 1 : -1;
                    }

                }
                return 0;
            }

            #endregion
        }

        public override RoleMenu Get(string ID)
        {
            throw new NotImplementedException();
        }
    }
}