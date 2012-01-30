using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace EagleCTI
{
    public partial class FormPassInfo : Form
    {
        string egUser = "";
        string ws = "";
        public FormPassInfo(string EgUser,string wsServer)
        {
            InitializeComponent();
            egUser = EgUser;
            ws = wsServer;
            CheckForIllegalCrossThreadCalls = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.TopMost = true;
            //this.Size.Height = Screen.PrimaryScreen.Bounds.Height - 16;
            //this.Size.Width = Screen.PrimaryScreen.Bounds.Width / 4 - 16;
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width *3/ 8 - 16, Screen.PrimaryScreen.Bounds.Height - 16);
            
            //this.Show();
            //this.Hide();
        }

        private void FormPassInfo_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width * 5 / 8, 0);
        }
        //public string teleNum = "";
        public void setWebBrowser(string xml0)
        {
            if (xml0.IndexOf("�����ڸÿͻ���Ϣ��") >= 0)//"<eg>�����ڸÿͻ���Ϣ���绰Ϊ��" +"11111111" + "</eg>"
            {
                webBrowser1.Dock = DockStyle.Top;
                groupBox1.Visible = true;

                int pos1 = xml0.IndexOf("��") + 1;
                int pos2 = xml0.IndexOf("</eg>");
                tbTele.Text = xml0.Substring(pos1, pos2 - pos1);
                tbEgUser.Text = egUser;
            }
            else
            {
                webBrowser1.Dock = DockStyle.Fill;
                groupBox1.Visible = false;
            }

            string xml = xml0;
            try
            {
                XmlDocument xd = new XmlDocument ();
                xd.LoadXml(xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", " "));

                string htm = "";
                htm += "<table border=0 width=100% cellpadding=2 cellspacing=1 bgcolor=#0000FF style=font-size:12px>";
                //����
                htm += "<tr>";
                int substart = xml0.IndexOf("<cm>");
                if (substart < 0) throw new Exception("");
                substart += 4;
                int subend = xml0.IndexOf("</cm>");
                htm += "<td bgcolor=#FFFFFF>" + xml0.Substring(substart, subend - substart) + "</td>";
                htm += "</tr>";
                //�ͻ�ID
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�ͻ�ID</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�ͻ�ID").InnerText + "</td>";
                htm += "</tr>";
                //�ͻ���
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�ͻ���</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�ͻ���").InnerText + "</td>";

                htm += "</tr>";
                //����
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>����</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/����").InnerText + "</td>";

                htm += "</tr>";
                //�绰
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�绰</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�绰").InnerText + "</td>";

                htm += "</tr>";
                //֤�����ͼ�֤����
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/֤������").InnerText + "</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/֤����").InnerText + "</td>";

                htm += "</tr>";
                //�û�����
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�û�����</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�û�����").InnerText + "</td>";

                htm += "</tr>";
                //�û���Դ
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>��Դ</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/��Դ").InnerText + "</td>";

                htm += "</tr>";
                //����
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>����</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/����").InnerText + "</td>";

                htm += "</tr>";
                //����ʱ��
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>����ʱ��</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/����ʱ��").InnerText + "</td>";

                htm += "</tr>";
                //�ϴ�����
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�ϴ�����</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�ϴ�����").InnerText + "</td>";

                htm += "</tr>";
                //���������
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>���������</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/������").InnerText + "</td>";

                htm += "</tr>";
                //������
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>������</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/������").InnerText + "</td>";

                htm += "</tr>";
                //������
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>������</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/������").InnerText + "</td>";

                htm += "</tr>";
                //�ͻ���ַ
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�ͻ���ַ</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�ͻ���ַ").InnerText + "</td>";

                htm += "</tr>";
                //�ͻ�����
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>�ͻ�����</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/�ͻ�����").InnerText + "</td>";

                htm += "</tr>";
                //���ÿ�
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>���ÿ�</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/���ÿ�").InnerText + "</td>";

                htm += "</tr>";

                htm += "</table>";
                //��������¼*******************************�����ķָ���**************************************
                string s = "";
                string[] a = null;
                    htm += "<table border=0 width=100% cellpadding=2 cellspacing=1 bgcolor=#FF0000 style=font-size:12px>";
                    try
                    {
                        s = xd.SelectSingleNode("//A/eg/Content").InnerText;
                        a = s.Split('$');
                        htm += "<tr>";
                        htm += "<td bgcolor=#FFFFFF>�������</td><td bgcolor=#FFFFFF>����ʱ��</td>";
                        htm += "</tr>";
                        for (int i = 0; i < a.Length; i++)
                        {
                            htm += "<tr>";
                            htm += "<td bgcolor=#FFFFFF>" + a[i].Split('~')[0] + "</td><td bgcolor=#FFFFFF>" + a[i].Split('~')[1] + "</td>";
                            htm += "</tr>";
                        }
                    }
                    catch
                    {
                        htm += "<tr>";
                        htm += "<td bgcolor=#FFFFFF>" + "ȡ�����¼ʧ��" + "</td>";
                        htm += "</tr>";
                    }
                    htm += "</table>";

                //���Ѽ�¼��*******************************�����ķָ���**************************************
                
                htm += "<table border=0 width=100% cellpadding=2 cellspacing=1 bgcolor=#00FF00 style=font-size:12px>";
                try
                {
                    s = xd.SelectSingleNode("//A/eg/AirFee").InnerText;
                    a = s.Split('$');
                    for (int i = 0; i < a.Length; i++)
                    {
                        htm += "<tr>";
                        string[] b = a[i].Split('~');
                        for (int j = 0; j < b.Length; j++)
                        {
                            htm += "<td bgcolor=#FFFFFF>" + b[j] + "</td>";
                        }
                        htm += "</tr>";
                    }
                }
                catch
                {
                    htm += "<tr>";
                    htm += "<td bgcolor=#FFFFFF>" + "ȡ���Ѽ�¼ʧ��" + "</td>";
                    htm += "</tr>";
                }
                htm += "</table>";

                FileStream fs = new FileStream(Application.StartupPath + "\\eagleCTItemp.htm", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs,Encoding.Default);
                sw.Write(htm);
                sw.Close();
                fs.Close();
                webBrowser1.Url = new Uri(Application.StartupPath + "\\eagleCTItemp.htm");


                this.Show();
                return;
            }
            catch
            {
            }
            //******************************************�����ķָ���********************************************************
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\eagleCTItemp.xml", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(xml);
                sw.Close();
                fs.Close();
                webBrowser1.Url = new Uri(Application.StartupPath + "\\eagleCTItemp.xml");
            }
            this.Show();
        }

        private void FormPassInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btSaveCost_Click(object sender, EventArgs e)
        {
            if (tbCostName.Text.Trim().Length < 2) return;
            NewPara npCust = new NewPara();
            npCust.AddPara("cm", "SaveCust");//��������˵��
            npCust.AddPara("vcInpEgUser", egUser);
            npCust.AddPara("vcIdentCard", tbCostCardId.Text.Trim());
            npCust.AddPara("vcCustName", tbCostName.Text.Trim());
            npCust.AddPara("vcMobile", tbTele.Text.Trim());
            npCust.AddPara("numMemSrc", "1");
            WS.egws ws = new WS.egws(this.ws);
            string strRet = ws.getEgSoap(npCust.GetXML());
            //<?xml version="1.0" encoding="utf-8"?><eg><cm>RetSaveCust</cm><Flag>succ</Flag><CustId>1649</CustId><Mes>�����ɹ�</Mes></eg>
            setWebBrowser(strRet);
            webBrowser1.Dock = DockStyle.Top;
            groupBox1.Visible = true;
            NewPara npRet = new NewPara(strRet);
            string strCustId = npRet.FindTextByPath("//eg/CustId");
            if (strCustId == "" || strCustId == null) ;
            else
            {
                //tbCostName.Text = "";
                //tbCostCardId.Text = "";
            }
        }
    }
}