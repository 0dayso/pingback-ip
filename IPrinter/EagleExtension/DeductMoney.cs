using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace EagleExtension
{
    public class DeductMoney
    {
        public DeductMoney(string username,string webserver)
        {
            m_username = username;
            m_webserver = webserver;
        }

        public void Run()
        {
            if (m_running) return;
            m_running = true;
            string[] pnrs = PnrsNeedDeduct();
            //string[] pnrs = new string[] { "VXVZ6" };
            for (int i = 0; i < pnrs.Length; i++)
            {
                try
                {
                    int money = MoneyOfPnr(pnrs[i]);
                    CutMoney(pnrs[i], money);
                }
                catch(Exception e1)
                {
                    bool bflag = false;
                    EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_webserver);
                    EagleString.EagleFileIO.LogWrite("(不能扣款：特殊舱位，无法对应价格!)" + pnrs[i]);
                    EagleString.EagleFileIO.LogWrite(e1.Message);
                    kf.WriteLogToServer(m_username, "(不能扣款：特殊舱位，无法对应价格!)", pnrs[i], ref bflag);
                }
            }
            m_running = false;
        }
        /// <summary>
        /// 从服务器上取没有扣款的PNR
        /// </summary>
        private string[] PnrsNeedDeduct()
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_webserver);
            return kf.PnrNeedDeduct(m_username);//admin
        }
        private int MoneyOfPnr(string pnr)
        {
            if (!EagleString.BaseFunc.PnrValidate(pnr)) throw new Exception("INVALID PNR!");

            EagleWebService.IbeFunc ibe = new EagleWebService.IbeFunc();

            string xml = ibe.RT2(pnr);

            if (xml == "")
            {
                if (ibe.RT(pnr).ToLower().Contains("cancel")) return 0;
            }
            EagleWebService.IbeRtResult ibert = new EagleWebService.IbeRtResult(xml);
            string[] names = ibert.getpeopleinfo(0);
            int iAdult = 0, iChild = 0;//成人数与儿童数
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Contains("(CHD)"))
                {
                    iChild++;
                }
                else
                {
                    iAdult++;
                }
            }
            EagleWebService.IbeFunc func = new EagleWebService.IbeFunc();
            Hashtable ht ;
            try
            {
                ht = func.DSG(pnr);
            }
            catch
            {
                ht = null;
            }
            string[] flights = ibert.getflightsegsinfo();
            int pAdult = 0, pChild = 0;
            for (int i = 0; i < flights.Length; i++)
            {
                string[] a = flights[i].Split('~');
                if (a[0] == "ARNK") continue;
                int y = EagleString.EagleFileIO.PriceOf(a[2]);
                pChild += EagleString.egString.TicketPrice(y, 50);
                int rebate;
                //Fri Apr 17 10:30:00 CST 2009
                string[] b = a[3].Split(' ');
                DateTime flightdate = new DateTime(int.Parse(b[5]), EagleString.BaseFunc.MonthInt2(b[1]), int.Parse(b[2]));
                if (DateTime.Compare(flightdate, DateTime.Parse("2009-4-20")) >= 0)
                {
                    rebate = EagleString.EagleFileIO.RebateOfNew(a[1][0], a[0]);
                }
                else
                {
                    rebate = EagleString.EagleFileIO.RebateOf(a[1][0], a[0]);
                }
                if (rebate == 29 || rebate == 43) throw new Exception("SPECIAL BUNK!");
                pAdult += EagleString.egString.TicketPrice(y, rebate);
                pAdult += (ht == null ? 50 : EagleString.EagleFileIO.TaxOfBuildBy(ht[a[0]].ToString()));
                int fuel = EagleString.EagleFileIO.TaxOfFuelBy(EagleString.EagleFileIO.DistanceOf(a[2]));
                pChild += EagleString.egString.TicketPrice(fuel, 50);
                pAdult += fuel;
            }
            int ret = pAdult * iAdult + pChild * iChild;
            return ret;
        }
        private void CutMoney(string pnr,int price)
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_webserver);
            bool bflag = false;
            float ftemp = 0F;
            kf.DecFee(pnr, price, ref bflag, ref ftemp);
            if (!bflag)
            {
                kf.WriteLogToServer(m_username, "调用扣款失败！", pnr + price.ToString(), ref bflag);
            }
            else
            {
                kf.WriteLogToServer(m_username, "调用扣款成功！", pnr + price.ToString(), ref bflag);
            }
        }
        string m_username;
        string m_webserver;
        static bool m_running = false;
    }
}
