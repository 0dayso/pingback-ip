namespace ePlus
{
    partial class ShortCutKeySettingsForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "sdfadsfasf",
            "Shift+Alt+Ctrl+F4"}, -1);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbStandardCode = new System.Windows.Forms.RadioButton();
            this.rbSpecialCode = new System.Windows.Forms.RadioButton();
            this.cbSpecialCodeList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInstruction = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbKeyList = new System.Windows.Forms.ComboBox();
            this.cbShift = new System.Windows.Forms.CheckBox();
            this.cbCtrl = new System.Windows.Forms.CheckBox();
            this.cbAlt = new System.Windows.Forms.CheckBox();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "说明：输入特殊指令要 [] ，比如要输入SOE就需要键入 [SOE]";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "指令类型";
            // 
            // rbStandardCode
            // 
            this.rbStandardCode.AutoSize = true;
            this.rbStandardCode.Location = new System.Drawing.Point(71, 34);
            this.rbStandardCode.Name = "rbStandardCode";
            this.rbStandardCode.Size = new System.Drawing.Size(71, 16);
            this.rbStandardCode.TabIndex = 1;
            this.rbStandardCode.TabStop = true;
            this.rbStandardCode.Text = "标准指令";
            this.rbStandardCode.UseVisualStyleBackColor = true;
            // 
            // rbSpecialCode
            // 
            this.rbSpecialCode.AutoSize = true;
            this.rbSpecialCode.Location = new System.Drawing.Point(148, 34);
            this.rbSpecialCode.Name = "rbSpecialCode";
            this.rbSpecialCode.Size = new System.Drawing.Size(71, 16);
            this.rbSpecialCode.TabIndex = 1;
            this.rbSpecialCode.TabStop = true;
            this.rbSpecialCode.Text = "特殊指令";
            this.rbSpecialCode.UseVisualStyleBackColor = true;
            // 
            // cbSpecialCodeList
            // 
            this.cbSpecialCodeList.FormattingEnabled = true;
            this.cbSpecialCodeList.Location = new System.Drawing.Point(221, 32);
            this.cbSpecialCodeList.Name = "cbSpecialCodeList";
            this.cbSpecialCodeList.Size = new System.Drawing.Size(121, 20);
            this.cbSpecialCodeList.TabIndex = 2;
            this.cbSpecialCodeList.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "输入指令";
            // 
            // txtInstruction
            // 
            this.txtInstruction.Location = new System.Drawing.Point(83, 60);
            this.txtInstruction.Multiline = true;
            this.txtInstruction.Name = "txtInstruction";
            this.txtInstruction.Size = new System.Drawing.Size(259, 83);
            this.txtInstruction.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "选择快捷键";
            // 
            // cbKeyList
            // 
            this.cbKeyList.FormattingEnabled = true;
            this.cbKeyList.Location = new System.Drawing.Point(82, 149);
            this.cbKeyList.Name = "cbKeyList";
            this.cbKeyList.Size = new System.Drawing.Size(100, 20);
            this.cbKeyList.TabIndex = 2;
            this.cbKeyList.Enter += new System.EventHandler(this.cbKeyList_Enter);
            this.cbKeyList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbKeyList_KeyUp);
            this.cbKeyList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbKeyList_KeyDown);
            // 
            // cbShift
            // 
            this.cbShift.AutoSize = true;
            this.cbShift.Location = new System.Drawing.Point(188, 151);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(54, 16);
            this.cbShift.TabIndex = 4;
            this.cbShift.Text = "Shift";
            this.cbShift.UseVisualStyleBackColor = true;
            // 
            // cbCtrl
            // 
            this.cbCtrl.AutoSize = true;
            this.cbCtrl.Location = new System.Drawing.Point(248, 151);
            this.cbCtrl.Name = "cbCtrl";
            this.cbCtrl.Size = new System.Drawing.Size(48, 16);
            this.cbCtrl.TabIndex = 4;
            this.cbCtrl.Text = "Ctrl";
            this.cbCtrl.UseVisualStyleBackColor = true;
            // 
            // cbAlt
            // 
            this.cbAlt.AutoSize = true;
            this.cbAlt.Location = new System.Drawing.Point(299, 151);
            this.cbAlt.Name = "cbAlt";
            this.cbAlt.Size = new System.Drawing.Size(42, 16);
            this.cbAlt.TabIndex = 4;
            this.cbAlt.Text = "Alt";
            this.cbAlt.UseVisualStyleBackColor = true;
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.FullRowSelect = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView.Location = new System.Drawing.Point(12, 173);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(330, 186);
            this.listView.TabIndex = 5;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 158;
            // 
            // columnHeader2
            // 
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 140;
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAdd.Location = new System.Drawing.Point(74, 365);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(58, 24);
            this.btAdd.TabIndex = 6;
            this.btAdd.Text = "添加(&A)";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDelete.Location = new System.Drawing.Point(228, 365);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(58, 24);
            this.btDelete.TabIndex = 6;
            this.btDelete.Text = "删除(&D)";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // ShortCutKeySettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 397);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.cbAlt);
            this.Controls.Add(this.cbCtrl);
            this.Controls.Add(this.cbShift);
            this.Controls.Add(this.txtInstruction);
            this.Controls.Add(this.cbKeyList);
            this.Controls.Add(this.cbSpecialCodeList);
            this.Controls.Add(this.rbSpecialCode);
            this.Controls.Add(this.rbStandardCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "ShortCutKeySettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "快捷键设定";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortCutKeySettingsForm_KeyDown);
            this.Load += new System.EventHandler(this.ShortCutKeySettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbStandardCode;
        private System.Windows.Forms.RadioButton rbSpecialCode;
        private System.Windows.Forms.ComboBox cbSpecialCodeList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInstruction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbKeyList;
        private System.Windows.Forms.CheckBox cbShift;
        private System.Windows.Forms.CheckBox cbCtrl;
        private System.Windows.Forms.CheckBox cbAlt;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}