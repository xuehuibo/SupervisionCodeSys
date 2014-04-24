using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace ProductLineSettingCtl
{
    [Guid("A850C423-9F91-4E70-8802-FAA7542E5269")]
    public partial class ProductLineCtl : UserControl,IObjectSafety
    {
        public ProductLineCtl()
        {
            InitializeComponent();
            //Init("AAFD5024-D761-4343-8C82-506F31E74DC4", "9862A4D0089F364DD108EA396ACD3C3272A1F323DD061B22352970DFE9CD1BAAA5CB8521409A85F67B8A2F0AE80BFF05436D1222C43C9014EB2B24E489B78443222C44B259C52C3DA96E133BB294CF4493F7E0A18D29D907F6AE3CAD26A26D51");
        }
        private Des des = new Des();

        #region IObjectSafety 成员

        public void GetInterfacceSafyOptions(int riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = 1;
            pdwEnabledOptions = 2;
        }

        public void SetInterfaceSafetyOptions(int riid, int dwOptionsSetMask, int dwEnabledOptions)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 输入参数
        /// <summary>
        /// 数据库连接
        /// </summary>
        private string DBConnection
        {
            get;
            set;
        }

        /// <summary>
        /// 生产线ID
        /// </summary>
        private Guid ProductLineID
        {
            get;
            set;
        }
        #endregion

        #region 点击按钮
        private void button1_Click(object sender, EventArgs e)
        {
            AddWorkCenter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEquipment();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurUC == null)
            {
                MessageBox.Show("请先选择要配置的对象!");
                return;
            }
            else
            {
                switch (CurUC.GetType().ToString().Split('.').Last())
                {
                    case "WorkCenterCtl":
                            EditWorkCenter(CurUC.Tag as Models.WorkCenter);
                        break;
                    case "EquipmentCtl":
                            EditEquipment(CurUC.Tag as Models.Equipment);
                        break;
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (CurUC == null)
            {
                MessageBox.Show("请先选择删除对象");
            }
            else
            {
                switch (CurUC.GetType().ToString().Split('.').Last())
                {
                    case "WorkCenterCtl":
                        DeleteWorkCenter(CurUC as WorkCenterCtl);
                        break;
                    case "EquipmentCtl":
                        DeleteEquipment(CurUC as EquipmentCtl);
                        break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveProductLine())
                {
                    MessageBox.Show("保存成功!");
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败!错误信息:" + ex.Message);
            }
        }
        #endregion

        #region 选中状态
        public UserControl CurUC = null;
        public void ReFlushSelectCtl(UserControl UC)
        {

            foreach (UserControl ctl in CtlList)
            {
                string CtlType = ctl.GetType().ToString().Split('.').Last();
                switch (CtlType)
                {
                    case "WorkCenterCtl":
                        WorkCenterCtl WCC = ctl as WorkCenterCtl;
                        if (WCC != UC)
                        {
                            WCC.IsSelect = false;
                            WCC.SelectThis();
                        }
                        break;
                    case "EquipmentCtl":
                        EquipmentCtl EC = ctl as EquipmentCtl;
                        if (EC != UC)
                        {
                            EC.IsSelect = false;
                            EC.SelectThis();
                        }
                        break;
                }
            }
        }
        #endregion

        List<Models.WorkCenter> workCenterList=new List<Models.WorkCenter>();
        List<UserControl> CtlList = new List<UserControl>();
        List<Models.Equipment> equipmentList;//可用设备列表
        List<Models.EquipmentSetting> settingLIst;//设备类型列表

        WorkCenterSettingForm WCSF = new WorkCenterSettingForm();//工位设置窗口
        EquipmentSettingForm ESF = new EquipmentSettingForm();//设备设置窗口
        /// <summary>
        /// 初始化流水线
        /// </summary>
        /// <param name="ProductLineID">流水线ID</param>
        /// <param name="Str"></param>
        public void Init(string ProductLineID, string Str)
        {
            workCenterList = new List<Models.WorkCenter>();
            this.ProductLineID =new Guid(ProductLineID);
            this.DBConnection = des.DecryStrHex(Str, "0125");

            #region 读取流水线数据
            try
            {
                using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(DBConnection))
                {
                    List<SysInfo.Param> plist = new List<SysInfo.Param>();
                    plist.Add(new SysInfo.Param("@ProductLineID", ProductLineID));
                    dal.Select(plist, out workCenterList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            #endregion

            #region 显示流水线
            
            #endregion

            #region 读取设备类型列表
            using (DAL.EquipmentSettingDAL dal = new DAL.EquipmentSettingDAL(DBConnection))
            {
                List<SysInfo.Param> plist = new List<SysInfo.Param>();
                plist.Add(new SysInfo.Param("@ID", string.Empty));
                dal.Select(plist, out settingLIst);
            }
            #endregion

            #region 读取设备列表
            using (DAL.EquipmentDAL dal = new DAL.EquipmentDAL(DBConnection))
            {
                try
                {
                    List<SysInfo.Param> plist = new List<SysInfo.Param>();
                    plist.Add(new SysInfo.Param("@EquipmentCode", string.Empty));
                    plist.Add(new SysInfo.Param("@EquipmentSettingID", string.Empty));
                    dal.Select(plist, out equipmentList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion

            #region 显示流水线图
            foreach (Models.WorkCenter workcenter in workCenterList)
            {
                WorkCenterCtl WCC = new WorkCenterCtl();
                WCC.productLineCtl = this;
                WCC.Tag = workcenter;
                WorkCenterFlowPanel.Controls.Add(WCC);
                CtlList.Add(WCC);
                foreach (Models.Equipment equipment in workcenter.Equipments)
                {
                    EquipmentCtl EC = new EquipmentCtl();
                    EC.ParentProductLineCtl = this;
                    EC.ParentWorkCenterCtl = WCC;
                    EC.Tag = equipment;
                    EC.ReView();
                    CtlList.Add(EC);
                    WCC.AddEquipment(EC);
                }
                WCC.ReView();
            }
            #endregion

            #region 初始化窗口
            WCSF.DBConnection = DBConnection;
            WCSF.productLineCtl = this;

            ESF.DBConnection = DBConnection;
            ESF.EquipmentSettingList = settingLIst;
            ESF.EquipmentList = equipmentList;
            #endregion
        }

        /// <summary>
        /// 增加设备
        /// </summary>
        private void AddEquipment()
        {
            if (CurUC == null || CurUC.GetType().ToString().Split('.').Last() != "WorkCenterCtl")
            {
                MessageBox.Show("请先选择工位!");
                return;
            }
            ESF.Equipment = null;
            ESF.UnLockEquipment();
            ESF.ReView();
            if (ESF.ShowDialog() == DialogResult.OK)
            {
                EquipmentCtl ec = new EquipmentCtl();
                Models.Equipment eq = ESF.Equipment;
                ec.Tag = eq;
                ec.ParentProductLineCtl = this;
                ec.ParentWorkCenterCtl = CurUC as WorkCenterCtl;
                ec.ReView();
                (CurUC as WorkCenterCtl).AddEquipment(ec);
                CtlList.Add(ec);
            } 
        }

        /// <summary>
        /// 增加工位
        /// </summary>
        private void AddWorkCenter()
        {
            if (workCenterList.Count < 3)
            {
                WCSF.workCenter = null;
                WCSF.ReView();
                if (WCSF.ShowDialog() == DialogResult.OK)
                {
                    WorkCenterCtl WCCtl = new WorkCenterCtl();
                    Models.WorkCenter WC = WCSF.workCenter;
                    WC.ID = new Guid();
                    WCCtl.Tag = WC;
                    WCCtl.ReView();//显示工位信息
                    WC.Equipments = new List<Models.Equipment>();

                    WCCtl.productLineCtl = this;
                    WorkCenterFlowPanel.Controls.Add(WCCtl);
                    workCenterList.Add(WC);
                    CtlList.Add(WCCtl);
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("最多设置3个工位!");
            }
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="equipmentCtl"></param>
        private void DeleteEquipment(EquipmentCtl equipmentCtl)
        {
            if (MessageBox.Show("确定删除该设备?", "删除设备", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                equipmentCtl.ParentWorkCenterCtl.DelEquipment(equipmentCtl);
            }
        }



        List<Models.WorkCenter> RemoveWorkCenterList = new List<Models.WorkCenter>();//删除的工位
        /// <summary>
        /// 删除工位
        /// </summary>
        /// <param name="workCenterCtl"></param>
        private void DeleteWorkCenter(WorkCenterCtl workCenterCtl)
        {
            if ((workCenterCtl.Tag as Models.WorkCenter).Equipments.Count > 0)
            {
                MessageBox.Show("该工位已配备设备,不允许删除!请先删除工位上的设备.");
                return;
            }
            if (MessageBox.Show("确定删除该工位?删除后,此工位后序工位将提前.", "删除工位", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                Models.WorkCenter w = workCenterCtl.Tag as Models.WorkCenter;
                RemoveWorkCenterList.Add(w);
                workCenterList.Remove(w);
                foreach (Models.WorkCenter workcenter in workCenterList)
                {
                    if (workcenter.LevelNo > w.LevelNo)
                    {
                        workcenter.LevelNo--;
                    }
                }
                WorkCenterFlowPanel.Controls.Remove(workCenterCtl);
                foreach (WorkCenterCtl wc in WorkCenterFlowPanel.Controls)
                {
                    wc.ReView();
                }
            }
        }

        /// <summary>
        /// 保存流水线
        /// </summary>
        private bool SaveProductLine()
        {
            using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(DBConnection))
            {
                try
                {
                    string msg;
                    return dal.SaveList(ProductLineID, workCenterList,RemoveWorkCenterList, out msg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败!错误信息:" + ex.Message);
                    return false;
                }
            }
        }


        /// <summary>
        /// 编辑工位
        /// </summary>
        /// <param name="WC"></param>
        public void EditWorkCenter(Models.WorkCenter WC)
        {
            WCSF.workCenter = CurUC.Tag as Models.WorkCenter;
            WCSF.ReView();
            WCSF.ShowDialog();
            (CurUC as WorkCenterCtl).ReView();
        }


        /// <summary>
        /// 编辑设备
        /// </summary>
        /// <param name="Eq"></param>
        public void EditEquipment(Models.Equipment Eq)
        {
            ESF.Equipment = CurUC.Tag as Models.Equipment;
            ESF.ReView();
            ESF.LockEquipment();
            ESF.ShowDialog();
            (CurUC as EquipmentCtl).ReView();
        }

        /// <summary>
        /// 获取下一级工位的LevelNo
        /// </summary>
        /// <returns></returns>
        public int GetNextLevelNo()
        {
            return workCenterList.Count+1;
        }


    }
}
