using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ePlus
{
    public partial class CommenCmdAdd : Form
    {
        public CommenCmdAdd()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string temp = EagleAPI.egReadFile(System.Windows.Forms.Application.StartupPath + "\\options.XML");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("CommenCmds");
            XmlNode xnAdd = xd.CreateElement("cmd");
            xnAdd.InnerText = textBox1.Text;
            xn.AppendChild(xnAdd);
            xd.Save(System.Windows.Forms.Application.StartupPath + "\\options.XML");
            this.Close();

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void CommenCmdAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}