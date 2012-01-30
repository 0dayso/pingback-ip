using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleString
{
    public partial class SfcForm : Form
    {
        public SfcForm(string patxt,List<string> sfc)
        {
            InitializeComponent();
            txt.Text = patxt;
            lb.Items.AddRange(sfc.ToArray());
        }
        string m_sfc = "";
        public string SFCITEM { get { return m_sfc; } }
        private void txt_TextChanged(object sender, EventArgs e)
        {

        }

        void select()
        {
            if (lb.SelectedItems.Count > 0)
            {
                m_sfc = lb.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            select();
        }

        private void lb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            select();
        }
    }
}