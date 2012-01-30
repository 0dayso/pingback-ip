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
    public partial class Pacific : ePlus.PrintHyx.Insurance
    {
        public Pacific()
        {
            InitializeComponent();
            try
            {
                LogoPicture.pictures pic = new LogoPicture.pictures();
                this.imgLogo.Image = pic.pictureBox8.Image;
                lb��˾����.Text = "�й�̫ƽ��Ʋ����չɷ����޹�˾";
                lb��������.Text = "�����ÿ����������˺����ձ��յ�";
                this.Text = "��ӡ ̫ƽ�󣭺��������˺�����";
            }
            catch
            {
            }


            this.xmlname = "Pacific";
            if (!checkxmlfile()) Dispose();
            this.insuranceType = "B0A";//B09 Eagle���տ�
            this.insuranceNumberLength = 10;
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
            xn.InnerText = "";//δ���õ��ӱ�����ͷ��
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
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            throw new Exception("Unknown Error!");
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

                ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(161.5F * scale, 25.9F * scale));
                ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(161.5F * scale, 33.5F * scale));
                ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(40F * scale, 39.8F * scale));
                ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(137.4F * scale, 39.8F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(40F * scale, 47.5F * scale));
                ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(137.4F * scale, 47.5F * scale));
                ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(36.8F * scale, 55.8F * scale));
                ptString.Add(this.tb���ս���д.Text); ptPoint.Add(new PointF(70.1F * scale, 55.8F * scale));
                ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(117.8F * scale, 55.8F * scale));
                ptString.Add(this.tb���շѴ�д.Text); ptPoint.Add(new PointF(157.8F * scale, 55.8F * scale));


                ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(144.7F * scale, 63.1F * scale));
                ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(36.8F * scale, 69.4F * scale));//����
                ptString.Add(this.tb�����˵�ַ.Text); ptPoint.Add(new PointF(36.8F * scale, 75.3F * scale));
                ptString.Add(this.tb�������ʱ�.Text); ptPoint.Add(new PointF(117.8F * scale, 75.3F * scale));
                ptString.Add(this.tb�����绰.Text); ptPoint.Add(new PointF(36.8F * scale, 82.1F * scale));


                //ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(138.4F * scale, (82.1F) * scale));

                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
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
                    hs.Remark = insuranceType; //���������������B0A-̫ƽ������
                    hs.IssuePeriod = "";
                    hs.IssueBegin = (bhyx ? tb�˻���.Text : dtp������ʼʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.IssueEnd = (bhyx ? tb�˻���.Text : dtp������ֹʱ��.Value.ToString());//����Ϊʱ�䴮
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tb����������.Text;// +tb�����˹�ϵ.Text;
                    hs.Signature =tb�����绰.Text + tb���λ.Text + tb������.Text;// tbSignatureDate.Text;
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

                        ptString.Add(hs.eNumber); ptPoint.Add(new PointF(161.5F * scale, 25.9F * scale));
                        ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(161.5F * scale, 33.5F * scale));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(40F * scale, 39.8F * scale));//����
                        ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(137.4F * scale, 39.8F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(40F * scale, 47.5F * scale));
                        ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(137.4F * scale, 47.5F * scale));
                        ptString.Add(this.tb���ս��.Text); ptPoint.Add(new PointF(36.8F * scale, 55.8F * scale));
                        ptString.Add(this.tb���ս���д.Text); ptPoint.Add(new PointF(70.1F * scale, 55.8F * scale));
                        ptString.Add(this.tb���շ�.Text); ptPoint.Add(new PointF(117.8F * scale, 55.8F * scale));
                        ptString.Add(this.tb���շѴ�д.Text); ptPoint.Add(new PointF(157.8F * scale, 55.8F * scale));


                        ptString.Add(this.tb�����˹�ϵ.Text); ptPoint.Add(new PointF(144.7F * scale, 63.1F * scale));
                        ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(36.8F * scale, 69.4F * scale));//����
                        ptString.Add(this.tb�����˵�ַ.Text); ptPoint.Add(new PointF(36.8F * scale, 75.3F * scale));
                        ptString.Add(this.tb�������ʱ�.Text); ptPoint.Add(new PointF(117.8F * scale, 75.3F * scale));
                        ptString.Add(this.tb�����绰.Text); ptPoint.Add(new PointF(36.8F * scale, 82.1F * scale));


                        //ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(138.4F * scale, (82.1F) * scale));


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
    }
}

