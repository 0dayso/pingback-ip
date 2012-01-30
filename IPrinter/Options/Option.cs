using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Options
{
    public partial class Options : Form
    {
        public bool b_listnoseatbunk = false;
        public string localcitycode = "";
        public Options()
        {
            InitializeComponent();
        }

        string filename = System.Windows.Forms.Application.StartupPath + "\\options.XML";
        public void save()
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("LocalCityCode");
            xn.InnerText = this.tbLocalCityCode.Text.Trim().ToUpper();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SelectCityType");
            xn.InnerText = this.cbSelectCityType.SelectedIndex.ToString();
            //isListChinaCity
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListChinaCity");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("isListChinaCity");
                xe.InnerText = "1";
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListChinaCity");
            }
            xn.InnerText = (cbCity1.Checked ? "1" : "0");
            //isListForeignCity
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListForeignCity");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("isListForeignCity");
                xe.InnerText = "1";
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListForeignCity");
            }
            xn.InnerText = (cbCity2.Checked ? "1" : "0");
            //isListNoSeatBunk
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListNoSeatBunk");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("isListNoSeatBunk");
                xe.InnerText = "1";
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListNoSeatBunk");
            }

            xn.InnerText = (this.cbListNoSeatBunk.Checked ? "1" : "0");
            //CmdSendType
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("CmdSendType");
                if (xn == null) throw new Exception();
            }
            catch
            {
                XmlElement xe;
                xe = xd.CreateElement("CmdSendType");
                xe.InnerText = "1";
                xn = xd.SelectSingleNode("eg");
                xn.AppendChild(xe);
                xn = xd.SelectSingleNode("eg").SelectSingleNode("CmdSendType");
            }
            if (radioButton1.Checked) xn.InnerText = "1";
            if (radioButton2.Checked) xn.InnerText = "2";
            if (radioButton3.Checked) xn.InnerText = "3";

            xd.Save(filename);
        }

        public void read()
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);

            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("LocalCityCode");
            this.tbLocalCityCode.Text = xn.InnerText;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("SelectCityType");
            this.cbSelectCityType.SelectedIndex = int.Parse(xn.InnerText);

            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListChinaCity");
                this.cbCity1.Checked = (xn.InnerText.Trim() == "1");

                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListForeignCity");
                this.cbCity2.Checked = (xn.InnerText.Trim() == "1");
            }
            catch
            {
                this.cbCity1.Checked = true;
                this.cbCity2.Checked = true;
            }
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("isListNoSeatBunk");
                this.cbListNoSeatBunk.Checked = (xn.InnerText.Trim() == "1");
            }
            catch
            {
                this.cbListNoSeatBunk.Checked = false;
            }
            try
            {
                xn = xd.SelectSingleNode("eg").SelectSingleNode("CmdSendType");
                switch (xn.InnerText)
                {
                    case "1":
                        radioButton1.Checked = true;
                        break;
                    case "2":
                        radioButton2.Checked = true;
                        break;
                    case "3":
                        radioButton3.Checked = true;
                        break;
                }
            }
            catch
            {
                radioButton1.Checked = true;
            }
        }

        private void Options_Load(object sender, EventArgs e)
        {
            read();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }


    }
}