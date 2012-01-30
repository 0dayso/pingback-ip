using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Options
{
    public partial class NoticeDlg : Form
    {
        public NoticeDlg()
        {
            InitializeComponent();
        }
        public string content = "";
        private void NoticeDlg_Load(object sender, EventArgs e)
        {
            this.richEditor.Text = content;
            if (content == "") this.richEditor.Text = "无公告";
        }

        private void NoticeDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void NoticeDlg_MouseMove(object sender, MouseEventArgs e)
        {
            this.TopMost = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void richEditor_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", e.LinkText);
        }
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTextExtentPoint32(IntPtr hdc, string lpString, int cbString, ref   Size lpSize); 
        private void richEditor_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}