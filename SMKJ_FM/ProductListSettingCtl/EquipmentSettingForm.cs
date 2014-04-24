using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProductLineSettingCtl
{
     partial class EquipmentSettingForm : Form
    {
         /// <summary>
         /// 可用设备列表
         /// </summary>
         public List<Models.Equipment> EquipmentList
         {
             get;
             set;
         }
         public string DBConnection
         {
             get;
             set;
         }
         /// <summary>
         /// 设备类型列表
         /// </summary>
         public List<Models.EquipmentSetting> EquipmentSettingList
         {
             get;
             set;
         }

         public Models.Equipment Equipment
         {
             get;
             set;
         }

        public EquipmentSettingForm()
        {
            InitializeComponent();
        }

         /// <summary>
         /// 显示指定设备明细
         /// </summary>
         /// <param name="Eq"></param>
        private void ReView(Models.Equipment Eq)
        {
            SettingCmbBox.DataSource = EquipmentSettingList;

            IPTxtBox.Text = Eq.IP;
            PortTxtBox.Text = Eq.Port;
            BaudrateTxtBox.Text = Eq.BaudRate;
            DatabitsTxtBox.Text = Eq.DataBits;
            StopbitsTxtBox.Text = Eq.StopBits;
            ParityTxtBox.Text = Eq.Parity;
            DelayTxtBox.Text = Eq.Delay.ToString();
        }
         /// <summary>
         /// 显示当前设备明细
         /// </summary>
        public void ReView()
        {
            SettingCmbBox.DataSource = EquipmentSettingList;

            if (Equipment != null)
            {
                Models.EquipmentSetting e = EquipmentSettingList.Where(s => s.ID == Equipment.Setting.ID).First();
                SettingCmbBox.SelectedItem = e;
                IPTxtBox.Text = Equipment.IP;
                PortTxtBox.Text = Equipment.Port;
                BaudrateTxtBox.Text = Equipment.BaudRate;
                DatabitsTxtBox.Text = Equipment.DataBits;
                StopbitsTxtBox.Text = Equipment.StopBits;
                ParityTxtBox.Text = Equipment.Parity;
                DelayTxtBox.Text = Equipment.Delay.ToString();
            }
            else
            {
                IPTxtBox.Clear();
                PortTxtBox.Clear();
                BaudrateTxtBox.Clear();
                DatabitsTxtBox.Clear();
                StopbitsTxtBox.Clear();
                ParityTxtBox.Clear();
                DelayTxtBox.Clear();
            }
        }

         /// <summary>
         /// 锁定设备,不允许更换设备
         /// </summary>
        public void LockEquipment()
        {
            EquipmentCmbBox.Enabled = false;
            SettingCmbBox.Enabled = false;
        }

         /// <summary>
         /// 解锁设备 允许更换设备
         /// </summary>
        public void UnLockEquipment()
        {
            SettingCmbBox.Enabled = true;
            EquipmentCmbBox.Enabled = true;
        }
         /// <summary>
         /// 将初始设备备份
         /// </summary>
        public void BackUpEquipment()
        {

        }

        private void SettingCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EquipmentCmbBox.DataSource = EquipmentList.Where(eq => eq.Setting.ID ==( (Models.EquipmentSetting)(SettingCmbBox.SelectedValue)).ID).ToList();
        }

        private void EquipmentCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReView(EquipmentCmbBox.SelectedItem as Models.Equipment);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            int delay;
            if (int.TryParse(DelayTxtBox.Text, out delay))
            {
                this.Equipment = EquipmentCmbBox.SelectedItem as Models.Equipment;
                Equipment.Delay = delay;
            }
            else
            {
                MessageBox.Show("请输入正确延迟数字,单位毫秒.");
            }
            this.Close();
        }
    }
}
