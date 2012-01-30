using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleFinance
{
    public partial class MultiSelect : Form
    {
        public MultiSelect()
        {
            InitializeComponent();
        }
        public string strResult = "";
        public MultiSelect(string list1, string list2)
        {
            InitializeComponent();
            string[] ls1 = list1.Split(',');
            for (int i = 0; i < ls1.Length; i++)
            {
                listBox1.Items.Add(ls1[i]);
            }
            string[] ls2 = list2.Split(',');
            for (int i = 0; i < ls2.Length; i++)
            {
                if (ls2[i] != "多个用逗号隔开")

                listBox2.Items.Add(ls2[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                bool bExist = false;
                for (int j = 0; j < listBox2.Items.Count; j++)
                {
                    if (listBox1.SelectedItems[i].ToString() == listBox2.Items[j].ToString())
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist) listBox2.Items.Add(listBox1.SelectedItems[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = listBox2.SelectedItems.Count-1; i >=0; i--)
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndices[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                strResult += listBox2.Items[i].ToString() + ",";
            }
            if (strResult.Length > 0) strResult = strResult.Substring(0, strResult.Length - 1);
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                bool bExist = false;
                for (int j = 0; j < listBox2.Items.Count; j++)
                {
                    if (listBox1.Items[i].ToString() == listBox2.Items[j].ToString())
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist) listBox2.Items.Add(listBox1.Items[i]);
            }

        }
    }
}