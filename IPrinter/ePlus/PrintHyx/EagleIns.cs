using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
//HBSX070000000001
namespace ePlus.PrintHyx
{
    public partial class EagleIns : ePlus.PrintHyx.Insurance
    {
        public int EalgeBxtype = 1;//0:�»� 1:PICC
        public EagleIns()
        {
            InitializeComponent();
            GlobalVar2.bxTelephone = textBox1.Text;
            switch (this.EalgeBxtype)
            {
                case 0:
                    this.xmlname = "EagleIns";
                    this.insuranceType = "B09";//B09 Eagle���տ��»�
                    this.insuranceNumberLength = 12;
                    break;
                case 1:
                    this.xmlname = "EagleIns2";
                    this.insuranceType = "B0D";//B0D Eagle���տ�PICC
                    this.insuranceNumberLength = 12;
                    break;
            }
            
            if (!checkxmlfile()) Dispose();
            
            
            this.paperHeight = 400;
            this.paperWidth = 949;
            this.bhyx = false;
            //����enumberHead
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("ENumberHead");
            xn.InnerText = "EAGLE";//δ���õ��ӱ�����ͷ��
            xd.Save(GlobalVar.s_configfile);
            //
            cfg_insurance cc = new cfg_insurance();
            cc.GetConfig(this.xmlname);
            this.eNumberHead = cc.ENumberHead;
            //+ EagleAPI.GetRandom01(DateTime.Now.Minute) 
            //+ EagleAPI.GetRandom01(DateTime.Now.Second) 
            //+ EagleAPI.GetRandom01(DateTime.Now.Millisecond);

            this.tb������.Text = cc.Signature;
            this.numericUpDown1.Value = cc.OffsetX;
            this.numericUpDown2.Value = cc.OffsetY;
            this.tb�������.Text = cc.SaveNo;
            tb�����.Text = System.DateTime.Now.ToShortDateString();

            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            this.ActiveControl = this.tbPnr;
            if (GlobalVar2.bTempus) lb��������.Text = "�����Ա��Ϣ��";
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            switch (this.EalgeBxtype)
            {
                case 0:
                    ptDoc_PrintPage_�»�(sender, e);
                    break;
                case 1:
                    ptDoc_PrintPage_PICC(sender, e);
                    break;
            }
        }
        /// <summary>
        /// ����:65.1,26.7 / ֤��:159.2,26.7 / �绰:65.1,36.2 / ΢����:159.2,36.2 / ��Ա��65.1,45.5 / ����(���࣫����):159.2,45.5
        /// ���:65.1,64.2 / ��Ч:159.2,64.2 / ���죫����:65.1,73.5 / �б���:159.2,73.5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptDoc_PrintPage_PICC(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("system", 11, System.Drawing.FontStyle.Regular);
            //Font ptFontCn = new Font("tec", EagleAPI.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();
            o.X = float.Parse(numericUpDown1.Value.ToString());
            o.Y = float.Parse(numericUpDown2.Value.ToString());
            //��ӡ��
            HyxStructs hs = new HyxStructs();
            float scale = 1F;
            if (!bLianxu)//���Ŵ�ӡ
            {

                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();
                ptString.Add("PICC�б����ա������������ϳб���֪��"); ptPoint.Add(new PointF(159.2F * scale, 6.7F * scale));
                ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(65.1F * scale, 26.7F * scale));
                ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(159.2F * scale, 26.7F * scale));
                ptString.Add(this.textBox1.Text); ptPoint.Add(new PointF(65.1F * scale, 36.2F * scale));//�绰����
                ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(159.2F * scale, 36.2F * scale));//΢����
                ptString.Add(this.cbPrice.Text); ptPoint.Add(new PointF(65.1F * scale, 45.5F * scale));//��Ա��


                ptString.Add(this.tb�����.Text + "  " + this.tb�˻���.Text); ptPoint.Add(new PointF(159.2F * scale, 45.5F * scale));
                ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(65.1F * scale, 64.2F * scale));//
                ptString.Add("7������Ч,�޵��κ���"); ptPoint.Add(new PointF(129.2F * scale, 64.2F* scale));//
                ptString.Add(this.tb������.Text + "  " + this.tb�����.Text); ptPoint.Add(new PointF(65.1F * scale, 73.3F * scale));

                //ptString.Add(this.tb�������.Text); ptPoint.Add(new PointF(159.2F * scale, 73.3F * scale));
                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);

            }
            else//������ӡ
            {
                MessageBox.Show("��֧��������ӡ");//�����HS���ύ�ĺ�̨����ϵͳ�������׸���ϵͳ
                return;
                if (ls == null || ls.Count == 0) return;
                //for (int i = 0; i < ls.Count; i++)
                bool bSubmited = false;
                {

                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = eNumberHead + DateTime.Now.Date.ToString("yyyyMMdd") + ls[iPage].Split('~')[2]; //this.tb��������.Text;
                    hs.IssueNumber = ls[iPage].Split('~')[2];
                    hs.NameIssued = ls[iPage].Split('~')[0];
                    hs.CardType = "�����" + tb�����.Text + "�˻���" + tb�˻���.Text;
                    hs.CardNumber = ls[iPage].Split('~')[1];
                    hs.Remark = insuranceType; //���������������B09-�׸��Ա��
                    hs.IssuePeriod = "";
                    hs.IssueBegin = (bhyx ? tb�˻���.Text : dtp������ʼʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.IssueEnd = (bhyx ? tb�˻���.Text : dtp������ֹʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tb����������.Text;// +tb�����˹�ϵ.Text;
                    hs.Signature = tb���λ.Text + tb������.Text;// tbSignatureDate.Text;
                    hs.SignDate = tb�����.Text;//dtp_Date.Value.ToShortDateString();
                    hs.InssuerName = "";
                    hs.Pnr = tbPnr.Text;
                    if (!hs.SubmitInfo())
                    {
                        MessageBox.Show(ls[iPage].Replace("~", "��") + "�ύʧ�ܣ�");
                        bSubmited = false;
                    }
                    else
                    {
                        bSubmited = true;
                    }
                }
                {
                    if (bSubmited)
                    {
                        List<string> ptString = new List<string>();
                        List<PointF> ptPoint = new List<PointF>();

                        ptString.Add(hs.eNumber); ptPoint.Add(new PointF(60.3F * scale, 29.2F * scale));

                        ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(148.2F * scale, 37.7F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(60.3F * scale, 46.8F * scale));
                        ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(148.2F * scale, 46.8F * scale));


                        ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(92.6F * scale, 67.5F * scale));
                        ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(92.6F * scale, 74.6F * scale));
                        ptString.Add(this.textBox1.Text); ptPoint.Add(new PointF(128.3F * scale, 67.5F * scale));
                        ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(167.6F * scale, (88.8F) * scale));

                        ptString.Add("HBSX" + this.tb�������.Text); ptPoint.Add(new PointF(137.8F * scale, 28.4F * scale));
                        PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);

                        ptString.Clear(); ptPoint.Clear();
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(60.3F * scale, 37.7F * scale));
                        ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(60.3F * scale, 55.5F * scale));
                        ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(148.2F * scale, 55.5F * scale));
                        PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, new Font("System", 10.5F), ptBrush, e);

                    }
                    iPage++;
                    if (iPage < ls.Count)
                        e.HasMorePages = true;
                    else
                        e.HasMorePages = false;
                }

            }
        }
        private void ptDoc_PrintPage_�»�(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            getTitleOffset();
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("system", 9, System.Drawing.FontStyle.Regular);
            //Font ptFontCn = new Font("tec", EagleAPI.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = new PointF();    
            o.X = float.Parse(numericUpDown1.Value.ToString());
            o.Y = float.Parse(numericUpDown2.Value.ToString());
            //��ӡ��
            HyxStructs hs = new HyxStructs();
            float scale = 1F;
            if (!bLianxu)
            {

                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();
                //�»����ٱ��չɷ����޹�˾�����˺����ճб���֪��
                //�����������˺�����һ�ݣ����ղ�ѯ���½http://www.eg66.com

                ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(60.3F * scale, 29.2F * scale));
                
                ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(148.2F * scale, 37.7F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(60.3F * scale, 46.8F * scale));
                ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(148.2F * scale, 46.8F * scale));

                
                ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(92.6F * scale, 67.5F * scale));
                ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(92.6F * scale, 74.6F * scale));
                ptString.Add(this.textBox1.Text); ptPoint.Add(new PointF(128.3F * scale, 67.5F * scale));//�绰����
                ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(167.6F * scale, (88.8F) * scale));
                if (GlobalVar2.bTempus)
                {
                    ptString.Add("�����������˺�����һ�ݣ����ղ�ѯ���½HTTP://WWW.EG66.COM");
                    ptPoint.Add(new PointF(32.0F * scale, 83.9F * scale));
                }
                else
                {
                    ptString.Add("�����������˺�����һ�ݣ����ղ�ѯ���½HTTP://WWW.EG66.COM");
                    ptPoint.Add(new PointF(32.0F * scale, 83.9F * scale));
                }
                //ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(133.7F * scale, (74.5F) * scale));
                //ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(51.1F * scale, (79.9F + 5F) * scale));

                ptString.Add(this.tb�������.Text); ptPoint.Add(new PointF(137.8F * scale, 28.4F * scale));
                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);

                ptString.Clear(); ptPoint.Clear();
                ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(60.3F * scale, 37.7F * scale));
                ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(60.3F * scale, 55.5F * scale));
                ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(148.2F * scale, 55.5F * scale));
                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, new Font("System", 10.5F), ptBrush, e);
                if (cbPrintTitle.Checked)
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    e.Graphics.DrawString(this.lb��˾����.Text, new Font("System", 15), Brushes.Black,
                        new RectangleF(15.5F + titleOffset.X, 4.5F + titleOffset.Y, 209.9F + titleOffset.X, 9.2F + titleOffset.Y), sf);
                }
            }
            else
            {
                if (ls == null || ls.Count == 0) return;
                //for (int i = 0; i < ls.Count; i++)
                bool bSubmited = false;
                {

                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = eNumberHead + DateTime.Now.Date.ToString("yyyyMMdd") + ls[iPage].Split('~')[2]; //this.tb��������.Text;
                    hs.IssueNumber = ls[iPage].Split('~')[2];
                    hs.NameIssued = ls[iPage].Split('~')[0];
                    hs.CardType = "�����" + tb�����.Text + "�˻���" + tb�˻���.Text;
                    hs.CardNumber = ls[iPage].Split('~')[1];
                    hs.Remark = insuranceType; //���������������B09-�׸��Ա��
                    hs.IssuePeriod = "";
                    hs.IssueBegin = (bhyx ? tb�˻���.Text : dtp������ʼʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.IssueEnd = (bhyx ? tb�˻���.Text : dtp������ֹʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tb����������.Text;// +tb�����˹�ϵ.Text;
                    hs.Signature = tb���λ.Text + tb������.Text;// tbSignatureDate.Text;
                    hs.SignDate = tb�����.Text;//dtp_Date.Value.ToShortDateString();
                    hs.InssuerName = "";
                    hs.Pnr = tbPnr.Text;
                    if (!hs.SubmitInfo())
                    {
                        MessageBox.Show(ls[iPage].Replace("~", "��") + "�ύʧ�ܣ�");
                        bSubmited = false;
                    }
                    else
                    {
                        bSubmited = true;
                    }
                }
                {
                    if (bSubmited)
                    {
                        List<string> ptString = new List<string>();
                        List<PointF> ptPoint = new List<PointF>();

                        ptString.Add(hs.eNumber); ptPoint.Add(new PointF(60.3F * scale, 29.2F * scale));
                        
                        ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(148.2F * scale, 37.7F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(60.3F * scale, 46.8F * scale));
                        ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(148.2F * scale, 46.8F * scale));

                        
                        ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(92.6F * scale, 67.5F * scale));
                        ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(92.6F * scale, 74.6F * scale));
                        ptString.Add(this.textBox1.Text); ptPoint.Add(new PointF(128.3F * scale, 67.5F * scale));
                        ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(167.6F * scale, (88.8F) * scale));
                        if (GlobalVar2.bTempus)
                        {
                            ptString.Add("�����������˺�����һ�ݣ����ղ�ѯ���½HTTP://WWW.EG66.COM");
                            ptPoint.Add(new PointF(32.0F * scale, 83.9F * scale));
                        }
                        else
                        {
                            ptString.Add("�����������˺�����һ�ݣ����ղ�ѯ���½HTTP://WWW.EG66.COM");
                            ptPoint.Add(new PointF(32.0F * scale, 83.9F * scale));
                        }
                        ptString.Add("HBSX" + this.tb�������.Text); ptPoint.Add(new PointF(137.8F * scale, 28.4F * scale));
                        PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);

                        ptString.Clear(); ptPoint.Clear();
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(60.3F * scale, 37.7F * scale));
                        ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(60.3F * scale, 55.5F * scale));
                        ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(148.2F * scale, 55.5F * scale));
                        PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, new Font("System", 10.5F), ptBrush, e);
                        if (cbPrintTitle.Checked)
                        {
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                            e.Graphics.DrawString(this.lb��˾����.Text, new Font("System", 15), Brushes.Black,
                        new RectangleF(15.5F + titleOffset.X, 4.5F + titleOffset.Y, 209.9F + titleOffset.X, 9.2F + titleOffset.Y), sf);
                        }
                    }
                    iPage++;
                    if (iPage < ls.Count)
                        e.HasMorePages = true;
                    else
                        e.HasMorePages = false;
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GlobalVar2.bxTelephone = textBox1.Text;
        }

        private void bt_Print_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("");
            Insurance.context.tb��������.Text = this.tb��������.Text;
            Insurance.context.tb��������.Text = this.tb��������.Text;
            Insurance.context.tb�������.Text = this.tb�������.Text;
            Insurance.context.tb���շ�.Text = this.tb���շ�.Text;
            Insurance.context.tb���ս��.Text = this.tb���ս��.Text;
            Insurance.context.tb�˻���.Text = this.tb�˻���.Text;
            Insurance.context.tb�����.Text = this.tb�����.Text;
            Insurance.context.tb������.Text = this.tb������.Text;
            Insurance.context.tb�����˹�ϵ.Text = this.tb�����˹�ϵ.Text;
            Insurance.context.tb����������.Text = this.tb����������.Text;
            Insurance.context.tb֤����.Text = this.tb֤����.Text;

        }

        private void bt���ϱ���_Click(object sender, EventArgs e)
        {
            string url = "http://hbpiao.3322.org:6000/IA/Default.aspx";
            if (GlobalVar2.gbConnectType != 1)
                url = "http://hbpiao.3322.org:6000/IA/Default.aspx";
            try
            {
                PrintTicket.RunProgram("C:\\Program Files\\Internet Explorer\\IEXPLORE.EXE", 
                    url);
            }
            catch
            {
                try
                {
                    PrintTicket.RunProgram("D:\\Program Files\\Internet Explorer\\IEXPLORE.EXE", 
                        url);
                }
                catch
                {
                }
            }
        }
        //����վ��ť
        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://hbpiao.3322.org:6000/IA/Default.aspx";
            if (GlobalVar2.gbConnectType != 1)
                url = "http://hbpiao.3322.org:6000/IA/Default.aspx";

            try
            {
                PrintTicket.RunProgram("C:\\Program Files\\Internet Explorer\\IEXPLORE.EXE",
                    url);
            }
            catch
            {
                try
                {
                    PrintTicket.RunProgram("D:\\Program Files\\Internet Explorer\\IEXPLORE.EXE",
                        url);
                }
                catch
                {
                }
            }
        }

        private void btSetTitleOffset_Click(object sender, EventArgs e)
        {
            PrintTicket.RunProgram(Environment.SystemDirectory + "\\notepad.exe", Application.StartupPath + "\\EagleIns.ini");
        }
        PointF titleOffset = new PointF();
        void getTitleOffset()
        {
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\EagleIns.ini", FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    line = line.Split(';')[0].Trim();
                    string left = line.Split('=')[0].Trim();
                    if (left == "EAGLEINS_TITLE_OFFSET_X")
                    {
                        try
                        {
                            titleOffset.X = float.Parse(line.Split('=')[1].Trim());
                        }
                        catch
                        {
                            titleOffset.X = 0;
                        }
                    }
                    if (left == "EAGLEINS_TITLE_OFFSET_Y")
                    {
                        try
                        {
                            titleOffset.Y = float.Parse(line.Split('=')[1].Trim());
                        }
                        catch
                        {
                            titleOffset.Y = 0;
                        }
                    }
                    if (left == "EAGLEINS_TITLE_PRINT")
                    {
                        try
                        {
                            cbPrintTitle.Checked = (line.Split('=')[1].Trim() == "1");
                        }
                        catch
                        {
                            cbPrintTitle.Checked = true;
                        }
                    }
                }
                sr.Close();
                fs.Close();
 
            }
            catch
            {
                titleOffset.X = 0;
                titleOffset.Y = 0;
                cbPrintTitle.Checked = true;
            }
        }

        private void EagleIns_Load(object sender, EventArgs e)
        {
            //string str = "���������û���\n";
            //str += "    �ҹ�˾���˱����չ�˾���������û�Ա����������\n";
            //str += "Ч�ڴ�2009��3��5�����ҹ�˾���ٷ�����֤������û�\n";
            //str += "��λ��ʱ������ĵ�֤��\n";
            //str += "    �µĵ�֤�ҹ�˾��������ʵ�������ʱ�����Ƴ���\nлл����";
            //MessageBox.Show(str,"��Ҫ֪ͨ");
            switch (this.EalgeBxtype)
            {
                case 0:
                    this.cbPrice.Visible = false;
                    break;
                case 1:
                    this.cbPrice.Visible = true;
                    this.cbPrice.Location = this.tb���շ�.Location;
                    this.tb���շ�.Visible = false;
                    this.tb���ս��.Text = "���ɻ���40��Ԫ\r\nRMB(Aircraft400000)";
                    this.lb��˾����.Text = "PICC�й�����Ʋ����չɷ����޹�˾";
                    this.cbPrintTitle.Checked = false;
                    this.cbPrintTitle.Visible = false;
                    this.btSetTitleOffset.Visible = false;
                    this.cbPrice.SelectedIndex = 1;
                    this.panel1.BackColor = Color.PaleGreen;
                    this.Text = "�׸����û�Ա����ӡ";
                    break;
            }
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\EagleIns.ini", FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf("EAGLEINS_TITLE_PRINT") >= 0)
                    {
                        cbPrintTitle.Checked = (line.Split('=')[1].Trim() == "1");
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch
            {
            }
        }

        private void cbPrintTitle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool bExist = false;
                List<string> ls = new List<string>();
                FileStream fs = new FileStream(Application.StartupPath + "\\EagleIns.ini", FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf("EAGLEINS_TITLE_PRINT") >= 0)
                    {
                        line = line.Replace("1", "0").Replace("0", cbPrintTitle.Checked ? "1" : "0");
                        bExist = true;
                    }
                    ls.Add(line);
                }
                sr.Close();
                fs.Close();
                if (!bExist) ls.Add("EAGLEINS_TITLE_PRINT=" + (cbPrintTitle.Checked ? "1" : "0"));
                fs = new FileStream(Application.StartupPath + "\\EagleIns.ini", FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                for (int i = 0; i < ls.Count; i++)
                {
                    sw.WriteLine(ls[i]);
                }
                sw.Close();
                fs.Close();
            }
            catch
            {
            }
        }

        private void tb�������_KeyUp(object sender, KeyEventArgs e)
        {
            string h = this.tb�������.Text.Substring(0, 4);
            if (e.KeyValue == 13)
            {
                string s = this.tb�������.Text.Substring(4);//this.tb�������.Text.ToUpper().Replace(h, "");
                string[] a = s.Split('-');
                int len = a[0].Trim().Length;
                long start=0, end=0;
                if (a.Length == 1)
                {
                    start = end = long.Parse(a[0].Trim());
                }
                else if(a.Length==2)
                {
                    start = long.Parse(a[0].Trim());
                    end = long.Parse(a[0].Trim().Substring(0, a[0].Trim().Length - a[1].Trim().Length) + a[1].Trim());
                    if (end < start)
                    {
                        long temp = start;
                        start = end;
                        end = temp;
                    }
                }
                else 
                {
                    MessageBox.Show("����ȷ���룡��:12345678-89");
                }
                for (long ln = start; ln <= end; ln++)
                {
                    try
                    {
                        this.tb�������.Text = h + ln.ToString("d" + len.ToString());
                        Application.DoEvents();
                        string para = this.tb�������.Text.ToUpper();
                        //if (!para.StartsWith("WHHY")) para = "WHHY" + para;
                        EP.WebService ep = new EP.WebService();
                        string xml = ep.QueryCaseByCase(this.tb�������.Text);
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(xml);
                        XmlNode xn = xd.SelectSingleNode("//NewDataSet/Table");
                        //            xn.SelectSingleNode("caseNo").InnerText;
                        tb��������.Text = xn.SelectSingleNode("caseSerial").InnerText;
                        //            xn.SelectSingleNode("caseOwner").InnerText;
                        //            xn.SelectSingleNode("caseSupplier").InnerText;
                        //            xn.SelectSingleNode("caseRecursive").InnerText;
                        cb������������.Text = xn.SelectSingleNode("customerName").InnerText;
                        //            xn.SelectSingleNode("customerID").InnerText;
                        textBox1.Text = xn.SelectSingleNode("customerPhone").InnerText;
                        tb�����.Text = xn.SelectSingleNode("customerFlightNo").InnerText;
                        tb�˻���.Text = DateTime.Parse(xn.SelectSingleNode("customerFlightDate").InnerText).ToShortDateString();
                        //            xn.SelectSingleNode("beneficiary").InnerText;
                        this.tb�����.Text = DateTime.Parse(xn.SelectSingleNode("datetime").InnerText).ToShortDateString();
                        this.tb֤����.Text = xn.SelectSingleNode("customerID").InnerText;
                        bool b_printed = (xn.SelectSingleNode("isPrinted").InnerText == "true");
                        bool b_plus = (xn.SelectSingleNode("plus").InnerText == "true");
                        bool b_enabled = (xn.SelectSingleNode("enabled").InnerText == "true");
                        if (b_printed)
                            this.ptDoc.Print();
                        else MessageBox.Show("�ú�δʹ�ã�");
                    }
                    catch
                    {
                        MessageBox.Show("��ȡ��Ϣ����");
                    }
                }
            }
            else if (e.KeyValue == 38)//UP
            {
                try
                {
                    string s = tb�������.Text.Trim().Replace(h, "");
                    int len = s.Length;
                    long no = long.Parse(s) + 1;
                    tb�������.Text = no.ToString("d" + len.ToString());
                }
                catch
                {
                }
            }
            else if (e.KeyValue == 40)//down
            {
                try
                {
                    string s = tb�������.Text.Trim().Replace(h, "");
                    int len = s.Length;
                    long no = long.Parse(s) - 1;
                    tb�������.Text = no.ToString("d" + len.ToString());
                }
                catch
                {
                }
            }
        }
    }
}

