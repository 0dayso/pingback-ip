using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.Model
{
    public partial class TprCmd : Form
    {
        public TprCmd()
        {

            InitializeComponent();
            try
            {
                textBox1.Text = "tpr:c/" + CreateETicket.GetPrinterNumber(GlobalVar.officeNumberCurrent) + "/-";
            }
            catch
            {
                textBox1.Text = "tpr:c/" + "0" + "/-";
            }
        }
        public TprCmd(string tslcmd)
        {

            InitializeComponent();
            try
            {
                textBox1.Text = tslcmd;
            }
            catch
            {
                textBox1.Text = "tsl";
            }
        }
        public string cmd = "";
        private void button1_Click(object sender, EventArgs e)
        {
            cmd = textBox1.Text;
        }
    }
}