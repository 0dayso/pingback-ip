namespace Options
{
    partial class NoticeDlg
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
            this.richEditor = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richEditor
            // 
            this.richEditor.Location = new System.Drawing.Point(12, 12);
            this.richEditor.Name = "richEditor";
            this.richEditor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richEditor.Size = new System.Drawing.Size(578, 318);
            this.richEditor.TabIndex = 0;
            this.richEditor.Text = "";
            this.richEditor.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richEditor_LinkClicked);
            this.richEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.richEditor_MouseMove);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(576, 336);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(14, 10);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NoticeDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(602, 345);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richEditor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoticeDlg";
            this.Text = "公告框";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NoticeDlg_FormClosed);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NoticeDlg_MouseMove);
            this.Load += new System.EventHandler(this.NoticeDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richEditor;
        private System.Windows.Forms.Button button1;
    }
}