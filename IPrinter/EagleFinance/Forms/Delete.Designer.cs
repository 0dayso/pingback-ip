namespace EagleFinance
{
    partial class Delete
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NumberEnd = new System.Windows.Forms.TextBox();
            this.NumberBeg = new System.Windows.Forms.TextBox();
            this.btIncoming = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "终止票号10位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "起始票号10位";
            // 
            // NumberEnd
            // 
            this.NumberEnd.Location = new System.Drawing.Point(128, 33);
            this.NumberEnd.Name = "NumberEnd";
            this.NumberEnd.Size = new System.Drawing.Size(121, 21);
            this.NumberEnd.TabIndex = 5;
            // 
            // NumberBeg
            // 
            this.NumberBeg.Location = new System.Drawing.Point(128, 6);
            this.NumberBeg.Name = "NumberBeg";
            this.NumberBeg.Size = new System.Drawing.Size(121, 21);
            this.NumberBeg.TabIndex = 4;
            this.NumberBeg.TextChanged += new System.EventHandler(this.NumberBeg_TextChanged);
            // 
            // btIncoming
            // 
            this.btIncoming.Location = new System.Drawing.Point(156, 60);
            this.btIncoming.Name = "btIncoming";
            this.btIncoming.Size = new System.Drawing.Size(75, 23);
            this.btIncoming.TabIndex = 8;
            this.btIncoming.Text = "删除";
            this.btIncoming.UseVisualStyleBackColor = true;
            this.btIncoming.Click += new System.EventHandler(this.btIncoming_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(61, 60);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "清空数据库";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 95);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btIncoming);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NumberEnd);
            this.Controls.Add(this.NumberBeg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Delete";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "删除记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NumberEnd;
        private System.Windows.Forms.TextBox NumberBeg;
        private System.Windows.Forms.Button btIncoming;
        private System.Windows.Forms.Button btnClear;
    }
}