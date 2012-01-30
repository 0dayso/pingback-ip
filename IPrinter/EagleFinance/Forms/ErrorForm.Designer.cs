namespace EagleFinance
{
    partial class ErrorForm
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
            this.lb = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb
            // 
            this.lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb.FormattingEnabled = true;
            this.lb.ItemHeight = 12;
            this.lb.Location = new System.Drawing.Point(0, 0);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(349, 556);
            this.lb.TabIndex = 0;
            this.lb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_MouseClick);
            this.lb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lb_MouseUp);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 558);
            this.Controls.Add(this.lb);
            this.Name = "ErrorForm";
            this.Text = "错误信息窗口";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox lb;

    }
}