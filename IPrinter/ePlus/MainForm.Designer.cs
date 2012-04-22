using System.Windows.Forms;
namespace ePlus
{
    partial class frmMain
    {

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        internal void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripStatusLabel stiSysInfo;
            System.Windows.Forms.ToolStripStatusLabel stiDateTime;
            System.Windows.Forms.ToolStripStatusLabel stiPos;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.firstMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.miDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.miServerInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.切换用户SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.查看日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看今天日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.查看可用指令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.启动对帐程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新下载对帐程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.r恢复老对账程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.miShortKey = new System.Windows.Forms.ToolStripMenuItem();
            this.miOption = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绑定端口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印选定内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrintReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrintInsurance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.打印设置SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.航意险打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ｐＩＣＣToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.永安保险ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新华保险ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旅游产品打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.行程单检查ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miTools = new System.Windows.Forms.ToolStripMenuItem();
            this.miCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.miNotepad = new System.Windows.Forms.ToolStripMenuItem();
            this.miPainter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miNewClient = new System.Windows.Forms.ToolStripMenuItem();
            this.工具项TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.常用指令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提取大编码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.重发当前指令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.财务监听ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工作号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航意险ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.华安ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交通意外伤害保险单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人寿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航空旅客人身意外伤害保险单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航空意外保险单2008ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.都邦ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出行无忧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航翼网航空意外伤害保险单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出行乐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平安ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.周游列国 = new System.Windows.Forms.ToolStripMenuItem();
            this.阳光ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SunShineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航翼网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航翼网会员保险卡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.易格网ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新华人寿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.安邦商行通ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pICCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.太平洋ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.航空意外险ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miHowto = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.修复字体FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnHelpEagle20 = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.电子客票ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miETMANAGE_CHECK = new System.Windows.Forms.ToolStripMenuItem();
            this.testbuttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电子客票手动提交ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.提交PNRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eRP机票录入单插件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订座显示DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_style1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_style2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_style3 = new System.Windows.Forms.ToolStripMenuItem();
            this.国内票方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.国际票方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sD转换SSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.显示特价产品ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.强制指定配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.提示新订单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cTICToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.i签入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.o签出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.r录入客户基本信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.z转移内线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.sti黑屏政策 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stiIBE = new System.Windows.Forms.ToolStripStatusLabel();
            this.sti散拼 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sti订单 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stiSD2SS = new System.Windows.Forms.ToolStripStatusLabel();
            this.sti指定配置 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stiETERM = new System.Windows.Forms.ToolStripStatusLabel();
            this.sti抢占 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowserDaLianCTI = new System.Windows.Forms.WebBrowser();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnConnect = new System.Windows.Forms.ToolStripSplitButton();
            this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.style1 = new System.Windows.Forms.ToolStripButton();
            this.style2 = new System.Windows.Forms.ToolStripButton();
            this.style3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CAL = new System.Windows.Forms.ToolStripButton();
            this.Book = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_WebBrowser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTravel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mi_CONFIG = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton_ClearQ = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSubmitPnr = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ttsddbBookModel = new System.Windows.Forms.ToolStripDropDownButton();
            this.普通模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复杂模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.简易模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonNewOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCall = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDaLianCtiMessage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDaLianCtiPlayback = new System.Windows.Forms.ToolStripButton();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            stiSysInfo = new System.Windows.Forms.ToolStripStatusLabel();
            stiDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            stiPos = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // stiSysInfo
            // 
            stiSysInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            stiSysInfo.Name = "stiSysInfo";
            stiSysInfo.Size = new System.Drawing.Size(627, 17);
            stiSysInfo.Spring = true;
            stiSysInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stiDateTime
            // 
            stiDateTime.AutoSize = false;
            stiDateTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            stiDateTime.Name = "stiDateTime";
            stiDateTime.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            stiDateTime.Size = new System.Drawing.Size(40, 17);
            // 
            // stiPos
            // 
            stiPos.Name = "stiPos";
            stiPos.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            stiPos.Size = new System.Drawing.Size(2, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            toolStripStatusLabel1.Size = new System.Drawing.Size(6, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            toolStripStatusLabel2.Size = new System.Drawing.Size(6, 17);
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miSetup,
            this.打印PToolStripMenuItem,
            this.miTools,
            this.航意险ToolStripMenuItem,
            this.miHelp,
            this.miEdit,
            this.电子客票ToolStripMenuItem,
            this.订座显示DToolStripMenuItem,
            this.cTICToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 40);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1016, 25);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miClose,
            this.firstMenuSeparator,
            this.miConnect,
            this.miDisconnect,
            this.miServerInfo,
            this.toolStripMenuItem2,
            this.miChangePassword,
            this.切换用户SToolStripMenuItem,
            this.toolStripSeparator3,
            this.查看日志ToolStripMenuItem,
            this.查看今天日志ToolStripMenuItem,
            this.toolStripSeparator4,
            this.查看可用指令ToolStripMenuItem,
            this.toolStripSeparator18,
            this.启动对帐程序ToolStripMenuItem,
            this.重新下载对帐程序ToolStripMenuItem,
            this.r恢复老对账程序ToolStripMenuItem,
            this.toolStripSeparator19,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(58, 21);
            this.miFile.Text = "文件(&F)";
            // 
            // miNew
            // 
            this.miNew.Image = global::ePlus.Properties.Resources.NewFolderHS;
            this.miNew.Name = "miNew";
            this.miNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miNew.Size = new System.Drawing.Size(200, 22);
            this.miNew.Text = "新建";
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            // 
            // miClose
            // 
            this.miClose.Image = global::ePlus.Properties.Resources.DeleteFolderHS;
            this.miClose.Name = "miClose";
            this.miClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.miClose.Size = new System.Drawing.Size(200, 22);
            this.miClose.Text = "关闭";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // firstMenuSeparator
            // 
            this.firstMenuSeparator.Name = "firstMenuSeparator";
            this.firstMenuSeparator.Size = new System.Drawing.Size(197, 6);
            // 
            // miConnect
            // 
            this.miConnect.Image = global::ePlus.Properties.Resources.SearchWebHS;
            this.miConnect.Name = "miConnect";
            this.miConnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.miConnect.Size = new System.Drawing.Size(200, 22);
            this.miConnect.Text = "连接";
            // 
            // miDisconnect
            // 
            this.miDisconnect.Image = global::ePlus.Properties.Resources.DeleteHS;
            this.miDisconnect.Name = "miDisconnect";
            this.miDisconnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.miDisconnect.Size = new System.Drawing.Size(200, 22);
            this.miDisconnect.Text = "断开连接";
            this.miDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // miServerInfo
            // 
            this.miServerInfo.Name = "miServerInfo";
            this.miServerInfo.Size = new System.Drawing.Size(200, 22);
            this.miServerInfo.Text = "服务器信息";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
            // 
            // miChangePassword
            // 
            this.miChangePassword.ImageTransparentColor = System.Drawing.Color.Blue;
            this.miChangePassword.Name = "miChangePassword";
            this.miChangePassword.Size = new System.Drawing.Size(200, 22);
            this.miChangePassword.Text = "(&P)修改密码";
            this.miChangePassword.Click += new System.EventHandler(this.miChangePassword_Click);
            // 
            // 切换用户SToolStripMenuItem
            // 
            this.切换用户SToolStripMenuItem.Name = "切换用户SToolStripMenuItem";
            this.切换用户SToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.切换用户SToolStripMenuItem.Text = "(&S)重新登陆";
            this.切换用户SToolStripMenuItem.Click += new System.EventHandler(this.切换用户SToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // 查看日志ToolStripMenuItem
            // 
            this.查看日志ToolStripMenuItem.Name = "查看日志ToolStripMenuItem";
            this.查看日志ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.查看日志ToolStripMenuItem.Text = "(&L)查看日志";
            this.查看日志ToolStripMenuItem.Click += new System.EventHandler(this.查看日志ToolStripMenuItem_Click);
            // 
            // 查看今天日志ToolStripMenuItem
            // 
            this.查看今天日志ToolStripMenuItem.Name = "查看今天日志ToolStripMenuItem";
            this.查看今天日志ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.查看今天日志ToolStripMenuItem.Text = "(&T)查看今天日志";
            this.查看今天日志ToolStripMenuItem.Click += new System.EventHandler(this.查看今天日志ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(197, 6);
            // 
            // 查看可用指令ToolStripMenuItem
            // 
            this.查看可用指令ToolStripMenuItem.Name = "查看可用指令ToolStripMenuItem";
            this.查看可用指令ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.查看可用指令ToolStripMenuItem.Text = "(&C)查看可用指令";
            this.查看可用指令ToolStripMenuItem.Click += new System.EventHandler(this.查看可用指令ToolStripMenuItem_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(197, 6);
            // 
            // 启动对帐程序ToolStripMenuItem
            // 
            this.启动对帐程序ToolStripMenuItem.Name = "启动对帐程序ToolStripMenuItem";
            this.启动对帐程序ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.启动对帐程序ToolStripMenuItem.Text = "(&A)启动对帐程序";
            this.启动对帐程序ToolStripMenuItem.Click += new System.EventHandler(this.启动对帐程序ToolStripMenuItem_Click);
            // 
            // 重新下载对帐程序ToolStripMenuItem
            // 
            this.重新下载对帐程序ToolStripMenuItem.Name = "重新下载对帐程序ToolStripMenuItem";
            this.重新下载对帐程序ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.重新下载对帐程序ToolStripMenuItem.Text = "(&D)重新下载对帐程序";
            this.重新下载对帐程序ToolStripMenuItem.Click += new System.EventHandler(this.重新下载对帐程序ToolStripMenuItem_Click);
            // 
            // r恢复老对账程序ToolStripMenuItem
            // 
            this.r恢复老对账程序ToolStripMenuItem.Name = "r恢复老对账程序ToolStripMenuItem";
            this.r恢复老对账程序ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.r恢复老对账程序ToolStripMenuItem.Text = "(&R)恢复成老版对账程序";
            this.r恢复老对账程序ToolStripMenuItem.Click += new System.EventHandler(this.r恢复老对账程序ToolStripMenuItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(197, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(200, 22);
            this.miExit.Text = "(&X)退出";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miSetup
            // 
            this.miSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miShortKey,
            this.miOption,
            this.配置ToolStripMenuItem,
            this.绑定端口ToolStripMenuItem});
            this.miSetup.Name = "miSetup";
            this.miSetup.Size = new System.Drawing.Size(59, 21);
            this.miSetup.Text = "设置(&S)";
            // 
            // miShortKey
            // 
            this.miShortKey.Name = "miShortKey";
            this.miShortKey.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.K)));
            this.miShortKey.Size = new System.Drawing.Size(169, 22);
            this.miShortKey.Text = "(&K)快捷键";
            this.miShortKey.Click += new System.EventHandler(this.miShortKey_Click);
            // 
            // miOption
            // 
            this.miOption.Name = "miOption";
            this.miOption.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.miOption.Size = new System.Drawing.Size(169, 22);
            this.miOption.Text = "(&O)选项...";
            this.miOption.Click += new System.EventHandler(this.miOption_Click);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.配置ToolStripMenuItem.Text = "(&C)常用参数配置";
            this.配置ToolStripMenuItem.Click += new System.EventHandler(this.配置ToolStripMenuItem_Click);
            // 
            // 绑定端口ToolStripMenuItem
            // 
            this.绑定端口ToolStripMenuItem.Name = "绑定端口ToolStripMenuItem";
            this.绑定端口ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.绑定端口ToolStripMenuItem.Text = "(&P)绑定端口";
            this.绑定端口ToolStripMenuItem.ToolTipText = "指定连接配置服务器本地端口";
            this.绑定端口ToolStripMenuItem.Click += new System.EventHandler(this.绑定端口ToolStripMenuItem_Click);
            // 
            // 打印PToolStripMenuItem
            // 
            this.打印PToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打印选定内容ToolStripMenuItem,
            this.miPrint,
            this.miPrintReceipt,
            this.miPrintInsurance,
            this.toolStripSeparator10,
            this.打印设置SToolStripMenuItem,
            this.toolStripSeparator8,
            this.航意险打印ToolStripMenuItem,
            this.旅游产品打印ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.行程单检查ToolStripMenuItem});
            this.打印PToolStripMenuItem.Name = "打印PToolStripMenuItem";
            this.打印PToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.打印PToolStripMenuItem.Text = "打印(&P)";
            // 
            // 打印选定内容ToolStripMenuItem
            // 
            this.打印选定内容ToolStripMenuItem.Name = "打印选定内容ToolStripMenuItem";
            this.打印选定内容ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打印选定内容ToolStripMenuItem.Text = "打印选定内容";
            this.打印选定内容ToolStripMenuItem.Visible = false;
            this.打印选定内容ToolStripMenuItem.Click += new System.EventHandler(this.打印选定内容ToolStripMenuItem_Click);
            // 
            // miPrint
            // 
            this.miPrint.Name = "miPrint";
            this.miPrint.Size = new System.Drawing.Size(152, 22);
            this.miPrint.Text = "机票打印(&P)";
            this.miPrint.Click += new System.EventHandler(this.miPrint_Click);
            // 
            // miPrintReceipt
            // 
            this.miPrintReceipt.Name = "miPrintReceipt";
            this.miPrintReceipt.Size = new System.Drawing.Size(152, 22);
            this.miPrintReceipt.Text = "行程单打印(&R)";
            this.miPrintReceipt.Click += new System.EventHandler(this.miPrintReceipt_Click);
            // 
            // miPrintInsurance
            // 
            this.miPrintInsurance.Name = "miPrintInsurance";
            this.miPrintInsurance.Size = new System.Drawing.Size(152, 22);
            this.miPrintInsurance.Text = "保险单打印(&I)";
            this.miPrintInsurance.Visible = false;
            this.miPrintInsurance.Click += new System.EventHandler(this.miPrintInsurance_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(149, 6);
            // 
            // 打印设置SToolStripMenuItem
            // 
            this.打印设置SToolStripMenuItem.Name = "打印设置SToolStripMenuItem";
            this.打印设置SToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打印设置SToolStripMenuItem.Text = "打印设置(&S)";
            this.打印设置SToolStripMenuItem.Click += new System.EventHandler(this.打印设置SToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
            // 
            // 航意险打印ToolStripMenuItem
            // 
            this.航意险打印ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ｐＩＣＣToolStripMenuItem,
            this.永安保险ToolStripMenuItem,
            this.新华保险ToolStripMenuItem});
            this.航意险打印ToolStripMenuItem.Name = "航意险打印ToolStripMenuItem";
            this.航意险打印ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.航意险打印ToolStripMenuItem.Text = "航意险打印";
            this.航意险打印ToolStripMenuItem.Visible = false;
            // 
            // ｐＩＣＣToolStripMenuItem
            // 
            this.ｐＩＣＣToolStripMenuItem.Name = "ｐＩＣＣToolStripMenuItem";
            this.ｐＩＣＣToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ｐＩＣＣToolStripMenuItem.Text = "ＰＩＣＣ";
            this.ｐＩＣＣToolStripMenuItem.Click += new System.EventHandler(this.ｐＩＣＣToolStripMenuItem_Click);
            // 
            // 永安保险ToolStripMenuItem
            // 
            this.永安保险ToolStripMenuItem.Name = "永安保险ToolStripMenuItem";
            this.永安保险ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.永安保险ToolStripMenuItem.Text = "永安保险";
            this.永安保险ToolStripMenuItem.Click += new System.EventHandler(this.永安保险ToolStripMenuItem_Click);
            // 
            // 新华保险ToolStripMenuItem
            // 
            this.新华保险ToolStripMenuItem.Name = "新华保险ToolStripMenuItem";
            this.新华保险ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新华保险ToolStripMenuItem.Text = "新华保险";
            this.新华保险ToolStripMenuItem.Click += new System.EventHandler(this.新华保险ToolStripMenuItem_Click);
            // 
            // 旅游产品打印ToolStripMenuItem
            // 
            this.旅游产品打印ToolStripMenuItem.Name = "旅游产品打印ToolStripMenuItem";
            this.旅游产品打印ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.旅游产品打印ToolStripMenuItem.Text = "旅游产品打印";
            this.旅游产品打印ToolStripMenuItem.Click += new System.EventHandler(this.旅游产品打印ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // 行程单检查ToolStripMenuItem
            // 
            this.行程单检查ToolStripMenuItem.Name = "行程单检查ToolStripMenuItem";
            this.行程单检查ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.行程单检查ToolStripMenuItem.Text = "行程单检查";
            this.行程单检查ToolStripMenuItem.Click += new System.EventHandler(this.行程单检查ToolStripMenuItem_Click);
            // 
            // miTools
            // 
            this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCalc,
            this.miNotepad,
            this.miPainter,
            this.toolStripMenuItem1,
            this.miNewClient,
            this.工具项TToolStripMenuItem,
            this.toolStripSeparator14,
            this.重发当前指令ToolStripMenuItem,
            this.财务监听ToolStripMenuItem,
            this.工作号ToolStripMenuItem});
            this.miTools.Name = "miTools";
            this.miTools.Size = new System.Drawing.Size(59, 21);
            this.miTools.Text = "工具(&T)";
            // 
            // miCalc
            // 
            this.miCalc.Image = global::ePlus.Properties.Resources.CalculatorHS;
            this.miCalc.Name = "miCalc";
            this.miCalc.Size = new System.Drawing.Size(205, 22);
            this.miCalc.Text = "计算器";
            this.miCalc.Click += new System.EventHandler(this.miCalc_Click);
            // 
            // miNotepad
            // 
            this.miNotepad.Image = global::ePlus.Properties.Resources.NoteHS;
            this.miNotepad.Name = "miNotepad";
            this.miNotepad.Size = new System.Drawing.Size(205, 22);
            this.miNotepad.Text = "记事本";
            this.miNotepad.Click += new System.EventHandler(this.miNotepad_Click);
            // 
            // miPainter
            // 
            this.miPainter.Image = global::ePlus.Properties.Resources.ColorHS;
            this.miPainter.Name = "miPainter";
            this.miPainter.Size = new System.Drawing.Size(205, 22);
            this.miPainter.Text = "画板";
            this.miPainter.Click += new System.EventHandler(this.miPainter_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
            // 
            // miNewClient
            // 
            this.miNewClient.Name = "miNewClient";
            this.miNewClient.Size = new System.Drawing.Size(205, 22);
            this.miNewClient.Text = "新客户端";
            this.miNewClient.Visible = false;
            this.miNewClient.Click += new System.EventHandler(this.miNewClient_Click);
            // 
            // 工具项TToolStripMenuItem
            // 
            this.工具项TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常用指令ToolStripMenuItem,
            this.提取大编码ToolStripMenuItem});
            this.工具项TToolStripMenuItem.Name = "工具项TToolStripMenuItem";
            this.工具项TToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.工具项TToolStripMenuItem.Text = "工具项(&T)";
            // 
            // 常用指令ToolStripMenuItem
            // 
            this.常用指令ToolStripMenuItem.Name = "常用指令ToolStripMenuItem";
            this.常用指令ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.常用指令ToolStripMenuItem.Text = "常用指令";
            this.常用指令ToolStripMenuItem.Click += new System.EventHandler(this.常用指令ToolStripMenuItem_Click);
            // 
            // 提取大编码ToolStripMenuItem
            // 
            this.提取大编码ToolStripMenuItem.Name = "提取大编码ToolStripMenuItem";
            this.提取大编码ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.提取大编码ToolStripMenuItem.Text = "提取大编码";
            this.提取大编码ToolStripMenuItem.Click += new System.EventHandler(this.提取大编码ToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(202, 6);
            // 
            // 重发当前指令ToolStripMenuItem
            // 
            this.重发当前指令ToolStripMenuItem.Name = "重发当前指令ToolStripMenuItem";
            this.重发当前指令ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.重发当前指令ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.重发当前指令ToolStripMenuItem.Text = "重发当前指令";
            this.重发当前指令ToolStripMenuItem.Click += new System.EventHandler(this.重发当前指令ToolStripMenuItem_Click);
            // 
            // 财务监听ToolStripMenuItem
            // 
            this.财务监听ToolStripMenuItem.Name = "财务监听ToolStripMenuItem";
            this.财务监听ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.财务监听ToolStripMenuItem.Text = "财务监听";
            this.财务监听ToolStripMenuItem.Click += new System.EventHandler(this.财务监听ToolStripMenuItem_Click);
            // 
            // 工作号ToolStripMenuItem
            // 
            this.工作号ToolStripMenuItem.Name = "工作号ToolStripMenuItem";
            this.工作号ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.工作号ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.工作号ToolStripMenuItem.Text = "工作号";
            this.工作号ToolStripMenuItem.Click += new System.EventHandler(this.工作号ToolStripMenuItem_Click);
            // 
            // 航意险ToolStripMenuItem
            // 
            this.航意险ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.华安ToolStripMenuItem,
            this.人寿ToolStripMenuItem,
            this.都邦ToolStripMenuItem,
            this.平安ToolStripMenuItem,
            this.阳光ToolStripMenuItem,
            this.航翼网ToolStripMenuItem,
            this.易格网ToolStripMenuItem,
            this.太平洋ToolStripMenuItem});
            this.航意险ToolStripMenuItem.Name = "航意险ToolStripMenuItem";
            this.航意险ToolStripMenuItem.Size = new System.Drawing.Size(73, 21);
            this.航意险ToolStripMenuItem.Text = "航意险(&H)";
            // 
            // 华安ToolStripMenuItem
            // 
            this.华安ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.交通意外伤害保险单ToolStripMenuItem});
            this.华安ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("华安ToolStripMenuItem.Image")));
            this.华安ToolStripMenuItem.Name = "华安ToolStripMenuItem";
            this.华安ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.华安ToolStripMenuItem.Text = "华安";
            // 
            // 交通意外伤害保险单ToolStripMenuItem
            // 
            this.交通意外伤害保险单ToolStripMenuItem.Name = "交通意外伤害保险单ToolStripMenuItem";
            this.交通意外伤害保险单ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.交通意外伤害保险单ToolStripMenuItem.Text = "交通意外伤害保险单";
            this.交通意外伤害保险单ToolStripMenuItem.Click += new System.EventHandler(this.交通意外伤害保险单ToolStripMenuItem_Click);
            // 
            // 人寿ToolStripMenuItem
            // 
            this.人寿ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.航空旅客人身意外伤害保险单ToolStripMenuItem,
            this.航空意外保险单2008ToolStripMenuItem});
            this.人寿ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("人寿ToolStripMenuItem.Image")));
            this.人寿ToolStripMenuItem.Name = "人寿ToolStripMenuItem";
            this.人寿ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.人寿ToolStripMenuItem.Text = "人寿";
            // 
            // 航空旅客人身意外伤害保险单ToolStripMenuItem
            // 
            this.航空旅客人身意外伤害保险单ToolStripMenuItem.Name = "航空旅客人身意外伤害保险单ToolStripMenuItem";
            this.航空旅客人身意外伤害保险单ToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.航空旅客人身意外伤害保险单ToolStripMenuItem.Text = "航空旅客人身意外伤害保险单";
            this.航空旅客人身意外伤害保险单ToolStripMenuItem.Click += new System.EventHandler(this.航空旅客人身意外伤害保险单ToolStripMenuItem_Click);
            // 
            // 航空意外保险单2008ToolStripMenuItem
            // 
            this.航空意外保险单2008ToolStripMenuItem.Name = "航空意外保险单2008ToolStripMenuItem";
            this.航空意外保险单2008ToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.航空意外保险单2008ToolStripMenuItem.Text = "航空意外保险单(2008)";
            this.航空意外保险单2008ToolStripMenuItem.Click += new System.EventHandler(this.航空意外保险单2008ToolStripMenuItem_Click);
            // 
            // 都邦ToolStripMenuItem
            // 
            this.都邦ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.出行无忧ToolStripMenuItem,
            this.航翼网航空意外伤害保险单ToolStripMenuItem,
            this.出行乐ToolStripMenuItem});
            this.都邦ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("都邦ToolStripMenuItem.Image")));
            this.都邦ToolStripMenuItem.Name = "都邦ToolStripMenuItem";
            this.都邦ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.都邦ToolStripMenuItem.Text = "都邦";
            // 
            // 出行无忧ToolStripMenuItem
            // 
            this.出行无忧ToolStripMenuItem.Name = "出行无忧ToolStripMenuItem";
            this.出行无忧ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.出行无忧ToolStripMenuItem.Text = "出行无忧";
            this.出行无忧ToolStripMenuItem.Click += new System.EventHandler(this.出行无忧ToolStripMenuItem_Click);
            // 
            // 航翼网航空意外伤害保险单ToolStripMenuItem
            // 
            this.航翼网航空意外伤害保险单ToolStripMenuItem.Name = "航翼网航空意外伤害保险单ToolStripMenuItem";
            this.航翼网航空意外伤害保险单ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.航翼网航空意外伤害保险单ToolStripMenuItem.Text = "航翼网航空意外伤害保险单";
            this.航翼网航空意外伤害保险单ToolStripMenuItem.Click += new System.EventHandler(this.航翼网航空意外伤害保险单ToolStripMenuItem_Click);
            // 
            // 出行乐ToolStripMenuItem
            // 
            this.出行乐ToolStripMenuItem.Name = "出行乐ToolStripMenuItem";
            this.出行乐ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.出行乐ToolStripMenuItem.Text = "出行乐";
            this.出行乐ToolStripMenuItem.Click += new System.EventHandler(this.出行乐ToolStripMenuItem_Click);
            // 
            // 平安ToolStripMenuItem
            // 
            this.平安ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.周游列国});
            this.平安ToolStripMenuItem.Name = "平安ToolStripMenuItem";
            this.平安ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.平安ToolStripMenuItem.Text = "平安";
            // 
            // 周游列国
            // 
            this.周游列国.Name = "周游列国";
            this.周游列国.Size = new System.Drawing.Size(208, 22);
            this.周游列国.Text = "周游列国会员保险信息卡";
            this.周游列国.Click += new System.EventHandler(this.周游列国_Click);
            // 
            // 阳光ToolStripMenuItem
            // 
            this.阳光ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SunShineToolStripMenuItem});
            this.阳光ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("阳光ToolStripMenuItem.Image")));
            this.阳光ToolStripMenuItem.Name = "阳光ToolStripMenuItem";
            this.阳光ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.阳光ToolStripMenuItem.Text = "阳光";
            // 
            // SunShineToolStripMenuItem
            // 
            this.SunShineToolStripMenuItem.Name = "SunShineToolStripMenuItem";
            this.SunShineToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.SunShineToolStripMenuItem.Text = "一路阳光人身意外伤害保险单";
            this.SunShineToolStripMenuItem.Click += new System.EventHandler(this.SunShineToolStripMenuItem_Click);
            // 
            // 航翼网ToolStripMenuItem
            // 
            this.航翼网ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.航翼网会员保险卡ToolStripMenuItem});
            this.航翼网ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("航翼网ToolStripMenuItem.Image")));
            this.航翼网ToolStripMenuItem.Name = "航翼网ToolStripMenuItem";
            this.航翼网ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.航翼网ToolStripMenuItem.Text = "航翼网";
            // 
            // 航翼网会员保险卡ToolStripMenuItem
            // 
            this.航翼网会员保险卡ToolStripMenuItem.Name = "航翼网会员保险卡ToolStripMenuItem";
            this.航翼网会员保险卡ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.航翼网会员保险卡ToolStripMenuItem.Text = "航翼网会员保险卡";
            this.航翼网会员保险卡ToolStripMenuItem.Click += new System.EventHandler(this.航翼网会员保险卡ToolStripMenuItem_Click);
            // 
            // 易格网ToolStripMenuItem
            // 
            this.易格网ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新华人寿ToolStripMenuItem,
            this.安邦商行通ToolStripMenuItem,
            this.pICCToolStripMenuItem});
            this.易格网ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("易格网ToolStripMenuItem.Image")));
            this.易格网ToolStripMenuItem.Name = "易格网ToolStripMenuItem";
            this.易格网ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.易格网ToolStripMenuItem.Text = "易格网";
            // 
            // 新华人寿ToolStripMenuItem
            // 
            this.新华人寿ToolStripMenuItem.Name = "新华人寿ToolStripMenuItem";
            this.新华人寿ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.新华人寿ToolStripMenuItem.Text = "新华人寿";
            this.新华人寿ToolStripMenuItem.Click += new System.EventHandler(this.新华人寿ToolStripMenuItem_Click);
            // 
            // 安邦商行通ToolStripMenuItem
            // 
            this.安邦商行通ToolStripMenuItem.Name = "安邦商行通ToolStripMenuItem";
            this.安邦商行通ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.安邦商行通ToolStripMenuItem.Text = "安邦商行通";
            this.安邦商行通ToolStripMenuItem.Click += new System.EventHandler(this.安邦商行通ToolStripMenuItem_Click);
            // 
            // pICCToolStripMenuItem
            // 
            this.pICCToolStripMenuItem.Name = "pICCToolStripMenuItem";
            this.pICCToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.pICCToolStripMenuItem.Text = "PICC";
            this.pICCToolStripMenuItem.Click += new System.EventHandler(this.pICCToolStripMenuItem_Click);
            // 
            // 太平洋ToolStripMenuItem
            // 
            this.太平洋ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.航空意外险ToolStripMenuItem});
            this.太平洋ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("太平洋ToolStripMenuItem.Image")));
            this.太平洋ToolStripMenuItem.Name = "太平洋ToolStripMenuItem";
            this.太平洋ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.太平洋ToolStripMenuItem.Text = "太平洋";
            // 
            // 航空意外险ToolStripMenuItem
            // 
            this.航空意外险ToolStripMenuItem.Name = "航空意外险ToolStripMenuItem";
            this.航空意外险ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.航空意外险ToolStripMenuItem.Text = "航空意外险";
            this.航空意外险ToolStripMenuItem.Click += new System.EventHandler(this.航空意外险ToolStripMenuItem_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHowto,
            this.miAbout,
            this.修复字体FToolStripMenuItem,
            this.mnHelpEagle20});
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(61, 21);
            this.miHelp.Text = "帮助(&H)";
            // 
            // miHowto
            // 
            this.miHowto.Image = global::ePlus.Properties.Resources.Book_openHS;
            this.miHowto.Name = "miHowto";
            this.miHowto.Size = new System.Drawing.Size(173, 22);
            this.miHowto.Text = "使用帮助";
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(173, 22);
            this.miAbout.Text = "关于……";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // 修复字体FToolStripMenuItem
            // 
            this.修复字体FToolStripMenuItem.Name = "修复字体FToolStripMenuItem";
            this.修复字体FToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.修复字体FToolStripMenuItem.Text = "修复字体(&F)";
            this.修复字体FToolStripMenuItem.Click += new System.EventHandler(this.修复字体FToolStripMenuItem_Click);
            // 
            // mnHelpEagle20
            // 
            this.mnHelpEagle20.Name = "mnHelpEagle20";
            this.mnHelpEagle20.Size = new System.Drawing.Size(173, 22);
            this.mnHelpEagle20.Text = "启动Eagle2.0版本";
            this.mnHelpEagle20.Click += new System.EventHandler(this.mnHelpEagle20_Click);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCut,
            this.miCopy,
            this.miPaste});
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(59, 21);
            this.miEdit.Text = "编辑(&E)";
            this.miEdit.Visible = false;
            // 
            // miCut
            // 
            this.miCut.Image = global::ePlus.Properties.Resources.CutHS;
            this.miCut.Name = "miCut";
            this.miCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miCut.Size = new System.Drawing.Size(152, 22);
            this.miCut.Text = "剪切";
            this.miCut.Visible = false;
            this.miCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // miCopy
            // 
            this.miCopy.Image = global::ePlus.Properties.Resources.CopyHS;
            this.miCopy.Name = "miCopy";
            this.miCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miCopy.Size = new System.Drawing.Size(152, 22);
            this.miCopy.Text = "复制";
            this.miCopy.Visible = false;
            this.miCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // miPaste
            // 
            this.miPaste.Image = global::ePlus.Properties.Resources.PasteHS;
            this.miPaste.Name = "miPaste";
            this.miPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miPaste.Size = new System.Drawing.Size(152, 22);
            this.miPaste.Text = "粘贴";
            this.miPaste.Visible = false;
            this.miPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // 电子客票ToolStripMenuItem
            // 
            this.电子客票ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miETMANAGE_CHECK,
            this.testbuttonToolStripMenuItem,
            this.电子客票手动提交ToolStripMenuItem,
            this.提交PNRToolStripMenuItem,
            this.eRP机票录入单插件ToolStripMenuItem});
            this.电子客票ToolStripMenuItem.Name = "电子客票ToolStripMenuItem";
            this.电子客票ToolStripMenuItem.Size = new System.Drawing.Size(88, 21);
            this.电子客票ToolStripMenuItem.Text = "电子客票(&M)";
            this.电子客票ToolStripMenuItem.Visible = false;
            // 
            // miETMANAGE_CHECK
            // 
            this.miETMANAGE_CHECK.Name = "miETMANAGE_CHECK";
            this.miETMANAGE_CHECK.Size = new System.Drawing.Size(187, 22);
            this.miETMANAGE_CHECK.Text = "电子客票核对";
            this.miETMANAGE_CHECK.Visible = false;
            this.miETMANAGE_CHECK.Click += new System.EventHandler(this.miETMANAGE_CHECK_Click);
            // 
            // testbuttonToolStripMenuItem
            // 
            this.testbuttonToolStripMenuItem.Name = "testbuttonToolStripMenuItem";
            this.testbuttonToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.testbuttonToolStripMenuItem.Text = "testbutton";
            this.testbuttonToolStripMenuItem.Visible = false;
            this.testbuttonToolStripMenuItem.Click += new System.EventHandler(this.testbuttonToolStripMenuItem_Click);
            // 
            // 电子客票手动提交ToolStripMenuItem
            // 
            this.电子客票手动提交ToolStripMenuItem.Name = "电子客票手动提交ToolStripMenuItem";
            this.电子客票手动提交ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.电子客票手动提交ToolStripMenuItem.Text = "电子客票手动提交";
            this.电子客票手动提交ToolStripMenuItem.Visible = false;
            this.电子客票手动提交ToolStripMenuItem.Click += new System.EventHandler(this.电子客票手动提交ToolStripMenuItem_Click);
            // 
            // 提交PNRToolStripMenuItem
            // 
            this.提交PNRToolStripMenuItem.Name = "提交PNRToolStripMenuItem";
            this.提交PNRToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.提交PNRToolStripMenuItem.Text = "提交PNR";
            this.提交PNRToolStripMenuItem.Visible = false;
            this.提交PNRToolStripMenuItem.Click += new System.EventHandler(this.提交PNRToolStripMenuItem_Click);
            // 
            // eRP机票录入单插件ToolStripMenuItem
            // 
            this.eRP机票录入单插件ToolStripMenuItem.Checked = true;
            this.eRP机票录入单插件ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eRP机票录入单插件ToolStripMenuItem.Name = "eRP机票录入单插件ToolStripMenuItem";
            this.eRP机票录入单插件ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.eRP机票录入单插件ToolStripMenuItem.Text = "ERP-机票录入单插件";
            this.eRP机票录入单插件ToolStripMenuItem.Click += new System.EventHandler(this.eRP机票录入单插件ToolStripMenuItem_Click);
            // 
            // 订座显示DToolStripMenuItem
            // 
            this.订座显示DToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_style1,
            this.mi_style2,
            this.mi_style3,
            this.国内票方式ToolStripMenuItem,
            this.国际票方式ToolStripMenuItem,
            this.sD转换SSToolStripMenuItem,
            this.toolStripSeparator15,
            this.显示特价产品ToolStripMenuItem,
            this.toolStripSeparator17,
            this.强制指定配置ToolStripMenuItem,
            this.toolStripSeparator21,
            this.提示新订单ToolStripMenuItem});
            this.订座显示DToolStripMenuItem.Name = "订座显示DToolStripMenuItem";
            this.订座显示DToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.订座显示DToolStripMenuItem.Text = "订座显示(&D)";
            this.订座显示DToolStripMenuItem.Visible = false;
            // 
            // mi_style1
            // 
            this.mi_style1.Checked = true;
            this.mi_style1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mi_style1.Name = "mi_style1";
            this.mi_style1.Size = new System.Drawing.Size(148, 22);
            this.mi_style1.Text = "不显示";
            this.mi_style1.Visible = false;
            this.mi_style1.Click += new System.EventHandler(this.mi_style1_Click);
            // 
            // mi_style2
            // 
            this.mi_style2.Name = "mi_style2";
            this.mi_style2.Size = new System.Drawing.Size(148, 22);
            this.mi_style2.Text = "逐条显示";
            this.mi_style2.Visible = false;
            this.mi_style2.Click += new System.EventHandler(this.mi_style2_Click);
            // 
            // mi_style3
            // 
            this.mi_style3.Name = "mi_style3";
            this.mi_style3.Size = new System.Drawing.Size(148, 22);
            this.mi_style3.Text = "直接发送";
            this.mi_style3.Visible = false;
            this.mi_style3.Click += new System.EventHandler(this.mi_style3_Click);
            // 
            // 国内票方式ToolStripMenuItem
            // 
            this.国内票方式ToolStripMenuItem.Checked = true;
            this.国内票方式ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.国内票方式ToolStripMenuItem.Name = "国内票方式ToolStripMenuItem";
            this.国内票方式ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.国内票方式ToolStripMenuItem.Text = "国内票方式";
            this.国内票方式ToolStripMenuItem.Visible = false;
            this.国内票方式ToolStripMenuItem.Click += new System.EventHandler(this.国内票方式ToolStripMenuItem_Click);
            // 
            // 国际票方式ToolStripMenuItem
            // 
            this.国际票方式ToolStripMenuItem.Name = "国际票方式ToolStripMenuItem";
            this.国际票方式ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.国际票方式ToolStripMenuItem.Text = "国际票方式";
            this.国际票方式ToolStripMenuItem.Visible = false;
            this.国际票方式ToolStripMenuItem.Click += new System.EventHandler(this.国际票方式ToolStripMenuItem_Click);
            // 
            // sD转换SSToolStripMenuItem
            // 
            this.sD转换SSToolStripMenuItem.Checked = true;
            this.sD转换SSToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sD转换SSToolStripMenuItem.Name = "sD转换SSToolStripMenuItem";
            this.sD转换SSToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.sD转换SSToolStripMenuItem.Text = "SD转换SS";
            this.sD转换SSToolStripMenuItem.Click += new System.EventHandler(this.sD转换SSToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(145, 6);
            // 
            // 显示特价产品ToolStripMenuItem
            // 
            this.显示特价产品ToolStripMenuItem.Checked = true;
            this.显示特价产品ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.显示特价产品ToolStripMenuItem.Name = "显示特价产品ToolStripMenuItem";
            this.显示特价产品ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示特价产品ToolStripMenuItem.Text = "显示特价产品";
            this.显示特价产品ToolStripMenuItem.Click += new System.EventHandler(this.显示特价产品ToolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(145, 6);
            // 
            // 强制指定配置ToolStripMenuItem
            // 
            this.强制指定配置ToolStripMenuItem.Name = "强制指定配置ToolStripMenuItem";
            this.强制指定配置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.强制指定配置ToolStripMenuItem.Text = "强制指定配置";
            this.强制指定配置ToolStripMenuItem.ToolTipText = "选定后，AV等配置无关指令将不做轮询";
            this.强制指定配置ToolStripMenuItem.Click += new System.EventHandler(this.强制指定配置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(145, 6);
            // 
            // 提示新订单ToolStripMenuItem
            // 
            this.提示新订单ToolStripMenuItem.Name = "提示新订单ToolStripMenuItem";
            this.提示新订单ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.提示新订单ToolStripMenuItem.Text = "提示新订单";
            this.提示新订单ToolStripMenuItem.Click += new System.EventHandler(this.提示新订单ToolStripMenuItem_Click);
            // 
            // cTICToolStripMenuItem
            // 
            this.cTICToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.i签入ToolStripMenuItem,
            this.o签出ToolStripMenuItem,
            this.r录入客户基本信息ToolStripMenuItem,
            this.z转移内线ToolStripMenuItem});
            this.cTICToolStripMenuItem.Name = "cTICToolStripMenuItem";
            this.cTICToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.cTICToolStripMenuItem.Text = "CTI(&C)";
            // 
            // i签入ToolStripMenuItem
            // 
            this.i签入ToolStripMenuItem.Name = "i签入ToolStripMenuItem";
            this.i签入ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.i签入ToolStripMenuItem.Text = "(&I)签入";
            this.i签入ToolStripMenuItem.Click += new System.EventHandler(this.i签入ToolStripMenuItem_Click);
            // 
            // o签出ToolStripMenuItem
            // 
            this.o签出ToolStripMenuItem.Name = "o签出ToolStripMenuItem";
            this.o签出ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.o签出ToolStripMenuItem.Text = "(&O)签出";
            this.o签出ToolStripMenuItem.Click += new System.EventHandler(this.o签出ToolStripMenuItem_Click);
            // 
            // r录入客户基本信息ToolStripMenuItem
            // 
            this.r录入客户基本信息ToolStripMenuItem.Name = "r录入客户基本信息ToolStripMenuItem";
            this.r录入客户基本信息ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.r录入客户基本信息ToolStripMenuItem.Text = "(&R)录入客户基本信息";
            // 
            // z转移内线ToolStripMenuItem
            // 
            this.z转移内线ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12});
            this.z转移内线ToolStripMenuItem.Enabled = false;
            this.z转移内线ToolStripMenuItem.Name = "z转移内线ToolStripMenuItem";
            this.z转移内线ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.z转移内线ToolStripMenuItem.Text = "(&Z)转移内线";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem5.Text = "&0 线";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem6.Text = "&1 线";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem7.Text = "&2 线";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem8.Text = "&3 线";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem9.Text = "&4 线";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem10.Text = "&5 线";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem11.Text = "&6 线";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem12.Text = "&7 线";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            stiSysInfo,
            toolStripStatusLabel1,
            stiPos,
            toolStripStatusLabel2,
            stiDateTime,
            this.sti黑屏政策,
            this.stiIBE,
            this.sti散拼,
            this.sti订单,
            this.stiSD2SS,
            this.sti指定配置,
            this.stiETERM,
            this.sti抢占});
            this.statusBar.Location = new System.Drawing.Point(0, 0);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowItemToolTips = true;
            this.statusBar.Size = new System.Drawing.Size(1016, 22);
            this.statusBar.TabIndex = 1;
            // 
            // sti黑屏政策
            // 
            this.sti黑屏政策.AutoSize = false;
            this.sti黑屏政策.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sti黑屏政策.Name = "sti黑屏政策";
            this.sti黑屏政策.Size = new System.Drawing.Size(40, 17);
            this.sti黑屏政策.Text = "政策";
            this.sti黑屏政策.ToolTipText = "是否显示政策窗口";
            this.sti黑屏政策.Click += new System.EventHandler(this.sti黑屏政策_Click);
            // 
            // stiIBE
            // 
            this.stiIBE.AutoSize = false;
            this.stiIBE.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stiIBE.Name = "stiIBE";
            this.stiIBE.Size = new System.Drawing.Size(40, 17);
            this.stiIBE.Text = "IBE";
            this.stiIBE.ToolTipText = "是否使用IBE接口";
            this.stiIBE.Click += new System.EventHandler(this.stiIBE_Click);
            // 
            // sti散拼
            // 
            this.sti散拼.AutoSize = false;
            this.sti散拼.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sti散拼.Name = "sti散拼";
            this.sti散拼.Size = new System.Drawing.Size(40, 17);
            this.sti散拼.Text = "散拼";
            this.sti散拼.ToolTipText = "是否弹出特价产品(散客拼团)\\n红色:不弹出\\n绿色:弹出";
            this.sti散拼.Click += new System.EventHandler(this.sti散拼_Click);
            // 
            // sti订单
            // 
            this.sti订单.AutoSize = false;
            this.sti订单.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sti订单.Name = "sti订单";
            this.sti订单.Size = new System.Drawing.Size(40, 17);
            this.sti订单.Text = "订单";
            this.sti订单.ToolTipText = "是否提示新订单:\\n红色:不提示\\n绿色:提示";
            this.sti订单.Click += new System.EventHandler(this.sti订单_Click);
            // 
            // stiSD2SS
            // 
            this.stiSD2SS.AutoSize = false;
            this.stiSD2SS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stiSD2SS.Name = "stiSD2SS";
            this.stiSD2SS.Size = new System.Drawing.Size(40, 17);
            this.stiSD2SS.Text = "SD2SS";
            this.stiSD2SS.ToolTipText = "内部转换(sd->ss)";
            this.stiSD2SS.Click += new System.EventHandler(this.stiSD2SS_Click);
            // 
            // sti指定配置
            // 
            this.sti指定配置.AutoSize = false;
            this.sti指定配置.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sti指定配置.Name = "sti指定配置";
            this.sti指定配置.Size = new System.Drawing.Size(40, 17);
            this.sti指定配置.Text = "指定";
            this.sti指定配置.ToolTipText = "对av查询类指令强制指定配置";
            this.sti指定配置.Click += new System.EventHandler(this.sti指定配置_Click);
            // 
            // stiETERM
            // 
            this.stiETERM.AutoSize = false;
            this.stiETERM.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stiETERM.Name = "stiETERM";
            this.stiETERM.Size = new System.Drawing.Size(40, 17);
            this.stiETERM.Text = "ETERM";
            this.stiETERM.ToolTipText = "是否使用ETERM输入风格";
            this.stiETERM.Click += new System.EventHandler(this.stiETERM_Click);
            // 
            // sti抢占
            // 
            this.sti抢占.AutoSize = false;
            this.sti抢占.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sti抢占.Name = "sti抢占";
            this.sti抢占.Size = new System.Drawing.Size(40, 17);
            this.sti抢占.Text = "抢占";
            this.sti抢占.ToolTipText = "在未独占配置下，是否进行抢占";
            this.sti抢占.Click += new System.EventHandler(this.sti抢占_Click);
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusBar);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.AutoScroll = true;
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1016, 310);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(1016, 397);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Tag = "9999";
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.MainMenu);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowserDaLianCTI);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 310);
            this.splitContainer1.SplitterDistance = 71;
            this.splitContainer1.TabIndex = 1;
            // 
            // webBrowserDaLianCTI
            // 
            this.webBrowserDaLianCTI.AllowWebBrowserDrop = false;
            this.webBrowserDaLianCTI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDaLianCTI.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserDaLianCTI.Location = new System.Drawing.Point(0, 0);
            this.webBrowserDaLianCTI.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowserDaLianCTI.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDaLianCTI.Name = "webBrowserDaLianCTI";
            this.webBrowserDaLianCTI.ScrollBarsEnabled = false;
            this.webBrowserDaLianCTI.Size = new System.Drawing.Size(150, 71);
            this.webBrowserDaLianCTI.TabIndex = 1;
            this.webBrowserDaLianCTI.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1016, 310);
            this.tabControl.TabIndex = 0;
            this.tabControl.Tag = "";
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseClick);
            this.tabControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseDoubleClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripMargin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnDisconnect,
            this.toolStripSeparator2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.toolStripSeparator1,
            this.style1,
            this.style2,
            this.style3,
            this.toolStripButton_CAL,
            this.Book,
            this.toolStripSeparator7,
            this.tsb_WebBrowser,
            this.toolStripSeparator9,
            this.tsbTravel,
            this.toolStripSeparator6,
            this.mi_CONFIG,
            this.toolStripButton_ClearQ,
            this.toolStripSeparator12,
            this.tsSubmitPnr,
            this.toolStripSeparator13,
            this.toolStripButton1,
            this.toolStripSeparator16,
            this.toolStripButton2,
            this.toolStripSeparator5,
            this.ttsddbBookModel,
            this.toolStripButtonNewOrder,
            this.toolStripButtonCall,
            this.toolStripButtonDaLianCtiMessage,
            this.toolStripButtonDaLianCtiPlayback});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1016, 40);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Tag = "";
            // 
            // btnConnect
            // 
            this.btnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(48, 37);
            this.btnConnect.ToolTipText = "建立连接";
            this.btnConnect.ButtonClick += new System.EventHandler(this.btnConnect_ButtonClick);
            this.btnConnect.DropDownOpening += new System.EventHandler(this.btnConnect_DropDownOpening);
            this.btnConnect.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.btnConnect_DropDownItemClicked);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("btnDisconnect.Image")));
            this.btnDisconnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(36, 37);
            this.btnDisconnect.ToolTipText = "断开连接";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::ePlus.Properties.Resources.CutHS;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 37);
            this.btnCut.ToolTipText = "剪切";
            this.btnCut.Visible = false;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::ePlus.Properties.Resources.CopyHS;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 37);
            this.btnCopy.ToolTipText = "复制";
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::ePlus.Properties.Resources.PasteHS;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 37);
            this.btnPaste.ToolTipText = "粘贴";
            this.btnPaste.Visible = false;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            this.toolStripSeparator1.Visible = false;
            // 
            // style1
            // 
            this.style1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.style1.Image = ((System.Drawing.Image)(resources.GetObject("style1.Image")));
            this.style1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.style1.Name = "style1";
            this.style1.Size = new System.Drawing.Size(26, 37);
            this.style1.Text = "S1";
            this.style1.ToolTipText = "方式一：不显示";
            this.style1.Visible = false;
            this.style1.Click += new System.EventHandler(this.style1_Click);
            // 
            // style2
            // 
            this.style2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.style2.Image = ((System.Drawing.Image)(resources.GetObject("style2.Image")));
            this.style2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.style2.Name = "style2";
            this.style2.Size = new System.Drawing.Size(26, 37);
            this.style2.Text = "S2";
            this.style2.ToolTipText = "方式二：逐条显示";
            this.style2.Visible = false;
            this.style2.Click += new System.EventHandler(this.style2_Click);
            // 
            // style3
            // 
            this.style3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.style3.Image = ((System.Drawing.Image)(resources.GetObject("style3.Image")));
            this.style3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.style3.Name = "style3";
            this.style3.Size = new System.Drawing.Size(26, 37);
            this.style3.Text = "S3";
            this.style3.ToolTipText = "方式三：直接发送";
            this.style3.Visible = false;
            this.style3.Click += new System.EventHandler(this.style3_Click);
            // 
            // toolStripButton_CAL
            // 
            this.toolStripButton_CAL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_CAL.Image = global::ePlus.Properties.Resources.CalculatorHS;
            this.toolStripButton_CAL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CAL.Name = "toolStripButton_CAL";
            this.toolStripButton_CAL.Size = new System.Drawing.Size(23, 37);
            this.toolStripButton_CAL.Text = "toolStripButton1";
            this.toolStripButton_CAL.ToolTipText = "计算器";
            this.toolStripButton_CAL.Visible = false;
            this.toolStripButton_CAL.Click += new System.EventHandler(this.toolStripButton_CAL_Click);
            // 
            // Book
            // 
            this.Book.Image = ((System.Drawing.Image)(resources.GetObject("Book.Image")));
            this.Book.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Book.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Book.Name = "Book";
            this.Book.Size = new System.Drawing.Size(72, 37);
            this.Book.Text = "中文专业版";
            this.Book.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Book.ToolTipText = "简易订座方式";
            this.Book.Click += new System.EventHandler(this.Book_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 40);
            // 
            // tsb_WebBrowser
            // 
            this.tsb_WebBrowser.Image = ((System.Drawing.Image)(resources.GetObject("tsb_WebBrowser.Image")));
            this.tsb_WebBrowser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_WebBrowser.Name = "tsb_WebBrowser";
            this.tsb_WebBrowser.Size = new System.Drawing.Size(60, 37);
            this.tsb_WebBrowser.Text = "后台管理";
            this.tsb_WebBrowser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_WebBrowser.Click += new System.EventHandler(this.tsb_WebBrowser_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 40);
            // 
            // tsbTravel
            // 
            this.tsbTravel.Image = global::ePlus.Properties.Resources.SearchWebHS;
            this.tsbTravel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTravel.Name = "tsbTravel";
            this.tsbTravel.Size = new System.Drawing.Size(60, 37);
            this.tsbTravel.Text = "网站平台";
            this.tsbTravel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbTravel.Click += new System.EventHandler(this.tsbTravel_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 40);
            // 
            // mi_CONFIG
            // 
            this.mi_CONFIG.ForeColor = System.Drawing.Color.Blue;
            this.mi_CONFIG.Image = global::ePlus.Properties.Resources.PasteHS;
            this.mi_CONFIG.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mi_CONFIG.Name = "mi_CONFIG";
            this.mi_CONFIG.Size = new System.Drawing.Size(69, 37);
            this.mi_CONFIG.Text = "配置选择";
            this.mi_CONFIG.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mi_CONFIG.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mi_CONFIG_DropDownItemClicked);
            // 
            // toolStripButton_ClearQ
            // 
            this.toolStripButton_ClearQ.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_ClearQ.Image")));
            this.toolStripButton_ClearQ.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_ClearQ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ClearQ.Name = "toolStripButton_ClearQ";
            this.toolStripButton_ClearQ.Size = new System.Drawing.Size(58, 37);
            this.toolStripButton_ClearQ.Text = "自动清Q";
            this.toolStripButton_ClearQ.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton_ClearQ.Click += new System.EventHandler(this.toolStripButton_ClearQ_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 40);
            // 
            // tsSubmitPnr
            // 
            this.tsSubmitPnr.Image = ((System.Drawing.Image)(resources.GetObject("tsSubmitPnr.Image")));
            this.tsSubmitPnr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSubmitPnr.Name = "tsSubmitPnr";
            this.tsSubmitPnr.Size = new System.Drawing.Size(61, 37);
            this.tsSubmitPnr.Text = "PNR提交";
            this.tsSubmitPnr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSubmitPnr.ToolTipText = "PNR 提交";
            this.tsSubmitPnr.Click += new System.EventHandler(this.tsSubmitPnr_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(59, 37);
            this.toolStripButton1.Text = " ErpPnr ";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(60, 37);
            this.toolStripButton2.Text = "一键出票";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 40);
            // 
            // ttsddbBookModel
            // 
            this.ttsddbBookModel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.普通模式ToolStripMenuItem,
            this.复杂模式ToolStripMenuItem,
            this.简易模式ToolStripMenuItem});
            this.ttsddbBookModel.Image = ((System.Drawing.Image)(resources.GetObject("ttsddbBookModel.Image")));
            this.ttsddbBookModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttsddbBookModel.Name = "ttsddbBookModel";
            this.ttsddbBookModel.Size = new System.Drawing.Size(69, 37);
            this.ttsddbBookModel.Text = "普通模式";
            this.ttsddbBookModel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ttsddbBookModel.ToolTipText = "出票模式：普通模式（原国内票方式，默认），复杂模式（原国际票方式，速度最慢，耗资源最多，但最准确），快速模式（新增，速度最快，耗资源最少）";
            // 
            // 普通模式ToolStripMenuItem
            // 
            this.普通模式ToolStripMenuItem.Name = "普通模式ToolStripMenuItem";
            this.普通模式ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.普通模式ToolStripMenuItem.Text = "普通模式";
            this.普通模式ToolStripMenuItem.Click += new System.EventHandler(this.普通模式ToolStripMenuItem_Click);
            // 
            // 复杂模式ToolStripMenuItem
            // 
            this.复杂模式ToolStripMenuItem.Name = "复杂模式ToolStripMenuItem";
            this.复杂模式ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复杂模式ToolStripMenuItem.Text = "复杂模式";
            this.复杂模式ToolStripMenuItem.Click += new System.EventHandler(this.复杂模式ToolStripMenuItem_Click);
            // 
            // 简易模式ToolStripMenuItem
            // 
            this.简易模式ToolStripMenuItem.Name = "简易模式ToolStripMenuItem";
            this.简易模式ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.简易模式ToolStripMenuItem.Text = "快速模式";
            this.简易模式ToolStripMenuItem.Click += new System.EventHandler(this.简易模式ToolStripMenuItem_Click);
            // 
            // toolStripButtonNewOrder
            // 
            this.toolStripButtonNewOrder.Image = global::ePlus.Properties.Resources.PasteHS;
            this.toolStripButtonNewOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewOrder.Name = "toolStripButtonNewOrder";
            this.toolStripButtonNewOrder.Size = new System.Drawing.Size(48, 37);
            this.toolStripButtonNewOrder.Text = "新订单";
            this.toolStripButtonNewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonNewOrder.Click += new System.EventHandler(this.toolStripButtonNewOrder_Click);
            // 
            // toolStripButtonCall
            // 
            this.toolStripButtonCall.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCall.Image")));
            this.toolStripButtonCall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCall.Name = "toolStripButtonCall";
            this.toolStripButtonCall.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonCall.Text = "来电信息";
            this.toolStripButtonCall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonCall.Click += new System.EventHandler(this.toolStripButtonCall_Click);
            // 
            // toolStripButtonDaLianCtiMessage
            // 
            this.toolStripButtonDaLianCtiMessage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDaLianCtiMessage.Image")));
            this.toolStripButtonDaLianCtiMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDaLianCtiMessage.Name = "toolStripButtonDaLianCtiMessage";
            this.toolStripButtonDaLianCtiMessage.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonDaLianCtiMessage.Text = "留言查询";
            this.toolStripButtonDaLianCtiMessage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDaLianCtiMessage.Click += new System.EventHandler(this.toolStripButtonCtiMessage_Click);
            // 
            // toolStripButtonDaLianCtiPlayback
            // 
            this.toolStripButtonDaLianCtiPlayback.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDaLianCtiPlayback.Image")));
            this.toolStripButtonDaLianCtiPlayback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDaLianCtiPlayback.Name = "toolStripButtonDaLianCtiPlayback";
            this.toolStripButtonDaLianCtiPlayback.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonDaLianCtiPlayback.Text = "录音查询";
            this.toolStripButtonDaLianCtiPlayback.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDaLianCtiPlayback.Click += new System.EventHandler(this.toolStripButtonDaLianCtiPlayback_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(39, 16);
            this.toolStripStatusLabel5.Text = "ETERM";
            this.toolStripStatusLabel5.ToolTipText = "是否使用ETERM输入风格";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "normal");
            this.imageList1.Images.SetKeyName(1, "warning");
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 397);
            this.Controls.Add(this.toolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.Deactivate += new System.EventHandler(this.frmMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        void mnHelpEagle20_Click(object sender, System.EventArgs e)
        {
            RunProgram("eagle2.0.exe");
        }



        #endregion

        internal System.Windows.Forms.MenuStrip MainMenu;
        internal System.Windows.Forms.ToolStripMenuItem miFile;
        internal System.Windows.Forms.ToolStripMenuItem miEdit;
        internal System.Windows.Forms.ToolStripMenuItem miSetup;
        internal System.Windows.Forms.ToolStripMenuItem miTools;
        internal System.Windows.Forms.ToolStripMenuItem miHelp;
        internal System.Windows.Forms.ToolStripMenuItem miNew;
        internal System.Windows.Forms.ToolStripMenuItem miConnect;
        internal System.Windows.Forms.ToolStripMenuItem miDisconnect;
        internal System.Windows.Forms.ToolStripMenuItem miServerInfo;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        internal System.Windows.Forms.ToolStripMenuItem miChangePassword;
        internal System.Windows.Forms.ToolStripMenuItem miExit;
        internal System.Windows.Forms.ToolStripMenuItem miClose;
        internal System.Windows.Forms.ToolStripSeparator firstMenuSeparator;
        internal System.Windows.Forms.ToolStripMenuItem miCut;
        internal System.Windows.Forms.ToolStripMenuItem miCopy;
        internal System.Windows.Forms.ToolStripMenuItem miPaste;
        internal System.Windows.Forms.ToolStripMenuItem miShortKey;
        internal System.Windows.Forms.ToolStripMenuItem miOption;
        internal System.Windows.Forms.ToolStripMenuItem miCalc;
        internal System.Windows.Forms.ToolStripMenuItem miNotepad;
        internal System.Windows.Forms.ToolStripMenuItem miPainter;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem miNewClient;
        internal System.Windows.Forms.ToolStripMenuItem miHowto;
        internal System.Windows.Forms.ToolStripMenuItem miAbout;
        internal System.Windows.Forms.StatusStrip statusBar;
        internal System.Windows.Forms.ToolStripContainer toolStripContainer;
        internal System.Windows.Forms.TabControl tabControl;
        private System.ComponentModel.IContainer components;
        internal System.Windows.Forms.ToolStripMenuItem 订座显示DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mi_style1;
        private System.Windows.Forms.ToolStripMenuItem mi_style2;
        private System.Windows.Forms.ToolStripMenuItem mi_style3;
        private System.Windows.Forms.ToolStripMenuItem 打印PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miPrint;
        private System.Windows.Forms.ToolStripMenuItem miPrintReceipt;
        private System.Windows.Forms.ToolStripMenuItem miPrintInsurance;
        private System.Windows.Forms.ToolStripMenuItem 打印设置SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 查看日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem 航意险打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ｐＩＣＣToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 电子客票ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miETMANAGE_CHECK;
        internal System.Windows.Forms.ToolStrip toolStrip;
        internal System.Windows.Forms.ToolStripSplitButton btnConnect;
        internal System.Windows.Forms.ToolStripButton btnDisconnect;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton btnCut;
        internal System.Windows.Forms.ToolStripButton btnCopy;
        internal System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton style1;
        private System.Windows.Forms.ToolStripButton style2;
        private System.Windows.Forms.ToolStripButton style3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton Book;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsb_WebBrowser;
        public System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripMenuItem 打印选定内容ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButton_CAL;
        private System.Windows.Forms.ToolStripMenuItem 永安保险ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新华保险ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton mi_CONFIG;
        private System.Windows.Forms.ToolStripMenuItem testbuttonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 国内票方式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 国际票方式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sD转换SSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 航意险ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 华安ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 交通意外伤害保险单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人寿ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 航空旅客人身意外伤害保险单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 都邦ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出行无忧ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 航翼网航空意外伤害保险单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具项TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 常用指令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提取大编码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出行乐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_ClearQ;
        private System.Windows.Forms.ToolStripMenuItem 平安ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 周游列国;
        private System.Windows.Forms.ToolStripMenuItem 电子客票手动提交ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 提交PNRToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem 查看可用指令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsSubmitPnr;
        private System.Windows.Forms.ToolStripMenuItem 阳光ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SunShineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eRP机票录入单插件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem 显示特价产品ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旅游产品打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem 强制指定配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton ttsddbBookModel;
        private System.Windows.Forms.ToolStripMenuItem 普通模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复杂模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 简易模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem 启动对帐程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem 重新下载对帐程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem 提示新订单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 航翼网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 航翼网会员保险卡ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 易格网ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新华人寿ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 太平洋ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 航空意外险ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看今天日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel sti订单;
        private System.Windows.Forms.ToolStripStatusLabel sti散拼;
        private System.Windows.Forms.ToolStripStatusLabel stiSD2SS;
        private System.Windows.Forms.ToolStripStatusLabel sti指定配置;
        private System.Windows.Forms.ToolStripStatusLabel stiETERM;
        private System.Windows.Forms.ToolStripStatusLabel stiIBE;
        private System.Windows.Forms.ToolStripStatusLabel sti黑屏政策;
        private System.Windows.Forms.ToolStripStatusLabel sti抢占;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripMenuItem 修复字体FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换用户SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 安邦商行通ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绑定端口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 重发当前指令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 财务监听ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工作号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTICToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem i签入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem o签出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem r录入客户基本信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem z转移内线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem 航空意外保险单2008ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pICCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem r恢复老对账程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewOrder;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCall;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowserDaLianCTI;
        private System.Windows.Forms.ToolStripButton toolStripButtonDaLianCtiMessage;
        private System.Windows.Forms.ToolStripButton toolStripButtonDaLianCtiPlayback;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem 行程单检查ToolStripMenuItem;
        private ToolStripMenuItem mnHelpEagle20;
        private ToolStripButton tsbTravel;
    }
}

