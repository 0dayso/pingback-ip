namespace EagleForms.Printer
{
    partial class FrmInsuranceList
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.补打PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.作废DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.colEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDatetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFlightNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFlightDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCaseNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPolicyNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colproductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEnabled,
            this.colDatetime,
            this.colCustomerName,
            this.colCustomerId,
            this.colFlightNo,
            this.colFlightDate,
            this.colCaseNo,
            this.colPolicyNo,
            this.colproductName});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(0, 40);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(646, 336);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.补打PToolStripMenuItem,
            this.作废DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 补打PToolStripMenuItem
            // 
            this.补打PToolStripMenuItem.Name = "补打PToolStripMenuItem";
            this.补打PToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.补打PToolStripMenuItem.Text = "补打(&P)";
            this.补打PToolStripMenuItem.Click += new System.EventHandler(this.补打PToolStripMenuItem_Click);
            // 
            // 作废DToolStripMenuItem
            // 
            this.作废DToolStripMenuItem.Name = "作废DToolStripMenuItem";
            this.作废DToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.作废DToolStripMenuItem.Text = "作废(&D)";
            this.作废DToolStripMenuItem.Click += new System.EventHandler(this.作废DToolStripMenuItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(498, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(71, 22);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(575, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(71, 22);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "补打(&P)";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = global::EagleForms.Properties.Resources.load;
            this.picLoading.Location = new System.Drawing.Point(502, 13);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(16, 16);
            this.picLoading.TabIndex = 5;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy-M-d";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtpStartDate.Location = new System.Drawing.Point(61, 11);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(101, 21);
            this.dtpStartDate.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "起始日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "截止日期";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy-M-d";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtpEndDate.Location = new System.Drawing.Point(235, 11);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(101, 21);
            this.dtpEndDate.TabIndex = 11;
            // 
            // colEnabled
            // 
            this.colEnabled.DataPropertyName = "enabled";
            this.colEnabled.HeaderText = "有效 ";
            this.colEnabled.Name = "colEnabled";
            this.colEnabled.ReadOnly = true;
            this.colEnabled.Width = 37;
            // 
            // colDatetime
            // 
            this.colDatetime.DataPropertyName = "datetime";
            this.colDatetime.HeaderText = "出单时间";
            this.colDatetime.Name = "colDatetime";
            this.colDatetime.ReadOnly = true;
            this.colDatetime.Width = 78;
            // 
            // colCustomerName
            // 
            this.colCustomerName.DataPropertyName = "customerName";
            this.colCustomerName.HeaderText = "姓名";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            this.colCustomerName.Width = 54;
            // 
            // colCustomerId
            // 
            this.colCustomerId.DataPropertyName = "customerId";
            this.colCustomerId.HeaderText = "证件号码";
            this.colCustomerId.Name = "colCustomerId";
            this.colCustomerId.ReadOnly = true;
            this.colCustomerId.Width = 78;
            // 
            // colFlightNo
            // 
            this.colFlightNo.DataPropertyName = "customerFlightNo";
            this.colFlightNo.HeaderText = "航班号";
            this.colFlightNo.Name = "colFlightNo";
            this.colFlightNo.ReadOnly = true;
            this.colFlightNo.Width = 66;
            // 
            // colFlightDate
            // 
            this.colFlightDate.DataPropertyName = "customerFlightDate";
            this.colFlightDate.HeaderText = "乘机时间";
            this.colFlightDate.Name = "colFlightDate";
            this.colFlightDate.ReadOnly = true;
            this.colFlightDate.Width = 78;
            // 
            // colCaseNo
            // 
            this.colCaseNo.DataPropertyName = "caseNo";
            this.colCaseNo.HeaderText = "电子单号";
            this.colCaseNo.Name = "colCaseNo";
            this.colCaseNo.ReadOnly = true;
            this.colCaseNo.Width = 78;
            // 
            // colPolicyNo
            // 
            this.colPolicyNo.DataPropertyName = "certNo";
            this.colPolicyNo.HeaderText = "保单号";
            this.colPolicyNo.Name = "colPolicyNo";
            this.colPolicyNo.ReadOnly = true;
            this.colPolicyNo.Width = 66;
            // 
            // colproductName
            // 
            this.colproductName.DataPropertyName = "productName";
            this.colproductName.HeaderText = "单证类型";
            this.colproductName.Name = "colproductName";
            this.colproductName.ReadOnly = true;
            this.colproductName.Width = 78;
            // 
            // FrmInsuranceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 376);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnRefresh);
            this.Name = "FrmInsuranceList";
            this.Text = "出单记录";
            this.Load += new System.EventHandler(this.FrmInsuranceList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 补打PToolStripMenuItem;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolStripMenuItem 作废DToolStripMenuItem;
        private System.Windows.Forms.PictureBox picLoading;
        public System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDatetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlightNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlightDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCaseNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPolicyNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colproductName;
    }
}