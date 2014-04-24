using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Models;
using SysInfo;
namespace DAL
{
    public class UserLoginDAL : BaseDAL<Models.LoginUser>,IDisposable
    {

        public UserLoginDAL(string DBConnection)
            : base(DBConnection) {
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="_userCode"></param>
        /// <param name="_uPassword"></param>
        /// <returns></returns>
        public Models.LoginUser Login(string _userCode, string _uPassword, string _orgCode, out bool success, out string msg)
        {
            Models.LoginUser user = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_UserLogin",
                    new SqlParameter[]{
                    SqlEngine.MakeParam("@UserCode",_userCode,ParameterDirection.Input),
                    SqlEngine.MakeParam("@UPassword",_uPassword,ParameterDirection.Input),
                    SqlEngine.MakeParam("@OrgCode",_orgCode,ParameterDirection.Input)
                });

                if (ds.Tables.Count > 0)
                {
                    user = new Models.LoginUser();
                    Models.Role role = new Models.Role();
                    Models.Organization org = new Models.Organization();

                    #region 组装user
                    ((LoginUser)user).ID = ConvertToInt(ds.Tables[0].Rows[0]["id"]).ToString();
                    ((LoginUser)user).UserCode = ConvertToString(ds.Tables[0].Rows[0]["UserCode"]);
                    ((LoginUser)user).UserName = ConvertToString(ds.Tables[0].Rows[0]["UserName"]);
                    #endregion

                    #region 组装角色
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        role.ID = ConvertToInt(ds.Tables[1].Rows[0]["id"]).ToString();
                        role.RoleCode = ConvertToString(ds.Tables[1].Rows[0]["RoleCode"]);
                        role.RoleName = ConvertToString(ds.Tables[1].Rows[0]["RoleName"]);
                        role.Status = (StatusEnum)ConvertToShort(ds.Tables[1].Rows[0]["Status"]);
                    }
                    #endregion

                    #region 组装组织
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        org.ID = ConvertToInt(ds.Tables[2].Rows[0]["id"]).ToString();
                        org.OrgCode = ConvertToString(ds.Tables[2].Rows[0]["OrgCode"]);
                        org.OrgName = ConvertToString(ds.Tables[2].Rows[0]["OrgName"]);
                        org.OrgType = (OrgTypeEnum)ConvertToShort(ds.Tables[2].Rows[0]["OrgType"]);
                        org.Status = (StatusEnum)ConvertToShort(ds.Tables[2].Rows[0]["Status"]);
                    }
                    #endregion

                    #region 组装角色菜单
                    role.RoleMenu = new List<Menus>();
                    List<Menus> menuList = new List<Menus>();
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[3].Rows)
                        {
                            Menus menu = new Menus();
                            menu.id = ConvertToString(row["MenuCode"]);
                            menu.text = ConvertToString(row["MenuName"]);
                            menu.attributes = new MenuAttributes();
                            menu.attributes.Url = ConvertToString(row["EnterPath"]);
                            menu.RightFlag = ConvertToString(row["RightFlag"]);
                            menu.state = "open";
                            if (menu.id.Length == 2)
                            {
                                //menu.id = ConvertToString(row["MenuCode"]);
                                //menu.text = ConvertToString(row["MenuName"]);
                                //menu.attributes.Url = ConvertToString(row["EnterPath"]);

                                role.RoleMenu.Add(menu);
                            }
                            else
                            {
                                menuList.Add(menu);
                            }
                        }

                        #region 取所有根节点
                        #endregion
                        BuildMenuTree(role.RoleMenu, menuList);
                        menuList.Clear();
                    }
                    #endregion


                    ((LoginUser)user).UserRole = role;
                    ((LoginUser)user).UserOrg = org;
                    msg = SysInfo.SysMessageTxt.SYS_LOGIN_SUCCESS;
                    success = true;
                    return user;

                }
                else
                {
                    user = null;
                    msg= SysMessageTxt.SYS_LOGIN_FAILED;
                    success = false;
                    return user;
                }
            }
            catch (Exception ex)
            {
                user=null;
                msg = ex.Message;
                WriteLog(ex.Message);
                success = false;
                return user;
            }
        }

       /// <summary>
       /// 构造菜单树
       /// </summary>
       /// <param name="parentMenu">父级菜单</param>
       /// <param name="menuList">菜单列表</param>
        private void BuildMenuTree(List<Menus> parentMenu,List<Menus> menuList)
        {
            
            foreach (Menus menu in parentMenu)
            {
                string pMenuCode = menu.id;
                menu.children = menuList.Where(m => m.id.Length == pMenuCode.Length + 2 
                    && m.id.StartsWith(pMenuCode)).ToList<Menus>();
                if (menu.children.Count > 0)
                {
                    menu.state = "closed";
                    BuildMenuTree(menu.children, menuList);
                }
            }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="oldPassword">老密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public bool ChangePassword(string userId, string oldPassword, string newPassword,out string msg)
        {
            try
            {
                SqlParameter msgParam = new SqlParameter("@msg", SqlDbType.NVarChar, 50);
                msgParam.Direction = ParameterDirection.Output;

                int i = 0;
                SqlEngine.RunProcedure("PROC_ChangePassword",
                    new SqlParameter[]{
                        new SqlParameter("@UserId",userId),
                        new SqlParameter("@OldPassword",oldPassword),
                        new SqlParameter("@NewPassword",newPassword),
                        msgParam
                    },
                    out i);
                msg = msgParam.Value.ToString();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
                WriteLog(msg);
                return false;
            }
        }



        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }


        public override bool Save(LoginUser obj, out string msg)
        {
            throw new NotImplementedException();
        }
        public override bool Select(List<Param> param, int page, int size, out List<LoginUser> rst, out int total, out string msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过ID获取登陆用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override LoginUser Get(string ID)
        {
            Models.LoginUser user = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetUserLogin",
                    new SqlParameter[]{
                    SqlEngine.MakeParam("@ID",ID,ParameterDirection.Input),
                });

                if (ds.Tables.Count > 0)
                {
                    user = new Models.LoginUser();
                    Models.Role role = new Models.Role();
                    Models.Organization org = new Models.Organization();

                    #region 组装user
                    ((LoginUser)user).ID = ConvertToInt(ds.Tables[0].Rows[0]["id"]).ToString();
                    ((LoginUser)user).UserCode = ConvertToString(ds.Tables[0].Rows[0]["UserCode"]);
                    ((LoginUser)user).UserName = ConvertToString(ds.Tables[0].Rows[0]["UserName"]);
                    #endregion

                    #region 组装角色
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        role.ID = ConvertToInt(ds.Tables[1].Rows[0]["id"]).ToString();
                        role.RoleCode = ConvertToString(ds.Tables[1].Rows[0]["RoleCode"]);
                        role.RoleName = ConvertToString(ds.Tables[1].Rows[0]["RoleName"]);
                        role.Status = (StatusEnum)ConvertToShort(ds.Tables[1].Rows[0]["Status"]);
                    }
                    #endregion

                    #region 组装组织
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        org.ID = ConvertToInt(ds.Tables[2].Rows[0]["id"]).ToString();
                        org.OrgCode = ConvertToString(ds.Tables[2].Rows[0]["OrgCode"]);
                        org.OrgName = ConvertToString(ds.Tables[2].Rows[0]["OrgName"]);
                        org.OrgType = (OrgTypeEnum)ConvertToShort(ds.Tables[2].Rows[0]["OrgType"]);
                        org.Status = (StatusEnum)ConvertToShort(ds.Tables[2].Rows[0]["Status"]);
                    }
                    #endregion

                    #region 组装角色菜单
                    role.RoleMenu = new List<Menus>();
                    List<Menus> menuList = new List<Menus>();
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[3].Rows)
                        {
                            Menus menu = new Menus();
                            menu.id = ConvertToString(row["MenuCode"]);
                            menu.text = ConvertToString(row["MenuName"]);
                            menu.attributes = new MenuAttributes();
                            menu.attributes.Url = ConvertToString(row["EnterPath"]);
                            menu.RightFlag = ConvertToString(row["RightFlag"]);
                            menu.state = "open";
                            if (menu.id.Length == 2)
                            {
                                //menu.id = ConvertToString(row["MenuCode"]);
                                //menu.text = ConvertToString(row["MenuName"]);
                                //menu.attributes.Url = ConvertToString(row["EnterPath"]);

                                role.RoleMenu.Add(menu);
                            }
                            else
                            {
                                menuList.Add(menu);
                            }
                        }

                        #region 取所有根节点
                        #endregion
                        BuildMenuTree(role.RoleMenu, menuList);
                        menuList.Clear();
                    }
                    #endregion


                    ((LoginUser)user).UserRole = role;
                    ((LoginUser)user).UserOrg = org;
                    return user;

                }
                else
                {
                    user = null;
                    return user;
                }
            }
            catch (Exception ex)
            {
                user = null;
                WriteLog(ex.Message);
                return user;
            }
        }
    }
}