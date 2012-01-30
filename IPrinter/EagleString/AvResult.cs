
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
namespace EagleString
{

    public class AvResult
    {

        public AvResult(string avres):this(avres,0,0)
        {
        }
        public AvResult(string avres, int price, int distance)
        {
            avres = egString.trim(avres);
            avres = egString.trim(avres, '>');
            if (avres.IndexOf("*\n") > 0) AvResult2(avres, price, distance, false);
            else
            {
                AvResult1(avres, price, distance);
            }
            //if (!SUCCEED) AvResult2(avres, price, distance, false);
            if (m_price == 0)
            {
                m_price = EagleFileIO.PriceOf(m_fromto);
                m_distance = EagleFileIO.DistanceOf(m_fromto);
            }
        }
        /// <summary>
        /// av H��
        /// </summary>
        public void AvResult1(string avres, int price, int distance)
        {
            try
            {
                m_price = price;
                m_distance = distance;

                IsAvReturnString = true;
                string[] sp = new string[16];//�ָ��Ϊ"\ni"
                for (int i = 0; i < 8; ++i)
                {
                    int itemp = i + 1;
                    sp[i] = "\n" + itemp.ToString();
                    sp[i + 8] = "\r\n" + itemp.ToString();
                }

                //Modify by wangfb 20060630
                //CZ  FARE    HGHPEK/PEKHGH   YI:CZ/TZ304
                //AIRPORT CHARGE FOR LATERAL FLIGHT
                string[] lastlines = avres.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (lastlines[lastlines.Length - 1].IndexOf("FARE") >= 0
                    || lastlines[lastlines.Length - 1].IndexOf("AIRPORT") >= 0)
                    avres = avres.Replace(lastlines[lastlines.Length - 1], "");



                string[] avSeg = avres.Split(sp, StringSplitOptions.RemoveEmptyEntries);

                nSegs = avSeg.Length - 1;
                //"27JUL(THU) WUHHKG\n"
                string str_date = avSeg[0].Trim().Split(' ')[0];
                int lPos = str_date.IndexOf('(');
                if (lPos >= 0) str_date = str_date.Substring(0, lPos);//27JUL or 27JUL08
                m_date = BaseFunc.str2datetime(str_date, true);
                m_fromto = avSeg[0].Trim().Split(' ')[1];                                                                      //------2

                si = new SegmentInfomation[nSegs];


                for (int i = 0; i < nSegs; ++i)
                {
                    si[i] = new SegmentInfomation();
                    si[i].context = avSeg[i + 1];
                    string[] siSplit = si[i].context.Split(new string[1] { "\n " }, StringSplitOptions.RemoveEmptyEntries);
                    si[i].b_directly = (siSplit.Length == 1);
                    si[i].fi = new FlightInformation[siSplit.Length];
                    for (int j = 0; j < siSplit.Length; j++)
                    {
                        int id = i + 1;
                        si[i].fi[j] = new FlightInformation(siSplit[j], id, m_price);
                        si[i].fi[j].a_rebates = (EagleString.egString.LargeThan420(m_date) ? EagleFileIO.arrayOfRebate2 : EagleFileIO.arrayOfRebate);
                        if (si[i].b_directly)
                        {
                            m_flights.Add(si[i].fi[j].info_Flight);
                            m_bunks_lowest.Add(si[i].fi[j].info_Bunk_Lowest);
                        }
                        else
                        {
                            string citypair = si[i].fi[j].info_CityPair.Trim();
                            if (citypair.Length == 3)
                            {
                                si[i].fi[j].info_CityPair = si[i].fi[j - 1].info_CityPair.Substring(3) + citypair;
                            }
                        }
                    }
                }

                avstring = EagleString.egString.trim(avres);
            }
            catch (Exception ex)
            {
                IsAvReturnString = false;
                //MessageBox.Show("AvResult Constructor:" + ex.Message);
                //EagleFileIO.LogWrite("AvResult ����:" + ex.Message);
            }

        }
        /// <summary>
        /// av ��
        /// </summary>
        public void AvResult2(string avres, int price, int distance, bool avh)
        {
            try
            {
                m_price = price;
                m_distance = distance;

                IsAvReturnString = true;
                string[] sp = new string[16];//�ָ��Ϊ"\ni"
                for (int i = 0; i < 8; ++i)
                {
                    int itemp = i + 1;
                    sp[i] = "\n" + itemp.ToString();
                    sp[i + 8] = "\r\n" + itemp.ToString();
                }
                string[] avSeg = avres.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                nSegs = avSeg.Length - 1;
                //"27JUL(THU) WUHHKG\n"
                string str_date = avSeg[0].Trim().Split(' ')[0];
                int lPos = str_date.IndexOf('(');
                if (lPos >= 0) str_date = str_date.Substring(0, lPos);//27JUL or 27JUL08
                m_date = BaseFunc.str2datetime(str_date, true);
                m_fromto = avSeg[0].Trim().Split(' ')[1];                                                                      //------2

                si = new SegmentInfomation[nSegs];


                for (int i = 0; i < nSegs; ++i)
                {
                    
                    si[i] = new SegmentInfomation();
                    si[i].context = avSeg[i + 1];
                    string[] siSplit;
                    if (avh)
                    {
                        siSplit = si[i].context.Split(new string[] { "\n " }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        siSplit = si[i].context.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    si[i].b_directly = (siSplit.Length == 1);
                    si[i].fi = new FlightInformation[siSplit.Length];
                    for (int j = 0; j < siSplit.Length; j++)
                    {
                        int id = i + 1;
                        if (avh)
                        {
                            si[i].fi[j] = new FlightInformation(siSplit[j], id, m_price);//����av��avh��һ��
                        }
                        else
                        {
                            si[i].fi[j] = new FlightInformation(siSplit[j], id, m_price, false);//����av��avh��һ��
                        }
                        si[i].fi[j].a_rebates = (EagleString.egString.LargeThan420(m_date) ? EagleFileIO.arrayOfRebate2 : EagleFileIO.arrayOfRebate);
                        if (si[i].b_directly)
                        {
                            m_flights.Add(si[i].fi[j].info_Flight);
                            m_bunks_lowest.Add(si[i].fi[j].info_Bunk_Lowest);
                        }
                    }
                }

                avstring = EagleString.egString.trim(avres);
            }
            catch
            {
                IsAvReturnString = false;
            }
        }
        private bool IsAvReturnString = true;

        delegate void MyInvoke(ListView lv, bool b_display_no_seat, TO_LIST_WAYS pWays, bool b_share);
        MyInvoke mi;
        /// <summary>
        /// ʼ�ղ���ʾ������ĵ���
        /// </summary>
        public void ToListView(ListView lv, bool b_display_no_seat, TO_LIST_WAYS pWays)
        {
            ToListView(lv, b_display_no_seat, pWays,false);
        }
        /// <summary>
        /// ��ѡ��ʾ������
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="b_display_no_seat">����ʾ������λ</param>
        /// <param name="pWays">��ʾ��ʽ����ͼۣ�ȫ��</param>
        /// <param name="b_share">�Ƿ���ʾ������</param>
        /// <param name="b_spec">�Ƿ���ʾ�����λ</param>
        public void ToListView(ListView lv, bool b_display_no_seat, TO_LIST_WAYS pWays,bool b_share)
        {
            if (lv.InvokeRequired)
            {
                if (mi == null)
                {
                    mi = new MyInvoke(ToListView);
                    lv.Invoke(mi, new object[] { lv, b_display_no_seat, pWays ,b_share});
                    //ToListView(lv, b_display_no_seat, pWays);
                    return;
                }
            }
            else
            {
                try
                {
                    if (lv == null) throw new Exception("δ����LV_LOWEST��");

                    for (int i = 0; i < si.Length; ++i)
                    {
                        if (!si[i].b_directly && pWays == TO_LIST_WAYS.LOWEST) continue;//filter the not direct arrive flight, ȫ����ʾʱ���Ƿ���ֱ����AVָ����ƣ�����Ҫ���������
                        for (int j = 0; j < si[i].fi.Length; ++j)
                        {
                            if (si[i].fi[j].SharedFlight && (!b_share)) continue; //jump over shared flight
                            if (m_shield_airline.IndexOf(si[i].fi[j].info_AirLine) >= 0) continue;//jump over shield airline
                            ListViewItem lvi = new ListViewItem();
                            si[i].fi[j].Price = m_price;
                            si[i].fi[j].ToListViewItem(lvi, b_display_no_seat, pWays,m_base_id,m_display_seat_amount);
                            lv.Items.Add(lvi);
                        }
                    }
                    //{ "���","���չ�˾","����","��ɳ���","�ִ����","���ʱ��","�ִ�ʱ��","����"}
                    //switch (pWays)
                    //{//�������ӵĵ�һ�е�
                    //    case TO_LIST_WAYS.ALL:
                    //        if (!string.IsNullOrEmpty(lv.Items[0].Text))
                    //        {
                    //            ListViewItem lvi = new ListViewItem();
                    //            lvi.Text = "";
                    //            lvi.SubItems.Add("");// lvi.SubItems.Add(m_airline + m_int_flighno.ToString());
                    //            lvi.SubItems.Add("");//lvi.SubItems.Add(m_policy_string);
                    //            lvi.SubItems.Add("");//lvi.SubItems.Add(m_citypair);
                    //            lvi.SubItems.Add("");//lvi.SubItems.Add(m_time_begin);
                    //            lvi.SubItems.Add("");//lvi.SubItems.Add(m_time_end);
                    //            lvi.SubItems.Add("");//lvi.SubItems.Add(m_plane_type);
                    //            lvi.SubItems.Add("");
                    //            int[] a_rebates = new int[] { 150, 130, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30 };
                    //            for (int i = 0; i < a_rebates.Length; ++i)
                    //            {
                    //                lvi.SubItems.Add(egString.TicketPrice(m_price, a_rebates[i]).ToString()+"Ԫ");
                    //            }
                    //            lv.Items.Insert(0, lvi);
                    //        }
                    //        break;
                    //}
                    for (int i = 0; i < lv.Items.Count; ++i)
                    {
                        ListViewItem lvi = lv.Items[i];
                        if (i % 2 == 1)
                            lvi.BackColor = Color.LightGray;
                    }
                }
                catch (Exception ex)
                {
                    EagleFileIO.LogWrite("AvResult.ToListView:" + ex.Message);
                }
                mi = null;
            }
        }
        public int BASE_ID { get { return m_base_id; } set { m_base_id = value; } }
        /// <summary>
        /// TRUE:��ʾ����,FALSE:��ʾ��λ����
        /// </summary>
        public bool DISPLAY_SEAT_AMOUNT { get { return m_display_seat_amount; } set { m_display_seat_amount = value; } }
        private bool m_display_seat_amount;
        private int m_base_id = 0;
        private int nSegs = 0;
        private string avstring = "";
        private string m_fromto = "      ";
        private string m_shield_airline = "";//split by ',' ,like mu,cz,mf 
        private int m_price = 0;
        private int m_distance = 0;
        private List<string> m_flights = new List<string>();
        private List<char> m_bunks_lowest = new List<char>();
        public SegmentInfomation[] si;
        /// <summary>
        /// �Ƿ�AV�Ĳ�ѯ���
        /// </summary>
        public bool SUCCEED { get { return IsAvReturnString; } }
        /// <summary>
        /// ��:ֱ�ɺ����б�
        /// </summary>
        public List<string> Flights { get { return m_flights; } }
        /// <summary>
        /// ��:��Ӧֱ�ɺ����б��е���ͼ�������λ�б�
        /// </summary>
        public List<char> BunksLowest { get { return m_bunks_lowest; } }
        /// <summary>
        /// ��:AV���ԭ�ı�
        /// </summary>
        public string AvString { get { return avstring; } }
        /// <summary>
        /// ��д:Y�յļ۸�
        /// </summary>
        public int Price { get { return m_price; } set { m_price = value; } }
        /// <summary>
        /// ��д:ֱ�ɾ���
        /// </summary>
        public int Distance { get { return m_distance; } set { m_distance = value; } }
        /// <summary>
        /// ��:���ж�
        /// </summary>
        public string CityPair { get { return m_fromto; } }
        /// <summary>
        /// ��:��������
        /// </summary>
        public string CityBeg { get { return m_fromto.Substring(0, 3); } }
        /// <summary>
        /// ��:�ִ����
        /// </summary>
        public string CityEnd { get { return m_fromto.Substring(3); } }

        private DateTime m_date = DateTime.Now;
        /// <summary>
        /// ��:AV����е�����(������)
        /// </summary>
        public DateTime FlightDate_DT { get { return m_date; } }
        /// <summary>
        /// ��:AV����е�����(�ı���)like 27JUL08
        /// </summary>
        public string FlightDate_STR { get { return m_date.ToString("ddMMMyy", new CultureInfo("en-us", false).DateTimeFormat).ToUpper(); } }
        /// <summary>
        /// ��д:the airlines will be shielding in List Controls
        /// </summary>
        public string ShieldAirline { get { return m_shield_airline.ToUpper(); } set { m_shield_airline = value.ToUpper(); } }
    }
    /// <summary>
    /// һ�����ε���Ϣ�������У������ࣺ���̣�
    /// </summary>
    public class SegmentInfomation
    {
        public string context;
        public FlightInformation[] fi = null;
        public bool b_directly = true;
    }
    /// <summary>
    /// һ���������Ϣ
    /// </summary>
    public class FlightInformation
    {
        public string context;
        private int m_id;
        private bool m_shareflight;
        private string m_airline;
        private int m_int_flighno;
        private string m_policy_string;
        private string m_citypair;
        private string m_time_begin;
        private string m_time_end;
        private string m_plane_type;
        private int m_transits;
        private bool m_meal;
        private bool m_eticket;
        private List<string> m_bunk = new List<string>();
        private string m_shareflightname;
        private char m_bunk_lowest;

        private string m_bunkseq = "";//the sequence of bunk
        public int[] a_rebates;// = new int[] { 150, 130, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30 };
        private int m_price_y = 0;

        /// <summary>
        /// ����������MU2501
        /// </summary>
        public string info_Flight { get { return m_airline + m_int_flighno.ToString(); } }
        /// <summary>
        /// ������ͼ۲�
        /// </summary>
        public char info_Bunk_Lowest { get { return m_bunk_lowest; } }

        public FlightInformation(string str, int id, int price)
        {
            
            try
            {
                m_id = id;
                m_price_y = price;
                context = str;
                string[] fiRow = context.Split(new string[] { "\n>" }, StringSplitOptions.RemoveEmptyEntries);

                string temp = fiRow[0];
                string temp_bunk = "";
                string flight = egString.substring(temp, 2, 8).Trim();
                try
                {
                    m_shareflightname = egString.substring(fiRow[1], 2, 8).Trim();
                }
                catch
                {
                }
                m_shareflight = false;
                if (flight[0] == '*')
                {
                    flight = flight.Substring(1);
                    m_shareflight = true;
                }
                m_airline = flight.Substring(0, 2);
                m_int_flighno = int.Parse(flight.Substring(2));
                m_policy_string = egString.substring(temp, 11, 3).Trim();
                temp_bunk += egString.substring(temp, 15, 44 - 15).Trim();
                m_citypair = egString.substring(temp, 46, 6).Trim();
                m_time_begin = egString.substring(temp, 53, 6);
                m_time_end = egString.substring(temp, 60, 6);
                if (m_time_end.IndexOf("+") > 0)
                {
                    try
                    {
                        int tempi = int.Parse(m_time_end.Substring(0, 4)) + int.Parse(m_time_end.Split('+')[1]) * 2400;
                        m_time_end = tempi.ToString("d4");
                    }
                    catch
                    {
                    }
                }
                m_plane_type = egString.substring(temp, 67, 3);
                int.TryParse(egString.substring(temp, 71, 1), out m_transits);
                m_meal = (egString.substring(temp, 73, 1) == "M");
                m_eticket = (egString.substring(temp, 76, 1) == "E");
                if (fiRow.Length > 1)
                {
                    temp = fiRow[1].Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    temp_bunk += (" " + egString.substring(temp, 15, temp.Length - 15).Trim());
                }
                temp_bunk = egString.trim(temp_bunk);

                string[] bk = temp_bunk.Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < bk.Length; ++i)
                {
                    if (bk[i].Length != 2) continue;
                    m_bunk.Add(bk[i]);
                }
                m_bunkseq = EagleFileIO.ReadBunkSeq(m_airline).ToUpper();
                

                m_bunk_lowest = get_lowest_bunk_has_seat();
                SortByRebate(m_bunkseq.ToCharArray());

                
            }
            catch
            {
                throw new Exception("AVH FlightInfomation����ʧ�ܣ�ת��AV���죡");
            }
        }
        
        public FlightInformation(string str, int id, int price, bool avh)
        {
            try
            {
                str = egString.trim(str, " +-");
                m_shareflight = (str[0] == '*');
                str = egString.trim(str, " *");
                //0         1         2         3         4   
                //01234567890123456789012345678901234567890123456789
                //CZ3631  WUHTAO 0800   0920   738 0^C  E   DS# F3 A3 P2 YA TA KA HA MA GA SQ*
                string[] a = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                m_airline = a[0].Substring(0, 2);
                m_int_flighno = int.Parse(a[0].Substring(2));
                m_citypair = a[1];
                m_time_begin = a[2];
                m_time_end = a[3].Trim();
                if (m_time_end.IndexOf("+") > 0)
                {
                    try
                    {
                        int tempi = int.Parse(m_time_end.Substring(0, 4)) + int.Parse(m_time_end.Split('+')[1]) * 2400;
                        m_time_end = tempi.ToString("d4");
                    }
                    catch
                    {
                    }
                }
                m_plane_type = a[3];
                int.TryParse(egString.substring(str, 33, 1), out m_transits);
                m_eticket = (egString.substring(str, 38, 1) == "E");
                string[] bk = str.Substring(46).Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < bk.Length; ++i) m_bunk.Add(bk[i]);
                m_bunkseq = EagleFileIO.ReadBunkSeq(m_airline).ToUpper();

                m_bunk_lowest = get_lowest_bunk_has_seat();
                SortByRebate(m_bunkseq.ToCharArray());
            }
            catch
            {
                throw new Exception("AV FlightInfomation����ʧ�ܣ�����ϵ�׸�");
            }
        }
        private void SortByRebate(char[] input)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] > 'Z' || input[i] < 'A')
                {
                    ls.Add("  ");
                    continue;
                }
                int j = 0;
                for (j = 0; j < m_bunk.Count; ++j)
                {
                    if (m_bunk[j][0] == input[i])
                    {
                        ls.Add(m_bunk[j]);
                        m_bunk.RemoveAt(j);
                        break;
                    }
                }
                //�Ҳ��������Կո����
                if (j == m_bunk.Count)
                {
                    ls.Add("  ");
                }
            }
            m_bunk.Clear();
            m_bunk.AddRange(ls);
        }
        /// <summary>
        /// ��FI���뵽һ��LVI�У�ID��Ҫ���ϻ���
        /// </summary>
        /// <param name="lvi"></param>
        /// <param name="b_display_no_seat_bunks">pWays=0ʱ��Ч</param>
        /// <param name="pWays">0:��ȫ����λ��lvi,1:������׼۲�λ��lvi</param>
        public void ToListViewItem(ListViewItem lvi, bool b_display_no_seat_bunks, TO_LIST_WAYS pWays,int baseNumber,bool display_seats)
        {
            if (lvi == null) lvi = new ListViewItem();
            lvi.SubItems.Clear();

            switch (pWays)
            {
                case TO_LIST_WAYS.ALL://"���","���չ�˾","����","��ɳ���","�ִ����","���ʱ��","�ִ�ʱ��","����"
                    lvi.Text = Convert.ToString(baseNumber +m_id);
                    lvi.SubItems.Add(EagleString.BaseFunc.AirLineCnName(m_airline));
                    lvi.SubItems.Add(m_airline + m_int_flighno.ToString());
                    //lvi.SubItems.Add(m_policy_string);
                    //lvi.SubItems.Add(m_citypair);
                    if (display_seats)
                    {
                        lvi.SubItems.Add(EagleString.EagleFileIO.CityCnName(m_citypair.Substring(0, 3)));
                        lvi.SubItems.Add(EagleString.EagleFileIO.CityCnName(m_citypair.Substring(3)));
                    }
                    else
                    {
                        lvi.SubItems.Add(m_citypair.Substring(0, 3));
                        lvi.SubItems.Add(m_citypair.Substring(3));
                    }
                    lvi.SubItems.Add(m_time_begin.Insert(2,":"));
                    lvi.SubItems.Add(m_time_end.Insert(2, ":"));
                    lvi.SubItems.Add(m_plane_type);
                    for (int i = 0; i < m_bunk.Count; ++i)
                    {
                        string temp = "";
                        if (b_display_no_seat_bunks)
                        {
                            temp = (m_bunk[i]);
                        }
                        else
                        {
                            temp = (m_bunk[i][1] > 'A' ? "" : m_bunk[i]);
                        }
                        if (display_seats)
                        {
                            if (temp != "")
                            {
                                if (temp[1] == 'A') temp = ">10";
                                else temp = temp.Substring(1);
                            }
                        }
                        lvi.SubItems.Add(temp);
                    }
                    break;
                case TO_LIST_WAYS.LOWEST://add subItems : Lowest Bunk , Lowest Price
                    lvi.Text = m_id.ToString();
                    lvi.SubItems.Add(m_airline + m_int_flighno.ToString());
                    //lvi.SubItems.Add(m_policy_string);
                    //lvi.SubItems.Add(m_citypair);
                    lvi.SubItems.Add(m_time_begin);
                    lvi.SubItems.Add(m_time_end);
                    //lvi.SubItems.Add(m_plane_type);
                    for (int i = m_bunkseq.Length - 1; i >= 0; --i)
                    {
                        char c = m_bunkseq[i];
                        if (c > 'Z' || c < 'A') continue;
                        bool bFlag = false;
                        for (int j = 0; j < m_bunk.Count; ++j)
                        {
                            string d = m_bunk[j].Trim();
                            if (d == "" || d[0] != c || d[1] > 'A') continue;
                            lvi.SubItems.Add(m_bunk[j]);
                            m_bunk_lowest = m_bunk[j][0];
                            lvi.SubItems.Add(egString.TicketPrice(m_price_y, a_rebates[i]).ToString());
                            lvi.SubItems.Add("0");//profit
                            lvi.SubItems.Add(a_rebates[j].ToString());
                            bFlag = true;
                            break;
                        }
                        if (bFlag) break;
                    }
                    break;
            }
        }

        private char get_lowest_bunk_has_seat()
        {
            for (int i = m_bunkseq.Length - 1; i >= 0; --i)
            {
                char c = m_bunkseq[i];
                if (c > 'Z' || c < 'A') continue;
                bool bFlag = false;
                for (int j = 0; j < m_bunk.Count; ++j)
                {
                    string d = m_bunk[j].Trim();
                    if (d == "" || d[0] != c || d[1] > 'A') continue;
                    m_bunk_lowest = m_bunk[j][0];
                    return m_bunk_lowest;
                }
            }
            return ' ';
        }
        /// <summary>
        /// ��ȡ������AvResult�е����
        /// </summary>
        public int info_ID { get { return m_id; } }
        /// <summary>
        /// ��ȡ���չ�˾���ִ���
        /// </summary>
        public string info_AirLine { get { return m_airline; } }
        /// <summary>
        /// ��ȡ������Y�ռ۸�
        /// </summary>
        public int info_Price { get { return m_price_y; } set { m_price_y = value; } }
        /// <summary>
        /// �������Ƿ�Ϊ������
        /// </summary>
        public bool SharedFlight { get { return m_shareflight; } }
        /// <summary>
        /// ��������
        /// </summary>
        public string SharedFlightName { get { return m_shareflightname; } }
        /// <summary>
        /// Y�ռ۸�
        /// </summary>
        public int Price { get { return m_price_y; } set { m_price_y = value; } }

        public string info_CityPair { get { return m_citypair; } set { m_citypair = value; } }
    }

    /// <summary>
    /// ���뵽ListView�ķ�ʽ
    /// </summary>
    public enum TO_LIST_WAYS 
    { 
        /// <summary>
        /// ��ȫ����λ��lvi,��Ŀǰ�ļ��װ�
        /// </summary>
        ALL,
        /// <summary>
        /// ������׼۲�λ��lvi
        /// </summary>
        LOWEST
    };
}
