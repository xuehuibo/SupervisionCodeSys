using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysInfo;
using System.Collections;
using System.IO.Ports;

namespace TaskCtl
{
    public partial class TaskShowForm : Form
    {
        List<Models.WorkCenter> WorkCenterList;//工位列表

        Models.Task task
        {
            get;
            set;
        }//任务信息

        #region 一工位

        Queue<string> QueueLV1 = new Queue<string>();//一级码队列

        /// <summary>
        /// 一级包装数量
        /// </summary>
        int LV1Num
        {
            get;
            set;
        }

        /// <summary>
        /// 工位包装状态
        /// </summary>
        public Models.CodePackEnum PackFlagLV1 = Models.CodePackEnum.整箱;

        /// <summary>
        /// 扫描编号LV1
        /// </summary>
        public long ScanNoLV1;
        #endregion

        #region 二工位

        Queue<string> QueueLV2 = new Queue<string>();//二级码队列

        /// <summary>
        /// 二级包装数量
        /// </summary>
        int LV2Num
        {
            get;
            set;
        }

        /// <summary>
        /// 工位包装状态
        /// </summary>
        public Models.CodePackEnum PackFlagLV2 = Models.CodePackEnum.整箱;

        /// <summary>
        /// 扫描编号LV2
        /// </summary>
        public long ScanNoLV2;
        #endregion

        #region 三工位

        Queue<string> QueueLV3 = new Queue<string>();//三级码队列

        /// <summary>
        /// 工位包装状态
        /// </summary>
        public Models.CodePackEnum PackFlagLV3 = Models.CodePackEnum.整箱;

        /// <summary>
        /// 扫描编号LV3
        /// </summary>
        public long ScanNoLV3;
        #endregion

        public TaskShowForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定任务
        /// </summary>
        public bool BindingTask(Models.Task task)
        {
            QueueLV1.Clear();
            QueueLV2.Clear();
            QueueLV3.Clear();
            bool ok=false;
            this.Text = task.TaskCode;
            #region 读取工位信息
                this.task = task;
                WorkCenterList = new List<Models.WorkCenter>();
                WorkCenterPanel.Controls.Clear();
                using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(Config.DBCCN))
                {
                    ok=dal.GetProductLineWorkCenter(task.ProductLine.ID.ToString(), out WorkCenterList);
                }
                if (ok && WorkCenterList != null)
                {
                    foreach (Models.WorkCenter workCenter in WorkCenterList)
                    {
                        WorkCenterCtl.WorkCenterInfo w = new WorkCenterCtl.WorkCenterInfo();
                        w.BindingWorkCenter(workCenter, task.ID);
                        w.scanDelegate = new ScanCodeDelegate(ScanCode);
                        w.lingxiangDelegate = new LingxiangDelegate(Lingxiang);
                        w.pinxiangDelegate = new PinxiangDelegate(Pinxiang);
                        w.sanzhuangDelegate = new SanzhuangDelegate(Sanzhuang);
                        WorkCenterPanel.Controls.Add(w);
                    }

                    #region 读取当前任务信息
                    using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(Config.DBCCN))
                    {
                        int lvno = WorkCenterList.Count;
                        if (lvno > 0)
                        {
                            ScanNoLV1 = dal.GetMaxScanNo(task.ID, WorkCenterList[0].ID);
                            List<string> L1 = dal.GetWorkCenterPackCode(task.ID, WorkCenterList[0].ID);
                            if (L1 != null)
                            {
                                foreach (string code in L1)
                                {
                                    QueueLV1.Enqueue(code);
                                }
                            }
                        }
                        if (lvno > 1)
                        {
                            ScanNoLV2 = dal.GetMaxScanNo(task.ID, WorkCenterList[1].ID);
                            List<string> L2 = dal.GetWorkCenterPackCode(task.ID, WorkCenterList[1].ID);
                            if (L2 != null)
                            {
                                foreach (string code in L2)
                                {
                                    QueueLV2.Enqueue(code);
                                }
                            }
                        }
                        if (lvno > 2)
                        {
                            ScanNoLV3 = dal.GetMaxScanNo(task.ID, WorkCenterList[2].ID);
                            List<string> L3 = dal.GetWorkCenterPackCode(task.ID, WorkCenterList[2].ID);
                            if (L3 != null)
                            {
                                foreach (string code in L3)
                                {
                                    QueueLV3.Enqueue(code);
                                }
                            }
                        }
                    }
                    #endregion

                    #region 显示任务信息
                    TaskNameLbl.Text = task.TaskCode;
                    BatchNoLbl.Text = task.BatchNo;
                    ProductNameLbl.Text = task.Product.ProductName;
                    TaskAmountLbl.Text = task.TaskAmount.ToString();
                    DoneAmountLbl.Text = task.DoneAmount.ToString();
                    #endregion 
                }
            #endregion



            return ok;
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        /// <returns></returns>
        public bool StartTask(out string msg)
        {
            string[] cscade = task.PackageSpec.PackageRatio.Split(':');
            int cscadeNum = cscade.Count();
            switch (cscadeNum)
            {
                case 1:
                    LV1Num = 1;
                    LV2Num = 0;
                    break;
                case 2:
                    LV1Num = int.Parse(cscade[1])/int.Parse(cscade[0]);
                    LV2Num = 1;
                    break;
                case 3:
                    LV1Num = int.Parse(cscade[1])/int.Parse(cscade[0]);
                    LV2Num = int.Parse(cscade[2])/int.Parse(cscade[1]);
                    break;
            }
            using (DAL.TaskDAL dal = new DAL.TaskDAL(Config.DBCCN))
            {
                return dal.StartTask(task.ID, out msg);
            }
        }

        public void StopBtn_Click(object sender, EventArgs e)
        {
            int LevelNo = task.PackageSpec.PackageRatio.Split(':').Count();
            bool ok = false;
            switch (LevelNo)
            {
                case 1:
                    ok = true;
                    break;
                case 2:
                    ok = QueueLV1.Count <=0;
                    break;
                case 3:
                    ok = QueueLV1.Count <= 0 && QueueLV2.Count <= 0;
                    break;
            }

            if (!ok)
            {
                MessageBox.Show("有未完成的包装，不允许结束任务！");
                return;//取消结束
            }
            else
            {
                if (MessageBox.Show("有确定要结束任务？", "结束任务", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;//取消结束
                }
            }

            task.Status = Models.TaskStatus.已结束;
            using (DAL.TaskDAL dal = new DAL.TaskDAL(Config.DBCCN))
            {
                string msg;
                dal.StopTask(task.ID, out msg);
            }
            StopEquipment();
            this.Close();
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
                if (MessageBox.Show("确定要暂停该任务？", "暂停任务", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;//取消暂停
                }
            task.Status = Models.TaskStatus.已暂停;
            StopEquipment();
            this.Close();
        }
        /// <summary>
        /// 处理扫描到的码
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="Codes"></param>
        private bool ScanCode(Models.WorkCenter WorkCenter, string[] Codes)
        {
            int successNum=0;
            switch (WorkCenter.LevelNo)
            {
                case 1:
                    //一工位
                    WorkCenterCtl.WorkCenterInfo WCinfo1 = WorkCenterPanel.Controls[0] as WorkCenterCtl.WorkCenterInfo;
                    if (Codes.Contains("NO READ"))
                    {
                        WCinfo1.ShowMessage("扫描错误");
                        if (WCinfo1.WorkcenterViewer != null)
                        {
                            //工位显示
                            WCinfo1.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "扫描错误");
                        }
                        return false;
                    }
                    else
                    {
                        bool ScanOK=true;

                        #region 数据库中处理码
                        using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.Config.DBCCN))
                        {
                            string msg="";
                            if (PackFlagLV1 == Models.CodePackEnum.拼箱)
                            {
                                foreach (string code in Codes)
                                {
                                    bool ok = dal.GetSanzhuang(code, task.PackageSpec.ID);
                                    if (!ok)
                                    {
                                        ScanOK = ok;
                                        WCinfo1.ShowMessage("【" + code + "】不是散装产品！");
                                    }
                                }
                            }
                            else
                            {
                                ScanOK = dal.SaveScanCode(Codes,PackFlagLV1, ref ScanNoLV1, task.PackageSpec.ID, task.ID, WorkCenterList[0].ID, out msg,out successNum);
                            }
                            if (!ScanOK)
                            {
                                WCinfo1.ShowMessage(msg);
                                if (WCinfo1.WorkcenterViewer != null)
                                {
                                    WCinfo1.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, msg);
                                }
                            }
                        }
                        #endregion

                        #region 码检测成功，加入成功队列
                        if (ScanOK)
                        {
                            foreach (string code in Codes)
                            {
                                if (!string.IsNullOrEmpty(code))
                                {
                                    QueueLV1.Enqueue(code);
                                }
                            }
                            //DoneAmountLbl.Text =( int.Parse(DoneAmountLbl.Text) + Codes.Count()).ToString();
                            if (WCinfo1.WorkcenterViewer != null)
                            {
                                WCinfo1.WorkViewer_Show(WorkCenterViewer.SpeakMsg.成功, "成功");
                            }
                        }

                        #endregion

                        #region 保存工位信息
                        if (ScanOK)
                        {
                            WorkCenter.Data.CurrentNum += successNum;
                            WorkCenter.Data.FinishedNum = WorkCenter.Data.CurrentNum / LV1Num;
                        }

                        using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(Config.DBCCN))
                        {
                            dal.SaveWorkCenterData(WorkCenter.Data);
                        }

                        WCinfo1.BindingWorkCenter();
                        #endregion

                        return ScanOK;
                    }
                case 2:
                    //二工位
                    WorkCenterCtl.WorkCenterInfo WCinfo2 = WorkCenterPanel.Controls[1] as WorkCenterCtl.WorkCenterInfo;
                    if (Codes.Contains("NO READ"))
                    {
                        WCinfo2.ShowMessage("扫描错误！");
                        if (WCinfo2.WorkcenterViewer != null)
                        {
                            WCinfo2.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "扫描错误");
                        }
                        return false;
                    }
                    else
                    {
                        bool ScanOK=true;
                        bool RelationOK;
                        if (PackFlagLV2 == Models.CodePackEnum.零箱 && QueueLV1.Count > 0)
                        {
                            //零箱操作
                        }
                        else
                        {
                            if (QueueLV1.Count < LV1Num)
                            {
                                WCinfo2.ShowMessage("未满足包装数量！");
                                if (WCinfo2.WorkcenterViewer != null)
                                {
                                    WCinfo2.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "未满足包装数量");
                                }
                                return false;
                            }
                        }
                        using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(Config.DBCCN))
                        {
                            #region 数据库中处理码
                            string msg="";
                            if (PackFlagLV2 == Models.CodePackEnum.拼箱)
                            {
                                foreach (string code in Codes)
                                {
                                    bool ok = dal.GetSanzhuang(code, task.PackageSpec.ID);
                                    if (!ok)
                                    {
                                        ScanOK = ok;
                                        WCinfo2.ShowMessage("【" + code + "】不是散装产品！");
                                    }
                                }
                            }
                            else
                            {
                                ScanOK = dal.SaveScanCode(Codes, PackFlagLV2, ref ScanNoLV2, task.PackageSpec.ID, task.ID, WorkCenterList[1].ID, out msg,out successNum);
                            }
                            if (!ScanOK)
                            {
                                WCinfo2.ShowMessage(msg);
                                if (WCinfo2.WorkcenterViewer != null)
                                {
                                    WCinfo2.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, msg);
                                }
                                return false; 
                            }
                            #endregion

                            #region 码检测成功，进行关联
                            string[] curCodes = new string[LV1Num];
                            for (int i = 0; i < LV1Num; i++)
                            {
                                curCodes[i] = QueueLV1.Dequeue();
                            }
                            RelationOK= dal.SaveRelation(curCodes, Codes[0], task.ID, WorkCenterList[1].ID,PackFlagLV2);
                            #endregion

                            #region 关联成功，加入二级队列,否则报错
                            if (RelationOK)
                            {
                                QueueLV2.Enqueue(Codes[0]);
                                if (WCinfo2.WorkcenterViewer != null)
                                {
                                    WCinfo2.WorkViewer_Show(WorkCenterViewer.SpeakMsg.成功, "成功");
                                }
                            }
                            else
                            {
                                WCinfo2.ShowMessage("关联错误！");
                                if (WCinfo2.WorkcenterViewer != null)
                                {
                                    WCinfo2.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "关联错误");
                                }
                            }

                            #endregion
                        }
                        #region 保存工位信息
                        if (RelationOK)
                        {
                            WorkCenter.Data.CurrentNum += successNum;
                            WorkCenter.Data.FinishedNum = WorkCenter.Data.CurrentNum / LV2Num;
                            WorkCenterList[0].Data.FinishedNum -= 1;
                            WorkCenterList[0].Data.PackageNum += 1;
                        }
                        using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(Config.DBCCN))
                        {
                            dal.SaveWorkCenterData(WorkCenter.Data);
                            dal.SaveWorkCenterData(WorkCenterList[0].Data);
                        }
                        WCinfo2.BindingWorkCenter();
                        #endregion

                        return RelationOK;
                    }
                case 3:
                    //三工位
                    WorkCenterCtl.WorkCenterInfo WCinfo3 = WorkCenterPanel.Controls[2] as WorkCenterCtl.WorkCenterInfo;
                    if (Codes.Contains("NO READ"))
                    {
                        WCinfo3.ShowMessage("扫描错误！");
                        if (WCinfo3.WorkcenterViewer != null)
                        {
                            WCinfo3.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "扫描错误");
                        }
                        return false;
                    }
                    else
                    {
                        bool ScanOK;
                        bool RelationOK;
                        if (QueueLV2.Count < LV2Num)
                        {
                            WCinfo3.ShowMessage("未满足包装数量！");
                            if (WCinfo3.WorkcenterViewer != null)
                            {
                                WCinfo3.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "未满足包装数量");
                            }
                            return false;
                        }
                        using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(Config.DBCCN))
                        {

                            #region 数据库中处理码
                            string msg="";
                            ScanOK = dal.SaveScanCode(Codes, PackFlagLV3, ref ScanNoLV3, task.PackageSpec.ID, task.ID, WorkCenterList[2].ID, out msg,out successNum);
                            if (!ScanOK)
                            {
                                WCinfo3.ShowMessage(msg);
                                if (WCinfo3.WorkcenterViewer != null)
                                {
                                    WCinfo3.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, msg);
                                }
                                return false;
                            }
                            #endregion

                            #region 码检测成功，进行关联
                            string[] curCodes = new string[LV2Num];
                            for (int i = 0; i < LV2Num; i++)
                            {
                                curCodes[i] = QueueLV2.Dequeue();
                            }
                            RelationOK = dal.SaveRelation(curCodes, Codes[0], task.ID, WorkCenterList[2].ID,PackFlagLV3);
                            #endregion

                            #region 关联成功
                            if (RelationOK)
                            {
                                QueueLV3.Enqueue(Codes[0]);
                                if (WCinfo3.WorkcenterViewer != null)
                                {
                                    WCinfo3.WorkViewer_Show(WorkCenterViewer.SpeakMsg.成功, "成功");
                                }
                            }
                            else
                            {
                                WCinfo3.ShowMessage("关联错误！");
                                if (WCinfo3.WorkcenterViewer != null)
                                {
                                    WCinfo3.WorkViewer_Show(WorkCenterViewer.SpeakMsg.错误, "关联错误");
                                }
                            }

                            #endregion
                        }
                         #region 保存工位信息
                        if (RelationOK)
                        {
                            WorkCenter.Data.CurrentNum += successNum;
                            WorkCenterList[1].Data.FinishedNum -= 1;
                            WorkCenterList[1].Data.PackageNum += 1;
                        }
                        using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(Config.DBCCN))
                        {
                            dal.SaveWorkCenterData(WorkCenter.Data);
                            dal.SaveWorkCenterData(WorkCenterList[1].Data);
                        }
                        WCinfo3.BindingWorkCenter();
                        #endregion
                        return RelationOK;
                    }
                default:
                    return false;

            }
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool StartEquipment()
        {
            try
            {
                foreach (WorkCenterCtl.WorkCenterInfo wcinfo in WorkCenterPanel.Controls)
                {
                    if (!wcinfo.OpenPort())
                    {
                        throw new Exception("打开设备错误！");
                    }
                }
                return true;
            }
            catch
            {
                StopEquipment();
                MessageBox.Show("打开设备错误！");
                return false;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool StopEquipment()
        {
            foreach (WorkCenterCtl.WorkCenterInfo wcinfo in WorkCenterPanel.Controls)
            {
                wcinfo.StopPort();
            }
            return true;
        }

        /// <summary>
        /// 零箱操作
        /// </summary>
        /// <param name="WorkCenter"></param>
        /// <returns></returns>
        public bool Lingxiang(Models.WorkCenter WorkCenter)
        {
            if (QueueLV1.Count >= LV1Num)
            {
                //msg = "操作失败，工位已满足整包数量，无需零箱操作！";
                return false;
            }
            switch (WorkCenter.LevelNo)
            {
                case 1:
                    //msg = "操作失败！该工位不允许零箱操作！";
                    return false;
                case 2:
                    if (PackFlagLV2 == Models.CodePackEnum.零箱)
                    {
                        PackFlagLV2 = Models.CodePackEnum.整箱;
                        //msg = "已切换至整包状态";
                        return false;
                    }
                    else
                    {
                        PackFlagLV2 = Models.CodePackEnum.零箱;
                        //msg = "已切换至零箱状态";
                        return true;
                    }
                case 3:
                    if (PackFlagLV3 == Models.CodePackEnum.零箱)
                    {
                        PackFlagLV3 = Models.CodePackEnum.整箱;
                        //msg = "已切换至整包状态";
                        return false;
                    }
                    else
                    {
                        PackFlagLV3 = Models.CodePackEnum.零箱;
                       // msg = "已切换至零箱状态";
                        return true;
                    }
                default:
                    //msg = "操作错误！";
                    return false;
            }
        }

        /// <summary>
        /// 拼箱操作
        /// </summary>
        /// <param name="WorkCenter"></param>
        /// <returns></returns>
        public bool Pinxiang(Models.WorkCenter WorkCenter)
        {
            switch (WorkCenter.LevelNo)
            {
                case 1:
                    if (PackFlagLV1 == Models.CodePackEnum.拼箱)
                    {
                        PackFlagLV1 = Models.CodePackEnum.整箱;
                        //msg = "已切换至整箱状态";
                        return false;
                    }
                    else
                    {
                        PackFlagLV1 = Models.CodePackEnum.拼箱;
                        //msg = "已切换至拼箱状态";
                        return true;
                    }
                case 2:
                    if (PackFlagLV2 == Models.CodePackEnum.拼箱)
                    {
                        PackFlagLV2 = Models.CodePackEnum.整箱;
                        //msg = "已切换至整箱状态";
                        return false;
                    }
                    else
                    {
                        PackFlagLV2 = Models.CodePackEnum.拼箱;
                        //msg = "已切换至拼箱状态";
                        return true;
                    }
                case 3:
                    if (PackFlagLV3 == Models.CodePackEnum.拼箱)
                    {
                        PackFlagLV3 = Models.CodePackEnum.整箱;
                        //msg = "已切换至整箱状态";
                        return false;
                    }
                    else
                    {
                        PackFlagLV3 = Models.CodePackEnum.拼箱;
                        //msg = "已切换至拼箱状态";
                        return true;
                    }
                default:
                    //msg = "操作错误！";
                    return false;
            }
        }

        /// <summary>
        /// 散装操作
        /// </summary>
        /// <param name="WorkCenter"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool Sanzhuang(Models.WorkCenter WorkCenter)
        {
            if (MessageBox.Show("确定进行散装操作？", "散装操作", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                //msg = "操作被取消";
                return false;
            }
            bool ok = false;
            using (DAL.PackageCodeDAL dal = new DAL.PackageCodeDAL(SysInfo.Config.DBCCN))
            {
                int num;
                string[] codes;
                switch (WorkCenter.LevelNo)
                {
                    case 1:
                        num=QueueLV1.Count;
                        codes = new string[num];
                        if (num <= 0)
                        {
                           // msg = "操作失败！该工位没有能散装的包装！";
                            return false;
                        }
                        #region 散装操作
                        string[] codes1 = new string[num];
                        for (int i = 0; i < num; i++)
                        {
                            codes[i] = QueueLV1.Dequeue();
                        }
                         ok=dal.Sanzhuang(codes,WorkCenter.ID);
                        // msg = ok ? "散装操作成功！" : "散装操作失败！";
                        #endregion

                    #region 更新工位状态
                         WorkCenter.Data.CurrentNum = 0;
                         WorkCenter.Data.FinishedNum = WorkCenter.Data.CurrentNum / LV1Num;
                         using (DAL.WorkCenterDAL wdal = new DAL.WorkCenterDAL(Config.DBCCN))
                         {
                             wdal.SaveWorkCenterData(WorkCenter.Data);
                         }
                         WorkCenterCtl.WorkCenterInfo wcinfo1 = WorkCenterPanel.Controls[0] as WorkCenterCtl.WorkCenterInfo;
                         wcinfo1.BindingWorkCenter();
                    #endregion
                         return ok;
                    case 2:
                         num=QueueLV1.Count;
                        codes = new string[num];
                        if (num <= 0)
                        {
                          //  msg = "操作失败！该工位没有能散装的包装！";
                            return false;
                        }
                        #region 散装操作
                        for (int i = 0; i < num; i++)
                        {
                            codes[i] = QueueLV1.Dequeue();
                        }
                         ok=dal.Sanzhuang(codes,WorkCenter.ID);
                          #region 更新工位状态
                         WorkCenter.Data.CurrentNum = 0;
                         WorkCenter.Data.FinishedNum = WorkCenter.Data.CurrentNum / LV2Num;
                         using (DAL.WorkCenterDAL wdal = new DAL.WorkCenterDAL(Config.DBCCN))
                         {
                             wdal.SaveWorkCenterData(WorkCenter.Data);
                         }
                         WorkCenterCtl.WorkCenterInfo wcinfo2 = WorkCenterPanel.Controls[1] as WorkCenterCtl.WorkCenterInfo;
                         wcinfo2.BindingWorkCenter();
                    #endregion

                         //msg = ok ? "散装操作成功！" : "散装操作失败！";
                         return ok;
                        #endregion
                    case 3:
                        // msg = "操作失败！该工位不能进行散装操作";
                        return false;
                    default:
                       // msg = "操作失败";
                        return false;
                }

            }
        }
    }
}
