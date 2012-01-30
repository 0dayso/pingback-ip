using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.NetworkSetup
{
    public partial class Eticket2Pnr : Form
    {
        public Eticket2Pnr()
        {
            InitializeComponent();
        }
        int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string head = "detr:tn/";
            string[] middle = { "999", "784", "883", "781", "479", "774", "880", "324", "731", "876", "859", "987", "822", "811" };
            EagleAPI.EagleSendCmd(head + middle[count] + "-" + textBox1.Text.Trim().Substring(textBox1.Text.Trim().Length-10));
            
            count++;
            if (count == middle.Length) count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count = 0;
        }
    }
}