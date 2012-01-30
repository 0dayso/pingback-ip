using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace EagleCTI
{
    public partial class CtiLogin : Form
    {
        public CtiLogin()
        {
            InitializeComponent();
        }
        public string serverip = "";
        public int serverport = 0;
        public string teleport = "";
        private void btLogin_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\EagleCti.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter (fs);
            sw.WriteLine(textBox1.Text.Trim() + "," + textBox2.Text.Trim() + "," + textBox3.Text.Trim());
            sw.Close();
            fs.Close();

            
            
            try
            {
                serverip = textBox1.Text.Trim();
                IPAddress.Parse(textBox1.Text.Trim());
                serverport = int.Parse(textBox2.Text.Trim());
                teleport = textBox3.Text.Trim();
                int temp = int.Parse(textBox3.Text.Trim());
                if (temp < 0) throw new Exception("内线接口号错误！");

            }
            catch (Exception ex)
            {
                MessageBox.Show("CTI ERROR: " +ex.Message);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CtiLogin_Load(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\EagleCti.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string s = sr.ReadLine();//字符串格式：192.168.0.118,1234,7
                sr.Close();
                fs.Close();
                string[] a = s.Split(',');
                textBox1.Text = a[0];
                textBox2.Text = a[1];
                textBox3.Text = a[2];
            }
            catch
            {
            }
        }
    }
}