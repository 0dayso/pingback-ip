namespace EagleForms.Easy
{
    partial class AvPanel
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
            this.pAvOption = new System.Windows.Forms.Panel();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.btnAfterTomorrow = new System.Windows.Forms.Button();
            this.btnTomorrow = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnBackDay = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkSpecBunk = new System.Windows.Forms.CheckBox();
            this.checkListNoSeatBunk = new System.Windows.Forms.CheckBox();
            this.checkDirect = new System.Windows.Forms.CheckBox();
            this.checkReturn = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioThreeCode = new System.Windows.Forms.RadioButton();
            this.radioPinyin = new System.Windows.Forms.RadioButton();
            this.radioChinese = new System.Windows.Forms.RadioButton();
            this.checkOutside = new System.Windows.Forms.CheckBox();
            this.checkInside = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAv = new System.Windows.Forms.Button();
            this.cbCityBeg = new System.Windows.Forms.ComboBox();
            this.dtpAv = new System.Windows.Forms.DateTimePicker();
            this.cbCarrier = new System.Windows.Forms.ComboBox();
            this.cbCityEnd = new System.Windows.Forms.ComboBox();
            this.pAvOption.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pAvOption
            // 
            this.pAvOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAvOption.Controls.Add(this.btnNextDay);
            this.pAvOption.Controls.Add(this.btnAfterTomorrow);
            this.pAvOption.Controls.Add(this.btnTomorrow);
            this.pAvOption.Controls.Add(this.btnToday);
            this.pAvOption.Controls.Add(this.btnBackDay);
            this.pAvOption.Controls.Add(this.panel3);
            this.pAvOption.Controls.Add(this.panel2);
            this.pAvOption.Controls.Add(this.panel1);
            this.pAvOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pAvOption.Location = new System.Drawing.Point(0, 0);
            this.pAvOption.Name = "pAvOption";
            this.pAvOption.Size = new System.Drawing.Size(632, 86);
            this.pAvOption.TabIndex = 0;
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(408, 50);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(75, 23);
            this.btnNextDay.TabIndex = 7;
            this.btnNextDay.Text = "后一天";
            this.btnNextDay.UseVisualStyleBackColor = true;
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnAfterTomorrow
            // 
            this.btnAfterTomorrow.Location = new System.Drawing.Point(446, 13);
            this.btnAfterTomorrow.Name = "btnAfterTomorrow";
            this.btnAfterTomorrow.Size = new System.Drawing.Size(75, 23);
            this.btnAfterTomorrow.TabIndex = 5;
            this.btnAfterTomorrow.Text = "后天";
            this.btnAfterTomorrow.UseVisualStyleBackColor = true;
            this.btnAfterTomorrow.Click += new System.EventHandler(this.btnAfterTomorrow_Click);
            // 
            // btnTomorrow
            // 
            this.btnTomorrow.Location = new System.Drawing.Point(365, 13);
            this.btnTomorrow.Name = "btnTomorrow";
            this.btnTomorrow.Size = new System.Drawing.Size(75, 23);
            this.btnTomorrow.TabIndex = 4;
            this.btnTomorrow.Text = "明天";
            this.btnTomorrow.UseVisualStyleBackColor = true;
            this.btnTomorrow.Click += new System.EventHandler(this.btnTomorrow_Click);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(284, 13);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(75, 23);
            this.btnToday.TabIndex = 3;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnBackDay
            // 
            this.btnBackDay.Location = new System.Drawing.Point(310, 50);
            this.btnBackDay.Name = "btnBackDay";
            this.btnBackDay.Size = new System.Drawing.Size(75, 23);
            this.btnBackDay.TabIndex = 6;
            this.btnBackDay.Text = "前一天";
            this.btnBackDay.UseVisualStyleBackColor = true;
            this.btnBackDay.Click += new System.EventHandler(this.btnBackDay_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.checkSpecBunk);
            this.panel3.Controls.Add(this.checkListNoSeatBunk);
            this.panel3.Controls.Add(this.checkDirect);
            this.panel3.Controls.Add(this.checkReturn);
            this.panel3.Location = new System.Drawing.Point(180, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(95, 84);
            this.panel3.TabIndex = 1;
            // 
            // checkSpecBunk
            // 
            this.checkSpecBunk.AutoSize = true;
            this.checkSpecBunk.Location = new System.Drawing.Point(8, 63);
            this.checkSpecBunk.Name = "checkSpecBunk";
            this.checkSpecBunk.Size = new System.Drawing.Size(84, 16);
            this.checkSpecBunk.TabIndex = 0;
            this.checkSpecBunk.Text = "显示特殊舱";
            this.checkSpecBunk.UseVisualStyleBackColor = true;
            this.checkSpecBunk.CheckedChanged += new System.EventHandler(this.checkSpecBunk_CheckedChanged);
            // 
            // checkListNoSeatBunk
            // 
            this.checkListNoSeatBunk.AutoSize = true;
            this.checkListNoSeatBunk.Location = new System.Drawing.Point(8, 45);
            this.checkListNoSeatBunk.Name = "checkListNoSeatBunk";
            this.checkListNoSeatBunk.Size = new System.Drawing.Size(84, 16);
            this.checkListNoSeatBunk.TabIndex = 0;
            this.checkListNoSeatBunk.Text = "显示无座舱";
            this.checkListNoSeatBunk.UseVisualStyleBackColor = true;
            this.checkListNoSeatBunk.CheckedChanged += new System.EventHandler(this.checkListNoSeatBunk_CheckedChanged);
            // 
            // checkDirect
            // 
            this.checkDirect.AutoSize = true;
            this.checkDirect.Checked = true;
            this.checkDirect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDirect.Location = new System.Drawing.Point(8, 24);
            this.checkDirect.Name = "checkDirect";
            this.checkDirect.Size = new System.Drawing.Size(48, 16);
            this.checkDirect.TabIndex = 0;
            this.checkDirect.Text = "直飞";
            this.checkDirect.UseVisualStyleBackColor = true;
            this.checkDirect.CheckedChanged += new System.EventHandler(this.checkDirect_CheckedChanged);
            // 
            // checkReturn
            // 
            this.checkReturn.AutoSize = true;
            this.checkReturn.Location = new System.Drawing.Point(8, 5);
            this.checkReturn.Name = "checkReturn";
            this.checkReturn.Size = new System.Drawing.Size(48, 16);
            this.checkReturn.TabIndex = 0;
            this.checkReturn.Text = "返程";
            this.checkReturn.UseVisualStyleBackColor = true;
            this.checkReturn.CheckedChanged += new System.EventHandler(this.checkReturn_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radioThreeCode);
            this.panel2.Controls.Add(this.radioPinyin);
            this.panel2.Controls.Add(this.radioChinese);
            this.panel2.Controls.Add(this.checkOutside);
            this.panel2.Controls.Add(this.checkInside);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(531, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 84);
            this.panel2.TabIndex = 2;
            // 
            // radioThreeCode
            // 
            this.radioThreeCode.AutoSize = true;
            this.radioThreeCode.Location = new System.Drawing.Point(3, 67);
            this.radioThreeCode.Name = "radioThreeCode";
            this.radioThreeCode.Size = new System.Drawing.Size(59, 16);
            this.radioThreeCode.TabIndex = 4;
            this.radioThreeCode.Text = "三字码";
            this.radioThreeCode.UseVisualStyleBackColor = true;
            // 
            // radioPinyin
            // 
            this.radioPinyin.AutoSize = true;
            this.radioPinyin.Location = new System.Drawing.Point(3, 51);
            this.radioPinyin.Name = "radioPinyin";
            this.radioPinyin.Size = new System.Drawing.Size(83, 16);
            this.radioPinyin.TabIndex = 3;
            this.radioPinyin.Text = "拼音首字母";
            this.radioPinyin.UseVisualStyleBackColor = true;
            // 
            // radioChinese
            // 
            this.radioChinese.AutoSize = true;
            this.radioChinese.Checked = true;
            this.radioChinese.Location = new System.Drawing.Point(3, 35);
            this.radioChinese.Name = "radioChinese";
            this.radioChinese.Size = new System.Drawing.Size(47, 16);
            this.radioChinese.TabIndex = 2;
            this.radioChinese.TabStop = true;
            this.radioChinese.Text = "中文";
            this.radioChinese.UseVisualStyleBackColor = true;
            // 
            // checkOutside
            // 
            this.checkOutside.AutoSize = true;
            this.checkOutside.Location = new System.Drawing.Point(3, 19);
            this.checkOutside.Name = "checkOutside";
            this.checkOutside.Size = new System.Drawing.Size(96, 16);
            this.checkOutside.TabIndex = 1;
            this.checkOutside.Text = "显示国际城市";
            this.checkOutside.UseVisualStyleBackColor = true;
            this.checkOutside.CheckedChanged += new System.EventHandler(this.checkOutside_CheckedChanged);
            // 
            // checkInside
            // 
            this.checkInside.AutoSize = true;
            this.checkInside.Checked = true;
            this.checkInside.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkInside.Location = new System.Drawing.Point(3, 2);
            this.checkInside.Name = "checkInside";
            this.checkInside.Size = new System.Drawing.Size(96, 16);
            this.checkInside.TabIndex = 0;
            this.checkInside.Text = "显示国内城市";
            this.checkInside.UseVisualStyleBackColor = true;
            this.checkInside.CheckedChanged += new System.EventHandler(this.checkInside_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnAv);
            this.panel1.Controls.Add(this.cbCityBeg);
            this.panel1.Controls.Add(this.dtpAv);
            this.panel1.Controls.Add(this.cbCarrier);
            this.panel1.Controls.Add(this.cbCityEnd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 84);
            this.panel1.TabIndex = 0;
            // 
            // btnAv
            // 
            this.btnAv.Location = new System.Drawing.Point(121, 39);
            this.btnAv.Name = "btnAv";
            this.btnAv.Size = new System.Drawing.Size(57, 41);
            this.btnAv.TabIndex = 4;
            this.btnAv.Text = "查询";
            this.btnAv.UseVisualStyleBackColor = true;
            this.btnAv.Click += new System.EventHandler(this.btnAv_Click);
            // 
            // cbCityBeg
            // 
            this.cbCityBeg.FormattingEnabled = true;
            this.cbCityBeg.Location = new System.Drawing.Point(1, 1);
            this.cbCityBeg.MaxDropDownItems = 30;
            this.cbCityBeg.Name = "cbCityBeg";
            this.cbCityBeg.Size = new System.Drawing.Size(177, 20);
            this.cbCityBeg.TabIndex = 0;
            this.cbCityBeg.Text = "出发城市";
            // 
            // dtpAv
            // 
            this.dtpAv.Location = new System.Drawing.Point(1, 39);
            this.dtpAv.Name = "dtpAv";
            this.dtpAv.Size = new System.Drawing.Size(121, 21);
            this.dtpAv.TabIndex = 2;
            this.dtpAv.ValueChanged += new System.EventHandler(this.dtpAv_ValueChanged);
            // 
            // cbCarrier
            // 
            this.cbCarrier.FormattingEnabled = true;
            this.cbCarrier.Location = new System.Drawing.Point(1, 60);
            this.cbCarrier.MaxDropDownItems = 20;
            this.cbCarrier.Name = "cbCarrier";
            this.cbCarrier.Size = new System.Drawing.Size(121, 20);
            this.cbCarrier.TabIndex = 3;
            this.cbCarrier.Text = "全部";
            // 
            // cbCityEnd
            // 
            this.cbCityEnd.FormattingEnabled = true;
            this.cbCityEnd.Location = new System.Drawing.Point(1, 20);
            this.cbCityEnd.MaxDropDownItems = 30;
            this.cbCityEnd.Name = "cbCityEnd";
            this.cbCityEnd.Size = new System.Drawing.Size(177, 20);
            this.cbCityEnd.TabIndex = 1;
            this.cbCityEnd.Text = "到达城市";
            // 
            // AvPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 87);
            this.Controls.Add(this.pAvOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AvPanel";
            this.Text = "EasyForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EasyForm_FormClosing);
            this.Load += new System.EventHandler(this.EasyForm_Load);
            this.pAvOption.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pAvOption;
        private System.Windows.Forms.DateTimePicker dtpAv;
        private System.Windows.Forms.ComboBox cbCityEnd;
        private System.Windows.Forms.ComboBox cbCityBeg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAv;
        private System.Windows.Forms.ComboBox cbCarrier;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkOutside;
        private System.Windows.Forms.CheckBox checkInside;
        private System.Windows.Forms.RadioButton radioThreeCode;
        private System.Windows.Forms.RadioButton radioPinyin;
        private System.Windows.Forms.RadioButton radioChinese;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkReturn;
        private System.Windows.Forms.CheckBox checkSpecBunk;
        private System.Windows.Forms.CheckBox checkListNoSeatBunk;
        private System.Windows.Forms.CheckBox checkDirect;
        private System.Windows.Forms.Button btnNextDay;
        private System.Windows.Forms.Button btnBackDay;
        private System.Windows.Forms.Button btnAfterTomorrow;
        private System.Windows.Forms.Button btnTomorrow;
        private System.Windows.Forms.Button btnToday;
    }
}