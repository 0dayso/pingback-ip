using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EagleForms.ToCommand
{
    /// <summary>
    /// 一键出票对话框类
    /// </summary>
    public partial class EtdzOneKey : Form
    {
        EagleString.Structs.ETDZONEKEY m_onekey;
        EagleProtocal.MyTcpIpClient m_socket;
        EagleString.CommandPool m_cmdpool;
        public EtdzOneKey(EagleString.Structs.ETDZONEKEY onekey,EagleProtocal.MyTcpIpClient socket,EagleString.CommandPool cmdpool)
        {
            InitializeComponent();
            m_onekey = onekey;
            m_socket = socket;
            m_cmdpool = cmdpool;
        }

        private void EtdzOneKey_Load(object sender, EventArgs e)
        {
            cbPat.Enabled = !chkPat.Checked;
            cbPat.SelectedIndex = 0;
        }
        public string PNR { get { return m_pnr; } set { txtPnr.Text = value; } }
        private string m_pnr;
        public string PAT { get { return m_pat; } }
        private string m_pat;
        private void btnEtdz_Click(object sender, EventArgs e)
        {
            m_pnr = txtPnr.Text.Trim();

            if (EagleString.BaseFunc.PnrValidate(m_pnr))
            {
                if (chkPat.Checked) m_pat = "";
                else m_pat = cbPat.Text.Trim();
                m_onekey.SET(m_pnr,m_pat);// = new EagleString.Structs.ETDZONEKEY(m_pnr, m_pat);
                string cmd = m_cmdpool.HandleCommand("rt" + m_onekey.pnr);
                m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.ETDZ_ONEKEY_RT);
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
                //this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请正确输入PNR");
            }
        }

        private void chkPat_CheckedChanged(object sender, EventArgs e)
        {
            cbPat.Enabled = !chkPat.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
///默认eg黑屏指令eg etdz pnr [pat:|pat:A|pat:..]
/// eg etdz pnr         已做PAT的自动出票
/// eg etdz pnr pat项   根据PAT项自动做价格出票
///第一步:发送rt得到pnr信息->rtResult,计算航段序号及时限序号 人数+航段i(1,2...)RR    人数+航段数+3->at/WUHYY
///第二步:a.已做价格->RTpnr,RR,AT/WUHYY,EI,ETDZ
///       b.选择了PAT:A以外的PAT种类
///         1.发送rtpnr~pat:取得pat项
///         2.发送->rtpnr,pat:,pat项,rr,at/wuhyy,ei,etdz
///       c.选择了PAT:A
///         1.发送rtpnr~pat:a取得sfc项
///         2.用户选择sfc项(只有1项，自动选择)，发送rtpnr~pat:a~sfc:0x,rr,at/wuhyy,ei,etdz