using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options.ibe
{
    public partial class Working : Form
    {
        public Working()
        {
            InitializeComponent();
        }
        public DateTime timeStart;
        Timer timer = new Timer();
        public bool finish = false;
        private void Working_Load(object sender, EventArgs e)
        {
            label1.Text = "IBE查询已耗时： 00:00:00";
            timeStart = DateTime.Now;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            finish = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (finish)
            {
                timer.Stop();
                this.Close();
            }
            DateTime dt = DateTime.Now;
            int hh = dt.Hour - timeStart.Hour;
            int mm = dt.Minute - timeStart.Minute;
            int ss = dt.Second - timeStart.Second;
            if (ss < 0)
            {
                ss += 60;
                mm--;
            }
            if (mm < 0)
            {
                mm += 60;
                hh--;
            }
            label1.Text = "IBE查询已耗时： " + hh.ToString("d2") + ":" + mm.ToString("d2") + ":" + ss.ToString("d2");
            if (mm >= 2)
            {
                timer.Stop();
                this.Close();
            }
        }
    }
}