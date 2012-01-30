namespace EagleForms.General
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.Del = new System.Windows.Forms.Button();
            this.cbLoginName = new System.Windows.Forms.ComboBox();
            this.cbAutoLogin = new System.Windows.Forms.CheckBox();
            this.cbSavePassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pUp = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbProvider = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pDown = new System.Windows.Forms.Panel();
            this.txtCTIPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblCTIPhoneNumber = new System.Windows.Forms.Label();
            this.cbBtocUser = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pLeft = new System.Windows.Forms.Panel();
            this.pRight = new System.Windows.Forms.Panel();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loadingCircle1 = new EagleControls.LoadingCircle();
            this.label6 = new System.Windows.Forms.Label();
            this.txtB2cService = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtB2cManager = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pUp.SuspendLayout();
            this.pDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.Del);
            this.panel1.Controls.Add(this.cbLoginName);
            this.panel1.Controls.Add(this.cbAutoLogin);
            this.panel1.Controls.Add(this.cbSavePassword);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(269, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 95);
            this.panel1.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(196, 26);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(31, 20);
            this.btnClear.TabIndex = 12;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Del
            // 
            this.Del.Location = new System.Drawing.Point(196, 5);
            this.Del.Name = "Del";
            this.Del.Size = new System.Drawing.Size(31, 20);
            this.Del.TabIndex = 12;
            this.Del.UseVisualStyleBackColor = true;
            this.Del.Click += new System.EventHandler(this.Del_Click);
            // 
            // cbLoginName
            // 
            this.cbLoginName.DropDownHeight = 400;
            this.cbLoginName.FormattingEnabled = true;
            this.cbLoginName.IntegralHeight = false;
            this.cbLoginName.Location = new System.Drawing.Point(57, 5);
            this.cbLoginName.Name = "cbLoginName";
            this.cbLoginName.Size = new System.Drawing.Size(133, 20);
            this.cbLoginName.Sorted = true;
            this.cbLoginName.TabIndex = 8;
            this.cbLoginName.SelectedIndexChanged += new System.EventHandler(this.cbLoginName_SelectedIndexChanged);
            // 
            // cbAutoLogin
            // 
            this.cbAutoLogin.AutoSize = true;
            this.cbAutoLogin.Enabled = false;
            this.cbAutoLogin.Location = new System.Drawing.Point(135, 48);
            this.cbAutoLogin.Name = "cbAutoLogin";
            this.cbAutoLogin.Size = new System.Drawing.Size(72, 16);
            this.cbAutoLogin.TabIndex = 10;
            this.cbAutoLogin.Text = "自动登录";
            this.cbAutoLogin.UseVisualStyleBackColor = true;
            // 
            // cbSavePassword
            // 
            this.cbSavePassword.AutoSize = true;
            this.cbSavePassword.Location = new System.Drawing.Point(57, 48);
            this.cbSavePassword.Name = "cbSavePassword";
            this.cbSavePassword.Size = new System.Drawing.Size(72, 16);
            this.cbSavePassword.TabIndex = 10;
            this.cbSavePassword.Text = "记住密码";
            this.cbSavePassword.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(84, 67);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(64, 23);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "登 录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(57, 25);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(133, 21);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "密  码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "用户名：";
            // 
            // pUp
            // 
            this.pUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pUp.Controls.Add(this.comboBox2);
            this.pUp.Controls.Add(this.label3);
            this.pUp.Controls.Add(this.cbServer);
            this.pUp.Controls.Add(this.label4);
            this.pUp.Controls.Add(this.cbProvider);
            this.pUp.Controls.Add(this.label5);
            this.pUp.Location = new System.Drawing.Point(234, 156);
            this.pUp.Name = "pUp";
            this.pUp.Size = new System.Drawing.Size(242, 75);
            this.pUp.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "默认服务器",
            "www.eg66.com(电信)"});
            this.comboBox2.Location = new System.Drawing.Point(35, 51);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(204, 20);
            this.comboBox2.TabIndex = 13;
            this.toolTip1.SetToolTip(this.comboBox2, "选择自动更新服务器");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "更新";
            // 
            // cbServer
            // 
            this.cbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Items.AddRange(new object[] {
            "默认服务器",
            "www.eg66.com(电信)"});
            this.cbServer.Location = new System.Drawing.Point(35, 27);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(204, 20);
            this.cbServer.TabIndex = 13;
            this.toolTip1.SetToolTip(this.cbServer, "要登陆的目标服务器");
            this.cbServer.SelectedIndexChanged += new System.EventHandler(this.cbServer_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "目标";
            // 
            // cbProvider
            // 
            this.cbProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProvider.FormattingEnabled = true;
            this.cbProvider.Items.AddRange(new object[] {
            "电信",
            "网通",
            "默认"});
            this.cbProvider.Location = new System.Drawing.Point(35, 3);
            this.cbProvider.Name = "cbProvider";
            this.cbProvider.Size = new System.Drawing.Size(204, 20);
            this.cbProvider.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cbProvider, "选择您的网络(电信，网通或者其它)");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "线路";
            // 
            // pDown
            // 
            this.pDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pDown.Controls.Add(this.txtB2cManager);
            this.pDown.Controls.Add(this.label7);
            this.pDown.Controls.Add(this.txtB2cService);
            this.pDown.Controls.Add(this.label6);
            this.pDown.Controls.Add(this.txtCTIPhoneNumber);
            this.pDown.Controls.Add(this.lblCTIPhoneNumber);
            this.pDown.Controls.Add(this.cbBtocUser);
            this.pDown.Location = new System.Drawing.Point(234, 368);
            this.pDown.Name = "pDown";
            this.pDown.Size = new System.Drawing.Size(242, 88);
            this.pDown.TabIndex = 2;
            // 
            // txtCTIPhoneNumber
            // 
            this.txtCTIPhoneNumber.Location = new System.Drawing.Point(35, 20);
            this.txtCTIPhoneNumber.Name = "txtCTIPhoneNumber";
            this.txtCTIPhoneNumber.ReadOnly = true;
            this.txtCTIPhoneNumber.Size = new System.Drawing.Size(204, 21);
            this.txtCTIPhoneNumber.TabIndex = 18;
            // 
            // lblCTIPhoneNumber
            // 
            this.lblCTIPhoneNumber.AutoSize = true;
            this.lblCTIPhoneNumber.Location = new System.Drawing.Point(3, 23);
            this.lblCTIPhoneNumber.Name = "lblCTIPhoneNumber";
            this.lblCTIPhoneNumber.Size = new System.Drawing.Size(29, 12);
            this.lblCTIPhoneNumber.TabIndex = 17;
            this.lblCTIPhoneNumber.Text = "分机";
            // 
            // cbBtocUser
            // 
            this.cbBtocUser.AutoSize = true;
            this.cbBtocUser.Location = new System.Drawing.Point(46, 4);
            this.cbBtocUser.Name = "cbBtocUser";
            this.cbBtocUser.Size = new System.Drawing.Size(72, 16);
            this.cbBtocUser.TabIndex = 0;
            this.cbBtocUser.Text = "BTOC用户";
            this.cbBtocUser.UseVisualStyleBackColor = true;
            this.cbBtocUser.CheckedChanged += new System.EventHandler(this.cbBtocUser_CheckedChanged);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDown.ForeColor = System.Drawing.Color.Red;
            this.btnDown.Location = new System.Drawing.Point(269, 349);
            this.btnDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(16, 16);
            this.btnDown.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnDown, "显示或隐藏BTOC选项");
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUp.ForeColor = System.Drawing.Color.Red;
            this.btnUp.Location = new System.Drawing.Point(269, 234);
            this.btnUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(16, 16);
            this.btnUp.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnUp, "显示或隐藏网络选项");
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Blue;
            this.btnMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMinimize.BackgroundImage")));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinimize.ForeColor = System.Drawing.Color.Red;
            this.btnMinimize.Location = new System.Drawing.Point(467, 234);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(16, 16);
            this.btnMinimize.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnMinimize, "最小化");
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Blue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Location = new System.Drawing.Point(485, 234);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnClose, "关闭");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pLeft
            // 
            this.pLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pLeft.Location = new System.Drawing.Point(79, 261);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(164, 75);
            this.pLeft.TabIndex = 2;
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pRight.Location = new System.Drawing.Point(521, 261);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(164, 75);
            this.pRight.TabIndex = 2;
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLeft.BackgroundImage")));
            this.btnLeft.FlatAppearance.BorderSize = 0;
            this.btnLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLeft.ForeColor = System.Drawing.Color.Red;
            this.btnLeft.Location = new System.Drawing.Point(246, 301);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(0);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(16, 16);
            this.btnLeft.TabIndex = 0;
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRight.BackgroundImage")));
            this.btnRight.FlatAppearance.BorderSize = 0;
            this.btnRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRight.ForeColor = System.Drawing.Color.Red;
            this.btnRight.Location = new System.Drawing.Point(503, 301);
            this.btnRight.Margin = new System.Windows.Forms.Padding(0);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(16, 16);
            this.btnRight.TabIndex = 0;
            this.btnRight.UseVisualStyleBackColor = false;
            this.btnRight.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 600);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.Lime;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(449, 391);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(116, 114);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = EagleControls.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 3;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "服务";
            // 
            // txtB2cService
            // 
            this.txtB2cService.Location = new System.Drawing.Point(35, 41);
            this.txtB2cService.Name = "txtB2cService";
            this.txtB2cService.ReadOnly = true;
            this.txtB2cService.Size = new System.Drawing.Size(204, 21);
            this.txtB2cService.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "后台";
            // 
            // txtB2cManager
            // 
            this.txtB2cManager.Location = new System.Drawing.Point(35, 62);
            this.txtB2cManager.Name = "txtB2cManager";
            this.txtB2cManager.ReadOnly = true;
            this.txtB2cManager.Size = new System.Drawing.Size(204, 21);
            this.txtB2cManager.TabIndex = 18;
            // 
            // Login
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pDown);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pUp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pUp.ResumeLayout(false);
            this.pUp.PerformLayout();
            this.pDown.ResumeLayout(false);
            this.pDown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Del;
        private System.Windows.Forms.ComboBox cbLoginName;
        private System.Windows.Forms.CheckBox cbAutoLogin;
        private System.Windows.Forms.CheckBox cbSavePassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pUp;
        private System.Windows.Forms.Panel pDown;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.CheckBox cbBtocUser;
        private System.Windows.Forms.TextBox txtCTIPhoneNumber;
        private System.Windows.Forms.Label lblCTIPhoneNumber;
        private System.Windows.Forms.ComboBox cbProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private EagleControls.LoadingCircle loadingCircle1;
        private System.Windows.Forms.TextBox txtB2cManager;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtB2cService;
        private System.Windows.Forms.Label label6;
    }
}