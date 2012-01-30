using System;
using System.Collections.Generic;
using System.Text;

namespace ePlus.eTicket
{
    class NewEticket
    {
        public int peaplecount = 0;

        public string psgname = "";
        public string sail = "";
        public string pnr = "";

        public string date1 = "";
        public string hour1 = "";
        public string minute1 = "";
        public string carrier1 = "";
        public string flightno1 = "";
        public string bunk1 = "";

        public string date2 = "";
        public string hour2 = "";
        public string minute2 = "";
        public string carrier2 = "";
        public string flightno2 = "";
        public string bunk2 = "";

        public string citypair1 = "";
        public string citypair2 = "";
        /// <summary>
        /// 根据rt提PNR产生
        /// </summary>
        /// <param name="rTResult"></param>
        public void SetVarsByRt(string rtResult)
        {
            if (!EagleAPI.GetNoPnr(rtResult,false)) return;



            psgname = "";
            List<string> names = new List<string>();
            names = EagleAPI.GetNames(rtResult);
            for (int i = 0; i < names.Count; i++)
            {
                psgname += names[i]+",";
            }
            if (psgname.Length > 0) psgname = psgname.Substring(0, psgname.Length - 1);

            peaplecount = (names.Count >= EagleAPI.GetPeopleNumber(rtResult) ? names.Count : EagleAPI.GetPeopleNumber(rtResult));
            if (peaplecount <= 0) peaplecount = 1;

            sail = EagleAPI.GetFromTo(rtResult);

            pnr = EagleAPI.etstatic.Pnr;

            citypair1 = EagleAPI.GetStartCity(rtResult) + EagleAPI.GetEndCity(rtResult);
            citypair2 = (EagleAPI.GetEndCity2(rtResult) == "" ? "" : EagleAPI.GetEndCity(rtResult) + EagleAPI.GetEndCity2(rtResult));

            string tempdate = EagleAPI.GetDateStart(rtResult).Trim();
            if (tempdate != "")
            {
                if (tempdate[0] == '0') tempdate = tempdate.Substring(1);
                string tempday = tempdate.Substring(0, tempdate.Length - 3);
                string tempmonth = EagleAPI.GetMonthInt(mystring.right(tempdate, 3));
                string tempyear = System.DateTime.Now.Year.ToString();
                if ((int.Parse(tempmonth) - System.DateTime.Now.Month) > 6) tempyear = System.DateTime.Now.AddYears(-1).Year.ToString();
                date1 = tempyear + "-" + tempmonth + "-" + tempday;
            }
            else
            {
                date1 = "";
            }
            try
            {
                hour1 = EagleAPI.GetTimeStart(rtResult).Substring(0, 2);
                minute1 = EagleAPI.GetTimeStart(rtResult).Substring(2, 2);
            }
            catch 
            {
                hour1 = "";
                minute1 = "";
            }

            tempdate = EagleAPI.GetDateStart2(rtResult).Trim();
            if (tempdate != "")
            {
                if (tempdate[0] == '0') tempdate = tempdate.Substring(1);
                string tempday = tempdate.Substring(0, tempdate.Length - 3);
                string tempmonth = EagleAPI.GetMonthInt(mystring.right(tempdate, 3));
                string tempyear = System.DateTime.Now.Year.ToString();
                if ((int.Parse(tempmonth) - System.DateTime.Now.Month) > 6) tempyear = System.DateTime.Now.AddYears(-1).Year.ToString();
                date2 = tempyear + "-" + tempmonth + "-" + tempday;
            }
            else
            {
                date2 = "";
            }
            try
            {
                hour2 = EagleAPI.GetTimeStart2(rtResult).Substring(0, 2);
                minute2 = EagleAPI.GetTimeStart2(rtResult).Substring(2, 2);
            }
            catch
            {
                hour2 = "";
                minute2 = "";
            }

            carrier1 = EagleAPI.GetCarrier(rtResult);
            carrier2 = EagleAPI.GetCarrier2(rtResult);
            flightno1 = EagleAPI.GetFlight(rtResult);
            flightno2 = EagleAPI.GetFlight2(rtResult);
            bunk1 = EagleAPI.GetClass(rtResult);
            bunk2 = EagleAPI.GetClass2(rtResult);

            float fare1 = 0F;
            float fare2 = 0F;
            switch (bunk1)
            {
                case "F":
                    fare1 = 1600F;
                    break;
                case "C":
                    fare1 = 1400F;
                    break;
                case "Y":
                    fare1 = 1100F;
                    break;
                default:
                    fare1 = 600F;
                    break;
            }
            switch (bunk2)
            {
                case "F":
                    fare2 = 1600F;
                    break;
                case "C":
                    fare2 = 1400F;
                    break;
                case "Y":
                    fare2 = 1100F;
                    break;
                case "":
                    break;
                default:
                    fare2 = 600F;
                    break;
            }
            GlobalVar.f_limitMoneyPerTicket = fare1 + fare2;
        }
        /// <summary>
        /// 根据detr提取票号产生
        /// </summary>
        /// <param name="detrResult"></param>
        public void SetVarsByDetr(string detrResult)
        {
            peaplecount = 1;

            ePlus.eTicket.etInfomation ei = new ePlus.eTicket.etInfomation();
            ei.SetVar(detrResult);
            psgname = ei.PASSENGER;
            sail = ei.ORGDST;
            pnr = ei.SmallCode;

            string tempdate = EagleAPI.substring(ei.FROM, 18, 5);
            if (tempdate != "")
            {
                if (tempdate[0] == '0') tempdate = tempdate.Substring(1);
                string tempday = tempdate.Substring(0, tempdate.Length - 3);
                string tempmonth = EagleAPI.GetMonthInt(mystring.right(tempdate, 3));
                string tempyear = System.DateTime.Now.Year.ToString();
                if ((int.Parse(tempmonth) - System.DateTime.Now.Month) > 6) tempyear = System.DateTime.Now.AddYears(-1).Year.ToString();
                date1 = tempyear + "-" + tempmonth + "-" + tempday;
            }
            else
            {
                date1 = "";
            }
            try
            {
                hour1 = EagleAPI.substring(ei.FROM, 24, 4).Substring(0, 2);
                minute1 = EagleAPI.substring(ei.FROM, 24, 4).Substring(2, 2);
            }
            catch
            {
                hour1 = "";
                minute1 = "";
            }

            tempdate = EagleAPI.substring(ei.TO1, 18, 5);
            if (tempdate != "")
            {
                if (tempdate[0] == '0') tempdate = tempdate.Substring(1);
                string tempday = tempdate.Substring(0, tempdate.Length - 3);
                string tempmonth = EagleAPI.GetMonthInt(mystring.right(tempdate, 3));
                string tempyear = System.DateTime.Now.Year.ToString();
                if ((int.Parse(tempmonth) - System.DateTime.Now.Month) > 6) tempyear = System.DateTime.Now.AddYears(-1).Year.ToString();
                date2 = tempyear + "-" + tempmonth + "-" + tempday;
            }
            else
            {
                date2 = "";
            }
            try
            {
                hour2 = EagleAPI.substring(ei.TO1, 24, 4).Substring(0, 2);
                minute2 = EagleAPI.substring(ei.TO1, 24, 4).Substring(2, 2);
            }
            catch
            {
                hour2 = "";
                minute2 = "";
            }
        }
        /// <summary>
        /// 在行程单对话框中使用。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <param name="d1"></param>
        /// <param name="h1"></param>
        /// <param name="m1"></param>
        /// <param name="d2"></param>
        /// <param name="h2"></param>
        /// <param name="m2"></param>
        /// <param name="c1"></param>
        /// <param name="f1"></param>
        /// <param name="b1"></param>
        /// <param name="c2"></param>
        /// <param name="f2"></param>
        /// <param name="b2"></param>
        public void SetVarsByForm(string name, string s, string p, string d1, string h1, string m1, string d2, string h2, string m2, string c1, string f1, string b1, string c2, string f2, string b2)
        {
            peaplecount = name.Split(',').Length;

            psgname = name;
            sail = s;
            pnr = p;
            date1 = d1;
            hour1 = h1;
            minute1 = m1;
            carrier1 = c1;
            flightno1 = f1;
            bunk1 = b1;
            date2 = d2;
            hour2 = h2;
            minute2 = m2;
            carrier2 = c2;
            flightno2 = f2;
            bunk2 = b2;
        }
        /// <summary>
        /// 用于GlobalVar.newEticket.SetErp调用
        /// </summary>
        public void SetErp()
        {
            ErpPlugin.Class1.SetTicketRecordSheet(
                GlobalVar.newEticket.psgname,
                GlobalVar.newEticket.sail,
                GlobalVar.newEticket.pnr,
                GlobalVar.newEticket.date1,
                GlobalVar.newEticket.hour1,
                GlobalVar.newEticket.minute1,
                GlobalVar.newEticket.date2,
                GlobalVar.newEticket.hour2,
                GlobalVar.newEticket.minute2,
                GlobalVar.newEticket.carrier1,
                GlobalVar.newEticket.flightno1,
                GlobalVar.newEticket.bunk1,
                GlobalVar.newEticket.carrier2,
                GlobalVar.newEticket.flightno2,
                GlobalVar.newEticket.bunk2);
        }

        public string cnystring = "";
        public void decfee()
        {
            
            string cny = "";
            if (cnystring.Trim().Length >= 30) return;
            try
            {
                cny = cnystring.Substring(cnystring.IndexOf("CNY") + 3, cnystring.IndexOf(".00") - (cnystring.IndexOf("CNY") + 3)).Trim();
                float c = float.Parse(cny);
                string pnr = mystring.trim(cnystring);
                pnr = mystring.right(pnr, 5);
                if (EagleAPI.etstatic.Pnr.ToLower() != pnr.ToLower())//当返回结果中PNR与操作的PNR不同时
                {
                    throw new Exception("返回结果有误");
                    //EagleAPI.etstatic.Pnr = pnr;//分离式出票，可能产生不同PNR？
                }
            }
            catch(Exception e1)
            {
                cny = "0";
                EagleString.EagleFileIO.LogWrite("扣款解析票价出错！");
                EagleString.EagleFileIO.LogWrite(e1.Message);
                return;
            }
            EagleAPI.LogWrite("\r\n*****开始扣款！");
            eTicket.etDecFee df = new ePlus.eTicket.etDecFee();
            df.Pnr = EagleAPI.etstatic.Pnr.ToUpper();
            df.TotalFC = cny;
            GlobalVar.GlobalString = "扣款前余额为" + GlobalVar.f_CurMoney + "，当前扣款金额为" + cny + "\n>";

            EagleAPI.LogWrite(GlobalVar.GlobalString);
            for (int i = 0; i < 1; i++)
            {
                //if (!df.submitinfo())
                //{
                //    GlobalVar.GlobalString += "扣款失败，系统将会补扣！\r>";
                //    break;
                //}
                try
                {
                    df.submitinfo();
                    break;
                }
                catch(Exception ee)
                {
                    GlobalVar.GlobalString += "扣款失败，系统将会补扣！\r>";
                    GlobalVar.GlobalString += ee.Message + "\r>";
                }
            }
            EagleAPI.LogWrite("\r\n*****结束扣款！");
        }
        private void decfeeCheck()
        {
            EagleAPI.LogWrite("由于ETDZ未扣款，用IBE检查ETDZ是否成功");
            string pnr = EagleAPI.etstatic.Pnr.ToLower();
            Options.ibe.ibeInterface rt = new Options.ibe.ibeInterface();
            Options.ibe.IbeRt rtRes = new Options.ibe.IbeRt(rt.rt2(pnr));
            bool succ = false;
            string[] tktnos = rtRes.getpeopleinfo(2);
            if (tktnos[0] == "")//没有电子票号，出票失败，退出检测
            {
                EagleAPI.LogWrite("IBE检查完毕，当前PNR"+pnr+"出票失败");
                return;
            }
            //成功
            EagleAPI.LogWrite("IBE检查完毕，当前PNR" + pnr + "出票成功，开始进行扣款计算！");
            //人数
            int pCount = tktnos.Length;
            EagleAPI.LogWrite("人数："+pCount.ToString());
            //航段数
            int fCount = rtRes.getflightsegsinfo().Length;
            EagleAPI.LogWrite("航段数：" + fCount.ToString());
        }
    }
}
