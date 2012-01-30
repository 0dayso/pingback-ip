namespace EagleForms.ToCommand
{
    partial class SwitchVisableConfig
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.btnAddSameOffice = new System.Windows.Forms.Button();
            this.btnAddSameServer = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnDelSelected = new System.Windows.Forms.Button();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lvAll = new EagleControls.SortListView();
            this.chId = new System.Windows.Forms.ColumnHeader();
            this.chOffice = new System.Windows.Forms.ColumnHeader();
            this.chOfficeAlly = new System.Windows.Forms.ColumnHeader();
            this.chServerBelong = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAddAll);
            this.panel1.Controls.Add(this.btnDelAll);
            this.panel1.Controls.Add(this.btnAddSameServer);
            this.panel1.Controls.Add(this.btnDelSelected);
            this.panel1.Controls.Add(this.btnAddSameOffice);
            this.panel1.Controls.Add(this.btnAddSelected);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lb1);
            this.panel1.Controls.Add(this.lvAll);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 367);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(434, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "切换至";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "全部可用配置列表";
            // 
            // lb1
            // 
            this.lb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb1.FormattingEnabled = true;
            this.lb1.ItemHeight = 12;
            this.lb1.Location = new System.Drawing.Point(436, 24);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(71, 340);
            this.lb1.Sorted = true;
            this.lb1.TabIndex = 1;
            this.lb1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lb1_MouseDoubleClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(352, 370);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "切换";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(434, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "全部可用配置列表";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "添加";
            // 
            // btnAddSelected
            // 
            this.btnAddSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSelected.Location = new System.Drawing.Point(353, 39);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(75, 23);
            this.btnAddSelected.TabIndex = 1;
            this.btnAddSelected.Text = "选中配置";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            // 
            // btnAddSameOffice
            // 
            this.btnAddSameOffice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSameOffice.Location = new System.Drawing.Point(353, 68);
            this.btnAddSameOffice.Name = "btnAddSameOffice";
            this.btnAddSameOffice.Size = new System.Drawing.Size(75, 23);
            this.btnAddSameOffice.TabIndex = 1;
            this.btnAddSameOffice.Text = "同OFFICE";
            this.btnAddSameOffice.UseVisualStyleBackColor = true;
            this.btnAddSameOffice.Click += new System.EventHandler(this.btnAddSameOffice_Click);
            // 
            // btnAddSameServer
            // 
            this.btnAddSameServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSameServer.Location = new System.Drawing.Point(353, 97);
            this.btnAddSameServer.Name = "btnAddSameServer";
            this.btnAddSameServer.Size = new System.Drawing.Size(75, 23);
            this.btnAddSameServer.TabIndex = 1;
            this.btnAddSameServer.Text = "同服务器";
            this.btnAddSameServer.UseVisualStyleBackColor = true;
            this.btnAddSameServer.Click += new System.EventHandler(this.btnAddSameServer_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAll.Location = new System.Drawing.Point(353, 126);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(75, 23);
            this.btnAddAll.TabIndex = 1;
            this.btnAddAll.Text = "所有配置";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnDelSelected
            // 
            this.btnDelSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelSelected.Location = new System.Drawing.Point(353, 176);
            this.btnDelSelected.Name = "btnDelSelected";
            this.btnDelSelected.Size = new System.Drawing.Size(75, 23);
            this.btnDelSelected.TabIndex = 1;
            this.btnDelSelected.Text = "选中配置";
            this.btnDelSelected.UseVisualStyleBackColor = true;
            this.btnDelSelected.Click += new System.EventHandler(this.btnDelSelected_Click);
            // 
            // btnDelAll
            // 
            this.btnDelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelAll.Location = new System.Drawing.Point(353, 205);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(75, 23);
            this.btnDelAll.TabIndex = 1;
            this.btnDelAll.Text = "所有配置";
            this.btnDelAll.UseVisualStyleBackColor = true;
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(370, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "删除";
            // 
            // lvAll
            // 
            this.lvAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvAll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chOffice,
            this.chOfficeAlly,
            this.chServerBelong});
            this.lvAll.FullRowSelect = true;
            this.lvAll.GridLines = true;
            this.lvAll.HideSelection = false;
            this.lvAll.Location = new System.Drawing.Point(0, 24);
            this.lvAll.Name = "lvAll";
            this.lvAll.Size = new System.Drawing.Size(344, 340);
            this.lvAll.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAll.TabIndex = 0;
            this.lvAll.UseCompatibleStateImageBehavior = false;
            this.lvAll.View = System.Windows.Forms.View.Details;
            this.lvAll.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAll_MouseDoubleClick);
            this.lvAll.DoubleClick += new System.EventHandler(this.lvAll_DoubleClick);
            // 
            // chId
            // 
            this.chId.Text = "编号";
            // 
            // chOffice
            // 
            this.chOffice.Text = "Office";
            this.chOffice.Width = 72;
            // 
            // chOfficeAlly
            // 
            this.chOfficeAlly.Text = "别名";
            this.chOfficeAlly.Width = 74;
            // 
            // chServerBelong
            // 
            this.chServerBelong.Text = "所在服务器";
            this.chServerBelong.Width = 133;
            // 
            // SwitchVisableConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 395);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SwitchVisableConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "切换可用配置";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private EagleControls.SortListView lvAll;
        private System.Windows.Forms.ListBox lb1;
        private System.Windows.Forms.ColumnHeader chId;
        private System.Windows.Forms.ColumnHeader chOffice;
        private System.Windows.Forms.ColumnHeader chOfficeAlly;
        private System.Windows.Forms.ColumnHeader chServerBelong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnAddSameServer;
        private System.Windows.Forms.Button btnAddSameOffice;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDelAll;
        private System.Windows.Forms.Button btnDelSelected;
    }
}