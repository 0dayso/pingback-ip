using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Options.ibe
{
    
    public class ibeInterface
    {
        static public System.Globalization.DateTimeFormatInfo gbDtfi = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
        test.AuthenticationToken token = new test.AuthenticationToken();
        test.IbeService ibe = new test.IbeService();

        public ibeInterface()
        {
            token.Username = "user";
            token.Password = "shzylg";
            ibe.TokenValue = token;
        }

        private string av(string org, string dst, string dt, string al, bool d, bool e)
        {


            try
            {
                string ret = ibe.wsAV(org.ToUpper(), dst.ToUpper(), dt, al.ToUpper(), d, e);
                return ret;

                wsIBE.HiWSService ws = new wsIBE.HiWSService();
                return ws.wsAV(org.ToUpper(), dst.ToUpper(), dt, al.ToUpper(), d, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return "";
        }
        /// <summary>
        /// string stest = ib.av("peksha", "20070812 12:00:00", "ALL", false, true);
        /// </summary>
        /// <param name="fromto"></param>
        /// <param name="dt">��ʽ��YYYYMMDD HH:MI:SS</param>
        /// <param name="al"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public string av(string fromto, string dt, string al, bool d, bool e)
        {
            string ret = "";
            try
            {
                
                ret= av(fromto.Substring(0,3), fromto.Substring(3), dt, al, d, e);
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 0:fromto  1:date  2:airline 3:direct(1=true)  4:eticket(1=true)  5:avResult   6:avResultTemp
        /// </summary>
        /// <param name="paras"></param>
        public void av(object data)
        {
            string[] paras = (string[])data;
            if (paras.Length != 7) return;
            paras[5] = av(paras[0].Substring(0, 3),
                            paras[0].Substring(3),
                            paras[1],
                            paras[2],
                            paras[3] == "1" ? true : false,
                            paras[4] == "1" ? true : false);
            paras[6] = paras[5];
            avresult = paras[5];
        }
        public string avresult = "";



        private string fd(string org, string dst, string dt, string al, string planeModel, string passType,bool fullFareBasis)
        {
            try
            {
                return ibe.wsFD(
                    org.ToUpper(),
                    dst.ToUpper(),
                    dt.ToUpper(),
                    al.ToUpper(),
                    planeModel.ToUpper(),
                    passType.ToUpper(),
                    true);
                wsIBE.HiWSService ws = new wsIBE.HiWSService();
                return ws.wsFD(
                    org.ToUpper(),
                    dst.ToUpper(),
                    dt.ToUpper(),
                    al.ToUpper(),
                    planeModel.ToUpper(),
                    passType.ToUpper(),
                    true);
            }
            catch
            {
            }
            return "";
        }
        /// <summary>
        /// ���� fareF~fareC~fareY
        /// </summary>
        /// <param name="fromto"></param>
        /// <param name="date"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        public string fd(string fromto, string date, string al)
        {
            string ret = "";
            try
            {
                ret = fd(fromto.Substring(0, 3), fromto.Substring(3), date, al, "", "", true);
            }
            catch
            {
            }
            return ret;
        }
        public string fd2(string org,string dst,string dt,string al)
        {
            try
            {
                return ibe.wsFD2(
                    org.ToUpper(),
                    dst.ToUpper(),
                    dt.ToUpper(),
                    al.ToUpper(),
                    "",//planeModel.ToUpper(),
                    "",//passType.ToUpper(),
                    true);
                wsIBE.HiWSService ws = new wsIBE.HiWSService();
                return ws.wsFD2(
                    org.ToUpper(),
                    dst.ToUpper(),
                    dt.ToUpper(),
                    al.ToUpper(),
                    "",//planeModel.ToUpper(),
                    "",//passType.ToUpper(),
                    true);
            }
            catch
            {
            }
            return "";
        }
        /// <summary>
        /// 0:fromto  1:date(02AUG07)  2:airline 
        /// </summary>
        /// <param name="data"></param>
        public void fd(object data)
        {
            string[] paras = (string[])data;
            if (paras.Length != 3) return;
            fdresult = fd(paras[0].Substring(0, 3),
                            paras[0].Substring(3),
                            paras[1],
                            paras[2],
                            "",
                            "",
                            true
                            );

        }
        public string fdresult = "";

        public string rt(string pnr)
        {
            return ibe.wsRT(pnr);
            wsIBE.HiWSService hi = new wsIBE.HiWSService();
            return hi.wsRT(pnr);
        }
        public string rt(string pnr, bool bETC)
        {
            //if (bETC)
            //{
            //    IbeEagleClient.HiWSService hi = new IbeEagleClient.HiWSService();
            //    return hi.wsRT(pnr);
            //}
            //else
            {
                return rt(pnr);
            }
        }
        public string rt2(string pnr)
        {
            return ibe.wsRT2(pnr);
            wsIBE.HiWSService hi = new wsIBE.HiWSService();
            return hi.wsRT2(pnr);
        }
        public string rt2(string pnr,bool bETC)
        {
            //if (bETC)
            //{
            //    IbeEagleClient.HiWSService hi = new IbeEagleClient.HiWSService();
            //    return hi.wsRT2(pnr);
            //}
            //else
            {
                return rt2(pnr);
            }
        }
        public string xepnr(string pnr, string office)
        {
            return ibe.wsXePnr(pnr, office);
            wsIBE.HiWSService hi = new wsIBE.HiWSService();
            return hi.wsXePnr(pnr,office);

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="flightno">������</param>
        /// <param name="actioncode">�ж�������</param>
        /// <param name="flightdate">�˻�����:2007-09-01</param>
        /// <param name="orgcity">��ʼ����</param>
        /// <param name="destcity"></param>
        /// <param name="bunk"></param>
        /// <param name="name"></param>
        /// <param name="limitdate"></param>
        /// <param name="cardids"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public string ss(string[] flightno, string[] actioncode, string[] flightdate, string[] orgcity, string[] destcity, string[] bunk,string[] name, string limitdate, string[] cardids, string[] remarks)
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
                    fn += flightno[i] + (i == flightno.Length - 1 ? "" : "~");
                    ac += actioncode[i] + (i == flightno.Length - 1 ? "" : "~");
                    fd += flightdate[i] + (i == flightno.Length - 1 ? "" : "~");
                    oc += orgcity[i] + (i == flightno.Length - 1 ? "" : "~");
                    dc += destcity[i] + (i == flightno.Length - 1 ? "" : "~");
                    bk += bunk[i] + (i == flightno.Length - 1 ? "" : "~");
                }
                for (int i = 0; i < name.Length; i++) nm += name[i] + (i == name.Length - 1 ? "" : "~");
                if (cardids != null)
                    for (int i = 0; i < cardids.Length; i++)
                    {
                        if (cardids[i] == "") continue;
                        ci += cardids[i] + (i == cardids.Length - 1 ? "" : "~");
                    }
                if (remarks != null)
                    for (int i = 0; i < remarks.Length; i++) rmk += remarks[i] + (i == remarks.Length - 1 ? "" : "~");

                return ibe.wsSS(fn, ac, fd, oc, dc, bk, nm, limitdate, ci, rmk);
                wsIBE.HiWSService hi = new wsIBE.HiWSService();
                return hi.wsSS(fn, ac, fd, oc, dc, bk, nm, limitdate, ci, rmk);
            }
            catch
            {
                return "";
            }
        }

        public string dsg(string pnr)
        {
            return ibe.wsDSG(pnr);
            wsIBE.HiWSService hi = new wsIBE.HiWSService();
            return hi.wsDSG(pnr);
        }
    }

    public class IbeRt
    {
        string xml = "";
        public IbeRt(string rtxml)
        {
            xml = rtxml;
        }
        public string[] getpeopleinfo(int iVal)//0��ʾȡ����.1��ʾȡ���֤.2��ʾȡ����Ʊ��
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNode xn = xd.SelectSingleNode("ibe").SelectSingleNode("passenger");
            int count = xn.ChildNodes.Count;
            if (count == 0) return null;
            string[] ret = new string[count];
            for (int i = 0; i < count; i++)
            {
                XmlNode xmtmp = xn.ChildNodes[i];
                switch (iVal)
                {
                    case 0:
                        ret[i] = xmtmp.SelectSingleNode("name").InnerText;
                        break;
                    case 1:
                        ret[i] = xmtmp.SelectSingleNode("cardid").InnerText;
                        break;
                    case 2:
                        ret[i] = xmtmp.SelectSingleNode("tktno").InnerText;
                        break;
                    default:
                        ret[i] = "";
                        break;
                }
            }
            return ret;
        }
        public string[] getflightsegsinfo()
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNode xn = xd.SelectSingleNode("ibe").SelectSingleNode("segment");
            int count = xn.ChildNodes.Count;
            if (count == 0) return null;
            string[] ret = new string[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = "";
                XmlNode xmtmp = xn.ChildNodes[i];
                ret[i] += xmtmp.SelectSingleNode("flightno").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("bunk").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("fromto").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("flightdate").InnerText+"~";
                ret[i] += xmtmp.SelectSingleNode("timeLeave").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("timeArrive").InnerText;
#if !REALIBE
                ret[i] += "~";
                ret[i] += xmtmp.SelectSingleNode("planetype").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("tkPrc").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("fule").InnerText + "~";
                ret[i] += xmtmp.SelectSingleNode("baseFee").InnerText;
#endif
            }
            return ret;
        }
    }

    public class IbeBlack
    {
        string avreturn = "";
        string getEtermDate(DateTime dateTime)
        {
            return (dateTime.ToString("ddMMM", System.Globalization.DateTimeFormatInfo.InvariantInfo)).ToUpper();
        }
        string getIbeDate(string etermDate)
        {
            etermDate = etermDate.ToUpper();
            string day = etermDate.Substring(0,etermDate.Length-3);
            string monthcode = etermDate.Substring(etermDate.Length - 3);
            string[] monthList = System.Globalization.DateTimeFormatInfo.InvariantInfo.AbbreviatedMonthNames;
            int month = 0;
            for (int i = 0; i < monthList.Length; i++)
            {
                if (monthcode == monthList[i].ToUpper())
                {
                    month = i+1;
                }
            }
            int monthNow = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (month < monthNow && monthNow - month >= 6)
                year = DateTime.Now.AddYears(1).Year;
            return year.ToString() + month.ToString("d2") + int.Parse(day).ToString("d2");
        }
        string localcitycode()
        {
            string filename = System.Windows.Forms.Application.StartupPath + "\\options.XML";
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(temp);

            XmlNode xn = xd.SelectSingleNode("eg");
            xn = xn.SelectSingleNode("LocalCityCode");
            return xn.InnerText;
        }
        string[] getArray(string avcmd)
        {
            try
            {
                System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                if (avcmd.Trim().Substring(0, 2).ToUpper() != "AV") throw new Exception("��AVָ��");
                EagleString.AvCommand avc = new EagleString.AvCommand(localcitycode(), avcmd);
                List<string> ret = new List<string>();
                ret.Add(avc.City1 + avc.City2);
                ret.Add(avc.Date.ToString("ddMMM", dtFormat));
                ret.Add(avc.AirLine);
                ret.Add(avc.Direct ? "1" : "0");
                return ret.ToArray();
                
                string av = avcmd.Trim().ToUpper();
                av = av.Replace(".", getEtermDate(DateTime.Now));
                av = av.Replace("-", getEtermDate(DateTime.Now.AddDays(-1)));
                av = av.Replace("+", getEtermDate(DateTime.Now.AddDays(1)));
                av = av.Substring(2).Trim();//h wuhsha �� h/wuhsha
                if (av.Substring(0, 2) == "H " || av.Substring(0, 2) == "H/")
                    av = av.Substring(2).Trim();//wuhsha ��/wuhsha
                while (av[0] == '/') av = av.Substring(1);//wuhsha����

                string dateString = getEtermDate(DateTime.Now);
                for (int i = 0; i < av.Length; i++)
                {
                    if (!(av[i] > '9' || av[i] < '0'))
                    {
                        try
                        {
                            string fromto = (av.Substring(0, i)).Replace("/"," ").Trim();
                            if (fromto.Length < 6) fromto = localcitycode() + fromto;
                            ret.Add(fromto);
                            
                            if (av[i + 1] <= '9' && av[i + 1] >= '0')
                            {
                                dateString = av.Substring(i, 5);
                            }
                            else
                            {
                                dateString = av.Substring(i, 4);
                            }
                            
                        }
                        catch
                        {
                            //throw new Exception("�����ڻ����ڴ���!IBE���������������");
                            
                            dateString = DateTime.Now.ToString("ddMMM", dtFormat);
                        }
                        break;
                    }
                }
                ret.Add(dateString);
                if (ret.Count > 1)
                    av = av.Substring(av.IndexOf(ret[1]) + ret[1].Length);
                string al = "ALL";
                string d = "0";
                string[] args = av.Split('/');
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Trim().Length == 2) al = args[i].Trim();
                    if (args[i].Trim().Length == 1) d = "1";
                }
                ret.Add(al);
                ret.Add(d);
                return ret.ToArray();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// ����ib.av��ȡav����
        /// </summary>
        /// <param name="avcmd"></param>
        /// <returns></returns>
        string av(string avcmd)
        {
            try
            {
                string[] array = getArray(avcmd);
                ibeInterface ib = new ibeInterface();
                string time = getIbeDate(array[1]) + " 00:00:00";
                avreturn = ib.av(array[0], time, array[2], (array[3] == "1"), true);
                return pn(1);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// ����av������,ֱ�����ڴ��е��÷�ҳָ��
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        string pn(int n)
        {
            try
            {
                int p = n;
                string[] pages = avreturn.Split(new string[] { "\n##########\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (n > pages.Length - 1) p = pages.Length - 1;
                if (n < 1) p = 1;
                return pages[0] + pages[p];
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// ������Ϊ"",���ʾ��av����ط�ҳ����,�ɼ���ʹ�����÷���ָ��,�����жϺ���,���������ݼ�Ϊ���ؽ��,������������ָ����default����Ӽ���
        /// </summary>
        /// <param name="cmdstrings">ָ�:��i~av h sha+~pn</param>
        /// <returns></returns>
        public string avp(string cmdstrings)
        {
            string[] array = cmdstrings.Split('~');
            bool bOnlyAvOrPage = true;
            int pages = 1;
            string avcmd = "";
            bool bOtherCmdBetweenAvAndPage = false;
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i].ToLower())
                {
                    case "i":
                        if (i == array.Length - 1) return "NO PNR";//�������һ��ָ���򷵻�
                        break;
                    case "ig":
                        if (i == array.Length - 1) return "NO PNR";//�������һ��ָ���򷵻�
                        break;
                    case "pn":
                        pages++;
                        break;
                    case "pb":
                        pages--;
                        break;
                    case "pl":
                        pages = 99;
                        break;
                    case "pf":
                        pages = 1;
                        break;
                    default:
                        string head = array[i].Substring(0, 2);
                        if (head != "av") 
                        {
                            bOtherCmdBetweenAvAndPage = true;
                            bOnlyAvOrPage = false;
                            avreturn = "";
                        }
                        else 
                        {
                            bOtherCmdBetweenAvAndPage = false;
                            //if (i == array.Length - 1) avreturn = "";
                            avcmd = array[i];
                        }
                        break;
                }
            }
            if (bOtherCmdBetweenAvAndPage) avreturn = "";//av��page֮��������ָ��,��avreturn=""
            if (bOnlyAvOrPage)
            {
                if (avreturn != "")
                    return pn(pages);
                else
                    return av(avcmd);
            }
            else
                return "";
        }
        /// <summary>
        /// ����Ƿ������IBE�ӿ�����ָ��:cmdstrings��������ָ��
        /// </summary>
        /// <param name="cmdstrings"></param>
        /// <returns></returns>
        public bool avpCan(string cmdstrings)
        {
            string[] array = cmdstrings.Split('~');
            bool bOnlyAvOrPage = true;//�Ƿ����ʹ��,����ֵ
            int pages = 1;
            string avcmd = "";
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i].ToLower())
                {
                    case "i":
                        break;
                    case "ig":
                        break;
                    case "pn":
                        pages++;
                        break;
                    case "pb":
                        pages--;
                        break;
                    case "pl":
                        pages = 99;
                        break;
                    case "pf":
                        pages = 1;
                        break;
                    default:
                        string head = array[i].Substring(0, 2);
                        if (head != "av")
                        {
                            bOnlyAvOrPage = false;
                            avreturn = "";
                        }
                        else
                        {
                            if (i == array.Length - 1) avreturn = "";
                            avcmd = array[i];
                        }
                        break;
                }
            }
            return (bOnlyAvOrPage);

        }


        string ssreturn = "";
        public string ss(string sscmd)
        {
            string[] cmdary = sscmd.Split('~');
            bool bnm = false;
            bool bct = false;
            bool bss = false;
            bool btk = false;
            bool bfk = false;

            string[] fns = null;
            List<string> fnl = new List<string>();
            string[] acs = null;
            List<string> acl = new List<string>();
            string[] fds = null;
            List<string> fdl = new List<string>();
            string[] ocs = null;
            List<string> ocl = new List<string>();
            string[] dcs = null;
            List<string> dcl = new List<string>();
            string[] bks = null;
            List<string> bkl = new List<string>();
            string[] nms = null;
            string ld = "";
            string[] cis = null;
            List<string> cil = new List<string>();

            for (int i = 0; i < cmdary.Length; i++)
            {
                switch (cmdary[i].ToLower())
                {
                    case "i":
                        break;
                    case "ig":
                        break;
                    default:
                        string[] bookcmd = cmdary[i].Split('\r');
                        for (int j = 0; j < bookcmd.Length; j++)
                        {
                            string c = bookcmd[j].ToUpper();
                            if (c.Length >= 2 && c.Substring(0, 2) == "NM")
                            {
                                bnm = true;
                                nms = c.Substring(3).Split('~');
                            }
                            else if (c.Length >= 2 && (c.Substring(0, 2) == "CT" || c.Substring(0, 2) == "C:"))
                            {
                                bct = true;
                            }
                            else if (c.Length >= 3 && c.Substring(0, 3) == "SSR")
                            {
                                
                            }
                            else if (c.Length >= 3 && c.Substring(0, 3) == "SS:")
                            {
                                bss = true;
                                string[] seginfo = c.Substring(3).Split('/');
                                fnl.Add(seginfo[0]);
                                bkl.Add(seginfo[1]);
                                fdl.Add(DateTime.ParseExact
                                    (seginfo[2], "ddMMM", ibeInterface.gbDtfi).
                                    ToString("yyyy-MM-dd",ibeInterface.gbDtfi));
                                ocl.Add(seginfo[3].Substring(0, 3));
                                dcl.Add(seginfo[3].Substring(3));
                                acl.Add("LL");
                            }
                            else if (c.Length >= 4 && c.Substring(0, 4) == "TKTL")
                            {
                                btk = true;
                                string tkinfo = c.Substring(4);
                                System.Globalization.DateTimeFormatInfo myDTFI 
                                    = new System.Globalization.CultureInfo("en-us", false).DateTimeFormat;
                                tkinfo = tkinfo.Replace(".", DateTime.Now.ToString("ddMMM", myDTFI));
                                tkinfo = tkinfo.Replace("-", DateTime.Now.AddDays(-1).ToString("ddMMM", myDTFI));
                                tkinfo = tkinfo.Replace("+", DateTime.Now.AddDays(1).ToString("ddMMM", myDTFI));
                                string time = tkinfo.Split('/')[0];
                                string date = tkinfo.Split('/')[1];
                                while (time.Length < 4) time = "0" + time;
                                while (date.Length < 5) date = "0" + date;
                                DateTime dt = DateTime.ParseExact(time + date, "HHmmddMMM", myDTFI);
                                ld = dt.ToString();
                            }
                            else if (c.Length >=1 &&(c.Substring(0, 1) == "@" || c.Substring(0, 1) == "\\"))
                            {
                                bfk = true;
                            }
                            else
                            {
                                throw new Exception("��Ԥ��ָ��");
                            }

                        }
                        break;
                }
                
            }
            if (bct && bfk && btk && bss && bnm)
            {
                ibeInterface ib = new ibeInterface();
                string ret = ib.ss(fnl.ToArray(), acl.ToArray(), fdl.ToArray(), ocl.ToArray(), dcl.ToArray(), bkl.ToArray(), nms, ld, null, null);
                if (ret.Length != 5) throw new Exception("Ԥ����������");
                return ret;
            }
            throw new Exception("��Ԥ��ָ��");
        }

    }
}
