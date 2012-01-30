namespace EagleForms.Easy
{
    partial class EasyMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnSaveAndSubmit = new System.Windows.Forms.Button();
            this.btnSellSeat = new System.Windows.Forms.Button();
            this.txtPnr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 86);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(0, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(832, 191);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.Location = new System.Drawing.Point(0, 279);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(314, 80);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.Location = new System.Drawing.Point(0, 360);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(314, 115);
            this.panel4.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.Controls.Add(this.btnSaveAndSubmit);
            this.panel5.Controls.Add(this.btnSellSeat);
            this.panel5.Controls.Add(this.txtPnr);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.txtPhone);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(315, 279);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(123, 196);
            this.panel5.TabIndex = 3;
            // 
            // btnSaveAndSubmit
            // 
            this.btnSaveAndSubmit.Location = new System.Drawing.Point(6, 105);
            this.btnSaveAndSubmit.Name = "btnSaveAndSubmit";
            this.btnSaveAndSubmit.Size = new System.Drawing.Size(112, 23);
            this.btnSaveAndSubmit.TabIndex = 2;
            this.btnSaveAndSubmit.Text = "保存并提交";
            this.btnSaveAndSubmit.UseVisualStyleBackColor = true;
            this.btnSaveAndSubmit.Click += new System.EventHandler(this.btnSaveAndSubmit_Click);
            // 
            // btnSellSeat
            // 
            this.btnSellSeat.Location = new System.Drawing.Point(6, 50);
            this.btnSellSeat.Name = "btnSellSeat";
            this.btnSellSeat.Size = new System.Drawing.Size(112, 23);
            this.btnSellSeat.TabIndex = 2;
            this.btnSellSeat.Text = "生成PNR(预定)";
            this.btnSellSeat.UseVisualStyleBackColor = true;
            this.btnSellSeat.Click += new System.EventHandler(this.btnSellSeat_Click);
            // 
            // txtPnr
            // 
            this.txtPnr.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtPnr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPnr.ForeColor = System.Drawing.Color.Lime;
            this.txtPnr.Location = new System.Drawing.Point(31, 78);
            this.txtPnr.Name = "txtPnr";
            this.txtPnr.Size = new System.Drawing.Size(87, 21);
            this.txtPnr.TabIndex = 1;
            this.txtPnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "PNR";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Location = new System.Drawing.Point(6, 22);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(112, 21);
            this.txtPhone.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "代理点电话";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Location = new System.Drawing.Point(439, 279);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(393, 196);
            this.panel6.TabIndex = 4;
            // 
            // EasyMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 475);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EasyMain";
            this.Text = "EasyMain";
            this.Load += new System.EventHandler(this.EasyMain_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSellSeat;
        private System.Windows.Forms.TextBox txtPnr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveAndSubmit;
        private System.Windows.Forms.Panel panel6;
    }
}