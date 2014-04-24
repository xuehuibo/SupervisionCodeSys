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
    public partial class WorkCenterSettingForm : Form
    {
        public ProductLineCtl productLineCtl
        {
            get;
            set;
        }

        public Models.WorkCenter workCenter
        {
            get;
            set;
        }

        public string DBConnection
        {
            get;
            set;
        }
        
        public WorkCenterSettingForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //#region 检查当前工位是否有相同编码
            //if (!productLineCtl.TestWorkCenterCode(WorkCenterCodeTxtBox.Text))
            //{
            //    MessageBox.Show("该工位编码在当前流水线中已存在!");
            //    WorkCenterCodeTxtBox.Focus();
            //    WorkCenterCodeTxtBox.SelectAll();
            //    return;
            //}
            //#endregion

            //#region 检查数据库中是否有相同编码
            //using (DAL.WorkCenterDAL dal = new DAL.WorkCenterDAL(DBConnection))
            //{
            //    try
            //    {
            //        if (!dal.TestCode(WorkCenterCodeTxtBox.Text))
            //        {
            //            MessageBox.Show("该工位编码已存在!");
            //            WorkCenterCodeTxtBox.Focus();
            //            WorkCenterCodeTxtBox.SelectAll();
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        return;
            //    }
            //}
            //#endregion

            short i;
            if (!short.TryParse(LevelTxtBox.Text,out i))
            {
                MessageBox.Show("工位级别非法!");
                return;
            }
            if (workCenter == null)
            {
                workCenter = new Models.WorkCenter();
            }
            workCenter.WorkCenterCode = WorkCenterCodeTxtBox.Text;
            workCenter.LevelNo = short.Parse(LevelTxtBox.Text);
            workCenter.Remark = RemarkTxtBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        public void ReView()
        {
            if (workCenter != null)
            {
                WorkCenterCodeTxtBox.Text = workCenter.WorkCenterCode;
                LevelTxtBox.Text = workCenter.LevelNo.ToString();
                RemarkTxtBox.Text = workCenter.Remark;
            }
            else
            {
                WorkCenterCodeTxtBox.Clear();
                LevelTxtBox.Text=productLineCtl.GetNextLevelNo().ToString();
                RemarkTxtBox.Clear();
            }
        }
    }
}
