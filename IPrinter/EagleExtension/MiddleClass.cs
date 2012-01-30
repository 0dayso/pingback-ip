using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EagleString;
using System.Xml;

namespace EagleExtension
{
   
    public class Api
    {
        public static void login2server(string username, string password, ref EagleString.LoginInfo loginInfo)
        {
            EagleString.LoginResultBtoc.LOGIN_INFO_BTOC resB2c = new LoginResultBtoc.LOGIN_INFO_BTOC();
            login2server(false, "", "", username, password, LineProvider.DianXin, resB2c, ref loginInfo);
        }
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="isB2c">�Ƿ�B2C</param>
        /// <param name="b2cService">B2C�����ַ</param>
        /// <param name="b2cManager">B2C��̨��ַ</param>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
        /// <param name="lp">������·,����:0,��ͨ:1</param>
        /// <param name="resB2c">B2C���</param>
        /// <param name="loginInfo">���ؽ��</param>
        public static void login2server(
            bool isB2c, 
            string b2cService, 
            string b2cManager, 
            string username, 
            string password, 
            EagleString.LineProvider lp, 
            EagleString.LoginResultBtoc.LOGIN_INFO_BTOC resB2c, 
            ref EagleString.LoginInfo loginInfo
            )
        {
            loginInfo.borc = (isB2c ? LoginInfo.BORC.b2c : LoginInfo.BORC.b2b);
            if (isB2c)
            {
                EagleExtension.LoginB2c(username, password, ref resB2c, b2cManager, b2cService);
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
            if (!flagLogin) throw new Exception("��¼B2Bʧ�ܣ��������缰�û��������Ƿ���ȷ��");
            EagleString.LoginResult lr = new LoginResult(loginb2bxml);
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
        /// ���ù����¼
        /// </summary>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
        /// <param name="newipid">����������Ϣ</param>
        /// <param name="loginInfo">���������¼��Ϣ</param>
        public static void login2server(
           string username,
           string password,
           string newipid,
           ref EagleString.LoginInfo loginInfo
           )
        {
            string b2buser = username;
            string b2bpass = password;
            string loginb2bxml=string.Empty;
            bool flagLogin = false;
            EagleString.LineProvider lp=new LineProvider();            
            EagleString.ServerCenterB2B scb2b = new ServerCenterB2B();
            string b2bWebSite = "";
            string b2bWebService = "";
            scb2b.ServerAddressB2B(EagleFileIO.Server, lp, 0, ref b2bWebService, ref b2bWebSite);

            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(b2bWebService);
            kf.LogIn(b2buser, b2bpass, ref loginb2bxml, ref flagLogin);
            if (!flagLogin) throw new Exception("��¼B2Bʧ�ܣ��������缰�û��������Ƿ���ȷ��");
            ///�ѹ�������üӽ�ȥ
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(loginb2bxml);
            XmlNode xn = xmldoc.SelectSingleNode("//eg//IPS");
            xn.InnerXml += newipid;
            loginb2bxml = xmldoc.InnerXml;
            ///�������ü������
            EagleString.LoginResult lr = new LoginResult(loginb2bxml);
            if (lr.EXPIRED) throw new Exception("��B2B�û���ʧЧ��");
            if (lr.AGENT_STAT != 0) throw new Exception("��B2B�û����ڴ�������ʧЧ��");
            if (lr.USER_STAT != 0) throw new Exception("��B2B�û�״̬��ʧЧ��");
            loginInfo.b2b.username = username;
            loginInfo.b2b.password = password;
            loginInfo.b2b.webservice = b2bWebService;
            loginInfo.b2b.lr = lr;
        }
        /// <summary>
        /// ��IBE��ȡ���
        /// </summary>
        /// <param name="cmds">ָ����</param>
        /// <param name="result">�������Ϊtrue,��ΪIBE���ؽ��</param>
        /// <returns>�Ƿ����IBE</returns>
        public static bool ResultFromIbe(string cmds,string city,ref string result)
        {
            bool ret = false;
            string[] a = cmds.ToLower().Split('~');
            string last = a[a.Length - 1];
            if (last.StartsWith("av"))
            {
                ret = true;
                EagleString.AvCommand avcmd = new AvCommand(city, last);
                
                new EagleWebService.IbeFunc().AV(avcmd.City1, avcmd.City2, avcmd.Date, avcmd.AirLine, avcmd.Direct, true, ref result);
            }
            return ret;
        }
    }
}
