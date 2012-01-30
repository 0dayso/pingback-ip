namespace EagleForms
{
    partial class GroupeAddPassenger
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
            this.bt_DeletePsger = new System.Windows.Forms.Button();
            this.bt_AddPsger = new System.Windows.Forms.Button();
            this.lb_CardNo = new System.Windows.Forms.ListBox();
            this.lb_PsgerName = new System.Windows.Forms.ListBox();
            this.bt_Close = new System.Windows.Forms.Button();
            this.bt_AddToGroup = new System.Windows.Forms.Button();
            this.tb_CardNo = new System.Windows.Forms.TextBox();
            this.tb_PsgerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_DeletePsger);
            this.panel1.Controls.Add(this.bt_AddPsger);
            this.panel1.Controls.Add(this.lb_CardNo);
            this.panel1.Controls.Add(this.lb_PsgerName);
            this.panel1.Controls.Add(this.bt_Close);
            this.panel1.Controls.Add(this.bt_AddToGroup);
            this.panel1.Controls.Add(this.tb_CardNo);
            this.panel1.Controls.Add(this.tb_PsgerName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 212);
            this.panel1.TabIndex = 9;
            // 
            // bt_DeletePsger
            // 
            this.bt_DeletePsger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_DeletePsger.Location = new System.Drawing.Point(218, 53);
            this.bt_DeletePsger.Name = "bt_DeletePsger";
            this.bt_DeletePsger.Size = new System.Drawing.Size(77, 23);
            this.bt_DeletePsger.TabIndex = 3;
            this.bt_DeletePsger.Text = "删除(&D)";
            this.bt_DeletePsger.UseVisualStyleBackColor = true;
            this.bt_DeletePsger.Click += new System.EventHandler(this.bt_DeletePsger_Click);
            // 
            // bt_AddPsger
            // 
            this.bt_AddPsger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_AddPsger.Location = new System.Drawing.Point(218, 24);
            this.bt_AddPsger.Name = "bt_AddPsger";
            this.bt_AddPsger.Size = new System.Drawing.Size(77, 23);
            this.bt_AddPsger.TabIndex = 2;
            this.bt_AddPsger.Text = "添加(&A)";
            this.bt_AddPsger.UseVisualStyleBackColor = true;
            this.bt_AddPsger.Click += new System.EventHandler(this.bt_AddPsger_Click);
            // 
            // lb_CardNo
            // 
            this.lb_CardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_CardNo.Enabled = false;
            this.lb_CardNo.FormattingEnabled = true;
            this.lb_CardNo.ItemHeight = 12;
            this.lb_CardNo.Location = new System.Drawing.Point(73, 51);
            this.lb_CardNo.Name = "lb_CardNo";
            this.lb_CardNo.Size = new System.Drawing.Size(141, 158);
            this.lb_CardNo.TabIndex = 18;
            // 
            // lb_PsgerName
            // 
            this.lb_PsgerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PsgerName.FormattingEnabled = true;
            this.lb_PsgerName.ItemHeight = 12;
            this.lb_PsgerName.Location = new System.Drawing.Point(0, 51);
            this.lb_PsgerName.Name = "lb_PsgerName";
            this.lb_PsgerName.Size = new System.Drawing.Size(69, 158);
            this.lb_PsgerName.TabIndex = 17;
            // 
            // bt_Close
            // 
            this.bt_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_Close.Location = new System.Drawing.Point(218, 186);
            this.bt_Close.Name = "bt_Close";
            this.bt_Close.Size = new System.Drawing.Size(77, 23);
            this.bt_Close.TabIndex = 5;
            this.bt_Close.Text = "关闭";
            this.bt_Close.UseVisualStyleBackColor = true;
            this.bt_Close.Click += new System.EventHandler(this.bt_Close_Click);
            // 
            // bt_AddToGroup
            // 
            this.bt_AddToGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_AddToGroup.Location = new System.Drawing.Point(218, 82);
            this.bt_AddToGroup.Name = "bt_AddToGroup";
            this.bt_AddToGroup.Size = new System.Drawing.Size(77, 98);
            this.bt_AddToGroup.TabIndex = 4;
            this.bt_AddToGroup.Text = "入团(&G)";
            this.bt_AddToGroup.UseVisualStyleBackColor = true;
            this.bt_AddToGroup.Click += new System.EventHandler(this.bt_AddToGroup_Click);
            // 
            // tb_CardNo
            // 
            this.tb_CardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_CardNo.Location = new System.Drawing.Point(73, 24);
            this.tb_CardNo.Name = "tb_CardNo";
            this.tb_CardNo.Size = new System.Drawing.Size(141, 21);
            this.tb_CardNo.TabIndex = 1;
            // 
            // tb_PsgerName
            // 
            this.tb_PsgerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_PsgerName.Location = new System.Drawing.Point(0, 24);
            this.tb_PsgerName.Name = "tb_PsgerName";
            this.tb_PsgerName.Size = new System.Drawing.Size(69, 21);
            this.tb_PsgerName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "身份证号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "姓名";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox9);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.textBox8);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.textBox10);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.textBox7);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(302, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 212);
            this.panel2.TabIndex = 0;
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox9.Location = new System.Drawing.Point(216, 6);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(100, 21);
            this.textBox9.TabIndex = 2;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(110, 6);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 1;
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Location = new System.Drawing.Point(216, 46);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(100, 21);
            this.textBox8.TabIndex = 8;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Location = new System.Drawing.Point(110, 46);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(100, 21);
            this.textBox6.TabIndex = 7;
            // 
            // textBox10
            // 
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox10.Location = new System.Drawing.Point(4, 66);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(312, 143);
            this.textBox10.TabIndex = 9;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(4, 46);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 6;
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Location = new System.Drawing.Point(216, 26);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(100, 21);
            this.textBox7.TabIndex = 5;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Location = new System.Drawing.Point(110, 26);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(100, 21);
            this.textBox5.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(4, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(4, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 0;
            // 
            // GroupeAddPassenger
            // 
            this.AcceptButton = this.bt_AddToGroup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_Close;
            this.ClientSize = new System.Drawing.Size(623, 212);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GroupeAddPassenger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "散客拼团";
            this.Load += new System.EventHandler(this.AddPassenger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_DeletePsger;
        private System.Windows.Forms.Button bt_AddPsger;
        private System.Windows.Forms.ListBox lb_CardNo;
        private System.Windows.Forms.ListBox lb_PsgerName;
        private System.Windows.Forms.Button bt_Close;
        private System.Windows.Forms.Button bt_AddToGroup;
        private System.Windows.Forms.TextBox tb_CardNo;
        private System.Windows.Forms.TextBox tb_PsgerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox5;

    }
}