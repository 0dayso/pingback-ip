using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EagleWebService;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;

namespace EagleForms.Printer
{
    /// <summary>
    /// ʹ�ü򵥵���ģʽ
    /// </summary>
    public partial class Insurance : Form
    {
        /// <summary>
        /// Ʊ��/PNR/�����ַ���
        /// </summary>
        string input;
        static public Insurance Instance = null;
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        /// <summary>
        /// �Ƿ����ڲ�ѯ��
        /// </summary>
        public bool IsQuerying = false;

        public Insurance(PRINT_TYPE pType, string iaCode, EagleProtocal.MyTcpIpClient socket, EagleString.CommandPool cmdpool)
        {
            //lock (Instance)
            {
                CheckForIllegalCrossThreadCalls = false;
                if (Instance != null)
                {
                    Instance.Dispose();
                    Instance = null;
                    return;
                }
                InitializeComponent();

                m_socket = socket;
                m_cmdpool = cmdpool;
                Insurance.Instance = this;

                GetProductList(iaCode);

                this.cmbCardType.DataSource = Enum.GetNames(typeof(EagleWebService.IdentityType));
                cmbCardType.SelectedIndex = 0;
                this.tsbUsername.Text = Options.GlobalVar.IAUsername;
            }
        }

        EagleProtocal.MyTcpIpClient m_socket;
        EagleString.CommandPool m_cmdpool;
        PrintXmlHandle printHandle;
        int selectIndex = 0;

        private void Insurance_Load(object sender, EventArgs e)
        {

        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (DialogResult.OK == fd.ShowDialog())
            {
                printHandle.m_pInfo.m_font = fd.Font;
                btnFont.Text = fd.Font.Name + "," + fd.Font.Size.ToString("f1");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Instance = null;
            this.Close();
            this.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbInsuranceType.SelectedIndex == -1)
            {
                MessageBox.Show("û�п�ѡ�ı������ͣ��볢�����µ�¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mobile = EagleString.egString.Full2Half(txtCustomerPhone.Text.Trim());
            txtCustomerPhone.Text = mobile;

            if (!string.IsNullOrEmpty(mobile))
            {
                if (!Regex.IsMatch(mobile, "^1[358][0-9]{9}$"))
                {
                    MessageBox.Show("����ȷ�����ֻ���,���ṩ����֪ͨ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string cardNo = EagleString.egString.Full2Half(txtCardNo.Text.Trim());
            txtCardNo.Text = cardNo;

            if (cardNo.Length < 4)//����֤������4λ����
            {
                MessageBox.Show("����ȷ����֤�����룡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (cmbCardType.Text == "���֤")
                {
                    try
                    {
                        BirthAndGender bg = Common.GetBirthAndSex(cardNo);
                    }
                    catch
                    {
                        MessageBox.Show("���֤�����������飡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            decimal years = DateTime.Today.Subtract(dtpBirth.Value.Date).Days/365m;
            if(years >90 || years < 0)
            {
                MessageBox.Show("���������������飡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime dtFlight = dtpFlightDate.Value;

            if (dtFlight < DateTime.Today)
            {
                MessageBox.Show("����ȷ��д�˻����ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string flightNo = EagleString.egString.Full2Half(txtFlightNo.Text.Trim());
            txtFlightNo.Text = flightNo;
            dtpFlightDate_ValueChanged(null, null);//��Чʱ���ٴ�ͬ��

            if (MessageBox.Show("�������Ӵ�ӡ�����Ƿ�ȷ�ϣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                pbPrint.Visible = true;
                btnPrint.Enabled = false;
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(GetIt));
                th.Start();
            }
        }

        void GetIt()
        {
            CheckForIllegalCrossThreadCalls = false;
            try
            {
                if(GetInsuranceSeq())
                    printHandle.Print(this);
            }
            catch (System.Drawing.Printing.InvalidPrinterException e)
            {
                this.BeginInvoke(new MethodInvoker(delegate()
                    {
                        MessageBox.Show("�޷����Ӵ�ӡ����" + System.Environment.NewLine
                                            + System.Environment.NewLine
                                            + "���ü��±���Word�Ȱ칫������Դ�ӡ���Ƿ�����������", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
                EagleString.EagleFileIO.LogWrite(e.ToString());
            }
            //catch (System.Net.WebException e1)
            //{
                
            //}
            catch (Exception e2)
            {
                this.BeginInvoke(new MethodInvoker(delegate()
                {
                    MessageBox.Show("�����쳣�����ѯ������¼��" + System.Environment.NewLine
                                            + System.Environment.NewLine
                                            + "���ѳ����ɽ��в�����δ�������Ժ����³��ԡ�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
                EagleString.EagleFileIO.LogWrite(e2.ToString());
            }

            this.Invoke(new MethodInvoker(delegate()
                    {
                        pbPrint.Visible = false;
                        btnPrint.Enabled = true;
                    }));
        }

        bool GetInsuranceSeq()
        {
            EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
            EagleWebService.PurchaseResponseEntity ret;
            EagleWebService.PurchaseRequestEntity req = new EagleWebService.PurchaseRequestEntity();

            this.Invoke(new MethodInvoker(delegate()
                    {
                        string productString = ((EagleWebService.t_Product)(cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex])).FilterComment;
                        string[] productIdAndDuration = productString.Split('|');
                        string productID = productIdAndDuration[0];

                        req.customerID = txtCardNo.Text.Trim();
                        req.customerName = cbName.Text.Trim();
                        req.customerPhone = txtCustomerPhone.Text.Trim();
                        req.flightDate = dtpFlightDate.Value;
                        req.flightNo = txtFlightNo.Text.Trim();
                        req.InsuranceCode = productID;
                        req.password = printHandle.m_pInfo.m_password;
                        req.username = printHandle.m_pInfo.m_username;
                        req.PNR = autoSizeTextBox1.Text.Trim();
                        //req.Reserved = txtPrintingNo.Text.Trim();
                        req.customerIDType = (EagleWebService.IdentityType)Enum.Parse(typeof(EagleWebService.IdentityType), cmbCardType.Text);
                        req.customerGender = rbMale.Checked ? EagleWebService.Gender.Male : EagleWebService.Gender.Female;
                        req.customerBirth = dtpBirth.Value.Date;

                        txtCode.Text = txtENumber.Text = string.Empty;//��յ�֤��
                    }));

            ret = ws.Purchase(req);
            //2011.11.13 ����bug��������ڶԽӵ������ӿ�ʱ���صĳ�ʱ���Խ�ʧ�ܺ���Ȼ���������������أ�
            //���ͬʱ����Ҳ���ڳ�ʱ��ȴ�����ʱ�����´˴��׳��쳣��ʵ���Ϸ������ѳ�����ֻ�ǶԽӵ�����ʧ�ܣ�

            if (!string.IsNullOrEmpty(ret.Trace.ErrorMsg))
            {
                MessageBox.Show(ret.Trace.ErrorMsg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    //if (!string.IsNullOrEmpty(ret.Trace.Detail))
                    //    MessageBox.Show(ret.Trace.Detail, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (!string.IsNullOrEmpty(ret.PolicyNo))//��ʽ������
                        txtCode.Text = ret.PolicyNo;
                    if (!string.IsNullOrEmpty(ret.CaseNo))
                        txtENumber.Text = ret.CaseNo;
                    if (!string.IsNullOrEmpty(ret.AgentName))
                        txtSign.Text = ret.AgentName;
                    if (!string.IsNullOrEmpty(ret.Insurer))//�б���˾
                        txtInsurer.Text = ret.Insurer;
                    if (!string.IsNullOrEmpty(ret.AmountInsured))//����
                        txtAmountInsured.Text = ret.AmountInsured;
                    if (!string.IsNullOrEmpty(ret.ValidationPhoneNumber))//��ѯ�绰
                        txtCustomerService.Text = ret.ValidationPhoneNumber;
                    if (!string.IsNullOrEmpty(ret.ValidationWebsite))//��ѯ��ַ
                        txtWebsite.Text = ret.ValidationWebsite;
                }));

                return true;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public void LoadingStart()
        {
            IsQuerying = true;
            timerCommand.Enabled = true;

            stopwatch.Reset();
            stopwatch.Start();
        }

        public void LoadingWebServiceStart()
        {
            IsQuerying = true;
            stopwatch.Reset();
            stopwatch.Start();
        }

        /// <summary>
        /// ֹͣ������
        /// </summary>
        public void LoadingEnd()
        {
            IsQuerying = false;
            CheckForIllegalCrossThreadCalls = false;
            this.picLoadName.Visible = false;
            this.picLoadID.Visible = false;
            timerCommand.Enabled = false;

            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                FeedbackEntity feedback;
                feedback.elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                feedback.etermString = this.autoSizeTextBox1.Text.Trim();
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Feedback.PnrTimer));
                th.Start(feedback);
            }
        }

        public void SetControlsByDetrResult(EagleString.DetrResult dr)
        {
            CheckForIllegalCrossThreadCalls = false;
            LoadingEnd();
            this.btnGetCardNo.Enabled = true;

            try
            {
                if (dr.SUCCEED)
                {
                    lsName.Clear();
                    lsCard.Clear();
                    lsTktNo.Clear();
                    lsName.Add(dr.PASSENGER);
                    lsTktNo.Add(dr.TKTN);


                    cbName.Text = dr.PASSENGER;
                    List<string> lsflightno = new List<string>();
                    for (int i = 0; i < dr.LS_SEG_DETR.Count; ++i)
                    {
                        lsflightno.Add(dr.LS_SEG_DETR[i].AIRLINE + dr.LS_SEG_DETR[i].NUMBER);
                    }
                    txtFlightNo.Text = string.Join(",", lsflightno.ToArray());

                    if (dr.LS_SEG_DETR.Count > 0)
                    {
                        string timeBoarding = dr.LS_SEG_DETR[0].TIME.ToString().PadLeft(4, '0');
                        timeBoarding = timeBoarding.Substring(0, 2) + ":" + timeBoarding.Substring(2, 2);
                        dtpFlightDate.Text = dr.LS_SEG_DETR[0].DATE.ToString("yyyy-M-d") + " " + timeBoarding;
                        txtDest.Text = EagleString.EagleFileIO.CityCnName(dr.TO);
                        if (string.IsNullOrEmpty(txtDest.Text))
                            txtDest.Text = "����";
                    }

                    //if (this.Visible)
                    //{
                    //    //string cmd = m_cmdpool.HandleCommand("detr:tn/" + dr.TKTN + ",f");
                    //    //m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                    //    this.progressBarCustomerID.Visible = true;//������������ǲ���ʾ��Ը��δ����
                    //    //��ָ�� by king 2009.12.07
                    //    string cmd = "detr:tn/" + dr.TKTN + ",f";
                    //    m_cmdpool.SetCommandType(cmd);
                    //    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                    //}
                }
            }
            catch
            {
            }
        }
        public void SetCardByDetrfResult(EagleString.DetrFResult dr)
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                LoadingEnd();
                btnGetCardNo.Enabled = false;
                txtCardNo.Text = dr.CARDNO;
                if (lsName.Count == 1)
                {
                    lsCard.Clear();
                    lsCard.Add(dr.CARDNO);
                }
                else
                {
                    int index = lsName.IndexOf(dr.NAME);
                    if (index > -1)
                        lsCard[index] = dr.CARDNO;
                }
            }
            catch
            {
            }
        }
        List<string> lsName = new List<string>();
        List<string> lsCard = new List<string>();
        List<string> lsTktNo = new List<string>();
        /// <summary>
        /// ������Ϣ����ʽ����HU8965|2011-11-25 13:30��
        /// </summary>
        List<string> lsFlight = new List<string>();

        public void SetControlsByRtResult(EagleString.RtResult rtres)
        {
            CheckForIllegalCrossThreadCalls = false;
            LoadingEnd();
            lsName.Clear();
            lsCard.Clear();
            lsTktNo.Clear();

            if (rtres.FLAG_OF_PNR == EagleString.PNR_FLAG.CANCELLED)
            {
                throw new Exception("PNR��ȡ��");
            }

            if (rtres.NAMES != null)
            {
                foreach (string s in rtres.NAMES) lsName.Add(s);

                if (rtres.CARDID == null)
                {
                    foreach (string s in rtres.NAMES) lsCard.Add("");
                }
                else
                {
                    foreach (string s in rtres.CARDID) lsCard.Add(s);
                }

                if (rtres.TKTNO != null)
                    foreach (string s in rtres.TKTNO)
                    {
                        //if(!string.IsNullOrEmpty(s))
                            lsTktNo.Add(s);
                    }

                cbName.Items.Clear();
                cbName.Items.AddRange(lsName.ToArray());

                cbName.Text = lsName[0];
                txtCardNo.Text = lsCard[0];
                txtFlightNo.Text = string.Join(",", rtres.FLIGHTS);
                txtDest.Text = EagleString.EagleFileIO.CityCnName(rtres.CITYPAIRS[0].Substring(3, 3));
                if (string.IsNullOrEmpty(txtDest.Text))
                    txtDest.Text = "����";

                if (lsTktNo.Count > 0)//���ݵ��ӿ�Ʊ��ȡ���֤��
                    btnGetCardNo.Enabled = true;
                else
                    btnGetCardNo.Enabled = false;

                //��������ڼ���ʱ��
                if (rtres.SEGMENG.Length > 0)
                {
                    string timeBoarding = rtres.SEGMENG[0].Time.ToString().PadLeft(4, '0');
                    timeBoarding = timeBoarding.Substring(0, 2) + ":" + timeBoarding.Substring(2, 2);
                    dtpFlightDate.Text = rtres.FLIGHTDATES[0].ToString("yyyy-M-d") + " " + timeBoarding;
                }
            }
        }
        private void tbPnr_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && !IsQuerying)
            {
                btnGetPnr_Click(sender, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnGetCardNo_Click(object sender, EventArgs e)
        {
            if (lsTktNo.Count > selectIndex && !string.IsNullOrEmpty(lsTktNo[selectIndex]))
            {
                this.picLoadID.Visible = true;
                if (Options.GlobalVar.QueryType == XMLConfig.QueryType.Eterm)
                {
                    LoadingWebServiceStart();
                    new Thread(QueryZizaibaoForNI).Start(lsTktNo[selectIndex]);
                    return;

                    LoadingStart();
                    //string cmd = m_cmdpool.HandleCommand("detr:tn/" + lsTktNo[selectIndex] + ",f");
                    //m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                    //��ָ�� by king 2009.12.07
                    string cmd = "detr:tn/" + lsTktNo[selectIndex] + ",f";
                    m_cmdpool.SetCommandType(cmd);
                    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                }
                else if (Options.GlobalVar.QueryType == XMLConfig.QueryType.WebService)
                {
                    LoadingWebServiceStart();
                    new Thread(QueryWebserviceForNI).Start(lsTktNo[selectIndex]);
                }
                else
                {
                    LoadingWebServiceStart();
                    new Thread(QueryZizaibaoForNI).Start(lsTktNo[selectIndex]);
                }
            }
        }

        private void cbName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string name = cbName.Text;
                int index = lsName.IndexOf(name);
                txtCardNo.Text = lsCard[index];
                selectIndex = index;

                if (lsTktNo.Count > index)
                    btnGetCardNo.Enabled = true;
                else
                    btnGetCardNo.Enabled = false;
            }
            catch
            {
            }
        }

        private void btnTestPrint_Click(object sender, EventArgs e)
        {
            cbName.Text = "����";
            txtCardNo.Text = "1234567890ABCDEFGH";
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            printHandle.Print(this);
        }

        public void PrintIt(EagleWebService.t_Case policy)
        {
            cbName.Text = policy.customerName;
            txtCardNo.Text = policy.customerID;
            txtFlightNo.Text = policy.customerFlightNo;
            dtpFlightDate.Value = policy.customerFlightDate;
            txtENumber.Text = policy.caseNo;
            txtCode.Text = policy.CertNo;
            txtDest.Text = "����";
            txtCustomerPhone.Text = policy.customerPhone;
            txtDate.Text = policy.datetime.ToString("yyyy-MM-dd HH:mm:ss");
            txtSign.Text = policy.caseOwnerDisplay;

            try { Common.GetBirthAndSex(policy.customerID); }//���������֤����Ϊ�����������޷�ͨ����ӡ��֤
            catch
            {
                cmbCardType.Text = IdentityType.����֤��.ToString();
            }

            printHandle.Print(this);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            cmbDuration_SelectedIndexChanged(sender, e);
        }

        private void Insurance_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
            this.Dispose();
            Application.ExitThread();
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ȷ�����ϱ��� " + txtENumber.Text + " ��", "ע��", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            else
            {
                btnCancel.Enabled = false;
                pbCancle.Visible = true;
                System.Threading.Thread th = new System.Threading.Thread(Cancle);
                th.Start();
            }
        }

        void Cancle()
        {
            EagleWebService.TraceEntity ret = cancelIA();

            this.Invoke(new MethodInvoker(delegate()
            {
                if (string.IsNullOrEmpty(ret.ErrorMsg))
                    MessageBox.Show("���ϳɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(ret.ErrorMsg + System.Environment.NewLine + ret.Detail);

                btnCancel.Enabled = true;
                pbCancle.Visible = false;
            }));
        }

        private EagleWebService.TraceEntity cancelIA()
        {
            EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
            EagleWebService.TraceEntity ret = new EagleWebService.TraceEntity();
            ret = ws.DiscardIt(printHandle.m_pInfo.m_username, printHandle.m_pInfo.m_password, txtENumber.Text);
            return ret;
        }

        private void tbPnr_TextChanged(object sender, EventArgs e)
        {
            cbName.Items.Clear();
            cbName.Text = string.Empty;
            txtCardNo.Text = string.Empty;
            //txtFlightDate.Text = string.Empty;
            this.btnGetCardNo.Enabled = false;
        }

        private void cmbInsuranceDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            EagleWebService.t_Product product = (EagleWebService.t_Product)(cmbInsuranceType.Items[cmbInsuranceType.SelectedIndex]);

            if (string.IsNullOrEmpty(product.PrintingConfig))
            {
                MessageBox.Show("δ�ҵ��ò�Ʒ�Ĵ�ӡ��ʽ��Ϣ,����������Ա��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                printHandle = new PrintXmlHandle(PRINT_TYPE.INSURANCE, product.PrintingConfig, product.productID);
                printHandle.ToForm(this);

                string productString = product.FilterComment;
                string[] productIdAndDuration = productString.Split('|');
                int productDuration = int.Parse(productIdAndDuration[1]);
                cmbDuration.Items.Clear();
                for (int i = 1; i <= productDuration; i++)
                {
                    cmbDuration.Items.Add(i + "��");
                }
                cmbDuration.SelectedItem = productDuration + "��";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ȡ��Ʒ�б�
        /// </summary>
        /// <param name="iaCode">Ĭ��ѡ�еĲ�Ʒ���</param>
        void GetProductList(string iaCode)
        {
            try
            {
                EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
                EagleWebService.ProductListResponseEntity ret;
                ret = ws.GetProductList(Options.GlobalVar.IAUsername, Options.GlobalVar.IAPassword);

                if (string.IsNullOrEmpty(ret.Trace.ErrorMsg))
                {
                    if (ret.ProductList.Length < 1)
                        return;

                    this.cmbInsuranceType.DisplayMember = "productName";
                    this.cmbInsuranceType.ValueMember = "FilterComment";//���ø��ֶ�

                    for (int i = 0; i < ret.ProductList.Length; i++)
                    {
                        EagleWebService.t_Product product = ret.ProductList[i];
                        product.FilterComment = product.productID + "|" + product.productDuration;//���ø�string���ֶ�,�����޷�ͨ������.
                        this.cmbInsuranceType.Items.Add(product);

                        //if (product.productID.ToString() == iaCode)
                            //this.cmbInsuranceType.SelectedIndex = i;//��bug����Ϊ������cmbInsuranceType��Sorted���ԣ�ÿ��Addʱ���������У���SelectedIndexȴ���䣬����������ʾ��λ��
                            //cmbInsuranceType.SelectedItem = product;//��Ȼbug����Ϊ�ò���Ҳ���������SelectedIndex�����ͬ�ϣ�������ʾ��λ��
                            //�����������Ҫ�ڸ��������ѭ����ִ�г�ʼ���������Ƶ�ѭ���⣬�������foreachѭ��
                    }

                    foreach (var item in cmbInsuranceType.Items)
                    {
                        EagleWebService.t_Product product = (EagleWebService.t_Product)item;
                        if (product.productID.ToString() == iaCode)
                        {
                            cmbInsuranceType.SelectedItem = product;
                            break;
                        }
                    }

                    if (this.cmbInsuranceType.SelectedIndex < 0)
                        this.cmbInsuranceType.SelectedIndex = 0;
                }
                else
                    MessageBox.Show("��ȡ��Ʒ�б�ʧ�ܣ�" + ret.Trace.ErrorMsg + System.Environment.NewLine + ret.Trace.Detail);
            }
            catch (Exception e)
            {
                EagleString.EagleFileIO.LogWrite(e.ToString());
            }
        }

        private void dtpFlightDate_ValueChanged(object sender, EventArgs e)
        {
            dtpStartDate.Value = dtpFlightDate.Value;
        }

        private void tsbConsumed_Click(object sender, EventArgs e)
        {
            EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
            this.tsbConsumed.Text = ws.CountConsumed(Options.GlobalVar.IAUsername, "").ToString();
        }

        private void tsbBalance_Click(object sender, EventArgs e)
        {
            EagleWebService.wsInsurrance ws = new EagleWebService.wsInsurrance();
            this.tsbBalance.Text = ws.CountBalance(Options.GlobalVar.IAUsername, "").ToString();
        }

        private void txtCustomerPhone_Leave(object sender, EventArgs e)
        {
            txtCustomerPhone.Text = EagleString.egString.Full2Half(txtCustomerPhone.Text);
        }

        private void txtPrintingNo_Leave(object sender, EventArgs e)
        {
            txtPrintingNo.Text = EagleString.egString.Full2Half(txtPrintingNo.Text);
        }

        private void timerCommand_Tick(object sender, EventArgs e)
        {
            LoadingEnd();
        }

        private void btnGetPnr_Click(object sender, EventArgs e)
        {
            if (Options.GlobalVar.IsConnecting)
                MessageBox.Show("�������ӷ��������Ժ����ԡ���");
            else
            {
                input = autoSizeTextBox1.Text.Trim();
                if (input.Contains("����"))
                    return;

                this.picLoadName.Visible = true;
                if (Options.GlobalVar.QueryType == XMLConfig.QueryType.Eterm)
                    QueryZizaibao();//QueryEterm();
                else if (Options.GlobalVar.QueryType == XMLConfig.QueryType.WebService)
                    QueryWebservice();
                else
                    QueryZizaibao();
            }
        }

        /// <summary>
        /// �ɱ�����ȡ�˿���Ϣ
        /// </summary>
        private void QueryEterm()
        {
            try
            {
                string tktno = "";

                if (EagleString.BaseFunc.TicketNumberValidate(input, ref tktno))
                {
                    LoadingStart();
                    //this.cbName.Text = "��ѯ�У����Ժ�";
                    //string cmd = m_cmdpool.HandleCommand("detr:tn/" + tktno);
                    //m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                    //��ָ�� by king 2009.12.07
                    string cmd = "detr:tn/" + tktno;
                    m_cmdpool.SetCommandType(cmd);
                    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);//�ĳ�TypeOfCommand.Single�ᵼ��Ʊ�Ų�����ȡ
                }
                else if (EagleString.BaseFunc.PnrValidate(input))
                {
                    LoadingStart();
                    //this.cbName.Text = "��ѯ�У����Ժ�";
                    //string cmd = m_cmdpool.HandleCommand("rt:n/" + input + "/eg");
                    //m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.AutoPn);
                    //��ָ�� by king 2009.12.07
                    string cmd = "rt:n/" + input;
                    m_cmdpool.SetCommandType(cmd);
                    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.AutoPn);
                }
                else
                {
                    MessageBox.Show("�������");
                    LoadingEnd();
                }
            }
            catch (Exception ee)
            {
                LoadingEnd();
                MessageBox.Show("���޷���ȡ�����ֹ�����˿�������֤���������Ϣ��ɳ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        private void QueryWebservice()
        {
            string tktno = "";

            if (EagleString.BaseFunc.TicketNumberValidate(input, ref tktno))
            {
                this.picLoadName.Visible = true;
                LoadingWebServiceStart();
                new Thread(QueryWebserviceForTicket).Start(input);
            }
            else if (EagleString.BaseFunc.PnrValidate(input))
            {
                this.picLoadName.Visible = true;
                LoadingWebServiceStart();
                new Thread(QueryWebserviceForPNR).Start(input);
            }
            else
            {
                MessageBox.Show("�������");
            }
        }

        private void QueryZizaibao()
        {
            string tktno = "";

            if (EagleString.BaseFunc.TicketNumberValidate(input, ref tktno))
            {
                this.picLoadName.Visible = true;
                LoadingWebServiceStart();
                new Thread(QueryZizaibaoForTicket).Start(input);
            }
            else if (EagleString.BaseFunc.PnrValidate(input))
            {
                this.picLoadName.Visible = true;
                LoadingWebServiceStart();
                new Thread(QueryZizaibaoForTicket).Start(input);
            }
            else
            {
                MessageBox.Show("�������");
            }
        }

        private void QueryWebserviceForPNR(object pnr)
        {
            try
            {
                string url = "http://www.6jh.com/PIDweb/ScanPnr.asp?u={0}&p={1}&pnr={2}";
                url = string.Format(url, Options.GlobalVar.B2bLoginName, Options.GlobalVar.B2bLoginName, pnr);
                string ret = Common.GetResponse(url);

                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(ret);
                }
                catch
                {
                    EagleString.EagleFileIO.LogWrite(ret);
                    throw;
                }
                string flag = xml.DocumentElement.Attributes["ret_value"].Value;
                if (flag != "1")
                {
                    string error = xml.DocumentElement.Attributes["err_info"].Value;
                    this.BeginInvoke(new MethodInvoker(delegate
                        {
                            LoadingEnd(); 
                            MessageBox.Show(error);
                        }));
                    return;
                }
                else
                {
                    XmlNodeList xnlist = xml.SelectNodes("rt_parse/passengers/passenger");
                    lsName.Clear();
                    lsCard.Clear();
                    lsTktNo.Clear();
                    lsFlight.Clear();
                    foreach (XmlNode xn in xnlist)
                    {
                        lsName.Add(xn.SelectSingleNode("Name").InnerText);
                        lsCard.Add("");
                        lsTktNo.Add(xn.SelectSingleNode("Ticket").InnerText);
                    }

                    xnlist = xml.SelectNodes("rt_parse/lines/Line");
                    foreach (XmlNode xn in xnlist)
                    {
                        string flightDate = xn.SelectSingleNode("Date").InnerText;
                        string flightTime = xn.SelectSingleNode("StartTime").InnerText;
                        string flightNo = xn.SelectSingleNode("AirLine").InnerText;
                        DateTime dtFlight = EagleString.BaseFunc.str2datetime(flightDate, true);

                        lsFlight.Add(flightNo + "|" + dtFlight.ToString("yyyy-M-d") + " " + flightTime);
                    }

                    this.Invoke(new MethodInvoker(delegate
                        {
                            cbName.Items.Clear();
                            cbName.Items.AddRange(lsName.ToArray());
                            cbName.Text = lsName[0];
                            txtCardNo.Text = lsCard[0];

                            txtFlightNo.Items.Clear();
                            for (int i = 0; i < lsFlight.Count; i++) { txtFlightNo.Items.Add(lsFlight[i].Split('|')[0]); }
                            txtFlightNo.Text = lsFlight[0].Split('|')[0];

                            dtpFlightDate.Text = lsFlight[0].Split('|')[1];
                            LoadingEnd();
                        }));
                }
            }
            catch (Exception ee)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                    {
                        LoadingEnd();
                        MessageBox.Show("���޷���ȡ�����ֹ�����˿�������֤���������Ϣ��ɳ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }));
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        private void QueryWebserviceForTicket(object ticket)
        {
            try
            {
                string url = "http://www.6jh.com/PIDweb/ScanDetr.asp?u={0}&p={1}&str={2}";
                url = string.Format(url, Options.GlobalVar.B2bLoginName, Options.GlobalVar.B2bLoginName, ticket.ToString().Replace("-",string.Empty));
                string ret = Common.GetResponse(url);

                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(ret);
                }
                catch
                {
                    EagleString.EagleFileIO.LogWrite(ret);
                    throw;
                }
                string flag = xml.DocumentElement.Attributes["ret_value"].Value;
                if (flag != "1")
                {
                    string error = xml.DocumentElement.InnerText;
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        LoadingEnd(); 
                        MessageBox.Show(error);
                    }));
                    return;
                }
                else
                {
                    lsName.Clear();
                    lsCard.Clear();
                    lsTktNo.Clear();
                    lsFlight.Clear();

                    lsName.Add(xml.SelectSingleNode("cmd/ticket/NAME").InnerText);
                    lsCard.Add(xml.SelectSingleNode("cmd/ticket/NI").InnerText);

                    XmlNodeList xnlist = xml.SelectNodes("cmd/ticket/VOYAGES/VOYAGE");
                    foreach (XmlNode xn in xnlist)
                    {
                        string flightDate = xn.SelectSingleNode("DATE").InnerText;
                        string flightTime = xn.SelectSingleNode("TIME").InnerText;
                        string flightNo = xn.SelectSingleNode("FLIGHT").InnerText;
                        string CARRIER = xn.SelectSingleNode("CARRIER").InnerText;
                        DateTime dtFlight = EagleString.BaseFunc.str2datetime(flightDate, true);
                        flightTime = flightTime.Substring(0, 2) + ":" + flightTime.Substring(2, 2);

                        if (flightNo.Length < 5)//��������Ŵ��������
                            flightNo = CARRIER + flightNo;

                        lsFlight.Add(flightNo + "|" + dtFlight.ToString("yyyy-M-d") + " " + flightTime);
                    }

                    this.Invoke(new MethodInvoker(delegate
                    {
                        cbName.Items.Clear();
                        cbName.Items.AddRange(lsName.ToArray());
                        cbName.Text = lsName[0];
                        txtCardNo.Text = lsCard[0];

                        txtFlightNo.Items.Clear();
                        for (int i = 0; i < lsFlight.Count; i++) { txtFlightNo.Items.Add(lsFlight[i].Split('|')[0]); }
                        txtFlightNo.Text = lsFlight[0].Split('|')[0];

                        dtpFlightDate.Text = lsFlight[0].Split('|')[1];
                        LoadingEnd();
                    }));
                }
            }
            catch (Exception ee)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    LoadingEnd();
                    MessageBox.Show("���޷���ȡ�����ֹ�����˿�������֤���������Ϣ��ɳ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }));
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        private void QueryWebserviceForNI(object ticket)
        {
            try
            {
                string url = "http://www.6jh.com/PIDweb/ScanDetr.asp?u={0}&p={1}&str={2}";
                url = string.Format(url, Options.GlobalVar.B2bLoginName, Options.GlobalVar.B2bLoginName, ticket.ToString().Replace("-", string.Empty));
                string ret = Common.GetResponse(url);

                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(ret);
                }
                catch
                {
                    EagleString.EagleFileIO.LogWrite(ret);
                    throw;
                }
                string flag = xml.DocumentElement.Attributes["ret_value"].Value;
                if (flag != "1")
                {
                    string error = xml.DocumentElement.InnerText;
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        LoadingEnd(); 
                        MessageBox.Show(error);
                    }));
                    return;
                }
                else
                {
                    string name = xml.SelectSingleNode("cmd/ticket/NAME").InnerText;
                    string ni = xml.SelectSingleNode("cmd/ticket/NI").InnerText;
                    if (!string.IsNullOrEmpty(ni))
                    {
                        int index = lsName.IndexOf(name);
                        if (index > -1)
                            lsCard[index] = ni;
                    }

                    this.Invoke(new MethodInvoker(delegate
                    {
                        txtCardNo.Text = ni;

                        LoadingEnd();
                    }));
                }
            }
            catch (Exception ee)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    LoadingEnd();
                    MessageBox.Show("�޷���ȡ�����ֹ�����֤�����롣", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }));
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        private void QueryZizaibaoForNI(object ticket)
        {
            try
            {
                string ticketStr = ticket.ToString();
                string url = "http://www.zizaibao.com/API/AnalyzeTicket/?ticketCode={0}";
                url = string.Format(url, ticketStr.Replace("-", string.Empty));
                string ret = Common.GetResponse(url, System.Text.Encoding.UTF8);
                EagleString.EagleFileIO.LogWrite(ret);
                ret = ret.Replace("\\r\\n", "\r");
                Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(ret);
                ret = obj["Result"].ToString();

                if (!ticketStr.Contains("-"))//��ʽ��Ʊ��
                    ticketStr = ticketStr.Substring(0, 3) + "-" + ticketStr.Substring(3, ticketStr.Length - 3);
                //ret = ">DETR:TN/" + ticketStr + "\r" + ret + "\r>";
                ret = ">" + ret + "\r>";
                SetCardByDetrfResult(new EagleString.DetrFResult(ret));
            }
            catch (Exception ee)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    LoadingEnd();
                    MessageBox.Show("��ȡʧ�ܣ������³��ԣ����ֹ�����˿͵�֤�����롣", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }));
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        private void QueryZizaibaoForTicket(object ticket)
        {
            try
            {
                string ticketStr = ticket.ToString();
                string url = "http://www.zizaibao.com/API/AnalyzePNR/?pnrCode={0}";
                url = string.Format(url, ticketStr.Replace("-", string.Empty));
                string ret = Common.GetResponse(url, System.Text.Encoding.UTF8);
                EagleString.EagleFileIO.LogWrite(ret);
                ret = ret.Replace("\\r\\n", "\r");
                Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(ret);
                ret = obj["PNR"].ToString();

                if (!ticketStr.Contains("-"))//��ʽ��Ʊ��
                    ticketStr = ticketStr.Substring(0, 3) + "-" + ticketStr.Substring(3, ticketStr.Length - 3);
                //ret = ">DETR:TN/" + ticketStr + "\r" + ret + "\r>";
                ret = ">" + ret + "\r>";
                SetControlsByDetrResult(new EagleString.DetrResult(ret));
            }
            catch (Exception ee)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    LoadingEnd();
                    MessageBox.Show("��ȡʧ�ܣ������³��ԣ����ֹ�����˿�������֤���������Ϣ��ɳ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }));
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        private void cmbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCardType.Text == "���֤")
            {
                rbFemale.Enabled = false;
                rbMale.Enabled = false;
                dtpBirth.Enabled = false;
                txtCardNo_TextChanged(sender, e);
            }
            else
            {
                rbFemale.Enabled = true;
                rbMale.Enabled = true;
                //dtpBirth.Enabled = true;
            }
        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            if (cmbCardType.Text == "���֤")
            {
                try
                {
                    string idno = txtCardNo.Text.Trim();
                    idno = EagleString.egString.Full2Half(idno);

                    BirthAndGender bg = Common.GetBirthAndSex(idno);
                    if (bg.Gender == Gender.Female)
                        rbFemale.Checked = true;
                    else
                        rbMale.Checked = true;

                    //dtpBirth.Value = bg.Birth;
                }
                catch
                { }
            }
        }

        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDuration.SelectedIndex > -1)
            {
                int productDuration = Convert.ToInt32(cmbDuration.SelectedItem.ToString().TrimEnd('��'));
                dtpStopDate.Value = dtpStartDate.Value.AddDays(productDuration - 1);
            }
        }

        private void tbPnr_Enter(object sender, EventArgs e)
        {
            string txt = tbPnr.Text.Trim();
            if (txt.Contains("����"))
            {
                tbPnr.Text = string.Empty;
            }
        }

        private void tbPnr_Leave(object sender, EventArgs e)
        {
            string txt = tbPnr.Text.Trim();
            if (string.IsNullOrEmpty(txt))
            {
                tbPnr.Text = "����PNR��Ʊ��";
            }
        }

        private void txtFlightNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = txtFlightNo.SelectedIndex;
            dtpFlightDate.Text = lsFlight[index].Split('|')[1];
        }

        private void autoSizeTextBox1_Leave(object sender, EventArgs e)
        {
            autoSizeTextBox1.MiniSize();
        }

        private void autoSizeTextBox1_Enter(object sender, EventArgs e)
        {
            autoSizeTextBox1.AdjustSize();
        }

        private void autoSizeTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (!IsQuerying)
                {
                    btnGetPnr.Focus();
                    btnGetPnr_Click(sender, null);
                }
            }
        }
    }
}