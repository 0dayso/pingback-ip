namespace Options
{
    partial class BatReceipt
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
            this.lv = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chReceiptNo = new System.Windows.Forms.ColumnHeader();
            this.chEticketNo = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chIdCard = new System.Windows.Forms.ColumnHeader();
            this.tbReceiptBeg = new System.Windows.Forms.TextBox();
            this.tbEticketBeg = new System.Windows.Forms.TextBox();
            this.tbReceiptEnd = new System.Windows.Forms.TextBox();
            this.tbEticketEnd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.btBat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chReceiptNo,
            this.chEticketNo,
            this.chName,
            this.chIdCard});
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(12, 12);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(572, 313);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            // 
            // chID
            // 
            this.chID.Text = "序号";
            this.chID.Width = 40;
            // 
            // chReceiptNo
            // 
            this.chReceiptNo.Text = "行程单号";
            this.chReceiptNo.Width = 138;
            // 
            // chEticketNo
            // 
            this.chEticketNo.Text = "电子客票号";
            this.chEticketNo.Width = 145;
            // 
            // chName
            // 
            this.chName.Text = "姓名";
            this.chName.Width = 77;
            // 
            // chIdCard
            // 
            this.chIdCard.Text = "证件号";
            this.chIdCard.Width = 160;
            // 
            // tbReceiptBeg
            // 
            this.tbReceiptBeg.Location = new System.Drawing.Point(590, 26);
            this.tbReceiptBeg.Name = "tbReceiptBeg";
            this.tbReceiptBeg.Size = new System.Drawing.Size(100, 21);
            this.tbReceiptBeg.TabIndex = 1;
            this.tbReceiptBeg.TextChanged += new System.EventHandler(this.tbReceiptBeg_TextChanged);
            // 
            // tbEticketBeg
            // 
            this.tbEticketBeg.Location = new System.Drawing.Point(590, 92);
            this.tbEticketBeg.Name = "tbEticketBeg";
            this.tbEticketBeg.Size = new System.Drawing.Size(100, 21);
            this.tbEticketBeg.TabIndex = 1;
            this.tbEticketBeg.TextChanged += new System.EventHandler(this.tbEticketBeg_TextChanged);
            // 
            // tbReceiptEnd
            // 
            this.tbReceiptEnd.Location = new System.Drawing.Point(590, 53);
            this.tbReceiptEnd.Name = "tbReceiptEnd";
            this.tbReceiptEnd.Size = new System.Drawing.Size(100, 21);
            this.tbReceiptEnd.TabIndex = 1;
            this.tbReceiptEnd.Enter += new System.EventHandler(this.tbReceiptEnd_Enter);
            // 
            // tbEticketEnd
            // 
            this.tbEticketEnd.Location = new System.Drawing.Point(590, 119);
            this.tbEticketEnd.Name = "tbEticketEnd";
            this.tbEticketEnd.Size = new System.Drawing.Size(100, 21);
            this.tbEticketEnd.TabIndex = 1;
            this.tbEticketEnd.Enter += new System.EventHandler(this.tbEticketEnd_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(590, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "行程单范围";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(590, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "电子客票号范围";
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(590, 147);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(100, 23);
            this.btAdd.TabIndex = 3;
            this.btAdd.Text = "增加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDel
            // 
            this.btDel.Location = new System.Drawing.Point(590, 176);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(100, 23);
            this.btDel.TabIndex = 3;
            this.btDel.Text = "删除";
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btBat
            // 
            this.btBat.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btBat.Location = new System.Drawing.Point(592, 206);
            this.btBat.Name = "btBat";
            this.btBat.Size = new System.Drawing.Size(98, 23);
            this.btBat.TabIndex = 4;
            this.btBat.Text = "批量";
            this.btBat.UseVisualStyleBackColor = true;
            this.btBat.Click += new System.EventHandler(this.btBat_Click);
            // 
            // BatReceipt
            // 
            this.AcceptButton = this.btBat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 337);
            this.Controls.Add(this.btBat);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEticketEnd);
            this.Controls.Add(this.tbEticketBeg);
            this.Controls.Add(this.tbReceiptEnd);
            this.Controls.Add(this.tbReceiptBeg);
            this.Controls.Add(this.lv);
            this.Name = "BatReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "行程单批量处理";
            this.Load += new System.EventHandler(this.BatReceipt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.TextBox tbReceiptBeg;
        private System.Windows.Forms.TextBox tbEticketBeg;
        private System.Windows.Forms.TextBox tbReceiptEnd;
        private System.Windows.Forms.TextBox tbEticketEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chReceiptNo;
        private System.Windows.Forms.ColumnHeader chEticketNo;
        private System.Windows.Forms.Button btBat;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chIdCard;
    }
}