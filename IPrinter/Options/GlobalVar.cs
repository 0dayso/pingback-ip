/*Create by 易格网科技
 * 创建人：Wang.Clawc 创建时间：2007年
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Options
{
    public class GlobalVar
    {
        /// <summary>
        /// B2C 乘客列表
        /// 格式:姓名-身份证
        /// </summary>
        static public string[] PassengersArray = null;
        /// <summary>
        /// B2C 部门ID:部门名称
        /// 用分号相隔
        /// </summary>
        static public string B2CDepartmentID = "";
        /// <summary>
        /// B2C 公司ID:公司名称
        /// 用分号相隔
        /// </summary>
        static public string B2CCorpID = "";
        /// <summary>
        /// B2C 用户UID
        /// </summary>
        static public string B2CUserID = "";
        /// <summary>
        /// B2C 用户登录SessionID 形如"strSessionUUID=xxxxxxxxxxxx"
        /// </summary>
        static public string B2CSessionStr = "";
        /// <summary>
        /// 新增订单，返回的订单 ID 号
        /// </summary>
        static public int B2CNewOrderID;
        /// <summary>
        /// 是否自动提交
        /// </summary>
        static public bool B2CIsAutoSubmit = false;
        /// <summary>
        /// B2C 模式的配置字符串节点名称
        /// </summary>
        static public string B2CConfigString = "BtocUser";
        /// <summary>
        /// B2C 后台网址
        /// </summary>
        static public string B2CURL = "";
        /// <summary>
        /// B2C 电话呼叫信息
        /// </summary>
        static public string B2CCallingXml = "";

        /// <summary>
        /// B2C 是否有权生成新订单
        /// “true” 有权 “false”无权
        /// </summary>
        static public string B2cCanCreateNewOrder ="";

        /// <summary>
        /// B2C 是否有权进行退款和撤销支付（电话支付）
        /// “1” 有权 “0”无权
        /// </summary>
        static public bool B2cCanRefund = false;

        /// <summary>
        /// 旅游平台操作员用户名
        /// </summary>
        static public string TravelUsername = "";
        /// <summary>
        /// 旅游平台操作员密码
        /// </summary>
        static public string TravelPassword = "";
        /// <summary>
        /// IBE 地址
        /// </summary>
        static public string IbeUrl = "http://221.123.65.26:8080/testWS/HiWSService";//"http://221.123.65.26:8088/axis/HiWS.jws";
        static public Int32 IbeID;
        /// <summary>
        /// Remoting 地址
        /// </summary>
        static public string RemotingUrl;

        static public string B2bWebServiceURL;
        static public string B2bWebServiceURL1;
        static public string B2bWebServiceURL2;
        static public string B2bLoginURL;
        static public string B2bLoginName;
        static public string B2bLoginPassword;
        static public string B2bLoginXml;

        //保险账号和密码
        static public string IAUsername;
        static public string IAPassword;
        static public string IAWebServiceURL;
        static public string IAWebServiceURL1;
        static public string IAWebServiceURL2;
        /// <summary>
        /// 保存最后一次选中的产品类型
        /// </summary>
        static public string IACode;
        static public string IALoginUrl;
        static public string IALoginUrl1;
        static public string IALoginUrl2;
        /// <summary>
        /// 左边距
        /// </summary>
        static public int IAOffsetX;
        /// <summary>
        /// 上边距
        /// </summary>
        static public int IAOffsetY;
        /// <summary>
        /// 已选中的网络运营商
        /// </summary>
        static public XMLConfig.ISP SelectedISP;
        /// <summary>
        /// 导PNR、票号的方式
        /// </summary>
        static public XMLConfig.QueryType QueryType;

        /// <summary>
        /// 是否还在连接配置服务器
        /// </summary>
        static public bool IsConnecting = false;
        //全局socket
        static public System.Net.Sockets.Socket socketGlobal = null;
    }
}
