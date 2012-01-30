namespace ePlus.PrintHyx
{
    partial class EagleIns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EagleIns));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btSetTitleOffset = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbPrintTitle = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrice = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbPrice);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(14, 94);
            this.panel1.Size = new System.Drawing.Size(796, 179);
            this.panel1.TabIndex = 4;
            this.panel1.Tag = "9999";
            this.panel1.Controls.SetChildIndex(this.label2, 0);
            this.panel1.Controls.SetChildIndex(this.textBox1, 0);
            this.panel1.Controls.SetChildIndex(this.label1, 0);
            this.panel1.Controls.SetChildIndex(this.tb受益人资料, 0);
            this.panel1.Controls.SetChildIndex(this.tb受益人关系, 0);
            this.panel1.Controls.SetChildIndex(this.lb受益人关系, 0);
            this.panel1.Controls.SetChildIndex(this.lb受益人资料, 0);
            this.panel1.Controls.SetChildIndex(this.cb证件类型, 0);
            this.panel1.Controls.SetChildIndex(this.tb保险费, 0);
            this.panel1.Controls.SetChildIndex(this.lb保险费, 0);
            this.panel1.Controls.SetChildIndex(this.tb乘机日, 0);
            this.panel1.Controls.SetChildIndex(this.lb乘机日, 0);
            this.panel1.Controls.SetChildIndex(this.tb证件号, 0);
            this.panel1.Controls.SetChildIndex(this.lb有效身份证件号码, 0);
            this.panel1.Controls.SetChildIndex(this.lb保险金额, 0);
            this.panel1.Controls.SetChildIndex(this.tb航班号, 0);
            this.panel1.Controls.SetChildIndex(this.lb航班号, 0);
            this.panel1.Controls.SetChildIndex(this.cb被保险人姓名, 0);
            this.panel1.Controls.SetChildIndex(this.lb被保险人姓名, 0);
            this.panel1.Controls.SetChildIndex(this.tb保险终止时间, 0);
            this.panel1.Controls.SetChildIndex(this.tb保险起始时间, 0);
            this.panel1.Controls.SetChildIndex(this.dtp保险终止时间, 0);
            this.panel1.Controls.SetChildIndex(this.dtp保险起始时间, 0);
            this.panel1.Controls.SetChildIndex(this.tb保险天数, 0);
            this.panel1.Controls.SetChildIndex(this.lb保险天数, 0);
            this.panel1.Controls.SetChildIndex(this.lb保险起始时间, 0);
            this.panel1.Controls.SetChildIndex(this.lb保险终止时间, 0);
            this.panel1.Controls.SetChildIndex(this.tb保险金额, 0);
            this.panel1.Controls.SetChildIndex(this.bt取身份证号, 0);
            this.panel1.Controls.SetChildIndex(this.label3, 0);
            this.panel1.Controls.SetChildIndex(this.cbPrice, 0);
            // 
            // lb公司名称
            // 
            this.lb公司名称.Location = new System.Drawing.Point(278, 19);
            // 
            // lb保单标题
            // 
            this.lb保单标题.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb保单标题.ForeColor = System.Drawing.Color.Red;
            this.lb保单标题.Location = new System.Drawing.Point(307, 58);
            this.lb保单标题.Size = new System.Drawing.Size(149, 19);
            this.lb保单标题.Text = "易格商旅会员卡";
            // 
            // lb保单序号
            // 
            this.lb保单序号.Location = new System.Drawing.Point(10, 73);
            this.lb保单序号.Size = new System.Drawing.Size(41, 12);
            this.lb保单序号.Text = "微机码";
            // 
            // tb保单序号
            // 
            this.tb保单序号.Location = new System.Drawing.Point(710, 70);
            this.tb保单序号.Size = new System.Drawing.Size(97, 21);
            this.tb保单序号.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb保单序号_KeyUp);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Size = new System.Drawing.Size(198, 62);
            this.GroupBox2.TabIndex = 0;
            // 
            // lb险种条款
            // 
            this.lb险种条款.Location = new System.Drawing.Point(327, 336);
            this.lb险种条款.Visible = false;
            // 
            // cb险种条款
            // 
            this.cb险种条款.Location = new System.Drawing.Point(385, 333);
            this.cb险种条款.Visible = false;
            // 
            // lb保单号码
            // 
            this.lb保单号码.Location = new System.Drawing.Point(621, 73);
            this.lb保单号码.Text = "单证号码";
            // 
            // tb保单号码
            // 
            this.tb保单号码.HideSelection = false;
            this.tb保单号码.Location = new System.Drawing.Point(69, 70);
            this.tb保单号码.ReadOnly = true;
            this.tb保单号码.Size = new System.Drawing.Size(203, 21);
            this.tb保单号码.TabIndex = 1;
            // 
            // lb保单密码
            // 
            this.lb保单密码.Location = new System.Drawing.Point(336, 303);
            this.lb保单密码.Visible = false;
            // 
            // tb保单密码
            // 
            this.tb保单密码.Location = new System.Drawing.Point(395, 300);
            this.tb保单密码.Visible = false;
            // 
            // lb填开单位
            // 
            this.lb填开单位.Location = new System.Drawing.Point(27, 277);
            this.lb填开单位.Size = new System.Drawing.Size(65, 12);
            this.lb填开单位.Text = "加盟商名称";
            // 
            // tb填开单位
            // 
            this.tb填开单位.Location = new System.Drawing.Point(261, 274);
            this.tb填开单位.ReadOnly = true;
            this.tb填开单位.TabIndex = 6;
            // 
            // lb填开日期
            // 
            this.lb填开日期.Location = new System.Drawing.Point(394, 277);
            // 
            // tb填开日期
            // 
            this.tb填开日期.Location = new System.Drawing.Point(453, 274);
            this.tb填开日期.TabIndex = 7;
            // 
            // lb经办人
            // 
            this.lb经办人.Location = new System.Drawing.Point(214, 277);
            // 
            // tb经办人
            // 
            this.tb经办人.Location = new System.Drawing.Point(98, 274);
            this.tb经办人.TabIndex = 5;
            // 
            // lb报案电话
            // 
            this.lb报案电话.Location = new System.Drawing.Point(336, 318);
            this.lb报案电话.Visible = false;
            // 
            // tb报案电话
            // 
            this.tb报案电话.Location = new System.Drawing.Point(395, 315);
            this.tb报案电话.Visible = false;
            // 
            // cb被保险人姓名
            // 
            this.cb被保险人姓名.Location = new System.Drawing.Point(94, 5);
            this.cb被保险人姓名.Size = new System.Drawing.Size(166, 20);
            this.cb被保险人姓名.TabIndex = 0;
            // 
            // lb被保险人姓名
            // 
            this.lb被保险人姓名.Location = new System.Drawing.Point(11, 11);
            // 
            // tb证件号
            // 
            this.tb证件号.Location = new System.Drawing.Point(561, 6);
            this.tb证件号.Size = new System.Drawing.Size(150, 21);
            this.tb证件号.TabIndex = 1;
            // 
            // lb有效身份证件号码
            // 
            this.lb有效身份证件号码.Location = new System.Drawing.Point(454, 11);
            // 
            // tb乘机日
            // 
            this.tb乘机日.Location = new System.Drawing.Point(561, 32);
            this.tb乘机日.Size = new System.Drawing.Size(150, 21);
            this.tb乘机日.TabIndex = 4;
            // 
            // lb乘机日
            // 
            this.lb乘机日.Location = new System.Drawing.Point(454, 33);
            // 
            // lb航班号
            // 
            this.lb航班号.Size = new System.Drawing.Size(41, 12);
            this.lb航班号.Text = "航班号";
            // 
            // tb保险金额
            // 
            this.tb保险金额.ReadOnly = true;
            this.tb保险金额.Size = new System.Drawing.Size(245, 48);
            this.tb保险金额.TabIndex = 5;
            this.tb保险金额.Text = "意外伤害 人民币肆拾万元整 \r\nRMB ￥400,000.00";
            // 
            // tb保险费
            // 
            this.tb保险费.Location = new System.Drawing.Point(561, 54);
            this.tb保险费.ReadOnly = true;
            this.tb保险费.Size = new System.Drawing.Size(150, 21);
            this.tb保险费.TabIndex = 6;
            this.tb保险费.Text = "人民币 贰拾元  RMB ￥20";
            // 
            // lb保险费
            // 
            this.lb保险费.Location = new System.Drawing.Point(454, 57);
            this.lb保险费.Text = "会员费";
            // 
            // tb保险天数
            // 
            this.tb保险天数.Location = new System.Drawing.Point(77, 188);
            // 
            // lb保险终止时间
            // 
            this.lb保险终止时间.Location = new System.Drawing.Point(478, 192);
            // 
            // lb保险天数
            // 
            this.lb保险天数.Location = new System.Drawing.Point(18, 191);
            // 
            // lb保险起始时间
            // 
            this.lb保险起始时间.Location = new System.Drawing.Point(188, 191);
            // 
            // dtp保险终止时间
            // 
            this.dtp保险终止时间.Location = new System.Drawing.Point(561, 188);
            // 
            // dtp保险起始时间
            // 
            this.dtp保险起始时间.Location = new System.Drawing.Point(266, 187);
            // 
            // tb受益人资料
            // 
            this.tb受益人资料.Location = new System.Drawing.Point(94, 150);
            this.tb受益人资料.Size = new System.Drawing.Size(372, 21);
            this.tb受益人资料.TabIndex = 9;
            // 
            // lb受益人资料
            // 
            this.lb受益人资料.Location = new System.Drawing.Point(10, 153);
            // 
            // tb受益人关系
            // 
            this.tb受益人关系.Location = new System.Drawing.Point(94, 126);
            this.tb受益人关系.TabIndex = 7;
            // 
            // lb受益人关系
            // 
            this.lb受益人关系.Location = new System.Drawing.Point(10, 129);
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(724, 301);
            this.bt_Exit.TabIndex = 14;
            // 
            // btPrintLianXu
            // 
            this.btPrintLianXu.Location = new System.Drawing.Point(226, 301);
            this.btPrintLianXu.TabIndex = 11;
            // 
            // tbTestPrint
            // 
            this.tbTestPrint.Location = new System.Drawing.Point(509, 301);
            this.tbTestPrint.TabIndex = 12;
            // 
            // bt_Print
            // 
            this.bt_Print.Enabled = false;
            this.bt_Print.Location = new System.Drawing.Point(601, 301);
            this.bt_Print.TabIndex = 13;
            this.bt_Print.Enter += new System.EventHandler(this.bt_Print_Enter);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(71, 327);
            this.numericUpDown2.TabIndex = 9;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(72, 301);
            this.numericUpDown1.TabIndex = 8;
            // 
            // lb左边距
            // 
            this.lb左边距.Location = new System.Drawing.Point(16, 306);
            // 
            // bt_SetToDefault
            // 
            this.bt_SetToDefault.Location = new System.Drawing.Point(134, 301);
            this.bt_SetToDefault.TabIndex = 10;
            // 
            // lb上边距
            // 
            this.lb上边距.Location = new System.Drawing.Point(16, 330);
            // 
            // bt取身份证号
            // 
            this.bt取身份证号.TabIndex = 2;
            // 
            // bt作废保单
            // 
            this.bt作废保单.Location = new System.Drawing.Point(732, 41);
            // 
            // tb航班号
            // 
            this.tb航班号.Location = new System.Drawing.Point(94, 31);
            this.tb航班号.Size = new System.Drawing.Size(164, 21);
            // 
            // ptDoc
            // 
            this.ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ptDoc_PrintPage);
            // 
            // cb证件类型
            // 
            this.cb证件类型.Location = new System.Drawing.Point(297, 6);
            this.cb证件类型.Visible = false;
            // 
            // tb保险终止时间
            // 
            this.tb保险终止时间.Location = new System.Drawing.Point(675, 186);
            // 
            // tb保险起始时间
            // 
            this.tb保险起始时间.Location = new System.Drawing.Point(380, 186);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(259, 126);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 21);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "联系电话";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(732, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "打开网站";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btSetTitleOffset
            // 
            this.btSetTitleOffset.Location = new System.Drawing.Point(585, 14);
            this.btSetTitleOffset.Name = "btSetTitleOffset";
            this.btSetTitleOffset.Size = new System.Drawing.Size(126, 23);
            this.btSetTitleOffset.TabIndex = 53;
            this.btSetTitleOffset.Text = "设置标题打印偏移量";
            this.btSetTitleOffset.UseVisualStyleBackColor = true;
            this.btSetTitleOffset.Click += new System.EventHandler(this.btSetTitleOffset_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox2.Location = new System.Drawing.Point(3, 297);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(811, 1);
            this.textBox2.TabIndex = 54;
            // 
            // cbPrintTitle
            // 
            this.cbPrintTitle.AutoSize = true;
            this.cbPrintTitle.Checked = true;
            this.cbPrintTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrintTitle.Location = new System.Drawing.Point(585, 39);
            this.cbPrintTitle.Name = "cbPrintTitle";
            this.cbPrintTitle.Size = new System.Drawing.Size(96, 16);
            this.cbPrintTitle.TabIndex = 55;
            this.cbPrintTitle.Text = "是否打印标题";
            this.cbPrintTitle.UseVisualStyleBackColor = true;
            this.cbPrintTitle.CheckedChanged += new System.EventHandler(this.cbPrintTitle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(714, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "(如2008-3-2)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(266, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 42);
            this.label3.TabIndex = 11;
            this.label3.Text = "小提示:\r\n若您在使用PNR无法提取时,\r\n可以手工输入信息进行打印";
            // 
            // cbPrice
            // 
            this.cbPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrice.FormattingEnabled = true;
            this.cbPrice.Items.AddRange(new object[] {
            "人民币 拾伍元  RMB ￥15元",
            "人民币 贰拾元  RMB ￥20元",
            "人民币 贰拾伍元  RMB ￥25元"});
            this.cbPrice.Location = new System.Drawing.Point(561, 82);
            this.cbPrice.Name = "cbPrice";
            this.cbPrice.Size = new System.Drawing.Size(230, 20);
            this.cbPrice.TabIndex = 12;
            // 
            // EagleIns
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(240)))), ((int)(((byte)(222)))));
            this.CancelButton = this.bt_Exit;
            this.ClientSize = new System.Drawing.Size(820, 354);
            this.Controls.Add(this.cbPrintTitle);
            this.Controls.Add(this.btSetTitleOffset);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EagleIns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "9999";
            this.Load += new System.EventHandler(this.EagleIns_Load);
            this.Controls.SetChildIndex(this.textBox2, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.btSetTitleOffset, 0);
            this.Controls.SetChildIndex(this.cbPrintTitle, 0);
            this.Controls.SetChildIndex(this.tb报案电话, 0);
            this.Controls.SetChildIndex(this.tb保单密码, 0);
            this.Controls.SetChildIndex(this.lb险种条款, 0);
            this.Controls.SetChildIndex(this.lb保单密码, 0);
            this.Controls.SetChildIndex(this.cb险种条款, 0);
            this.Controls.SetChildIndex(this.lb报案电话, 0);
            this.Controls.SetChildIndex(this.bt作废保单, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lb公司名称, 0);
            this.Controls.SetChildIndex(this.lb保单标题, 0);
            this.Controls.SetChildIndex(this.lb保单序号, 0);
            this.Controls.SetChildIndex(this.lb保单号码, 0);
            this.Controls.SetChildIndex(this.imgLogo, 0);
            this.Controls.SetChildIndex(this.lb填开单位, 0);
            this.Controls.SetChildIndex(this.lb填开日期, 0);
            this.Controls.SetChildIndex(this.tb保单序号, 0);
            this.Controls.SetChildIndex(this.lb经办人, 0);
            this.Controls.SetChildIndex(this.tb保单号码, 0);
            this.Controls.SetChildIndex(this.tb填开单位, 0);
            this.Controls.SetChildIndex(this.tb填开日期, 0);
            this.Controls.SetChildIndex(this.tb经办人, 0);
            this.Controls.SetChildIndex(this.GroupBox2, 0);
            this.Controls.SetChildIndex(this.lb上边距, 0);
            this.Controls.SetChildIndex(this.bt_SetToDefault, 0);
            this.Controls.SetChildIndex(this.lb左边距, 0);
            this.Controls.SetChildIndex(this.numericUpDown1, 0);
            this.Controls.SetChildIndex(this.numericUpDown2, 0);
            this.Controls.SetChildIndex(this.bt_Print, 0);
            this.Controls.SetChildIndex(this.tbTestPrint, 0);
            this.Controls.SetChildIndex(this.btPrintLianXu, 0);
            this.Controls.SetChildIndex(this.bt_Exit, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btSetTitleOffset;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox cbPrintTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPrice;
    }
}
