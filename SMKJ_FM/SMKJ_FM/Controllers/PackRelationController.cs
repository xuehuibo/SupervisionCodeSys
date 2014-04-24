using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMKJ_FM.Controllers
{
    public class PackRelationController : Controller
    {
        //
        // GET: /PackRelation/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 关联关系操作
        /// </summary>
        /// <param name="DoFlag"></param>
        /// <param name="SCode"></param>
        /// <param name="DCode"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Do(string DoFlag, string SCode,string DCode)
        {
            SysInfo.Message msg = new SysInfo.Message();
            switch (DoFlag)
            {
                case "Replace":
                    if (string.IsNullOrEmpty(SCode))
                    {
                        msg.Success = false;
                        msg.Msg = "源码不能为空！";
                        break;
                    }
                    if (string.IsNullOrEmpty(DCode))
                    {
                        msg.Success = false;
                        msg.Msg = "目的码不能为空！";
                        break;
                    }
                    using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.SysSetting.DBCCN))
                    {
                        msg.Success = dal.ReplaceCode(SCode, DCode, out msg.Msg);
                    }
                    break;
                case "UnRelation":
                                        if (string.IsNullOrEmpty(SCode))
                    {
                        msg.Success = false;
                        msg.Msg = "子码不能为空！";
                        break;
                    }
                    if (string.IsNullOrEmpty(DCode))
                    {
                        msg.Success = false;
                        msg.Msg = "父码不能为空！";
                        break;
                    }
                    using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.SysSetting.DBCCN))
                    {
                        msg.Success = dal.UnRelation(SCode, DCode, out msg.Msg);
                    }
                    break;
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
