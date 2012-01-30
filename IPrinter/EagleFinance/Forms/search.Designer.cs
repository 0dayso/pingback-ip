namespace EagleFinance
{
    partial class search
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
            this.tbTicketNumberBeg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTicketNumberEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpSaleEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpIncomeEnd = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAirlineCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbOrig = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbDest = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPNR = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpSaleBeg = new System.Windows.Forms.DateTimePicker();
            this.dtpIncomeBeg = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbUserAccount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbAgentName = new System.Windows.Forms.TextBox();
            this.tbOffice = new System.Windows.Forms.TextBox();
            this.cbSaleDate = new System.Windows.Forms.CheckBox();
            this.cbIncomeDate = new System.Windows.Forms.CheckBox();
            this.btClear = new System.Windows.Forms.Button();
            this.btSearch = new System.Windows.Forms.Button();
            this.cbOrderBy = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbGroupBy = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cbSearchField = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "OFFICE：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "起始票号(10位)";
            // 
            // tbTicketNumberBeg
            // 
            this.tbTicketNumberBeg.Location = new System.Drawing.Point(534, 0);
            this.tbTicketNumberBeg.Name = "tbTicketNumberBeg";
            this.tbTicketNumberBeg.Size = new System.Drawing.Size(111, 21);
            this.tbTicketNumberBeg.TabIndex = 4;
            this.tbTicketNumberBeg.TextChanged += new System.EventHandler(this.tbTicketNumberBeg_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "终止票号(10位)";
            // 
            // tbTicketNumberEnd
            // 
            this.tbTicketNumberEnd.Location = new System.Drawing.Point(534, 27);
            this.tbTicketNumberEnd.Name = "tbTicketNumberEnd";
            this.tbTicketNumberEnd.Size = new System.Drawing.Size(111, 21);
            this.tbTicketNumberEnd.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "入库起始日期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "入库截止日期";
            // 
            // dtpSaleEnd
            // 
            this.dtpSaleEnd.Location = new System.Drawing.Point(322, -1);
            this.dtpSaleEnd.Name = "dtpSaleEnd";
            this.dtpSaleEnd.Size = new System.Drawing.Size(111, 21);
            this.dtpSaleEnd.TabIndex = 1;
            // 
            // dtpIncomeEnd
            // 
            this.dtpIncomeEnd.Location = new System.Drawing.Point(322, 26);
            this.dtpIncomeEnd.Name = "dtpIncomeEnd";
            this.dtpIncomeEnd.Size = new System.Drawing.Size(111, 21);
            this.dtpIncomeEnd.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "结算号";
            // 
            // tbAirlineCode
            // 
            this.tbAirlineCode.Location = new System.Drawing.Point(146, 161);
            this.tbAirlineCode.Name = "tbAirlineCode";
            this.tbAirlineCode.Size = new System.Drawing.Size(304, 21);
            this.tbAirlineCode.TabIndex = 10;
            this.tbAirlineCode.Text = "多个用逗号隔开";
            this.tbAirlineCode.Enter += new System.EventHandler(this.tbAirlineCode_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "出发地（三字码）";
            // 
            // tbOrig
            // 
            this.tbOrig.Location = new System.Drawing.Point(146, 53);
            this.tbOrig.Name = "tbOrig";
            this.tbOrig.Size = new System.Drawing.Size(499, 21);
            this.tbOrig.TabIndex = 6;
            this.tbOrig.Text = "多个用逗号隔开";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "到达地（三字码）";
            // 
            // tbDest
            // 
            this.tbDest.Location = new System.Drawing.Point(146, 80);
            this.tbDest.Name = "tbDest";
            this.tbDest.Size = new System.Drawing.Size(499, 21);
            this.tbDest.TabIndex = 7;
            this.tbDest.Text = "多个用逗号隔开";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "PNR";
            // 
            // tbPNR
            // 
            this.tbPNR.Location = new System.Drawing.Point(146, 107);
            this.tbPNR.Name = "tbPNR";
            this.tbPNR.Size = new System.Drawing.Size(499, 21);
            this.tbPNR.TabIndex = 8;
            this.tbPNR.Text = "多个用逗号隔开";
            this.tbPNR.Enter += new System.EventHandler(this.tbPNR_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "销售起始日期";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(239, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "销售截止日期";
            // 
            // dtpSaleBeg
            // 
            this.dtpSaleBeg.Location = new System.Drawing.Point(122, -1);
            this.dtpSaleBeg.Name = "dtpSaleBeg";
            this.dtpSaleBeg.Size = new System.Drawing.Size(111, 21);
            this.dtpSaleBeg.TabIndex = 0;
            // 
            // dtpIncomeBeg
            // 
            this.dtpIncomeBeg.Location = new System.Drawing.Point(122, 26);
            this.dtpIncomeBeg.Name = "dtpIncomeBeg";
            this.dtpIncomeBeg.Size = new System.Drawing.Size(111, 21);
            this.dtpIncomeBeg.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(456, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 46);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(126, 20);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(48, 16);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "退票";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(72, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "废票";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "正常票";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 192);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "出票操作员帐号";
            // 
            // tbUserAccount
            // 
            this.tbUserAccount.Location = new System.Drawing.Point(146, 188);
            this.tbUserAccount.Name = "tbUserAccount";
            this.tbUserAccount.Size = new System.Drawing.Size(418, 21);
            this.tbUserAccount.TabIndex = 11;
            this.tbUserAccount.Text = "多个用逗号隔开";
            this.tbUserAccount.Enter += new System.EventHandler(this.tbUserAccount_Enter);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(39, 219);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "出票操作员中文名";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(146, 215);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(418, 21);
            this.tbUserName.TabIndex = 12;
            this.tbUserName.Text = "多个用逗号隔开";
            this.tbUserName.Enter += new System.EventHandler(this.tbUserName_Enter);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 245);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "出票代理商名";
            // 
            // tbAgentName
            // 
            this.tbAgentName.Location = new System.Drawing.Point(146, 240);
            this.tbAgentName.Name = "tbAgentName";
            this.tbAgentName.Size = new System.Drawing.Size(418, 21);
            this.tbAgentName.TabIndex = 13;
            this.tbAgentName.Text = "多个用逗号隔开";
            this.tbAgentName.Enter += new System.EventHandler(this.tbAgentName_Enter);
            // 
            // tbOffice
            // 
            this.tbOffice.Location = new System.Drawing.Point(146, 134);
            this.tbOffice.Name = "tbOffice";
            this.tbOffice.Size = new System.Drawing.Size(304, 21);
            this.tbOffice.TabIndex = 9;
            this.tbOffice.Text = "多个用逗号隔开";
            this.tbOffice.Enter += new System.EventHandler(this.tbOffice_Enter);
            // 
            // cbSaleDate
            // 
            this.cbSaleDate.AutoSize = true;
            this.cbSaleDate.Location = new System.Drawing.Point(18, 3);
            this.cbSaleDate.Name = "cbSaleDate";
            this.cbSaleDate.Size = new System.Drawing.Size(15, 14);
            this.cbSaleDate.TabIndex = 6;
            this.cbSaleDate.UseVisualStyleBackColor = true;
            // 
            // cbIncomeDate
            // 
            this.cbIncomeDate.AutoSize = true;
            this.cbIncomeDate.Location = new System.Drawing.Point(18, 29);
            this.cbIncomeDate.Name = "cbIncomeDate";
            this.cbIncomeDate.Size = new System.Drawing.Size(15, 14);
            this.cbIncomeDate.TabIndex = 6;
            this.cbIncomeDate.UseVisualStyleBackColor = true;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(570, 266);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 18;
            this.btClear.Text = "清空";
            this.btClear.UseVisualStyleBackColor = true;
            // 
            // btSearch
            // 
            this.btSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSearch.Location = new System.Drawing.Point(486, 265);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 17;
            this.btSearch.Text = "查询";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // cbOrderBy
            // 
            this.cbOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderBy.FormattingEnabled = true;
            this.cbOrderBy.Items.AddRange(new object[] {
            "十位票号",
            "OFFICE",
            "入库日期",
            "电子客票号",
            "出发地到达地",
            "票面价",
            "PNR",
            "销售日期",
            "状态标志",
            "Eagle帐户名",
            "Eagle帐户中文名",
            "Eagle代理商名"});
            this.cbOrderBy.Location = new System.Drawing.Point(146, 267);
            this.cbOrderBy.Name = "cbOrderBy";
            this.cbOrderBy.Size = new System.Drawing.Size(121, 20);
            this.cbOrderBy.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 270);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "排序方式";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(273, 270);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "分组方式";
            // 
            // cbGroupBy
            // 
            this.cbGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupBy.FormattingEnabled = true;
            this.cbGroupBy.Items.AddRange(new object[] {
            "十位票号",
            "OFFICE",
            "入库日期",
            "电子客票号",
            "出发地到达地",
            "票面价",
            "PNR",
            "销售日期",
            "状态标志",
            "Eagle帐户名",
            "Eagle帐户中文名",
            "Eagle代理商名"});
            this.cbGroupBy.Location = new System.Drawing.Point(356, 267);
            this.cbGroupBy.Name = "cbGroupBy";
            this.cbGroupBy.Size = new System.Drawing.Size(121, 20);
            this.cbGroupBy.TabIndex = 15;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(39, 296);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "搜索范围";
            // 
            // cbSearchField
            // 
            this.cbSearchField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchField.FormattingEnabled = true;
            this.cbSearchField.Items.AddRange(new object[] {
            "全部入库票",
            "全部已经售出票",
            "售出并已完整",
            "售出但未完整"});
            this.cbSearchField.Location = new System.Drawing.Point(146, 293);
            this.cbSearchField.Name = "cbSearchField";
            this.cbSearchField.Size = new System.Drawing.Size(121, 20);
            this.cbSearchField.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(570, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "……";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(570, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "……";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(570, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "……";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 324);
            this.Controls.Add(this.cbGroupBy);
            this.Controls.Add(this.cbSearchField);
            this.Controls.Add(this.cbOrderBy);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.cbIncomeDate);
            this.Controls.Add(this.cbSaleDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpIncomeBeg);
            this.Controls.Add(this.dtpIncomeEnd);
            this.Controls.Add(this.dtpSaleBeg);
            this.Controls.Add(this.dtpSaleEnd);
            this.Controls.Add(this.tbTicketNumberEnd);
            this.Controls.Add(this.tbAgentName);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.tbUserAccount);
            this.Controls.Add(this.tbOffice);
            this.Controls.Add(this.tbPNR);
            this.Controls.Add(this.tbDest);
            this.Controls.Add(this.tbOrig);
            this.Controls.Add(this.tbAirlineCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.tbTicketNumberBeg);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "search";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查询";
            this.Load += new System.EventHandler(this.search_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTicketNumberEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpSaleEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbOrig;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbDest;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbPNR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpSaleBeg;
        private System.Windows.Forms.DateTimePicker dtpIncomeBeg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbUserAccount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbAgentName;
        private System.Windows.Forms.TextBox tbOffice;
        private System.Windows.Forms.CheckBox cbSaleDate;
        private System.Windows.Forms.CheckBox cbIncomeDate;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox tbTicketNumberBeg;
        private System.Windows.Forms.DateTimePicker dtpIncomeEnd;
        private System.Windows.Forms.TextBox tbAirlineCode;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.ComboBox cbOrderBy;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbGroupBy;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbSearchField;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}