namespace WorkCenterCtl
{
    partial class WorkCenterInfo
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CurrentNumLbl = new System.Windows.Forms.Label();
            this.finishedNumLbl = new System.Windows.Forms.Label();
            this.WorkCenterNameTxt = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.LevelNoLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PackageNumLbl = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "已扫描:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "已完成:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(8, 100);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(430, 69);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "工位信息";
            // 
            // CurrentNumLbl
            // 
            this.CurrentNumLbl.AutoSize = true;
            this.CurrentNumLbl.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentNumLbl.Location = new System.Drawing.Point(69, 49);
            this.CurrentNumLbl.Name = "CurrentNumLbl";
            this.CurrentNumLbl.Size = new System.Drawing.Size(33, 35);
            this.CurrentNumLbl.TabIndex = 3;
            this.CurrentNumLbl.Text = "2";
            // 
            // finishedNumLbl
            // 
            this.finishedNumLbl.AutoSize = true;
            this.finishedNumLbl.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.finishedNumLbl.Location = new System.Drawing.Point(199, 49);
            this.finishedNumLbl.Name = "finishedNumLbl";
            this.finishedNumLbl.Size = new System.Drawing.Size(33, 35);
            this.finishedNumLbl.TabIndex = 4;
            this.finishedNumLbl.Text = "1";
            // 
            // WorkCenterNameTxt
            // 
            this.WorkCenterNameTxt.AutoSize = true;
            this.WorkCenterNameTxt.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WorkCenterNameTxt.Location = new System.Drawing.Point(109, 2);
            this.WorkCenterNameTxt.Name = "WorkCenterNameTxt";
            this.WorkCenterNameTxt.Size = new System.Drawing.Size(100, 29);
            this.WorkCenterNameTxt.TabIndex = 5;
            this.WorkCenterNameTxt.Text = "工位一";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(500, 13);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(188, 144);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "零箱";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(89, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "拼箱";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 59);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 50);
            this.button3.TabIndex = 2;
            this.button3.Text = "散装";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // LevelNoLbl
            // 
            this.LevelNoLbl.AutoSize = true;
            this.LevelNoLbl.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LevelNoLbl.Location = new System.Drawing.Point(3, 2);
            this.LevelNoLbl.Name = "LevelNoLbl";
            this.LevelNoLbl.Size = new System.Drawing.Size(58, 29);
            this.LevelNoLbl.TabIndex = 9;
            this.LevelNoLbl.Text = "LV1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "已包装";
            // 
            // PackageNumLbl
            // 
            this.PackageNumLbl.AutoSize = true;
            this.PackageNumLbl.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PackageNumLbl.Location = new System.Drawing.Point(321, 49);
            this.PackageNumLbl.Name = "PackageNumLbl";
            this.PackageNumLbl.Size = new System.Drawing.Size(33, 35);
            this.PackageNumLbl.TabIndex = 11;
            this.PackageNumLbl.Text = "1";
            // 
            // WorkCenterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Controls.Add(this.PackageNumLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LevelNoLbl);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.WorkCenterNameTxt);
            this.Controls.Add(this.finishedNumLbl);
            this.Controls.Add(this.CurrentNumLbl);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WorkCenterInfo";
            this.Size = new System.Drawing.Size(700, 185);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label CurrentNumLbl;
        private System.Windows.Forms.Label finishedNumLbl;
        private System.Windows.Forms.Label WorkCenterNameTxt;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label LevelNoLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PackageNumLbl;
    }
}
