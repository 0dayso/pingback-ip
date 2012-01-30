namespace ePlus.PrintHyx
{
    partial class PrintPICC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPICC));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_eNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_No = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_cardType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_cardNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_Signture = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtp2 = new System.Windows.Forms.DateTimePicker();
            this.dtp_Date = new System.Windows.Forms.DateTimePicker();
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.cb_name = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_RandomNo = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.bt_SetToDefault = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.bt_Print = new System.Windows.Forms.Button();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.ptDoc = new System.Drawing.Printing.PrintDocument();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            //this.pictureBox1.Image = global::ePlus.Properties.Resources.PICC_ICO;
            this.pictureBox1.Location = new System.Drawing.Point(47, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 44);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(273, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "航空旅客权益告知单";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(112, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "电子单号(ELECTRONIC NUMBER):";
            // 
            // tb_eNumber
            // 
            this.tb_eNumber.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_eNumber.Location = new System.Drawing.Point(216, 63);
            this.tb_eNumber.Name = "tb_eNumber";
            this.tb_eNumber.Size = new System.Drawing.Size(69, 21);
            this.tb_eNumber.TabIndex = 0;
            this.tb_eNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_eNumber_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(484, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "NO.:";
            // 
            // tb_No
            // 
            this.tb_No.ForeColor = System.Drawing.Color.Red;
            this.tb_No.Location = new System.Drawing.Point(519, 102);
            this.tb_No.Name = "tb_No";
            this.tb_No.Size = new System.Drawing.Size(162, 21);
            this.tb_No.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(112, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "姓名:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label5.Location = new System.Drawing.Point(248, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "证件种类:";
            // 
            // cb_cardType
            // 
            this.cb_cardType.FormattingEnabled = true;
            this.cb_cardType.Items.AddRange(new object[] {
            "身份证"});
            this.cb_cardType.Location = new System.Drawing.Point(310, 129);
            this.cb_cardType.Name = "cb_cardType";
            this.cb_cardType.Size = new System.Drawing.Size(118, 20);
            this.cb_cardType.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(434, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "证件号码:";
            // 
            // tb_cardNo
            // 
            this.tb_cardNo.Location = new System.Drawing.Point(499, 129);
            this.tb_cardNo.Name = "tb_cardNo";
            this.tb_cardNo.Size = new System.Drawing.Size(182, 21);
            this.tb_cardNo.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(112, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(427, 50);
            this.label7.TabIndex = 2;
            this.label7.Text = "旅客权益：\r\n１、免费预订机票，代订目的地酒店\r\n２、免费订送车辆保险及强制三者险（限市内）\r\n３、您在我公司购买的航空机票已经由中国人民财产保险股份有限公司责任" +
                "承保\r\n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label8.Location = new System.Drawing.Point(112, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(335, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "承保项目   飞机     人民币四十万元整   RMB￥400000.00元";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label9.Location = new System.Drawing.Point(112, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "保险期间    三天   （即自";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label10.Location = new System.Drawing.Point(112, 276);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(245, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "经办人签名（盖章）　　  　　　  经办日期\r\n";
            // 
            // tb_Signture
            // 
            this.tb_Signture.Location = new System.Drawing.Point(218, 271);
            this.tb_Signture.Name = "tb_Signture";
            this.tb_Signture.Size = new System.Drawing.Size(86, 21);
            this.tb_Signture.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.dtp2);
            this.panel1.Controls.Add(this.dtp_Date);
            this.panel1.Controls.Add(this.dtp1);
            this.panel1.Controls.Add(this.cb_name);
            this.panel1.Controls.Add(this.tb_eNumber);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.tb_RandomNo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(67, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 296);
            this.panel1.TabIndex = 5;
            // 
            // dtp2
            // 
            this.dtp2.Location = new System.Drawing.Point(407, 237);
            this.dtp2.Name = "dtp2";
            this.dtp2.Size = new System.Drawing.Size(138, 21);
            this.dtp2.TabIndex = 11;
            // 
            // dtp_Date
            // 
            this.dtp_Date.Location = new System.Drawing.Point(294, 266);
            this.dtp_Date.Name = "dtp_Date";
            this.dtp_Date.Size = new System.Drawing.Size(145, 21);
            this.dtp_Date.TabIndex = 11;
            this.dtp_Date.ValueChanged += new System.EventHandler(this.dtp1_ValueChanged);
            // 
            // dtp1
            // 
            this.dtp1.Location = new System.Drawing.Point(203, 237);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(138, 21);
            this.dtp1.TabIndex = 11;
            this.dtp1.ValueChanged += new System.EventHandler(this.dtp1_ValueChanged);
            // 
            // cb_name
            // 
            this.cb_name.FormattingEnabled = true;
            this.cb_name.Location = new System.Drawing.Point(86, 124);
            this.cb_name.Name = "cb_name";
            this.cb_name.Size = new System.Drawing.Size(86, 20);
            this.cb_name.TabIndex = 3;
            this.cb_name.SelectedIndexChanged += new System.EventHandler(this.cb_name_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label13.Location = new System.Drawing.Point(140, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "订座记录号:";
            // 
            // tb_RandomNo
            // 
            this.tb_RandomNo.ForeColor = System.Drawing.Color.Red;
            this.tb_RandomNo.Location = new System.Drawing.Point(218, 97);
            this.tb_RandomNo.Name = "tb_RandomNo";
            this.tb_RandomNo.ReadOnly = true;
            this.tb_RandomNo.Size = new System.Drawing.Size(193, 21);
            this.tb_RandomNo.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label15.Location = new System.Drawing.Point(555, 241);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "二十四时止）";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.label14.Location = new System.Drawing.Point(346, 241);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "零时起至";
            // 
            // bt_SetToDefault
            // 
            this.bt_SetToDefault.Location = new System.Drawing.Point(218, 309);
            this.bt_SetToDefault.Name = "bt_SetToDefault";
            this.bt_SetToDefault.Size = new System.Drawing.Size(86, 52);
            this.bt_SetToDefault.TabIndex = 15;
            this.bt_SetToDefault.Text = "设为默认值";
            this.bt_SetToDefault.UseVisualStyleBackColor = true;
            this.bt_SetToDefault.Click += new System.EventHandler(this.bt_SetToDefault_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(121, 340);
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
            this.numericUpDown2.TabIndex = 18;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(122, 309);
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
            this.numericUpDown1.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(66, 313);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "横向偏移";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(66, 344);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "竖向偏移";
            // 
            // bt_Print
            // 
            this.bt_Print.Location = new System.Drawing.Point(519, 309);
            this.bt_Print.Name = "bt_Print";
            this.bt_Print.Size = new System.Drawing.Size(86, 52);
            this.bt_Print.TabIndex = 15;
            this.bt_Print.Text = "打印(&P)";
            this.bt_Print.UseVisualStyleBackColor = true;
            this.bt_Print.Click += new System.EventHandler(this.bt_Print_Click);
            // 
            // bt_Exit
            // 
            this.bt_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Exit.Location = new System.Drawing.Point(642, 309);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(86, 52);
            this.bt_Exit.TabIndex = 15;
            this.bt_Exit.Text = "退出(&X)";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // ptDoc
            // 
            this.ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ptDoc_PrintPage);
            this.ptDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.ptDoc_EndPrint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 52);
            this.button1.TabIndex = 20;
            this.button1.Text = "切换至任我游";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PrintPICC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_Exit;
            this.ClientSize = new System.Drawing.Size(792, 365);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.bt_Print);
            this.Controls.Add(this.bt_SetToDefault);
            this.Controls.Add(this.cb_cardType);
            this.Controls.Add(this.tb_No);
            this.Controls.Add(this.tb_cardNo);
            this.Controls.Add(this.tb_Signture);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PrintPICC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ＰＩＣＣ-权益告知单";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PrintPICC_FormClosed);
            this.Load += new System.EventHandler(this.PrintPICC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_eNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_No;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_cardType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_cardNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_Signture;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_name;
        private System.Windows.Forms.Button bt_SetToDefault;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button bt_Print;
        private System.Windows.Forms.Button bt_Exit;
        private System.Drawing.Printing.PrintDocument ptDoc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_RandomNo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtp2;
        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtp_Date;
    }
}