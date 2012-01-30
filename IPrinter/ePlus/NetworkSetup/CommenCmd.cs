using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ePlus
{
    public partial class CommenCmd : Form
    {
        public string selectedString = "";
        public CommenCmd()
        {
            InitializeComponent();
        }


        private void CommenCmd_Load(object sender, EventArgs e)
        {
            string filename = System.Windows.Forms.Application.StartupPath + "\\options.XML";
            //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //StreamReader sr = new StreamReader(fs, Encoding.Default);
            //string temp = sr.ReadToEnd();
            //sr.Close();
            //fs.Close();
            string temp = EagleAPI.egReadFile(filename);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("CommenCmds");
            lbCmds.Items.Clear();
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                lbCmds.Items.Add(xn.ChildNodes[i].InnerText);
            }
        }

        private void lbCmds_DoubleClick(object sender, EventArgs e)
        {
            selectedString = lbCmds.SelectedItem.ToString();
            GlobalVar.CommenCmdTemp = selectedString;
            GlobalVar.CommenCmdCurrent = selectedString;
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            CommenCmdAdd dlg = new CommenCmdAdd();
            dlg.ShowDialog();
            CommenCmd_Load(sender, e);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            string filename = System.Windows.Forms.Application.StartupPath + "\\options.XML";
            string temp = EagleAPI.egReadFile(filename);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("CommenCmds");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                if(lbCmds.SelectedItem.ToString()==xn.ChildNodes[i].InnerText)
                    xn.RemoveChild(xn.ChildNodes[i]);
            }
            xd.Save(filename);
            CommenCmd_Load(sender, e);
        }
        
    }
}