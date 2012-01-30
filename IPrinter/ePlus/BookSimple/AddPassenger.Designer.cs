namespace ePlus.BookSimple
{
    partial class AddPassenger
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbCardID = new System.Windows.Forms.TextBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_CardNo = new System.Windows.Forms.ListBox();
            this.lb_姓名 = new System.Windows.Forms.ListBox();
            this.bt_删除 = new System.Windows.Forms.Button();
            this.bt_添加姓名 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "姓名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "身份证号：";
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Location = new System.Drawing.Point(18, 68);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(67, 21);
            this.tbName.TabIndex = 0;
            // 
            // tbCardID
            // 
            this.tbCardID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCardID.Location = new System.Drawing.Point(91, 68);
            this.tbCardID.Name = "tbCardID";
            this.tbCardID.Size = new System.Drawing.Size(139, 21);
            this.tbCardID.TabIndex = 1;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(236, 153);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 48);
            this.btAdd.TabIndex = 4;
            this.btAdd.Text = "入团";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(236, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lb_CardNo
            // 
            this.lb_CardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_CardNo.Enabled = false;
            this.lb_CardNo.FormattingEnabled = true;
            this.lb_CardNo.ItemHeight = 12;
            this.lb_CardNo.Location = new System.Drawing.Point(91, 95);
            this.lb_CardNo.Name = "lb_CardNo";
            this.lb_CardNo.Size = new System.Drawing.Size(139, 134);
            this.lb_CardNo.TabIndex = 8;
            // 
            // lb_姓名
            // 
            this.lb_姓名.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_姓名.FormattingEnabled = true;
            this.lb_姓名.ItemHeight = 12;
            this.lb_姓名.Location = new System.Drawing.Point(18, 95);
            this.lb_姓名.Name = "lb_姓名";
            this.lb_姓名.Size = new System.Drawing.Size(67, 134);
            this.lb_姓名.TabIndex = 7;
            // 
            // bt_删除
            // 
            this.bt_删除.Location = new System.Drawing.Point(236, 95);
            this.bt_删除.Name = "bt_删除";
            this.bt_删除.Size = new System.Drawing.Size(75, 23);
            this.bt_删除.TabIndex = 3;
            this.bt_删除.Text = "删除(&D)";
            this.bt_删除.UseVisualStyleBackColor = true;
            this.bt_删除.Click += new System.EventHandler(this.bt_删除_Click);
            // 
            // bt_添加姓名
            // 
            this.bt_添加姓名.Location = new System.Drawing.Point(236, 68);
            this.bt_添加姓名.Name = "bt_添加姓名";
            this.bt_添加姓名.Size = new System.Drawing.Size(75, 23);
            this.bt_添加姓名.TabIndex = 2;
            this.bt_添加姓名.Text = "添加(&A)";
            this.bt_添加姓名.UseVisualStyleBackColor = true;
            this.bt_添加姓名.Click += new System.EventHandler(this.bt_添加姓名_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(212, 20);
            this.comboBox1.TabIndex = 9;
            // 
            // AddPassenger
            // 
            this.AcceptButton = this.bt_添加姓名;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(322, 240);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.bt_删除);
            this.Controls.Add(this.bt_添加姓名);
            this.Controls.Add(this.lb_CardNo);
            this.Controls.Add(this.lb_姓名);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.tbCardID);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddPassenger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加乘客";
            this.Load += new System.EventHandler(this.AddPassenger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbCardID;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lb_CardNo;
        private System.Windows.Forms.ListBox lb_姓名;
        private System.Windows.Forms.Button bt_删除;
        private System.Windows.Forms.Button bt_添加姓名;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}