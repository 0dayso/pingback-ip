using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Options.tpr
{
    class trpResult
    {
        public string tprstring = "";
        public trpResult(string str)
        {
            tprstring = str;
        }
        string getoffice()
        {
            string ret = "";
            try
            {
                string temp = "*   OFFICE :";
                int pos0 = tprstring.IndexOf(temp);
                if (pos0 < 0) return "";
                pos0 += temp.Length;

                temp = "IATA NUMBER : ";
                int pos1 = tprstring.IndexOf(temp);
                if (pos1 < 0) return "";
                ret = tprstring.Substring(pos0, pos1 - pos0).Trim();
            }
            catch
            {
            }
            return ret;
        }
        string getdate()
        {
            string ret = "";
            try
            {
                string temp = "DATE   : ";
                int pos0 = tprstring.IndexOf(temp);
                if (pos0 < 0) return "";
                pos0 += temp.Length;

                temp = "AIRLINE:";
                int pos1 = tprstring.IndexOf(temp);
                if (pos1 < 0) return "";
                ret = tprstring.Substring(pos0, pos1 - pos0).Trim();

            }
            catch
            {
            }
            return ret;
        }
        string gettotalticket()
        {
            string ret = "";
            try
            {
                string temp = "TOTAL TICKETS:";
                int pos0 = tprstring.IndexOf(temp);
                if (pos0 < 0) return "";
                pos0 += temp.Length;

                temp = "(";
                int pos1 = tprstring.IndexOf(temp,pos0);
                if (pos1 < 0) return "";
                ret = tprstring.Substring(pos0, pos1 - pos0).Trim();

            }
            catch
            {
            }
            return ret;
        }
        string getnormalfare()
        {
            string ret = "";
            try
            {
                string temp = "NORMAL FARE -- AMOUNT :";
                int pos0 = tprstring.IndexOf(temp);
                if (pos0 < 0) return "";
                pos0 += temp.Length;

                temp = "CNY";
                int pos1 = tprstring.IndexOf(temp,pos0);
                if (pos1 < 0) return "";
                ret = tprstring.Substring(pos0, pos1 - pos0).Trim();

            }
            catch
            {
            }
            return ret;
        }
        string getnomaltax()
        {
            string ret = "";
            try
            {
                string temp = "NORMAL TAX -- AMOUNT :";
                int pos0 = tprstring.IndexOf(temp);
                if (pos0 < 0) return "";
                pos0 += temp.Length;

                temp = "CNY";
                int pos1 = tprstring.IndexOf(temp, pos0);
                if (pos1 < 0) return "";
                ret = tprstring.Substring(pos0, pos1 - pos0).Trim();

            }
            catch
            {
            }
            return ret;
        }
        int gettotalticketInt()
        {
            string[] tkt = tprstring.Split('\n');
            int count = 0;
            for (int i = 0; i < tkt.Length; i++)
            {
                if (tkt[i][0] >= '0' && tkt[i][0] <= '9')
                {
                    count++;
                }
            }
            return count;
        }
        bool camptotalticket()
        {
            int i_count = gettotalticketInt();
            string s_count = gettotalticket();
            return (i_count.ToString() == s_count);
        }
        void writetprreport()
        {
            if (!camptotalticket()) return;
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\tpr"))
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\tpr");
            string filename = Application.StartupPath + "\\tpr\\" + getdate() + ".log";
            bool bexist = File.Exists(filename);
            FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            if(!bexist)
                sw.Write("<?xml version=\"1.0\" encoding=\"gb2312\"?><tpr></tpr>");
            sw.Close();
            fs.Close();

            XmlDocument xd = new XmlDocument();
            xd.Load(filename);

            XmlNode xn = xd.SelectSingleNode("tpr");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                try
                {
                    XmlNode tmpxn = xn.ChildNodes[i];
                    XmlAttributeCollection att = tmpxn.Attributes;
                    if (att["office"].ToString() == getoffice() && att["date"].ToString() == getdate())
                    {
                        if (MessageBox.Show
                            ("已经存在当前报表，是否覆盖？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            xn.RemoveChild(tmpxn);
                        }
                    }
                }
                catch
                {
                }
            }

            
            
        }
    }
}
