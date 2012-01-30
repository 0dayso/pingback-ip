namespace ePlus
{
    partial class LogonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSavePassword = new System.Windows.Forms.CheckBox();
            this.picLogin = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cbLoginName = new System.Windows.Forms.ComboBox();
            this.Del = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.rbDianx = new System.Windows.Forms.RadioButton();
            this.rbWangt = new System.Windows.Forms.RadioButton();
            this.lblRoute = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(358, 105);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(10, 10);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLogin.Location = new System.Drawing.Point(269, 162);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(64, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(62, 123);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(234, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "密  码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "用户名：";
            // 
            // cbSavePassword
            // 
            this.cbSavePassword.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbSavePassword.AutoSize = true;
            this.cbSavePassword.Location = new System.Drawing.Point(191, 166);
            this.cbSavePassword.Name = "cbSavePassword";
            this.cbSavePassword.Size = new System.Drawing.Size(72, 16);
            this.cbSavePassword.TabIndex = 2;
            this.cbSavePassword.Text = "记住密码";
            this.cbSavePassword.UseVisualStyleBackColor = true;
            // 
            // picLogin
            // 
            this.picLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogin.Location = new System.Drawing.Point(0, 0);
            this.picLogin.Name = "picLogin";
            this.picLogin.Size = new System.Drawing.Size(339, 75);
            this.picLogin.TabIndex = 9;
            this.picLogin.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 188);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(339, 13);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Visible = false;
            // 
            // cbLoginName
            // 
            this.cbLoginName.FormattingEnabled = true;
            this.cbLoginName.Location = new System.Drawing.Point(62, 91);
            this.cbLoginName.Name = "cbLoginName";
            this.cbLoginName.Size = new System.Drawing.Size(234, 20);
            this.cbLoginName.Sorted = true;
            this.cbLoginName.TabIndex = 0;
            this.cbLoginName.SelectedIndexChanged += new System.EventHandler(this.cbLoginName_SelectedIndexChanged);
            // 
            // Del
            // 
            this.Del.Image = global::ePlus.Properties.Resources.DeleteHS;
            this.Del.Location = new System.Drawing.Point(302, 89);
            this.Del.Name = "Del";
            this.Del.Size = new System.Drawing.Size(31, 23);
            this.Del.TabIndex = 4;
            this.Del.UseVisualStyleBackColor = true;
            this.Del.Click += new System.EventHandler(this.Del_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.Active = false;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // rbDianx
            // 
            this.rbDianx.AutoSize = true;
            this.rbDianx.Location = new System.Drawing.Point(65, 150);
            this.rbDianx.Name = "rbDianx";
            this.rbDianx.Size = new System.Drawing.Size(47, 16);
            this.rbDianx.TabIndex = 11;
            this.rbDianx.TabStop = true;
            this.rbDianx.Text = "电信";
            this.rbDianx.UseVisualStyleBackColor = true;
            this.rbDianx.CheckedChanged += new System.EventHandler(this.rbDianx_CheckedChanged);
            // 
            // rbWangt
            // 
            this.rbWangt.AutoSize = true;
            this.rbWangt.Location = new System.Drawing.Point(118, 150);
            this.rbWangt.Name = "rbWangt";
            this.rbWangt.Size = new System.Drawing.Size(47, 16);
            this.rbWangt.TabIndex = 12;
            this.rbWangt.TabStop = true;
            this.rbWangt.Text = "网通";
            this.rbWangt.UseVisualStyleBackColor = true;
            this.rbWangt.CheckedChanged += new System.EventHandler(this.rbWangt_CheckedChanged);
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.Location = new System.Drawing.Point(10, 152);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(53, 12);
            this.lblRoute.TabIndex = 13;
            this.lblRoute.Text = "线  路：";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "卡了吗？";
            // 
            // LogonForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 201);
            this.Controls.Add(this.lblRoute);
            this.Controls.Add(this.rbWangt);
            this.Controls.Add(this.rbDianx);
            this.Controls.Add(this.picLogin);
            this.Controls.Add(this.Del);
            this.Controls.Add(this.cbLoginName);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cbSavePassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogonForm_FormClosed);
            this.Load += new System.EventHandler(this.LogonForm_Load);
            this.Shown += new System.EventHandler(this.LogonForm_Shown);
            this.Move += new System.EventHandler(this.LogonForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSavePassword;
        private System.Windows.Forms.PictureBox picLogin;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cbLoginName;
        private System.Windows.Forms.Button Del;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.RadioButton rbDianx;
        private System.Windows.Forms.RadioButton rbWangt;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}