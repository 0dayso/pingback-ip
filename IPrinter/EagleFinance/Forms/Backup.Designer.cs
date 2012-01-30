namespace EagleFinance
{
    partial class Backup
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
            this.AutoBackup = new System.Windows.Forms.CheckBox();
            this.tbDir = new System.Windows.Forms.TextBox();
            this.btSelectAutoBackupDirectory = new System.Windows.Forms.Button();
            this.btBackupManual = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AutoBackup
            // 
            this.AutoBackup.AutoSize = true;
            this.AutoBackup.Checked = true;
            this.AutoBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoBackup.Enabled = false;
            this.AutoBackup.Location = new System.Drawing.Point(12, 12);
            this.AutoBackup.Name = "AutoBackup";
            this.AutoBackup.Size = new System.Drawing.Size(132, 16);
            this.AutoBackup.TabIndex = 0;
            this.AutoBackup.Text = "程序退出时自动备份";
            this.AutoBackup.UseVisualStyleBackColor = true;
            // 
            // tbDir
            // 
            this.tbDir.Location = new System.Drawing.Point(12, 34);
            this.tbDir.Name = "tbDir";
            this.tbDir.Size = new System.Drawing.Size(268, 21);
            this.tbDir.TabIndex = 2;
            this.tbDir.Text = "C:\\";
            // 
            // btSelectAutoBackupDirectory
            // 
            this.btSelectAutoBackupDirectory.Location = new System.Drawing.Point(150, 8);
            this.btSelectAutoBackupDirectory.Name = "btSelectAutoBackupDirectory";
            this.btSelectAutoBackupDirectory.Size = new System.Drawing.Size(130, 23);
            this.btSelectAutoBackupDirectory.TabIndex = 1;
            this.btSelectAutoBackupDirectory.Text = "选择自动备份目录";
            this.btSelectAutoBackupDirectory.UseVisualStyleBackColor = true;
            this.btSelectAutoBackupDirectory.Click += new System.EventHandler(this.btSelectAutoBackupDirectory_Click);
            // 
            // btBackupManual
            // 
            this.btBackupManual.Location = new System.Drawing.Point(12, 61);
            this.btBackupManual.Name = "btBackupManual";
            this.btBackupManual.Size = new System.Drawing.Size(130, 23);
            this.btBackupManual.TabIndex = 3;
            this.btBackupManual.Text = "手动备份";
            this.btBackupManual.UseVisualStyleBackColor = true;
            this.btBackupManual.Click += new System.EventHandler(this.btBackupManual_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(150, 61);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(130, 23);
            this.btClose.TabIndex = 4;
            this.btClose.Text = "关闭";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // Backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 91);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btBackupManual);
            this.Controls.Add(this.btSelectAutoBackupDirectory);
            this.Controls.Add(this.tbDir);
            this.Controls.Add(this.AutoBackup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Backup";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "备份选项";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Backup_FormClosed);
            this.Load += new System.EventHandler(this.Backup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox AutoBackup;
        private System.Windows.Forms.TextBox tbDir;
        private System.Windows.Forms.Button btSelectAutoBackupDirectory;
        private System.Windows.Forms.Button btBackupManual;
        private System.Windows.Forms.Button btClose;
    }
}