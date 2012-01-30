namespace EagleCTI
{
    partial class FormPassInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassInfo));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEgUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTele = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCostName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCostCardId = new System.Windows.Forms.TextBox();
            this.btSaveCost = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Top;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(292, 153);
            this.webBrowser1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btSaveCost);
            this.groupBox1.Controls.Add(this.tbCostCardId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbCostName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbTele);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbEgUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 297);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "录入资料";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "录入者ID:";
            // 
            // tbEgUser
            // 
            this.tbEgUser.Location = new System.Drawing.Point(125, 14);
            this.tbEgUser.Name = "tbEgUser";
            this.tbEgUser.ReadOnly = true;
            this.tbEgUser.Size = new System.Drawing.Size(155, 21);
            this.tbEgUser.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "客户来电:";
            // 
            // tbTele
            // 
            this.tbTele.Location = new System.Drawing.Point(125, 41);
            this.tbTele.Name = "tbTele";
            this.tbTele.Size = new System.Drawing.Size(155, 21);
            this.tbTele.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "客户姓名:";
            // 
            // tbCostName
            // 
            this.tbCostName.Location = new System.Drawing.Point(125, 68);
            this.tbCostName.Name = "tbCostName";
            this.tbCostName.Size = new System.Drawing.Size(155, 21);
            this.tbCostName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "客户证件号:";
            // 
            // tbCostCardId
            // 
            this.tbCostCardId.Location = new System.Drawing.Point(125, 95);
            this.tbCostCardId.Name = "tbCostCardId";
            this.tbCostCardId.Size = new System.Drawing.Size(155, 21);
            this.tbCostCardId.TabIndex = 3;
            // 
            // btSaveCost
            // 
            this.btSaveCost.Location = new System.Drawing.Point(125, 122);
            this.btSaveCost.Name = "btSaveCost";
            this.btSaveCost.Size = new System.Drawing.Size(75, 23);
            this.btSaveCost.TabIndex = 4;
            this.btSaveCost.Text = "录入！";
            this.btSaveCost.UseVisualStyleBackColor = true;
            this.btSaveCost.Click += new System.EventHandler(this.btSaveCost_Click);
            // 
            // FormPassInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPassInfo";
            this.Text = "客户信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPassInfo_FormClosing);
            this.Load += new System.EventHandler(this.FormPassInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbCostCardId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTele;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEgUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSaveCost;
    }
}