using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Text;

namespace SMKJ_FM.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 保存产品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save()
        {
            return Json("");
        }


        /// <summary>
        /// 上传产品
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase fileData,string userID)
        {
            // 产品信息
            // 包装规格
            //资源码
            StringBuilder errorList = new StringBuilder();
            if (fileData != null)
            {
                string fname = System.IO.Path.GetFileName(fileData.FileName);
                XmlDocument doc = new XmlDocument();
                doc.Load(fileData.InputStream);
                XmlNodeList productList = doc.SelectSingleNode("productList").SelectNodes("product");
                using (DAL.ProductDAL pdal = new DAL.ProductDAL(SysInfo.SysSetting.DBCCN))
                {
                    string msg;
                    foreach (XmlNode node in productList)
                    {
                        #region 产品信息
                        //short CodeLen = 20;
                        string productCode = node.Attributes["productCode"].Value;
                        string productName = node.Attributes["productName"].Value;
                        #endregion

                        #region 子类信息

                        XmlNodeList subTypes = node.SelectNodes("subType");
                        foreach (XmlNode subTypeNode in subTypes)
                        {
                            Models.Product product = new Models.Product();
                            product.ID = string.Empty;
                            product.DocID = 1;//默认doc
                            product.SubName = string.Empty;
                            product.CreateUserID = userID;
                            product.CodeLen = 20;
                            product.ProductCode = productCode;
                            product.ProductName = productName;
                            product.SubNo = subTypeNode.Attributes["typeNo"].Value;
                            product.PermitNo = subTypeNode.Attributes["authorizedNo"].Value;
                            product.ProductType = subTypeNode.Attributes["type"].Value;
                            product.Spec = subTypeNode.Attributes["spec"].Value;
                            product.PackSpec = subTypeNode.Attributes["packageSpec"].Value;
                            product.PackUnit = subTypeNode.Attributes["packUnit"].Value;
                            product.Status = Models.StatusEnum.启用;
                            product.Detail_Type=  pdal.GetDetailTypeByCode(subTypeNode.Attributes["physicDetailType"].Value);
                            if (product.Detail_Type == null)
                            {
                                //产品明细类不存在
                                errorList.Append("产品保存错误!详细信息:产品"+product.ProductCode+"-"+product.ProductName+"的"+"明细类" + product.Detail_Type.DetailTypeCode + "-" + product.Detail_Type.DetailTypeName + "不存在");
                                continue;
                            }
                            product.Product_Category = pdal.GetProductCategory(product.Detail_Type.CategoryID);
                            if (pdal.Save(product, out msg, out product.ID))//保存产品信息,返回产品ID
                            {
                                List<Models.ProductRes> resList = new List<Models.ProductRes>();
                                List<Models.PackageSpecific> specList = new List<Models.PackageSpecific>();//已处理包装规格
                                Models.PackageSpecific spec;//当前包装规格
                                foreach (XmlNode resNode in subTypeNode.SelectSingleNode("resProdCodes").SelectNodes("resCode"))
                                {
                                    #region 码资源文件
                                    Models.ProductRes res = new Models.ProductRes();
                                    res.PackageSpec = new Models.PackageSpecific();
                                    res.CodeVersion = resNode.Attributes["codeVersion"].Value;
                                    res.LevelNo = short.Parse(resNode.Attributes["codeLevel"].Value);
                                    res.ProductID = product.ID;
                                    res.ProductResCode = resNode.InnerText;
                                    string pkgRatio = resNode.Attributes["pkgRatio"].Value;
                                    if (specList.Where(s => s.PackageRatio == pkgRatio).Count() <= 0)
                                    {
                                        //未处理该规格
                                        spec = new Models.PackageSpecific();
                                        spec.PackageRatio = pkgRatio;
                                        spec.ProductID = product.ID;
                                        spec.PackageSpec = product.PackSpec;
                                        if (pdal.SavePackageSpec(ref spec, out msg))
                                        {
                                            specList.Add(spec);
                                        }
                                        else
                                        {
                                            errorList.Append("规格保存错误!相信信息:" + product.ProductCode + "-" + product.ProductName + "-" + spec.PackageSpec + "-" + spec.PackageRatio+"\r\n");
                                            spec = null;
                                        }
                                    }
                                    else
                                    {
                                        spec = specList.Where(s => s.PackageRatio == pkgRatio).First();//取已存的规格
                                    }
                                    if (spec != null)
                                    {
                                        res.PackageSpec.ID = spec.ID;
                                        if (!pdal.SaveRes(res, out msg))
                                        {
                                            errorList.Append("资源码保存错误!详细信息:" + product.ProductCode + "-" + product.ProductName + "-" + res.LevelNo + "-" + res.ProductResCode + "\r\n");
                                        }
                                    }
                                    else
                                    {
                                        errorList.Append("资源码保存错误!详细信息:"+product.ProductCode+"-"+product.ProductName+"-"+res.LevelNo+"-"+res.ProductResCode+"\r\n");
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                //产品信息保存失败!
                                errorList.Append("产品保存失败!详细信息:" + product.ProductCode + "-" + product.ProductName+"\r\n");
                            }
                           
                        }
                        #endregion
                    }
                }
            }
            SysInfo.Message message = new SysInfo.Message();
            message.Obj = errorList.ToString();
            if (errorList.Length == 0)
            {
                message.Msg = "导入成功!";
            }
            else
            {
                message.Msg = "导入完成!以下内容未导入:";
            }
            message.Success = true;
            JsonResult jr = Json(message);
            jr.ContentType = "text/html";
            return jr;
        }


        /// <summary>
        /// 查询产品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Select(string ProductCode, string ProductName, string PermitNo,string page,string rows)
        {
            List<SysInfo.Param> paramList = new List<SysInfo.Param>();
            SysInfo.DatagridPage<Models.Product> rst=new SysInfo.DatagridPage<Models.Product>();
            paramList.Add(new SysInfo.Param("@ProductCode", string.IsNullOrEmpty(ProductCode)?string.Empty:ProductCode));
            paramList.Add(new SysInfo.Param("@ProductName",string.IsNullOrEmpty( ProductName)?string.Empty:ProductName));
            paramList.Add(new SysInfo.Param("@PermitNo", string.IsNullOrEmpty(PermitNo)?string.Empty:PermitNo));
            string msg;
            using (DAL.ProductDAL dal = new DAL.ProductDAL(SysInfo.SysSetting.DBCCN))
            {
                dal.Select(paramList, int.Parse(page), int.Parse(rows), out rst.rows, out rst.total,out msg);
            }
            JsonResult jr = Json(rst);
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 查询资源码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SelectProductRes(string ProductID)
        {
            List<Models.ProductRes> resList ;
            using (DAL.ProductDAL dal = new DAL.ProductDAL(SysInfo.SysSetting.DBCCN))
            {
                dal.SelectProductRes(ProductID, out resList);
            }
            JsonResult jr = Json(resList);
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
