using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options
{//b943v
    public partial class PrintBaoXianLianXu : Form
    {
        public PrintBaoXianLianXu()
        {
            InitializeComponent();
        }
        public int insLength = 0;
        public PrintBaoXianLianXu(string[] names,string [] cardids,string [] policyno)
        {
            InitializeComponent();
            for (int i = 0; i < names.Length; i++)
            {
                System.Windows.Forms.ListViewItem li = new ListViewItem();
                li.Text = names[i];
                li.SubItems.Add(cardids[i]);
                li.SubItems.Add(policyno[i]);
                lvPrint.Items.Add(li);
            }
        }

        private void lvPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvPrint.SelectedItems.Count <= 0) return;
                System.Windows.Forms.ListViewItem li = new ListViewItem();
                li = lvPrint.SelectedItems[0];
                tbName.Text = li.Text.Trim();
                tbCardId.Text = li.SubItems[1].Text;
                tbPolicyNo.Text = li.SubItems[2].Text;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() == "" || tbPolicyNo.Text.Trim().Length != insLength || tbCardId.Text.Trim() == "")
            {
                MessageBox.Show("请正确输入！"); return;
            }
            for (int i = 0; i < lvPrint.Items.Count; i++)
            {
                if (tbPolicyNo.Text.Trim() == lvPrint.Items[i].SubItems[2].Text.Trim())
                {
                    MessageBox.Show("保单号已经存在！"); return;
                }
            }
            long no = 0L;
            try
            {
                no = (long.Parse(tbPolicyNo.Text.Trim()) + 1L);
                
            }
            catch
            {
                MessageBox.Show("保单号错误！"); return;
            }
            System.Windows.Forms.ListViewItem li = new ListViewItem();
            li.Text = tbName.Text.Trim();
            li.SubItems.Add(tbCardId.Text.Trim());
            li.SubItems.Add(tbPolicyNo.Text.Trim());
            lvPrint.Items.Add(li);

            tbPolicyNo.Text = no.ToString("D"+insLength.ToString());          
            
        }

        private void btModify_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() == "" || tbPolicyNo.Text.Trim().Length != 7 || tbCardId.Text.Trim() == "")
            {
                MessageBox.Show("请正确输入！"); return;
            }
            for (int i = 0; i < lvPrint.Items.Count; i++)
            {
                if (lvPrint.SelectedItems.Count != 1) return;
                if (tbPolicyNo.Text.Trim() == lvPrint.Items[i].SubItems[2].Text.Trim())
                {
                    if (i != lvPrint.SelectedItems[0].Index)
                    { MessageBox.Show("保单号已经存在！"); return; }
                }
            }
            long no = 0L;
            try
            {
                no = (long.Parse(tbPolicyNo.Text.Trim()) + 1L);

            }
            catch
            {
                MessageBox.Show("保单号错误！"); return;
            }
            System.Windows.Forms.ListViewItem li = new ListViewItem();
            li.Text = tbName.Text.Trim();
            li.SubItems.Add(tbCardId.Text.Trim());
            li.SubItems.Add(tbPolicyNo.Text.Trim());
            lvPrint.Items[lvPrint.SelectedItems[0].Index] = li;

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (lvPrint.SelectedItems.Count != 1) return;
            lvPrint.Items.RemoveAt(lvPrint.SelectedItems[0].Index);
        }
        public List<string> ls = new List<string>();
        private void btPrint_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvPrint.Items.Count; i++)
            {
                string temp = "";
                for (int j = 0; j < lvPrint.Items[i].SubItems.Count; j++)
                {
                    temp += lvPrint.Items[i].SubItems[j].Text.Trim() + "~";
                }
                temp = temp.Substring(0, temp.Length - 1);
                ls.Add(temp);
            }
            this.Close();

        }

        private void PrintBaoXianLianXu_Load(object sender, EventArgs e)
        {
            if (this.insLength == 0) insLength = 7;
        }
    }
}