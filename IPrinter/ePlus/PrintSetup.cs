using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ePlus
{
    public partial class PrintSetup : Form
    {
        public PrintSetup()
        {
            InitializeComponent();
        }

        private void PrintSetup_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ptconfig.mp3", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            List<string> readins = new List<string>();
            string temp;
            temp = sr.ReadLine();
            while (temp != null)
            {
                readins.Add(temp);
                temp = sr.ReadLine();
            }
            
            numericUpDown1.Value = decimal.Parse(readins[0].Substring(15));
            numericUpDown2.Value = decimal.Parse(readins[1].Substring(15));
            numericUpDown3.Value = decimal.Parse(readins[2].Substring(16));
            numericUpDown4.Value = decimal.Parse(readins[3].Substring(16));
            numericUpDown5.Value = decimal.Parse(readins[4].Substring(18));
            numericUpDown6.Value = decimal.Parse(readins[5].Substring(18));
            numericUpDown7.Value = decimal.Parse(readins[6].Substring(11));
            numericUpDown8.Value = decimal.Parse(readins[7].Substring(11));
            sr.Close();
            fs.Close();



        }
        //ÍË³ö
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //È·¶¨
        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ptconfig.mp3", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            List<string> readins = new List<string>();
            string temp;
            temp = sr.ReadLine();
            while (temp != null)
            {
                readins.Add(temp);
                temp = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            readins[0] = readins[0].Substring(0, 15) + numericUpDown1.Value.ToString();
            readins[1] = readins[1].Substring(0, 15) + numericUpDown2.Value.ToString();
            readins[2] = readins[2].Substring(0, 16) + numericUpDown3.Value.ToString();
            readins[3] = readins[3].Substring(0, 16) + numericUpDown4.Value.ToString();
            readins[4] = readins[4].Substring(0, 18) + numericUpDown5.Value.ToString();
            readins[5] = readins[5].Substring(0, 18) + numericUpDown6.Value.ToString();
            readins[6] = readins[6].Substring(0, 11) + numericUpDown7.Value.ToString();
            readins[7] = readins[7].Substring(0, 11) + numericUpDown8.Value.ToString();

            fs = new FileStream(Application.StartupPath + "\\ptconfig.mp3", FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("gb2312"));
            for (int i = 0; i < readins.Count; i++)
            {
                sw.WriteLine(readins[i]);
            }
            sw.Close();
            fs.Close();
            GlobalVar.fontsizecn = float.Parse(numericUpDown7.Value.ToString());
            GlobalVar.fontsizeen = float.Parse(numericUpDown8.Value.ToString());
            GlobalVar.o_ticket.X = float.Parse(numericUpDown1.Value.ToString());
            GlobalVar.o_ticket.Y = float.Parse(numericUpDown2.Value.ToString());
            GlobalVar.o_receipt.X = float.Parse(numericUpDown3.Value.ToString());
            GlobalVar.o_receipt.Y = float.Parse(numericUpDown4.Value.ToString());
            GlobalVar.o_insurance.X = float.Parse(numericUpDown5.Value.ToString());
            GlobalVar.o_insurance.Y = float.Parse(numericUpDown6.Value.ToString());
            this.Close();
        }

    }
}