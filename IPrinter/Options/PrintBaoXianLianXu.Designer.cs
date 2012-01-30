namespace Options
{
    partial class PrintBaoXianLianXu
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
            this.label1 = new System.Windows.Forms.Label();
            this.lvPrint = new System.Windows.Forms.ListView();
            this.Name = new System.Windows.Forms.ColumnHeader();
            this.CardID = new System.Windows.Forms.ColumnHeader();
            this.PolicyNo = new System.Windows.Forms.ColumnHeader();
            this.btAdd = new System.Windows.Forms.Button();
            this.btModify = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbCardId = new System.Windows.Forms.TextBox();
            this.tbPolicyNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "打印列表";
            // 
            // lvPrint
            // 
            this.lvPrint.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name,
            this.CardID,
            this.PolicyNo});
            this.lvPrint.FullRowSelect = true;
            this.lvPrint.GridLines = true;
            this.lvPrint.HideSelection = false;
            this.lvPrint.Location = new System.Drawing.Point(12, 24);
            this.lvPrint.Name = "lvPrint";
            this.lvPrint.Size = new System.Drawing.Size(431, 175);
            this.lvPrint.TabIndex = 1;
            this.lvPrint.UseCompatibleStateImageBehavior = false;
            this.lvPrint.View = System.Windows.Forms.View.Details;
            this.lvPrint.SelectedIndexChanged += new System.EventHandler(this.lvPrint_SelectedIndexChanged);
            // 
            // Name
            // 
            this.Name.Text = "姓名";
            this.Name.Width = 95;
            // 
            // CardID
            // 
            this.CardID.Text = "证件号";
            this.CardID.Width = 192;
            // 
            // PolicyNo
            // 
            this.PolicyNo.Text = "保单号";
            this.PolicyNo.Width = 139;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(60, 238);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "增加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btModify
            // 
            this.btModify.Location = new System.Drawing.Point(141, 238);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(75, 23);
            this.btModify.TabIndex = 2;
            this.btModify.Text = "修改";
            this.btModify.UseVisualStyleBackColor = true;
            this.btModify.Click += new System.EventHandler(this.btModify_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(222, 238);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 2;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btPrint
            // 
            this.btPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btPrint.Location = new System.Drawing.Point(303, 238);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 23);
            this.btPrint.TabIndex = 2;
            this.btPrint.Text = "打印";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 205);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(88, 21);
            this.tbName.TabIndex = 3;
            // 
            // tbCardId
            // 
            this.tbCardId.Location = new System.Drawing.Point(106, 205);
            this.tbCardId.Name = "tbCardId";
            this.tbCardId.Size = new System.Drawing.Size(191, 21);
            this.tbCardId.TabIndex = 3;
            // 
            // tbPolicyNo
            // 
            this.tbPolicyNo.Location = new System.Drawing.Point(303, 205);
            this.tbPolicyNo.Name = "tbPolicyNo";
            this.tbPolicyNo.Size = new System.Drawing.Size(140, 21);
            this.tbPolicyNo.TabIndex = 3;
            // 
            // PrintBaoXianLianXu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 273);
            this.Controls.Add(this.tbCardId);
            this.Controls.Add(this.tbPolicyNo);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btModify);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.lvPrint);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.Name = "PrintBaoXianLianXu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "保险连续打印";
            this.Load += new System.EventHandler(this.PrintBaoXianLianXu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvPrint;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btModify;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader CardID;
        private System.Windows.Forms.ColumnHeader PolicyNo;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbCardId;
        private System.Windows.Forms.TextBox tbPolicyNo;
    }
}