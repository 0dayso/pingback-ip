using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using gs.para;
using System.Net.Sockets;
using System.Net;

namespace ePlus
{
    class GlobalVar
    {
        
        static public bool printProgram = true;//软件版本控制：true为打印版本。false为正常订票版本。
        static public bool bHakUser = false;//是否为hak
        static public bool bPekGuangShunUser = false;
        public static int PackageNumberSend = 0;
        public static int PackageNumberRecv = 0;
        //是否重启或切换用户
        static public bool gbIsRestartEagle = false;
        //全局临时字符串变量
        public static string GlobalString = "";

        public static bool b_OffLine = false;
        public static bool b_test = false;
        //是否为航意险专用客户端
        public static bool b_黑屏 = true;
        //打印设置全局变量
        static public PointF o_ticket = new PointF(0F, 0F);
        static public PointF o_receipt = new PointF(0F, 0F);
        static public PointF o_insurance = new PointF(0F, 0F);
        static public float fontsizecn = 10;
        static public float fontsizeen = 8;

        //电子客票行程单用是否 客票号提取
        static public bool b_rtByEticket = false;

        //登陆信息
        static public string loginNameOfBtoC = "";
        static public string loginName = "";
        static public string loginPassword = "";
        static public bool loginSuccess = false;
        static public Login_Classes loginLC = new Login_Classes();
        static public string loginXml = "";
        static public string DaLianCTIPhoneNumber = string.Empty;//大连呼叫中心 分机号码

        //用于嵌入浏览器
        public static string Notice = "\r\n系统提示：";
        public static string WaitString = "请稍等……";
        public static string EagleWebString = "后台管理";
        public static string exeTitle = "Eagle 航空订票系统";
        public static string serverTitle = "";
        //
        public static bool etProcessing = false;

        public static int connCount = 0;
        public static List<string> lsWangtongServer = new List<string>();
        public static List<string> lsDianxin = new List<string>();

        public enum ServerAddr { Eagle, HangYiWang, Claw, ZhenZhouJiChang, NKG, KunMing, NA };
        static public GlobalVar.ServerAddr serverAddr = ServerAddr.Eagle;
        
#if !shenguangtong
        public static string WebUrl = "http://yinge.eg66.com/EagleWeb2/login.aspx";
        public static string WebServer = "http://yinge.eg66.com:80/WS3/egws.asmx";
        
        //网通
        public static string weburl_WT = "http://wangtong.eg66.com/EagleWeb2/login.aspx";
        public static string webserver_WT = "http://wangtong.eg66.com:80/WS3/egws.asmx";
        
        //电信
        public static string weburl_DX = "http://yinge.eg66.com/EagleWeb2/login.aspx";
        public static string webserver_DX = "http://yinge.eg66.com:80/WS3/egws.asmx";
        

        //public static string WebUrl = "http://hbpiao.3322.org/EagleWeb2/login.aspx";
        //public static string WebServer = "http://hbpiao.3322.org:81/WS2/egws.asmx";

        ////网通
        //public static string weburl_WT = "http://hbpiao.3322.org/EagleWeb2/login.aspx";
        //public static string webserver_WT = "http://hbpiao.3322.org:81/WS2/egws.asmx";

        ////电信
        //public static string weburl_DX = "http://hbpiao.3322.org/EagleWeb2/login.aspx";
        //public static string webserver_DX = "http://hbpiao.3322.org:81/WS2/egws.asmx";
#else
        public static string WebUrl = "http://10.10.18.18/et/login.aspx";
        public static string WebServer = "http://10.10.18.18/ws/egws.asmx";

        //网通
        public static string weburl_WT = "http://10.10.18.18/et/login.aspx";
        public static string webserver_WT = "http://10.10.18.18/ws/egws.asmx";

        //电信
        public static string weburl_DX = "http://10.10.18.18/et/login.aspx";
        public static string webserver_DX = "http://10.10.18.18/ws/egws.asmx";
#endif
        public static string LocalCityCode = "WUH";
        public static string SelectCityType = "0";
        //加入日志的指令表
        //public static string LogCmdString = "rT~ss~sd~sp~nm~etdz~dz~@~\\~";

        //用于配置切换

        public static bool b_cfg_First = true;
        public static string str_cfg_name = "";//当前使用配置的ID。
        public static bool bSwichConfigByManual = false;
        public static bool bSwichConfigAuto = false;
        public static bool bSwiching = false;//正在切换配置，防止同帐号重复登陆而被踢出
        /// <summary>
        /// 当前使用的配置ID
        /// </summary>
        public static string current_using_config = "";//IpId
        public static string CurIPUsing = "";
        public static string etdz_config = "";
        /// <summary>
        /// 当前使用的OFFICE号，如WUH128
        /// </summary>
        public static string officeNumberCurrent = "";
        /// <summary>
        /// 表示当前正在切换配置，在协议中作相应的处理
        /// </summary>
        public static bool b_switchingconfig = false;

        //用于扣款的全局变量

        public static bool b_etdz = false;
        public static bool b_etdz_A = false;
        public static bool b_enoughMoney = false;
        public static bool b_endbook = false;//封口
        public static string f_CurMoney = "0.00";
        public static decimal f_Balance;
        public static float f_limitMoneyPerTicket = 500F;
        public static float f_fc = 0F;//要扣款的金额数，即票价
        public static bool b_rt = false;//新版扣款用全局变量标识发了RT正在取是否出票了及乘客人数
        public static bool b_getfc = false;//新版扣款用全局变量标识正在进行取成人价格Eric
        public static bool b_getchildfc = false;//新版扣款用全局变量标识正在进行取儿童价格Eric
        public static bool b_netdz = false;//新版扣款用全局变量标识已扣款马上进行出票Eric
        public static bool b_NotDoDec = false;//标识先扣款失败
        //用于退票指令
        public static bool b_cmd_trfd_AM = false;
        public static bool b_cmd_trfd_M = false;
        public static bool b_cmd_trfd_L = false;
        //其它控制变量
        public static bool b_pat = false;//判断是否刚刚使用了pat:指令
        public static string s_configfile = Application.StartupPath + "\\config.xml";//配置文件所在路径
        //public static string s_ConnectCFG = Application.StartupPath + "\\ConnectCFG.xml";//配置文件2

        public static bool b_pnCommand = false;
        public static bool b_rtCommand = false;
        public static bool b_querryCommand = false;
        public static bool b_otherCommand = false;
        public enum CommandSendType { A, B, Fast };//A默认国内方式(普通),B国际方式(复杂),Fast快速模式
        static public CommandSendType commandSendtype = CommandSendType.Fast;//默认为快速方式
        static public bool bSendTypeFastFunction = false;
        //当前操作的PNR
        public static string s_current_pnr = "";
        //SD转换SS
        public static bool b_sd2ss = true;

        public static MenuStrip mainMenu = null;

        public static string HYXTESTPRINT = "测试";
        

        public static string CommenCmdTemp;
        public static string CommenCmdCurrent
        {
            set
            {
                if (stdRichTB != null)
                {
                    stdRichTB.AppendText(CommenCmdTemp);                    
                }
            }
        }

        public static int intPrintErrorType = 0;//打印行程单的错误类型0无错，1是返回格式错误数据，2是用户使用错误
        //一键定票
        public static string strPatItem = "";
        /// <summary>
        /// 一键出票窗口
        /// </summary>
        public static bool b_GetPatItem = false;
        //计数器
        public static int stdCount = 0;
        //编译日期
        public static string debugDate = "20070328";
        //可用IP及其ID表
        public static List<string> ipListId = new List<string>();
        //当前使用的IP
        //过票时不显示MessageBox(提交PNR信息)
        public static bool b_SubmitPNR = false;
        //保单打印字体大小
        public static float fontsize = 11.2F;
        //是否小回车(数字回车)
        public static bool b_NumpadEnter = false;
        //当前黑屏的内容及历史指令表，翻指令时的当前次数
        public static RichTextBox stdRichTB;
        public static List<string> qHistoryCmd = new List<string>();
        public static int iHistory = 0;
        public static string CurrentAddHistory = "";
        //当前向服务器发送的指令组
        public static string CurrentSendCommands = "";
        public static string CurrentSendCommands0007 = "";
        public static int resendCount0003 = 0;
        public static int resendCount0007 = 0;
        //自动重新连接次数
        public static int AutoConnectCount = 0;
        //财务监听
        public static Model.CommServer commServer = new ePlus.Model.CommServer();
        public static string cwRecvString = "";
        public static bool bListenCw = false;
        //当前发送指令种类
        public enum FormSendCommandType { detrF,none };
        static public FormSendCommandType formSendCmdType = FormSendCommandType.none;
        public static int iLastCommand3or7 = 3;
        //批量打印或批量作废的列表
        static public List<string> lsEticketNumber = new List<string>();
        static public List<string> lsReceiptNumber = new List<string>();
        static public List<string> lsRctName = new List<string>();
        static public List<string> lsRctIdCard = new List<string>();
        static public List<string> ls3IN1Command = new List<string>();
        static public bool bBatting = false;//是否正在进行批处理

        
        static public bool b_replaceSocket = false;
        static public bool b_IsKick = true;

        //新电子客票之信息
        static public eTicket.NewEticket newEticket = new ePlus.eTicket.NewEticket();

        //是否打开ERP-机票录入单插件
        static public bool b_erp_机票录入单 = true;
        static public bool b_提示新订单 = false;

        static public bool b_EtermType = false;

        //用于指令重发，即CurrentSendCommands重定义
        //static public string resendcommand = "";

        //是否弹出散客拼团
        static public bool IsListGroupTicket = true;

        //用于指定0x0003中，是否使用指定配置
        static public bool b_UseSpecifiedConfig = true;
        static public bool b_UseSpecified强制 = false;
        //用于简版是否显示无座舱位
        static public bool b_ListNoSeatBunk = false;
        //用于保存RT指令后返回的结果
        static public string strRtPnrResult = "";
        /// <summary>
        /// 表示独占配置
        /// </summary>
        static public bool bUsingConfigLonely = false;
        static public DateTime dtLonelyStart = DateTime.Now;

        //指令的枚举类型
        public enum CommandName {NONE, CP};
        static public CommandName commandName = CommandName.NONE;

        //本地IP地址及端口号
        static public string ipport = "";

        //监听得到的数据
        static public tcpListen.mylisten mylis = new ePlus.tcpListen.mylisten();
        /*
            GlobalVar.mylis.ipSource = Login_Classes.dns2ip_static(GlobalVar.loginLC.SrvIP);
            GlobalVar.mylis.portSource = GlobalVar.loginLC.SrvPort;
            GlobalVar.mylis.ipDest = GlobalVar.ipport.Split(':')[0];
            GlobalVar.mylis.portDest = int.Parse(GlobalVar.ipport.Split(':')[1]);
            GlobalVar.mylis.StartWork();
        */
        static public string str_Listener = "";
        static public string str_Listener_eagle = "";
        static public int countListener = 0;
        static public string str_Listener_Set
        {//假设str_Listener_eagle先得到，在str_Listener得到后，睡上1000ms，则可肯定str_Listener_eagle已得到
            set
            {
                str_Listener = value;
                if (str_Listener_eagle != "" && value != "")
                {
                    string s1 = mystring.trim(value);
                    string s2 = mystring.trim(str_Listener_eagle);
                    if (s1.Contains(s2) || s2.Contains(s1))
                    {
                        if(EagleAPI.test())
                            MessageBox.Show("Y");
                        //return "y";//正确
                    }
                    else
                    {
                        if (EagleAPI.test())
                        MessageBox.Show("E");
                        //return "e";//错误
                    }
                    str_Listener_eagle = "";
                    countListener = 0;
                }
                else if (str_Listener_eagle == "")
                {
                    if (EagleAPI.test())
                        MessageBox.Show("R");
                    if (GlobalVar.CurrentSendCommands != "" && countListener < 1)
                    {
                        EagleAPI.EagleSendOneCmd(GlobalVar.CurrentSendCommands, GlobalVar.iLastCommand3or7);//重发
                        countListener++;
                    }
                    //return "r";
                }

            }
            get
            {
                return GlobalVar.str_Listener;
            }
        }

        static public string gbZzInternetIP = "125.46.19.155";//郑州公网地址
        static public string gbZzVpnIP = "10.2.1.30";//郑州内网地址


        static public bool gbIsNkgFunctions = false;
    }
    public enum ReceiptType { Default, Business }; 
   
}
