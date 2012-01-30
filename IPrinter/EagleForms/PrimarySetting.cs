using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// ����ʾ�г̵���ӡ
        /// </summary>
        public void OuterOnlyDisplayReceiptPrint()
        {
            this.Visible = false;
            menu_event_print_receipt_RWY();
            
        }
        /// <summary>
        /// ����ʾ����
        /// </summary>
        public void OuterOnlyDispayBlackWnd()
        {
            OuterCall = true;
            pStatusBar.Dock = System.Windows.Forms.DockStyle.None;
            pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tcMain.TabPages.Remove(tpEasy);
            tcMain.TabPages.Remove(tpFinance);
            tcMain.TabPages.Remove(tpManager);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            m_notifyIcon.Enable = false;
        }
        /// <summary>
        /// �Ƿ���ʾ�Դ��Ҽ��˵�
        /// </summary>
        public bool OuterTakeOwnRightMenu
        {
            get
            {
                return (rightMenu != null);
            }
            set
            {
                if (!value) rightMenu.Items.Clear();
            }
        }
        /// <summary>
        /// ����һ�������Ҽ��˵�
        /// </summary>
        public void OuterAddRightMenu(string menutxt,EventHandler onClick)
        {
            if (rightMenu == null) rightMenu = new System.Windows.Forms.ContextMenuStrip();
            rightMenu.Items.Add(menutxt, null, onClick);
        }
        /// <summary>
        /// �л�����
        /// </summary>
        /// <param name="ipid"></param>
        public void OuterSwitchConfig(string [] ipid)
        {
            EagleString.EagleFileIO.LogWrite("�ⲿ�л�����" + string.Join(",",ipid));
            operation_switch_config(ipid);
        }
        public delegate void Deleg4ssResult(EagleString.SsResult ss);
        /// <summary>
        /// ����PNR��Ҫ���õ�ί�к���(������SsResult)
        /// </summary>
        public Deleg4ssResult OuterDeleg4ssresult;
        public bool OuterCall = false;

        private void OuterHandleSsResult(EagleString.SsResult ss)
        {
            try
            {
                if (OuterCall && OuterDeleg4ssresult != null)
                {
                    OuterDeleg4ssresult.BeginInvoke(ss,null,null);
                }
            }
            catch (Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("OuterHandleSsResult:" + ex.Message);
            }
        }
        
        public delegate void Deleg4rtResult(EagleString.RtResult rt);
        /// <summary>
        /// Rt PNR��Ҫ���õ�ί�к���(������RtResult)
        /// </summary>
        public Deleg4rtResult OuterDeleg4rtresult;
        private void OuterHandleRtResult(EagleString.RtResult rt)
        {
            if (OuterCall && OuterDeleg4rtresult != null)
            {
                OuterDeleg4rtresult.Invoke(rt);
            }
        }
        /// <summary>
        /// ����Socket
        /// </summary>
        public EagleProtocal.MyTcpIpClient OuterSocket { get { return socket; } }
        /// <summary>
        /// ����ָ��
        /// </summary>
        public void OuterSendCommand(string cmd)
        {
            string s = commandPool.HandleCommand(cmd);
            socket.SendCommand(s, EagleProtocal.TypeOfCommand.Multi);
        }
        /// <summary>
        /// ����������ı�
        /// </summary>
        public void OuterAppendText(string s)
        {
            AppendBlackWindow(s);
        }
        public void OuterEtdzOneKey(string pnr)
        {
            menu_event_operation_etdz_one_key(pnr);
        }
        
    }
}
