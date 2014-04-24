using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO.Ports;
using SysInfo;

namespace WorkCenterCtl
{
    public partial class WorkCenterInfo : UserControl
    {
        Models.CodePackEnum PackFlag = Models.CodePackEnum.整箱;
        public WorkCenterInfo()
        {
            InitializeComponent();
            showWorkCenter = new ShowWorkCenterDelegate(ShowWorkCenter);
            showMsgDelegate = new ShowMsgDelegate(ShowMsg);
            showButton = new ShowButtonDelegate(ShowButton);
            button1.BackColor = Color.DarkTurquoise;
            button2.BackColor = Color.DarkTurquoise;
            button3.BackColor = Color.DarkTurquoise;
        }
        /// <summary>
        /// 工位
        /// </summary>
        public  Models.WorkCenter WkCenter
        {
            get;
            set;
        }

        public Scan.Scanner ScanEquipment;//扫描器

        public WorkCenterViewer.WorkViewer WorkcenterViewer;//工位显示器

        //public Models.Equipment Printer;//打印机

        #region 扫描器代理
        public ScanCodeDelegate scanDelegate;
        #endregion

        #region 包装操作代理
        public PinxiangDelegate pinxiangDelegate;//拼箱
        public LingxiangDelegate lingxiangDelegate;//零箱
        public SanzhuangDelegate sanzhuangDelegate;//散装
        #endregion

        /// <summary>
        /// 绑定工位
        /// </summary>
        /// <param name="workCenter"></param>
        public void BindingWorkCenter(Models.WorkCenter workCenter,string TaskID)
        {
            WkCenter = workCenter;
            using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(SysInfo.Config.DBCCN))
            {
                WkCenter.Data = dal.GetWorkCenterData(TaskID, WkCenter.ID);
            }
            ShowWorkCenter();
            if (WkCenter.LevelNo == 1)
            {
                button1.Visible = false;
            }
            #region 初始化设备
            foreach (Models.Equipment e in WkCenter.Equipments)
                {
                    switch (e.Setting.DriverType)
                    {
                        case Models.EquipmentSettingDriverType.串口:
                            SerialPort serialPort = new SerialPort();
                            serialPort.PortName = e.Port;
                            serialPort.BaudRate = int.Parse(e.BaudRate);
                            serialPort.DataBits = int.Parse(e.DataBits);
                            switch (e.StopBits)
                            {
                                case Models.StopBitsEnum.使用一个停止位:
                                    serialPort.StopBits = StopBits.One;
                                    break;
                                //case Models.StopBitsEnum.使用一点五个停止位:
                                //    serialPort.StopBits = StopBits.OnePointFive;
                                //    break;
                                case Models.StopBitsEnum.使用两个停止位:
                                    serialPort.StopBits = StopBits.Two;
                                    break;
                                default:
                                    serialPort.StopBits = StopBits.One;
                                    break;
                            }
                            switch (e.Parity)
                            {
                                case Models.ParityEnum.不发生奇偶校验检查:
                                    serialPort.Parity = Parity.None;
                                    break;
                                case Models.ParityEnum.将奇偶校验位保留为0:
                                    serialPort.Parity = Parity.Space;
                                    break;
                                case Models.ParityEnum.将奇偶校验位保留为1:
                                    serialPort.Parity = Parity.Mark;
                                    break;
                                case Models.ParityEnum.设置奇偶校验位使位数等于偶数:
                                    serialPort.Parity = Parity.Even;
                                    break;
                                case Models.ParityEnum.设置奇偶校验位使位数等于奇数:
                                    serialPort.Parity = Parity.Odd;
                                    break;
                            }
                            #region 设置各类设备索引
                            if (e.Setting.Category==Models.EquipmentSettingCategoryEnum.条码扫描器)
                            {
                                ScanEquipment = new Scan.Scanner(serialPort, new Scan.ReceivedDataDelegate(ReceivedCode));//注册扫描到码后的事件
                            }
                            else if (e.Setting.Category == Models.EquipmentSettingCategoryEnum.工位显示器)
                            {
                                WorkcenterViewer = new WorkCenterViewer.WorkViewer(serialPort);
                            }
                            else if (e.Setting.Category == Models.EquipmentSettingCategoryEnum.打印机)
                            {
                            }
                            #endregion

                            break;
                        case Models.EquipmentSettingDriverType.并口:
                            break;
                        case Models.EquipmentSettingDriverType.网口:
                            break;
                        case Models.EquipmentSettingDriverType.驱动:
                            break;
                    }
                }
            #endregion
        }

        public void BindingWorkCenter()
        {
            Invoke(showWorkCenter, new object[] { });
        }

        delegate void ShowWorkCenterDelegate();

        ShowWorkCenterDelegate showWorkCenter;//显示工位信息代理类 

        public void ShowWorkCenter()
        {
            WorkCenterNameTxt.Text = WkCenter.WorkCenterCode;
            if (WkCenter.Data != null)
            {
                CurrentNumLbl.Text = WkCenter.Data.CurrentNum.ToString();
                finishedNumLbl.Text = WkCenter.Data.FinishedNum.ToString();
                PackageNumLbl.Text = WkCenter.Data.PackageNum.ToString();
                LevelNoLbl.Text = "LV " + WkCenter.LevelNo.ToString();
            }
        }
        /// <summary>
        /// 接收码进行处理
        /// </summary>
        /// <param name="codes"></param>
        public void ReceivedCode(string[] codes)
        {
            bool ok=scanDelegate(this.WkCenter, codes);
            if (ok)
            {
                if (PackFlag == Models.CodePackEnum.零箱)
                {
                    ok = (bool)(this.Invoke(lingxiangDelegate, new object[] { WkCenter }));
                    PackFlag = ok ? Models.CodePackEnum.零箱 : Models.CodePackEnum.整箱;
                    this.Invoke(showButton);
                }
            }
        }

        /// <summary>
        /// 打开工位设备端口
        /// </summary>
        public bool OpenPort()
        {
            try
            {
                if (ScanEquipment != null)
                {
                    if (!ScanEquipment.Open())
                    {
                        throw new Exception("打开设备失败！");
                    }
                }
                if (WorkcenterViewer != null)
                {
                    if (!WorkcenterViewer.Open())
                    {
                        throw new Exception("打开设备失败！");
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 关闭工位设备端口
        /// </summary>
        /// <returns></returns>
        public bool StopPort()
        {
            if (ScanEquipment != null)
            {
                ScanEquipment.Close();
            }
            if (WorkcenterViewer != null)
            {
                WorkcenterViewer.Close();
            }
            return true;
        }

        /// <summary>
        /// 显示工位信息
        /// </summary>
        public void WorkViewer_Show(WorkCenterViewer.SpeakMsg speakMsg, string msg)
        {
            WorkcenterViewer.SpeakOn();//开启语音
            WorkcenterViewer.Speak(speakMsg);//播放语音
            WorkcenterViewer.SpeakOff();//关闭语音
            StringBuilder SBuilder = new StringBuilder();
            SBuilder.AppendFormat("工位 {0} 已扫描 {1} 已完成 {2} 信息:{3}", new object[]{
                WkCenter.LevelNo.ToString(),WkCenter.Data.CurrentNum.ToString().PadLeft(3,'0'),WkCenter.Data.FinishedNum.ToString().PadLeft(4,'0'),msg
            });
            WorkcenterViewer.CLS();//清屏
            WorkcenterViewer.Write(SBuilder.ToString());//显示信息
        }
        delegate void ShowMsgDelegate(string msg);
        ShowMsgDelegate showMsgDelegate;

        void ShowMsg(string msg)
        {
            textBox1.AppendText("\r\n" + msg);
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMessage(string msg)
        {
            this.Invoke(showMsgDelegate, new object[] { msg });
        }
        /// <summary>
        /// 零箱操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            bool ok=(bool)(this.Invoke(lingxiangDelegate, new object[] { WkCenter}));
            PackFlag = ok ? Models.CodePackEnum.零箱 : Models.CodePackEnum.整箱;
            if (ok)
            {
                this.Invoke(showMsgDelegate, new object[] { "请扫描【" + WkCenter.LevelNo.ToString() + "级码】，完成零箱操作！" });
            }
            else
            {
                this.Invoke(showMsgDelegate, new object[] {"操作失败！无可操作零箱！"});
            }
            ShowButton();
        }

        /// <summary>
        /// 散装操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            bool ok = (bool)this.Invoke(sanzhuangDelegate, new object[] { WkCenter});
            this.Invoke(showMsgDelegate, new object[] {ok?"散装操作成功！":"散装操作失败！" });
        }

        /// <summary>
        /// 拼箱操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            bool ok=(bool)this.Invoke(pinxiangDelegate, new object[] { WkCenter });
            PackFlag = ok ? Models.CodePackEnum.拼箱 : Models.CodePackEnum.整箱;
            ShowButton();
        }

        delegate void ShowButtonDelegate();
        ShowButtonDelegate showButton;
        private void ShowButton()
        {
            switch (PackFlag)
            {
                case Models.CodePackEnum.零箱:
                    button1.BackColor = Color.Blue;
                    button2.BackColor = Color.DarkTurquoise;
                    button3.BackColor = Color.DarkTurquoise;
                    break;
                case Models.CodePackEnum.散装:
                    button1.BackColor = Color.DarkTurquoise;
                    button2.BackColor = Color.DarkTurquoise;
                    button3.BackColor = Color.Blue;
                    break;
                case Models.CodePackEnum.拼箱:
                    button1.BackColor = Color.DarkTurquoise;
                    button2.BackColor = Color.Blue;
                    button3.BackColor = Color.DarkTurquoise;
                    break;
                case Models.CodePackEnum.整箱:
                    button1.BackColor = Color.DarkTurquoise;
                    button2.BackColor = Color.DarkTurquoise;
                    button3.BackColor = Color.DarkTurquoise;
                    break;
            }
        }
    }
}
