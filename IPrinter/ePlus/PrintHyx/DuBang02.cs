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
    public partial class DuBang02 : Form
    {
        public DuBang02()
        {
            InitializeComponent();
        }
        static public bool b_opened = false;
        static public string retstring = "";
        static public PrintHyx.DuBang02 context = null;
        string eNumberHead = "";

        public bool bEagleDubang = false;
        private void DuBang02_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            LogoPicture.pictures pic = new LogoPicture.pictures();
            this.pictureBox1.Image = pic.pictureBox3.Image;
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            dtpBeg.Value = dt;
            dtpPrintTime.Value = dt;
            cfg_dubang02 cfg = new cfg_dubang02();
            cfg.GetConfig();
            tbTerm.Text = cfg.Term;
            dtpEnd.Value = dt.AddDays(int.Parse(tbTerm.Text.Trim()));
            tbNo.Text = cfg.SaveNo;
            tbSignature.Text = cfg.Signature;
            numericUpDown1.Value = cfg.OffsetX;
            numericUpDown2.Value = cfg.OffsetY;
            eNumberHead = cfg.ENumberHead;
            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;

            if (tbNo.Text.Trim() == "") tbNo.Text = "110108070";

            if (bEagleDubang && b_dubang03)
            {
                PrintHyx.bxLogin bx = new ePlus.PrintHyx.bxLogin();
                if (bx.ShowDialog() != DialogResult.OK) this.Close();

            }
        }

        private void tbPnr_KeyUp(object sender, KeyEventArgs e)
        {
            PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cbName, e, ref retstring);

        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            if (!Model.md.b_004) Application.Exit();
        }
        static public string returnstring
        {
            set
            {
                if (context != null)
                {
                    string temp = "";
                    PrintHyx.PrintHyxPublic.GetRetString(ref retstring, ref temp);
                    if (temp != "") rs = temp;
                }

            }
        }
        static public string rs
        {
            set
            {
                if (context.InvokeRequired)
                {
                    EventHandler eh = new EventHandler(setcontrol);
                    PrintHyx.DuBang02 pt = PrintHyx.DuBang02.context;
                    PrintHyx.DuBang02.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            if (!EagleAPI.GetNoPnr(retstring)) return;
            context.cbName.Items.Clear();
            List<string> names = new List<string>();
            names = EagleAPI.GetNames(retstring);
            for (int i = 0; i < names.Count; i++)
            {
                context.cbName.Items.Add(names[i]);
            }
            context.cbName.Text = context.cbName.Items[0].ToString();
            context.tbCardID.Text = EagleAPI.GetIDCardNo(retstring)[0];
            string date = EagleAPI.GetDateStart(retstring);
            int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
            int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
            int iyy = System.DateTime.Now.Year;
            System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
            while (dt < System.DateTime.Now)
            {
                dt = dt.AddYears(1);
            }
            context.dtpBeg.Value = dt;
        }

        private void DuBang02_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;
            if (!Model.md.b_004) Application.Exit();

        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbName.SelectedItem != null)
            {
                this.tbCardID.Text = EagleAPI.GetIDCardNo(retstring)[cbName.SelectedIndex];
            }

        }

        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            Default yd = new Default();
            yd.xmlFirst = "DuBang02";
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_dubang02 cfg = new cfg_dubang02();
                cfg.GetConfig();
                eNumberHead = cfg.ENumberHead;
                this.tbSignature.Text = cfg.Signature;
                this.numericUpDown1.Value = cfg.OffsetX;
                this.numericUpDown2.Value = cfg.OffsetY;
                this.tbNo.Text = cfg.SaveNo;
            }
        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            save_phone();
            if (tbNo.Text.Trim().Length != 16)
            {
                MessageBox.Show("保单号长度错误");
                return;
            }
            try
            {
                long.Parse(tbNo.Text.Trim());
                int.Parse(tbTerm.Text.Trim());
                if (int.Parse(tbTimeBeg.Text.Trim()) >= 24 || int.Parse(tbTimeBeg.Text.Trim()) < 0 || int.Parse(tbTimeEnd.Text.Trim()) >= 24 || int.Parse(tbTimeEnd.Text.Trim()) < 0)
                {
                    MessageBox.Show("时间不正确");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("保单号或保险期限或时间只能为数字");
                return;
            }
            if (cbName.Text.Trim() == "")
            {
                MessageBox.Show("姓名不能为空");
                return;
            }
            if (tbCardID.Text.Trim() == "")
            {
                MessageBox.Show("证件号码不能为空");
                return;
            }

            this.tbPolicyNo.Text = this.eNumberHead + "0" + tbNo.Text.Trim().Substring(8, 8);
            if(!(bEagleDubang&&b_dubang03))
            {
                if (!GlobalVar.b_OffLine)
                {
                    //if (!b_dubang03)
                    {
                        if (cbName.Text != GlobalVar.HYXTESTPRINT)
                        {
                            HyxStructs hs = new HyxStructs();
                            hs.UserID = GlobalVar.loginName;
                            hs.eNumber = tbPolicyNo.Text;
                            hs.IssueNumber = tbNo.Text;
                            hs.NameIssued = cbName.Text;
                            hs.CardType = "";
                            hs.CardNumber = tbCardID.Text;
                            if (b_dubang03) hs.Remark = "B05";//出行乐
                            else hs.Remark = "B04";//8"都帮出行无忧";B04
                            hs.IssuePeriod = tbTerm.Text.Trim() + "天";
                            hs.IssueBegin = dtpBeg.Value.ToShortDateString() + " " + tbTimeBeg.Text.Trim() + ":00:00";
                            hs.IssueEnd = dtpEnd.Value.ToShortDateString() + " " + tbTimeEnd.Text.Trim() + ":00:00";
                            hs.SolutionDisputed = "";
                            hs.NameBeneficiary = this.tbBeneficiary.Text;
                            hs.Signature = this.tbSignature.Text;
                            hs.SignDate = dtpPrintTime.Value.ToShortDateString();
                            hs.Pnr = this.tbPnr.Text;
                            if (!hs.SubmitInfo())
                            {
                                MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                string btText = bt_Print.Text;
                bt_Print.Text = "验证……";
                Application.DoEvents();
                {
                    EP.WebService epws = new EP.WebService();
                    EP.WebServiceReturnEntity epret = new EP.WebServiceReturnEntity();
                    epret = epws.PurchaseDubang(GlobalVar2.bxUserAccount,
                        GlobalVar2.bxPassWord,
                        "都邦出行无忧(易格网)",//lb公司名称.Text,
                        DateTime.Parse(dtpBeg.Value.ToShortDateString() + " " + tbTimeBeg.Text.Trim() + ":00:00"),//DateTime.Parse(tb乘机日.Text)//七天为起保日期
                        "7天",//tb航班号.Text, //七天没有航班号
                        tbCardID.Text,//tb证件号.Text, 
                        cbName.Text,//cb被保险人姓名.Text, 
                        "02785777575",//GlobalVar2.bxTelephone,//保单上无电话
                        this.tbBeneficiary.Text,//tb受益人关系.Text, 
                        "",//tb受益人资料.Text//保单上
                        tbNo.Text
                        );
                    if (!epret.Enabled)
                    {
                        MessageBox.Show(epret.ErrorMsg);
                        bt_Print.Text = btText;
                        return;
                    }
                    else
                    {//打印
                        tbPolicyNo.Text = "微机码: " + epret.SerialNo;//微机码
                        //tb保单序号.Text = epret.CaseNo;//单证号码
                        this.tbSignature.Text = epret.AgentName;//加盟商明称
                    }
                }
                bt_Print.Text = btText;
            }
            PrintDialog pd = new PrintDialog();
            EagleAPI.PrinterSetupCostom(ptDoc, 951, 404);
            pd.Document = ptDoc;
            //DialogResult dr = pd.ShowDialog();
            //if (dr == DialogResult.OK)
            {
                ptDoc.Print();
            }

        }
        void save_phone()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            //xn = xn.SelectSingleNode("DuBang01");
            //xn = xn.SelectSingleNode("Phone");
            //xn.InnerText = tbPhone.Text;

            //xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang02");
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang02");
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang02");
            xn = xn.SelectSingleNode("Signature");
            xn.InnerText = this.tbSignature.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang02");
            xn = xn.SelectSingleNode("Term");
            xn.InnerText = this.tbTerm.Text;


            xd.Save(GlobalVar.s_configfile);
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("system", GlobalVar.fontsize, System.Drawing.FontStyle.Regular);
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
            List<string> ptString = new List<string>();
            List<PointF> ptPoint = new List<PointF>();
            if (!b_dubang03) { ptString.Add("仅限湖北省销售"); ptPoint.Add(new PointF(181F, 20F)); }
            ptString.Add(tbPolicyNo.Text); ptPoint.Add(new PointF(50F, 34F));
            ptString.Add(cbName.Text); ptPoint.Add(new PointF(50F, 43F));
            ptString.Add(tbCardID.Text); ptPoint.Add(new PointF(149F, 43F));
            ptString.Add(tbBeneficiary.Text); ptPoint.Add(new PointF(185F, 52F));
            ptString.Add(tbTerm.Text); ptPoint.Add(new PointF(58F, 82F));
            if (b_dubang03)
            {
                ptString.Add(dtpBeg.Value.Year.ToString()); ptPoint.Add(new PointF(83F + 28.7F, 82F));
                ptString.Add(dtpBeg.Value.Month.ToString("D2")); ptPoint.Add(new PointF(102F + 23.4F, 82F));
                ptString.Add(dtpBeg.Value.Day.ToString("D2")); ptPoint.Add(new PointF(115F + 19.1F, 82F));

                ptString.Add(dtpEnd.Value.Year.ToString()); ptPoint.Add(new PointF(156F + 12.4F, 82F));
                ptString.Add(dtpEnd.Value.Month.ToString("D2")); ptPoint.Add(new PointF(174F + 5.8F, 82F));
                ptString.Add(dtpEnd.Value.Day.ToString("D2")); ptPoint.Add(new PointF(188F + 0F, 82F));


            }
            else
            {
                ptString.Add(dtpBeg.Value.Year.ToString()); ptPoint.Add(new PointF(83F, 82F));
                ptString.Add(dtpBeg.Value.Month.ToString("D2")); ptPoint.Add(new PointF(102F, 82F));
                ptString.Add(dtpBeg.Value.Day.ToString("D2")); ptPoint.Add(new PointF(115F, 82F));
                ptString.Add(tbTimeBeg.Text); ptPoint.Add(new PointF(130F, 82F));
                ptString.Add(dtpEnd.Value.Year.ToString()); ptPoint.Add(new PointF(156F, 82F));
                ptString.Add(dtpEnd.Value.Month.ToString("D2")); ptPoint.Add(new PointF(174F, 82F));
                ptString.Add(dtpEnd.Value.Day.ToString("D2")); ptPoint.Add(new PointF(188F, 82F));
                ptString.Add(tbTimeEnd.Text); ptPoint.Add(new PointF(202F, 82F));
            }
            ptString.Add(tbSignature.Text); ptPoint.Add(new PointF(44F, 95F));
            ptString.Add(dtpPrintTime.Value.ToShortDateString()); ptPoint.Add(new PointF(190F, 95F));


            PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
            if (!b_dubang03) e.Graphics.DrawRectangle(new Pen(ptBrush, 0.3F), 181F, 16.9F, 28F, 8F);
        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                long it = long.Parse(tbNo.Text.Trim()) + 1L;
                tbNo.Text = it.ToString("D16");//8位航空旅客人身意外伤害保险单
            }
            catch
            {
                MessageBox.Show("保单号只能为数值");
            }
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("DuBang02");
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = this.tbNo.Text;

            xd.Save(GlobalVar.s_configfile);


        }

        private void tbTerm_Leave(object sender, EventArgs e)
        {
            if (tbTerm.Text.Trim() == "") return;
            int days = 0;
            try
            {
                days = int.Parse(tbTerm.Text.Trim());

            }
            catch
            {
                days = 0;
            }
            dtpEnd.Value = dtpBeg.Value.AddDays(days);
        }

        private void dtpBeg_ValueChanged(object sender, EventArgs e)
        {
            int days = 0;
            try
            {
                days = int.Parse(tbTerm.Text.Trim());

            }
            catch
            {
                days = 0;
            }
            if (days == 0)
            {
                TimeSpan tt = dtpEnd.Value - dtpBeg.Value;
                tbTerm.Text = tt.TotalDays.ToString();
            }
            else
            {
                dtpEnd.Value = dtpBeg.Value.AddDays(days);
            }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            int days = 0;
            try
            {
                days = int.Parse(tbTerm.Text.Trim());

            }
            catch
            {
                days = 0;
            }
            if (days == 0)
            {
                TimeSpan tt = dtpEnd.Value - dtpBeg.Value;
                tbTerm.Text = tt.TotalDays.ToString();
            }
            else
            {
                dtpBeg.Value = dtpEnd.Value.AddDays(0-days);
            }

        }

        private void DuBang02_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbName.Text = GlobalVar.HYXTESTPRINT;
            this.bt_Print_Click(sender, e);
        }
        bool b_dubang03 = false;
        public void Dubang03()
        {
            this.Text = "出行乐";
            this.label3.Text = "“出行乐”综合保障计划保险单（自助式）";
            this.label8.Text = "民航飞机意外保险金额；人民币肆拾万元整　RMB￥400,000.00元\r火车轮船意外保险金额：人民币伍万元整　　RMB￥50,000.00元";
            b_dubang03 = true;
        }

        private void btFading_Click(object sender, EventArgs e)
        {
            this.tbBeneficiary.Text = "法定人";
        }

        private void btPrintSeries_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbName.Items.Count; i++)
            {
                cbName.SelectedIndex = i;
                bt_Print_Click(sender,e);
                
            }
        }
    }
}