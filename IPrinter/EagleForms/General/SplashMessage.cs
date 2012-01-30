using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.General
{
    public partial class SplashMessage : Form
    {
        public SplashMessage(string str,int time)
        {
            InitializeComponent();
            textBox1.Text = str;
            timer1.Interval = time;
        }

        private void SplashMessage_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void SplashMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}