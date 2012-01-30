namespace ePlus.Data
{
    partial class dlgPolicySelect
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
            this.dg航班信息 = new System.Windows.Forms.DataGridView();
            this.dg乘客信息 = new System.Windows.Forms.DataGridView();
            this.dg政策信息 = new System.Windows.Forms.DataGridView();
            this.lbl乘客信息 = new System.Windows.Forms.Label();
            this.lbl政策信息 = new System.Windows.Forms.Label();
            this.lbl票价计算 = new System.Windows.Forms.Label();
            this.pn票价计算 = new System.Windows.Forms.Panel();
            this.btCreateOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg航班信息)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg乘客信息)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg政策信息)).BeginInit();
            this.SuspendLayout();
            // 
            // dg航班信息
            // 
            this.dg航班信息.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg航班信息.Dock = System.Windows.Forms.DockStyle.Top;
            this.dg航班信息.Location = new System.Drawing.Point(0, 0);
            this.dg航班信息.Name = "dg航班信息";
            this.dg航班信息.RowTemplate.Height = 23;
            this.dg航班信息.Size = new System.Drawing.Size(897, 122);
            this.dg航班信息.TabIndex = 0;
            // 
            // dg乘客信息
            // 
            this.dg乘客信息.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg乘客信息.Location = new System.Drawing.Point(0, 140);
            this.dg乘客信息.Name = "dg乘客信息";
            this.dg乘客信息.RowTemplate.Height = 23;
            this.dg乘客信息.Size = new System.Drawing.Size(897, 122);
            this.dg乘客信息.TabIndex = 0;
            // 
            // dg政策信息
            // 
            this.dg政策信息.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg政策信息.Location = new System.Drawing.Point(-1, 280);
            this.dg政策信息.Name = "dg政策信息";
            this.dg政策信息.RowTemplate.Height = 23;
            this.dg政策信息.Size = new System.Drawing.Size(898, 122);
            this.dg政策信息.TabIndex = 0;
            // 
            // lbl乘客信息
            // 
            this.lbl乘客信息.AutoSize = true;
            this.lbl乘客信息.Location = new System.Drawing.Point(-2, 125);
            this.lbl乘客信息.Name = "lbl乘客信息";
            this.lbl乘客信息.Size = new System.Drawing.Size(53, 12);
            this.lbl乘客信息.TabIndex = 1;
            this.lbl乘客信息.Text = "乘客信息";
            // 
            // lbl政策信息
            // 
            this.lbl政策信息.AutoSize = true;
            this.lbl政策信息.Location = new System.Drawing.Point(-3, 265);
            this.lbl政策信息.Name = "lbl政策信息";
            this.lbl政策信息.Size = new System.Drawing.Size(53, 12);
            this.lbl政策信息.TabIndex = 1;
            this.lbl政策信息.Text = "政策信息";
            // 
            // lbl票价计算
            // 
            this.lbl票价计算.AutoSize = true;
            this.lbl票价计算.Location = new System.Drawing.Point(-2, 405);
            this.lbl票价计算.Name = "lbl票价计算";
            this.lbl票价计算.Size = new System.Drawing.Size(53, 12);
            this.lbl票价计算.TabIndex = 1;
            this.lbl票价计算.Text = "票价计算";
            // 
            // pn票价计算
            // 
            this.pn票价计算.Location = new System.Drawing.Point(1, 420);
            this.pn票价计算.Name = "pn票价计算";
            this.pn票价计算.Size = new System.Drawing.Size(896, 90);
            this.pn票价计算.TabIndex = 2;
            // 
            // btCreateOrder
            // 
            this.btCreateOrder.Location = new System.Drawing.Point(411, 514);
            this.btCreateOrder.Name = "btCreateOrder";
            this.btCreateOrder.Size = new System.Drawing.Size(75, 23);
            this.btCreateOrder.TabIndex = 3;
            this.btCreateOrder.Text = "创建订单";
            this.btCreateOrder.UseVisualStyleBackColor = true;
            this.btCreateOrder.Click += new System.EventHandler(this.btCreateOrder_Click);
            // 
            // dlgPolicySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 538);
            this.Controls.Add(this.btCreateOrder);
            this.Controls.Add(this.pn票价计算);
            this.Controls.Add(this.lbl票价计算);
            this.Controls.Add(this.lbl政策信息);
            this.Controls.Add(this.lbl乘客信息);
            this.Controls.Add(this.dg政策信息);
            this.Controls.Add(this.dg乘客信息);
            this.Controls.Add(this.dg航班信息);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dlgPolicySelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dlgPolicySelect";
            this.SizeChanged += new System.EventHandler(this.dlgPolicySelect_SizeChanged);
            this.Load += new System.EventHandler(this.dlgPolicySelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg航班信息)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg乘客信息)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg政策信息)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg航班信息;
        private System.Windows.Forms.DataGridView dg乘客信息;
        private System.Windows.Forms.DataGridView dg政策信息;
        private System.Windows.Forms.Label lbl乘客信息;
        private System.Windows.Forms.Label lbl政策信息;
        private System.Windows.Forms.Label lbl票价计算;
        private System.Windows.Forms.Panel pn票价计算;
        private System.Windows.Forms.Button btCreateOrder;
    }
}