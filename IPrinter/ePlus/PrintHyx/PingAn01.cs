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
    public partial class PingAn01 : Form
    {
        public PingAn01()
        {
            InitializeComponent();
            
        }
        static public bool b_opened = false;
        static public string retstring = "";
        static public string retstringDetrF = "";
        static public PrintHyx.PingAn01 context = null;
        string eNumberHead = "";
        
        bool bLianxu = false;
        private void PingAn01_Load(object sender, EventArgs e)
        {
            throw new Exception("Unknown Error!");
            MessageBox.Show("本保险目前暂停使用");
            this.Close();
            cfg_pingan01 cc = new cfg_pingan01();
            cc.GetConfig();

            this.tbSignatureDate.Text = cc.Signature;
            this.numericUpDown1.Value = cc.OffsetX;
            this.numericUpDown2.Value = cc.OffsetY;
            this.tbNo.Text = cc.SaveNo;
            this.tbPhone.Text = cc.Phone;

            this.dateTimePicker1.Value = DateTime.Now;
            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            this.ActiveControl = this.tbPnr;

        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            if (!Model.md.b_004) Application.Exit();

        }

        private void tbPnr_KeyUp(object sender, KeyEventArgs e)
        {
            GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.none;
            if(radioButton1.Checked)
                PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cbName, e, ref retstring);
            if (radioButton3.Checked && e.KeyValue==13)
            {
                string piaohao = tbPnr.Text.Trim().Replace("-", "").Replace(" ", "");
                if (piaohao.Length != 13) { MessageBox.Show("票号错误"); return; }
                piaohao = piaohao.Substring(0, 3) + "-" + piaohao.Substring(3);
                string cmd = "detr:tn/" + piaohao;
                
                PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cbName, e, ref retstring, cmd);
            }
            if (radioButton2.Checked)
            {
                if (e.KeyValue == 13)
                {
                    AirCode ac = new AirCode(tbPnr.Text);
                    ac.ShowDialog();
                    if (ac.airCode != "") tbPnr.Text = ac.airCode;
                    PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cbName, e, ref retstring);
                    
                }
            }
        }
        static public string returnstring
        {
            set
            {
                if (context != null)
                {
                    string temp = "";
                    if(GlobalVar.formSendCmdType==GlobalVar.FormSendCommandType.detrF)
                        PrintHyx.PrintHyxPublic.GetRetString(ref retstringDetrF, ref temp);
                    else
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
                    PrintHyx.PingAn01 pt = PrintHyx.PingAn01.context;
                    PrintHyx.PingAn01.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            if (GlobalVar.formSendCmdType == GlobalVar.FormSendCommandType.detrF)
            {
                try
                {
                    context.tbCardID.Text = EagleAPI.GetCardIdByDetr_F(retstringDetrF);
                    return;
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
            }
            context.cbName.Items.Clear();
            try
            {
                if (context.radioButton1.Checked || context.radioButton2.Checked)
                {
                    retstring = retstring.Replace('+', ' ');
                    retstring = retstring.Replace('-', ' ');
                    if (!EagleAPI.GetNoPnr(retstring)) return;

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
                    //context.dtp_Start.Value = dt;

                    context.tbFlightNo.Text = EagleAPI.GetFlightNo(retstring) + EagleAPI.GetFlightNo2(retstring);
                    context.tbDate.Text = dt.ToShortDateString();
                }
                if (context.radioButton3.Checked)
                {
                    ePlus.eTicket.etInfomation ei = new ePlus.eTicket.etInfomation();
                    ei.SetVar(retstring);
                    context.cbName.Items.Add(ei.PASSENGER);
                    context.cbName.Text = ei.PASSENGER;
                    context.tbCardID.Text = "";
                    context.tbFlightNo.Text = EagleAPI.substring(ei.FROM, 4, 2) + EagleAPI.substring(ei.FROM, 10, 4);
                    string date = EagleAPI.substring(ei.FROM, 18, 5);
                    int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
                    int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
                    int iyy = System.DateTime.Now.Year;
                    System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
                    while (dt < System.DateTime.Now)
                    {
                        dt = dt.AddYears(1);
                    }
                    context.tbDate.Text = dt.ToShortDateString();

                }
            }
            catch
            {
            }
        }

        private void PingAn01_FormClosed(object sender, FormClosedEventArgs e)
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
            yd.xmlFirst = "PingAn01";
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_pingan01 cfg = new cfg_pingan01();
                cfg.GetConfig();
                eNumberHead = cfg.ENumberHead;
                this.tbSignatureDate.Text = cfg.Signature;
                this.numericUpDown1.Value = cfg.OffsetX;
                this.numericUpDown2.Value = cfg.OffsetY;
                this.tbNo.Text = cfg.SaveNo;
            }

        }

        private void bt_Print_Click(object sender, EventArgs e)
        {
            bLianxu = false;
            save_phone();
            if (tbNo.Text.Trim().Length != 7)
            {
                MessageBox.Show("保单号长度错误，7位");
                return;
            }
            try
            {
                long.Parse(tbNo.Text.Trim());
            }
            catch
            {
                MessageBox.Show("保单号只能为数字");
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
            try
            {
                DateTime.Parse(tbDate.Text.Trim());
            }
            catch
            {
                MessageBox.Show("乘机日期格式错误，如2007-4-2");
                return;
            }
            this.tbPolicyNo.Text = System.DateTime.Now.Year.ToString().Remove(1, 1) + EagleAPI.GetRandom62();
            if (!GlobalVar.b_OffLine)
            {
                if (cbName.Text.Trim() != GlobalVar.HYXTESTPRINT)
                {
                    HyxStructs hs = new HyxStructs();
                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = tbPolicyNo.Text;
                    hs.IssueNumber = tbNo.Text;
                    hs.NameIssued = cbName.Text;
                    hs.CardType = "航班号" + tbFlightNo.Text + "乘机日" + tbDate.Text; ;
                    hs.CardNumber = tbCardID.Text;
                    hs.Remark = "B06"; //9"平安－周游列国"B06
                    hs.IssuePeriod = "";
                    hs.IssueBegin = tbDate.Text;// dtp_Start.Value.ToShortDateString() + " 00:00:00";
                    hs.IssueEnd = tbDate.Text;// dtp_End.Value.ToShortDateString() + " 00:00:00";
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tbBenefit.Text;
                    hs.Signature = this.tbSignatureDate.Text;// this.tbSignatureDate.Text;
                    hs.SignDate = this.dateTimePicker1.Value.ToShortDateString();//dtp_Date.Value.ToShortDateString();
                    hs.InssuerName = "";
                    hs.Pnr = this.tbPnr.Text;
                    if (!hs.SubmitInfo())
                    {
                        //MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                        return;
                    }
                }
            }
            PrintDialog pd = new PrintDialog();
            EagleAPI.PrinterSetupCostom(ptDoc, 951, 399);
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
            xn = xn.SelectSingleNode("PingAn01");
            xn = xn.SelectSingleNode("Phone");
            xn.InnerText = tbPhone.Text;

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PingAn01");
            xn = xn.SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("PingAn01");
            xn = xn.SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            //xn = xd.SelectSingleNode("eg");
            //xn = xn.SelectSingleNode("PingAn01");
            //xn = xn.SelectSingleNode("Signature");
            //xn.InnerText = this.tb_Signature.Text;

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
            if (!bLianxu)
            {
                List<string> ptString = new List<string>();
                List<PointF> ptPoint = new List<PointF>();
                ptString.Add(this.tbPolicyIDNo.Text); ptPoint.Add(new PointF(185F, 31F));
                ptString.Add(cbName.Text); ptPoint.Add(new PointF(57.1F, 39F));
                ptString.Add(tbCardID.Text); ptPoint.Add(new PointF(155F, 39F));
                ptString.Add(tbFlightNo.Text); ptPoint.Add(new PointF(57.1F, 50F));
                ptString.Add(tbDate.Text); ptPoint.Add(new PointF(155F, 50F));
                ptString.Add(tbRelation.Text); ptPoint.Add(new PointF(131.8F, 72.4F));
                ptString.Add(tbBenefit.Text); ptPoint.Add(new PointF(47F, 72.4F));
                //ptString.Add(tbPhone.Text); ptPoint.Add(new PointF(44F, 90F));
                ptString.Add(dateTimePicker1.Value.ToShortDateString()); ptPoint.Add(new PointF(106.4F, 84.3F));
                ptString.Add(tbSignatureDate.Text); ptPoint.Add(new PointF(183.1F, 84.3F));
                PrintHyx.PrintHyxPublic.PrintItems(ptString.ToArray(), ptPoint.ToArray(), o, ptFontEn, ptBrush, e);
            }
            else
            {
                if (ls == null || ls.Count == 0) return;
                //for (int i = 0; i < ls.Count; i++)
                bool bSubmited = false;
                {
                    HyxStructs hs = new HyxStructs();
                    hs.UserID = GlobalVar.loginName;
                    hs.eNumber = System.DateTime.Now.Year.ToString().Remove(1, 1) + EagleAPI.GetRandom62();// tbPolicyNo.Text;
                    hs.IssueNumber = ls[iPage].Split('~')[2];
                    hs.NameIssued = ls[iPage].Split('~')[0];
                    hs.CardType = "航班号" + tbFlightNo.Text + "乘机日" + tbDate.Text; ;
                    hs.CardNumber = ls[iPage].Split('~')[1];
                    hs.Remark = "B06"; //9"平安－周游列国"
                    hs.IssuePeriod = "";
                    hs.IssueBegin = tbDate.Text;// dtp_Start.Value.ToShortDateString() + " 00:00:00";
                    hs.IssueEnd = tbDate.Text;// dtp_End.Value.ToShortDateString() + " 00:00:00";
                    hs.SolutionDisputed = "";
                    hs.NameBeneficiary = tbBenefit.Text;
                    hs.Signature = this.tbSignatureDate.Text;// this.tbSignatureDate.Text;
                    hs.SignDate = this.dateTimePicker1.Value.ToShortDateString();//dtp_Date.Value.ToShortDateString();
                    hs.InssuerName = "";
                    hs.Pnr = this.tbPnr.Text;
                    if (!hs.SubmitInfo())
                    {
                        MessageBox.Show(ls[iPage].Replace("~","－")+"提交失败！");
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
                        ptString.Add(this.tbPolicyIDNo.Text); ptPoint.Add(new PointF(185F, 31F));
                        ptString.Add(ls[iPage].Split('~')[0]); ptPoint.Add(new PointF(57.1F, 39F));
                        ptString.Add(ls[iPage].Split('~')[1]); ptPoint.Add(new PointF(155F, 39F));
                        ptString.Add(tbFlightNo.Text); ptPoint.Add(new PointF(57.1F, 50F));
                        ptString.Add(tbDate.Text); ptPoint.Add(new PointF(155F, 50F));
                        ptString.Add(tbRelation.Text); ptPoint.Add(new PointF(131.8F, 72.4F));
                        ptString.Add(tbBenefit.Text); ptPoint.Add(new PointF(47F, 72.4F));
                        //ptString.Add(tbPhone.Text); ptPoint.Add(new PointF(44F, 90F));
                        ptString.Add(dateTimePicker1.Value.ToShortDateString()); ptPoint.Add(new PointF(106.4F, 84.3F));
                        ptString.Add(tbSignatureDate.Text); ptPoint.Add(new PointF(183.1F, 84.3F));
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
        int iPage = 0;
        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            iPage = 0;
            try
            {
                long it = long.Parse(tbNo.Text.Trim()) + 1L;
                tbNo.Text = it.ToString("D7");//7位平安周游列国会员保险信息卡
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
            xn = xn.SelectSingleNode("PingAn01");
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = this.tbNo.Text;

            xd.Save(GlobalVar.s_configfile);
        }

        private void PingAn01_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbName.Text = GlobalVar.HYXTESTPRINT;
            bt_Print_Click(sender, e);
        }
        List<string> ls = null;
        private void btPrintLianXu_Click(object sender, EventArgs e)
        {
            if (tbFlightNo.Text.Trim() == "" || tbDate.Text.Trim() == "")
            {
                MessageBox.Show("请先输入航班及日期"); return;
            }
            bLianxu = true;
            string[] names = new string[cbName.Items.Count];
            string[] cardids = new string[cbName.Items.Count];
            string[] policynos = new string[cbName.Items.Count];
            Options.PrintBaoXianLianXu pb = null;
            if (cbName.Items.Count < 1)
                pb = new Options.PrintBaoXianLianXu();
            else
            {
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = cbName.Items[i].ToString();
                    cardids[i] = EagleAPI.GetIDCardNo(retstring)[i];
                    long no = (long.Parse(tbNo.Text.Trim())+(long)i);
                    policynos[i] = no.ToString("D7");
                }
                pb = new Options.PrintBaoXianLianXu(names, cardids, policynos);
            }
            if (pb.ShowDialog() != DialogResult.OK) return;
            if (pb.ls == null || pb.ls.Count < 1) return;
            ls = new List<string>(pb.ls);
            pb.Dispose();
            PrintDialog pd = new PrintDialog();
            EagleAPI.PrinterSetupCostom(ptDoc, 951, 399);
            pd.Document = ptDoc;
            //DialogResult dr = pd.ShowDialog();

            //if (dr == DialogResult.OK)
            {
                ptDoc.Print();
            }
        }

        private void btCancelInsurance_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定作废保单" + tbNo.Text.Trim() + "吗？", "注意", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            if(WebService.CancelInsurance(GlobalVar.loginName,"B06",tbNo.Text.Trim()))
                MessageBox.Show("作废成功");
            else
                MessageBox.Show("作废失败");
        }

        private void PingAn01_MouseClick_1(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);

        }

        private void btGetCardId_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.detrF;
                EagleAPI.CLEARCMDLIST(3);
                string etnumber = this.radioButton3.Checked?tbPnr.Text:EagleAPI.GetETNumber(retstring)[cbName.SelectedIndex].Replace(' ', '-');
                EagleAPI.EagleSendCmd("detr:tn/" + etnumber + ",f");
                this.tbCardID.Text = "请稍等…………";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message+"，请先提取PNR或票号！");
            }
        }

    }
}