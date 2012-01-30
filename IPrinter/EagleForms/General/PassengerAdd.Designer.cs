namespace EagleForms.General
{
    partial class PassengerAdd
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbBunkSelectable = new System.Windows.Forms.ComboBox();
            this.btnPsgDelete = new System.Windows.Forms.Button();
            this.btnPsgAdd = new System.Windows.Forms.Button();
            this.lbCard = new System.Windows.Forms.ListBox();
            this.lbName = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblRebate = new System.Windows.Forms.Label();
            this.lblTime2 = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.lblBunk = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblFlight = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 249);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cbBunkSelectable);
            this.panel3.Controls.Add(this.btnPsgDelete);
            this.panel3.Controls.Add(this.btnPsgAdd);
            this.panel3.Controls.Add(this.lbCard);
            this.panel3.Controls.Add(this.lbName);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnOK);
            this.panel3.Controls.Add(this.txtCard);
            this.panel3.Controls.Add(this.txtName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(530, 176);
            this.panel3.TabIndex = 1;
            // 
            // cbBunkSelectable
            // 
            this.cbBunkSelectable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbBunkSelectable.FormattingEnabled = true;
            this.cbBunkSelectable.Location = new System.Drawing.Point(-1, 25);
            this.cbBunkSelectable.Name = "cbBunkSelectable";
            this.cbBunkSelectable.Size = new System.Drawing.Size(109, 152);
            this.cbBunkSelectable.TabIndex = 0;
            this.cbBunkSelectable.SelectedIndexChanged += new System.EventHandler(this.cbBunkSelectable_SelectedIndexChanged);
            // 
            // btnPsgDelete
            // 
            this.btnPsgDelete.Location = new System.Drawing.Point(442, 55);
            this.btnPsgDelete.Name = "btnPsgDelete";
            this.btnPsgDelete.Size = new System.Drawing.Size(75, 23);
            this.btnPsgDelete.TabIndex = 4;
            this.btnPsgDelete.Text = "删除(&D)";
            this.btnPsgDelete.UseVisualStyleBackColor = true;
            // 
            // btnPsgAdd
            // 
            this.btnPsgAdd.Location = new System.Drawing.Point(442, 26);
            this.btnPsgAdd.Name = "btnPsgAdd";
            this.btnPsgAdd.Size = new System.Drawing.Size(75, 23);
            this.btnPsgAdd.TabIndex = 3;
            this.btnPsgAdd.Text = "添加(&A)";
            this.btnPsgAdd.UseVisualStyleBackColor = true;
            this.btnPsgAdd.Click += new System.EventHandler(this.btnPsgAdd_Click);
            // 
            // lbCard
            // 
            this.lbCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCard.Enabled = false;
            this.lbCard.FormattingEnabled = true;
            this.lbCard.ItemHeight = 12;
            this.lbCard.Location = new System.Drawing.Point(246, 48);
            this.lbCard.Name = "lbCard";
            this.lbCard.Size = new System.Drawing.Size(190, 122);
            this.lbCard.TabIndex = 8;
            // 
            // lbName
            // 
            this.lbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbName.FormattingEnabled = true;
            this.lbName.ItemHeight = 12;
            this.lbName.Location = new System.Drawing.Point(114, 48);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(126, 122);
            this.lbName.TabIndex = 7;
            this.lbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbName_KeyUp);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(442, 147);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(442, 84);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 48);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "文本";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCard
            // 
            this.txtCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCard.Location = new System.Drawing.Point(246, 26);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(190, 21);
            this.txtCard.TabIndex = 2;
            this.txtCard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCard_KeyUp);
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(114, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(126, 21);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "身份证号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "可申请的舱位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "姓名：";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.lblPrice);
            this.panel2.Controls.Add(this.lblRebate);
            this.panel2.Controls.Add(this.lblTime2);
            this.panel2.Controls.Add(this.lblProfit);
            this.panel2.Controls.Add(this.lblBunk);
            this.panel2.Controls.Add(this.lblTime);
            this.panel2.Controls.Add(this.lblFlight);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(530, 75);
            this.panel2.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(430, 49);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(47, 12);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "lblDate";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(430, 23);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(41, 12);
            this.lblPrice.TabIndex = 1;
            this.lblPrice.Text = "label1";
            // 
            // lblRebate
            // 
            this.lblRebate.AutoSize = true;
            this.lblRebate.Location = new System.Drawing.Point(322, 49);
            this.lblRebate.Name = "lblRebate";
            this.lblRebate.Size = new System.Drawing.Size(41, 12);
            this.lblRebate.TabIndex = 1;
            this.lblRebate.Text = "label1";
            // 
            // lblTime2
            // 
            this.lblTime2.AutoSize = true;
            this.lblTime2.Location = new System.Drawing.Point(189, 49);
            this.lblTime2.Name = "lblTime2";
            this.lblTime2.Size = new System.Drawing.Size(41, 12);
            this.lblTime2.TabIndex = 1;
            this.lblTime2.Text = "label1";
            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Location = new System.Drawing.Point(322, 23);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(41, 12);
            this.lblProfit.TabIndex = 1;
            this.lblProfit.Text = "label1";
            // 
            // lblBunk
            // 
            this.lblBunk.AutoSize = true;
            this.lblBunk.Location = new System.Drawing.Point(11, 49);
            this.lblBunk.Name = "lblBunk";
            this.lblBunk.Size = new System.Drawing.Size(41, 12);
            this.lblBunk.TabIndex = 1;
            this.lblBunk.Text = "label1";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(189, 23);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(41, 12);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "label1";
            // 
            // lblFlight
            // 
            this.lblFlight.AutoSize = true;
            this.lblFlight.Location = new System.Drawing.Point(11, 23);
            this.lblFlight.Name = "lblFlight";
            this.lblFlight.Size = new System.Drawing.Size(41, 12);
            this.lblFlight.TabIndex = 1;
            this.lblFlight.Text = "label1";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(220, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(89, 12);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "选择的航班信息";
            // 
            // PassengerAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(530, 249);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PassengerAdd";
            this.Text = "添加旅客信息";
            this.Load += new System.EventHandler(this.PassengerAdd_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFlight;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbBunkSelectable;
        private System.Windows.Forms.Button btnPsgDelete;
        private System.Windows.Forms.Button btnPsgAdd;
        private System.Windows.Forms.ListBox lbCard;
        private System.Windows.Forms.ListBox lbName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime2;
        private System.Windows.Forms.Label lblBunk;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblRebate;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.Label lblDate;
    }
}