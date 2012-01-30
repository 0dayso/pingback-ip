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
    //CommenCmd cc = new CommenCmd();
    //cc.Location = new Point(Screen.PrimaryScreen.Bounds.Width - cc.Width - 5 , 16 + MainMenu.Height + toolStrip.Height + lblNotice.Height + 8);
    //cc.Show();

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
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("CommenCmds");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                lbCmds.Items.Add(xn.ChildNodes[i].InnerText);
            }
        }

        private void lbCmds_DoubleClick(object sender, EventArgs e)
        {
            selectedString = lbCmds.SelectedItem.ToString();
        }

    }
}