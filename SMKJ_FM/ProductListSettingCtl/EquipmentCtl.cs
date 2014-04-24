using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProductLineSettingCtl
{
    partial class EquipmentCtl : UserControl
    {
        public bool IsSelect
        {
            get;
            set;
        }
        /// <summary>
        /// 所在生产线
        /// </summary>
        public ProductLineCtl ParentProductLineCtl
        {
            get;
            set;
        }

        /// <summary>
        /// 所在工位
        /// </summary>
        public WorkCenterCtl ParentWorkCenterCtl
        {
            get;
            set;
        }

        public EquipmentCtl()
        {
            InitializeComponent();
        }

        #region 点击选择
        private void EquipmentCtl_MouseClick(object sender, MouseEventArgs e)
        {
            IsSelect = !IsSelect;
            ParentProductLineCtl.CurUC = IsSelect ? this : null;
            SelectThis();
            ParentProductLineCtl.ReFlushSelectCtl(this);
        }
        public void SelectThis()
        {
            panel1.BackColor = IsSelect ? Color.GreenYellow : Color.Honeydew;
        }
        #endregion

        /// <summary>
        /// 刷新显示
        /// </summary>
        public void ReView()
        {
            NameLbl.Text = (this.Tag as Models.Equipment).EquipmentCode+"    ";
        }



    }
}
