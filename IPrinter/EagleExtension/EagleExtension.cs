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
        /// <param name="avstring">Av指令的返回结果</param>
        /// <param name="cn">连接本地数据库的connection</param>
        /// <param name="shieldstring">要屏蔽的航空公司代码，如MU,CZ(用逗号隔开)</param>
        /// <param name="wsaddr">WebService地址</param>
        /// <param name="lv">ListView指向一个LV_Lowest对象</param>
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
        /// 将AvResult显示到ListView中(定义avresult,取价格,取政策,放到LV)
        /// </summary>
        /// <param name="avstring">AvResult查询结果</param>
        /// <param name="cn">打开的data.mdb连接,可不连接,但不能为null</param>
        /// <param name="shieldstring">要屏蔽的航空公司,如"MU,CZ"，逗号分隔</param>
        /// <param name="wsaddr">Web服务地址</param>
        /// <param name="lv">LV_Lowest的引用</param>
        /// <param name="username">用户名(以确定返点政策)</param>
        /// <param name="pricei">返回整型的Y舱价格</param>
        /// <param name="distancei">返回整型的距离(公里)</param>
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
                System.Threading.Thread.Sleep(100);//需要等待委托结束吗？
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
                System.Threading.Thread.Sleep(100);//需要等待委托结束吗？
                if(bpolicy)ProfitSet_LV_Lowest(lv, wsaddr, username, ar_new.FlightDate_DT.ToShortDateString(), ar_new.CityPair);

                pricei = price;
                distancei = dist;
            }
        }













        /// <summary>
        /// 将AvResult显示到ListView中,其它返点政策为现有:profit(与重载2的不同点是profit为政策串，即已经取到了政策)
        /// </summary>
        /// <param name="avstring"></param>
        /// <param name="cn"></param>
        /// <param name="shieldstring"></param>
        /// <param name="wsaddr"></param>
        /// <param name="profit">用取到的GetPromot</param>
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
        /// 将返点信息显示在LV_Lowest中
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
        /// 将散拼信息显示在ListView中
        /// </summary>
        /// <param name="groupstring">散拼信息XML串</param>
        /// <param name="lv">指向LV_GroupList的ListView</param>
        public static void GroupResultToListView_Group(string groupstring, ListView lv)
        {
            EagleString.GroupResult gr = new EagleString.GroupResult(groupstring);
            gr.ToListView(lv);
        }
        /// <summary>
        /// 将散拼信息显示在ListView中
        /// </summary>
        /// <param name="username">当前登陆用户帐号</param>
        /// <param name="fromto">城市对</param>
        /// <param name="dt">航班日期</param>
        /// <param name="userclass">用户级别A,B,C,D,E,F</param>
        /// <param name="wsaddr">WebService地址</param>
        /// <param name="lv">指向LV_GroupList的ListView</param>
        public static void GroupResultToListView_Group
            (string username, string fromto, DateTime dt, char userclass, string wsaddr, ListView lv)
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(wsaddr);
            string groupstring = kf.Group_List(username, fromto, dt, userclass);
            EagleExtension.GroupResultToListView_Group(groupstring, lv);
        }
        /// <summary>
        /// 将散拼信息显示在ListView中
        /// </summary>
        /// <param name="username">当前登陆用户帐号</param>
        /// <param name="userclass">用户级别A,B,C,D,E,F</param>
        /// <param name="wsaddr">WebService地址</param>
        /// <param name="avstring">AV指令返回的结果串</param>
        /// <param name="lv">指向LV_GroupList的ListView</param>
        public static void GroupResultToListView_Group(string username, char userclass, string wsaddr,string avstring, ListView lv)
        {
            EagleString.AvResult ar_new = new EagleString.AvResult(avstring, 0, 0);
            GroupResultToListView_Group(username, ar_new.CityPair, ar_new.FlightDate_DT, userclass,wsaddr, lv);
        }

        /// <summary>
        /// 将特殊舱位信息显示在ListView中
        /// </summary>
        /// <param name="avstring">Av查询结果</param>
        /// <param name="wsaddr">WebService地址</param>
        /// <param name="lv">被显示的ListView</param>
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
        /// 申请无座低价舱位
        /// </summary>
        /// <param name="wsaddr"></param>
        /// <param name="username">申请人帐号名</param>
        /// <param name="dataid">被申请数据在表中的id</param>
        /// <param name="date">航班日期</param>
        /// <param name="bunk">被申请舱位，未知舱位用'-'表示</param>
        /// <param name="count">人数</param>
        /// <param name="pnr">PNR,当bunk='-'时，为NOPNR</param>
        /// <param name="psgers">乘客姓名</param>
        /// <param name="cardnos">乘客证件号</param>
        /// <param name="phones">电话号码:可为phones</param>
        /// <param name="ret">表示是否成功提交申请</param>
        public static void SpecTickRequest(string wsaddr,string username,int dataid,DateTime date,char bunk,int count,string pnr,
            string [] psgers,string [] cardnos,string [] phones,ref bool ret,ref string [] passport)
        {
            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(wsaddr);
            kf.SpecialTicketRequest(username, dataid, date,bunk, count, pnr, psgers, cardnos, phones,ref ret,ref passport);
        }
        /// <summary>
        /// 产生PNR
        /// </summary>
        /// <param name="flightno">航班号</param>
        /// <param name="actioncode">"LL"</param>
        /// <param name="date">航班日期</param>
        /// <param name="city">起飞城市</param>
        /// <param name="city2">抵达城市</param>
        /// <param name="bunk">舱位</param>
        /// <param name="names">姓名</param>
        /// <param name="dtlimit">时限</param>
        /// <param name="cardnos">证件号</param>
        /// <param name="remarks">备注</param>
        /// <param name="pnr">返回PNR</param>
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
        /// 登录到B2C，并返回LOGIN_INFO_BTOC(登录B2C服务器的返回XML代理类)
        /// </summary>
        /// <param name="username">B2C用户名</param>
        /// <param name="password">B2C密码</param>
        /// <param name="li">B2C返回信息</param>
        /// <param name="site">B2C的后台地址</param>
        /// <param name="service">B2C的服务地址</param>
        public static void LoginB2c(string username, string password, ref EagleString.LoginResultBtoc.LOGIN_INFO_BTOC li,string site,string service)
        {
            EagleWebService.wsYzpbtoc ws = new EagleWebService.wsYzpbtoc(service);
            string loginxml = ws.UserLogin(username, password);
            li.username = username;
            li.password = password;
            
            li.FromXml(loginxml, site);
        }

        /// <summary>
        /// 通过WebService取余额
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
        /// 计算一个PNR的总价格含税(可能与实际价格不符，价格数据库必须有对应价格)
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
        /// 计算一个PNR的各种价格(各航段分计),注：不取返点政策
        /// </summary>
        /// <param name="total">总票价与etdz结果比较</param>
        /// <param name="sFare">单个成人票面价</param>
        /// <param name="sBuild">单个成人机建</param>
        /// <param name="sFuel">单个成人燃油</param>
        /// <param name="sFareY">Y舱的价格，为了儿童或婴儿的计算</param>
        public static void CalPnrsTotalPrice(RtResult rtres, string wsaddr, 
            ref int total,ref int[] sFare,ref int[] sBuild,ref int[] sFuel,ref int[] sFareY)
        {
            total = 0;
            EagleWebService.IbeFunc fc = new EagleWebService.IbeFunc();
            //Hashtable ht = fc.DSG(rtres.PNR);//有待IBE恢复
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
                //由于IBE不能使用，暂时用50代替
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
        /// 计算每个航段的价格及返点
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
            //所有航段之和，一个成人的价格，一个儿童的价格
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
        /// 当日志超过指定条数时，上传日志并清除本地日志
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
                //MessageBox.Show("LogUpload : 上传日志失败");
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
                if (dt.Rows.Count <= count) return;//大于count条时上传日志
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
                            EagleString.EagleFileIO.LogWrite("上传日志完成！");
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
