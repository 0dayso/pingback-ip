namespace EagleForms
{
    partial class Primary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Primary));
            this.scrollNotice1 = new EagleControls.ScrollNotice();
            this.pMenu = new System.Windows.Forms.Panel();
            this.mainMenu = new EagleControls.MainMenu();
            this.pControlBox = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.pToolBar = new System.Windows.Forms.Panel();
            this.pNotice = new System.Windows.Forms.Panel();
            this.pMain = new System.Windows.Forms.Panel();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpBlack = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tpManager = new System.Windows.Forms.TabPage();
            this.tpFinance = new System.Windows.Forms.TabPage();
            this.tpEasy = new System.Windows.Forms.TabPage();
            this.tpReceipt = new System.Windows.Forms.TabPage();
            this.pRight1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pRight2 = new System.Windows.Forms.Panel();
            this.pRight3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pRight4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pStatusBar = new System.Windows.Forms.Panel();
            this.pMenu.SuspendLayout();
            this.pControlBox.SuspendLayout();
            this.pNotice.SuspendLayout();
            this.pMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpBlack.SuspendLayout();
            this.pRight1.SuspendLayout();
            this.pRight3.SuspendLayout();
            this.pRight4.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollNotice1
            // 
            this.scrollNotice1.BackColor = System.Drawing.Color.Black;
            this.scrollNotice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollNotice1.ForeColor = System.Drawing.Color.Lime;
            this.scrollNotice1.Location = new System.Drawing.Point(0, 0);
            this.scrollNotice1.Name = "scrollNotice1";
            this.scrollNotice1.Size = new System.Drawing.Size(1021, 18);
            this.scrollNotice1.TabIndex = 0;
            this.scrollNotice1.Tag = "9999";
            this.scrollNotice1.Text = resources.GetString("scrollNotice1.Text");
            this.scrollNotice1.UPDATEINTERVAL = 15;
            // 
            // pMenu
            // 
            this.pMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMenu.Controls.Add(this.mainMenu);
            this.pMenu.Location = new System.Drawing.Point(0, 0);
            this.pMenu.Name = "pMenu";
            this.pMenu.Size = new System.Drawing.Size(975, 24);
            this.pMenu.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(973, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu1";
            this.mainMenu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainMenu_MouseDoubleClick);
            this.mainMenu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainMenu_MouseMove);
            this.mainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mainMenu_ItemClicked);
            // 
            // pControlBox
            // 
            this.pControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pControlBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pControlBox.Controls.Add(this.btnExit);
            this.pControlBox.Controls.Add(this.btnMinimize);
            this.pControlBox.Location = new System.Drawing.Point(973, 0);
            this.pControlBox.Name = "pControlBox";
            this.pControlBox.Size = new System.Drawing.Size(50, 24);
            this.pControlBox.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Blue;
            this.btnExit.Location = new System.Drawing.Point(27, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(20, 20);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "x";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Blue;
            this.btnMinimize.Location = new System.Drawing.Point(2, 1);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 20);
            this.btnMinimize.TabIndex = 0;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // pToolBar
            // 
            this.pToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pToolBar.Location = new System.Drawing.Point(0, 22);
            this.pToolBar.Name = "pToolBar";
            this.pToolBar.Size = new System.Drawing.Size(1023, 35);
            this.pToolBar.TabIndex = 2;
            this.pToolBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainMenu_MouseMove);
            // 
            // pNotice
            // 
            this.pNotice.BackColor = System.Drawing.SystemColors.ControlText;
            this.pNotice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNotice.Controls.Add(this.scrollNotice1);
            this.pNotice.Location = new System.Drawing.Point(0, 56);
            this.pNotice.Name = "pNotice";
            this.pNotice.Size = new System.Drawing.Size(1023, 20);
            this.pNotice.TabIndex = 2;
            // 
            // pMain
            // 
            this.pMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMain.Controls.Add(this.tcMain);
            this.pMain.Location = new System.Drawing.Point(0, 75);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(768, 674);
            this.pMain.TabIndex = 3;
            // 
            // tcMain
            // 
            this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tcMain.Controls.Add(this.tpBlack);
            this.tcMain.Controls.Add(this.tpManager);
            this.tcMain.Controls.Add(this.tpFinance);
            this.tcMain.Controls.Add(this.tpEasy);
            this.tcMain.Controls.Add(this.tpReceipt);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(0);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(3, 1);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(766, 672);
            this.tcMain.TabIndex = 0;
            this.tcMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainMenu_MouseMove);
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpBlack
            // 
            this.tpBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpBlack.Controls.Add(this.label4);
            this.tpBlack.Location = new System.Drawing.Point(20, 4);
            this.tpBlack.Margin = new System.Windows.Forms.Padding(0);
            this.tpBlack.Name = "tpBlack";
            this.tpBlack.Size = new System.Drawing.Size(742, 664);
            this.tpBlack.TabIndex = 0;
            this.tpBlack.Tag = "";
            this.tpBlack.Text = "黑屏";
            this.tpBlack.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "欢迎使用易格航空订票软件2.0系统";
            // 
            // tpManager
            // 
            this.tpManager.Location = new System.Drawing.Point(20, 4);
            this.tpManager.Name = "tpManager";
            this.tpManager.Size = new System.Drawing.Size(742, 664);
            this.tpManager.TabIndex = 2;
            this.tpManager.Text = "后台管理";
            this.tpManager.UseVisualStyleBackColor = true;
            // 
            // tpFinance
            // 
            this.tpFinance.Location = new System.Drawing.Point(20, 4);
            this.tpFinance.Name = "tpFinance";
            this.tpFinance.Size = new System.Drawing.Size(742, 664);
            this.tpFinance.TabIndex = 3;
            this.tpFinance.Text = "对帐平台";
            this.tpFinance.UseVisualStyleBackColor = true;
            // 
            // tpEasy
            // 
            this.tpEasy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpEasy.Location = new System.Drawing.Point(20, 4);
            this.tpEasy.Margin = new System.Windows.Forms.Padding(0);
            this.tpEasy.Name = "tpEasy";
            this.tpEasy.Size = new System.Drawing.Size(742, 664);
            this.tpEasy.TabIndex = 1;
            this.tpEasy.Text = "中文版";
            this.tpEasy.UseVisualStyleBackColor = true;
            // 
            // tpReceipt
            // 
            this.tpReceipt.Location = new System.Drawing.Point(20, 4);
            this.tpReceipt.Name = "tpReceipt";
            this.tpReceipt.Size = new System.Drawing.Size(742, 664);
            this.tpReceipt.TabIndex = 4;
            this.tpReceipt.Text = "行程单";
            this.tpReceipt.UseVisualStyleBackColor = true;
            // 
            // pRight1
            // 
            this.pRight1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRight1.Controls.Add(this.panel1);
            this.pRight1.Controls.Add(this.label1);
            this.pRight1.Location = new System.Drawing.Point(768, 84);
            this.pRight1.Name = "pRight1";
            this.pRight1.Size = new System.Drawing.Size(255, 100);
            this.pRight1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 82);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "最低价与返点";
            // 
            // pRight2
            // 
            this.pRight2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pRight2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRight2.Location = new System.Drawing.Point(768, 184);
            this.pRight2.Name = "pRight2";
            this.pRight2.Size = new System.Drawing.Size(255, 100);
            this.pRight2.TabIndex = 4;
            // 
            // pRight3
            // 
            this.pRight3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pRight3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRight3.Controls.Add(this.panel2);
            this.pRight3.Controls.Add(this.label2);
            this.pRight3.Location = new System.Drawing.Point(768, 284);
            this.pRight3.Name = "pRight3";
            this.pRight3.Size = new System.Drawing.Size(255, 106);
            this.pRight3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 87);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "固定舱位申请";
            // 
            // pRight4
            // 
            this.pRight4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pRight4.Controls.Add(this.panel3);
            this.pRight4.Controls.Add(this.label3);
            this.pRight4.Location = new System.Drawing.Point(768, 643);
            this.pRight4.Name = "pRight4";
            this.pRight4.Size = new System.Drawing.Size(255, 106);
            this.pRight4.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(253, 87);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "浮动舱位申请";
            // 
            // pStatusBar
            // 
            this.pStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pStatusBar.Location = new System.Drawing.Point(0, 748);
            this.pStatusBar.Name = "pStatusBar";
            this.pStatusBar.Size = new System.Drawing.Size(1024, 20);
            this.pStatusBar.TabIndex = 2;
            this.pStatusBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainMenu_MouseMove);
            // 
            // Primary
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pRight4);
            this.Controls.Add(this.pRight3);
            this.Controls.Add(this.pRight2);
            this.Controls.Add(this.pRight1);
            this.Controls.Add(this.pStatusBar);
            this.Controls.Add(this.pNotice);
            this.Controls.Add(this.pControlBox);
            this.Controls.Add(this.pMenu);
            this.Controls.Add(this.pToolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Primary";
            this.Text = "Primary";
            this.SizeChanged += new System.EventHandler(this.Primary_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Primary_FormClosing);
            this.Load += new System.EventHandler(this.Primary_Load);
            this.pMenu.ResumeLayout(false);
            this.pMenu.PerformLayout();
            this.pControlBox.ResumeLayout(false);
            this.pNotice.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpBlack.ResumeLayout(false);
            this.tpBlack.PerformLayout();
            this.pRight1.ResumeLayout(false);
            this.pRight1.PerformLayout();
            this.pRight3.ResumeLayout(false);
            this.pRight3.PerformLayout();
            this.pRight4.ResumeLayout(false);
            this.pRight4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pMenu;
        private System.Windows.Forms.Panel pControlBox;
        private System.Windows.Forms.Panel pToolBar;
        private System.Windows.Forms.Panel pNotice;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pRight1;
        private System.Windows.Forms.Panel pRight2;
        private System.Windows.Forms.Panel pRight3;
        private System.Windows.Forms.Panel pRight4;
        private System.Windows.Forms.Panel pStatusBar;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpBlack;
        private System.Windows.Forms.TabPage tpEasy;
        private EagleControls.MainMenu mainMenu;
        private System.Windows.Forms.TabPage tpManager;
        private System.Windows.Forms.TabPage tpFinance;
        private EagleControls.ScrollNotice scrollNotice1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpReceipt;
    }
}