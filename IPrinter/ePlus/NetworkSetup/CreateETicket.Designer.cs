namespace ePlus
{
    partial class CreateETicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateETicket));
            this.label1 = new System.Windows.Forms.Label();
            this.btGetPat = new System.Windows.Forms.Button();
            this.tbPnr = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLocalCity = new System.Windows.Forms.TextBox();
            this.cbRestrictions = new System.Windows.Forms.ComboBox();
            this.nPrinter = new System.Windows.Forms.NumericUpDown();
            this.nSegment = new System.Windows.Forms.NumericUpDown();
            this.nPeople = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbRTPNR = new System.Windows.Forms.TextBox();
            this.tbPAT = new System.Windows.Forms.TextBox();
            this.tbRRA = new System.Windows.Forms.TextBox();
            this.tbRRB = new System.Windows.Forms.TextBox();
            this.tbRRC = new System.Windows.Forms.TextBox();
            this.tbRRD = new System.Windows.Forms.TextBox();
            this.tbATWUH = new System.Windows.Forms.TextBox();
            this.tbEI = new System.Windows.Forms.TextBox();
            this.tbETDZ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btPATCH = new System.Windows.Forms.Button();
            this.btPATA = new System.Windows.Forms.Button();
            this.bt查看PNR = new System.Windows.Forms.Button();
            this.btAutoETDZ = new System.Windows.Forms.Button();
            this.cbAutoPat = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.isPATed = new System.Windows.Forms.CheckBox();
            this.tbPNR2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nPrinter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSegment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPeople)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "编码：";
            // 
            // btGetPat
            // 
            this.btGetPat.Location = new System.Drawing.Point(210, 57);
            this.btGetPat.Name = "btGetPat";
            this.btGetPat.Size = new System.Drawing.Size(53, 23);
            this.btGetPat.TabIndex = 2;
            this.btGetPat.Text = "PAT:";
            this.btGetPat.UseVisualStyleBackColor = true;
            this.btGetPat.Click += new System.EventHandler(this.btGetPat_Click);
            // 
            // tbPnr
            // 
            this.tbPnr.Location = new System.Drawing.Point(53, 59);
            this.tbPnr.Name = "tbPnr";
            this.tbPnr.Size = new System.Drawing.Size(66, 21);
            this.tbPnr.TabIndex = 0;
            this.tbPnr.TextChanged += new System.EventHandler(this.tbPnr_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbLocalCity);
            this.groupBox1.Controls.Add(this.cbRestrictions);
            this.groupBox1.Controls.Add(this.nPrinter);
            this.groupBox1.Controls.Add(this.nSegment);
            this.groupBox1.Controls.Add(this.nPeople);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 158);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "确认项";
            // 
            // tbLocalCity
            // 
            this.tbLocalCity.Location = new System.Drawing.Point(66, 97);
            this.tbLocalCity.Name = "tbLocalCity";
            this.tbLocalCity.Size = new System.Drawing.Size(120, 21);
            this.tbLocalCity.TabIndex = 3;
            this.tbLocalCity.TextChanged += new System.EventHandler(this.tbLocalCity_TextChanged);
            // 
            // cbRestrictions
            // 
            this.cbRestrictions.FormattingEnabled = true;
            this.cbRestrictions.Location = new System.Drawing.Point(9, 124);
            this.cbRestrictions.Name = "cbRestrictions";
            this.cbRestrictions.Size = new System.Drawing.Size(178, 20);
            this.cbRestrictions.TabIndex = 4;
            this.cbRestrictions.TextChanged += new System.EventHandler(this.cbRestrictions_TextChanged);
            // 
            // nPrinter
            // 
            this.nPrinter.Location = new System.Drawing.Point(66, 71);
            this.nPrinter.Name = "nPrinter";
            this.nPrinter.Size = new System.Drawing.Size(120, 21);
            this.nPrinter.TabIndex = 2;
            this.nPrinter.Click += new System.EventHandler(this.nPrinter_Click);
            // 
            // nSegment
            // 
            this.nSegment.Location = new System.Drawing.Point(66, 44);
            this.nSegment.Name = "nSegment";
            this.nSegment.Size = new System.Drawing.Size(120, 21);
            this.nSegment.TabIndex = 1;
            this.nSegment.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nSegment.ValueChanged += new System.EventHandler(this.nSegment_ValueChanged);
            // 
            // nPeople
            // 
            this.nPeople.Location = new System.Drawing.Point(66, 17);
            this.nPeople.Name = "nPeople";
            this.nPeople.Size = new System.Drawing.Size(120, 21);
            this.nPeople.TabIndex = 0;
            this.nPeople.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nPeople.ValueChanged += new System.EventHandler(this.nPeople_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "取消时限";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "打印机号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "航段数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "旅客人数";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(8, 251);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "执行出票";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(129, 251);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbRTPNR
            // 
            this.tbRTPNR.Location = new System.Drawing.Point(396, 58);
            this.tbRTPNR.Name = "tbRTPNR";
            this.tbRTPNR.Size = new System.Drawing.Size(54, 21);
            this.tbRTPNR.TabIndex = 8;
            // 
            // tbPAT
            // 
            this.tbPAT.Location = new System.Drawing.Point(210, 80);
            this.tbPAT.Multiline = true;
            this.tbPAT.Name = "tbPAT";
            this.tbPAT.Size = new System.Drawing.Size(335, 128);
            this.tbPAT.TabIndex = 9;
            this.tbPAT.Text = resources.GetString("tbPAT.Text");
            this.tbPAT.Enter += new System.EventHandler(this.tbPAT_Enter);
            // 
            // tbRRA
            // 
            this.tbRRA.Location = new System.Drawing.Point(210, 207);
            this.tbRRA.Name = "tbRRA";
            this.tbRRA.Size = new System.Drawing.Size(56, 21);
            this.tbRRA.TabIndex = 10;
            // 
            // tbRRB
            // 
            this.tbRRB.Location = new System.Drawing.Point(272, 207);
            this.tbRRB.Name = "tbRRB";
            this.tbRRB.Size = new System.Drawing.Size(56, 21);
            this.tbRRB.TabIndex = 11;
            // 
            // tbRRC
            // 
            this.tbRRC.Location = new System.Drawing.Point(334, 207);
            this.tbRRC.Name = "tbRRC";
            this.tbRRC.Size = new System.Drawing.Size(56, 21);
            this.tbRRC.TabIndex = 12;
            // 
            // tbRRD
            // 
            this.tbRRD.Location = new System.Drawing.Point(396, 207);
            this.tbRRD.Name = "tbRRD";
            this.tbRRD.Size = new System.Drawing.Size(56, 21);
            this.tbRRD.TabIndex = 13;
            // 
            // tbATWUH
            // 
            this.tbATWUH.Location = new System.Drawing.Point(458, 207);
            this.tbATWUH.Name = "tbATWUH";
            this.tbATWUH.Size = new System.Drawing.Size(87, 21);
            this.tbATWUH.TabIndex = 14;
            // 
            // tbEI
            // 
            this.tbEI.Location = new System.Drawing.Point(210, 227);
            this.tbEI.Name = "tbEI";
            this.tbEI.Size = new System.Drawing.Size(335, 21);
            this.tbEI.TabIndex = 15;
            // 
            // tbETDZ
            // 
            this.tbETDZ.Location = new System.Drawing.Point(210, 252);
            this.tbETDZ.Name = "tbETDZ";
            this.tbETDZ.Size = new System.Drawing.Size(56, 21);
            this.tbETDZ.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "可直接修改以上内容以适于出票\r\n若不需要该项，设为空即可";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(456, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "不适用于团队票";
            // 
            // btPATCH
            // 
            this.btPATCH.Location = new System.Drawing.Point(271, 57);
            this.btPATCH.Name = "btPATCH";
            this.btPATCH.Size = new System.Drawing.Size(57, 23);
            this.btPATCH.TabIndex = 3;
            this.btPATCH.Text = "PAT:*CH";
            this.btPATCH.UseVisualStyleBackColor = true;
            this.btPATCH.Click += new System.EventHandler(this.btPATCH_Click);
            // 
            // btPATA
            // 
            this.btPATA.Location = new System.Drawing.Point(335, 57);
            this.btPATA.Name = "btPATA";
            this.btPATA.Size = new System.Drawing.Size(57, 23);
            this.btPATA.TabIndex = 4;
            this.btPATA.Text = "PAT:A";
            this.btPATA.UseVisualStyleBackColor = true;
            this.btPATA.Click += new System.EventHandler(this.btPATA_Click);
            // 
            // bt查看PNR
            // 
            this.bt查看PNR.Location = new System.Drawing.Point(125, 57);
            this.bt查看PNR.Name = "bt查看PNR";
            this.bt查看PNR.Size = new System.Drawing.Size(75, 23);
            this.bt查看PNR.TabIndex = 1;
            this.bt查看PNR.Text = "查看PNR";
            this.bt查看PNR.UseVisualStyleBackColor = true;
            this.bt查看PNR.Click += new System.EventHandler(this.bt查看PNR_Click);
            // 
            // btAutoETDZ
            // 
            this.btAutoETDZ.Location = new System.Drawing.Point(451, 251);
            this.btAutoETDZ.Name = "btAutoETDZ";
            this.btAutoETDZ.Size = new System.Drawing.Size(94, 23);
            this.btAutoETDZ.TabIndex = 17;
            this.btAutoETDZ.Text = "一键自动出票";
            this.btAutoETDZ.UseVisualStyleBackColor = true;
            this.btAutoETDZ.Click += new System.EventHandler(this.btAutoETDZ_Click);
            // 
            // cbAutoPat
            // 
            this.cbAutoPat.AutoSize = true;
            this.cbAutoPat.Location = new System.Drawing.Point(451, 280);
            this.cbAutoPat.Name = "cbAutoPat";
            this.cbAutoPat.Size = new System.Drawing.Size(90, 16);
            this.cbAutoPat.TabIndex = 18;
            this.cbAutoPat.Text = "是否自动PAT";
            this.cbAutoPat.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.isPATed);
            this.groupBox2.Controls.Add(this.tbPNR2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(8, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 47);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "一键出票";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(335, 25);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(77, 16);
            this.radioButton6.TabIndex = 7;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "PAT:#3UZZ";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(252, 25);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(77, 16);
            this.radioButton4.TabIndex = 5;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "PAT:#YZZS";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "ETDZ出票";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btAutoETDZ_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(335, 10);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(83, 16);
            this.radioButton5.TabIndex = 6;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "PAT:#MUYTR";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(252, 10);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(65, 16);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "PAT:*CH";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(193, 25);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(53, 16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "PAT:A";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(193, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "PAT:";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // isPATed
            // 
            this.isPATed.AutoSize = true;
            this.isPATed.Checked = true;
            this.isPATed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isPATed.Location = new System.Drawing.Point(121, 20);
            this.isPATed.Name = "isPATed";
            this.isPATed.Size = new System.Drawing.Size(66, 16);
            this.isPATed.TabIndex = 1;
            this.isPATed.Text = "已做PAT";
            this.isPATed.UseVisualStyleBackColor = true;
            this.isPATed.CheckedChanged += new System.EventHandler(this.isPATed_CheckedChanged);
            // 
            // tbPNR2
            // 
            this.tbPNR2.Location = new System.Drawing.Point(45, 17);
            this.tbPNR2.Name = "tbPNR2";
            this.tbPNR2.Size = new System.Drawing.Size(66, 21);
            this.tbPNR2.TabIndex = 0;
            this.tbPNR2.TextChanged += new System.EventHandler(this.tbPnr_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "编码：";
            // 
            // CreateETicket
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(553, 297);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbAutoPat);
            this.Controls.Add(this.btAutoETDZ);
            this.Controls.Add(this.bt查看PNR);
            this.Controls.Add(this.tbPAT);
            this.Controls.Add(this.tbATWUH);
            this.Controls.Add(this.tbRRD);
            this.Controls.Add(this.tbRRC);
            this.Controls.Add(this.tbRRB);
            this.Controls.Add(this.tbETDZ);
            this.Controls.Add(this.tbRRA);
            this.Controls.Add(this.tbEI);
            this.Controls.Add(this.tbRTPNR);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbPnr);
            this.Controls.Add(this.btPATA);
            this.Controls.Add(this.btPATCH);
            this.Controls.Add(this.btGetPat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateETicket";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "一键出票 - 产生电子客票";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateETicket_FormClosed);
            this.Load += new System.EventHandler(this.CreateETicket_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nPrinter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSegment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPeople)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btGetPat;
        private System.Windows.Forms.TextBox tbPnr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nPrinter;
        private System.Windows.Forms.NumericUpDown nSegment;
        private System.Windows.Forms.NumericUpDown nPeople;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbRestrictions;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox tbLocalCity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbRTPNR;
        private System.Windows.Forms.TextBox tbPAT;
        private System.Windows.Forms.TextBox tbRRA;
        private System.Windows.Forms.TextBox tbRRB;
        private System.Windows.Forms.TextBox tbRRC;
        private System.Windows.Forms.TextBox tbRRD;
        private System.Windows.Forms.TextBox tbATWUH;
        private System.Windows.Forms.TextBox tbEI;
        private System.Windows.Forms.TextBox tbETDZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btPATCH;
        private System.Windows.Forms.Button btPATA;
        private System.Windows.Forms.Button bt查看PNR;
        private System.Windows.Forms.Button btAutoETDZ;
        private System.Windows.Forms.CheckBox cbAutoPat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPNR2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.CheckBox isPATed;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Button button1;
    }
}