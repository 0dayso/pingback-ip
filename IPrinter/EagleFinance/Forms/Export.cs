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
                if (sort.Length != 26) return;////////////列数
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
            "票号",
            "入库日期",
            "电子票号",
            "始终",
            "票价",
            "税",
            "底价点扣",
            "PNR",
            "销售日期",
            "状态",

            "用户",
            "中文名",
            "代理商",
            "乘机日期",
            "航班号",
            "舱位",
            "飞抵城市",
            "代理返点", 

            "行程单号",
            "底价金额",
            "返点金额",
            "利润",
            "实收",
            "备注"};
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
        //导出按钮
        private void button1_Click(object sender, EventArgs e)
        {
            saveExportItems();
            saveSort();
            //调节顺序
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
                    case "票号":
                        if (checkBox3.Checked) rowExport.Add(2);
                        break;
                    case "入库日期":
                        if (checkBox4.Checked) rowExport.Add(3);
                        break;
                    case "电子票号":
                        if (checkBox5.Checked) rowExport.Add(4);
                        break;
                    case "始终":
                        if (checkBox6.Checked) rowExport.Add(5);
                        break;
                    case "票价":
                        if (checkBox7.Checked) rowExport.Add(6);
                        break;
                    case "税":
                        if (checkBox8.Checked) rowExport.Add(7);
                        break;
                    case "底价点扣":
                        if (checkBox9.Checked) rowExport.Add(8);
                        break;
                    case "PNR":
                        if (checkBox10.Checked) rowExport.Add(9);
                        break;
                    case "行程单号":
                        if (checkBox21.Checked) rowExport.Add(10);
                        break;
                    case "销售日期":
                        if (checkBox11.Checked) rowExport.Add(11);
                        break;
                    case "状态":
                        if (checkBox12.Checked) rowExport.Add(12);
                        break;
                    case "用户":
                        if (checkBox13.Checked) rowExport.Add(13);
                        break;
                    case "中文名":
                        if (checkBox14.Checked) rowExport.Add(14);
                        break;
                    case "代理商":
                        if (checkBox15.Checked) rowExport.Add(15);
                        break;
                    case "乘机日期":
                        if (checkBox16.Checked) rowExport.Add(16);
                        break;
                    case "航班号":
                        if (checkBox17.Checked) rowExport.Add(17);
                        break;
                    case "舱位":
                        if (checkBox18.Checked) rowExport.Add(18);
                        break;
                    case "飞抵城市":
                        if (checkBox19.Checked) rowExport.Add(19);
                        break;
                    case "代理返点":
                        if (checkBox20.Checked) rowExport.Add(20);
                        break;
                    case "底价金额":
                        if (checkBox22.Checked) rowExport.Add(22);
                        break;
                    case "返点金额":
                        if (checkBox23.Checked) rowExport.Add(23);
                        break;
                    case "利润":
                        if (checkBox24.Checked) rowExport.Add(24);
                        break;
                    case "实收":
                        if (checkBox25.Checked) rowExport.Add(25);
                        break;
                    case "备注":
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
            "票号",
            "入库日期",
            "电子票号",
            "始终",
            "票价",
            "税",
            "底价点扣",
            "PNR",
            "销售日期",
            "状态",
            "用户",
            "中文名",
            "代理商",
            "乘机日期",
            "航班号",
            "舱位",
            "飞抵城市",
            "代理返点", "行程单号",
            "底价金额",
            "返点金额",
            "利润",
            "实收",
            "备注"});
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