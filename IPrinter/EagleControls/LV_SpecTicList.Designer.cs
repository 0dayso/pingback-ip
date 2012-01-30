namespace EagleControls
{
    partial class LV_SpecTicList
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
            this.ch_id = new System.Windows.Forms.ColumnHeader();
            this.ch_cp = new System.Windows.Forms.ColumnHeader();
            this.ch_flight = new System.Windows.Forms.ColumnHeader();
            this.ch_bunk = new System.Windows.Forms.ColumnHeader();
            this.ch_price = new System.Windows.Forms.ColumnHeader();
            this.ch_rebate = new System.Windows.Forms.ColumnHeader();
            this.ch_date = new System.Windows.Forms.ColumnHeader();
            this.ch_remark = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // ch_id
            // 
            this.ch_id.Text = "序号";
            this.ch_id.Width = 0;
            // 
            // ch_cp
            // 
            this.ch_cp.Text = "城市对";
            this.ch_cp.Width = 0;
            // 
            // ch_flight
            // 
            this.ch_flight.Text = "航班";
            // 
            // ch_bunk
            // 
            this.ch_bunk.Text = "舱位";
            // 
            // ch_price
            // 
            this.ch_price.Text = "价格";
            // 
            // ch_rebate
            // 
            this.ch_rebate.Text = "折扣";
            this.ch_rebate.Width = 0;
            // 
            // ch_date
            // 
            this.ch_date.Text = "日期";
            this.ch_date.Width = 0;
            // 
            // ch_remark
            // 
            this.ch_remark.Text = "备注";
            // 
            // LV_SpecTicList
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_id,
            this.ch_cp,
            this.ch_flight,
            this.ch_bunk,
            this.ch_price,
            this.ch_rebate,
            this.ch_date,
            this.ch_remark});
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.View = System.Windows.Forms.View.Details;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ch_id;
        private System.Windows.Forms.ColumnHeader ch_cp;
        private System.Windows.Forms.ColumnHeader ch_flight;
        private System.Windows.Forms.ColumnHeader ch_bunk;
        private System.Windows.Forms.ColumnHeader ch_price;
        private System.Windows.Forms.ColumnHeader ch_rebate;
        private System.Windows.Forms.ColumnHeader ch_date;
        private System.Windows.Forms.ColumnHeader ch_remark;
    }
}
