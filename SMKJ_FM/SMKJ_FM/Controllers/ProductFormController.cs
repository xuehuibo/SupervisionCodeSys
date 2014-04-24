using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Collections;

namespace SMKJ_FM.Controllers
{
    public class ProductFormController : ApiController
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Hashtable GetDBConnection()
        {
            Hashtable db = new Hashtable();
            db.Add("DBCCN", SysInfo.SysSetting.DBCCN);
            return db;
        }

/// <summary>
/// 测试配置信息
/// </summary>
/// <param name="WorkComputerName"></param>
/// <param name="msg"></param>
/// <returns></returns>
        [HttpGet]
        public bool TestConfig(string WorkComputerName)
        {
            List<Models.WorkComputer> resList;
            List<SysInfo.Param> plist = new List<SysInfo.Param>();
            plist.Add(new SysInfo.Param("@WorkComputerName",WorkComputerName));
            plist.Add(new SysInfo.Param("@ComputerIP",string.Empty));
            int total;
            string msg;
            using (DAL.WorkComputerDAL dal = new DAL.WorkComputerDAL(SysInfo.SysSetting.DBCCN))
            {
                dal.Select(plist, 0, 99999, out resList, out total, out msg);
            }
            if (total > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
