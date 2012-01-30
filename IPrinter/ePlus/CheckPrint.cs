using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ePlus
{
    public partial class CheckPrint : Form
    {
        CheckPrintObject cpo;
        public CheckPrint()
        {
            InitializeComponent();
            cpo = new CheckPrintObject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Filter = "Excel �ļ�|*.xls|All   Files|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckPrintObject.bImport)//ȡ�ÿ�Ʊ�ųɹ�
            {
                timer1.Start();
                CheckPrintObject.intDlay = System.Convert.ToInt16(this.textBox3.Text);
                this.Text = "���ڼ��Ʊ��,�����ĵȴ�...";
                Thread th = new Thread(new ThreadStart(CheckPrintObject.StartCheck));
                th.Start();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = CheckPrintObject.intAmount;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cpo.WriteResult(1);
            cpo.WriteResult(2);
            string filename = Application.StartupPath + "\\Log\\NOPrintEtk" + System.DateTime.Now.ToShortDateString() + ".log";
            string filename1 = Application.StartupPath + "\\Log\\PrintedReceeipt" + System.DateTime.Now.ToShortDateString() + ".log";
            try
            {
                Process.Start(filename);//"C:\\WINDOWS\\notepad.exe" + 
                Process.Start(filename1);//"c:\\windows\\Notepad.exe" + 
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.Message + ea.StackTrace);
            }
        }

        private void CheckPrint_Load(object sender, EventArgs e)
        {
            this.textBox3.Text = CheckPrintObject.intDlay.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Text = "���ڵ�������...";
            cpo.ImportExcel(this.textBox1.Text.Trim());
           
            this.label2.Text = "�Ѿ��ɹ�����" + CheckPrintObject.alEtn.Count.ToString() + "�ŵ���Ʊ��!";
            this.Text = "��Ʊ����ɹ�!";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = CheckPrintObject.intCurCount;
            int percent = Convert.ToInt32(CheckPrintObject.intCurCount*1.0 / CheckPrintObject.intAmount*100);
            label3.Text = percent.ToString() + "%";
            if (CheckPrintObject.intCurCount == CheckPrintObject.intAmount)
            {
                timer1.Stop();
                MessageBox.Show("��������,������ɲ��鿴�����ť!");
            }
        }
    }
}