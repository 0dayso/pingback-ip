using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleControls
{
    public partial class ScrollNotice : Panel
    {

        private DateTime m_lastTimeOfSetText = new DateTime();
        private int m_IntervalOfSetText = 15;
        private bool m_updateText = true;
        /// <summary>
        /// 是否需要更新滚动文本
        /// </summary>
        public bool UPDATETEXT { get { return m_updateText; } }
        /// <summary>
        /// 以分钟为单位，默认为15分钟
        /// </summary>
        public int UPDATEINTERVAL { get { return m_IntervalOfSetText; } set { m_IntervalOfSetText = value; } }
        System.Drawing.Graphics g;
        int m_pWidth = 480;
        int m_pX;
        /// <summary>
        /// 构造传入父容器的宽度
        /// </summary>
        /// <param name="width"></param>
        public ScrollNotice()
        {
            InitializeComponent();
            space = space.PadLeft(180, ' ');
            this.label1.Text = "滚动条公告";
            g = this.CreateGraphics();
            timer1.Interval = 40;
            m_pX = m_pWidth;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.label1.DoubleClick += new EventHandler(label1_DoubleClick);
        }

        void label1_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
            m_pWidth = this.Width;
            int posy = this.Height / 2 - this.label1.Height / 2;
            m_pX -= 2;
            if (m_pX +label1.Width<= 0) m_pX = m_pWidth;
            this.label1.Location = new Point(m_pX, posy);
            m_updateText = (m_lastTimeOfSetText.AddMinutes(m_IntervalOfSetText) <= DateTime.Now);
        }
        string space = " ";
        public void SetText(string text)
        {
            this.label1.Text = text;
            m_lastTimeOfSetText = DateTime.Now;
            m_updateText = false;
        }
        public void start()
        {
            timer1.Start();
        }
        public void stop()
        {
            timer1.Stop();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            stop();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            start();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            stop();
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            start();
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                menu.MenuItems.Add("拷贝", new EventHandler(copy));
                menu.Show(this.label1, new Point(e.X, e.Y));
            }
        }
        private void copy(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.label1.Text, true, 5, 10);
        }

    }
}
