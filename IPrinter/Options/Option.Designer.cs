namespace Options
{
    partial class Options
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cbIbeUse = new System.Windows.Forms.CheckBox();
            this.cbListNoSeatBunk = new System.Windows.Forms.CheckBox();
            this.cbCity2 = new System.Windows.Forms.CheckBox();
            this.cbCity1 = new System.Windows.Forms.CheckBox();
            this.cbSelectCityType = new System.Windows.Forms.ComboBox();
            this.tbLocalCityCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbIbeUse);
            this.groupBox1.Controls.Add(this.cbListNoSeatBunk);
            this.groupBox1.Controls.Add(this.cbCity2);
            this.groupBox1.Controls.Add(this.cbCity1);
            this.groupBox1.Controls.Add(this.cbSelectCityType);
            this.groupBox1.Controls.Add(this.tbLocalCityCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 261);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(111, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(104, 83);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "黑屏";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "S3(回车发送)";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "S2(逐条显示)";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "S1(正常)";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // cbIbeUse
            // 
            this.cbIbeUse.AutoSize = true;
            this.cbIbeUse.Checked = true;
            this.cbIbeUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIbeUse.Location = new System.Drawing.Point(11, 132);
            this.cbIbeUse.Name = "cbIbeUse";
            this.cbIbeUse.Size = new System.Drawing.Size(90, 16);
            this.cbIbeUse.TabIndex = 3;
            this.cbIbeUse.Text = "使用IBE接口";
            this.cbIbeUse.UseVisualStyleBackColor = true;
            // 
            // cbListNoSeatBunk
            // 
            this.cbListNoSeatBunk.AutoSize = true;
            this.cbListNoSeatBunk.Location = new System.Drawing.Point(11, 110);
            this.cbListNoSeatBunk.Name = "cbListNoSeatBunk";
            this.cbListNoSeatBunk.Size = new System.Drawing.Size(96, 16);
            this.cbListNoSeatBunk.TabIndex = 3;
            this.cbListNoSeatBunk.Text = "显示无座舱位";
            this.cbListNoSeatBunk.UseVisualStyleBackColor = true;
            // 
            // cbCity2
            // 
            this.cbCity2.AutoSize = true;
            this.cbCity2.Location = new System.Drawing.Point(11, 88);
            this.cbCity2.Name = "cbCity2";
            this.cbCity2.Size = new System.Drawing.Size(96, 16);
            this.cbCity2.TabIndex = 3;
            this.cbCity2.Text = "显示国外城市";
            this.cbCity2.UseVisualStyleBackColor = true;
            // 
            // cbCity1
            // 
            this.cbCity1.AutoSize = true;
            this.cbCity1.Location = new System.Drawing.Point(11, 66);
            this.cbCity1.Name = "cbCity1";
            this.cbCity1.Size = new System.Drawing.Size(96, 16);
            this.cbCity1.TabIndex = 3;
            this.cbCity1.Text = "显示国内城市";
            this.cbCity1.UseVisualStyleBackColor = true;
            // 
            // cbSelectCityType
            // 
            this.cbSelectCityType.FormattingEnabled = true;
            this.cbSelectCityType.Items.AddRange(new object[] {
            "三字码",
            "中文",
            "拼音"});
            this.cbSelectCityType.Location = new System.Drawing.Point(111, 40);
            this.cbSelectCityType.Name = "cbSelectCityType";
            this.cbSelectCityType.Size = new System.Drawing.Size(94, 20);
            this.cbSelectCityType.TabIndex = 2;
            // 
            // tbLocalCityCode
            // 
            this.tbLocalCityCode.Location = new System.Drawing.Point(111, 14);
            this.tbLocalCityCode.Name = "tbLocalCityCode";
            this.tbLocalCityCode.Size = new System.Drawing.Size(94, 21);
            this.tbLocalCityCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "简版城市选择方式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "本地城市代码";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(519, 267);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消(&C)";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(600, 267);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "保存(&S)";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // Options
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(687, 298);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbLocalCityCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ComboBox cbSelectCityType;
        private System.Windows.Forms.CheckBox cbCity2;
        private System.Windows.Forms.CheckBox cbCity1;
        public System.Windows.Forms.CheckBox cbListNoSeatBunk;
        public System.Windows.Forms.CheckBox cbIbeUse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}