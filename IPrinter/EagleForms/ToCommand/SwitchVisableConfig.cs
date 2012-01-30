using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.ToCommand
{
    public partial class SwitchVisableConfig : Form
    {
        EagleString.LoginResult m_lr;
        public SwitchVisableConfig(EagleString.LoginResult lr)
        {
            InitializeComponent();
            m_lr = lr;
            lvAll.Items.Clear();
            label3.Text = "正在使用的配置编号：" + string.Join(",", lr.IpidUsing.ToArray());
            for (int i = 0; i < m_lr.m_ls_office.Count; ++i)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = m_lr.m_ls_office[i].IP_ID.ToString();
                lvi.SubItems.Add(m_lr.m_ls_office[i].OFFICE_NO);
                lvi.SubItems.Add(m_lr.m_ls_office[i].OFFICE_ALLY);
                lvi.SubItems.Add(m_lr.m_ls_office[i].SERVER_NAME);
                lvAll.Items.Add(lvi);
            }
        }

        private void lvAll_DoubleClick(object sender, EventArgs e)
        {
            lvAllClick();
        }
        private bool CheckSameServer(string ipid,ListBox lb,ListView lv)
        {
            if (lb.Items.Count == 0) return true;
            string ipid2 = lb.Items[0].ToString();
            string server = "";
            string server2 = "";
            for (int i = 0; i < lv.Items.Count; ++i)
            {
                if (ipid == lv.Items[i].Text) server = lv.Items[i].SubItems[3].Text;
                if (ipid2 == lv.Items[i].Text) server2 = lv.Items[i].SubItems[3].Text;
                
            }
            if (server == server2) return true;
            return false;
        }
        private void lvAll_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lvAllClick();
        }
        void lvAllClick()
        {
            if (lvAll.SelectedItems.Count == 0) return;
            string ipid = lvAll.SelectedItems[0].Text;

            if (CheckSameServer(ipid, lb1, lvAll))
            {


                int pos = 0;
                for (pos = 0; pos < lb1.Items.Count; ++pos)
                {
                    if (ipid == lb1.Items[pos].ToString()) break;

                }
                if (pos == lb1.Items.Count)
                {
                    lb1.Items.Add(ipid);
                }
            }
            else
            {
                MessageBox.Show("目标配置不在同一个服务器上！");
            }
        }
        void lb1Click()
        {
            try
            {
                lb1.Items.RemoveAt(lb1.SelectedIndex);
            }
            catch
            {
            }
        }

        private void lb1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lb1Click();
        }
        public List<string> LSIPID = new List<string>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            LSIPID.Clear();
            for (int i = 0; i < lb1.Items.Count; ++i)
            {
                LSIPID.Add(lb1.Items[i].ToString());
            }
            if (LSIPID.Count == 0)
            {
                MessageBox.Show("您必须选择要切换的目标配置");
            }
            else
            {
                this.DialogResult = DialogResult.OK;

            }
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            lvAllClick();
        }

        private void btnAddSameOffice_Click(object sender, EventArgs e)
        {
            if (lvAll.SelectedItems.Count == 0) return;
            lvAllClick();
            string office = lvAll.SelectedItems[0].SubItems[1].Text.ToUpper();
            string server = lvAll.SelectedItems[0].SubItems[3].Text;
            for (int i = 0; i < lvAll.Items.Count; i++)
            {
                if (lvAll.Items[i].SubItems[1].Text.ToUpper() == office && lvAll.Items[i].SubItems[3].Text == server)
                {
                    if(!lb1.Items.Contains(lvAll.Items[i].Text))
                    {
                        lb1.Items.Add(lvAll.Items[i].Text);
                    }
                }
            }
        }

        private void btnAddSameServer_Click(object sender, EventArgs e)
        {
            if (lvAll.SelectedItems.Count == 0) return;
            lvAllClick();
            string server = lvAll.SelectedItems[0].SubItems[3].Text;
            for (int i = 0; i < lvAll.Items.Count; i++)
            {
                if (lvAll.Items[i].SubItems[3].Text == server)
                {
                    if (!lb1.Items.Contains(lvAll.Items[i].Text))
                    {
                        lb1.Items.Add(lvAll.Items[i].Text);
                    }
                }
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvAll.Items.Count; i++)
            {
                if (!lb1.Items.Contains(lvAll.Items[i].Text))
                {
                    lb1.Items.Add(lvAll.Items[i].Text);
                }
            }
        }

        private void btnDelSelected_Click(object sender, EventArgs e)
        {
            lb1Click();
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            lb1.Items.Clear();
        }
    }
}