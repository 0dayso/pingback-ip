#define RECEIPT
#define newCheck
#define receipt_//�г̵���ӡ������ʹ��
#define receipt2_  //receipt���Ϊ���´�ӡ
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Threading;


namespace ePlus
{
    //etdz�󣬶�ʱװ�ã��˵����������ӡʱ���ĸ���ʽ�����˴˴���
    //����ǰ����Ӧ�����ں�ִ̨��
    public partial class PrintReceipt : Form
    {
        public List<string> eTicketNumbersOfSubmitted = new List<string>();

        public bool backGroundSubmitEticket = false;
        static public bool b0007 = false;//��־�Ƿ�Ϊ��̨�������Ǻ�̨��ֻ��Ϊһ������
        static public bool b0003 = false;//��־�Ƿ���˴�ӡ����
        public string bigCode = ""; 

        bool canLinaxuPrint = false;

        private bool b_MethodGetWithPNR = true;
        public static string combox_string = "";
        static public string retstringDetrF = ""; 
        static public string retstring = "";
        public PrintReceipt()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }//13065899281����
        public bool b_ETDZSUCC = true;

        public static bool opened = false;
        public static bool IsPrinting = false;
        public static bool IsRemoving = false;

        string rtXml = "";

        static string strCurrentPrintCommand = "";//��ǰ���͵Ĵ�ӡָ��Eric
        static int intPrintCount = 0;//ͳ�������ӡ���ɹ��ط��˼���Eric
        static string strPrintInfo = "";//��¼��ǰ��ӡ�ĵ��Ժţ���Ʊ�ţ��г̵���Eric
        private void PrintReceipt_Load(object sender, EventArgs e)
        {
            cbIbe.Checked = BookTicket.bIbe;
            try
            {
                if (GlobalVar.printProgram)
                {
                    this.btBatCancel.Visible = false;
                    this.button_����.Visible = false;
                    this.button_��ӡ.Visible = false;
                }

                init_restrictions();
                connect_4_Command.PrintWindowOpen = true;
                opened = true;
                Context = this;
                retstring = "";

                textBox_�����.Text = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString("D2") + "-" + System.DateTime.Now.Day.ToString("D2");
                //ActiveControl = button1;
                ActiveControl = textBox_������;

                eTicket.AutoSetReceiptNumber asn = new ePlus.eTicket.AutoSetReceiptNumber();
                textBox_ӡˢ���.Text = asn.GetReceiptNumberFromXML();
                textBox_���λ.Text = asn.GetReceiptSignatureFromXML();
                tbSaleCode2.Text = asn.GetReceiptSaleCodeFromXML();
                textBox_���۵�λ����.Text = asn.GetXiaoShouDaiHao(EagleAPI.substring(GlobalVar.str_cfg_name, 0, 6));
                textBox_��֤��.Text = EagleAPI.substring(textBox_ӡˢ���.Text.Trim(), textBox_ӡˢ���.Text.Trim().Length - 4, 4);

                btOffLinePrint.Enabled = Model.md.b_00A;//�ѻ���ӡ��ť

                b_submitinfo_window = (Text == "���ӿ�Ʊȷ�ϼ���Ϣ�ύ");
                b_checkinfo_window = (Text == "���ӿ�Ʊ��̨�˲����");
                btPause.Visible = false;
                //if (b_submitinfo_window) { button1.Enabled = false; FormBorderStyle = FormBorderStyle.None; WindowState = FormWindowState.Minimized; ShowInTaskbar = true; timer1.Start(); }
                if (b_submitinfo_window)
                {
                    button1.Enabled = false; 
                    FormBorderStyle = FormBorderStyle.None; 
                    WindowState = FormWindowState.Minimized; 
                    ShowInTaskbar = true; btGetPnrFake();
                }
                if (b_checkinfo_window) { /*timer3.Start();*/ btPause.Visible = true; btGetPNR_Click(sender, e); }
                if (b_submitinfo_window || b_checkinfo_window) ;// timerProgress.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PrintReceipt_Load:" + ex.Message);
            }
#if receipt
            btOffLinePrint.Enabled = true;
            btBatCancel.Visible = false;
            btLianxuPrint.Visible = false;
            button_����.Visible = false;
#endif
            if (GlobalVar.printProgram) this.btOffLinePrint.Enabled = true;
            if (this.textBox_���λ.Text == "�人����Ʊ��" && GlobalVar.serverAddr!= GlobalVar.ServerAddr.Eagle) this.textBox_���λ.Text = "";
        }
        bool b_submitinfo_window = false;
        bool b_checkinfo_window = false;
        //��ʼ��ǩע��
        private void init_restrictions()
        {
            if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang) this.btZzPrintBus.Visible = true;

            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\restictions.mp3", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));

                string sline = sr.ReadLine();
                while (sline != null)
                {
                    comboBox_ǩע.Items.Add(sline);
                    sline = sr.ReadLine();
                }
                if (comboBox_ǩע.Items.Count > 0)
                    comboBox_ǩע.SelectedIndex = 0;
                sr.Close();
                fs.Close();
            }
            catch
            {
            }

        }

        /// <summary>
        /// ά��ǩע��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            //RunProgram(Environment.SystemDirectory + "\\notepad.exe restictions.mp3");
            PrintTicket.RunProgram(Environment.SystemDirectory + "\\notepad.exe", path + "\\restictions.mp3");
        }

        private void button_�˳�_Click(object sender, EventArgs e)
        {
            Close();
        }


        public static PrintReceipt Context = null;
        public static string ReturnString
        {
            set
            {
                
                if (Context != null)
                {
                    if (GlobalVar.formSendCmdType == GlobalVar.FormSendCommandType.detrF)
                    {
                        try
                        {
                            retstringDetrF = connect_4_Command.AV_String;
                            rs = retstringDetrF;
                        }
                        catch
                        {
                        }
                        GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.none;
                        return;
                    }
                    //׷�ӽ��
                    retstring += "\n" + connect_4_Command.AV_String;
                    
                    //no pnr
                    if (retstring.Split('\n').Length < 4)
                    {
                        if(!(IsPrinting || IsRemoving))
                            if(!Context.b_checkinfo_window)
                                MessageBox.Show("NO PNR");
                        return;
                    }
                    //��ҳ
                    //if (retstring.IndexOf('+')>-1)// == "+\r\n")
                    string plustemp = mystring.trim(retstring);
                    if (plustemp.Length > 0 && plustemp[plustemp.Length - 1] == '+' && retstring.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") < 0)
                    {
                        //����PNָ��
                        retstring = retstring.Replace('+', ' ');
                        if (frmMain.st_tabControl.InvokeRequired)
                        {
                            EventHandler eh;// = new EventHandler(Context.backGroundSubmitEticket?PrintTicket.sendpn7:PrintTicket.sendpn);
                            if (Context.backGroundSubmitEticket) eh = new EventHandler(PrintTicket.sendpn7);
                            else eh = new EventHandler(PrintTicket.sendpn);
                            TabControl tc = frmMain.st_tabControl;
                            frmMain.st_tabControl.Invoke(eh, new object[] { tc, EventArgs.Empty });
                        }
                        rsRichTextBox = retstring;
                    }
                    //if (retstring.Substring(retstring.Length - 3) != "+\r\n") rs = retstring;
                    else rs = retstring;
                }
                
            }
        }
        /// <summary>
        /// ������richtextbox1��ʵʱ��ʾ��������
        /// </summary>
        static public string rsRichTextBox
        {
            set
            {
                try
                {
                    if (Context.InvokeRequired)
                    {
                        EventHandler eh = new EventHandler(setrichtextbox);
                        PrintReceipt pt = PrintReceipt.Context;
                        PrintReceipt.Context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                    }
                }
                catch
                {

                }
            }
        }
        static private void setrichtextbox(object sender, EventArgs e)
        {
            Context.richTextBox1.Text = retstring + Context.richTextBox1.Text;
        }
        static public string rs
        {
            set
            {
                //��������retstring
                try
                {

                    //string temp = EagleAPI.GetNames(retstring)[0];
                    if (Context.InvokeRequired)
                    {
                        //ePlus.PrintTicket.Context.textBox_�ÿ�����.Text = temp;
                        // Context.textBox_ʼ��_Ŀ�ĵ�.Text = EagleAPI.GetStartCity(retstring) + "/" + EagleAPI.GetEndCity(retstring);
                        EventHandler eh = new EventHandler(setcontrol);
                        PrintReceipt pt = PrintReceipt.Context;
                        PrintReceipt.Context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                    }
                }
                catch (Exception ex)
                {
                    EagleAPI.LogWrite("�г̵����Ʋ��ܸ�ֵ!" + ex.Message);
                }
                finally
                {
                    //Context.Close();
                }
            }
        }
        static private void setcontrol(object sender, EventArgs e)
        {
            if (GlobalVar.formSendCmdType == GlobalVar.FormSendCommandType.detrF)
            {
                try
                {
                    Context.textBox_֤������.Text = EagleAPI.GetCardIdByDetr_F(retstringDetrF);
                    return;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
            }
            if (Model.md.b_0FM) Context.disableControls();
            Context.richTextBox1.Text = retstring + Context.richTextBox1.Text;
            if (Context.bGetPat)
            {
                Context.bGetPat = false;
                string fare = "";
                string tb = "";
                string tf = "";
                string total = "";
                try
                {
                    EagleAPI.GetFareFromPat(retstring, ref fare, ref tb, ref tf, ref total);
                    Context.textBox_Ʊ��.Text = "CNY" + fare.Substring(3);
                    Context.textBox_���������.Text = "CN" + tb.Substring(3);
                    Context.textBox_ȼ�ͷ�.Text = "YQ" + tf.Substring(3);
                    Context.textBox_ȼ�ͷ�_Leave(sender, e);
                    Context.tbBusinessUnitPrice.Text = Context.textBox_Ʊ��.Text + "  "
                        + Context.textBox_���������.Text + "  " + Context.textBox_ȼ�ͷ�.Text;
                    Context.tbBusinessTotal.Text = Context.textBox_�ϼ�.Text;
                }
                catch
                { }
                //8.�ύ���ӿ�Ʊ��Ϣ���ص�1(�ɷ��ؽ������)(������1)
                Context.button2_Click(sender, e);
                Context.btGetPNR_Click(sender, e);
                try
                {
                    Context.textBox_������.Text = GlobalVar2.tmpPrintPnr;
                }
                catch
                {
                    
                }
                return;
            }


            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            Context.textBox_������.Text = GlobalVar2.tmpPrintPnr;
            if (IsPrinting || IsRemoving) return;
#if !newCheck
            if (!EagleAPI.GetNoPnr(retstring,!Context.b_checkinfo_window))
            {
                Context.button2.Enabled = false;
                Context.button3.Enabled = true;
                return;
            }
#else
            //4.�����ǵ��ӿ�Ʊ��ȡ��,�ص�1(�ɷ��ؽ������)
            if (!EagleAPI.GetNoPnr(retstring, !Context.b_checkinfo_window))
            {
                Context.button3_Click(sender, e);//ȡ��
                if (!Context.bPause) Context.btGetPNR_Click(sender, e);//�ص�1
                return;
            }
#endif
            //������PNR��ȡ
            if (Context.b_MethodGetWithPNR)
            {
                #region
                Context.bigCode = EagleAPI.GetCodeBig(retstring);
                
                List<string> names = new List<string>();
                names = EagleAPI.GetNames(retstring);
                Context.listBox_������.Items.Clear();
                if (names.Count > 0) Context.textBox_�ÿ�����.Text = names[0];
                for (int i = 0; i < names.Count; i++)
                {
                    Context.listBox_������.Items.Add(names[i]);
                }

                Context.textBox101.Text = EagleAPI.GetStartCityCn(retstring);
                Context.textBox102.Text = EagleAPI.GetStartCity(retstring);
                Context.textBox103.Text = EagleAPI.GetCarrier(retstring);
                //Context.comboBox_���չ�˾
                //Context.textBox_���λ
                //string temp = Context.textBox103.Text;

                Context.textBox104.Text = EagleAPI.GetFlight(retstring);
                Context.textBox105.Text = EagleAPI.GetClass(retstring);
                Context.textBox106.Text = EagleAPI.GetDateStart(retstring);
                Context.textBox106.Text = EagleAPI.substring(Context.textBox106.Text, Context.textBox106.Text.Length - 5, 5);
                Context.textBox107.Text = EagleAPI.GetTimeStart(retstring);
                //Context.textBox108.Text = EagleAPI.GetAirLineBunkFareClass(Context.textBox103.Text, Context.textBox105.Text);
                try
                {
                    Context.textBox109.Text = (
                        EagleAPI.GetFC(retstring) == "" ?
                        EagleAPI.GetAirLineBunkFareClass
                        (Context.textBox103.Text, Context.textBox105.Text, DateTime.Parse
                        (System.DateTime.Now.Year.ToString() + "-" + EagleAPI.GetMonthInt(mystring.right(Context.textBox106.Text, 3)) + "-" +
                        Context.textBox106.Text.Substring(0, Context.textBox106.Text.Length - 3))) : EagleAPI.GetFC(retstring)
                        );
                }
                catch
                {
                    Context.textBox109.Text = (
    EagleAPI.GetFC(retstring) == "" ?
    EagleAPI.GetAirLineBunkFareClass
    (Context.textBox103.Text, Context.textBox105.Text, DateTime.Now
                 ) : EagleAPI.GetFC(retstring)
    );

                }

                Context.textBox201.Text = EagleAPI.GetEndCityCn(retstring);
                Context.textBox202.Text = EagleAPI.GetEndCity(retstring);

                Context.textBox203.Text = EagleAPI.GetCarrier2(retstring);
                //Context.comboBox_���չ�˾
                //Context.textBox_���λ
                //string temp = Context.textBox103.Text;

                Context.textBox204.Text = EagleAPI.GetFlight2(retstring);
                Context.textBox205.Text = EagleAPI.GetClass2(retstring);
                Context.textBox206.Text = EagleAPI.GetDateStart2(retstring);
                Context.textBox206.Text = EagleAPI.substring(Context.textBox206.Text, Context.textBox206.Text.Length - 5, 5);
                Context.textBox207.Text = EagleAPI.GetTimeStart2(retstring);

                if (Context.textBox203.Text != "")
                {
                    try
                    {
                        Context.textBox209.Text = (EagleAPI.GetFC(retstring) == "" ? EagleAPI.GetAirLineBunkFareClass(Context.textBox203.Text, Context.textBox205.Text, DateTime.Parse
                        (System.DateTime.Now.Year.ToString() + "-" + EagleAPI.GetMonthInt(mystring.right(Context.textBox206.Text, 3)) + "-" +
                        Context.textBox206.Text.Substring(0, Context.textBox206.Text.Length - 3))) : EagleAPI.GetFC(retstring));
                    }
                    catch
                    {
                        Context.textBox209.Text = (EagleAPI.GetFC(retstring) == "" ? EagleAPI.GetAirLineBunkFareClass(Context.textBox203.Text, Context.textBox205.Text, DateTime.Now) : EagleAPI.GetFC(retstring));

                    }
                }
                if (EagleAPI.HasSecond(retstring) > 0) Context.textBox20c.Text = "20K";

                Context.textBox302.Text = EagleAPI.GetEndCity2(retstring);
                Context.textBox301.Text = EagleAPI.GetCityCn(retstring, Context.textBox302.Text);

                if (EagleAPI.GetFare(retstring).Length > 3)
                    Context.textBox_Ʊ��.Text = "CNY" + EagleAPI.GetFare(retstring).Substring(3);
                if (EagleAPI.GetTaxBuild(retstring).Length > 3)
                    Context.textBox_���������.Text = "CN" + EagleAPI.GetTaxBuild(retstring).Substring(EagleAPI.GetTaxBuild(retstring)[3]>'9'?0:3);
                if (EagleAPI.GetTaxFuel(retstring).Length > 3)
                    Context.textBox_ȼ�ͷ�.Text = "YQ" + EagleAPI.GetTaxFuel(retstring).Substring(3);
                if (EagleAPI.GetTatol(retstring).Length > 3)
                    Context.textBox_�ϼ�.Text = "CNY" + EagleAPI.GetTatol(retstring).Substring(3);

                Context.tbBusinessUnitPrice.Text = Context.textBox_Ʊ��.Text + "  "
    + Context.textBox_���������.Text + "  " + Context.textBox_ȼ�ͷ�.Text;
                Context.tbBusinessTotal.Text = Context.textBox_�ϼ�.Text;


                Context.textBox_����.Text = string.Format("��{0}�ˣ�ѡ��{1}��", Context.listBox_������.Items.Count, Context.listBox_������.SelectedItems.Count);

                Context.textBox_֤������.Text = (EagleAPI.GetIDCardNo(retstring).Length != 0 ? EagleAPI.GetIDCardNo(retstring)[0] : "");
                Context.textBox_��Ʊ��.Text = (EagleAPI.GetETNumber(retstring).Length != 0 ? EagleAPI.GetETNumber(retstring)[0] : "");
                Context.textBox_��Ʊ��.Text = Context.textBox_��Ʊ��.Text.Trim().Replace(' ', '-');
                Context.b_ETDZSUCC = EagleAPI.IsBookETOK(retstring);
                Context.textBox_���۵�λ����.Text = EagleAPI.GetOfficeNumberByETicket(retstring);
                if (Context.b_ETDZSUCC)
                {
                    //MessageBox.Show("���ӿ�Ʊ�ѹ��ɹ���");
                }
                else
                {
                    if(!Context.backGroundSubmitEticket)
                        MessageBox.Show("HN:���ӿ�Ʊδ���ɹ�������״̬��PNRΪ" + Context.textBox_������.Text);
                }
                #endregion
            }
                //�������õ��ӿ�Ʊ����ȡ
            else
            {
                #region
                ePlus.eTicket.etInfomation ei = new ePlus.eTicket.etInfomation();
                ei.SetVar(retstring);
                Context.textBox_�ÿ�����.Text = ei.PASSENGER;
                Context.comboBox_ǩע.Text = ei.ER;
                EagleString.DetrResult dr = new EagleString.DetrResult(retstring);

                try
                {
                    Context.textBox_Ʊ��.Text = "CNY" + ei.FARE.Trim().Substring(3).Trim();
                }
                catch { }
                try
                {
                    string temp = ei.TAX1.Trim().Substring(3).Trim();
                    Context.textBox_���������.Text = "CN" + temp.Substring(0, temp.Length - 2);
                }
                catch
                { }
                try
                {
                    string temp = ei.TAX2.Trim().Substring(3).Trim();
                    Context.textBox_ȼ�ͷ�.Text = "YQ" + temp.Substring(0, temp.Length - 2);
                }
                catch { }
                try
                {
                    Context.textBox_�ϼ�.Text = "CNY" + ei.TOTAL.Trim().Substring(3).Trim();
                }
                catch { }
                Context.textBox_������Ʊ.Text = ei.CONJTKT;
                Context.textBox101.Text = EagleAPI.GetCityCn("", EagleAPI.substring(ei.FROM, 0, 3));
                Context.textBox102.Text = EagleAPI.substring(ei.FROM, 0, 3);
                Context.textBox103.Text = EagleAPI.substring(ei.FROM, 4, 2);
                Context.textBox104.Text = EagleAPI.substring(ei.FROM, 10, 4);
                Context.textBox105.Text = EagleAPI.substring(ei.FROM, 16, 1);
                Context.textBox106.Text = EagleAPI.substring(ei.FROM, 18, 5);
                Context.textBox107.Text = EagleAPI.substring(ei.FROM, 24, 4);
                try
                {
                    Context.textBox109.Text = EagleAPI.substring(ei.FROM, 32, ei.FROM.IndexOf(' ', 32) - 32);
                    Context.textBox10c.Text = EagleAPI.substring(ei.FROM, 57, ei.FROM.IndexOf(' ', 57) - 57);
                }
                catch { };
                if (ei.TO2 == "")//ֻ��һ������
                {
                    Context.textBox201.Text = EagleAPI.GetCityCn("", EagleAPI.substring(ei.TO1, 0, 3));
                    Context.textBox202.Text = EagleAPI.substring(ei.TO1, 0, 3);
                    Context.textBox203.Text = "";
                    Context.textBox303.Text = "";
                }
                else
                {
                    Context.textBox201.Text = EagleAPI.GetCityCn("", EagleAPI.substring(ei.TO1, 0, 3));
                    Context.textBox202.Text = EagleAPI.substring(ei.TO1, 0, 3);
                    Context.textBox203.Text = EagleAPI.substring(ei.TO1, 4, 2);
                    Context.textBox204.Text = EagleAPI.substring(ei.TO1, 10, 4);
                    Context.textBox205.Text = EagleAPI.substring(ei.TO1, 16, 1);
                    Context.textBox206.Text = EagleAPI.substring(ei.TO1, 18, 5);
                    Context.textBox207.Text = EagleAPI.substring(ei.TO1, 24, 4);
                    try
                    {
                        Context.textBox209.Text = EagleAPI.substring(ei.TO1, 32, ei.TO1.IndexOf(' ', 32) - 32);
                        Context.textBox20c.Text = EagleAPI.substring(ei.TO1, 57, ei.TO1.IndexOf(' ', 57) - 57);
                    }
                    catch { };
                    Context.textBox301.Text = EagleAPI.GetCityCn("", EagleAPI.substring(ei.TO2, 0, 3));
                    Context.textBox302.Text = EagleAPI.substring(ei.TO2, 0, 3);
                    Context.textBox303.Text = "";
                }
                GlobalVar2.tmpPrintPnr = ei.SmallCode;
                Context.bigCode = ei.BigCode;
                #endregion
            }
            //�����
            if (Context.textBox105.Text.Trim() == "F") Context.textBox10c.Text = "40K";
            if (Context.textBox205.Text.Trim() == "F") Context.textBox20c.Text = "40K";
            if (Context.textBox105.Text.Trim() == "C") Context.textBox10c.Text = "30K";
            if (Context.textBox205.Text.Trim() == "C") Context.textBox20c.Text = "30K";

            if (Context.textBox301.Text.Trim() == "") Context.textBox301.Text = "VOID";
            if (Context.textBox204.Text.Trim() == "") Context.textBox204.Text = "VOID";
            #region //��
            {
                //Context.textBox_��Ʊ��.Text = "11122233334445";
                //Context.textBox_�ϼ�.Text = "CNY100.00";
            }
            #endregion

#if !newCheck
            if (Context.textBox_��Ʊ��.Text.Trim() != "")
            {
                Context.button2.Enabled = true;
                Context.button3.Enabled = false;
            }
            else
            {
                Context.button2.Enabled = false;
                Context.button3.Enabled = true;
            }
            if (retstring.IndexOf("**ELECTRONIC TICKET PNR**") >= 0)
            {
                if (Context.textBox_��Ʊ��.Text.Trim() == "")
                {
                    if(!Context.b_checkinfo_window)
                        MessageBox.Show("��ȡPNR����������<ȷ��PNR>");
                    Context.button2.Enabled = false;
                    Context.button3.Enabled = false;
                }
            }
#else
            
            if (Context.b_MethodGetWithPNR)
            {
                //6.��û�е��ӿ�Ʊ�ţ�ȡ�����ص�1(�ɷ��ؽ������)
                if (Context.textBox_��Ʊ��.Text.Trim() == "")
                {
                    Context.retstring_back = retstring;
                    if (Context.b_checkinfo_window || Context.b_submitinfo_window)
                    {
                        Context.button3_Click(sender, e);//ȡ��
                        if (!Context.bPause) Context.btGetPNR_Click(sender, e);//�ص�1
                    }
                }
                else
                {
                    //7.���۸�Ϊ�㣬����rt+PNR+~pat:��ȡ���۸�(�ɷ��ؽ������)
                    
                    Context.retstring_back = retstring;
                    string totalcny = Context.textBox_�ϼ�.Text.Substring(3);
                    float fCny = 0F;
                    try { fCny = float.Parse(totalcny); }
                    catch { }
                    if (fCny < 1F)
                    {
                        #region
                        Context.bGetPat = true;


                        retstring = "";


                        if (Context.backGroundSubmitEticket)
                        {
                            EagleAPI.CLEARCMDLIST(7);
                            EagleAPI.EagleSendCmd("rT" + Context.textBox_������.Text + "~pat:", 7);
                        }
                        else
                        {
                            EagleAPI.CLEARCMDLIST(3);
                            EagleAPI.EagleSendCmd("rT" + Context.textBox_������.Text + "~pat:");
                        }


                        Context.richTextBox1.Text = "rT" + Context.textBox_������.Text + "~pat:" + Context.richTextBox1.Text;
                        #endregion
                    }
                    else
                    {
                        #region
                        if (retstring.IndexOf("**ELECTRONIC TICKET PNR**") >= 0)
                        {
                            if (Context.textBox_��Ʊ��.Text.Trim() == "")
                            {
                                if (!Context.b_checkinfo_window)
                                    Context.richTextBox1.Text = ("��ȡPNR��������<ȷ��PNR>") + Context.richTextBox1.Text;
                                //�ص�1
                                Context.btGetPNR_Click(sender, e);
                            }
                            else
                            {
                                //8.�ύ���ӿ�Ʊ��Ϣ���ص�1(�ɷ��ؽ������)(������1)
                                Context.button2_Click(sender, e);
                                //Context.clear();
                                if(Context.b_submitinfo_window)
                                    Context.btGetPNR_Click(sender, e);
                            }
                        }
                        else//���ǵ��ӿ�Ʊ
                        {
                            Context.button3_Click(sender, e);
                            if (!Context.bPause) Context.btGetPNR_Click(sender, e);//�ص�1
                        }
                        #endregion
                    }
                }
            }
#endif
            try
            {
                Context.textBox_������.Text = GlobalVar2.tmpPrintPnr;
            }
            catch
            {
            }
        }
        public string retstring_back = "";
        private void textBox_��Ʊ��_MouseUp(object sender, MouseEventArgs e)
        {

        }
        private void textBox_��Ʊ��_KeyUp(object sender, KeyEventArgs e)
        {
            btA4Print.Enabled = false;

            canLinaxuPrint = false;
            GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.none;
            b_MethodGetWithPNR = false;

            IsPrinting = false;
            IsRemoving = false;
            GlobalVar.b_rtByEticket = true;
            if (e.KeyValue == 13)//�س�
            {
                retstring = "";
                string temp = textBox_��Ʊ��.Text;
                button_���_Click(button_���, e);
                textBox_��Ʊ��.Text = temp;
                
                EagleAPI.EagleSendCmd("detr:tn/" + textBox_��Ʊ��.Text.Trim().Replace(' ','-'));

            }
        }
        public string tempNO = "";
        public void textBox_������_KeyUp(object sender, KeyEventArgs e)
        {
            btA4Print.Enabled = true;
            GlobalVar2.tmpPrintPnr = textBox_������.Text;
            canLinaxuPrint = true;
            GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.none;
            b_MethodGetWithPNR = true;

            IsPrinting = false;
            IsRemoving = false;
            GlobalVar.b_rtByEticket = false;

            if (e.KeyValue != 13)return;//�س�
            tbKeyUp1();


        }
        void tbKeyUp1()
        {
            if (!BookTicket.bIbe)
            {
                PrintReceipt.Context = this;
                textBox_������.Text = textBox_������.Text.ToUpper();
                retstring = "";

                clear();

                if (backGroundSubmitEticket)
                {
                    if (!EagleAPI.IsRtCode(textBox_������.Text.Trim()))
                    {
                        Context.button3_Click(null, null);
                        if (!Context.bPause) Context.btGetPNR_Click(null, null);//�ص�1
                        return;
                    }

                    EagleAPI.CLEARCMDLIST(7);
                    EagleAPI.EagleSendCmd("rT:n/" + textBox_������.Text.Trim(), 7);
                }
                else
                {
                    if (textBox_������.Text.Trim().Length != 5) return;
                    EagleAPI.CLEARCMDLIST(3);
                    EagleAPI.EagleSendCmd("rT:n/" + textBox_������.Text.Trim());
                }
                GlobalVar2.tmpPrintPnr = textBox_������.Text;
                textBox_������.Text = GlobalVar.WaitString;
            }
            else
            {
                try
                {
                    clear();
                    Application.DoEvents();
                    Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                    rtXml = (ib.rt2(textBox_������.Text.Trim(),GlobalVar.serverAddr== GlobalVar.ServerAddr.HangYiWang));//.xepnr("MDR1R",""));
                    if (rtXml == "") return;
                    Options.ibe.IbeRt ir = new Options.ibe.IbeRt(rtXml);
                    string[] names = ir.getpeopleinfo(0);
                    string[] cardids = ir.getpeopleinfo(1);
                    string[] tktno = ir.getpeopleinfo(2);
                    listBox_������.Items.Clear();
                    for (int i = 0; i < names.Length; i++)
                    {
                        listBox_������.Items.Add(names[i]);
                    }
                    textBox_�ÿ�����.Text = names[0];
                    textBox_����.Text = names.Length.ToString();
                    textBox_֤������.Text = cardids[0];
                    textBox_��Ʊ��.Text = tktno[0];

                    string[] flightinfo = ir.getflightsegsinfo();
                    for (int i = 0; i < flightinfo.Length; i++)
                    {
                        string[] fi = flightinfo[i].Split('~');
                        if (i == 0)
                        {
                            string from = fi[2].Substring(0, 3);
                            string to = fi[2].Substring(3);
                            textBox101.Text = EagleAPI.GetCityCn("", from);
                            textBox102.Text = from;
                            textBox103.Text = fi[0].Substring(0, 2);
                            textBox104.Text = fi[0].Substring(2);
                            textBox105.Text = fi[1];
                            System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                            DateTime dt = DateTime.ParseExact(fi[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                            textBox106.Text = dt.ToString("ddMMM", myDTFI).ToUpper();
                            textBox107.Text = dt.ToString("HHmm");
                            textBox109.Text = EagleAPI.GetAirLineBunkFareClass(textBox103.Text, textBox105.Text, DateTime.Now);

                            textBox201.Text = EagleAPI.GetCityCn("", to);
                            textBox202.Text = to;
                        }
                        if (i == 1)
                        {
                            string from = fi[2].Substring(0, 3);
                            string to = fi[2].Substring(3);
                            textBox201.Text = EagleAPI.GetCityCn("", from);
                            textBox202.Text = from;
                            textBox203.Text = fi[0].Substring(0, 2);
                            textBox204.Text = fi[0].Substring(2);
                            textBox205.Text = fi[1];
                            System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                            DateTime dt = DateTime.ParseExact(fi[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                            textBox206.Text = dt.ToString("ddMMM", myDTFI).ToUpper();
                            textBox207.Text = dt.ToString("HHmm");
                            textBox209.Text = EagleAPI.GetAirLineBunkFareClass(textBox203.Text, textBox205.Text, DateTime.Now);

                            textBox301.Text = EagleAPI.GetCityCn("", to);
                            textBox302.Text = to;
                        }
                        if (i == 2)
                        {
                            string from = fi[2].Substring(0, 3);
                            string to = fi[2].Substring(3);
                            textBox301.Text = EagleAPI.GetCityCn("", from);
                            textBox302.Text = from;
                            textBox303.Text = fi[0].Substring(0, 2);
                            textBox304.Text = fi[0].Substring(2);
                            textBox305.Text = fi[1];
                            System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                            DateTime dt = DateTime.ParseExact(fi[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                            textBox306.Text = dt.ToString("ddMMM", myDTFI).ToUpper();
                            textBox307.Text = dt.ToString("HHmm");
                            textBox309.Text = EagleAPI.GetAirLineBunkFareClass(textBox303.Text, textBox305.Text, DateTime.Now);

                            textBox401.Text = EagleAPI.GetCityCn("", to);
                            textBox402.Text = to;
                        }
                    }
                    if (textBox105.Text.Trim() == "F") Context.textBox10c.Text = "40K";
                    if (textBox205.Text.Trim() == "F") Context.textBox20c.Text = "40K";
                    if (textBox105.Text.Trim() == "C") Context.textBox10c.Text = "30K";
                    if (textBox205.Text.Trim() == "C") Context.textBox20c.Text = "30K";

                    if (textBox301.Text.Trim() == "") Context.textBox301.Text = "VOID";
                    if (textBox204.Text.Trim() == "") Context.textBox204.Text = "VOID";
                    bGetPat = true;
                    EagleAPI.CLEARCMDLIST(3);
                    EagleAPI.EagleSendCmd("rT" + Context.textBox_������.Text + "~pat:");

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Source + ee.Message);
                }
            }
        }

        void clear()
        {
            //if (!b_submitinfo_window) return;
            textBox_�ÿ�����.Text = "";
            listBox_������.Items.Clear();
            textBox_֤������.Text = "";
            //textBox_������.Text = "";
            //textBox_ӡˢ���.Text = "";
            textBox_Ʊ��.Text = "CNY";
            textBox_���������.Text = "CN";
            textBox_ȼ�ͷ�.Text = "YQ";
            textBox_������.Text = "";
            textBox_�ϼ�.Text = "";
            textBox_��Ʊ��.Text = "";
            textBox_��֤��.Text = "";
            textBox_������Ʊ.Text = "";
            //textBox_���շ�.Text = "";
            //textBox_���۵�λ����.Text = "";
            //textBox_���λ.Text = "";
            //textBox_�����.Text = "";

            textBox101.Text = "";
            textBox102.Text = "";
            textBox103.Text = "";
            textBox104.Text = "";
            textBox105.Text = "";
            textBox106.Text = "";
            textBox107.Text = "";
            textBox108.Text = "OK";
            textBox109.Text = "";
            textBox10a.Text = "";
            textBox10b.Text = "";
            textBox10c.Text = "20K";

            textBox201.Text = "";
            textBox202.Text = "";
            textBox203.Text = "";
            textBox204.Text = "VOID";
            textBox205.Text = "";
            textBox206.Text = "";
            textBox207.Text = "";
            textBox208.Text = "";
            textBox209.Text = "";
            textBox20a.Text = "";
            textBox20b.Text = "";
            textBox20c.Text = "";

            textBox301.Text = "VOID";
            textBox302.Text = "";
            textBox303.Text = "";
            textBox304.Text = "";
            textBox305.Text = "";
            textBox306.Text = "";
            textBox307.Text = "";
            textBox308.Text = "";
            textBox309.Text = "";
            textBox30a.Text = "";
            textBox30b.Text = "";
            textBox30c.Text = "";

            textBox401.Text = "";
        }
        private void button_���_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void PrintReceipt_FormClosed(object sender, FormClosedEventArgs e)
        {
#if receipt
            Application.Exit();
#endif
            connect_4_Command.PrintWindowOpen = false;
            Context = null;
            opened = false;
            IsPrinting = false;
            IsRemoving = false;
            EagleAPI.CLEARCMDLIST(7);
            //EagleAPI.CLEARCMDLIST(3);
            if (b_checkinfo_window)
                PrintReceipt.b0007 = false;
            else
                PrintReceipt.b0003 = false;
            stoptimer();

            if (rType == ReceiptType.Business) BusinessClose();
        }
        [DllImport("TestDll.dll", EntryPoint = "RecieptPrint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public extern unsafe static string RecieptPrint([MarshalAs(UnmanagedType.LPStr)]string sn, [MarshalAs(UnmanagedType.LPStr)]string en, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);

        static public bool b_3IN1 = false;

        private void button_��ӡ_Click(object sender, EventArgs e)
        {
            string receiptNumber = textBox_ӡˢ���.Text.Trim();
            if (receiptNumber == "7166206881")//added by chenqj
            {
                MessageBox.Show("��֤�� 7166206881 �Ѿ���ʹ�ù�������ȷ���룡", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (receiptNumber == "6001022230")//added by chenqj
            {
                MessageBox.Show("��֤�� 6001022230 �Ѿ���ʹ�ù�������ȷ���룡", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GlobalVar.bBatting = false;
            buttonprint();
           
        }
        private void buttonprint()
        {
            GlobalVar2.tmpPrintName = textBox_�ÿ�����.Text;
            GlobalVar2.tmpPrintPnr = textBox_������.Text.ToUpper();

            eTicket.AutoSetReceiptNumber asn = new ePlus.eTicket.AutoSetReceiptNumber();
            asn.SaveReceiptSignatureToXML(textBox_���λ.Text.Trim());
            asn.SaveReceiptSaleCodeToXML(tbSaleCode2.Text.Trim());

            if (!EagleAPI.PrinterSetup(ptDoc)) return;

            combox_string = comboBox_ǩע.Text;
            try
            {
                if (textBox_��֤��.Text.Trim() != textBox_ӡˢ���.Text.Trim().Substring(textBox_ӡˢ���.Text.Trim().Length - 4))
                {
                    MessageBox.Show("��֤�����", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("ӡˢ��Ż���֤�����", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dd = MessageBox.Show("ȷ��Ҫ��ӡ�г̵���", "�׸�Ƽ�", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dd == DialogResult.Cancel)
                return;

#if !RECEIPT
            {//ȡ���񣬼���Ƿ���Դ�ӡ
                string rn = textBox_ӡˢ���.Text.Trim();
                string on = EagleAPI.substring(EagleAPI.str_cfg_name, 0, 6);
                string en = textBox_��Ʊ��.Text.Trim();
                en = en.Substring(en.Length - 10);
                if (on.Length != 6) { MessageBox.Show("��ָ������"); return; }
                if (WebService.CanPrint(rn, on, en).ToUpper() != "TRUE") { MessageBox.Show("����ԭ��1���г̵�����������2���г̵������ڸ����á�3�����ӿ�Ʊ��δά����4���������Ա��ϵ"); return; }
            }
#else
            string rn = textBox_ӡˢ���.Text.Trim();
            string on = EagleAPI.substring(GlobalVar.str_cfg_name, 0, 6);
            string en = textBox_��Ʊ��.Text.Trim();
            en = EagleAPI.substring(en, en.Length - 10, 10);

            //if (WebService.CanPrint(rn, on, en).ToUpper() != "TRUE") { MessageBox.Show("�����г̵��Ƿ����"); ; }
#endif
            IsPrinting = true;
            IsRemoving = false;
            opened = false;//���صĽ������ReturnString����Command.AV_String����
            //a.�ж��г̵��Ŵ�����ȷ��            
            if (textBox_ӡˢ���.Text.Trim().Length == 10 && string.Compare(textBox_ӡˢ���.Text.Trim(), "0000000000", true) >= 0 && string.Compare(textBox_ӡˢ���.Text.Trim(), "9999999999", true) <= 0 && textBox_��Ʊ��.Text.Trim().Length >= 13)
            {
                b_3IN1 = true;
                StringBuilder sb = new StringBuilder("", 4096);
                string etno = textBox_��Ʊ��.Text.Trim();
                while (etno.IndexOf(' ') > 0 || etno.IndexOf('-') > 0) etno = etno.Remove(etno.IndexOf(' ') > 0 ? etno.IndexOf(' ') : etno.IndexOf('-'), 1);
                RecieptPrint(etno, textBox_ӡˢ���.Text.Trim(), sb);//testdll.ʮ�������ַ����������ӦȥЭ��ͷ��ת��Ϊʮ������ֵ
                string str_send = sb.ToString();
                
                #region send3in1 package
                {
                    EagleAPI.EagleSendPrintReceipt(str_send);
                }
                #endregion
                b_3IN1 = false;
            }
            else//����ȷ���򲻷���
            {
                MessageBox.Show("��������ȷӡˢ��Ż���ӿ�Ʊ�ţ�", "��ӡ�г̵�", MessageBoxButtons.OK);
                IsPrinting = false;
                opened = true;//��open��ԭ
            }
        }
        public static string str_printreceipt
        {
            set
            {
#if RECEIPT
                //����if (!EagleAPI.IsCreateReceiptSuccess()) return;
#else

                if (!EagleAPI.IsCreateReceiptSuccess())//����Ч�������Ƿ��ӡ��ͨ���ݵĶԻ���
                {

                    DialogResult dd = MessageBox.Show("����ƾ֤����Ч���Ƿ��ӡ��ͨ���ݣ�", "�׸�Ƽ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dd == DialogResult.No)
                        return;
                }
#endif
                bool bSucc = EagleAPI.IsCreateReceiptSuccess();

                if (GlobalVar.bBatting)//�Ƿ��������ӡEric
                {
                    if (bSucc)
                    {
                        Context.textBox_��Ʊ��.Text = GlobalVar.lsEticketNumber[Context.iOperating];
                        Context.textBox_�ÿ�����.Text = GlobalVar.lsRctName[Context.iOperating];
                        Context.textBox_ӡˢ���.Text = GlobalVar.lsReceiptNumber[Context.iOperating];
                        Context.textBox_֤������.Text = GlobalVar.lsRctIdCard[Context.iOperating];
                        Context.textBox_��֤��.Text = Context.textBox_ӡˢ���.Text.Trim().Substring(6, 4);
                        //Context.setControlCallback = new SetControlCallback(Context.setprintctrl);
                        PrintDialog pd = new PrintDialog();
                        EagleAPI.PrinterSetupCostom(Context.ptDoc, 950, 400);
                        pd.Document = Context.ptDoc;
                        if (MessageBox.Show("���ӿ�Ʊ�ţ�" + GlobalVar.lsEticketNumber[Context.iOperating] + "\r\n"
              + "������" + GlobalVar.lsRctName[Context.iOperating] + "\r\n"
              + "�г̵��ţ�" + GlobalVar.lsReceiptNumber[Context.iOperating] + "\r\n"
              + "���֤�ţ�" + GlobalVar.lsRctIdCard[Context.iOperating], "��˶�", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
              == DialogResult.OK)
                        {
                            Context.ptDoc_Print();
                            //Context.ptDoc.Print();
                            Context.ReceiptNumberLog(false);//added by chenqj
                        }
                        Context.iOperating++;
                        if (Context.iOperating < GlobalVar.lsRctIdCard.Count)
                        {
                            b_3IN1 = true;
                            IsPrinting = true;
                            opened = false;//��open��ԭ

                            EagleAPI.EagleSendPrintReceipt(GlobalVar.ls3IN1Command[Context.iOperating]);

                        }
                        else
                        {
                            IsPrinting = false;
                            opened = true;//��open��ԭ

                        }
                    }
                    else
                    {
                        if (Context.iOperating + 1 < GlobalVar.lsRctIdCard.Count)
                        {
                            if (DialogResult.Yes == MessageBox.Show("�г̵�" + GlobalVar.lsReceiptNumber[Context.iOperating] + "��Ч���Ƿ������ӡ��һ�ţ�", "ע��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            {
                                Context.iOperating++;
                                if (Context.iOperating < GlobalVar.lsRctIdCard.Count)
                                {
                                    b_3IN1 = true;
                                    IsPrinting = true;
                                    opened = false;//��open��ԭ

                                    EagleAPI.EagleSendPrintReceipt(GlobalVar.ls3IN1Command[Context.iOperating]);

                                }
                            }
                            else
                            {
                                IsPrinting = false;
                                opened = true;//��open��ԭ

                            }
                        }
                    }
                }
                else
                {//���������ӡEric

                    if (bSucc)//������Ϣ�ɹ�Eric
                    {
                        Application.DoEvents();
                        PrintDialog pd = new PrintDialog();
                        EagleAPI.PrinterSetupCostom(Context.ptDoc, 950, 400);
                        pd.Document = Context.ptDoc;
                        Context.ptDoc_Print();
                        //Context.ptDoc.Print();
                        Context.ReceiptNumberLog(false);//added by chenqj
                        IsPrinting = false;
                        opened = true;//��open��ԭ
                    }
                    else//������Ϣ�а�������Eric
                    {
                        if (GlobalVar.intPrintErrorType == 1)//��������Ϊ����û����ȷ��Ӧ
                        {
                            if (intPrintCount <= 3)
                            {
                                //��һ�δ�ӡ�������ط�����
                                intPrintCount++;
                                EagleAPI.EagleSendPrintReceipt(strCurrentPrintCommand);//�ط�����һ��
                                return;
                            }
                            //����ʧ�ܺ�Ĭ�Ϻ�̨��ӡ
                            else
                            {
                                //PrintDialog pd = new PrintDialog();
                                //EagleAPI.PrinterSetupCostom(Context.ptDoc, 950, 400);
                                //pd.Document = Context.ptDoc;
                                //Context.ptDoc_Print();
                                //IsPrinting = false;
                                //opened = true;//��open��ԭ
                                //LogForPrintReceipt lfpr = new LogForPrintReceipt();
                                //Thread st = new Thread(new ThreadStart(lfpr.WritePrintLog));
                                //lfpr.info = strPrintInfo;
                                //st.Start();
                                //MessageBox.Show("�����г̵�Ϊ�ѻ���ӡ���г̵���δ���������ţ���������Ѿ����浽������־", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (GlobalVar.intPrintErrorType == 2)//��������Ϊ�û��������ݴ���
                        {
                            //if (MessageBox.Show("�Ƿ��ѻ�ǿ�д�ӡ��", "�׸�Ƽ�", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            //{
                            //    PrintDialog pd = new PrintDialog();
                            //    EagleAPI.PrinterSetupCostom(Context.ptDoc, 950, 400);
                            //    pd.Document = Context.ptDoc;
                            //    Context.ptDoc_Print();
                            //    IsPrinting = false;
                            //    opened = true;//��open��ԭ
                            //}
                        }
                        else if (GlobalVar.intPrintErrorType == 3)//2009-4-14�·��ִ�������"[Itinerary_INV: Not Support]"
                        {
                        }

                    }
                }



            }
        }
        void printchinesechar(string ch, System.Drawing.Printing.PrintPageEventArgs e, float x, float y,Font ptFontCn,Brush ptBrush)
        {
            EagleAPI2.printchinesechar(ch.ToUpper(), e, x, y);
            return;
            if (ch.CompareTo("zzzzzzzzzzzzzzz") > 0)
            {
                string lkxm = ch;
                for (int i = 0; i < lkxm.Length; i++)
                {
                    e.Graphics.DrawString(lkxm[i].ToString(), ptFontCn, ptBrush, x  + 5F * i, y );
                }
            }
            else
                e.Graphics.DrawString(ch, ptFontCn, ptBrush, x , y );

        }
        void printenglisghchar(string ch, System.Drawing.Printing.PrintPageEventArgs e, float x, float y, Font ptFontEn, Brush ptBrush)
        {
            EagleAPI2.printenglisghchar(ch.ToUpper(), e, x, y);
            return;
            string lkxm = ch;
            for (int i = 0; i < lkxm.Length; i++)
            {
                e.Graphics.DrawString(lkxm[i].ToString(), ptFontEn, ptBrush, x + 2.6F * i, y);
            }
        }
        private void ptDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (zzprintbus)
            {
                ZzPrintBus (strprint ,sender,e);
                return;
            }

            try
            {
                if (rType == ReceiptType.Business)
                {
                    printbussiness(sender, e);
                    return;
                }
                
#if receipt2
                

                e.Graphics.PageUnit = GraphicsUnit.Millimeter;
                Font ptFontCn = new Font("TEC", 18.0F, System.Drawing.FontStyle.Regular);
                if (EagleAPI2.ExistFont("TEC"))
                {
                    ptFontCn = new Font("System", 12.0F, System.Drawing.FontStyle.Regular);
                }
                Font ptFontEn = null;
                if (EagleAPI2.ExistFont("MingLiU"))
                    ptFontEn = new Font("MingLiU", 11.0F, System.Drawing.FontStyle.Regular);
                else if (EagleAPI2.ExistFont("����_GB2312"))
                    ptFontEn = new Font("����_GB2312", 12.0F, System.Drawing.FontStyle.Regular);
                else if (EagleAPI2.ExistFont("Courier New"))
                    ptFontEn = new Font("Courier New", 12.0F, System.Drawing.FontStyle.Regular);
                else if (EagleAPI2.ExistFont("Courier"))
                    ptFontEn = new Font("Courier", 12.0F, System.Drawing.FontStyle.Regular);
                else
                    ptFontEn = new Font("System", 12.0F, System.Drawing.FontStyle.Regular);
                
                Brush ptBrush =Brushes.Black ;
                e.PageSettings.Margins.Left = 0;
                e.PageSettings.Margins.Right = 0;
                e.PageSettings.Margins.Top = 0;
                e.PageSettings.Margins.Bottom = 0;
                PointF o = GlobalVar.o_receipt;
                //1.�ÿ�����25,25
                if (GlobalVar2.b_���ð�) textBox_�ÿ�����.Text = "���ð�";
                printchinesechar(textBox_�ÿ�����.Text, e, 31F + o.X, 24F + o.Y, ptFontCn, ptBrush);
                
                //2.��Ч���֤������70,25
                printenglisghchar(textBox_֤������.Text, e, 72F + o.X, 25F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_֤������.Text, ptFontEn, ptBrush, 72F + o.X, 25F + o.Y);
                //3.ǩע134,25
                printchinesechar(comboBox_ǩע.Text, e, 147F + o.X, 24F + o.Y, ptFontCn, ptBrush);
                //e.Graphics.DrawString(comboBox_ǩע.Text, ptFontCn, ptBrush, 134F + o.X, 25F + o.Y);

                //4.Ʊ��66,75
                printenglisghchar(textBox_Ʊ��.Text, e, 66F-2.6F + o.X, 75F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_Ʊ��.Text, ptFontEn, ptBrush, 66F + o.X, 75F + o.Y);

                //5.���������95,75
                printenglisghchar(textBox_���������.Text, e, 95F-2F + o.X, 75F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_���������.Text, ptFontEn, ptBrush, 95F-2F + o.X, 75F + o.Y);

                //6.ȼ�͸��ӷ�120,75
                printenglisghchar(textBox_ȼ�ͷ�.Text, e, 120F + o.X, 75F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_ȼ�ͷ�.Text, ptFontEn, ptBrush, 120F + o.X, 75F + o.Y);

                //7.����˰��147,75
                printenglisghchar(textBox_������.Text, e, 147F + o.X, 75F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_������.Text, ptFontEn, ptBrush, 147F + o.X, 75F + o.Y);

                //8.�ϼ�175,75
                printenglisghchar(textBox_�ϼ�.Text, e, 175F + o.X, 75F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_�ϼ�.Text, ptFontEn, ptBrush, 175F + o.X, 75F + o.Y);

                //9.���ӿ�Ʊ��40,84
                printenglisghchar(textBox_��Ʊ��.Text, e, 40F + o.X, 80F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_��Ʊ��.Text, ptFontEn, ptBrush, 40F + o.X, 80F + o.Y);

                //10.��֤��87,84
                printenglisghchar(textBox_��֤��.Text, e, 87F + o.X, 80F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_��֤��.Text, ptFontEn, ptBrush, 87F + o.X, 80F + o.Y);

                //11.������Ʊ130,84
                printenglisghchar(textBox_������Ʊ.Text, e, 130F + o.X, 80F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_������Ʊ.Text, ptFontEn, ptBrush, 130F + o.X, 80F + o.Y);

                //12.���շ�203,84
                printenglisghchar(textBox_���շ�.Text, e, 203F + o.X, 80F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_���շ�.Text, ptFontEn, ptBrush, 203F + o.X, 80F + o.Y);

                //13.���۵�λ����40,92
                printenglisghchar(textBox_���۵�λ����.Text, e, 40F + o.X, 88F + o.Y, ptFontEn, ptBrush);
                printenglisghchar(tbSaleCode2.Text, e, 40F + o.X, 92F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_���۵�λ����.Text, ptFontEn, ptBrush, 40F + o.X, 88F + o.Y);
                //e.Graphics.DrawString(tbSaleCode2.Text, ptFontEn, ptBrush, 40F + o.X, 92F + o.Y);

                //14.���λ100,92
                printchinesechar(textBox_���λ.Text, e, 100F + o.X, 89F + o.Y, ptFontCn, ptBrush);
                //e.Graphics.DrawString(textBox_���λ.Text, ptFontCn, ptBrush, 100F + o.X, 92F + o.Y);

                //15.�����186,92
                printenglisghchar(textBox_�����.Text, e, 187F + o.X, 88F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_�����.Text, ptFontEn, ptBrush, 187F + o.X, 88F + o.Y);

                //16.������
                printenglisghchar(textBox_������.Text, e, 30.5F + o.X, 32.5F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox_������.Text.ToUpper(), ptFontEn, ptBrush, 32F + o.X, 32.5F + o.Y);

                //������Ϣ
                printchinesechar(textBox101.Text, e, 31F + o.X, 42F-2F + o.Y, ptFontCn, ptBrush);
                //e.Graphics.DrawString(textBox101.Text, ptFontCn, ptBrush, 31F + o.X, 41F + o.Y);
                printenglisghchar(textBox102.Text, e, 53F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox102.Text, ptFontEn, ptBrush, 53F + o.X, 41F + o.Y);
                printenglisghchar(textBox103.Text, e, 68F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox103.Text, ptFontEn, ptBrush, 68F + o.X, 41F + o.Y);//66,41
                printenglisghchar(textBox104.Text, e,80F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox104.Text, ptFontEn, ptBrush, 80F + o.X, 41F + o.Y);
                printenglisghchar(textBox105.Text, e, 97F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox105.Text, ptFontEn, ptBrush, 97F + o.X, 41F + o.Y);
                printenglisghchar(textBox106.Text, e, 105F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox106.Text, ptFontEn, ptBrush, 105F + o.X, 41F + o.Y);
                printenglisghchar(textBox107.Text, e, 122F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox107.Text, ptFontEn, ptBrush, 122F + o.X, 41F + o.Y);
                //e.Graphics.DrawString(textBox108.Text, ptFontCn, ptBrush, 175F+o.X, 41F+o.Y);
                printenglisghchar(textBox109.Text, e, 135F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox109.Text, ptFontEn, ptBrush, 135F + o.X, 41F + o.Y);
                printenglisghchar(textBox10a.Text, e, 175F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox10a.Text, ptFontEn, ptBrush, 175F + o.X, 41F + o.Y);
                printenglisghchar(textBox10b.Text, e, 192F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox10b.Text, ptFontEn, ptBrush, 192F + o.X, 41F + o.Y);
                printenglisghchar(textBox10c.Text, e, 207F + o.X, 42F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox10c.Text, ptFontEn, ptBrush, 207F + o.X, 41F + o.Y);

                printchinesechar(textBox201.Text, e, 31F + o.X, 50F-2F + o.Y, ptFontCn, ptBrush);
                //e.Graphics.DrawString(textBox201.Text, ptFontCn, ptBrush, 31F + o.X, 50F + o.Y);
                printenglisghchar(textBox202.Text, e, 53F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox202.Text, ptFontEn, ptBrush, 53F + o.X, 50F + o.Y);
                printenglisghchar(textBox203.Text, e, 68F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox203.Text, ptFontEn, ptBrush, 68F + o.X, 50F + o.Y);
                printenglisghchar(textBox204.Text, e, 80F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox204.Text, ptFontEn, ptBrush, 80F + o.X, 50F + o.Y);
                printenglisghchar(textBox205.Text, e, 97F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox205.Text, ptFontEn, ptBrush, 97F + o.X, 50F + o.Y);
                printenglisghchar(textBox206.Text, e, 105F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox206.Text, ptFontEn, ptBrush, 105F + o.X, 50F + o.Y);
                printenglisghchar(textBox207.Text, e, 122F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox207.Text, ptFontEn, ptBrush, 122F + o.X, 50F + o.Y);
                //e.Graphics.DrawString(textBox208.Text, ptFontCn, ptBrush, 175F+o.X, 41F+o.Y);
                printenglisghchar(textBox209.Text, e, 135F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox209.Text, ptFontEn, ptBrush, 135F + o.X, 50F + o.Y);
                printenglisghchar(textBox20a.Text, e, 175F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox20a.Text, ptFontEn, ptBrush, 175F + o.X, 50F + o.Y);
                printenglisghchar(textBox20b.Text, e, 192F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox20b.Text, ptFontEn, ptBrush, 192F + o.X, 50F + o.Y);
                printenglisghchar(textBox20c.Text, e, 207F + o.X, 50F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox20c.Text, ptFontEn, ptBrush, 207F + o.X, 50F + o.Y);

                printchinesechar(textBox301.Text, e, 31F + o.X, 58F-2F + o.Y, ptFontCn, ptBrush);
                //e.Graphics.DrawString(textBox301.Text, ptFontCn, ptBrush, 31F + o.X, 58F + o.Y);
                printenglisghchar(textBox302.Text, e, 53F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox302.Text, ptFontEn, ptBrush, 53F + o.X, 58F + o.Y);
                printenglisghchar(textBox303.Text, e, 68F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox303.Text, ptFontEn, ptBrush, 68F + o.X, 58F + o.Y);
                printenglisghchar(textBox304.Text, e, 80F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox304.Text, ptFontEn, ptBrush, 80F + o.X, 58F + o.Y);
                printenglisghchar(textBox305.Text, e, 97F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox305.Text, ptFontEn, ptBrush, 97F + o.X, 58F + o.Y);
                printenglisghchar(textBox306.Text, e, 105F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox306.Text, ptFontEn, ptBrush, 105F + o.X, 58F + o.Y);
                printenglisghchar(textBox307.Text, e, 122F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox307.Text, ptFontEn, ptBrush, 122F + o.X, 58F + o.Y);
                //e.Graphics.DrawString(textBox308.Text, ptFontCn, ptBrush, 175F+o.X, 58F+o.Y);
                printenglisghchar(textBox309.Text, e, 135F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox309.Text, ptFontEn, ptBrush, 135F + o.X, 58F + o.Y);
                printenglisghchar(textBox30a.Text, e, 175F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox30a.Text, ptFontEn, ptBrush, 175F + o.X, 58F + o.Y);
                printenglisghchar(textBox30b.Text, e, 192F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox30b.Text, ptFontEn, ptBrush, 192F + o.X, 58F + o.Y);
                printenglisghchar(textBox30c.Text, e, 207F + o.X, 58F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox30c.Text, ptFontEn, ptBrush, 207F + o.X, 58F + o.Y);

                printchinesechar(textBox401.Text, e, 31F + o.X, 66F-2F + o.Y, ptFontCn, ptBrush);
                //e.Graphics.DrawString(textBox401.Text, ptFontCn, ptBrush, 31F + o.X, 66F + o.Y);
                printenglisghchar(textBox402.Text, e, 53F + o.X, 66F + o.Y, ptFontEn, ptBrush);
                //e.Graphics.DrawString(textBox402.Text, ptFontEn, ptBrush, 53F + o.X, 66F + o.Y);
#else
                oldprint(sender, e);
#endif
            }
            catch (Exception ex)
            {
                MessageBox.Show("�г̵���ӡ����"+ex.Message);
            }

                

        }
        void oldprint(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;
                
                Font ptFontEn = new Font("TEC", GlobalVar.fontsizeen, System.Drawing.FontStyle.Regular);
                Font ptFontCn = new Font("TEC", GlobalVar.fontsizecn, System.Drawing.FontStyle.Regular);
                if (!EagleAPI2.ExistFont("TEC"))
                {
                    //if (GlobalVar.fontsizecn <= 8 || GlobalVar.fontsizecn <= 8) throw new Exception("�����С����������8pt����");
                    ptFontEn = new Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular);
                    ptFontCn = new Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular);
                }
                Brush ptBrush = Brushes.Black;
                e.PageSettings.Margins.Left = 0;
                e.PageSettings.Margins.Right = 0;
                e.PageSettings.Margins.Top = 0;
                e.PageSettings.Margins.Bottom = 0;
                PointF o = GlobalVar.o_receipt;
                //1.�ÿ�����25,25
                e.Graphics.DrawString(textBox_�ÿ�����.Text, ptFontCn, ptBrush, 25F + o.X, 25F + o.Y);
                //2.��Ч���֤������70,25
                e.Graphics.DrawString(textBox_֤������.Text, ptFontEn, ptBrush, 70F + o.X, 25F + o.Y);
                //3.ǩע134,25
                e.Graphics.DrawString(comboBox_ǩע.Text, ptFontCn, ptBrush, 134F + o.X, 25F + o.Y);
                //4.Ʊ��66,75
                e.Graphics.DrawString(textBox_Ʊ��.Text, ptFontEn, ptBrush, 66F + o.X, 75F + o.Y);
                //5.���������95,75
                e.Graphics.DrawString(textBox_���������.Text, ptFontEn, ptBrush, 95F + o.X, 75F + o.Y);
                //6.ȼ�͸��ӷ�120,75
                e.Graphics.DrawString(textBox_ȼ�ͷ�.Text, ptFontEn, ptBrush, 120F + o.X, 75F + o.Y);
                //7.����˰��147,75
                e.Graphics.DrawString(textBox_������.Text, ptFontEn, ptBrush, 147F + o.X, 75F + o.Y);
                //8.�ϼ�175,75
                e.Graphics.DrawString(textBox_�ϼ�.Text, ptFontEn, ptBrush, 175F + o.X, 75F + o.Y);
                //9.���ӿ�Ʊ��40,84
                e.Graphics.DrawString(textBox_��Ʊ��.Text, ptFontEn, ptBrush, 40F + o.X, 84F + o.Y);
                //10.��֤��87,84
                e.Graphics.DrawString(textBox_��֤��.Text, ptFontEn, ptBrush, 87F + o.X, 84F + o.Y);
                //11.������Ʊ130,84
                e.Graphics.DrawString(textBox_������Ʊ.Text, ptFontEn, ptBrush, 130F + o.X, 84F + o.Y);
                //12.���շ�203,84
                e.Graphics.DrawString(textBox_���շ�.Text, ptFontEn, ptBrush, 203F + o.X, 84F + o.Y);
                //13.���۵�λ����40,92
                e.Graphics.DrawString(textBox_���۵�λ����.Text, ptFontEn, ptBrush, 40F + o.X, 88F + o.Y);
                e.Graphics.DrawString(tbSaleCode2.Text, ptFontEn, ptBrush, 40F + o.X, 92F + o.Y);
                //14.���λ100,92
                e.Graphics.DrawString(textBox_���λ.Text, ptFontCn, ptBrush, 100F + o.X, 92F + o.Y);
                //15.�����186,92
                e.Graphics.DrawString(textBox_�����.Text, ptFontEn, ptBrush, 182F + o.X, 92F + o.Y);
                //16.������
                e.Graphics.DrawString(textBox_������.Text.ToUpper(), ptFontEn, ptBrush, 25F + o.X, 31F + o.Y);

                //������Ϣ
                e.Graphics.DrawString(textBox101.Text, ptFontCn, ptBrush, 31F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox102.Text, ptFontCn, ptBrush, 54F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox103.Text, ptFontCn, ptBrush, 66F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox104.Text, ptFontCn, ptBrush, 78F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox105.Text, ptFontCn, ptBrush, 95F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox106.Text, ptFontCn, ptBrush, 102F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox107.Text, ptFontCn, ptBrush, 123F + o.X, 41F + o.Y);
                //e.Graphics.DrawString(textBox108.Text, ptFontCn, ptBrush, 175F+o.X, 41F+o.Y);
                e.Graphics.DrawString(textBox109.Text, ptFontCn, ptBrush, 135F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox10a.Text, ptFontCn, ptBrush, 175F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox10b.Text, ptFontCn, ptBrush, 192F + o.X, 41F + o.Y);
                e.Graphics.DrawString(textBox10c.Text, ptFontCn, ptBrush, 204F + o.X, 41F + o.Y);

                e.Graphics.DrawString(textBox201.Text, ptFontCn, ptBrush, 31F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox202.Text, ptFontCn, ptBrush, 54F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox203.Text, ptFontCn, ptBrush, 66F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox204.Text, ptFontCn, ptBrush, 78F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox205.Text, ptFontCn, ptBrush, 95F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox206.Text, ptFontCn, ptBrush, 102F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox207.Text, ptFontCn, ptBrush, 123F + o.X, 50F + o.Y);
                //e.Graphics.DrawString(textBox208.Text, ptFontCn, ptBrush, 175F+o.X, 41F+o.Y);
                e.Graphics.DrawString(textBox209.Text, ptFontCn, ptBrush, 135F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox20a.Text, ptFontCn, ptBrush, 175F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox20b.Text, ptFontCn, ptBrush, 192F + o.X, 50F + o.Y);
                e.Graphics.DrawString(textBox20c.Text, ptFontCn, ptBrush, 204F + o.X, 50F + o.Y);

                e.Graphics.DrawString(textBox301.Text, ptFontCn, ptBrush, 31F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox302.Text, ptFontCn, ptBrush, 54F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox303.Text, ptFontCn, ptBrush, 66F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox304.Text, ptFontCn, ptBrush, 78F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox305.Text, ptFontCn, ptBrush, 95F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox306.Text, ptFontCn, ptBrush, 102F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox307.Text, ptFontCn, ptBrush, 123F + o.X, 58F + o.Y);
                //e.Graphics.DrawString(textBox308.Text, ptFontCn, ptBrush, 175F+o.X, 58F+o.Y);
                e.Graphics.DrawString(textBox309.Text, ptFontCn, ptBrush, 135F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox30a.Text, ptFontCn, ptBrush, 175F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox30b.Text, ptFontCn, ptBrush, 192F + o.X, 58F + o.Y);
                e.Graphics.DrawString(textBox30c.Text, ptFontCn, ptBrush, 204F + o.X, 58F + o.Y);

                e.Graphics.DrawString(textBox401.Text, ptFontCn, ptBrush, 31F + o.X, 66F + o.Y);
                e.Graphics.DrawString(textBox402.Text, ptFontCn, ptBrush, 54F + o.X, 66F + o.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�г̵���ӡ����" + ex.Message);
            }
        }
        private void listBox_������_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!BookTicket.bIbe)
            {
                if (listBox_������.SelectedItem != null)
                {
                    textBox_�ÿ�����.Text = listBox_������.SelectedItem.ToString();
                    try
                    {
                        textBox_֤������.Text = EagleAPI.GetIDCardNo(retstring_back)[listBox_������.SelectedIndex];
                    }
                    catch { }
                    textBox_��Ʊ��.Text = EagleAPI.GetETNumber(retstring_back)[listBox_������.SelectedIndex];
                }
            }
            else
            {
                try
                {
                    textBox_�ÿ�����.Text = listBox_������.SelectedItem.ToString();
                    Options.ibe.IbeRt ir = new Options.ibe.IbeRt(rtXml);
                    textBox_֤������.Text = ir.getpeopleinfo(1)[listBox_������.SelectedIndex];
                    textBox_��Ʊ��.Text = ir.getpeopleinfo(2)[listBox_������.SelectedIndex];
                }
                catch
                {
                }
            }
            textBox_����.Text = string.Format("��{0}�ˣ�ѡ��{1}��", Context.listBox_������.Items.Count, Context.listBox_������.SelectedItems.Count);
        }

        private void button_ɾ��_Click(object sender, EventArgs e)
        {

        }
        [DllImport("TestDll.dll", EntryPoint = "RecieptRemove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public extern unsafe static string RecieptRemove([MarshalAs(UnmanagedType.LPStr)]string sn, [MarshalAs(UnmanagedType.LPStr)]string en,[MarshalAs(UnmanagedType.LPStr)]string userid,[MarshalAs(UnmanagedType.LPStr)]string pwd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);

        private void button_����_Click(object sender, EventArgs e)
        {
            GlobalVar.bBatting = false;
            CancelReceipt();
        }
        /// <summary>
        /// ���ϵ��ű���ƾ֤
        /// </summary>
        string userid = ";USERID=xPkbi2qSPe8=";
        string pwd = ";PWD=OKK5r2tgag3UwE9nNaTCXw==";

        public void CancelReceipt()
        {
            DialogResult dd = MessageBox.Show("ȷ��Ҫ���ϱ���ƾ֤��\r\nӡˢ��ţ�" + textBox_ӡˢ���.Text + "\r\n���ӿ�Ʊ�ţ�" + textBox_��Ʊ��.Text, "�׸�Ƽ�", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dd == DialogResult.Cancel)
                return;
            IsRemoving = true;
            IsPrinting = false;
            opened = false;


            if (textBox_ӡˢ���.Text.Trim().Length == 10 &&
                string.Compare(textBox_ӡˢ���.Text.Trim(), "0000000000", true) > 0 &&//textBox_ӡˢ���.Text.Trim() > "0000000000" &&
                string.Compare(textBox_ӡˢ���.Text.Trim(), "9999999999", true) <= 0 &&//textBox_ӡˢ���.Text.Trim() <= "9999999999" &&
                textBox_��Ʊ��.Text.Trim().Length >= 13
                )//���Ͱ�
            //if(checkCancelReceipt())
            {
                b_3IN1 = true;
                StringBuilder sb = new StringBuilder("", 4096);
                //########################################################################

                string etno = textBox_��Ʊ��.Text.Trim();
                while (etno.IndexOf(' ') > 0 || etno.IndexOf('-') > 0)
                {
                    etno = etno.Remove(etno.IndexOf(' ') > 0 ? etno.IndexOf(' ') : etno.IndexOf('-'), 1);
                }

                RecieptRemove(etno, textBox_ӡˢ���.Text.Trim(), userid, pwd, sb);//ʮ�������ַ����������ӦȥЭ��ͷ��ת��Ϊʮ������ֵ
                string str_send = sb.ToString();
                str_send = EagleString.BaseFunc.Receipt3In1Cancel(textBox_ӡˢ���.Text.Trim(), etno);
                EagleAPI.EagleSendCancelReceipt(str_send);
                b_3IN1 = false;
                //EagleAPI.EagleSend("015" + str_send);

            }
            else//����ȷ���򲻷���
            {
                //MessageBox.Show("��������ȷӡˢ��Ż���ӿ�Ʊ�ţ�", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsRemoving = false;
            }

        }
        public static string str_removereceipt
        {
            set
            {
                bool bValid = false;
                //���ݷ��ش�Command.AV_String�жϴ�ƾ֤�Ƿ����
                {
                    bValid = EagleAPI.IsRemoveReceiptSuccess();
                }
                if (GlobalVar.bBatting && Context.iOperating < GlobalVar.ls3IN1Command.Count)
                {
                    if(!bValid)
                        if (DialogResult.OK == MessageBox.Show("����ƾ֤ʧ�ܣ�\r\n\t�г̵���" + GlobalVar.lsReceiptNumber[Context.iOperating]
                            + "\r\n\t���ӿ�Ʊ��" + GlobalVar.lsEticketNumber[Context.iOperating] + "\r\n�Ƿ����������һ�ţ�",
                            "����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                        {
                            Context.iOperating++;
                            //System.Threading.Thread.Sleep(1000);
                            PrintReceipt.b_3IN1 = true;
                            if (Context.iOperating < GlobalVar.ls3IN1Command.Count)
                                EagleAPI.EagleSendCancelReceipt(GlobalVar.ls3IN1Command[Context.iOperating]);
                            else
                                MessageBox.Show("������ϣ�");
                            
                        }
                }
                else
                {
                    IsRemoving = false;
                    opened = true;//��open��ԭ
                }
            }
        }

        private void button_��ӡ����_Click(object sender, EventArgs e)
        {
            PrintSetup ps = new PrintSetup();
            ps.ShowDialog();
        }

        private void textBox_ӡˢ���_Leave(object sender, EventArgs e)
        {
            string temp = textBox_ӡˢ���.Text.Trim();
            if (temp.Length >= 4)
            {
                textBox_��֤��.Text = temp.Substring(temp.Length - 4, 4);
            }
            else
                textBox_��֤��.Text = "";
        }
        public void Window_CheckET()
        {
            btLianxuPrint.Visible = false;
            button_��ӡ.Visible = false;
            button_���.Visible = false;
            button_�˳�.Visible = false;
            button_����.Visible = false;
            ��ӡ����.Visible = false;
            btOffLinePrint.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button2.Enabled = false;
            button3.Enabled = false;
            progressBar1.Visible = true;
            btA4Print.Visible = false;

            textBox_�ϼ�.ReadOnly = true;
            textBox_��Ʊ��.ReadOnly = true;

            sb_label.Visible = true;
            sb_panel.Visible = true;

            label1.Text = "���ӿ�Ʊ״̬ȷ��";
            label2.Text = "";
            button_����.Visible = false;
            label4.Text = "";
            textBox_ӡˢ���.Visible = false;

            
        }
        public void Window_ManageET()
        {
            Window_CheckET();
            btGetPNR.Visible = true;
            btDetail.Visible = true;
            btPause.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsPrinting = false;
            IsRemoving = false;
            GlobalVar.b_rtByEticket = false;

            retstring = "";

            

            button_���_Click(button_���, e);
            if (textBox_������.Text.Trim().Length != 5) return;

            if (backGroundSubmitEticket)
            {
                EagleAPI.CLEARCMDLIST(7);
                EagleAPI.EagleSendCmd("rT:n/" + textBox_������.Text.Trim(), 7);
            }
            else
            {
                EagleAPI.CLEARCMDLIST(3);
                EagleAPI.EagleSendCmd("rT:n/" + textBox_������.Text.Trim());
            }
            GlobalVar2.tmpPrintPnr = textBox_������.Text;
            textBox_������.Text = GlobalVar.WaitString;

        }
        /// <summary>
        /// �ύ��ť���ȿۿ���ύ���ӿ�Ʊ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            if (b_submitinfo_window || b_checkinfo_window) ;
            else return;
            //1.�ۿ�
            eTicket.etDecFee df = new ePlus.eTicket.etDecFee();
            df.Pnr = textBox_������.Text;
            //df.Pnr = EagleAPI.etstatic.Pnr;
            int peoples = EagleAPI.GetPeopleNumber(retstring_back);

            float ft = 0F;
            try
            {
                ft = (float)listBox_������.Items.Count * float.Parse(textBox_�ϼ�.Text.Substring(3));
                if (peoples != 0) ft = peoples * float.Parse(textBox_�ϼ�.Text.Substring(3));
            }
            catch { ft = 0F; }
            df.TotalFC = ft.ToString("f2");
#if newCheck
            //////�۸�Ϊ0ʱ���л�����
            string[] ips = GlobalVar.loginLC.IPsString_backup.Split('~');
            if (ft == 0F && GlobalVar.stdCount<ips.Length)
            {
                EagleAPI.SpecifyCFG(ips[GlobalVar.stdCount % ips.Length]);
                System.Threading.Thread.Sleep(500);
                GlobalVar.stdCount++;
                return;
            }
            if (GlobalVar.stdCount == ips.Length)
                if (!b_checkinfo_window)
                MessageBox.Show("��ǰ�������ö����ܲ鵽��Ʊ�۸񣬽���0�ƣ�");
            GlobalVar.stdCount = 0;
#else
            //7.���۸�Ϊ�㣬����rt+PNR+~pat:��ȡ���۸�(�ɷ��ؽ������)
            if (ft == 0F)
            {
                EagleAPI.CLEARCMDLIST();
                EagleAPI.EagleSendCmd("rT" + EagleAPI.etstatic.Pnr + "~pat:");
            }
#endif
            //2.�ύ
            eTicket.etStatic ets = new ePlus.eTicket.etStatic();
            ets.Pnr = textBox_������.Text.Trim();
            string[] etno = EagleAPI.GetETNumber(retstring_back);
            for (int i = 0; i < etno.Length; i++)
            {
                ets.etNumber += ";" + etno[i];
            }
            while (ets.etNumber.IndexOf(";;") >= 0)
                ets.etNumber = ets.etNumber.Replace(";;", ";");
            ets.etNumber = ets.etNumber.Substring(1);
            ets.etNumber = mystring.trim(ets.etNumber, ';');

            //�ۿ�
            try
            {
                df.TotalFC = string.Format("{0}", (float)ets.etNumber.Split(';').Length * float.Parse(textBox_�ϼ�.Text.Substring(3)));
            }
            catch
            {
                df.TotalFC = "0";
            }
#if newEticket //�ۿ����etdz�����н���
            if (!df.submitinfo())
            {
                MessageBox.Show("���û�����!PNR=" + df.Pnr + "&�۸�Ϊ" + df.TotalFC);
                if (Text != "���ӿ�Ʊ��̨�˲����") Close();
                return;
            }
#endif


            //��
            //ets.etNumber = "29759183274089";
            ets.FlightNumber1 = textBox103.Text + textBox104.Text;
            ets.FlightNumber2 = textBox203.Text + textBox204.Text;
            if (ets.FlightNumber2 == ets.FlightNumber1) ets.FlightNumber2 = "";
            ets.Bunk1 = textBox105.Text;
            ets.Bunk2 = textBox205.Text;
            ets.CityPair1 = textBox102.Text + textBox202.Text;
            ets.CityPair2 = textBox202.Text + textBox302.Text;
            if (textBox302.Text == "") ets.CityPair2 = "";
            ets.Date1 = textBox106.Text;
            ets.Date2 = textBox206.Text;
            ets.TotalFC = df.TotalFC;
            

            List<string> names = EagleAPI.GetNames(retstring_back);
            string[] cardids = EagleAPI.GetIDCardNo(retstring_back);
            for (int i = 0; i < names.Count; i++)
            {
                ets.Passengers += ";" + names[i] + "-" + cardids[i];
            }
            ets.Passengers = EagleAPI.substring(ets.Passengers, 1, ets.Passengers.Length - 1);
            try
            {
                ets.TotalTaxBuild = string.Format("{0}", (float)ets.etNumber.Split(';').Length * float.Parse(textBox_���������.Text.Substring(2)));
            }
            catch
            {
                ets.TotalTaxBuild = "0";
            }
            try
            {
                ets.TotalTaxFuel = string.Format("{0}", (float)ets.etNumber.Split(';').Length * float.Parse(textBox_ȼ�ͷ�.Text.Substring(2)));
            }
            catch
            {
                
                ets.TotalTaxFuel = "0";
            }
            //ets.TerminalNumber = 
            //string [] sp1={"<eg66>"};
            //for (int i = 0; i < GlobalVar.ipListId.Count; i++)
            //{
            //    if (GlobalVar.ipListId[i].Split(sp1, StringSplitOptions.RemoveEmptyEntries)[0] == GlobalVar.CurIPUsing)
            //    {
            //        try
            //        {
            //            ets.TerminalNumber = GlobalVar.ipListId[i].Split(sp1, StringSplitOptions.RemoveEmptyEntries)[1];
            //            break;
            //        }
            //        catch { }
            //    }
            //}
            try
            {
                ets.TerminalNumber = GlobalVar.officeNumberCurrent.Substring(0, 6);
            }
            catch
            {
                ets.TerminalNumber = GlobalVar.officeNumberCurrent;
            }
            if (textBox102.Text.Trim() != "")
            {
                bool bContainedEticketNumber = eTicketNumbersOfSubmitted.Contains(ets.etNumber);//��ֹ��ͬ���ӿ�Ʊ���ύ
                if (bContainedEticketNumber) { EagleAPI.LogWrite("��ֹ��ͬ���ӿ�Ʊ���ύ��"); Close(); }
                if (bContainedEticketNumber || !ets.SubmitInfo())
                {//�ύ�ɹ�
                    richTextBox1.Text = "���ӿ�Ʊ��Ϣ�ύʧ�ܣ�����������Ա��ϵ��������������ʧ\r" + richTextBox1.Text;
                    button3_Click(sender, e);//ȡ����
                    clear();
                    btGetPNR_Click(sender, e);//�ص�1
                }
                else
                {
                    richTextBox1.Text = "���ӿ�Ʊ��Ϣ�ύ�ɹ�\r" + richTextBox1.Text;
                    if (!b_checkinfo_window) { stoptimer(); Close(); }//MessageBox.Show("��Ʊ�ɹ�"); }
                    eTicketNumbersOfSubmitted.Add(ets.etNumber);
                }
            }
            else
            {
                clear();
                btGetPNR_Click(sender, e);//�ص�1
            }
            if (!b_checkinfo_window) Close();
            else
            {
                button_���_Click(sender, e);
                textBox_������.Text = "";
            }
            button2.Enabled = false;
            button3.Enabled = false;
        }
        /// <summary>
        /// ȡ�������ӿ�Ʊ���е�Ԥ�ύPNR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (b_submitinfo_window || b_checkinfo_window) ;
            else return;

            //ȡ���ۿ�
            ePlus.eTicket.etCreateFailed etcf = new ePlus.eTicket.etCreateFailed();
            etcf.Pnr = textBox_������.Text.Trim();
            if (retstring.IndexOf("**ELECTRONIC TICKET PNR**") > -1) return;
            if (!etcf.submitinfo())
            {
                if (!b_checkinfo_window)
                    MessageBox.Show("ʧ�ܣ����Ժ�����");
            }
            richTextBox1.Text = "ȡ���ۿ�ɹ���" + richTextBox1.Text;


            textBox_������.Text = "ȡ���ɹ�";
            button2.Enabled = false;
            button3.Enabled = false;
            if (Text != "���ӿ�Ʊ��̨�˲����")
            {
                if (!b_checkinfo_window)
                    MessageBox.Show("��Ʊʧ��");
                Close();
            }
        }
        private void btGetPnrFake()
        {
            Thread th0 = new Thread(new ThreadStart(getpnrthread));
            th0.Start();


        }
        void getpnrthread()
        {
            ePlus.eTicket.etGetUncheckedPnr etgp = new ePlus.eTicket.etGetUncheckedPnr();
            textBox_������.Text = etgp.getpnr();
            textBox_������.Text = EagleAPI.etstatic.Pnr;

            tbKeyUp1();
        }
        void getpnrthreadbutton()
        {
            //2.��û��PNR�����˳�(���̽���)
            ePlus.eTicket.etGetUncheckedPnr etgp = new ePlus.eTicket.etGetUncheckedPnr();
            textBox_������.Text = etgp.getpnr();
            label17.Text = "PNR��" + textBox_������.Text;
            if (bPause) return;
            if (!b_checkinfo_window) return;

            if (textBox_������.Text.Trim() == "")
            {
                //��7Э��󣬲������˶Ի���
                if (!backGroundSubmitEticket)
                {
                    if (!GlobalVar.etProcessing)//���ǹ�Ʊ������ʾ������ֱ�ӹر�
                        MessageBox.Show("û����Ҫ���ĵ��ӿ�Ʊ��");
                }
                stoptimer();
                Close();
                //Dispose();
            }
            //3.����rt+PNR(��ȡ��PNR����)
            else
            {
                richTextBox1.Text = "���δ�˶Ե��Ժ�" + textBox_������.Text.Trim() + "\r\n" + richTextBox1.Text;
                EagleAPI.LogWrite("���δ�˶Ե��Ժ�" + textBox_������.Text.Trim());
                clear();
                tbKeyUp1();
            }
        }
        private void btGetPNR_Click(object sender, EventArgs e)
        {//��ȡPNR
            if (b_submitinfo_window)
            {

                Close();
                return;
            }
            bGetPat = false;
            timerProgress.Start();
#if !newCheck
            ePlus.eTicket.etGetUncheckedPnr etgp = new ePlus.eTicket.etGetUncheckedPnr();
            textBox_������.Text = etgp.getpnr();
            if (textBox_������.Text.Trim() == "")
            {
                MessageBox.Show("û����Ҫ���ĵ��ӿ�Ʊ��");
                stoptimer();
                Dispose();
            }
#else
            Thread th0 = new Thread(new ThreadStart(getpnrthreadbutton));
            th0.Start();



#endif
        }

        private void ptDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            //�г̵���+1
            if (textBox_ӡˢ���.InvokeRequired)
            {
                EventHandler eh = new EventHandler(addnumber);
                TextBox tb = textBox_ӡˢ���;
                textBox_ӡˢ���.Invoke(eh, new object[] { tb, EventArgs.Empty });
            }
            //�ύ��ӡ��־
            LocalOperation.LogOperation.staticwritelocallog("��ӡ�г̵�", "PNR:" + textBox_������.Text + "\r\n" + "���ӿ�Ʊ��:" + textBox_��Ʊ��.Text + "\r\n" + "�г̵���:" + textBox_ӡˢ���.Text + "\r\n");
        }
        private void addnumber(object sender, EventArgs e)
        {
            try
            {
                long number = long.Parse(textBox_ӡˢ���.Text.Trim());
                //number++;//�ر��г̵����Զ���һ��С���� commented by chenqj
                textBox_ӡˢ���.Text = number.ToString("d10");
                textBox_��֤��.Text = EagleAPI.substring(textBox_ӡˢ���.Text.Trim(), textBox_ӡˢ���.Text.Trim().Length - 4, 4);
                eTicket.AutoSetReceiptNumber asn = new ePlus.eTicket.AutoSetReceiptNumber();
                asn.SaveReceiptNumberToXML(textBox_ӡˢ���.Text.Trim());
            }
            catch
            {
            }
        }

        private void textBox_Ʊ��_Leave(object sender, EventArgs e)
        {
            caltotal();
        }

        private void textBox_���������_Leave(object sender, EventArgs e)
        {
            caltotal();
        }

        private void textBox_ȼ�ͷ�_Leave(object sender, EventArgs e)
        {
            caltotal();
        }
        void caltotal()
        {
            string fc = textBox_Ʊ��.Text.Trim();
            string cn = textBox_���������.Text.Trim();
            string yq = textBox_ȼ�ͷ�.Text.Trim();
            if (EagleAPI.substring(fc, 0, 3).ToUpper() != "CNY" || EagleAPI.substring(cn,0,2).ToUpper()!="CN" || EagleAPI.substring(yq,0,2).ToUpper()!="YQ") return;
            fc = fc.Substring(3).Trim();
            cn = cn.Substring(2).Trim();
            yq = yq.Substring(2).Trim();
            try
            {
                float temp = float.Parse(fc) + float.Parse(cn) + float.Parse(yq);
                textBox_�ϼ�.Text ="CNY"+ temp.ToString("f2");
            }
            catch{}
        }

        private void btOffLinePrint_Click(object sender, EventArgs e)
        {
            string receiptNumber = textBox_ӡˢ���.Text.Trim();
            if (textBox_ӡˢ���.Text.Trim() == "7166206881")//added by chenqj
            {
                MessageBox.Show("��֤�� 7166206881 �Ѿ���ʹ�ù�������ȷ���룡", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (receiptNumber == "6001022230")//added by chenqj
            {
                MessageBox.Show("��֤�� 6001022230 �Ѿ���ʹ�ù�������ȷ���룡", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EagleAPI.PrinterSetup(ptDoc)) return;
            PrintDialog pd = new PrintDialog();
            try
            {
                EagleAPI.PrinterSetupCostom(ptDoc, 950, 400);
            }
            catch(Exception ex)
            {
                MessageBox.Show("btOffLinePrint_Click:" + ex.Message);
            }

            pd.Document = ptDoc;
            ptDoc_Print();
            //ptDoc.Print();
        }
        void ptDoc_Print()
        {
#if receipt2
            //check Fare
            try
            {
                string checkFare = textBox_Ʊ��.Text.Trim().ToUpper();
                if (checkFare.Substring(0, 3) == "CNY")
                {
                    while (checkFare.Length < 11)
                    {
                        checkFare = checkFare.Insert(3, " ");
                    }
                    textBox_Ʊ��.Text = checkFare;
                }
            }
            catch { }
            //check AIRPORT TAX
            try
            {
                string airportTax = textBox_���������.Text.Trim().ToUpper();
                if (airportTax.Substring(0, 2) == "CN")
                {
                    while (airportTax.Length < 10)
                    {
                        airportTax = airportTax.Insert(2, " ");
                    }
                    textBox_���������.Text = airportTax;
                }
            }
            catch { }
            //check FUEL SURCHARGE
            try
            {
                string fuelSurcharge = textBox_ȼ�ͷ�.Text.Trim().ToUpper();
                if (fuelSurcharge.Substring(0, 2) == "YQ")
                {
                    while (fuelSurcharge.Length < 10)
                    {
                        fuelSurcharge = fuelSurcharge.Insert(2, " ");
                    }
                    textBox_ȼ�ͷ�.Text = fuelSurcharge;
                }
            }
            catch
            {
            }
            //check TOTAL
            try
            {
                string total = textBox_�ϼ�.Text.Trim().ToUpper();
                if (total.Substring(0, 3) == "CNY")
                {
                    if (total.Substring(3, 1) != " ") total = total.Insert(3, " ");
                    textBox_�ϼ�.Text = total;
                }
            }
            catch { }

#endif
             if ((!EagleAPI2.ExistFont("TEC")))// ||( !EagleAPI2.ExistFont("MingLiU") )|| (!EagleAPI2.ExistFont("����_GB2312")))
            {
                if (MessageBox.Show("δ�ҵ�������ӡ����,�Ƿ����?\n���ǽ�ʹ����ͨ�����ӡ��", "ע��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ptDoc.Print();
                }
            }
            else
            {
                ptDoc.Print();
            }

            ReceiptNumberLog(true);//added by chenqj
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //tbKeyUp1();
            //timer1.Stop();
            //timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (button2.Enabled) button2_Click(sender, e);
            //if (button3.Enabled) button3_Click(sender, e);
            //if (b_checkinfo_window) timer3.Start();
            //if (button3.Enabled || button2.Enabled)
            //{
            //    timer2.Stop();

            //    if (b_submitinfo_window)
            //    {
            //        Close();
            //        timer3.Stop();
            //    }
            //}
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
#if !newCheck
            btGetPNR_Click(sender, e);
            timer3.Stop();
            timer1.Start();
#else
            //btGetPNR_Click(sender, e);//1.��ȡPNR(���̿�ʼ)
#endif

        }
        void stoptimer()
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timerProgress.Stop();
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == progressBar1.Maximum) progressBar1.Value = progressBar1.Minimum;
        }

        private void testPrint_Click(object sender, EventArgs e)
        {
            comboBox_ǩע.Text = "����ǩתCZ ONLY";
            textBox_���շ�.Text = "XXX";
            textBox_������.Text = "ABCDE";
            textBox_�ϼ�.Text = "CNY TOTAL";
            textBox_���������.Text = "CN50.00";
            textBox_��Ʊ��.Text = "9991234567890";
            textBox_������Ʊ.Text = "CONTINUAL TICKET";
            textBox_�ÿ�����.Text = "���޿Ƽ�";
            textBox_Ʊ��.Text = "CNY1440.00";
            textBox_������.Text = "OTHER   TAX";
            textBox_ȼ�ͷ�.Text = "YQ80.00";
            textBox_����.Text = "����";
            textBox_���λ.Text = "�����������޹�˾";
            tbSaleCode2.Text = "AIR NUMBER";
            textBox_�����.Text = "2007-08-09";
            textBox_���۵�λ����.Text = "OFFICE";
            textBox_��֤��.Text = "1234";
            textBox_ӡˢ���.Text = "ӡˢ���";
            textBox_֤������.Text = "330225198508060816";
            textBox101.Text = "�人";
            textBox102.Text = "WUH";
            textBox103.Text = "MU";
            textBox104.Text = "2501";
            textBox105.Text = "Y";
            textBox106.Text = "01JAN";
            textBox107.Text = "0840";
            textBox109.Text = "Y45";
            textBox10c.Text = "20K";

            textBox201.Text = "�Ϻ�";
            textBox202.Text = "SHA";
            textBox203.Text = "CA";
            textBox204.Text = "3389";
            textBox205.Text = "Y";
            textBox206.Text = "02JAN";
            textBox207.Text = "0940";
            textBox209.Text = "Y88";
            textBox20c.Text = "20K";

            textBox301.Text = "����";
            textBox302.Text = "PEK";
            textBox303.Text = "MF";
            textBox304.Text = "8888";
            textBox305.Text = "F";
            textBox306.Text = "03JAN";
            textBox307.Text = "1040";
            textBox309.Text = "Y99";
            textBox30c.Text = "20K";

            textBox401.Text = "VOID";
            textBox402.Text = "VOID";

            

            btOffLinePrint_Click(sender, e);
        }
        bool bPause = false;
        bool bGetPat = false;
        private void btPause_Click(object sender, EventArgs e)
        {
            
#if !newCheck
            stoptimer();
#else 
            if (btPause.Text == "��ͣ")
            {
                timerProgress.Stop();
                btPause.Text = "����";
                bPause = true;
            }
            else
            {
                timerProgress.Start();
                btPause.Text = "��ͣ";
                bPause = false;
                btGetPNR_Click(sender, e);
            }
#endif
        }

        private void btDetail_Click(object sender, EventArgs e)
        {
            Width = 1190;
        }
        public void disableControls()
        {
            textBox_���������.Enabled = false;
            textBox_ȼ�ͷ�.Enabled = false;
            textBox_Ʊ��.Enabled = false;
            textBox_������.Enabled = false;
            textBox_�ϼ�.Enabled = false;
            textBox_�ÿ�����.Enabled = false;
            textBox101.Enabled = false;
            textBox201.Enabled = false;
            textBox301.Enabled = false;
            textBox401.Enabled = false;
            textBox102.Enabled = false;
            textBox202.Enabled = false;
            textBox302.Enabled = false;
            textBox402.Enabled = false;
            //textBox103.Enabled = false;
            //textBox104.Enabled = false;
            //textBox105.Enabled = false;
            //textBox106.Enabled = false;
            //textBox107.Enabled = false;
            //textBox108.Enabled = false;
            //textBox109.Enabled = false;
            //textBox10a.Enabled = false;
        }
        public void display��Ʊ()
        {
            backGroundSubmitEticket = true;
            label15.Visible = true;
            panel1.Visible = true;
            label16.Visible = true;
            Width = panel1.Width;
            Height = panel1.Height;
            //FormBorderStyle = FormBorderStyle.None;
        }

        private void btGetCardId_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.formSendCmdType = GlobalVar.FormSendCommandType.detrF;
                EagleAPI.CLEARCMDLIST(3);
                string etnumber = textBox_��Ʊ��.Text.Trim().Replace(' ', '-');
                EagleAPI.EagleSendCmd("detr:tn/" + etnumber + ",f");
                textBox_֤������.Text = "���Եȡ�������";
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }
        private bool checkPrintNo()
        {
            
            string printno = textBox_ӡˢ���.Text.Trim();
            if (printno == "") return false;
            string[] split = printno.Split('-');
            if (split.Length >= 2) return false;
            if (split[0].Length != 10) return false;
            try { long ll = (long.Parse(split[0])); }
            catch { return false; }
            if (split.Length == 2)
            {
                if (split[1].Length != 2) return false;
                try { int.Parse(split[1]); }
                catch { return false; }
            }
            return true;
        }
        private bool checkTicketNo()
        {
            string printno = textBox_��Ʊ��.Text.Trim();
            if (printno == "") return false;
            if (printno[3] == '-') printno = printno.Remove(3, 1);
            printno = printno.Replace('/', '-');
            string[] split = printno.Split('-');
            if (split.Length >= 2) return false;
            if (split[0].Length != 13) return false;
            try { long ll = (long.Parse(split[0])); }
            catch { return false; }
            if (split.Length == 2)
            {
                if (split[1].Length != 2) return false;
                try { int.Parse(split[1]); }
                catch { return false; }
            }
            return true;
        }
        private bool checkCancelReceipt()
        {
            if (!checkTicketNo())
            {
                MessageBox.Show("���ӿ�Ʊ�Ŵ���!");
                return false;
            }
            if (!checkPrintNo())
            {
                MessageBox.Show("����ƾ֤�Ŵ���!");
                return false; ;
            }
            return true;
        }

        public int iOperating = 0;
        private void btBatCancel_Click(object sender, EventArgs e)
        {
            batcancel();
        }
        private void batcancel()
        {
            iOperating = 0;
            IsRemoving = true;
            IsPrinting = false;
            opened = false;

            GlobalVar.bBatting = true;
            Options.BatReceipt br = new Options.BatReceipt(false);
            if (br.ShowDialog() != DialogResult.OK) return; ;

            DialogResult dd = MessageBox.Show("ȷ��Ҫ���ϱ���ƾ֤��", "�׸�Ƽ�", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dd == DialogResult.Cancel || br.lsEticketNumber.Count<=0)
                return;

            GlobalVar.lsReceiptNumber.Clear();
            GlobalVar.lsEticketNumber.Clear();
            GlobalVar.ls3IN1Command.Clear();
            StringBuilder sb = new StringBuilder("", 1024);
            for (int i = 0; i < br.lsReceiptNumber.Count; i++)
            {
                GlobalVar.lsEticketNumber.Add(br.lsEticketNumber[i]);
                GlobalVar.lsReceiptNumber.Add(br.lsReceiptNumber[i]);
                RecieptRemove(br.lsEticketNumber[i], br.lsReceiptNumber[i], userid, pwd, sb);
                GlobalVar.ls3IN1Command.Add(sb.ToString());
            }
            b_3IN1 = true;
            EagleAPI.EagleSendCancelReceipt(GlobalVar.ls3IN1Command[0]);
            
            br.Dispose();

        }
        private void btLianxuPrint_Click(object sender, EventArgs e)
        {

            batprint();
        }
        bool bLianXuPrintOnline = true;
        private void batprint()
        {
            if (!canLinaxuPrint)
            {
                MessageBox.Show("����PNR��ȡ�����������ӡ");
                return;
            }
            if (EagleAPI.GetETNumber(retstring_back).Length <= 1)
            {
                MessageBox.Show("�����˿ͣ���ֱ�Ӵ�ӡ");
                return;
            }
            GlobalVar.lsEticketNumber.Clear();
            GlobalVar.lsRctIdCard.Clear();
            GlobalVar.lsRctName.Clear();

            GlobalVar.bBatting = true;
            iOperating = 0;
            IsRemoving = false;
            IsPrinting = true;
            opened = false;
            Options.BatReceipt br = new Options.BatReceipt(true);
            for (int i = 0; i < EagleAPI.GetETNumber(retstring_back).Length; i++)
            {
                br.lsEticketNumber.Add(EagleAPI.GetETNumber(retstring_back)[i]);
                br.lsRcpName.Add(EagleAPI.GetNames(retstring_back)[i]);
                br.lsRcpIdCard.Add(EagleAPI.GetIDCardNo(retstring_back)[i]);
            }
            {
                if (btOffLinePrint.Enabled)
                {
                    DialogResult drTemp = MessageBox.Show("�Ƿ�����������ӡ?\r\n������ӡ �� ��\r\n�ѻ���ӡ �� ��", "ע��", MessageBoxButtons.YesNoCancel);
                    if (drTemp == DialogResult.Cancel) return;
                    if (drTemp == DialogResult.Yes) bLianXuPrintOnline = true;
                    if (drTemp == DialogResult.No) bLianXuPrintOnline = false;
                }
            }

            if (br.ShowDialog() != DialogResult.OK) return;
            DialogResult dd = MessageBox.Show("ȷ��Ҫ������ӡ����ƾ֤��", "�׸�Ƽ�", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dd == DialogResult.Cancel || br.lsEticketNumber.Count <= 0)
                return;
            GlobalVar.lsReceiptNumber.Clear();
            GlobalVar.lsEticketNumber.Clear();
            GlobalVar.lsRctIdCard.Clear();
            GlobalVar.lsRctName.Clear();

            GlobalVar.ls3IN1Command.Clear();


            StringBuilder sb = new StringBuilder("", 1024);
            for (int i = 0; i < br.lsReceiptNumber.Count; i++)
            {
                GlobalVar.lsEticketNumber.Add(br.lsEticketNumber[i]);
                GlobalVar.lsReceiptNumber.Add(br.lsReceiptNumber[i]);
                GlobalVar.lsRctIdCard.Add(br.lsRcpIdCard[i]);
                GlobalVar.lsRctName.Add(br.lsRcpName[i]);
                RecieptPrint(br.lsEticketNumber[i], br.lsReceiptNumber[i], sb);
                GlobalVar.ls3IN1Command.Add(sb.ToString());
            }
            b_3IN1 = true;

            if (bLianXuPrintOnline)
            {
                EagleAPI.EagleSendPrintReceipt(GlobalVar.ls3IN1Command[0]);
            }
            else
            {
                for (int i = 0; i < br.lsReceiptNumber.Count; i++)
                {
                    textBox_��Ʊ��.Text = GlobalVar.lsEticketNumber[i];
                    textBox_ӡˢ���.Text = GlobalVar.lsReceiptNumber[i];
                    textBox_֤������.Text = GlobalVar.lsRctIdCard[i];
                    textBox_�ÿ�����.Text = GlobalVar.lsRctName[i];
                    Application.DoEvents();
                    ptDoc.Print();
                }
            }

            br.Dispose();

        }
        private delegate void SetControlCallback(int iPos);
        private SetControlCallback setControlCallback;
        private void setprintctrl(int iPos)
        {
            if (textBox_�ÿ�����.InvokeRequired)
            {
                textBox_�ÿ�����.Invoke(setControlCallback,iPos);
            }
            else
            {
                textBox_��Ʊ��.Text = GlobalVar.lsEticketNumber[iPos];
                textBox_ӡˢ���.Text = GlobalVar.lsReceiptNumber[iPos];
                textBox_�ÿ�����.Text = GlobalVar.lsRctName[iPos];
                textBox_֤������.Text = GlobalVar.lsRctIdCard[iPos];
                textBox_��֤��.Text = Context.textBox_ӡˢ���.Text.Trim().Substring(6, 4);
            }
        }

        private void btA4Print_Click(object sender, EventArgs e)
        {
            Options.A4ReceiptPrint a4 = new Options.A4ReceiptPrint();
            a4.bigPnr = bigCode;
            a4.passagerName = textBox_�ÿ�����.Text;
            a4.idcardNumber = textBox_֤������.Text;
            a4.airlineName = EagleAPI.GetAirLineName(textBox103.Text);
            a4.sailinfo[0][0] = textBox101.Text;
            a4.sailinfo[0][1] = textBox103.Text + textBox104.Text;
            a4.sailinfo[0][2] = textBox105.Text;
            a4.sailinfo[0][3] = textBox106.Text;
            a4.sailinfo[0][4] = textBox107.Text;
            a4.sailinfo[0][5] = "/";
            a4.sailinfo[0][6] = textBox108.Text;
            a4.sailinfo[0][7] = textBox10c.Text;

            a4.sailinfo[1][0] = textBox201.Text;
            if (textBox203.Text.Trim() != "")
            {
                a4.sailinfo[1][1] = textBox203.Text + textBox204.Text;
                a4.sailinfo[1][2] = textBox205.Text;
                a4.sailinfo[1][3] = textBox206.Text;
                a4.sailinfo[1][4] = textBox207.Text;
                a4.sailinfo[1][5] = "/";
                a4.sailinfo[1][6] = textBox208.Text;
                a4.sailinfo[1][7] = textBox20c.Text;
            }
            a4.sailinfo[2][0] = textBox301.Text;
            if (textBox303.Text.Trim() != "")
            {
                a4.sailinfo[2][1] = textBox303.Text + textBox304.Text;
                a4.sailinfo[2][2] = textBox305.Text;
                a4.sailinfo[2][3] = textBox306.Text;
                a4.sailinfo[2][4] = textBox307.Text;
                a4.sailinfo[2][5] = "/";
                a4.sailinfo[2][6] = textBox308.Text;
                a4.sailinfo[2][7] = textBox30c.Text;
            }
            a4.fc = EagleAPI.GetFareCal(retstring);//
            a4.fp = textBox_Ʊ��.Text;
            a4.ft = textBox_�ϼ�.Text;
            a4.restriction = comboBox_ǩע.Text;
            a4.smallPnr = textBox_������.Text;
            a4.ticketNumber = textBox_��Ʊ��.Text;
            a4.��Ʊ = textBox_������Ʊ.Text;
            a4.ticketTime = textBox_�����.Text;
            a4.��Эcode = tbSaleCode2.Text;
            a4.taxBuild = textBox_���������.Text;
            a4.taxFuel = textBox_ȼ�ͷ�.Text;
            a4.ShowDialog();
        }

        private void PrintReceipt_MouseClick(object sender, MouseEventArgs e)
        {
            printReceiptMouseClick(this, e);
        }
        void printReceiptMouseClick(Control c , MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightMenu = new ContextMenuStrip();
                EventHandler eh = new EventHandler(erppnr);
                if (GlobalVar.printProgram)
                {
                    PrintHyx.rightMenuHYX hyxMenu = new ePlus.PrintHyx.rightMenuHYX(this);
                    hyxMenu.ShowHYXMenu(this, e);
                }
                else
                {
                    rightMenu.Items.Add("ErpPnr", null, eh);
                    rightMenu.Show(c, e.X, e.Y);
                }
                
            }
        }
        private void erppnr(object sender, EventArgs e)
        {
            if (GlobalVar.b_erp_��Ʊ¼�뵥)
            {
                try
                {
                    string name = "";
                    for (int i = 0; i < listBox_������.Items.Count; i++)
                    {
                        name += listBox_������.Items[i].ToString() + ",";
                    }
                    if (name.Length > 0) name = name.Substring(0, name.Length - 1);
                    string sail = textBox102.Text + "/" + (textBox302.Text == "" ? textBox202.Text : textBox302.Text);
                    string pnr = textBox_������.Text;
                    string d1 = "";
                    string tempdate = textBox106.Text;
                    if (tempdate != "")
                    {
                        if (tempdate[0] == '0') tempdate = tempdate.Substring(1);
                        string tempday = tempdate.Substring(0, tempdate.Length - 3);
                        string tempmonth = EagleAPI.GetMonthInt(mystring.right(tempdate, 3));
                        string tempyear = System.DateTime.Now.Year.ToString();
                        if ((int.Parse(tempmonth) - System.DateTime.Now.Month) > 6) tempyear = System.DateTime.Now.AddYears(-1).Year.ToString();
                        d1 = tempyear + "-" + tempmonth + "-" + tempday;
                    }
                    else
                    {
                        d1 = "";
                    }
                    string hour1 = "";
                    string minute1 = "";
                    try
                    {
                        hour1 = textBox107.Text.Substring(0, 2);
                        minute1 = textBox107.Text.Substring(2, 2);
                    }
                    catch
                    {
                        hour1 = "";
                        minute1 = "";
                    }

                    string d2 = "";
                    tempdate = textBox206.Text;
                    if (tempdate != "")
                    {
                        if (tempdate[0] == '0') tempdate = tempdate.Substring(1);
                        string tempday = tempdate.Substring(0, tempdate.Length - 3);
                        string tempmonth = EagleAPI.GetMonthInt(mystring.right(tempdate, 3));
                        string tempyear = System.DateTime.Now.Year.ToString();
                        if ((int.Parse(tempmonth) - System.DateTime.Now.Month) > 6) tempyear = System.DateTime.Now.AddYears(-1).Year.ToString();
                        d2 = tempyear + "-" + tempmonth + "-" + tempday;
                    }
                    else
                    {
                        d2 = "";
                    }
                    string hour2 = "";
                    string minute2 = "";
                    try
                    {
                        hour1 = textBox207.Text.Substring(0, 2);
                        minute1 = textBox207.Text.Substring(2, 2);
                    }
                    catch
                    {
                        hour2 = "";
                        minute2 = "";
                    }
                    GlobalVar.newEticket.SetVarsByForm(name, sail, pnr, d1, hour1, minute1, d2, hour2, minute2, textBox103.Text, textBox104.Text, textBox105.Text, textBox203.Text, textBox204.Text == "VOID" ? "" : textBox204.Text, textBox205.Text);
                    GlobalVar.newEticket.SetErp();
                }
                catch
                {
                    MessageBox.Show("ERP�������ȷ��");
                }
            }
        }

        #region �������β�Ʒ�����г̵�
        private ReceiptType rType = ReceiptType.Default;
        public void applyType(ReceiptType rt)
        {
            rType = rt;
            switch (rt)
            {

                case ReceiptType.Business:
                    Text = "�������β�Ʒ��ӡ";
                    panelBusiness.Visible = true;
                    label1.Text = "�������β�Ʒ�����г̵�";
                    label2.Visible = false;
                    btBatCancel.Visible = false;
                    button_����.Visible = false;
                    tbBusinessSignDate.Text = System.DateTime.Now.ToShortDateString();
                    BusinessLoad();
                    break;

                case ReceiptType.Default:
                default:
                    break;
            }
        }

        private void btBusinessPrint_Click(object sender, EventArgs e)
        {
            rType = ReceiptType.Business;
            PrintDialog pd = new PrintDialog();
            EagleAPI.PrinterSetupCostom(Context.ptDoc, 950, 400);
            pd.Document = ptDoc;
            ptDoc_Print();
            //ptDoc.Print();
        }
        private void BusinessLoad()
        {
            //��4��ֵ
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\options.xml");
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("BusinessPrint");
                tbBusinessCompany.Text = xn.SelectSingleNode("agentname").InnerText;
                tbBusinessPhone.Text = xn.SelectSingleNode("agentphone").InnerText;
            }
            catch (Exception ee)
            {
                //MessageBox.Show("BusinessLoad" + ee.Message);
            }

        }
        private void BusinessClose()
        {
            //д4��ֵ
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\options.xml");
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("BusinessPrint");
                XmlElement xe;
                if (xn == null)
                {
                    xe = xd.CreateElement("BusinessPrint");
                    xe.InnerText = "";
                    xn = xd.SelectSingleNode("eg");
                    xn.AppendChild(xe);
                    xn = xn.SelectSingleNode("BusinessPrint");
                }
                //else
                {
                    if (xn.SelectSingleNode("agentname") == null)
                    {
                        xe = xd.CreateElement("agentname");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }

                    if (xn.SelectSingleNode("agentphone") == null)
                    {
                        xe = xd.CreateElement("agentphone");
                        xe.InnerText = "";
                        xn.AppendChild(xe);
                    }

                }
                xd.SelectSingleNode("eg").SelectSingleNode("BusinessPrint").SelectSingleNode("agentname").InnerText = tbBusinessCompany.Text;
                xd.SelectSingleNode("eg").SelectSingleNode("BusinessPrint").SelectSingleNode("agentphone").InnerText = tbBusinessPhone.Text;

                xd.Save(Application.StartupPath + "\\options.xml");
            }
            catch (Exception ee)
            {
                MessageBox.Show("BusinessClose" + ee.Message);
            }
        }
        private void printbussiness(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontEn = new Font("tec", GlobalVar.fontsizeen, System.Drawing.FontStyle.Regular);
            Font ptFontCn = new Font("tec", GlobalVar.fontsizecn, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = GlobalVar.o_receipt;
            //1.�ÿ�����25,25
            e.Graphics.DrawString(textBox_�ÿ�����.Text, ptFontCn, ptBrush, 25F + o.X, 25F + o.Y);
            //2.��Ч���֤������70,25
            e.Graphics.DrawString(textBox_֤������.Text, ptFontEn, ptBrush, 70F + o.X, 25F + o.Y);
            //3.ǩע134,25
            e.Graphics.DrawString(comboBox_ǩע.Text, ptFontCn, ptBrush, 134F + o.X, 25F + o.Y);
            //4.Ʊ��66,75->����
            e.Graphics.DrawString(tbBusinessUnitPrice.Text, ptFontEn, ptBrush, 66F + o.X, 75F + o.Y);
            //8.�ϼ�175,75
            e.Graphics.DrawString(tbBusinessTotal.Text, ptFontEn, ptBrush, 175F + o.X, 75F + o.Y);
            //13.���۵�λ����40,92->�绰
            e.Graphics.DrawString(tbBusinessPhone.Text, ptFontEn, ptBrush, 40F + o.X, 92F + o.Y);
            
            //14.���λ100,92
            e.Graphics.DrawString(tbBusinessCompany.Text, ptFontCn, ptBrush, 100F + o.X, 92F + o.Y);
            //15.�����186,92
            e.Graphics.DrawString(tbBusinessSignDate.Text, ptFontEn, ptBrush, 182F + o.X, 92F + o.Y);
            //16.������
            e.Graphics.DrawString(textBox_������.Text.ToUpper(), ptFontEn, ptBrush, 25F + o.X, 31F + o.Y);

            //������Ϣ
            e.Graphics.DrawString(textBox101.Text, ptFontCn, ptBrush, 31F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox102.Text, ptFontCn, ptBrush, 54F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox103.Text, ptFontCn, ptBrush, 66F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox104.Text, ptFontCn, ptBrush, 78F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox105.Text, ptFontCn, ptBrush, 95F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox106.Text, ptFontCn, ptBrush, 102F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox107.Text, ptFontCn, ptBrush, 123F + o.X, 41F + o.Y);
            //e.Graphics.DrawString(textBox108.Text, ptFontCn, ptBrush, 175F+o.X, 41F+o.Y);
            e.Graphics.DrawString(textBox109.Text, ptFontCn, ptBrush, 135F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox10a.Text, ptFontCn, ptBrush, 175F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox10b.Text, ptFontCn, ptBrush, 192F + o.X, 41F + o.Y);
            e.Graphics.DrawString(textBox10c.Text, ptFontCn, ptBrush, 204F + o.X, 41F + o.Y);

            e.Graphics.DrawString(textBox201.Text, ptFontCn, ptBrush, 31F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox202.Text, ptFontCn, ptBrush, 54F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox203.Text, ptFontCn, ptBrush, 66F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox204.Text, ptFontCn, ptBrush, 78F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox205.Text, ptFontCn, ptBrush, 95F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox206.Text, ptFontCn, ptBrush, 102F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox207.Text, ptFontCn, ptBrush, 123F + o.X, 50F + o.Y);
            //e.Graphics.DrawString(textBox208.Text, ptFontCn, ptBrush, 175F+o.X, 41F+o.Y);
            e.Graphics.DrawString(textBox209.Text, ptFontCn, ptBrush, 135F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox20a.Text, ptFontCn, ptBrush, 175F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox20b.Text, ptFontCn, ptBrush, 192F + o.X, 50F + o.Y);
            e.Graphics.DrawString(textBox20c.Text, ptFontCn, ptBrush, 204F + o.X, 50F + o.Y);

            e.Graphics.DrawString(textBox301.Text, ptFontCn, ptBrush, 31F + o.X, 58F + o.Y);
            e.Graphics.DrawString(textBox302.Text, ptFontCn, ptBrush, 54F + o.X, 58F + o.Y);


            Font ptFontRemark = new Font("system", 8, System.Drawing.FontStyle.Regular);
            e.Graphics.DrawString(tbBusinessName.Text, ptFontRemark, ptBrush, 40F + o.X, 66F + o.Y);
            e.Graphics.DrawString(tbBusinessRemark.Text, ptFontRemark, ptBrush, 40F + o.X, 84F + o.Y);

        }
        #endregion

        #region �ύ���ӿ�Ʊ����
        public void setToSubmitForm()
        {
            StartPosition = FormStartPosition.Manual;
            Size = new Size(1, 1);
            Location = new Point(0, 0);
        }
        #endregion

        private void ptDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //���һ��������������Ƿ��Ѿ�������
            GlobalVar2.tmpPrintName = textBox_�ÿ�����.Text;
            GlobalVar2.tmpPrintPnr = textBox_������.Text.ToUpper();
        }

        private void cbIbe_CheckedChanged(object sender, EventArgs e)
        {
            BookTicket.bIbe = cbIbe.Checked;
        }

        /// <summary>
        /// ��ӡ��ͬʱ��¼���г̵��ţ�����̨����鿴 added by chenqj
        /// </summary>
        void ReceiptNumberLog(bool isOffline)
        {
            if (GlobalVar.serverAddr != GlobalVar.ServerAddr.Eagle)
                return;

            string strRe = string.Empty;
            string receiptNumber = textBox_ӡˢ���.Text.Trim();

            try
            {
                gs.para.NewPara np = new gs.para.NewPara();
                np.AddPara("cm", "ReceiptNumberLog");
                np.AddPara("username", GlobalVar.loginName);
                np.AddPara("pnr", textBox_������.Text.Trim());
                np.AddPara("receiptNumber", receiptNumber);

                if(isOffline)
                    np.AddPara("isOffline", "1");
                else
                    np.AddPara("isOffline", "0");

                EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                strRe = ws.getEgSoap(np.GetXML());

                if (!string.IsNullOrEmpty(strRe))
                {
                    gs.para.NewPara np1 = new gs.para.NewPara(strRe);
                    string err = np1.FindTextByPath("//eg/err");

                    if (!string.IsNullOrEmpty(err))
                    {
                        MessageBox.Show("��������ӡ���յ���Ϣ��" + System.Environment.NewLine + err, "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("���� WebService:ReceiptNumberLog ����δ֪����", "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("ReceiptNumberLog() ��������" + System.Environment.NewLine + ee.Message, "�׸�Ƽ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        string strprint = "";
        bool zzprintbus = false;
        private void btZzPrintBus_Click(object sender, EventArgs e)
        {
            strprint = "";
            strprint += "����: ";
            strprint += textBox_�ÿ�����.Text;
            int iBlankets = System.Text.Encoding.Default.GetBytes(strprint).Length;
            if (textBox101.Text.Trim() != "")
            {
                strprint += "    �����:" + textBox101.Text;
                strprint += "    ���ʱ��:" + textBox106.Text + " " + textBox107.Text;
            }
            //if (textBox201.Text.Trim() != "")
            //{
            //    for (int i = 0; i < iBlankets; i++) strprint += " ";
            //    strprint += "\r\n";
            //    strprint += "    �����:" + textBox201.Text;
            //    strprint += "    ���ʱ��:" + textBox206.Text + " " + textBox207.Text;

            //}
            zzprintbus = true;
            ptDoc.Print();
            zzprintbus = false;
        }
        void ZzPrintBus(string str, object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            Font ptFontEn = new Font("TEC", GlobalVar.fontsizeen, System.Drawing.FontStyle.Regular);
            Font ptFontCn = new Font("TEC", GlobalVar.fontsizecn, System.Drawing.FontStyle.Regular);
            if (!EagleAPI2.ExistFont("TEC"))
            {
                //if (GlobalVar.fontsizecn <= 8 || GlobalVar.fontsizecn <= 8) throw new Exception("�����С����������8pt����");
                ptFontEn = new Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular);
                ptFontCn = new Font("Times New Roman", 10.5F, System.Drawing.FontStyle.Regular);
            }
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            PointF o = GlobalVar.o_receipt;
            //1.�ÿ�����25,25
            e.Graphics.DrawString(str, ptFontCn, ptBrush, 25F + o.X, 38F + o.Y);
        }
    }
}