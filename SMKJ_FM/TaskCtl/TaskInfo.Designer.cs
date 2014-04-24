namespace TaskCtl
{
    partial class TaskInfo
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProductNameLbl = new System.Windows.Forms.Label();
            this.BatchNoLbl = new System.Windows.Forms.Label();
            this.TaskNameLbl = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DoneAmountLbl = new System.Windows.Forms.Label();
            this.TaskAmountLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "批号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "产品名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "任务量";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 117);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ProductNameLbl);
            this.panel1.Controls.Add(this.BatchNoLbl);
            this.panel1.Controls.Add(this.TaskNameLbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(694, 29);
            this.panel1.TabIndex = 0;
            // 
            // ProductNameLbl
            // 
            this.ProductNameLbl.AutoSize = true;
            this.ProductNameLbl.Location = new System.Drawing.Point(340, 8);
            this.ProductNameLbl.Name = "ProductNameLbl";
            this.ProductNameLbl.Size = new System.Drawing.Size(41, 12);
            this.ProductNameLbl.TabIndex = 5;
            this.ProductNameLbl.Text = "label6";
            // 
            // BatchNoLbl
            // 
            this.BatchNoLbl.AutoSize = true;
            this.BatchNoLbl.Location = new System.Drawing.Point(186, 8);
            this.BatchNoLbl.Name = "BatchNoLbl";
            this.BatchNoLbl.Size = new System.Drawing.Size(41, 12);
            this.BatchNoLbl.TabIndex = 4;
            this.BatchNoLbl.Text = "label6";
            // 
            // TaskNameLbl
            // 
            this.TaskNameLbl.AutoSize = true;
            this.TaskNameLbl.Location = new System.Drawing.Point(62, 8);
            this.TaskNameLbl.Name = "TaskNameLbl";
            this.TaskNameLbl.Size = new System.Drawing.Size(41, 12);
            this.TaskNameLbl.TabIndex = 3;
            this.TaskNameLbl.Text = "label6";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.StopBtn);
            this.flowLayoutPanel2.Controls.Add(this.StartBtn);
            this.flowLayoutPanel2.Controls.Add(this.panel2);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 38);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(694, 76);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(616, 3);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 53);
            this.StopBtn.TabIndex = 2;
            this.StopBtn.Text = "结束";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(535, 3);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 53);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "开始";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.StatusLbl);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.DoneAmountLbl);
            this.panel2.Controls.Add(this.TaskAmountLbl);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(524, 73);
            this.panel2.TabIndex = 4;
            // 
            // StatusLbl
            // 
            this.StatusLbl.AutoSize = true;
            this.StatusLbl.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusLbl.Location = new System.Drawing.Point(273, 15);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(103, 29);
            this.StatusLbl.TabIndex = 8;
            this.StatusLbl.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "任务状态";
            // 
            // DoneAmountLbl
            // 
            this.DoneAmountLbl.AutoSize = true;
            this.DoneAmountLbl.Location = new System.Drawing.Point(85, 41);
            this.DoneAmountLbl.Name = "DoneAmountLbl";
            this.DoneAmountLbl.Size = new System.Drawing.Size(41, 12);
            this.DoneAmountLbl.TabIndex = 6;
            this.DoneAmountLbl.Text = "label6";
            // 
            // TaskAmountLbl
            // 
            this.TaskAmountLbl.AutoSize = true;
            this.TaskAmountLbl.Location = new System.Drawing.Point(85, 10);
            this.TaskAmountLbl.Name = "TaskAmountLbl";
            this.TaskAmountLbl.Size = new System.Drawing.Size(41, 12);
            this.TaskAmountLbl.TabIndex = 5;
            this.TaskAmountLbl.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "完成量";
            // 
            // TaskInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TaskInfo";
            this.Size = new System.Drawing.Size(700, 117);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label TaskNameLbl;
        private System.Windows.Forms.Label BatchNoLbl;
        private System.Windows.Forms.Label ProductNameLbl;
        private System.Windows.Forms.Label TaskAmountLbl;
        private System.Windows.Forms.Label DoneAmountLbl;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.Label label6;
    }
}
