using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using DataEntity.XMLSchema;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace EagleCTI
{
    public partial class FormPassInfoB2C : Form
    {
        EagleWebService.wsYzpbtoc ws = new EagleWebService.wsYzpbtoc();
        XmlDocument xd = new XmlDocument();
        UdpClient udp = new UdpClient(5162);
        IPEndPoint epPayServer = new IPEndPoint(IPAddress.Parse("192.168.1.3"), 5162);

        /// <summary>
        /// 当前呼入用户的 ID
        /// </summary>
        string CustomerID = string.Empty;

        string phoneNumber = "";

        public FormPassInfoB2C()
        {
            InitializeComponent();

            try
            {
                AsyncCallback callback = new AsyncCallback(receiveCallback);
                udp.BeginReceive(callback, udp);
            }
            catch
            {
                MessageBox.Show("启动支付监听失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void receiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)(ar.AsyncState);

            if (u == null || u.Client == null)// u == null 判断失败,u 似乎不会为空
                return;//这里线程结束(由 CtiUdpListening.Close() 触发),返回
            else
            {
                //Thread.CurrentThread.Join(3000);// 3 秒内不会接收新的消息
                //重新启动接收线程
                AsyncCallback callback = new AsyncCallback(receiveCallback);
                u.BeginReceive(callback, u);
            }

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            Byte[] receiveBytes = u.EndReceive(ar, ref ep);
            string receiveString = Encoding.Default.GetString(receiveBytes);
            string receiveData = string.Empty;

            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(receiveString);
                XmlNode xn = xd.ChildNodes[0];
                string msgType = xn.Name.ToLower();

                if (msgType == "reversal")//<Reversal OrderID='406' Flag='{1}' /> 
                {
                    receiveData = xn.Attributes["OrderID"].Value;
                    string flag = xn.Attributes["Flag"].Value;
                    if (flag == "1")
                        MessageBox.Show("撤销支付成功！");
                    else
                        MessageBox.Show(flag);
                }
                else if (msgType == "refund")//<Refund OrderID='406' Flag='{1}' /> 
                {
                    receiveData = xn.Attributes["OrderID"].Value;
                    string flag = xn.Attributes["Flag"].Value;
                    if (flag == "1")
                        MessageBox.Show("退款成功！");
                    else
                        MessageBox.Show(flag);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btSaveCost_Click(object sender, EventArgs e)
        {
            btnSaveCustomer.Enabled = false;
            string name = txtName.Text.Trim();
            string customerNo = txtCustomerNo.Text.Trim();
            string tel = cmbLandline.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                errorProvider1.SetError(txtName, "名字不能为空！");
                return;
            }
            else
                errorProvider1.SetError(txtName, "");

            if (string.IsNullOrEmpty(tel))
            {
                errorProvider1.SetError(cmbLandline, "联系电话不能为空！");
                return;
            }
            else
                errorProvider1.SetError(cmbLandline, "");

            DataEntity.XMLSchema.NewCustomers customer = new DataEntity.XMLSchema.NewCustomers();
            customer.CustomerName = name;
            customer.CustomerCode = customerNo;
            customer.CardID = cmbIdentity.Text.Trim();
            customer.DepartmentID = Options.GlobalVar.B2CDepartmentID.Split(':')[0];
            customer.OperaterID = Options.GlobalVar.B2CUserID.Split(':')[0];
            customer.Sex = this.rbMale.Checked ? "0" : "1";
            customer.AccumulatedNum = "0";
            customer.VIPcard = txtSepcialNo.Text.Trim();
            customer.IsInceptSMS = cbSMS.Checked ? "true" : "false";
            //if (tel.Length >= 11)
                customer.Mobile = tel;
            //else
            //    customer.Tel = tel;//B2C后台已取消该字段

            string xml = new TXIO.XML_IO().SaveToText(customer);

            try
            {
                int ret = ws.NewCustomer(xml);

                switch (ret)
                {
                    case -1:
                        //MessageBox.Show("改来电客户已经存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DataEntity.XMLSchema.UpdateCustomer cus = new UpdateCustomer();
                        cus.CustomerName = customer.CustomerName;
                        cus.CardID = customer.CardID;
                        cus.IsInceptSMS = Convert.ToInt32(cbSMS.Checked).ToString();
                        cus.Sex = customer.Sex;// customer.Sex;
                        cus.VIPcard = customer.VIPcard;

                        string xmlUpdate = new TXIO.XML_IO().SaveToText(cus);
                        string retUpdate = ws.UpdateCustomer(int.Parse(this.CustomerID), xmlUpdate);
                        switch (retUpdate)
                        {
                            case "1":
                                MessageBox.Show("更新客户资料成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case "-1":
                                MessageBox.Show("客户不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case "0":
                                MessageBox.Show("更新客户资料发生错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                        }
                        break;
                    case -2:
                        MessageBox.Show("数据库错误，保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("客户资料保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CustomerID = ret.ToString();
                        Options.GlobalVar.B2CCallingXml = ws.GetCustomerByPhone(phoneNumber);
                        break;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("保存客户资料发生错误，详细内容如下：" + System.Environment.NewLine + System.Environment.NewLine
                    + ee.Message + System.Environment.NewLine + System.Environment.NewLine
                    + "[" + ws.Url + "]",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btnSaveCustomer.Enabled = true;
        }

        private void FormPassInfoB2C_FormClosing(object sender, FormClosingEventArgs e)
        {
            //停止闪烁
            this.txtPhoneQuery.BackColor = Color.White;
            this.timerColorful.Stop();
            this.Visible = false;
            e.Cancel = true;
        }

        private void FormPassInfoB2C_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Today;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadCustomerInfo(this.txtPhoneQuery.Text.Trim());
        }

        /// <summary>
        /// 查询客户资料
        /// </summary>
        /// <param name="phoneOrMobile"></param>
        public void LoadCustomerInfo(string phoneOrMobile)
        {
            this.btnQuery.Enabled = false;
            this.btnMemoAdd.Enabled = false;
            this.txtName.Text = "查询中…";
            this.phoneNumber = phoneOrMobile;
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(LoadingThread));
            th.Start();

            this.lvTravel.Items.Clear();
            this.lvTravel.Items.Add("提取中…");
            th = new System.Threading.Thread(new System.Threading.ThreadStart(LoadingTravelHistory));
            th.Start();
        }

        /// <summary>
        /// 取旅游平台的消费记录
        /// </summary>
        private void LoadingTravelHistory()
        {
            try
            {
                string xmlRet = @"<?xml version=""1.0"" encoding=""utf-8""?>
<message method=""GetTravelHistory"" type=""response"">
	<history>
		<datetime>2009-9-9 13:01:01</datetime>
		<reserved>test1</reserved>
	</history>
	<history>
		<datetime>2009-9-9 13:01:01</datetime>
		<reserved>test2</reserved>
	</history>
</message>";

                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlRet);
                XmlNodeList xns = xd.SelectNodes("message/history");
                this.lvTravel.Items.Clear();

                foreach (XmlNode xn in xns)
                {
                    string date = xn.SelectSingleNode("datetime").InnerText;
                    DateTime dt = DateTime.Parse(date);
                    date = dt.ToShortDateString();

                    string reserved = xn.SelectSingleNode("reserved").InnerText;
                    ListViewItem item = new ListViewItem(new string[] { date, reserved });
                    Invoke(new dlgListViewAddItem(ListViewAddItem), lvTravel, item);
                }
            }
            catch(Exception e1)
            {
                //MessageBox.Show("提取旅游平台消费记录发生错误，详细内容如下：" + System.Environment.NewLine + System.Environment.NewLine + e1.Message,
                //    "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        delegate void dlgControlClear();
        void ControlClear()
        {
            this.cmbLandline.Items.Clear();
            this.cmbLandline.Text = "";
            this.txtCustomerNo.Text = string.Empty;
            //this.txtIdentityType.Text = string.Empty;
            this.cmbIdentity.Items.Clear();
            this.cmbIdentity.Text = "";
            this.dateTimePicker1.Value = DateTime.Today;
            this.lvMemo.Items.Clear();
            this.lvOrderHistory.Items.Clear();
            this.txtMemo.Text = string.Empty;
            this.txtSepcialNo.Text = string.Empty;
            this.cbSMS.Checked = false;
        }

        delegate void dlgTextBoxUpdater(TextBox textBox, string txt);
        void TextBoxUpdater(TextBox textBox, string txt)
        {
            textBox.Text = txt;
        }

        delegate void dlgDateTimePickerUpdater(DateTimePicker dateTimePicker, string txt);
        void DateTimePickerUpdater(DateTimePicker dateTimePicker, string txt)
        {
            dateTimePicker.Text = txt;
        }

        delegate void dlgRadioButtonUpdater(RadioButton radioButton, bool check);
        void RadioButtonUpdater(RadioButton radioButton, bool check)
        {
            radioButton.Checked = check;
        }

        delegate void dlgCheckBoxUpdater(CheckBox checkBox, bool check);
        void CheckBoxUpdater(CheckBox checkBox, bool check)
        {
            checkBox.Checked = check;
        }

        delegate void dlgComboBoxUpdater(ComboBox comboBox, string txt);
        void ComboBoxUpdater(ComboBox comboBox, string txt)
        {
            comboBox.Text = txt;
        }

        delegate void dlgComboBoxAdd(ComboBox comboBox, string txt, string[] array);
        void ComboBoxAdd(ComboBox comboBox, string txt, string[] array)
        {
            if (txt != null)
                comboBox.Items.Add(txt);
            else if (array != null)
                comboBox.Items.AddRange(array);

            this.cmbIdentity.SelectedIndex = this.cmbIdentity.Items.Count - 1;
        }

        delegate void dlgListViewAddText(ListView listView, string txt, ListViewGroup group);
        void ListViewAddText(ListView listView, string txt, ListViewGroup group)
        {
            listView.Groups.Add(group);
            listView.Items.Add(txt).Group = group;
        }

        delegate void dlgListViewAddItem(ListView listView, ListViewItem item);
        void ListViewAddItem(ListView listView, ListViewItem item)
        {
            listView.Items.Add(item);
        }

        delegate void dlgControlEnable(Control button, bool enable);
        void ControlEnable(Control ctl, bool enable)
        {
            ctl.Enabled = enable;
        }

        void LoadingThread()
        {
            //try
            {
                //清除界面内容
                Invoke(new dlgControlClear(ControlClear));
                this.CustomerID = "";//必须清空,否则还可以添加备注

                //this.txtPhoneQuery.Text = phoneNumber;
                Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtPhoneQuery, phoneNumber);
                string ret = ws.GetCustomerByPhone(phoneNumber);

                if (ret.ToLower() == "none")
                {
                    Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtName, "客户资料不存在！");
                    Invoke(new dlgComboBoxUpdater(ComboBoxUpdater), cmbLandline, phoneNumber);
                    Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtCustomerNo, phoneNumber);
                    Invoke(new dlgControlEnable(ControlEnable), txtSepcialNo, true);
                    string xml = "<NewCustomer><Tel>{0}</Tel></NewCustomer>";
                    ret = string.Format(xml, phoneNumber);
                    Options.GlobalVar.B2CCallingXml = ret;
                }
                else if (ret.ToLower() == "failed")
                    Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtName, "查询失败！");
                else
                {
                    Invoke(new dlgControlEnable(ControlEnable), btnSaveCustomer, false);
                    //客户基本资料
                    DataEntity.XMLSchema.Customers customer;
                    customer = DataEntity.XMLSchema.xml_BaseClass.LoadXml<DataEntity.XMLSchema.Customers>(ret);
                    Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtName, customer.CustomerName);
                    Invoke(new dlgComboBoxAdd(ComboBoxAdd), cmbLandline, null, customer.Mobiles);
                    Invoke(new dlgComboBoxUpdater(ComboBoxUpdater), cmbLandline, phoneNumber);
                    //this.txtIdentityType.Text = xd.SelectSingleNode("//Customer/CardIDType").InnerText;
                    Invoke(new dlgDateTimePickerUpdater(DateTimePickerUpdater), dateTimePicker1, customer.CreateTime);
                    Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtCustomerNo, customer.CustomerCode);
                    Invoke(new dlgTextBoxUpdater(TextBoxUpdater), txtSepcialNo, customer.VIPcard);

                    if(string.IsNullOrEmpty(customer.VIPcard))
                        Invoke(new dlgControlEnable(ControlEnable), txtSepcialNo, true);
                    else
                        Invoke(new dlgControlEnable(ControlEnable), txtSepcialNo, false);

                    this.CustomerID = customer.CustomerID;

                    if (!string.IsNullOrEmpty(customer.CardID))
                        Invoke(new dlgComboBoxAdd(ComboBoxAdd), cmbIdentity, customer.CardID, null);
                    if (!string.IsNullOrEmpty(customer.CardID2))
                        Invoke(new dlgComboBoxAdd(ComboBoxAdd), cmbIdentity, customer.CardID2, null);
                    if (!string.IsNullOrEmpty(customer.CardID3))
                        Invoke(new dlgComboBoxAdd(ComboBoxAdd), cmbIdentity, customer.CardID3, null);
                    //this.cmbIdentity.SelectedIndex = this.cmbIdentity.Items.Count - 1;

                    if (customer.Sex == "0")
                        Invoke(new dlgRadioButtonUpdater(RadioButtonUpdater), rbMale, true);
                    else
                        Invoke(new dlgRadioButtonUpdater(RadioButtonUpdater), rbFemale, true);

                    Invoke(new dlgCheckBoxUpdater(CheckBoxUpdater), cbSMS, customer.IsInceptSMS.ToLower() == "true" ? true : false);

                    //客户上n次订单记录
                    List<DataEntity.XMLSchema.AboutTicketOrder> orderArray = customer.TicketOrders;
                    if (orderArray != null)
                    {
                        foreach (DataEntity.XMLSchema.AboutTicketOrder order in orderArray)
                        {
                            string date = order.CreateTime.Split(' ')[0];
                            string PassengerNames = order.PassengerNames;
                            string AirlineCode = order.AirlineCode;
                            string TakeOffDay = order.TakeOffDay.Split(' ')[0];
                            string TakeOffCity = order.TakeOffCity;
                            string ArrivedCity = order.ArrivedCity;
                            string pnr = order.CAACOrderCode;
                            string status = order.str_OperateTypeID;

                            ListViewItem item = new ListViewItem(new string[] { date, status, TakeOffCity, ArrivedCity, TakeOffDay, AirlineCode, pnr, PassengerNames });
                            item.Tag = order.TicketOrderID;//订单号，用于撤销和退款
                            Invoke(new dlgListViewAddItem(ListViewAddItem), lvOrderHistory, item);
                        }
                    }

                    //客户上n次来电备注
                    int i = 0;
                    List<DataEntity.XMLSchema.CustomerQueryRecords> historyOrderArray = customer.CustomerQueryRecordss;
                    if (historyOrderArray != null)
                    {
                        foreach (DataEntity.XMLSchema.CustomerQueryRecords record in historyOrderArray)
                        {
                            string date = record.QueryTime;
                            string text = record.QueryContent;
                            i++;
                            ListViewGroup group = new ListViewGroup(i.ToString(), date);
                            Invoke(new dlgListViewAddText(ListViewAddText), lvMemo, text, group);
                        }
                    }

                    Options.GlobalVar.B2CCallingXml = ret;
                    this.timerColorful.Start();
                    Invoke(new dlgControlEnable(ControlEnable), btnMemoAdd, true);
                }

                Invoke(new dlgControlEnable(ControlEnable), btnQuery, true);
            }
            //catch (Exception e)
            //{
            //    //MessageBox.Show("来电信息发生错误，详细内容如下：" + System.Environment.NewLine + System.Environment.NewLine + e.Message,
            //    //    "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    this.txtName.Text = "来电信息发生错误，详细内容：" + e.Message + "[" + ws.Url + "]";
            //    this.btnQuery.Enabled = true;
            //}
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.btnSaveCustomer.Enabled = true;
        }

        private void btnMemoAdd_Click(object sender, EventArgs e)
        {
            string memo = txtMemo.Text.Trim();
            
            if(string.IsNullOrEmpty(memo))
                return;
            if (string.IsNullOrEmpty(this.CustomerID))
                MessageBox.Show("客户不存在，无法添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            this.btnMemoAdd.Enabled = false;
            DataEntity.XMLSchema.CustomerQueryRecords query = new CustomerQueryRecords();
            query.CustomerID = this.CustomerID;
            query.OperaterID = Options.GlobalVar.B2CUserID.Split(':')[0];
            query.Tel = this.txtPhoneQuery.Text.Trim();
            query.QueryContent = memo;
            query.QueryTime = DateTime.Now.ToString();
            string xml = new TXIO.XML_IO().SaveToText(query);

            if (ws.InputCustomerQuery(xml))
            {
                string date = DateTime.Now.ToString();
                ListViewGroup group = this.lvMemo.Groups.Add(date, date);
                ListViewItem item = new ListViewItem(memo);
                item.Group = group;
                item.Selected = true;
                this.lvMemo.Items.Add(item);
                item.EnsureVisible();

                this.txtMemo.Text = string.Empty;
            }
            else
                MessageBox.Show("添加备注信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            this.btnMemoAdd.Enabled = true;
        }

        private void timerColorful_Tick(object sender, EventArgs e)
        {
            this.txtPhoneQuery.BackColor = txtPhoneQuery.BackColor == Color.White ? Color.Orange : Color.White;
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = string.Empty;

            foreach (ListViewItem item in this.lvMemo.SelectedItems)
            {
                message += item.Text + System.Environment.NewLine;
            }

            if(!string.IsNullOrEmpty(message))
                Clipboard.SetText(message);
        }

        private void pbCustomerDetail_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;//设置IE隐藏或最大化,最小化   
            startInfo.Arguments = Options.GlobalVar.B2CURL + "/Customers/CustomerInfo.aspx?Type=1&CustomerID=" + this.CustomerID;
            startInfo.Arguments += "&" + Options.GlobalVar.B2CSessionStr;
            Process.Start(startInfo);
        }

        private void cMenuPayment_Opening(object sender, CancelEventArgs e)
        {
            if (this.lvOrderHistory.SelectedItems.Count == 0)
                this.cMenuPayment.Enabled = false;
        }

        private void 退款RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRefund fr = new FrmRefund();

            if (fr.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(this.lvOrderHistory.FocusedItem.Tag.ToString());
                string orderID = this.lvOrderHistory.FocusedItem.Tag.ToString();
                //string xml = "<Refund OrderID='{0}' Amount='{1}' /> ";
                //xml = string.Format(xml, orderID, 1200);

                //Byte[] xmlByte = Encoding.Default.GetBytes(xml);
                //udp.Send(xmlByte, xmlByte.Length, epPayServer);
                EagleWebService.CTIPayWebService pay = new EagleWebService.CTIPayWebService();
                string ret = pay.Refund(orderID, fr.RefundAmount);

                if (ret == "1")
                    MessageBox.Show("退款成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(ret, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 撤销RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string orderID = this.lvOrderHistory.FocusedItem.Tag.ToString();
            //string xml = "<Reversal OrderID='{0}' />";
            //xml = string.Format(xml, orderID);

            //Byte[] xmlByte = Encoding.Default.GetBytes(xml);
            //udp.Send(xmlByte, xmlByte.Length, epPayServer);
            EagleWebService.CTIPayWebService pay = new EagleWebService.CTIPayWebService();
            string ret = pay.Reversal(orderID);

            if (ret == "1")
                MessageBox.Show("撤销成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(ret, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void 支付明细报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;//设置IE隐藏或最大化,最小化   
            startInfo.Arguments = Options.GlobalVar.B2CURL.Replace("AirLineTicket/", "CTIPayService/CiticList.aspx");
            startInfo.Arguments = startInfo.Arguments.Replace("AirLineTicket", "CTIPayService/CiticList.aspx");
            Process.Start(startInfo);
        }
    }
}