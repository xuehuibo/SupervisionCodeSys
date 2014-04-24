using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysInfo;

namespace ProductController
{
    public partial class ProductControllerForm : Form
    {
        List<Models.Task> TaskList;

        /// <summary>
        /// 登陆对话框
        /// </summary>
        public LogonForm LF
        {
            get;
            set;
        }

        public ProductControllerForm()
        {
            InitializeComponent();
        }

        private void ProductControllerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LF.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void showMsg(string msg){
            MessageBox.Show(msg);
        }

        private void ProductControllerForm_Load(object sender, EventArgs e)
        {
            LoadTask();
        }

        private void LoadTask()
        {
            using (DAL.TaskDAL dal = new DAL.TaskDAL(Config.DBCCN))
            {
                TaskList = dal.GetTaskByWorkComputerName(Config.WorkComputerName);      
            }
            if (TaskList != null)
            {
                foreach (Models.Task task in TaskList)
                {
                    TaskCtl.TaskInfo taskCtl = new TaskCtl.TaskInfo();
                    taskCtl.BindingTask(task);
                    TaskLayoutPanel.Controls.Add(taskCtl);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}
