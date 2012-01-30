using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
namespace EagleFinance
{
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
            try
            {
                readSort();
            }
            catch
            {
            }
            loadExportItems();
        }
        void readSort()
        {
            string fn = Application.StartupPath+"\\"+"eticketManagerExportSort.txt";
            if (File.Exists(fn))
            {
                StreamReader sr = new StreamReader(fn);
                string[] sort = sr.ReadLine().Trim().Split(',');
                sr.Close();
                if (sort.Length != 26) return;////////////����
                List<string> ls = new List<string>();
                for (int i = 0; i < sort.Length; i++)
                {
                    ls.Add(listBox1.Items[int.Parse(sort[i])].ToString());
                }
                listBox1.Items.Clear();
                listBox1.Items.AddRange(ls.ToArray());
                sr.Close();
            }
            else File.Create(fn);
        }
        void saveSort()
        {
            string order = "";
            string fn = Application.StartupPath + "\\" + "eticketManagerExportSort.txt";
            StreamWriter sw = new StreamWriter(fn, false);
            string[] orig = new string[]{
            "ID",
            "OFFICE",
            "Ʊ��",
            "�������",
            "����Ʊ��",
            "ʼ��",
            "Ʊ��",
            "˰",
            "�׼۵��",
            "PNR",
            "��������",
            "״̬",

            "�û�",
            "������",
            "������",
            "�˻�����",
            "�����",
            "��λ",
            "�ɵֳ���",
            "������", 

            "�г̵���",
            "�׼۽��",
            "������",
            "����",
            "ʵ��",
            "��ע"};
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                for (int j = 0; j < orig.Length; j++)
                {
                    if (listBox1.Items[i].ToString() == orig[j]) order += j.ToString() + ",";
                }
            }
            order = order.Substring(0, order.Length - 1);
            sw.WriteLine(order);
            sw.Close();
        }
        public List<int> rowExport = new List<int>();
        //������ť
        private void button1_Click(object sender, EventArgs e)
        {
            saveExportItems();
            saveSort();
            //����˳��
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                switch (listBox1.Items[i].ToString())
                {
                    case "ID":
                        if (checkBox1.Checked) rowExport.Add(0);
                        break;
                    case "OFFICE":
                        if (checkBox2.Checked) rowExport.Add(1);
                        break;
                    case "Ʊ��":
                        if (checkBox3.Checked) rowExport.Add(2);
                        break;
                    case "�������":
                        if (checkBox4.Checked) rowExport.Add(3);
                        break;
                    case "����Ʊ��":
                        if (checkBox5.Checked) rowExport.Add(4);
                        break;
                    case "ʼ��":
                        if (checkBox6.Checked) rowExport.Add(5);
                        break;
                    case "Ʊ��":
                        if (checkBox7.Checked) rowExport.Add(6);
                        break;
                    case "˰":
                        if (checkBox8.Checked) rowExport.Add(7);
                        break;
                    case "�׼۵��":
                        if (checkBox9.Checked) rowExport.Add(8);
                        break;
                    case "PNR":
                        if (checkBox10.Checked) rowExport.Add(9);
                        break;
                    case "�г̵���":
                        if (checkBox21.Checked) rowExport.Add(10);
                        break;
                    case "��������":
                        if (checkBox11.Checked) rowExport.Add(11);
                        break;
                    case "״̬":
                        if (checkBox12.Checked) rowExport.Add(12);
                        break;
                    case "�û�":
                        if (checkBox13.Checked) rowExport.Add(13);
                        break;
                    case "������":
                        if (checkBox14.Checked) rowExport.Add(14);
                        break;
                    case "������":
                        if (checkBox15.Checked) rowExport.Add(15);
                        break;
                    case "�˻�����":
                        if (checkBox16.Checked) rowExport.Add(16);
                        break;
                    case "�����":
                        if (checkBox17.Checked) rowExport.Add(17);
                        break;
                    case "��λ":
                        if (checkBox18.Checked) rowExport.Add(18);
                        break;
                    case "�ɵֳ���":
                        if (checkBox19.Checked) rowExport.Add(19);
                        break;
                    case "������":
                        if (checkBox20.Checked) rowExport.Add(20);
                        break;
                    case "�׼۽��":
                        if (checkBox22.Checked) rowExport.Add(22);
                        break;
                    case "������":
                        if (checkBox23.Checked) rowExport.Add(23);
                        break;
                    case "����":
                        if (checkBox24.Checked) rowExport.Add(24);
                        break;
                    case "ʵ��":
                        if (checkBox25.Checked) rowExport.Add(25);
                        break;
                    case "��ע":
                        if (checkBox26.Checked) rowExport.Add(26);
                        break;

                }
            }
            //if (checkBox1.Checked) rowExport.Add(0);
            //if (checkBox2.Checked) rowExport.Add(1);
            //if (checkBox3.Checked) rowExport.Add(2);
            //if (checkBox4.Checked) rowExport.Add(3);
            //if (checkBox5.Checked) rowExport.Add(4);
            //if (checkBox6.Checked) rowExport.Add(5);
            //if (checkBox7.Checked) rowExport.Add(6);
            //if (checkBox8.Checked) rowExport.Add(7);
            //if (checkBox9.Checked) rowExport.Add(8);
            //if (checkBox10.Checked) rowExport.Add(9);
            //if (checkBox11.Checked) rowExport.Add(10);
            //if (checkBox12.Checked) rowExport.Add(11);
            //if (checkBox13.Checked) rowExport.Add(12);
            //if (checkBox14.Checked) rowExport.Add(13);
            //if (checkBox15.Checked) rowExport.Add(14);
            //if (checkBox19.Checked) rowExport.Add(15);
            //if (checkBox16.Checked) rowExport.Add(16);
            //if (checkBox17.Checked) rowExport.Add(17);
            //if (checkBox18.Checked) rowExport.Add(18);
            //if (checkBox20.Checked) rowExport.Add(19);
        }

        private void btOrignal_Click(object sender, EventArgs e)
        {
            orignalSort();
        }
        void orignalSort()
        {
            listBox1.Items.Clear();
            this.listBox1.Items.AddRange(new object[] {
            "ID",
            "OFFICE",
            "Ʊ��",
            "�������",
            "����Ʊ��",
            "ʼ��",
            "Ʊ��",
            "˰",
            "�׼۵��",
            "PNR",
            "��������",
            "״̬",
            "�û�",
            "������",
            "������",
            "�˻�����",
            "�����",
            "��λ",
            "�ɵֳ���",
            "������", "�г̵���",
            "�׼۽��",
            "������",
            "����",
            "ʵ��",
            "��ע"});
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i < 1) return;
            string up = listBox1.Items[i - 1].ToString();
            string down = listBox1.Items[i].ToString();
            listBox1.Items[i - 1] = down;
            listBox1.Items[i] = up;
            listBox1.SelectedIndex--;
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i >= listBox1.Items.Count - 1) return;
            string up = listBox1.Items[i ].ToString();
            string down = listBox1.Items[i+1].ToString();
            listBox1.Items[i] = down;
            listBox1.Items[i+1] = up;
            listBox1.SelectedIndex++;
        }
        void saveExportItems()
        {
            string h = "EAGLEFINANCEEXPORT";
            Hashtable ht = new Hashtable();
            foreach (Control c in groupBox1.Controls)
            {
                if ((c as CheckBox).Checked)
                {
                    ht.Add(h + c.Name, "1");
                }
                else
                {
                    ht.Add(h + c.Name, "0");
                }
            }
            foreach (Control c in groupBox2.Controls)
            {
                if ((c as CheckBox).Checked)
                {
                    ht.Add(h + c.Name, "1");
                }
                else
                {
                    ht.Add(h + c.Name, "0");
                }
            }
            foreach (Control c in groupBox3.Controls)
            {
                if ((c as CheckBox).Checked)
                {
                    ht.Add(h + c.Name, "1");
                }
                else
                {
                    ht.Add(h + c.Name, "0");
                }
            }
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
        }
        void loadExportItems()
        {
            Hashtable ht = EagleString.EagleFileIO.ReadEagleDotTxtFileToHashTable();
            
            string h = "EAGLEFINANCEEXPORT";

            bool bSaved = false;
            foreach (DictionaryEntry de in ht)
            {
                if (de.Key.ToString().IndexOf(h) == 0)
                {
                    bSaved = true;
                    break;
                }
            }
            if (!bSaved) return;
            foreach (Control c in groupBox1.Controls)
            {
                (c as CheckBox).Checked = false;
            }
            foreach (Control c in groupBox2.Controls)
            {
                (c as CheckBox).Checked = false;
            }
            foreach (Control c in groupBox3.Controls)
            {
                (c as CheckBox).Checked = false;
            }
            
            foreach (DictionaryEntry de in ht)
            {
                string key = de.Key.ToString().Replace(h,"");
                foreach (Control c in groupBox1.Controls)
                {
                    if (key == c.Name) (c as CheckBox).Checked = (de.Value.ToString() == "1");
                }
                foreach (Control c in groupBox2.Controls)
                {
                    if (key == c.Name) (c as CheckBox).Checked = (de.Value.ToString() == "1");
                }
                foreach (Control c in groupBox3.Controls)
                {
                    if (key == c.Name) (c as CheckBox).Checked = (de.Value.ToString() == "1");
                }
            }
        }
    }
}