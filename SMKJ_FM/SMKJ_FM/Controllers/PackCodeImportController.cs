using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SMKJ_FM.Controllers
{
    public class PackCodeImportController : Controller
    {
        //
        // GET: /PackCodeImport/

        public ActionResult Index()
        {
            ViewBag.Title = "监管码导入";
            return View();
        }

        /// <summary>
        /// 上传监管码
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase fileData, string userID)
        {
            string fileName = fileData.FileName;
            StreamReader SR = new StreamReader(fileData.InputStream);
            string Info = SR.ReadLine();
            string[] header = Info.Split('#');

            string ResCode = header[2];
            string SubType = header[3];
            string levelNo = header[4];
            string codeBegin = header[5];
            string codeEnd = header[6];
            string CodeHead = header[7];
            Models.ProductRes Res;
            using (DAL.ProductDAL dal = new DAL.ProductDAL(SysInfo.SysSetting.DBCCN))
            {
                Res = dal.GetProductRes(ResCode);
            }

            #region 建表
            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add("PackCode");
            ds.Tables[0].Columns.Add("PackCode", typeof(string));
            ds.Tables[0].Columns.Add("PackSpecID", typeof(short));
            ds.Tables[0].Columns.Add("LevelNo", typeof(short));
            ds.Tables[0].Columns.Add("PrintFlag", typeof(short));
            ds.Tables[0].Columns.Add("UpdateTime", typeof(DateTime));
            ds.Tables[0].Columns.Add("Remark", typeof(string));
            #endregion

            while (!SR.EndOfStream)
            {
                System.Data.DataRow row = ds.Tables[0].NewRow();
                row["PackCode"]= SR.ReadLine();
                row["PackSpecID"] = Res.PackageSpec.ID;
                row["LevelNo"] = Res.LevelNo;
                row["PrintFlag"] = 0;
                row["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ds.Tables[0].Rows.Add(row);
            }
            SysInfo.Message message = new SysInfo.Message();
            using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.SysSetting.DBCCN))
            {
                int i;
                message.Success=dal.UploadPackageCode(ds,out i);
                message.Obj = i;
            }
            message.Msg = message.Success ? SysInfo.SysMessageTxt.SYS_SAVE_SUCCESS : SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
            JsonResult jr = Json(message);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
