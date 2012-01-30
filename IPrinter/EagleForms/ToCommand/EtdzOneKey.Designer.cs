namespace EagleForms.ToCommand
{
    partial class EtdzOneKey
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
            this.cbPat = new System.Windows.Forms.ComboBox();
            this.chkPat = new System.Windows.Forms.CheckBox();
            this.txtPnr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEtdz = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbPat);
            this.panel1.Controls.Add(this.chkPat);
            this.panel1.Controls.Add(this.txtPnr);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 32);
            this.panel1.TabIndex = 0;
            // 
            // cbPat
            // 
            this.cbPat.FormattingEnabled = true;
            this.cbPat.Items.AddRange(new object[] {
            "PAT:",
            "PAT:A",
            "PAT:*CH",
            "PAT:#YZZS",
            "PAT:#MUTTR",
            "PAT:#3UZZ"});
            this.cbPat.Location = new System.Drawing.Point(184, 4);
            this.cbPat.Name = "cbPat";
            this.cbPat.Size = new System.Drawing.Size(143, 20);
            this.cbPat.TabIndex = 2;
            // 
            // chkPat
            // 
            this.chkPat.AutoSize = true;
            this.chkPat.Checked = true;
            this.chkPat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPat.Location = new System.Drawing.Point(112, 6);
            this.chkPat.Name = "chkPat";
            this.chkPat.Size = new System.Drawing.Size(66, 16);
            this.chkPat.TabIndex = 1;
            this.chkPat.Text = "已做PAT";
            this.chkPat.UseVisualStyleBackColor = true;
            this.chkPat.CheckedChanged += new System.EventHandler(this.chkPat_CheckedChanged);
            // 
            // txtPnr
            // 
            this.txtPnr.Location = new System.Drawing.Point(40, 3);
            this.txtPnr.Name = "txtPnr";
            this.txtPnr.Size = new System.Drawing.Size(66, 21);
            this.txtPnr.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "PNR:";
            // 
            // btnEtdz
            // 
            this.btnEtdz.Location = new System.Drawing.Point(172, 38);
            this.btnEtdz.Name = "btnEtdz";
            this.btnEtdz.Size = new System.Drawing.Size(75, 23);
            this.btnEtdz.TabIndex = 0;
            this.btnEtdz.Text = "出票";
            this.btnEtdz.UseVisualStyleBackColor = true;
            this.btnEtdz.Click += new System.EventHandler(this.btnEtdz_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "提示:建议先在黑屏中做好PAT项";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(253, 38);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EtdzOneKey
            // 
            this.AcceptButton = this.btnEtdz;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(332, 91);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEtdz);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EtdzOneKey";
            this.Text = "一键出票";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.EtdzOneKey_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPnr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbPat;
        private System.Windows.Forms.CheckBox chkPat;
        private System.Windows.Forms.Button btnEtdz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
    }
}