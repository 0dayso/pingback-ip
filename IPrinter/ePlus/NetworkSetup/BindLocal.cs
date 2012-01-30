using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ePlus.NetworkSetup
{
    public partial class BindLocal : Form
    {
        public BindLocal()
        {
            InitializeComponent();
            IPHostEntry oIPHost = Dns.Resolve(Environment.MachineName);
            for (int i = 0; i < oIPHost.AddressList.Length; i++)
            {
                comboBox1.Items.Add(oIPHost.AddressList[i].ToString());
            }
            checkBox1_CheckedChanged(null, null);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox1.Checked;
            numericUpDown1.Enabled = checkBox1.Checked;
        }

        private void BindLocal_Load(object sender, EventArgs e)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream("c:\\localiport.txt", System.IO.FileMode.Open);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                string localiport = sr.ReadLine();
                comboBox1.Text = (localiport.Split(':')[0]);
                numericUpDown1.Value = int.Parse(localiport.Split(':')[1]);
                sr.Close();
                fs.Close();
                checkBox1.Checked = true;
            }
            catch
            {
                checkBox1.Checked = false;
                try
                {
                    numericUpDown1.Value = 2000;
                    comboBox1.Text = comboBox1.Items[0].ToString();
                }
                catch
                {
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    FileStream fs = new FileStream("c:\\localiport.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(comboBox1.Text.Trim() + ":" + numericUpDown1.Value.ToString());
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    File.Delete("c:\\localiport.txt");
                }
            }
            catch
            {
            }
            this.Close();
        }
    }
}