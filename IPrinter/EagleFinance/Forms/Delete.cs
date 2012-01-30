using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace EagleFinance
{
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void NumberBeg_TextChanged(object sender, EventArgs e)
        {
            NumberEnd.Text = NumberBeg.Text;
        }

        private void btIncoming_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ȷ��Ҫɾ����?", "ע��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (this.NumberBeg.Text.Trim().Length != 10) throw new Exception("����ȷ��д��ʼƱ�ţ�10λ��");
                    if (this.NumberEnd.Text.Trim().Length != 10) throw new Exception("����ȷ��д��ֹƱ�ţ�10λ��");
                    long numbeg = 0;
                    long numend = 0;
                    if (!long.TryParse(this.NumberBeg.Text, out numbeg)) throw new Exception("����ȷ��д��ʼƱ�ţ�10λ��ֻ��������");
                    if (!long.TryParse(this.NumberEnd.Text, out numend)) throw new Exception("����ȷ��д��ֹƱ�ţ�10λ��ֻ��������");
                    string cmString = string.Format("delete from etickets where TKTNUMBER>='{0}' and TKTNUMBER <='{1}'",
                        numbeg.ToString(), numend.ToString());
                    OleDbCommand cmd = new OleDbCommand(cmString, GlobalVar.cn);

                    int rows = cmd.ExecuteNonQuery();
                    throw new Exception("��ɾ��" + rows.ToString() + "��");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("���棺��������ݿ⣬���ǰ���ȱ������ݣ��Ƿ����Ҫ���", "",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string cmString = string.Format("delete from etickets");
                    OleDbCommand cmd = new OleDbCommand(cmString, GlobalVar.cn);

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("��ճɹ���");
                }
                catch
                {
                    MessageBox.Show("���ʧ�ܣ�");
                }
            }
        }
    }
}