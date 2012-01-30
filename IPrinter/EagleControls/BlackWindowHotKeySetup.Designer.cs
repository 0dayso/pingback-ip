namespace EagleControls
{
    partial class BlackWindowHotKeySetup
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelShortCommand = new System.Windows.Forms.Button();
            this.btnAddShortCommand = new System.Windows.Forms.Button();
            this.lv1 = new SortListView ();
            this.chHotKey1 = new System.Windows.Forms.ColumnHeader();
            this.chDestCommand = new System.Windows.Forms.ColumnHeader();
            this.txtHotKey1 = new System.Windows.Forms.TextBox();
            this.txtDestCommand = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelShortFunction = new System.Windows.Forms.Button();
            this.btnAddShortFunction = new System.Windows.Forms.Button();
            this.txtHotKey3 = new System.Windows.Forms.TextBox();
            this.txtHotKey2 = new System.Windows.Forms.TextBox();
            this.lv2 = new SortListView ();
            this.chShortCut = new System.Windows.Forms.ColumnHeader();
            this.chShortCommand = new System.Windows.Forms.ColumnHeader();
            this.chFunction = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDelShortCommand);
            this.panel1.Controls.Add(this.btnAddShortCommand);
            this.panel1.Controls.Add(this.lv1);
            this.panel1.Controls.Add(this.txtHotKey1);
            this.panel1.Controls.Add(this.txtDestCommand);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 531);
            this.panel1.TabIndex = 0;
            // 
            // btnDelShortCommand
            // 
            this.btnDelShortCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelShortCommand.Location = new System.Drawing.Point(198, 58);
            this.btnDelShortCommand.Name = "btnDelShortCommand";
            this.btnDelShortCommand.Size = new System.Drawing.Size(55, 21);
            this.btnDelShortCommand.TabIndex = 3;
            this.btnDelShortCommand.Text = "删除";
            this.btnDelShortCommand.UseVisualStyleBackColor = true;
            this.btnDelShortCommand.Click += new System.EventHandler(this.btnDelShortCommand_Click);
            // 
            // btnAddShortCommand
            // 
            this.btnAddShortCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddShortCommand.Location = new System.Drawing.Point(141, 58);
            this.btnAddShortCommand.Name = "btnAddShortCommand";
            this.btnAddShortCommand.Size = new System.Drawing.Size(55, 21);
            this.btnAddShortCommand.TabIndex = 3;
            this.btnAddShortCommand.Text = "增加";
            this.btnAddShortCommand.UseVisualStyleBackColor = true;
            this.btnAddShortCommand.Click += new System.EventHandler(this.btnAddShortCommand_Click);
            // 
            // lv1
            // 
            this.lv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chHotKey1,
            this.chDestCommand});
            this.lv1.FullRowSelect = true;
            this.lv1.GridLines = true;
            this.lv1.HideSelection = false;
            this.lv1.Location = new System.Drawing.Point(0, 79);
            this.lv1.Name = "lv1";
            this.lv1.Size = new System.Drawing.Size(253, 450);
            this.lv1.TabIndex = 2;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.View = System.Windows.Forms.View.Details;
            this.lv1.SelectedIndexChanged += new System.EventHandler(this.lv1_SelectedIndexChanged);
            // 
            // chHotKey1
            // 
            this.chHotKey1.Text = "快键";
            // 
            // chDestCommand
            // 
            this.chDestCommand.Text = "目标文字";
            this.chDestCommand.Width = 192;
            // 
            // txtHotKey1
            // 
            this.txtHotKey1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHotKey1.Location = new System.Drawing.Point(0, 58);
            this.txtHotKey1.Name = "txtHotKey1";
            this.txtHotKey1.ReadOnly = true;
            this.txtHotKey1.ShortcutsEnabled = false;
            this.txtHotKey1.Size = new System.Drawing.Size(139, 21);
            this.txtHotKey1.TabIndex = 1;
            this.txtHotKey1.Text = "按下指定的快键";
            this.txtHotKey1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotKey1_KeyDown);
            // 
            // txtDestCommand
            // 
            this.txtDestCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDestCommand.Location = new System.Drawing.Point(0, 0);
            this.txtDestCommand.Multiline = true;
            this.txtDestCommand.Name = "txtDestCommand";
            this.txtDestCommand.Size = new System.Drawing.Size(253, 58);
            this.txtDestCommand.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDelShortFunction);
            this.panel2.Controls.Add(this.btnAddShortFunction);
            this.panel2.Controls.Add(this.txtHotKey3);
            this.panel2.Controls.Add(this.txtHotKey2);
            this.panel2.Controls.Add(this.lv2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(256, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(314, 531);
            this.panel2.TabIndex = 1;
            // 
            // btnDelShortFunction
            // 
            this.btnDelShortFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelShortFunction.Location = new System.Drawing.Point(256, 0);
            this.btnDelShortFunction.Name = "btnDelShortFunction";
            this.btnDelShortFunction.Size = new System.Drawing.Size(55, 21);
            this.btnDelShortFunction.TabIndex = 5;
            this.btnDelShortFunction.Text = "删除";
            this.btnDelShortFunction.UseVisualStyleBackColor = true;
            this.btnDelShortFunction.Click += new System.EventHandler(this.btnDelShortFunction_Click);
            // 
            // btnAddShortFunction
            // 
            this.btnAddShortFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddShortFunction.Location = new System.Drawing.Point(199, 0);
            this.btnAddShortFunction.Name = "btnAddShortFunction";
            this.btnAddShortFunction.Size = new System.Drawing.Size(55, 21);
            this.btnAddShortFunction.TabIndex = 6;
            this.btnAddShortFunction.Text = "增加";
            this.btnAddShortFunction.UseVisualStyleBackColor = true;
            this.btnAddShortFunction.Click += new System.EventHandler(this.btnAddShortFunction_Click);
            // 
            // txtHotKey3
            // 
            this.txtHotKey3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHotKey3.Location = new System.Drawing.Point(104, 0);
            this.txtHotKey3.Name = "txtHotKey3";
            this.txtHotKey3.Size = new System.Drawing.Size(93, 21);
            this.txtHotKey3.TabIndex = 4;
            this.txtHotKey3.Text = "输入指令";
            this.txtHotKey3.Enter += new System.EventHandler(this.txtHotKey3_Enter);
            // 
            // txtHotKey2
            // 
            this.txtHotKey2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHotKey2.Location = new System.Drawing.Point(0, 0);
            this.txtHotKey2.Name = "txtHotKey2";
            this.txtHotKey2.ReadOnly = true;
            this.txtHotKey2.ShortcutsEnabled = false;
            this.txtHotKey2.Size = new System.Drawing.Size(102, 21);
            this.txtHotKey2.TabIndex = 4;
            this.txtHotKey2.Text = "按下指定的快键";
            this.txtHotKey2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotKey1_KeyDown);
            // 
            // lv2
            // 
            this.lv2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lv2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chShortCut,
            this.chShortCommand,
            this.chFunction});
            this.lv2.FullRowSelect = true;
            this.lv2.GridLines = true;
            this.lv2.HideSelection = false;
            this.lv2.Location = new System.Drawing.Point(0, 21);
            this.lv2.Name = "lv2";
            this.lv2.Size = new System.Drawing.Size(312, 508);
            this.lv2.TabIndex = 0;
            this.lv2.UseCompatibleStateImageBehavior = false;
            this.lv2.View = System.Windows.Forms.View.Details;
            this.lv2.SelectedIndexChanged += new System.EventHandler(this.lv2_SelectedIndexChanged);
            // 
            // chShortCut
            // 
            this.chShortCut.Text = "快捷键";
            this.chShortCut.Width = 70;
            // 
            // chShortCommand
            // 
            this.chShortCommand.Text = "快捷指令";
            this.chShortCommand.Width = 71;
            // 
            // chFunction
            // 
            this.chFunction.Text = "功能说明";
            this.chFunction.Width = 167;
            // 
            // BlackWindowHotKeySetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BlackWindowHotKeySetup";
            this.Text = "黑屏快捷键设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BlackWindowHotKeySetup_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private EagleControls.SortListView lv1;
        private System.Windows.Forms.TextBox txtHotKey1;
        private System.Windows.Forms.TextBox txtDestCommand;
        private System.Windows.Forms.Button btnDelShortCommand;
        private System.Windows.Forms.Button btnAddShortCommand;
        private System.Windows.Forms.ColumnHeader chHotKey1;
        private System.Windows.Forms.ColumnHeader chDestCommand;
        private System.Windows.Forms.Panel panel2;
        private EagleControls.SortListView lv2;
        private System.Windows.Forms.ColumnHeader chShortCut;
        private System.Windows.Forms.ColumnHeader chShortCommand;
        private System.Windows.Forms.ColumnHeader chFunction;
        private System.Windows.Forms.Button btnDelShortFunction;
        private System.Windows.Forms.Button btnAddShortFunction;
        private System.Windows.Forms.TextBox txtHotKey3;
        private System.Windows.Forms.TextBox txtHotKey2;
    }
}