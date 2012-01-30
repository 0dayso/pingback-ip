using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.PrintHyx
{
    public partial class AirCode : Form
    {
        public AirCode()
        {
            InitializeComponent();
        }
        public AirCode(string pnr)
        {
            InitializeComponent();
            this.tbAirCode.Text = pnr;
        }

        public string airCode = "";

        static public bool b_opened = false;
        static public string retstring = "";
        static public PrintHyx.AirCode context = null;

        private void AirCode_Load(object sender, EventArgs e)
        {
            dtpDate.Value = System.DateTime.Now;
            tbDate.Text = dtpDate.Value.Day.ToString("d2") + EagleAPI.GetMonthCode(dtpDate.Value.Month);


            retstring = "";
            b_opened = true;
            connect_4_Command.PrintWindowOpen = true;
            context = this;

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            tbDate.Text = dtpDate.Value.Day.ToString("d2") + EagleAPI.GetMonthCode(dtpDate.Value.Month);
        }

        private void btGetPNR_Click(object sender, EventArgs e)
        {
            airCode = "";
            textBox4.Text = "请稍等……";
            tbAirCode.Text = tbAirCode.Text.Trim().ToUpper();
            tbFlightNumber.Text = tbFlightNumber.Text.Trim().ToUpper();
            tbDate.Text = tbDate.Text.Trim().ToUpper();
            if (!EagleAPI.IsRtCode(tbAirCode.Text)) { MessageBox.Show("大编码错误"); return; };
            if (tbFlightNumber.Text == "") { MessageBox.Show("请输入航班号"); return; };
            if (tbDate.Text.Length != 5) { MessageBox.Show("请输入正确日期"); return; };
            EagleAPI.CLEARCMDLIST(3);
            string cmd = "RRT:V/" + tbAirCode.Text + "/" + tbFlightNumber.Text + "/" + tbDate.Text;
            EagleAPI.EagleSendCmd(cmd+"~rrt ok~"+cmd,3);
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
                    PrintHyx.AirCode pt = PrintHyx.AirCode.context;
                    PrintHyx.AirCode.context.Invoke(eh, new object[] { pt, EventArgs.Empty });
                }
            }

        }
        static private void setcontrol(object sender, EventArgs e)
        {
            retstring = retstring.Replace('+', ' ');
            retstring = retstring.Replace('-', ' ');
            if (retstring.IndexOf("PNR CANCELLED") >= 0) { MessageBox.Show("PNR CANCELLED"); return; }
            //"PNR ALREADY ON THIS SYSTEM. RLOC IS SZ6K5"
            int i = retstring.IndexOf("RLOC IS ");
            if (i < 0) { MessageBox.Show("不能得到编码"); return; };
            context.textBox4.Text = retstring.Substring(i + 8, 5);
            context.Close();
            context.airCode = context.textBox4.Text;
        }
    }
}