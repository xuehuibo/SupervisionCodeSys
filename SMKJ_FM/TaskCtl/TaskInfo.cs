using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaskCtl
{
    public partial class TaskInfo : UserControl
    {
        /// <summary>
        /// 当前任务
        /// </summary>
        public Models.Task CurTask
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定任务
        /// </summary>
        /// <param name="task"></param>
        public void BindingTask(Models.Task task)
        {
            CurTask = task;
            TaskNameLbl.Text = CurTask.TaskCode;
            BatchNoLbl.Text = CurTask.BatchNo;
            ProductNameLbl.Text = CurTask.Product.ProductName;
            TaskAmountLbl.Text = CurTask.TaskAmount.ToString();
            DoneAmountLbl.Text = CurTask.DoneAmount.ToString();
            StatusLbl.Text = CurTask.Status.ToString();
            switch (CurTask.Status)
            {
                case Models.TaskStatus.已审核:
                    StartBtn.Enabled = true;
                    StopBtn.Enabled = true;
                    break;
                case Models.TaskStatus.已暂停:
                    StartBtn.Enabled = true;
                    StopBtn.Enabled = true;
                    break;
                case Models.TaskStatus.运行中:
                    StartBtn.Enabled = false;
                    StopBtn.Enabled = true;
                    break;
                case Models.TaskStatus.已结束:
                    StartBtn.Enabled = false;
                    StopBtn.Enabled = false;
                    break;
            }
        }

        public TaskInfo()
        {
            InitializeComponent();
        }
        TaskShowForm tsf = new TaskShowForm();//任务明细窗口
        /// <summary>
        /// 开启任务
        /// </summary>
        private void StartTask()
        {
            tsf.BindingTask(CurTask);//绑定任务
            if (tsf.StartEquipment())//开启设备
            {
                string msg;
                if (tsf.StartTask(out msg))
                {
                    CurTask.Status = Models.TaskStatus.运行中;
                    StatusLbl.Text = CurTask.Status.ToString();
                    tsf.ShowDialog();
                    StatusLbl.Text = CurTask.Status.ToString();
                    BindingTask(CurTask);
                }
                else
                {
                    MessageBox.Show(msg);
                }
            }
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        private void StopTask()
        {
            if (MessageBox.Show("有确定要结束任务？", "结束任务", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;//取消结束
            }
            using (DAL.TaskDAL dal = new DAL.TaskDAL(SysInfo.Config.DBCCN))
            {
                string msg;
                dal.StopTask(CurTask.ID, out msg);
            }
            StartBtn.Enabled = false;
            StopBtn.Enabled = false;
            CurTask.Status = Models.TaskStatus.已结束;
            StatusLbl.Text = CurTask.Status.ToString();
            BindingTask(CurTask);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            StartTask();
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopTask();
        }
    }
}
