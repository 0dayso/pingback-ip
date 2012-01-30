using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.PrintHyx
{
    public partial class bxLogin : Form
    {
        public bxLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("ÃÜÂë²»Ò»ÖÂ");
                return;
            }
            else
            {
                GlobalVar2.bxUserAccount = textBox1.Text;
                GlobalVar2.bxPassWord = textBox2.Text;
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bxLogin_Load(object sender, EventArgs e)
        {
            textBox1.Text = GlobalVar2.bxUserAccount;
            textBox2.Text = textBox3.Text = GlobalVar2.bxPassWord;
        }
    }
}