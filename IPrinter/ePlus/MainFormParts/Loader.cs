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
    public partial class frmMain
    {
        private void wf_int()
        {
#if RWY//modified by king
            try { this.Icon = new Icon("ico.ico"); }
            catch { }
            //this.ShowIcon = false;
            this.Text = "单证打印";
            GlobalVar.exeTitle = this.Text;
            this.btnConnect.Visible = false;
            this.btnDisconnect.Visible = false;
            this.statusBar.Visible = false;
            this.tabControl.Alignment = TabAlignment.Top;
            this.Book.Text = "图形界面";
            this.Book.Image = null;
            this.Book.Visible = false;

            this.tsb_WebBrowser.Visible = false;
            this.toolStripSeparator7.Visible = false;//分割条
            this.toolStripSeparator8.Visible = false;
            this.toolStripSeparator9.Visible = false;
            this.toolStripSeparator10.Visible = false;
            this.toolStripSeparator6.Visible = false;
            this.toolStripSeparator5.Visible = false;
            this.toolStripSeparator4.Visible = false;

            //工具栏
            this.toolStrip.Visible = false;
            this.toolStripButton1.Visible = false;
            this.toolStripButton_ClearQ.Visible = false;//自动清Q
            this.toolStripButton2.Visible = false;//一键出票
            this.ttsddbBookModel.Visible = false;//快速模式
            this.toolStripButtonCall.Visible = false;//来电信息
            this.toolStripButtonNewOrder.Visible = false;
            this.tsSubmitPnr.Visible = false;

            splitContainer1.Panel1MinSize = 0;
            splitContainer1.Panel2MinSize = 0;
            //splitContainer1.SplitterDistance = 0;
            this.tabControl.TabPages[0].Text = "";// "黑屏";
            //this.tabControl.Selecting += new TabControlCancelEventHandler(tabControl_Selecting);

            //菜单
            MainMenu.Visible = false;
            miPrint.Visible = false;
            miPrintReceipt.Text = "行程单";
            miPrintInsurance.Visible = false;
            toolStripSeparator10.Visible = false;
            打印设置SToolStripMenuItem.Visible = false;
            toolStripSeparator8.Visible = false;
            航意险打印ToolStripMenuItem.Visible = false;
            旅游产品打印ToolStripMenuItem.Visible = false;
            行程单检查ToolStripMenuItem.Visible = false;
            miTools.Visible = false;
            航意险ToolStripMenuItem.Visible = false;
            miHelp.Visible = false;
            cTICToolStripMenuItem.Visible = false;

            this.miConnect.Visible = false;
            this.miDisconnect.Visible = false;
            miServerInfo.Visible = false;
            toolStripMenuItem2.Visible = false;

            切换用户SToolStripMenuItem.Visible = false;
            启动对帐程序ToolStripMenuItem.Visible = false;
            重新下载对帐程序ToolStripMenuItem.Visible = false;
            r恢复老对账程序ToolStripMenuItem.Visible = false;
            toolStripSeparator19.Visible = false;
            miSetup.Visible = false;

            this.Width = 680;
            this.Height = 450;
#endif
        }

        void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0)
                e.Cancel = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ReadCmdSendTypeFromOptionsTxt();

            EagleAPI.LogWrite("");//时间间隔

            //时间调校
            Thread thGetServerTime = new Thread(new ThreadStart(Options.gbOption.isDifferCompareTimeWithServerThan12));
            thGetServerTime.Start();
            //提示新订单ToolStripMenuItem.Checked = EagleAPI2.initNewOrder();

            EagleAPI.LogWrite("");//时间间隔

            try
            {
                GlobalVar.mainMenu = this.MainMenu;
                //this.Text = Properties.Resources.MainFormTitle;//commentted by king

                //EagleAPI.GetPrintConfig();
                //EagleAPI.GetOptions();

                {
                    //这里控制可用菜单
                    int iConflict = 0;
                    if (md.b_001) iConflict++;
                    if (md.b_006) iConflict++;
                    if (md.b_007) iConflict++;
                    if (md.b_009) iConflict++;
                    Model.md.SetBoolVars();

                    if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
                    {
                        BookTicket.bIbe = false;
                    }
                    //BookTicket.bIbe = Model.md.b_00E;//ＩＢＥ


                    this.Visible = Model.md.b_004;
                    #region 终端功能控制


                    if (Model.md.b_004)
                    {//若有黑屏
                        this.tsb_WebBrowser.Enabled = !md.b_00H;
                        this.style3.Enabled = Model.md.b_F12;//S3按钮
                        this.mi_style3.Enabled = Model.md.b_F12;
                        this.ｐＩＣＣToolStripMenuItem.Visible = Model.md.b_001;
                        this.miPrint.Visible = Model.md.b_002;
                        this.miPrintReceipt.Visible = Model.md.b_003;
                        //this.miPrintInsurance.Visible = Model.md.b_005;
                        this.Book.Enabled = Model.md.b_006;//简版

                        this.toolStripButton_ClearQ.Enabled = Model.md.b_QQQ;//自动清Q
                        this.永安保险ToolStripMenuItem.Visible = Model.md.b_007;
                        this.新华保险ToolStripMenuItem.Visible = Model.md.b_009;

                        this.交通意外伤害保险单ToolStripMenuItem.Visible = Model.md.b_B01;//华安
                        if (!Model.md.b_B01)
                            this.华安ToolStripMenuItem.Visible = false;

                        this.航空旅客人身意外伤害保险单ToolStripMenuItem.Visible = Model.md.b_B02;//人寿
                        if (!Model.md.b_B02)
                            this.人寿ToolStripMenuItem.Visible = false;

                        this.出行无忧ToolStripMenuItem.Visible = Model.md.b_B04;//都帮航意险

                        this.航翼网航空意外伤害保险单ToolStripMenuItem.Visible = Model.md.b_B03;//都邦出行无忧
                        this.出行乐ToolStripMenuItem.Visible = Model.md.b_B05;//都帮出行乐

                        if (!(Model.md.b_B03 || Model.md.b_B04 || Model.md.b_B05))
                            this.都邦ToolStripMenuItem.Visible = false;

                        this.SunShineToolStripMenuItem.Visible = Model.md.b_B07;//一路阳光

                        if (!(Model.md.b_B07))
                            this.阳光ToolStripMenuItem.Visible = false;

                        this.航翼网会员保险卡ToolStripMenuItem.Visible = Model.md.b_B08;//航翼网会员保险卡
                        if (!(Model.md.b_B08))
                            this.航翼网ToolStripMenuItem.Visible = false;

                        this.新华人寿ToolStripMenuItem.Visible = Model.md.b_B09;//易格网会员保险卡
                        this.安邦商行通ToolStripMenuItem.Visible = Model.md.b_B0B;//安邦商行通

                        this.pICCToolStripMenuItem.Visible = Model.md.b_B0D;//易格PICC
                        if (!(Model.md.b_B09 || Model.md.b_B0B || Model.md.b_B0D)) this.易格网ToolStripMenuItem.Visible = false;

                        this.周游列国.Visible = Model.md.b_B06;//周游列国
                        //if (!(Model.md.b_B06)) 
                        this.平安ToolStripMenuItem.Visible = false;

                        this.航空意外险ToolStripMenuItem.Visible = Model.md.b_B0A;//太平洋意外险
                        if (!(Model.md.b_B0A))
                            this.太平洋ToolStripMenuItem.Visible = false;

                        if (EagleAPI.GetCmdName("etdz", GlobalVar.loginLC.VisuableCommand) == "" || EagleAPI.GetCmdName("etdz", GlobalVar.loginLC.VisuableCommand) == null) toolStripButton2.Enabled = false;

                        if (!GlobalVar.bPekGuangShunUser) toolStripButton1.Visible = toolStripSeparator16.Visible = false;

                        this.cTICToolStripMenuItem.Visible = Model.md.b_0CTI;//呼叫中心

                    }
                    //else if (iConflict > 1)
                    //{//冲突，没有黑屏下，不能同时为保险及简版用户

                    //    MessageBox.Show("模块分配冲突，请与管理员联系！");
                    //    Application.Exit();
                    //}
                    else if (Model.md.b_006)
                    {//没黑屏，仅为简版用户

                        BookTicket bt = new BookTicket();
                        bt.Show();
                        BookTicket.bIbe = !Model.md.b_00F;
                        MessageBox.Show("您的帐户余额为：￥" + GlobalVar.f_CurMoney);
                    }

                    else if (Model.md.b_001)
                    {//没黑屏，仅为PICC保险打印
                        PrintHyx.PrintPICC2 pp = new ePlus.PrintHyx.PrintPICC2();
                        pp.Show();
                    }
                    else if (Model.md.b_007)
                    {//没黑屏，仅为永安保险打印
                        PrintHyx.Yongan ya = new ePlus.PrintHyx.Yongan();
                        ya.Show();
                    }
                    else if (Model.md.b_009)
                    {//新华保险
                        PrintHyx.NewChina nc = new ePlus.PrintHyx.NewChina();
                        nc.Show();
                    }
                    else if (Model.md.b_B01)
                    {//华安保险
                        PrintHyx.SinoSafe ss = new ePlus.PrintHyx.SinoSafe();
                        ss.Show();
                    }
                    else if (Model.md.b_B02)
                    {//人寿航意险

                        PrintHyx.ChinaLife cl = new ePlus.PrintHyx.ChinaLife();
                        cl.Show();
                    }
                    else if (Model.md.b_B03)
                    {//都帮航意险

                        PrintHyx.DuBang01 db1 = new ePlus.PrintHyx.DuBang01();
                        db1.Show();
                    }
                    else if (Model.md.b_B04)
                    {//都帮出行无忧
                        PrintHyx.DuBang02 db2 = new ePlus.PrintHyx.DuBang02();
                        db2.Show();
                    }
                    else if (Model.md.b_B05)
                    {//都帮出行乐

                        PrintHyx.DuBang02 db3 = new ePlus.PrintHyx.DuBang02();
                        db3.Dubang03();
                        db3.Show();
                    }
                    else if (Model.md.b_B06)
                    {
                        PrintHyx.PingAn01 pa = new ePlus.PrintHyx.PingAn01();
                        pa.Show();
                    }
                    else if (Model.md.b_B07)
                    {
                        PrintHyx.Sunshine ins = new ePlus.PrintHyx.Sunshine();
                        ins.Show();
                    }
                    else if (Model.md.b_B08)
                    {
                        PrintHyx.Hangyiwang ins = new ePlus.PrintHyx.Hangyiwang();
                        ins.Show();
                    }
                    else if (Model.md.b_B09)
                    {
                        PrintHyx.bxLogin bx = new ePlus.PrintHyx.bxLogin();
                        if (bx.ShowDialog() != DialogResult.OK) return;

                        PrintHyx.EagleIns ins = new ePlus.PrintHyx.EagleIns();
                        ins.Text = ins.lb公司名称.Text = "新华人寿保险股份有限公司意外伤害保险承保告知单";
                        ins.Show();
                    }
                    else if (Model.md.b_B0A)
                    {
                        PrintHyx.Pacific ins = new ePlus.PrintHyx.Pacific();
                        ins.Show();
                    }
                    else if (Model.md.b_B0B)
                    {
                        PrintHyx.EagleAnbang ea = new ePlus.PrintHyx.EagleAnbang();
                        ea.Show();
                    }
                    else if (Model.md.b_003)//只显示行程单打印
                    {
                        PrintReceipt pr = new PrintReceipt();

                        pr.Show();
                    }
                    else
                    {//无模块使用

                        //MessageBox.Show("您无权使用该系统，请让您的管理员打开必要权限");
                        //Application.Exit();
                    }

                    #endregion

                    this.ttsddbBookModel.Text = this.简易模式ToolStripMenuItem.Text;
                    GlobalVar.commandSendtype = GlobalVar.CommandSendType.Fast;
                    //
                    //this.ttsddbBookModel.Text = this.普通模式ToolStripMenuItem.Text;
                    //GlobalVar.commandSendtype = GlobalVar.CommandSendType.A;
                }

#if receipt
                
                this.Visible = false;
                PrintReceipt pr = new PrintReceipt();
                pr.Text = "ClawSoft - 行程单打印";
                pr.Icon = new Icon(Application.StartupPath + "\\claw.ico");
                pr.Show();
#endif
                if (Options.GlobalVar.QueryType == XMLConfig.QueryType.Eterm)
                {
                    #region//配置选择按钮增加ip
                    {
                        string[] ipls = GlobalVar.loginLC.IPsString.Split('~');
                        List<string> lsip = new List<string>();
                        List<string> addedIP = new List<string>();
                        for (int i = 0; i < ipls.Length; i++)
                        {

                            string[] cfgs = EagleAPI.GetConfigNumberByIP(ipls[i]).Split('~');//相同IP不同配置也能得到
                            if (addedIP.Contains(ipls[i])) continue;
                            addedIP.Add(ipls[i]);
                            for (int j = 0; j < cfgs.Length; j++)
                                lsip.Add(cfgs[j]);
                        }
                        //对cfg排序
                        lsip.Sort();
                        mi_CONFIG.DropDownItems.Add("全部配置");
                        for (int i = 0; i < lsip.Count; i++)
                        {
                            //mi_CONFIG.DropDownItems.Add(EagleAPI.GetConfigNumberByIP(ipls[i]));
                            if (md.b_00I || lsip[i].ToLower().IndexOf("tao") >= 0 || GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang || GlobalVar.serverAddr == GlobalVar.ServerAddr.KunMing)
                                mi_CONFIG.DropDownItems.Add(lsip[i]);
                        }




                        int theLastOne = lsip.Count - 1;
                        if (lsip.Count > 0)
                        {
                            //应指定当前所连接服务器的可用cfg的最后一个，而不是未排序的ip的最后一个


                            for (int i = lsip.Count - 1; i > 0; i--)
                            {
                                XmlDocument xd = new XmlDocument();
                                xd.LoadXml(GlobalVar.loginXml);
                                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("IPS");
                                for (int j = 0; j < xn.ChildNodes.Count; j++)
                                {
                                    if (lsip[i].Trim() == xn.ChildNodes[j].SelectSingleNode("PeiZhi").InnerText.Trim())
                                    {
                                        theLastOne = i;

                                        break;
                                    }
                                }
                            }
                            try
                            {
#if 全部配置
                        ((ToolStripMenuItem)(mi_CONFIG.DropDownItems[theLastOne + 1])).Checked = true;//启动时为最后一个，对黑屏用户有效

                        mi_CONFIG.Text = mi_CONFIG.DropDownItems[theLastOne + 1].Text;
                        GlobalVar.officeNumberCurrent = mi_CONFIG.Text;
#else
                                //bool bSpecifyDefaultConfig = false;
                                //for (int i = 0; i < mi_CONFIG.DropDownItems.Count; i++)
                                //{

                                //}
                                mi_CONFIG.Text = "全部配置";
                                ((ToolStripMenuItem)(mi_CONFIG.DropDownItems[0])).Checked = true;
#endif

                            }
                            catch
                            {
                                ((ToolStripMenuItem)(mi_CONFIG.DropDownItems[theLastOne])).Checked = true;
                                mi_CONFIG.Text = mi_CONFIG.DropDownItems[theLastOne].Text;
                                GlobalVar.officeNumberCurrent = mi_CONFIG.Text;

                            }
                        }
                        mi_CONFIG.DropDownItems.Add("-");
                        GlobalAPI.NotGlobal ng = new ePlus.GlobalAPI.NotGlobal();
                        List<string> ipgroup = ng.GetConfigGroupsBy(lsip);
                        for (int iip = 0; iip < ipgroup.Count; iip++)
                        {
                            //#if RWY
                            //                        break;
                            //#endif
                            mi_CONFIG.DropDownItems.Add(ipgroup[iip]);
                        }
                    }
                    #endregion

                    //Thread th = new Thread(new ThreadStart(connect));
                    //th.Start();
                    connect_1();
                }

#if !RWY
                this.Text = GlobalVar.exeTitle + "（服务器位置：" + GlobalVar.loginLC.SrvName + "）";
#endif
                //if (GlobalVar.loginLC.SrvName.IndexOf("外围") >= 0) BookTicket.bIbe = true;
                //timerNotice1();
            }
            catch (Exception ex1)
            {
                EagleAPI.LogWrite(ex1.Message);//时间间隔
            }
            initStatusBar();
            //SetNkgMode();

            instance = this;
            SetIA10();
            ShowIA();//added by king
            EagleAPI.LogWrite("init finished");//时间间隔
        }

        public EagleForms.Printer.PrintIA10.MiddleClassCallIA ia10 = new EagleForms.Printer.PrintIA10.MiddleClassCallIA();
 
        void SetIA10()
        {
            ia10.SetLoginInfo(GlobalVar.loginName, GlobalVar.loginPassword, EagleString.LineProvider.DianXin);
            ia10.SetSocket(Options.GlobalVar.socketGlobal);
            ia10.SetCommandPool();
        }
        /// <summary>
        /// 显示保险打印界面
        /// </summary>
        void ShowIA()
        {
            int index = this.tabControl.TabPages.Count;
            this.tabControl.TabPages.Add("打印保险");
            Form dlg = (Form)ia10.GetIA(Options.GlobalVar.IACode);
            dlg.TopLevel = false;
            dlg.FormBorderStyle = FormBorderStyle.None;
            tabControl.TabPages[index].Controls.Add(dlg);
            dlg.Show();
            tabControl.SelectedIndex = index;

            this.tabControl.TabPages.Add("出单记录");
            dlg = new EagleForms.Printer.FrmInsuranceList();
            dlg.TopLevel = false;
            dlg.FormBorderStyle = FormBorderStyle.None;
            dlg.Dock = DockStyle.Fill;
            tabControl.TabPages[index + 1].Controls.Add(dlg);
            dlg.Show();

            this.tabControl.TabPages.Add("后台管理");
            TabPage tp = tabControl.TabPages[tabControl.TabPages.Count - 1];
            WebBrowser wb = new WebBrowser();
            wb.Dock = DockStyle.Fill;
            wb.ScriptErrorsSuppressed = true;
            tp.Controls.Add(wb);
        }
        public static frmMain instance = null;
        /// <summary>
        /// 在link.txt文件中读取地址列表，有几个加几个按钮
        /// </summary>
        void ZzLink()
        {
            try
            {
                string[] links = File.ReadAllLines(Application.StartupPath + "\\link.txt",Encoding.Default);
                ToolStripButton[] btnLink = new ToolStripButton[links.Length];
                for (int i = 0; i < links.Length; i++)
                {
                    string title = links[i].Split(',')[0];
                    string icofile = links[i].Split(',')[1];
                    string dest = links[i].Split(',')[2];
                    Image image;
                    try
                    {
                        image = Image.FromFile(Application.StartupPath + "\\" + icofile);
                    }
                    catch
                    {
                        image = null;
                    }
                    btnLink[i] = new ToolStripButton(title, image, btnLinkClick);
                    btnLink[i].TextImageRelation = TextImageRelation.ImageAboveText;
                    btnLink[i].ToolTipText = dest;
                    
                }
                //if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
                {
                    this.toolStrip.Items.AddRange(btnLink);
                }
            }
            catch
            {
            }
        }
        void btnLinkClick(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            string prog = "C:\\Program Files\\Internet Explorer\\IEXPLORE.EXE";
            string prog2 = "D:\\Program Files\\Internet Explorer\\IEXPLORE.EXE";
            try
            {
                EagleString.EagleFileIO.RunProgram(prog, btn.ToolTipText);
            }
            catch
            {
                EagleString.EagleFileIO.RunProgram(prog2, btn.ToolTipText);
            }
        }
    }
}
