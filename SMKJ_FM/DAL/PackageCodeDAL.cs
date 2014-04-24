using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Xml;

namespace DAL
{
    public class PackageCodeDAL:BaseDAL<Models.PackageCode>
    {
        public PackageCodeDAL(string DBConnection)
            : base(DBConnection) { }

        /// <summary>
        /// 保存监管码
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Save(Models.PackageCode obj, out string msg)
        {
            SysInfo.Param [] plist={
            new SysInfo.Param("@PackageCode",obj.PackCode),
            new SysInfo.Param("@PackSpecID",obj.PackSpecID),
            new SysInfo.Param("@LevelNo",obj.LevelNo,SqlDbType.SmallInt),
            new SysInfo.Param("@UpdateTime",obj.UpdateTime),
            new SysInfo.Param("@msg",string.Empty,ParameterDirection.Output)
                                   };
            SqlParameter [] ps;
            BuildParam(out ps,plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_SavePackageCode", ps, out i);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_SUCCESS;
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = SysInfo.SysMessageTxt.SYS_SAVE_FAILED;
                return false;
            }
        }

        public override bool Delete(string id, out string msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询码
        /// </summary>
        /// <param name="param"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="rst"></param>
        /// <param name="total"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool Select(List<SysInfo.Param> param, int page, int size, out List<Models.PackageCode> rst, out int total, out string msg)
        {
            rst = new List<Models.PackageCode>();
            msg = SysInfo.SysMessageTxt.SYS_SEARCH_FAILED;
            total = 0;
            SqlParameter[] ps;
            BuildParam(out ps, param.ToArray());
            try
            {
                DataSet ds = SqlEngine.ExecuteDataSet(size, page, CommandType.StoredProcedure, "PROC_ListPackCode", ps);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Models.PackageCode code = new Models.PackageCode();
                        code.PackCode = ConvertToString(row["PackCode"]);
                        code.LevelNo = ConvertToShort(row["LevelNo"]);
                        code.Status = (Models.CodeStatuFlag)ConvertToShort(row["Status"]);
                        code.ParentPackCode = ConvertToString(row["ParentPackCode"]);
                        code.TaskID = ConvertToString(row["TaskID"]);
                        code.PackFlag = (Models.CodePackEnum)ConvertToShort(row["PackFlag"]);
                        code.PrintFlag = (Models.CodePrintFlag)ConvertToShort(row["PrintFlag"]);
                        rst.Add(code);
                    }
                    total = ConvertToInt(ds.Tables[1].Rows[0]["total"]);
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }

        public override Models.PackageCode Get(string ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 上传监管码
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool UploadPackageCode(DataSet ds ,out int i)
        {
            string tableName;
           
            try
            {
                if (InitTempTable(out tableName))
                {
                    SqlEngine.SqlBulkCopy(ds.Tables[0], tableName, null);
                    Save(tableName, out i);
                    return true;
                }
                else
                {
                    throw new Exception("初始化临时表失败!");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                i = 0;
                return false;
            }
        }

        /// <summary>
        /// 保存监管码
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private bool Save(string tableName,out int count)
        {
            try
            {
                SqlEngine.RunProcedure("PROC_SavePackageCode", new SqlParameter[] { new SqlParameter("@tableName", tableName) }, out count);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                count = 0;
                return false;
            }
        }
        /// <summary>
        /// 初始化临时表
        /// </summary>
        /// <returns></returns>
        private bool InitTempTable(out string tableName)
        {
             tableName = "PackCode#Temp#" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            int i;
            try
            {
                SqlEngine.RunProcedure("PROC_InitTempTable", new SqlParameter[] { new SqlParameter("@tableName", tableName) }, out i);
                return true;
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 返工
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool UnDo(string TaskID)
        {
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_UnDoTask_Code", new SqlParameter[] { new SqlParameter("@TaskID", TaskID) }, out i);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 扫描监管码
        /// </summary>
        /// <returns></returns>
        public bool SaveScanCode(string[] Codes,Models.CodePackEnum PackFlag, ref long ScanNo,string PackSpecID,string TaskID,Guid WorkCenterID, out string msg,out int SuccessNum)
        {
            SuccessNum = 0;
            long sno = ScanNo;
            StringBuilder StrBuilder = new StringBuilder();
            ArrayList UpdateCodeStrs = new ArrayList();
            foreach (string code in Codes)
            {
                if (code != string.Empty)
                {
                    Models.PackageCode pcode;
                    #region 检测该码是否可用
                    StrBuilder.Clear();
                    StrBuilder.AppendFormat("Select Status,TaskID,PackFlag,PrintFlag,LockedByTask,PackSpecID from PackCode where PackCode='{0}' ", code);
                    using (SqlDataReader dr = SqlEngine.ExecuteReader(StrBuilder.ToString()))
                    {

                        #region 读取码信息
                        if (dr.Read())
                        {
                            pcode = new Models.PackageCode();
                            pcode.PackCode = code;
                            pcode.Status = (Models.CodeStatuFlag)ConvertToShort(dr["Status"]);
                            pcode.TaskID = ConvertToString(dr["TaskID"]);
                            pcode.PackFlag = (Models.CodePackEnum)ConvertToShort(dr["PackFlag"]);
                            pcode.PrintFlag = (Models.CodePrintFlag)ConvertToShort(dr["PrintFlag"]);
                            pcode.LockedByTask = ConvertToShort(dr["LockedByTask"]);
                            pcode.PackSpecID = ConvertToString(dr["PackSpecID"]);
                        }
                        else
                        {
                            pcode = null;
                        }
                        dr.Close();
                        #endregion
                    }

                    #region 码不可用,返回错误信息
                    if (pcode == null)
                    {
                        msg = "监管码【" + code + "】不存在！";
                        return false;
                    }

                    if (pcode.PackSpecID != PackSpecID)
                    {
                        msg = "监管码【" + pcode.PackCode + "】不属于该产品！";
                        return false;
                    }

                    if (pcode.PrintFlag == Models.CodePrintFlag.未打印)
                    {
                        msg = "监管码【" + pcode.PackCode + "】未打印！";
                        return false;
                    }

                    if (pcode.Status == Models.CodeStatuFlag.已使用)
                    {
                        msg = "监管码【" + pcode.PackCode + "】已使用！";
                        return false;
                    }
                    if (pcode.Status == Models.CodeStatuFlag.散装)
                    {
                        msg = "监管码【" + pcode.PackCode + "】是散装产品，请拼箱！";
                        return false;
                    }
                    if (pcode.LockedByTask == 1)
                    {
                        msg = "监管码【" + pcode.PackCode + "】已扫描！";
                        return false;
                    }
                    #endregion

                    SuccessNum++;
                    #endregion

                    #region 更改该码状态
                    StrBuilder.Clear();
                    if (PackFlag == Models.CodePackEnum.拼箱)
                    {
                        StrBuilder.AppendFormat("UPDATE PackCode set PackFlag=4, Remark={0},WorkCenterID='{1}' ,LockedByTask=1,ScanNo={2} where PackCode='{3}' ;"
                            , new object[] { TaskID, WorkCenterID.ToString(), ++sno, code }
                            );
                    }
                    else
                    {
                        StrBuilder.AppendFormat("UPDATE PackCode set PackFlag=1,  TaskID={0},WorkCenterID='{1}' ,LockedByTask=1,ScanNo={2} where PackCode='{3}' ;"
                            , new object[] { TaskID, WorkCenterID.ToString(), ++sno, code }
                            );
                    }

                    #endregion

                    //#region 更新工位信息
                    //StrBuilder.AppendFormat(" UPDATE WorkCenterData set CurrentNum=CurrentNum+1 where TaskID='{0}' and WorkCenterId='{1}';"
                    //    , new object[] { TaskID, WorkCenterID }
                    //    );
                    //#endregion

                    #region 更新任务信息
                    StrBuilder.AppendFormat("update Task set DoneAmount=DoneAmount+1 where ID='{0}';",
                        new object[] { TaskID }
                        );
                    #endregion
                }
            }
            try
            {
                UpdateCodeStrs.AddRange(StrBuilder.ToString().Split(';').ToList());
                bool ok = SqlEngine.ExecuteSqlTran(UpdateCodeStrs);
                msg = ok ? "扫描成功!" : "扫描失败！";
                ScanNo = sno;
                return ok;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 关联监管码
        /// </summary>
        /// <param name="ParentCode"></param>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool SaveRelation(string[] PackCodes,string ParentCode,string TaskID,Guid WorkCenterID,Models.CodePackEnum PackFlag)
        {
            StringBuilder StrBuilder = new StringBuilder();
            ArrayList Sqlstrlist = new ArrayList();
            
            foreach (string code in PackCodes)
            {
                #region 更新父码
                StrBuilder.AppendFormat("update PackCode set ParentPackCode='{0}',  PackFlag={1} , LockedByTask=0 where PackCode='{2}' ;"
                    , new object[] { ParentCode, (short)PackFlag ,code }
                    );
                #endregion
            }
            #region 更新工位信息
            StrBuilder.AppendFormat("update WorkCenterData set CurrentNum=CurentNum-"+PackCodes.Count().ToString()+",FinishedNum=FinishedNum+1,PackageNum=PackageNum+1,PackageFlag=0 where TaskID='{0}' and WorkCenterId='{1}';"
                ,new object[]{TaskID,WorkCenterID.ToString()}
                );
            #endregion

            try
            {
                Sqlstrlist.AddRange(StrBuilder.ToString().Split(';').ToList());
                return SqlEngine.ExecuteSqlTran(Sqlstrlist);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 散装操作
        /// </summary>
        /// <param name="PackCodes"></param>
        /// <returns></returns>
        public bool Sanzhuang(string[] PackCodes,Guid WorkCenterID)
        {
            StringBuilder StrBuilder = new StringBuilder();
            foreach (string code in PackCodes)
            {
                StrBuilder.AppendFormat(" update PackCode set PackFlag=3 ,LockedByTask=0 Status=2 where PackCode='{0}'; ",
                    new object[]{code}
                    );
            }
            //#region 更新工位信息
            //StrBuilder.AppendFormat(" update WorkCenterData set CurrentNum=0 where WorkCenterId='{0}';",
            //    new object[]{WorkCenterID.ToString()}
            //    );
            //#endregion
            try
            {
                ArrayList sqlStrs = new ArrayList();
                sqlStrs.AddRange(StrBuilder.ToString().Split(';'));
                return SqlEngine.ExecuteSqlTran(sqlStrs);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 检验散装码
        /// </summary>
        /// <returns></returns>
        public bool GetSanzhuang(string PackCode,string PackSpecID)
        {
            bool ok = false;
            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.AppendFormat("select PackCode from PackCode where Status=2 and PackFlag=3 and PackSpecID='{0}' and PackCode='{1}' ",
                new object[]{PackSpecID,PackCode}
                );
            try
            {
                using (SqlDataReader SqlDr = SqlEngine.ExecuteReader(StrBuilder.ToString()))
                {
                    if (SqlDr.Read())
                    {
                        ok=true;
                    }
                    else
                    {
                        ok=false;
                    }
                    SqlDr.Close();
                }
                return ok;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 读取指定任务工位上的码
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public List<string> GetWorkCenterPackCode(string TaskID, Guid WorkCenterID)
        {
            List<string> codes = new List<string>();
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("select PackCode from PackCode where TaskID='{0}' and WorkCenterID='{1}' and LockedByTask=1 order by ScanNo",
                    new object[]{TaskID,WorkCenterID.ToString()}
                );
            try
            {
                using (SqlDataReader Sqldr = SqlEngine.ExecuteReader(strBuilder.ToString()))
                {
                    while (Sqldr.Read())
                    {
                        codes.Add(ConvertToString(Sqldr["PackCode"]));
                    }
                    Sqldr.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
            return codes;
        }

        /// <summary>
        /// 读取指定任务ScanNo
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public long GetMaxScanNo(string TaskID,Guid WorkCenterID)
        {
            StringBuilder StrBuilder = new StringBuilder();
            StrBuilder.AppendFormat("select max(ScanNo)ScanNo from PackCode where TaskID='{0}' and WorkCenterID='{1}' ",
                new object []{TaskID,WorkCenterID.ToString()}
                );
            try
            {
                using (SqlDataReader sqldr = SqlEngine.ExecuteReader(StrBuilder.ToString()))
                {
                    long no;
                    if (sqldr.Read())
                    {
                        no = ConvertToLong(sqldr["ScanNo"]);

                    }
                    else
                    {
                        no = 0;
                    }
                    sqldr.Close();
                    return no;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 导出关联关系
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool ExportRelation(string TaskID,string RelationModel, string ServerPath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(RelationModel);//读取模板文件
            string SuccessFileName;
            try
            {
                Models.Task task;//主任务
                Models.Task CurTask;//当前任务
                using (DAL.TaskDAL dal = new TaskDAL(ConStr))
                {

                    task = dal.Get(TaskID);
                    CurTask = task;
                    SuccessFileName = "/Export/Relation/" + task.Product.ProductName + "_" + task.Product.SubNo + "_" + task.BatchNo + "_" + task.Product.PackSpec
                        + DateTime.Parse(task.EndDate).ToString("yyyyMMdd") + ".xml";
                    DataSet ds = SqlEngine.ExecuteDataSet(CommandType.StoredProcedure, "PROC_ExportRelation",
                        new SqlParameter[] { new SqlParameter("TaskID", TaskID) });

                    XmlNode relationEventNode = XmlDoc.SelectSingleNode("Document")
                         .SelectSingleNode("Events").SelectSingleNode("Event");
                    relationEventNode.RemoveAll();

                    #region 生成relation节点
                    XmlNode relationNode = XmlDoc.CreateElement("Relation");
                    relationNode.Attributes.Append(XmlDoc.CreateAttribute("productCode"));
                    relationNode.Attributes.Append(XmlDoc.CreateAttribute("subTypeNo"));
                    relationNode.Attributes.Append(XmlDoc.CreateAttribute("cascade"));
                    relationNode.Attributes.Append(XmlDoc.CreateAttribute("packageSpec"));
                    relationNode.Attributes.Append(XmlDoc.CreateAttribute("comment"));
                    relationNode.Attributes["productCode"].Value = task.Product.ProductCode;
                    relationNode.Attributes["subTypeNo"].Value = task.Product.SubNo;
                    relationNode.Attributes["cascade"].Value = task.PackageSpec.PackageRatio;
                    relationNode.Attributes["packageSpec"].Value = task.PackageSpec.PackageSpec;
                    relationNode.Attributes["comment"].Value = "";
                    relationEventNode.AppendChild(relationNode);
                    #endregion

                    string CurBatchNo=task.BatchNo;//当前批次号

                    #region 生成批次
                    XmlNode batchNode = XmlDoc.CreateElement("Batch");
                    batchNode.Attributes.Append(XmlDoc.CreateAttribute("batchNo"));
                    batchNode.Attributes.Append(XmlDoc.CreateAttribute("madeDate"));
                    batchNode.Attributes.Append(XmlDoc.CreateAttribute("validateDate"));
                    batchNode.Attributes.Append(XmlDoc.CreateAttribute("workshop"));
                    batchNode.Attributes.Append(XmlDoc.CreateAttribute("lineName"));
                    batchNode.Attributes.Append(XmlDoc.CreateAttribute("lineManager"));
                    batchNode.Attributes["batchNo"].Value = CurTask.BatchNo;
                    batchNode.Attributes["madeDate"].Value = CurTask.ProductDate;
                    batchNode.Attributes["validateDate"].Value = CurTask.InvalidDate;
                    batchNode.Attributes["workshop"].Value = CurTask.ProductLine.WorkShop;
                    batchNode.Attributes["lineName"].Value = CurTask.ProductLine.LineName;
                    batchNode.Attributes["lineManager"].Value = CurTask.ProductLine.ManageUser;
                    relationNode.AppendChild(batchNode);
                    #endregion

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        string PinxiangTaskId = ConvertToString(row["TaskID"]);//拼箱操作的TaskID
                        if (PinxiangTaskId != TaskID)
                        {
                            CurTask = dal.Get(PinxiangTaskId);
                        }
                        else
                        {
                            CurTask = task;
                        }

                        if (CurTask.BatchNo != CurBatchNo)
                        {
                            #region 生成新批次
                            batchNode = XmlDoc.CreateElement("Batch");
                            batchNode.Attributes.Append(XmlDoc.CreateAttribute("batchNo"));
                            batchNode.Attributes.Append(XmlDoc.CreateAttribute("madeDate"));
                            batchNode.Attributes.Append(XmlDoc.CreateAttribute("validateDate"));
                            batchNode.Attributes.Append(XmlDoc.CreateAttribute("workshop"));
                            batchNode.Attributes.Append(XmlDoc.CreateAttribute("lineName"));
                            batchNode.Attributes.Append(XmlDoc.CreateAttribute("lineManager"));
                            batchNode.Attributes["batchNo"].Value = CurTask.BatchNo;
                            batchNode.Attributes["madeDate"].Value = CurTask.ProductDate;
                            batchNode.Attributes["validateDate"].Value = CurTask.InvalidDate;
                            batchNode.Attributes["workshop"].Value = CurTask.ProductLine.WorkShop;
                            batchNode.Attributes["lineName"].Value = CurTask.ProductLine.LineName;
                            batchNode.Attributes["lineManager"].Value = CurTask.ProductLine.ManageUser;
                            relationNode.AppendChild(batchNode);
                            #endregion
                        }


                        #region 插入码
                        XmlNode code = XmlDoc.CreateElement("Code");
                        code.Attributes.Append(XmlDoc.CreateAttribute("curCode"));
                        code.Attributes.Append(XmlDoc.CreateAttribute("packLayer"));
                        code.Attributes.Append(XmlDoc.CreateAttribute("parentCode"));
                        code.Attributes.Append(XmlDoc.CreateAttribute("flag"));
                        code.Attributes["curCode"].Value = ConvertToString(row["PackCode"]);
                        code.Attributes["packLayer"].Value = ConvertToString(row["LevelNo"]);
                        code.Attributes["parentCode"].Value = ConvertToString(row["ParentPackCode"]);
                        code.Attributes["flag"].Value = "0";
                        batchNode.InsertAfter(code, batchNode.LastChild);
                        #endregion
                    }
                }
                XmlDoc.Save(ServerPath +SuccessFileName);

                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                SuccessFileName = null;
                return false;
            }

        }

        /// <summary>
        /// 码替换
        /// </summary>
        /// <param name="SCode"></param>
        /// <param name="DCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ReplaceCode(string SCode, string DCode, out string msg)
        {
            SysInfo.Param[] plist = { 
                                    new SysInfo.Param("@SCode",SCode),
                                    new SysInfo.Param("@DCode",DCode),
                                    new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter[] ps;
            BuildParam(out ps, plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_ReplaceCode", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return i > 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = "系统异常！操作失败！";
                return false;
            }
        }


        /// <summary>
        /// 取消关联
        /// </summary>
        /// <param name="Scode"></param>
        /// <param name="DCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UnRelation(string SCode, string DCode, out string msg)
        {
            SysInfo.Param[] plist = { 
                                    new SysInfo.Param("@SCode",SCode),
                                    new SysInfo.Param("@DCode",DCode),
                                    new SysInfo.Param("@msg",string.Empty,SqlDbType.NVarChar,100,ParameterDirection.Output)
                                    };
            SqlParameter[] ps;
            BuildParam(out ps, plist);
            try
            {
                int i;
                SqlEngine.RunProcedure("PROC_UnRelation", ps, out i);
                msg = ConvertToString(ps.Last().Value);
                return i > 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                msg = "系统异常！";
                return false;
            }
        }
    }
}