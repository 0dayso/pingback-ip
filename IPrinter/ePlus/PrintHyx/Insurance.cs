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
            //tb������.Text = cc.Signature;
            //numericUpDown1.Value = cc.OffsetX;
            //numericUpDown2.Value = cc.OffsetY;
            //tb�������.Text = cc.SaveNo;
            //tb�����绰.Text = cc.Phone;
            //tb���λ.Text = cc.CompanyAddr;

            //tb�����.Text = System.DateTime.Now.ToString();
            
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
                MessageBox.Show("δ����xmlname");
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
    <Signature>������1</Signature>
    <OffsetX>1</OffsetX>
    <OffsetY>0</OffsetY>
    <SaveNo>0123456789</SaveNo>
    <CompanyAddr>���λ1</CompanyAddr>
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
            cb������������.Items.Clear();
            GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.none;
            if (rb1.Checked)
            {
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                    PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cb������������, e, ref retstring);
                else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                {
                    if (e.KeyValue == 13)
                        rtPnr();
                }
            }
            if (rb3.Checked && e.KeyValue == 13)
            {
                string piaohao = tbPnr.Text.Trim().Replace("-", "").Replace(" ", "");
                if (piaohao.Length != 13) { MessageBox.Show("Ʊ�Ŵ���"); return; }
                piaohao = piaohao.Substring(0, 3) + "-" + piaohao.Substring(3);
                string cmd = "detr:tn/" + piaohao;

                PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cb������������, e, ref retstring, cmd);
            }
            if (rb2.Checked)
            {
                if (e.KeyValue == 13)
                {
                    AirCode ac = new AirCode(tbPnr.Text);
                    ac.ShowDialog();
                    if (ac.airCode != "") tbPnr.Text = ac.airCode;
                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                        PrintHyx.PrintHyxPublic.PnrTextBoxKeyUp(tbPnr, cb������������, e, ref retstring);
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
            if (rtXml == "") { cb������������.Text = "PNR������PNR��ȡ��"; return; }
            Options.ibe.IbeRt ir = new Options.ibe.IbeRt (rtXml);
            string[] names = ir.getpeopleinfo(0);
            cb������������.Items.AddRange(names);
            cb������������.Text = names[0];
            tb֤����.Text = ir.getpeopleinfo(1)[0];
            string[] segments = ir.getflightsegsinfo();
            tb�����.Text = "";
            for (int i = 0; i < segments.Length; i++)
            {
                string[] fi = segments[i].Split('~');
                tb�����.Text += fi[0];
                if (i == 0)
                {
                    System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                    DateTime dt = DateTime.ParseExact(fi[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                    tb�˻���.Text = dt.ToString();// dt.ToShortDateString();
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
                    context.tb֤����.Text = EagleAPI.GetCardIdByDetr_F(retstringDetrF);
                    return;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
            }
            context.cb������������.Items.Clear();
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
                        context.cb������������.Items.Add(names[i]);
                    }
                    context.cb������������.Text = context.cb������������.Items[0].ToString();
                    context.tb֤����.Text = EagleAPI.GetIDCardNo(retstring)[0];
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

                    context.tb�����.Text = EagleAPI.GetFlightNo(retstring) + EagleAPI.GetFlightNo2(retstring);
                    context.tb�˻���.Text = dt.ToShortDateString();
                    context.dtp������ʼʱ��.Value = DateTime.Parse(dt.ToShortDateString());
                }
                if (context.rb3.Checked)
                {
                    ePlus.eTicket.etInfomation ei = new ePlus.eTicket.etInfomation();
                    ei.SetVar(retstring);
                    context.cb������������.Items.Add(ei.PASSENGER);
                    context.cb������������.Text = ei.PASSENGER;
                    context.tb֤����.Text = "";
                    context.tb�����.Text = EagleAPI.substring(ei.FROM, 4, 2) + EagleAPI.substring(ei.FROM, 10, 4);
                    string date = EagleAPI.substring(ei.FROM, 18, 5);
                    int imm = int.Parse(EagleAPI.GetMonthInt(date.Substring(date.Length - 3)));
                    int idd = int.Parse(date.Substring(date.Length - 5).Substring(0, 2));
                    int iyy = System.DateTime.Now.Year;
                    System.DateTime dt = new DateTime(iyy, imm, idd, 23, 59, 59);
                    while (dt < System.DateTime.Now)
                    {
                        dt = dt.AddYears(1);
                    }
                    context.tb�˻���.Text = dt.ToShortDateString();

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

        private void cb������������_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb������������.SelectedItem != null)
                {
                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                        tb֤����.Text = EagleAPI.GetIDCardNo(retstring)[cb������������.SelectedIndex];
                    else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                    {
                        Options.ibe.IbeRt ir = new Options.ibe.IbeRt(rtXml);
                        if (rtXml == "") { MessageBox.Show("����ȡPNR"); return; }
                        tb֤����.Text = ir.getpeopleinfo(1)[cb������������.SelectedIndex];
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ѡ������ʱ��" + ex.Message);
            }
        }

        private void btȡ���֤��_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                {
                    GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.detrF;
                    EagleAPI.CLEARCMDLIST(3);
                    string etnumber = rb3.Checked ? tbPnr.Text : EagleAPI.GetETNumber(retstring)[cb������������.SelectedIndex].Replace(' ', '-');
                    //EagleAPI.EagleSendCmd("detr:tn/" + etnumber + ",f");
                    tb֤����.Text = "���Եȡ�������";
                }
                else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
                {
                    GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.detrF;
                    EagleAPI.CLEARCMDLIST(3);
                    Options.ibe.IbeRt ir = new Options.ibe.IbeRt(rtXml);
                    string etnumber = ir.getpeopleinfo(2)[cb������������.SelectedIndex];//���ﲻһ��(IBE������ô�죿)
                    EagleAPI.EagleSendCmd("detr:tn/" + etnumber + ",f");
                    tb֤����.Text = "���Եȡ�������";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "��������ȡPNR��Ʊ�ţ�");
            }
        }

        private void bt���ϱ���_Click(object sender, EventArgs e)
        {
            if (insuranceType == "")
            {
                MessageBox.Show("δ���ñ���������insuranceType");
                return;
            }
            
            else
            {

                CancelInsurance(insuranceType);
            }
        }
        public void CancelInsurance(string insType)//������ָ���B04
        {
            //�������Ϸ���userid,������,insType

            if (MessageBox.Show("ȷ�����ϱ���" + tb�������.Text.Trim() + "��", "ע��", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            if (insuranceType == "B09" || insuranceType == "B0D")
            {
                string caseno = tb�������.Text.Trim();
                EP.WebService epws = new EP.WebService();
                EP.WebServiceReturnEntity epret = new EP.WebServiceReturnEntity();
                epret = epws.DiscardIt(GlobalVar2.bxUserAccount, GlobalVar2.bxPassWord, caseno);
                if (epret.Enabled)
                {
                    MessageBox.Show(caseno + " ���ϳɹ�");
                }
                else
                {
                    MessageBox.Show(caseno + " ����ʧ��:" + epret.ErrorMsg);
                }
            }
            else
            {
                if (WebService.CancelInsurance(GlobalVar.loginName, insType, tb�������.Text))
                    MessageBox.Show("���ϳɹ�");
                else
                    MessageBox.Show("����ʧ��");
            }

        }
        public string xmlname = "";
        private void bt_SetToDefault_Click(object sender, EventArgs e)
        {
            if (xmlname == "")
            {
                MessageBox.Show("δ���ñ������õ�xmlname");
                return;
            }
            Default yd = new Default();
            yd.xmlFirst = xmlname;
            if (DialogResult.OK == yd.ShowDialog())
            {
                cfg_insurance cfg = new cfg_insurance();
                cfg.GetConfig(xmlname);
                eNumberHead = cfg.ENumberHead;
                tb������.Text = cfg.Signature;
                numericUpDown1.Value = cfg.OffsetX;
                numericUpDown2.Value = cfg.OffsetY;
                tb�������.Text = cfg.SaveNo;
            }
        }
        private void dtp������ʼʱ��_ValueChanged(object sender, EventArgs e)
        {
            
        }
        public bool bLianxu = false;
        string bLastString = "";
        private void bt_Print_Click(object sender, EventArgs e)
        {
            if (dtp������ʼʱ��.Value < DateTime.Today)
            {
                MessageBox.Show("������ʼ���ڲ������ڽ��գ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (DateTime.Parse(tb�˻���.Text.Trim()) < DateTime.Today)
                {
                    MessageBox.Show("�˻����ڲ������ڵ�ǰ���ڣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("�˻�������д��ʽ������˶ԣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bLastString == cb������������.Text && cb������������.Text!="")
            {
                if (MessageBox.Show(bLastString + "->�Ѿ���ӡ���Ƿ��ٴ�ӡһ�ţ�", "����", MessageBoxButtons.YesNo) == DialogResult.No)
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
                    MessageBox.Show("δ���ö�Ӧ���ֵı�����ų���insuranceNumberLength");
                    return;
                }
                if (tb�������.Text.Trim().Length != insuranceNumberLength)
                {
                    if (insuranceType == "B09" || insuranceType =="B0D") ;
                    else
                    {
                        MessageBox.Show("������ų��ȴ���" + insuranceNumberLength.ToString() + "λ");
                        return;
                    }
                }
                try
                {
                    long.Parse(tb�������.Text.Trim());
                }
                catch
                {
                    if ((insuranceType == "B09") || (insuranceType == "B0D")) ;
                    else
                    {
                        MessageBox.Show("�������ֻ��Ϊ����");

                        return;
                    }
                }
                if (cb������������.Text.Trim() == "")
                {
                    MessageBox.Show("��������Ϊ��");
                    return;
                }
                if (tb֤����.Text.Trim() == "")
                {
                    MessageBox.Show("֤�����벻��Ϊ��");
                    return;
                }
                try
                {
                    DateTime dtTemp = DateTime.Parse(tb�˻���.Text.Trim());
                    if (dtTemp.Year != DateTime.Now.Year)
                    {
                        if (MessageBox.Show("�˻�����:" + tb�˻���.Text + "   ��ȷ��!", "ע��", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
                catch
                {
                    if (bhyx)
                    {
                        MessageBox.Show("�˻����ڸ�ʽ������2007-4-2");
                        return;
                    }
                }
                tb��������.Text = eNumberHead + EagleAPI.GetRandom62();
                if (insuranceType == "B08") tb��������.Text = eNumberHead + DateTime.Now.Date.ToString("yyyyMMdd") + tb�������.Text;
                if (paperHeight == 0 || paperWidth == 0)
                {
                    MessageBox.Show("δ���ô�ӡֽ�Ŀ����paperHeight,paperWidth");
                    return;
                }
                if (!GlobalVar.b_OffLine)
                {
                    if (cb������������.Text.Trim() != GlobalVar.HYXTESTPRINT)
                    {
                        HyxStructs hs = new HyxStructs();
                        hs.UserID = GlobalVar.loginName;
                        hs.eNumber = tb��������.Text;
                        hs.IssueNumber = tb�������.Text;
                        hs.NameIssued = cb������������.Text;
                        hs.CardType = "�����" + tb�����.Text + "�˻���" + tb�˻���.Text; ;
                        hs.CardNumber = tb֤����.Text;
                        hs.Remark = insuranceType; //���������������B06
                        hs.IssuePeriod = "";
                        hs.IssueBegin = (bhyx ? tb�˻���.Text : dtp������ʼʱ��.Value.ToString());//����Ϊʱ�䴮
                        hs.IssueEnd = (bhyx ? tb�˻���.Text : dtp������ֹʱ��.Value.ToString());//����Ϊʱ�䴮
                        hs.SolutionDisputed = "";
                        hs.NameBeneficiary = tb����������.Text + tb�����˹�ϵ.Text;
                        hs.Signature = tb������.Text;// tbSignatureDate.Text;
                        hs.SignDate = tb�����.Text;//dtp_Date.Value.ToShortDateString();
                        hs.InssuerName = "";
                        hs.Pnr = tbPnr.Text;

                        bSubmitting = true;
                        bt_Print.Text = "�ύ�С��������������Ե�";
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
                            np.AddPara("CardIdNumber", "���֤��");
                            np.AddPara("FlightNumber", "�����");
                            np.AddPara("FlightDate", "�˻���");
                            np.AddPara("BenefitRelation", "�����˹�ϵ");
                            np.AddPara("BenefitZiliao", "����������");
                            np.AddPara("Telephone", "�绰����");
                            np.AddPara("Name", "����������");
                            np.AddPara("PrintHead", "̨ͷ"+insuranceType);


                            string strReq = np.GetXML();
                            string strRet = ws.getEgSoap(strReq);
                            EP.WebService epws = new EP.WebService();
                            EP.WebServiceReturnEntity epret = new EP.WebServiceReturnEntity();
                            if (insuranceType == "B09")
                                epret = epws.Purchase(GlobalVar2.bxUserAccount, GlobalVar2.bxPassWord,
                                    lb��˾����.Text,
                                    DateTime.Parse(tb�˻���.Text), tb�����.Text, tb֤����.Text, cb������������.Text, GlobalVar2.bxTelephone,
                                    tb�����˹�ϵ.Text, tb����������.Text);
                            else epret = epws.PurchasePICC(GlobalVar2.bxUserAccount, GlobalVar2.bxPassWord,
                                lb��˾����.Text,
                                DateTime.Parse(tb�˻���.Text), tb�����.Text, tb֤����.Text, cb������������.Text, GlobalVar2.bxTelephone,
                                tb�����˹�ϵ.Text, tb����������.Text);
                            bt_Print.Text = "��ӡ(&P)";
                            if (!epret.Enabled)
                            {
                                MessageBox.Show(epret.ErrorMsg);
                                return;
                            }
                            else
                            {//��ӡ
                                tb��������.Text = epret.SerialNo;//΢����
                                tb�������.Text = epret.CaseNo;//��֤����
                                tb������.Text = epret.AgentName;//����������
                            }
                        }
                        else
                        {
                            if (!hs.SubmitInfo())
                            {
                                bSubmitting = false;
                                bt_Print.Text = "��ӡ(&P)";
                                //MessageBox.Show("�����ύʧ�ܣ����鱣�����Ƿ��ѱ�ʹ�ã��������Ƿ�������");
                                return;
                            }
                        }
                        bt_Print.Text = "��ӡ(&P)";
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
            bLastString = cb������������.Text;
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
            xn.InnerText = tb�����绰.Text;

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("OffsetX");
            xn.InnerText = numericUpDown1.Value.ToString();

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("OffsetY");
            xn.InnerText = numericUpDown2.Value.ToString();

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("Signature");
            xn.InnerText = tb������.Text;

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("CompanyAddr");
            xn.InnerText = tb���λ.Text;

            xn = xd.SelectSingleNode("eg").SelectSingleNode(xmlname).SelectSingleNode("Term");
            xn.InnerText = dtp������ֹʱ��.Value.CompareTo(dtp������ʼʱ��.Value).ToString();

            //xn = xd.SelectSingleNode("eg");
            //xn = xn.SelectSingleNode("PingAn01");
            //xn = xn.SelectSingleNode("Signature");
            //xn.InnerText = tb_Signature.Text;

            xd.Save(GlobalVar.s_configfile);
        }

        private void tbTestPrint_Click(object sender, EventArgs e)
        {
            cb������������.Text = GlobalVar.HYXTESTPRINT;
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
            if (tb�����.Text.Trim() == "" || tb�˻���.Text.Trim() == "")
            {
                MessageBox.Show("�������뺽�༰����"); return;
            }
            bLianxu = true;
            string[] names = new string[cb������������.Items.Count];
            string[] cardids = new string[cb������������.Items.Count];
            string[] policynos = new string[cb������������.Items.Count];
            Options.PrintBaoXianLianXu pb = null;
            if (cb������������.Items.Count < 1)
                pb = new Options.PrintBaoXianLianXu();
            else
            {
                for (int i = 0; i < names.Length; i++)
                {
                    try
                    {
                        names[i] = cb������������.Items[i].ToString();
                        cardids[i] = EagleAPI.GetIDCardNo(retstring)[i];
                        long no = (long.Parse(tb�������.Text.Trim()) + (long)i);
                        policynos[i] = no.ToString("D" + insuranceNumberLength.ToString());
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message+"\r\n\r\n��ʾ��ֻ�������Զ���ȡ�����֤������¿�����������ӡ");
                        return;
                    }
                }
                pb = new Options.PrintBaoXianLianXu(names, cardids, policynos);
            }
            pb.insLength = insuranceNumberLength;
            if (pb.insLength == 0)
            {
                MessageBox.Show("δ���ñ�������insuranceNumberLength");
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
                long it = long.Parse(tb�������.Text.Trim()) + 1L;
                tb�������.Text = it.ToString("D" + insuranceNumberLength);//7λƽ�������й���Ա������Ϣ��
            }
            catch
            {
                if (insuranceType == "B09" || insuranceType == "B0D") ;
                else MessageBox.Show("������ֻ��Ϊ��ֵ");
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
            xn.InnerText = tb�������.Text;

            xd.Save(GlobalVar.s_configfile);
        }
        bool bSubmitting = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bSubmitting)
            {
                bt_Print.Text = "���ݴ����С���";
                Application.DoEvents();
            }
            else
            {
                bt_Print.Text = "��ӡ(&P)";
            }
        }

        private void Insurance_Activated(object sender, EventArgs e)
        {
            context = this;
        }


    }
}