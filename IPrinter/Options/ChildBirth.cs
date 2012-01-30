using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options
{
    public partial class ChildBirth : Form
    {
        public ChildBirth()
        {
            InitializeComponent();
        }
        public DateTime dtBirth = new DateTime();
        private void ChildBirth_Load(object sender, EventArgs e)
        {
            dtpBirth.Value = DateTime.Now;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            dtBirth = dtpBirth.Value;
            this.Close();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}