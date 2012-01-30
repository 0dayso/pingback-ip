/*Sample String Of 443
TRFU:M 1 /D/0
Airline Code 876  TKT Number 3338264920 -8264920   Check
Conjunction No. 1  Coupon No.  11200  20000  30000  40000
Passenger Name LIZHIYONG
Currency Code CNY-   Form Of Payment  CASH
Gross Refund 1580.00                             ET-(Y/N): Y
Deduction                 Commission 3.00 % =           ---
TAX [ 1] CN      100  [ 2] YQ       80  [ 3] ___________
    [ 4] ___________  [ 5] ___________  [ 6] ___________
    [ 7] ___________  [ 8] ___________  [ 9] ___________
    [10] ___________  [11] ___________  [12] ___________
    [13] ___________  [14] ___________  [15] ___________
    [16] ___________  [17] ___________  [18] ___________
    [19] ___________  [20] ___________  [21] ___________
    [22] ___________  [23] ___________  [24] ___________
    [25] ___________  [26] ___________  [27] ___________
Remark                   Credit Card
Net Refund =           CNY

>请在退票对话框中进行退票
>

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.ToCommand
{
    public partial class RefundTicket : Form
    {
        EagleProtocal.MyTcpIpClient m_socket;
        EagleString.CommandPool m_cmdpool;
        EagleString.LoginInfo m_li;
        public RefundTicket(EagleProtocal.MyTcpIpClient sk,EagleString.CommandPool pool,EagleString.LoginInfo li)
        {
            InitializeComponent();
            m_socket = sk;
            m_cmdpool = pool;
            m_li = li;
            int id = EagleString.EagleFileIO.EtdzPrinterNumber(li.b2b.lr.IpidUsingIsSameOffice());
            if (id > 0)
            {
                txtTrfdOption.Text = string.Format("/AM/{0}/D", id);
            }
        }
        public void SetControlsByTrfuString(string rev)
        {

            char ch;
            ch = (char)0x0E;
            rev = rev.Replace(ch.ToString(), "");
            ch = (char)0x1B;
            rev = rev.Replace(ch.ToString(), "");
            ch = (char)0x09;
            rev = rev.Replace(ch.ToString(), "");
            ch = (char)0x0F;
            rev = rev.Replace(ch.ToString(), "*");
            //rev = rev.Replace(" *", "*");
            string st = "\r\n*";
            rev = rev.Replace(st, "*");

            try
            {
                InitControls(rev);
            }
            catch
            {
                MessageBox.Show("您可能正在使用443类型配置");
                InitControls(rev.Replace("*", ""));
            }
            try
            {
                //if (textBox2.Text.Trim() == "")
                //    textBox2.Text = ePlus.CreateETicket.GetRefundNumber(GlobalVar.officeNumberCurrent).ToString();
            }
            catch
            {
            }
        }
        public RefundTicket(string recvstring)
        {
            InitializeComponent();
            SetControlsByTrfuString(recvstring);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if (textBox2.Text.Trim() == "" || textBox2.Text.Trim() == "0") { MessageBox.Show("打印机序号错误！第一行第二格"); return; }
            //if (DialogResult.OK != MessageBox.Show("确定要退票吗", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) return;
            //string combstring = combinestring();
            //EagleAPI.EagleSendCmd(combstring);
            //this.Close();
        }
        void initControls443(string recvstring)
        {
            //TRFU:M 1 /D/0
            recvstring = recvstring.Replace("\n", "\r");
            string t = EagleString.egString.Between2String(recvstring, "TRFU:", "\r");
            string[] a = t.Split(new string[] { " ", "/" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < a.Length; ++i)
            {
                string key = "textBox" + Convert.ToString(i + 1);
                panel1.Controls[key].Text = a[i];
            }
            //Airline Code 876  TKT Number 3338264920 -8264920   Check
            string[] array = new string[] { "Airline Code", "TKT Number", "-", "Check", "\r" };
            for (int i = 0; i < array.Length - 1; ++i)
            {
                string key = "textBox" + Convert.ToString(i + 5);
                panel1.Controls[key].Text = EagleString.egString.Between2String(recvstring, array[i], array[i + 1]);
            }
            //Conjunction No. 1  Coupon No.  11200  20000  30000  40000
            array = new string[] { "Conjunction No.", "TCoupon No.", "\r" };
            textBox9.Text = EagleString.egString.Between2String(recvstring, array[0], array[1]);
            string temp = EagleString.egString.Between2String(recvstring, array[1], array[2]);
            array = temp.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < array.Length; ++i)
            {
                string key = "textBox" + Convert.ToString(i + 10);
                panel1.Controls[key].Text = array[i].Substring(1);
            }
            //Passenger Name LIZHIYONG
            textBox14.Text = EagleString.egString.Between2String(recvstring, "Passenger Name", "\r");
            //Currency Code CNY-   Form Of Payment  CASH
            array = new string[] { "Currency Code", "Form Of Payment", "\r" };
            for (int i = 0; i < array.Length - 1; ++i)
            {
                string key = "textBox" + Convert.ToString(i + 15);
                panel1.Controls[key].Text = EagleString.egString.Between2String(recvstring, array[i], array[i + 1]);
            }
            //Gross Refund 1580.00                             ET-(Y/N): Y
            array = new string[] { "Gross Refund", "ET-(Y/N):", "\r" };
            for (int i = 0; i < array.Length - 1; ++i)
            {
                string key = "textBox" + Convert.ToString(i + 17);
                panel1.Controls[key].Text = EagleString.egString.Between2String(recvstring, array[i], array[i + 1]);
            }
            //Deduction                 Commission 3.00 % =           ---
            array = new string[] { "Deduction", "Commission", "% =", "\r" };
            for (int i = 0; i < array.Length - 1; ++i)
            {
                string key = "textBox" + Convert.ToString(i + 19);
                panel1.Controls[key].Text = EagleString.egString.Between2String(recvstring, array[i], array[i + 1]);
            }
            array = new string[28];
            for (int i = 0; i < array.Length - 1; ++i) array[i] = "[" + Convert.ToString(i + 1).PadLeft(2, ' ') + "]";
            array[27] = "\r";
            for (int i = 0; i < array.Length - 1; ++i)
            {
                string key1 = "textBox" + Convert.ToString(22 + i * 2);
                string key2 = "textBox" + Convert.ToString(22 + i * 2 + 1);
                t = EagleString.egString.Between2String(recvstring, array[i], array[i + 1]);
                string[] b = t.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (t.IndexOf("_") < 0)
                {
                    groupBox1.Controls[key1].Text = b[0];
                    groupBox1.Controls[key2].Text = b[1];
                }
                else
                {
                    groupBox1.Controls[key1].Text = "";
                    groupBox1.Controls[key2].Text = "";
                }

            }
            //Remark                   Credit Card           Net Refund =           CNY
            {
                temp = EagleString.egString.Between2String(recvstring, "Remark", "Credit Card");
                string[] b = temp.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                textBox76.Text = (b.Length > 1 ? b[0] : "");
                textBox77.Text = (b.Length > 1 ? b[1] : "");
                array = new string[] { "Credit Card", "Net Refund =", "CNY"};
                for (int i = 0; i < array.Length - 1; ++i)
                {
                    string key = "textBox" + Convert.ToString(i + 78);
                    panel1.Controls[key].Text = EagleString.egString.Between2String(recvstring, array[i], array[i + 1]);
                }
            }
        }
        void InitControls(string recvstring)
        {

            if (recvstring.IndexOf("*") < 0)
            {
                initControls443(recvstring);
                return;
            }
            string temp = "";
            int ibeg = -1;
            int iend = -1;

            #region//第一行
            temp = "TRFU:*";
            ibeg = recvstring.IndexOf(temp);

            
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox1.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "/*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox2.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "/*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox3.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "\r";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox4.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            #region//第二行
            temp = "Airline Code *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "TKT Number *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox5.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "-*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox6.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "Check *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox7.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox8.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            #region//第三行
            temp = "Conjunction No. *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "Coupon No.  1*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox9.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "2*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox10.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "3*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox11.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "4*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox12.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox13.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            #region//第四行
            temp = "Passenger Name *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "\r";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox14.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            #region//第五行
            temp = "Currency Code *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "Form Of Payment  *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox15.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox16.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion

            #region//第六行
            temp = "Gross Refund *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "ET-(Y/N): *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox17.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox18.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            #region//第七行
            temp = "Deduction    *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "Commission *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox19.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "% =*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox20.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox21.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            #region//第八行
            temp = "TAX [ 1] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox22.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[ 2] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox23.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox24.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[ 3] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox25.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox26.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox27.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第九行
            temp = "    [ 4] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox28.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[ 5] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox29.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox30.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[ 6] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox31.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox32.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox33.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十行
            temp = "    [ 7] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox34.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[ 8] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox35.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox36.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[ 9] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox37.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox38.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox39.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十一行
            temp = "    [10] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox40.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[11] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox41.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox42.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[12] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox43.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox44.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox45.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十二行
            temp = "    [13] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox46.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[14] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox47.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox48.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[15] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox49.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox50.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox51.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十三行
            temp = "    [16] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox52.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[17] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox53.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox54.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[18] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox55.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox56.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox57.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十四行
            temp = "    [19] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox58.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[20] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox59.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox60.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[21] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox61.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox62.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox63.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十五行
            temp = "    [22] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox64.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[23] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox65.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox66.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[24] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox67.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox68.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox69.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            //第十六行
            temp = "    [25] *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox70.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[26] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox71.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox72.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "[27] *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox73.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox74.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox75.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            #endregion
            //第十七行
            temp = "Remark *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox76.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "Credit Card *";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox77.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox78.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
            //第十八行
            temp = "Net Refund = *";
            ibeg = recvstring.IndexOf(temp);
            if (ibeg >= 0) ibeg += temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox79.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();

            ibeg = iend + temp.Length;
            temp = "*";
            iend = recvstring.IndexOf(temp, ibeg);
            if (iend >= 0) this.textBox80.Text = recvstring.Substring(ibeg, iend - ibeg).Trim();
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string combinestring()
        {
            string ret ="";
            char ch =(char)0x1A;
            string sch = ch.ToString();
            ch = (char)0x0D;
            string returnch = ch.ToString();
            ret += "TRFU:";
            ret += sch;
            ret += textBox1.Text.PadRight(2, ' ');
            ret += sch;
            ret += textBox2.Text.PadRight(2, ' ');
            ret += sch;
            ret += textBox3.Text.PadRight(1, ' ');
            ret += sch;
            ret += textBox4.Text.PadRight(1, ' ');
            ret += returnch;

            ret += sch;
            ret += textBox5.Text.PadRight(3, ' '); ;
            ret += sch;
            ret += textBox6.Text.PadRight(10, ' ');
            ret += sch;
            ret += textBox7.Text.PadRight(7, ' ');
            ret += sch;
            ret += textBox8.Text.PadRight(1, ' ');
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += textBox9.Text.PadRight(1, ' ');
            ret += sch;
            ret += textBox10.Text.PadRight(4, ' ');
            ret += sch;
            ret += textBox11.Text.PadRight(4, ' ');
            ret += sch;
            ret += textBox12.Text.PadRight(4, ' ');
            ret += sch;
            ret += textBox13.Text.PadRight(4, ' ');
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += textBox14.Text.PadRight(46, ' ');
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += textBox15.Text.PadRight(5, ' ');
            ret += sch;
            ret += textBox16.Text.PadRight(18, ' ');
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += textBox17.Text.PadRight(10, ' ');
            ret += sch;
            ret += textBox18.Text.PadRight(1, ' ');
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += textBox19.Text.PadRight(10, ' ');
            ret += sch;
            ret += textBox20.Text.PadRight(5, ' ');
            ret += sch;
            ret += textBox21.Text.PadRight(9, ' ');
            ret += sch;
            ret += returnch;

            for (int i = 0; i < 9; ++i)
            {
                ret += sch;
                for (int j = 0; j < 3; ++j)
                {
                    int index1 = 22 + i * 6 + j * 2;
                    int index2 = index1 + 1;
                    string key1 = "textBox" + index1.ToString();
                    string key2 = "textBox" + index2.ToString();
                    ret += groupBox1.Controls[key1].Text.PadRight(2, '_');
                    ret += sch;
                    ret += groupBox1.Controls[key2].Text.PadRight(9, '_');
                    ret += sch;
                }
                ret += returnch;
            }
            /*
            ret += sch;
            ret += EagleAPI.FullSpace(textBox22.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox23.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox24.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox25.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox26.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox27.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox28.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox29.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox30.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox31.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox32.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox33.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox34.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox35.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox36.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox37.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox38.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox39.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox40.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox41.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox42.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox43.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox44.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox45.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox46.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox47.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox48.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox49.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox50.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox51.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox52.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox53.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox54.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox55.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox56.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox57.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox58.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox59.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox60.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox61.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox62.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox63.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox64.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox65.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox66.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox67.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox68.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox69.Text, 9, "_");
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += EagleAPI.FullSpace(textBox70.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox71.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox72.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox73.Text, 9, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox74.Text, 2, "_");
            ret += sch;
            ret += EagleAPI.FullSpace(textBox75.Text, 9, "_");
            ret += sch;
            ret += returnch;
            */
            ret += sch;
            ret += textBox76.Text.PadRight(2, ' ');
            ret += sch;
            ret += textBox77.Text.PadRight(12, ' ');
            ret += sch;
            ret += textBox78.Text.PadRight(22, ' ');
            ret += sch;
            ret += returnch;

            ret += sch;
            ret += textBox79.Text.PadRight(10, ' ');
            ret += sch;
            ret += textBox80.Text.PadRight(3, ' ');
            ret += sch;
            return ret;
        }

        private void btnTRFX_Click(object sender, EventArgs e)
        {
            string ret = "";
            char ch = (char)0x1A;
            string sch = ch.ToString();
            ch = (char)0x0D;
            string returnch = ch.ToString();
            ret += "TRFX:";
            ret += sch;
            ret += textBox1.Text.PadRight(2, ' ');
            ret += sch;
            ret += textBox2.Text.PadRight(2, ' ');
            ret += sch;
            ret += textBox3.Text.PadRight(1, ' ');
            ret += sch;
            ret += textBox4.Text.PadRight(1, ' ');
            ret += returnch;
            string cmd = m_cmdpool.HandleCommand(ret);
            m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
        }

        private void btnTrfdL_Click(object sender, EventArgs e)
        {
            string cmd = "TRFD:L/" + txtTrfdTktno.Text + txtTrfdOption.Text;
            cmd = m_cmdpool.HandleCommand(cmd);
            m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
        }



        private void btnTrfu_Click(object sender, EventArgs e)
        {
            string combstring = combinestring();
            string cmd = m_cmdpool.HandleCommand(combstring);
            m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
        }

        private void RefundTicket_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}