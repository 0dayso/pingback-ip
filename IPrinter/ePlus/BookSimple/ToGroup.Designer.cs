namespace ePlus.BookSimple
{
    partial class ToGroup
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
            this.lvToGroup = new System.Windows.Forms.ListView();
            this.ch_ID = new System.Windows.Forms.ColumnHeader();
            this.ch_Name = new System.Windows.Forms.ColumnHeader();
            this.ch_CityPair = new System.Windows.Forms.ColumnHeader();
            this.ch_FlightNo = new System.Windows.Forms.ColumnHeader();
            this.ch_Date = new System.Windows.Forms.ColumnHeader();
            this.ch_Rebate = new System.Windows.Forms.ColumnHeader();
            this.ch_Total = new System.Windows.Forms.ColumnHeader();
            this.ch_Pnr = new System.Windows.Forms.ColumnHeader();
            this.ch_Booked = new System.Windows.Forms.ColumnHeader();
            this.ch_Remark = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btUdate = new System.Windows.Forms.Button();
            this.btSelect = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.ttTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btExport = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvToGroup
            // 
            this.lvToGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvToGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_ID,
            this.ch_Name,
            this.ch_CityPair,
            this.ch_FlightNo,
            this.ch_Date,
            this.ch_Rebate,
            this.ch_Total,
            this.ch_Pnr,
            this.ch_Booked,
            this.ch_Remark});
            this.lvToGroup.FullRowSelect = true;
            this.lvToGroup.GridLines = true;
            this.lvToGroup.Location = new System.Drawing.Point(1, 34);
            this.lvToGroup.MultiSelect = false;
            this.lvToGroup.Name = "lvToGroup";
            this.lvToGroup.Size = new System.Drawing.Size(1002, 297);
            this.lvToGroup.SmallImageList = this.imageList1;
            this.lvToGroup.TabIndex = 0;
            this.lvToGroup.UseCompatibleStateImageBehavior = false;
            this.lvToGroup.View = System.Windows.Forms.View.Details;
            this.lvToGroup.DoubleClick += new System.EventHandler(this.lvToGroup_DoubleClick);
            this.lvToGroup.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvToGroup_MouseMove);
            this.lvToGroup.MouseHover += new System.EventHandler(this.lvToGroup_MouseHover);
            // 
            // ch_ID
            // 
            this.ch_ID.Text = "ID";
            this.ch_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_ID.Width = 0;
            // 
            // ch_Name
            // 
            this.ch_Name.Text = "产 品 名 称";
            this.ch_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Name.Width = 193;
            // 
            // ch_CityPair
            // 
            this.ch_CityPair.Text = "城市对";
            this.ch_CityPair.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_CityPair.Width = 64;
            // 
            // ch_FlightNo
            // 
            this.ch_FlightNo.Text = "航班号/起飞时间";
            this.ch_FlightNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_FlightNo.Width = 150;
            // 
            // ch_Date
            // 
            this.ch_Date.Text = "日期";
            this.ch_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Date.Width = 73;
            // 
            // ch_Rebate
            // 
            this.ch_Rebate.Text = "折扣/联系方式";
            this.ch_Rebate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Rebate.Width = 364;
            // 
            // ch_Total
            // 
            this.ch_Total.Text = "总人数";
            this.ch_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Total.Width = 54;
            // 
            // ch_Pnr
            // 
            this.ch_Pnr.Text = "PNR";
            this.ch_Pnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Pnr.Width = 0;
            // 
            // ch_Booked
            // 
            this.ch_Booked.Text = "已订数";
            this.ch_Booked.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Booked.Width = 56;
            // 
            // ch_Remark
            // 
            this.ch_Remark.Text = "备注";
            this.ch_Remark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_Remark.Width = 36;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(1, 18);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btUdate
            // 
            this.btUdate.Location = new System.Drawing.Point(163, 337);
            this.btUdate.Name = "btUdate";
            this.btUdate.Size = new System.Drawing.Size(75, 23);
            this.btUdate.TabIndex = 1;
            this.btUdate.Text = "更新";
            this.btUdate.UseVisualStyleBackColor = true;
            this.btUdate.Click += new System.EventHandler(this.btUdate_Click);
            // 
            // btSelect
            // 
            this.btSelect.Location = new System.Drawing.Point(364, 337);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(75, 23);
            this.btSelect.TabIndex = 2;
            this.btSelect.Text = "选择";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btExit
            // 
            this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btExit.Location = new System.Drawing.Point(565, 337);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(75, 23);
            this.btExit.TabIndex = 3;
            this.btExit.Text = "关闭";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // ttTooltip
            // 
            this.ttTooltip.AutomaticDelay = 600000;
            this.ttTooltip.UseAnimation = false;
            this.ttTooltip.UseFading = false;
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(766, 337);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(75, 23);
            this.btExport.TabIndex = 4;
            this.btExport.Text = "导出备注";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btUdate_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(364, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "选择";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(565, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btExit_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(766, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "导出备注";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btExport_Click);
            // 
            // ToGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btExit;
            this.ClientSize = new System.Drawing.Size(1004, 364);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.btUdate);
            this.Controls.Add(this.lvToGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToGroup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "特价产品";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToGroup_MouseMove);
            this.Load += new System.EventHandler(this.ToGroup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvToGroup;
        private System.Windows.Forms.ColumnHeader ch_ID;
        private System.Windows.Forms.ColumnHeader ch_CityPair;
        private System.Windows.Forms.ColumnHeader ch_FlightNo;
        private System.Windows.Forms.ColumnHeader ch_Date;
        private System.Windows.Forms.ColumnHeader ch_Rebate;
        private System.Windows.Forms.ColumnHeader ch_Total;
        private System.Windows.Forms.ColumnHeader ch_Pnr;
        private System.Windows.Forms.ColumnHeader ch_Booked;
        private System.Windows.Forms.Button btUdate;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader ch_Name;
        private System.Windows.Forms.ToolTip ttTooltip;
        private System.Windows.Forms.ColumnHeader ch_Remark;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}