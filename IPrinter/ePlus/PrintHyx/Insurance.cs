using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;//roc,rmk,ig
using System.IO;
using gs.para;

namespace ePlus.PrintHyx
{
    public partial class Insurance : Form
    {
        static public bool b_opened = false;
        static public string retstring = "";
        static public string retstringDetrF = "";
        static public PrintHyx.Insurance context = null;
        public string eNumberHead = "";

        public bool bhyx = false;
        public string insuranceType = "";
        public int insuranceNumberLength = 0;
        public int paperWidth = 0;
        public int paperHeight = 0;
        cfg_insurance cc = new cfg_insurance();
        public Insurance()
        {
            InitializeComponent();

            //if (!checkxmlfile()) Dispose();
            //cc.GetConfig(xmlname);
            //eNumberHead = cc.ENumberHead;
            //tb经办人.Text = cc.Signature;
            //numericUpDown1.Value = cc.OffsetX;
            //numericUpDown2.Value = cc.OffsetY;
            //tb保单序号.Text = cc.SaveNo;
            //tb报案电话.Text = cc.Phone;
            //tb填开单位.Text = cc.CompanyAddr;

            //tb填开日期.Text = System.DateTime.Now.ToString();
            
            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;
            ActiveControl = tbPnr;
        }
        public bool checkxmlfile()
        {
            if (xmlname == "")
            {
                MessageBox.Show("未设置xmlname");
                return false;
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(GlobalVar.s_configfile);
                if (xd.SelectSingleNode("eg").SelectSingleNode(xmlname) == null)
                {
                    FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    string temp = sr.ReadToEnd();
                    sr.Close();
                    fs.Close();
                    temp = temp.Insert(temp.IndexOf("</eg>"),
                        @"  <"+xmlname+@">
    <ENumberHead>ABCDEFG</ENumberHead>
    <Signature>请设置1</Signature>
    <OffsetX>1</OffsetX>
    <OffsetY>0</OffsetY>
    <SaveNo>0123456789</SaveNo>
    <CompanyAddr>填开单位1</CompanyAddr>
    <Phone>82424242</Phone>
    <Term>0</Term>
  </"+xmlname+@">");
                    xd.LoadXml(temp);
                    xd.Save(GlobalVar.s_configfile);

                }
            }
            return true;
        }


        private void bt_Exit_Click(object sender, EventArgs e)
        {
            Close();
            if (!Model.md.b_004) Application.Exit();

        }

        private void tbPnr_KeyUp(object sender, KeyEventArgs e)
        {
            cb被保险人姓名.Items.Clear();
            GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.none;
            if (rb1.Checked)
            {
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                    PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cb被保险人姓名, e, ref retstring);
                else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                {
                    if (e.KeyValue == 13)
                        rtPnr();
                }
            }
            if (rb3.Checked && e.KeyValue == 13)
            {
                string piaohao = tbPnr.Text.Trim().Replace("-", "").Replace(" ", "");
                if (piaohao.Length != 13) { MessageBox.Show("票号错误"); return; }
                piaohao = piaohao.Substring(0, 3) + "-" + piaohao.Substring(3);
                string cmd = "detr:tn/" + piaohao;

                PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cb被保险人姓名, e, ref retstring, cmd);
            }
            if (rb2.Checked)
            {
                if (e.KeyValue == 13)
                {
                    AirCode ac = new AirCode(tbPnr.Text);
                    ac.ShowDialog();
                    if (ac.airCode != "") tbPnr.Text = ac.airCode;
                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                        PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cb被保险人姓名, e, ref retstring);
                    else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                        rtPnr();

                }
            }
        }
        private string rtXml = "";
        private void rtPnr()
        {
            Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
            rtXml = ib.rt2(tbPnr.Text);
            if (rtXml == "") { cb被保险人姓名.Text = "PNR有误或该PNR已取消"; return; }
            Options.ibe.IbeRt ir = new Options.ibe.IbeRt (rtXml);
            string[] names = ir.getpeopleinfo(0);
            cb被保险人姓名.Items.AddRange(names);
            cb被保险人姓名.Text = names[0];
            tb证件号.Text = ir.getpeopleinfo(1)[0];
            string[] segments = ir.getflightsegsinfo();
            tb航班号.Text = "";
            for (int i = 0; i < segments.Length; i++)
            {
                string[] fi = segments[i].Split('~');
                tb航班号.Text += fi[0];
                if (i == 0)
                {
                    System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                    DateTime dt = DateTime.ParseExact(fi[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                    tb乘机日.Text = dt.ToString();// dt.ToShortDateString();
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
                    if (GlobalVar.formSendCmdType == GlobalVar.FormSendCommandType.detrF)
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
                    PrintHyx.Insurance pt = PrintHyx.Insurance.context;
                    PrintHyx.Insurance.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            if (GlobalVar.formSendCmdType == GlobalVar.FormSendCommandType.detrF)
            {
                try
                {
                    context.tb证件号.Text = EagleAPI.GetCardIdByDetr_F(retstringDetrF);
                    return;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
            }
            context.cb被保险人姓名.Items.Clear();
            try
            {
                if (context.rb1.Checked || context.rb2.Checked)
                {
                    retstring = retstring.Replace('+', ' ');
                    retstring = retstring.Replace('-', ' ');
                    if (!EagleAPI.GetNoPnr(retstring)) return;

                    List<string> names = new List<string>();
                    names = EagleAPI.GetNames(retstring);
                    for (int i = 0; i < names.Count; i++)
                    {
                        context.cb被保险人姓名.Items.Add(names[i]);
                    }
                    context.cb被保险人姓名.Text = context.cb被保险人姓名.Items[0].ToString();
                    context.tb证件号.Text = EagleAPI.GetIDCardNo(retstring)[0];
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

                    context.tb航班号.Text = EagleAPI.GetFlightNo(retstring) + EagleAPI.GetFlightNo2(retstring);
                    context.tb乘机日.Text = dt.ToShortDateString();
                    context.dtp保险起始时间.Value = DateTime.Parse(dt.ToShortDateString());
                }
                if (context.rb3.Checked)
                {
                    ePlus.eTicket.etInfomation ei = new ePlus.eTicket.etInfomation();
                    ei.SetVar(retstring);
                    context.cb被保险人姓名.Items.Add(ei.PASSENGER);
                    context.cb被保险人姓名.Text = ei.PASSENGER;
                    context.tb证件号.Text = "";
                    context.tb航班号.Text = EagleAPI.substring(ei.FROM, 4, 2) + EagleAPI.substring(ei.FROM, 10, 4);
                    string date = EagleAPI.substring(ei.FROM, 18, 5);
                    int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
                    int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
                    int iyy = System.DateTime.Now.Year;
                    System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
                    while (dt < System.DateTime.Now)
                    {
                        dt = dt.AddYears(1);
                    }
                    context.tb乘机日.Text = dt.ToShortDateString();

                }
            }
            catch
            {
            }
        }

        private void Insurance_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_opened = false;
            connect_4_Command.PrintWindowOpen = false;
            if (!Model.md.b_004) Application.Exit();

        }

        private void cb被保险人姓名_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb被保险人姓名.SelectedItem != null)
                {
                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                        tb证件号.Text = EagleAPI.GetIDCardNo(retstring)[cb被保险人姓名.SelectedIndex];
                    else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                    {
                        Options.ibe.IbeRt ir = new Options.ibe.IbeRt(rtXml);
                        if (rtXml == "") { MessageBox.Show("请提取PNR"); return; }
                        tb证件号.Text = ir.getpeopleinfo(1)[cb被保险人姓名.SelectedIndex];
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("选择姓名时：" + ex.Message);
            }
        }

        private void bt取身份证号_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                {
                    GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.detrF;
                    EagleAPI.CLEARCMDLIST(3);
                    string etnumber = rb3.Checked ? tbPnr.Text : EagleAPI.GetETNumber(retstring)[cb被保险人姓名.SelectedIndex].Replace(' ', '-');
                    //EagleAPI.EagleSendCmd("detr:tn/" + etnumber + ",f");
                    tb证件号.Text = "请稍等…………";
                }
                else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                {
                    GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.detrF;
                    EagleAPI.CLEARCMDLIST(3);
                    Options.ibe.IbeRt ir = new Options.ibe.IbeRt(rtXml);
                    string etnumber = ir.getpeopleinfo(2)[cb被保险人姓名.SelectedIndex];//这里不一样(IBE坏了怎么办？)
                    EagleAPI.EagleSendCmd("detr:tn/" + etnumber + ",f");
                    tb证件号.Text = "请稍等…………";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "，请先提取PNR或票号！");
            }
        }

        private void bt作废保单_Click(object sender, EventArgs e)
        {
            if (insuranceType == "")
            {
                MessageBox.Show("未设置保险类别代号insuranceType");
                return;
            }
            
            else
            {

                CancelInsurance(insuranceType);
            }
        }
        public void CancelInsurance(string insType)//即保险指令，如B04
        {
            //保单作废服务，userid,保单号,insType

            if (MessageBox.Show("确定作废保单" + tb保单序号.Text.Trim() + "吗？", "注意", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            if (insuranceType == "B09" || insuranceType == "B0D")
            {
                string caseno = tb保单序号.Text.Trim();
                EP.WebService epws = new EP.WebService();
                EP.WebServiceReturnEntity epret = new EP.WebServiceReturnEntity();
                epret = epws.DiscardIt(GlobalVar2.bxUserAccount, GlobalVar2.bxPassWord, caseno);
                if (epret.Enabled)
                {
                    MessageBox.Show(caseno + " 作废成功");
                }
                else
                {
                    MessageBox.Show(caseno + " 作废失败:" + epret.ErrorMsg);
                }
            }
            else
            {
                if (WebService.CancelInsurance(GlobalVar.loginName, insType, tb保单序号.Text))
                    MessageBox.Show("作废成功");
                else
                    MessageBox.Show("作废失败");
            }

        }
        public string xmlname = "";
        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            if (xmlname == "")
            {
                MessageBox.Show("未设置保险配置的xmlname");
                return;
            }
            Default yd = new Default();
            yd.xmlFirst = xmlname;
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_insurance cfg = new cfg_insurance();
                cfg.GetConfig(xmlname);
                eNumberHead = cfg.ENumberHead;
                tb经办人.Text = cfg.Signature;
                numericUpDown1.Value = cfg.OffsetX;
                numericUpDown2.Value = cfg.OffsetY;
                tb保单序号.Text = cfg.SaveNo;
            }
        }
        private void dtp保险起始时间_ValueChanged(object sender, EventArgs e)
        {
            
        }
        public bool bLianxu = false;
        string bLastString = "";
        private void bt_Print_Click(object sender, EventArgs e)
        {
            if (dtp保险起始时间.Value < DateTime.Today)
            {
                MessageBox.Show("保险起始日期不能早于今日！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (DateTime.Parse(tb乘机日.Text.Trim()) < DateTime.Today)
                {
                    MessageBox.Show("乘机日期不能早于当前日期！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("乘机日期填写格式有误，请核对！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bLastString == cb被保险人姓名.Text && cb被保险人姓名.Text!="")
            {
                if (MessageBox.Show(bLastString + "->已经打印，是否再打印一张？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            //bt_Print.Enabled = false;
            Application.DoEvents();
            try
            {
                bLianxu = false;
                try
                {
                    save_cfg();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message + "save_cfg()");
                }
                if (insuranceNumberLength == 0)
                {
                    MessageBox.Show("未设置对应险种的保单序号长度insuranceNumberLength");
                    return;
                }
                if (tb保单序号.Text.Trim().Length != insuranceNumberLength)
                {
                    if (insuranceType == "B09" || insuranceType =="B0D") ;
                    else
                    {
                        MessageBox.Show("保单序号长度错误，" + insuranceNumberLength.ToString() + "位");
                        return;
                    }
                }
                try
                {
                    long.Parse(tb保单序号.Text.Trim());
                }
                catch
                {
                    if ((insuranceType == "B09") || (insuranceType == "B0D")) ;
                    else
                    {
                        MessageBox.Show("保单序号只能为数字");

                        return;
                    }
                }
                if (cb被保险人姓名.Text.Trim() == "")
                {
                    MessageBox.Show("姓名不能为空");
                    return;
                }
                if (tb证件号.Text.Trim() == "")
                {
                    MessageBox.Show("证件号码不能为空");
                    return;
                }
                try
                {
                    DateTime dtTemp = DateTime.Parse(tb乘机日.Text.Trim());
                    if (dtTemp.Year != DateTime.Now.Year)
                    {
                        if (MessageBox.Show("乘机日期:" + tb乘机日.Text + "   请确认!", "注意", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
                catch
                {
                    if (bhyx)
                    {
                        MessageBox.Show("乘机日期格式错误，如2007-4-2");
                        return;
                    }
                }
                tb保单号码.Text = eNumberHead + EagleAPI.GetRandom62();
                if (insuranceType == "B08") tb保单号码.Text = eNumberHead + DateTime.Now.Date.ToString("yyyyMMdd") + tb保单序号.Text;
                if (paperHeight == 0 || paperWidth == 0)
                {
                    MessageBox.Show("未设置打印纸的宽与高paperHeight,paperWidth");
                    return;
                }
                if (!GlobalVar.b_OffLine)
                {
                    if (cb被保险人姓名.Text.Trim() != GlobalVar.HYXTESTPRINT)
                    {
                        HyxStructs hs = new HyxStructs();
                        hs.UserID = GlobalVar.loginName;
                        hs.eNumber = tb保单号码.Text;
                        hs.IssueNumber = tb保单序号.Text;
                        hs.NameIssued = cb被保险人姓名.Text;
                        hs.CardType = "航班号" + tb航班号.Text + "乘机日" + tb乘机日.Text; ;
                        hs.CardNumber = tb证件号.Text;
                        hs.Remark = insuranceType; //保险种类别名代码B06
                        hs.IssuePeriod = "";
                        hs.IssueBegin = (bhyx ? tb乘机日.Text : dtp保险起始时间.Value.ToString());//必须为时间串
                        hs.IssueEnd = (bhyx ? tb乘机日.Text : dtp保险终止时间.Value.ToString());//必须为时间串
                        hs.SolutionDisputed = "";
                        hs.NameBeneficiary = tb受益人资料.Text + tb受益人关系.Text;
                        hs.Signature = tb经办人.Text;// tbSignatureDate.Text;
                        hs.SignDate = tb填开日期.Text;//dtp_Date.Value.ToShortDateString();
                        hs.InssuerName = "";
                        hs.Pnr = tbPnr.Text;

                        bSubmitting = true;
                        bt_Print.Text = "提交中………………请稍等";
                        Application.DoEvents();
                        //while (this.insuranceType=="B07" && GlobalVar.serverAddr== GlobalVar.ServerAddr.HangYiWang)
                        //{
                        //    try
                        //    {
                        //        string ddd = "adksfj";
                        //        DateTime eee = DateTime.Parse(ddd);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        MessageBox.Show(ex.Message);
                        //    }
                        //}
                        if (insuranceType == "B09" || insuranceType =="B0D")
                        {
                            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                            NewPara np = new NewPara();
                            np.AddPara("cm", "SubmitEagleIns");
                            np.AddPara("UserAccount", "testaccount");
                            np.AddPara("UserPassword", "testpassword");
                            np.AddPara("CardIdNumber", "身份证号");
                            np.AddPara("FlightNumber", "航班号");
                            np.AddPara("FlightDate", "乘机日");
                            np.AddPara("BenefitRelation", "受益人关系");
                            np.AddPara("BenefitZiliao", "受益人资料");
                            np.AddPara("Telephone", "电话号码");
                            np.AddPara("Name", "被保险人名");
                            np.AddPara("PrintHead", "台头"+insuranceType);


                            string strReq = np.GetXML();
                            string strRet = ws.getEgSoap(strReq);
                            EP.WebService epws = new EP.WebService();
                            EP.WebServiceReturnEntity epret = new EP.WebServiceReturnEntity();
                            if (insuranceType == "B09")
                                epret = epws.Purchase(GlobalVar2.bxUserAccount, GlobalVar2.bxPassWord,
                                    lb公司名称.Text,
                                    DateTime.Parse(tb乘机日.Text), tb航班号.Text, tb证件号.Text, cb被保险人姓名.Text, GlobalVar2.bxTelephone,
                                    tb受益人关系.Text, tb受益人资料.Text);
                            else epret = epws.PurchasePICC(GlobalVar2.bxUserAccount, GlobalVar2.bxPassWord,
                                lb公司名称.Text,
                                DateTime.Parse(tb乘机日.Text), tb航班号.Text, tb证件号.Text, cb被保险人姓名.Text, GlobalVar2.bxTelephone,
                                tb受益人关系.Text, tb受益人资料.Text);
                            bt_Print.Text = "打印(&P)";
                            if (!epret.Enabled)
                            {
                                MessageBox.Show(epret.ErrorMsg);
                                return;
                            }
                            else
                            {//打印
                                tb保单号码.Text = epret.SerialNo;//微机码
                                tb保单序号.Text = epret.CaseNo;//单证号码
                                tb经办人.Text = epret.AgentName;//加盟商明称
                            }
                        }
                        else
                        {
                            if (!hs.SubmitInfo())
                            {
                                bSubmitting = false;
                                bt_Print.Text = "打印(&P)";
                                //MessageBox.Show("数据提交失败！请检查保单号是否已被使用，或网络是否正常！");
                                return;
                            }
                        }
                        bt_Print.Text = "打印(&P)";
                        bSubmitting = false;
                    }
                }
                PrintDialog pd = new PrintDialog();
                EagleAPI.PrinterSetupCostom(ptDoc, paperWidth, paperHeight);
                pd.Document = ptDoc;
                //DialogResult dr = pd.ShowDialog();

                //if (dr == DialogResult.OK)
                {
                    ptDoc.Print();
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            bLastString = cb被保险人姓名.Text;
            bt_Print.Enabled = true;
        }
        void save_cfg()
        {
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("Phone");
            xn.InnerText = tb报案电话.Text;

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("Signature");
            xn.InnerText = tb经办人.Text;

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("CompanyAddr");
            xn.InnerText = tb填开单位.Text;

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("Term");
            xn.InnerText = dtp保险终止时间.Value.CompareTo(dtp保险起始时间.Value).ToString();

            //xn = xd.SelectSingleNode("eg");
            //xn = xn.SelectSingleNode("PingAn01");
            //xn = xn.SelectSingleNode("Signature");
            //xn.InnerText = tb_Signature.Text;

            xd.Save(GlobalVar.s_configfile);
        }

        private void tbTestPrint_Click(object sender, EventArgs e)
        {
            cb被保险人姓名.Text = GlobalVar.HYXTESTPRINT;
            bt_Print_Click(sender, e);
        }

        private void Insurance_MouseClick(object sender, MouseEventArgs e)
        {
            rightMenuHYX menu = new rightMenuHYX(this);
            menu.ShowHYXMenu(this, e);
            
        }
        public List<string> ls = null;
        private void btPrintLianXu_Click(object sender, EventArgs e)
        {
            if (tb航班号.Text.Trim() == "" || tb乘机日.Text.Trim() == "")
            {
                MessageBox.Show("请先输入航班及日期"); return;
            }
            bLianxu = true;
            string[] names = new string[cb被保险人姓名.Items.Count];
            string[] cardids = new string[cb被保险人姓名.Items.Count];
            string[] policynos = new string[cb被保险人姓名.Items.Count];
            Options.PrintBaoXianLianXu pb = null;
            if (cb被保险人姓名.Items.Count < 1)
                pb = new Options.PrintBaoXianLianXu();
            else
            {
                for (int i = 0; i < names.Length; i++)
                {
                    try
                    {
                        names[i] = cb被保险人姓名.Items[i].ToString();
                        cardids[i] = EagleAPI.GetIDCardNo(retstring)[i];
                        long no = (long.Parse(tb保单序号.Text.Trim()) + (long)i);
                        policynos[i] = no.ToString("D" + insuranceNumberLength.ToString());
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message+"\r\n\r\n提示：只有在能自动提取出身份证的情况下可正常连续打印");
                        return;
                    }
                }
                pb = new Options.PrintBaoXianLianXu(names, cardids, policynos);
            }
            pb.insLength = insuranceNumberLength;
            if (pb.insLength == 0)
            {
                MessageBox.Show("未设置保单长度insuranceNumberLength");
                return;
            }

            if (pb.ShowDialog() != DialogResult.OK) return;
            if (pb.ls == null || pb.ls.Count < 1) return;
            ls = new List<string>(pb.ls);
            pb.Dispose();
            PrintDialog pd = new PrintDialog();
            EagleAPI.PrinterSetupCostom(ptDoc, paperWidth, paperHeight);
            pd.Document = ptDoc;
            //DialogResult dr = pd.ShowDialog();

            //if (dr == DialogResult.OK)
            {
                ptDoc.Print();
            }
        }

        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }
        public int iPage = 0;
        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            iPage = 0;
            try
            {
                long it = long.Parse(tb保单序号.Text.Trim()) + 1L;
                tb保单序号.Text = it.ToString("D" + insuranceNumberLength);//7位平安周游列国会员保险信息卡
            }
            catch
            {
                if (insuranceType == "B09" || insuranceType == "B0D") ;
                else MessageBox.Show("保单号只能为数值");
            }
            FileStream fs = new FileStream(GlobalVar.s_configfile, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);
            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode(xmlname);
            xn = xn.SelectSingleNode("SaveNo");
            xn.InnerText = tb保单序号.Text;

            xd.Save(GlobalVar.s_configfile);
        }
        bool bSubmitting = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bSubmitting)
            {
                bt_Print.Text = "数据传输中……";
                Application.DoEvents();
            }
            else
            {
                bt_Print.Text = "打印(&P)";
            }
        }

        private void Insurance_Activated(object sender, EventArgs e)
        {
            context = this;
        }


    }
}