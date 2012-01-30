namespace EagleControls
{
    partial class LV_GroupList
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ch_Id = new System.Windows.Forms.ColumnHeader();
            this.ch_Title = new System.Windows.Forms.ColumnHeader();
            this.ch_Citypair = new System.Windows.Forms.ColumnHeader();
            this.ch_flightno = new System.Windows.Forms.ColumnHeader();
            this.ch_Date = new System.Windows.Forms.ColumnHeader();
            this.ch_Rebate = new System.Windows.Forms.ColumnHeader();
            this.ch_Total = new System.Windows.Forms.ColumnHeader();
            this.ch_Pnr = new System.Windows.Forms.ColumnHeader();
            this.ch_Remain = new System.Windows.Forms.ColumnHeader();
            this.ch_Remark = new System.Windows.Forms.ColumnHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ch_Fuck = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // ch_Id
            // 
            this.ch_Id.Text = "ID";
            this.ch_Id.Width = 0;
            // 
            // ch_Title
            // 
            this.ch_Title.Text = "标题";
            this.ch_Title.Width = 0;
            // 
            // ch_Citypair
            // 
            this.ch_Citypair.Text = "城市对";
            this.ch_Citypair.Width = 0;
            // 
            // ch_flightno
            // 
            this.ch_flightno.Text = "航班";
            this.ch_flightno.Width = 0;
            // 
            // ch_Date
            // 
            this.ch_Date.Text = "日期";
            this.ch_Date.Width = 0;
            // 
            // ch_Rebate
            // 
            this.ch_Rebate.Text = "折扣";
            this.ch_Rebate.Width = 0;
            // 
            // ch_Total
            // 
            this.ch_Total.Text = "总人数";
            this.ch_Total.Width = 0;
            // 
            // ch_Pnr
            // 
            this.ch_Pnr.Text = "PNR";
            this.ch_Pnr.Width = 0;
            // 
            // ch_Remain
            // 
            this.ch_Remain.Text = "剩余数";
            this.ch_Remain.Width = 0;
            // 
            // ch_Remark
            // 
            this.ch_Remark.Text = "备注";
            this.ch_Remark.Width = 0;
            // 
            // ch_Fuck
            // 
            this.ch_Fuck.Text = "散 客 拼 团";
            this.ch_Fuck.Width = 280;
            this.ch_Fuck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LV_GroupList
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_Id,
            this.ch_Title,
            this.ch_Citypair,
            this.ch_flightno,
            this.ch_Date,
            this.ch_Rebate,
            this.ch_Total,
            this.ch_Pnr,
            this.ch_Remain,
            this.ch_Remark,
            this.ch_Fuck});
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.HideSelection = false;
            this.HoverSelection = true;
            this.View = System.Windows.Forms.View.Details;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LV_GroupList_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ch_Id;
        private System.Windows.Forms.ColumnHeader ch_Title;
        private System.Windows.Forms.ColumnHeader ch_Citypair;
        private System.Windows.Forms.ColumnHeader ch_flightno;
        private System.Windows.Forms.ColumnHeader ch_Date;
        private System.Windows.Forms.ColumnHeader ch_Rebate;
        private System.Windows.Forms.ColumnHeader ch_Total;
        private System.Windows.Forms.ColumnHeader ch_Pnr;
        private System.Windows.Forms.ColumnHeader ch_Remain;
        private System.Windows.Forms.ColumnHeader ch_Remark;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ColumnHeader ch_Fuck;
    }
}
