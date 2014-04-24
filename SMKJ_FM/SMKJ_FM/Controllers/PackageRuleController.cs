using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace SMKJ_FM.Controllers
{
    public class PackageRuleController : Controller
    {
        //
        // GET: /PackageRule/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(string ID, string PackageRuleName, string Status,string Remark,string CreateUserID,string ItemsJson)
        {
            return Save(ID,PackageRuleName,Status,Remark,CreateUserID, ItemsJson);
        }
        [HttpPost]
        public ActionResult Delete(string ID)
        {
            SysInfo.Message msg = new SysInfo.Message();
            using (DAL.PackageRuleDAL dal = new DAL.PackageRuleDAL(SysInfo.SysSetting.DBCCN))
            {
                msg.Success = dal.Delete(ID, out msg.Msg);
            }
            JsonResult jr = Json(msg);
            jr.ContentType = "text/html";
            return jr;
        }
        [HttpPost]
        public ActionResult Update(string ID, string PackageRuleName, string Status, string Remark, string CreateUserID,string ItemsJson)
        {
            CreateUserID = string.Empty;
            return Save(ID, PackageRuleName, Status, Remark,CreateUserID, ItemsJson);
        }
        [HttpPost]
        public ActionResult Select(string PackageRuleName, string LevelCount, string CascadeCode, string Status,string page,string rows)
        {
            SysInfo.DatagridPage<Models.PackageRule> rst = new SysInfo.DatagridPage<Models.PackageRule>();
            using (DAL.PackageRuleDAL dal = new DAL.PackageRuleDAL(SysInfo.SysSetting.DBCCN))
            {
                List<SysInfo.Param> plist = new List<SysInfo.Param>();
                plist.Add(new SysInfo.Param("@PackageRuleName", string.IsNullOrEmpty(PackageRuleName) ? string.Empty : PackageRuleName));
                plist.Add(new SysInfo.Param("@LevelCount", string.IsNullOrEmpty(LevelCount) ? string.Empty : LevelCount));
                plist.Add(new SysInfo.Param("@CascadeCode", string.IsNullOrEmpty(CascadeCode) ? string.Empty : CascadeCode));
                plist.Add(new SysInfo.Param("@Status", string.IsNullOrEmpty(Status) ? string.Empty : Status));
                string msg;
                dal.Select(plist,int.Parse(page),int.Parse(rows),out rst.rows,out rst.total,out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/html";
            return jr;
        }

        private ActionResult Save(string ID, string PackageRuleName, string Status,string Remark,string CreateUserID,string ItemsJson)
        {
          

            SysInfo.Message msg = new SysInfo.Message();
            Models.PackageRule packageRule = new Models.PackageRule();
            packageRule.ID = ID;
            packageRule.PackageRuleName = PackageRuleName;
            packageRule.Status = (Models.StatusEnum)short.Parse(Status);
            packageRule.Remark = Remark;
            packageRule.CreateUser = new Models.User();
            packageRule.CreateUser.ID = CreateUserID;
            packageRule.PackageRule_Item = JsonConvert.DeserializeObject<List<Models.PackageRuleItem>>(ItemsJson);
            int L1=0;
            int L2=0;
            int L3 = 0;
            int L4 = 0;
            foreach (Models.PackageRuleItem item in packageRule.PackageRule_Item)
            {
                switch (item.LevelNo)
                {
                    case 1: L1 = item.Amount;
                        break;
                    case 2: L2 = item.Amount;
                        break;
                    case 3: L3 = item.Amount;
                        break;
                    case 4: L4 = item.Amount;
                        break;
                }
            }
            packageRule.CascadeCode = L1.ToString();
            if (L2 > 0)
            {
                packageRule.CascadeCode += ":" + L2.ToString();
            }
            if (L3 > 0)
            {
                packageRule.CascadeCode += ":" + L3.ToString();
            }
            if (L4 > 0)
            {
                packageRule.CascadeCode += ":" + L4.ToString();
            }


            //保存包装规则主信息
            using (DAL.PackageRuleDAL dal = new DAL.PackageRuleDAL(SysInfo.SysSetting.DBCCN))
           {
              msg.Success=dal.Save(packageRule,out packageRule.ID,out msg.Msg);
           }
            //保存明细信息
           using (DAL.PackageRuleItemDAL dal = new DAL.PackageRuleItemDAL(SysInfo.SysSetting.DBCCN))
           {
               string s;
               if (dal.DeleteByPackageRuleID(packageRule.ID, out s))
               {
                   foreach (Models.PackageRuleItem item in packageRule.PackageRule_Item)
                   {
                       item.PackageRuleID = packageRule.ID;
                       msg.Success = dal.Save(item, out msg.Msg);
                       if (!msg.Success)
                       {
                           break;
                       }
                   }
               }
           }
           JsonResult jr = Json(msg);
           jr.ContentType = "text/html";
            return jr;
        }

    }
}
