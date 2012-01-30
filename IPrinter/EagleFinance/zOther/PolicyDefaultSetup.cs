using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleFinance.zOther
{
    public partial class PolicyDefaultSetup : Form
    {
        public PolicyDefaultSetup()
        {
            InitializeComponent();
        }
        public bool bReget = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) policyht.i网络类型 = 0;
            if (radioButton2.Checked) policyht.i网络类型 = 1;
            bReget = !checkBox1.Checked;
            try
            {
                int a = int.Parse(textBox1.Text.Trim());
                int b = int.Parse(textBox2.Text.Trim());
                GlobalVar.gbMaxPolicy = a.ToString();
                GlobalVar.gbAgentPolicy = b.ToString();
                this.Close();
            }
            catch
            {
                MessageBox.Show("请输入整数");
                return;
            }
        }
    }
}