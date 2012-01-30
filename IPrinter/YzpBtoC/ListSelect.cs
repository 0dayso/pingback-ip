using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YzpBtoC
{
    public partial class ListSelect : Form
    {
        public ListSelect()
        {
            InitializeComponent();
        }
        public ListSelect(List<string> ls)
        {
            InitializeComponent();
            for (int i = 0; i < ls.Count; i++)
            {
                listBox1.Items.Add(ls[i]);
            }
        }
        private void ListSelect_Load(object sender, EventArgs e)
        {
            this.ActiveControl = listBox1;
            listBox1.SelectedItem = 0;
        }
        public string retString = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 1) return;
            retString = listBox1.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                button1_Click(null, null);
            }
            catch
            {
            }
        }
    }
}