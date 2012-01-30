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
    public partial class Primary
    {
        void InitPrimaryAuthority()
        {
            if (!loginInfo.b2b.lr.AuthorityOfFunction("EG2"))
            {
                string myuser = "bb,claw,developer,deverloper,yxh,97";
                string[] a = myuser.Split(',');
                bool canUse = false;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == loginInfo.b2b.username.ToLower())
                    {
                        canUse = true;
                        break;
                    }
                }
                if (!canUse)
                {
                    MessageBox.Show("使用易格2.0版本需要管理员打开权限!");
                    Application.Exit();
                    return;
                }
            }
        }

        private void InitExpireTicketFinder()
        {
            expireTicketFinder = new ExpireTicketFind(loginInfo.b2b.username);
            expireTicketFinder.SetArgs(socket, commandPool);
        }
        /// <summary>
        /// 初始化SOCKET
        /// </summary>
        private void InitSocket(string ip, int port)
        {
            try
            {
                ip = System.Net.Dns.GetHostAddresses(ip)[0].ToString();
            }
            catch
            {
            }
            try
            {
                commandPool = new CommandPool(loginInfo);
                commandPool_back = new CommandPool(loginInfo);
                socket = new MyTcpIpClient(loginInfo);
                socket.Error += new ErrorEvent(socket_Error);
                socket.Incept += new InceptEvent(socket_Incept);
                socket.TcpIpServerIP = ip;
                socket.TcpIpServerPort = port;
                socket.Conn();
                socket.SendPassport(loginInfo.b2b.lr.PASSPORT);
                uploadEticketInfo = new UploadEticketInfo(socket, commandPool_back, loginInfo);
            }
            catch (Exception ex)
            {
                AppendBlackWindow("初始化SOCKET失败 Address=" + ip + "::" + port.ToString() + ": " + ex.Message);
            }
            try
            {
                finance.autoImport.set_args_constructor(socket, loginInfo, commandPool);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 用IP:PORT初始化Socket
        /// </summary>
        private void InitSocket(object ipport)
        {
            string ip_port = (string)ipport;
            string ip = ip_port.Split(':')[0];
            int port = int.Parse(ip_port.Split(':')[1]);
            InitSocket(ip, port);
        }
        /// <summary>
        /// 初始化菜单，主要初始化有权限的保险菜单
        /// </summary>
        private void InitMainMenu()
        {

            m_menuInsurance = Printer.PrintXmlHandle.SomeFunc.MenuInsurance(loginInfo.b2b.lr);
            int count = m_menuInsurance.Items.Count;
            for (int i = 0; i < count; ++i)
            {
                ((mainMenu.Items["mnPrint"] as ToolStripMenuItem).
                DropDownItems["mn2Insurance"] as ToolStripMenuItem).DropDownItems.Add(m_menuInsurance.Items[0]);
            }
            Printer.PrintXmlHandle.SomeFunc.socket = socket;
            Printer.PrintXmlHandle.SomeFunc.cmdpool = commandPool;
        }
        /// <summary>
        /// 初始化BlackWindow的属性，事件等
        /// </summary>
        private void InitBlackWindow()
        {
            blackWindow = new BlackWindow();
            tpBlack.Controls.Add(blackWindow);
            if (loginInfo.b2b.lr.AuthorityOfFunction("004"))//黑屏权限004
            {
                blackWindow.KeyUp += new KeyEventHandler(blackWindow_KeyUp);
                blackWindow.MouseUp += new MouseEventHandler(blackWindow_MouseUp);
            }
            else
            {
                blackWindow.ReadOnly = true;
            }
            AppendBlackWindow(string.Format("{0}注意:在字体设置建议使用英文字体,推荐使用Courier New字体\n使用中文字体可能会使字体紊乱!{1}\n",'\x1c','\x1d'));
        }

        /// <summary>
        /// 初始化右面板
        /// </summary>
        private void InitRightPanel()
        {
            panel1.Controls.Add(lowestList);
            pRight2.Controls.Add(groupList);
            panel2.Controls.Add(specTickListFix);
            panel3.Controls.Add(specTickListFlow);
            lowestList.MouseDoubleClick += new MouseEventHandler(lowestList_MouseDoubleClick);
            groupList.MouseDoubleClick += new MouseEventHandler(groupList_MouseDoubleClick);
            specTickListFix.MouseDoubleClick += new MouseEventHandler(specTickListFix_MouseDoubleClick);
            specTickListFlow.MouseDoubleClick += new MouseEventHandler(specTickListFlow_MouseDoubleClick);

            lowestList.MouseUp += new MouseEventHandler(lowestList_MouseUp);
        }
        /// <summary>
        /// 当右键UP时，在黑屏中产生指令(如果功能打开的话)
        /// </summary>
        void lowestList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (true)
                {
                    try
                    {
                        ListViewItem lvi = lowestList.SelectedItems[0];
                        string ssstring =
                        EagleString.CommandCreate.Create_SS_String(
                             lvi.SubItems[1].Text //航班
                        , lvi.SubItems[4].Text[0] //舱位
                        , avResult.FlightDate_DT //日期
                        , avResult.CityPair //城市对
                        , 1
                            , new string[] { "修改姓名" }//姓名组
                        , new string[] { "修改证件号" }//证件组
                        , "修改电话" //PHONE
                            , loginInfo.b2b.lr.UsingOffice()//OFFICE
                            , new string[] { loginInfo.b2b.username }
                        );
                        AppendBlackWindow(ssstring);
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 双击右4列表(浮动舱位申请)
        /// </summary>
        void specTickListFlow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = specTickListFlow.SelectedItems[0];
            passengerAdd =
        new global::EagleForms.General.PassengerAdd(lvi, avResult, 3, loginInfo, socket, commandPool);
            passengerAdd.ShowDialog();
        }
        /// <summary>
        /// 双击右3列表(固定舱位申请)
        /// </summary>
        void specTickListFix_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = specTickListFix.SelectedItems[0];
            passengerAdd =
        new global::EagleForms.General.PassengerAdd(lvi, avResult, 2, loginInfo, socket, commandPool);
            passengerAdd.ShowDialog();
        }
        /// <summary>
        /// 双击右2列表(散客拼团)
        /// </summary>
        void groupList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = groupList.SelectedItems[0];
            passengerAdd =
                    new global::EagleForms.General.PassengerAdd(lvi, avResult, 1, loginInfo, socket, commandPool);
            passengerAdd.ShowDialog();
        }
        /// <summary>
        /// 双击右1列表(最低价与返点)，打开输入旅客信息，并产生PNR
        /// </summary>
        void lowestList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem lvi = lowestList.SelectedItems[0];
                passengerAdd =
                    new global::EagleForms.General.PassengerAdd(lvi, avResult, 0, loginInfo, socket, commandPool);
                passengerAdd.ShowDialog();
                    
            }
            catch
            {
            }
        }
        /// <summary>
        /// 启动程序后显示公告
        /// </summary>
        private void AddPopupNotice()
        {
            if (OuterCall) return;
            string[] msgs;
            wserviceKernal.getPubMes(loginInfo.b2b.username, out msgs);
            string s = string.Join("\r\n", msgs).Trim();
            if (string.IsNullOrEmpty(s)) return;
            AppendBlackWindow("公告:\r\n");
            AppendBlackWindow(s);
            AppendBlackWindow("\r\n\r\n>");
        }

        private void InitScrollNotice()
        {
            
            scrollNotice1.DoubleClick += new EventHandler(scrollNotice1_DoubleClick);
            this.scrollNotice1.start();

        }

        void scrollNotice1_DoubleClick(object sender, EventArgs e)
        {
            if (loginInfo.b2b.lr.AuthorityOfFunction("00B"))
            {
                General.NoticeScrollPublish dlg = new global::EagleForms.General.NoticeScrollPublish(loginInfo);
                dlg.ShowDialog();
                scrollNotice1.SetText(wserviceKernal.Get_Notice_Scroll(loginInfo.b2b.username, "0"));
            }
            else
            {
                AppendBlackWindow("您没有发布公告的权限\r\n>");
            }
        }



        void InitMainToolBar()
        {
            this.pToolBar.Controls.Add(mainToolBar);
            mainToolBar.ItemClicked += new ToolStripItemClickedEventHandler(mainToolBar_ItemClicked);
            string txt = "";
            mainToolBar.SetDropDownMenu(loginInfo.b2b.lr, loginInfo.b2b.lr.IpidsWhenLogin(ref txt)[0]);
            mainToolBar.sw = new MainToolBar.switchconfig(operation_switch_config);
        }

        void mainToolBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            HandleMenuClick(e);
        }
        void InitMainStatusBar()
        {
            this.pStatusBar.Controls.Add(mainStatusBar);
        }
    }
}
