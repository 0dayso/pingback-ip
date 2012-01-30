using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Globalization;

namespace EagleWebService
{
    public class IbeFunc
    {
        wsIbe ws;
        public IbeFunc()
        {
            ws = new wsIbe();
        }
        /// <summary>
        /// ֱ�ɵ�SK��ѯ
        /// </summary>
        public string SK(string city1, string city2, DateTime date, string airline)
        {
            return ws.wsSK(city1, city2, date.ToString("yyyyMMdd 00:00:00"), airline, true);
        }
        private DateTimeFormatInfo gbDtfi = new CultureInfo("en-us", false).DateTimeFormat;
        public void AV(string city, string city2, DateTime dt, string airline, bool directly, bool eticket, ref string avres)
        {
            avres = ws.wsAV(city, city2, dt.ToString("yyyyMMdd 00:00:00"), airline, directly, eticket);
        }
        /// <summary>
        /// SellSeat:ע��flightdate�ĸ�ʽ,yyyy-MM-dd,10λ
        /// </summary>
        public string SS(string[] flightno, string[] actioncode, string[] flightdate, string[] orgcity, string[] destcity, string[] bunk, string[] name, string limitdate, string[] cardids, string[] remarks)
        {
            try
            {
                string fn = "";
                string ac = "";
                string fd = "";
                string oc = "";
                string dc = "";
                string bk = "";
                string nm = "";
                string ci = "";
                string rmk = "";
                for (int i = 0; i < flightno.Length; i++)
                {
                    string temp = "~";
                    if (flightno[i] == "") continue;
                    fn += flightno[i] + temp;
                    ac += actioncode[i] + temp;
                    fd += flightdate[i] + temp;
                    if (flightdate[i].Length != 10) EagleString.EagleFileIO.LogWrite("IBE�ĺ������ڸ�ʽΪyyyy-MM-dd,10λ");
                    oc += orgcity[i] + temp;
                    dc += destcity[i] + temp;
                    bk += bunk[i] + temp;
                }
                if (fn == "") return "";
                fn = EagleString.egString.trim(fn, '~');
                ac = EagleString.egString.trim(ac, '~');
                fd = EagleString.egString.trim(fd, '~');
                oc = EagleString.egString.trim(oc, '~');
                dc = EagleString.egString.trim(dc, '~');
                bk = EagleString.egString.trim(bk, '~');
                for (int i = 0; i < name.Length; i++) nm += name[i] + (i == name.Length - 1 ? "" : "~");
                if (cardids != null)
                    for (int i = 0; i < cardids.Length; i++)
                    {
                        if (cardids[i] == "") continue;
                        ci += cardids[i] + (i == cardids.Length - 1 ? "" : "~");
                    }
                if (remarks != null)
                    for (int i = 0; i < remarks.Length; i++) rmk += remarks[i] + (i == remarks.Length - 1 ? "" : "~");

                string pnr = ws.wsSS(fn, ac, fd, oc, dc, bk, nm, limitdate, ci, rmk);
                EagleString.EagleFileIO.LogWrite("����IBE��SS����,PNR=" + pnr);
                return pnr;
            }
            catch
            {
                throw new Exception("ibe����");
                //return "";
            }
        }
        /// <summary>
        /// ����IBE��rt�����(��XML��)
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        public string RT(string pnr)
        {
            string ret = ws.wsRT(pnr);
            EagleString.EagleFileIO.LogWrite("����IBE��RT����,PNR=" + pnr);
            EagleString.EagleFileIO.LogWrite(ret);
            return ret;
        }
        /// <summary>
        /// ����IBE��RT���(XML��)
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        public string RT2(string pnr)
        {
            string ret = ws.wsRT2(pnr);
            return ret;
        }
        /// <summary>
        /// ȡPNR�к���Ļ���
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        public Hashtable DSG(string pnr)
        {
            string xmlstring = ws.wsDSG(pnr);

            if (xmlstring == "") throw new Exception("DSG:IBE����");
            Hashtable ht = new Hashtable();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xmlstring);
            XmlNode xn = xd.SelectSingleNode("ibe");
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                string key = xn.ChildNodes[i].SelectSingleNode("flightno").InnerText;
                string value = xn.ChildNodes[i].SelectSingleNode("planestyle").InnerText;
                if (key[0] == '*') key = key.Substring(1);
                ht.Add(key, value);
            }
            EagleString.EagleFileIO.LogWrite("����IBE��DSG����,PNR=" + pnr);
            return ht;
        }

        public DateTime ServerTime()
        {
            return DateTime.Parse(ws.wsGetServerTime());
        }
    }
}
