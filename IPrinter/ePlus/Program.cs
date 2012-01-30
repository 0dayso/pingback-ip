#define receipt_
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
using System.Threading;
using gs.para;
using System.Xml;
using XMLConfig;

namespace ePlus
{
    static class Program
    {
        static GlobalVar.ServerAddr GetServerAddr(string root)
        {
            switch (root)
            {
                case "0":
                    return GlobalVar.ServerAddr.Eagle;
                case "1":
                    return GlobalVar.ServerAddr.ZhenZhouJiChang;
                case "2":
                    GlobalVar.gbIsNkgFunctions = true;
                    return GlobalVar.ServerAddr.Eagle;
                case "3":
                    return GlobalVar.ServerAddr.KunMing;
                default:
                    return GlobalVar.ServerAddr.NA;
            }
        }
        static bool GetPrintVersion(string print)
        {
            switch (print)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    return false;
            }
        }

        static Mutex mutex = new Mutex(false, "f880a127-9d21-4907-aa83-6041dc0faa4a");

        /// <summary>
        /// 更新 AutoUpdate.exe 文件
        /// </summary>
        static void AutoUpdate()
        {
            string oriFile = "AutoUpdate.exe";
            string newFile = "AutoUpdate.new.exe";

            if (File.Exists(newFile))
            {
                string tempFile = "AutoUpdate.exe" + DateTime.Now.Ticks.ToString();

                try
                {
                    File.Move(oriFile, tempFile);//备份
                }
                catch
                {
                    return;
                }

                try
                { 
                    File.Move(newFile, oriFile);
                }
                catch 
                {
                    File.Move(tempFile, oriFile);//还原
                    return;
                }

                try { File.Delete(tempFile); }
                catch { }
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(5000, false)) //等待5秒, 如果有相同实例运行则给用户提示
            {
                return;
            }
            try
            {
                AutoUpdate();
                try
                {
                    Properties.Settings.Default.Reload();
                }
                catch { }
                DateTime ProgressStartTime = DateTime.Now;//用于杀死当前进程的判断　
                //EagleString.EagleFileIO.CreateShortCut20();
                //EagleString.EagleFileIO.BackupOptionsXml();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                GlobalVar.printProgram = false;//版本控制(true:打印与false:订票版本)

                GlobalVar.serverAddr = GlobalVar.ServerAddr.NA;
                BookTicket.bIbe = false;

                //try
                //{
                //    if (EagleAPI2.ValidateFiles())//修复丢失文件
                //        return;
                //}
                //catch (Exception ex)
                //{
                //    EagleString.EagleFileIO.LogWrite("ValidateFiles : " + ex.Message);
                //}

                XMLConfig.XMLSettingsGlobal xmlConfig = new XMLSettingsGlobal().Read("XMLConfigGlobal.xml") as XMLSettingsGlobal;
                XMLConfigUser user = new XMLConfigUser().Read() as XMLConfigUser;
                GlobalVar.WebServer = xmlConfig.LoginWebService;
                GlobalVar.WebUrl = xmlConfig.Website;
                string[] credential = xmlConfig.LoginCredential.Split(',');
                if (credential.Length > 1)
                {
                    GlobalVar.loginName = credential[0];
                    GlobalVar.loginPassword = credential[1];
                    Options.GlobalVar.B2bLoginName = credential[0];
                    Options.GlobalVar.B2bLoginPassword = credential[1];
                }
                //EagleString.ServerCenterB2B sc = new EagleString.ServerCenterB2B();
                ////sc.ServerAddressB2B((EagleString.ServerAddr)byte.Parse(root),
                ////                    EagleString.LineProvider.DianXin,
                ////                    0,
                ////                    ref GlobalVar.WebServer,
                ////                    ref GlobalVar.WebUrl);

                Options.GlobalVar.IALoginUrl = xmlConfig.Website;
                Options.GlobalVar.IALoginUrl1 = xmlConfig.Website1;
                Options.GlobalVar.IALoginUrl2 = xmlConfig.Website2;
                Options.GlobalVar.B2bWebServiceURL = xmlConfig.LoginWebService;
                Options.GlobalVar.B2bWebServiceURL1 = xmlConfig.LoginWebService1;
                Options.GlobalVar.B2bWebServiceURL2 = xmlConfig.LoginWebService2;
                Options.GlobalVar.RemotingUrl = xmlConfig.RemotingUrl;
                Options.GlobalVar.IAWebServiceURL = xmlConfig.IAWebServiceURL;
                Options.GlobalVar.IAWebServiceURL1 = xmlConfig.IAWebServiceURL1;
                Options.GlobalVar.IAWebServiceURL2 = xmlConfig.IAWebServiceURL2;
                Options.GlobalVar.QueryType = xmlConfig.QueryType;

                //兼容性代码，用于参数复制，记得删除
                if (user.IACode == "1")
                {
                    user.IACode = xmlConfig.IACode;
                    user.SelectedISP = xmlConfig.SelectedISP;
                    user.Save();
                }

                Options.GlobalVar.IACode = user.IACode;
                Options.GlobalVar.SelectedISP = user.SelectedISP;

                LogonForm logon = new LogonForm();
                if (logon.ShowDialog() == DialogResult.OK)
                {
                    SplashScreen.Splasher.Status = "正在启动…";
                    try { SplashScreen.Splasher.Banner = Image.FromFile("login.jpg"); }
                    catch { }
                    SplashScreen.Splasher.Show(typeof(SplashScreen.frmSplash));

                    frmMain mainForm = new frmMain();
                    Application.Run(mainForm);
                    GlobalVar.commServer.Close();
                    EagleAPI2.DeleteSubmittedPnr();
                }
                else
                {
                    try
                    {
                        GlobalAPI.Hosts.setDefault();
                    }
                    catch { }
                    return;
                }


                GlobalVar.mylis.AbortWork();
                LogoutForm lf = new LogoutForm();


                lf.ShowDialog();
                try
                {
                    GlobalAPI.Hosts.setDefault();
                }

                catch { }
                EagleString.BaseFunc.KillProcess("iprinter", ProgressStartTime);
                if (GlobalVar.gbIsRestartEagle)
                {
                    PrintTicket.RunProgram(Application.StartupPath + "\\iprinter.exe", "");
                }
            }
            finally
            { 
                mutex.ReleaseMutex(); 
            }
        }
    }
}