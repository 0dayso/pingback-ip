using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using logic;



namespace WS4
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string getEgSoap(string p_str)
        {
            string strRet = "";
            /*if(logic.GlobalFunc.iMaxConns <= 0)
            {
                logic.GlobalFunc.iMaxConns = Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["ConnMax"].ToString());
            }
            //<?xml version="1.0" encoding="utf-8"?><eg><cm>RefreshSelf</cm><UserName>jm</UserName></eg>

            if(logic.GlobalFunc.iGlobalUserNum < 0)
                logic.GlobalFunc.iGlobalUserNum = 0;

            if(logic.GlobalFunc.iGlobalUserNum < logic.GlobalFunc.iMaxConns)
            {
                logic.GlobalFunc.iGlobalUserNum++;*/
            strRet = CMM.procCmm(p_str);
            //}
            return strRet;
        }


        [WebMethod]//得到代理商信息
        public string getAgentInfo(string p_strAgentId)
        {
            string strAgentId = p_strAgentId.Trim();
            logic.JoinAgent eu = new logic.JoinAgent();
            string strRet = eu.getAgentSrvInfo(strAgentId);
            return strRet;
        }

        /// <summary>
        /// 返回“s”则合法，即可以使用该配置服务器程序
        /// 返回“f”则非法，客户端会提示“passport认证失败”
        /// </summary>
        /// <param name="p_strPassPort"></param>
        /// <returns></returns>
        [WebMethod]//得到服务器上的身份令牌
        public string getSevInfo(string p_strPassPort)
        {
            string strPassPort = p_strPassPort.Trim();
            logic.EgUser eu = new logic.EgUser();
            string strRet = eu.CmpPassport(strPassPort);
            return strRet;
        }

        [WebMethod]//得到首页特价航班信息
        public DataSet getTjInfo(string p_strAgentId, string p_strIsAll)
        {
            string strAgentId = p_strAgentId.Trim();
            Tj tj = new Tj();
            return tj.getTjRs(p_strAgentId, p_strIsAll);
        }

    }
}
