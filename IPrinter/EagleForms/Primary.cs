using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EagleString;
using EagleControls;
using EagleProtocal;
using EagleExtension;
using EagleWebService;
namespace EagleForms
{
    public partial class Primary : Form,IMessageFilter
    {
        public LoginInfo loginInfo;
        private BlackWindow blackWindow;
        /// <summary>
        /// 注，在重连之时，若重新new，则所有对socket的引将无效，要重新引用
        /// </summary>
        private MyTcpIpClient socket;
        private DataHandler dataHandler = new DataHandler ();
        private CommandPool commandPool;
        private CommandPool commandPool_back;
        private EagleExtension.UploadEticketInfo uploadEticketInfo;
        //返回结果
        private AvResult avResult;
        private SsResult ssResult;
        private RtResult rtResult;
        private DetrResult detrResult;
        private DetrFResult detrFResult;
        private XepnrResult xepnrResult;
        private float userBalance;
        private string commandResult = "";
        private kernalFunc wserviceKernal;
        private IbeFunc wserviceIbe = new IbeFunc();
        private Thread threadWebService;
        //控件
        private LV_Lowest lowestList = new LV_Lowest();
        private LV_GroupList groupList = new LV_GroupList();
        private LV_SpecTicList specTickListFix = new LV_SpecTicList();
        private LV_SpecTicList specTickListFlow = new LV_SpecTicList();
        private WebBrowser webBrowser;
        private EagleControls.Forms.ProgressForm progressForm = new EagleControls.Forms.ProgressForm();
        private EagleControls.MainToolBar mainToolBar = new MainToolBar();
        private EagleControls.MainStatusBar mainStatusBar = new MainStatusBar();
        private EagleExtension.ExpireTicketFind expireTicketFinder;
        /// 菜单
        private ContextMenuStrip m_menuInsurance;
        /// 对话框
        private Printer.Receipt receipt;
        private ToCommand.RefundTicket refundTicket;
        private ToCommand.QueueClear queueClear;
        private ToCommand.AirCode2Pnr airCode2Pnr;
        private General.PassengerAdd passengerAdd;
        private EagleFinance.FormMain finance;
        private Easy.EasyMain easyMain;
        ///其它变量
        private bool m_littleenter = false;
        private bool m_hiderightpanel = false;
        private bool m_ibeUsing = false;
        /// <summary>
        /// 当前AV的全价
        /// </summary>
        private int m_avPrice = 0;
        /// <summary>
        /// 当前AV的距离
        /// </summary>
        private int m_avDistance = 0;
        public Primary()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
        }
        List<string> ls_city;
        private void Primary_Load(object sender, EventArgs e)
        {
            
            InitPrimaryAuthority();


            wserviceKernal = new kernalFunc(loginInfo.b2b.webservice);


            (new Thread(new ThreadStart(load))).Start();
            InitBlackWindow();


            InitSocket(loginInfo.b2b.lr.SERVER_IP, loginInfo.b2b.lr.SERVER_PORT);


            InitRightPanel();
            InitMainMenu();
            InitTimer();


            InitMainToolBar();
            InitMainStatusBar();
            InitScrollNotice();


            (new Thread(new ThreadStart(AddPopupNotice))).Start();
            this.label4.Visible = false;
            ls_city = EagleString.EagleFileIO.WhiteWindowCity(0, true, false);
            try
            {
                InitExpireTicketFinder();
            }
            catch(Exception ex)
            {
                EagleFileIO.LogWrite("InitExpireTicketFinder : " + ex.Message);
            }

            if(!OuterCall)
                this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X, Screen.PrimaryScreen.WorkingArea.Y);

        }
        void load()
        {
            userBalance = EagleExtension.EagleExtension.BALANCE(loginInfo.b2b.username, loginInfo.b2b.webservice);
            scrollNotice1.SetText(wserviceKernal.Get_Notice_Scroll(loginInfo.b2b.username, "0"));
        }
        
        /// <summary>
        /// SOCKET接收事件
        /// </summary>
        void socket_Incept(object sender, InceptEventArgs e)
        {
            lock (o)
            {
                try
                {
                    byte[] buffer = new byte[e.Astream.Length];
                    e.Astream.Position = 0;
                    e.Astream.Read(buffer, 0, (int)e.Astream.Length);
                    dataHandler.InitInputArgs(buffer, loginInfo, EagleProtocal.EagleProtocal.MsgNo++);
                    dataHandler.MONEY = userBalance.ToString("f2");
                    dataHandler.recvHandle();
                    EagleFileIO.LogWrite(dataHandler.TYPE350 ? "RECV:来自350配置" : "RECV:来自443配置");
                    string tails = "";
                    if (!dataHandler.TYPE350) tails = "\r\n>";
                    if (!dataHandler.BackGroundCommand)
                    {
                        
                        switch (commandPool.TYPE)
                        {
                            case ETERM_COMMAND_TYPE.TRFD: break;
                            default:
                                if (OuterCall &&
                                    (dataHandler.typeOfRecv == DataHandler.TypeOfRecv.Passport))
                                {
                                }
                                else
                                {
                                    if (loginInfo.b2b.lr.AuthorityOfFunction("0FN") &&commandPool.TYPE == ETERM_COMMAND_TYPE.RT)//关闭了FN项的显示
                                    {
                                        string fn = EagleString.egString.Between2String(dataHandler.Text, "FN/", "\n");
                                        if (fn != "") commandResult = (dataHandler.Text.Replace(fn, ""));
                                        else commandResult = dataHandler.Text;
                                        fn = EagleString.egString.Between2String(dataHandler.Text, "FC/", "\n");
                                        if (fn != "") commandResult = (commandResult.Replace(fn, ""));
                                        AppendBlackWindow(commandResult);
                                    }
                                    else
                                    {
                                        AppendBlackWindow(commandResult = (dataHandler.Text + tails));
                                    }
                                }
                                break;
                        }
                    }
                    if (dataHandler.BUFFER != null) socket.Send(dataHandler.BUFFER);
                    EagleFileIO.LogWrite(commandResult);//dataHandler.Text
                    switch (dataHandler.PROMOPTTYPE)
                    {
                        case PromoptType.NewApply:
                            string s = "";
                            m_notifyIcon.start("9:收到申请舱位消息\r\n" + s);
                            break;
                        case PromoptType.ApplyHandled:
                            EagleProtocal.PACKET_PROMOPT_FINISH_APPLY_RESULT packet = 
                                new EagleProtocal.PACKET_PROMOPT_FINISH_APPLY_RESULT();
                            packet.FromBytes(buffer);
                            m_notifyIcon.start("A:收到申请舱位被处理消息\r\n" + packet.content);
                            break;
                        case PromoptType.NewOrder:
                            m_notifyIcon.start("6:有新订单\r\n");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    EagleFileIO.LogWrite("SOCKET接收处理时发生错误:" + ex.Message);
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(dataHandler.Text) && !dataHandler.BackGroundCommand)
                    HandleResult();
            }
            catch (Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("Primary.HandleResult" + ex.Message);
            }
            try
            {
                //if (dataHandler.BackGroundCommand) dataHandler.COMMANDRESULT_BACK = dataHandler.Text;
                if (!string.IsNullOrEmpty(dataHandler.COMMANDRESULT_BACK))
                {
                    string t = dataHandler.COMMANDRESULT_BACK;
                    dataHandler.COMMANDRESULT_BACK = "";
                    HandleResult_background(t);
                }
            }
            catch
            {
            }
        }
        object o = new object();
        /// <summary>
        /// SOCKET错误事件
        /// </summary>
        void socket_Error(object sender, ErrorEventArgs e)
        {
            AppendBlackWindow("\r\nSOCKET 错误：" + e.Error.Message);
        }
        /// <summary>
        /// 委托往黑屏增加文本
        /// </summary>
        void AppendBlackWindow(string txt)
        {
            if (blackWindow.InvokeRequired)
            {
                
                delegAppend del= blackWindow.AppendResult;
                blackWindow.Invoke(del, new object[] {txt });
            }
            else
            {
                blackWindow.AppendResult(txt);
            }
        }
        delegate void delegAppend(string txt);
        /// <summary>
        /// 重载消息过滤
        /// </summary>
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if ((Keys)m.WParam.ToInt32() == Keys.Enter)
            {
                if (m.LParam.ToInt32() == 1835009 && m.Msg == 256)//大回车,256为KeyDown,258为KeyUp
                {
                    m_littleenter = false;
                }
                else if (m.LParam.ToInt32() == 18612225 && m.Msg == 256)//小回车?
                {
                    m_littleenter = true;
                }
            }
            return false;
        }
        protected override void DefWndProc(ref Message m)
        {
            
            if (m.Msg == (int)EagleString.Imported.egMsg.SWITCH_CONFIG)
            {
                int ipid = (int)m.WParam;
                string[] ips = new string[] { ipid.ToString()};
                operation_switch_config(ips);
                Thread.Sleep(200);
                operation_switch_config(ips);
            }
            else
            {
                base.DefWndProc(ref m);
            }
            
        }
        private void Primary_SizeChanged(object sender, EventArgs e)
        {
            OnResize();
        }
        /// <summary>
        /// 隐藏显示右面板
        /// </summary>
        private void HideRightPanel(bool hide)
        {
            m_hiderightpanel = hide;
            if (hide)
                pMain.Dock = DockStyle.Bottom;
            else
                pMain.Dock = DockStyle.None;
        }
        private void OnResize()
        {
            int totHeight = pMain.Height;//pStatusBar.Location.Y - pRight1.Location.Y;
            int w = pRight1.Size.Width;
            int h = totHeight * 1 / 5;
            if (h == 0) return;
            int x = pRight1.Location.X;
            pRight1.Size = new Size(w, h);
            pRight1.Location = new Point(x, pMain.Location.Y);
            pRight2.Location = new Point(x, pRight1.Location.Y + h);
            pRight2.Size = new Size(w, h);
            pRight3.Location = new Point(x, pRight2.Location.Y + h);
            h = totHeight * 3 / 10;
            pRight3.Size = new Size(w, h);
            pRight4.Location = new Point(x, pRight3.Location.Y + h);
            h = totHeight - pRight3.Size.Height - pRight2.Size.Height - pRight1.Size.Height;
            pRight4.Size = new Size(w, h);
            int tolerance = 6;
            panel1.Location = new Point(label1.Location.Y + label1.Height + tolerance);
            panel2.Location = new Point(label2.Location.Y + label2.Height + tolerance);
            panel3.Location = new Point(label3.Location.Y + label3.Height + tolerance);
            panel1.Height = pRight1.Height - label1.Height - tolerance;
            panel2.Height = pRight3.Height - label1.Height - tolerance;
            panel3.Height = pRight4.Height - label1.Height - tolerance;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                receipt.Dispose();
            }
            catch
            {
            }
            Application.RemoveMessageFilter(this);
            this.Close();
            Application.Exit();
        }
        private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            HandleMenuClick(e);
        }
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcMain.SelectedTab.Name)
            {
                case "tpBlack"://黑屏
                    menu_event_window_blackwindow_normal();
                    menu_event_window_rightpanel_show();
                    break;
                case "tpManager"://后台
                    if (webBrowser == null)
                    {
                        webBrowser = new WebBrowser();
                        string url = "";
                        if (string.IsNullOrEmpty(loginInfo.b2c.username))
                        {
                            url = loginInfo.b2b.webside
                                + "?user="
                                + loginInfo.b2b.username
                                + "&pwd="
                                + loginInfo.b2b.password;
                        }
                        else
                        {
                            url = loginInfo.b2c.website 
                                + "/default.aspx?strSessionUUID=" 
                                + loginInfo.b2c.lr.uuid;
                        }
                        webBrowser.Navigate(url);
                        tpManager.Controls.Add(webBrowser);
                        webBrowser.Dock = DockStyle.Fill;
                    }
                    menu_event_window_blackwindow_max();
                    menu_event_window_rightpanel_hide();
                    break;
                case "tpFinance"://对帐
                    tpFinance.Tag = 9999;
                    if (finance == null)
                    {
                        finance = new EagleFinance.FormMain(loginInfo, socket, commandPool);
                        finance.TopLevel = false;
                        tpFinance.Controls.Add(finance);
                        finance.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        finance.set_args(loginInfo, socket, commandPool);
                    }
                    
                    menu_event_window_blackwindow_max();
                    menu_event_window_rightpanel_hide();
                    finance.Size = new Size(tpFinance.Size.Width, tpFinance.Size.Height);
                    finance.Show();
                    break;
                case "tpEasy"://简版
                    if (easyMain == null)
                    {
                        easyMain = new global::EagleForms.Easy.EasyMain(socket, loginInfo,commandPool);
                        tpEasy.Controls.Add(easyMain);
                        easyMain.Show();
                    }
                    break;
                case "tpReceipt":
                    if (!loginInfo.b2b.lr.AuthorityOfFunction("003"))
                    {
                        AppendBlackWindow("无权使用\r\n>");
                        break;
                    }
                    if (receipt == null)
                        receipt = new global::EagleForms.Printer.Receipt(socket, commandPool, loginInfo);
                    receipt.TopLevel = false;
                    receipt.FormBorderStyle = FormBorderStyle.None;
                    tpReceipt.Controls.Add(receipt);
                    receipt.Show();
                    break;
            }
        }
        private void Primary_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                EagleExtension.EagleExtension.LogUpload(loginInfo, 0);
                if (receipt != null) receipt.Dispose();
                if (refundTicket != null) refundTicket.Dispose();
                if (queueClear != null) queueClear.Dispose();
                if (airCode2Pnr != null) airCode2Pnr.Dispose();
                if (passengerAdd != null) passengerAdd.Dispose();
                if (m_notifyIcon != null) m_notifyIcon.Dispose();
            }
            catch
            {
            }
        }

        private void mainMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Size worksize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            if (this.Size == worksize)
                this.Size = new Size(640, 480);
            else
                this.Size = worksize;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X, Screen.PrimaryScreen.WorkingArea.Y);
        }
        private bool isFirst = true;
        private bool toBlock = true;
        private Point prevLeftClick;
        private void mainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // If this is the first mouse move event for left click dragging 
                // of the form, store the current point clicked so that we can use 
                // it to calculate the form's new location in subsequent mouse move 
                // events due to left click dragging of the form 
                if (isFirst == true)
                {
                    // Store previous left click position 
                    prevLeftClick = new Point(e.X, e.Y);
                    // Subsequent mouse move events will not be treated as first time, 
                    // until the left mouse click is released or other mouse click 
                    // occur 
                    isFirst = false;
                }
                // On subsequent mouse move events with left mouse click down. 
                // (i.e. During dragging of form) 
                else
                {
                    // This flag here is to do alternate processing for the form 
                    // dragging because it causes serious flicking when u allow 
                    // every such events to change the form's location. 
                    // You can try commenting this out to see what i mean 
                    if (toBlock == false)
                        this.Location = new Point(this.Location.X + e.X -
                        prevLeftClick.X, this.Location.Y + e.Y - prevLeftClick.Y);
                    // Store new previous left click position 
                    prevLeftClick = new Point(e.X, e.Y);
                    // Allow or deny next mouse move dragging event 
                    toBlock = !toBlock;
                }
            }
            // This is a new mouse move event so reset flag 
            else
                isFirst = true; 
        }
    }
}