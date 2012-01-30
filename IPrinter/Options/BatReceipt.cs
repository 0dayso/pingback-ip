using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Options
{
    public partial class BatReceipt : Form
    {
        public BatReceipt()
        {
            InitializeComponent();
        }
        bool isprint = false;
        
        public BatReceipt(bool isPrint)
        {
            InitializeComponent();
            isprint = isPrint;
            if (isPrint)//��ӡ
            {
                this.Text = "������ӡ�г̵�";
                this.btBat.Text += "��ӡ";
            }
            else
            {
                this.Text = "���������г̵�";
                this.btBat.Text += "����";
                this.chName.Width = 0;
                this.chIdCard.Width = 0;
            }
        }
        private void BatReceipt_Load(object sender, EventArgs e)
        {
            if (isprint)
            {
                this.btDel.Enabled = false;
                this.btAdd.Text = "�����г̵���";
                for (int i = 0; i < lsEticketNumber.Count; i++)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = string.Format("{0}", i + 1);
                    li.SubItems.Add("0");//13707159625
                    li.SubItems.Add(lsEticketNumber[i].Remove(3,1));
                    li.SubItems.Add(lsRcpName[i]);
                    li.SubItems.Add(lsRcpIdCard[i]);
                    lv.Items.Add(li);
                }

            }
        }

        private void tbReceiptBeg_TextChanged(object sender, EventArgs e)
        {
            tbReceiptEnd.Text = tbReceiptBeg.Text;
        }

        private void tbEticketBeg_TextChanged(object sender, EventArgs e)
        {
            tbEticketEnd.Text = tbEticketBeg.Text;
        }

        private void tbReceiptEnd_Enter(object sender, EventArgs e)
        {
            if (!isprint)
                focusReceiptEnd();
            else
            {
                tbReceiptEnd.Text = string.Format("{0}",long.Parse(tbReceiptBeg.Text) + (long)(lv.Items.Count - 1));
            }
        }
        private void focusReceiptEnd()
        {
            try
            {
                long receipt1 = long.Parse(tbReceiptBeg.Text);
                long eticket1 = long.Parse(tbEticketBeg.Text);
                long eticket2 = long.Parse(tbEticketEnd.Text);
                long receipt2 = receipt1 + eticket2 - eticket1;
                tbReceiptEnd.Text = receipt2.ToString("D10");
            }
            catch
            {
            }

        }
        private void tbEticketEnd_Enter(object sender, EventArgs e)
        {
            focusEticketEnd();
        }
        private void focusEticketEnd()
        {
            try
            {
                long receipt1 = long.Parse(tbReceiptBeg.Text);
                long receipt2 = long.Parse(tbReceiptEnd.Text);
                long eticket1 = long.Parse(tbEticketBeg.Text);
                long eticket2 = eticket1 + (receipt2 - receipt1);

                tbEticketEnd.Text = eticket2.ToString("D13");
            }
            catch
            {
            }

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (!isprint)
                addrow();
            else
            {
                try
                {
                    if ((long)(lv.Items.Count - 1) != (long.Parse(tbReceiptEnd.Text) - long.Parse(tbReceiptBeg.Text)))
                    {
                        MessageBox.Show("��Χ����ȷ");
                    }
                    else
                    {
                        for (long l = long.Parse(tbReceiptBeg.Text); l <= long.Parse(tbReceiptEnd.Text); l++)
                        {
                            int pos = (int)(l - long.Parse(tbReceiptBeg.Text));
                            lv.Items[pos].SubItems[1].Text = l.ToString("D10");
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
        private void addrow()
        {
            string rc1 = tbReceiptBeg.Text.Trim();
            string rc2 = tbReceiptEnd.Text.Trim();
            string et1 = tbEticketBeg.Text.Trim();
            string et2 = tbEticketEnd.Text.Trim();
            if (rc1.Length != 10 || rc2.Length != 10 || et2.Length != 13 || et2.Length != 13)
            {
                MessageBox.Show("�г̵��Ż���ӿ�Ʊ�ų��ȴ���");
                return;
            }
            try
            {
                if (long.Parse(et2) - long.Parse(et1) != long.Parse(rc2) - long.Parse(rc1))
                {
                    MessageBox.Show("����Ƿ�Ϊ����");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("�г̵��Ż���ӿ�Ʊ�Ŵ���");
                return;
            }
            //�鿴�Ƿ��Ѿ������г̵���
            for (long i = long.Parse(rc1); i <= long.Parse(rc2); i++)
            {
                for (int j = 0; j < lv.Items.Count; j++)
                {
                    if (lv.Items[j].SubItems[1].Text == i.ToString("D10"))
                    {
                        MessageBox.Show(i.ToString() + " - �г̵����ظ�");
                        return;
                    }
                }
            }
            for (long i = long.Parse(et1); i <= long.Parse(et2); i++)
            {
                for (int j = 0; j < lv.Items.Count; j++)
                {
                    if (lv.Items[j].SubItems[2].Text == i.ToString("D13"))
                    {
                        MessageBox.Show(i.ToString() + " - ���ӿ�Ʊ���ظ�");
                        return;
                    }
                }
            }


            //����
            for (long l = long.Parse(et1); l <= long.Parse(et2); l++)
            {
                long step = l - long.Parse(et1);
                ListViewItem li = new ListViewItem();
                li.Text = (string.Format("{0}", lv.Items.Count + 1));
                long currc = long.Parse(rc1) + step;
                li.SubItems.Add(currc.ToString("D10"));
                li.SubItems.Add(l.ToString("D13"));
                lv.Items.Add(li);
            }
                

        }

        private void btDel_Click(object sender, EventArgs e)
        {
            delrows();
        }
        private void delrows()
        {
            for (int i = lv.SelectedItems.Count - 1; i >= 0; i--)
                lv.Items.RemoveAt(lv.SelectedItems[i].Index);
            for (int i = 0; i < lv.Items.Count; i++)
            {
                int j = i+1;
                lv.Items[i].Text = j.ToString(); 
            }
        }

        private void btBat_Click(object sender, EventArgs e)
        {
            bat();
        }
        public List<string> lsEticketNumber = new List<string>();
        public List<string> lsReceiptNumber = new List<string>();
        public List<string> lsRcpName = new List<string>();
        public List<string> lsRcpIdCard = new List<string>();

        private void bat()
        {
            lsEticketNumber.Clear();
            lsReceiptNumber.Clear();
            lsRcpName.Clear();
            lsRcpIdCard.Clear();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                lsEticketNumber.Add(lv.Items[i].SubItems[2].Text);
                lsReceiptNumber.Add(lv.Items[i].SubItems[1].Text);
                if (isprint)
                {
                    lsRcpName.Add(lv.Items[i].SubItems[3].Text);
                    lsRcpIdCard.Add(lv.Items[i].SubItems[4].Text);
                }
            }
            this.Close();
            
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tbEticketBeg.Text = lv.Items[lv.SelectedIndices[0]].SubItems[2].Text;
                tbEticketEnd.Text = lv.Items[lv.SelectedIndices[0]].SubItems[2].Text;
            }
            catch
            {
            }
        }


        
        
    }
}