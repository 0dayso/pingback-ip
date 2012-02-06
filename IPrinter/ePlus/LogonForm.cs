#define receipt_
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Xml;
using System.IO;
using System.Threading;
using XMLConfig;
using gs.para;

namespace ePlus
{
    public partial class LogonForm : Form
    {
        public LogonForm()
        {

            InitializeComponent();
#if RWY
            skinEngine1.SkinFile = Application.StartupPath + "\\Macos.ssk";
#else
            skinEngine1.SkinFile = Application.StartupPath + "\\EagleSkin.ssk";
#endif
            //pictureBox1.Visible = (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle);
            //pictureBox2.Visible = (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang);
            picLogin.Visible = true;
            LogoPicture.pictures pic = new LogoPicture.pictures();
            if (GlobalVar.serverAddr == GlobalVar.ServerAddr.Eagle)
            {
              picLogin.Image = pic.pictureBox2.Image;
               
#if receipt
                pictureBox1.Image = pic.pictureBox7.Image;
                this.ShowIcon = false;
#endif
            }

            else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.HangYiWang)
                picLogin.Image = pic.pictureBox2.Image;// pictureBox1.Image = pic.pictureBox1.Image;
            else if (GlobalVar.serverAddr == GlobalVar.ServerAddr.ZhenZhouJiChang)
            {
                //pictureBox1.Image = pic.pictureBox9.Image;
                
                try
                {
                    Bitmap bi = new Bitmap(Application.StartupPath + "\\resources\\Application.gif");
                    picLogin.Image = (Image)bi;
                }
                catch (Exception eee)
                {
                    //MessageBox.Show("请将Logo文件Application.gif 放置到系统安装目录下的Resources目录里面！");
                }
            }
            if (GlobalVar2.bTempus)
            {
                picLogin .Image = Image.FromFile (Application.StartupPath + "\\Resources\\eagle1.gif");
            }

            try
            {
                picLogin.Image = Image.FromFile("login.jpg");
            }
            catch { }

            try { this.Icon = new Icon("ico.ico"); }
            catch { }

            XMLConfig.ISP isp = Options.GlobalVar.SelectedISP;
            if (isp == XMLConfig.ISP.ChinaTelecom)
                rbDianx.Checked = true;
            else if (isp == XMLConfig.ISP.ChinaUnicom)
                rbWangt.Checked = true;
        }

        public ServerInfo server = null;
        public static bool isZzInternet = false;

        private void LogonForm_Load(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings settings = new ePlus.Properties.Settings();
                txtPassword.Text = settings.Password.Trim();
                cbLoginName.Text = settings.LastLogin.Trim();
                cbSavePassword.Checked = cbLoginName.Text != "" && txtPassword.Text != "";

                init_cb();
            }
            catch
            {
            }
        }
        
        string errorMsg;

        /// <summary>
        /// 保险账户信息验证
        /// </summary>
        private void LoginIA()
        {
            this.errorMsg = string.Empty;
            EagleWebService.UserLoginResponse ret = new EagleWebService.UserLoginResponse();
            EagleWebService.wsInsurrance wsi = null;

            try
            {
                wsi = new EagleWebService.wsInsurrance();
                //解决部分用户登录异常:"The underlying connection was closed: The connection was closed unexpectedly."
                //取消以下语句,直接修改服务器的web.config
                //System.Net.ServicePointManager.Expect100Continue = false;
                ret = wsi.Login(Options.GlobalVar.IAUsername, Options.GlobalVar.IAPassword);

                if (!string.IsNullOrEmpty(ret.Trace.ErrorMsg))
                    this.errorMsg = ret.Trace.ErrorMsg;
                else
                {
                    Options.GlobalVar.IAOffsetX = ret.OffsetX;
                    Options.GlobalVar.IAOffsetY = ret.OffsetY;
                }
            }
            //catch (System.Threading.ThreadAbortException)
            //{ }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                EagleString.EagleFileIO.LogWrite(e.ToString());
            }
        }

        Thread threadLogin;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "登录")
            {
                btnLogin.Text = "取消";
                this.progressBar1.Visible = true;
                this.cbLoginName.Enabled = false;
                this.txtPassword.Enabled = false;
                CheckForIllegalCrossThreadCalls = false;

                threadLogin = new System.Threading.Thread(new System.Threading.ThreadStart(LoginStep1));
                threadLogin.Start();
            }
            else
            {
                threadLogin.Abort();
                btnLogin.Text = "登录";
            }
        }

        private void LoginStep1()
        {
            try
            {
                Options.GlobalVar.IAUsername = this.cbLoginName.Text.Trim();
                Options.GlobalVar.IAPassword = this.txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(Options.GlobalVar.IAUsername) || string.IsNullOrEmpty(Options.GlobalVar.IAPassword))
                {
                    throw new Exception("用户名或者密码不能为空！");
                }

                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(LoginIA));
                th.Start();
                if (!th.Join(System.Threading.Timeout.Infinite))//此处被Abort
                {
                    th.Abort();
                    throw new TimeoutException("连接超时，请稍后再试！");
                }
                else if (!string.IsNullOrEmpty(this.errorMsg))
                {
                    throw new Exception(this.errorMsg);
                }

                if (Options.GlobalVar.QueryType == XMLConfig.QueryType.Eterm)
                {
                    th = new System.Threading.Thread(new System.Threading.ThreadStart(LoginStep2));
                    th.Start();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (ThreadAbortException)
            {
                Stop();
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show("登录失败：" + ex.Message);
                    }));
                Stop();
            }
        }

        void LoginStep2()
        {
            try
            {
                if (string.IsNullOrEmpty(GlobalVar.loginName) || string.IsNullOrEmpty(GlobalVar.loginPassword))
                {
                    Stop();
                    MessageBox.Show("请输入B2B帐号和密码！");
                    return;
                }

                Login lg = new Login();
                //lg.login();
                string strRet;
                try
                {
                    strRet = lg.login();
                }
                catch { strRet = string.Empty; }

                if (string.IsNullOrEmpty(strRet))
                {
                    //不再尝试登陆，直接进入主界面！
                    MessageBox.Show("暂不能提取PNR等编码，请手工输入乘客信息！");
                    this.DialogResult = DialogResult.OK;
                    GlobalVar.loginSuccess = true;
                    return;
                }

                GlobalVar.loginXml = strRet;
                Options.GlobalVar.B2bLoginXml = strRet;
                Login_Classes lc = new Login_Classes(strRet);
                EagleString.LoginResult loginResult = new EagleString.LoginResult(strRet);
                bool isLogin = loginResult.SUCCEED;

                if (isLogin)//登陆成功
                {
                    if (lc.IPsString.Trim().Length < 7)
                    {
                        Stop();
                        MessageBox.Show("无可用的配置服务器，请与管理员联系！");
                        return;
                    }
                    if (lc.PassPort.Length != 32) throw new Exception("未获取到PASSPORT,请重试！");

                    GlobalVar.loginLC = lc;
                    GlobalVar.loginSuccess = true;
                    //this.Close();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("登录B2B系统失败，详情：" + strRet);
                    Stop();
                    return;
                }
            }
            catch (Exception e)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show("登录B2B系统出错：" + e.Message);
                        Stop();
                    }));
            }
            return;
        }

        private void LogonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;

            picLogin.Dispose();//释放该图片资源，否则导致后台的自动更新无法操作该文件,下面2个语句缺一不可
            picLogin = null;

            try
            {
                Properties.Settings.Default.LastLogin = Options.GlobalVar.IAUsername;//GlobalVar.loginName;
                if (cbSavePassword.Checked)
                {
                    Properties.Settings.Default.Password = Options.GlobalVar.IAPassword;// this.txtPassword.Text;

                }
                else
                {
                    Properties.Settings.Default.Password = "";

                }

                Properties.Settings.Default.UserInfo = save_userinfo();
                Properties.Settings.Default.Save();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message + "LogonForm_FormClosed");
            }

        }
        Hashtable ht = new Hashtable();
        void init_cb()
        {
            try
            {
                Properties.Settings settings = new ePlus.Properties.Settings();
                string[] users = settings.UserInfo.Split('|');

                for (int i = 0; i < users.Length; i++)
                {
                    if (users[i] == "") continue;
                    if (users[i].Split(',').Length > 1)
                        ht.Add(users[i].Split(',')[0], users[i].Split(',')[1]);
                    else ht.Add(users[i].Split(',')[0], "");
                }
                cbLoginName.Items.Clear();
                for (int i = 0; i < users.Length; i++)
                {
                    cbLoginName.Items.Add(users[i].Split(',')[0].Trim());
                }
            }
            catch//过渡期代码
            {
                try
                {
                    cbLoginName.Items.Clear();
                    Properties.Settings settings = new ePlus.Properties.Settings();
                    string[] users = settings.UserInfo.Split('\n');

                    for (int i = 0; i < users.Length; i++)
                    {
                        if (users[i] == "") continue;
                        if (users[i].Split('\r').Length > 1)
                            ht.Add(users[i].Split('\r')[0], users[i].Split('\r')[1]);
                        else ht.Add(users[i].Split('\r')[0], "");
                    }
                    cbLoginName.Items.Clear();
                    for (int i = 0; i < users.Length; i++)
                    {
                        cbLoginName.Items.Add(users[i].Split('\r')[0].Trim());
                    }
                }
                catch
                { }
            }
        }
        string save_userinfo()
        {
            string name = cbLoginName.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (ht.Contains(name))
                ht[name] = (cbSavePassword.Checked ? pass : "");
            else 
                ht.Add(name, cbSavePassword.Checked ? pass : "");
            string[] keys = new string[ht.Count];
            ht.Keys.CopyTo(keys, 0);
            string[] values = new string[ht.Count];
            ht.Values.CopyTo(values, 0);
            string ret = "";
            for (int i = 0; i < ht.Count; i++)
            {
                ret = ret + keys[i].Trim() + "," + values[i].Trim() + "|";
            }
            ret = ret.TrimEnd('|');
            return ret;
        }
        private void cbLoginName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPassword.Text = ht[cbLoginName.Text].ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value += 1;
            if (this.progressBar1.Value == this.progressBar1.Maximum)
                this.progressBar1.Value = 0;
        }

        private void Del_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbLoginName.Items.Count <= 0) return;
                ht.Remove(cbLoginName.Text);
                cbLoginName.Items.Remove(cbLoginName.Text);
                cbLoginName.Text = "";
            }
            catch { };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbUserBtoC_CheckedChanged(object sender, EventArgs e)
        {
            //this.txtCTIPhoneNumber.ReadOnly = !rbUserBtoC.Checked;
        }

        void Stop()
        {
            this.Invoke(new MethodInvoker(delegate()
                {
                    this.progressBar1.Visible = false;
                    this.cbLoginName.Enabled = true;
                    this.txtPassword.Enabled = true;
                    btnLogin.Text = "登录";
                    TipRoute();
                }));
        }

        private void rbDianx_CheckedChanged(object sender, EventArgs e)
        {
            XMLConfigUser user = new XMLConfigUser().Read() as XMLConfigUser;
            if (rbDianx.Checked)
                user.SelectedISP = XMLConfig.ISP.ChinaTelecom;
            else if (rbWangt.Checked)
                user.SelectedISP = XMLConfig.ISP.ChinaUnicom;

            user.Save();
            Options.GlobalVar.SelectedISP = user.SelectedISP;
        }

        private void rbWangt_CheckedChanged(object sender, EventArgs e)
        {
            rbDianx_CheckedChanged(sender, e);
        }

        private void LogonForm_Shown(object sender, EventArgs e)
        {
            TipRoute();
        }

        void TipRoute()
        {
            //bug？：要执行2次，否则toolTip的箭头位置不对！
            toolTip1.Show("换一条线路，也许可以更快登录。", lblRoute);
            toolTip1.Show("换一条线路，也许可以更快登录。", lblRoute);
        }

        private void LogonForm_Move(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
        }
    }
}