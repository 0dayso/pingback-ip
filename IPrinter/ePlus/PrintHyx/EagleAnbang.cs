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
    public partial class EagleAnbang : ePlus.PrintHyx.Insurance
    {
        public EagleAnbang()
        {
            InitializeComponent();
            this.xmlname = "EagleAnbang";                   //�޸�1
            if (!checkxmlfile()) Dispose();
            this.insuranceType = "B0B";//B0B ��������ͨ     //�޸�2
            this.insuranceNumberLength = 8;                 //�޸�3
            this.paperHeight = 372;                         //�޸�4
            this.paperWidth = 827;                          //�޸�5
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
            xn.InnerText = "AnBang";//δ���õ��ӱ�����ͷ��  //�޸�6
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
                ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(48.5F * scale, 35.5F * scale));
                ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(117.9F * scale, 35.5F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(45.7F * scale, 43.7F * scale));
                ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(111.3F * scale, 43.7F * scale));
                ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(45.7F * scale, 59.9F * scale));
                ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(166.6F * scale, 74.2F * scale));
                ptString.Add(this.tb������ .Text); ptPoint.Add(new PointF(35.1F * scale, 83.3F * scale));

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
                        ptString.Add(this.cb������������.Text); ptPoint.Add(new PointF(48.5F * scale, 35.5F * scale));
                        //ptString.Add(this.tb֤����.Text); ptPoint.Add(new PointF(117.9F * scale, 35.5F * scale));
                        //ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(45.7F * scale, 43.7F * scale));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(117.9F * scale, 35.5F * scale));
                        ptString.Add(ls[iPage].Split('~')[1]); ptPoint.Add(new PointF(45.7F * scale, 43.7F * scale));

                        ptString.Add(this.tb�˻���.Text); ptPoint.Add(new PointF(111.3F * scale, 43.7F * scale));
                        ptString.Add(this.tb����������.Text); ptPoint.Add(new PointF(45.7F * scale, 59.9F * scale));
                        ptString.Add(this.tb�����.Text); ptPoint.Add(new PointF(166.6F * scale, 74.2F * scale));
                        ptString.Add(this.tb������.Text); ptPoint.Add(new PointF(35.1F * scale, 83.3F * scale));

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string tele = textBox2.Text.Trim();
            bool bNumber = false;
            try
            {
                long l = long.Parse(tele);
                bNumber = true;
            }
            catch
            {
                bNumber = false;
            }
            if (tele.Length == 11 && bNumber)
            {
                if (tele[0] == '1')
                {
                    if (tele[1] == '3' || tele[1] == '5')
                    {
                        
                        checkBox1.Checked = true;
                        return;
                    }
                }
            }
            checkBox1.Checked = false;
        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //���أ����ӷ��Ͷ��Ź���
            //logic.mysms ms = new logic.mysms();
            //if (checkBox1.Checked)
            //{
            //    string tele = textBox2.Text;
            //    string text = "�𾴵�"+cb������������.Text +"�������һ�ݰ���Ʋ����չ�˾�б��Ľ�ͨ������һ�ݣ�(�����������)";
            //    ms.SendSms(tele, text);
            //    textBox2.Text = "";
            //}
        }


    }
}

