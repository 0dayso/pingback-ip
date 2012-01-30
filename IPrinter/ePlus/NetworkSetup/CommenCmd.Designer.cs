namespace ePlus
{
    partial class CommenCmd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommenCmd));
            this.lbCmds = new System.Windows.Forms.ListBox();
            this.btNew = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbCmds
            // 
            this.lbCmds.FormattingEnabled = true;
            this.lbCmds.ItemHeight = 12;
            this.lbCmds.Location = new System.Drawing.Point(1, 1);
            this.lbCmds.Name = "lbCmds";
            this.lbCmds.Size = new System.Drawing.Size(210, 244);
            this.lbCmds.Sorted = true;
            this.lbCmds.TabIndex = 0;
            this.lbCmds.DoubleClick += new System.EventHandler(this.lbCmds_DoubleClick);
            // 
            // btNew
            // 
            this.btNew.Image = ((System.Drawing.Image)(resources.GetObject("btNew.Image")));
            this.btNew.Location = new System.Drawing.Point(168, 248);
            this.btNew.Margin = new System.Windows.Forms.Padding(0);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(16, 16);
            this.btNew.TabIndex = 1;
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // btDelete
            // 
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.Location = new System.Drawing.Point(195, 248);
            this.btDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(16, 16);
            this.btDelete.TabIndex = 1;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // CommenCmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 264);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btNew);
            this.Controls.Add(this.lbCmds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CommenCmd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "常用指令";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CommenCmd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbCmds;
        private System.Windows.Forms.Button btNew;
        private System.Windows.Forms.Button btDelete;
    }
}