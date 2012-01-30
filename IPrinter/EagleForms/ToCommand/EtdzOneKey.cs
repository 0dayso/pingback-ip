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
    /// һ����Ʊ�Ի�����
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
                MessageBox.Show("����ȷ����PNR");
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
///Ĭ��eg����ָ��eg etdz pnr [pat:|pat:A|pat:..]
/// eg etdz pnr         ����PAT���Զ���Ʊ
/// eg etdz pnr pat��   ����PAT���Զ����۸��Ʊ
///��һ��:����rt�õ�pnr��Ϣ->rtResult,���㺽����ż�ʱ����� ����+����i(1,2...)RR    ����+������+3->at/WUHYY
///�ڶ���:a.�����۸�->RTpnr,RR,AT/WUHYY,EI,ETDZ
///       b.ѡ����PAT:A�����PAT����
///         1.����rtpnr~pat:ȡ��pat��
///         2.����->rtpnr,pat:,pat��,rr,at/wuhyy,ei,etdz
///       c.ѡ����PAT:A
///         1.����rtpnr~pat:aȡ��sfc��
///         2.�û�ѡ��sfc��(ֻ��1��Զ�ѡ��)������rtpnr~pat:a~sfc:0x,rr,at/wuhyy,ei,etdz