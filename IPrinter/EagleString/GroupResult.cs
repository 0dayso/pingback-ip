using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class GroupResult
    {
        private string m_GroupInfoString = "";
        private bool m_HasContent = false;
        string[] splitLine = { "<eg666>" };
        string[] splitField = { "<eg66>" };

        private List<GroupTicketInfo> m_GroupInfoList = new List<GroupTicketInfo>();

        public GroupResult(string content)
        {
            m_GroupInfoString = content;
            m_HasContent = (m_GroupInfoString != "");
            init();
        }
        public string GroupInfoString
        {
            get
            {
                return m_GroupInfoString;
            }
            set
            {
                m_GroupInfoString = value;
            }
        }
        private void init()
        {
            if (!m_HasContent) return;
            string[] lsrows = m_GroupInfoString.Split(splitLine, StringSplitOptions.None);
            for (int irows = 0; irows < lsrows.Length; irows++)
            {
                string[] lscols = lsrows[irows].Split(splitField, StringSplitOptions.None);
                GroupTicketInfo ti = new GroupTicketInfo();
                ti.A_id = lscols[0];
                ti.B_name = lscols[7];
                ti.C_citypair = lscols[1];
                ti.D_flightno = lscols[2];
                ti.E_date = lscols[3];
                ti.F_rebate = lscols[4];
                ti.G_total = lscols[5];
                ti.H_pnr = lscols[6];
                ti.I_remain = Convert.ToString(int.Parse(lscols[5])-int.Parse(lscols[9]));
                ti.J_remark = lscols[8];
                m_GroupInfoList.Add(ti);
            }
        }
        public void ToListView(System.Windows.Forms.ListView lv)
        {
            string dh = " , ";
            try
            {
                lv.Items.Clear();
                if (!m_HasContent) return;
                for (int i = 0; i < m_GroupInfoList.Count; ++i)
                {
                    System.Windows.Forms.ListViewItem lvi = new System.Windows.Forms.ListViewItem();
                    GroupTicketInfo gi = m_GroupInfoList[i];
                    lvi.Text = gi.A_id;
                    lvi.SubItems.Add(gi.B_name);
                    lvi.SubItems.Add(gi.C_citypair);
                    lvi.SubItems.Add(gi.D_flightno);
                    lvi.SubItems.Add(gi.E_date);
                    lvi.SubItems.Add(gi.F_rebate);
                    lvi.SubItems.Add(gi.G_total);
                    lvi.SubItems.Add(gi.H_pnr);
                    lvi.SubItems.Add(gi.I_remain);
                    lvi.SubItems.Add(gi.J_remark);
                    System.Windows.Forms.ListViewItem lvi2 = new System.Windows.Forms.ListViewItem(gi.A_id);
                    System.Windows.Forms.ListViewItem lvi3 = new System.Windows.Forms.ListViewItem(gi.A_id);
                    for (int j = 1; j < lvi.SubItems.Count; ++j)
                    {
                        lvi2.SubItems.Add(lvi.SubItems[j].Text);
                        lvi3.SubItems.Add(lvi.SubItems[j].Text);
                    }

                    lvi.SubItems.Add(gi.B_name);//TITLE
                    lvi.ForeColor = System.Drawing.Color.Red;
                    //MU2501 , 价格:250 , 共0/100人
                    lvi2.SubItems.Add("航班:"+gi.D_flightno + dh + "价格:" + gi.F_rebate + dh + "剩余" + gi.I_remain + "/" + gi.G_total + "人");
                    //备注
                    lvi3.SubItems.Add("备注:" + gi.J_remark);
                    lv.Items.Add(lvi);
                    lv.Items.Add(lvi2);
                    lv.Items.Add(lvi3);

                }
                for (int i = 0; i < lv.Items.Count/3; i++)
                {
                    if (i % 2 == 0)
                    {
                        lv.Items[3 * i].BackColor = System.Drawing.Color.LightGray;
                        lv.Items[3 * i + 1].BackColor = System.Drawing.Color.LightGray;
                        lv.Items[3 * i + 2].BackColor = System.Drawing.Color.LightGray;
                    }
                }
                
            }
            catch
            {
            }
            
        }
        public void ToListView(System.Windows.Forms.ListView lv, string context)
        {
            m_GroupInfoString = context;
            m_HasContent = (m_GroupInfoString != "");
            if (!m_HasContent) return;
            init();
            ToListView(lv);
        }

    }
    struct GroupTicketInfo
    {
        public string A_id;
        public string B_name;
        public string C_citypair;
        public string D_flightno;
        public string E_date;
        public string F_rebate;
        public string G_total;
        public string H_pnr;
        public string I_remain;
        public string J_remark;
    }
}
