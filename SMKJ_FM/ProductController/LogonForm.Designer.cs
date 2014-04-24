namespace ProductController
{
    partial class LogonForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UserCodeTxt = new System.Windows.Forms.TextBox();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.LogonBtn = new System.Windows.Forms.Button();
            this.SettingBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            // 
            // UserCodeTxt
            // 
            this.UserCodeTxt.Location = new System.Drawing.Point(119, 63);
            this.UserCodeTxt.Name = "UserCodeTxt";
            this.UserCodeTxt.Size = new System.Drawing.Size(100, 21);
            this.UserCodeTxt.TabIndex = 2;
            this.UserCodeTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserCodeTxt_KeyDown);
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.Location = new System.Drawing.Point(119, 118);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.PasswordChar = '#';
            this.PasswordTxt.Size = new System.Drawing.Size(100, 21);
            this.PasswordTxt.TabIndex = 3;
            this.PasswordTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordTxt_KeyDown);
            // 
            // LogonBtn
            // 
            this.LogonBtn.Location = new System.Drawing.Point(58, 203);
            this.LogonBtn.Name = "LogonBtn";
            this.LogonBtn.Size = new System.Drawing.Size(75, 23);
            this.LogonBtn.TabIndex = 4;
            this.LogonBtn.Text = "登陆";
            this.LogonBtn.UseVisualStyleBackColor = true;
            this.LogonBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingBtn
            // 
            this.SettingBtn.Location = new System.Drawing.Point(168, 203);
            this.SettingBtn.Name = "SettingBtn";
            this.SettingBtn.Size = new System.Drawing.Size(75, 23);
            this.SettingBtn.TabIndex = 5;
            this.SettingBtn.Text = "设置";
            this.SettingBtn.UseVisualStyleBackColor = true;
            this.SettingBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // LogonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.SettingBtn);
            this.Controls.Add(this.LogonBtn);
            this.Controls.Add(this.PasswordTxt);
            this.Controls.Add(this.UserCodeTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LogonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登陆";
            this.Load += new System.EventHandler(this.LogonForm_Load);
            this.Shown += new System.EventHandler(this.LogonForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserCodeTxt;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.Button LogonBtn;
        private System.Windows.Forms.Button SettingBtn;
    }
}

