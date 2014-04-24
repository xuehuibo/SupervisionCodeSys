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
     partial class WorkCenterCtl : UserControl
    {
         /// <summary>
         /// 所在生产线
         /// </summary>
         public ProductLineCtl productLineCtl
         {
             get;
             set;
         }

         /// <summary>
         /// 是否已选择
         /// </summary>
         public bool IsSelect
         {
             get;
             set;
         }

        public WorkCenterCtl()
        {
            InitializeComponent();
        }

        public void ReView()
        {
            try
            {
                LevelLbl.Text = "LV-"+(Tag as Models.WorkCenter).LevelNo.ToString();
                CodeLbl.Text = (Tag as Models.WorkCenter).WorkCenterCode;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 向工位添加设备
        /// </summary>
        /// <param name="equipmentCtl"></param>
        public void AddEquipment(EquipmentCtl equipmentCtl)
        {
            Models.WorkCenter WC = this.Tag as Models.WorkCenter;
            if (!WC.Equipments.Contains(equipmentCtl.Tag as Models.Equipment))
            {
                WC.Equipments.Add(equipmentCtl.Tag as Models.Equipment);
            }


            switch (((Models.Equipment)equipmentCtl.Tag).Setting.InOutType)
            {
                case Models.EquipmentSettingInOutType.输出设备:
                    OutPanel.Controls.Add(equipmentCtl);
                    break;
                case Models.EquipmentSettingInOutType.输入设备:
                    InPanel.Controls.Add(equipmentCtl);
                    break;
            }
        }

        /// <summary>
        /// 从工位删除设备
        /// </summary>
        /// <param name="equipmentCtl"></param>
        public void DelEquipment(EquipmentCtl equipmentCtl)
        {
            Models.Equipment equipment = (Models.Equipment)(equipmentCtl.Tag);
            (Tag as Models.WorkCenter).Equipments.Remove(equipment);
            switch (equipment.Setting.InOutType)
            {
                case Models.EquipmentSettingInOutType.输入设备:
                    InPanel.Controls.Remove(equipmentCtl);
                    break;
                case Models.EquipmentSettingInOutType.输出设备:
                    OutPanel.Controls.Remove(equipmentCtl);
                    break;
            }

        }

        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            IsSelect = !IsSelect;
            productLineCtl.CurUC = IsSelect? this:null;
            productLineCtl.ReFlushSelectCtl(this);
            SelectThis();
        }
        public void SelectThis()
        {
            splitContainer1.BackColor = IsSelect ? Color.GreenYellow : Color.Azure;
        }
    }
}
