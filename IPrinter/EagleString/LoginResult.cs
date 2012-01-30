using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;


namespace EagleString
{
    public class LoginResult
    {
        private string m_LoginXml = "";
        public LoginResult(string xml)
        {
            if (xml == "")
            {
                return;
            }
            m_LoginXml = xml;
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(m_LoginXml);
            m_loginCmdFlag = (xd.SelectSingleNode("//eg/cm").InnerText.Trim() == "RetLogin");
            if (!m_loginCmdFlag) return;
            m_loginResFlag = (xd.SelectSingleNode("//eg/LoginFlag").InnerText.Trim() == "LoginSucc");
            if (!m_loginResFlag) return;
            m_Passport = xd.SelectSingleNode("//eg/PassPort").InnerText.Trim();
            m_TrimLen = Convert.ToInt32(xd.SelectSingleNode("//eg/TrimLen").InnerText.Trim());
            m_ServerIp = xd.SelectSingleNode("//eg/SrvIp").InnerText.Trim();
            m_ServerDns = xd.SelectSingleNode("//eg/SrvDNS").InnerText.Trim();
            m_ServerPort = Convert.ToInt32(xd.SelectSingleNode("//eg/SrvPort").InnerText.Trim());
            m_ServerName = xd.SelectSingleNode("//eg/SrvName").InnerText.Trim();
            m_UserStat = Convert.ToInt32(xd.SelectSingleNode("//eg/UserStat").InnerText.Trim());
            m_Insurance_User = xd.SelectSingleNode("//eg/InsuranceUserName").InnerText.Trim();
            m_Insurance_Pass = xd.SelectSingleNode("//eg/InsurancePassWord").InnerText.Trim();
            m_AgentStat = Convert.ToInt32(xd.SelectSingleNode("//eg/AgentStat").InnerText.Trim());
            m_Expired = (xd.SelectSingleNode("//eg/UserExpire").InnerText.Trim() == "true");
            m_Tickets = xd.SelectSingleNode("//eg/Tickets").InnerText.Trim();
            XmlNode xn = xd.SelectSingleNode("//eg/IPS");
            for (int i = 0; i < xn.ChildNodes.Count; ++i)
            {

                XmlNode node = xn.ChildNodes[i];
                OFFICE_INFO oi = new OFFICE_INFO();
                oi.IP_ID = Convert.ToInt32(node.SelectSingleNode("ipid").InnerText.Trim());
                oi.CONFIG_IP = node.SelectSingleNode("ip").InnerText.Trim();
                oi.SERVER_IP = node.SelectSingleNode("SrvIp").InnerText.Trim();
                oi.SERVER_DNS = node.SelectSingleNode("SrvDNS").InnerText.Trim();
                oi.SERVER_PORT = Convert.ToInt32(node.SelectSingleNode("SrvPort").InnerText.Trim());
                oi.SERVER_NAME = node.SelectSingleNode("SrvName").InnerText.Trim();
                oi.OFFICE_ALLY = node.SelectSingleNode("PeiZhi").InnerText.Trim();
                oi.OFFICE_NO = node.SelectSingleNode("vcOffNo").InnerText.Trim();
                m_ls_office.Add(oi);
            }
            xn = xd.SelectSingleNode("//eg/UseCms");
            for (int i = 0; i < xn.ChildNodes.Count; ++i)
            {
                XmlNode node = xn.ChildNodes[i];
                string cmd = node.InnerText.Trim();
                m_ls_command.Add(cmd);
            }
            m_ls_command.Add("av");
        }
        public void AddAuthorityOfFunction(string egcmdcode)
        {
            m_ls_command.Add(egcmdcode);
        }
        /// <summary>
        /// ����ָ��Ȩ��
        /// </summary>
        /// <param name="command">�����ں����е�ָ��</param>
        /// <returns></returns>
        public bool AuthorityOfCommand(string command)
        {
            string c = command.ToLower().Trim();
            for (int i = 0; i < m_ls_command.Count; ++i)
            {
                string a = m_ls_command[i].ToLower().Trim();
                if (c.IndexOf(a) == 0) return true;
            }
            return false;
        }
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="func">Ҫʹ�õĹ��ܴ���</param>
        /// <returns></returns>
        public bool AuthorityOfFunction(string func)
        {
            string c = func.ToLower().Trim();
            for (int i = 0; i < m_ls_command.Count; ++i)
            {
                if (func == m_ls_command[i]) return true;
            }
            return false;
        }

        /// <summary>
        /// �л�������officeno��������Ա����SERVER��Ϣ���л���������ͬofficeno���Է��ڲ�ͬ�������ϣ����Ա��л���׼ȷ
        /// </summary>
        /// <param name="officeally"></param>
        public void SwitchConfig(string officeno)
        {
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                if (officeno == m_ls_office[i].OFFICE_NO)
                {
                    string str1 = m_ServerIp + ":" + m_ServerPort.ToString();
                    string str2 = m_ls_office[i].SERVER_IP + ":" + m_ls_office[i].SERVER_PORT;
                    if (str1 != str2)
                    {
                        this.m_ServerDns = m_ls_office[i].SERVER_DNS;
                        this.m_ServerIp = m_ls_office[i].SERVER_IP;
                        this.m_ServerName = m_ls_office[i].SERVER_NAME;
                        this.m_ServerPort = m_ls_office[i].SERVER_PORT;
                        break;
                    }
                }
            }
        }
        public void SwitchConfig(int ipid)
        {
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                if (ipid == m_ls_office[i].IP_ID)
                {
                    string str1 = m_ServerIp + ":" + m_ServerPort.ToString();
                    string str2 = m_ls_office[i].SERVER_IP + ":" + m_ls_office[i].SERVER_PORT;
                    if (str1 != str2)
                    {
                        this.m_ServerDns = m_ls_office[i].SERVER_DNS;
                        this.m_ServerIp = m_ls_office[i].SERVER_IP;
                        this.m_ServerName = m_ls_office[i].SERVER_NAME;
                        this.m_ServerPort = m_ls_office[i].SERVER_PORT;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// �������л��ɹ�ʱ����
        /// �л���ѡ�������ã�����ǰ���ж�Ŀ¼ipid�Ƿ���ͬһ�������ϣ��˺�������������������������������
        /// </summary>
        public void SwitchConfig(string[] destipid)
        {
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                if (destipid[0] == m_ls_office[i].IP_ID.ToString())
                {
                    SwitchConfig(int.Parse(destipid[0]));
                    break;
                }
            }
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                m_ls_office[i].USINGFLAG = false;
                for (int j = 0; j < destipid.Length; ++j)
                {
                    if (destipid[j] == m_ls_office[i].IP_ID.ToString())
                    {
                        m_ls_office[i].USINGFLAG = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ȡ�������ͬ��office��IPID�б�
        /// </summary>
        /// <param name="office">OFFICE</param>
        /// <returns></returns>
        public List<int> SameConfigs(string office)
        {
            List<int> ls = new List<int>();
            if (office.Length != 6) return null;
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                if (office == m_ls_office[i].OFFICE_NO)
                {
                    ls.Add(m_ls_office[i].IP_ID);
                }
            }
            return ls;
        }
        /// <summary>
        /// ȡ��¼ʱ����λ�����ӷ�������ipid�б�
        /// </summary>
        public List<string> IpidsWhenLogin(ref string txt)
        {
            List<string> ls = new List<string>();
            string[] ts = new string[] { "SIA180-01" };//�����û�����ʱ��Ҫָ��������,����ɴ���Ϊ�ϴ��˳�ʱʹ�õ�����,����������Ĭ������
            txt = "";
            for (int i = 0; i < m_ls_office.Count; i++)
            {
                for (int j = 0; j < ts.Length; ++j)
                {
                    if (m_ls_office[i].OFFICE_ALLY.ToUpper() == ts[j].ToUpper())
                    {
                        if (SERVER_NAME == m_ls_office[i].SERVER_NAME)
                        {
                            ls.Add(m_ls_office[i].IP_ID.ToString());
                            m_ls_office[i].USINGFLAG = (true);
                            txt += m_ls_office[i].OFFICE_NO + ",";
                        }
                    }
                }
            }
            if (ls.Count > 0) return ls;
            for (int i = 0; i < m_ls_office.Count; i++)
            {
                if (SERVER_NAME == m_ls_office[i].SERVER_NAME)
                {
                    ls.Add(m_ls_office[i].IP_ID.ToString());
                    m_ls_office[i].USINGFLAG = (true);
                    txt += m_ls_office[i].OFFICE_NO + ",";
                }
            }
            return ls;
        }
        /// <summary>
        /// ȡips��office�ı�
        /// </summary>
        public string IpidsText(string[] ipids)
        {
            string ret = "";
            for (int i = 0; i < m_ls_office.Count; i++)
            {
                for (int j = 0; j < ipids.Length; j++)
                {
                    if (ipids[j] == m_ls_office[i].IP_ID.ToString())
                    {
                        ret += m_ls_office[i].OFFICE_ALLY + ",";
                    }
                }
            }
            return egString.trim(ret, ',');
        }
        /// <summary>
        /// ȡһ������ʹ�õ�OFFICE����
        /// </summary>
        public string UsingOffice()
        {
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                if (m_ls_office[i].USINGFLAG) return m_ls_office[i].OFFICE_NO;
            }
            return "WUH128";
        }

        


        /// <summary>
        /// �жϵ�ǰ��server��ipid���ڵ�server�Ƿ���ͬ(��������,IP,PORT)
        /// </summary>
        public bool IpidSameServer(string ipid)
        {
            string name = "";
            string ip = "";
            int port = 0;
            for (int i = 0; i < m_ls_office.Count; i++)
            {
                if (ipid == m_ls_office[i].IP_ID.ToString())
                {
                    name = m_ls_office[i].SERVER_NAME;
                    ip = m_ls_office[i].SERVER_IP;
                    port = m_ls_office[i].SERVER_PORT;
                    break;
                }

            }
            return (name == SERVER_NAME && ip==SERVER_IP && port == SERVER_PORT);
        }
        /// <summary>
        /// �ж�ipid�����Ƿ�����ͬһ��������
        /// </summary>
        /// <param name="ipid"></param>
        /// <returns></returns>
        public bool IpidSameServer(string[] ipid)
        {
            if (ipid.Length < 2) return true;
            int index = indexof(ipid[0]);
            if (index < 0) throw new Exception("LoginResult.IpidSameServer : ���ڲ�����Ȩʹ�õ�����");
            for (int i = 1; i < ipid.Length; ++i)
            {
                int index2 = indexof(ipid[i]);
                if (index2 < 0) throw new Exception("LoginResult.IpidSameServer : ���ڲ�����Ȩʹ�õ�����");
                if (!m_ls_office[index].SameServerWith(m_ls_office[index2])) return false;
            }
            return true;
        }
        /// <summary>
        /// �ж�����ʹ�õ������Ƿ�����ͬһ��OFFICE,���򷵻�OFFICE,���򷵻�""
        /// </summary>
        /// <returns></returns>
        public string IpidUsingIsSameOffice()
        {
            string office = "";
            List<int> indexs = indexOfUsing;
            for (int i = 0; i < indexs.Count; ++i)
            {
                if (office == "") office = m_ls_office[indexs[i]].OFFICE_NO;
                else
                {
                    if (office != m_ls_office[indexs[i]].OFFICE_NO) return "";
                }
            }
            return office;
        }
        /// <summary>
        /// ����ʹ�õ�IPID�б�
        /// </summary>
        public List<string> IpidUsing
        {
            get
            {
                List<string> ret = new List<string>();
                for (int i = 0; i < m_ls_office.Count; ++i)
                {
                    if (m_ls_office[i].USINGFLAG) ret.Add(m_ls_office[i].IP_ID.ToString());
                }
                return ret;
            }
        }
        /// <summary>
        /// ipid��m_ls_office�ж�Ӧ���������Ҳ���Ϊ-1
        /// </summary>
        public int indexof(string ipid)
        {
            for (int i = 0; i < m_ls_office.Count; ++i)
            {
                if (ipid == m_ls_office[i].IP_ID.ToString())
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// ȡ�ö�Ӧipid���ڵķ�����
        /// </summary>
        public void IpidServerPort(string ipid, ref string serverip, ref int port)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < m_ls_office.Count; i++)
            {
                ls.Add(m_ls_office[i].IP_ID.ToString() + "  "+m_ls_office[i].SERVER_IP + ":" + m_ls_office[i].SERVER_PORT.ToString());
            }
            EagleFileIO.LogWrite(string.Join("\r\n" , ls.ToArray()));
            int index = indexof(ipid);
            if (index < 0) return;
            serverip = m_ls_office[index].SERVER_IP;
            port = m_ls_office[index].SERVER_PORT;
            return;
        }

        private bool m_loginCmdFlag = false;
        private bool m_loginResFlag = false;
        private string m_Passport = "";
        private int m_TrimLen = 0;
        private string m_ServerIp = "";
        private string m_ServerDns = "";
        private int m_ServerPort = 0;
        private string m_ServerName = "";
        private int m_UserStat = 1;
        private string m_Insurance_User = "";
        private string m_Insurance_Pass = "";
        private int m_AgentStat = 1;
        private bool m_Expired = true;
        private string m_Tickets = "";
        /// <summary>
        /// ���������б�
        /// </summary>
        public List<OFFICE_INFO> m_ls_office = new List<OFFICE_INFO>();
        /// <summary>
        /// ����ָ���б�
        /// </summary>
        public List<string> m_ls_command = new List<string>();
        public string PASSPORT { get { return m_Passport; } }
        /// <summary>
        /// ����Э�����ͷ����
        /// </summary>
        public int TRIM_LENGTH { get { return m_TrimLen; } }
        /// <summary>
        /// �û�Ҫ���ӵķ�����IP
        /// </summary>
        public string SERVER_IP { get { return m_ServerIp; } }
        /// <summary>
        /// �û����ӵķ�����DNS
        /// </summary>
        public string SERVER_DNS { get { return m_ServerDns; } }
        /// <summary>
        /// �û����ӵķ������˿�
        /// </summary>
        public int SERVER_PORT { get { return m_ServerPort; } }
        /// <summary>
        /// �û����ӵķ�������
        /// </summary>
        public string SERVER_NAME { get { return m_ServerName; } }
        /// <summary>
        /// �û�״̬ 0:����
        /// </summary>
        public int USER_STAT { get { return m_UserStat; } }
        /// <summary>
        /// �󶨵ı��մ�ӡ�ʺ�
        /// </summary>
        public string INSURANCE_USER { get { return m_Insurance_User; } }
        /// <summary>
        /// �󶨵ı��մ�ӡ����
        /// </summary>
        public string INSURANCE_PASS { get { return m_Insurance_Pass; } }
        /// <summary>
        /// �û�����������״̬ 0:����
        /// </summary>
        public int AGENT_STAT { get { return m_AgentStat; } }
        /// <summary>
        /// �û��Ƿ����
        /// </summary>
        public bool EXPIRED { get { return m_Expired; } }
        /// <summary>
        /// ���ʾʲô������
        /// </summary>
        public string TICKETS { get { return m_Tickets; } }
        /// <summary>
        /// �Ƿ��¼�ɹ�
        /// </summary>
        public bool SUCCEED
        {
            get
            {
                bool ret = (m_loginResFlag && m_loginCmdFlag && m_UserStat == 0 && m_AgentStat == 0 && !m_Expired);
                if (ret) return true;
                try
                {
                    if (!m_loginCmdFlag) throw new Exception("CM:��������������");
                    if (!m_loginResFlag) throw new Exception("��¼ʧ�ܣ������û���������");
                    if (m_Expired) throw new Exception("�û��ѹ���");
                    if (m_UserStat != 0) throw new Exception("�û�״̬����");
                    if (m_AgentStat != 0) throw new Exception("�û����������̵�״̬����");
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                return false;
            }
        }
        /// <summary>
        /// ����ʹ�õ�������LIST�е�������
        /// </summary>
        public List<int> indexOfUsing
        {
            get
            {
                List<int> ret = new List<int>();
                for (int i = 0; i < m_ls_office.Count; ++i)
                {
                    if (m_ls_office[i].USINGFLAG) ret.Add(i);
                }
                return ret;
            }
        }
    }
    /// <summary>
    /// ������Ϣ(IPID,���ŷ���IP,������IP,������DNS,����˿�,����,OFFICE�ű���,OFFICE��,�Ƿ����ڱ�ʹ��)
    /// </summary>
    public class OFFICE_INFO
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public int IP_ID;
        /// <summary>
        /// ���õĺ���ר�ߵ�ַ(350) �� ��ʾ���Ӻ��ŷ������ĵ�ַ
        /// </summary>
        public string CONFIG_IP;
        /// <summary>
        /// ���������ڷ������Ĺ����򹩿ͻ����ӵķ�������ַ��������Ӧ
        /// </summary>
        public string SERVER_IP;
        /// <summary>
        /// ���������ڷ������Ĺ����򹩿ͻ����ӵķ������������ַ��Ӧ
        /// </summary>
        public string SERVER_DNS;
        /// <summary>
        /// ���������ڷ������Ĺ����򹩿ͻ����ӵĶ˿�
        /// </summary>
        public int SERVER_PORT;
        /// <summary>
        /// �������ڷ�����������
        /// </summary>
        public string SERVER_NAME;
        /// <summary>
        /// �����õ�OFFICE�ֺ�(��ӦPeiZhi,��WUH128-1)
        /// </summary>
        public string OFFICE_ALLY;
        /// <summary>
        /// �����õ�OFFICE(��WUH128)
        /// </summary>
        public string OFFICE_NO;
        /// <summary>
        /// ��ʾ�Ƿ�����ʹ��
        /// </summary>
        public bool USINGFLAG;

        public bool SameServerWith(OFFICE_INFO oi)
        {
            if (SERVER_IP == oi.SERVER_IP && SERVER_DNS == oi.SERVER_DNS && SERVER_NAME == oi.SERVER_NAME && SERVER_PORT == oi.SERVER_PORT)
            {
                return true;
            }
            return false;
        }
    
    }
    public class LoginResultBtoc
    {
        /// <summary>
        /// ��¼B2C��Ľ����Ϣ(XML������)
        /// </summary>
        public struct LOGIN_INFO_BTOC
        {
            /// <summary>
            /// indicate the login is succeed or not
            /// </summary>
            public bool bFlag;
            /// <summary>
            /// b2c username
            /// </summary>
            public string username;
            /// <summary>
            /// b2c password
            /// </summary>
            public string password;
            public string uuid;
            /// <summary>
            /// b2b username
            /// </summary>
            public string mapuser;
            /// <summary>
            /// b2b password
            /// </summary>
            public string mappass;
            public int departid;
            public string departname;
            public int corpid;
            public string corpname;

            /// <summary>
            /// b2c ��̨��ַ
            /// </summary>
            public string b2cweburl;
            public int userid;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="xml">login b2c xml </param>
            /// <param name="b2curl">b2c url like "http://addr/"</param>
            public void FromXml(string xml,string b2curl)
            {
                bFlag = !(xml.Trim().ToLower() == "failed");
                if (!bFlag) return;
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xml);
                uuid = xd.SelectSingleNode("//UserRecord/Uuid").InnerText.Trim();
                mapuser = xd.SelectSingleNode("//UserRecord/MapUser").InnerText.Trim();
                mappass = xd.SelectSingleNode("//UserRecord/MapPassword").InnerText.Trim();
                departid = Convert.ToInt32(xd.SelectSingleNode("//UserRecord/DepartmentID").InnerText.Trim());
                departname = xd.SelectSingleNode("//UserRecord/DepartmentName").InnerText.Trim();
                corpid = Convert.ToInt32(xd.SelectSingleNode("//UserRecord/CorpID").InnerText.Trim());
                corpname = xd.SelectSingleNode("//UserRecord/CorpName").InnerText.Trim();

                b2cweburl = string.Format("{0}/default.aspx?strSessionUUID={1}", b2curl, uuid);

                userid = Convert.ToInt32(xd.SelectSingleNode("//UserRecord/UserID").InnerText);

            }
        }
    }
    /// <summary>
    /// ���������е�¼��ϢB2B,B2C,�û���,����,��ַ,�������
    /// </summary>
    public class LoginInfo
    {
        public B2B b2b;
        public B2C b2c;
        public BORC borc;
        /// <summary>
        /// b2bΪ0,b2cΪ1
        /// </summary>
        public enum BORC : byte
        {
            b2b = 0,
            b2c = 1
        }
        /// <summary>
        /// �ṹ��B2B��Ϣ
        /// </summary>
        public struct B2B
        {
            /// <summary>
            /// b2b �û���
            /// </summary>
            public string username;
            /// <summary>
            /// b2b ����
            /// </summary>
            public string password;
            /// <summary>
            /// b2b web�����ַ
            /// </summary>
            public string webservice;
            /// <summary>
            /// b2b web��̨��ַ
            /// </summary>
            public string webside;
            /// <summary>
            /// b2b ��¼��Ϣ
            /// </summary>
            public LoginResult lr;
        }
        public struct B2C
        {
            /// <summary>
            /// b2c �û���
            /// </summary>
            public string username;
            /// <summary>
            /// b2c ����
            /// </summary>
            public string password;
            /// <summary>
            /// b2c web�����ַ
            /// </summary>
            public string webservice;
            /// <summary>
            /// b2c web��̨��ַ
            /// </summary>
            public string website;
            /// <summary>
            /// b2c ��¼��Ϣ
            /// </summary>
            public LoginResultBtoc.LOGIN_INFO_BTOC lr;
        }

    }
}
