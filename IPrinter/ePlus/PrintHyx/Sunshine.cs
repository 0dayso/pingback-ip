using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ePlus.PrintHyx
{
    public partial class Sunshine : ePlus.PrintHyx.Insurance
    {
        
        public Sunshine()
        {
            InitializeComponent();
            this.xmlname = "SunShine";
            if (!checkxmlfile()) Dispose();
            this.insuranceType = "B07";//B07 ����
            this.insuranceNumberLength = 10;
            this.paperHeight = 400;
            this.paperWidth = 932;
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
            xn.InnerText = "0130705";
            xd.Save(GlobalVar.s_configfile);
            //


            this.Text = "������մ�ӡ";
            cfg_insurance cc = new cfg_insurance();
            cc.GetConfig(this.xmlname);
            //013070508
            this.eNumberHead = cc.ENumberHead + EagleAPI.GetRandom01(DateTime.Now.Minute) + EagleAPI.GetRandom01(DateTime.Now.Second) + EagleAPI.GetRandom01(DateTime.Now.Millisecond);
            //this.eNumberHead = "013070508" + EagleAPI.GetRandom01();

            this.tb������.Text = cc.Signature;
            this.numericUpDown1.Value = cc.OffsetX;
            this.numericUpDown2.Value = cc.OffsetY;
            this.tb�������.Text = cc.SaveNo;
            this.tb�����绰.Text = cc.Phone;
            this.tb���λ.Text = cc.CompanyAddr;

            tb�����.Text = System.DateTime.Now.ToString();
            this.cb��������.Text = "���⽻ͨ���߳˿������˺���������";
            timer1.Start();



            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            this.ActiveControl = this.tbPnr;
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
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
            float scale = 2360F / 2466F;
            if (!bLianxu)
            {
                
                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();
                ptString.Add(this.cb��������.Text); ptPoint.Add(new PointF(59.7F * scale, 20.6F * scale));
                ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(164.9F * scale, 20.6F * scale));
                ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(59.7F * scale, 27.5F * scale));
                ptString.Add(this.cb֤������.Text + this.tb֤����.Text); ptPoint.Add(new PointF(148.2F * scale, 27.5F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(59.7F * scale, 34.7F * scale));
                ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(131.2F * scale, 34.7F * scale));
                ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(40.7F * scale, 50.4F * scale));
                ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(131.2F * scale, 41.5F * scale));
                ptString.Add(this.dtp������ʼʱ��.Value.ToShortDateString() + " " + tb������ʼʱ��.Text); ptPoint.Add(new PointF(49.5F * scale, 72.9F * scale));
                ptString.Add(this.dtp������ֹʱ��.Value.ToShortDateString() + " " + tb������ֹʱ��.Text); ptPoint.Add(new PointF(120.0F * scale, 72.9F * scale)); 
                ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(105.3F * scale, 80.5F * scale));
                ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(83.3F * scale, 85.7F * scale));
                ptString.Add(this.tb���λ.Text); ptPoint.Add(new PointF(46.9F * scale, 93.9F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(126.2F * scale, 93.9F * scale));
                ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(187.2F * scale, 93.9F * scale));


                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
            }
            else
            {
                if (ls == null || ls.Count == 0) return;
                //for (int i = 0; i < ls.Count; i++)
                bool bSubmited = false;
                {
                    
                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = eNumberHead + EagleAPI.GetRandom62(); //this.tb��������.Text;
                    hs.IssueNumber = ls[iPage].Split('~')[2];
                    hs.NameIssued = ls[iPage].Split('~')[0];
                    hs.CardType = "�����" + tb�����.Text + "�˻���" + tb�˻���.Text;
                    hs.CardNumber = ls[iPage].Split('~')[1];
                    hs.Remark = insuranceType; //���������������B07
                    hs.IssuePeriod = "";
                    hs.IssueBegin = (bhyx ? tb�˻���.Text : dtp������ʼʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.IssueEnd = (bhyx ? tb�˻���.Text : dtp������ֹʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tb����������.Text + tb�����˹�ϵ.Text;
                    hs.Signature = tb������.Text;// tbSignatureDate.Text;
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
                        //ptString.Add(this.tb�������.Text); ptPoint.Add(new PointF(185F, 31F));
                        //ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(57.1F, 39F));
                        //ptString.Add(ls[iPage].Split('~')[1]); ptPoint.Add(new PointF(155F, 39F));
                        //ptString.Add(tb�����.Text); ptPoint.Add(new PointF(57.1F, 50F));
                        //ptString.Add(tb�˻���.Text); ptPoint.Add(new PointF(155F, 50F));
                        //ptString.Add(tb�����.Text); ptPoint.Add(new PointF(131.8F, 72.4F));
                        //ptString.Add(tb�����.Text); ptPoint.Add(new PointF(47F, 72.4F));
                        ////ptString.Add(tbPhone.Text); ptPoint.Add(new PointF(44F, 90F));
                        //ptString.Add(tb�����.Value.ToShortDateString()); ptPoint.Add(new PointF(106.4F, 84.3F));
                        //ptString.Add(tb�����.Text); ptPoint.Add(new PointF(183.1F, 84.3F));
                        ptString.Add(this.cb��������.Text); ptPoint.Add(new PointF(59.7F * scale, 20.6F * scale));
                        ptString.Add(hs.eNumber); ptPoint.Add(new PointF(164.9F * scale, 20.6F * scale));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(59.7F * scale, 27.5F * scale));
                        ptString.Add(this.cb֤������.Text + ls[iPage].Split('~')[1]); ptPoint.Add(new PointF(148.2F * scale, 27.5F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(59.7F * scale, 34.7F * scale));
                        ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(131.2F * scale, 34.7F * scale));
                        ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(40.7F * scale, 50.4F * scale));
                        ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(131.2F * scale, 41.5F * scale));
                        ptString.Add(this.dtp������ʼʱ��.Value.ToShortDateString() + " " + tb������ʼʱ��.Text); ptPoint.Add(new PointF(49.5F * scale, 72.9F * scale));
                        ptString.Add(this.dtp������ֹʱ��.Value.ToShortDateString() + " " + tb������ֹʱ��.Text); ptPoint.Add(new PointF(120.0F * scale, 72.9F * scale));
                        ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(105.3F * scale, 80.5F * scale));
                        ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(83.3F * scale, 85.7F * scale));
                        ptString.Add(this.tb���λ.Text); ptPoint.Add(new PointF(46.9F * scale, 93.9F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(126.2F * scale, 93.9F * scale));
                        ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(187.2F * scale, 93.9F * scale));

                        PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
                    }
                    iPage++;
                    if (iPage < ls.Count)
                        e.HasMorePages = true;
                    else
                        e.HasMorePages = false;
                }

            }
        }

        private void dtp������ʼʱ��_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtp������ֹʱ��.Value = dtp������ʼʱ��.Value.AddDays(7);
                dtp������ֹʱ��.Value = dtp������ֹʱ��.Value.AddSeconds(-1);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Sunshine_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            LogoPicture.pictures pic = new LogoPicture.pictures();
            this.imgLogo.Image = pic.pictureBox6.Image;
            try
            {
                cb֤������.Text = "���֤";
                DateTime dt = System.DateTime.Now;
                dtp������ʼʱ��.Value = dt;
                dtp������ֹʱ��.Value = dtp������ʼʱ��.Value.AddDays(7);
                dtp������ֹʱ��.Value = dtp������ֹʱ��.Value.AddSeconds(-1);
                tb������ʼʱ��.Text = dtp������ʼʱ��.Value.ToLongTimeString();
                tb������ֹʱ��.Text = dtp������ֹʱ��.Value.ToLongTimeString();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            if (this.tb�����绰.Text.Trim() == "82424242") tb�����绰.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dtp������ʼʱ��.Value.ToLongDateString() == System.DateTime.Now.ToLongDateString())
            {
                this.tb������ʼʱ��.Text = System.DateTime.Now.ToLongTimeString();
                this.tb������ֹʱ��.Text = System.DateTime.Now.AddSeconds(-1).ToLongTimeString();

            }
            else
            {
                this.tb������ʼʱ��.Text = "00:00:00";
                this.tb������ֹʱ��.Text = "23:59:59";
                dtp������ֹʱ��.Value = dtp������ʼʱ��.Value.AddDays(6);
            }
            this.tb�˻���.Text = this.tb�����.Text = "----------";
            this.tb�����.Text = System.DateTime.Now.ToString();
        }
    }
}

