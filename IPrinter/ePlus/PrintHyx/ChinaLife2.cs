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
    public partial class ChinaLife2 : ePlus.PrintHyx.Insurance
    {
        //2008��7��8�� claw
        public ChinaLife2()
        {
            InitializeComponent();
            this.xmlname = "ChinaLife2";//***********************
            if (!checkxmlfile()) Dispose();
            this.insuranceType = "B0C";//�������ʹ���,��md.cs******************
            this.insuranceNumberLength = 16;
            this.paperHeight = 400;
            this.paperWidth = 949;
            this.bhyx = true;//1�챣��Ϊfalse.����Ϊtrue***********************
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
            //if (GlobalVar2.bTempus) lb��������.Text = "�����Ա��Ϣ��";
        }

        private void ChinaLife2_Load(object sender, EventArgs e)
        {

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
            float scale = 1F;
            if (!bLianxu)
            {

                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();
                ptString.Add(this.tb��������.Text); ptPoint.Add(new PointF(49.3F * scale, 31.4F * scale));
                ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(65.3F * scale, 42.9F * scale));
                ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(116.3F * scale, 42.9F * scale));
                ptString.Add(this.tb�����.Text +"  "+ this.tb�˻���.Text); ptPoint.Add(new PointF(111.3F * scale, 57.9F * scale));
                //ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(111.3F * scale, 43.7F * scale));
                ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(118.9F * scale, 82.0F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(175.3F * scale, 92.3F * scale));
                ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(175.3F * scale, 98.0F * scale));

                /*��������������48.5,35.3
���֤��/���պ�:117.9,35.3
�����:45.7,43.7
�˻���:111.3,43.7
����������:45.7,59.9
��ӡ����:166.6,74.2
������:3.51,83.3*/
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
                        ptString.Add(hs.eNumber); ptPoint.Add(new PointF(49.3F * scale, 31.4F * scale));
                        ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(65.3F * scale, 42.9F * scale));
                        //ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(117.9F * scale, 35.5F * scale));
                        //ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(45.7F * scale, 43.7F * scale));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(116.3F * scale, 42.9F * scale));
                        ptString.Add(ls[iPage].Split('~')[1]); ptPoint.Add(new PointF(111.3F * scale, 57.9F * scale));

                        //ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(111.3F * scale, 43.7F * scale));
                        ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(118.9F * scale, 82.0F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(175.3F * scale, 92.3F * scale));
                        ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(175.3F * scale, 98.0F * scale));

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

