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
    public partial class Incoming : Form
    {
        public Incoming()
        {
            InitializeComponent();
        }

        private void btIncoming_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cbOffice.Text.Trim().Length != 6) throw new Exception("����ȷ��дOFFICE�ţ�6λ��");
                if (this.NumberBeg.Text.Trim().Length != 10) throw new Exception("����ȷ��д��ʼƱ�ţ�10λ��");
                if (this.NumberEnd.Text.Trim().Length != 10) throw new Exception("����ȷ��д��ֹƱ�ţ�10λ��");
                long numbeg = 0;
                long numend = 0;
                if (!long.TryParse(this.NumberBeg.Text, out numbeg)) throw new Exception("����ȷ��д��ʼƱ�ţ�10λ��ֻ��������");
                if (!long.TryParse(this.NumberEnd.Text, out numend)) throw new Exception("����ȷ��д��ֹƱ�ţ�10λ��ֻ��������");

                //���ʱ���Ѿ����ڵ�Ʊ�Ų������
                //string cmString = "insert into etickets (OFFICE,TKTNUMBER) values ('{0}','{1}')";
                for (long i = numbeg; i <= numend; i++)
                {
                    try
                    {
                        label4.Text = string.Format("�������--��{0}��/��{1}��,Ʊ��{2}", i - numbeg, numend - numbeg, i);
                        Application.DoEvents();
                        string cmString = "insert into etickets (OFFICE,TKTNUMBER,IMPORTCOUNT) values ('{0}','{1}',0)";
                        cmString = string.Format(cmString, this.cbOffice.Text.Trim(), i.ToString());
                        OleDbCommand cmd = new OleDbCommand(cmString, GlobalVar.cn);
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new Exception(i.ToString() + "������⣬�������ظ�");
                    }
                }
                label4.Text = string.Format("������,��{0}��Ʊ",numend-numbeg+1);
                MessageBox.Show("���ɹ���");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumberBeg_TextChanged(object sender, EventArgs e)
        {
            NumberEnd.Text = NumberBeg.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] a = textBox1.Text.Trim().Split('-');
                NumberBeg.Text = a[0] + a[1];
                NumberEnd.Text = a[0] + a[2];
            }
            catch
            {
            }
        }
    }
}