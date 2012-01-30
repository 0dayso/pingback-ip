namespace EagleControls
{
    partial class LV_Lowest
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
            this.id = new System.Windows.Forms.ColumnHeader();
            this.flight = new System.Windows.Forms.ColumnHeader();
            this.time = new System.Windows.Forms.ColumnHeader();
            this.time2 = new System.Windows.Forms.ColumnHeader();
            this.bunk = new System.Windows.Forms.ColumnHeader();
            this.price = new System.Windows.Forms.ColumnHeader();
            this.profit = new System.Windows.Forms.ColumnHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rebate = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.Text = "序号";
            this.id.Width = 0;
            // 
            // flight
            // 
            this.flight.Text = "航班";
            // 
            // time
            // 
            this.time.DisplayIndex = 6;
            this.time.Text = "离港";
            this.time.Width = 48;
            // 
            // time2
            // 
            this.time2.DisplayIndex = 7;
            this.time2.Text = "到达";
            this.time2.Width = 0;
            // 
            // bunk
            // 
            this.bunk.Text = "舱位";
            this.bunk.Width = 36;
            // 
            // price
            // 
            this.price.Text = "价格";
            this.price.Width = 48;
            // 
            // profit
            // 
            this.profit.Text = "返点";
            // 
            // rebate
            // 
            this.rebate.DisplayIndex = 2;
            this.rebate.Text = "折扣";
            this.rebate.Width = 36;
            // 
            // LV_Lowest
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.flight,
            this.time,
            this.time2,
            this.bunk,
            this.price,
            this.profit,
            this.rebate});
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.HideSelection = false;
            this.HoverSelection = true;
            this.Name = "this";
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LV_Lowest_MouseDoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LV_Lowest_MouseMove);
            this.MouseHover += new System.EventHandler(this.LV_Lowest_MouseHover);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader flight;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader time2;
        private System.Windows.Forms.ColumnHeader bunk;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader profit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ColumnHeader rebate;
    }
}
