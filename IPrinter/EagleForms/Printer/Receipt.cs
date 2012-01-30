using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.Printer
{
    public partial class Receipt : Form
    {
        EagleProtocal.MyTcpIpClient m_socket;
        private string m_pnr;
        private string m_eticketno;
        private EagleString.CommandPool m_cmdpool;
        private EagleString.LoginInfo m_li;
        PrintXmlHandle printHandle;


        readonly string cny = "CNY";
        /// <summary>
        /// 收到rt结果时创建，收到detr结果时置为null，在detr,f时，若不为null,给对应rtResult中ticketno的card赋值
        /// </summary>
        EagleString.RtResult rtResult;
        /// <summary>
        /// 引用SOCKET与指令池的构造
        /// </summary>
        public Receipt(EagleProtocal.MyTcpIpClient socket,EagleString.CommandPool cmdpool,EagleString.LoginInfo li)
        {
            InitializeComponent();
            printHandle = new PrintXmlHandle(PRINT_TYPE.RECEIPT, "", 0);
            m_socket = socket;
            m_cmdpool = cmdpool;
            m_li = li;
            btnOffline.Enabled = m_li.b2b.lr.AuthorityOfFunction("00A");
            btnPrintOffline_LX.Enabled = btnOffline.Enabled;
        }
        public void set_args(EagleProtocal.MyTcpIpClient socket,EagleString.CommandPool cmdpool)
        {
            m_socket = socket;
            m_cmdpool = cmdpool;
        }
        

        private void Receipt_Load(object sender, EventArgs e)
        {
            udLeftMargin.Value = (decimal)printHandle.m_pInfo.m_offset.X;
            udUpMargin.Value = (decimal)printHandle.m_pInfo.m_offset.Y;
            this.txtReceiptNo.Text = printHandle.m_pInfo.m_last_seq;
            printHandle.ToFormReceipt(this);
            Enable(!m_li.b2b.lr.AuthorityOfFunction("0FM"));
            txtOffice.Text = m_li.b2b.lr.UsingOffice();
            btnPrintOnline.Visible = btnPrintOffline_LX.Visible = m_li.b2b.lr.AuthorityOfFunction("00A");//脱机打
            txtReceiptNo2.Text = txtReceiptNo.Text;
        }

        private void Enable(bool b)
        {
            foreach (Control ctrl in pnlFlight.Controls)
            {
                ctrl.Enabled = b;
            }
            txtFare.Enabled = b;
            txtFuel.Enabled = b;
            txtBuild.Enabled = b;
            txtOther.Enabled = b;
            txtTotal.Enabled = b;
        }

        private void Receipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtPnr_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                m_pnr = txtPnr.Text.Trim();
                if(!EagleString.BaseFunc.PnrValidate(m_pnr))
                {
                    MessageBox.Show("输入的PNR不正确！");
                    return;
                }
                else
                {
                    string cmd = "rt:n/" + m_pnr + "/eg";
                    cmd = m_cmdpool.HandleCommand(cmd);
                    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.AutoPn);
                }
            }
        }

        private void txtEticketNo_KeyUp(object sender, KeyEventArgs e)
        {
            rtResult = null;
            if (e.KeyValue == 13)
            {
                m_eticketno = txtEticketNo.Text.Trim();
                string temp = "";
                if (!EagleString.BaseFunc.TicketNumberValidate(m_eticketno, ref temp))
                {
                    MessageBox.Show("输入的电子客票号不正确！");
                    return;
                }
                else
                {
                    string cmd = m_cmdpool.HandleCommand("detr:tn/" + m_eticketno);
                    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                }
            }
        }
        private void flight_info_clear()
        {
            for (int i = 1; i <= 50; ++i)
            {
                string key = "textBox" + i.ToString();
                pnlFlight.Controls[key].Text = "";
            }
            txtOther.Text = "";
            txtContinueTkt.Text = "";
        }
        /// <summary>
        /// 将detr结果放到控件上
        /// </summary>
        /// <param name="dr"></param>
        public void SetControlsByDetrResult(EagleString.DetrResult dr)
        {
            try
            {
                if (!dr.SUCCEED) return;
                rtResult = null;
                lbPassengers.Items.Clear();
                flight_info_clear();
                txtName.Text = dr.PASSENGER;
                cbEI.Text = dr.EI;
                txtFare.Text = cny + dr.FARE;
                txtBuild.Text = "";
                txtFuel.Text = cny + dr.TAX;
                txtTotal.Text = cny + dr.TOTAL;
                txtContinueTkt.Text = dr.CONJ_TKT;
                txtPnr.Text = dr.LS_SEG_DETR[0].PNR;
                for (int i = 0; i < 4; ++i)
                {
                    TextBox[] tb = new TextBox[14];
                    int start = i * 12 + 1;
                    for (int j = start; j < start + 14; ++j)
                    {
                        
                        string key = "textBox" + j.ToString();
                        tb[j - start] = (TextBox)pnlFlight.Controls[key];
                    }
                    try
                    {
                        dr.LS_SEG_DETR[i].ToTextBoxArrayLikeReceipt(tb);
                    }
                    catch (Exception ex)
                    {
                        EagleString.EagleFileIO.LogWrite(ex.Message);
                        break;
                    }
                    
                }
                if (this.Visible == true)//打印窗口可见时，自动发送取证件号指令
                {
                    string cmd = m_cmdpool.HandleCommand("detr:tn/" + dr.TKTN + ",f");
                    m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                }
                textBox8.Text = textBox20.Text = textBox32.Text = textBox44.Text = "";
            }
            catch(Exception ex2)
            {
                EagleString.EagleFileIO.LogWrite("Receipt.SetControlsByDetrResult : " + ex2.Message);
            }
        }
        /// <summary>
        /// 将detr,f的结果放到控件上
        /// </summary>
        /// <param name="dr"></param>
        public void SetCardByDetrfResult(EagleString.DetrFResult dr)
        {
            try
            {
                txtCard.Text = dr.CARDNO;
                if (rtResult == null)//detr之后的detr,f
                {
                    if (dr.RECEIPTNO != "") txtReceiptNo.Text = dr.RECEIPTNO;
                }
                else//rt之后的detr,f
                {
                    rtResult.CardIdSet(dr);
                    passenger_to_listbox(rtResult);
                    if (b_getid_s)
                    {
                        get_multi_cards();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("SetCardByDetrfResult : " + ex.Message);
            }
        }
        public void SetControlsByRtResult(EagleString.RtResult dr)
        {

            try
            {
                rtResult = dr;
                passenger_to_listbox(rtResult);
                lbPassengers.SelectedIndex = 0;
                flight_info_clear();
                txtName.Text = rtResult.NAMES[0];
                
                if (rtResult.CARDID != null) txtCard.Text = rtResult.CARDID[0];
                else txtCard.Text = "";
                txtEticketNo.Text = rtResult.TKTNO[0];
                if (string.IsNullOrEmpty(rtResult.EI))
                    cbEI.Text = EagleString.BaseFunc.EIstring(
                        rtResult.FLIGHTS[0], rtResult.BUNKS[0],EagleString.egString.LargeThan420(dr.SEGMENG[0].Date));
                else
                    cbEI.Text = rtResult.EI;
                for (int i = 0; i < rtResult.SEGMENG.Length; ++i)
                {
                    TextBox[] tb = new TextBox[14];
                    int start = i * 12 + 1;
                    for (int j = start; j < start + 14; ++j)
                    {
                        string key = "textBox" + j.ToString();
                        tb[j - start] = (TextBox)pnlFlight.Controls[key];
                    }
                    try
                    {
                        rtResult.SEGMENG[i].ToTextBoxArrayLikeReceipt(tb);
                    }
                    catch (Exception ex)
                    {
                        EagleString.EagleFileIO.LogWrite(ex.Message);
                        break;
                    }
                }
                if (m_li.b2b.lr.AuthorityOfFunction("0FN"))
                {
                    txtFare.Text = cny + rtResult.PRICE_CAL.FARE.ToString("f2");
                    txtFuel.Text = cny + rtResult.PRICE_CAL.FUEL.ToString("f2");
                    txtBuild.Text = cny + rtResult.PRICE_CAL.BUILD.ToString("f2");
                    txtTotal.Text = cny + rtResult.PRICE_CAL.TOTAL.ToString("f2");
                }
                textBox8.Text = textBox20.Text = textBox32.Text = textBox44.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void passenger_to_listbox(EagleString.RtResult rr)
        {
            lbPassengers.Items.Clear();
            for (int i = 0; i < rr.PSGCOUNT; ++i)
            {
                lbPassengers.Items.Add(rr.Name_CARDS[i]);
            }
            
        }
        /// <summary>
        /// 针对lbPassenger中的取证件
        /// </summary>
        private void btnCardIdGet_Click(object sender, EventArgs e)
        {
            b_getid_s = false;
            get_single_card();
        }
        private void get_single_card()
        {
            m_eticketno = txtEticketNo.Text;
            if (lbPassengers.SelectedIndices.Count > 0)
            {
                string cmd = m_cmdpool.HandleCommand("detr:tn/" + m_eticketno + ",f");
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
            }
        }
        /// <summary>
        /// txtReceiptNo.Text,txtEticketNo.Text,txtName,txtCard:打印按钮，实际先发送3in1打印指令(过行程单)，收到结果后才能打印
        /// </summary>
        private void btnPrintOnline_Click(object sender, EventArgs e)
        {
            long lon;
            string receiptno = txtReceiptNo.Text.Trim();
            string tktno = "";
            EagleString.BaseFunc.TicketNumberValidate(txtEticketNo.Text, ref tktno);
            if (long.TryParse(receiptno, out lon) && receiptno.Length==10)
            {
                DialogResult dd = MessageBox.Show("确定要打印行程单吗？", "易格科技", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dd == DialogResult.Cancel) return;
                txtValidate.Text = receiptno.Substring(6);
                Application.DoEvents();
                EagleString.EagleFileIO.LogWrite(string.Format("尝试打印行程单:({0})({1})", receiptno, tktno));
                m_socket.SendReceiptPrint(tktno, receiptno);

                m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.RECEIPT_PRINT);
            }
            else
            {
                MessageBox.Show("行程单号输入不正确！");
            }
        }
        public void Print()
        {
            printHandle.Print(this);
        }

        private void btnCancelReceipt_Click(object sender, EventArgs e)
        {
            long lon;
            string receiptno = txtReceiptNo.Text.Trim();
            string tktno = "";
            EagleString.BaseFunc.TicketNumberValidate(txtEticketNo.Text, ref tktno);
            if (long.TryParse(receiptno, out lon) && receiptno.Length == 10)
            {
                DialogResult dd = MessageBox.Show("确定要作废报销凭证吗？\r\n印刷序号：" + receiptno + "\r\n电子客票号：" + tktno, "易格科技", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dd == DialogResult.Cancel)
                    return;
                EagleString.EagleFileIO.LogWrite(string.Format("尝试作废行程单:({0})({1})", receiptno, tktno));
                m_socket.SendReceiptCancel(tktno, receiptno);
                m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.RECEIPT_CANCEL);
            }
            else
            {
                MessageBox.Show("行程单号输入不正确！");
            }
        }

        private void lbPassengers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string temp = lbPassengers.SelectedItem.ToString();
                txtName.Text = temp.Split('-')[0];
                txtCard.Text = temp.Split('-')[1];
                txtEticketNo.Text = rtResult.TKTNO4Name(txtName.Text);
            }
            catch
            {
            }
        }
        bool b_getid_s = false;
        private void btnCardIdGets_Click(object sender, EventArgs e)
        {
            b_getid_s = true;
            get_multi_cards();

        }
        private void get_multi_cards()
        {
            for (int i = 0; i < lbPassengers.Items.Count; i++)
            {
                lbPassengers.SelectedIndex = i;//触发事件
                string temp = lbPassengers.Items[i].ToString(); ;
                if (temp.Split('-')[1] == "")
                {
                    get_single_card();
                    break;
                }
            }
        }

        private void btnOffline_Click(object sender, EventArgs e)
        {
            this.Print();
        }

        private void btnPrintOnlineLX_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbPassengers.Items.Count; i++)
            {
                
                lbPassengers.SelectedIndex = i;
                txtReceiptNo.Text = string.Format("{0}", long.Parse(txtReceiptNo.Text) + 1);//或者由webservice获取当前帐号分配的行程单号
                Application.DoEvents();
                
                btnPrintOnline_Click(sender, e);
            }
        }

        private void txtReceiptNo_TextChanged(object sender, EventArgs e)
        {
            txtReceiptNo2.Text = txtReceiptNo.Text;
        }

        private void btnPrintOffline_LX_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbPassengers.Items.Count; i++)
            {

                lbPassengers.SelectedIndex = i;
                txtReceiptNo.Text = string.Format("{0}", long.Parse(txtReceiptNo.Text) + 1);//或者由webservice获取当前帐号分配的行程单号
                Application.DoEvents();

                btnOffline_Click(sender, e);
            }
        }

        private void btnBaoxian_Click(object sender, EventArgs e)
        {
            bool has = (btnBaoxian.Text == "有");
            has = !has;
            btnBaoxian.Text = (has ? "有" : "无");
            string s = (has ? "CNY20.00" : "XXX");
            printHandle.ModifyReceiptFixDataXXX(s);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string tempname = this.txtName.Text;
            string tempcard = this.txtCard.Text;
            this.txtName.Text = "测试";
            this.txtCard.Text = "1234567890ABCDEFGH";
            this.Print();
            this.txtName.Text = tempname;
            this.txtCard.Text = tempcard;
        }

        private void btnCancelReceiptLX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂不支持!如有需要此功能,请联系易格!");
        }

        private void btnPrintOfflineA4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂不支持!如有需要此功能,请联系易格!");
        }
    }
}