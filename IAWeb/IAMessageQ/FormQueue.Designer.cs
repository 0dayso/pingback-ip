namespace IAMessageQ
{
    partial class FormQueue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnIssueStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudThreads = new System.Windows.Forms.NumericUpDown();
            this.btnIssueStart = new System.Windows.Forms.Button();
            this.txtLogInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCountIdle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIssueStop
            // 
            this.btnIssueStop.Enabled = false;
            this.btnIssueStop.Location = new System.Drawing.Point(99, 6);
            this.btnIssueStop.Name = "btnIssueStop";
            this.btnIssueStop.Size = new System.Drawing.Size(75, 23);
            this.btnIssueStop.TabIndex = 11;
            this.btnIssueStop.Text = "Stop";
            this.btnIssueStop.UseVisualStyleBackColor = true;
            this.btnIssueStop.Click += new System.EventHandler(this.btnIssueStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "并发线程数：";
            // 
            // nudThreads
            // 
            this.nudThreads.Location = new System.Drawing.Point(274, 10);
            this.nudThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThreads.Name = "nudThreads";
            this.nudThreads.Size = new System.Drawing.Size(42, 21);
            this.nudThreads.TabIndex = 9;
            this.nudThreads.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnIssueStart
            // 
            this.btnIssueStart.Location = new System.Drawing.Point(4, 7);
            this.btnIssueStart.Name = "btnIssueStart";
            this.btnIssueStart.Size = new System.Drawing.Size(75, 23);
            this.btnIssueStart.TabIndex = 8;
            this.btnIssueStart.Text = "Start";
            this.btnIssueStart.UseVisualStyleBackColor = true;
            this.btnIssueStart.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // txtLogInfo
            // 
            this.txtLogInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogInfo.Location = new System.Drawing.Point(0, 37);
            this.txtLogInfo.Multiline = true;
            this.txtLogInfo.Name = "txtLogInfo";
            this.txtLogInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogInfo.Size = new System.Drawing.Size(558, 269);
            this.txtLogInfo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(457, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "后台线程数：";
            // 
            // lblCountIdle
            // 
            this.lblCountIdle.AutoSize = true;
            this.lblCountIdle.Location = new System.Drawing.Point(529, 14);
            this.lblCountIdle.Name = "lblCountIdle";
            this.lblCountIdle.Size = new System.Drawing.Size(11, 12);
            this.lblCountIdle.TabIndex = 13;
            this.lblCountIdle.Text = "0";
            // 
            // FormQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 308);
            this.Controls.Add(this.lblCountIdle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnIssueStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudThreads);
            this.Controls.Add(this.btnIssueStart);
            this.Controls.Add(this.txtLogInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormQueue";
            this.Text = "FormQueue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormQueue_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIssueStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudThreads;
        private System.Windows.Forms.Button btnIssueStart;
        private System.Windows.Forms.TextBox txtLogInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCountIdle;
    }
}