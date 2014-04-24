namespace ProductLineSettingCtl
{
    partial class EquipmentSettingForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SettingCmbBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.EquipmentCmbBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DelayTxtBox = new System.Windows.Forms.TextBox();
            this.BaudrateTxtBox = new System.Windows.Forms.TextBox();
            this.ParityTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StopbitsTxtBox = new System.Windows.Forms.TextBox();
            this.DatabitsTxtBox = new System.Windows.Forms.TextBox();
            this.PortTxtBox = new System.Windows.Forms.TextBox();
            this.IPTxtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 287);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(261, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(342, 281);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SettingCmbBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.EquipmentCmbBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 77);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择设备";
            // 
            // SettingCmbBox
            // 
            this.SettingCmbBox.DisplayMember = "Name";
            this.SettingCmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SettingCmbBox.FormattingEnabled = true;
            this.SettingCmbBox.Location = new System.Drawing.Point(85, 14);
            this.SettingCmbBox.Name = "SettingCmbBox";
            this.SettingCmbBox.Size = new System.Drawing.Size(121, 20);
            this.SettingCmbBox.TabIndex = 17;
            this.SettingCmbBox.ValueMember = "ID";
            this.SettingCmbBox.SelectedIndexChanged += new System.EventHandler(this.SettingCmbBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "选择设备";
            // 
            // EquipmentCmbBox
            // 
            this.EquipmentCmbBox.DisplayMember = "EquipmentCode";
            this.EquipmentCmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EquipmentCmbBox.FormattingEnabled = true;
            this.EquipmentCmbBox.Location = new System.Drawing.Point(85, 42);
            this.EquipmentCmbBox.Name = "EquipmentCmbBox";
            this.EquipmentCmbBox.Size = new System.Drawing.Size(121, 20);
            this.EquipmentCmbBox.TabIndex = 15;
            this.EquipmentCmbBox.ValueMember = "ID";
            this.EquipmentCmbBox.SelectedIndexChanged += new System.EventHandler(this.EquipmentCmbBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "设备类型";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DelayTxtBox);
            this.groupBox2.Controls.Add(this.BaudrateTxtBox);
            this.groupBox2.Controls.Add(this.ParityTxtBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.StopbitsTxtBox);
            this.groupBox2.Controls.Add(this.DatabitsTxtBox);
            this.groupBox2.Controls.Add(this.PortTxtBox);
            this.groupBox2.Controls.Add(this.IPTxtBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 195);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备参数";
            // 
            // DelayTxtBox
            // 
            this.DelayTxtBox.Location = new System.Drawing.Point(55, 131);
            this.DelayTxtBox.Name = "DelayTxtBox";
            this.DelayTxtBox.Size = new System.Drawing.Size(86, 21);
            this.DelayTxtBox.TabIndex = 1;
            // 
            // BaudrateTxtBox
            // 
            this.BaudrateTxtBox.Location = new System.Drawing.Point(277, 27);
            this.BaudrateTxtBox.Name = "BaudrateTxtBox";
            this.BaudrateTxtBox.ReadOnly = true;
            this.BaudrateTxtBox.Size = new System.Drawing.Size(42, 21);
            this.BaudrateTxtBox.TabIndex = 12;
            this.BaudrateTxtBox.Text = "9600";
            // 
            // ParityTxtBox
            // 
            this.ParityTxtBox.Location = new System.Drawing.Point(277, 79);
            this.ParityTxtBox.Name = "ParityTxtBox";
            this.ParityTxtBox.ReadOnly = true;
            this.ParityTxtBox.Size = new System.Drawing.Size(42, 21);
            this.ParityTxtBox.TabIndex = 11;
            this.ParityTxtBox.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "延时";
            // 
            // StopbitsTxtBox
            // 
            this.StopbitsTxtBox.Location = new System.Drawing.Point(182, 79);
            this.StopbitsTxtBox.Name = "StopbitsTxtBox";
            this.StopbitsTxtBox.ReadOnly = true;
            this.StopbitsTxtBox.Size = new System.Drawing.Size(42, 21);
            this.StopbitsTxtBox.TabIndex = 10;
            this.StopbitsTxtBox.Text = "1";
            // 
            // DatabitsTxtBox
            // 
            this.DatabitsTxtBox.Location = new System.Drawing.Point(53, 79);
            this.DatabitsTxtBox.Name = "DatabitsTxtBox";
            this.DatabitsTxtBox.ReadOnly = true;
            this.DatabitsTxtBox.Size = new System.Drawing.Size(42, 21);
            this.DatabitsTxtBox.TabIndex = 9;
            this.DatabitsTxtBox.Text = "8";
            // 
            // PortTxtBox
            // 
            this.PortTxtBox.Location = new System.Drawing.Point(182, 27);
            this.PortTxtBox.Name = "PortTxtBox";
            this.PortTxtBox.ReadOnly = true;
            this.PortTxtBox.Size = new System.Drawing.Size(42, 21);
            this.PortTxtBox.TabIndex = 7;
            // 
            // IPTxtBox
            // 
            this.IPTxtBox.Location = new System.Drawing.Point(53, 27);
            this.IPTxtBox.Name = "IPTxtBox";
            this.IPTxtBox.ReadOnly = true;
            this.IPTxtBox.Size = new System.Drawing.Size(88, 21);
            this.IPTxtBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "校验位";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(135, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "停止位";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "数据位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "波特率";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "端口";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // EquipmentSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 322);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EquipmentSettingForm";
            this.Text = "设备";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DelayTxtBox;
        private System.Windows.Forms.ComboBox EquipmentCmbBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox BaudrateTxtBox;
        private System.Windows.Forms.TextBox ParityTxtBox;
        private System.Windows.Forms.TextBox StopbitsTxtBox;
        private System.Windows.Forms.TextBox DatabitsTxtBox;
        private System.Windows.Forms.TextBox PortTxtBox;
        private System.Windows.Forms.TextBox IPTxtBox;
        private System.Windows.Forms.ComboBox SettingCmbBox;
        private System.Windows.Forms.Label label10;
    }
}