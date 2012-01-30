using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class MsnPromote : Form
    {
        public MsnPromote()
        {
            InitializeComponent();

        }
        private int heightMax, widthMax;
        public int HeightMax
        {
            set
            {
                heightMax = value;
            }
            get
            {
                return heightMax;
            }
        }

        public int WidthMax
        {
            set
            {
                widthMax = value;
            }
            get
            {
                return widthMax;
            }
        }
        //添加一个ScrollShow的公共方法：

        public void ScrollShow()
        {
            this.Width = widthMax;
            this.Height = 0;
            this.Show();
            this.timer1.Enabled = true;
        }
        //添加一个StayTime属性设置窗体停留时间（默认为5秒）：
        public int StayTime = 60000;
        //添加ScrollUp和ScrollDown方法来编写窗体如何滚出和滚入：
        private void ScrollUp()
        {
            if (Height < heightMax)
            {
                this.Height += 3;
                this.Location = new Point(this.Location.X, this.Location.Y - 3);
            }
            else
            {
                this.timer1.Enabled = false;
                this.timer2.Enabled = true;
            }
        }

        private void ScrollDown()
        {
            if (Height > 3)
            {
                this.Height -= 3;
                this.Location = new Point(this.Location.X, this.Location.Y + 3);
            }
            else
            {
                this.timer3.Enabled = false;
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ScrollUp();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer3.Enabled = true;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            ScrollDown();

        }

        private void MsnPromote_Load(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            Screen screen = screens[0];//获取屏幕变量
            this.Location = new Point(screen.WorkingArea.Width - widthMax - 20, screen.WorkingArea.Height - 34);//WorkingArea为Windows桌面的工作区
            this.timer2.Interval = StayTime;
            this.label1.Text = "有新订单";
            this.button1.Text = "关闭";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            //PrintReceipt pr = new PrintReceipt();
            //pr.Window_ManageET();
            //pr.Text = "电子客票后台核查管理";
            //pr.ShowDialog();
        }

    }
}