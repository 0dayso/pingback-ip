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
        /// 登录
        /// </summary>
        /// <param name="isB2c">是否B2C</param>
        /// <param name="b2cService">B2C服务地址</param>
        /// <param name="b2cManager">B2C后台地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="lp">网络线路,电信:0,网通:1</param>
        /// <param name="resB2c">B2C结果</param>
        /// <param name="loginInfo">返回结果</param>
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
            if (!flagLogin) throw new Exception("登录B2B失败，请检查网络及用户名密码是否正确！");
            EagleString.LoginResult lr = new LoginResult(loginb2bxml);
            if (lr.EXPIRED) throw new Exception("该B2B用户已失效！");
            if (lr.AGENT_STAT != 0) throw new Exception("该B2B用户所在代理商已失效！");
            if (lr.USER_STAT != 0) throw new Exception("该B2B用户状态已失效！");

            loginInfo.b2b.username = b2buser;
            loginInfo.b2b.password = b2bpass;
            loginInfo.b2b.webservice = b2bWebService;
            loginInfo.b2b.webside = b2bWebSite;
            loginInfo.b2b.lr = lr;
        }
        /// <summary>
        /// 配置共享登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="newipid">共享配置信息</param>
        /// <param name="loginInfo">引用输出登录信息</param>
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
            if (!flagLogin) throw new Exception("登录B2B失败，请检查网络及用户名密码是否正确！");
            ///把共享的配置加进去
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(loginb2bxml);
            XmlNode xn = xmldoc.SelectSingleNode("//eg//IPS");
            xn.InnerXml += newipid;
            loginb2bxml = xmldoc.InnerXml;
            ///共享配置加入完毕
            EagleString.LoginResult lr = new LoginResult(loginb2bxml);
            if (lr.EXPIRED) throw new Exception("该B2B用户已失效！");
            if (lr.AGENT_STAT != 0) throw new Exception("该B2B用户所在代理商已失效！");
            if (lr.USER_STAT != 0) throw new Exception("该B2B用户状态已失效！");
            loginInfo.b2b.username = username;
            loginInfo.b2b.password = password;
            loginInfo.b2b.webservice = b2bWebService;
            loginInfo.b2b.lr = lr;
        }
        /// <summary>
        /// 从IBE获取结果
        /// </summary>
        /// <param name="cmds">指令组</param>
        /// <param name="result">如果返回为true,则为IBE返回结果</param>
        /// <returns>是否可用IBE</returns>
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
