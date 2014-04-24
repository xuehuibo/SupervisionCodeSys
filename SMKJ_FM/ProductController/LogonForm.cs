using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models;
using System.Net;
using System.Collections;
using System.Configuration;
using SysInfo;
namespace ProductController
{
    public partial class LogonForm : Form
    {
        public LogonForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok;
            string msg;
            using (DAL.UserLoginDAL dal = new DAL.UserLoginDAL(Config.DBCCN))
            {
                Models.LoginUser user = dal.Login(UserCodeTxt.Text, PasswordTxt.Text, string.Empty, out ok, out msg);
            }
            if (ok)
            {
                ProductControllerForm productForm = new ProductControllerForm();
                productForm.LF = this;
                this.Hide();
                productForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfigForm CF = new ConfigForm();
            CF.ShowDialog();
        }

        private void LogonForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LogonForm_Shown(object sender, EventArgs e)
        {
            Config.WebURL = ConfigurationManager.AppSettings["WebURL"] ;
            try
            {
                System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.WebURL + Config.API_GetDBConnection);
                request.Method = "get";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream s = response.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(s);
                Hashtable db = Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(sr.ReadToEnd());
                Config.DBCCN = db["DBCCN"] as string;
                request = (HttpWebRequest)WebRequest.Create(Config.WebURL + Config.API_TestConfig + "?WorkComputerName=" + Config.WorkComputerName);
                request.Method = "get";
                response = (HttpWebResponse)request.GetResponse();
                s = response.GetResponseStream();
                sr = new System.IO.StreamReader(s);
                if (!Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(sr.ReadToEnd()))
                {
                    throw new Exception("本工控机未注册,系统不能正常运行!请联系管理员!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                button2_Click(null, null);
            }
        }

        private void UserCodeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    PasswordTxt.Focus();
                    break;
            }
        }

        private void PasswordTxt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    LogonBtn.Focus();
                    break;
            }
        }
    }
}
