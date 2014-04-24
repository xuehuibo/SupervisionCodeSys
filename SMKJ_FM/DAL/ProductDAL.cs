using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Models;
using System.Text;

namespace DAL
{
    public class ProductDAL:BaseDAL<Models.Product>
    {
        public ProductDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存产品
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Save(Models.Product obj, out string msg)
        {
            SqlParameter[] ps;
            int i;
            BuildParam(out ps, new SysInfo.Param[] {
                new SysInfo.Param("@ID",obj.ID),
                new SysInfo.Param("@ProductCode",obj.ProductCode),
                new SysInfo.Param("@ProductName",obj.ProductName),
                new SysInfo.Param("@SubNo",obj.SubNo),
                new SysInfo.Param("@SubName",obj.SubName),
                new SysInfo.Param("@Status",(short)(obj.Status),System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@ProductType",obj.ProductType),
                new SysInfo.Param("@Spec",obj.Spec),
                new SysInfo.Param("@PermitNo",obj.PermitNo),
                new SysInfo.Param("@PackSpec",obj.PackSpec),
                new SysInfo.Param("@CodeLen",obj.CodeLen,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@DetailTypeCode",obj.Detail_Type.DetailTypeCode),
                new SysInfo.Param("@DocID",obj.DocID,System.Data.SqlDbType.Int),
                new SysInfo.Param("@ValidTime",obj.ValidTime,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@ValidTimeUnit",obj.ValidTimeUnit,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@PackUnit",obj.PackUnit),
                new SysInfo.Param("@CreateUserID",obj.CreateUserID,System.Data.SqlDbType.Int),
                new SysInfo.Param("@Remark",obj.Remark),
                new SysInfo.Param("@msg",string.Empty,System.Data.ParameterDirection.Output)
            });
            try
            {
                SqlEngine.RunProcedure("PROC_SaveProduct", ps, out i);
                msg = ConvertToString(ps.Last().Value);
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
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                return false;
            }
        }

        public  bool Save(Models.Product obj, out string msg, out string productID)
        {
            SqlParameter[] ps;
            int i;
            BuildParam(out ps, new SysInfo.Param[] {
                new SysInfo.Param("@ID",string.IsNullOrEmpty(obj.ID)?string.Empty:obj.ID,System.Data.SqlDbType.NVarChar,60,System.Data.ParameterDirection.InputOutput),
                new SysInfo.Param("@ProductCode",obj.ProductCode),
                new SysInfo.Param("@ProductName",obj.ProductName),
                new SysInfo.Param("@SubNo",string.IsNullOrEmpty(obj.SubNo)?string.Empty:obj.SubNo),
                new SysInfo.Param("@SubName",string.IsNullOrEmpty(obj.SubName)?string.Empty:obj.SubName),
                new SysInfo.Param("@Status",(short)(obj.Status),System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@ProductType",obj.ProductType),
                new SysInfo.Param("@Spec",obj.Spec),
                new SysInfo.Param("@PermitNo",obj.PermitNo),
                new SysInfo.Param("@PackSpec",obj.PackSpec),
                new SysInfo.Param("@GetCodeType",(short)(obj.Product_Category.GetCodeType),System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@ProductCategoryID",obj.Product_Category.ID),
                new SysInfo.Param("@CodeLen",obj.CodeLen,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@DetailTypeID",obj.Detail_Type.ID),
                new SysInfo.Param("@DocID",obj.DocID,System.Data.SqlDbType.Int),
                new SysInfo.Param("@ValidTime",obj.ValidTime,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@ValidTimeUnit",obj.ValidTimeUnit,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@PackUnit",obj.PackUnit),
                new SysInfo.Param("@CreateUserID",obj.CreateUserID,System.Data.SqlDbType.Int),
                new SysInfo.Param("@Remark",string.IsNullOrEmpty(obj.Remark)?string.Empty:obj.Remark),
                new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100, System.Data.ParameterDirection.Output)
            });
            try
            {
                SqlEngine.RunProcedure("PROC_SaveProduct", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                productID = ConvertToString(ps.First().Value);
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
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                productID = string.Empty;
                return false;
            }
        }

        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.Product> rst, out int total, out string msg)
        {
            rst = new List<Models.Product>();
            SqlParameter[] ps;
            BuildParam(out ps,param.ToArray());
            total = 0;
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size,page,CommandType.StoredProcedure, "PROC_listProduct", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.Product product = new Models.Product();
                        product.ID = ConvertToString(row["ID"]);
                        product.ProductCode = ConvertToString(row["ProductCode"]);
                        product.ProductName = ConvertToString(row["ProductName"]);
                        product.SubNo = ConvertToString(row["SubNo"]);
                        product.SubName = ConvertToString(row["SubName"]);
                        product.Status = (Models.StatusEnum)ConvertToShort(row["Status"]);
                        product.ProductType = ConvertToString(row["ProductType"]);
                        product.Spec = ConvertToString(row["Spec"]);
                        product.PermitNo = ConvertToString(row["PermitNo"]);
                        product.PackSpec = ConvertToString(row["PackSpec"]);
                        product.Product_Category =GetProductCategory(ConvertToString(row["ProductCategoryID"]));
                        product.CodeLen = ConvertToShort(row["CodeLen"]);
                        product.Detail_Type =GetDetailTypeByID( ConvertToString(row["DetailTypeID"]));
                        product.DocID = ConvertToInt(row["DocID"]);
                        product.ValidTime = ConvertToShort(row["ValidTime"]);
                        product.ValidTimeUnit =(Models.ValidTimeUnitEnum )ConvertToShort(row["ValidTimeUnit"]);
                        product.PackUnit = ConvertToString(row["PackUnit"]);
                        product.CreateUserID = ConvertToString(row["CreateUserID"]);
                        product.CreateDate = ConvertToDatetime(row["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        product.CreateUserName = ConvertToString(row["UserName"]);
                        rst.Add(product);
                    }
                    total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
                    msg = SysInfo.SysMessageTxt.SYS_SEARCH_SUCCESS;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 保存码资源
        /// </summary>
        /// <param name="res"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveRes(Models.ProductRes res,out string msg)
        {
            SqlParameter[] ps;
            BuildParam(out ps, new SysInfo.Param[] { 
                new SysInfo.Param("@ID",string.IsNullOrEmpty(res.ID)?string.Empty:res.ID),
                new SysInfo.Param("@LevelNo",res.LevelNo,System.Data.SqlDbType.SmallInt),
                new SysInfo.Param("@CodeVersion",res.CodeVersion),
                new SysInfo.Param("@ProdCode",res.ProductResCode),
                new SysInfo.Param("@PackageSpecID",res.PackageSpec.ID),
                new SysInfo.Param("@ProductID",res.ProductID),
                new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,System.Data.ParameterDirection.Output)
            });

            try
            {
                int i = 0;
                SqlEngine.RunProcedure("PROC_SaveResourceCode", ps, out i);
                msg = ConvertToString(ps.Last().Value);
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
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                return false;
            }
        }

        /// <summary>
        /// 获取指定产品的资源码
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public bool SelectProductRes(string ProductID,out List<Models.ProductRes> resList)
        {
            resList = new List<ProductRes>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListProductRes", new SqlParameter[] {
                    new SqlParameter("@ProductID",ProductID)
                });

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.ProductRes res = new ProductRes();
                        res.PackageSpec = new PackageSpecific();
                        res.LevelNo = ConvertToShort(row["LevelNo"]);
                        res.PackageSpec.PackageSpec = ConvertToString(row["PackageSpec"]);
                        res.ProductID = ConvertToString(row["ProductID"]);
                        res.CodeVersion = ConvertToString(row["CodeVersion"]);
                        res.ID = ConvertToString(row["ID"]);
                        res.ProductResCode = ConvertToString(row["ProdCode"]);
                        res.PackageSpec.ID = ConvertToString(row["PackageSpecID"]);
                        res.PackageSpec.PackageRatio = ConvertToString(row["PackageRatio"]);
                        res.PackageSpec.ProductID = res.ProductID;
                        resList.Add(res);
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 保存包装规格
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="msg"></param>
        /// <param name="specID"></param>
        /// <returns></returns>
        public bool SavePackageSpec(ref Models.PackageSpecific spec,out string msg)
        {
            SqlParameter[] ps;
            BuildParam(out ps, new SysInfo.Param[] {
                new SysInfo.Param("@ID",string.IsNullOrEmpty(spec.ID)?string.Empty:spec.ID,System.Data.SqlDbType.NVarChar,60,System.Data.ParameterDirection.InputOutput),
                new SysInfo.Param("@PackageRatio",spec.PackageRatio),
                new SysInfo.Param("@PackageSpec",spec.PackageSpec),
                new SysInfo.Param("@ProductID",spec.ProductID),
                new SysInfo.Param("@Weight",spec.Weight,System.Data.SqlDbType.Decimal),
                new SysInfo.Param("@Remark",string.IsNullOrEmpty(spec.Remark)?string.Empty:spec.Remark),
                new SysInfo.Param("@msg",string.Empty,System.Data.SqlDbType.NVarChar,100,System.Data.ParameterDirection.Output)
            });

            try
            {
                int i = 0;
                SqlEngine.RunProcedure("PROC_SavePackageSpecific", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                spec.ID = ConvertToString(ps.First().Value);
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
                spec.ID = string.Empty;
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取类别
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public Models.ProductCategory GetProductCategory(string CategoryID)
        {
            Models.ProductCategory category = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetProductCategory", new SqlParameter[]{
                    new SqlParameter("@ID",CategoryID)
                });

                if (ds.Tables.Count > 0)
                {
                    category = new Models.ProductCategory();
                    category.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    category.CategoryCode = ConvertToString(ds.Tables[0].Rows[0]["CategoryCode"]);
                    category.CategoryName = ConvertToString(ds.Tables[0].Rows[0]["CategoryName"]);
                    category.GetCodeType = (GetCodeTypeEnum)ConvertToShort(ds.Tables[0].Rows[0]["GetCodeType"]);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                category = null;
            }
            return category;
        }

        /// <summary>
        /// 获取明细类
        /// </summary>
        /// <param name="DetailTypeCode"></param>
        /// <returns></returns>
        public Models.DetailType GetDetailTypeByCode(string DetailTypeCode)
        {
            Models.DetailType detailType=null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetDetailTypeByCode",
                    new SqlParameter[] {new SqlParameter("@DetailTypeCode",DetailTypeCode )});

                if(ds.Tables.Count>0)
                {
                    detailType=new Models.DetailType();
                    detailType.ID=ConvertToString( ds.Tables[0].Rows[0]["ID"]);
                    detailType.DetailTypeCode=ConvertToString(ds.Tables[0].Rows[0]["DetailTypeCode"]);
                    detailType.DetailTypeName=ConvertToString(ds.Tables[0].Rows[0]["DetailTypeName"]);
                    detailType.CategoryID=ConvertToString(ds.Tables[0].Rows[0]["CategoryID"]);
                }
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
                detailType=null;
            }
            return detailType;
        }


        public Models.DetailType GetDetailTypeByID(string DetailTypeID)
        {
            Models.DetailType detailType = null;
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetDetailTypeByID",
                    new SqlParameter[] { new SqlParameter("@DetailTypeID", DetailTypeID) });

                if (ds.Tables.Count > 0)
                {
                    detailType = new Models.DetailType();
                    detailType.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    detailType.DetailTypeCode = ConvertToString(ds.Tables[0].Rows[0]["DetailTypeCode"]);
                    detailType.DetailTypeName = ConvertToString(ds.Tables[0].Rows[0]["DetailTypeName"]);
                    detailType.CategoryID = ConvertToString(ds.Tables[0].Rows[0]["CategoryID"]);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                detailType = null;
            }
            return detailType;
        }

        /// <summary>
        /// 取全部类别
        /// </summary>
        /// <returns></returns>
        public List<Models.ProductCategory> ListProductCategory()
        {
            List<Models.ProductCategory> cateGoryList = new List<Models.ProductCategory>();
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ListProductCategory", new SqlParameter[] { });
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.ProductCategory category = new Models.ProductCategory();
                        category.DetailTypes = new List<Models.DetailType>();
                        category.ID = ConvertToString(row["ID"]);
                        category.CategoryCode = ConvertToString(row["CategoryCode"]);
                        category.CategoryName = ConvertToString(row["CategoryName"]);
                        category.GetCodeType = (Models.GetCodeTypeEnum)ConvertToShort(row["GetCodeType"]);
                        cateGoryList.Add(category);
                    }
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        Models.DetailType detailType = new Models.DetailType();
                        detailType.ID = ConvertToString(row["ID"]);
                        detailType.DetailTypeCode = ConvertToString(row["DetailTypeCode"]);
                        detailType.DetailTypeName = ConvertToString(row["DetailTypeName"]);
                        detailType.CategoryID = ConvertToString(row["CategoryID"]);
                        cateGoryList.Where(c => c.ID == detailType.CategoryID).First().DetailTypes.Add(detailType);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return cateGoryList;
        }

        public override Product Get(string ID)
        {
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_GetProduct", new SqlParameter[] { 
                    new SqlParameter("@ID",ID)
                });
                if (ds.Tables.Count > 0)
                {
                    Models.Product product = new Product();
                    product.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    product.ProductCode = ConvertToString(ds.Tables[0].Rows[0]["ProductCode"]);
                    product.ProductName = ConvertToString(ds.Tables[0].Rows[0]["ProductName"]);
                    product.SubNo = ConvertToString(ds.Tables[0].Rows[0]["SubNo"]);
                    product.SubName = ConvertToString(ds.Tables[0].Rows[0]["SubName"]);
                    product.Status = (Models.StatusEnum)ConvertToShort(ds.Tables[0].Rows[0]["Status"]);
                    product.ProductType = ConvertToString(ds.Tables[0].Rows[0]["ProductType"]);
                    product.Spec = ConvertToString(ds.Tables[0].Rows[0]["Spec"]);
                    product.PermitNo = ConvertToString(ds.Tables[0].Rows[0]["PermitNo"]);
                    product.PackSpec = ConvertToString(ds.Tables[0].Rows[0]["PackSpec"]);
                    product.PackUnit = ConvertToString(ds.Tables[0].Rows[0]["PackUnit"]);
                    product.Product_Category = GetProductCategory(ConvertToString(ds.Tables[0].Rows[0]["ProductCategoryID"]));
                    product.CodeLen = ConvertToShort(ds.Tables[0].Rows[0]["CodeLen"]);
                    product.Detail_Type = GetDetailTypeByID(ConvertToString(ds.Tables[0].Rows[0]["DetailTypeID"]));
                    product.DocID = ConvertToInt(ds.Tables[0].Rows[0]["DocID"]);
                    product.ValidTime = ConvertToShort(ds.Tables[0].Rows[0]["ValidTime"]);
                    product.ValidTimeUnit = (Models.ValidTimeUnitEnum)ConvertToShort(ds.Tables[0].Rows[0]["ValidTimeUnit"]);
                    product.CreateUserID = ConvertToString(ds.Tables[0].Rows[0]["CreateUserID"]);
                    product.CreateDate = ConvertToDatetime(ds.Tables[0].Rows[0]["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    product.Remark = ConvertToString(ds.Tables[0].Rows[0]["Remark"]);
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取产品资源码
        /// </summary>
        /// <param name="ResCode"></param>
        /// <returns></returns>
        public Models.ProductRes GetProductRes(string ResCode)
        {
            Models.ProductRes res = new ProductRes();
            StringBuilder SB = new StringBuilder();
            SB.AppendFormat("select  * from ResourceCode where ProdCode='{0}'",ResCode);
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.Text, SB.ToString(), new SqlParameter[] { });
                if (ds.Tables.Count > 0)
                {
                    res.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    res.LevelNo = ConvertToShort(ds.Tables[0].Rows[0]["LevelNo"]);
                    res.CodeVersion = ConvertToString(ds.Tables[0].Rows[0]["CodeVersion"]);
                    res.ProductResCode = ResCode;
                    res.PackageSpec = GetPackageSpecific(ConvertToString(ds.Tables[0].Rows[0]["PackageSpecID"]));
                    res.ProductID = ConvertToString(ds.Tables[0].Rows[0]["ProductID"]);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 获取指定包装规格
        /// </summary>
        /// <param name="PackID"></param>
        /// <returns></returns>
        public Models.PackageSpecific GetPackageSpecific(string PackID)
        {
            Models.PackageSpecific pack = new PackageSpecific();
            StringBuilder SB = new StringBuilder();
            SB.AppendFormat("select  *  from PackageSpecific where ID='{0}'",PackID);
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(CommandType.Text, SB.ToString(), new SqlParameter[] { });
                if (ds.Tables.Count > 0)
                {
                    pack.ID = ConvertToString(ds.Tables[0].Rows[0]["ID"]);
                    pack.PackageRatio = ConvertToString(ds.Tables[0].Rows[0]["PackageRatio"]);
                    pack.PackageSpec = ConvertToString(ds.Tables[0].Rows[0]["PackageSpec"]);
                    pack.ProductID = ConvertToString(ds.Tables[0].Rows[0]["ProductID"]);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return pack;
        }
    }
}