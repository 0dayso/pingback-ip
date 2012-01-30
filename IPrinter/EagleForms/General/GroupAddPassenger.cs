using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms
{
    public partial class GroupeAddPassenger : Form
    {
        private string m_wsAddress = "";
        private string m_userName = "";

        private string m_id;
        private string m_name;
        private string m_citypair;
        private string m_flightno;
        private string m_date;
        private string m_rebate;
        private string m_total;
        private string m_pnr;
        private string m_remain;
        private string m_remark;
        public GroupeAddPassenger(string [] groupinfo,string wsaddr,string username)
        {
            InitializeComponent();
            m_wsAddress = wsaddr;
            m_userName = username;
            int i=0;
            m_id        = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_name      = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_citypair  = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_flightno  = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_date      = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_rebate    = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_total     = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_pnr       = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_remain    = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
            m_remark    = panel2.Controls["textBox" + Convert.ToString(i + 1)].Text = groupinfo[i++];
        }
        public string groupid = "";
        public string total = "";
        public string booked = "";
        private void AddPassenger_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void add_to_group(object sender, EventArgs e)
        {

            if (lb_PsgerName.Items.Count == 0)
            {
                MessageBox.Show("抱歉：请正确输入", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < lb_PsgerName.Items.Count; i++)
            {
                EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_wsAddress);
                bool bFlag = kf.Group_Add(m_userName, lb_PsgerName.Items[i].ToString(), lb_CardNo.Items[i].ToString(), groupid);
                if (bFlag)
                {
                    //MessageBox.Show("入团成功！");
                }
                else
                {
                    MessageBox.Show("警告：" + lb_PsgerName.Items[i].ToString() + "入团失败！");
                }
            }
            MessageBox.Show("恭喜，入团完毕！", "CONGRATUATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void add_psger_to_listbox(object sender, EventArgs e)
        {
            if (lb_PsgerName.Items.Count + int.Parse(booked) >= int.Parse(total))
            {
                MessageBox.Show("抱歉：人数超出！不能再增加", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tb_CardNo.Text.Trim() == "" || tb_PsgerName.Text.Trim() == "")
            {
                MessageBox.Show("抱歉：请正确输入", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                lb_PsgerName.Items.Add(tb_PsgerName.Text.Trim());
                lb_CardNo.Items.Add(tb_CardNo.Text.Trim());
                tb_CardNo.Text = tb_PsgerName.Text = "";
            }
            catch
            {
                MessageBox.Show("抱歉：添加乘客出错！", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_psger_from_listbox(object sender, EventArgs e)
        {
            for (int i = 0; i < lb_PsgerName.SelectedItems.Count; i++)
            {
                int it = lb_PsgerName.SelectedIndices[i];
                lb_PsgerName.Items.RemoveAt(it);
                lb_CardNo.Items.RemoveAt(it);
            }
        }

        private void bt_AddToGroup_Click(object sender, EventArgs e)
        {
            add_to_group(sender, e);
        }

        private void bt_AddPsger_Click(object sender, EventArgs e)
        {
            add_psger_to_listbox(sender, e);
        }

        private void bt_DeletePsger_Click(object sender, EventArgs e)
        {
            delete_psger_from_listbox(sender, e);
        }

        private void bt_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}