namespace EagleFinance.Forms
{
    partial class AutoImport
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
            this.pTprReport = new System.Windows.Forms.Panel();
            this.btnResetTpr = new System.Windows.Forms.Button();
            this.btnTprStart = new System.Windows.Forms.Button();
            this.dtpTpr = new System.Windows.Forms.DateTimePicker();
            this.lvPlan = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.lbOfficeVisable = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pExcel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnXlsStart = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkXlsPnrReport = new System.Windows.Forms.CheckBox();
            this.checkXls = new System.Windows.Forms.CheckBox();
            this.checkTpr = new System.Windows.Forms.CheckBox();
            this.btnAllStart = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbInfomation = new System.Windows.Forms.RichTextBox();
            this.btnAutoImcoming = new System.Windows.Forms.Button();
            this.pTprReport.SuspendLayout();
            this.pExcel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTprReport
            // 
            this.pTprReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTprReport.Controls.Add(this.btnResetTpr);
            this.pTprReport.Controls.Add(this.btnAutoImcoming);
            this.pTprReport.Controls.Add(this.btnTprStart);
            this.pTprReport.Controls.Add(this.dtpTpr);
            this.pTprReport.Controls.Add(this.lvPlan);
            this.pTprReport.Controls.Add(this.lbOfficeVisable);
            this.pTprReport.Controls.Add(this.label3);
            this.pTprReport.Controls.Add(this.label1);
            this.pTprReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.pTprReport.Location = new System.Drawing.Point(0, 0);
            this.pTprReport.Name = "pTprReport";
            this.pTprReport.Size = new System.Drawing.Size(388, 268);
            this.pTprReport.TabIndex = 0;
            // 
            // btnResetTpr
            // 
            this.btnResetTpr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetTpr.Location = new System.Drawing.Point(227, 242);
            this.btnResetTpr.Name = "btnResetTpr";
            this.btnResetTpr.Size = new System.Drawing.Size(75, 23);
            this.btnResetTpr.TabIndex = 4;
            this.btnResetTpr.Text = "重置状态";
            this.btnResetTpr.UseVisualStyleBackColor = true;
            this.btnResetTpr.Click += new System.EventHandler(this.btnResetTpr_Click);
            // 
            // btnTprStart
            // 
            this.btnTprStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTprStart.Location = new System.Drawing.Point(308, 242);
            this.btnTprStart.Name = "btnTprStart";
            this.btnTprStart.Size = new System.Drawing.Size(75, 23);
            this.btnTprStart.TabIndex = 4;
            this.btnTprStart.Text = "启动TPR";
            this.btnTprStart.UseVisualStyleBackColor = true;
            this.btnTprStart.Click += new System.EventHandler(this.btnTprStart_Click);
            // 
            // dtpTpr
            // 
            this.dtpTpr.Location = new System.Drawing.Point(77, -1);
            this.dtpTpr.Name = "dtpTpr";
            this.dtpTpr.Size = new System.Drawing.Size(114, 21);
            this.dtpTpr.TabIndex = 3;
            // 
            // lvPlan
            // 
            this.lvPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvPlan.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvPlan.FullRowSelect = true;
            this.lvPlan.GridLines = true;
            this.lvPlan.Location = new System.Drawing.Point(78, 21);
            this.lvPlan.Name = "lvPlan";
            this.lvPlan.Size = new System.Drawing.Size(307, 220);
            this.lvPlan.TabIndex = 2;
            this.lvPlan.UseCompatibleStateImageBehavior = false;
            this.lvPlan.View = System.Windows.Forms.View.Details;
            this.lvPlan.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPlan_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Office";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "配置编号";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tpr指令";
            this.columnHeader3.Width = 132;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "处理";
            this.columnHeader4.Width = 50;
            // 
            // lbOfficeVisable
            // 
            this.lbOfficeVisable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbOfficeVisable.FormattingEnabled = true;
            this.lbOfficeVisable.ItemHeight = 12;
            this.lbOfficeVisable.Location = new System.Drawing.Point(2, 21);
            this.lbOfficeVisable.Name = "lbOfficeVisable";
            this.lbOfficeVisable.Size = new System.Drawing.Size(74, 220);
            this.lbOfficeVisable.TabIndex = 1;
            this.lbOfficeVisable.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbOfficeVisable_MouseDoubleClick);
            this.lbOfficeVisable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbOfficeVisable_MouseUp);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "注意:请确定已入库对应的OFFICE票号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tpr选项设置";
            // 
            // pExcel
            // 
            this.pExcel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pExcel.Controls.Add(this.textBox1);
            this.pExcel.Controls.Add(this.btnXlsStart);
            this.pExcel.Controls.Add(this.label6);
            this.pExcel.Controls.Add(this.label5);
            this.pExcel.Controls.Add(this.label4);
            this.pExcel.Controls.Add(this.label2);
            this.pExcel.Controls.Add(this.dateTimePicker2);
            this.pExcel.Controls.Add(this.dateTimePicker1);
            this.pExcel.Location = new System.Drawing.Point(390, 0);
            this.pExcel.Name = "pExcel";
            this.pExcel.Size = new System.Drawing.Size(198, 137);
            this.pExcel.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(5, 78);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "C:\\";
            // 
            // btnXlsStart
            // 
            this.btnXlsStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXlsStart.Location = new System.Drawing.Point(61, 106);
            this.btnXlsStart.Name = "btnXlsStart";
            this.btnXlsStart.Size = new System.Drawing.Size(81, 23);
            this.btnXlsStart.TabIndex = 4;
            this.btnXlsStart.Text = "启动";
            this.btnXlsStart.UseVisualStyleBackColor = true;
            this.btnXlsStart.Click += new System.EventHandler(this.btnXlsStart_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "XLS存储位置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "终止日期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "起始日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "易格报表设置(电子客票、PNR订单)";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(79, 37);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(114, 21);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(79, 17);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(114, 21);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pTprReport);
            this.panel1.Controls.Add(this.pExcel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 270);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkXlsPnrReport);
            this.panel3.Controls.Add(this.checkXls);
            this.panel3.Controls.Add(this.checkTpr);
            this.panel3.Controls.Add(this.btnAllStart);
            this.panel3.Location = new System.Drawing.Point(390, 144);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(198, 124);
            this.panel3.TabIndex = 5;
            // 
            // checkXlsPnrReport
            // 
            this.checkXlsPnrReport.AutoSize = true;
            this.checkXlsPnrReport.Enabled = false;
            this.checkXlsPnrReport.Location = new System.Drawing.Point(51, 47);
            this.checkXlsPnrReport.Name = "checkXlsPnrReport";
            this.checkXlsPnrReport.Size = new System.Drawing.Size(126, 16);
            this.checkXlsPnrReport.TabIndex = 5;
            this.checkXlsPnrReport.Text = "易格报表(PNR订单)";
            this.checkXlsPnrReport.UseVisualStyleBackColor = true;
            // 
            // checkXls
            // 
            this.checkXls.AutoSize = true;
            this.checkXls.Location = new System.Drawing.Point(51, 25);
            this.checkXls.Name = "checkXls";
            this.checkXls.Size = new System.Drawing.Size(132, 16);
            this.checkXls.TabIndex = 5;
            this.checkXls.Text = "易格报表(电子客票)";
            this.checkXls.UseVisualStyleBackColor = true;
            // 
            // checkTpr
            // 
            this.checkTpr.AutoSize = true;
            this.checkTpr.Location = new System.Drawing.Point(51, 3);
            this.checkTpr.Name = "checkTpr";
            this.checkTpr.Size = new System.Drawing.Size(66, 16);
            this.checkTpr.TabIndex = 5;
            this.checkTpr.Text = "Tpr报表";
            this.checkTpr.UseVisualStyleBackColor = true;
            // 
            // btnAllStart
            // 
            this.btnAllStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllStart.Location = new System.Drawing.Point(24, 69);
            this.btnAllStart.Name = "btnAllStart";
            this.btnAllStart.Size = new System.Drawing.Size(147, 55);
            this.btnAllStart.TabIndex = 4;
            this.btnAllStart.Text = "全部启动";
            this.btnAllStart.UseVisualStyleBackColor = true;
            this.btnAllStart.Click += new System.EventHandler(this.btnAllStart_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbInfomation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 272);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 194);
            this.panel2.TabIndex = 2;
            // 
            // rtbInfomation
            // 
            this.rtbInfomation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbInfomation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfomation.Location = new System.Drawing.Point(0, 0);
            this.rtbInfomation.Name = "rtbInfomation";
            this.rtbInfomation.Size = new System.Drawing.Size(590, 194);
            this.rtbInfomation.TabIndex = 0;
            this.rtbInfomation.Text = "";
            // 
            // btnAutoImcoming
            // 
            this.btnAutoImcoming.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoImcoming.Location = new System.Drawing.Point(197, -1);
            this.btnAutoImcoming.Name = "btnAutoImcoming";
            this.btnAutoImcoming.Size = new System.Drawing.Size(188, 22);
            this.btnAutoImcoming.TabIndex = 4;
            this.btnAutoImcoming.Text = "自动入库";
            this.btnAutoImcoming.UseVisualStyleBackColor = true;
            this.btnAutoImcoming.Click += new System.EventHandler(this.btnAutoImcoming_Click);
            // 
            // AutoImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 466);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AutoImport";
            this.Text = "自动导入";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoImport_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoImport_FormClosing);
            this.Load += new System.EventHandler(this.AutoImport_Load);
            this.pTprReport.ResumeLayout(false);
            this.pTprReport.PerformLayout();
            this.pExcel.ResumeLayout(false);
            this.pExcel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTprReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvPlan;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListBox lbOfficeVisable;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbInfomation;
        private System.Windows.Forms.DateTimePicker dtpTpr;
        private System.Windows.Forms.Button btnTprStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResetTpr;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnXlsStart;
        private System.Windows.Forms.Button btnAllStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkXls;
        private System.Windows.Forms.CheckBox checkTpr;
        private System.Windows.Forms.CheckBox checkXlsPnrReport;
        private System.Windows.Forms.Button btnAutoImcoming;
    }
}