using System;
using System.Collections.Generic;
using System.Text;
using gs.para;

namespace EagleFinance.zOther.ws
{
    class services
    {
        public static string url = "http://yinge.eg66.com/WS3/egws.asmx";//网通http://wangtong.eg66.com/WS3/egws.asmx
        public static string getUndecfeePnr(string userAccount)
        {
            try
            {
                WS.egws w = new WS.egws(services.url);
                NewPara np = new NewPara();
                np.AddPara("cm", "GetUnPayPnr");
                np.AddPara("UserName", userAccount);
                string send = np.GetXML();
                string recv = w.getEgSoap(send);
                if (recv == "") throw new Exception("不能从服务器取到未扣款PNR");
                NewPara n = new NewPara(recv);
                string cm = n.FindTextByPath("//eg/cm");
                if (cm != "RetUnPayPnr") throw new Exception(cm);
                return n.FindTextByPath("//eg/Pnrs");
            }
            catch(Exception ex)
            {
                GlobalApi.appenderrormessage("同步错误:"+ex.Message);
            }

            return "";
        }
        public static bool decfee(string pnr, string money)
        {
            try
            {
                WS.egws ws = new WS.egws(url);
                NewPara np = new NewPara();
                np.AddPara("cm", "DecFee2");
                np.AddPara("Pnr", pnr);
                np.AddPara("TicketPrice", money);
                string strReq = np.GetXML();
                string strRet = ws.getEgSoap(strReq);
                NewPara np1 = new NewPara(strRet);
                string cm = np1.FindTextByPath("//eg/cm");
                string decstat = np1.FindTextByPath("//eg/DecStat");
                string m = np1.FindTextByPath("//eg/NewUserYe");
                if (cm == "RetDecFee2" && decstat == "DecSucc")
                {
                    GlobalApi.appenderrormessage(string.Format("--->同步扣款成功:PNR={0},MONEY={1}",pnr,money));
                    return true;
                }
            }
            catch(Exception ex)
            {
                GlobalApi.appenderrormessage("同步错误:" + ex.Message);
            }
            return false;
        }
        public static string XlsAutoImport(string username, DateTime start, DateTime stop)
        {
            WS.egws ws = new WS.egws(url);
            NewPara np = new NewPara();
            np.AddPara("cm", "GetEticketExcel");
            np.AddPara("user", username);
            np.AddPara("startdate", start.ToShortDateString());
            np.AddPara("enddate", stop.AddDays(1).ToShortDateString());
            string strReq = np.GetXML();
            string strRet = ws.getEgSoap(strReq);
            //<eg><cm>ReGetEticketExcel</cm><result>E:\www\Excel\claw2009-1-15.xls<result></eg>
            NewPara np1 = new NewPara(strRet);
            string ret = np1.FindTextByPath("//eg/result");
            return ret;
        }
    }
}
