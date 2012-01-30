namespace ePlus.PrintHyx
{
    partial class AirCode
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
            this.label4 = new System.Windows.Forms.Label();
            this.tbAirCode = new System.Windows.Forms.TextBox();
            this.tbFlightNumber = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.btGetPNR = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "大编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "航班号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "日  期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "RRT:V/大编/航班号/日期";
            // 
            // tbAirCode
            // 
            this.tbAirCode.Location = new System.Drawing.Point(71, 50);
            this.tbAirCode.Name = "tbAirCode";
            this.tbAirCode.Size = new System.Drawing.Size(209, 21);
            this.tbAirCode.TabIndex = 1;
            // 
            // tbFlightNumber
            // 
            this.tbFlightNumber.Location = new System.Drawing.Point(71, 73);
            this.tbFlightNumber.Name = "tbFlightNumber";
            this.tbFlightNumber.Size = new System.Drawing.Size(209, 21);
            this.tbFlightNumber.TabIndex = 1;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(71, 96);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(112, 21);
            this.dtpDate.TabIndex = 2;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(189, 96);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(91, 21);
            this.tbDate.TabIndex = 1;
            // 
            // btGetPNR
            // 
            this.btGetPNR.Location = new System.Drawing.Point(12, 123);
            this.btGetPNR.Name = "btGetPNR";
            this.btGetPNR.Size = new System.Drawing.Size(75, 23);
            this.btGetPNR.TabIndex = 3;
            this.btGetPNR.Text = "提取小编";
            this.btGetPNR.UseVisualStyleBackColor = true;
            this.btGetPNR.Click += new System.EventHandler(this.btGetPNR_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(93, 125);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(187, 21);
            this.textBox4.TabIndex = 1;
            // 
            // AirCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 156);
            this.Controls.Add(this.btGetPNR);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.tbFlightNumber);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.tbAirCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AirCode";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AirCode";
            this.Load += new System.EventHandler(this.AirCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAirCode;
        private System.Windows.Forms.TextBox tbFlightNumber;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Button btGetPNR;
        private System.Windows.Forms.TextBox textBox4;
    }
}