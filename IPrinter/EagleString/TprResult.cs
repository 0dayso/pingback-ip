using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class TprResult
    {
        public string TXT { get { return m_txt; } }
        public bool SUCCEED { get { return m_succeed; } }
        public string OFFICE { get { return m_office; } }
        public DateTime DATE { get { return m_date; } }
        string m_txt;
        bool m_succeed;
        string m_office;
        string m_iata_number;
        int m_device;
        DateTime m_date;
        int m_ticket_total;
        int m_ticket_void;
        int m_ticket_refund;

        float m_fare_normal;
        float m_carriers;
        float m_tax_normal;
        float m_commit_normal;
        float m_refund_net;
        float m_deduction;
        float m_tax_refund;
        float m_commit_refund;
        public TprResult(string t)
        {
            t = egString.trim(t.Trim(), '>');
            m_txt = t;
            try
            {
                m_succeed = true;
                m_office = egString.Between2String(t, "OFFICE :", "IATA NUMBER :");
                m_iata_number = egString.Between2String(t, "IATA NUMBER :", "DEVICE :");
                m_device = Convert.ToInt32(egString.Between2String(t, "DEVICE :","/"));
                m_date = BaseFunc.str2datetime(egString.Between2String(t, "DATE   :", "AIRLINE:"), false);

                m_ticket_total = Convert.ToInt32(egString.Between2String(t, "TOTAL TICKETS:", "("));
                m_ticket_void = Convert.ToInt32(egString.Between2String(t, "TICKETS VOID", "TICKETS REFUND"));
                m_ticket_refund = Convert.ToInt32(egString.Between2String(t, "TICKETS REFUND", ")"));
                m_fare_normal = float.Parse(egString.Between2String(t, "NORMAL FARE -- AMOUNT :", "CNY"));
                m_carriers = float.Parse(egString.Between2String(t, "CARRIERS -- AMOUNT :", "CNY"));
                m_tax_normal = float.Parse(egString.Between2String(t, "NORMAL TAX -- AMOUNT :", "CNY"));
                m_commit_normal = float.Parse(egString.Between2String(t, "NORMAL COMMIT -- AMOUNT :", "CNY"));
                m_refund_net = float.Parse(egString.Between2String(t, "NET REFUND -- AMOUNT :", "CNY"));
                m_deduction = float.Parse(egString.Between2String(t, "DEDUCTION -- AMOUNT :", "CNY"));
                m_tax_refund = float.Parse(egString.Between2String(t, "REFUND TAX -- AMOUNT :", "CNY"));
                m_commit_refund = float.Parse(egString.Between2String(t, "REFUND COMMIT -- AMOUNT :", "CNY"));
                if (TicketNumbers() != m_ticket_total) throw new Exception("");
            }
            catch
            {
                m_succeed = false;
            }
        }

        private int TicketNumbers()
        {
            int numbers = 0;
            string[] a = m_txt.Split('\n');
            for (int i = 0; i < a.Length; ++i)
            {
                try
                {
                    string tkt = "";
                    if (BaseFunc.TicketNumberValidate(a[i].Substring(0, 14), ref tkt))
                    {
                        ++numbers;
                    }
                }
                catch
                {
                }
            }
            return numbers;
        }
    }
}
