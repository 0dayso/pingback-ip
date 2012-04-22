#define receipt_     //行程单打印程序专用

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Management;
using ePlus.Model;
using System.Threading;
using System.Xml;
using mshtml;
using gs.para;
using System.Net;
using System.Net.Sockets;
using DevComponents.DotNetBar;

namespace ePlus
{
    public partial class frmMain : Form
    {
        public static bool b_s1;
        public static bool b_s2;
        public static bool b_s3;

        /// <summary>
        /// 新订单编辑页面

        /// </summary>
        NewOrderWeb newOrderWeb;
        /// <summary>
        /// B2C 软电话 UDP 监听，退出时记得关闭
        /// </summary>
        UdpClient CtiUdpListening;
        /// <summary>
        /// 呼叫中心类型,板卡?交换机?USB模式?
        /// </summary>
        XMLConfig.CtiTypeEnum CtiType = XMLConfig.CtiTypeEnum.EGPlug;

        #region hotkey定义并处理热键

        Options.otherClass.Hotkey hotkey;// = new Options.otherClass.Hotkey(this.Handle);
        int hk_ibe = 0;
        int hk_eterm = 0;
        int hk_specify = 0;
        int hk_sd2ss = 0;
        int hk_togroup = 0;
        int hk_book = 0;
        int hk_blackpolicy = 0;
        int hk_eplus = 0;
        public void OnHotkey(int HotkeyID)
        {
            if (HotkeyID == hk_ibe)
                BookTicket.bIbe = !BookTicket.bIbe;
            if (HotkeyID == hk_eterm)
                stiETERM_Click(null, null);
            if (HotkeyID == hk_specify)
                GlobalVar.b_UseSpecified强制 = !GlobalVar.b_UseSpecified强制;
            if (HotkeyID == hk_sd2ss)
                GlobalVar.b_sd2ss = !GlobalVar.b_sd2ss;
            if (HotkeyID == hk_togroup)
                GlobalVar.IsListGroupTicket = !GlobalVar.IsListGroupTicket;
            if (HotkeyID == hk_book)
                GlobalVar.b_提示新订单 = !GlobalVar.b_提示新订单;
            if (HotkeyID == hk_blackpolicy)
                GlobalVar2.gbDisplayPolicy = !GlobalVar2.gbDisplayPolicy;
            if (HotkeyID == hk_eplus)
                GlobalVar2.gbEplusStyle = !GlobalVar2.gbEplusStyle;
        }
        #endregion
        public frmMain()
        {
            InitializeComponent();
            #region hotkey初始化热键

            //try
            //{
                
            //    hotkey = new Options.otherClass.Hotkey(this.Handle);

            //    hk_ibe = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F2, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_eterm = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F7, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_specify = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F6, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_sd2ss = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F5, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_togroup = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F3, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_book = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F4, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_blackpolicy = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F1, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hk_eplus = hotkey.RegisterHotkey(System.Windows.Forms.Keys.F8, Options.otherClass.Hotkey.KeyFlags.MOD_SHIFT);
            //    hotkey.OnHotkey += new Options.otherClass.HotkeyEventHandler(OnHotkey);
            //}
            //catch
            //{
            //}
            #endregion
            windowSwitch = new WindowSwitch(this.tabControl);
            windowSwitch.New();

            InitializeStates();

            b_s1 = mi_style1.Checked;
            b_s2 = mi_style2.Checked;
            b_s3 = mi_style3.Checked;
            CheckForIllegalCrossThreadCalls = false;

            if (GlobalVar2.gbUserModel != 1)
            {
                this.toolStripButtonNewOrder.Visible = false;
                this.tsbTravel.Visible = false;
            }

            if (string.IsNullOrEmpty(GlobalVar.DaLianCTIPhoneNumber))
            {
                //this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer1.SplitterDistance = 28;
                this.toolStripButtonDaLianCtiMessage.Visible = false;
                this.toolStripButtonDaLianCtiPlayback.Visible = false;
            }
            else
            {
                Thread th = new Thread(new ThreadStart(ShowDaLianCTI));
                th.Start();
            }

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            wf_int();
            
        }

        #region 自定义过程

        /// <summary>
        /// 运行一个外部应用程序

        /// </summary>
        /// <param name="fileName">文件名</param>
        private void RunProgram(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
                throw new Exception(Properties.Resources.NoThisProgram);

            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.Start();
        }
        
        /// <summary>
        /// 获取功能键状态，并反映到工具栏上。

        /// </summary>
        private void Check_FuncKey()
        {
            Microsoft.VisualBasic.Devices.Keyboard keyboard = new Microsoft.VisualBasic.Devices.Keyboard();
            this.statusBar.Items[3].Text = keyboard.CapsLock ? "大写" : "小写";
            
            //this.statusBar.Items[4].Text = keyboard.NumLock ? "Num" : "";
        }
        #endregion 

        #region 事件处理
        private void miCalc_Click(object sender, EventArgs e)
        {
            RunProgram(Environment.SystemDirectory + "\\calc.exe");
        }

        private void miNotepad_Click(object sender, EventArgs e)
        {
            RunProgram(Environment.SystemDirectory + "\\notepad.exe"); 
        }

        private void miPainter_Click(object sender, EventArgs e)
        {
            RunProgram(Environment.SystemDirectory + "\\mspaint.exe");
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }
 
        private void trafficAmount()
        {
            int[] amount = EagleString.EagleFileIO.TrafficAmountRead();
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(GlobalVar.WebServer);
            bool btraf = false;
            if (amount[0] > 0)
            {
                kf.updateutraffic(GlobalVar.loginName, 1, amount[0], ref btraf);
                if (btraf)
                {
                    EagleString.EagleFileIO.TrafficAmount(1, -amount[0]);
                }
            }
            if (amount[1] > 0)
            {
                kf.updateutraffic(GlobalVar.loginName, 2, amount[1], ref btraf);
                if (btraf)
                {
                    EagleString.EagleFileIO.TrafficAmount(2, -amount[1]);
                }
            }
        }

        void update_notice_scroll()
        {
            try
            {
                Notice nt = new Notice();
                string tempnt = nt.get_notice_scroll(BookTicket.b_BookWndOpen ? "1" : "0");
            }
            catch
            {
            }

        }
        private void lblNotice_Click(object sender, EventArgs e)
        {
            Thread thTemp = new Thread(new ThreadStart(update_notice_scroll));
            thTemp.Start();// update_notice_scroll();
        }
        #endregion 

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            Check_FuncKey();
        }

        private void lblNotice_SlideOver(object sender, EventArgs e)
        {
           //测试用代码段//

            //lblNotice.Text = infoList[index];// infoList[index++];
            //if (index >= infoList.Length)
            //    index = 0;
            ///////////////
        }

        void ReadCmdSendTypeFromOptionsTxt()//在FormLoad时读一次，在打开Options并保存后读一次！
        {
            b_s1 = true;
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            windowSwitch.New((new ePlus.Properties.Settings()).JumpToAfterCreate);

            SetButtonCloseEnable(windowSwitch.Count > 0);
            SetButtonConnectEnable(windowSwitch.Count > 0);

            SetButtonNewEnable(windowSwitch.Count < WindowSwitch.MaxWindowNum);  
        }

        void frmMain_EditorCreating(object sender, EditorEventArgs e)
        {
            LoadEditorConfig(e.Editor);
            e.Editor.SelectionChanged += new EventHandler(Editor_SelectionChanged);
            if (e.Editor.IsHandleCreated)
            {
                //为何该语句和下面的语句一起出现的时候,会导致粘贴快捷键失效!?
                //SetButtonPasteEnable(e.Editor.CanPaste(DataFormats.GetFormat(DataFormats.Text)));
            }
        }

        void Editor_SelectionChanged(object sender, EventArgs e)
        {
            RichTextBox editor = (sender as RichTextBox);
            SetButtonCutEnable(editor.SelectionLength > 0);
            SetButtonCopyEnable(editor.SelectionLength > 0);

            if (editor.IsHandleCreated)
            {
                //为何该语句和上面的语句一起出现的时候,会导致粘贴快捷键失效!?
                //SetButtonPasteEnable(editor.CanPaste(DataFormats.GetFormat(DataFormats.Text)));   
            }

        }

        #region Private Members
        static public WindowSwitch windowSwitch = null;
        static public System.Windows.Forms.TabControl st_tabControl;
        

        private void miClose_Click(object sender, EventArgs e)
        {
            windowSwitch.Remove();

            SetButtonCloseEnable(windowSwitch.Count > 0);
            SetButtonConnectEnable(windowSwitch.Count > 0);

            SetButtonNewEnable(windowSwitch.Count < WindowSwitch.MaxWindowNum);
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();  
        }

        public void connect_2(ServerInfo serverInfo)
        {
            WindowInfo wndInfo = windowSwitch[0];//[tabControl.SelectedIndex];
            st_tabControl = tabControl;

            wndInfo.EditorCreating += new EventHandler<EditorEventArgs>(frmMain_EditorCreating);
            wndInfo.AfterConnect += new EventHandler(wndInfo_AfterConnect);
            wndInfo.AfterDisConnect += new EventHandler(wndInfo_AfterDisConnect);

            wndInfo.connect_3(windowSwitch.ActiveWindow,serverInfo);
            //windowSwitch.ActiveWindow.Text = serverInfo.Name + "(黑屏)";// +serverInfo.Description + ")";//commentted by king
    
            SetButtonDisConnectEnable(wndInfo.Connected);
            SetButtonConnectEnable(!wndInfo.Connected);
        }

        void wndInfo_AfterDisConnect(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            SetButtonDisConnectEnable(false);
            SetButtonConnectEnable(true);
            SetEditButtonEnable(false); 
        }

        private void btnConnect_ButtonClick(object sender, EventArgs e)
        {
            connect_1();
        }

        void connect_1()
        {
            Options.GlobalVar.IsConnecting = true;
            //this.Text = "正在连接配置服务器：" + GlobalVar.loginLC.SrvName + "，请稍等……";//commentted by king
            EagleAPI.LogWrite("connect start");//时间间隔

            ServerInfo serverInfo = new ServerInfo();
            EagleAPI.CLEARCMDLIST(3);
            EagleAPI.CLEARCMDLIST(7);
            //if (btnConnect.DropDownItems.Count < 1)
            //     RefreshServerList();

            //foreach (ToolStripItem item in btnConnect.DropDownItems)
            //{
            //    if (((ToolStripMenuItem)item).Checked)
            //    {
            //        serverInfo = (ServerInfo)item.Tag;
            //        break;
            //    }
            //}
            serverInfo.Edit();
            serverInfo.Address = GlobalVar.loginLC.SrvIP;//下拉列表里面的服务器地址，被替换成当前配置（故上面的循环是多余的） commentted by chenqj
            serverInfo.Port = GlobalVar.loginLC.SrvPort;

            if (serverInfo != null)
                connect_2(serverInfo);
        }

        void wndInfo_AfterConnect(object sender, EventArgs e)
        {
            //SetButtonPasteEnable(true); 
            SetButtonSOEEnable(true);
            //LoadEditorConfig(e.Editor);
            //LoadEditorConfig(windowSwitch[windowSwitch.ActiveWindowIndex].Editor);
        }

        private void InitializeStates()
        {
            SetButtonDisConnectEnable(false);

            SetEditButtonEnable(false);

            SetButtonSOEEnable(false);
            //firstSeparator.Visible = btnNew.Visible = btnClose.Visible = 
            miNew.Visible = miClose.Visible = firstMenuSeparator.Visible = WindowSwitch.MaxWindowNum > 1;            
        }

        private void LoadEditorConfig(RichTextBox editor)
        {
            Properties.Settings setting = new ePlus.Properties.Settings();

            editor.BackColor = setting.EditorBackColor;
            editor.ForeColor = setting.EditorForeColor;
            editor.Font = setting.EditorFont;

            editor.SelectionBackColor = editor.BackColor;
            editor.SelectionColor = editor.ForeColor; 
            editor.SelectionFont = editor.Font;

            editor.LanguageOption &= ~RichTextBoxLanguageOptions.DualFont;   
        }
        #endregion

        private void btnCut_Click(object sender, EventArgs e)
        {
            string temp = windowSwitch[0].Editor.SelectedText;
            if (windowSwitch.Count > 0 && tabControl.SelectedIndex > -1)
            {
                windowSwitch[0].Editor.Cut(); 
            }
            //Clipboard.SetDataObject(temp);
            
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            //if (windowSwitch.Count > 0 && tabControl.SelectedIndex > -1)
            //{
            //    windowSwitch[tabControl.SelectedIndex].Editor.Copy(); 
            //}
            Clipboard.SetDataObject(windowSwitch[0].Editor.SelectedText);
            //string temp = Clipboard.GetText();
            //Clipboard.SetText(temp);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (windowSwitch.Count > 0 && tabControl.SelectedIndex > -1)
            {
                try
                {
                    Clipboard.SetText(Clipboard.GetText(TextDataFormat.Text), TextDataFormat.Text);
                    windowSwitch[tabControl.SelectedIndex].Editor.Paste();
                }
                catch
                {
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //以下程序段在加入网络功能后使用。                

            //if (tabControl.SelectedTab.Text == GlobalVar.EagleWebString)
            //    return;
            //if (tabControl.SelectedIndex > 0)
            //    return;

            //if (tabControl.SelectedTab.Text == "中文专业版" || tabControl.SelectedTab.Text == "白屏终端")
            //{
            //    this.toolStrip.Visible = false;
            //    this.MainMenu.Visible = false;
            //    this.panel.Visible = false;
            //    return;
            //}
            if (tabControl.SelectedTab.Text == "后台管理")
            {
                this.WindowState = FormWindowState.Maximized;
                WebBrowser wb = (WebBrowser)tabControl.SelectedTab.Controls[0];
                if (wb.Url == null)
                {
                    string url = "";
                    switch (Options.GlobalVar.SelectedISP)
                    {
                        case XMLConfig.ISP.ChinaTelecom:
                            url = Options.GlobalVar.IALoginUrl1;
                            break;
                        case XMLConfig.ISP.ChinaUnicom:
                            url = Options.GlobalVar.IALoginUrl2;
                            break;
                        default:
                            url = Options.GlobalVar.IALoginUrl;
                            break;
                    }

                    url += string.Format("/Default.aspx?u={0}&p={1}", Options.GlobalVar.IAUsername, Options.GlobalVar.IAPassword);
                    wb.Navigate(url);
                }
                return;
            }
            else
                this.WindowState = FormWindowState.Normal;
            //if (tabControl.SelectedIndex > -1 && windowSwitch[tabControl.SelectedIndex].Editor != null)
            //{
            //    //if (string.IsNullOrEmpty(tabControl.SelectedTab.Text))//禁止查看黑屏 by king
            //    //    return;
            //    SetButtonCutEnable(windowSwitch[tabControl.SelectedIndex].Editor.SelectionLength > 0);
            //    SetButtonCopyEnable(windowSwitch[tabControl.SelectedIndex].Editor.SelectionLength > 0);
            //    if (windowSwitch[tabControl.SelectedIndex].Editor.IsHandleCreated)
            //    {
            //        SetButtonPasteEnable(windowSwitch[tabControl.SelectedIndex].Editor.CanPaste(DataFormats.GetFormat(DataFormats.Text)));
            //    }
            //    SetButtonSOEEnable(true);
            //}
            //else
            //{
            //    SetEditButtonEnable(false);
            //    SetButtonSOEEnable(false);
            //}

            //bool connState = (tabControl.SelectedIndex > -1 && windowSwitch[tabControl.SelectedIndex].Connected);
            //SetButtonConnectEnable(!connState);
            //SetButtonDisConnectEnable(connState);  
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            WindowInfo wndInfo = frmMain.windowSwitch[0];//[frmMain.st_tabControl.SelectedIndex];

            if (wndInfo != null)
            {
                wndInfo.DisConnect();
            }
        }

        private void miOption_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm();
            if (optionForm.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    WindowInfo wndInfo = windowSwitch[i];
                    if (wndInfo.Editor != null)
                        LoadEditorConfig(wndInfo.Editor);  
                }
            }
            optionForm.Dispose();  
        }

        private void miShortKey_Click(object sender, EventArgs e)
        {
            ShortCutKeySettingsForm shortCutKeySettingsForm = new ShortCutKeySettingsForm();
            shortCutKeySettingsForm.ShowDialog();
            shortCutKeySettingsForm.Dispose();  
        }



        private void btnConnect_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //ToolStripItem item = e.ClickedItem;
            //ServerInfo serverInfo = (ServerInfo)item.Tag;
            //NewConnect(serverInfo);
            btnConnect_ButtonClick(sender, e);
            
        }

        private void btnConnect_DropDownOpening(object sender, EventArgs e)
        {
            //RefreshServerList();
        }

        /// <summary>
        /// 从数据库中获取最新的服务器信息，并放入List中。

        /// </summary>
        private void RefreshServerList()
        {
            DataTable dataTable = ShortCutKeySettingsForm.dataBaseProcess.ExcuteQuery("select * from serverinfo");

            btnConnect.DropDownItems.Clear();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];

                ServerInfo serverInfo = new ServerInfo(
                                                        long.Parse(dr["ID"].ToString()),
                                                        dr["Name"].ToString(),
                                                        dr["Address"].ToString(),
                                                        int.Parse(dr["Port"].ToString()),
                                                        dr["Descript"].ToString(),
                                                        bool.Parse(dr["AuthenticationType"].ToString()),
                                                        dr["UserName"].ToString(),
                                                        dr["Password"].ToString(),
                                                        bool.Parse(dr["Default"].ToString())
                                                      );

                ToolStripMenuItem item = new ToolStripMenuItem(); 

                item.Text = string.Format("[{0}({1})] {{{2}:{3}}} {4}", serverInfo.Name, serverInfo.UserName, serverInfo.Address, serverInfo.Port, serverInfo.Description);
                item.Checked = serverInfo.IsDefault;  
                item.Tag = serverInfo;

                btnConnect.DropDownItems.Add(item);
            }
        }

        private void miNewClient_Click(object sender, EventArgs e)
        {
            RunProgram(Application.ExecutablePath);  
        }

        private void btnSetSOE_Click(object sender, EventArgs e)
        {
            WindowInfo wndInfo = windowSwitch[0];
            if (wndInfo != null)
            {
                if (wndInfo.Editor != null)
                    wndInfo.Editor.AppendText(">");
                //else
                    //btnSetSOE.Enabled = false;
            }
        }

        private void mi_style1_Click(object sender, EventArgs e)
        {
            b_s1 = mi_style1.Checked = true;
            b_s2 = mi_style2.Checked = false;
            b_s3 = mi_style3.Checked = false;
            WindowInfo wndInfo = windowSwitch[tabControl.SelectedIndex];
            if (wndInfo != null)
            {
                if (wndInfo.Editor != null)
                    wndInfo.SendData("i");
                //else
                //    btnSetSOE.Enabled = false;
            }
        }

        private void mi_style2_Click(object sender, EventArgs e)
        {
            b_s1 = mi_style1.Checked = false;
            b_s2 = mi_style2.Checked = true;
            b_s3 = mi_style3.Checked = false;
            WindowInfo wndInfo = windowSwitch[tabControl.SelectedIndex];
            if (wndInfo != null)
            {
                if (wndInfo.Editor != null)
                    wndInfo.SendData("i");
                //else
                //    btnSetSOE.Enabled = false;
            }
        }

        private void mi_style3_Click(object sender, EventArgs e)
        {
            b_s1 = mi_style1.Checked = false;
            b_s2 = mi_style2.Checked = false;
            b_s3 = mi_style3.Checked = true;
            WindowInfo wndInfo = windowSwitch[tabControl.SelectedIndex];
            if (wndInfo != null)
            {
                if (wndInfo.Editor != null)
                    wndInfo.SendData("i");
                //else
                //    btnSetSOE.Enabled = false;
            }
        }

        private void miPrint_Click(object sender, EventArgs e)
        {
            //EagleAPI.EagleSendCmd("ig");
            PrintTicket pt = new PrintTicket();
            pt.Show();
        }
/// <summary>
/// 行程单打印按钮

/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void miPrintReceipt_Click(object sender, EventArgs e)
        {
            //EagleAPI.EagleSendCmd("ig");
#if RWY
            int index = this.tabControl.TabPages.Count;
            this.tabControl.TabPages.Add("行程单");
            PrintReceipt dlg = new PrintReceipt();
            dlg.TopLevel = false;
            dlg.FormBorderStyle = FormBorderStyle.None;
            tabControl.TabPages[index].Controls.Add(dlg);
            dlg.Show();
            tabControl.SelectedIndex = index;
            return;
#endif
            try
            {
                int count = 0;
                while (PrintReceipt.Context != null)
                {
                    count++;
                    PrintReceipt.Context.Close();
                    if (count > 5)
                    {
                        //PrintReceipt.Context = null;
                        break;
                    }
                }
                PrintReceipt.b0003 = true;
                PrintReceipt pr = new PrintReceipt();

                pr.Show();
            }
            catch
            {
            }
            
        }

        private void miPrintInsurance_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本功能已经取消！");
            //EagleAPI.EagleSendCmd("ig");
            //PrintInsurance pi = new PrintInsurance();
            //pi.Show();
        }

        private void 打印设置SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintSetup ps = new PrintSetup();
            ps.ShowDialog();
            ps.Dispose();
        }

        private void 查看日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            FileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.Filter = "所有文件(*.*)|*.*|文本文件(*.log)|*.log|TPR报表文件(*.tpr)|*.tpr";

            string path = Application.StartupPath;
            if (!Directory.Exists(path + "\\Log"))
                Directory.CreateDirectory(path + "\\Log");   
            dlg.InitialDirectory = Application.StartupPath+"\\Log";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filename = dlg.FileName;//包括了绝对路径

                EagleAPI.LogRead(filename);
            }
        }

        private void 升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!EagleAPI.SearchUpdateForm())
            //    PrintTicket.RunProgram(Application.StartupPath + "\\wsupdate.exe", null);
            //else
            //    MessageBox.Show("自动升级程序已经启动！");
        }

        private void style1_Click(object sender, EventArgs e)
        {
            mi_style1_Click(sender, e);
        }

        private void style2_Click(object sender, EventArgs e)
        {
            mi_style2_Click(sender, e);
        }

        private void style3_Click(object sender, EventArgs e)
        {
            mi_style3_Click(sender, e);
        }
        BookTicket bt;// = new BookTicket();
        private void Book_Click(object sender, EventArgs e)
        {
            
#if RWY
            if (bt == null) bt = new BookTicket();
            foreach (TabPage tp in tabControl.TabPages)
            {
                if (tp.Text == "图形界面") return;
            }
            bt.TopLevel = false;
            bt.FormBorderStyle = FormBorderStyle.None;
            int index = tabControl.TabPages.Count;
            tabControl.TabPages.Add("图形界面");
            tabControl.TabPages[index].Controls.Add(bt);
            bt.Show();
            tabControl.SelectedIndex = index;
#else
            try
            {
                bt.Show();
            }
            catch
            {
                bt = new BookTicket(); bt.Show();
            }
#endif

            //bt.TopLevel = false;
            //bt.FormBorderStyle = FormBorderStyle.None;
            //this.tabControl.TabPages.Add("中文专业版");
            //int index = tabControl.TabPages.Count - 1;
            //tabControl.TabPages[index].Controls.Add(bt);
            //bt.Show();
            //tabControl.SelectedTab = tabControl.TabPages[index];

        }

        private void miChangePassword_Click(object sender, EventArgs e)
        {
            EagleForms.General.PasswordModify pm = new EagleForms.General.PasswordModify(GlobalVar.WebServer, GlobalVar.loginName, GlobalVar.loginPassword);
            pm.ShowDialog();
            GlobalVar.loginPassword = pm.PASSWORD_NEW;
        }

        private void tsb_WebBrowser_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Add(GlobalVar.EagleWebString);            
            TabPage tp = tabControl.TabPages[tabControl.TabPages.Count - 1];
            WindowInfo wi = windowSwitch[tabControl.TabPages.Count - 1];

            WebBrowser wb = new WebBrowser();
            wb.Name = "website";
            string url = "";
            if (GlobalVar2.gbUserModel == 1)
                url = GlobalVar.WebUrl;
            else
                url = GlobalVar.WebUrl + "?user=" + GlobalVar.loginName + "&pwd=" + GlobalVar.loginPassword;
            wb.Navigate(url);
            wb.Dock = DockStyle.Fill;
            tp.SuspendLayout();
            tp.Controls.Add(wb);
            tp.ResumeLayout();
            tp.PerformLayout();
            tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
            tsb_WebBrowser.Enabled = false;

            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            
        }

        delegate void NavigateDelegate(string url);

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            
        }

        private void ｐＩＣＣToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.PrintPICC2 pp = new ePlus.PrintHyx.PrintPICC2();
            pp.Show();
        }
        //电子客票核对按钮
        private void miETMANAGE_CHECK_Click(object sender, EventArgs e)
        {
            try
            {
                if (eTicket.etSubmitClass.running) return;
                Thread et = new Thread(new ThreadStart(eTicket.etrun.runthread));
                et.Start();
            }
            catch
            {
            }
            //if (PrintReceipt.b0003)
            //{
            //    MessageBox.Show("请关闭行程单打印窗口……");
            //    return;
            //}
            //if (PrintReceipt.b0007)
            //{
            //    MessageBox.Show("电子客票提交正在后台运行中……");
            //    return;
            //}
            //hideCheckWindow();

        }
        void hideCheckWindow()
        {
            PrintReceipt.b0007 = true;
            PrintReceipt pr = new PrintReceipt();

            pr.Window_ManageET();
            pr.Text = "电子客票后台核查管理";
            //pr.ShowDialog();
            pr.backGroundSubmitEticket = true;
            pr.Visible = false;
            pr.setToSubmitForm();
            pr.Show();
            pr.Hide();

        }
        int pages = 0;
        int curpage = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            WindowInfo wndInfo = windowSwitch[0];
            string ptstr = wndInfo.Editor.SelectedText;
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontCn = new Font("system", GlobalVar.fontsize, System.Drawing.FontStyle.Regular);
            Brush ptBrush = Brushes.Black;
            e.PageSettings.Margins.Left = 0;
            e.PageSettings.Margins.Right = 0;
            e.PageSettings.Margins.Top = 0;
            e.PageSettings.Margins.Bottom = 0;
            try
            {
                string[] strings = ptstr.Split('\n');
                pages = strings.Length / 64 + 1;//页数



                string printstring = "";
                for (int i = 0; i < 64; i++)
                {
                    try
                    {
                        printstring += strings[curpage * 64 + i] + "\n";
                    }
                    catch
                    {
                        break;
                    }
                }
                curpage++;
                e.Graphics.DrawString(printstring, ptFontCn, ptBrush, 0F, 0F);
                if (curpage > pages) e.HasMorePages = false;
                else e.HasMorePages = true;
            }
            catch
            {
            }
            
        }

        public void 打印选定内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void toolStripButton_CAL_Click(object sender, EventArgs e)
        {
            this.miCalc_Click(sender, e);
        }

        private void 永安保险ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.Yongan ya = new ePlus.PrintHyx.Yongan();
            ya.ShowDialog();
            ya.Dispose();
        }

        private void 新华保险ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.NewChina nc = new ePlus.PrintHyx.NewChina();
            nc.ShowDialog();
            nc.Dispose();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            //hotkey.UnregisterHotkeys();// by chenqj
        }

        private void sD转换SSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sD转换SSToolStripMenuItem.Checked = !sD转换SSToolStripMenuItem.Checked;
            GlobalVar.b_sd2ss = sD转换SSToolStripMenuItem.Checked;
            initStatusBar();
        }
        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options.Options ot = new Options.Options();
            ot.cbIbeUse.Checked = BookTicket.bIbe;
            ot.ShowDialog();
            BookTicket.bIbe = ot.cbIbeUse.Checked;
            EagleAPI.GetPrintConfig();
            EagleAPI.GetOptions();
            ReadCmdSendTypeFromOptionsTxt();
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            connect_4_Command.PrintWindowOpen = false;
        }

        private void frmMain_Deactivate(object sender, EventArgs e)
        {
            try
            {
                connect_4_Command.PrintWindowOpen = true;
                if (BookTicket.b_BookWndOpen) connect_4_Command.PrintWindowOpen = false;
            }
            catch
            {
            }
        }

        private void 交通意外伤害保险单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.SinoSafe ss = new ePlus.PrintHyx.SinoSafe();
            ss.ShowDialog();
            ss.Dispose();
        }

        private void 航空旅客人身意外伤害保险单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.ChinaLife cl = new ePlus.PrintHyx.ChinaLife();
            cl.ShowDialog();
            cl.Dispose();
        }
        private void 航空意外保险单2008ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.ChinaLife2 cl2 = new ePlus.PrintHyx.ChinaLife2();
            cl2.ShowDialog();
            cl2.Dispose();
        }
        private void 航翼网航空意外伤害保险单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.DuBang01 dlg = new ePlus.PrintHyx.DuBang01();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void 出行无忧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.DuBang02 dlg = new ePlus.PrintHyx.DuBang02();
            dlg.ShowDialog();
            dlg.Dispose();
        }
        private void 出行乐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.DuBang02 db3 = new ePlus.PrintHyx.DuBang02();
            db3.Dubang03();
            db3.ShowDialog();

        }

        void tabControl_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }

        void tabControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (tabControl.SelectedTab.Text == "后台管理")
            //    this.tsb_WebBrowser.Enabled = true;
            //else if (tabControl.SelectedTab.Text == "旅游平台")
            //    this.tsbTravel.Enabled = true;

            //if (tabControl.TabCount > 1)
            //    tabControl.TabPages.Remove(tabControl.SelectedTab);
        }
        
        private void 常用指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 提取大编码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.AirCode ac = new ePlus.PrintHyx.AirCode();
            ac.ShowDialog();
            GlobalVar.stdRichTB.AppendText(ac.airCode);
            ac.Dispose();
        }
        private void toolStripButton_ClearQ_Click(object sender, EventArgs e)
        {//自动清Q
            NetworkSetup.Quenes dlg = new ePlus.NetworkSetup.Quenes();
            dlg.Show();
        }


        private void 周游列国_Click(object sender, EventArgs e)
        {
            PrintHyx.PingAn01 dlg = new ePlus.PrintHyx.PingAn01();
            dlg.ShowDialog();
        }

        private void 电子客票手动提交ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options.SubmitManual dlg = new Options.SubmitManual();
            dlg.ShowDialog();
            eTicket.etStatic et = new ePlus.eTicket.etStatic();
            et.Bunk1 = dlg.Bunk1;
            et.Bunk2 = dlg.Bunk2;
            et.CityPair1 = dlg.CityPair1;
            et.CityPair2 = dlg.CityPair2;
            et.Date1 = dlg.Date1;
            et.Date2 = dlg.Date2;
            et.etNumber = dlg.etNumber;
            et.FlightNumber1 = dlg.FlightNumber1;
            et.FlightNumber2 = dlg.FlightNumber2;
            et.Passengers = dlg.Passengers;
            et.Pnr = dlg.Pnr;
            //string[] sp1 ={ "<eg66>" };
            //for (int i = 0; i < GlobalVar.ipListId.Count; i++)
            //{
            //    if (GlobalVar.ipListId[i].Split(sp1, StringSplitOptions.RemoveEmptyEntries)[0] == GlobalVar.CurIPUsing)
            //    {
            //        try
            //        {
            //            et.TerminalNumber = GlobalVar.ipListId[i].Split(sp1, StringSplitOptions.RemoveEmptyEntries)[1];
            //            break;
            //        }
            //        catch { }
            //    }
            //}
            try
            {
                et.TerminalNumber = GlobalVar.officeNumberCurrent.Substring(0, 6);
            }
            catch
            {
                et.TerminalNumber = GlobalVar.officeNumberCurrent;
            }
            et.TotalFC = dlg.TotalFC;
            et.TotalTaxBuild = dlg.TotalTaxBuild;
            et.TotalTaxFuel = dlg.TotalTaxFuel;
            et.UserID = GlobalVar.loginName;
            if (et.SubmitInfo()) MessageBox.Show("成功！");
            else MessageBox.Show("失败");
        }

        private void 提交PNRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookSimple.SubmitPnr sp = new ePlus.BookSimple.SubmitPnr(EagleAPI.etstatic.Pnr,"");
            sp.Show();

        }

        private void 查看可用指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            
            GlobalVar.loginLC = new Login_Classes(lg.login());
            GetRemainMoney rm = new GetRemainMoney();
            rm.getcurmoney();
            string cmoney = rm.getcurmoney();
            GlobalVar.f_CurMoney = cmoney;
            GlobalVar.f_CurMoney = EagleAPI.substring(GlobalVar.f_CurMoney, 0, GlobalVar.f_CurMoney.Length - 2) + "00";
            
            GlobalVar.stdRichTB.AppendText("\r\n您的可用指令为\r\n>");
            for (int i = 0; i < GlobalVar.loginLC.VisuableCommand.Split('~').Length; i++)
            {
                GlobalVar.stdRichTB.AppendText(GlobalVar.loginLC.VisuableCommand.Split('~')[i] + "\r\n>");
            }
            GlobalVar.stdRichTB.AppendText("\r\n您的余额为" + GlobalVar.f_CurMoney+"\r\n>");
            Model.md.SetBoolVars();
        }

        private void 财务监听ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                财务监听ToolStripMenuItem.Checked = !财务监听ToolStripMenuItem.Checked;
                GlobalVar.bListenCw = 财务监听ToolStripMenuItem.Checked;
                if (GlobalVar.bListenCw)
                    GlobalVar.commServer.Start();
                else
                    GlobalVar.commServer.Close();
            }
            catch
            {
                if (MessageBox.Show("由于端口被占用，将强行关闭所有EAGLE订票程序", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    == DialogResult.Cancel) return;
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcesses())
                {
                    if (thisproc.ProcessName.ToLower().IndexOf("eagle") == 0)//.Equals("eagle"))
                    {
                        thisproc.Kill();
                    }
                }
            }
        }

        private void mi_CONFIG_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string srvip;
            string srvdns="";
            string srvport;
            string srvname = GlobalAPI.SwitchConfigBetweenServer.GetDestServerInfoAfterSwitch(e.ClickedItem.Text, out srvip, out srvport);
            if (e.ClickedItem.Text != "全部配置")// && e.ClickedItem.Text.IndexOf("集群")<0)
            {
                //跨服务器选择配置
                try
                {
                    System.Net.IPAddress  temp = System.Net.IPAddress.Parse(srvip);
                }
                catch
                {
                    srvdns = Login_Classes.dns2ip_static(srvip);
                }
                if ((GlobalVar.loginLC.SrvIP == srvip || GlobalVar.loginLC.SrvIP==srvdns) && GlobalVar.loginLC.SrvPort.ToString() == srvport) ;
                else
                {
                    GlobalVar.b_replaceSocket = true;
#if !RWY
                    this.Text = GlobalVar.exeTitle + "（服务器位置：" + srvname + "）"; //MessageBox.Show(srvname);
#endif
                    GlobalAPI.SwitchConfigBetweenServer.ModifyLoginXmlAfterSwitch(e.ClickedItem.Text);
                    //GlobalAPI.SwitchConfigBetweenServer.ReplaceGlobalSocket(GlobalVar.loginLC.SrvIP, GlobalVar.loginLC.SrvPort);
                }
            }
            GlobalVar.bSwichConfigByManual = true;
            for (int i = 0; i < mi_CONFIG.DropDownItems.Count; i++)
            {

                try
                {
                    ((ToolStripMenuItem)mi_CONFIG.DropDownItems[i]).Checked = false;
                }
                catch
                {
                }
            }
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;

            string selectedText = e.ClickedItem.Text.Trim();

            string ip = "";// EagleAPI.GetIPByConfigNumber(e.ClickedItem.Text.Trim());
            if (selectedText.IndexOf("集群") > 0)
            {
                GlobalAPI.NotGlobal ng = new ePlus.GlobalAPI.NotGlobal();
                ip = ng.GetConfigIPsGroupBy(selectedText.Substring(0, 6));
            }
            else
            {
                ip = EagleAPI.GetIPByConfigNumber(e.ClickedItem.Text.Trim());
            }
            mi_CONFIG.Text = e.ClickedItem.Text;
            GlobalVar.officeNumberCurrent = e.ClickedItem.Text;
            EagleAPI.SpecifyCFG(ip);
            Thread.Sleep(500);
            EagleAPI.SpecifyCFG(ip);

            GlobalVar.etdz_config = ip;
            GlobalVar.b_cfg_First = false;
            try
            {
                CreateETicket.configstring1 = mi_CONFIG.Text;
                CreateETicket.configstring2 = mi_CONFIG.Text;
            }
            catch
            {
            }
        }
        


        private void tsSubmitPnr_Click(object sender, EventArgs e)
        {
            BookSimple.SubmitPnr sp = new ePlus.BookSimple.SubmitPnr(EagleAPI.etstatic.Pnr,"");
            sp.ShowDialog();
        }

        private void SunShineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.Sunshine ins = new ePlus.PrintHyx.Sunshine();
            ins.ShowDialog();
            ins.Dispose();
        }

        private void eRP机票录入单插件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eRP机票录入单插件ToolStripMenuItem.Checked = !eRP机票录入单插件ToolStripMenuItem.Checked;
            GlobalVar.b_erp_机票录入单 = eRP机票录入单插件ToolStripMenuItem.Checked;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (GlobalVar.b_erp_机票录入单)
            {
                try
                {
                    GlobalVar.newEticket.SetErp();
                }
                catch
                {
                    MessageBox.Show("ERP插件不正确！");
                }
            }
        }

        private void 重发当前指令ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.stdRichTB.AppendText("\r\n重发指令：# " + GlobalVar.CurrentSendCommands.Replace("~", " # ") + "\r\n");
            EagleAPI.EagleSendOneCmd(GlobalVar.CurrentSendCommands);
        }

        private void 显示特价产品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.IsListGroupTicket = 显示特价产品ToolStripMenuItem.Checked = !显示特价产品ToolStripMenuItem.Checked;
            initStatusBar();
        }



        private void 旅游产品打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                while (PrintReceipt.Context != null) PrintReceipt.Context.Close();
                PrintReceipt.b0003 = true;
                PrintReceipt pr = new PrintReceipt();
                pr.applyType(ReceiptType.Business);
                pr.Show();
            }
            catch
            {
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        static public string newbooksheet
        {
            set
            {
                MsnPromote mp1 = new MsnPromote();
                mp1.HeightMax = 148;//窗体滚动的高度

                mp1.WidthMax = 172;//窗体滚动的宽度

                mp1.Show();
            }
        }

        private void 强制指定配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            强制指定配置ToolStripMenuItem.Checked = !强制指定配置ToolStripMenuItem.Checked;
            GlobalVar.b_UseSpecified强制 = 强制指定配置ToolStripMenuItem.Checked;
            initStatusBar();
        }
        private void 国内票方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本选项将取消，请在工具条出票模式中选择普通模式");
            return;
            GlobalVar.commandSendtype = GlobalVar.CommandSendType.A;
            this.国际票方式ToolStripMenuItem.Checked = false;
            this.国内票方式ToolStripMenuItem.Checked = true;
            LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
            lo.writelocallog("切换至国内票方式", "OK");
        }

        private void 国际票方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本选项将取消，请在工具条出票模式中选择复杂模式");
            return;
            GlobalVar.commandSendtype = GlobalVar.CommandSendType.B;
            this.国际票方式ToolStripMenuItem.Checked = true;
            this.国内票方式ToolStripMenuItem.Checked = false;
            LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
            lo.writelocallog("切换至国际票方式", "OK");
        }

        private void 普通模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ttsddbBookModel.Text = 普通模式ToolStripMenuItem.Text;
            GlobalVar.commandSendtype = GlobalVar.CommandSendType.A;
            this.国际票方式ToolStripMenuItem.Checked = false;
            this.国内票方式ToolStripMenuItem.Checked = true;
            LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
            lo.writelocallog("切换至普通模式", "OK");
        }

        private void 复杂模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ttsddbBookModel.Text = 复杂模式ToolStripMenuItem.Text;
            GlobalVar.commandSendtype = GlobalVar.CommandSendType.B;
            this.国际票方式ToolStripMenuItem.Checked = true;
            this.国内票方式ToolStripMenuItem.Checked = false;
            LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
            lo.writelocallog("切换至复杂模式", "OK");

        }

        private void 简易模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ttsddbBookModel.Text = 简易模式ToolStripMenuItem.Text;
            GlobalVar.commandSendtype = GlobalVar.CommandSendType.Fast;
            this.国际票方式ToolStripMenuItem.Checked = true;
            this.国内票方式ToolStripMenuItem.Checked = false;
            LocalOperation.LogOperation lo = new ePlus.LocalOperation.LogOperation();
            lo.writelocallog("切换至快速模式", "OK");

        }
        private void 启动对帐程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //added by chenqj
            //FileInfo fi = new FileInfo(Application.StartupPath + "\\eTicketManager.exe");

            //if (fi.CreationTime < DateTime.Parse("2008-9-12 0:0:0"))
            //{
            //    MessageBox.Show("即将下载新的对账程序，请稍等……", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    fi.CopyTo(Application.StartupPath + "\\eTicketManager.cqj", true);
            //    downfile("eTicketManager.exe");
            //    return;
            //}

            try
            {
                if (!GlobalVar.bListenCw)
                    财务监听ToolStripMenuItem_Click(sender, e);

                if (File.Exists(Application.StartupPath + "\\eTicketManager.exe")
                    && File.Exists(Application.StartupPath + "\\database.mdb")
                    && File.Exists(Application.StartupPath + "\\etmconfig.txt"))
                {
                    RunProgram(Application.StartupPath + "\\eTicketManager.exe");
                }
                else
                    throw new Exception("您的对帐程序不完整");
            }
            catch
            {
                if (MessageBox.Show("不能找到对帐程序或不完整,是否下载?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    downfile("etm.exe");
                }
                else
                {
                }
            }
        }

        private void 重新下载对帐程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            downfile("eTicketManager.exe");
        }
        private void downfile(string filename)
        {
            try
            {
                MessageBox.Show("若已经打开对帐程序,请先关闭!");
                if (!GlobalVar.bListenCw)
                    财务监听ToolStripMenuItem_Click(null, null);

                FileStream fs = new FileStream("AutoUpdatePath.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string WebSite = sr.ReadLine().Trim();
                string WebDirectory = sr.ReadLine().Trim();
                sr.Close();
                fs.Close();
                Application.DoEvents();
                System.Net.WebClient webClient = new System.Net.WebClient();
                GlobalVar.stdRichTB.AppendText("开始下载" + filename + "……请等待……\r>");
                Application.DoEvents();
                webClient.DownloadFile("http://" + WebSite + WebDirectory + filename,
                    Application.StartupPath + "\\" + filename);
                GlobalVar.stdRichTB.AppendText("程序文件下载完成!开始解压缩……\r>");
                Application.DoEvents();
                RunProgram(Application.StartupPath + "\\" + filename);
                GlobalVar.stdRichTB.AppendText("正在启动对帐程序……\r>");

            }
            catch (Exception ex)
            {
                MessageBox.Show("downfile:" + ex.Message);
            }
        }

        private void 工作号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string config = this.mi_CONFIG.Text.Substring(0, 6).ToUpper();
                switch (config)
                {
                    case "WUH128":
                        EagleAPI.EagleSendOneCmd("si:39072/56789a~si:39072/23456a");
                        break;
                    case "WUH129":
                        EagleAPI.EagleSendOneCmd("si:2986/56789a/41~si:2986/23456a/41");
                        break;
                    case "WUH402":
                        EagleAPI.EagleSendOneCmd("si:64554/23456a~si:64554/56789a");
                        break;
                    case "WUH285":
                        EagleAPI.EagleSendOneCmd("si:39069/23456a~si:39069/56789a");
                        break;
                    case "WUH169":
                        EagleAPI.EagleSendOneCmd("si:39076/56789a~si:39076/23456a");
                        break;
                    case "ENH101":
                        EagleAPI.EagleSendOneCmd("si:71970/56789a~si:71970/23456a~si:71970/12345a");
                        break;
                    default:
                        throw new Exception("配置选择不正确或您的工作号未写入程序！");

                }
            }
            catch(Exception ex)
            {
                GlobalVar.stdRichTB.AppendText(ex.Message + "\r>");
            }
        }





        
        void testthread()
        {
            for (int i = 0; i < 20; i++)
            {
                EagleAPI.EagleSendCmd("qt");
                Thread.Sleep(1000);
            }
        }

        private void 提示新订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            提示新订单ToolStripMenuItem.Checked = !提示新订单ToolStripMenuItem.Checked;
            GlobalVar.b_提示新订单 = 提示新订单ToolStripMenuItem.Checked;
            initStatusBar();
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\options.xml");
                XmlNode xn;
                xn = xd.SelectSingleNode("eg");
                xn = xn.SelectSingleNode("neworder");
                xn.InnerText = 提示新订单ToolStripMenuItem.Checked ? "1" : "0";
                xd.Save(Application.StartupPath + "\\options.xml");
            }
            catch (Exception ee)
            {
                //MessageBox.Show("neworder" + ee.Message);
            }
        }

        private void 航翼网会员保险卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.Hangyiwang dlg = new ePlus.PrintHyx.Hangyiwang();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void 新华人寿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.bxLogin bx = new ePlus.PrintHyx.bxLogin();
            if (bx.ShowDialog() != DialogResult.OK) return;

            PrintHyx.EagleIns dlg = new ePlus.PrintHyx.EagleIns();
            dlg.Text = dlg.lb公司名称.Text = "新华人寿保险股份有限公司意外伤害保险承保告知单";

            dlg.Show();
        }

        private void 航空意外险ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            PrintHyx.Pacific dlg = new ePlus.PrintHyx.Pacific();
            dlg.ShowDialog();
        }

        private void 查看今天日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            //FileDialog dlg = new OpenFileDialog();
            //dlg.AddExtension = true;
            //dlg.CheckFileExists = true;
            //dlg.CheckPathExists = true;
            //dlg.Filter = "所有文件(*.*)|*.*|文本文件(*.log)|*.log|TPR报表文件(*.tpr)|*.tpr";

            string path = Application.StartupPath;
            if (!Directory.Exists(path + "\\Log"))
                Directory.CreateDirectory(path + "\\Log");
            string[] files = Directory.GetFiles(path + "\\log");
            foreach (string file in files)
            {
                int posPoint = file.LastIndexOf('.');
                int pos_ = file.LastIndexOf('\\');
                if (pos_ < 0 || posPoint < 0 || posPoint<pos_)
                {
                    continue;
                }
                else
                {
                    pos_++;//\12345.log
                    string date = file.Substring(pos_, posPoint - pos_);
                    try
                    {
                        if(DateTime.Parse(date) == DateTime.Parse(DateTime.Now.ToShortDateString()))
                        {
                            EagleAPI.LogRead(file);
                            return;
                        }
                    }
                    catch(Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
                
            }
            MessageBox.Show("无今天日志");
            //dlg.InitialDirectory = Application.StartupPath + "\\Log";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    filename = dlg.FileName;//包括了绝对路径

            //    EagleAPI.LogRead(filename);
            //}
        }
        void initStatusBar()
        {
            try
            {
                GlobalVar.b_EtermType = true;
                //GlobalVar.b_EtermType = (
                //    EagleAPI2.initOneXmlItem(Application.StartupPath + "\\options.xml", "eg", "EtermType",
                //    GlobalVar.b_EtermType ? "1" : "0")
                //    == "1");
                stiSD2SS.ForeColor = (GlobalVar.b_sd2ss ? Color.Green : Color.Red);
                sti散拼.ForeColor = (GlobalVar.IsListGroupTicket ? Color.Green : Color.Red);
                sti指定配置.ForeColor = (GlobalVar.b_UseSpecified强制 ? Color.Green : Color.Red);
                sti订单.ForeColor = (GlobalVar.b_提示新订单 ? Color.Green : Color.Red);
                stiETERM.ForeColor = (GlobalVar.b_EtermType ? Color.Green : Color.Red);
                stiIBE.ForeColor = (BookTicket.bIbe ? Color.Green : Color.Red);
                sti黑屏政策.ForeColor = (GlobalVar2.gbDisplayPolicy ? Color.Green : Color.Red);
                sti抢占.ForeColor = (GlobalVar2.gbEplusStyle ? Color.Green : Color.Red);
                //if (GlobalVar2.gbDisplayPolicy)
                //{
                //    GlobalVar2.bookTicket.Show();
                //}
                //else GlobalVar2.bookTicket.Hide();
            }
            catch
            {
            }
        }

        private void sti订单_Click(object sender, EventArgs e)
        {
            提示新订单ToolStripMenuItem_Click(sender, e);
            GlobalVar.stdRichTB.AppendText("切换订单提示的快键为 Shift+F4\r>");
        }

        private void sti散拼_Click(object sender, EventArgs e)
        {
            显示特价产品ToolStripMenuItem_Click(sender, e);
            GlobalVar.stdRichTB.AppendText("切换散拼显示的快键为 Shift+F3\r>");
        }

        private void stiSD2SS_Click(object sender, EventArgs e)
        {
            sD转换SSToolStripMenuItem_Click(sender, e);
            GlobalVar.stdRichTB.AppendText("切换sd自动转换ss的快键为 Shift+F5\r>");
        }

        private void sti指定配置_Click(object sender, EventArgs e)
        {
            强制指定配置ToolStripMenuItem_Click(sender, e);
            GlobalVar.stdRichTB.AppendText("切换是否强制指定配置的快键为 Shift+F6\r>");
        }

        private void stiETERM_Click(object sender, EventArgs e)
        {
            GlobalVar.b_EtermType = !GlobalVar.b_EtermType;
            GlobalVar.stdRichTB.AppendText("切换是否使用ETERM风格的快键为 Shift+F7\r>");
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Application.StartupPath + "\\options.xml");
                XmlNode xn;
                xn = xd.SelectSingleNode("eg");
                xn = xn.SelectSingleNode("EtermType");
                xn.InnerText = GlobalVar.b_EtermType ? "1" : "0";
                xd.Save(Application.StartupPath + "\\options.xml");
            }
            catch (Exception ee)
            {
                //MessageBox.Show("neworder" + ee.Message);
            }
            initStatusBar();
        }

        private void stiIBE_Click(object sender, EventArgs e)
        {
            BookTicket.bIbe = !BookTicket.bIbe;
            initStatusBar();
            GlobalVar.stdRichTB.AppendText("切换IBE使用的快键为 Shift+F2\r>");
        }
        private void sti黑屏政策_Click(object sender, EventArgs e)
        {
            GlobalVar2.gbDisplayPolicy = !GlobalVar2.gbDisplayPolicy;
            GlobalVar.stdRichTB.AppendText("切换政策显示的快键为 Shift+F1(本功能已取消)\r>");
        }
        private void sti抢占_Click(object sender, EventArgs e)
        {
            GlobalVar2.gbEplusStyle = !GlobalVar2.gbEplusStyle;
            GlobalVar.stdRichTB.AppendText("切换是否抢占配置的快键为 Shift+F8\r>");

        }
        private void testbuttonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //eTicket.etDecFee df = new ePlus.eTicket.etDecFee();
            //df.Pnr = "ABCDE";
            //df.TotalFC = "12";
            //GlobalVar.GlobalString = "扣款前余额为" + GlobalVar.f_CurMoney + "，当前扣款金额为" + df.Pnr + "\n>";
            //for (int i = 0; i < 1; i++)
            //{
            //    if (!df.submitinfo())
            //    {
            //        GlobalVar.GlobalString += "扣款失败，系统将会补扣！\r>";
            //        break;
            //    }
            //}
            Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
            string ibret = (ib.rt2("PJ2SS"));//.xepnr("MDR1R",""));
            Options.ibe.IbeRt ir = new Options.ibe.IbeRt(ibret);
            
            //GlobalVar.b_test = !GlobalVar.b_test;
            //GlobalVar.stdRichTB.AppendText(string.Format("发出包{0}，收到包{1}\n>", GlobalVar.PackageNumberSend, GlobalVar.PackageNumberRecv));
            /*
            Tpr.run("tpr:2/-");
            //*/
            //Thread th = new Thread(new ThreadStart(testthread));
            //th.Start();

            /*指令工具条

            Options.ToolBar.CmdQueryTool tool = new Options.ToolBar.CmdQueryTool();
            tool.Show();
             * //*/
            /*显示简版查询结果界面

            BookTicket bt = new BookTicket();
            bt.setToBooktktExtDisplay();
            bt.Show();
            //*/
            /*国际票版打印
            Options.InterTicketPrint dlg = new Options.InterTicketPrint();
            dlg.ShowDialog();
             //*/
            //*输入票号，查找电脑号
            //NetworkSetup.Eticket2Pnr ep = new ePlus.NetworkSetup.Eticket2Pnr();
            //ep.Show();
            //*/
            //GlobalVar.stdRichTB.AppendText(WebService.wsGetPolicies("MU2501,CZ3571,MU2507,FM9362,CZ3823,FM9450,MU2503,CZ3579", "2007-9-29", "WUH", "PVG"));

        }

        private void 修复字体FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("若依然不能正常打印字体，请做如下操作！"+
                "\r\n1. 进入系统Fonts目录，一般为C:\\Windows\\Fonts"+
                "\r\n2. 找到TEC.TTF文件，将其拖动到桌面"+
                "\r\n3. 再把桌面上的TEC.TTF文件拖回到C:\\Windows\\Fonts完成字体安装");
            EagleAPI2.FixFont();
        }

        private void 切换用户SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.gbIsRestartEagle = true;
            this.Close();
        }
        /// <summary>
        /// 查找过去的本地日志上传到服务器　
        /// </summary>
        /// <param name="fnString">日志文件名，如：2008-01-13.log</param>
        /// <param name="keys">要搜索的关键字，小写</param>
        void searchlog(string fnString,string keys)
        {
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\log\\" + fnString, FileMode.Open);//fnstring = 2008-01-13.log
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    string temp = sr.ReadLine();
                    if (temp.ToLower().IndexOf(keys) >= 0)
                    {
                        EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
                        NewPara np = new NewPara();
                        np.AddPara("cm", "WriteLog");
                        np.AddPara("User", GlobalVar.loginName);
                        np.AddPara("Cmd", "SEARCH项/" + keys);
                        np.AddPara("ReturnResult", temp);
                        string strReq = np.GetXML();
                        while (true)
                        {
                            string strRet = ws.getEgSoap(strReq);
                            if (strRet != "")
                            {
                                NewPara np1 = new NewPara(strRet);
                                if (np1.FindTextByPath("//eg/cm") == "RetWriteLog" && np1.FindTextByPath("//eg/OperationFlag") == "SaveSucc")
                                {

                                    break;
                                }
                            }
                        }
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch
            {
            }
        }

        private void 安邦商行通ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.EagleAnbang ea = new ePlus.PrintHyx.EagleAnbang();
            ea.ShowDialog();
            return;
        }

        private void 绑定端口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetworkSetup.BindLocal bl = new ePlus.NetworkSetup.BindLocal();
            bl.ShowDialog();
        }

        MyKJ.MyTcpIpClient ctiClient = new MyKJ.MyTcpIpClient();
        MyKJ.MyTcpIpServer ctiClientB2C = new MyKJ.MyTcpIpServer();//added by chenqj

        string teleNum = "";
        void getCostomInfo()
        {
            NewPara npSent = new NewPara();
            npSent.AddPara("cm", "GetCustInfoByMobile");
            npSent.AddPara("MobileNo", teleNum);
            EagleWebService.wsKernal ws = new EagleWebService.wsKernal(GlobalVar.WebServer);
            string strRet = ws.getEgSoap(npSent.GetXML());
            string strRec = "";
            NewPara npRet = new NewPara(strRet);
            if (npRet.FindTextByPath("//eg/bExistCust").ToLower().Trim() == "true")
            {
                string costomID = npRet.FindTextByPath("//eg/numCustId").Trim();
                
                //存入来电记录
                {
                    NewPara npTemp = new NewPara();
                    npTemp.AddPara("cm", "NewCustCallRec");
                    npTemp.AddPara("numCustId", costomID);
                    npTemp.AddPara("MobileNo", teleNum);
                    ws.getEgSoap(npTemp.GetXML());
                }
                //取消费记录

                {
                    NewPara npTemp = new NewPara();
                    npTemp.AddPara("cm", "GetCustLastCalls");
                    npTemp.AddPara("numCustId", costomID);
                    strRec = ws.getEgSoap(npTemp.GetXML());
                }
                strRec = strRec.Replace("RetGetCustLastCalls", "消费记录");
                strRet = "<A>" + strRet + strRec + "</A>";
                strRet = strRet.Replace("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", "");
                strRet = strRet.Replace("RetCustInfoByMobile", teleNum + "的客户资料");
                strRet = strRet.Replace("<bExistCust>true</bExistCust>", "");
                strRet = strRet.Replace("numCustId", "客户ID");
                strRet = strRet.Replace("vcCustNo", "客户号");
                strRet = strRet.Replace("vcCustName", "姓名");
                strRet = strRet.Replace("vcTel", "电话");
                strRet = strRet.Replace("vcIdentCard", "证件号");
                strRet = strRet.Replace("vcIdentType", "证件类型");
                strRet = strRet.Replace("vcCustType", "用户类型");
                strRet = strRet.Replace("vcMenSrcDesc", "来源");
                strRet = strRet.Replace("numMemScore", "积分");
                strRet = strRet.Replace("dtCustBuildTime", "建立时间");
                strRet = strRet.Replace("dtLastFee", "上次消费");
                strRet = strRet.Replace("numAgentId", "归属号");
                strRet = strRet.Replace("vcInpEgUser", "建档人");
                strRet = strRet.Replace("vcNewAgentName", "归属名");
                strRet = strRet.Replace("vcCustAdr", "客户地址");
                strRet = strRet.Replace("dtBirthDay", "客户生日");
                strRet = strRet.Replace("vcCredCardOne", "信用卡");
            }
            else
            {
                strRet = "<eg>不存在该客户信息！电话为：" + teleNum + "</eg>";
            }
            costominfo = strRet;
            try
            {
                if (dlgPassInfo.InvokeRequired)
                {
                    EventHandler eh = new EventHandler(showinfo);
                    EagleCTI.FormPassInfo dlg = dlgPassInfo;
                    dlgPassInfo.Invoke(eh, new object[] { dlg, EventArgs.Empty });
                }
                //dlgPassInfo.setWebBrowser(strRet);
            }
            catch(Exception ex)
            {
                MessageBox.Show("getCostomInfo1" + ex.Message);
            }
        }
        string costominfo = "";
        void showinfo(object sender,EventArgs e)
        {
            dlgPassInfo.setWebBrowser(costominfo);
        }
        string ctiTeleport = "";
        void ctiClient_Error(object sender, MyKJ.ErrorEventArgs e)
        {
            try
            {
                if (ctiClient.Activ)
                    GlobalVar.stdRichTB.AppendText("CTI Client ERROR: 发生意外错误！\r\n>");
                else
                    GlobalVar.stdRichTB.AppendText("CTI Client ERROR: 未建立SOCKET连接！可能CTI服务未启动！\r\n>");
            }
            catch
            {
                MessageBox.Show("可能服务程序被关闭");
            }
        }
        EagleCTI.FormPassInfo dlgPassInfo =null;// new EagleCTI.FormPassInfo(GlobalVar.loginName,GlobalVar.WebServer);
        EagleCTI.FormPassInfoB2C dlgPassInfoB2C = null;
        //Yzp.FormPassInfo dlgPassInfo = new Yzp.FormPassInfo (GlobalVar.loginName, GlobalVar.WebServer);

        void ctiClient_Incept(object sender, MyKJ.InceptEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[2048];
                e.Astream.Position = 0;
                e.Astream.Read(buffer, 0, 2048);
                string cmdData = System.Text.Encoding.Default.GetString(buffer);
                cmdData = cmdData.Split('\0')[0];
                //cmdData为电话号码，用这个电话号码作为参数，请求服务！

                switch (cmdData)
                {
                    case "listen":
                        ctiClient.Send(new MemoryStream(System.Text.Encoding.Default.GetBytes("ok|" + ctiTeleport)));
                        break;
                    default:
                        teleNum = cmdData;
                        if (GlobalVar.stdRichTB.InvokeRequired)
                        {

                        }
                        else
                        {
                            EagleAPI.LogWrite("**********************来电：" + cmdData + "**********************\r\n>");
                            if (dlgPassInfo.WindowState == FormWindowState.Minimized)
                                dlgPassInfo.WindowState = FormWindowState.Normal;
                        }
                        getCostomInfo();
                        break;
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("接收电话号码消息失败:" + ex.Message);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctiClient.Send(new MemoryStream(System.Text.Encoding.Default.GetBytes("close|" + ctiTeleport)));

            if(this.CtiUdpListening != null)
                this.CtiUdpListening.Close();
        }

        private void i签入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //test area
            //dlgPassInfo.setWebBrowser("<A><?xml version=\"1.0\" encoding=\"utf-8\"?><eg><cm>13554256476的客户资料</cm><客户ID>9</客户ID><客户号></客户号><姓名>钟少卿</姓名><电话></电话><证件号>4211821984061145</证件号><证件类型>身份证</证件类型><用户类型></用户类型><来源>门市</来源><积分>0</积分><建立时间>2008-6-13 14:10:08</建立时间><上次消费></上次消费><归属号>00010001</归属号><建档人>chenglong</建档人><归属名>eg-湖北省兰翔航空</归属名><客户地址></客户地址><客户生日>1900-1-1 0:00:00</客户生日><信用卡></信用卡></eg><?xml version=\"1.0\" encoding=\"utf-8\"?><eg><cm>消费记录</cm><Content>13554256476~$13554256476~</Content><AirFee>海南航空公司~长沙~海口~2008-6-16 0:00:00~HU7392~E~440.00~40~tp5pm~2008-6-12 15:10:07$中国南方航空公司~武汉~南宁~2008-6-13 0:00:00~CZ3341~T~980.00~90~QN10B~2008-6-13 8:53:11$中国南方航空公司~北京PEK~南京~2008-6-13 0:00:00~CZ3181~Q~560.00~55~TJY8E~2008-6-13 9:53:36$深圳航空~深圳~宜昌~2008-6-13 0:00:00~ZH9943~Y~990.00~100~MCR3M~2008-6-13 12:52:42$中国南方航空公司~武汉~北京PEK~2008-6-17 0:00:00~CZ3117~Y~1080.00~~V83M8~2008-6-13 14:33:32</AirFee></eg></A>");
            //dlgPassInfo.setWebBrowser("<eg>不存在该客户信息！电话为：" +"11111111" + "</eg>");
            //dlgPassInfo.Show();
            //getCostomInfo();

            //return;
            ctiClient.Incept += new MyKJ.InceptEvent(ctiClient_Incept);
            ctiClient.Error += new MyKJ.ErrorEvent(ctiClient_Error);
            EagleCTI.CtiLogin dlg = new EagleCTI.CtiLogin();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //dlg.ShowDialog();
                string ctiServer = dlg.serverip;
                int ctiPort = dlg.serverport;
                ctiTeleport = dlg.teleport;

                ctiClient.TcpIpServerIP = ctiServer;
                ctiClient.TcpIpServerPort = ctiPort;
                ctiClient.Conn();
                if (ctiClient.Activ)
                {
                    ctiClient.Send(new MemoryStream(System.Text.Encoding.Default.GetBytes("connect|" + ctiTeleport)));
                    GlobalVar.stdRichTB.AppendText("签入成功！\r\n>");
                    z转移内线ToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void o签出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ctiClient.Send(new MemoryStream(System.Text.Encoding.Default.GetBytes("close|" + ctiTeleport)));
                GlobalVar.stdRichTB.AppendText("签出成功！\r\n>");
                z转移内线ToolStripMenuItem.Enabled = false;
            }
            catch
            {
                GlobalVar.stdRichTB.AppendText("签出失败，可能尚未签入！\r\n>");
            }
        }

        private void r录入客户基本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dlgPassInfo.setWebBrowser("<A><?xml version=\"1.0\" encoding=\"utf-8\"?><eg><cm>13554256476的客户资料</cm><客户ID>9</客户ID><客户号></客户号><姓名>钟少卿</姓名><电话></电话><证件号>4211821984061145</证件号><证件类型>身份证</证件类型><用户类型></用户类型><来源>门市</来源><积分>0</积分><建立时间>2008-6-13 14:10:08</建立时间><上次消费></上次消费><归属号>00010001</归属号><建档人>chenglong</建档人><归属名>eg-湖北省兰翔航空</归属名><客户地址></客户地址><客户生日>1900-1-1 0:00:00</客户生日><信用卡></信用卡></eg><?xml version=\"1.0\" encoding=\"utf-8\"?><eg><cm>消费记录</cm><Content>13554256476~$13554256476~</Content><AirFee>海南航空公司~长沙~海口~2008-6-16 0:00:00~HU7392~E~440.00~40~tp5pm~2008-6-12 15:10:07$中国南方航空公司~武汉~南宁~2008-6-13 0:00:00~CZ3341~T~980.00~90~QN10B~2008-6-13 8:53:11$中国南方航空公司~北京PEK~南京~2008-6-13 0:00:00~CZ3181~Q~560.00~55~TJY8E~2008-6-13 9:53:36$深圳航空~深圳~宜昌~2008-6-13 0:00:00~ZH9943~Y~990.00~100~MCR3M~2008-6-13 12:52:42$中国南方航空公司~武汉~北京PEK~2008-6-17 0:00:00~CZ3117~Y~1080.00~~V83M8~2008-6-13 14:33:32</AirFee></eg></A>");
            dlgPassInfo.setWebBrowser("<eg>不存在该客户信息！电话为：" +"|录入客户基本信息|" + "</eg>");
            dlgPassInfo.Show();
            getCostomInfo();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                string to = tsmi.Text.Substring(1, 1);
                string from = ctiTeleport;
                ctiClient.Send(new MemoryStream(System.Text.Encoding.Default.GetBytes("bridge|" + from + "|" + to)));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pICCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintHyx.bxLogin bx = new ePlus.PrintHyx.bxLogin();
            if (bx.ShowDialog() != DialogResult.OK) return;

            PrintHyx.EagleIns dlg = new ePlus.PrintHyx.EagleIns();
            dlg.EalgeBxtype = 1;
            dlg.Show();
        }

        //added by chenqj
        private void r恢复老对账程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(Application.StartupPath + "\\eTicketManager.cqj");

            if (MessageBox.Show("确定恢复成老版的对账程序？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                fi.CopyTo(Application.StartupPath + "\\eTicketManager.exe", true);
                MessageBox.Show("已恢复成老版的对账程序！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 弹出新订单的编辑页面
        /// added by chenqj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonNewOrder_Click(object sender, EventArgs e)
        {
            //if (web != null)
            //    web.Close();

            //web = new NewOrderWeb();
            newOrderWeb.Show();
            newOrderWeb.Focus();
            newOrderWeb.Navigate();

            this.b.Close();
        }

        bool HasBeenWarned = false;
        DevComponents.DotNetBar.Balloon b;

        /// <summary>
        /// 根据是否有新订单改变菜单的图标

        /// addedy by chenqj
        /// </summary>
        void WarningNewOrder()
        {
            if (GlobalVar2.gbUserModel != 1)
                return;

            if (Options.GlobalVar.B2CNewOrderID > 0)
            {
                if (!HasBeenWarned)
                {
                    b = new DevComponents.DotNetBar.Balloon();
                    b.Style = eBallonStyle.Balloon;
                    b.CaptionText = "提示";
                    b.Text = "发现新的订单，请点击进行查看！";
                    b.AlertAnimation = eAlertAnimation.TopToBottom;
                    //b.AutoResize();
                    b.Width = 170;
                    //b.AutoClose = true;
                    //b.AutoCloseTimeOut = 30;
                    b.Owner = this;

                    Point p = this.toolStripButtonNewOrder.Bounds.Location;
                    p = this.PointToScreen(p);
                    p.Offset(0, 10);
                    Size s = this.toolStripButtonNewOrder.Bounds.Size;
                    b.Show(new Rectangle(p, s), false);

                    this.HasBeenWarned = true;
                }

                //object obj = ePlus.Properties.Resources.ResourceManager.GetObject("loading01");
                //System.Drawing.Image img = (System.Drawing.Image)(obj);
                toolStripButtonNewOrder.Image = this.imageList1.Images[1];
                toolStripButtonNewOrder.Enabled = true;
            }
            else
            {
                //object obj = ePlus.Properties.Resources.ResourceManager.GetObject("note");
                //System.Drawing.Image img = (System.Drawing.Image)(obj);
                toolStripButtonNewOrder.Image = this.imageList1.Images[0];
                toolStripButtonNewOrder.Enabled = false;

                this.HasBeenWarned = false;
            }
        }

        private void toolStripButtonCall_Click(object sender, EventArgs e)
        {
            if (dlgPassInfo != null)
                dlgPassInfo.Show();
            else
            {
                //Rectangle r = Screen.GetWorkingArea(this);
                //Point p = new Point(r.Right - dlgPassInfoB2C.Width, r.Bottom - dlgPassInfoB2C.Height);
                //dlgPassInfoB2C.Location = p;

                dlgPassInfoB2C.WindowState = FormWindowState.Normal;
                dlgPassInfoB2C.Show();
            }
        }

        private void receiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)(ar.AsyncState);

            if (u.Client == null)// u == null 判断失败,u 似乎不会为空
                return;//这里线程结束(由 CtiUdpListening.Close() 触发),返回

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);//该值(可以任意),似乎完全不影响下面方法的调用?
            Byte[] receiveBytes = u.EndReceive(ar, ref ep);
            string receiveString = Encoding.Default.GetString(receiveBytes);
            string receiveNumber = string.Empty;
            //GlobalVar.stdRichTB.AppendText("原始消息：" + receiveString + System.Environment.NewLine + ">");

            try
            {
                switch (this.CtiType)
                {
                    case XMLConfig.CtiTypeEnum.EGPlug://<callinform ani='82422229' ext='200' agt='800' msgtype='130' svctype='11' addr='' data='can be other item' />
                        XmlDocument xd = new XmlDocument();
                        xd.LoadXml(receiveString);

                        if (xd.ChildNodes[0].Name.ToLower() == "callinform")
                        {
                            XmlNode xn = xd.ChildNodes[0];
                            receiveNumber = xn.Attributes["ani"].Value;
                        }
                        break;

                    case XMLConfig.CtiTypeEnum.EGMMPBX://<evt_newcall ani='82422229' ext='200' agt='800' msgtype='130' svctype='11' addr='' data='can be other item' />
                        XmlDocument xd2 = new XmlDocument();
                        xd2.LoadXml(receiveString);

                        if (xd2.ChildNodes[0].Name.ToLower() == "evt_newcall")
                        {
                            XmlNode xn = xd2.ChildNodes[0];
                            receiveNumber = xn.Attributes["ani"].Value;
                        }
                        break;

                    case XMLConfig.CtiTypeEnum.EGSwitch://130|1|1015013945|11|0|82422229|11|11|896||j|
                        receiveString = receiveString.Replace("\x02", "");// \x02 既符号“”

                        {
                            string[] receiveArray = receiveString.Split('|');
                            string msgType = receiveArray[0];

                            if (msgType == "130")
                            {
                                receiveNumber = receiveArray[5];
                            }
                        }
                        break;
                    case XMLConfig.CtiTypeEnum.EGUSB://130|130|1|1015013945|11|0|13971582543
                        receiveString = receiveString.Replace("\x02", "");// \x02 既符号“”

                        receiveString = receiveString.Replace("\x03", "");// \x03 既符号“”

                        {
                            string[] receiveArray = receiveString.Split('|');
                            string msgType = receiveArray[0];

                            if (msgType == "130")
                            {
                                receiveNumber = receiveArray[6];
                            }
                        }
                        break;
                }

                if (!string.IsNullOrEmpty(receiveNumber))
                {
                    BeginInvoke(new dlgUpdater(Calling), receiveNumber);
                }
                //else
                //    throw new Exception("来电消息错误，主叫号码为空？");
            }
            catch (Exception ee)
            {
                GlobalVar.stdRichTB.AppendText(ee.Message + System.Environment.NewLine + ">");
                GlobalVar.stdRichTB.AppendText("请确认是否选择了正确的呼叫中心类型？" + System.Environment.NewLine + ">");
                GlobalVar.stdRichTB.AppendText("消息内容：" + receiveString + System.Environment.NewLine + ">");
            }

            //GlobalVar.stdRichTB.AppendText(DateTime.Now.ToString() + "\r\n>");
            //Thread.CurrentThread.Join(3000);// 3 秒内不会接收新的消息
            //GlobalVar.stdRichTB.AppendText(DateTime.Now.ToString() + "\r\n>");

            //重新启动接收线程
            if (u != null)
            {
                AsyncCallback callback = new AsyncCallback(receiveCallback);
                u.BeginReceive(callback, u);
            }
        }

        private delegate void dlgUpdater(string msg);
        /// <summary>
        /// 弹出 来电客户资料
        /// </summary>
        /// <param name="phone"></param>
        private void Calling(string phone)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Focus();

            if (dlgPassInfo != null)
                dlgPassInfo.Show();
            else
            {
                //Rectangle r = Screen.GetWorkingArea(this);
                //Point p = new Point(r.Right - dlgPassInfoB2C.Width, r.Bottom - dlgPassInfoB2C.Height);
                //dlgPassInfoB2C.Location = p;

                dlgPassInfoB2C.WindowState = FormWindowState.Normal;
                dlgPassInfoB2C.Show();
                dlgPassInfoB2C.LoadCustomerInfo(phone);
            }
        }

        /// <summary>
        /// 显示大连南北航空的Web软电话界面

        /// </summary>
        private void ShowDaLianCTI()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\EagleCti.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string url = sr.ReadLine();//http://192.168.18.101:8080/NBAIR/
            sr.Close();
            fs.Close();

            url += string.Format("V_BUSIPHONE.VIEW?id={0}&ext={1}", GlobalVar.loginNameOfBtoC, GlobalVar.DaLianCTIPhoneNumber);
            this.webBrowserDaLianCTI.Navigate(url);
        }

        /// <summary>
        /// 显示大连南北航空的CTI留言查询页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCtiMessage_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\EagleCti.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string url = sr.ReadLine();//http://192.168.18.101:8080/NBAIR/
            sr.Close();
            fs.Close();

            tabControl.TabPages.Add("留言查询");
            TabPage tp = tabControl.TabPages[tabControl.TabPages.Count - 1];

            WebBrowser wb = new WebBrowser();
            url += string.Format("V_LEAVEWORD_LIST_FRAME.VIEW?id={0}", GlobalVar.loginNameOfBtoC);

            wb.Navigate(url);
            wb.Dock = DockStyle.Fill;
            tp.SuspendLayout();
            tp.Controls.Add(wb);
            tp.ResumeLayout();
            tp.PerformLayout();
            tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
            toolStripButtonDaLianCtiMessage.Enabled = false;
        }

        /// <summary>
        /// 显示大连南北航空的CTI录音查询页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonDaLianCtiPlayback_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\EagleCti.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string url = sr.ReadLine();//http://192.168.18.101:8080/NBAIR/
            sr.Close();
            fs.Close();

            tabControl.TabPages.Add("录音查询");
            TabPage tp = tabControl.TabPages[tabControl.TabPages.Count - 1];

            WebBrowser wb = new WebBrowser();
            url += "V_RECINFO_ALL_LIST_FRAME.VIEW";

            wb.Navigate(url);
            wb.Dock = DockStyle.Fill;
            tp.SuspendLayout();
            tp.Controls.Add(wb);
            tp.ResumeLayout();
            tp.PerformLayout();
            tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
            toolStripButtonDaLianCtiPlayback.Enabled = false;
        }

        private void 行程单检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            CheckPrint cp = new CheckPrint();
            cp.Show();
        }

        private void tsbTravel_Click(object sender, EventArgs e)
        {
            string url = XMLConfig.Operation.GetSettingsCTI().B2CURL;//http://59.175.179.130:81/web/

            tabControl.TabPages.Add("旅游平台");
            TabPage tp = tabControl.TabPages[tabControl.TabPages.Count - 1];

            WebBrowser wb = new WebBrowser();
            url += string.Format("/login2.aspx?user={0}&pwd={1}", "admin", "admin");

            wb.Navigate(url);
            wb.Dock = DockStyle.Fill;
            tp.SuspendLayout();
            tp.Controls.Add(wb);
            tp.ResumeLayout();
            tp.PerformLayout();
            tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
            tsbTravel.Enabled = false;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            SplashScreen.Splasher.Close();
            Autoupdate au = new Autoupdate();
            au.Start();
        }
    }
}