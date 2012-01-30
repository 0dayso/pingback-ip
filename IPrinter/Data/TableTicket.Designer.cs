namespace ePlus.Data
{
    partial class TableTicket
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
            this.lv = new System.Windows.Forms.ListView();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblCityPair = new System.Windows.Forms.Label();
            this.btnSubmitOrderWithPnr = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblTimeBeg = new System.Windows.Forms.Label();
            this.lblTimeEnd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Dock = System.Windows.Forms.DockStyle.Top;
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(292, 118);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 121);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(29, 12);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "日期";
            // 
            // lblCityPair
            // 
            this.lblCityPair.AutoSize = true;
            this.lblCityPair.Location = new System.Drawing.Point(47, 121);
            this.lblCityPair.Name = "lblCityPair";
            this.lblCityPair.Size = new System.Drawing.Size(41, 12);
            this.lblCityPair.TabIndex = 2;
            this.lblCityPair.Text = "城市对";
            // 
            // btnSubmitOrderWithPnr
            // 
            this.btnSubmitOrderWithPnr.Location = new System.Drawing.Point(95, 238);
            this.btnSubmitOrderWithPnr.Name = "btnSubmitOrderWithPnr";
            this.btnSubmitOrderWithPnr.Size = new System.Drawing.Size(122, 23);
            this.btnSubmitOrderWithPnr.TabIndex = 3;
            this.btnSubmitOrderWithPnr.Text = "提交订单(当前PNR)";
            this.btnSubmitOrderWithPnr.UseVisualStyleBackColor = true;
            this.btnSubmitOrderWithPnr.Click += new System.EventHandler(this.btnSubmitOrderWithPnr_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 136);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "只显示最低价格";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblTimeBeg
            // 
            this.lblTimeBeg.AutoSize = true;
            this.lblTimeBeg.Location = new System.Drawing.Point(94, 121);
            this.lblTimeBeg.Name = "lblTimeBeg";
            this.lblTimeBeg.Size = new System.Drawing.Size(53, 12);
            this.lblTimeBeg.TabIndex = 1;
            this.lblTimeBeg.Text = "起飞时间";
            // 
            // lblTimeEnd
            // 
            this.lblTimeEnd.AutoSize = true;
            this.lblTimeEnd.Location = new System.Drawing.Point(153, 121);
            this.lblTimeEnd.Name = "lblTimeEnd";
            this.lblTimeEnd.Size = new System.Drawing.Size(53, 12);
            this.lblTimeEnd.TabIndex = 2;
            this.lblTimeEnd.Text = "到达时间";
            // 
            // TableTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnSubmitOrderWithPnr);
            this.Controls.Add(this.lblTimeEnd);
            this.Controls.Add(this.lblTimeBeg);
            this.Controls.Add(this.lblCityPair);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TableTicket";
            this.Text = "TableTicket";
            this.SizeChanged += new System.EventHandler(this.TableTicket_SizeChanged);
            this.Load += new System.EventHandler(this.TableTicket_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblCityPair;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblTimeBeg;
        private System.Windows.Forms.Label lblTimeEnd;
        public System.Windows.Forms.Button btnSubmitOrderWithPnr;
    }
}