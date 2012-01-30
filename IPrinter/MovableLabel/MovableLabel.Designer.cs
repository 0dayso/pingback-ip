namespace TomatoControls
{
    partial class MovableLabel
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
            components = new System.ComponentModel.Container();
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.timer.Tick += new System.EventHandler(timer_Tick);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;

            //this.OnPaintBackground(e);

            if (this.timer.Enabled)
                g.DrawString(this.Text, this.Font, new System.Drawing.SolidBrush(this.ForeColor), xPos, yPos);
            else
                base.OnPaint(e); 
        }
        
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            xPos = this.Size.Width;
            yPos = (this.Height - (int)this.CreateGraphics().MeasureString(this.Text, this.Font).Height) / 2;
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            this.Text = Text.Replace(System.Environment.NewLine, "     ");
            base.OnTextChanged(e);
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            this.timer.Stop();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            this.timer.Start();
            base.OnMouseLeave(e);
        }

        int xPos = 0, yPos = 0;

        void timer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                if (this.Text == "")
                {
                    xPos = this.Size.Width;
                    return;
                }
                this.Invalidate();
                System.Drawing.Graphics g = this.CreateGraphics();

                //从右到左的行为方式
                xPos -= 2;
                yPos = (this.Height - (int)this.CreateGraphics().MeasureString(this.Text, this.Font).Height) / 2;
                if (xPos <= -g.MeasureString(this.Text, this.Font).Width)
                {
                    xPos = this.Width;
                    OnSlideOver(System.EventArgs.Empty);
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
