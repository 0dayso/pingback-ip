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
                lb公司名称.Text = "中国太平洋财产保险股份有限公司";
                lb保单标题.Text = "航空旅客人身意外伤害保险保险单";
                this.Text = "打印 太平洋－航空意外伤害保险";
            }
            catch
            {
            }


            this.xmlname = "Pacific";
            if (!checkxmlfile()) Dispose();
            this.insuranceType = "B0A";//B09 Eagle保险卡
            this.insuranceNumberLength = 10;
            this.paperHeight = 400;
            this.paperWidth = 949;
            this.bhyx = false;
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
            //打印项
            HyxStructs hs = new HyxStructs();
            float scale = 1F;
            if (!bLianxu)
            {

                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();

                ptString.Add(this.tb保单号码.Text); ptPoint.Add(new PointF(161.5F * scale, 25.9F * scale));
                ptString.Add(this.tb保单密码.Text); ptPoint.Add(new PointF(161.5F * scale, 33.5F * scale));
                ptString.Add(this.cb被保险人姓名.Text); ptPoint.Add(new PointF(40F * scale, 39.8F * scale));
                ptString.Add(this.tb证件号.Text); ptPoint.Add(new PointF(137.4F * scale, 39.8F * scale));
                ptString.Add(this.tb航班号.Text); ptPoint.Add(new PointF(40F * scale, 47.5F * scale));
                ptString.Add(this.tb乘机日.Text); ptPoint.Add(new PointF(137.4F * scale, 47.5F * scale));
                ptString.Add(this.tb保险金额.Text); ptPoint.Add(new PointF(36.8F * scale, 55.8F * scale));
                ptString.Add(this.tb保险金额大写.Text); ptPoint.Add(new PointF(70.1F * scale, 55.8F * scale));
                ptString.Add(this.tb保险费.Text); ptPoint.Add(new PointF(117.8F * scale, 55.8F * scale));
                ptString.Add(this.tb保险费大写.Text); ptPoint.Add(new PointF(157.8F * scale, 55.8F * scale));


                ptString.Add(this.tb受益人关系.Text); ptPoint.Add(new PointF(144.7F * scale, 63.1F * scale));
                ptString.Add(this.tb受益人资料.Text); ptPoint.Add(new PointF(36.8F * scale, 69.4F * scale));//姓名
                ptString.Add(this.tb受益人地址.Text); ptPoint.Add(new PointF(36.8F * scale, 75.3F * scale));
                ptString.Add(this.tb受益人邮编.Text); ptPoint.Add(new PointF(117.8F * scale, 75.3F * scale));
                ptString.Add(this.tb报案电话.Text); ptPoint.Add(new PointF(36.8F * scale, 82.1F * scale));


                //ptString.Add(this.tb经办人.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                ptString.Add(this.tb填开日期.Text); ptPoint.Add(new PointF(138.4F * scale, (82.1F) * scale));

                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
            }
            else
            {
                if (ls == null || ls.Count == 0) return;
                //for (int i = 0; i < ls.Count; i++)
                bool bSubmited = false;
                {

                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = eNumberHead + DateTime.Now.Date.ToString("yyyyMMdd") + ls[iPage].Split('~')[2]; //this.tb保单号码.Text;
                    hs.IssueNumber = ls[iPage].Split('~')[2];
                    hs.NameIssued = ls[iPage].Split('~')[0];
                    hs.CardType = "航班号" + tb航班号.Text + "乘机日" + tb乘机日.Text;
                    hs.CardNumber = ls[iPage].Split('~')[1];
                    hs.Remark = insuranceType; //保险种类别名代码B0A-太平洋航意险
                    hs.IssuePeriod = "";
                    hs.IssueBegin = (bhyx ? tb乘机日.Text : dtp保险起始时间.Value.ToString());//必须为时间串
                    hs.IssueEnd = (bhyx ? tb乘机日.Text : dtp保险终止时间.Value.ToString());//必须为时间串
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tb受益人资料.Text;// +tb受益人关系.Text;
                    hs.Signature =tb报案电话.Text + tb填开单位.Text + tb经办人.Text;// tbSignatureDate.Text;
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

                        ptString.Add(hs.eNumber); ptPoint.Add(new PointF(161.5F * scale, 25.9F * scale));
                        ptString.Add(this.tb保单密码.Text); ptPoint.Add(new PointF(161.5F * scale, 33.5F * scale));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(40F * scale, 39.8F * scale));//姓名
                        ptString.Add(this.tb证件号.Text); ptPoint.Add(new PointF(137.4F * scale, 39.8F * scale));
                        ptString.Add(this.tb航班号.Text); ptPoint.Add(new PointF(40F * scale, 47.5F * scale));
                        ptString.Add(this.tb乘机日.Text); ptPoint.Add(new PointF(137.4F * scale, 47.5F * scale));
                        ptString.Add(this.tb保险金额.Text); ptPoint.Add(new PointF(36.8F * scale, 55.8F * scale));
                        ptString.Add(this.tb保险金额大写.Text); ptPoint.Add(new PointF(70.1F * scale, 55.8F * scale));
                        ptString.Add(this.tb保险费.Text); ptPoint.Add(new PointF(117.8F * scale, 55.8F * scale));
                        ptString.Add(this.tb保险费大写.Text); ptPoint.Add(new PointF(157.8F * scale, 55.8F * scale));


                        ptString.Add(this.tb受益人关系.Text); ptPoint.Add(new PointF(144.7F * scale, 63.1F * scale));
                        ptString.Add(this.tb受益人资料.Text); ptPoint.Add(new PointF(36.8F * scale, 69.4F * scale));//姓名
                        ptString.Add(this.tb受益人地址.Text); ptPoint.Add(new PointF(36.8F * scale, 75.3F * scale));
                        ptString.Add(this.tb受益人邮编.Text); ptPoint.Add(new PointF(117.8F * scale, 75.3F * scale));
                        ptString.Add(this.tb报案电话.Text); ptPoint.Add(new PointF(36.8F * scale, 82.1F * scale));


                        //ptString.Add(this.tb经办人.Text); ptPoint.Add(new PointF(64.2F * scale, 88.8F * scale));
                        ptString.Add(this.tb填开日期.Text); ptPoint.Add(new PointF(138.4F * scale, (82.1F) * scale));


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

