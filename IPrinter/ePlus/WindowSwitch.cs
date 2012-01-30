///Create By Wang.Clawc   2006-12-20
///Eagle.Net

#define Branch1
#define TRACE
//#define Sync
#define NewProtocol

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using gs.para;
using System.Diagnostics;


namespace ePlus
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    
    public class WindowInfo : IDisposable
    {

        /// <summary>
        /// 获得或设置窗口标签名称。
        /// </summary>
        public string Caption = "";
        //public ServerInfo ServerParameter
        //{
        //    get
        //    {
        //        return m_ServerInfo; 
        //    }
        //}
        ///// <summary>
        ///// 获得或设置与服务器通讯的Socket
        ///// </summary>
        //public System.Net.Sockets.Socket ConnSocket = null;
        /// <summary>
        /// 取得窗口中文本编辑器。
        /// </summary>
        public System.Windows.Forms.RichTextBox Editor
        {
            get
            {
                return m_Editor; 
            }
        }
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="caption">窗口标签名称</param>
        public WindowInfo(string caption)
        {
            this.Caption = caption;
        }

        /// <summary>
        /// 创建与服务器的连接
        /// </summary>
        /// <param name="parentWindow"></param>
        public void connect_3(System.Windows.Forms.TabPage parentWindow, ServerInfo serverInfo)
        {
            m_ParentWindow = parentWindow;

            //1、根据配置文件，得到服务器信息
            //2、建立Socket连接
            command = new connect_4_Command(serverInfo, m_ParentWindow);

            //if (!command.IsConnected)
            //{
            //    return;
            //}

            #region 3、新建一个Editor，放至窗口中。
            m_Editor = new System.Windows.Forms.RichTextBox();
            groupBox = new Panel();
            groupBox.BackColor = System.Drawing.Color.Blue;
            EditorEventArgs editorEventArgs = new EditorEventArgs(m_Editor);
            OnEditorCreating(editorEventArgs);

            m_Editor.Size = new System.Drawing.Size(parentWindow.Size.Width * 3 / 4, parentWindow.Height);//FOR NKG
            groupBox.Size = new System.Drawing.Size(parentWindow.Size.Width - m_Editor.Size.Width, parentWindow.Height);//FOR NKG
            if (GlobalVar.gbIsNkgFunctions)
                m_Editor.Dock = System.Windows.Forms.DockStyle.Left;//.Fill; //FOR NKG
            else m_Editor.Dock = System.Windows.Forms.DockStyle.Fill;//.Fill; //FOR NKG
            groupBox.Dock = DockStyle.Right; //FOR NKG
            m_Editor.KeyDown += new System.Windows.Forms.KeyEventHandler(m_Editor_KeyDown);
            m_Editor.KeyDown += new System.Windows.Forms.KeyEventHandler(m_Editor_KeyUp);
            m_Editor.MouseUp += new MouseEventHandler(m_Editor_MouseUp);
            ShortCutHandle.NewShorCutHandle(m_Editor);
            m_Editor.PerformLayout();
            m_Editor.WordWrap = false;


            //m_Editor.CanPaste(System.Windows.Forms.DataFormats.GetFormat(System.Windows.Forms.DataFormats.Html));        

            OnAfterConnect(EventArgs.Empty);
            parentWindow.SuspendLayout();
            {
                pnlLeft = new Panel();
                pnlLeft.BorderStyle = BorderStyle.FixedSingle;
                

                parentWindow.Controls.Add(pnlLeft);

#if RWY
                pnlLeft.Dock = DockStyle.Fill;
#endif
                pnlLeft.Controls.Add(m_Editor);
            }
            if (GlobalVar.gbIsNkgFunctions)
                parentWindow.Controls.Add(groupBox);//FOR NKG
            parentWindow.ResumeLayout(false);
            parentWindow.PerformLayout();
            #endregion

            command.SetEditor(m_Editor);

            m_Connected = true;

            //m_Editor.Font = new System.Drawing.Font(m_Editor.Font.FontFamily, 12F);            
            //EagleAPI.SpecifyPassport();


        }

        void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if(sender.Equals (pnlLeft))
            {
                Cursor.Current = Cursors.Arrow;
                if (e.X != pnlLeft.Width) return;
                if (e.Y > 0 && e.Y < pnlLeft.Height) return;

                Cursor.Current = Cursors.VSplit;
            }
            if (sender.Equals(pnlRightTop))
            {
                Cursor.Current = Cursors.Arrow;
                if (e.X != 0 ) return;
                if (!(e.Y > 0 && e.Y < pnlRightTop.Height)) return;
                Cursor.Current = Cursors.VSplit;
                if (e.Button == MouseButtons.Left)
                {
                    m_Editor.AppendText(e.Y.ToString());
                }
            }
        }

        void m_Editor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_Editor.Tag = this; 
        }
        void m_Editor_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_Editor.Tag = this;
        }
        //右键菜单
        void m_Editor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m_Editor.Tag = this;
            if (e.Button == MouseButtons.Right)
            {
                EventHandler eh1 = new EventHandler(copy);
                EventHandler eh2 = new EventHandler(paste);
                EventHandler eh3 = new EventHandler(cut);
                EventHandler eh4 = new EventHandler(clear);
                EventHandler eh5 = new EventHandler(print);
                EventHandler eh6 = new EventHandler(printscreen);
                EventHandler eh7 = new EventHandler(resend);
                EventHandler eh8 = new EventHandler(usecurrentconfiglonely);
                EventHandler eh9 = new EventHandler(useibe);
                ContextMenu rightMenu = new ContextMenu();
                
                //rightMenu.MenuItems.
                rightMenu.MenuItems.Add("(&C)复制\tCTRL+C", eh1);
                rightMenu.MenuItems.Add("(&V)粘贴\tCTRL+V", eh2);
                rightMenu.MenuItems.Add("(&X)剪切\tCTRL+X", eh3);

                rightMenu.MenuItems.Add("-");
                rightMenu.MenuItems.Add("(&D)清除内容", eh4);
                rightMenu.MenuItems.Add("(&P)打印选定内容", eh5);
                //rightMenu.MenuItems.Add("打印屏幕显示内容", eh6);
                rightMenu.MenuItems.Add("(&R)重发当前指令\tCTRL+F12", eh7);
                rightMenu.MenuItems.Add("-");

                MenuItem mi = new MenuItem("(&L)独占当前配置", eh8);
                mi.Checked = GlobalVar.bUsingConfigLonely;
                //mi.BarBreak = true;
                rightMenu.MenuItems.Add(mi);

                mi = new MenuItem(BookTicket.bIbe ? "(&I)取消使用IBE" : "(&I)使用IBE查询", eh9);
                mi.Checked = BookTicket.bIbe;
                
                rightMenu.MenuItems.Add(mi);

                rightMenu.MenuItems.Add("-");

                EventHandler eha = new EventHandler(tpr);
                mi = new MenuItem("(&T)取当前配置昨日报表……", eha);
                rightMenu.MenuItems.Add(mi);

                EventHandler ehb = new EventHandler(tsl);
                mi = new MenuItem("(&T)取当前配置今日报表……", ehb);
                rightMenu.MenuItems.Add(mi);

                EventHandler ehc = new EventHandler(trfu);

                rightMenu.MenuItems.Add("-");
                mi = new MenuItem("(&R)退票", ehc);
                rightMenu.MenuItems.Add(mi);

                EventHandler ehd = new EventHandler(SpeckTickHandle);
                mi = new MenuItem("(&H)舱位申请处理", ehd);
                rightMenu.MenuItems.Add(mi);

                System.Drawing.Point ep = new System.Drawing.Point(e.X, e.Y);
                rightMenu.Show(Editor, ep);
                
            }
        }
        private void SpeckTickHandle(object sender, EventArgs e)
        {
            EagleForms.General.SpecTickHandle ef =
                new EagleForms.General.SpecTickHandle(GlobalVar.WebServer, GlobalVar.loginName);
            ef.dg_spectick = new EagleForms.General.SpecTickHandle.deleg4SepcTick(EagleAPI.EagleSendBytes);
            ef.Show();
            //DialogResult dr;
            //do
            //{
            //    ef.READ_NO_HANDLE();
            //    dr = ef.Show();
            //    if (ef.PASSPORT != "" && dr == DialogResult.OK)
            //    {
            //        EagleProtocal.PACKET_PROMOPT_FINISH_APPLAY ep = new EagleProtocal.PACKET_PROMOPT_FINISH_APPLAY
            //        (EagleProtocal.EagleProtocal.MsgNo++, new string[] { ef.PASSPORT },ef.SUBMIT_XML);
            //        EagleAPI.EagleSendBytes(ep.ToBytes());
            //    }
            //} while (dr == DialogResult.OK);
        }
        private void trfu(object sender, EventArgs e)
        {
            NetworkSetup.RefundTicket rt = new ePlus.NetworkSetup.RefundTicket();
            rt.ShowDialog();

        }
        private void tpr(object sender, EventArgs e)
        {
            Model.Tpr.b启动独占配置 = true;
            if (GlobalVar.bUsingConfigLonely)
            {
                Model.Tpr.run();
            }
            else
            {
                usecurrentconfiglonely(sender, e);
            }
        }
        private void tsl(object sender, EventArgs e)
        {
            Model.Tpr.b启动独占配置 = true;
            if (GlobalVar.bUsingConfigLonely)
            {
                Model.Tpr.run1("tsl:c/");
            }
            else
            {
                usecurrentconfiglonely(sender, e);
            }
        }
        private void useibe(object sender, EventArgs e)
        {
            BookTicket.bIbe = !BookTicket.bIbe;
            
        }
        private void usecurrentconfiglonely(object sender,EventArgs e)
        {
            try
            {
                if (!Model.md.b_00D) throw new Exception("您无权独占配置!");
                if (GlobalVar.bUsingConfigLonely) throw new Exception("配置独占中!");
                if (GlobalVar.current_using_config.IndexOf('~') >= 0)
                    throw new Exception("独占配置失败，可能选择了多个配置!");
                Editor.AppendText("\r\n" + "开始尝试独占当前配置……" + "\r\n>");
                command.useconfiglonely(int.Parse(GlobalVar.current_using_config));
            }
            catch(Exception ex)
            {
                Editor.AppendText("\r\n" + ex.Message + "\r\n>");
            }
        }
        private void resend(object sender, EventArgs e)
        {
            Editor.AppendText("\r\n重发指令：# " + GlobalVar.CurrentSendCommands.Replace("~"," # ") + "\r\n>");
            EagleAPI.EagleSendOneCmd(GlobalVar.CurrentSendCommands);
        }
        private void copy(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(Editor.SelectedText, true, 5, 10);
                //Editor.Copy();
                
                //Clipboard.SetText(Clipboard.GetText());
            }
            catch { };
        }
        private void paste(object sender, EventArgs e)
        {
            try
            {
                //Clipboard.SetDataObject(Clipboard.GetText(TextDataFormat.Text), true, 5, 10);
                //Clipboard.SetText(Clipboard.GetText(TextDataFormat.Text), TextDataFormat.Text);
                //Clipboard.SetText(Clipboard.GetText());
                Editor.Paste();
            }
            catch { }
            //Editor.Text = Editor.Text;
        }
        private void cut(object sender, EventArgs e)
        {
            Editor.Cut();
        }
        private void clear(object sender, EventArgs e)
        {
            Editor.Clear();
            Editor.AppendText(">");
        }



        private void print(object sender, EventArgs e)
        {
            frmMain fm = new frmMain();
            fm.printDocument1.Print();
        }
        private void printscreen(object sender, EventArgs e)
        {
            print(sender, e);
        }
        //断开与服务器的连接
        public void DisConnect()
        {
            command.DisConnect();
            OnAfterDisConnect(EventArgs.Empty);   
        }

        public void SendData(string data)
        {
            try
            {
                command.Send(data, 3);
            }
            catch(Exception ex)
            {
                MessageBox.Show("SendData : " + ex.Message);
            }
        }
        public void SendData(string data,int msgtype)
        {
            try
            {
                command.Send(data, msgtype);
            }
            catch(Exception ex)
            {
                MessageBox.Show("SendData : " + ex.Message);
            }
        }
        public void SendData(char[] content, int length)
        {
            command.SendData(content,length);
        }
        public void sendbyte(byte[] b)
        {
            command.sendbyte(b);
        }
        /// <summary>
        /// 未用到此重载
        /// </summary>
        /// <param name="data"></param>
        public void Send3IN1(string data)
        {
            command.SendData(data);
        }
        public void Send3IN1(char[] content, int length)
        {
            command.SendData(content, length);
        }
        static public void SendData_static(string data)
        {
            
        }

        #region 事件定义与处理
        //Unique Keys
        static readonly object editorCreatingKey = new object();
        static readonly object afterConnectKey = new object();
        static readonly object afterDisConnectKey = new object(); 

        protected System.ComponentModel.EventHandlerList eventList = new System.ComponentModel.EventHandlerList(); 
        
        public event EventHandler<EditorEventArgs> EditorCreating
        {
            add
            {
                eventList.AddHandler(editorCreatingKey,value); 
            }
            remove
            {
                eventList.RemoveHandler(editorCreatingKey, value);   
            }
        }

        public event EventHandler AfterConnect
        {
            add
            {
                eventList.AddHandler(afterConnectKey, value);   
            }
            remove
            {
                eventList.RemoveHandler(afterConnectKey, value);   
            }
        }

        public event EventHandler AfterDisConnect
        {
            add
            {
                eventList.AddHandler(afterDisConnectKey, value);
            }
            remove
            {
                eventList.RemoveHandler(afterDisConnectKey, value);
            }
        }

        protected virtual void OnEditorCreating(EditorEventArgs e)
        {
            EventHandler<EditorEventArgs> editorEventHandle = (EventHandler<EditorEventArgs>)eventList[editorCreatingKey];
            if (editorEventHandle != null)
            {
                editorEventHandle(this, e);
            }
        }

        protected virtual void OnAfterConnect(EventArgs e)
        {
            EventHandler afterConnect = (EventHandler)eventList[afterConnectKey];

            if (afterConnect != null)
            {
                afterConnect(this, e); 
            }
        }

        protected virtual void OnAfterDisConnect(EventArgs e)
        {
            EventHandler afterDisConnect = (EventHandler)eventList[afterDisConnectKey];

            if (afterDisConnect != null)
            {
                this.m_Editor.Dispose();
                this.m_Editor = null;
                afterDisConnect(this, e);
            }
        }
        #endregion

        #region private members

        private Panel pnlLeft = null;
        private Panel pnlRightTop = null;
        private Panel pnlRightMiddle = null;
        private Panel pnlRightBottom = null;
        private Panel pnlRightBottom2 = null;
        private ListView lvRightTop = null;
        private EagleControls.LV_Lowest lvLowest = null;
        private EagleControls.LV_GroupList lvGroup = null;
        private EagleControls.LV_SpecTicList lvSpecTick = null;
        private EagleControls.LV_SpecTicList lvSpecTick2 = null;

        private System.Windows.Forms.RichTextBox m_Editor = null;
        private System.Windows.Forms.Panel groupBox = null;//FOR NKG
        private System.Windows.Forms.TabPage m_ParentWindow = null;
        private connect_4_Command command = null;
        #endregion 

        #region IDisposable 成员

        public void Dispose()
        {
            
        }

        #endregion

        /// <summary>
        /// 获取与服务器的连接装态。
        /// </summary>
        public bool Connected
        {
            get
            {
                //return m_Connected;
                return (command != null && command.IsConnected);
            }
        }

        private bool m_Connected = false;
    }


    public class EditorEventArgs : System.EventArgs
    {
        public EditorEventArgs(System.Windows.Forms.RichTextBox editor)
        {
            this.m_Editor = editor;  
        }

        /// <summary>
        /// 获得正在使用的Editor
        /// </summary>
        public System.Windows.Forms.RichTextBox Editor
        {
            get
            {
                return m_Editor; 
            }
        }

        private System.Windows.Forms.RichTextBox m_Editor = null;   
    }
    /// <summary>
    /// 窗口选择器，负责窗口的新建与关闭。
    /// </summary>
    public class WindowSwitch   
    {
        //用于保存所有的窗口状态
        private static List<WindowInfo> windowList = new List<WindowInfo>();
        /// <summary>
        /// 获取允许创建的最大窗口数。
        /// </summary>
        public static int MaxWindowNum
        {
            get
            {
                return (new Properties.Settings()).MaxWindow;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public WindowSwitch(System.Windows.Forms.TabControl parent)
        {
            this.m_Parent = parent;  ;  
            
        }

        /// <summary>
        /// 在父TabControl上新建一个窗口(TabPage)
        /// </summary>
        /// <returns>窗口信息对象 WindowInfo</returns>
        /// <see cref="WindowInfo"/>
        public WindowInfo New()
        {
            if (m_Parent.TabCount < MaxWindowNum)
            {
                WindowInfo wi = new WindowInfo(GetWindowCaption());

                m_Parent.TabPages.Add(wi.Caption);
                windowList.Add(wi);
                return wi;
            }
            else
                throw new Exception("已到达允许创建窗口的最大数！"); 
            
        }

        /// <summary>
        /// 在父TabControl上新建一个窗口(TabPage)
        /// </summary>
        /// <param name="jumpTo">是否立即跳至新窗口</param>
        /// <returns>窗口信息对象 WindowInfo</returns>
        /// <see cref="WindowInfo"/>
        public WindowInfo New(bool jumpTo)
        {
            WindowInfo wi = New();

            if (jumpTo)
            {
                m_Parent.SelectedIndex = m_Parent.TabCount - 1;  
            }

            return wi;
        }

        /// <summary>
        /// 在父TabControl上新建一个窗口(TabPage)
        /// </summary>
        /// <param name="caption">窗口标签</param>
        /// <returns>窗口信息对象 WindowInfo</returns>
        public WindowInfo New(string caption)
        {
            WindowInfo wi = new WindowInfo(caption);

            m_Parent.TabPages.Add(wi.Caption);
            windowList.Add(wi);
            return wi;
        }

        /// <summary>
        /// 在父TabControl上新建一个窗口(TabPage)
        /// </summary>
        /// <param name="caption">窗口标签</param>
        /// <param name="jumpTo">是否立即跳至新窗口</param>
        /// <returns>窗口信息对象 WindowInfo</returns>
        public WindowInfo New(string caption,bool jumpTo)
        {
            WindowInfo wi = New(caption);

            if (jumpTo)
            {
                m_Parent.SelectedIndex = m_Parent.TabCount - 1;
            }

            return wi;
        }

        /// <summary>
        /// 移除当前窗口
        /// </summary>
        public void Remove()
        {
            windowList.RemoveAt(m_Parent.SelectedIndex);
            m_Parent.TabPages.RemoveAt(m_Parent.SelectedIndex);
        }

        /// <summary>
        /// 通过序号移除窗口
        /// </summary>
        /// <param name="index">序号。从0开始计数</param>
        public void Remove(int index)
        {
            windowList.RemoveAt(index);
            m_Parent.TabPages.RemoveAt(index);
        }

        
        public WindowInfo this[int index]
        {
            get
            {
                if (index < 0 || index >= windowList.Count)
                    return null;

                return windowList[index]; 
            }
        }

        #region 属性
        /// <summary>
        /// 获取当前活动窗口序号
        /// </summary>
        public int ActiveWindowIndex
        {
            get
            {
                return m_Parent.SelectedIndex;  
            }
        }

        /// <summary>
        /// 获取当前活动窗口标签
        /// </summary>
        public string ActiveWindowName
        {
            get
            {
                return m_Parent.SelectedTab.Text;   
            }
        }

        /// <summary>
        /// 获取当前活动窗口的TabPage对象
        /// </summary>
        public System.Windows.Forms.TabPage ActiveWindow
        {
            get
            {
                return m_Parent.SelectedTab;  
            }
        }
        #endregion

        #region 私有成员
        private System.Windows.Forms.TabControl m_Parent = null;

        private string GetWindowCaption()
        {
            if (windowList.Count < 1)
            {
                return Properties.Resources.FirstWindowText; 
            }
            else
            {
                int num = windowList.Count, temp = -1;
                List<int> array = new List<int>();  

                foreach (WindowInfo wi in windowList)
                {
                    string[] dim = wi.Caption.Split(' ');
                    if (dim.Length > 1)
                    {
                        temp = int.Parse(dim[1]);

                        if (temp > num)
                            array.Add(temp);
 
                        if (temp == num)
                            num++;
                    }
                }

                while (array.IndexOf(num) > -1)
                    num++;

                array.Clear(); 
                return Properties.Resources.NewWindowPrefixText + " " + num.ToString();   
            }
        }
        #endregion

        /// <summary>
        /// 获取当前窗口的个数
        /// </summary>
        public int Count
        {
            get
            {
                return windowList.Count;  
            }
        }
    }

    #region 处理事务型命令组

    public class connect_4_Command
    {
        ~connect_4_Command()
        {
            try
            {
                m_notifyIcon.stop();
            }
            catch
            {
            }
        }
#if RWY//added by king
        private EagleNotifyIcon.EagleNotify m_notifyIcon;
#else
        private EagleNotifyIcon.EagleNotify m_notifyIcon = new EagleNotifyIcon.EagleNotify(
            new string[] { 
                Application.StartupPath+"\\e0.ico",
                Application.StartupPath+"\\e1.ico",
                Application.StartupPath+"\\e2.ico",
                Application.StartupPath+"\\e3.ico",
                Application.StartupPath+"\\e4.ico",
            });
#endif
        public static connect_4_Command cmdWindow ;
        public static bool PrintWindowOpen = false;
        public static string AV_String ="";
        public static string SendString = "";//存放发送串
        public static string ReceiveString = "";//存放返回串
        public NewDecFee2 newdefee;//新扣款对象

        Options.ibe.IbeBlack ibb = new Options.ibe.IbeBlack();

        private object osend = new object();
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="content">指令内容</param>
        public void Send(string content, int msgtype)
        {

            lock (osend)
            {
                try
                {
                    if (!string.IsNullOrEmpty(content)) m_cmdpool.HandleCommand(content);
                    if (content.ToUpper().IndexOf("~ETDZ") >= 0) m_cmdpool.SetType(EagleString.ETERM_COMMAND_TYPE.ETDZ);
                    switch (m_cmdpool.TYPE)
                    {
                        case EagleString.ETERM_COMMAND_TYPE.ETDZ:
                            int price = EagleString.EagleFileIO.PriceOfPnrInFile(m_cmdpool.PNRing);

                            float yue = EagleExtension.EagleExtension.BALANCE(GlobalVar.loginName, GlobalVar.WebServer);
                            if (yue < (float)price)
                            {
                                MessageBox.Show("您的余额不足，不能出票！");
                                content = "i";
                                return;
                            }
                            if (GlobalVar.serverAddr == GlobalVar.ServerAddr.KunMing)
                            {
                                EagleAPI.LogWrite("GlobalVar.f_Balance:" + GlobalVar.f_Balance + "\r\n");
                                //if (GlobalVar.f_Balance < 0)
                                //{
                                //    MessageBox.Show("出票过多，请检查余额！");
                                //    return;
                                //}
                            }
                            break;
                    }
                }
                catch
                {
                }
                //bool bNewDecSend = false;
                //if (content.IndexOf("^|_^") > -1)
                //{
                //    bNewDecSend = true;
                //}
                //else
                //{
                //    bNewDecSend = false;
                //}

                //content = content.Trim();
                //cr ea te by wa ng .c la wc
                //if (content == "") return;
                //string[] rrt = content.Split('~');
                //for (int i = 0; i < rrt.Length; i++)
                //{
                //    if (rrt[i].ToLower().IndexOf("rrt") == 0)
                //    {
                //        if (GlobalVar.bHakUser)
                //        {
                //            //AppendEditorText("\r\n无法使用RRT指令\r\n>");
                //            //return;
                //        }
                //    }
                //}



                content = content.Trim();
                if (content == "") return;
                string[] rrt = content.Split('~');
                for (int i = 0; i < rrt.Length; i++)
                {
                    if (rrt[i].ToLower().IndexOf("rrt") == 0)
                    {
                        if (GlobalVar.bHakUser)
                        {
                            //AppendEditorText("\r\n无法使用RRT指令\r\n>");
                            //return;
                        }
                    }
                }

                if (GlobalVar.bUsingConfigLonely)//不管是３还是７都置30秒
                    GlobalVar.dtLonelyStart = DateTime.Now;
                GlobalVar.iLastCommand3or7 = msgtype;
                if (content.ToLower().Trim() == "cp")
                {
                    GlobalVar.commandName = GlobalVar.CommandName.CP;
                    return;
                }
                else
                {
                    GlobalVar.commandName = GlobalVar.CommandName.NONE;
                }
                try
                {//处理PAT:A指令
                    if (mystring.left(content, 5).ToLower() == "sfc:0")
                    {
                        content = content.Replace("\n", "~");
                        if (content.IndexOf("@") > 0 || content.IndexOf("\\") > 0) content += "~@~i";
                    }
                }
                catch
                {
                }//PAT:A指令处理完毕
                if (content.ToLower().Trim() == "i" || content.ToLower().Trim() == "ig")
                {
                    EagleAPI.CLEARCMDLIST(0);
                    EagleAPI.CLEARCMDLIST(3);
                    EagleAPI.CLEARCMDLIST(7);
                }


                log.strSend = content;
                Thread lgfn = new Thread(new ThreadStart(log.submitFNpnr));
                lgfn.Start();


                //if (mystring.substring(content, 0, 4).ToLower() == "sfc:") content += "~pat:a~@";
                if ((content.ToLower().IndexOf("etdz") == 0 || content.ToLower().IndexOf("~etdz") >= 0) &&
                    ((GlobalVar.officeNumberCurrent == "全部配置" || GlobalVar.officeNumberCurrent == "") && GlobalVar.loginLC.configCount == 1))
                {
                    if(!GlobalVar .loginLC .bSameAllConfig )
                    { AppendEditorText(GlobalVar.Notice + "不能用全部配置出票，请先选择配置！\r>"); return; }
                }
                //EagleAPI.egNeedTransferSS(content);

#if !new
                try
                {
                    switch (streamctrl.send(content, msgtype))
                    {
                        case streamctrl_enum.TEST_ACCOUNT:
                            { AppendEditorText(GlobalVar.Notice + "测试帐号，无法ＥＴＤＺ\r>"); return; }
                        case streamctrl_enum.XEPNR_FAIL:
                            { AppendEditorText(GlobalVar.Notice + "您无权取消该PNR……需打开“取消他人PNR”功能\r>"); return; }
                        case streamctrl_enum.XE_FAIL:
                            { 
                                if(content.ToUpper().Contains("XEPNR"))
                                    AppendEditorText(GlobalVar.Notice + "您无权XE他人PNR项,若确认是您的PNR,请重试.\r>"); 
                                return; 
                            }
                        //case streamctrl_enum.NOT_ENOUGH_MONEY:
                        //    { AppendEditorText(GlobalVar.Notice + "您的帐户余额不足，不能订电子客票！\r>"); content = "i"; return; }

                        case streamctrl_enum.PRE_SUBMIT_FAIL:
                            { AppendEditorText(GlobalVar.Notice + "预提交失败，请重新操作！若没产生PNR，请先生成PNR！\r>"); content = "i"; break; }
                        case streamctrl_enum.HAS_NO_PNR:
                            { AppendEditorText(GlobalVar.Notice + "未发现PNR,请先生成PNR.勿用一次性出票操作！\r>"); content = "i"; break; };
                        case streamctrl_enum.NONE:
                            { content = log.strSend; break; }
                    }
                }
                catch
                {
                    throw new Exception("Error In Switch!");
                }
#else
            
            //projiam error creati handle
                #region xepnr操作权限控制
            {
                string t_xepnr = content.ToLower();
                if (t_xepnr.IndexOf("xepnr") == 0)
                {
                    string t_pnr = EagleAPI.etstatic.Pnr;
                    pnr_statistics ps = new pnr_statistics();
                    ps.pnr = t_pnr;
                    ps.state = "2";
                    if (!ps.submit())
                    {
                        AppendEditorText("您无权取消该PNR\r>");
                        return;
                    }
                }
            }
            #endregion

            if (EagleAPI.substring(content, 0, 4).ToLower() == "etdz")
            {
                GlobalVar.b_etdz = true;
            }
            GlobalVar.b_pat = (EagleAPI.substring(content, 0, 4).ToLower() == "pat:");

            GlobalVar.b_cmd_trfd_AM = ((EagleAPI.substring(content, 0, 7).ToLower() == "trfd:am") || (EagleAPI.substring(content, 0, 7).ToLower() == "trfd am"));
            GlobalVar.b_cmd_trfd_M = ((EagleAPI.substring(content, 0, 6).ToLower() == "trfd:m") || (EagleAPI.substring(content, 0, 6).ToLower() == "trfd m"));
            GlobalVar.b_cmd_trfd_L = ((EagleAPI.substring(content, 0, 6).ToLower() == "trfd:l") || (EagleAPI.substring(content, 0, 6).ToLower() == "trfd l"));

            if (EagleAPI.substring(content, 0, 4).ToLower() == "trfx")
            {
                content = content.Replace('*', (char)0x1A);
                content = content.Replace("/", "");
            }

            if (content == "i" || content == "ig")
            {
                GlobalVar.b_etdz = false;
                GlobalVar.b_enoughMoney = false;
                GlobalVar.b_endbook = false;
            }
            //if(EagleAPI.substring(content,0,1)=="@" || EagleAPI.substring(content,0,1)=="\\")//0926

                #region etdz金额不足控制
            if (GlobalVar.b_etdz)
            {
                if (float.Parse(GlobalVar.f_CurMoney) < GlobalVar.f_fc || float.Parse(GlobalVar.f_CurMoney) <= 100.0F)
                {
                    //余额不足
                    GlobalVar.b_etdz = false;
                    GlobalVar.b_enoughMoney = false;
                    GlobalVar.b_endbook = false;
                    AppendEditorText("\r警告：您的帐户余额不足，不能订电子客票！\r>");
                    content = "i";
                }
                else
                {
                    GlobalVar.b_enoughMoney = true;
                    content = content + "";
                    //初步提交，状态0
                    ePlus.eTicket.etPreSubmit etmp = new ePlus.eTicket.etPreSubmit();
                    if (!etmp.submitinfo())
                    {
                        AppendEditorText(GlobalVar.Notice + "\rPre-Submit Failed, Please Attach The Manager!\r>");
                        content = "i";
                    }
                }
            }
            #endregion

                #region rtxxxxx取当前操作的PNR
            if (EagleAPI.substring(content, 0, 2).ToLower() == "rt" && content.Length >= 7)
            {
                EagleAPI.CLEARCMDLIST();
                EagleAPI.etstatic.Pnr = mystring.right(content.Trim(), 5);
            }
            #endregion

            content = content.Replace((char)0x0A, (char)0x0D);//发送中换行\r转换成\n
            log.strSend = content;//同Command.SendString
            Command.SendString = content;
            GlobalVar.b_querryCommand = false;
            GlobalVar.b_otherCommand = false;

            //写入本地日志
            EagleAPI.LogWrite(content);
#endif
                streamctrl.UseTheCommandQueneListBegin(msgtype);
                StringBuilder data = new StringBuilder();
                string split_sign = "~";
                Qall.Add(content);
                int cLen = content.Length;
                Qsend.Clear();
                //if (!EagleAPI.b_onecmd)
                {
                    try
                    {
                        //0.1转换fd指令
                        av_sd avsd = new av_sd();
                        avsd.avstring = connect_4_Command.AV_String;
                        //content = avsd.fdi2fdxx(content);

                        #region//1.六个查询指令av,fv,sk,fd,ds,dsg#使用查询配置#######################################################################################################################
                        if (EagleString.Old.api.old_0(content)) //注意不区分大小写,二个字母的查询指令处理
                        {
                            data.Append("i" + split_sign);
                            Qsend.Add("i");
                            GlobalVar.b_querryCommand = true;
                            if (mystring.substring(content, 0, 2).ToLower() == "ds")//查询事务类特殊部分
                            {
                                if (mystring.substring(content, 0, 3).ToLower() == "dsg")//ds与dsg是两个不同的指令,dsg可能要依赖于rt
                                {

                                    int i = 2;//当前为倒数第二个
                                    if (Qall.Count >= 2)
                                    {
                                        while (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pn" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pb" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pf" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pl" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pg")//这里需要考虑rt后的翻页指令pn,pb,pf,pl,pg
                                        {
                                            i++;//找到非翻页指令,并得到其倒数第i个的位置
                                        }
                                        if (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "rt")//判断当前总表中倒数第二个指令是否为rt,在遇到用户输入rt时,对此表中的rt进行删减
                                        {
                                            for (int j = 0; j <= i - 2; j++)
                                            {
                                                Qquery.Add(Qall[Qall.Count - i + j]);//是则加入到Qquery中来
                                            }
                                        }
                                        else//否则程序自动先发一条rt指令,以防在订了航段后,dsg直接生效
                                        {
                                            //加入到Qquery列表;
                                            Qquery.Add("rt");
                                        }
                                    }
                                }
                            }
                            if (mystring.substring(content, 0, 2).ToLower() == "fd")//查询事务类特殊部分
                            {

                                {

                                    int i = 2;//当前为倒数第二个
                                    if (Qall.Count >= 2)
                                    {
                                        while (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pn" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pb" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pf" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pl" ||
                                            mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pg")//这里需要考虑rt后的翻页指令pn,pb,pf,pl,pg
                                        {
                                            i++;//找到非翻页指令,并得到其倒数第i个的位置
                                        }
                                        i--;
                                        //if (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "rt")//判断当前总表中倒数第二个指令是否为rt,在遇到用户输入rt时,对此表中的rt进行删减
                                        {
                                            for (int j = 0; j <= i - 2; j++)
                                            {
                                                Qquery.Add(Qall[Qall.Count - i + j]);//是则加入到Qquery中来
                                            }
                                        }
                                        //else//否则程序自动先发一条rt指令,以防在订了航段后,dsg直接生效
                                        {
                                            //加入到Qquery列表;
                                            //Qquery.Add("rt");
                                        }
                                    }
                                }
                            }
                            Qquery.Add(content);
                            //逐条发送Qquery中所有指令;
                            for (int t = 0; t < Qquery.Count; t++)
                            {
                                //Outputable.Enqueue(new OutputState(t + 1 == Qquery.Count,syncEvent));
                                //SendData(Qquery[t]);
                                data.Append(Qquery[t] + split_sign);
                                Qsend.Add(Qquery[t]);
                            }

                            //1.1以下对三个队列进行删减操作######################################################################################
                            if (content.Substring(0, 2).ToLower() == "av" && (content.Substring(cLen >= 5 ? 3 : 0, 2).ToLower() == "ra" || content.Substring(cLen >= 4 ? 2 : 0, 2).ToLower() == "ra"))//(若是av并且是返程指令)
                            {
                                //则不做删减操作,保留av;
                            }
                            else if (content.Substring(0, 2).ToLower() == "av")//(仅仅是普通的av)
                            {
                                //(可清空查询队列，及Qall中所有查询指令(不包括当前av))
                                for (int t = Qall.Count - 2; t >= 0; t--)
                                {
                                    if (Qall[t].Substring(0, Qall[t].Length >= 2 ? 2 : Qall[t].Length) == "av" ||
                                        Qall[t].Substring(0, Qall[t].Length >= 2 ? 2 : Qall[t].Length) == "fv" ||
                                        Qall[t].Substring(0, Qall[t].Length >= 2 ? 2 : Qall[t].Length) == "sk" ||
                                        Qall[t].Substring(0, Qall[t].Length >= 2 ? 2 : Qall[t].Length) == "fd" ||
                                        Qall[t].Substring(0, Qall[t].Length >= 2 ? 2 : Qall[t].Length) == "ds")
                                    //Qall[t].Substring(0,Qall[t].Length >= 2 ? 2 : Qall[t].Length) = "dsg" ||
                                    {
                                        Qall.Remove(Qall[t]);
                                    }
                                }
                                Qquery.Clear();
                                Qquery.Add(content);
                            }
                            else//否则,对于一般查询指令,对队列进行删减操作
                            {
                                if (Qquery.Count >= 2)
                                {
                                    int QL = Qquery.Count;
                                    for (int i = QL - 2; i >= 0; i--)//从后面删起,确保删除后队列的移动不影响i对应的值
                                    {
                                        if (Qquery[i].Length >= 2)
                                        {
                                            if (Qquery[i].Substring(0, 2).ToLower() == mystring.substring(content, 0, 2).ToLower() ||
                                                Qquery[i].Substring(0, 2).ToLower() == "pn" ||
                                                Qquery[i].Substring(0, 2).ToLower() == "pb" ||
                                                Qquery[i].Substring(0, 2).ToLower() == "pl" ||
                                                Qquery[i].Substring(0, 2).ToLower() == "pf" ||
                                                Qquery[i].Substring(0, 2).ToLower() == "pg")////////////////////////		(一).删除Qquery中的翻页及查询
                                                Qquery.Remove(Qquery[i]);//删除Qquery中除最后一个指令外(即不能删刚add的cmd)与cmd相同指令
                                        }
                                    }
                                }
                                //同理对Qall作删减操作;//这里对Qall中的查询指令得到了删减操作
                                //同时删去Qall中所有翻页指令;//(这里对Qall中的翻页指令得到了删减操作)pn,pb,pl.pf,pg
                                if (Qall.Count >= 2)
                                {
                                    int QL = Qall.Count;
                                    for (int i = QL - 2; i >= 0; i--)//从后面删起,确保删除后队列的移动不影响i对应的值
                                    {
                                        if (mystring.substring(Qall[i], 0, 2).ToLower() == content.Substring(0, 2).ToLower() ||//	(二)Qall中的查询指令
                                            mystring.substring(Qall[i], 0, 2).ToLower() == "pn" ||//								(三)Qall中的翻页指令
                                            mystring.substring(Qall[i], 0, 2).ToLower() == "pb" ||
                                            mystring.substring(Qall[i], 0, 2).ToLower() == "pl" ||
                                            mystring.substring(Qall[i], 0, 2).ToLower() == "pf" ||
                                            mystring.substring(Qall[i], 0, 2).ToLower() == "pg" ||
                                                (
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "av" &&				//	(四)Qall中的非事务指令
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "fv" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "sk" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "fd" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "ds" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "dsg" &&

                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "pn" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "pb" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "pl" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "pf" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "pg" &&

                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "rt" &&
                                                    Qall[i].Substring(0, content.Length >= 3 ? 3 : content.Length).ToLower() != "rrt" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "xe" &&
                                                    Qall[i].Substring(0, content.Length >= 2 ? 2 : content.Length).ToLower() != "xn" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "nm" &&
                                                    Qall[i].Substring(0, content.Length >= 2 ? 2 : content.Length).ToLower() != "gn" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "ss" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "sfc" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "sd" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "sa" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "sn" &&
                                                    Qall[i].Substring(0, content.Length >= 2 ? 2 : content.Length).ToLower() != "st" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "ct" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "c:" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "cy" &&
                                                    Qall[i].Substring(0, content.Length >= 2 ? 2 : content.Length).ToLower() != "ei" &&

                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "tk" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "t:" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "ma" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "op" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "ba" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "cs" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "es" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "sp" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "ni" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "fc" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "fn" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "fp" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "tc" &&
                                                    mystring.substring(Qall[i], 0, 2).ToLower() != "hb" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "osi" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "sel" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "qte" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "ssr" &&
                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "pat" &&

                                                    mystring.substring(Qall[i], 0, 3).ToLower() != "rmk" &&
                                                    Qall[i].Substring(0, Qall[i].Length >= 4 ? 4 : Qall[i].Length).ToLower() != "etdz" &&
                                                    Qall[i].Substring(0, Qall[i].Length >= 4 ? 4 : Qall[i].Length).ToLower() != "etry" &&


                                                    Qall[i].Substring(0, Qall[i].Length >= 1 ? 1 : Qall[i].Length) != "@" &&
                                                    Qall[i].Substring(0, Qall[i].Length >= 1 ? 1 : Qall[i].Length) != "\\"





                                                )
                                            )
                                            Qall.Remove(Qall[i]);
                                    }
                                }
                                //同时在这里对Qquery中的翻页指令作删减操作;//因为rt的交叉事务可能加入翻页指令
                            }
                        }
                        #endregion

                        #region//2.订座类#########################使用订座配置######################################################################################################################
                        else if (
                            content.ToLower().StartsWith("nfd")||
                            content.ToLower().StartsWith("nfr")||
                            content.ToLower().StartsWith("nfn")||

                            mystring.substring(content, 0, 2).ToLower() == "c:" ||
                            mystring.substring(content, 0, 2).ToLower() == "cy" ||
                            (mystring.substring(content, 0, 2).ToLower() == "rt" && !content.ToLower().StartsWith("rtu") && !content.ToLower().StartsWith("rtc")) && !content.ToLower().StartsWith("rtn") ||
                            mystring.substring(content, 0, 3).ToLower() == "rrt" ||
                            mystring.substring(content, 0, 2).ToLower() == "xe" ||
                            mystring.substring(content, 0, 2).ToLower() == "xn" ||
                            mystring.substring(content, 0, 2).ToLower() == "nm" ||
                            mystring.substring(content, 0, 2).ToLower() == "gn" ||
                            mystring.substring(content, 0, 2).ToLower() == "ss" ||
                            mystring.substring(content, 0, 3).ToLower() == "sfc" ||
                            mystring.substring(content, 0, 2).ToLower() == "sd" ||
                            mystring.substring(content, 0, 2).ToLower() == "sa" ||
                            mystring.substring(content, 0, 2).ToLower() == "sn" ||
                            mystring.substring(content, 0, 2).ToLower() == "st" ||
                            mystring.substring(content, 0, 2).ToLower() == "ct" ||
                            mystring.substring(content, 0, 2).ToLower() == "ei" ||

                            mystring.substring(content, 0, 2).ToLower() == "ma" ||
                            mystring.substring(content, 0, 2).ToLower() == "op" ||
                            mystring.substring(content, 0, 2).ToLower() == "ba" ||
                            mystring.substring(content, 0, 2).ToLower() == "cs" ||
                            mystring.substring(content, 0, 2).ToLower() == "es" ||
                            mystring.substring(content, 0, 2).ToLower() == "sp" ||
                            mystring.substring(content, 0, 2).ToLower() == "ni" ||
                            mystring.substring(content, 0, 2).ToLower() == "fc" ||
                            mystring.substring(content, 0, 2).ToLower() == "fn" ||
                            mystring.substring(content, 0, 2).ToLower() == "fp" ||
                            mystring.substring(content, 0, 2).ToLower() == "hb" ||
                            mystring.substring(content, 0, 2).ToLower() == "tc" ||
                            mystring.substring(content, 0, 2).ToLower() == "tk" ||
                            mystring.substring(content, 0, 2).ToLower() == "t:" ||
                            mystring.substring(content, 0, 3).ToLower() == "osi" ||
                            mystring.substring(content, 0, 3).ToLower() == "sel" ||
                            mystring.substring(content, 0, 3).ToLower() == "qte" ||
                            mystring.substring(content, 0, 3).ToLower() == "ssr" ||
                            mystring.substring(content, 0, 3).ToLower() == "rmk" ||
                            mystring.substring(content, 0, 3).ToLower() == "pat" ||
                            mystring.substring(content, 0, 4).ToLower() == "etdz" ||
                            mystring.substring(content, 0, 4).ToLower() == "etry" ||
                            mystring.substring(content, 0, 1).ToLower() == "@" ||
                            mystring.substring(content, 0, 1).ToLower() == "\\" ||
                            (content[0] > '0' && content[0] <= '9')
                            )
                        {
                            //先发送ig指令;
                            //Outputable.Enqueue(new OutputState(false, syncEvent));
                            //SendData("ig");
                            data.Append("i" + split_sign);
                            Qsend.Add("i");
                            if (mystring.substring(content, 0, 2).ToLower() == "rt")
                            {
                                int QL = Qquery.Count;
                                for (int i = QL - 1; i >= 0; i--)//从最后面开始删起,以确保删除后队列的前移而不改变i对应的值
                                {
                                    if (Qquery[i].Substring(0, Qquery[i].Length >= 2 ? 2 : Qquery[i].Length).ToLower() == "rt") Qquery.Remove(Qquery[i]);//如上所述,遇到rt对Qquery中的rt删减
                                }
                            }
                            //处理特殊指令hb,sd
                            if (EagleAPI.substring(content, 0, 2).ToLower() == "hb"
                                || EagleAPI.substring(content, 0, 2).ToLower() == "xe")//hb依赖ha指令,如果有翻页指令,继续向前找.找到后回滚翻页指令
                            {
                                int i = 2;//当前为倒数第二个
                                if (Qall.Count >= 2)
                                {
                                    while (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pn" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pb" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pf" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pl" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pg")//翻页指令pn,pb,pf,pl,pg
                                    {
                                        if (EagleAPI.substring(content, 0, 2).ToLower() == "xe")
                                            Qorder.Add(Qall[Qall.Count - i]);//xe时要把pg加入
                                        i++;//找到非翻页指令,并得到其倒数第i个的位置
                                    }
                                    if (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "ha")//若为ha,ha到hb之间的指令加到Qorder中
                                    {
                                        for (int j = 0; j <= i - 2; j++)
                                        {
                                            Qorder.Add(Qall[Qall.Count - i + j]);
                                        }
                                    }
                                }
                            }
                            #region sd指令可取消
                            if (EagleAPI.substring(content, 0, 2) == "sd")//sd指令依赖于之前的航班查询指令,如果有翻页指令,继续向前找.找到后回滚翻页指令
                            {//需要考虑翻页指令
                                int i = 2;//当前为倒数第二个
                                if (Qall.Count >= 2)
                                {
                                    while (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pn" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pb" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pf" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pl" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "pg")//翻页指令pn,pb,pf,pl,pg
                                    {
                                        i++;//找到非翻页指令,并得到其倒数第i个的位置
                                    }
                                    if (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "fv" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "sk" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "ds" ||
                                        mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "av")//sd会依赖于dsg吗？？？&&
                                    // Qall[Qall.Count - 2].Substring(0, Qall[Qall.Count - i].Length >= 3 ? 3 : Qall[Qall.Count - i].Length).ToLower() != "dsg")
                                    {
                                        if (mystring.substring(Qall[Qall.Count - i], 0, 2).ToLower() == "av")//若是av指令,要考虑返程
                                        {
                                            for (int k = 0; k < Qquery.Count - 1; k++)//将Qquery队列中的av指令加入Qorder队列,最后一个av在下面的循环中加
                                            {
                                                if (Qquery[k].Substring(0, 2).ToLower() == "av")//Qquery中的命令不可能小于2个字节
                                                {
                                                    Qorder.Add(Qquery[k]);
                                                }
                                            }

                                        }
                                        for (int j = 0; j <= i - 2; j++)//加入到Qorder中,包括翻页指令
                                        {
                                            Qorder.Add(Qall[Qall.Count - i + j]);
                                        }
                                    }
                                }
                            }
                            #endregion
                            Qorder.Add(content);	//将当前订座指令加到队列中
                            //逐条发送Qorder中所有指令;//考虑是否可以同时发送Qorder中所有指令,每条指令以0x0d隔开,订票指令可以作为组指令同时发送.但中间交叉了查询指令则不行
                            //取最后一条指令返回结果;
                            for (int t = 0; t < Qorder.Count; t++)
                            {

                                //Outputable.Enqueue(new OutputState(t + 1 == Qorder.Count, syncEvent));
                                //SendData(Qorder[t]);
                                data.Append(Qorder[t] + split_sign);
                                Qsend.Add(Qorder[t]);
                            }
                            //若有封口指令则清空三个列表
                            if (content.Contains("@") || content.Contains("\\"))
                            {
                                Qorder.Clear();
                                Qquery.Clear();
                                Qall.Clear();
                            }
                        }
                        #endregion

                        #region//3.订坐类的ig指令#################使用当前配置#############################################################################################################################
                        else if (content.ToLower() == "ig" || content.ToLower() == "i") //IG指令
                        {
                            Qall.Clear();
                            Qorder.Clear();
                            Qquery.Clear();
                            //Outputable.Enqueue(new OutputState(true, syncEvent));
                            //SendData("ig");
                            data.Append("i" + split_sign + "i" + split_sign);
                            Qsend.Add("i");
                        }
                        #endregion

                        #region//4.订坐类的@,\指令############################################################################################################################################
                        else if (content.Substring(0, 1).ToLower() == "@" || content.Substring(0, 1).ToLower() == "\\") //封口指令
                        {
                            //处理Qall(),删去所有订坐类事务指令,这里对Qall中的订座指令得到了删减操作
                            for (int i = 0; i < Qorder.Count; i++)//遍历Qorder队列
                            {
                                for (int j = 0; j < Qall.Count; j++)//遍历Qall队列
                                {
                                    if (Qall[j] == Qorder[i])
                                    {
                                        Qall.Remove(Qall[j]);													//	(六)同五,删Qall中的订坐指令
                                        break;
                                    }
                                }
                            }
                            Qorder.Add(content);
                            //发送IG指令;
                            //Outputable.Enqueue(new OutputState(false, syncEvent));
                            //SendData("ig");
                            data.Append("i" + split_sign);
                            Qsend.Add("i");

                            //逐条发送Qorder中所有指令;
                            //取最后一条指令返回结果;
                            for (int t = 0; t < Qorder.Count; t++)
                            {
                                //Outputable.Enqueue(new OutputState(t + 1 == Qorder.Count, syncEvent));
                                //SendData(Qorder[t]);
                                data.Append(Qorder[t] + split_sign);
                                Qsend.Add(Qorder[t]);
                            }
                            Qorder.Clear();
                            Qquery.Clear();
                            Qall.Clear();
                            //bool sd = false;
                            //bool nm = false;
                            //bool tk = false;
                            //for (int ii = 0; ii < Qorder.Count; ii++)
                            //{
                            //    if (Qorder[ii].Length < 2) continue;
                            //    if (Qorder[ii].Substring(0, 2) == "sd" || Qorder[ii].Substring(0, 2) == "ss")
                            //    {
                            //        sd = true;
                            //        break;
                            //    }
                            //}
                            //for (int ii = 0; ii < Qorder.Count; ii++)
                            //{
                            //    if (Qorder[ii].Length < 2) continue;
                            //    if (Qorder[ii].Substring(0, 2) == "nm")
                            //    {
                            //        nm = true;
                            //        break;
                            //    }
                            //}
                            //for (int ii = 0; ii < Qorder.Count; ii++)
                            //{
                            //    if (Qorder[ii].Length < 2) continue;
                            //    if (Qorder[ii].Substring(0, 2) == "tk")
                            //    {
                            //        tk = true;
                            //        break;
                            //    }
                            //}
                            //if (sd && nm && tk)//如果封口成功，清列表
                            //{
                            //    Qorder.Clear();
                            //    Qquery.Clear();
                            //    Qall.Clear();
                            //}
                            //bool rt = false;
                            //for (int ii = 0; ii < Qorder.Count; ii++)
                            //{
                            //    if (Qorder[ii].Length < 2) continue;
                            //    if (Qorder[ii].Substring(0, 2) == "rT")
                            //    {
                            //        rt = true;
                            //        break;
                            //    }
                            //}
                            //if (rt)//如果封口成功，清列表
                            //{
                            //    for (int ii = 0; ii < Qorder.Count; ii++)
                            //    {
                            //        if ((Qorder[ii][0] > '0' && Qorder[ii][0] <= '9') || (Qorder[ii].Length>=2 && Qorder[ii].Substring(0, 2) == "xe"))
                            //        {
                            //            Qorder.Clear();
                            //            Qquery.Clear();
                            //            Qall.Clear();
                            //            break;
                            //        }
                            //    }
                            //}

                        }
                        #endregion

                        #region//5.翻页类指令五个pn,pb,pf,pl,pg###使用当前配置###############################################################################################################################
                        else if (mystring.substring(content, 0, 2).ToLower() == "pn" ||
                                mystring.substring(content, 0, 2).ToLower() == "pb" ||
                                mystring.substring(content, 0, 2).ToLower() == "pf" ||
                                mystring.substring(content, 0, 2).ToLower() == "pl" ||
                                mystring.substring(content, 0, 2).ToLower() == "pg" ||
                                content.ToLower().StartsWith("rtc") ||
                            content.ToLower().StartsWith("rtu") ||
                            content.ToLower().StartsWith("rtn") 
                            )//翻页指令,从最后面一条非翻页指令开始发送
                        {
                            data.Append("i" + split_sign);
                            Qsend.Add("i");
                            int i = 0;
                            if (Qall.Count >= 2)
                            {
                                for (i = Qall.Count - 2; i >= 0; i--)
                                {
                                    if (mystring.substring(Qall[i], 0, 2).ToLower() != "pn" &&
                                        mystring.substring(Qall[i], 0, 2).ToLower() != "pb" &&
                                        mystring.substring(Qall[i], 0, 2).ToLower() != "pf" &&
                                        mystring.substring(Qall[i], 0, 2).ToLower() != "pl" &&
                                        mystring.substring(Qall[i], 0, 2).ToLower() != "pg" &&
                                        Qall[i].ToLower() != "rtu1" &&
                                        Qall[i].ToLower() != "rtn" &&
                                        Qall[i].ToLower() !="rtc")
                                    {
                                        break;//得到i为最后一条非翻页指令的位置
                                    }
                                }
                                if (i > 0)
                                {
                                    if (mystring.substring(Qall[i], 0, 2).ToLower() == "av")//考虑返程票查询,最后一条非翻页指令为av
                                    {
                                        //把Qquery队列中除最后一条av外前面所有av指令发送一次;//同label1的for
                                        for (int k = 0; k < Qquery.Count - 1; k++)//将Qquery队列中的av指令加入Qorder队列,最后一个av在下面的循环中加
                                        {
                                            if (Qquery[k].Substring(0, 2).ToLower() == "av")
                                            {
                                                //Outputable.Enqueue(new OutputState(false, syncEvent));
                                                //SendData(Qquery[k]);
                                                data.Append(Qquery[k] + split_sign);
                                                Qsend.Add(Qquery[k]);
                                            }
                                        }
                                    }
                                    else//考虑Qall[i]指令可能依赖Qall[i-1]
                                    {
                                        if (i - 1 >= 0)
                                        {
                                            //发送Qall[i-1];//先发送Qall[i]前的指令,以防不测//这里Qall[i - 1]只能为查询类可能被依赖的指令2006.4.25
                                            //Outputable.Enqueue(new OutputState(false, syncEvent));
                                            if (Qall[i - 1].Length >= 2)
                                            {
                                                if (Qall[i - 1].Substring(0, 2) == "av")//同上面要考虑返程
                                                {
                                                    for (int k = 0; k < Qquery.Count - 1; k++)//将Qquery队列中的av指令加入Qorder队列,最后一个av在下面的循环中加
                                                    {
                                                        if (Qquery[k].Substring(0, 2).ToLower() == "av")
                                                        {
                                                            //Outputable.Enqueue(new OutputState(false, syncEvent));
                                                            //SendData(Qquery[k]);
                                                            data.Append(Qquery[k] + split_sign);
                                                            Qsend.Add(Qquery[k]);
                                                        }
                                                    }
                                                }
                                            }
                                            //							SendData(Qall[i - 1]);//这里Qall[i - 1]只能为查询类可能被依赖的指令2006.4.25
                                        }
                                    }
                                    //最后一条非翻页指令为ha或rt
                                    if (Qall[i].Substring(0, 2) == "ha" || (Qall[i].Substring(0, 2) == "rt" && Qall[i].Length >= 7))
                                    {
                                        //Outputable.Enqueue(new OutputState(false, syncEvent));
                                        //SendData(Qall[i]);
                                        data.Append(Qall[i] + split_sign);
                                        Qsend.Add(Qall[i]);
                                    }
                                    //最后一条非翻页指令为rt时
                                    if (Qall[i].Substring(0, 2) == "rt" && Qall[i].Length >= 7)
                                    {
                                        Qorder.Add(content);
                                    }
                                }

                            }
                            if (i < 0) i = 0;
                            for (int j = i; j < Qall.Count; j++)
                            {
                                //发送Qall[j];//发送Qall[i]及后面的翻页指令
                                //Outputable.Enqueue(new OutputState(j + 1 == Qall.Count, syncEvent));
                                //SendData(Qall[j]);
                                data.Append(Qall[j] + split_sign);
                                Qsend.Add(Qall[j]);
                            }
                        }
                        #endregion

                        #region//6.其它指令,包括错误指令,全部视为无事务指令###使用查询配置################################################################################################################
                        else //
                        {
                            data.Append("i" + split_sign);
                            Qsend.Add("i");
                            GlobalVar.b_otherCommand = true;
                            //发送Qall中的最后一条指令;
                            //取得指令返回结果;
                            //Outputable.Enqueue(new OutputState(true, syncEvent));
                            //SendData(Qall[Qall.Count - 1]);
                            data.Append(Qall[Qall.Count - 1] + split_sign);
                            Qsend.Add(Qall[Qall.Count - 1]);
                        }
                        #endregion

                        streamctrl.UseTheCommandQueneListEnd(msgtype);


                    }
                    catch
                    {
                        streamctrl.cmdListOperating = false;
                        AppendEditorText(GlobalVar.Notice + "错误号：WindowSwitch-100000");
                        return;
                    }
                }
                string newtemp = EagleAPI.GetCurrentStrings2Send(content, lsNewCmdStrings);
#if !NewProtocol
            SendData(data.ToString());  
#else
                string sendstring = "";
                if (!EagleAPI.b_onecmd)
                {
                    sendstring = newtemp;
                    //sendstring = data.ToString();
                    //sendstring = sendstring.Substring(0, sendstring.Length-1);
                }
                else sendstring = content.Trim();
                #region 发送指令
                {
                    int outlength = 0;
                    EagleProtocol ep = new EagleProtocol();
                    ep.MsgType = (ushort)msgtype;//3




                    //给ep.cmdstring与ep.MsgBody赋值
                    if (content.ToLower() == "pn" && !EagleAPI.b_onecmd)
                        ep.cmdstring = ep.MsgBody = streamctrl.OptimizeCommandStrings(newtemp);
                    else
                    {
                        if (GlobalVar.commandSendtype == GlobalVar.CommandSendType.A)
                            ep.cmdstring = ep.MsgBody = streamctrl.OptimizeCommandStrings(sendstring);
                        if (GlobalVar.commandSendtype == GlobalVar.CommandSendType.B)
                            ep.cmdstring = ep.MsgBody = streamctrl.OptimizeCommandStrings(newtemp);
                        if (GlobalVar.commandSendtype == GlobalVar.CommandSendType.Fast)
                            ep.cmdstring = ep.MsgBody = streamctrl.OptimizeCommandStrings(sendstring);//同A
                    }
                    try
                    {
                        string TempClaw = ep.cmdstring;

                        string[] TempA = TempClaw.Split('~');
                        List<string> TempLS = new List<string>();
                        for (int i = 0; i < TempA.Length; i++)
                        {
                            if (TempLS.Count == 0)
                            {
                                TempLS.Add(TempA[i]);
                            }
                            else
                            {
                                if (TempA[i].ToLower() == TempLS[TempLS.Count - 1].ToLower() &&
    (TempA[i].ToLower().IndexOf("ss") == 0 || TempA[i].ToLower().IndexOf("sd") == 0))
                                {
                                    continue;
                                }
                                else
                                {
                                    TempLS.Add(TempA[i]);
                                }

                            }
                        }
                        TempClaw = string.Join("~", TempLS.ToArray());
                        EagleString.EagleFileIO.LogWrite("SENDER: " + TempClaw);
                        ep.cmdstring = TempClaw;
                    }
                    catch
                    {
                    }
                    curSendString = ep.cmdstring;
                    #region 使用IBE

                    if (BookTicket.bIbe && !GlobalVar.bUsingConfigLonely)
                    {//可使用线程
                        try
                        {
                            ep.cmdstring = string.Join("~", EagleString.Old.api.EtermCommandGroupOptimize(ep.cmdstring));
                            if (ibb.avpCan(ep.cmdstring))//该指令串能用IBE查询吗？
                            {
                                ibethreadarg = ep.cmdstring;
                                Thread thIbe;
                                if(GlobalVar .gbIsNkgFunctions )
                                    thIbe = new Thread(new ThreadStart(threadIbeInvoke));
                                else thIbe = new Thread(new ThreadStart(threadIbe));
                                thIbe.Start();
                                return;
                                
                            }
                            
                        }
                        catch
                        {
                        }
                        try
                        {
                            if (content.ToLower().Substring(0, 2) == "fd")
                            {
                                string sfrom = "";
                                string sto = "";
                                string sdate = "";
                                string sairline = "";
                                string fdstring = content;
                                if (content.Length < 5)//fdn
                                {
                                    av_sd fd2fdx = new av_sd();
                                    fd2fdx.avstring = connect_4_Command.AV_String;
                                    fdstring = fd2fdx.fdi2fdxx(content);
                                    fdstring = fdstring.Substring(2).Trim();
                                    if (fdstring[0] == ':') fdstring = fdstring.Substring(1).Trim();
                                    string[] fdarray = fdstring.Split('/');
                                    sfrom = fdarray[0].Substring(0, 3);
                                    sto = fdarray[0].Substring(3, 3);
                                    sdate = fdarray[1];
                                    sairline = fdarray[2];

                                }
                                else//fdxxxxxx/xx/xx
                                {
                                    fdstring = fdstring.Substring(2).Trim();
                                    if (fdstring[0] == ':') fdstring = fdstring.Substring(1).Trim();
                                    string[] fdarray = fdstring.Split('/');
                                    if (fdarray.Length >= 1)//fdorgdst or fd dst
                                    {
                                        if (fdarray[0].Length < 6) fdarray[0] = GlobalVar.LocalCityCode + fdarray[0];
                                        sfrom = fdarray[0].Substring(0, 3);
                                        sto = fdarray[0].Substring(3, 3);
                                        sdate = DateTime.Now.ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                        sairline = "ALL";

                                    }
                                    if (fdarray.Length >= 2)
                                    {
                                        if (fdarray[1].Length == 2) sairline = fdarray[1];
                                        if (fdarray[1].Length == 1)
                                        {
                                            if (fdarray[1] == ".") sdate = DateTime.Now.ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                            if (fdarray[1] == "+") sdate = DateTime.Now.AddDays(1).ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                            if (fdarray[1] == "-") sdate = DateTime.Now.AddDays(-1).ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                        }
                                        if (fdarray[1].Length == 4)
                                            sdate = DateTime.ParseExact(fdarray[1], "dMMM", GlobalVar2.gbDtfi).ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                        if (fdarray[1].Length == 5)
                                            sdate = DateTime.ParseExact(fdarray[1], "ddMMM", GlobalVar2.gbDtfi).ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                        if (fdarray[1].Length == 6)
                                            sdate = DateTime.ParseExact(fdarray[1], "dMMMyy", GlobalVar2.gbDtfi).ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                        if (fdarray[1].Length == 7)
                                            sdate = DateTime.ParseExact(fdarray[1], "ddMMMyy", GlobalVar2.gbDtfi).ToString("ddMMMyy", GlobalVar2.gbDtfi);
                                    }
                                    if (fdarray.Length >= 3)
                                    {
                                        if (fdarray[2].Length == 2) sairline = fdarray[2];
                                    }
                                }
                                Options.ibe.ibeInterface ii = new Options.ibe.ibeInterface();
                                string fdres = "";
                                string year = DateTime.Now.ToString("yy", GlobalVar2.gbDtfi);
                                fdres = ii.fd2(sfrom, sto, sdate.Substring(0, 5) + year, sairline);
                                if (fdres == "") throw new Exception("IBE返回空,自动使用配置执行操作");
                                else
                                {
                                    AppendEditorText("IBE返回\r" + fdres + "\r>");
                                    return;
                                }
                            }
                            else if (content.ToLower().StartsWith("sk"))
                            {
                                EagleString.SkCommand skcmd = new EagleString.SkCommand(content);
                                EagleWebService.IbeFunc ibefunc = new EagleWebService.IbeFunc();
                                string skres = ibefunc.SK(skcmd.m_city1, skcmd.m_city2, skcmd.m_date, skcmd.m_airline);
                                AppendEditorText("IBE返回\r" + skres + "\r>");
                                return;
                            }
                        }
                        catch
                        {
                            AppendEditorText("\rIBE查询失败，自动使用配置查询!\r");
                        }
                    }
                    else
                    {
                        connect_4_Command.AV_String = "";
                    }
                    #endregion 使用IBE
                    //独占配置时，只发送当前指令即可
                    if (GlobalVar.bUsingConfigLonely)
                        ep.cmdstring = ep.MsgBody = content;
                    if (msgtype == 3)//为0x0003时，把当前指令加入，并启动0003定时器
                    {
                        GlobalVar.CurrentSendCommands = ep.cmdstring;
                        timer0003.Stop();
                        timer0003.Start();
                    }
                    else if (msgtype == 7)//为0x0007时，把当前指令加入GlobalVar.CurrentSendCommands0007，并启动0007定时器
                    {
                        GlobalVar.CurrentSendCommands0007 = ep.cmdstring;
                        timer0007.Stop();
                        timer0007.Start();
                    }
                    EagleAPI.LogWrite("指令类型" + msgtype.ToString() + "\r\n" + ep.cmdstring);
                    if ((!GlobalVar.bUsingConfigLonely) && (!streamctrl.IsEtdzStartWithRtCommand(ep.cmdstring)))
                    {
                        //AppendEditorText(GlobalVar.Notice + "请先产生PNR，再进行出票\r\n>");
                        //return;
                    }
                   
                
                    /////////////////////////////////新扣款//////////////////////////////////
                    #region 新扣款开始
                    //string eptemp = ep.cmdstring.ToLower();
                    //bool tempetdz = false;
                    //if (!GlobalVar.bUsingConfigLonely)
                    //{
                    //    if (eptemp.IndexOf("rt") > -1 && eptemp.IndexOf("~etdz") > -1 && !GlobalVar.b_netdz)//判断是不是出票指令
                    //    {
                    //        tempetdz = true;
                    //    }
                    //    else
                    //    {
                    //        tempetdz = false;
                    //    }
                    //}
                    //else
                    //{
                    //    if (eptemp.IndexOf("etdz") > -1 && !GlobalVar.b_netdz)
                    //    {
                    //        tempetdz = true;
                    //    }
                    //    else
                    //    {
                    //        tempetdz = false;
                    //    }
                    //}
                    //int iDecType = 0;
                    //try
                    //{
                    //    NewPara np = new NewPara(GlobalVar.loginXml);
                    //    string strDecType = np.FindTextByPath("//eg/DecFeeType");
                    //    iDecType=Convert.ToInt16(Convert.ToDouble(strDecType));
                    //}
                    //catch
                    //{
                    //}
                    //if (tempetdz&&iDecType==2)//判断是不是出票指令&&要不要先扣款2为先扣款，3为后扣款
                    //{
                    //    try
                    //    {
                    //        newdefee = new NewDecFee2(ep.cmdstring);
                    //        ep.cmdstring = "";
                    //        ep.MsgBody = "";
                    //        return;
                    //    }
                    //    catch (Exception ea)
                    //    {
                    //        AppendEditorText(ea.Message);//显示取PNR遇到的错误
                    //    }
                    //}
                    //if (GlobalVar.b_netdz&&GlobalVar.b_NotDoDec)
                    //{
                    //    GlobalVar.b_netdz = false;
                    //    if (newdefee != null)
                    //        AppendEditorText(newdefee.strProResult);//显示扣款信息
                    //}
                    #endregion                    
                
                    /////////////////////////////////新扣款//////////////////////////////////
                    if (GlobalVar.b_test) AppendEditorText("\r" + ep.cmdstring + "\r");


                    GlobalVar.b_UseSpecifiedConfig = streamctrl.IsCommandHasRelationWithConfig(ep.cmdstring);
                    if (GlobalVar.b_UseSpecifiedConfig)//是否使用轮询配置，而置指令类型
                        ep.cmdType = 1;//不轮询用1(原不轮询)

                    else
                        ep.cmdType = 3;//轮询用3

                    //if (GlobalVar.b_UseSpecified强制) 
                    ep.cmdType = 1;//强制不轮询，指定！

                    string[] arr = EagleString.Old.api.EtermCommandGroupOptimize(ep.cmdstring);
                    ep.cmdstring = string.Join("~", arr);
                    ep.SetMsgLength();

                    if (GlobalVar.commandSendtype == GlobalVar.CommandSendType.Fast //快速
                        && !GlobalVar.bUsingConfigLonely//共享
                        && !streamctrl.MemoryOperation(ref GlobalVar.strRtPnrResult, content))//内存处理
                    {
                        GlobalVar.bSendTypeFastFunction = true;
                        AppendEditorText("\r\n" + GlobalVar.strRtPnrResult + "\n>");
                    }
                    else
                    {
                        EagleAPI.LogWrite("\r\n<eagle66>"+ep.MsgType+ep.cmdType+"\r\n");
                        streamctrl.limitCommandNumber(ep.cmdstring);
                        GlobalVar.bSendTypeFastFunction = false;
                        char[] sendstring1 = ep.ConvertToString(out outlength);
                        SendData(sendstring1, outlength);
                        //EagleString.EagleFileIO.TrafficAmount(content.ToLower().Contains("etdz") ? 2 : 1, arr.Length);
                    }
                }
                #endregion
#if !new
                streamctrl.send_end(content, msgtype);
#else
            //发送完毕，增加控制变量
            //GlobalVar.b_pnCommand = (EagleAPI.substring(content, 0, 2).ToUpper() == "PN");
            //if (Command.Qorder.Count < 1) GlobalVar.b_rtCommand = false;
            //else
            //    GlobalVar.b_rtCommand = (EagleAPI.substring(Qorder[Qorder.Count - 1], 0, 2).ToUpper() == "rt");
            //if (GlobalVar.b_etdz && EagleAPI.etstatic.Pnr.Trim().Length == 5)
            //{
            //    EagleAPI.CLEARCMDLIST();
            //    GlobalVar.b_etdz = false;
            //    GlobalVar.b_etdz_A = true;
            //}
#endif
#endif

            }
        }
        //创建委托
        private delegate void myDelegateShowTableTicket();
        //创建委托对像
        private myDelegateShowTableTicket delegateShowTT;
        //在函数中实例化委托对像并指定调用方法
        private System.Timers.Timer timer0003 = null;
        private System.Timers.Timer timer0007 = null;
        private string ibethreadarg = "";
        private string curSendString = "";//保存当前发送串，用于IBE失败时，自动转换到配置中执行

        void threadIbeInvoke()
        {
            m_Panel.Invoke(delegateShowTT,null);
        }

        void threadIbe()
        {
            EagleString.EagleFileIO.LogWrite("使用IBE查询");
            string ret = ibb.avp(ibethreadarg);
            if (ret != "")
            {
                string tempret = ret;
                {
                    connect_4_Command.AV_String = ret.Replace("\n", "\r\n");
                    if (connect_4_Command.AV_String.IndexOf("\r\n") == 0) connect_4_Command.AV_String = connect_4_Command.AV_String.Substring(2);
                    //散拼
                    //if ((Model.md.b_008 || Model.md.b_018 || Model.md.b_028 || Model.md.b_038 || Model.md.b_048) && GlobalVar.IsListGroupTicket)
                    //{
                    //    PassengerToGroup tg = new PassengerToGroup();
                    //    tg.avstring = log.strSend;
                    //    Thread th = new Thread(new ThreadStart(tg.execute));

                    //    th.Start();
                    //}
                }
                //这２句必须必在后面，因为fromto是在PassengerToGroup里计算的
                
                EagleString.EagleFileIO.LogWrite("IBE结果：");
                AppendEditorText(tempret + "\r>");
                //GlobalVar2.bookTicket.setToBooktktListView(Command.AV_String, GlobalVar2.gbFromto);
                {//下面一段位置必须放在最后面
                    string rev2 = tempret;
                    System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection();
                    ListView lview = (ListView)m_lvLowest;
                    int price = 0;
                    int distance = 0;
                    //显示到最低价与返点列表
                    if (lview.InvokeRequired)
                    {
                        lvLowestClear dg = new lvLowestClear(lview.Items.Clear);
                        lview.Invoke(dg);
                    }
                    else
                    {
                        lview.Items.Clear();
                    }
                    EagleExtension.EagleExtension.AvResultToListView_Lowest(rev2, cn, "", GlobalVar.WebServer, lview, GlobalVar.loginName
                        , ref price, ref distance);
                    lview = (ListView)m_lvGroup;
                    //显示散拼列表
                    try
                    {
                        if (lview.InvokeRequired)
                        {
                            listGroupResult lgr = EagleExtension.EagleExtension.GroupResultToListView_Group;
                            lgr.Invoke(GlobalVar.loginName, 'A', GlobalVar.WebServer, rev2, lview);
                        }
                        else
                        {
                            EagleExtension.EagleExtension.GroupResultToListView_Group(GlobalVar.loginName, 'A', GlobalVar.WebServer, rev2, lview);
                        }
                    }
                    catch
                    {
                    }
                    lview = (ListView)m_lvSpec;
                    ListView lview2 = (ListView)m_lvSpec2;
                    //显示固定与浮动列表
                    EagleExtension.EagleExtension.SpecTickResultToListView_Spec(rev2, GlobalVar.WebServer, lview, lview2, price);
                }
                showTableTicket();
                return;
            }
            else
            {
                AppendEditorText("IBE提示:可能缺少日期,自动切换到配置中执行！" + "\r>");
                BookTicket.bIbe = false;
                EagleAPI.EagleSendOneCmd(curSendString);
            }
        }
        delegate void listGroupResult(string s, char s2, string s3, string s4, ListView lv);
        Button button = null;
        
        //显示右窗体
        void showTableTicket()
        {
            try
            {

                EasyPrice.tt = new ePlus.Data.TableTicket();
                EasyPrice.tt.TopLevel = false;
                m_Panel.Controls.Clear();
                m_Panel.Controls.Add(EasyPrice.tt);
                EasyPrice.tt.Dock = DockStyle.Fill;
                EasyPrice.avDS.FromTo = GlobalVar2.gbFromto;
                EasyPrice.tt.initListWithDs(EasyPrice.avDS, -1);
                EasyPrice.tt.Show();
                button = EasyPrice.tt.btnSubmitOrderWithPnr;
                button.Click += new EventHandler(button_Click);
            }
            catch (Exception exx)
            {
                //AppendEditorText("showTableTicket" + exx.Message + "\r>");
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            BookSimple.SubmitPnr dlg = new ePlus.BookSimple.SubmitPnr();
            dlg.Size = new System.Drawing.Size(dlg.Size.Width, 1);
            dlg.ControlBox = false;
            dlg.Show();
            dlg.正常提交按钮(EagleAPI.etstatic.Pnr, "95161");


        }
        public connect_4_Command(ServerInfo serverInfo, System.Windows.Forms.TabPage parentWindow)
        {
            
            delegateShowTT = new myDelegateShowTableTicket(threadIbe);
            delegateShowTTInBlackWindow = new myDelegateShowTableTicket1(blackPolicy);
            

            cmdWindow = this;
            m_ServerInfo = serverInfo;
            m_ParentWindow = parentWindow;
            m_ParentWindow.SizeChanged += new EventHandler(m_ParentWindow_SizeChanged);
            
            connect_5_ShakeHand(serverInfo);

            timer = new System.Timers.Timer(180000);//180000
            timer.Elapsed += new System.Timers.ElapsedEventHandler(conn_Elapsed); 
            timer.Start();

            timer0003 = new System.Timers.Timer(120000);
            timer0007 = new System.Timers.Timer(120000);
            timer0003.Elapsed += new System.Timers.ElapsedEventHandler(timer0003_Elapsed);
            timer0007.Elapsed += new System.Timers.ElapsedEventHandler(timer0007_Elapsed);
        }

        void m_ParentWindow_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVar.gbIsNkgFunctions)
                {
                    m_Editor.Size = new System.Drawing.Size(m_ParentWindow.Size.Width * 3 / 4, m_ParentWindow.Height);//FOR NKG
                    m_Panel.Size = new System.Drawing.Size(m_ParentWindow.Size.Width - m_Editor.Size.Width, m_ParentWindow.Height);//FOR NKG
                }
            }
            catch
            {
            }

        }
        void timer0003_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //if (GlobalVar.resendCount0003 < 3)
                //{
                //    GlobalVar.resendCount0003++;
                //    EagleAPI.EagleSendOneCmd(GlobalVar.CurrentSendCommands);
                //    AppendEditorText("\r\n服务器不响应，正在重发指令0x0003：\r\n# " + GlobalVar.CurrentSendCommands.Replace("~", " # ") + "\r\n");
                //}
                //else
                //{
                //    GlobalVar.resendCount0003 = 0;
                //    timer0003.Stop();
                //    AppendEditorText("\r\n重发超过3次，停止自动重发\r\n>");
                //}
            }
            catch
            {
            }
        }
        void timer0007_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (GlobalVar.resendCount0007 < 3)
                {
                    GlobalVar.resendCount0007++;
                    EagleAPI.EagleSendOneCmd(GlobalVar.CurrentSendCommands0007, 7);
                    //AppendEditorText("\r\n服务器不响应，正在重发指令0x0007：\r\n# " + GlobalVar.CurrentSendCommands0007.Replace("~", " # ") + "\r\n");
                }
                else
                {
                    GlobalVar.resendCount0007 = 0;
                    timer0007.Stop();
                    //AppendEditorText("\r\n重发超过3次，停止自动重发\r\n>");
                }
            }
            catch
            {
            }
        }
        public void CloseSocket_ReConnect()
        {
            m_Socket.Close();
            
            connect_5_ShakeHand(m_ServerInfo);
            timer.Start();
        }

        void conn_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (m_Socket != null && m_Socket.Connected)
                {
#if NewProtocol
                    if (EagleProtocol.b_passport)//保持连接
                    {
                        int it = 0;
                        EagleProtocol ep = new EagleProtocol();
                        ep.MsgBody = "";
                        ep.MsgType = 4;
                        ep.SetMsgLength();
                        char[] temp = ep.ConvertToString(out it);
                        byte[] bTemp = new byte [it];
                        for(int i = 0;i<it;i++)
                        {
                            bTemp[i] = (byte)temp[i];
                        }
                        m_Socket.Send(bTemp);
                    }
#else

                    //m_Socket.Send(System.Text.Encoding.ASCII.GetBytes("013"));
#endif
                }
            }
            catch
            {
            }
        }

        public void useconfiglonely(int ipid)
        {

            int it = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgBody = ipid.ToString();
            ep.MsgType = 8;
            ep.SetMsgLength();
            char[] temp = ep.ConvertToString(out it);

            EagleAPI.EagleSend(temp, it);

            //byte[] bTemp = new byte[it];
            //for (int i = 0; i < it; i++)
            //{
            //    bTemp[i] = (byte)temp[i];
            //}
            //m_Socket.Send(bTemp);
        }
        public void SetPanel(Panel p)
        {
            m_Panel = p;
        }
        public void SetEditor(RichTextBox editor)
        {
            m_Editor = editor;
#if RWY
            m_Editor.Font = new System.Drawing.Font("Courior New", 12F);
            m_Editor.ForeColor = System.Drawing.Color.White;
#endif
            AppendEditorText(">"); 
        }
        ListView m_listRightTop = null;
        public void SetListRightTop(ListView lv)
        {
            m_listRightTop = lv;
        }
        
        EagleControls.LV_Lowest m_lvLowest = null;
        EagleControls.LV_GroupList m_lvGroup = null;
        EagleControls.LV_SpecTicList m_lvSpec = null;
        EagleControls.LV_SpecTicList m_lvSpec2 = null;
        public void SetLvSpecTick(EagleControls.LV_SpecTicList lv)
        {
            m_lvSpec = lv;
            m_lvSpec.MouseDoubleClick += new MouseEventHandler(m_lvSpec_MouseDoubleClick);
        }
        public void SetLvSpecTick2(EagleControls.LV_SpecTicList lv)
        {
            m_lvSpec2 = lv;
            m_lvSpec2.MouseDoubleClick += new MouseEventHandler(m_lvSpec_MouseDoubleClick2);
        }

        void m_lvSpec_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = m_lvSpec.SelectedItems[0];
            BookSimple.AddPassenger ap = new ePlus.BookSimple.AddPassenger();
            ap.bSpecTickFlag = true;
            ap.flight = lvi.SubItems[2].Text;
            ap.promote = "申请" + lvi.SubItems[2].Text + "的" + lvi.SubItems[3].Text + "舱位";
            ap.total = "9999";
            ap.booked = "0";
            ap.groupid = lvi.SubItems[0].Text;
            ap.date = Convert.ToDateTime(lvi.SubItems[6].Text);
            ap.CombboxSet(true, lvi.SubItems[2].Text.Substring(0, 2), lvi.SubItems[3].Text[0], 0);
            ap.fromto = lvi.SubItems[1].Text;
            ap.ShowDialog();
        }
        void m_lvSpec_MouseDoubleClick2(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = m_lvSpec2.SelectedItems[0];
            BookSimple.AddPassenger ap = new ePlus.BookSimple.AddPassenger();
            ap.bSpecTickFlag = true;
            ap.flight = lvi.SubItems[2].Text;
            ap.promote = "申请" + lvi.SubItems[2].Text + "的" + lvi.SubItems[3].Text + "舱位";
            ap.total = "9999";
            ap.booked = "0";
            ap.groupid = lvi.SubItems[0].Text;
            ap.date = Convert.ToDateTime(lvi.SubItems[6].Text);
            ap.fromto = lvi.SubItems[1].Text;
            string flight = lvi.SubItems[2].Text;
            char bunk = ' ';
            for (int i = 0; i < m_lvLowest.Items.Count; ++i)
            {
                ListViewItem L = m_lvLowest.Items[i];
                if (L.SubItems[1].Text == flight)
                {
                    bunk = L.SubItems[4].Text[0];
                    break;
                }
            }
            if (bunk == ' ')
            {
                MessageBox.Show("AV结果中无对应航班");
                return;
            }

            ap.CombboxSet(false, lvi.SubItems[2].Text.Substring(0, 2), bunk, Convert.ToInt32(m_lvSpec2.SelectedItems[0].SubItems[5].Text));
            ap.ShowDialog();
        }
        public void SetLvLowest(EagleControls.LV_Lowest lv)
        {
            m_lvLowest = lv;
        }
        public void SetLvGroup(EagleControls.LV_GroupList lv)
        {
            m_lvGroup = lv;
            m_lvGroup.MouseDoubleClick += new MouseEventHandler(m_lvGroup_MouseDoubleClick);
        }

        void m_lvGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BookSimple.AddPassenger ap = new ePlus.BookSimple.AddPassenger();
            string id = "加入：";
            for (int i = 2; i < 6; i++)
            {
                id += " →" + m_lvGroup.SelectedItems[0].SubItems[i].Text;
            }
            ap.promote = id;
            ap.total = m_lvGroup.SelectedItems[0].SubItems[6].Text;
            ap.booked = m_lvGroup.SelectedItems[0].SubItems[8].Text;
            ap.booked = Convert.ToString(int.Parse(ap.total) - int.Parse(ap.booked));
            ap.groupid = m_lvGroup.SelectedItems[0].SubItems[0].Text;
            ap.ShowDialog();
        }
        public void DisConnect()
        {
            EagleAPI.LogWrite("断开连接…");
            if(m_notifyIcon != null)
                m_notifyIcon.stop();
            if (m_Editor != null)
            {
                m_ParentWindow.Controls.Remove(m_Editor);
                m_ParentWindow.PerformLayout();
                m_Editor.Dispose();
            }

            if (m_Socket != null && m_Socket.Connected)
            {
                m_Socket.Close();
                //m_Socket.Disconnect(true);
                //m_Socket.Close();
            }
        }

        public bool IsConnected
        {
            get
            {
                return (m_Socket != null && m_Socket.Connected);
            }
        }
        
        [DllImport("TestDll.dll", EntryPoint = "GetSendPacket", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public extern unsafe static string GetSendPacket([MarshalAs(UnmanagedType.LPStr)]string cmd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        //public extern static string GetSendPacket(string cmd);
        [DllImport("TestDll.dll", EntryPoint = "GetReturnString", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public extern unsafe static string GetReturnString([MarshalAs(UnmanagedType.LPStr)]string cmd, int psize, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        [DllImport("TestDll.dll", EntryPoint = "ChineseCodeSend", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public extern unsafe static string ChineseCodeSend([MarshalAs(UnmanagedType.LPStr)]string cmd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        [DllImport("TestDll.dll", EntryPoint = "ChineseCodeRecieve", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public extern unsafe static string ChineseCodeRecieve([MarshalAs(UnmanagedType.LPStr)]string cmd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);

        private const int MaxBuffer = 4096;
        private Socket m_Socket = null;
        private List<string> m_Transaction = new List<string>();
        private Stack<List<string>> m_TransactionList = new Stack<List<string>>();
        private ServerInfo m_ServerInfo = null;
        #if !Sync
        private System.Threading.ManualResetEvent receiveEvent = new System.Threading.ManualResetEvent(false);
        private bool transactionState = false;
        #endif
        private System.Threading.ManualResetEvent syncEvent = new System.Threading.ManualResetEvent(false);
        private System.Threading.ManualResetEvent transactionEvent = new ManualResetEvent(false);
        public System.Windows.Forms.RichTextBox m_Editor = null;
        public Panel m_Panel = null;
        
        private System.Windows.Forms.TabPage m_ParentWindow = null;
        
        private Queue<OutputState> Outputable = new Queue<OutputState>();
        private System.Timers.Timer timer = null; 
#if Branch1
        static public List<string> Qall = new List<string>();
        static public List<string> Qquery = new List<string>();
        static public List<string> Qorder = new List<string>();
        static public List<string> Qsend = new List<string>();

        static public List<string> Qall0007 = new List<string>();
        static public List<string> Qquery0007 = new List<string>();
        static public List<string> Qorder0007 = new List<string>();
        static public List<string> Qsend0007 = new List<string>();

        static public List<string> Qall0003 = new List<string>();
        static public List<string> Qquery0003 = new List<string>();
        static public List<string> Qorder0003 = new List<string>();
        static public List<string> Qsend0003 = new List<string>();

        static public List<string> lsNewCmdStrings = new List<string>();

#endif

        class OutputState
        {
            public OutputState(bool value, System.Threading.ManualResetEvent resetEvent)
                : this(value, true, resetEvent)
            {
            }

            public OutputState(bool value)
                : this(value, true,null)
            {
            }

            public OutputState(bool value, bool start, System.Threading.ManualResetEvent resetEvent)
            {
                m_Outputable = value;
                ResetEvent = resetEvent;
                //timer = new System.Windows.Forms.Timer();
                timer = new System.Timers.Timer();

                timer.Interval = 5000;
                m_Enabled = true;
                //timer.Tick += new EventHandler(timer_Tick);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed); 
                if (start)
                    timer.Start();
            }

            public System.Threading.ManualResetEvent ResetEvent = null;
            

            void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Stop();
                m_Enabled = false;
                if (ResetEvent != null)
                    ResetEvent.Set();
            }

            public void Start()
            {
                if (timer != null)
                    timer.Start(); 
            }

            void timer_Tick(object sender, EventArgs e)
            {
                timer.Stop();
                m_Enabled = false;

                //throw new Exception("服务器超时。");
            }

            public bool Outputable
            {
                get
                {
                    return m_Outputable; 
                }
            }

            public bool Enabled
            {
                get
                {
                    return m_Enabled;
                }
            }

            private bool m_Outputable = false;
            private bool m_Enabled = false;
            //private System.Windows.Forms.Timer timer = null;
            private System.Timers.Timer timer = null;
        }

        private void NewTransaction()
        {
            m_TransactionList.Push(new List<string>());
        }

        private void NewTransItem(string command)
        {
            if (m_TransactionList.Count > 0)
                m_TransactionList.Peek().Add(command);   
        }


        public void sendbyte(byte[] b)
        {
            NetworkStream stream = new NetworkStream(m_Socket);
            stream.Write(b, 0, b.Length);
            
        }
        /// <summary>
        /// 未用到此重载
        /// </summary>
        /// <param name="content"></param>
        public void SendData(string content)
        {
            if (!EagleProtocol.b_passport)//passport未通过认证，则不发
            {
                AppendEditorText(GlobalVar.Notice + GlobalVar.loginName + ":还未通过您的认证或正在认证中。。。。。。\r>");
                if (!Model.md.b_004) MessageBox.Show("还未通过您的认证或正在认证中。。。。。。");
                return;
            }
        shakeHande:
            if (!m_Socket.Connected)
                connect_5_ShakeHand(m_ServerInfo);

            NetworkStream stream =  null;

            try
            {
                stream = new NetworkStream(m_Socket);
                byte[] sendData = System.Text.Encoding.Default.GetBytes(content);  
                //sendData[17] = (byte)0x20;//0x27 
                //sendData[18] = (byte)0x20;//0x20
                stream.Write(sendData, 0, sendData.Length);
            }
            catch (System.IO.IOException ex)
            {
                goto shakeHande;
            }
            catch (Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message);
            }
            //finally
            //{
            //    Application.Exit();
            //}
        }
        public void SendData(char[] content,int length)
        {
        shakeHande:
            if (m_Socket == null)
            {
                m_Socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
            }
            if (GlobalVar.b_replaceSocket)
            {
                GlobalVar.b_replaceSocket = false;
                GlobalVar.b_IsKick = false;
                timer.Stop();
                m_Socket.Close();
                m_ServerInfo.Edit();
                m_ServerInfo.Address = GlobalVar.loginLC.SrvIP;
                m_ServerInfo.Port = (GlobalVar.loginLC.SrvPort);
                connect_5_ShakeHand(m_ServerInfo);//重连
                EagleAPI.SpecifyPassport();
                timer.Start();                
            }
            if (!m_Socket.Connected )
            {
                GlobalVar.AutoConnectCount++;
                if (GlobalVar.AutoConnectCount > 3)
                {
                    EagleAPI.LogWrite("重新连接三次仍失败，放弃！");
                    return;
                }
                connect_5_ShakeHand(m_ServerInfo);
                //AppendEditorText(GlobalVar.Notice + "服务器断开连接，正在重新连接服务器！");
                //EagleAPI.SpecifyPassport();
                //EagleAPI.SpecifyCFG();
                timer.Start();
                //if (!Model.md.b_004) MessageBox.Show("服务器断开连接，正在重新连接服务器！");

            }

            NetworkStream stream = null;
            try
            {
                stream = new NetworkStream(m_Socket);
                Options.GlobalVar.socketGlobal = m_Socket;
                byte[] sendData = System.Text.Encoding.Default.GetBytes(content,0,length);
                if (sendData.Length > 16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        sendData[i] = (byte)content[i];
                    }
                }
                if (PrintReceipt.b_3IN1)
                {
                    for (int i = 0; i < length; i++)
                    {
                        sendData[i] = (byte)content[i];
                    }
                    PrintReceipt.b_3IN1 = false;
                }
                //检查长度与包内容的长度是否一致
                int thirdbyte = (int)sendData[2];
                int forthbyte = (int)sendData[3];
                if ((forthbyte * 256 + thirdbyte) != sendData.Length)
                {
                    string sLen = sendData.Length.ToString("x4");
                    sendData[2] = (byte)int.Parse(sLen.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    sendData[3] = (byte)int.Parse(sLen.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);

                }
                //对可能出现不可见字符进行处理。奇怪，为什么为出现不可见字符。。。。
                int iPos = 16;
                if (GlobalVar.b_switchingconfig)GlobalVar.b_switchingconfig = false;
                else
                {
                    while (iPos < sendData.Length && sendData[iPos] < (byte)' ')
                    {
                        sendData[iPos] = (byte)'~';
                        iPos++;
                    }
                }

                try
                {
                    //AppendEditorText("         开始发出        ");
                    stream.Write(sendData, 0, sendData.Length);
                    GlobalVar.PackageNumberSend++;
                }
                catch
                {
                    AppendEditorText("         发出错误        ");
                }
            }
            catch (System.IO.IOException ex)
            {
                EagleAPI.LogWrite("SOCKET报错重连：" + ex.Message);
                goto shakeHande;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("SendData(char[] content,int length)" + e.Message);
            }
            //finally
            //{
            //    Application.Exit();
            //}
        }

        /// <summary>
        /// 建立与服务器的连接
        /// </summary>
        /// <param name="serverInfo">服务器信息</param>
        /// <returns>是否连接成功</returns>
        private bool connect_6_socket(ServerInfo serverInfo)
        {
            #region 1、根据配置文件，得到服务器信息
            IPEndPoint remoteServer = null;
            try
            {
                string ipORdns = serverInfo.Address.Trim();
                string[] s = ipORdns.Split('.');
                bool bAllNum = false;
                if (s.Length == 4 || s.Length == 6)
                {

                    for (int i = 0; i < s.Length; i++)
                    {
                        int num=0;
                        bAllNum = int.TryParse(s[i], out num);
                        if (!bAllNum) break;
                    }
                }
                if (bAllNum) throw new Exception("");
                remoteServer = new IPEndPoint(Dns.GetHostEntry(serverInfo.Address).AddressList[0], serverInfo.Port);
                
                if (remoteServer.Address.ToString() == "127.0.0.1") throw new Exception("错误解析：IP地址为127.0.0.1");
            }
            catch 
            {
                try
                {
                    remoteServer = new IPEndPoint(IPAddress.Parse(serverInfo.Address), serverInfo.Port);
                    if (remoteServer.Address.ToString() == "127.0.0.1") throw new Exception("错误解析：IP地址为127.0.0.1");
                }
                catch
                {
                    try
                    {
                        remoteServer = new IPEndPoint(Dns.GetHostEntry(GlobalVar.loginLC.SrvDNS).AddressList[0], serverInfo.Port);
                        if (remoteServer.Address.ToString() == "127.0.0.1") throw new Exception("错误解析：IP地址为127.0.0.1");
                    }
                    catch
                    {
                        try
                        {
                            remoteServer = new IPEndPoint(IPAddress.Parse(GlobalVar.loginLC.SrvDNS), serverInfo.Port);
                            if (remoteServer.Address.ToString() == "127.0.0.1") throw new Exception("错误解析：IP地址为127.0.0.1");
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show
                                ("ReConnect" + e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            //finally
            //{
            //    Application.Exit();
            //}
            #endregion

            #region 2、建立Socket连接
            if (remoteServer == null)
            {
                System.Windows.Forms.MessageBox.Show("解析服务器地址失败！", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            if (m_Socket != null)
            {
                m_Socket.Close();
                m_Socket = null;
            }
  
            m_Socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                EagleAPI.LogWrite("Socket 尝试连接:" + remoteServer.ToString());

                //m_Socket.Connect(remoteServer);//太耗时，若对方无应答，默认大约有21秒的阻塞，改用以下超时机制
                //EagleAPI.LogWrite("Connected!");
                
                //放弃6秒超时机制，改用多线程，将连接配置和弹出最终界面分开
                //if (!asyncResult.AsyncWaitHandle.WaitOne(System.Threading.Timeout.Infinite) && !m_Socket.Connected)//6秒超时
                //{
                //    try
                //    {
                //        //m_Socket.EndConnect(asyncResult);该语句一样阻塞，将引发SocketException：“由于连接方在一段时间后没有正确答复或连接的主机没有反应，连接尝试失败”
                //        m_Socket.Close();
                //    }
                //    catch (SocketException)
                //    { }
                //    catch (ObjectDisposedException)
                //    { }

                //    throw new TimeoutException("6秒超时！");
                //}
                
                var asyncResult = m_Socket.BeginConnect(remoteServer, connectCallback, m_Socket);
                
#if Sync
                ReceiveData(m_Socket, false);   
#endif
            }
            catch(Exception ex0)
            {
                EagleAPI.LogWrite("Socket 连接失败：" + ex0.ToString());
                return false;
                //EagleAPI.LogWrite("Socket 掉线重连" + ex0.Message);
                //try
                //{
                //    string temp = GlobalVar.loginLC.SrvIP;
                //    if (GlobalVar.gbZzInternetIP != GlobalVar.loginLC.SrvIP)//郑州内外网的话，不交换，因为内外网不是指向同一个域名
                //    {
                //        GlobalVar.loginLC.SrvIP = GlobalVar.loginLC.SrvDNS;
                //        GlobalVar.loginLC.SrvDNS = temp;
                //    }
                //    string tip = Login_Classes.dns2ip_static(GlobalVar.loginLC.SrvIP);
                //    m_Socket.Connect(tip, GlobalVar.loginLC.SrvPort);
                //    GlobalVar.loginLC.SrvIP = tip;
                //    return m_Socket.Connected;
                //}
                //catch(Exception ex)
                //{
                //    MessageBox.Show("暂不能提取编码，请手工输入乘客信息！",
                //        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    EagleString.EagleFileIO.LogWrite(ex.ToString());
                //}
            }
            #endregion

            return true;
        }

        //added by chenqj 2011.9.12
        private static void connectCallback(IAsyncResult ar)
        {
            // 从state对象获取socket.  
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                // 完成连接.  
                socket.EndConnect(ar);

                GlobalVar.ipport = ((IPEndPoint)socket.LocalEndPoint).Address.ToString() + ":" + ((IPEndPoint)socket.LocalEndPoint).Port.ToString();
                //指定配置，否则无法使用指令
                EagleAPI.SpecifyPassport();
                Options.GlobalVar.socketGlobal = socket;
            }
            catch (Exception e)
            {
                EagleString.EagleFileIO.LogWrite("connectCallback failed : " + e.ToString());
                socket.Close();
            }

            Options.GlobalVar.IsConnecting = false;
            EagleAPI.LogWrite("connect end");//时间间隔
        }

        private bool connect_5_ShakeHand(ServerInfo serverInfo)
        {
            serverInfo.Edit();
            //serverInfo.Address = GlobalVar.loginLC.SrvIP;//commentted by king
            //serverInfo.Port = GlobalVar.loginLC.SrvPort;
            if (m_Socket == null || (m_Socket != null && !m_Socket.Connected))
            {
                if (!connect_6_socket(serverInfo))//开始连接
                    return false;
            }

            try
            {

#if !Sync
                Thread thread = new Thread(new ThreadStart(ReceiveData));//开始监听
                thread.IsBackground = true;
                thread.Start();
#endif
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将字串以两位为一组16进制码，转换成Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private byte[] CodeToBytes(string value)
        {
            byte[] result = new byte[value.Length / 2];
            for (int i = 0, j = 0; i < value.Length; i += 2, j++)
            {
                result[j] = Convert.ToByte(value.Substring(i, 2), 16);
            }

            return result;
        }

        private byte[] SubByteDim(byte[] value)
        {

            int it = 0;
            try
            {
                it = int.Parse(GlobalVar.loginLC.TrimLen);
            }
            catch
            {
                it = 19;//返回包头之长
            }
            //if (value[18] == 0x0f) it = 19;
            //else if (value[16] == 0x0f) it = 17;
            it = 19;
            if (value.Length > 17 && (value[17] == 0x1B) && (value[18] == 0x4d)) it=19;
            else if (value.Length > 22 && (value[22] == 0x1B) && (value[23] == 0x4d)) it = 24;
            else it = 17;
            if (value.Length < (it+2))
                return null;

            //包尾可能不是2003
            int tailLen = 2;
            if (value[value.Length - 1] != 0x03)
                tailLen = 0;


            byte[] result = new byte[value.Length - (it + tailLen) + 1];
            for (int i = 0; i < value.Length - (it + tailLen); i++)
            {
                result[i] = value[i + it];
            }
            return result;
        }
        private byte[] SubByteEtermAgent(byte[] value)
        {
            byte[] result = new byte[value.Length - (23 + 2) + 1];
            for (int i = 0; i < value.Length - 25; i++)
            {
                result[i] = value[i + 23];
            }
            return result;
        }
        private byte[] SubByteDimReceiptCreact(byte[] value)
        {
            if (value.Length < 13)
                return null;

            byte[] result = new byte[value.Length - 13 + 1];
            for (int i = 0; i < value.Length - 13; i++)
            {
                result[i] = value[i + 12];
            }
            return result;
        }

        private byte[] SubByteDim(byte[] value, int start, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = value[i + start];
            }
            return result;
        }

        private byte[] SubByteDim(byte[] value, int start)
        {

            return SubByteDim(value, start, value.Length - start);
        }

        delegate void AppendTextCallback(string text);
        delegate void ProcedureCallback();
        //delegate LoginForm LoginFormCallback();


        private void AppendEditorText(string text)
        {
            if (m_Editor != null)
            {
                if (m_Editor.InvokeRequired)
                {
                    AppendTextCallback deleg = new AppendTextCallback(AppendEditorText2);
                    m_Editor.Invoke(deleg, text);
                }
                else
                {
                    AppendEditorText2(text);
                }
            }
        }


        private void AppendEditorText2(string text)
        {
            text = text.Replace("\x1E", ">");
            try
            {
                int oldTextLen = m_Editor.Text.Length;
                int len = text.Length;
                int gg = 0;
                char add = '\0';
                string tmp = "";
                while (gg < len)
                {
                    if (text[gg] == 0x1B && text[gg + 1] == 0x0E)
                    {
                        add = (char)128;
                        continue;
                    }
                    if (text[gg] == 0x1B && text[gg + 1] == 0x0F)
                    {
                        add = '\0';
                        continue;
                    }
                    tmp += (char)(text[gg] + add);
                    gg++;
                }

                if (this.m_Editor.InvokeRequired)
                {
                    AppendTextCallback d = new AppendTextCallback(AppendEditorText);
                    this.m_ParentWindow.Invoke(d, new object[] { tmp });
                }
                else
                {
                    string oldtext = m_Editor.Text;

                    this.m_Editor.AppendText(tmp);
                    string newtext = m_Editor.Text;
                    this.m_Editor.Focus();

                    //string str = "航空公司使用自动出票时限, 请检查PNR";

                    string str = "航空公司使用自动出票时限";
                    int start = m_Editor.Find(str);
                    int length = str.Length;
                    while (start > -1 && m_Editor.Text.Length > (start + length))
                    {

                        m_Editor.SelectionStart = start;
                        m_Editor.SelectionLength = length;
                        m_Editor.SelectionColor = System.Drawing.Color.Red;
                        start = m_Editor.Find(str, start + length, RichTextBoxFinds.None);
                    }
                    //订票状态颜色
                    try
                    {
                        string[] items = tmp.Split('\n');
                        int offset = 0;
                        for (int i = 0; i < items.Length; i++)
                        {
                            offset += items[i].Length;
                            if (items[i].Length < 5) continue;
                            if (items[i].Substring(3, 2) == "  " && int.Parse(items[i].Substring(0, 2).Trim()) < 100 && items[i][0] != '0')
                            {
                                m_Editor.SelectionStart = oldtext.Length + offset + 31 - items[i].Length;
                                m_Editor.SelectionLength = 5;
                                m_Editor.SelectionColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    catch
                    {
                    }
                    //颜色
                    int pos1c = m_Editor.Text.IndexOf("\x1C", oldTextLen);
                    int pos1d = m_Editor.Text.IndexOf("\x1D", pos1c + 1);

                    while (pos1c >= 0 && pos1d > 0)
                    {
                        try
                        {
                            m_Editor.SelectionStart = pos1c + 1;
                            m_Editor.SelectionLength = pos1d - pos1c - 1;
                            m_Editor.SelectionColor = System.Drawing.Color.Red;
                            pos1c = m_Editor.Text.IndexOf("\x1C", pos1d);
                            if (pos1c < 0) break;
                            pos1d = m_Editor.Text.IndexOf("\x1D", pos1c);
                        }
                        catch
                        {
                            break;
                        }
                    }
                    //m_Editor.Text = m_Editor.Text.Replace("\x1C", " ").Replace("\x1D", " ");
                    try
                    {
                        m_Editor.SelectionStart = m_Editor.Text.Length;
                        m_Editor.SelectionLength = 0;
                    }
                    catch
                    {
                        m_Editor.SelectionStart = m_Editor.Text.LastIndexOf('>') + 1;
                        m_Editor.SelectionLength = 0;
                    }
                }
            }
            catch
            {
            }
            //av指令显示简易版中的航班列表
            Thread blackPolicyThread;
            if(GlobalVar .gbIsNkgFunctions )blackPolicyThread = new Thread(new ThreadStart(blackPolicyInvoke));
            else blackPolicyThread = new Thread(new ThreadStart(blackPolicy));

            blackPolicyThread.Start();
        }
        bool bRunning = false;

        private delegate void myDelegateShowTableTicket1();
        private myDelegateShowTableTicket1 delegateShowTTInBlackWindow;
        void blackPolicyInvoke()
        {
            
            m_Panel.Invoke(delegateShowTTInBlackWindow, null);

        }

        void blackPolicy()
        {
            if (BookTicket.bIbe) return;
            if (bRunning) return;
            bRunning = true;
            try
            {
                
                //Command.bookTicket.setToBooktktExtDisplay();
                //Command.bookTicket.Location = m_Editor.PointToScreen(m_Editor.Location);
                //Command.bookTicket.TopMost = true;
                //Command.bookTicket.ControlBox = false;
                //Command.bookTicket.FormBorderStyle = FormBorderStyle.None;
                
                //GlobalVar2.bookTicket.setToBooktktListView(Command.AV_String, GlobalVar2.gbFromto);
                if(GlobalVar .gbIsNkgFunctions )
                    showTableTicket();//显示右窗体
            }
            catch
            {
                //MessageBox.Show("?");
            }
            bRunning = false;
        }
        
        private void DisconnectServer()
        {
            if (m_ParentWindow.InvokeRequired)
            {
                ProcedureCallback d = new ProcedureCallback(DisconnectServer);
                m_ParentWindow.Invoke(d);
            }
            else
                this.DisConnect();
        }

        //private LoginForm GetLoginForm()
        //{
            //if (m_ParentWindow.InvokeRequired)
            //{
            //    LoginFormCallback d = new LoginFormCallback(GetLoginForm);
            //    return (LoginForm)m_ParentWindow.Invoke(d);
            //}
            //else
            //{
            //    return ShowLoginForm();
            //}
        //}

        //private LoginForm ShowLoginForm()
        //{
            //LoginForm frmlogin = new LoginForm();
            //frmlogin.ShowDialog();
            //return frmlogin;
        //}
        bool bFirst = true;
        bool bEagleEtermRet = false;

        private void DeductMoney(string res)
        {
            EagleString.EtdzResult dzres = new EagleString.EtdzResult(res);
            if (dzres.SUCCEED && dzres.TOTAL>0)
            {
                //调用扣款
                EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(GlobalVar.WebServer);
                bool bflag = false;
                float ftemp = 0F;
                kf.DecFee(dzres.Pnr, dzres.TOTAL, ref bflag, ref ftemp);
                GlobalVar.f_CurMoney = ftemp.ToString("f2");
                if (!bflag)
                {
                    kf.WriteLogToServer(GlobalVar.loginName, "调用扣款失败！", dzres.STRING, ref bflag);
                }
                else
                {
                    kf.WriteLogToServer(GlobalVar.loginName, "调用扣款成功！", dzres.STRING, ref bflag);
                    AppendEditorText("余额：" + GlobalVar.f_CurMoney + "\r\n>");
                }
            }
        }
        //引入2.0中的CommandPool
        EagleString.CommandPool m_cmdpool = new EagleString.CommandPool ();
        private void SIGN_IN_FIRST(string res)
        {
            if (res.ToUpper().Contains("PLEASE SIGN IN FIRST") || res.ToUpper().Contains("SI\r\n"))
            {
                try
                {
                    string si = EagleString.EagleFileIO.SignCodeOf(GlobalVar.str_cfg_name);
                    this.Send(si,3);
                }
                catch(Exception e)
                {
                    EagleString.EagleFileIO.LogWrite(e.ToString());
                    EagleString.EagleFileIO.LogWrite("客户端SI(工作号/密码)失败，可能原因：1.选择了全部配置;2.系统中没有记录该配置的SI");
                }
                return;
            }
        }
        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                //syncEvent.Set();
                lock (ar)
                {

                    receiveEvent.Set();//signal 线程WaitOne处获得信号
                    GlobalVar.PackageNumberRecv++;
                    StateObject stateObject = (StateObject)ar.AsyncState;
                    Socket socket = stateObject.workSocket;
                    if (!(socket != null && socket.Connected))
                        goto goon;

                    int bytesRead = 0;
                    try
                    {
                        bytesRead = socket.EndReceive(ar);
                        if (EagleAPI.test())
                            AppendEditorText("收到" + bytesRead.ToString() + "字节\n>qt");
                    }
                    catch (Exception ex)
                    {
                        EagleAPI.LogWrite("ReceiveData 回调函数：" + ex.ToString());
                        m_Socket.Close();
                        m_Socket = null;
                        goto goon;
                    }

                    if (bytesRead < 1)
                    {
                        //receiveEvent.Set();
                        goto goon;
                    }
                    //try
                    //{
                        Thread th = new Thread(new ParameterizedThreadStart(frmMain.instance.ia10.RecvHz));
                        th.Start(stateObject.buffer);
                        //frmMain.instance.ia10.RecvHz(stateObject.buffer);//保险提取编码 commentted by king
                    //}
                    //catch
                    //{
                    //}

                    byte[] buffer = SubByteDim(stateObject.buffer, 0, bytesRead);
                    if (EagleAPI.test())
                        AppendEditorText("消息类型：" + buffer[0].ToString() + "\n>qt");

                    //string str = "001{0}*{1}";
                    #region if (buffer[0] == 0x01 && buffer[1] == 0x10)//passport返回消息 1001;
                    if (buffer[0] == 0x01 && buffer[1] == 0x10)//passport返回消息 1001;
                    {
                        if (buffer[12] == 0x01 && buffer[13] == 0x00)//成功
                        {
                            EagleAPI.LogWrite("Passport认证通过!");
                            EagleProtocol.b_passport = true;
                            if (GlobalVar.AutoConnectCount == 0)
                            {
#if !RWY
                                AppendEditorText(GlobalVar.loginName + ": 欢迎使用" + GlobalVar.exeTitle + "，已经通过您的认证！\r\n>");
                                AppendEditorText("您的帐户余额为：￥" + GlobalVar.f_CurMoney + "\r>");
#else
                                AppendEditorText("登录成功\r\n>");
#endif
                                GlobalVar.f_Balance = decimal.Parse(GlobalVar.f_CurMoney);
                            }
                            GlobalVar.AutoConnectCount = 0;
                            //w登陆时指定为IPsString的最后一个
                            string lastone = "";
                            string[] aip = GlobalVar.loginLC.IPsString.Split('~');
                            lastone = aip[aip.Length - 1];


                            //if (!Model.md.b_004) EagleAPI.SpecifyCFG(GlobalVar.etdz_config = GlobalVar.loginLC.IPsString);//非黑屏用户可用所有配置
                            {
#if 全部配置
                        if (!Model.md.b_004) EagleAPI.SpecifyCFG(EagleAPI.GetIPByConfigNumber("全部配置"));
                        else EagleAPI.SpecifyCFG(GlobalVar.etdz_config = EagleAPI.GetIPByConfigNumber(GlobalVar.officeNumberCurrent));
#else
                                if (EagleAPI.GetIPByConfigNumber("SIA180-01") != "")
                                {
                                    GlobalVar.officeNumberCurrent = "SIA180-01";
                                    EagleAPI.SpecifyCFG(GlobalVar.etdz_config = EagleAPI.GetIPByConfigNumber(GlobalVar.officeNumberCurrent));
                                }
                                else
                                {
                                    EagleAPI.SpecifyCFG(EagleAPI.GetIPByConfigNumber("全部配置"));
                                }
#endif
                            }

                            GlobalVar.mylis.ipSource = Login_Classes.dns2ip_static(GlobalVar.loginLC.SrvIP);
                            GlobalVar.mylis.portSource = GlobalVar.loginLC.SrvPort;
                            GlobalVar.mylis.ipDest = GlobalVar.ipport.Split(':')[0];
                            GlobalVar.mylis.portDest = int.Parse(GlobalVar.ipport.Split(':')[1]);
                            //GlobalVar.mylis.StartWork();

                        }
                        else if (buffer[12] == 0x04 && buffer[13] == 0x10)//认证失败
                        {
                            EagleProtocol.b_passport = false;
                            MessageBox.Show("Passport认证失败，请重新登录！\r");
                            Application.Exit();
                        }
                        else //认证消息错误
                        {
                            EagleProtocol.b_passport = false;
                            MessageBox.Show("Passport认证发生错误，请重新登录！\r");
                            Application.Exit();
                        }

                    }
                    #endregion
                    #region else if (buffer[0] == 0x02 && buffer[1] == 0x10)//IP返回消息 1002
                    else if (buffer[0] == 0x02 && buffer[1] == 0x10)//IP返回消息 1002
                    {
                        if (buffer[12] == 0x01 && buffer[13] == 0x00)//成功 0001
                        {
                            EagleAPI.LogWrite("连接配置成功!");
                            if ((!GlobalVar.b_cfg_First) && (GlobalVar.bSwichConfigByManual) && (!GlobalVar.bSwichConfigAuto)) AppendEditorText("配置切换到  " + GlobalVar.officeNumberCurrent/*GlobalVar.str_cfg_name*/ + "  成功\r>");//第一次发送IP消息，不显示
                            if (GlobalVar.CurrentSendCommands != "")
                            {
                                //重新发送断线时的指令
                                Thread.Sleep(100);
                                EagleAPI.CLEARCMDLIST(3);
                                //if((!GlobalVar.bSwichConfigByManual))//手工切换时，不重发数据
                                //    EagleAPI.EagleSendCmd(GlobalVar.CurrentSendCommands);//点击断开，连接，会自动重发
                                GlobalVar.bSwichConfigByManual = false;
                                GlobalVar.bSwichConfigAuto = false;

                            }
                        }
                        else
                        {
                            //if (!GlobalVar.b_cfg_First)
                            AppendEditorText("配置切换到  " + GlobalVar.officeNumberCurrent + "  失败，请重新设置！\r>");
                        }
                    }
                    #endregion
                    #region else if ((buffer[0] == 0x03 && buffer[1] == 0x10) || (buffer[0] == 0x07 && buffer[1] == 0x10))//民航返回 1003或1007
                    else if (buffer.Length > 1 && ((buffer[0] == 0x03 && buffer[1] == 0x10) || (buffer[0] == 0x07 && buffer[1] == 0x10)))//民航返回 1003
                    {
                        byte buffer0 = buffer[0];
                        //关闭重发定时器
                        if (buffer0 == 0x07)//关闭0x0007定时器
                        {
                            timer0007.Stop();
                        }
                        else//关闭0x0003定时器
                        {
                            timer0003.Stop();
                        }


                        //AppendEditorText("            收到       \r");
                        //MessageBox.Show("民航");
                        //E a g l e Group
                        //if ((Model.md.b_008 || Model.md.b_018 || Model.md.b_028 || Model.md.b_038 || Model.md.b_048) && GlobalVar.IsListGroupTicket)
                        //{
                        //    PassengerToGroup tg = new PassengerToGroup();
                        //    tg.avstring = log.strSend;
                        //    Thread th = new Thread(new ThreadStart(tg.execute));

                        //    th.Start();
                        //    //tg.execute();
                        //}



                        //定是当前包是否结束，后续包
                        bool bCurrentBuffHasEndFlag = true;
                        //if (buffer[buffer.Length - 1] >= 0x20 ) bCurrentBuffHasEndFlag = false;
                        if (buffer[buffer.Length - 1] == 0x03) bCurrentBuffHasEndFlag = true;
                        else bCurrentBuffHasEndFlag = false;
                        //buffer = SubByteDim(buffer, 12);

                        //减去EAGLE协议头----开始
                        if (buffer[10] == 0x01 || buffer[10] == 0x02)
                            buffer = SubByteDim(buffer, 10);
                        else if (buffer[12] == 0x01 || buffer[12] == 0x02)//民航协议第一个字节
                            buffer = SubByteDim(buffer, 12);
                        else
                        {
                            buffer = SubByteDim(buffer, 12);
                            if (bFirst)
                            {
                                bEagleEtermRet = true;//只在第一次返回结果中,确定当前使用的是443配置服务器还是350配置服务器

                            }
                        }
                        bFirst = false;
                        //减去EAGLE协议头----完成

                        bool bEtermAgent = false;
                        bool bAddLastBuff = false;
                        if (!(PrintReceipt.IsPrinting || PrintReceipt.IsRemoving))//不是打印电子行程单，则不减9
                        {
                            if (buffer[0] == 0x01)
                                buffer = SubByteDim(buffer);//减去民航包头
                            else if (buffer[0] == 0x02)
                            {
                                buffer = SubByteEtermAgent(buffer);//减去etermAgent头
                                bEtermAgent = true;
                            }
                            else if (!bEagleEtermRet)//可能是接上个包的返回，把上次保存的buffer加到当前的buffer前(350配置,443是完整的包)
                            {
                                bAddLastBuff = true;
                            }
                        }
                        else buffer = SubByteDimReceiptCreact(buffer);//7.10修改//是行程单打印，则减掉3in1指令头

                        if (buffer == null) goto goon;
                        //返回结果空时，直接返回
                        //if (buffer.Length == 3 && buffer[0] == 30 && buffer[1] == 27 && buffer[2] == 0)
                        //    goto goon;
                        //////////////////////
                        //for (int i = 0; i < buffer.Length - 1; i++)
                        //{
                        //    if (buffer[i] == 14) buffer[i] = 32;
                        //}
                        /////////////////////////////跃华退单问题
                        string rev = System.Text.Encoding.Default.GetString(buffer);//7.10修改UTF8
                        rev = rev.Replace((char)0x10, '>');

                        //help指令
                        if (rev.IndexOf("NOT A HTL CRT - CANNOT USE HB/HBC/HE/HN") >= 0) goto goon;

                        GlobalVar.str_Listener_eagle = "rev";// rev;

                        if (!(PrintReceipt.IsPrinting || PrintReceipt.IsRemoving))//不是打印电子行程单，则不减
                        {
                            #region 返回结果字符串处理
                            int index = rev.LastIndexOf('\xd');

                            if (index > -1 && rev.Length - index < 40) rev = rev.Substring(0, index + 1);
                            StringBuilder strClaw = new StringBuilder("", 4096);
                            if (!bEagleEtermRet)
                            {
                                GetReturnString(rev, rev.Length, strClaw);

                                rev = strClaw.ToString();

                                //0x1c为红色开始,0x1d为红色结束，替换为空格
                                //rev = rev.Replace((char)0x1C, ' ').Replace((char)0x1D, ' ');

                                ChineseCodeRecieve(rev, strClaw);
                                rev = strClaw.ToString();
                                char c_char = (char)0x1B;
                                while (rev.IndexOf("\x1b\x62") > -1) rev = rev.Remove(rev.IndexOf("\x1b\x62"), 2);
                                rev = rev.Replace(c_char.ToString() + "b", "  ");
                                rev = rev.Replace(c_char.ToString(), " ");
                            }
                            rev = EagleAPI2.NewLineBetweenNameAndFlight(rev);
                            string[] temp = rev.Split('\xd');
                            rev = "";
                            foreach (string s in temp)
                            {
                                if (s.Length > 80)
                                {
                                    int len = s.Length;
                                    int pos = 0;
                                    while (len > 0)
                                    {
                                        if (len > 80)
                                            rev += s.Substring(pos, 80) + "\r\n";
                                        else
                                            rev += s.Substring(pos, len) + "\r\n";

                                        len -= 80;
                                        pos += 80;
                                    }
                                }
                                else
                                    rev += s + "\r\n";
                            }
                            #endregion
                        }
                        {
                            int pos = rev.IndexOf(".  ");
                            while (pos > 0)
                            {
                                if (rev[pos + 3] > 'z')
                                {
                                    rev = rev.Remove(pos + 1, 2);
                                    pos = rev.IndexOf(".  ");
                                }
                                else
                                {
                                    pos = rev.IndexOf(".  ", pos + 1);
                                }
                            }
                        }
                        if (!bEagleEtermRet)
                        {
                            //对多包的组合处理A:
                            if (bAddLastBuff) rev = UseOnceClass.LastBuff + rev;
                            //保存buffer到全局变量，以备未结束和下一个包组合
                            UseOnceClass.LastBuff = (rev.Substring(rev.Length - 2) == "\r\n" ? rev.Substring(0, rev.Length - 2) : rev);
                        }
                        AV_String = rev;//位置不能变，影响从AV_String的取值

                        string rev3 = rev;
                        string rev2 = rev.Replace((char)0x1C, ' ').Replace((char)0x1D, ' '); 
                        rev = rev.Replace((char)0x1C, ' ').Replace((char)0x1D, ' ');



                        if (CheckPrintObject.nCheckFlag == 1)//如果是在检查行程单打印Eric
                            CheckPrintObject.ReceiveData(rev);//调用检查函数Eric
                        if (!bEagleEtermRet)
                        {
                            //对多包的组合处理B:
                            EagleAPI.LogWrite("没有结束符,接下一个包!");
                            if (!bCurrentBuffHasEndFlag) goto goon;

                        }
                        UseOnceClass.LastBuff = "";
                        if (Model.Tpr.running && buffer0 == 3) Model.Tpr.retstring = AV_String;
                        if (eTicket.etSubmitClass.running && buffer0 == 7)
                        {
                            eTicket.etSubmitClass.retstring = AV_String;
                            goto goon;
                        }
                        if (rev.IndexOf("ENSURE XMIT MODE IS VAR AND USE REF: TO REFRESH SCREEN") >= 0)
                        {
                            MessageBox.Show("ENSURE XMIT MODE IS VAR AND USE REF: TO REFRESH SCREEN\r\r请输入CVOF:");
                            goto goon;
                        }
                        if (rev.IndexOf("SESSION PATH DOWN") > 0)
                        {
                            goto goon;
                        }
                        //rev = mystring.trim(rev);

                        //替换掉空格+回车及回车+回车  etermagent
                        rev = rev.Replace(new string(new char[] { (char)0x0a, (char)0x10 }), "");
                        string strBeReplace = new string(new char[] { (char)0x20, (char)0x0D });
                        while (rev.IndexOf(strBeReplace) > 0)
                            rev = rev.Replace(strBeReplace, new string(new char[] { (char)0x0D }));
                        strBeReplace = "\r\n\r\n";
                        while (rev.IndexOf(strBeReplace) > 0)
                            rev = rev.Replace(strBeReplace, "\r\n");
                        rev = rev.Replace("\n\n", "\n");
                        AV_String = rev;
                        ///////////////////////////////////////////新扣款////////////////////////////////////
                        #region 新扣款黑屏显示
                        //if (GlobalVar.b_rt)
                        //{
                        //    try
                        //    {
                        //        newdefee.strRTReturn = rev;
                        //        return;
                        //    }
                        //    catch (Exception eae)
                        //    {
                        //        //AppendEditorText(eae.Message);
                        //        //return;
                        //    }
                        //}
                        /*
                        //if (GlobalVar.b_getfc)
                        //{
                        //    try
                        //    {
                        //        newdefee.strPATReturn = rev;
                        //        newdefee.nGetFcFlag = 1;
                        //        return;
                        //    }
                        //    catch (Exception eae)
                        //    {
                        //        AppendEditorText(eae.Message);
                        //        return;
                        //    }
                        //}
                        //if (GlobalVar.b_getchildfc)
                        //{
                        //    try
                        //    {
                        //        newdefee.strPATReturn = rev;
                        //        newdefee.nGetFcFlag = 2;
                        //        return;
                        //    }
                        //    catch (Exception eae)
                        //    {
                        //        AppendEditorText(eae.Message);
                        //        return;
                        //    }
                        //}
                        */
                        #endregion
                        ///////////////////////////////////////////新扣款////////////////////////////////////
                        #region PNR状态提交，完全根据返回结果处理数据
                        {
                            try
                            {
                                pnr_statistics ps = new pnr_statistics();
                                ps.str_analysis = rev;
                                Thread thSubmitPnr = new Thread(new ThreadStart(ps.submit1));
                                thSubmitPnr.Start();
                            }
                            catch { };
                        }
                        #endregion


                        #region 再处理返回结果
                        string replaceStr = new string(new char[] { (char)0x1E, (char)0x1B });
                        rev = rev.Replace(replaceStr, ">");
                        rev = rev.Replace((char)0x1E, '>');
                        replaceStr = new string(new char[] { (char)0x62, (char)0x03 });
                        rev = rev.Replace(">" + replaceStr, ">");
                        rev = rev.Replace("   \r\n", "\r\n");
                        rev = rev.Replace("  \r\n", "\r\n");
                        rev = rev.Substring(0, rev.Length - 2);//减去一个回车，减去后最后若不是换行，则还得增加
                        #endregion

                        #region 保存rtPnr返回结果buffer0 == 0x03在03下使用
                        if ((log.strSend.ToLower().IndexOf("rt") == 0
                            || log.strSend.ToLower().IndexOf("fn") == 0)
                            && buffer0 == 0x03) GlobalVar.strRtPnrResult = rev;

                        #endregion

                        //本地日志文件
                        EagleAPI.LogWrite("返回类型" + buffer0.ToString() + "\r\n" + rev);

                        if (GlobalVar.b_pnCommand) connect_4_Command.ReceiveString += rev;
                        else connect_4_Command.ReceiveString = rev;

                        EagleAPI ea = new EagleAPI();
                        if (EagleAPI.substring(rev, rev.Length - 1, 1) != "\n") rev += "\n";

                        #region rt,detr取客票信息
                        if (log.strSend.ToLower().IndexOf("rt") == 0 || log.strSend.ToLower().IndexOf("i~rt") == 0 && log.strSend.Length >= 7)
                        {
                            try
                            {
                                GlobalVar.newEticket.SetVarsByRt("\n" + rev);
                            }
                            catch
                            {
                            }
                        }
                        else if (log.strSend.ToLower().IndexOf("detr") == 0 || log.strSend.ToLower().IndexOf("i~detr") == 0 && log.strSend.Length <= 22)//detr:tn/14
                        {
                            try
                            {
                                GlobalVar.newEticket.SetVarsByDetr(rev);
                            }
                            catch
                            {
                            }
                        }
                        if (GlobalVar.b_erp_机票录入单)
                        {
                            try
                            {
                                //GlobalVar.newEticket.SetErp();
                            }
                            catch
                            {
                                MessageBox.Show("ERP插件不正确！");
                            }
                        }
                        #endregion

                        #region 返回结果在黑屏中显示或给对话框
                        else
                        {
                            if (buffer0 == 0x07)
                            {
                                if (PrintReceipt.opened) PrintReceipt.ReturnString = rev;
                                receiveEvent.Set();
                                goto goon;
                            }

                            if (Model.md.b_0FN)
                                rev = EagleAPI.CloseFnItem(rev);

                            if (!PrintWindowOpen || !Model.md.b_004 &&
                                !(PrintTicket.opened || PrintReceipt.opened || PrintReceipt.IsPrinting || PrintReceipt.IsRemoving
                                || PrintHyx.PrintPICC.b_opened || PrintHyx.PrintPICC2.b_opened || PrintHyx.Yongan.b_opened || PrintHyx.NewChina.b_opened
                                || PrintHyx.SinoSafe.b_opened || PrintHyx.ChinaLife.b_opened || PrintHyx.DuBang01.b_opened || PrintHyx.DuBang02.b_opened
                                || PrintHyx.AirCode.b_opened || BookSimple.SubmitPnr.opened || NetworkSetup.Quenes.b_opened || PrintHyx.PingAn01.b_opened || PrintHyx.Insurance.b_opened))
                            {


                                string addrev = "\r\n" + rev3;
                                addrev = mystring.trim(addrev);
                                if (mystring.right(addrev, 1) == ">")
                                    AppendEditorText("\r\n" + addrev);
                                else
                                    AppendEditorText("\r\n" + rev3 + ">");
                                //测试用AppendEditorText("\r\nEric测试：\t"+EagleAPI.etstatic.Pnr+"\t"+EagleAPI.etstatic.State+"\t"+EagleAPI.etstatic.Passengers+"\t"+EagleAPI.etstatic.TotalFC+"\r\n");
                                if (NetworkSetup.Quenes.b_opened) NetworkSetup.Quenes.returnstring = rev;

                                if (GlobalVar.b_GetPatItem) GlobalVar.strPatItem = rev.Substring(rev.LastIndexOf(">") + 1);
                                if (BookTicket.b_bookticket_提取)
                                {
                                    MessageBox.Show(rev);
                                    BookTicket.b_bookticket_提取 = false;
                                }
                                //傻瓜版
                                if (BookTicket.b_BookWndOpen && BookTicket.context.WindowState != FormWindowState.Minimized)
                                {
                                    BookTicket.stringDisplay = rev;
                                    if ((BookTicket.b_book || BookTicket.b_BookTicketAv) && AV_String != null)
                                    {
                                        if (m_Editor.InvokeRequired)
                                        {
                                            EventHandler eh = new EventHandler(clearedit);
                                            RichTextBox rtb = m_Editor;
                                            m_Editor.Invoke(eh, new object[] { rtb, EventArgs.Empty });
                                        }
                                        else
                                        {//如果NKG则不清
                                            if (!GlobalVar.gbIsNkgFunctions)
                                                m_Editor.Clear();
                                        }
                                        //if (AV_String.IndexOf("NO ROUTING") > -1)
                                        //{
                                        //    MessageBox.Show("没有相应航线或航班！");
                                        //}
                                        //else
                                        {
                                            AV_String = rev;
                                            if (!BookTicket.b_bookticket_fd) BookTicket.ret_one = AV_String;//未经再处理的rev，用于简版
                                            else BookTicket.bookticket_fd_return = AV_String;//未经再处理的rev，用于简版
                                        }
                                    }
                                    else
                                    {
                                        //BookTicket.stringDisplay = rev;
                                    }
                                }
                            }
                            else
                            {
                                bool b = false;
                                if (AV_String != null)
                                {

                                    if (BookSimple.SubmitPnr.opened) { BookSimple.SubmitPnr.ReturnString = rev; b = true; }
                                    if (PrintTicket.opened) { PrintTicket.ReturnString = rev; b = true; }
                                    if (PrintReceipt.opened) { PrintReceipt.ReturnString = rev; b = true; }
                                    if (PrintReceipt.IsPrinting) { PrintReceipt.str_printreceipt = rev; b = true; }
                                    if (PrintReceipt.IsRemoving) { PrintReceipt.str_removereceipt = rev; b = true; }
                                    if (PrintHyx.PrintPICC.b_opened) { PrintHyx.PrintPICC.returnstring = rev; b = true; }
                                    if (PrintHyx.PrintPICC2.b_opened) { PrintHyx.PrintPICC2.returnstring = rev; b = true; }
                                    if (PrintHyx.Yongan.b_opened) { PrintHyx.Yongan.returnstring = rev; b = true; }
                                    if (PrintHyx.NewChina.b_opened) { PrintHyx.NewChina.returnstring = rev; b = true; }
                                    if (PrintHyx.SinoSafe.b_opened) { PrintHyx.SinoSafe.returnstring = rev; b = true; }
                                    if (PrintHyx.ChinaLife.b_opened) { PrintHyx.ChinaLife.returnstring = rev; b = true; }
                                    if (PrintHyx.DuBang01.b_opened) { PrintHyx.DuBang01.returnstring = rev; b = true; }
                                    if (PrintHyx.DuBang02.b_opened) { PrintHyx.DuBang02.returnstring = rev; b = true; }
                                    if (PrintHyx.PingAn01.b_opened) { PrintHyx.PingAn01.returnstring = rev; b = true; }
                                    if (PrintHyx.AirCode.b_opened) { PrintHyx.AirCode.returnstring = rev; b = true; }
                                    if (NetworkSetup.Quenes.b_opened) { NetworkSetup.Quenes.returnstring = rev; b = true; }
                                    if (PrintHyx.Insurance.b_opened) { PrintHyx.Insurance.returnstring = rev; b = true; }


                                    if (GlobalVar.b_GetPatItem)
                                    {
                                        CreateETicket.returnstring = rev;
                                        GlobalVar.strPatItem = rev.Substring(rev.LastIndexOf(">") + 1); AppendEditorText("\r\n" + rev + ">");
                                        CreateETicket.retstring = rev;
                                        b = true;
                                    }
                                }
                                if (!b)
                                {
                                    string addrev = "\r\n" + rev3;
                                    addrev = mystring.trim(addrev);
                                    if (mystring.right(addrev, 1) == ">")
                                        AppendEditorText(addrev);
                                    else
                                        AppendEditorText("\r\n" + rev3 + ">");
                                }
                            }
                            #region//处理扣款,取要扣的金额
                            //if (EagleAPI.b_etdz && 1==0)
                            //{
                            //    int iCNY = rev.IndexOf("CNY");
                            //    int iPNR = rev.IndexOf(EagleAPI.etstatic.Pnr);
                            //    //etdz指令返回了价钱，与电脑号，意味着etdz成功，钱够与不够，都应扣款
                            //    if (iCNY > -1 && iPNR > iCNY)
                            //    {
                            //        iCNY += 3;
                            //        int iPOINT = rev.IndexOf(".", iCNY);
                            //        if(iPOINT >-1)
                            //        {
                            //            string mm = rev.Substring(iCNY, iPOINT - iCNY).Trim();
                            //            try
                            //            {
                            //                EagleAPI.f_fc = float.Parse(mm);
                            //                if (float.Parse(GlobalVar.f_CurMoney) >= EagleAPI.f_fc) EagleAPI.b_enoughMoney = true;
                            //            }
                            //            catch
                            //            {
                            //                EagleAPI.b_etdz = false;
                            //                EagleAPI.b_enoughMoney = false;
                            //                EagleAPI.b_endbook = false;
                            //                AppendEditorText("票价错误！\r>");
                            //            }
                            //            if (EagleAPI.b_enoughMoney)//符合扣款条件
                            //            {
                            //                WS.egws ws = new WS.egws(GlobalVar.WebServer);

                            //                NewPara np = new NewPara();
                            //                np.AddPara("cm", "DecFee");
                            //                np.AddPara("UserName", GlobalVar.loginName);
                            //                np.AddPara("TicketPrice", EagleAPI.f_fc.ToString("f2"));
                            //                string strReq = np.GetXML();
                            //                string strRet = ws.getEgSoap(strReq);
                            //                DecFee_Class dc = new DecFee_Class(strRet);
                            //                if (dc.cm == "RetDecFee" && dc.decstat == "DecSucc")
                            //                {//扣款成功，上传电子客票信息
                            //                    GlobalVar.f_CurMoney = dc.money;
                            //                    AppendEditorText("扣款成功，您的帐户余额为：￥" + dc.money + "\r>");
                            //                    AppendEditorText("正在传输电子客票信息……\r>");
                            //                    EagleAPI.b_etdz = false;
                            //                    EagleAPI.b_SubmitETicket = true;
                            //                    //ea.SetetStatic();
                            //                    EagleAPI.EagleSendCmd("@");
                            //                }
                            //                else
                            //                {//扣款失败
                            //                    AppendEditorText("扣款失败！将取消您的操作！\r>");
                            //                }

                            //            }
                            //            if (float.Parse(GlobalVar.f_CurMoney) < EagleAPI.f_fc)
                            //            {
                            //                //余额不足
                            //                EagleAPI.b_etdz = false;
                            //                EagleAPI.b_enoughMoney = false;
                            //                EagleAPI.b_endbook = false;
                            //                AppendEditorText("您的帐户余额不足，不能订电子客票！\r>");
                            //                EagleAPI.EagleSendCmd("i");
                            //            }
                            //            else
                            //            {
                            //                EagleAPI.b_enoughMoney = true;
                            //            }
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                        #endregion

                        if (GlobalVar.bListenCw)
                            GlobalVar.commServer.Send(rev);
                        if (GlobalVar.bListenCw)
                            GlobalVar.commServer.Send(rev);
                        {
                            //检查是否si未登录
                            SIGN_IN_FIRST(rev2);

                        }
                        //{//下面一段位置必须放在最后面
                        //    System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection();
                        //    ListView lview = (ListView)m_lvLowest;
                        //    int price = 0;
                        //    int distance = 0;
                        //    //显示到最低价与返点列表
                        //    if (lview.InvokeRequired)
                        //    {
                        //        lvLowestClear dg = new lvLowestClear(lview.Items.Clear);
                        //        lview.Invoke(dg);
                        //    }
                        //    else
                        //    {
                        //        lview.Items.Clear();
                        //    }
                        //    EagleExtension.EagleExtension.AvResultToListView_Lowest(rev2, cn, "", GlobalVar.WebServer, lview, GlobalVar.loginName
                        //        , ref price, ref distance);
                        //    lview = (ListView)m_lvGroup;
                        //    //显示散拼列表
                        //    try
                        //    {
                        //        if (lview.InvokeRequired)
                        //        {
                        //            listGroupResult lgr = EagleExtension.EagleExtension.GroupResultToListView_Group;
                        //            lgr.Invoke(GlobalVar.loginName, 'A', GlobalVar.WebServer, rev2, lview);

                        //        }
                        //        else
                        //        {
                        //            EagleExtension.EagleExtension.GroupResultToListView_Group
                        //                (GlobalVar.loginName, 'A', GlobalVar.WebServer, rev2, lview);
                        //        }
                        //    }
                        //    catch
                        //    {
                        //    }
                        //    lview = (ListView)m_lvSpec;
                        //    ListView lview2 = (ListView)m_lvSpec2;
                        //    //显示固定与浮动列表
                        //    EagleExtension.EagleExtension.SpecTickResultToListView_Spec(rev2, GlobalVar.WebServer, lview, lview2, price);
                        //}

                    }

                    #endregion
                    #region else if (buffer[0] == 0x04 && buffer[1] == 0x10)//保持连接返回，超时信息处理
                    else if (buffer[0] == 0x04 && buffer[1] == 0x10)//保持连接返回，超时信息处理
                    {
                        //超时协议体第13,14个字节
                        if (buffer.Length <= 12)
                        {
                            //if (GlobalVar.b_IsKick)
                            {
                                this.m_Editor.Dispose();
                                this.DisConnect();
                                if (!GlobalVar.bSwiching)
                                {
                                    MessageBox.Show("您的帐号在别处重复登陆", "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    Application.Exit();
                                }
                                else
                                {
                                    GlobalVar.bSwiching = false;
                                }
                            }
                            //else
                            //    GlobalVar.b_IsKick = true;
                        }
                        else
                        {
                            if (buffer[12] == 0x00 && buffer[13] == 0x00)
                            {

                                //AppendEditorText(GlobalVar.Notice + "连接服务器超时！开始尝试重新连接。。。\r>");
                                //if (!Model.md.b_004) MessageBox.Show("连接服务器超时！开始尝试重新连接。。。");
                                m_Socket.Close();
                                connect_5_ShakeHand(m_ServerInfo);
                                EagleAPI.SpecifyPassport();

                                timer.Start();
                            }
                            if (buffer[12] == 0x01 && buffer[13] == 0x00)
                            {
                                AppendEditorText(GlobalVar.Notice + "服务器繁忙！\r>");
                                if (!Model.md.b_004) MessageBox.Show("服务器繁忙！");//连服务器排队超时！请尝试重新操作！");
                            }
                            if (buffer[12] == 0x02 && buffer[13] == 0x00)
                            {
                                AppendEditorText(GlobalVar.Notice + "民航处理指令超时！\r>");
                                if (!Model.md.b_004) MessageBox.Show("民航处理指令超时！");
                            }
                        }


                    }
                    #endregion
                    #region else if (buffer[0] == 0x06 && buffer[1] == 0x10)提示新订单
                    else if (buffer[0] == 0x06 && buffer[1] == 0x10)
                    {
                        if (GlobalVar.b_提示新订单)
                        {
                            //MessageBox.Show("有新订单！");
                            m_notifyIcon.start("6:有新订单\r\n");
                        }
                    }
                    else if (buffer[0] == 0x09 && buffer[1] == 0x10)
                    {

                        //if (DialogResult.Yes == MessageBox.Show("收到申请舱位消息！是否查看？", "", MessageBoxButtons.YesNo))
                        {
                            string s = "";
                            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(GlobalVar.WebServer);
                            //kf.SpecialTicketAppliedNoHandleToDisplay(ref s);
                            m_notifyIcon.start("9:收到申请舱位消息\r\n" + s);
                        }
                    }
                    else if (buffer[0] == 0x0A && buffer[1] == 0x10)
                    {
                        //MessageBox.Show("收到申请舱位被处理消息，请查看！");
                        EagleProtocal.PACKET_PROMOPT_FINISH_APPLY_RESULT packet = new EagleProtocal.PACKET_PROMOPT_FINISH_APPLY_RESULT();
                        packet.FromBytes(buffer);
                        m_notifyIcon.start("A:收到申请舱位被处理消息\r\n" + packet.content);
                    }
                    #endregion
                    #region else if (buffer[0] == 0x08 && buffer[1] == 0x10)//独占配置
                    else if (buffer[0] == 0x08 && buffer[1] == 0x10)
                    {
                        //独占配置成功，启动独占定时器，30秒内没有动作，则释放，切换配置之后，取消独占功能，全部配置不能独占

                        bool btemp = false;
                        try
                        {
                            if (buffer[14] == 1) btemp = true;
                            if (buffer[14] == 2)
                            {
                                AppendEditorText("独占配置已到时间.\r>");
                            }
                        }
                        catch
                        {
                            AppendEditorText("独占时发生错误\r>");
                            btemp = false;
                        }
                        GlobalVar.bUsingConfigLonely = btemp;

                        if (btemp)
                        {
                            AppendEditorText("独占配置成功，开始计时!30秒内无输入，则释放配置!\r>连续按2下ALT键还原到30秒\r>");
                            //置复杂模式
                            GlobalVar.commandSendtype = GlobalVar.CommandSendType.B;
                            //开始计时
                            GlobalVar.dtLonelyStart = DateTime.Now;
                            if (Model.Tpr.b启动独占配置)
                            {
                                Model.Tpr.run();
                            }
                        }
                        else
                        {
                            AppendEditorText("独占配置失败,可能有人正在独占,请重试\r>");
                        }
                    }
                    #endregion
                    else if (buffer[0] == 0x00 && buffer[1] == 0x00)
                    {
                        MessageBox.Show("服务器断开了您的连接!");
                        Application.Exit();
                    }
                goon:
                    return;// receiveEvent.Set();
                }
            }
            catch(Exception ee)
            {
                EagleString.EagleFileIO.LogWrite(ee.ToString());
            }
        }

        delegate void lvLowestClear();


        
        void clearedit(object sender,EventArgs e)
        {
            if(!GlobalVar .gbIsNkgFunctions )
            m_Editor.Clear();
        }
        private void ReceiveData()
        {
            while (true)//无限循环，保持监听
            {
                receiveEvent.Reset();//unSignal 所有线程WaitOne处等信号
                if (m_Socket != null && m_Socket.Connected)
                {
                    StateObject stateObject = new StateObject();
                    stateObject.workSocket = m_Socket;
                    //NetworkStream stream = new NetworkStream(m_Socket);
                    try
                    {

                        //stream.BeginRead(stateObject.buffer, 0, MaxBuffer, new AsyncCallback(ReceiveData), stateObject);
                        m_Socket.BeginReceive(stateObject.buffer, 0, MaxBuffer, SocketFlags.None, ReceiveData, stateObject);
                        //AppendEditorText("接收……\n");
                        receiveEvent.WaitOne();//若没有收到数据，则阻塞
                    }
                    catch(Exception e)
                    {
                        //AppendEditorText("\n与服务器的连接已断开！\n>");
                        EagleAPI.LogWrite("ReceiveData : " + e.ToString());//“远程主机强迫关闭了一个现有的连接。”
                        //this.DisconnectServer();
                        break;
                    }
                }
                else//在socket没有connect成功前，自循环
                {
                    Thread.Sleep(500);
                }
            }
        
        }

        private void BeginTransaction()
        {
            BeginTransaction(false);
        }

        private void BeginTransaction(bool isPrintTransaction)
        {
            transactionState = false;

            if (this.m_Socket == null || (this.m_Socket != null && !this.m_Socket.Connected))
            {
                AppendEditorText("\n开始事务失败，与服务器的连接已中断，请重新连接服务器。\n");
                throw new Exception("开始事务失败，与服务器的连接已中断，请重新连接服务器。");
            }
           
            transactionState = true;
            
            transactionEvent.Reset();

            if (isPrintTransaction)
                m_Socket.Send(System.Text.Encoding.ASCII.GetBytes("0131"));
            else
                m_Socket.Send(System.Text.Encoding.ASCII.GetBytes("0130"));

            System.Timers.Timer timer = new System.Timers.Timer(); 
            timer.Interval = 3000;
            timer.Elapsed  +=new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start(); 

            transactionEvent.WaitOne();
            timer.Stop();
  
            if (!transactionState)
            {
                AppendEditorText("\n开始事务失败\n>");
                throw new Exception("开始事务失败"); 
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                transactionState = false;
                transactionEvent.Set();
            }
            catch
            {
            }
        }

        

        private void EndTransaction()
        {
            if (this.m_Socket == null || (this.m_Socket != null && !this.m_Socket.Connected))
            {
                AppendEditorText("终止事务失败，与服务器的连接已中断，请重新连接服务器。\n");
                throw new Exception("终止事务失败，与服务器的连接已中断，请重新连接服务器。");
            }

            //transactionEvent.Reset();
            m_Socket.Send(System.Text.Encoding.ASCII.GetBytes("015"));
            //transactionEvent.WaitOne();SM0N8
        }

        #if !Sync
        internal class StateObject
        {
            public Socket workSocket = null;
            public byte[] buffer = new byte[MaxBuffer];
            public StringBuilder sb = new StringBuilder();
            public StateObject()
            {
            }
        }
        #endif
    }
    #endregion
}
