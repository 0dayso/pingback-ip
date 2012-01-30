using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace EagleControls
{
    public partial class BlackWindowHotKeySetup : Form
    {
        public Hashtable m_ht1 = new Hashtable ();
        public Hashtable m_ht2 = new Hashtable ();
        public Hashtable m_ht3 = new Hashtable ();

        private string oldshortcmd = "";
        int FUNCTION_COUNT = 32;////////////////////枚举功能的长度
        public BlackWindowHotKeySetup(Hashtable ht1,Hashtable ht2,Hashtable ht3)
        {
            InitializeComponent();
            lv1.Items.Clear();
            foreach (DictionaryEntry de in ht1)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = de.Key.ToString();
                lvi.SubItems.Add(de.Value.ToString());
                lv1.Items.Add(lvi);
            }
            lv1.Sort();


            oldshortcmd = txtHotKey3.Text;
            
            lv2.Items.Clear();
            for(int i=0;i<FUNCTION_COUNT;++i)
            {
                EagleControls.FUNCTION_ID fi = (FUNCTION_ID)i;
                string s = fi.ToString();
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add("");
                lvi.SubItems.Add(s);
                lv2.Items.Add(lvi);
            }


            foreach (DictionaryEntry de in ht2)
            {
                for (int i = 0; i < lv2.Items.Count; ++i)
                {
                    string value = de.Value.ToString();
                    if (value == "") continue;
                    EagleControls.FUNCTION_ID fi = (FUNCTION_ID)byte.Parse(de.Value.ToString());
                    if (lv2.Items[i].SubItems[2].Text == fi.ToString())
                    {
                        lv2.Items[i].Text = de.Key.ToString();
                    }
                }
            }

            foreach (DictionaryEntry de in ht3)
            {
                for (int i = 0; i < lv2.Items.Count; ++i)
                {
                    string value = de.Value.ToString();
                    if (value == "") continue;
                    EagleControls.FUNCTION_ID fi = (FUNCTION_ID)byte.Parse(de.Value.ToString());
                    if (lv2.Items[i].SubItems[2].Text == fi.ToString())
                    {
                        lv2.Items[i].SubItems[1].Text = de.Key.ToString();
                    }
                }
            }
            lv2.Sort();
        }

        private void BlackWindowHotKeySetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < lv1.Items.Count; ++i)
            {
                string key = lv1.Items[i].Text;
                string value = lv1.Items[i].SubItems[1].Text;
                m_ht1.Add(key, value);
            }
            for (int i = 0; i < lv2.Items.Count; ++i)
            {
                string str = lv2.Items[i].SubItems[2].Text;
                string value="";
                for (int j = 0; j < FUNCTION_COUNT; ++j)
                {
                    FUNCTION_ID fi = (FUNCTION_ID)j;
                    if (str == fi.ToString())
                    {
                        value = j.ToString();
                        break;
                    }
                }
                if (lv2.Items[i].Text != "")
                {
                    string key = lv2.Items[i].Text;
                    m_ht2.Add(key, value);
                    
                }
                if (lv2.Items[i].SubItems[1].Text != "")
                {
                    string key = lv2.Items[i].SubItems[1].Text;
                    m_ht3.Add(key, value);
                }
            }
        }

        private void btnAddShortCommand_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lv1.Items.Count; ++i)
            {
                if (lv1.Items[i].Text == txtHotKey1.Text)
                {
                    lv1.Items.RemoveAt(i);
                    break;
                }
            }

            ListViewItem lvi = new ListViewItem();
            lvi.Text = txtHotKey1.Text;
            lvi.SubItems.Add(txtDestCommand.Text);
            lv1.Items.Add(lvi);
        }

        private void btnDelShortCommand_Click(object sender, EventArgs e)
        {
            try
            {
                lv1.Items.RemoveAt(lv1.SelectedIndices[0]);
            }
            catch
            {
            }
        }

        private void txtHotKey1_KeyDown(object sender, KeyEventArgs e)
        {
            string s = "";
            if (e.Control) s += "CTRL+";
            if (e.Alt) s += "ALT+";
            if (e.Shift) s += "SHIFT+";
            s += e.KeyCode.ToString();
            (sender as TextBox).Text = s;
        }

        private void txtHotKey3_Enter(object sender, EventArgs e)
        {
            if (txtHotKey3.Text == oldshortcmd)
            {
                txtHotKey3.Text = "";
            }
        }

        private void btnAddShortFunction_Click(object sender, EventArgs e)
        {
            int count = lv2.Items.Count;
            string key = txtHotKey2.Text;
            string cmd = txtHotKey3.Text;
            if (lv2.SelectedIndices.Count != 1)
            {
                MessageBox.Show("请从下表中选择一个要修改的快键功能");
                return;
            }
            if (key != "")
            {
                //是否有冲突，有则把原先的冲掉
                int i = 0;
                for (i = 0; i < count; ++i)
                {
                    if (lv2.Items[i].Text == key)
                    {
                        lv2.Items[i].Text = "";
                    }
                }
                lv2.SelectedItems[0].Text = key;
            }
            if (cmd != "" && cmd != oldshortcmd)
            {
                //是否有冲突，有则把原先的冲掉
                for (int i = 0; i < count; ++i)
                {
                    if (lv2.Items[i].SubItems[1].Text == cmd)
                    {
                        lv2.Items[i].SubItems[1].Text = "";
                    }

                }
                lv2.SelectedItems[0].SubItems[1].Text = cmd;
            }
        }

        private void btnDelShortFunction_Click(object sender, EventArgs e)
        {
            try
            {
                lv2.Items.RemoveAt(lv1.SelectedIndices[0]);
            }
            catch
            {
            }
        }

        private void lv2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = txtHotKey2;
                txtHotKey2.Text = lv2.SelectedItems[0].Text;
                txtHotKey3.Text = lv2.SelectedItems[0].SubItems[1].Text;
                txtHotKey2.Focus();
            }
            catch
            {
            }
        }

        private void lv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = txtHotKey1;
                txtHotKey1.Text = lv1.SelectedItems[0].Text;
                txtDestCommand.Text = lv1.SelectedItems[0].SubItems[1].Text;
            }
            catch
            {
            }
        }


    }
}