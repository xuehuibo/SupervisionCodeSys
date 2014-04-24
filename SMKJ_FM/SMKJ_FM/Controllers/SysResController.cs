using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace SMKJ_FM.Controllers
{
    /// <summary>
    /// 获取系统组件所需参数
    /// </summary>
    public class SysResController : Controller
    {
        /// <summary>
        /// 获取状态信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetStatus()
        {
            Models.SysResModel[] Status = {
                                         new Models.SysResModel((short)StatusEnum.启用,StatusEnum.启用.ToString()),
                                         new Models.SysResModel((short)StatusEnum.未启用,StatusEnum.未启用.ToString()),
                                          new Models.SysResModel((short)StatusEnum.停用,StatusEnum.停用.ToString()),
                                         };
            JsonResult jr = Json(Status);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrg()
        {
            List<Organization> orgList;
            int total = 0;
            string msg;

            using (DAL.OrganizationDAL dal = new DAL.OrganizationDAL(SysInfo.SysSetting.DBCCN))
            {
                dal.Select(new List<SysInfo.Param>(
                    ), 1, 99999, out orgList, out total, out msg);
            }
            JsonResult js = Json(orgList);
            js.ContentType = "text/html";
            return js;
        }


        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRole()
        {
            List<Models.Role> roleList;
            using (DAL.RoleDAL dal = new DAL.RoleDAL(SysInfo.SysSetting.DBCCN))
            {
                int total;
                string msg;
                dal.Select(new List<SysInfo.Param>(), 1, 99999, out roleList, out total, out msg);
            }
            JsonResult js = Json(roleList);
            js.ContentType = "text/html";
            return js;
        }



        /// <summary>
        /// 获取组织类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrgType()
        {
            Models.SysResModel[] orgtype = { 
                                           new SysResModel((short)OrgTypeEnum.财务部门,OrgTypeEnum.财务部门.ToString()),
                                           new SysResModel((short)OrgTypeEnum.管理部门,OrgTypeEnum.管理部门.ToString()),
                                           new SysResModel((short)OrgTypeEnum.其他部门,OrgTypeEnum.其他部门.ToString()),
                                           new SysResModel((short)OrgTypeEnum.系统组织,OrgTypeEnum.系统组织.ToString()),
                                           new SysResModel((short)OrgTypeEnum.营业部门,OrgTypeEnum.营业部门.ToString()),
                                           };
            JsonResult jr = Json(orgtype);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        ///  获取产品类别
        /// </summary>
        [HttpPost]
        public JsonResult GetProductCategory()
        {
            List<Models.ProductCategory> categoryList=null;
            using (DAL.ProductDAL dal = new DAL.ProductDAL(SysInfo.SysSetting.DBCCN))
            {
                categoryList = dal.ListProductCategory();
            }
            JsonResult jr = Json(categoryList);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProduct()
        {
            List<Models.Product> productList = null;
            using (DAL.ProductDAL dal = new DAL.ProductDAL(SysInfo.SysSetting.DBCCN))
            {
                    List<SysInfo.Param> plist = new List<SysInfo.Param>();
                    plist.Add(new SysInfo.Param("@ProductCode", string.Empty));
                    plist.Add(new SysInfo.Param("@ProductName", string.Empty));
                    plist.Add(new SysInfo.Param("@PermitNo", string.Empty));
                    int total;
                    string msg;
                    dal.Select(plist, 1, 99999, out productList, out total, out msg);
            }
            JsonResult jr = Json(productList);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 获取生产线
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductLine()
        {
            List<Models.ProductLine> productLineList = null;
            using (DAL.ProductLineDAL dal = new DAL.ProductLineDAL(SysInfo.SysSetting.DBCCN))
            {
                List<SysInfo.Param> plist = new List<SysInfo.Param>();
                plist.Add(new SysInfo.Param("@LineCode", string.Empty));
                plist.Add(new SysInfo.Param("@LineName", string.Empty));
                plist.Add(new SysInfo.Param("@ManageUser", string.Empty));
                plist.Add(new SysInfo.Param("@WorkShop", string.Empty));
                plist.Add(new SysInfo.Param("@Status", string.Empty));
                int total;
                string msg;
                dal.Select(plist, 1, 99999, out productLineList, out total, out msg);
            }
            JsonResult jr = Json(productLineList);
            jr.ContentType = "text/html";
            return jr;
        }
        /// <summary>
        /// 获取有效期单位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetValidTimeUnit()
        {
            SysResModel[] ValidTimeUnit = { 
                                          new SysResModel((short)ValidTimeUnitEnum.天,ValidTimeUnitEnum.天.ToString()),
                                          new SysResModel((short)ValidTimeUnitEnum.月,ValidTimeUnitEnum.月.ToString()),
                                          new SysResModel((short)ValidTimeUnitEnum.年,ValidTimeUnitEnum.年.ToString())
                                          };
            JsonResult jr = Json(ValidTimeUnit);
            jr.ContentType = "text/html";
            return jr;
        }


        /// <summary>
        /// 获取取码方式
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCodeType()
        {
            SysResModel[] getCodeType = { 
                                        new SysResModel((short)GetCodeTypeEnum.包装码区分最小包装,GetCodeTypeEnum.包装码区分最小包装.ToString()),
                                        new SysResModel((short)GetCodeTypeEnum.包装码没有区分,GetCodeTypeEnum.包装码没有区分.ToString()),
                                        new SysResModel((short)GetCodeTypeEnum.包装码区分全部级别,GetCodeTypeEnum.包装码区分全部级别.ToString())
                                        };
            JsonResult jr = Json(getCodeType);
            jr.ContentType = "text/html";
            return jr;
        }


        /// <summary>
        /// 获取设备类型信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEquipmentSetting()
        {
            List<Models.EquipmentSetting> settingList;
            int total;
            string msg;
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@ID", string.Empty));

            using (DAL.EquipmentSettingDAL dal = new DAL.EquipmentSettingDAL(SysInfo.SysSetting.DBCCN))
            {
                dal.Select(pList, 1, 99999, out settingList, out total, out msg);
            }

            JsonResult jr = Json(settingList);
            jr.ContentType = "text/html";
            return jr;
        }


        /// <summary>
        /// 获取工控机
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetWorkComputer()
        {
            List<Models.WorkComputer> workComputerList;
            int total;
            string msg;
            List<SysInfo.Param> pList = new List<SysInfo.Param>();
            pList.Add(new SysInfo.Param("@WorkComputerName", string.Empty));
            pList.Add(new SysInfo.Param("@ComputerIP", string.Empty));
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                dal.Select(pList, 1, 99999, out workComputerList, out total, out msg);
            }
            JsonResult jr = Json(workComputerList);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 获取任务状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTaskStatus()
        {
            SysResModel[] sysRes = { 
                                    new SysResModel((short)(Models.TaskStatus.未审核),Models.TaskStatus.未审核.ToString()),
                                    new SysResModel((short)(Models.TaskStatus.已返工),Models.TaskStatus.已返工.ToString()),
                                    new SysResModel((short)(Models.TaskStatus.已结束),Models.TaskStatus.已结束.ToString()),
                                    new SysResModel((short)(Models.TaskStatus.已审核),Models.TaskStatus.已审核.ToString()),
                                    new SysResModel((short)(Models.TaskStatus.已暂停),Models.TaskStatus.已暂停.ToString()),
                                    new SysResModel((short)(Models.TaskStatus.运行中),Models.TaskStatus.运行中.ToString()),
                                   };
            JsonResult jr = Json(sysRes);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 获取包装规格
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPackageRule()
        {
            List<Models.PackageRule> rst = new List<PackageRule>();
            using (DAL.PackageRuleDAL dal = new DAL.PackageRuleDAL(SysInfo.SysSetting.DBCCN))
            {
                List<SysInfo.Param> plist = new List<SysInfo.Param>();
                plist.Add(new SysInfo.Param("@PackageRuleName", string.Empty));
                plist.Add(new SysInfo.Param("@LevelCount", string.Empty));
                plist.Add(new SysInfo.Param("@CascadeCode", string.Empty));
                plist.Add(new SysInfo.Param("@Status", "1"));
                string msg;
                int total;
                dal.Select(plist, 1, 99999, out rst, out total, out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/thml";
            return jr;
        }
        ///// <summary>
        ///// 获取数据库连接参数
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult GetDBConnection()
        //{
        //    SysInfo.Message msg = new SysInfo.Message();
        //    msg.Obj = SysInfo.SysSetting.SMKJ_FM_SYS;
        //    msg.Success = true;
        //    JsonResult jr = Json(msg);
        //    jr.ContentType = "text/html";
        //    return jr;
        //}

        /// <summary>
        /// 获取停止位信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetStopBitsEnum()
        {
            SysResModel[] resModels = { 
                                      new SysResModel((short)(Models.StopBitsEnum.使用一个停止位),Models.StopBitsEnum.使用一个停止位.ToString()),
                                      new SysResModel((short)(Models.StopBitsEnum.使用两个停止位),Models.StopBitsEnum.使用两个停止位.ToString())                                      
                                      };
            JsonResult jr = Json(resModels);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 获取奇偶校验信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetParityEnum()
        {
            SysResModel[] resModels = { 
                                        new SysResModel((short)(Models.ParityEnum.不发生奇偶校验检查),Models.ParityEnum.不发生奇偶校验检查.ToString()),
                                        new SysResModel((short)(Models.ParityEnum.将奇偶校验位保留为0),Models.ParityEnum.将奇偶校验位保留为0.ToString()),
                                        new SysResModel((short)(Models.ParityEnum.将奇偶校验位保留为1),Models.ParityEnum.将奇偶校验位保留为1.ToString()),
                                        new SysResModel((short)(Models.ParityEnum.设置奇偶校验位使位数等于偶数),Models.ParityEnum.设置奇偶校验位使位数等于偶数.ToString()),
                                        new SysResModel((short)(Models.ParityEnum.设置奇偶校验位使位数等于奇数),Models.ParityEnum.设置奇偶校验位使位数等于奇数.ToString())
                                      };
            JsonResult jr = Json(resModels);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
