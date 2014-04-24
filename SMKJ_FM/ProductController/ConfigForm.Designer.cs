namespace ProductController
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.WebUrlTxt = new System.Windows.Forms.TextBox();
            this.TestUrlBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.WorkComputerTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器地址";
            // 
            // WebUrlTxt
            // 
            this.WebUrlTxt.Location = new System.Drawing.Point(101, 43);
            this.WebUrlTxt.Name = "WebUrlTxt";
            this.WebUrlTxt.Size = new System.Drawing.Size(184, 21);
            this.WebUrlTxt.TabIndex = 1;
            this.WebUrlTxt.TextChanged += new System.EventHandler(this.WebUrlTxt_TextChanged);
            // 
            // TestUrlBtn
            // 
            this.TestUrlBtn.Location = new System.Drawing.Point(51, 157);
            this.TestUrlBtn.Name = "TestUrlBtn";
            this.TestUrlBtn.Size = new System.Drawing.Size(75, 23);
            this.TestUrlBtn.TabIndex = 2;
            this.TestUrlBtn.Text = "测试";
            this.TestUrlBtn.UseVisualStyleBackColor = true;
            this.TestUrlBtn.Click += new System.EventHandler(this.TestUrlBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Enabled = false;
            this.SaveBtn.Location = new System.Drawing.Point(132, 157);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 3;
            this.SaveBtn.Text = "保存";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(213, 157);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "工控机名称";
            // 
            // WorkComputerTxt
            // 
            this.WorkComputerTxt.Location = new System.Drawing.Point(101, 100);
            this.WorkComputerTxt.Name = "WorkComputerTxt";
            this.WorkComputerTxt.ReadOnly = true;
            this.WorkComputerTxt.Size = new System.Drawing.Size(184, 21);
            this.WorkComputerTxt.TabIndex = 6;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 200);
            this.Controls.Add(this.WorkComputerTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.TestUrlBtn);
            this.Controls.Add(this.WebUrlTxt);
            this.Controls.Add(this.label1);
            this.Name = "ConfigForm";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WebUrlTxt;
        private System.Windows.Forms.Button TestUrlBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox WorkComputerTxt;
    }
}