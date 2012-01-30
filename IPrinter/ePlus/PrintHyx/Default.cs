using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using gs.para;

namespace ePlus.PrintHyx
{
    public partial class Default : Form
    {
        public Default()
        {
            InitializeComponent();
        }

        private void Yongan_Default_Load(object sender, EventArgs e)
        {
            
            GetConfig();
        }
        public string xmlFirst = "";
        private void btSave_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn;
            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode(xmlFirst);
            xn = xn.SelectSingleNode("ENumberHead");
            xn.InnerText = textBox1.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode(xmlFirst);
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = textBox2.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode(xmlFirst);
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDownX.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode(xmlFirst);
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDownY.Value.ToString();

            xd.Save(GlobalVar.s_configfile);
            this.Close();
        }
        public void GetConfig()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            NewPara np = new NewPara(temp);

            this.textBox1.Text = np.FindTextByPath("//eg/" + xmlFirst + "/ENumberHead");
            this.textBox2.Text = np.FindTextByPath("//eg/" + xmlFirst + "/Signature");
            this.numericUpDownX.Value = int.Parse(np.FindTextByPath("//eg/" + xmlFirst + "/OffsetX"));
            this.numericUpDownY.Value = int.Parse(np.FindTextByPath("//eg/" + xmlFirst + "/OffsetY"));
            
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}