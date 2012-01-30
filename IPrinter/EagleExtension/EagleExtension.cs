using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Data.OleDb;
using EagleString;
using System.Data;
namespace EagleExtension
{
    public class EagleExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="avstring">Avָ��ķ��ؽ��</param>
        /// <param name="cn">���ӱ������ݿ��connection</param>
        /// <param name="shieldstring">Ҫ���εĺ��չ�˾���룬��MU,CZ(�ö��Ÿ���)</param>
        /// <param name="wsaddr">WebService��ַ</param>
        /// <param name="lv">ListViewָ��һ��LV_Lowest����</param>
        public static void AvResultToListView_Lowest(string avstring, System.Data.OleDb.OleDbConnection cn,
                                      string shieldstring, string wsaddr, ListView lv,string username)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);
            if (ar_new.SUCCEED)
            {
                int dist = EagleString.EagleFileIO.DistanceOf(ar_new.CityPair);
                int price = EagleString.EagleFileIO.PriceOf(ar_new.CityPair);
                if (price == 0)
                {
                    EagleWebService.kernalFunkEx.FC_get_server_local(ar_new.CityPair, cn, wsaddr, ref dist, ref price);
                }
                ar_new.Price = price;
                ar_new.Distance = dist;
                ar_new.ShieldAirline = shieldstring;
                ar_new.ToListView(lv, false, EagleString.TO_LIST_WAYS.LOWEST);

                ProfitSet_LV_Lowest(lv, wsaddr, username, ar_new.FlightDate_DT.ToShortDateString(), ar_new.CityPair);
            }
        }
        /// <summary>
        /// ��AvResult��ʾ��ListView��(����avresult,ȡ�۸�,ȡ����,�ŵ�LV)
        /// </summary>
        /// <param name="avstring">AvResult��ѯ���</param>
        /// <param name="cn">�򿪵�data.mdb����,�ɲ�����,������Ϊnull</param>
        /// <param name="shieldstring">Ҫ���εĺ��չ�˾,��"MU,CZ"�����ŷָ�</param>
        /// <param name="wsaddr">Web�����ַ</param>
        /// <param name="lv">LV_Lowest������</param>
        /// <param name="username">�û���(��ȷ����������)</param>
        /// <param name="pricei">�������͵�Y�ռ۸�</param>
        /// <param name="distancei">�������͵ľ���(����)</param>
        public static void AvResultToListView_Lowest(string avstring, System.Data.OleDb.OleDbConnection cn,
                              string shieldstring, string wsaddr, ListView lv, string username,ref int pricei,ref int distancei)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);
            if (ar_new.SUCCEED)
            {
                int dist = EagleString.EagleFileIO.DistanceOf(ar_new.CityPair);
                int price = EagleString.EagleFileIO.PriceOf(ar_new.CityPair);
                if (price == 0)
                {
                    EagleWebService.kernalFunkEx.FC_get_server_local(ar_new.CityPair, cn, wsaddr, ref dist, ref price);
                }
                
                ar_new.Price = price;
                ar_new.Distance = dist;

                ar_new.ShieldAirline = shieldstring;
                ar_new.ToListView(lv, false, EagleString.TO_LIST_WAYS.LOWEST);
                System.Threading.Thread.Sleep(100);//��Ҫ�ȴ�ί�н�����
                ProfitSet_LV_Lowest(lv, wsaddr, username, ar_new.FlightDate_DT.ToShortDateString(), ar_new.CityPair);

                pricei = price;
                distancei = dist;
            }
        }
        public static void AvResultToListView_Lowest(bool bpolicy,string avstring, System.Data.OleDb.OleDbConnection cn,
                      string shieldstring, string wsaddr, ListView lv, string username, ref int pricei, ref int distancei)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);
            if (ar_new.SUCCEED)
            {
                int dist = EagleString.EagleFileIO.DistanceOf(ar_new.CityPair);
                int price = EagleString.EagleFileIO.PriceOf(ar_new.CityPair);
                if (price == 0)
                {
                    EagleWebService.kernalFunkEx.FC_get_server_local(ar_new.CityPair, cn, wsaddr, ref dist, ref price);
                }
                
                ar_new.Price = price;
                ar_new.Distance = dist;

                ar_new.ShieldAirline = shieldstring;
                ar_new.ToListView(lv, false, EagleString.TO_LIST_WAYS.LOWEST);
                System.Threading.Thread.Sleep(100);//��Ҫ�ȴ�ί�н�����
                if(bpolicy)ProfitSet_LV_Lowest(lv, wsaddr, username, ar_new.FlightDate_DT.ToShortDateString(), ar_new.CityPair);

                pricei = price;
                distancei = dist;
            }
        }













        /// <summary>
        /// ��AvResult��ʾ��ListView��,������������Ϊ����:profit(������2�Ĳ�ͬ����profitΪ���ߴ������Ѿ�ȡ��������)
        /// </summary>
        /// <param name="avstring"></param>
        /// <param name="cn"></param>
        /// <param name="shieldstring"></param>
        /// <param name="wsaddr"></param>
        /// <param name="profit">��ȡ����GetPromot</param>
        /// <param name="lv"></param>
        /// <param name="pricei"></param>
        /// <param name="distancei"></param>
        public static void AvResultToListView_Lowest(string avstring, System.Data.OleDb.OleDbConnection cn,
                              string shieldstring, string wsaddr, string profit,ListView lv, ref int pricei, ref int distancei)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);
            if (ar_new.SUCCEED)
            {
                int dist = EagleString.EagleFileIO.DistanceOf(ar_new.CityPair);
                int price = EagleString.EagleFileIO.PriceOf(ar_new.CityPair);
                if (price == 0)
                {
                    EagleWebService.kernalFunkEx.FC_get_server_local(ar_new.CityPair, cn, wsaddr, ref dist, ref price);
                }
                ar_new.Price = price;
                ar_new.Distance = dist;

                ar_new.ShieldAirline = shieldstring;
                ar_new.ToListView(lv, false, EagleString.TO_LIST_WAYS.LOWEST);

                ProfitSet_LV_Lowest(lv, profit);

                pricei = price;
                distancei = dist;
            }
        }
        
        /// <summary>
        /// ��������Ϣ��ʾ��LV_Lowest��
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="wsaddr"></param>
        /// <param name="username"></param>
        /// <param name="dt"></param>
        /// <param name="cp"></param>
        private static void ProfitSet_LV_Lowest(ListView lv, string wsaddr,string username,string dt,string cp)
        {
            try
            {
                EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(wsaddr);
                string profitstring = "";
                string flightnos = "";
                for (int i = 0; i < lv.Items.Count; ++i)
                {
                    if (i != 0) flightnos += ",";
                    flightnos += lv.Items[i].SubItems[1].Text;
                }
                profitstring = fc.GetPolicies(username, flightnos, dt, cp);
                if (lv.InvokeRequired)
                {
                    if (dg_profit == null) dg_profit = new deleg4Profit(ProfitSet_LV_Lowest);
                    lv.Invoke(dg_profit, new object[] {lv,profitstring});
                }
                else
                {
                    ProfitSet_LV_Lowest(lv, profitstring);
                }
            }
            catch(Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("EagleExtension:ProfitSet_LV_Lowest1:" + ex.Message);
            }
        }
        delegate void deleg4Profit(ListView lv, string profitstring);
        static deleg4Profit dg_profit;
        private static void ProfitSet_LV_Lowest(ListView lv, string profitstring)
        {
            EagleString.ProfitResult pr = new EagleString.ProfitResult(profitstring);
            if (lv.InvokeRequired) EagleString.EagleFileIO.LogWrite("NEED INVOKE!");
            for (int i = 0; i < lv.Items.Count; ++i)
            {
                try
                {
                    string flight = lv.Items[i].SubItems[1].Text;
                    string seats = lv.Items[i].SubItems[4].Text;
                    if (seats == "") continue;
                    char bunk = seats[0];
                    string profit = pr.ProfitWithFlightAndBunk(flight, bunk);
                    lv.Items[i].SubItems[6].Text = profit;
                }
                catch(Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("EagleExtension.ProfitSet_LV_Lowest2:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// ��ɢƴ��Ϣ��ʾ��ListView��
        /// </summary>
        /// <param name="groupstring">ɢƴ��ϢXML��</param>
        /// <param name="lv">ָ��LV_GroupList��ListView</param>
        public static void GroupResultToListView_Group(string groupstring, ListView lv)
        {
            EagleString.GroupResult gr = new EagleString.GroupResult(groupstring);
            gr.ToListView(lv);
        }
        /// <summary>
        /// ��ɢƴ��Ϣ��ʾ��ListView��
        /// </summary>
        /// <param name="username">��ǰ��½�û��ʺ�</param>
        /// <param name="fromto">���ж�</param>
        /// <param name="dt">��������</param>
        /// <param name="userclass">�û�����A,B,C,D,E,F</param>
        /// <param name="wsaddr">WebService��ַ</param>
        /// <param name="lv">ָ��LV_GroupList��ListView</param>
        public static void GroupResultToListView_Group
            (string username, string fromto, DateTime dt, char userclass, string wsaddr, ListView lv)
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(wsaddr);
            string groupstring = kf.Group_List(username, fromto, dt, userclass);
            EagleExtension.GroupResultToListView_Group(groupstring, lv);
        }
        /// <summary>
        /// ��ɢƴ��Ϣ��ʾ��ListView��
        /// </summary>
        /// <param name="username">��ǰ��½�û��ʺ�</param>
        /// <param name="userclass">�û�����A,B,C,D,E,F</param>
        /// <param name="wsaddr">WebService��ַ</param>
        /// <param name="avstring">AVָ��صĽ����</param>
        /// <param name="lv">ָ��LV_GroupList��ListView</param>
        public static void GroupResultToListView_Group(string username, char userclass, string wsaddr,string avstring, ListView lv)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);
            GroupResultToListView_Group(username, ar_new.CityPair, ar_new.FlightDate_DT, userclass,wsaddr, lv);
        }

        /// <summary>
        /// �������λ��Ϣ��ʾ��ListView��
        /// </summary>
        /// <param name="avstring">Av��ѯ���</param>
        /// <param name="wsaddr">WebService��ַ</param>
        /// <param name="lv">����ʾ��ListView</param>
        public static void SpecTickResultToListView_Spec(string avstring, string wsaddr, ListView lv,ListView lv2,int price)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);

            if (!ar_new.SUCCEED) return;
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(wsaddr);
            string outxml = "";
            kf.SpecialTicketList(ar_new.CityPair, ar_new.FlightDate_DT, ref outxml);
            EagleString.SpecTicResult st = new EagleString.SpecTicResult(outxml);
            st.FlightDate = ar_new.FlightDate_DT;
            st.ToListView(lv,lv2,ar_new.Flights.ToArray(),ar_new.BunksLowest.ToArray(),price);
        }
        /// <summary>
        /// ���������ͼ۲�λ
        /// </summary>
        /// <param name="wsaddr"></param>
        /// <param name="username">�������ʺ���</param>
        /// <param name="dataid">�����������ڱ��е�id</param>
        /// <param name="date">��������</param>
        /// <param name="bunk">�������λ��δ֪��λ��'-'��ʾ</param>
        /// <param name="count">����</param>
        /// <param name="pnr">PNR,��bunk='-'ʱ��ΪNOPNR</param>
        /// <param name="psgers">�˿�����</param>
        /// <param name="cardnos">�˿�֤����</param>
        /// <param name="phones">�绰����:��Ϊphones</param>
        /// <param name="ret">��ʾ�Ƿ�ɹ��ύ����</param>
        public static void SpecTickRequest(string wsaddr,string username,int dataid,DateTime date,char bunk,int count,string pnr,
            string [] psgers,string [] cardnos,string [] phones,ref bool ret,ref string [] passport)
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(wsaddr);
            kf.SpecialTicketRequest(username, dataid, date,bunk, count, pnr, psgers, cardnos, phones,ref ret,ref passport);
        }
        /// <summary>
        /// ����PNR
        /// </summary>
        /// <param name="flightno">�����</param>
        /// <param name="actioncode">"LL"</param>
        /// <param name="date">��������</param>
        /// <param name="city">��ɳ���</param>
        /// <param name="city2">�ִ����</param>
        /// <param name="bunk">��λ</param>
        /// <param name="names">����</param>
        /// <param name="dtlimit">ʱ��</param>
        /// <param name="cardnos">֤����</param>
        /// <param name="remarks">��ע</param>
        /// <param name="pnr">����PNR</param>
        public static void CreatePnrFromIbe(string [] flightno,string [] actioncode,DateTime [] date,string[] city,string [] city2,char[] bunk,
            string []names,DateTime dtlimit,string[] cardnos,string []remarks,ref string pnr)
        {
            EagleWebService.IbeFunc fc = new EagleWebService.IbeFunc();
            string[] flightdate = new string[date.Length];
            string[] bunks = new string[date.Length];
            for (int i = 0; i < date.Length; ++i)
            {
                flightdate[i] = date[i].ToString("yyyy-MM-dd");
                bunks[i] = bunk[i].ToString();
            }
            logic.IBEAgent ibeagent = new logic.IBEAgent();


            string xml = EagleString.egString.CreateXmlOfEagleIbe(flightno, actioncode, date, city, city2, bunk, names, dtlimit, cardnos, remarks);
            try
            {
                pnr = fc.SS(flightno, actioncode, flightdate, city, city2, bunks, names, dtlimit.ToString("yyyy-MM-dd HH:mm:00"), cardnos, remarks);
            }
            catch
            {
                pnr = ibeagent.MakeEbtOrder(xml);
            }
        }
        public static void CreatePnrFromIbe(string[] flightno, DateTime[] date, string[] city, string[] city2, char[] bunk,
            string[] names, string[] cardnos, string[] remarks, ref string pnr)
        {
            string[] actioncode = new string[flightno.Length];
            for (int i = 0; i < actioncode.Length; ++i) actioncode[i] = "LL";
            TimeSpan ts = date[0] - DateTime.Now;
            ts = new TimeSpan(ts.Ticks / 2);
            DateTime dtlimit = DateTime.Now.Add(ts);
            CreatePnrFromIbe(flightno, actioncode, date, city, city2, bunk, names, dtlimit, cardnos, remarks, ref pnr);
        }
        public static void CreatePnrFromIbe(string[] flightno, DateTime[] date, string[] citypair, char[] bunk,
    string[] names, string[] cardnos, string[] remarks, ref string pnr)
        {
            string[] actioncode = new string[flightno.Length];
            for (int i = 0; i < actioncode.Length; ++i) actioncode[i] = "LL";
            TimeSpan ts = date[0] - DateTime.Now;
            ts = new TimeSpan(ts.Ticks / 2);
            DateTime dtlimit = DateTime.Now.Add(ts);
            
            EagleControls.Operators.LimitTime(date,ref dtlimit);
            string [] city = new string [citypair.Length];
            string [] city2 = new string[citypair.Length];
            for(int i=0;i<citypair.Length;++i)
            {
                if(citypair[i].Length==6)
                {
                    city[i] = citypair[i].Substring(0,3);
                    city2[i] = citypair[i].Substring(3);
                }
            }
            CreatePnrFromIbe(flightno, actioncode, date, city, city2, bunk, names, dtlimit, cardnos, remarks, ref pnr);
        }
        /// <summary>
        /// ��¼��B2C��������LOGIN_INFO_BTOC(��¼B2C�������ķ���XML������)
        /// </summary>
        /// <param name="username">B2C�û���</param>
        /// <param name="password">B2C����</param>
        /// <param name="li">B2C������Ϣ</param>
        /// <param name="site">B2C�ĺ�̨��ַ</param>
        /// <param name="service">B2C�ķ����ַ</param>
        public static void LoginB2c(string username, string password, ref EagleString.LoginResultBtoc.LOGIN_INFO_BTOC li,string site,string service)
        {
            EagleWebService.wsYzpbtoc ws = new EagleWebService.wsYzpbtoc(service);
            string loginxml = ws.UserLogin(username, password);
            li.username = username;
            li.password = password;
            
            li.FromXml(loginxml, site);
        }

        /// <summary>
        /// ͨ��WebServiceȡ���
        /// </summary>
        public static float BALANCE(string username, string wsaddr)
        {
            string ret = "";
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(wsaddr);
            bool flag = false;
            kf.GetCloseBalance(username, ref flag, ref ret);
            return float.Parse(ret);
        }
        /// <summary>
        /// ����һ��PNR���ܼ۸�˰(������ʵ�ʼ۸񲻷����۸����ݿ�����ж�Ӧ�۸�)
        /// </summary>
        public static void CalPnrsTotalPrice(RtResult rtres,string wsaddr,ref int total)
        {
            int count = rtres.SEGMENG.Length;
            int []sFare = new int [count];
            int []sBuild = new int [count];
            int []sFuel = new int[count];
            int[] sFareY = new int[count];
            CalPnrsTotalPrice(rtres, wsaddr, ref total, ref sFare, ref sBuild, ref sFuel,ref sFareY);
        }
        /// <summary>
        /// ����һ��PNR�ĸ��ּ۸�(�����ηּ�),ע����ȡ��������
        /// </summary>
        /// <param name="total">��Ʊ����etdz����Ƚ�</param>
        /// <param name="sFare">��������Ʊ���</param>
        /// <param name="sBuild">�������˻���</param>
        /// <param name="sFuel">��������ȼ��</param>
        /// <param name="sFareY">Y�յļ۸�Ϊ�˶�ͯ��Ӥ���ļ���</param>
        public static void CalPnrsTotalPrice(RtResult rtres, string wsaddr, 
            ref int total,ref int[] sFare,ref int[] sBuild,ref int[] sFuel,ref int[] sFareY)
        {
            total = 0;
            EagleWebService.IbeFunc fc = new EagleWebService.IbeFunc();
            //Hashtable ht = fc.DSG(rtres.PNR);//�д�IBE�ָ�
            for (int i = 0; i < rtres.SEGMENG.Length; ++i)
            {
                string cp = rtres.SEGMENG[i].Citypair;
                OleDbConnection cn = new OleDbConnection();
                int price = EagleString.EagleFileIO.PriceOf(cp);
                int distance = EagleString.EagleFileIO.DistanceOf(cp);
                if (price == 0)
                {
                    EagleWebService.kernalFunkEx.FC_get_server_local(cp, cn, wsaddr, ref distance, ref price);
                }
                int pricec = EagleString.egString.TicketPrice(price, 50);
                int rebate = EagleString.EagleFileIO.RebateOf(rtres.SEGMENG[i].Bunk, rtres.SEGMENG[i].Flight);
                if(EagleString.egString.LargeThan420(rtres.SEGMENG[i].Date))
                    rebate = EagleString.EagleFileIO.RebateOfNew(rtres.SEGMENG[i].Bunk, rtres.SEGMENG[i].Flight);
                int pricea = EagleString.egString.TicketPrice(price, rebate);
                string flight = rtres.SEGMENG[i].Flight;
                if (flight[0] == '*') flight = flight.Substring(1);
                //����IBE����ʹ�ã���ʱ��50����
                int taxbuild = 50;
                //int taxbuild = EagleString.EagleFileIO.TaxOfBuildBy(ht[flight].ToString());
                int taxfuel = EagleString.EagleFileIO.TaxOfFuelBy(distance);
                pricec += EagleString.egString.TicketPrice(taxfuel, 50);//(taxfuel) / 2; 
                sFare[i] = pricea;
                pricea += taxfuel + taxbuild;
                total += pricec * rtres.CHILDRENCOUNT + pricea * rtres.ADULTCOUNT;
                
                sBuild[i] = taxbuild;
                sFuel[i] = taxfuel;
                sFareY[i] = price;
            }
        }
        /// <summary>
        /// ����ÿ�����εļ۸񼰷���
        /// </summary>
        public static void CalPnrsPrices(RtResult rtres, LoginInfo li,
            ref int []sFare, 
            ref int []sBuild, 
            ref int []sFuel, 
            ref double[] sGain, 
            ref int [] sFareY,
            ref int totalEtdz)
        {
            int total =0;
            CalPnrsTotalPrice(rtres, li.b2b.webservice, ref totalEtdz, ref sFare, ref sBuild, ref sFuel, ref sFareY);
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(li.b2b.webservice);
            EagleString.ProfitResult profitResult;
            for (int i = 0; i < rtres.SEGMENG.Length; ++i)
            {
                profitResult = new ProfitResult(
                                        kf.GetPolicies(
                                            li.b2b.username, 
                                            rtres.FLIGHTS[i], 
                                            rtres.FLIGHTDATES[i].ToShortDateString(), 
                                            rtres.CITYPAIRS[i]
                                                      ));
                string profit = profitResult.ProfitWithFlightAndBunk(rtres.FLIGHTS[i], rtres.BUNKS[i]);
                try
                {
                    sGain[i] = double.Parse(profit.Replace("%", ""));
                }
                catch
                {
                    sGain[i] = 0;
                }
            }
        }
        public static void CalPnrSomePrice(RtResult rtres, LoginInfo li,
            ref int fare,ref double real,ref int build,ref int fuel,ref double gainAver,ref double liren,ref double total)
        {
            int len = rtres.SEGMENG.Length;
            int[] sFare = new int[len];
            int[] sBuild = new int[len];
            int[] sFuel = new int[len];
            double[] sGain = new double[len];
            int[] sFareY = new int[len];
            int totalEtdz = 0;
            CalPnrsPrices(rtres, li, ref sFare, ref sBuild, ref sFuel, ref sGain, ref sFareY,ref totalEtdz);
            fare = 0;
            real = 0.0;
            build = 0;
            fuel = 0;
            liren = 0.0;
            total = 0.0;
            gainAver = 0.0;
            int cfare = 0;
            double creal = 0.0;
            int cbuild = 0;
            int cfuel = 0;
            //���к���֮�ͣ�һ�����˵ļ۸�һ����ͯ�ļ۸�
            for (int i = 0; i < len; ++i)
            {
                fare += sFare[i];
                real += (double)sFare[i] * (100F - sGain[i]) / 100F;
                build += sBuild[i];
                fuel += sFuel[i];

                int temp = EagleString.egString.TicketPrice(sFareY[i], 50);
                cfare += temp;
                creal += (double)temp * (100F - sGain[i]) / 100F;
                cbuild += 0;
                cfuel += EagleString.egString.TicketPrice(sFuel[i], 50);
            }
            liren = (double)fare - real;
            total = (real + (double)(build + fuel)) * rtres.ADULTCOUNT + 
                (creal + (double)(cbuild + cfuel)) * rtres.CHILDRENCOUNT;
            gainAver = ((double)totalEtdz - total) / (double)totalEtdz;
        }
        /// <summary>
        /// ����־����ָ������ʱ���ϴ���־�����������־
        /// </summary>
        static public void LogUpload(EagleString.LoginInfo li,int count)
        {
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\EagleLog.mdb;";
            OleDbConnection cn = new OleDbConnection();
            try
            {
                cn.ConnectionString = ConnStr;
                cn.Open();
            }
            catch
            {
                //MessageBox.Show("LogUpload : �ϴ���־ʧ��");
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand("select * from LocalLog", cn);
                DataTable dt = new DataTable();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                if (dt != null)
                {
                    adapter.Fill(dt);
                }
                if (dt.Rows.Count <= count) return;//����count��ʱ�ϴ���־
                string sendstr = "";
                string recvstr = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sendstr += "\r\n[" + i.ToString("d3") + "]" + dt.Rows[i]["STRINGSEND"];
                        recvstr += "\r\n[" + i.ToString("d3") + "]" + dt.Rows[i]["STRINGRECV"];
                    }

                    if (sendstr != "" || recvstr != "")
                    {
                        sendstr = sendstr.Substring(2);
                        recvstr = recvstr.Substring(2);
                        EagleWebService.kernalFunc fc = new EagleWebService.kernalFunc(li.b2b.webservice);
                        bool bflag = false;
                        fc.WriteLogToServer(li.b2b.username, sendstr, "", ref bflag);
                        if (bflag)
                        {
                            cmd.CommandText = "delete * from LocalLog";
                            cmd.ExecuteNonQuery();
                            EagleString.EagleFileIO.LogWrite("�ϴ���־��ɣ�");
                        }
                    }

                }
                cn.Close();
            }
            catch
            {
            }
        }
    }
}
