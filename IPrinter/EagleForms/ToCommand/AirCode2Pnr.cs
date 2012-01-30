using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.ToCommand
{
    public partial class AirCode2Pnr : Form
    {
        EagleProtocal.MyTcpIpClient socket;
        EagleString.CommandPool cmdpool;
        public AirCode2Pnr(EagleString.CommandPool pool,EagleProtocal.MyTcpIpClient sk)
        {
            InitializeComponent();
            cmdpool = pool;
            socket = sk;
        }
        public AirCode2Pnr(string pnr)
        {
            InitializeComponent();
            this.tbAirCode.Text = pnr;
        }

        public string airCode = "";


        private void AirCode_Load(object sender, EventArgs e)
        {
            dtpDate.Value = System.DateTime.Now;
            tbDate.Text = dtpDate.Value.ToString("ddMMM", EagleString.egString.dtFormat);
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            tbDate.Text = dtpDate.Value.ToString("ddMMM", EagleString.egString.dtFormat);
        }

        private void btGetPNR_Click(object sender, EventArgs e)
        {
            try
            {
                tbAirCode.Text = tbAirCode.Text.Trim().ToUpper();
                tbFlightNumber.Text = tbFlightNumber.Text.Trim().ToUpper();
                tbDate.Text = tbDate.Text.Trim().ToUpper();
                if (!EagleString.BaseFunc.PnrValidate(tbAirCode.Text)) throw new Exception("大编码错误");
                if (tbFlightNumber.Text == "") throw new Exception("请输入航班号");
                string cmd = "RRT:V/" + tbAirCode.Text + "/" + tbFlightNumber.Text + "/" + tbDate.Text;
                cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.RRT_AIRCODE2PNR);

                socket.SendCommand(cmd + "~rrt ok~" + cmd, EagleProtocal.TypeOfCommand.Multi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void setcontrol(string retstring)
        {
            try
            {
                retstring = retstring.Replace('+', ' ');
                retstring = retstring.Replace('-', ' ');
                if (retstring.IndexOf("PNR CANCELLED") >= 0) { MessageBox.Show("PNR CANCELLED"); return; }
                //"PNR ALREADY ON THIS SYSTEM. RLOC IS SZ6K5"
                int i = retstring.IndexOf("RLOC IS ");
                if (i < 0) { MessageBox.Show("不能得到编码"); return; };
                textBox4.Text = retstring.Substring(i + 8, 5);
                airCode = textBox4.Text;
            }
            catch(Exception ex)
            {
            }
        }

        private void AirCode2Pnr_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}