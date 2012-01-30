/*SAMPLE XML
 <eg>
    <cm>ReplyKBunkInfo</cm>
	<bunkinfo>
		<onebunkinfo>
			<kpolicyid>1</kpolicyid>
			<fromto>WUHSHA</fromto>
			<airways>CZ</airways>
			<bunk>t</bunk>
			<price>220</price>
			<discount>0.04</discount>
			<date>2008-12-14</date>
            <oprice></oprice>
		</onebunkinfo>
	</bunkinfo>
</eg>
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
namespace EagleString
{
    public class SpecTicResult
    {
        public DateTime FlightDate;
        private string m_main = "";
        private List<SPECIAL_TICKET_INFO> m_ls = new List<SPECIAL_TICKET_INFO>();
        /// <summary>
        /// xml is created from the webservice "GetKBunkInfo"
        /// </summary>
        /// <param name="xml"></param>
        public SpecTicResult(string xml)
        {
            m_main = xml;
            init();
        }
        private int m_count = 0;

        private void init()
        {
            if (m_main == "") return;
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(m_main);
            XmlNode xn = xd.SelectSingleNode("//eg/bunkinfo");
            m_count = xn.ChildNodes.Count;
            for (int i = 0; i < m_count; ++i)
            {
                XmlNode t = xn.ChildNodes[i];
                SPECIAL_TICKET_INFO s = new SPECIAL_TICKET_INFO(t);
                m_ls.Add(s);
            }

        }
        /// <summary>
        /// list special ticket in lv and lv2,
        /// </summary>
        /// <param name="lv">the fix bunk listview</param>
        /// <param name="lv2">the varable bunk listview</param>
        /// <param name="flight">the flights of AvResult</param>
        public void ToListView(ListView lv,ListView lv2,string [] flight,char [] bunk,int price)
        {
            try
            {
                if (lv == null) return;
                if (m_count == 0) return;
                List<string> lsFlight = EagleString.egString.ArrayToList(flight);
                lv.Items.Clear();
                lv2.Items.Clear();
                for (int i = 0; i < m_ls.Count; ++i)
                {
                    SPECIAL_TICKET_INFO s = m_ls[i];
                    bool bflowbunk = (s.bunk == "");
                    // if flight is null that indicate all flight,should be fit the flights in ListView of AvResult
                    if (s.flight == null || s.flight[0] == "*" || s.flight[0]=="")
                    {
                        if (s.airline == "") continue;
                        string al = s.airline;
                        List<string> ls = new List<string>();
                        for (int j = 0; j < flight.Length; ++j)
                        {
                            if (flight[j].IndexOf(al) == 0) ls.Add(flight[j]);
                        }
                        if (ls.Count == 0) continue;
                        s.flight = ls.ToArray();
                    }

                    for (int j = 0; j < s.flight.Length; ++j)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = s.id.ToString();
                        lvi.SubItems.Add(s.fromto);
                        if(s.flight[j].Substring(0,2)==s.airline)//若航班号中有航空公司代码
                            lvi.SubItems.Add(s.flight[j]);
                        else
                            lvi.SubItems.Add(s.airline + s.flight[j]);
                        lvi.SubItems.Add(s.bunk);
                        if (bflowbunk)
                        {
                            //if(flight.contain(s.fight[j])
                            if (lsFlight.Contains(s.flight[j]))
                            {
                                lvi.SubItems.Add(
                                    EagleString.egString.TicketPrice(
                                        price, 
                                        EagleString.EagleFileIO.RebateOf(bunk[lsFlight.IndexOf(s.flight[j])], s.airline)).ToString());
                            }
                            else
                                lvi.SubItems.Add("0");
                        }
                        else
                        {
                            int p = s.price;
                            if (p == 0)
                            {
                                p = EagleString.egString.TicketPrice(price, s.discounti);
                            }
                            lvi.SubItems.Add(p.ToString());
                        }
                        lvi.SubItems.Add(s.discounti.ToString());
                        lvi.SubItems.Add(s.date.ToShortDateString());
                        if (bflowbunk)//flow bunk
                        {
                            lvi.SubItems.Add(string.Format("{1}折以上见舱低{0}点", s.discounti, s.rebate_lowest));
                            lv2.Items.Add(lvi);

                        }
                        else//fix bunk
                        {
                            lv.Items.Add(lvi);
                        }
                    }
                }
                for (int i = 0; i < lv.Items.Count; ++i)
                {
                    if (i % 2 == 0)
                    {
                        lv.Items[i].BackColor = System.Drawing.Color.LightGray;
                    }
                }
                for (int i = 0; i < lv2.Items.Count; ++i)
                {
                    if (i % 2 == 0)
                    {
                        lv2.Items[i].BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
            catch
            {
            }
        }

        struct SPECIAL_TICKET_INFO
        {
            public int id;
            public string fromto;
            public string airline;
            public string bunk;//if bunk=="", it indicate that bunk is flow by discountf , else the bunk must be a value
            public int price;
            public string[] flight;
            /// <summary>
            /// 浮点型折扣0.01-1
            /// </summary>
            public float discountf;
            /// <summary>
            /// 整型折扣1-100
            /// </summary>
            public int discounti
            {
                get
                {
                    //return (int)(discountf * 100F);
                    return (int)discountf;
                }
                set
                {
                    //discountf = (float)value / 100F;
                    discountf = (float)value;
                }
            }
            public DateTime date;
            public int rebate_lowest;
            /// <summary>
            /// initial variables with Xml
            /// </summary>
            /// <param name="xn">should pointer the FLAG "onebunkinfo"</param>
            public SPECIAL_TICKET_INFO(XmlNode xn)
            {
                if (xn.InnerXml != "")
                {
                    id = Convert.ToInt32(xn.SelectSingleNode("kpolicyid").InnerText);
                    fromto = xn.SelectSingleNode("fromto").InnerText.ToUpper();
                    airline = xn.SelectSingleNode("airways").InnerText.ToUpper();
                    flight = xn.SelectSingleNode("flight").InnerText.ToUpper().Split(',') ;
                    bunk = xn.SelectSingleNode("bunk").InnerText.ToUpper();
                    price = Convert.ToInt32(xn.SelectSingleNode("price").InnerText);
                    discountf = (float)Convert.ToDouble(xn.SelectSingleNode("discount").InnerText);
                    date = Convert.ToDateTime(xn.SelectSingleNode("date").InnerText);
                    rebate_lowest = Convert.ToInt32(xn.SelectSingleNode("oprice").InnerText);
                }
                else
                {
                    fromto = airline = bunk = "";
                    discountf = 0F;
                    id = price = 0;
                    date = System.DateTime.Now;
                    rebate_lowest = 0;
                    flight = null;
                }
            }
            


        }
        /// <summary>
        /// 申请特殊舱位返回XML代理类
        /// </summary>
        public class ApplyResult
        {

        }
        /// <summary>
        /// 处理特殊舱位XML代理类
        /// </summary>
        public class HandleResult
        {
        }
    }
    
}
