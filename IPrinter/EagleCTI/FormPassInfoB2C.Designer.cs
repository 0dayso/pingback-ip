namespace EagleCTI
{
    partial class FormPassInfoB2C
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassInfoB2C));
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhoneQuery = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSMS = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSepcialNo = new System.Windows.Forms.TextBox();
            this.lblSepcialNo = new System.Windows.Forms.Label();
            this.pbCustomerDetail = new System.Windows.Forms.PictureBox();
            this.cmbIdentity = new System.Windows.Forms.ComboBox();
            this.cmbLandline = new System.Windows.Forms.ComboBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlHistory = new System.Windows.Forms.TabControl();
            this.tabPageFlight = new System.Windows.Forms.TabPage();
            this.lvOrderHistory = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.cMenuPayment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退款RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.支付明细报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageTravel = new System.Windows.Forms.TabPage();
            this.lvTravel = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.lvMemo = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.btnMemoAdd = new System.Windows.Forms.Button();
            this.txtIdentityType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomerNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.timerColorful = new System.Windows.Forms.Timer(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomerDetail)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlHistory.SuspendLayout();
            this.tabPageFlight.SuspendLayout();
            this.cMenuPayment.SuspendLayout();
            this.tabPageTravel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "客户姓名：";
            // 
            // txtPhoneQuery
            // 
            this.txtPhoneQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhoneQuery.BackColor = System.Drawing.Color.White;
            this.txtPhoneQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneQuery.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPhoneQuery.Location = new System.Drawing.Point(8, 7);
            this.txtPhoneQuery.Name = "txtPhoneQuery";
            this.txtPhoneQuery.Size = new System.Drawing.Size(173, 24);
            this.txtPhoneQuery.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(71, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(115, 21);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Enabled = false;
            this.btnSaveCustomer.Location = new System.Drawing.Point(72, 237);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCustomer.TabIndex = 4;
            this.btnSaveCustomer.Text = "保 存(&E)";
            this.btnSaveCustomer.UseVisualStyleBackColor = true;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btSaveCost_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbSMS);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtSepcialNo);
            this.groupBox1.Controls.Add(this.lblSepcialNo);
            this.groupBox1.Controls.Add(this.pbCustomerDetail);
            this.groupBox1.Controls.Add(this.cmbIdentity);
            this.groupBox1.Controls.Add(this.cmbLandline);
            this.groupBox1.Controls.Add(this.rbFemale);
            this.groupBox1.Controls.Add(this.rbMale);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Controls.Add(this.txtIdentityType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustomerNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSaveCustomer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 518);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户资料";
            // 
            // cbSMS
            // 
            this.cbSMS.AutoSize = true;
            this.cbSMS.Checked = true;
            this.cbSMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSMS.Location = new System.Drawing.Point(71, 216);
            this.cbSMS.Name = "cbSMS";
            this.cbSMS.Size = new System.Drawing.Size(36, 16);
            this.cbSMS.TabIndex = 28;
            this.cbSMS.Text = "是";
            this.cbSMS.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "接收短信：";
            // 
            // txtSepcialNo
            // 
            this.txtSepcialNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSepcialNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSepcialNo.Location = new System.Drawing.Point(71, 191);
            this.txtSepcialNo.Name = "txtSepcialNo";
            this.txtSepcialNo.Size = new System.Drawing.Size(115, 21);
            this.txtSepcialNo.TabIndex = 26;
            // 
            // lblSepcialNo
            // 
            this.lblSepcialNo.AutoSize = true;
            this.lblSepcialNo.Location = new System.Drawing.Point(6, 195);
            this.lblSepcialNo.Name = "lblSepcialNo";
            this.lblSepcialNo.Size = new System.Drawing.Size(65, 12);
            this.lblSepcialNo.TabIndex = 25;
            this.lblSepcialNo.Text = "贵宾卡号：";
            // 
            // pbCustomerDetail
            // 
            this.pbCustomerDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCustomerDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCustomerDetail.Image = ((System.Drawing.Image)(resources.GetObject("pbCustomerDetail.Image")));
            this.pbCustomerDetail.Location = new System.Drawing.Point(191, 20);
            this.pbCustomerDetail.Name = "pbCustomerDetail";
            this.pbCustomerDetail.Size = new System.Drawing.Size(21, 21);
            this.pbCustomerDetail.TabIndex = 24;
            this.pbCustomerDetail.TabStop = false;
            this.pbCustomerDetail.Tag = "显示客户详细信息";
            this.pbCustomerDetail.Click += new System.EventHandler(this.pbCustomerDetail_Click);
            // 
            // cmbIdentity
            // 
            this.cmbIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIdentity.FormattingEnabled = true;
            this.cmbIdentity.Location = new System.Drawing.Point(71, 119);
            this.cmbIdentity.Name = "cmbIdentity";
            this.cmbIdentity.Size = new System.Drawing.Size(115, 20);
            this.cmbIdentity.TabIndex = 23;
            // 
            // cmbLandline
            // 
            this.cmbLandline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLandline.FormattingEnabled = true;
            this.cmbLandline.Location = new System.Drawing.Point(71, 45);
            this.cmbLandline.Name = "cmbLandline";
            this.cmbLandline.Size = new System.Drawing.Size(115, 20);
            this.cmbLandline.TabIndex = 22;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(112, 145);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(35, 16);
            this.rbFemale.TabIndex = 21;
            this.rbFemale.Text = "女";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(71, 145);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(35, 16);
            this.rbMale.TabIndex = 20;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "男";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "性    别：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(8, 266);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlHistory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvMemo);
            this.splitContainer1.Panel2.Controls.Add(this.txtMemo);
            this.splitContainer1.Panel2.Controls.Add(this.btnMemoAdd);
            this.splitContainer1.Size = new System.Drawing.Size(204, 246);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.TabIndex = 18;
            // 
            // tabControlHistory
            // 
            this.tabControlHistory.Controls.Add(this.tabPageFlight);
            this.tabControlHistory.Controls.Add(this.tabPageTravel);
            this.tabControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHistory.ItemSize = new System.Drawing.Size(48, 17);
            this.tabControlHistory.Location = new System.Drawing.Point(0, 0);
            this.tabControlHistory.Name = "tabControlHistory";
            this.tabControlHistory.SelectedIndex = 0;
            this.tabControlHistory.Size = new System.Drawing.Size(204, 120);
            this.tabControlHistory.TabIndex = 18;
            // 
            // tabPageFlight
            // 
            this.tabPageFlight.Controls.Add(this.lvOrderHistory);
            this.tabPageFlight.Location = new System.Drawing.Point(4, 21);
            this.tabPageFlight.Name = "tabPageFlight";
            this.tabPageFlight.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFlight.Size = new System.Drawing.Size(196, 95);
            this.tabPageFlight.TabIndex = 0;
            this.tabPageFlight.Text = "机票";
            this.tabPageFlight.UseVisualStyleBackColor = true;
            // 
            // lvOrderHistory
            // 
            this.lvOrderHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader8,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6});
            this.lvOrderHistory.ContextMenuStrip = this.cMenuPayment;
            this.lvOrderHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvOrderHistory.FullRowSelect = true;
            this.lvOrderHistory.Location = new System.Drawing.Point(3, 3);
            this.lvOrderHistory.Name = "lvOrderHistory";
            this.lvOrderHistory.Size = new System.Drawing.Size(190, 89);
            this.lvOrderHistory.TabIndex = 17;
            this.lvOrderHistory.UseCompatibleStateImageBehavior = false;
            this.lvOrderHistory.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "状态";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "出发";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "到达";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "起飞日期";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "航班号";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "PNR";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "乘客";
            this.columnHeader6.Width = 200;
            // 
            // cMenuPayment
            // 
            this.cMenuPayment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退款RToolStripMenuItem,
            this.撤销RToolStripMenuItem,
            this.支付明细报表ToolStripMenuItem});
            this.cMenuPayment.Name = "cMenuPayment";
            this.cMenuPayment.Size = new System.Drawing.Size(163, 70);
            this.cMenuPayment.Opening += new System.ComponentModel.CancelEventHandler(this.cMenuPayment_Opening);
            // 
            // 退款RToolStripMenuItem
            // 
            this.退款RToolStripMenuItem.Name = "退款RToolStripMenuItem";
            this.退款RToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.退款RToolStripMenuItem.Text = "退款(&B)";
            this.退款RToolStripMenuItem.Click += new System.EventHandler(this.退款RToolStripMenuItem_Click);
            // 
            // 撤销RToolStripMenuItem
            // 
            this.撤销RToolStripMenuItem.Name = "撤销RToolStripMenuItem";
            this.撤销RToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.撤销RToolStripMenuItem.Text = "撤销(R)";
            this.撤销RToolStripMenuItem.Click += new System.EventHandler(this.撤销RToolStripMenuItem_Click);
            // 
            // 支付明细报表ToolStripMenuItem
            // 
            this.支付明细报表ToolStripMenuItem.Name = "支付明细报表ToolStripMenuItem";
            this.支付明细报表ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.支付明细报表ToolStripMenuItem.Text = "支付明细报表(&M)";
            this.支付明细报表ToolStripMenuItem.Click += new System.EventHandler(this.支付明细报表ToolStripMenuItem_Click);
            // 
            // tabPageTravel
            // 
            this.tabPageTravel.Controls.Add(this.lvTravel);
            this.tabPageTravel.Location = new System.Drawing.Point(4, 21);
            this.tabPageTravel.Name = "tabPageTravel";
            this.tabPageTravel.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTravel.Size = new System.Drawing.Size(196, 95);
            this.tabPageTravel.TabIndex = 1;
            this.tabPageTravel.Text = "旅游";
            this.tabPageTravel.UseVisualStyleBackColor = true;
            // 
            // lvTravel
            // 
            this.lvTravel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvTravel.ContextMenuStrip = this.cMenuPayment;
            this.lvTravel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTravel.FullRowSelect = true;
            this.lvTravel.Location = new System.Drawing.Point(3, 3);
            this.lvTravel.Name = "lvTravel";
            this.lvTravel.Size = new System.Drawing.Size(190, 89);
            this.lvTravel.TabIndex = 18;
            this.lvTravel.UseCompatibleStateImageBehavior = false;
            this.lvTravel.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "日期";
            this.columnHeader9.Width = 80;
            // 
            // lvMemo
            // 
            this.lvMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMemo.ContextMenuStrip = this.contextMenuStrip1;
            this.lvMemo.Location = new System.Drawing.Point(0, 0);
            this.lvMemo.MultiSelect = false;
            this.lvMemo.Name = "lvMemo";
            this.lvMemo.Size = new System.Drawing.Size(204, 95);
            this.lvMemo.TabIndex = 15;
            this.lvMemo.UseCompatibleStateImageBehavior = false;
            this.lvMemo.View = System.Windows.Forms.View.SmallIcon;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制CToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // 复制CToolStripMenuItem
            // 
            this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
            this.复制CToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.复制CToolStripMenuItem.Text = "复制(&C)";
            this.复制CToolStripMenuItem.Click += new System.EventHandler(this.复制CToolStripMenuItem_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemo.Location = new System.Drawing.Point(0, 100);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(118, 21);
            this.txtMemo.TabIndex = 16;
            // 
            // btnMemoAdd
            // 
            this.btnMemoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMemoAdd.Enabled = false;
            this.btnMemoAdd.Location = new System.Drawing.Point(124, 99);
            this.btnMemoAdd.Name = "btnMemoAdd";
            this.btnMemoAdd.Size = new System.Drawing.Size(79, 23);
            this.btnMemoAdd.TabIndex = 14;
            this.btnMemoAdd.Text = "添加备注(&M)";
            this.btnMemoAdd.UseVisualStyleBackColor = true;
            this.btnMemoAdd.Click += new System.EventHandler(this.btnMemoAdd_Click);
            // 
            // txtIdentityType
            // 
            this.txtIdentityType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentityType.FormattingEnabled = true;
            this.txtIdentityType.Items.AddRange(new object[] {
            "",
            "身份证",
            "军官证",
            "护照"});
            this.txtIdentityType.Location = new System.Drawing.Point(71, 94);
            this.txtIdentityType.Name = "txtIdentityType";
            this.txtIdentityType.Size = new System.Drawing.Size(115, 20);
            this.txtIdentityType.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "证件类型：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(71, 165);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(115, 21);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.Value = new System.DateTime(2008, 10, 1, 2, 24, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "注册日期：";
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerNo.Location = new System.Drawing.Point(71, 69);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.Size = new System.Drawing.Size(115, 21);
            this.txtCustomerNo.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "会员编号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "联系电话：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "证件号码：";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(187, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(38, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "搜索";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // timerColorful
            // 
            this.timerColorful.Interval = 500;
            this.timerColorful.Tick += new System.EventHandler(this.timerColorful_Tick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormPassInfoB2C
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 559);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtPhoneQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormPassInfoB2C";
            this.Text = "来电信息";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormPassInfoB2C_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPassInfoB2C_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomerDetail)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlHistory.ResumeLayout(false);
            this.tabPageFlight.ResumeLayout(false);
            this.cMenuPayment.ResumeLayout(false);
            this.tabPageTravel.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPhoneQuery;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCustomerNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox txtIdentityType;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnMemoAdd;
        private System.Windows.Forms.ListView lvMemo;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Timer timerColorful;
        private System.Windows.Forms.ListView lvOrderHistory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cmbLandline;
        private System.Windows.Forms.ComboBox cmbIdentity;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.PictureBox pbCustomerDetail;
        private System.Windows.Forms.TextBox txtSepcialNo;
        private System.Windows.Forms.Label lblSepcialNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbSMS;
        private System.Windows.Forms.ContextMenuStrip cMenuPayment;
        private System.Windows.Forms.ToolStripMenuItem 退款RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 支付明细报表ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlHistory;
        private System.Windows.Forms.TabPage tabPageFlight;
        private System.Windows.Forms.TabPage tabPageTravel;
        private System.Windows.Forms.ListView lvTravel;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}