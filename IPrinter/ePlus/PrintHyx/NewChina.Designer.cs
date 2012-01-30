namespace ePlus.PrintHyx
{
    partial class NewChina
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
            this.tbPnr = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbInsuranceNo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbENumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSignature = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpSignDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBeneficiary = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbCardID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbName = new System.Windows.Forms.ComboBox();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.btSubmit = new System.Windows.Forms.Button();
            this.bt_Print = new System.Windows.Forms.Button();
            this.bt_SetToDefault = new System.Windows.Forms.Button();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.ptDoc = new System.Drawing.Printing.PrintDocument();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPnr
            // 
            this.tbPnr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPnr.ForeColor = System.Drawing.Color.Blue;
            this.tbPnr.Location = new System.Drawing.Point(594, 20);
            this.tbPnr.Name = "tbPnr";
            this.tbPnr.Size = new System.Drawing.Size(100, 21);
            this.tbPnr.TabIndex = 20;
            this.tbPnr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPnr_KeyUp);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label18.Location = new System.Drawing.Point(553, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 12);
            this.label18.TabIndex = 19;
            this.label18.Text = "PNR：";
            // 
            // tbInsuranceNo
            // 
            this.tbInsuranceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbInsuranceNo.ForeColor = System.Drawing.Color.Red;
            this.tbInsuranceNo.Location = new System.Drawing.Point(153, 47);
            this.tbInsuranceNo.Name = "tbInsuranceNo";
            this.tbInsuranceNo.Size = new System.Drawing.Size(100, 21);
            this.tbInsuranceNo.TabIndex = 22;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label19.Location = new System.Drawing.Point(54, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 21;
            this.label19.Text = "流水号No：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(127, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "K";
            // 
            // tbENumber
            // 
            this.tbENumber.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tbENumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbENumber.Enabled = false;
            this.tbENumber.Location = new System.Drawing.Point(458, 47);
            this.tbENumber.Name = "tbENumber";
            this.tbENumber.Size = new System.Drawing.Size(236, 21);
            this.tbENumber.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label2.Location = new System.Drawing.Point(345, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "保单号POLICYNo.：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbENumber);
            this.panel1.Controls.Add(this.tbInsuranceNo);
            this.panel1.Controls.Add(this.tbSignature);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.tbPnr);
            this.panel1.Controls.Add(this.dtpSignDate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Location = new System.Drawing.Point(12, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 326);
            this.panel1.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(241, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 21);
            this.label3.TabIndex = 46;
            this.label3.Text = "出行关爱乘客意外伤害保险单";
            // 
            // tbSignature
            // 
            this.tbSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSignature.Location = new System.Drawing.Point(222, 270);
            this.tbSignature.Name = "tbSignature";
            this.tbSignature.Size = new System.Drawing.Size(100, 21);
            this.tbSignature.TabIndex = 43;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(163, 273);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 42;
            this.label17.Text = "经办人：";
            // 
            // dtpSignDate
            // 
            this.dtpSignDate.CalendarMonthBackground = System.Drawing.SystemColors.InactiveBorder;
            this.dtpSignDate.CalendarTitleForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.dtpSignDate.Enabled = false;
            this.dtpSignDate.Location = new System.Drawing.Point(222, 297);
            this.dtpSignDate.Name = "dtpSignDate";
            this.dtpSignDate.Size = new System.Drawing.Size(109, 21);
            this.dtpSignDate.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "签单日期：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(36, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 73);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(127, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(365, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "民航飞机意外伤害：                人民币肆拾万元整          ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(305, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "火车、轮船意外伤害：                人民币贰万元整";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(127, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(305, 12);
            this.label11.TabIndex = 31;
            this.label11.Text = "汽车意外伤害：                      人民币壹万元整";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "保险金额：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbBeneficiary);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.tbCardID);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbName);
            this.groupBox2.Controls.Add(this.dtpBegin);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.dtpEnd);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(36, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(658, 190);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(347, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "被保人护照号码：";
            // 
            // tbBeneficiary
            // 
            this.tbBeneficiary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBeneficiary.Location = new System.Drawing.Point(65, 159);
            this.tbBeneficiary.Name = "tbBeneficiary";
            this.tbBeneficiary.Size = new System.Drawing.Size(100, 21);
            this.tbBeneficiary.TabIndex = 44;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(184, 163);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 12);
            this.label16.TabIndex = 41;
            this.label16.Text = "（未指定则为法定继承人）";
            // 
            // tbCardID
            // 
            this.tbCardID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCardID.Location = new System.Drawing.Point(454, 15);
            this.tbCardID.Name = "tbCardID";
            this.tbCardID.Size = new System.Drawing.Size(202, 21);
            this.tbCardID.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "被保人姓名：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 165);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 40;
            this.label15.Text = "受益人：";
            // 
            // cbName
            // 
            this.cbName.FormattingEnabled = true;
            this.cbName.Location = new System.Drawing.Point(92, 14);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(236, 20);
            this.cbName.TabIndex = 29;
            this.cbName.SelectedIndexChanged += new System.EventHandler(this.cbName_SelectedIndexChanged);
            // 
            // dtpBegin
            // 
            this.dtpBegin.Location = new System.Drawing.Point(160, 40);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(126, 21);
            this.dtpBegin.TabIndex = 37;
            this.dtpBegin.ValueChanged += new System.EventHandler(this.dtpBegin_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 143);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(533, 12);
            this.label14.TabIndex = 39;
            this.label14.Text = "保险费：           人民币          贰拾          元整          RMB￥      20          元";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(342, 40);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(122, 21);
            this.dtpEnd.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(521, 12);
            this.label13.TabIndex = 36;
            this.label13.Text = "保险期限：　十天　　　自　　　　　　　　　　　零时起至　　　　　　　　　　　二十四时止";
            // 
            // bt_Exit
            // 
            this.bt_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Exit.Location = new System.Drawing.Point(652, 340);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(86, 52);
            this.bt_Exit.TabIndex = 49;
            this.bt_Exit.Text = "退出(&X)";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(81, 371);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(53, 21);
            this.numericUpDownY.TabIndex = 51;
            // 
            // btSubmit
            // 
            this.btSubmit.Location = new System.Drawing.Point(426, 340);
            this.btSubmit.Name = "btSubmit";
            this.btSubmit.Size = new System.Drawing.Size(86, 52);
            this.btSubmit.TabIndex = 47;
            this.btSubmit.Text = "上传(&S)";
            this.btSubmit.UseVisualStyleBackColor = true;
            this.btSubmit.Click += new System.EventHandler(this.btSubmit_Click);
            // 
            // bt_Print
            // 
            this.bt_Print.Location = new System.Drawing.Point(534, 340);
            this.bt_Print.Name = "bt_Print";
            this.bt_Print.Size = new System.Drawing.Size(86, 52);
            this.bt_Print.TabIndex = 48;
            this.bt_Print.Text = "打印(&P)";
            this.bt_Print.UseVisualStyleBackColor = true;
            this.bt_Print.Click += new System.EventHandler(this.bt_Print_Click);
            // 
            // bt_SetToDefault
            // 
            this.bt_SetToDefault.Location = new System.Drawing.Point(141, 340);
            this.bt_SetToDefault.Name = "bt_SetToDefault";
            this.bt_SetToDefault.Size = new System.Drawing.Size(86, 52);
            this.bt_SetToDefault.TabIndex = 46;
            this.bt_SetToDefault.Text = "设定默认值";
            this.bt_SetToDefault.UseVisualStyleBackColor = true;
            this.bt_SetToDefault.Click += new System.EventHandler(this.bt_SetToDefault_Click);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(82, 344);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(53, 21);
            this.numericUpDownX.TabIndex = 50;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(26, 348);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 52;
            this.label20.Text = "左边距：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(26, 375);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 53;
            this.label21.Text = "上边距：";
            // 
            // ptDoc
            // 
            this.ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ptDoc_PrintPage);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 52);
            this.button1.TabIndex = 54;
            this.button1.Text = "测试打印(&T)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NewChina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_Exit;
            this.ClientSize = new System.Drawing.Size(763, 398);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.numericUpDownY);
            this.Controls.Add(this.btSubmit);
            this.Controls.Add(this.bt_Print);
            this.Controls.Add(this.bt_SetToDefault);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numericUpDownX);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.MaximizeBox = false;
            this.Name = "NewChina";
            this.Text = "新华人寿";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewChina_FormClosed);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NewChina_MouseClick);
            this.Load += new System.EventHandler(this.NewChina_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPnr;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbInsuranceNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbENumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbSignature;
        private System.Windows.Forms.TextBox tbBeneficiary;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpSignDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCardID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Button btSubmit;
        private System.Windows.Forms.Button bt_Print;
        private System.Windows.Forms.Button bt_SetToDefault;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label3;
        private System.Drawing.Printing.PrintDocument ptDoc;
        private System.Windows.Forms.Button button1;
    }
}