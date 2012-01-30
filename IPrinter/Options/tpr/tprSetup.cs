using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Options.tpr
{
    public partial class tprSetup : Form
    {
        public tprSetup()
        {
            InitializeComponent();
        }
        string tprSetupNode = "tprSetup";
        string tprCmdNode = "tprCommand";
        string tprBeg = "tprBeg";
        string tprEnd = "tprEnd";

        string filename = System.Windows.Forms.Application.StartupPath + "\\options.XML";
        private void tprSetup_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);

            XmlNode xn = xd.SelectSingleNode("eg");
            try
            {
                textBox1.Text = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode).SelectSingleNode(tprCmdNode).InnerText;
                comboBox1.Text = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode).SelectSingleNode(tprBeg).InnerText;
                comboBox2.Text = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode).SelectSingleNode(tprEnd).InnerText;
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                save();
            }
            catch(Exception ex)
            {
                MessageBox.Show("tprSetup.cs button1_Click" + ex.Message);
            }
            
        }
        void save()
        {
            int iBeg = -1;
            int iEnd = -1;
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Text == comboBox1.Items[i].ToString()) iBeg = i;
                if (comboBox2.Text == comboBox2.Items[i].ToString()) iEnd = i;
            }
            if (iBeg < 0 || iEnd < 0) throw new Exception("请选择正确时间段！");

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn;
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode);
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement(tprSetupNode);
                xe.InnerText = textBox1.Text;
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode);
            }
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode).SelectSingleNode(tprBeg);
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement(tprBeg);
                xe.InnerText = comboBox1.Text;
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode);
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode);
            }
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode).SelectSingleNode(tprEnd);
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement(tprEnd);
                xe.InnerText = comboBox2.Text;
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode);
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode(tprSetupNode);
            }
            xd.Save(filename);
        }
    }
}