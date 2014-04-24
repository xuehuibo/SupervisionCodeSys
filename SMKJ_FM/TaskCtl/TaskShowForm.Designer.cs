namespace TaskCtl
{
    partial class TaskShowForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TaskInfoPanel = new System.Windows.Forms.Panel();
            this.StopBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.DoneAmountLbl = new System.Windows.Forms.Label();
            this.TaskAmountLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ProductNameLbl = new System.Windows.Forms.Label();
            this.BatchNoLbl = new System.Windows.Forms.Label();
            this.TaskNameLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.WorkCenterPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.TaskInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.TaskInfoPanel);
            this.flowLayoutPanel1.Controls.Add(this.WorkCenterPanel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(714, 730);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // TaskInfoPanel
            // 
            this.TaskInfoPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TaskInfoPanel.Controls.Add(this.StopBtn);
            this.TaskInfoPanel.Controls.Add(this.PauseBtn);
            this.TaskInfoPanel.Controls.Add(this.DoneAmountLbl);
            this.TaskInfoPanel.Controls.Add(this.TaskAmountLbl);
            this.TaskInfoPanel.Controls.Add(this.label5);
            this.TaskInfoPanel.Controls.Add(this.label4);
            this.TaskInfoPanel.Controls.Add(this.ProductNameLbl);
            this.TaskInfoPanel.Controls.Add(this.BatchNoLbl);
            this.TaskInfoPanel.Controls.Add(this.TaskNameLbl);
            this.TaskInfoPanel.Controls.Add(this.label1);
            this.TaskInfoPanel.Controls.Add(this.label2);
            this.TaskInfoPanel.Controls.Add(this.label3);
            this.TaskInfoPanel.Location = new System.Drawing.Point(3, 3);
            this.TaskInfoPanel.Name = "TaskInfoPanel";
            this.TaskInfoPanel.Size = new System.Drawing.Size(700, 100);
            this.TaskInfoPanel.TabIndex = 0;
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.Color.Bisque;
            this.StopBtn.Location = new System.Drawing.Point(587, 9);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 53);
            this.StopBtn.TabIndex = 18;
            this.StopBtn.Text = "结束";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.BackColor = System.Drawing.Color.Bisque;
            this.PauseBtn.Location = new System.Drawing.Point(506, 9);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(75, 53);
            this.PauseBtn.TabIndex = 17;
            this.PauseBtn.Text = "暂停";
            this.PauseBtn.UseVisualStyleBackColor = false;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // DoneAmountLbl
            // 
            this.DoneAmountLbl.AutoSize = true;
            this.DoneAmountLbl.Location = new System.Drawing.Point(209, 46);
            this.DoneAmountLbl.Name = "DoneAmountLbl";
            this.DoneAmountLbl.Size = new System.Drawing.Size(41, 12);
            this.DoneAmountLbl.TabIndex = 15;
            this.DoneAmountLbl.Text = "label6";
            // 
            // TaskAmountLbl
            // 
            this.TaskAmountLbl.AutoSize = true;
            this.TaskAmountLbl.Location = new System.Drawing.Point(92, 46);
            this.TaskAmountLbl.Name = "TaskAmountLbl";
            this.TaskAmountLbl.Size = new System.Drawing.Size(41, 12);
            this.TaskAmountLbl.TabIndex = 14;
            this.TaskAmountLbl.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "完成量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "任务量";
            // 
            // ProductNameLbl
            // 
            this.ProductNameLbl.AutoSize = true;
            this.ProductNameLbl.Location = new System.Drawing.Point(361, 16);
            this.ProductNameLbl.Name = "ProductNameLbl";
            this.ProductNameLbl.Size = new System.Drawing.Size(41, 12);
            this.ProductNameLbl.TabIndex = 11;
            this.ProductNameLbl.Text = "label6";
            // 
            // BatchNoLbl
            // 
            this.BatchNoLbl.AutoSize = true;
            this.BatchNoLbl.Location = new System.Drawing.Point(209, 16);
            this.BatchNoLbl.Name = "BatchNoLbl";
            this.BatchNoLbl.Size = new System.Drawing.Size(41, 12);
            this.BatchNoLbl.TabIndex = 10;
            this.BatchNoLbl.Text = "label6";
            // 
            // TaskNameLbl
            // 
            this.TaskNameLbl.AutoSize = true;
            this.TaskNameLbl.Location = new System.Drawing.Point(92, 16);
            this.TaskNameLbl.Name = "TaskNameLbl";
            this.TaskNameLbl.Size = new System.Drawing.Size(41, 12);
            this.TaskNameLbl.TabIndex = 9;
            this.TaskNameLbl.Text = "label6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "任务编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "批号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "产品名称";
            // 
            // WorkCenterPanel
            // 
            this.WorkCenterPanel.AutoSize = true;
            this.WorkCenterPanel.Location = new System.Drawing.Point(3, 109);
            this.WorkCenterPanel.Name = "WorkCenterPanel";
            this.WorkCenterPanel.Size = new System.Drawing.Size(0, 0);
            this.WorkCenterPanel.TabIndex = 1;
            // 
            // TaskShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 730);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskShowForm";
            this.Text = "任务窗口";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.TaskInfoPanel.ResumeLayout(false);
            this.TaskInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel WorkCenterPanel;
        private System.Windows.Forms.Panel TaskInfoPanel;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Label DoneAmountLbl;
        private System.Windows.Forms.Label TaskAmountLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ProductNameLbl;
        private System.Windows.Forms.Label BatchNoLbl;
        private System.Windows.Forms.Label TaskNameLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StopBtn;
    }
}