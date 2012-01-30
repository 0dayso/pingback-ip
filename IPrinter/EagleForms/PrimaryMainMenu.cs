/*
 * 
 * Primary的主菜单处理
 * 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;


using EagleString;
using EagleControls;
using EagleProtocal;
using EagleExtension;
namespace EagleForms
{
    public partial class Primary
    {
        /// <summary>
        /// 一键出票
        /// </summary>
        private EagleString.Structs.ETDZONEKEY m_etdzonekey;
        private void HandleMenuClick(ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.Name;
            switch (name)
            {
                //File下的子菜单
                case "mnFile":
                    break;
                case "mn1Reconnect":
                    menu_event_reconnect();
                    break;
                case "mn1ModifyPassword":
                    menu_event_modifypassword();
                    break;
                case "mn1ViewLog":
                    menu_event_viewlog(false);
                    break;
                case "mn1ViewLogToday":
                    menu_event_viewlog(true);
                    break;
                case "mn1Balance":
                    Thread thread = new Thread(new ThreadStart(menu_event_balance));
                    thread.Start();
                    //menu_event_balance();
                    break;
                case "mn1Command":
                    menu_event_can_use_command();
                    break;
                case "mn1Exit":
                    menu_event_exit();
                    break;

                //Print下的子菜单
                case "mnPrint":
                    break;
                case "mn2Receipt":
                    menu_event_print_receipt();
                    break;
                case "mn2Insurance":
                    //此为下拉菜单
                    break;

                //Operation下的子菜单
                case "mnOperation":
                    break;
                case "mnSwitchVisableConfig":
                    menu_event_operation_switchconfig();
                    break;
                case "mnEtdzOneKey":
                    menu_event_operation_etdz_one_key();
                    break;
                case "mnRefund":
                    menu_event_operation_refund();
                    break;
                case "mnSubmitPnrOrder":
                    menu_event_operation_submit_pnr_order();
                    break;
                case "mnQueueClear":
                    menu_event_operation_queue_clear();
                    break;
                case "mnBig2Pnr":
                    menu_event_operation_aircode2pnr();
                    break;
                case "mnHistoryCommandBack":
                    menu_event_operation_history_cmd(false);
                    break;
                case "mnHistoryCommandNext":
                    menu_event_operation_history_cmd(true);
                    break;
                case "mnIbe2Config":
                    menu_event_operation_ibe2config();
                    break;

                //window下的子菜单
                case "mn4BlackWindowMax":
                    menu_event_window_blackwindow_max();
                    break;
                case "mn4BlackWindowNormal":
                    menu_event_window_blackwindow_normal();
                    break;
                case "mn4RightPanelHide":
                    menu_event_window_rightpanel_hide();
                    break;
                case "mn4RightPanelShow":
                    menu_event_window_rightpanel_show();
                    break;
                case "mn4ScreenClear":
                    menu_event_window_blackwindow_clear();
                    break;
                case "mn4ShortCutKey":
                    menu_event_window_shortcutkey();
                    break;
                case "mn4BlackWindowColorFont":
                    menu_event_window_blackwindow_colorfont();
                    break;

                //help下的子菜单
                case "mn5About":
                    menu_event_help_about();
                    break;
                case "mn5Calculator":
                    menu_event_help_calculator();
                    break;
                case "mn5Help":
                    menu_event_help_help();
                    break;
                case "mn5NotePad":
                    menu_event_help_notepad();
                    break;
                case "mn5Paint":
                    menu_event_help_paint();
                    break;
            }
        }
        //文件子菜单
        private void menu_event_reconnect()
        {
            if (socket.Activ)
            {
                socket.Close();
                socket.Dispose();
            }
            
            InitSocket(loginInfo.b2b.lr.SERVER_IP, loginInfo.b2b.lr.SERVER_PORT);
        }
        private void menu_event_modifypassword()
        {
            General.PasswordModify dlg = new global::EagleForms.General.PasswordModify(loginInfo);
            dlg.ShowDialog();
            loginInfo.b2b.password = dlg.NEWPASSWORD;
        }
        private void menu_event_viewlog(bool today)
        {
            if (today)
            {
                EagleString.EagleFileIO.LogRead(Application.StartupPath + "\\Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            }
            else
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
                dlg.InitialDirectory = Application.StartupPath + "\\Log";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;//包括了绝对路径

                    EagleString.EagleFileIO.LogRead(filename);
                }
            }
        }
        private void menu_event_balance()
        {
            string s = EagleExtension.EagleExtension.BALANCE(loginInfo.b2b.username, loginInfo.b2b.webservice).ToString("f2");
            AppendBlackWindow("\r\n>您目前的余额为:" + s + "\r\n>");
        }
        private void menu_event_can_use_command()
        {
            string txt = "";
            int count = loginInfo.b2b.lr.m_ls_command.Count;
            for(int i=0;i<count;++i)
            {
                if ((i % 10) == 0) txt += "\r\n";
                txt += loginInfo.b2b.lr.m_ls_command[i] + ">";
            }
            AppendBlackWindow(txt);
        }
        private void menu_event_exit()
        {
            if (MessageBox.Show("真的要退出吗？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //打印子菜单
        private void menu_event_print_receipt()//权限代号003
        {
            if (!loginInfo.b2b.lr.AuthorityOfFunction("003"))
            {
                AppendBlackWindow("无权使用\r\n>");
                return;
            }
            tcMain.SelectedTab = tpReceipt;//触发indexchanged事件
        }
        private void menu_event_print_receipt_RWY()
        {
            if (receipt == null)
                receipt = new global::EagleForms.Printer.Receipt(socket, commandPool, loginInfo);
            
            receipt.Show();
            
        }
        //操作子菜单
        private void menu_event_operation_switchconfig()
        {
            ToCommand.SwitchVisableConfig dlg = new global::EagleForms.ToCommand.SwitchVisableConfig(loginInfo.b2b.lr);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] ips = dlg.LSIPID.ToArray();
                operation_switch_config(ips);
                mainToolBar.SetDropDownMenu(loginInfo.b2b.lr, ips[0]);
            }
        }
        private void operation_switch_config(string[] ips)
        {
            dataHandler.SetDestConfigIpids(ips);
            if (loginInfo.b2b.lr.IpidSameServer(ips[0]))
            {
                EagleString.EagleFileIO.LogWrite("Same Server!");
                socket.SendRegisterIPs(ips);
            }
            else
            {
                EagleString.EagleFileIO.LogWrite("Different Server! ");

                string serverip = "";
                int serverport = 0;
                loginInfo.b2b.lr.IpidServerPort(ips[0], ref serverip, ref serverport);
                EagleString.EagleFileIO.LogWrite("original server=" + socket.TcpIpServerIP + ":" + socket.TcpIpServerPort.ToString());
                EagleString.EagleFileIO.LogWrite("destination server="+serverip + ":" + serverport.ToString());
                InitSocket(serverip, serverport);
            }
        }
        private void menu_event_operation_etdz_one_key()
        {
            if (!loginInfo.b2b.lr.AuthorityOfCommand("etdz"))
            {
                AppendBlackWindow("无权使用\r\n>");
                return;
            }
            m_etdzonekey = new Structs.ETDZONEKEY("", "", loginInfo);
            ToCommand.EtdzOneKey dlg = new global::EagleForms.ToCommand.EtdzOneKey(m_etdzonekey,socket,commandPool);
            //非模式对话框:引用m_etdzonekey,commandPool,socket
            dlg.Show();
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    m_etdzonekey = new Structs.ETDZONEKEY(dlg.PNR, dlg.PAT);
            //    string cmd = commandPool.HandleCommand("rt" + m_etdzonekey.pnr);
            //    commandPool.SetType(ETERM_COMMAND_TYPE.ETDZ_ONEKEY_RT);
            //    socket.SendCommand(cmd, 3);
            //}
        }
        private void menu_event_operation_etdz_one_key(string pnr)
        {
            if (!loginInfo.b2b.lr.AuthorityOfCommand("etdz"))
            {
                AppendBlackWindow("无权使用\r\n>");
                return;
            }
            m_etdzonekey = new Structs.ETDZONEKEY("", "", loginInfo);
            ToCommand.EtdzOneKey dlg = new global::EagleForms.ToCommand.EtdzOneKey(m_etdzonekey, socket, commandPool);
            dlg.PNR = pnr;
            //非模式对话框:引用m_etdzonekey,commandPool,socket
            dlg.Show();
 
        }
        private void menu_event_operation_refund()
        {
            if (!loginInfo.b2b.lr.AuthorityOfCommand("trfd"))
            {
                AppendBlackWindow("无权使用\r\n>");
                return;
            }
            refundTicket = new global::EagleForms.ToCommand.RefundTicket(socket, commandPool, loginInfo);
            refundTicket.TopMost = true;
            refundTicket.Show();
        }
        private void menu_event_operation_submit_pnr_order()
        {
            ToCommand.PnrOrderSubmit dlg = new global::EagleForms.ToCommand.PnrOrderSubmit(commandPool,socket);
            dlg.Show();
        }
        private void menu_event_operation_queue_clear()
        {
            
            if ((!loginInfo.b2b.lr.AuthorityOfCommand("qt"))||(!loginInfo.b2b.lr.AuthorityOfFunction("QQQ")))
            {
                AppendBlackWindow("无权使用\r\n>");
                return;
            }
            if (queueClear == null)
            {
                queueClear = new global::EagleForms.ToCommand.QueueClear(socket, commandPool);
            }
            queueClear.Show();
        }
        private void menu_event_operation_aircode2pnr()
        {
            if (!loginInfo.b2b.lr.AuthorityOfCommand("rrt"))
            {
                AppendBlackWindow("无权使用\r\n>");
                return;
            }
            airCode2Pnr = new global::EagleForms.ToCommand.AirCode2Pnr(commandPool, socket);
            airCode2Pnr.Show();
        }
        private void menu_event_operation_history_cmd(bool next)
        {
            if (tcMain.SelectedIndex==0)//黑屏状态下才能使用历史指令
            {
                blackWindow.m_history.HistoryInsert((RichTextBox)blackWindow, next);
            }
            else
            {
            }
        }
        private void menu_event_operation_ibe2config()
        {
            m_ibeUsing = !m_ibeUsing;
        }

        //窗下子菜单事件
        private bool m_menu_blackwindow_max = false;
        private bool m_menu_rightpanel_show = true;
        private void menu_event_window_blackwindow_max()
        {
            if (!m_menu_blackwindow_max)
            {
                m_menu_blackwindow_max = true;
                pMain.Height += (pToolBar.Height + pNotice.Height);
                pMain.Location = new Point(pToolBar.Location.X, pToolBar.Location.Y);
                pToolBar.Visible = false;
                pNotice.Visible = false;
                OnResize();
            }
        }
        private void menu_event_window_blackwindow_normal()
        {
            if (m_menu_blackwindow_max)
            {
                m_menu_blackwindow_max = false;
                pMain.Height -= (pToolBar.Height + pNotice.Height);
                pMain.Location = new Point(pMain.Location.X, pMain.Location.Y + (pToolBar.Height + pNotice.Height));
                pToolBar.Visible = true;
                pNotice.Visible = true;
                OnResize();
            }
        }
        private void menu_event_window_rightpanel_show()
        {
            if (!m_menu_rightpanel_show)
            {
                pMain.Width -= pRight1.Width;
                pRight1.Location = new Point(pRight2.Location.X, pRight1.Location.Y);
                pRight1.Visible = true;
                pRight2.Visible = true;
                pRight3.Visible = true;
                pRight4.Visible = true;
                m_menu_rightpanel_show = true;
            }
        }
        private void menu_event_window_rightpanel_hide()
        {
            if (m_menu_rightpanel_show)
            {
                pMain.Width += pRight1.Width;
                pRight1.Visible = false;
                pRight2.Visible = false;
                pRight3.Visible = false;
                pRight4.Visible = false;
                m_menu_rightpanel_show = false;
            }
        }
        private void menu_event_window_blackwindow_clear()
        {
            blackWindow.Text = "";
        }
        private void menu_event_window_shortcutkey()
        {
            blackWindow.m_hotkey.SetHotKey();
        }
        private void menu_event_window_blackwindow_colorfont()
        {
            blackWindow.SetAttibutes();
        }
        //帮助下子菜单事件
        private void menu_event_help_about()
        {
            General.AboutBox dlg = new global::EagleForms.General.AboutBox();
            dlg.ShowDialog();
        }
        private void menu_event_help_help()
        {
            MessageBox.Show("文档编辑中……");
        }
        private void menu_event_help_notepad()
        {
            EagleString.EagleFileIO.RunProgram("NotePad.exe", "");
        }
        private void menu_event_help_calculator()
        {
            EagleString.EagleFileIO.RunProgram("Calc.exe", "");
        }
        private void menu_event_help_paint()
        {
            EagleString.EagleFileIO.RunProgram("mspaint.exe", "");
        }
    }
}
