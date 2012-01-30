namespace EagleForms.General
{
    partial class NoticeScrollPublish
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
            this.dtpBeg = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.cbBeg = new System.Windows.Forms.CheckBox();
            this.cbEnd = new System.Windows.Forms.CheckBox();
            this.tbNotice = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpBeg
            // 
            this.dtpBeg.Location = new System.Drawing.Point(104, 2);
            this.dtpBeg.Name = "dtpBeg";
            this.dtpBeg.Size = new System.Drawing.Size(110, 21);
            this.dtpBeg.TabIndex = 0;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(332, 2);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(110, 21);
            this.dtpEnd.TabIndex = 1;
            // 
            // cbBeg
            // 
            this.cbBeg.AutoSize = true;
            this.cbBeg.Checked = true;
            this.cbBeg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBeg.Enabled = false;
            this.cbBeg.Location = new System.Drawing.Point(2, 5);
            this.cbBeg.Name = "cbBeg";
            this.cbBeg.Size = new System.Drawing.Size(96, 16);
            this.cbBeg.TabIndex = 2;
            this.cbBeg.Text = "公告起效时间";
            this.cbBeg.UseVisualStyleBackColor = true;
            this.cbBeg.CheckedChanged += new System.EventHandler(this.cbBeg_CheckedChanged);
            // 
            // cbEnd
            // 
            this.cbEnd.AutoSize = true;
            this.cbEnd.Checked = true;
            this.cbEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnd.Enabled = false;
            this.cbEnd.Location = new System.Drawing.Point(230, 5);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(96, 16);
            this.cbEnd.TabIndex = 3;
            this.cbEnd.Text = "公告失效时间";
            this.cbEnd.UseVisualStyleBackColor = true;
            this.cbEnd.CheckedChanged += new System.EventHandler(this.cbEnd_CheckedChanged);
            // 
            // tbNotice
            // 
            this.tbNotice.Location = new System.Drawing.Point(2, 27);
            this.tbNotice.Multiline = true;
            this.tbNotice.Name = "tbNotice";
            this.tbNotice.Size = new System.Drawing.Size(440, 214);
            this.tbNotice.TabIndex = 4;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(144, 243);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 5;
            this.btOK.Text = "发布";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btClose
            // 
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(225, 243);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 6;
            this.btClose.Text = "关闭";
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // NoticeScroll
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(444, 266);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbNotice);
            this.Controls.Add(this.cbEnd);
            this.Controls.Add(this.cbBeg);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpBeg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoticeScroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发布滚动式公告";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NoticeScroll_FormClosed);
            this.Load += new System.EventHandler(this.NoticeScroll_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpBeg;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.CheckBox cbBeg;
        private System.Windows.Forms.CheckBox cbEnd;
        private System.Windows.Forms.TextBox tbNotice;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btClose;
    }
}