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
            if (xml0.IndexOf("不存在该客户信息！") >= 0)//"<eg>不存在该客户信息！电话为：" +"11111111" + "</eg>"
            {
                webBrowser1.Dock = DockStyle.Top;
                groupBox1.Visible = true;

                int pos1 = xml0.IndexOf("：") + 1;
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
                //标题
                htm += "<tr>";
                int substart = xml0.IndexOf("<cm>");
                if (substart < 0) throw new Exception("");
                substart += 4;
                int subend = xml0.IndexOf("</cm>");
                htm += "<td bgcolor=#FFFFFF>" + xml0.Substring(substart, subend - substart) + "</td>";
                htm += "</tr>";
                //客户ID
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>客户ID</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/客户ID").InnerText + "</td>";
                htm += "</tr>";
                //客户号
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>客户号</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/客户号").InnerText + "</td>";

                htm += "</tr>";
                //姓名
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>姓名</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/姓名").InnerText + "</td>";

                htm += "</tr>";
                //电话
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>电话</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/电话").InnerText + "</td>";

                htm += "</tr>";
                //证件类型及证件号
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/证件类型").InnerText + "</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/证件号").InnerText + "</td>";

                htm += "</tr>";
                //用户类型
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>用户类型</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/用户类型").InnerText + "</td>";

                htm += "</tr>";
                //用户来源
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>来源</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/来源").InnerText + "</td>";

                htm += "</tr>";
                //积分
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>积分</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/积分").InnerText + "</td>";

                htm += "</tr>";
                //建立时间
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>建立时间</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/建立时间").InnerText + "</td>";

                htm += "</tr>";
                //上次消费
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>上次消费</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/上次消费").InnerText + "</td>";

                htm += "</tr>";
                //归属代理号
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>归属代理号</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/归属号").InnerText + "</td>";

                htm += "</tr>";
                //建档人
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>建档人</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/建档人").InnerText + "</td>";

                htm += "</tr>";
                //归属名
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>归属名</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/归属名").InnerText + "</td>";

                htm += "</tr>";
                //客户地址
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>客户地址</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/客户地址").InnerText + "</td>";

                htm += "</tr>";
                //客户生日
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>客户生日</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/客户生日").InnerText + "</td>";

                htm += "</tr>";
                //信用卡
                htm += "<tr>";
                htm += "<td bgcolor=#FFFFFF>信用卡</td>";
                htm += "<td bgcolor=#FFFFFF>" + xd.SelectSingleNode("//A/eg/信用卡").InnerText + "</td>";

                htm += "</tr>";

                htm += "</table>";
                //最近来电记录*******************************波波的分割线**************************************
                string s = "";
                string[] a = null;
                    htm += "<table border=0 width=100% cellpadding=2 cellspacing=1 bgcolor=#FF0000 style=font-size:12px>";
                    try
                    {
                        s = xd.SelectSingleNode("//A/eg/Content").InnerText;
                        a = s.Split('$');
                        htm += "<tr>";
                        htm += "<td bgcolor=#FFFFFF>来电号码</td><td bgcolor=#FFFFFF>来电时间</td>";
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
                        htm += "<td bgcolor=#FFFFFF>" + "取来电记录失败" + "</td>";
                        htm += "</tr>";
                    }
                    htm += "</table>";

                //消费记录表*******************************波波的分割线**************************************
                
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
                    htm += "<td bgcolor=#FFFFFF>" + "取消费记录失败" + "</td>";
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
            //******************************************波波的分割线********************************************************
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
            npCust.AddPara("cm", "SaveCust");//操作类型说明
            npCust.AddPara("vcInpEgUser", egUser);
            npCust.AddPara("vcIdentCard", tbCostCardId.Text.Trim());
            npCust.AddPara("vcCustName", tbCostName.Text.Trim());
            npCust.AddPara("vcMobile", tbTele.Text.Trim());
            npCust.AddPara("numMemSrc", "1");
            WS.egws ws = new WS.egws(this.ws);
            string strRet = ws.getEgSoap(npCust.GetXML());
            //<?xml version="1.0" encoding="utf-8"?><eg><cm>RetSaveCust</cm><Flag>succ</Flag><CustId>1649</CustId><Mes>新增成功</Mes></eg>
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