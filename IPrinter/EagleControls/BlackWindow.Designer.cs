namespace EagleControls
{
    partial class BlackWindow
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
            this.SuspendLayout();
            // 
            // BlackWindow
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Lime;
            this.HideSelection = false;
            this.ShortcutsEnabled = false;
            this.Size = new System.Drawing.Size(1, 1);
            this.WordWrap = false;
            this.FontChanged += new System.EventHandler(this.BlackWindow_FontChanged);
            this.TextChanged += new System.EventHandler(this.BlackWindow_TextChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
