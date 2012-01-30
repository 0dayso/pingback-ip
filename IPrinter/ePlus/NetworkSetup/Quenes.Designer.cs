namespace ePlus.NetworkSetup
{
    partial class Quenes
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("全部信箱");
            this.tvQuene = new System.Windows.Forms.TreeView();
            this.tbQuene = new System.Windows.Forms.RichTextBox();
            this.btQueneState = new System.Windows.Forms.Button();
            this.cbQueneType = new System.Windows.Forms.ComboBox();
            this.btStart = new System.Windows.Forms.Button();
            this.btFirst = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.btLast = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvQuene
            // 
            this.tvQuene.Location = new System.Drawing.Point(12, 12);
            this.tvQuene.Name = "tvQuene";
            treeNode1.Name = "RootNode";
            treeNode1.Text = "全部信箱";
            this.tvQuene.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvQuene.Size = new System.Drawing.Size(136, 265);
            this.tvQuene.TabIndex = 0;
            this.tvQuene.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvQuene_AfterSelect);
            this.tvQuene.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvQuene_NodeMouseClick);
            // 
            // tbQuene
            // 
            this.tbQuene.Location = new System.Drawing.Point(154, 12);
            this.tbQuene.Name = "tbQuene";
            this.tbQuene.Size = new System.Drawing.Size(389, 265);
            this.tbQuene.TabIndex = 1;
            this.tbQuene.Text = "";
            // 
            // btQueneState
            // 
            this.btQueneState.Location = new System.Drawing.Point(12, 283);
            this.btQueneState.Name = "btQueneState";
            this.btQueneState.Size = new System.Drawing.Size(87, 23);
            this.btQueneState.TabIndex = 2;
            this.btQueneState.Text = "当前信箱状态";
            this.btQueneState.UseVisualStyleBackColor = true;
            this.btQueneState.Click += new System.EventHandler(this.btQueneState_Click);
            // 
            // cbQueneType
            // 
            this.cbQueneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueneType.FormattingEnabled = true;
            this.cbQueneType.Items.AddRange(new object[] {
            "GQ",
            "RP",
            "KK",
            "RE",
            "SR",
            "TC",
            "TL",
            "SC",
            "IB"});
            this.cbQueneType.Location = new System.Drawing.Point(106, 285);
            this.cbQueneType.Name = "cbQueneType";
            this.cbQueneType.Size = new System.Drawing.Size(121, 20);
            this.cbQueneType.TabIndex = 3;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(233, 283);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 4;
            this.btStart.Text = "清Q";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btFirst
            // 
            this.btFirst.Location = new System.Drawing.Point(328, 283);
            this.btFirst.Name = "btFirst";
            this.btFirst.Size = new System.Drawing.Size(40, 23);
            this.btFirst.TabIndex = 5;
            this.btFirst.Text = "|<";
            this.btFirst.UseVisualStyleBackColor = true;
            this.btFirst.Click += new System.EventHandler(this.btFirst_Click);
            // 
            // btBack
            // 
            this.btBack.Location = new System.Drawing.Point(374, 282);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(40, 23);
            this.btBack.TabIndex = 5;
            this.btBack.Text = "<";
            this.btBack.UseVisualStyleBackColor = true;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(420, 282);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(40, 23);
            this.btNext.TabIndex = 5;
            this.btNext.Text = ">";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btLast
            // 
            this.btLast.Location = new System.Drawing.Point(466, 282);
            this.btLast.Name = "btLast";
            this.btLast.Size = new System.Drawing.Size(40, 23);
            this.btLast.TabIndex = 5;
            this.btLast.Text = ">|";
            this.btLast.UseVisualStyleBackColor = true;
            this.btLast.Click += new System.EventHandler(this.btLast_Click);
            // 
            // Quenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 315);
            this.Controls.Add(this.btLast);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.btFirst);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.cbQueneType);
            this.Controls.Add(this.btQueneState);
            this.Controls.Add(this.tbQuene);
            this.Controls.Add(this.tvQuene);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Quenes";
            this.ShowIcon = false;
            this.Text = "自动清Q";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Quenes_FormClosed);
            this.Load += new System.EventHandler(this.Quenes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvQuene;
        private System.Windows.Forms.RichTextBox tbQuene;
        private System.Windows.Forms.Button btQueneState;
        private System.Windows.Forms.ComboBox cbQueneType;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btFirst;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btLast;
    }
}