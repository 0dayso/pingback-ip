using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace EagleForms.ToCommand
{
    public partial class PnrOrderSubmit : Form
    {
        EagleString.CommandPool m_cmdpool;
        EagleProtocal.MyTcpIpClient m_socket;
        string m_pnr = "";
        public PnrOrderSubmit(EagleString.CommandPool pool,EagleProtocal.MyTcpIpClient sk)
        {
            InitializeComponent();
            m_cmdpool = pool;
            m_socket = sk;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PNRORDERSUBMITPHONE", txtPhone.Text);
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(ht);
            if (txtPhone.Text.Length < 5)
            {
                MessageBox.Show("����ȷ����绰�����������ʱ��ʱ��ϵ");
                return;
            }
            if (EagleString.BaseFunc.PnrValidate(txtPnr.Text.Trim()))
            {
                m_pnr = txtPnr.Text.Trim();
                string cmd = m_cmdpool.HandleCommand("rt" + m_pnr);
                m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.PNR_ORDER_SUBMIT);
                m_socket.SendCommand(cmd, EagleProtocal.TypeOfCommand.Multi);
            }
            else
            {
                MessageBox.Show("PNR����");
                return;
            }
        }

        private void PnrOrderSubmit_Load(object sender, EventArgs e)
        {
            txtPhone.Text = EagleString.EagleFileIO.ValueOf("PNRORDERSUBMITPHONE");
        }
    }
}