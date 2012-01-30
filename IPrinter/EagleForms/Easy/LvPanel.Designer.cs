namespace EagleForms.Easy
{
    partial class LvPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lv = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lv2 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lv2);
            this.panel1.Controls.Add(this.lv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 273);
            this.panel1.TabIndex = 0;
            // 
            // lv
            // 
            this.lv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(295, 78);
            this.lv.SmallImageList = this.imageList1;
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lv2
            // 
            this.lv2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv2.FullRowSelect = true;
            this.lv2.GridLines = true;
            this.lv2.HideSelection = false;
            this.lv2.Location = new System.Drawing.Point(0, 147);
            this.lv2.Name = "lv2";
            this.lv2.Size = new System.Drawing.Size(295, 78);
            this.lv2.SmallImageList = this.imageList1;
            this.lv2.TabIndex = 0;
            this.lv2.UseCompatibleStateImageBehavior = false;
            this.lv2.View = System.Windows.Forms.View.Details;
            this.lv2.Visible = false;
            // 
            // LvPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 273);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LvPanel";
            this.Text = "LvPanel";
            this.Load += new System.EventHandler(this.LvPanel_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        /// <summary>
        /// 显示数量的LV
        /// </summary>
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ImageList imageList1;
        /// <summary>
        /// 显示舱位代号的LV
        /// </summary>
        private System.Windows.Forms.ListView lv2;
    }
}