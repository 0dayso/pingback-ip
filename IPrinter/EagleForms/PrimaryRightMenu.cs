using System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EagleString;
using EagleControls;
using EagleProtocal;
using EagleExtension;

namespace EagleForms
{
    public partial class Primary
    {
        ContextMenuStrip rightMenu = new ContextMenuStrip();
        void blackWindow_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 123 || m_littleenter)//修改为是否为发送指令
                {
                    m_littleenter = false;//小回车标志复原
                    string command = blackWindow.EagleCommand;
                    if (string.IsNullOrEmpty(command)) return;
                    if (!loginInfo.b2b.lr.AuthorityOfCommand(command))
                    {
                        AppendBlackWindow("\r\n***没有该指令或者您没有权限使用该指令\r\n>");
                        return;
                    }
                    string history = command;
                    command = commandPool.HandleCommand(command);
                    if (!socket.Activ)
                    {
                        AppendBlackWindow("\r\n***未连接服务器\r\n>");
                    }
                    else
                    {
                        CheckBeforeSend();
                        blackWindow.m_history.History_Add(history);
                        blackWindow.AppendResult("\r\n>");
                        socket.SendCommand(command, EagleProtocal.TypeOfCommand.Multi);
                    }
                }
                else if (e.KeyValue == 13 && !m_littleenter)
                {
                    string txt = blackWindow.EagleCommand;
                    egCommandFixed(txt);
                }
                try
                {
                    blackWindow.InsertShortCutEtermCommand(e);
                    blackWindow.ShowShortCutFunction(e);
                    blackWindow.ShowCommandFunction(e);
                }
                catch (Exception ex)//此处添加快键功能
                {
                    BlackWindowShortCutHandle(ex.Message);
                }
            }
            catch (Exception ex)
            {
                AppendBlackWindow(ex.Message + "\r\n>");
            }
        }
        /// <summary>
        /// 摘要：固定格式的eg指令
        /// </summary>
        /// <param name="egcmd"></param>
        private void egCommandFixed(string egcmd)
        {
            switch (egcmd.ToUpper())
            {
                case "EG CREATE TEST PNR":
                    socket.SendCommand("i~av h pvg+~sd1y1\rnm1ce/shi\rct123\rtktl2300/./wuh128\r@", EagleProtocal.TypeOfCommand.Multi);
                    return;
                case "CP":
                    blackWindow.Text = ">";
                    return;
                case "EG BALANCE":
                    menu_event_balance();
                    return;
                case "EG O":
                    displayOffice();
                    return;
                case "EG EXPFIND":
                    expireTicketFinder.Run();
                    return;
                case "EG EXPSETUP":
                    General.frmExpireTicketFind finderSetup = new global::EagleForms.General.frmExpireTicketFind();
                    if (finderSetup.ShowDialog() == DialogResult.OK)
                    {
                        InitExpireTicketFinder();
                        expireTicketFinder.Run();
                    }
                    return;
                case "EG TEST":
                    OuterEtdzOneKey("ABCDE");
                    break;

            }
            //B2C易格指令
            if (egcmd.ToUpper().StartsWith("KLD") || egcmd.ToUpper().StartsWith("KHZLLD"))
            {
                m_b2c_kldinfo = EagleDllImport.CtiFunction.kld(egcmd.Trim(), loginInfo.b2c.webservice);
            }
            else if (egcmd.ToUpper().StartsWith("EGYD"))
            {
                string temp = EagleDllImport.CtiFunction.egyd(
                    egcmd.Trim(),
                    m_b2c_kldinfo,
                    DateTime.Now.AddHours(2).ToString("hhmm"),
                    DateTime.Now.AddHours(2).ToString("ddMMM", EagleString.egString.dtFormat),
                    loginInfo.b2b.lr.UsingOffice()
                );
                AppendBlackWindow(temp);
            }
            else if (egcmd.ToUpper().StartsWith("BAOXIAN"))
            {
                AppendBlackWindow(EagleDllImport.CtiFunction.baoxian(egcmd.Trim(), m_b2c_baoxianPnr));
            }
        }
        string m_b2c_kldinfo = "";
        Hashtable m_b2c_baoxianPnr = new Hashtable();
        /// <summary>
        /// 打印所有配置
        /// </summary>
        void displayOffice()
        {
            for (int i = 0; i < loginInfo.b2b.lr.m_ls_office.Count; i++)
            {
                OFFICE_INFO oi = loginInfo.b2b.lr.m_ls_office[i];
                string d = oi.IP_ID + " " + oi.SERVER_IP + ":" + oi.SERVER_PORT + " \n";
                AppendBlackWindow(d);
            }
        }
        void blackWindow_MouseUp(object sender, MouseEventArgs e)
        {
            rightMenu.Items.Clear();
            rightMenu.Items.Add("(&C)复制", null, new EventHandler(copy));
            rightMenu.Items.Add("(&V)粘贴", null, new EventHandler(paste));
            rightMenu.Items.Add("-");
            rightMenu.Items.Add("(&P)打印选定内容", null, new EventHandler(print));
            rightMenu.Items.Add("-");
            rightMenu.Items.Add("(&T)提交PNR", null, new EventHandler(pnrsubmit));
            rightMenu.Items.Add("-");
            rightMenu.Items.Add("(&H)舱位申请处理", null, new EventHandler(bunkapplyhandle));
            //rightMenu.Items.Add("属性");
            if (e.Button == MouseButtons.Right)
            {
                rightMenu.Show(blackWindow, new Point(e.X, e.Y));
            }
        }
        void copy(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(blackWindow.SelectedText, true, 5, 10);
        }
        void cut(object sender, EventArgs e)
        {
        }
        void paste(object sender, EventArgs e)
        {
            blackWindow.InsertString(Clipboard.GetText(TextDataFormat.Text));
        }
        void print(object sender, EventArgs e)
        {
            PrintDocument ptDoc = new PrintDocument();
            ptDoc.PrintPage += new PrintPageEventHandler(ptDoc_PrintPage);
            ptDoc.DocumentName = "Eagle屏幕打印";
            ptDoc.Print();
        }
        void pnrsubmit(object sender, EventArgs e)
        {
            ToCommand.PnrOrderSubmit dlg = new global::EagleForms.ToCommand.PnrOrderSubmit(commandPool, socket);
            dlg.Show();
        }
        void bunkapplyhandle(object sender, EventArgs e)
        {
            
            General.SpecTickHandle ef =
                new General.SpecTickHandle(loginInfo.b2b.webservice, loginInfo.b2b.username);
            ef.dg_spectick = new General.SpecTickHandle.deleg4SepcTick(socket.Send);
            ef.Show();
        }
        int pages = 0;
        int curpage = 0;

        void ptDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            string ptstr = blackWindow.SelectedText;
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Font ptFontCn = new Font("system", 10.5F, System.Drawing.FontStyle.Regular);
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

        private EagleNotifyIcon.EagleNotify m_notifyIcon = new EagleNotifyIcon.EagleNotify(
new string[] { 
                Application.StartupPath+"\\e0.ico",
                Application.StartupPath+"\\e1.ico",
                Application.StartupPath+"\\e2.ico",
                Application.StartupPath+"\\e3.ico",
                Application.StartupPath+"\\e4.ico",
            });

        private void BlackWindowShortCutHandle(string key)
        {
            switch (key)
            {
                case "打印行程单":
                    menu_event_print_receipt();
                    break;
                case "设置黑屏界面":
                    menu_event_window_blackwindow_colorfont();
                    break;
                case "隐藏右面板":
                    menu_event_window_rightpanel_hide();
                    break;
                case "显示右面板":
                    menu_event_window_rightpanel_show();
                    break;
                case "黑屏全屏化":
                    menu_event_window_blackwindow_max();
                    break;
                case "黑屏还原全屏":
                    menu_event_window_blackwindow_normal();
                    break;
                case "上一条指令":
                    menu_event_operation_history_cmd(false);
                    break;
                case "下一条指令":
                    menu_event_operation_history_cmd(true);
                    break;
                case "查看今天日志":
                    menu_event_viewlog(true);
                    break;
                case "查看日志":
                    menu_event_viewlog(false);
                    break;
                case "查看余额":
                    menu_event_balance();
                    break;
                case "清屏":
                    menu_event_window_blackwindow_clear();
                    break;
                case "界面_中文专业版":
                    tcMain.SelectedTab = tpEasy;
                    break;
                case "界面_后台管理":
                    tcMain.SelectedTab = tpManager;
                    break;
                case "界面_对帐平台":
                    tcMain.SelectedTab = tpFinance;
                    break;
                case "手动退票":
                    menu_event_operation_refund();
                    break;
                case "一键出票":
                    menu_event_operation_etdz_one_key();
                    break;
                case "自动清Q":
                    menu_event_operation_queue_clear();
                    break;
                case "PNR订单提交":
                    menu_event_operation_submit_pnr_order();
                    break;
                case "修改B2B帐号密码":
                    menu_event_modifypassword();
                    break;
                case "计算器":
                    menu_event_help_calculator();
                    break;
                case "记事本":
                    menu_event_help_notepad();
                    break;
                case "画板":
                    menu_event_help_paint();
                    break;
                case "大编转小编":
                    menu_event_operation_aircode2pnr();
                    break;
                case "切换可用配置":
                    menu_event_operation_switchconfig();
                    break;
                case "黑屏快捷键设置":
                    menu_event_window_shortcutkey();
                    break;
                case "B2C设置":
                    break;
                case "查看可使用指令":
                    menu_event_can_use_command();
                    break;
                case "切换IBE或配置":
                    menu_event_operation_ibe2config();
                    break;
                case "界面_黑屏":
                    tcMain.SelectedTab = tpBlack;
                    break;
                case "打印保险单":
                    MessageBox.Show("保险单打印不能设置快键");
                    break;


            }
            //            打印行程单 = 0,             //--mnPrint Finished                 toolbar

            //设置黑屏界面 = 1,           //--mnWindow          7
            //隐藏右面板 = 2,             //--mnWindow          5
            //显示右面板 = 3,             //--mnWindow          4
            //黑屏全屏化 = 4,             //--mnWindow          2
            //黑屏还原全屏 = 5,           //--mnWindow          3
            //上一条指令 = 6,             //--mnOperation 6
            //下一条指令 = 7,             //--mnOperation 7
            //查看今天日志 = 8,           //--mnFile Finished 
            //查看日志 = 9,               //--mnFile Finished 
            //查看余额 = 10,              //--mnFile Finished 
            //清屏 = 11,                  //--mnWindow           1                     toolbar

            //界面_中文专业版 = 12,            //--
            //界面_后台管理 = 13,              //--
            //界面_对帐平台 = 14,              //--
            //手动退票 = 15,              //--mnOperation 2                     toolbar
            //一键出票 = 16,              //--mnOperation 1                     toolbar
            //自动清Q = 17,               //--mnOperation 4
            //PNR订单提交 = 18,           //--mnOperation 3                     toolbar
            //修改B2B帐号密码 = 19,       //--mnFile Finished
            //计算器 = 20,                //--mnHelp                             1
            //记事本 = 21,                //--mnHelp                             2
            //画板 = 22,                  //--mnHelp                             3
            //大编转小编 = 23,            //--mnOperation 5
            //切换可用配置 = 24,          //--mnOperation 0     Finished                toolbar
            ////25空出,有重复被删除
            //黑屏快捷键设置 = 26,        //--mnWindow            6
            //B2C设置 = 27,               //--mnHelp                             4
            //查看可使用指令 = 28,        //--mnFile Finished
            //切换IBE或配置 = 29,         //--mnOperation 8

            //界面_黑屏 = 30,              //--
            //打印保险单 = 31                //--mnPrint Finished 
        }
    }
}
