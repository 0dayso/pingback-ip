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
        //2008年7月8日 claw
        public ChinaLife2()
        {
            InitializeComponent();
            this.xmlname = "ChinaLife2";//***********************
            if (!checkxmlfile()) Dispose();
            this.insuranceType = "B0C";//保单类型代码,见md.cs******************
            this.insuranceNumberLength = 16;
            this.paperHeight = 400;
            this.paperWidth = 949;
            this.bhyx = true;//1天保单为false.多天为true***********************
            //保存enumberHead
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("ENumberHead");
            xn.InnerText = "";//未设置电子保单号头部
            xd.Save(GlobalVar.s_configfile);
            //
            cfg_insurance cc = new cfg_insurance();
            cc.GetConfig(this.xmlname);
            this.eNumberHead = cc.ENumberHead;
            //+ EagleAPI.GetRandom01(DateTime.Now.Minute) 
            //+ EagleAPI.GetRandom01(DateTime.Now.Second) 
            //+ EagleAPI.GetRandom01(DateTime.Now.Millisecond);

            this.tb经办人.Text = cc.Signature;
            this.numericUpDown1.Value = cc.OffsetX;
            this.numericUpDown2.Value = cc.OffsetY;
            this.tb保单序号.Text = cc.SaveNo;
            tb填开日期.Text = System.DateTime.Now.ToShortDateString();

            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            this.ActiveControl = this.tbPnr;
            //if (GlobalVar2.bTempus) lb保单标题.Text = "商务会员信息卡";
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
            //打印项
            HyxStructs hs = new HyxStructs();
            float scale = 1F;
            if (!bLianxu)
            {

                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();
                ptString.Add(this.tb保单号码.Text); ptPoint.Add(new PointF(49.3F * scale, 31.4F * scale));
                ptString.Add(this.cb被保险人姓名.Text); ptPoint.Add(new PointF(65.3F * scale, 42.9F * scale));
                ptString.Add(this.tb证件号.Text); ptPoint.Add(new PointF(116.3F * scale, 42.9F * scale));
                ptString.Add(this.tb航班号.Text +"  "+ this.tb乘机日.Text); ptPoint.Add(new PointF(111.3F * scale, 57.9F * scale));
                //ptString.Add(this.tb乘机日.Text); ptPoint.Add(new PointF(111.3F * scale, 43.7F * scale));
                ptString.Add(this.tb受益人资料.Text); ptPoint.Add(new PointF(118.9F * scale, 82.0F * scale));
                ptString.Add(this.tb填开日期.Text); ptPoint.Add(new PointF(175.3F * scale, 92.3F * scale));
                ptString.Add(this.tb经办人.Text); ptPoint.Add(new PointF(175.3F * scale, 98.0F * scale));

                /*被保险人姓名：48.5,35.3
身份证号/护照号:117.9,35.3
航班号:45.7,43.7
乘机日:111.3,43.7
受益人姓名:45.7,59.9
打印日期:166.6,74.2
经办人:3.51,83.3*/
                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
            }
            else
            {
                if (ls == null || ls.Count == 0) return;
                //for (int i = 0; i < ls.Count; i++)
                bool bSubmited = false;
                {

                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = eNumberHead + EagleAPI.GetRandom62(); //this.tb保单号码.Text;
                    hs.IssueNumber = ls[iPage].Split('~')[2];
                    hs.NameIssued = ls[iPage].Split('~')[0];
                    hs.CardType = "航班号" + tb航班号.Text + "乘机日" + tb乘机日.Text;
                    hs.CardNumber = ls[iPage].Split('~')[1];
                    hs.Remark = insuranceType; //保险种类别名代码B07
                    hs.IssuePeriod = "";
                    hs.IssueBegin = (bhyx ? tb乘机日.Text : dtp保险起始时间.Value.ToString());//必须为时间串
                    hs.IssueEnd = (bhyx ? tb乘机日.Text : dtp保险终止时间.Value.ToString());//必须为时间串
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tb受益人资料.Text + tb受益人关系.Text;
                    hs.Signature = tb经办人.Text;// tbSignatureDate.Text;
                    hs.SignDate = tb填开日期.Text;//dtp_Date.Value.ToShortDateString();
                    hs.InssuerName = "";
                    hs.Pnr = tbPnr.Text;
                    if (!hs.SubmitInfo())
                    {
                        MessageBox.Show(ls[iPage].Replace("~", "－") + "提交失败！");
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
                        ptString.Add(this.cb被保险人姓名.Text); ptPoint.Add(new PointF(65.3F * scale, 42.9F * scale));
                        //ptString.Add(this.tb证件号.Text); ptPoint.Add(new PointF(117.9F * scale, 35.5F * scale));
                        //ptString.Add(this.tb航班号.Text); ptPoint.Add(new PointF(45.7F * scale, 43.7F * scale));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(116.3F * scale, 42.9F * scale));
                        ptString.Add(ls[iPage].Split('~')[1]); ptPoint.Add(new PointF(111.3F * scale, 57.9F * scale));

                        //ptString.Add(this.tb乘机日.Text); ptPoint.Add(new PointF(111.3F * scale, 43.7F * scale));
                        ptString.Add(this.tb受益人资料.Text); ptPoint.Add(new PointF(118.9F * scale, 82.0F * scale));
                        ptString.Add(this.tb填开日期.Text); ptPoint.Add(new PointF(175.3F * scale, 92.3F * scale));
                        ptString.Add(this.tb经办人.Text); ptPoint.Add(new PointF(175.3F * scale, 98.0F * scale));

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

