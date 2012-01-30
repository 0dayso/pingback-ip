using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// 仅显示行程单打印
        /// </summary>
        public void OuterOnlyDisplayReceiptPrint()
        {
            this.Visible = false;
            menu_event_print_receipt_RWY();
            
        }
        /// <summary>
        /// 仅显示黑屏
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
        /// 是否显示自带右键菜单
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
        /// 增加一个黑屏右键菜单
        /// </summary>
        public void OuterAddRightMenu(string menutxt,EventHandler onClick)
        {
            if (rightMenu == null) rightMenu = new System.Windows.Forms.ContextMenuStrip();
            rightMenu.Items.Add(menutxt, null, onClick);
        }
        /// <summary>
        /// 切换配置
        /// </summary>
        /// <param name="ipid"></param>
        public void OuterSwitchConfig(string [] ipid)
        {
            EagleString.EagleFileIO.LogWrite("外部切换配置" + string.Join(",",ipid));
            operation_switch_config(ipid);
        }
        public delegate void Deleg4ssResult(EagleString.SsResult ss);
        /// <summary>
        /// 产生PNR后要调用的委托函数(带参数SsResult)
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
        /// Rt PNR后要调用的委托函数(带参数RtResult)
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
        /// 引用Socket
        /// </summary>
        public EagleProtocal.MyTcpIpClient OuterSocket { get { return socket; } }
        /// <summary>
        /// 发送指令
        /// </summary>
        public void OuterSendCommand(string cmd)
        {
            string s = commandPool.HandleCommand(cmd);
            socket.SendCommand(s, EagleProtocal.TypeOfCommand.Multi);
        }
        /// <summary>
        /// 往黑屏添加文本
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
