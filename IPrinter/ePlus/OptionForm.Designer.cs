namespace ePlus
{
    partial class OptionForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblWorkAreaFontSize = new System.Windows.Forms.Label();
            this.lblWorkAreaBackColor = new System.Windows.Forms.Label();
            this.lblWorkAreaForeColor = new System.Windows.Forms.Label();
            this.lblNoticeFontSize = new System.Windows.Forms.Label();
            this.lblNoticeBackColor = new System.Windows.Forms.Label();
            this.lblNoticeForeColor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtB2CURL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudUdpPort = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUSB = new System.Windows.Forms.RadioButton();
            this.rbEGPlug = new System.Windows.Forms.RadioButton();
            this.rbEGSwitch = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.rbMMPBX = new System.Windows.Forms.RadioButton();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUdpPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(6, 7);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(369, 238);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblWorkAreaFontSize);
            this.tabPage1.Controls.Add(this.lblWorkAreaBackColor);
            this.tabPage1.Controls.Add(this.lblWorkAreaForeColor);
            this.tabPage1.Controls.Add(this.lblNoticeFontSize);
            this.tabPage1.Controls.Add(this.lblNoticeBackColor);
            this.tabPage1.Controls.Add(this.lblNoticeForeColor);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(312, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "颜色与字体";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblWorkAreaFontSize
            // 
            this.lblWorkAreaFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkAreaFontSize.Location = new System.Drawing.Point(112, 157);
            this.lblWorkAreaFontSize.Name = "lblWorkAreaFontSize";
            this.lblWorkAreaFontSize.Size = new System.Drawing.Size(149, 22);
            this.lblWorkAreaFontSize.TabIndex = 4;
            this.lblWorkAreaFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWorkAreaFontSize.DoubleClick += new System.EventHandler(this.lblNoticeFontSize_DoubleClick);
            // 
            // lblWorkAreaBackColor
            // 
            this.lblWorkAreaBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkAreaBackColor.Location = new System.Drawing.Point(112, 128);
            this.lblWorkAreaBackColor.Name = "lblWorkAreaBackColor";
            this.lblWorkAreaBackColor.Size = new System.Drawing.Size(149, 22);
            this.lblWorkAreaBackColor.TabIndex = 4;
            this.lblWorkAreaBackColor.DoubleClick += new System.EventHandler(this.lblNoticeForeColor_DoubleClick);
            // 
            // lblWorkAreaForeColor
            // 
            this.lblWorkAreaForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWorkAreaForeColor.Location = new System.Drawing.Point(112, 99);
            this.lblWorkAreaForeColor.Name = "lblWorkAreaForeColor";
            this.lblWorkAreaForeColor.Size = new System.Drawing.Size(149, 22);
            this.lblWorkAreaForeColor.TabIndex = 4;
            this.lblWorkAreaForeColor.DoubleClick += new System.EventHandler(this.lblNoticeForeColor_DoubleClick);
            // 
            // lblNoticeFontSize
            // 
            this.lblNoticeFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoticeFontSize.Location = new System.Drawing.Point(112, 70);
            this.lblNoticeFontSize.Name = "lblNoticeFontSize";
            this.lblNoticeFontSize.Size = new System.Drawing.Size(149, 22);
            this.lblNoticeFontSize.TabIndex = 4;
            this.lblNoticeFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoticeFontSize.DoubleClick += new System.EventHandler(this.lblNoticeFontSize_DoubleClick);
            // 
            // lblNoticeBackColor
            // 
            this.lblNoticeBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoticeBackColor.Location = new System.Drawing.Point(112, 41);
            this.lblNoticeBackColor.Name = "lblNoticeBackColor";
            this.lblNoticeBackColor.Size = new System.Drawing.Size(149, 22);
            this.lblNoticeBackColor.TabIndex = 4;
            this.lblNoticeBackColor.DoubleClick += new System.EventHandler(this.lblNoticeForeColor_DoubleClick);
            // 
            // lblNoticeForeColor
            // 
            this.lblNoticeForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoticeForeColor.Location = new System.Drawing.Point(112, 12);
            this.lblNoticeForeColor.Name = "lblNoticeForeColor";
            this.lblNoticeForeColor.Size = new System.Drawing.Size(149, 22);
            this.lblNoticeForeColor.TabIndex = 4;
            this.lblNoticeForeColor.DoubleClick += new System.EventHandler(this.lblNoticeForeColor_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "工作区域背景色：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "工作区域字体：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "滚动广播字体：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "工作区域前景色：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "滚动广播背景色：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "滚动广播前景色：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(361, 213);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "呼叫中心";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(67, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "(*)以上修改需要重新登录客户端才可以生效";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtB2CURL);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nudUdpPort);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(6, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 113);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据对接";
            // 
            // txtB2CURL
            // 
            this.txtB2CURL.Location = new System.Drawing.Point(16, 75);
            this.txtB2CURL.Name = "txtB2CURL";
            this.txtB2CURL.Size = new System.Drawing.Size(327, 21);
            this.txtB2CURL.TabIndex = 4;
            this.txtB2CURL.Text = "http://221.232.148.250:6000/AirLineTicket/";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "B2C 地址：";
            // 
            // nudUdpPort
            // 
            this.nudUdpPort.Location = new System.Drawing.Point(83, 21);
            this.nudUdpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudUdpPort.Name = "nudUdpPort";
            this.nudUdpPort.Size = new System.Drawing.Size(59, 21);
            this.nudUdpPort.TabIndex = 2;
            this.nudUdpPort.Value = new decimal(new int[] {
            5161,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "UDP 端口：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbMMPBX);
            this.groupBox1.Controls.Add(this.rbUSB);
            this.groupBox1.Controls.Add(this.rbEGPlug);
            this.groupBox1.Controls.Add(this.rbEGSwitch);
            this.groupBox1.Location = new System.Drawing.Point(6, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 51);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "呼叫中心类型";
            // 
            // rbUSB
            // 
            this.rbUSB.AutoSize = true;
            this.rbUSB.Location = new System.Drawing.Point(161, 20);
            this.rbUSB.Name = "rbUSB";
            this.rbUSB.Size = new System.Drawing.Size(83, 16);
            this.rbUSB.TabIndex = 2;
            this.rbUSB.Text = "USB 小盒子";
            this.rbUSB.UseVisualStyleBackColor = true;
            // 
            // rbEGPlug
            // 
            this.rbEGPlug.AutoSize = true;
            this.rbEGPlug.Checked = true;
            this.rbEGPlug.Location = new System.Drawing.Point(12, 20);
            this.rbEGPlug.Name = "rbEGPlug";
            this.rbEGPlug.Size = new System.Drawing.Size(59, 16);
            this.rbEGPlug.TabIndex = 1;
            this.rbEGPlug.TabStop = true;
            this.rbEGPlug.Text = "板卡型";
            this.rbEGPlug.UseVisualStyleBackColor = true;
            // 
            // rbEGSwitch
            // 
            this.rbEGSwitch.AutoSize = true;
            this.rbEGSwitch.Location = new System.Drawing.Point(81, 20);
            this.rbEGSwitch.Name = "rbEGSwitch";
            this.rbEGSwitch.Size = new System.Drawing.Size(71, 16);
            this.rbEGSwitch.TabIndex = 0;
            this.rbEGSwitch.Text = "交换机型";
            this.rbEGSwitch.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(169, 251);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "保存(&S)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(250, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.SolidColorOnly = true;
            // 
            // fontDialog
            // 
            this.fontDialog.ShowEffects = false;
            // 
            // rbMMPBX
            // 
            this.rbMMPBX.AutoSize = true;
            this.rbMMPBX.Location = new System.Drawing.Point(252, 20);
            this.rbMMPBX.Name = "rbMMPBX";
            this.rbMMPBX.Size = new System.Drawing.Size(77, 16);
            this.rbMMPBX.TabIndex = 3;
            this.rbMMPBX.Text = "多媒体PBX";
            this.rbMMPBX.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(387, 280);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUdpPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWorkAreaFontSize;
        private System.Windows.Forms.Label lblWorkAreaBackColor;
        private System.Windows.Forms.Label lblWorkAreaForeColor;
        private System.Windows.Forms.Label lblNoticeFontSize;
        private System.Windows.Forms.Label lblNoticeBackColor;
        private System.Windows.Forms.Label lblNoticeForeColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEGPlug;
        private System.Windows.Forms.RadioButton rbEGSwitch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudUdpPort;
        private System.Windows.Forms.RadioButton rbUSB;
        private System.Windows.Forms.TextBox txtB2CURL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbMMPBX;
    }
}