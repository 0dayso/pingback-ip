using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EagleFinance
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }

        private void lb_MouseClick(object sender, MouseEventArgs e)
        {

        }
        void clear(object sender, EventArgs e)
        {
            lb.Items.Clear();
        }
        void save(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文档 (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "保存错误日志";

            saveFileDialog.ShowDialog();
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            for (int i = 0; i < lb.Items.Count; i++)
            {
                sw.WriteLine(lb.Items[i].ToString());
            }
            sw.Close();
            myStream.Close();
        }
        private void lb_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add("清空", null, new EventHandler(clear));
                rightMenu.Items.Add("保存", null, new EventHandler(save));
                rightMenu.Show(this.lb, e.X, e.Y);

            }
        }
    }
}