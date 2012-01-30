/*Sample String
>PAT:M
WUH MU2501 PVG    810.00  Y         100%    810.00 733 24DEC08 0830 0945
>FN FCNY  810.00/ SCNY 810.00/ C3.00/ TCNY 50.00CN/ TCNY 80.00YQ
FC:WUH MU PVG 810.00Y
-  CNY  810.00 END
FP CASH, CNY

>PAT:M
WUH MU2430 NGB    490.00  Y65        65%    750.00 320 24DEC08 2230 2355
>FN FCNY  490.00/ SCNY 490.00/ C3.00/ TCNY 50.00CN/ TCNY 80.00YQ
FC:WUH MU NGB 490.00Y65
-  CNY  490.00 END
FP CASH, CNY


>PAT:A
WUH MU  PVG     810.00 Y        100%     810.00 733  24DEC08
01 Y FARE:CNY810.00 TAX:CNY50.00 YQ:CNY80.00 TOTAL:940.00
WUH MU PVG 810.00Y CNY810.00END
>SFC:01

>PAT:A
WUH MU  NGB     490.00 Y65       65%     750.00 320  24DEC08
01 Y65 FARE:CNY490.00 TAX:CNY50.00 YQ:CNY80.00 TOTAL:620.00
WUH MU NGB 490.00Y65 CNY490.00END
>SFC:01

>PAT:A
没有符合条件的运价
>PAT:

>PAT:M*CH
WUH MU2501 PVG    410.00  YCH50      50%    810.00 733 24DEC08 0830 0945
>FN FCNY  410.00/ SCNY 410.00/ C3.00/ TEXEMPTCN/ TCNY 40.00YQ
FC:WUH MU PVG 410.00YCH50
-  CNY  410.00 END
FP CASH, CNY

>PAT:M*CH
WUH MU2430 NGB    380.00  YCH50      50%    750.00 320 24DEC08 2230 2355
>FN FCNY  380.00/ SCNY 380.00/ C3.00/ TEXEMPTCN/ TCNY 40.00YQ
FC:WUH MU NGB 380.00YCH50
-  CNY  380.00 END
FP CASH, CNY

 * 
 * 
 * #yzzs 同PAT:
>PAT:M
WUH CZ3571 PVG    770.00  YCT95      95%    810.00 738 24DEC08 0930 1105
PVG CZ3832 SZX   1330.00  YCT95      95%   1400.00 320 25DEC08 1400 1605
>FN FCNY 2100.00/ SCNY 2100.00/ C3.00/ TCNY100.00CN/ TCNY230.00YQ
FC WUH CZ PVG 770.00YCT95 CZ SZX 1330.00YCT95
-  CNY 2100.00 END
FP CASH, CNY

 * 双航段PAT:A
>PAT:A
WUH CZ  PVG     810.00 Y        100%     810.00 738  24DEC08
PVG CZ  SZX    1400.00 Y        100%    1400.00 320  25DEC08
01 Y+Y FARE:CNY2210.00 TAX:CNY100.00 YQ:CNY230.00 TOTAL:2540.00
WUH CZ PVG 810.00Y CZ SZX 1400.00Y CNY2210.00END
>SFC:01



*/
using System;
using System.Collections.Generic;
using System.Text;

namespace EagleString
{
    public class PatResult
    {

        public string STRING { get { return m_string; } }
        public PAT_TYPE PATTYPE { get { return m_pat; } }
        /// <summary>
        /// PAT:A时，是否能做出PAT:A项，即不是返回：没有符合条件的运价
        /// </summary>
        public bool HAS_PAT_A { get { return m_pat_a_has; } }
        public bool SUCCEED { get { return m_succeed; } }
        public int FARE { get { return m_fare; } }
        public int TAX_BUILD { get { return m_taxbuild; } }
        public int TAX_FUEL { get { return m_taxfuel; } }
        public int TAXS { get { return m_taxfuel + m_taxbuild; } }
        public int TOTAL { get { return m_taxfuel + m_taxbuild + m_fare; } }
        /// <summary>
        /// 做PAT:后，返回的需要直接发送的票价组项
        /// </summary>
        public string PAT_M { get { return m_pat_m; } }
        public List<string> LS_SFC { get { return m_sfc; } }
        private string m_string;
        private PAT_TYPE m_pat;
        private bool m_pat_a_has;
        private bool m_succeed;
        private int m_fare;//票面
        private int m_taxbuild;
        private int m_taxfuel;
        private List<string> m_sfc = new List<string>();
        private string m_pat_m;
        public PatResult(string m)
        {
            m = egString.trim(m.Trim(), '>');
            try
            {
                m_succeed = true;
                m_string = m;
                string[] line = m_string.Split(Structs.SP_R_N, StringSplitOptions.RemoveEmptyEntries);
                string t = egString.trim(line[0]);

                if (t == "PAT:A")
                {
                    m_pat = PAT_TYPE.PAT_A;
                    m_pat_a_has = (m_string.IndexOf("没有符合条件的运价") < 0);
                    if (!m_pat_a_has) return;

                    string s = "FARE:";
                    string s2 = "TAX:";
                    m_fare = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY","").Trim());

                    s = s2;
                    s2 = "YQ:";
                    m_taxbuild = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY", "").Trim());

                    s = s2;
                    s2 = "TOTAL:";
                    try
                    {
                        m_taxfuel = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY", "").Trim());
                    }
                    catch
                    {
                        m_taxfuel = 0;
                    }
                    set_sfc(m);

                }
                else if (t == "PAT:A*IN")
                {
                    m_pat = PAT_TYPE.PAT_IN;
                    m_pat_a_has = (m_string.IndexOf("没有符合条件的运价") < 0);
                    if (!m_pat_a_has) return;
                    string s = "FARE:";
                    string s2 = "TAX:";
                    m_fare = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY", "").Trim());
                    m_taxbuild = 0;
                    m_taxfuel = 0;

                    set_sfc(m);
                }
                else if (t == "PAT:M")
                {
                    m_pat = PAT_TYPE.PAT;
                    string s = "FCNY";
                    string s2 = "/";
                    m_fare = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY", "").Trim());

                    s = "TCNY";
                    s2 = "CN";
                    m_taxbuild = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY", "").Trim());

                    s = "YQ";
                    s2 = "TCNY";
                    try
                    {
                        m_taxfuel = (int)Convert.ToDouble(egString.Between2StringReverse(m_string, s, s2).Replace("CNY", "").Trim());
                    }
                    catch
                    {
                        m_taxfuel = 0;
                    }
                    set_pat_m(m);

                }
                else if (t == "PAT:M*CH")
                {
                    m_pat = PAT_TYPE.PAT_CH;
                    string s = "FCNY";
                    string s2 = "/";
                    m_fare = (int)Convert.ToDouble(egString.Between2String(m_string, s, s2).Replace("CNY", "").Trim());

                    m_taxbuild = 0;

                    s = "YQ";
                    s2 = "TCNY";
                    try
                    {
                        m_taxfuel = (int)Convert.ToDouble(egString.Between2StringReverse(m_string, s, s2).Replace("CNY", "").Trim());
                    }
                    catch
                    {
                        m_taxfuel = 0;
                    }
                    set_pat_m(m);
                }

                else m_succeed = false;
            }
            catch (Exception ex)
            {
                EagleFileIO.LogWrite("PatResult Constructor Failed:" + ex.Message);
                m_succeed = false;
            }
        }
        private void set_sfc(string m)
        {
            string[] a = m.Split(new string[] { "SFC:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < a.Length; ++i)
            {
                m_sfc.Add("SFC:" + i.ToString("d2"));
            }
        }
        private void set_pat_m(string m)
        {
            m_pat_m = m.Substring(m.LastIndexOf('>') + 1);
        }
        public enum PAT_TYPE : byte
        {
            /// <summary>
            /// 普通PAT:
            /// </summary>
            PAT = 0,
            /// <summary>
            /// 特价PAT:A
            /// </summary>
            PAT_A = 1,
            /// <summary>
            /// 儿童PAT:*CH
            /// </summary>
            PAT_CH = 2,
            /// <summary>
            /// 婴儿PAT:A*IN
            /// </summary>
            PAT_IN = 3,
            /// <summary>
            /// 南航纵横中国PAT:#YZZS
            /// </summary>
            PAT_YZZS = 4,
            /// <summary>
            /// 东航
            /// </summary>
            PAT_MUYTR = 5,
            /// <summary>
            /// 川航
            /// </summary>
            PAT_3UZZ =6
        }
    }
}
