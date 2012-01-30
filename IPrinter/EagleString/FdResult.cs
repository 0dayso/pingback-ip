using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class FdResult
    {
        private string m_string;
        private int m_distance;
        private int m_price;
        private string m_citypair;
        public string STRING { get { return m_string; } }
        public int DISTANCE { get { return m_distance; } }
        public int PRICE { get { return m_price; } }
        public string CITYPAIR { get { return m_citypair; } }
        
        public FdResult(string m)
        {
            try
            {
                m_citypair = egString.Between2String(m_string, "FD:", "/");

                m_string = egString.trim(m);
                m_price = (int)Convert.ToDouble(egString.Between2StringReverse(m_string, "/Y/Y/", "=")) / 2;
                m_distance = Convert.ToInt32(egString.Between2String(m_string, "/TPM", "/"));

            }
            catch
            {
            }
        }
    }
}
