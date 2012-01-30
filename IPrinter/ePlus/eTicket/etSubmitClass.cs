using System;
using System.Collections.Generic;
using System.Text;

namespace ePlus.eTicket
{
    class etSubmitClass
    {
        public static string etRtResult = "";

        public static bool running = false;

        public static bool bPating = false;

        public static string pnr = "";
        string getpnr()
        {
            ePlus.eTicket.etGetUncheckedPnr etgp = new ePlus.eTicket.etGetUncheckedPnr();
            string ret = "";
            ret = etgp.getpnr();
            if (ret.Length < 5) throw new Exception("没有要核对的ＰＮＲ！");
            if(EagleAPI2.CheckSubmitedPnrInHashTable(ret))throw new Exception("重复，稍候提交……");
            return ret;
        }
        public void run()
        {
            
            if (GlobalVar.bUsingConfigLonely) throw new Exception("配置独占中……");
            if (running) throw new Exception("线程运行中……");
            try
            {
                running = true;
                pnr = getpnr();
                /*
                etRtResult = "";
                EagleAPI.EagleSendOneCmd("i~rt:n/" + pnr,7);*/
                //使用ibe接口
#if REALIBE
                Options.ibe.ibeInterface ib = new Options.ibe.ibeInterface();
                string xml = ib.rt2(pnr,GlobalVar.serverAddr== GlobalVar.ServerAddr.HangYiWang);
#else
                //小魏ibe接口
                logic.IBEAgent ibeagent = new logic.IBEAgent();
                string xml = ibeagent.wsRT2(pnr.ToUpper());
#endif
                if (xml == "")
                {
                    et.pnr = pnr;
                    et.submitinfoCancelledPnr("IBE未取到PNR信息,可能已经取消或没有这个PNR", "NO PNR OR CANCELLED");
                    etRtResult = "";
                    bPating = false;
                    running = false;
                    return;

                }
                et.init();
                Options.ibe.IbeRt ir = new Options.ibe.IbeRt(xml);
                et.pnr = pnr;
                string[] tktnos = ir.getpeopleinfo(2);
                for (int i = 0; i < tktnos.Length; i++)
                {
                    et.etnumbers += tktnos[i] + (i == tktnos.Length - 1 ? "" : ";");
                    et.passengers += ir.getpeopleinfo(0)[i] + "-" + ir.getpeopleinfo(1)[i] + (i == tktnos.Length - 1 ? "" : ";");
                }
                string[] fi = ir.getflightsegsinfo();
                for (int i = 0; i < fi.Length; i++)
                {
                    string[] segment = fi[i].Split('~');
                    if (i == 0)
                    {
                        et.fn1 = segment[0];
                        et.bunk1 = segment[1];
                        et.cp1 = segment[2];
                        System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                        try
                        {
                            
                            DateTime dt = DateTime.ParseExact(segment[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                            et.date1 = dt.ToString("ddMMM", myDTFI).ToUpper();
                        }
                        catch
                        {
                            et.date1 = DateTime.Parse(segment[3]).ToString("ddMMM", myDTFI);
                        }
                    }
                    if (i == 1)
                    {
                        et.fn2 = segment[0];
                        et.bunk2 = segment[1];
                        et.cp2 = segment[2];
                        System.Globalization.DateTimeFormatInfo myDTFI = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                        try
                        {
                            DateTime dt = DateTime.ParseExact(segment[3].Replace(" CST ", " "), "ddd MMM dd HH:mm:ss yyyy", myDTFI);
                            et.date2 = dt.ToString("ddMMM", myDTFI).ToUpper();
                        }
                        catch
                        {
                            et.date1 = DateTime.Parse(segment[3]).ToString("ddMMM", myDTFI);
                        }
                    }
                }
                bPating = true;
                etRtResult = "";
                //EagleAPI.EagleSendOneCmd(string.Format("i~rt{0}~pat:", pnr), 7);//不发送pat,改为直接提交  2009-5-11 by wfb
                et.pnr = pnr;
                et.submitinfo();
                etRtResult = "";
                bPating = false;
                running = false;
            }
            catch(Exception ex)
            {
                
                etRtResult = "";
                running = false;
                throw new Exception(ex.Message +": 终止线程！");
            }
        }
        public static etinfo et = new etinfo();
        public static string retstring
        {
            set
            {
                etRtResult += connect_4_Command.AV_String;
                string temp = mystring.trim(connect_4_Command.AV_String);
                if (temp.Length>1 && temp[temp.Length - 1] == '+' && temp.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*")<0)
                    EagleAPI.EagleSendCmd("pn",7);
                else if (temp.IndexOf("*THIS PNR WAS ENTIRELY CANCELLED*") >= 0 )
                {//非电子客票直接取消行 
                    et.pnr = pnr;
                    et.submitinfoCancelledPnr(temp.Replace("\r", " ").Replace("\n", " "), "PNRCANCELLED");
                    etRtResult = "";
                    bPating = false;
                    running = false;
                }
                else if (temp.Length < 20 && temp.IndexOf("NO PNR") >= 0)
                {//NO PNR
                    et.pnr = pnr;
                    et.submitinfoCancelledPnr(temp.Replace("\r", " ").Replace("\n", " "),"NO PNR");
                    etRtResult = "";
                    bPating = false;
                    running = false;
                }
                else
                {
                    //这里提分析并看a.分析提交b.查看价格c.etRtResult置空
                    if (bPating)
                    {
                        EagleAPI.GetFareFromPat(etRtResult, ref temp, ref et.tb, ref et.tf, ref et.totalfc);
                        //提交
                        et.pnr = pnr;
                        et.submitinfo();
                        etRtResult = "";
                        bPating = false;
                        running = false;
                    }
                    else
                    {
                        et.set(pnr, "\n" + etRtResult);
                        if (et.hasFare()) ;
                        {
                            //有价格，提交
                            et.pnr = pnr;
                            bPating = false;
                            et.submitinfo();
                            etRtResult = "";
                            running = false;
                        }
                        //else
                        //{
                        //    //无价格，发送pat:
                        //    bPating = true;
                        //    etRtResult = "";
                        //    EagleAPI.EagleSendOneCmd(string.Format("i~rt{0}~pat:", pnr), 7);

                        //}
                    }

                }
            }
        }


    }
    class etrun
    {
        static public etSubmitClass es = new etSubmitClass();
        static public bool running = false;
        public static void runthread()
        {
            if (running) return;
            try
            {
                while (true)
                {
                    //GlobalVar.stdRichTB.AppendText("#################################################\r");
                    try
                    {
                        es.run();
                        System.Threading.Thread.Sleep(5000);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "线程运行中……") throw new Exception(ex.Message);
                        else System.Threading.Thread.Sleep(5000);
                    }
                    //GlobalVar.stdRichTB.AppendText("#################################################\r");
                }
            }
            catch(Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                //etSubmitClass.running = false;
            }
            running = false;
        }
    }
    class etinfo
    {
        public string pnr;
        public string etnumbers;
        public string fn1;
        public string bunk1;
        public string cp1;
        public string date1;
        public string fn2;
        public string bunk2;
        public string cp2;
        public string date2;
        public string totalfc;
        public string state;
        public string decfeestate;
        public string passengers;//姓名-身份证;[姓名-身份证……]
        public string tb;
        public string tf;
        public bool isEticket = true;
        public etinfo()
        {
            init();
        }
        public void init()
        {
            pnr = "";
            etnumbers = "";
            fn1 = "";
            bunk1 = "";
            cp1 = "";
            date1 = "";
            fn2 = "";
            bunk2 = "";
            cp2 = "";
            date2 = "";
            totalfc = "0";
            state = "0";
            decfeestate = "0";
            passengers = "";//姓名-身份证;[姓名-身份证……]
            tb = "0";
            tf = "0";
        }
        public bool set(string p,string s)
        {
            try
            {
                isEticket = (s.IndexOf("**ELECTRONIC TICKET PNR**") >= 0);
                pnr = p;
                string[] tArray = EagleAPI.GetETNumber(s);
                if (tArray.Length < 1) throw new Exception("未取到票号！");
                etnumbers = "";
                foreach (string t in tArray)
                    etnumbers += t + ";";
                while (etnumbers.IndexOf(";;") >= 0)
                    etnumbers = etnumbers.Replace(";;", ";");
                etnumbers = mystring.trim(etnumbers, ';');
                List<string> names = EagleAPI.GetNames(s);
                string[] cardids = EagleAPI.GetIDCardNo(s);
                passengers = "";
                for (int i = 0; i < names.Count; i++)
                {
                    passengers += ";" + names[i] + "-" + cardids[i];
                }
                passengers = mystring.trim(passengers, ';');

                //float count = (float)etnumbers.Split(':').Length;
                float count = 1F;
                try
                {
                    totalfc = string.Format("{0}", float.Parse(EagleAPI.GetTatol(s).Substring(3)) * count);
                }
                catch
                { totalfc = "0"; }
                try
                {
                    tf = string.Format("{0}", float.Parse(EagleAPI.GetTaxFuel(s).Substring(3)) * count);
                }
                catch { tf = "0"; }
                try
                {
                    tb = string.Format("{0}", float.Parse(EagleAPI.GetTaxBuild(s).Substring(EagleAPI.GetTaxBuild(s)[3] > '9' ? 0 : 3)) * count);
                }
                catch
                { tb = "0"; }
                

                fn1 = EagleAPI.GetCarrier(s) + EagleAPI.GetFlight(s);
                fn2 = EagleAPI.GetCarrier2(s) + EagleAPI.GetFlight2(s);
                bunk1 = EagleAPI.GetClass(s);
                bunk2 = EagleAPI.GetClass2(s);
                date1 = EagleAPI.GetDateStart(s);
                date2 = EagleAPI.GetDateStart2(s);
                cp1 = EagleAPI.GetStartCity(s) + EagleAPI.GetEndCity(s);
                cp2 = EagleAPI.GetEndCity(s) + EagleAPI.GetEndCity2(s);
                if (cp2.Length < 6 || cp2 == cp1) cp2 = "";
            }
            catch (Exception ex)
            {
                EagleAPI.LogWrite(ex.Message);
                return false;
            }
            return true;
        }
        public bool hasFare()
        {
            try
            {
                if (float.Parse(totalfc) > 0) return true;
            }
            catch
            {
            }
            return false;
        }
        public void submitinfo()
        {
            if (!isEticket)
            {
                ePlus.eTicket.etCreateFailed etcf = new ePlus.eTicket.etCreateFailed();
                etcf.Pnr = pnr;
                if (etcf.submitinfo()) EagleAPI.LogWrite("取消电子客票行！");
                return;
            }
            try
            {
                etStatic es = new etStatic();
                es.Bunk1 = bunk1;
                es.Bunk2 = bunk2;
                es.CityPair1 = cp1;
                es.CityPair2 = cp2;
                es.Date1 = date1;
                es.Date2 = date2;
                es.DecFeeState = "";
                es.etNumber = (etnumbers.Trim() == "" ? " si" : etnumbers.Trim());
                es.FlightNumber1 = fn1;
                es.FlightNumber2 = fn2;
                es.Passengers = passengers;
                es.Pnr = pnr;
                es.State = "0";
                try
                {
                    es.TotalFC = string.Format("{0}", float.Parse(totalfc) * etnumbers.Split(';').Length);
                }
                catch
                {
                    es.TotalFC = "0";
                }
                try
                {
                    if (tb.IndexOf("CNY") >= 0)
                        es.TotalTaxBuild = string.Format("{0}", float.Parse(tb.Substring(3)) * etnumbers.Split(';').Length);
                    else
                        es.TotalTaxBuild = string.Format("{0}", float.Parse(tb.Substring(0)) * etnumbers.Split(';').Length);
                }
                catch
                {
                    es.TotalTaxBuild = "0";
                }
                try
                {
                    if (tf.IndexOf("CNY") >= 0)
                        es.TotalTaxFuel = string.Format("{0}", float.Parse(tf.Substring(3)) * etnumbers.Split(';').Length);
                    else
                        es.TotalTaxFuel = string.Format("{0}", float.Parse(tf.Substring(0)) * etnumbers.Split(';').Length);
                }
                catch
                {
                    es.TotalTaxFuel = "0";
                }
                if (es.SubmitInfo()) EagleAPI.LogWrite(pnr + "提交成功！");
                return;
            }
            catch
            {
            }
            EagleAPI.LogWrite(pnr + "提交失败！");
        }
        public void submitinfoCancelledPnr(string content,string fakeEtno)
        {
            try
            {
                string NULL = "NULL";
                etStatic es = new etStatic();
                es.Bunk1 = bunk1;
                es.Bunk2 = bunk2;
                es.CityPair1 = cp1;
                es.CityPair2 = cp2;
                es.Date1 = date1;
                es.Date2 = date2;
                es.DecFeeState = "";
                es.etNumber = etnumbers;
                if (etnumbers.Trim() == "")
                    es.etNumber = "No TktNum When Submit";
                es.FlightNumber1 = fn1;
                es.FlightNumber2 = fn2;
                es.Passengers = passengers;
                es.Pnr = pnr;
                es.State = "0";
                es.TotalFC = "0";// string.Format("{0}", float.Parse(totalfc) * etnumbers.Split(';').Length);
                //if (tb.IndexOf("CNY") >= 0)
                es.TotalTaxBuild = "0";// = string.Format("{0}", float.Parse(tb.Substring(3)) * etnumbers.Split(';').Length);
                //else
                es.TotalTaxBuild = "0";// string.Format("{0}", float.Parse(tb.Substring(0)) * etnumbers.Split(';').Length);
                //if (tf.IndexOf("CNY") >= 0)
                    es.TotalTaxFuel = "0";// string.Format("{0}", float.Parse(tf.Substring(3)) * etnumbers.Split(';').Length);
                //else
                    es.TotalTaxFuel = "0";// string.Format("{0}", float.Parse(tf.Substring(0)) * etnumbers.Split(';').Length);
                if (es.SubmitInfo()) EagleAPI.LogWrite(pnr + "PNRCANCELLED提交成功！" + content +"\r\n\r\n");
                return;
            }
            catch
            {
            }
            EagleAPI.LogWrite(pnr + "PNRCANCELLED提交失败！" + content + "\r\n\r\n");
        }
    }

}
