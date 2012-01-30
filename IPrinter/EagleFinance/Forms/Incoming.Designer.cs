namespace EagleFinance
{
    partial class Incoming
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
            this.cbOffice = new System.Windows.Forms.ComboBox();
            this.NumberBeg = new System.Windows.Forms.TextBox();
            this.NumberEnd = new System.Windows.Forms.TextBox();
            this.btIncoming = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbOffice
            // 
            this.cbOffice.FormattingEnabled = true;
            this.cbOffice.Location = new System.Drawing.Point(136, 29);
            this.cbOffice.Name = "cbOffice";
            this.cbOffice.Size = new System.Drawing.Size(121, 20);
            this.cbOffice.TabIndex = 0;
            // 
            // NumberBeg
            // 
            this.NumberBeg.Location = new System.Drawing.Point(136, 55);
            this.NumberBeg.Name = "NumberBeg";
            this.NumberBeg.Size = new System.Drawing.Size(121, 21);
            this.NumberBeg.TabIndex = 1;
            this.NumberBeg.TextChanged += new System.EventHandler(this.NumberBeg_TextChanged);
            // 
            // NumberEnd
            // 
            this.NumberEnd.Location = new System.Drawing.Point(136, 82);
            this.NumberEnd.Name = "NumberEnd";
            this.NumberEnd.Size = new System.Drawing.Size(121, 21);
            this.NumberEnd.TabIndex = 2;
            // 
            // btIncoming
            // 
            this.btIncoming.Location = new System.Drawing.Point(205, 109);
            this.btIncoming.Name = "btIncoming";
            this.btIncoming.Size = new System.Drawing.Size(75, 23);
            this.btIncoming.TabIndex = 3;
            this.btIncoming.Text = "入库";
            this.btIncoming.UseVisualStyleBackColor = true;
            this.btIncoming.Click += new System.EventHandler(this.btIncoming_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "OFFICE(6位):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "起始票号10位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "终止票号10位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 111);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(190, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Incoming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 168);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btIncoming);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.NumberEnd);
            this.Controls.Add(this.NumberBeg);
            this.Controls.Add(this.cbOffice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Incoming";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "入库";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbOffice;
        private System.Windows.Forms.TextBox NumberBeg;
        private System.Windows.Forms.TextBox NumberEnd;
        private System.Windows.Forms.Button btIncoming;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
    }
}