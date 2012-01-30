namespace Options
{
    partial class A4ReceiptPrint
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
            this.ptDoc = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAgentName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAgentAddr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAgentPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPayMethod = new System.Windows.Forms.TextBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ptDoc
            // 
            this.ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ptDoc_PrintPage);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "代理名称：";
            // 
            // tbAgentName
            // 
            this.tbAgentName.Location = new System.Drawing.Point(83, 6);
            this.tbAgentName.Name = "tbAgentName";
            this.tbAgentName.Size = new System.Drawing.Size(197, 21);
            this.tbAgentName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "代理地址：";
            // 
            // tbAgentAddr
            // 
            this.tbAgentAddr.Location = new System.Drawing.Point(83, 33);
            this.tbAgentAddr.Name = "tbAgentAddr";
            this.tbAgentAddr.Size = new System.Drawing.Size(197, 21);
            this.tbAgentAddr.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "代理电话：";
            // 
            // tbAgentPhone
            // 
            this.tbAgentPhone.Location = new System.Drawing.Point(83, 60);
            this.tbAgentPhone.Name = "tbAgentPhone";
            this.tbAgentPhone.Size = new System.Drawing.Size(197, 21);
            this.tbAgentPhone.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "付款方式：";
            // 
            // tbPayMethod
            // 
            this.tbPayMethod.Location = new System.Drawing.Point(83, 87);
            this.tbPayMethod.Name = "tbPayMethod";
            this.tbPayMethod.Size = new System.Drawing.Size(197, 21);
            this.tbPayMethod.TabIndex = 3;
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(109, 157);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 23);
            this.btPrint.TabIndex = 4;
            this.btPrint.Text = "打印";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "标图位置：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(14, 131);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(266, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // A4ReceiptPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 186);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.tbPayMethod);
            this.Controls.Add(this.tbAgentPhone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbAgentAddr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbAgentName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "A4ReceiptPrint";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "A4ReceiptPrint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.A4ReceiptPrint_FormClosing);
            this.Load += new System.EventHandler(this.A4ReceiptPrint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument ptDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAgentName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAgentAddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAgentPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPayMethod;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}