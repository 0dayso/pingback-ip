using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EagleString;
using System.Collections;
using System.Threading;
namespace EagleForms.General
{
    /// <summary>
    /// this class should output LoginInfomations: b2b user info, b2c user info, b2b login info, b2c login info
    /// </summary>
    public partial class Login : Form
    {
        struct LOGIN_ITEMS
        {
            /// <summary>
            /// main item: �û���
            /// </summary>
            public string m_username;
            /// <summary>
            /// main item: ����
            /// </summary>
            public string m_password;
            /// <summary>
            /// main item: �Ƿ��ס����
            /// </summary>
            public bool m_remember;
            /// <summary>
            /// �Ƿ��Զ���¼,����ѡ�����������ӣ�ȡ���Զ���¼
            /// </summary>
            public bool m_autologin;
            /// <summary>
            /// network: provider
            /// </summary>
            public LineProvider m_provider;
            /// <summary>
            /// network: login server
            /// </summary>
            public string m_server_login;
            /// <summary>
            /// network: update address like ""
            /// </summary>
            public string m_server_update;
            /// <summary>
            /// btoc username
            /// </summary>
            public bool m_btocuser;
            /// <summary>
            /// btoc �ֻ���
            /// </summary>
            public string m_subnumber;
            /// <summary>
            /// btoc web�����ip���˿ڣ���(��Ҫhttp:// )hbpiao.3322.org:6000/AirLineTicket/
            /// </summary>
            public string m_b2cipservice;
            /// <summary>
            /// btoc web��̨�����ip���˿�(Ҫhttp://hbpiao.3322.org:3128/AirLineTicket/)
            /// </summary>
            public string m_b2cipsite;
            /// <summary>
            /// �������ļ��ж�ȡ�ϴε�¼��Ϣ
            /// </summary>
            public void FromFile(string file)
            {
                string pre = "LastLoginItem";
                Hashtable ht = EagleFileIO.ReadEagleDotTxtFileToHashTable();
                m_username = EagleString.egString.Null2S(ht[pre + "UserName"]);
                m_password = EagleString.egString.Null2S(ht[pre + "PassWord"]);
                m_password = EagleString.BaseFunc.Crypt.CryptString.DeCode(m_password);
                m_remember = (EagleString.egString.Null2S(ht[pre + "Remember"]) == "1");
                m_autologin = (EagleString.egString.Null2S(ht[pre + "AutoLogin"]) == "1");
                try
                {
                    m_provider = (LineProvider)byte.Parse(EagleString.egString.Null2S(ht[pre + "Provider"]));
                }
                catch
                {
                    m_provider = LineProvider.DianXin;
                }
                m_server_login = EagleString.egString.Null2S(ht[pre + "ServerLogin"]);
                m_server_update = EagleString.egString.Null2S(ht[pre + "ServerUpdate"]);
                m_btocuser = (EagleString.egString.Null2S(ht[pre + "BtocUser"]) == "1");
                m_subnumber = EagleString.egString.Null2S(ht[pre + "SubNumber"]);
                //m_b2cipservice = EagleString.egString.Null2S(ht[pre + "b2cIPservice"]);
                m_b2cipservice = EagleString.egString.Null2S(ht[pre + "b2cIPservice"]);
                if (m_b2cipservice.ToUpper().StartsWith("HTTP://")) m_b2cipservice = m_b2cipservice.Substring(7);
                m_b2cipsite = EagleString.egString.Null2S(ht[pre + "b2cIPsite"]);
            }
            /// <summary>
            /// ����ǰ��¼��Ϣд�������ļ�
            /// </summary>
            private void ToFile(string file)
            {
                Hashtable ht = new Hashtable();
                string pre = "LastLoginItem";
                ht.Add(pre + "UserName", m_username);
                ht.Add(pre + "PassWord", EagleString.BaseFunc.Crypt.CryptString.EnCode(m_password));
                ht.Add(pre + "Remember", m_remember ? "1" : "0");
                ht.Add(pre + "AutoLogin", m_autologin ? "1" : "0");
                ht.Add(pre + "Provider", Convert.ToString((byte)m_provider));
                ht.Add(pre + "ServerLogin", m_server_login);
                ht.Add(pre + "ServerUpdate", m_server_update);
                ht.Add(pre + "BtocUser", m_btocuser ? "1" : "0");
                ht.Add(pre + "SubNumber", m_subnumber);
                ht.Add(pre + "b2cIPservice", m_b2cipservice);
                ht.Add(pre + "b2cIPsite", m_b2cipsite);
                EagleFileIO.WiteHashTableToEagleDotTxt(ht);
            }
            public void ToFile(bool SavePassword)
            {
                ToFile("");
                if (!SavePassword) 
                {
                    Hashtable ht = new Hashtable();
                    string pre = "LastLoginItem";
                    ht.Add(pre + "PassWord", EagleString.BaseFunc.Crypt.CryptString.EnCode(""));
                    EagleFileIO.WiteHashTableToEagleDotTxt(ht);
                }
            }
        }
        public Login()
        {
            InitializeComponent();
            loadUserInfo();
            loadingCircle1.Visible = false;
            bup = bleft = bright = bdown = false;
            DisPanels();
            li.FromFile("");//LastLoginItemb2cIPservice=hbpiao.3322.org:6000/AirLineTicket/
            Login_Item_To_Controls();
            if (li.m_autologin) ;//�Զ���¼
        }
        LOGIN_ITEMS li;
        const string DIAN_XING = "����";
        const string WANG_TONG = "��ͨ";
        const string VPN = "VPN";
        /// <summary>
        /// ���ļ���ȡ�ϴε�¼��Ϣ���ö�Ӧ�ؼ�ֵ
        /// </summary>
        private void Login_Item_To_Controls()
        {
            //main panel
            cbLoginName.Text = li.m_username;
            txtPassword.Text = li.m_password;
            cbAutoLogin.Checked = li.m_autologin;
            cbSavePassword.Checked = li.m_remember;
            //up panel
            switch (li.m_provider)
            {
                case LineProvider.DianXin:
                    cbProvider.Text = DIAN_XING;
                    break;
                case LineProvider.WangTong:
                    cbProvider.Text = WANG_TONG;
                    break;
                case LineProvider.VPN:
                    cbProvider.Text = VPN;
                    break;
            }
            switch (li.m_server_login)
            {
                default:
                break;
            }
            switch (li.m_server_update)
            {
            }
            //down panel
            cbBtocUser.Checked = li.m_btocuser;
            txtCTIPhoneNumber.ReadOnly = !li.m_btocuser;
            txtCTIPhoneNumber.Text = li.m_subnumber;
            txtB2cService.Text = li.m_b2cipservice;
            txtB2cManager.Text = li.m_b2cipsite;
        }
        /// <summary>
        /// save controls' value to the li,
        /// </summary>
        private void Login_Item_From_Controls()
        {
            li.m_username = cbLoginName.Text;
            li.m_password = txtPassword.Text;
            li.m_autologin = cbAutoLogin.Checked;
            li.m_remember = cbSavePassword.Checked;
            li.m_btocuser = cbBtocUser.Checked;
            li.m_subnumber = txtCTIPhoneNumber.Text;
            li.m_b2cipservice = txtB2cService.Text;
            li.m_b2cipsite = txtB2cManager.Text;
            switch (cbProvider.Text)
            {
                case DIAN_XING:
                    li.m_provider = LineProvider.DianXin;
                    break;
                case WANG_TONG:
                    li.m_provider = LineProvider.WangTong;
                    break;
                case VPN:
                    li.m_provider = LineProvider.VPN;
                    break;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private bool bup, bleft, bright, bdown;
        private void DisPanels()
        {
            pUp.Visible = bup;
            pLeft.Visible = bleft;
            pRight.Visible = bright;
            pDown.Visible = bdown;
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string s = b.Name.ToLower();
            if (s.Contains("left")) bleft = !bleft;
            else if (s.Contains("right")) bright = !bright;
            else if (s.Contains("up")) bup = !bup;
            else if (s.Contains("down")) bdown = !bdown;
            DisPanels();
        }
        private void cbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private EagleString.LoginResult lr;
        EagleString.LoginResultBtoc.LOGIN_INFO_BTOC lib2c;
        int serverIndex = 0;
        void login2serverThread()
        {
            (new Thread(new ThreadStart(login2server))).Start();
        }

        /// <summary>
        /// ��ؼ��޹صĵ�¼����,��Ҫ�õ�LoginInfoʵ��,�ɷŵ�ȫ�ֺ�����EagleExtension.Api
        /// </summary>
        private void login2server(bool isB2c, string b2cService, string b2cManager, string username, string password, LineProvider lp,EagleString.LoginResultBtoc.LOGIN_INFO_BTOC resB2c,EagleString.LoginInfo loginInfo)
        {
            loginInfo.borc = (isB2c ? LoginInfo.BORC.b2c : LoginInfo.BORC.b2b);
            if (isB2c)
            {
                EagleExtension.EagleExtension.LoginB2c(username, password, ref resB2c, b2cManager, b2cService);
                loginInfo.b2c.username = username;
                loginInfo.b2c.password = password;
                loginInfo.b2c.webservice = b2cService;
                loginInfo.b2c.website = b2cManager;
                loginInfo.b2c.lr = resB2c;
            }
            string loginb2bxml = "";
            //here todo: get b2b webservice address
            EagleString.ServerCenterB2B scb2b = new ServerCenterB2B();
            string b2bWebSite = "";
            string b2bWebService = "";
            scb2b.ServerAddressB2B(EagleFileIO.Server, lp, 0, ref b2bWebService, ref b2bWebSite);
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(b2bWebService);
            bool flagLogin = false;
            string b2buser = username;
            string b2bpass = password;
            if (isB2c)
            {
                b2buser = resB2c.mapuser;
                b2bpass = resB2c.mappass;
            }
            kf.LogIn(b2buser, b2bpass, ref loginb2bxml, ref flagLogin);
            if (!flagLogin) throw new Exception("��¼ʧ�ܣ��������缰�û��������Ƿ���ȷ��");
            lr = new LoginResult(loginb2bxml);
            if (lr.EXPIRED) throw new Exception("��B2B�û���ʧЧ��");
            if (lr.AGENT_STAT != 0) throw new Exception("��B2B�û����ڴ�������ʧЧ��");
            if (lr.USER_STAT != 0) throw new Exception("��B2B�û�״̬��ʧЧ��");
            loginInfo.b2b.username = b2buser;
            loginInfo.b2b.password = b2bpass;
            loginInfo.b2b.webservice = b2bWebService;
            loginInfo.b2b.webside = b2bWebSite;
            loginInfo.b2b.lr = lr;
        }
        /// <summary>
        /// ���ӿؼ��Ļ����Ƿ�b2c,b2c��ַ,b2c��̨,�û���,����,������·
        /// </summary>
        private void login2server()
        {
            try
            {
                if (cbLoginName.InvokeRequired)
                {
                    deleg4login dele = Login_Item_From_Controls;
                    cbLoginName.Invoke(dele);
                }
                else
                {
                    Login_Item_From_Controls();
                }
                li.ToFile(cbSavePassword.Checked);//����ؼ�ֵ
                string b2buser = li.m_username;
                string b2bpass = li.m_password;
                saveUserInfo(b2buser, b2bpass);
                login2server(li.m_btocuser, li.m_b2cipservice, li.m_b2cipsite, li.m_username, li.m_password, li.m_provider, lib2c, LOGININFO);
                this.DialogResult = DialogResult.OK;
                return;
            }
            catch (Exception ex)
            {
                //loadingCircle1.Visible = false;
                MessageBox.Show(ex.Message);
            }
        }
        public void login2server_rwy(string username, string password)
        {
            login2server(false, "", "", username, password, new LineProvider(), lib2c, LOGININFO);
            LOGININFO.b2b.lr.m_ls_command.Add("EG2");
            Primary frmMain;
            frmMain = new Primary();
            frmMain.loginInfo = LOGININFO;
            
            frmMain.Show();

            frmMain.OuterOnlyDisplayReceiptPrint();
        }
        private EagleString.ServerAddr Server
        {
            get
            {
                Hashtable ht = EagleString.EagleFileIO.ReadEagleDotTxtFileToHashTable();
                string code = ht["TheRootAgent"].ToString();
                return (EagleString.ServerAddr)byte.Parse(code);
            }
        }
        private LineProvider Line
        {
            get
            {
                return li.m_provider;
            }
        }
        delegate void deleg4login();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            loadingCircle1.Visible = true;
            loadingCircle1.Active = true;
            Thread thread = new Thread(new ThreadStart(loginclick));
            thread.Start();
        }
        private void loginclick()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    deleg4login dlg = login2serverThread;
                    this.Invoke(dlg);
                }
                else
                {
                    login2serverThread();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public EagleString.LoginInfo LOGININFO = new LoginInfo ();
        private void out_put_login_info_b2b(string username,string password,string webservice,string website,LoginResult loginresult)
        {
            LOGININFO.b2b.username = username;
            LOGININFO.b2b.password = password;
            LOGININFO.b2b.webservice = webservice;
            LOGININFO.b2b.webside = website;
            LOGININFO.b2b.lr = loginresult;
        }
        /// <summary>
        /// ժҪ����LoginInfo�����е�b2c��ֵ
        /// </summary>
        private void out_put_login_info_b2c(string username, string password, string webservice, string website, 
            LoginResultBtoc.LOGIN_INFO_BTOC loginresult)
        {
            LOGININFO.b2c.username = username;
            LOGININFO.b2c.password = password;
            LOGININFO.b2c.webservice = webservice;
            LOGININFO.b2c.website = website;
            LOGININFO.b2c.lr = loginresult;
        }

        private void cbBtocUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtCTIPhoneNumber.ReadOnly = !cbBtocUser.Checked;
                txtB2cService.ReadOnly = !cbBtocUser.Checked;
                txtB2cManager.ReadOnly = !cbBtocUser.Checked;
            }
            catch
            {
            }
        }
        Properties.Settings setting = new global::EagleForms.Properties.Settings();
        Hashtable htUserInfo = new Hashtable();
        /// <summary>
        /// ȡ����¼�û���������
        /// </summary>
        private void loadUserInfo()
        {
            try
            {
                cbLoginName.Sorted = true;
                string[] a = setting.UserInfo.Split('\n');
                for (int i = 0; i < a.Length; i++)
                {
                    string user = a[i].Split('\r')[0];
                    string pass = a[i].Split('\r')[1];
                    cbLoginName.Items.Add(user);
                    htUserInfo.Add(user, pass);
                }
            }
            catch
            {
            }
        }
        private void saveUserInfo(string user, string pass)
        {
            string pwd = "";
            if (cbSavePassword.Checked)
            {
                pwd = EagleString.BaseFunc.Crypt.CryptString.EnCode(pass);
            }
            else
            {
                pwd = EagleString.BaseFunc.Crypt.CryptString.EnCode("");
            }
            if (htUserInfo.ContainsKey(user))
            {
                htUserInfo[user] = pwd;
            }
            else
            {
                htUserInfo.Add(user, pwd);
            }
            setting.UserInfo = "";
            foreach(DictionaryEntry de in htUserInfo)
            {
                setting.UserInfo += de.Key.ToString() + "\r" + de.Value.ToString() + "\n";
            }
            setting.UserInfo = egString.trim(setting.UserInfo,"\n");
            setting.Save();
        }
        private void delUserInfo(string key)
        {
            htUserInfo.Remove(key);
            setting.UserInfo = "";
            foreach (DictionaryEntry de in htUserInfo)
            {
                setting.UserInfo += de.Key.ToString() + "\r" + de.Value.ToString() + "\n";
            }
            setting.UserInfo = egString.trim(setting.UserInfo, "\n");
            setting.UserInfo = egString.trim(setting.UserInfo);
            setting.Save();
        }
        private void cbLoginName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Text = EagleString.BaseFunc.Crypt.CryptString.DeCode(htUserInfo[cbLoginName.Text].ToString());
            }
            catch
            {
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            delUserInfo(cbLoginName.Text);
            cbLoginName.Text = "";
            txtPassword.Text = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            setting.UserInfo = "";
            setting.Save();
            cbLoginName.Items.Clear();
            cbLoginName.Text = "";
            txtPassword.Text = "";
        }
    }
}