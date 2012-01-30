#define receipt_
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
using System.Collections;

namespace ePlus
{
    class GlobalVar2
    {
        /// <summary>
        /// 0:原普通用户,1:BtoC用户
        /// </summary>
        static public int gbUserModel = 0;

        static public YzpBtoC.YZPnewOrder gbYzpOrder;// = new YzpBtoC.YZPnewOrder();

        static public bool registered = false;
        static public bool b_试用版 = false;
        static public string xmlPolicies = "";

        static public string bxUserAccount = "";
        static public string bxPassWord = "";
        static public string bxTelephone = "";
        static public System.Globalization.DateTimeFormatInfo gbDtfi = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
        static public bool bTempus = false;//tempus的保险打印软件
        static public int gbConnectType = 1;//0:默认，1:电信，2:网通
        static public string gbFromto = "";//av指令中的fromto
        static public bool gbUsingBlackWindows = true;
        static public bool gbDisplayPolicy = false;

        static public int gbFeeOfDec = 0;
        static public string gbPnrOfEtdzing = "";

        
        /// <summary>
        /// 是否使用与eplus风格，进行抢配置模式
        /// </summary>
        static public bool gbEplusStyle = false;

        /// <summary>
        /// 行程单打印临时姓名，以防没打出姓名
        /// </summary>
        static public string tmpPrintName = "";
        /// <summary>
        /// 行程单打印临时PNR，以防没打出PNR
        /// </summary>
        static public string tmpPrintPnr = "";
        /// <summary>
        /// 用于存放已经提交的PNR及对应取到时间。
        /// </summary>
        static public Hashtable gbHashTableOfTheSubmittingPnr = new Hashtable();
        static public string strFare_Tax_Gain_1 = "0~0~0~0";//航段1的 票面+税+返点
        static public string strFare_Tax_Gain_2 = "0~0~0~0";//航段2的 票面+税+返点


        static public string gbPassegersInEasyVersion = "";//简易版乘客姓名，身份证，电话串;
        
#if !receipt
        //static public BookTicket bookTicket = new BookTicket(false);
#else
        static public BookTicket bookTicket;
#endif
        //可能存在不同价格的航段
        static public void differenceFC(string cityBeg, string cityEnd)
        {
            string city1 = cityBeg.ToLower().Trim();
            string city2 = cityEnd.ToLower().Trim();
            bool bDiff = false;
            if ((city1 == "ngb" && city2 == "sha") || (city1 == "sha" && city2 == "ngb"))
                bDiff = true;
            if ((city1 == "ngb" && city2 == "pvg") || (city1 == "pvg" && city2 == "ngb"))
                bDiff = true;
            if (bDiff) MessageBox.Show("注意：此航段对于不同航空公司存在不同价格，请在黑屏中用FD查询实际价格");
        }
    }
}
