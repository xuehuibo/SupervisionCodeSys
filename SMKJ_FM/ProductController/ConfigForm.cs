using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Configuration;
using SysInfo;

namespace ProductController
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            WebUrlTxt.Text = Config.WebURL;
            WorkComputerTxt.Text = Config.WorkComputerName;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TestUrlBtn_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.HttpWebRequest request
                    = (HttpWebRequest)WebRequest.Create(WebUrlTxt.Text + Config.API_TestConfig + "?WorkComputerName=" + Config.WorkComputerName);
                request.Method = "get";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream s = response.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(s);
                if (!Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(sr.ReadToEnd()))
                {
                    SaveBtn.Enabled = false;
                    throw new Exception("本工控机未注册,系统不能正常运行!请联系管理员!");
                }
                else
                {
                    MessageBox.Show("测试通过!请保存参数!");
                    SaveBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SaveBtn.Enabled = false;
            }
        }

        private void WebUrlTxt_TextChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = false;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration c = ConfigurationManager.OpenExeConfiguration("ProductController.exe");
                AppSettingsSection app = c.AppSettings;
                app.Settings["WebURL"].Value = WebUrlTxt.Text;
                c.Save();
                Config.WebURL = WebUrlTxt.Text;
                MessageBox.Show("保存成功!");
                SaveBtn.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
