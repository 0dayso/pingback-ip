namespace Options
{
    partial class InterTicketPrint
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
            this.tbContent = new System.Windows.Forms.TextBox();
            this.tbPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ptDoc
            // 
            this.ptDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ptDoc_PrintPage);
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(12, 12);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(597, 249);
            this.tbContent.TabIndex = 0;
            this.tbContent.Text = "请将要打印的内容";
            // 
            // tbPrint
            // 
            this.tbPrint.Location = new System.Drawing.Point(273, 271);
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(75, 23);
            this.tbPrint.TabIndex = 1;
            this.tbPrint.Text = "打印(&P)";
            this.tbPrint.UseVisualStyleBackColor = true;
            this.tbPrint.Click += new System.EventHandler(this.tbPrint_Click);
            // 
            // InterTicketPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 306);
            this.Controls.Add(this.tbPrint);
            this.Controls.Add(this.tbContent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterTicketPrint";
            this.Text = "InterTicketPrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument ptDoc;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Button tbPrint;
    }
}