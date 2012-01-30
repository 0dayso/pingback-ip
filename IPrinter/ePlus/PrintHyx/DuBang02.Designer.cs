namespace ePlus.PrintHyx
{
    partial class DuBang02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DuBang02));
            this.ptDoc = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpPrintTime = new System.Windows.Forms.DateTimePicker();
            this.tbPnr = new System.Windows.Forms.TextBox();
            this.tbSignature = new System.Windows.Forms.TextBox();
            this.tbPolicyNo = new System.Windows.Forms.TextBox();
            this.tbNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btFading = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.tbTimeEnd = new System.Windows.Forms.TextBox();
            this.dtpBeg = new System.Windows.Forms.DateTimePicker();
            this.tbTimeBeg = new System.Windows.Forms.TextBox();
            this.tbTerm = new System.Windows.Forms.TextBox();
            this.cbName = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbBeneficiary = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbCardID = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.bt_Print = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.bt_SetToDefault = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btPrintSeries = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // ptDoc
            // 
            this.ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ptDoc_PrintPage);
            this.ptDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.ptDoc_EndPrint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.dtpPrintTime);
            this.panel1.Controls.Add(this.tbPnr);
            this.panel1.Controls.Add(this.tbSignature);
            this.panel1.Controls.Add(this.tbPolicyNo);
            this.panel1.Controls.Add(this.tbNo);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(54, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 367);
            this.panel1.TabIndex = 0;
            // 
            // dtpPrintTime
            // 
            this.dtpPrintTime.Location = new System.Drawing.Point(675, 337);
            this.dtpPrintTime.Name = "dtpPrintTime";
            this.dtpPrintTime.Size = new System.Drawing.Size(126, 21);
            this.dtpPrintTime.TabIndex = 6;
            // 
            // tbPnr
            // 
            this.tbPnr.ForeColor = System.Drawing.Color.Blue;
            this.tbPnr.Location = new System.Drawing.Point(42, 66);
            this.tbPnr.Name = "tbPnr";
            this.tbPnr.Size = new System.Drawing.Size(65, 21);
            this.tbPnr.TabIndex = 5;
            this.tbPnr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPnr_KeyUp);
            // 
            // tbSignature
            // 
            this.tbSignature.Location = new System.Drawing.Point(109, 337);
            this.tbSignature.Name = "tbSignature";
            this.tbSignature.Size = new System.Drawing.Size(219, 21);
            this.tbSignature.TabIndex = 3;
            // 
            // tbPolicyNo
            // 
            this.tbPolicyNo.Location = new System.Drawing.Point(109, 95);
            this.tbPolicyNo.Name = "tbPolicyNo";
            this.tbPolicyNo.Size = new System.Drawing.Size(219, 21);
            this.tbPolicyNo.TabIndex = 3;
            // 
            // tbNo
            // 
            this.tbNo.Location = new System.Drawing.Point(557, 95);
            this.tbNo.Name = "tbNo";
            this.tbNo.Size = new System.Drawing.Size(244, 21);
            this.tbNo.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(347, 36);
            this.label8.TabIndex = 2;
            this.label8.Text = "民航飞机意外保险金额；人民币肆拾万元整　RMB￥400,000.00元\r\n火车轮船意外保险金额：人民币贰万元整　　RMB￥20,000.00元\r\n汽车意外保险金额" +
                "：　　人民币壹万元整　　RMB￥10,000.00元";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(591, 334);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 24);
            this.label14.TabIndex = 2;
            this.label14.Text = "打印时间：\r\nPRINT TIME：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 334);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 24);
            this.label13.TabIndex = 2;
            this.label13.Text = "经办：\r\nSIGNATURE：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(30, 287);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 24);
            this.label12.TabIndex = 2;
            this.label12.Text = "保险期限\r\nTERM OF INSURANCE";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 36);
            this.label7.TabIndex = 2;
            this.label7.Text = "保险金额\r\nINSURED\r\nAMOUNT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(353, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 24);
            this.label10.TabIndex = 2;
            this.label10.Text = "受益人姓名\r\nBENEFICIARY";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(213, 166);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 24);
            this.label11.TabIndex = 2;
            this.label11.Text = "人民币　贰拾元整\r\nRMB　　￥20.00元";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "保险费\r\nPREMIUM";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(353, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 24);
            this.label9.TabIndex = 2;
            this.label9.Text = "被保险人身份证件号码：\r\nID CARD NO.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "被保险人姓名\r\nINSURED";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(504, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 24);
            this.label15.TabIndex = 2;
            this.label15.Text = "NO.";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 69);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 12);
            this.label16.TabIndex = 2;
            this.label16.Text = "PNR";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "保单号\r\nPOLICY NO.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(202, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 43);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(269, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "DU-BANG PROPERTY && CASUALTY INSURANCE CO.,LTD.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(201, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(429, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "“出行无忧”综合保障计划保险单（自助式）";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(267, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "都邦财产保险股份有限公司";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(213, 287);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 24);
            this.label17.TabIndex = 2;
            this.label17.Text = "天（即自\r\nDAYS";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btFading);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.tbTimeEnd);
            this.groupBox1.Controls.Add(this.dtpBeg);
            this.groupBox1.Controls.Add(this.tbTimeBeg);
            this.groupBox1.Controls.Add(this.tbTerm);
            this.groupBox1.Controls.Add(this.cbName);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tbBeneficiary);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.tbCardID);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(793, 210);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btFading
            // 
            this.btFading.Location = new System.Drawing.Point(710, 47);
            this.btFading.Name = "btFading";
            this.btFading.Size = new System.Drawing.Size(46, 21);
            this.btFading.TabIndex = 7;
            this.btFading.Text = "法定";
            this.btFading.UseVisualStyleBackColor = true;
            this.btFading.Click += new System.EventHandler(this.btFading_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(465, 170);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(119, 21);
            this.dtpEnd.TabIndex = 6;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // tbTimeEnd
            // 
            this.tbTimeEnd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbTimeEnd.Location = new System.Drawing.Point(590, 171);
            this.tbTimeEnd.Name = "tbTimeEnd";
            this.tbTimeEnd.Size = new System.Drawing.Size(27, 21);
            this.tbTimeEnd.TabIndex = 5;
            this.tbTimeEnd.Text = "00";
            // 
            // dtpBeg
            // 
            this.dtpBeg.Location = new System.Drawing.Point(260, 170);
            this.dtpBeg.Name = "dtpBeg";
            this.dtpBeg.Size = new System.Drawing.Size(119, 21);
            this.dtpBeg.TabIndex = 6;
            this.dtpBeg.ValueChanged += new System.EventHandler(this.dtpBeg_ValueChanged);
            // 
            // tbTimeBeg
            // 
            this.tbTimeBeg.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbTimeBeg.Location = new System.Drawing.Point(385, 171);
            this.tbTimeBeg.Name = "tbTimeBeg";
            this.tbTimeBeg.Size = new System.Drawing.Size(27, 21);
            this.tbTimeBeg.TabIndex = 5;
            this.tbTimeBeg.Text = "00";
            // 
            // tbTerm
            // 
            this.tbTerm.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbTerm.Location = new System.Drawing.Point(130, 171);
            this.tbTerm.Name = "tbTerm";
            this.tbTerm.Size = new System.Drawing.Size(65, 21);
            this.tbTerm.TabIndex = 5;
            this.tbTerm.Leave += new System.EventHandler(this.tbTerm_Leave);
            // 
            // cbName
            // 
            this.cbName.FormattingEnabled = true;
            this.cbName.Location = new System.Drawing.Point(97, 11);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(219, 20);
            this.cbName.TabIndex = 5;
            this.cbName.SelectedIndexChanged += new System.EventHandler(this.cbName_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 65);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // tbBeneficiary
            // 
            this.tbBeneficiary.Location = new System.Drawing.Point(484, 47);
            this.tbBeneficiary.Name = "tbBeneficiary";
            this.tbBeneficiary.Size = new System.Drawing.Size(219, 21);
            this.tbBeneficiary.TabIndex = 3;
            this.tbBeneficiary.Text = "法定人";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(623, 173);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 12);
            this.label19.TabIndex = 2;
            this.label19.Text = "时止";
            // 
            // tbCardID
            // 
            this.tbCardID.Location = new System.Drawing.Point(484, 17);
            this.tbCardID.Name = "tbCardID";
            this.tbCardID.Size = new System.Drawing.Size(219, 21);
            this.tbCardID.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(418, 173);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 2;
            this.label18.Text = "时起至";
            // 
            // bt_Exit
            // 
            this.bt_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Exit.Location = new System.Drawing.Point(764, 385);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(86, 45);
            this.bt_Exit.TabIndex = 27;
            this.bt_Exit.Text = "退出(&X)";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // bt_Print
            // 
            this.bt_Print.Location = new System.Drawing.Point(641, 385);
            this.bt_Print.Name = "bt_Print";
            this.bt_Print.Size = new System.Drawing.Size(86, 45);
            this.bt_Print.TabIndex = 26;
            this.bt_Print.Text = "打印(&P)";
            this.bt_Print.UseVisualStyleBackColor = true;
            this.bt_Print.Click += new System.EventHandler(this.bt_Print_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(122, 409);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(53, 21);
            this.numericUpDown2.TabIndex = 24;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(123, 385);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(53, 21);
            this.numericUpDown1.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(68, 389);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 28;
            this.label20.Text = "左边距：";
            // 
            // bt_SetToDefault
            // 
            this.bt_SetToDefault.Location = new System.Drawing.Point(219, 385);
            this.bt_SetToDefault.Name = "bt_SetToDefault";
            this.bt_SetToDefault.Size = new System.Drawing.Size(86, 45);
            this.bt_SetToDefault.TabIndex = 25;
            this.bt_SetToDefault.Text = "设为默认值";
            this.bt_SetToDefault.UseVisualStyleBackColor = true;
            this.bt_SetToDefault.Click += new System.EventHandler(this.bt_SetToDefault_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(67, 412);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(53, 12);
            this.label21.TabIndex = 29;
            this.label21.Text = "上边距：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(549, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 45);
            this.button1.TabIndex = 36;
            this.button1.Text = "测试打印(&T)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btPrintSeries
            // 
            this.btPrintSeries.Location = new System.Drawing.Point(384, 385);
            this.btPrintSeries.Name = "btPrintSeries";
            this.btPrintSeries.Size = new System.Drawing.Size(94, 44);
            this.btPrintSeries.TabIndex = 37;
            this.btPrintSeries.Text = "连续打印";
            this.btPrintSeries.UseVisualStyleBackColor = true;
            this.btPrintSeries.Click += new System.EventHandler(this.btPrintSeries_Click);
            // 
            // DuBang02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 435);
            this.Controls.Add(this.btPrintSeries);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.bt_Print);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.bt_SetToDefault);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DuBang02";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "都邦－出行无忧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DuBang02_FormClosed);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DuBang02_MouseClick);
            this.Load += new System.EventHandler(this.DuBang02_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument ptDoc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbNo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpPrintTime;
        private System.Windows.Forms.TextBox tbPnr;
        private System.Windows.Forms.TextBox tbSignature;
        private System.Windows.Forms.TextBox tbPolicyNo;
        private System.Windows.Forms.TextBox tbTerm;
        private System.Windows.Forms.ComboBox cbName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbBeneficiary;
        private System.Windows.Forms.TextBox tbCardID;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.TextBox tbTimeEnd;
        private System.Windows.Forms.DateTimePicker dtpBeg;
        private System.Windows.Forms.TextBox tbTimeBeg;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Button bt_Print;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button bt_SetToDefault;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btFading;
        private System.Windows.Forms.Button btPrintSeries;

    }
}