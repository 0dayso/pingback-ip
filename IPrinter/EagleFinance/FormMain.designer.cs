namespace EagleFinance
{
    partial class FormMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入TSLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入Eagle电子客票报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入Eagle中文版订单报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ABMS报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.a全自动导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备份BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还原RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.政策FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动填入政策ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnImportPolicy = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.错误信息窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同步Eagle数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dg = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.入库ToolStripMenuItem,
            this.导入ToolStripMenuItem,
            this.查询SToolStripMenuItem,
            this.删除DToolStripMenuItem,
            this.导出XToolStripMenuItem,
            this.备份BToolStripMenuItem,
            this.还原RToolStripMenuItem,
            this.政策FToolStripMenuItem,
            this.窗口WToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.menuMain.Size = new System.Drawing.Size(885, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "菜单";
            // 
            // 入库ToolStripMenuItem
            // 
            this.入库ToolStripMenuItem.Name = "入库ToolStripMenuItem";
            this.入库ToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.入库ToolStripMenuItem.Text = "入库(&I)";
            this.入库ToolStripMenuItem.Click += new System.EventHandler(this.入库ToolStripMenuItem_Click);
            // 
            // 导入ToolStripMenuItem
            // 
            this.导入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入TSLToolStripMenuItem,
            this.导入Eagle电子客票报表ToolStripMenuItem,
            this.导入Eagle中文版订单报表ToolStripMenuItem,
            this.导入ABMS报表ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.a全自动导入ToolStripMenuItem});
            this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            this.导入ToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.导入ToolStripMenuItem.Text = "导入(&M)";
            // 
            // 导入TSLToolStripMenuItem
            // 
            this.导入TSLToolStripMenuItem.Name = "导入TSLToolStripMenuItem";
            this.导入TSLToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.导入TSLToolStripMenuItem.Text = "(&1)导入TPR报表文件";
            this.导入TSLToolStripMenuItem.Click += new System.EventHandler(this.导入TSLToolStripMenuItem_Click);
            // 
            // 导入Eagle电子客票报表ToolStripMenuItem
            // 
            this.导入Eagle电子客票报表ToolStripMenuItem.Name = "导入Eagle电子客票报表ToolStripMenuItem";
            this.导入Eagle电子客票报表ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.导入Eagle电子客票报表ToolStripMenuItem.Text = "(&2)导入Eagle电子客票报表";
            this.导入Eagle电子客票报表ToolStripMenuItem.Click += new System.EventHandler(this.导入Eagle电子客票报表ToolStripMenuItem_Click);
            // 
            // 导入Eagle中文版订单报表ToolStripMenuItem
            // 
            this.导入Eagle中文版订单报表ToolStripMenuItem.Name = "导入Eagle中文版订单报表ToolStripMenuItem";
            this.导入Eagle中文版订单报表ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.导入Eagle中文版订单报表ToolStripMenuItem.Text = "(&3)导入Eagle中文版订单报表";
            this.导入Eagle中文版订单报表ToolStripMenuItem.Click += new System.EventHandler(this.导入Eagle中文版订单报表ToolStripMenuItem_Click);
            // 
            // 导入ABMS报表ToolStripMenuItem
            // 
            this.导入ABMS报表ToolStripMenuItem.Name = "导入ABMS报表ToolStripMenuItem";
            this.导入ABMS报表ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.导入ABMS报表ToolStripMenuItem.Text = "(&4)导入ABMS报表";
            this.导入ABMS报表ToolStripMenuItem.Click += new System.EventHandler(this.导入ABMS报表ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
            // 
            // a全自动导入ToolStripMenuItem
            // 
            this.a全自动导入ToolStripMenuItem.Name = "a全自动导入ToolStripMenuItem";
            this.a全自动导入ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.a全自动导入ToolStripMenuItem.Text = "(&A)全自动导入";
            this.a全自动导入ToolStripMenuItem.Click += new System.EventHandler(this.a全自动导入ToolStripMenuItem_Click);
            // 
            // 查询SToolStripMenuItem
            // 
            this.查询SToolStripMenuItem.Name = "查询SToolStripMenuItem";
            this.查询SToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.查询SToolStripMenuItem.Text = "查询(&S)";
            this.查询SToolStripMenuItem.Click += new System.EventHandler(this.查询SToolStripMenuItem_Click);
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // 导出XToolStripMenuItem
            // 
            this.导出XToolStripMenuItem.Name = "导出XToolStripMenuItem";
            this.导出XToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.导出XToolStripMenuItem.Text = "导出(&X)";
            this.导出XToolStripMenuItem.Click += new System.EventHandler(this.导出XToolStripMenuItem_Click);
            // 
            // 备份BToolStripMenuItem
            // 
            this.备份BToolStripMenuItem.Name = "备份BToolStripMenuItem";
            this.备份BToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.备份BToolStripMenuItem.Text = "备份(&B)";
            this.备份BToolStripMenuItem.Click += new System.EventHandler(this.备份BToolStripMenuItem_Click);
            // 
            // 还原RToolStripMenuItem
            // 
            this.还原RToolStripMenuItem.Name = "还原RToolStripMenuItem";
            this.还原RToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.还原RToolStripMenuItem.Text = "还原(&R)";
            this.还原RToolStripMenuItem.Click += new System.EventHandler(this.还原RToolStripMenuItem_Click);
            // 
            // 政策FToolStripMenuItem
            // 
            this.政策FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自动填入政策ToolStripMenuItem,
            this.mnImportPolicy});
            this.政策FToolStripMenuItem.Name = "政策FToolStripMenuItem";
            this.政策FToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.政策FToolStripMenuItem.Text = "政策(&P)";
            // 
            // 自动填入政策ToolStripMenuItem
            // 
            this.自动填入政策ToolStripMenuItem.Name = "自动填入政策ToolStripMenuItem";
            this.自动填入政策ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.自动填入政策ToolStripMenuItem.Text = "自动填入政策";
            this.自动填入政策ToolStripMenuItem.Click += new System.EventHandler(this.自动填入政策ToolStripMenuItem_Click);
            // 
            // mnImportPolicy
            // 
            this.mnImportPolicy.Name = "mnImportPolicy";
            this.mnImportPolicy.Size = new System.Drawing.Size(154, 22);
            this.mnImportPolicy.Text = "从航协帐单导入";
            this.mnImportPolicy.Click += new System.EventHandler(this.mnImportPolicy_Click);
            // 
            // 窗口WToolStripMenuItem
            // 
            this.窗口WToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.错误信息窗口ToolStripMenuItem,
            this.同步Eagle数据ToolStripMenuItem});
            this.窗口WToolStripMenuItem.Name = "窗口WToolStripMenuItem";
            this.窗口WToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.窗口WToolStripMenuItem.Text = "窗口(&W)";
            // 
            // 错误信息窗口ToolStripMenuItem
            // 
            this.错误信息窗口ToolStripMenuItem.Name = "错误信息窗口ToolStripMenuItem";
            this.错误信息窗口ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.错误信息窗口ToolStripMenuItem.Text = "错误信息窗口";
            this.错误信息窗口ToolStripMenuItem.Click += new System.EventHandler(this.错误信息窗口ToolStripMenuItem_Click);
            // 
            // 同步Eagle数据ToolStripMenuItem
            // 
            this.同步Eagle数据ToolStripMenuItem.Name = "同步Eagle数据ToolStripMenuItem";
            this.同步Eagle数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.同步Eagle数据ToolStripMenuItem.Text = "同步Eagle数据";
            this.同步Eagle数据ToolStripMenuItem.Click += new System.EventHandler(this.同步Eagle数据ToolStripMenuItem_Click);
            // 
            // dg
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg.Location = new System.Drawing.Point(0, 24);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 23;
            this.dg.Size = new System.Drawing.Size(885, 534);
            this.dg.TabIndex = 1;
            this.dg.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellValueChanged);
            this.dg.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dg_RowPostPaint);
            this.dg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dg_MouseUp);
            this.dg.Click += new System.EventHandler(this.dg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 537);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 558);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "易格票证管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem 入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入TSLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入Eagle电子客票报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询SToolStripMenuItem;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备份BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还原RToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 导入ABMS报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗口WToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 错误信息窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 同步Eagle数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 政策FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动填入政策ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入Eagle中文版订单报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem a全自动导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnImportPolicy;
    }
}

