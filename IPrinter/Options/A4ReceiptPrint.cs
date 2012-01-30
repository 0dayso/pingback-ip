using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Options
{
    public partial class A4ReceiptPrint : Form
    {
        string[] logo = null;//{ "易网通", "武汉易格网科技", "北京广顺航空" };

        Image img = null;
        string toprightString = "中国BSP";
        string title = "电子客票行程单";
        public string bigPnr = "";
        public string passagerName = "";
        public string idcardNumber = "";

        public string airlineName = "";

        public string agentName = "";
        public string agentAddress = "";
        public string agentPhone = "";

        public string[][] sailinfo = { 
            new string [8]{"00","2","3","4","5","6","7","8"},
            new string [8]{"01","","","","","","",""},
            new string [8]{"","","","","","","",""},
            new string [8]{"","","","","","","",""},
            new string [8]{"","","","","","","",""}
        };

        public string fc = "";//19MAY07PEK CA SHA570.00CNY570.00END
        public string fp = "";
        public string ft = "";
        public string paymethod = "";//付款方式//CA3
        public string restriction = "";

        public string smallPnr = "";
        public string ticketNumber = "";
        public string 联票 = "";
        public string ticketTime = "";
        public string 航协code = "";
        public string taxBuild = "";
        public string taxFuel = "";
        public A4ReceiptPrint()
        {
            InitializeComponent();


        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            this.agentAddress = tbAgentAddr.Text;
            this.agentName = tbAgentName.Text;
            this.agentPhone = tbAgentPhone.Text;
            this.paymethod = tbPayMethod.Text;
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("宋体", 12, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            try
            {
                e.Graphics.DrawImage(img, new PointF(35.6F, 24.7F));//1.参数img的文件位置，参数全部存于xml中
            }
            catch
            {
            }
            e.Graphics.DrawString(toprightString, ptFontEn, ptBrush, new PointF(179.7F, 32.1F));//中国BSP
            e.Graphics.DrawLine(new Pen(Brushes.Gray, 0.5F), new PointF(35.6F, 46.1F), new PointF(196F, 46.1F));
            e.Graphics.DrawLine(new Pen(Brushes.Gray, 0.5F), new PointF(35.6F, 142.5F), new PointF(196F, 142.5F));
            e.Graphics.DrawString(title, new Font("System", 16), ptBrush, new PointF(96.4F, 56.4F));
            string sailstring = "";
            for (int i = 0; i < sailinfo.Length; i++)
            {
                if (sailinfo[i][0] == "") continue;
                int length = 0;
                for (int j = 0; j < sailinfo[i].Length; j++)
                {
                    length = System.Text.Encoding.Default.GetBytes(sailinfo[i][j]).Length;
                    switch (j)
                    {
                        case 0:
                            for (int k = 0; k < 15 - length; k++)
                            {
                                sailinfo[i][j] += " ";
                            }
                            break;
                        case 1:
                        case 2:
                        case 6:
                            for (int k = 0; k < 8 - length; k++)
                            {
                                sailinfo[i][j] += " ";
                            }

                            break;
                        case 3:
                        case 4:
                        case 5:
                            for (int k = 0; k < 10 - length; k++)
                            {
                                sailinfo[i][j] += " ";
                            }

                            break;
                        case 7:
                            for (int k = 0; k < 4 - length; k++)
                            {
                                sailinfo[i][j] += " ";
                            }

                            break;
                        default:
                            break;
                    }
                    sailstring += sailinfo[i][j];
                }
                sailstring += "\r\n";
            }
            e.Graphics.DrawString("航空公司记录编号：" + bigPnr + "\r\n"
                                    + "旅客姓名：" + passagerName + "\r\n"
                                    + "身份识别代码：" + idcardNumber + "\r\n"
                                    + "\r\n"
                                    + "出票航空公司：" + airlineName + "\r\n"
                                    + "\r\n"
                                    + "出票代理人：" + agentName + "\r\n"
                                    + "代理人地址：" + agentAddress + "\r\n"
                                    + "电话：" + agentPhone + "\r\n"
                                    + "\r\n"
                                    + "\r\n"
                                    + "\r\n"
                                    //15              10        10        12          12          12          10        4
                                    + "始发地/目的地  航班号  舱位    日期      时间      有效时间  状态    行李"
                                    + "\r\n"
                                    + "\r\n"
                                    + sailstring
                                    + "\r\n"
                                    + "票价计算："
                                    + fc + "\r\n"
                                    + "\r\n"
                                    + "付款方式：" + paymethod + "\r\n"
                                    + "\r\n"
                                    + "机票款：" + fp + "\r\n"
                                    + "总额：" + ft + "\r\n"
                                    + "\r\n"
                                    + "限制条件：" + restriction + "\r\n"
                                    + "须知：" + "\r\n"
                                    + "\r\n"
                                    , ptFontEn, ptBrush, new PointF(35.6F, 77.7F));
            e.Graphics.DrawString("·  请您在航班起飞前1小时到达机场，或请根据所乘航空公司规定提前到达机场，凭购票时使用"
                                    + "\r\n的有效身份证件到值机柜台按时办理乘机手续；"
                                    + "\r\n·  请您持有效身份证件、登机牌及本行程单办理安全检查手续；"
                                    + "\r\n·  如果需要提供帮助，请联系购票代理人或所乘航空公司。",
                                    new Font("System", 9, System.Drawing.FontStyle.Regular), ptBrush, new PointF(35.6F, 240.6F));
            e.Graphics.DrawString("订座记录编号：" + smallPnr + "\r\n"
                                    + "票号：" + ticketNumber + "\r\n"
                                    + "联票：" + 联票 + "\r\n"
                                    + "\r\n"
                                    + "出票时间：" + ticketTime + "\r\n"
                                    + "\r\n"
                                    + "航协代码：" + 航协code + "\r\n"
                                    + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n"
                                    + "税款：  " + taxBuild + "\r\n" + "        " + taxFuel
                                    , ptFontEn, ptBrush, new PointF(141.7F, 77.7F));
            switch (comboBox1.Text)
            {
                case "武汉易格网科技":
                    e.Graphics.DrawString("武汉易格网科技   HTTP://WWW.EG66.COM",
                                            new Font("system", 8, System.Drawing.FontStyle.Underline), ptBrush, new PointF(99.9F, 273.7F));
                    break;
                case "易网通":
                    e.Graphics.DrawString("易网通   HTTP://WWW.ET-CHINA.COM",
                        new Font("system", 8, System.Drawing.FontStyle.Underline), ptBrush, new PointF(99.9F, 273.7F));
                    break;

            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.Text)
                {
                    case "武汉易格网科技":
                        img = Image.FromFile(Application.StartupPath + "\\A4Print.jpg");
                        break;
                    case "北京广顺航空":
                        img = Image.FromFile(Application.StartupPath + "\\gs.jpg");
                        break;
                    case "易网通":
                        try
                        {
                            img = Image.FromFile(Application.StartupPath + "\\etc.jpg");
                        }
                        catch
                        {
                            MessageBox.Show("未找到图标文件，请将logo图标更名为etc.jpg，并重新打开此对话框后将生效");
                        }
                        break;
                    default:
                        MessageBox.Show("请选择图标");
                        return;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("未找到图标文件，请将logo图标更名为A4Print.jpg，并重新打开此对话框后将生效");
            }
            ptDoc.Print();
        }

        private void A4ReceiptPrint_Load(object sender, EventArgs e)
        {
            //logo = { "易网通", "武汉易格网科技", "北京广顺航空" };
            logo = new string[] { "易网通", "武汉易格网科技", "北京广顺航空" };
            for (int i = 0; i < logo.Length; i++)
            {
                comboBox1.Items.Add(logo[i]);
            }
            //读4个值
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\options.xml");
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("a4print");
                this.tbAgentAddr.Text = xn.SelectSingleNode("agentaddr").InnerText;
                this.tbAgentName.Text = xn.SelectSingleNode("agentname").InnerText;
                this.tbAgentPhone.Text = xn.SelectSingleNode("agentphone").InnerText;
                this.tbPayMethod.Text = xn.SelectSingleNode("paymethod").InnerText;
                try
                {
                    this.comboBox1.Text = xn.SelectSingleNode("logo").InnerText;
                    if (this.comboBox1.Text == "易网通")
                        this.comboBox1.Text = "武汉易格网科技";
                }
                catch
                {
                    this.comboBox1.Text = logo[0];
                    //XmlElement xe = xd.CreateElement("logo");
                    //xe.InnerText = "";
                    //xn = xd.SelectSingleNode("eg").SelectSingleNode("a4print");                    
                    //xn.AppendChild(xe);
                    //xd.Save(Application.StartupPath + "\\options.xml");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("A4ReceiptPrint_Load" + ee.Message);
            }

        }

        private void A4ReceiptPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            //写4个值
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\options.xml");
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("a4print");
                XmlElement xe;
                if (xn == null)
                {
                    xe = xd.CreateElement("a4print");
                    xe.InnerText = "";
                    xn = xd.SelectSingleNode("eg");
                    xn.AppendChild(xe);
                    xn = xn.SelectSingleNode("a4print");
                }
                //else
                {
                    if (xn.SelectSingleNode("agentaddr") == null)
                    {

                        xe = xd.CreateElement("agentaddr");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }
                    if (xn.SelectSingleNode("agentname") == null)
                    {
                        xe = xd.CreateElement("agentname");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }

                    if (xn.SelectSingleNode("agentphone") == null)
                    {
                        xe = xd.CreateElement("agentphone");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }
                    if (xn.SelectSingleNode("paymethod") == null)
                    {
                        xe = xd.CreateElement("paymethod");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }
                    if (xn.SelectSingleNode("logo") == null)
                    {
                        xe = xd.CreateElement("logo");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }
                }
                xd.SelectSingleNode("eg").SelectSingleNode("a4print").SelectSingleNode("agentaddr").InnerText = this.tbAgentAddr.Text;
                xd.SelectSingleNode("eg").SelectSingleNode("a4print").SelectSingleNode("agentname").InnerText = this.tbAgentName.Text;
                xd.SelectSingleNode("eg").SelectSingleNode("a4print").SelectSingleNode("agentphone").InnerText = this.tbAgentPhone.Text;
                xd.SelectSingleNode("eg").SelectSingleNode("a4print").SelectSingleNode("paymethod").InnerText = this.tbPayMethod.Text;
                xd.SelectSingleNode("eg").SelectSingleNode("a4print").SelectSingleNode("logo").InnerText = this.comboBox1.Text;

                xd.Save(Application.StartupPath + "\\options.xml");
            }
            catch (Exception ee)
            {
                MessageBox.Show("A4ReceiptPrint_FormClosing" + ee.Message);
            }
        }
    }
}