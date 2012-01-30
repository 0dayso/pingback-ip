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
        string[] logo = null;//{ "����ͨ", "�人�׸����Ƽ�", "������˳����" };

        Image img = null;
        string toprightString = "�й�BSP";
        string title = "���ӿ�Ʊ�г̵�";
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
        public string paymethod = "";//���ʽ//CA3
        public string restriction = "";

        public string smallPnr = "";
        public string ticketNumber = "";
        public string ��Ʊ = "";
        public string ticketTime = "";
        public string ��Эcode = "";
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
            Font ptFontEn = new Font("����", 12, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            try
            {
                e.Graphics.DrawImage(img, new PointF(35.6F, 24.7F));//1.����img���ļ�λ�ã�����ȫ������xml��
            }
            catch
            {
            }
            e.Graphics.DrawString(toprightString, ptFontEn, ptBrush, new PointF(179.7F, 32.1F));//�й�BSP
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
            e.Graphics.DrawString("���չ�˾��¼��ţ�" + bigPnr + "\r\n"
                                    + "�ÿ�������" + passagerName + "\r\n"
                                    + "���ʶ����룺" + idcardNumber + "\r\n"
                                    + "\r\n"
                                    + "��Ʊ���չ�˾��" + airlineName + "\r\n"
                                    + "\r\n"
                                    + "��Ʊ�����ˣ�" + agentName + "\r\n"
                                    + "�����˵�ַ��" + agentAddress + "\r\n"
                                    + "�绰��" + agentPhone + "\r\n"
                                    + "\r\n"
                                    + "\r\n"
                                    + "\r\n"
                                    //15              10        10        12          12          12          10        4
                                    + "ʼ����/Ŀ�ĵ�  �����  ��λ    ����      ʱ��      ��Чʱ��  ״̬    ����"
                                    + "\r\n"
                                    + "\r\n"
                                    + sailstring
                                    + "\r\n"
                                    + "Ʊ�ۼ��㣺"
                                    + fc + "\r\n"
                                    + "\r\n"
                                    + "���ʽ��" + paymethod + "\r\n"
                                    + "\r\n"
                                    + "��Ʊ�" + fp + "\r\n"
                                    + "�ܶ" + ft + "\r\n"
                                    + "\r\n"
                                    + "����������" + restriction + "\r\n"
                                    + "��֪��" + "\r\n"
                                    + "\r\n"
                                    , ptFontEn, ptBrush, new PointF(35.6F, 77.7F));
            e.Graphics.DrawString("��  �����ں������ǰ1Сʱ�������������������˺��չ�˾�涨��ǰ���������ƾ��Ʊʱʹ��"
                                    + "\r\n����Ч���֤����ֵ����̨��ʱ����˻�������"
                                    + "\r\n��  ��������Ч���֤�����ǻ��Ƽ����г̵�����ȫ���������"
                                    + "\r\n��  �����Ҫ�ṩ����������ϵ��Ʊ�����˻����˺��չ�˾��",
                                    new Font("System", 9, System.Drawing.FontStyle.Regular), ptBrush, new PointF(35.6F, 240.6F));
            e.Graphics.DrawString("������¼��ţ�" + smallPnr + "\r\n"
                                    + "Ʊ�ţ�" + ticketNumber + "\r\n"
                                    + "��Ʊ��" + ��Ʊ + "\r\n"
                                    + "\r\n"
                                    + "��Ʊʱ�䣺" + ticketTime + "\r\n"
                                    + "\r\n"
                                    + "��Э���룺" + ��Эcode + "\r\n"
                                    + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n" + "\r\n"
                                    + "˰�  " + taxBuild + "\r\n" + "        " + taxFuel
                                    , ptFontEn, ptBrush, new PointF(141.7F, 77.7F));
            switch (comboBox1.Text)
            {
                case "�人�׸����Ƽ�":
                    e.Graphics.DrawString("�人�׸����Ƽ�   HTTP://WWW.EG66.COM",
                                            new Font("system", 8, System.Drawing.FontStyle.Underline), ptBrush, new PointF(99.9F, 273.7F));
                    break;
                case "����ͨ":
                    e.Graphics.DrawString("����ͨ   HTTP://WWW.ET-CHINA.COM",
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
                    case "�人�׸����Ƽ�":
                        img = Image.FromFile(Application.StartupPath + "\\A4Print.jpg");
                        break;
                    case "������˳����":
                        img = Image.FromFile(Application.StartupPath + "\\gs.jpg");
                        break;
                    case "����ͨ":
                        try
                        {
                            img = Image.FromFile(Application.StartupPath + "\\etc.jpg");
                        }
                        catch
                        {
                            MessageBox.Show("δ�ҵ�ͼ���ļ����뽫logoͼ�����Ϊetc.jpg�������´򿪴˶Ի������Ч");
                        }
                        break;
                    default:
                        MessageBox.Show("��ѡ��ͼ��");
                        return;
                        break;
                }
            }
            catch
            {
                MessageBox.Show("δ�ҵ�ͼ���ļ����뽫logoͼ�����ΪA4Print.jpg�������´򿪴˶Ի������Ч");
            }
            ptDoc.Print();
        }

        private void A4ReceiptPrint_Load(object sender, EventArgs e)
        {
            //logo = { "����ͨ", "�人�׸����Ƽ�", "������˳����" };
            logo = new string[] { "����ͨ", "�人�׸����Ƽ�", "������˳����" };
            for (int i = 0; i < logo.Length; i++)
            {
                comboBox1.Items.Add(logo[i]);
            }
            //��4��ֵ
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
                    if (this.comboBox1.Text == "����ͨ")
                        this.comboBox1.Text = "�人�׸����Ƽ�";
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
            //д4��ֵ
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