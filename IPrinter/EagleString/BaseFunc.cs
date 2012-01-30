using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Text;
namespace EagleString
{
    public class BaseFunc
    {
        static public string Receipt3In1Cancel(string receiptno, string tktno)
        {
            try
            {
                string ret = "01 00 00 32 0C 00 00 01-8C 0C 00 02 56 54 49 4E-56 20 ".Replace(" ", "").Replace("-", "");
                ret = (ret + "{0}" + "2C-7B 49 54 54 4E 3D ".Replace(" ", "").Replace("-", "") + "{1}" + "7D03");
                tktno = tktno.Replace("-", "");
                string a = "";
                string b = "";
                for (int i = 0; i < tktno.Length; i++)
                {
                    a += ((byte)tktno[i]).ToString("x2");
                }
                for (int i = 0; i < receiptno.Length; i++)
                {
                    b += ((byte)receiptno[i]).ToString("x2");
                }
                ret = string.Format(ret, a, b);
                return ret;
            }
            catch
            {
                throw new Exception("���������г̵��ֽ���ʧ�ܣ�");
            }
        }
        static public string EIstring(string al, char bunk ,bool b2009420)
        {
            string air = egString.left(egString.trim(al, "* "), 2);
            int rebate = EagleFileIO.RebateOf(bunk, air);
            string EI = "";
            int[] sp = new int[] { 100, 85, 40 };
            if (b2009420)//420�޸�
            {
                sp = new int[] { 100, 88, 52 };
                rebate = EagleFileIO.RebateOfNew(bunk, air);
            }
            if (rebate >= sp[0])
            {
                EI = "";
            }
            else if (rebate >= sp[1])
            {
                EI = "����ǩת";
            }
            else if (rebate >= sp[2])
            {
                EI = "����ǩת����";
            }
            else
            {
                EI = "����ǩת������Ʊ";
            }
            return EI;
        }
        /// <summary>
        /// The Same In egString Class
        /// </summary>
        /// <param name="code">��������</param>
        /// <returns>����string�������·�</returns>
        static public string MonthInt(string code)
        {
            if (code.Length != 3) return "";
            switch (code.ToUpper())
            {
                case "JAN":
                    return "01";
                case "FEB":
                    return "02";
                case "MAR":
                    return "03";
                case "APR":
                    return "04";
                case "MAY":
                    return "05";
                case "JUN":
                    return "06";
                case "JUL":
                    return "07";
                case "AUG":
                    return "08";
                case "SEP":
                    return "09";
                case "OCT":
                    return "10";
                case "NOV":
                    return "11";
                case "DEC":
                    return "12";
            }
            return "";
        }
        /// <summary>
        /// ����int���·�
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        static public int MonthInt2(string code)
        {
            return int.Parse(MonthInt(code));
        }
        /// <summary>
        /// �����ִ���ȡ���չ�˾��������,ֻҪ������ȥ����Ӧ���չ�˾�����İ���޷�Ԥ����
        /// </summary>
        /// <param name="AirLineCode">����Ż򺽿չ�˾��(�ɴ��Ǻ�ֱ�Ӽ���)</param>
        /// <returns></returns>
        public static string AirLineCnName(string AirLineCode)
        {
            if (AirLineCode.Length < 2)
            {
                return "";
            }
            string temp = AirLineCode.ToUpper().Substring(0,2);
            if (temp[0] == '*') temp = AirLineCode.ToUpper().Substring(1, 2);

            switch (temp)
            {
                case "JD":                    return "��¹����";
                case "PN":                    return "��������";
                case "NS":                    return "�������չ�˾";

                case "CA":                    return "���ʺ��չ�˾";
                case "MU":                    return "�������չ�˾";
                case "CZ":                    return "�Ϸ����չ�˾";
                case "SZ":                    return "���Ϻ��չ�˾";
                case "WH":                    return "�������չ�˾";
                case "CJ":                    return "�������չ�˾";
                case "SH":                    return "�Ϻ����չ�˾";
                case "3Q":                    return "���Ϻ��չ�˾";
                case "F6":                    return "���չɷ����޹�˾";
                case "GP":                    return "ͨ�ú��չ�˾";
                case "Z2":                    return "��ԭ���չ�˾";
                case "MF":                    return "���ź��չ�˾";
                case "3U":                    return "�Ĵ����չ�˾";
                case "XO":                    return "�½����չ�˾";
                case "H4":                    return "����ʡ���չ�˾";
                case "X2":                    return "�»����չ�˾";
                case "ZH":                    return "���ں��չ�˾";
                case "ZJ":                    return "�㽭���չ�˾";
                case "WU":                    return "�人���չ�˾";
                case "GH":                    return "����ʡ���չ�˾";
                case "GW":                    return "���Ǻ��չ�˾";
                case "G8":                    return "���Ǻ��չ�˾";
                case "FJ":                    return "�������չ�˾";
                case "SC":                    return "ɽ�����չ�˾";
                case "3W":                    return "�Ͼ����չ�˾";
                case "2Z":                    return "�������չ�˾";
                case "FM":                    return "�Ϻ����չ�˾";
                case "HU":                    return "���Ϻ��չ�˾";
                case "8C":                    return "���Ǻ��չ�˾";
                case "EU":                    return "ӥ�����չ�˾";
                case "KA":                    return "�������չ�˾";
                case "HO":                    return "���麽�չ�˾";
                case "8L":                    return "���麽�չ�˾";
                case "9C":                    return "���ﺽ�չ�˾";
                case "KN":                    return "�������չ�˾";
                case "G5":                    return "���ĺ��չ�˾";
                case "GS":                    return "���ĺ��չ�˾";
                case "BK":                    return "�¿����չ�˾";
                case "CN":                    return "���»��������޹�˾";
                case "BR":                    return "���ٺ��չ�˾";
                case "CI":                    return "�л����չ�˾";
                case "CK":                    return "�й����˺������޹�˾";
                case "CX":                    return "��̩�������޹�˾";
                case "EF":                    return "Զ���������乫˾";
                case "JL":                    return "�ձ����չ�˾";
                case "NH":                    return "ȫ�տպ��չ�˾";
                case "UA":                    return "�������չ�˾";
                case "OS":                    return "�µ�������";
                case "QF":                    return "���޺��չ�˾";
                case "3K":                    return "�������޺��չ�˾";
                case "4L":                    return "AIR ASTANA";
                case "5J":                    return "����̫ƽ�󺽿չ�˾";
                case "5Q":                    return "KEENAIR CHARTER LTD";
                case "7G":                    return "Michael Kruger AirLines";
                case "AA":                    return "�������չ�˾";
                case "AB":                    return "���ֺ��չ�˾";
                case "AC":                    return "���ô󺽿չ�˾";
                case "AF":                    return "�������չ�˾";
                case "AI":                    return "ӡ�Ⱥ��չ�˾";
                case "AK":                    return "���޺��չ�˾";
                case "AM":                    return "ī���纽�չ�˾";
                case "AN":                    return "�Ĵ����ǰ��ݺ��չ�˾";
                case "AP":                    return "����ʱ���ʿ��˹�˾";
                case "AR":                    return "����͢���չ�˾";
                case "AS":                    return "����˹�Ӻ��չ�˾";
                case "AY":                    return "�������չ�˾";
                case "AZ":                    return "��������չ�˾";
                case "B2":                    return "�׶���˹���չ�˾";
                case "BA":                    return "Ӣ�����չ�˾";
                case "BD":                    return "Ӣ����½���չ�˾";
                case "BG":                    return "�ϼ������չ�˾";
                case "BI":                    return "�����ʼҺ���";
                case "BL":                    return "Խ��̫ƽ�󺽿�";
                case "CE":                    return "�Ϸ�Nationwide���չ�˾";
                case "CO":                    return "������½���չ�˾";
                case "D7":                    return "�Ǻ�X";
                case "DF":                    return "��¹��������޹�˾";
                case "DJ":                    return "����ά�亽�չ�˾";
                case "DL":                    return "������������";
                case "DN":                    return "Air Exel Belgique";
                case "DS":                    return "��ʿeasyJet Switzerland";
                case "EK":                    return "����������";
                case "EN":                    return "�����׵ٺ��չ�˾";
                case "EY":                    return "������������º��չ�˾";
                case "FD":                    return "̩�����޺��չ�˾";
                case "FL":                    return "�߾����չ�˾";
                case "FV":                    return "����˹���Һ��չ�˾";
                case "G6":                    return "����կ��纽�չ�˾";
                case "GA":                    return "ӡ�������Ǻ���";
                default:                      return "";
            }
        }
        /// <summary>
        /// ��Eterm���ڸ�ʽת��ΪDateTime,".","+","-"
        /// </summary>
        /// <param name="date">MO24DEC08</param>
        /// <param name="bFuture">indicate that the result date must be larger than now!</param>
        public static DateTime str2datetime(string date, bool bFuture)
        {
            if (date.Trim() == ".") return DateTime.Parse(DateTime.Now.ToShortDateString());
            if (date.Trim() == "+") return DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
            if (date.Trim() == "-") return DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString());
            string d = date.Trim();
            if (date[0] > '9') d = d.Substring(2);
            string day = d.Substring(0,2);
            string month = d.Substring(2, 3);
            string year =DateTime.Now.Year.ToString();
            if (d.Length >5)
            {
                string t;
                if (d.Length == 7)t = Convert.ToString(DateTime.Now.Year / 100) + d.Substring(5);
                else if (d.Length == 6)t = Convert.ToString(DateTime.Now.Year / 10) + d.Substring(5);
                else t = DateTime.Now.Year.ToString();
                try
                {
                    year = Convert.ToInt32(t).ToString();
                }
                catch
                {
                }
            }
            DateTime ret = new DateTime(Convert.ToInt32(year), MonthInt2(month), Convert.ToInt32(day));
            if (ret < DateTime.Now.AddDays(-1) && bFuture) ret = ret.AddYears(1);
            return ret;
        }
        /// <summary>
        /// У��PNR����ȷ��
        /// </summary>
        /// <param name="code">PNR</param>
        /// <returns>��ȷΪtrue</returns>
        static public bool PnrValidate(string code)
        {
            code = code.ToUpper();
            if (code.Contains("O")) return false;
            if (code.Contains("I")) return false;
            if (code.Contains("U")) return false;
            if (code.Length != 5 && code.Length != 6) return false;
            int i = 0;
            while (i < code.Length)
            {
                if (code[i] > 'Z' || code[i] < '0')
                    return false;
                i++;
            }
            string[] word = { "TOTAL", "SEATS", "CHECK", "LEASE", "NAMES", "MULTI", "NVALI", "PLEAS" };
            for (i = 0; i < word.Length; i++)
            {
                if (code == word[i]) return false;
            }
            return true;
        }
        /// <summary>
        /// �Ƿ���ȫ���Ϻ���Ÿ�ʽ:(*)���չ�˾������+���ֺ� ������ʽ\*[A-Za-z0-9]{2}[0-9]{3,4}
        /// </summary>
        static public bool FlightValidate(string flight)
        {
            if (flight[0] == '*') flight = flight.Substring(1);
            if (flight.Length < 3) return false;
            if (AirLineCnName(flight) == "") return false;
            int number;
            if (!int.TryParse(flight.Substring(2), out number)) return false;
            return true;
        }
        /// <summary>
        /// У��OFFICE����ȷ��
        /// </summary>
        static public bool OfficeValidate(string office)
        {
            if (office.Length != 6) return false;
            string s = office.ToUpper();
            string c = s.Substring(0, 3);//���д���
            string d = s.Substring(3);//����
            if (EagleFileIO.CityCnName(c) == "") return false;
            int i = 0;
            if (!int.TryParse(d, out i)) return false;
            return true;
        }
        /// <summary>
        /// У����ӿ�Ʊ�ŵĸ�ʽ
        /// </summary>
        /// <param name="tktno">13��14λ�ĵ��ӿ�Ʊ��aaabbbbbbbbbb��aaa-bbbbbbbbbb</param>
        /// <param name="tkt13">����13λ�ĵ��ӿ�Ʊ��</param>
        static public bool TicketNumberValidate(string tktno,ref string tkt13)
        {
            string s = tktno.Replace("-", "");
            if (s.Length != 13) return false;
            long i = 0;
            if (!long.TryParse(s,out i)) return false;
            tkt13 = s;
            return true;
        }
        /// <summary>
        /// ���ض�Ӧ��λ����������
        /// </summary>
        /// <param name="ch">1-9,A-Z</param>
        static public string BunkCnMean(char ch)
        {
            string ret = "";
            switch (ch)
            {
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ret = "ʣ��" + ch.ToString() + "��λ�ã�";
                    break;
                case 'A':
                    ret = "ʣ��10������λ�ã�";
                    break;
                case 'B':
                    ret = "";
                    break;
                case 'C':
                    ret = "��λȡ��";
                    break;
                case 'D':
                    ret = "";
                    break;
                case 'E':
                    ret = "";
                    break;
                case 'F':
                    ret = "";
                    break;
                case 'G':
                    ret = "";
                    break;
                case 'H':
                    ret = "";
                    break;
                case 'I':
                    ret = "";
                    break;
                case 'J':
                    ret = "";
                    break;
                case 'K':
                    ret = "";
                    break;
                case 'L':
                    ret = "��Ʊ��Ҫ�򺽿չ�˾����";
                    break;
                case 'M':
                    ret = "";
                    break;
                case 'N':
                    ret = "";
                    break;
                case 'O':
                    ret = "";
                    break;
                case 'P':
                    ret = "";
                    break;
                case 'Q':
                    ret = "��Ʊ����Ҫ�򺽿չ�˾����";
                    break;
                case 'R':
                    ret = "";
                    break;
                case 'S':
                    ret = "��Ʊ����Ҫ�򺽿չ�˾����";
                    break;
                case 'T':
                    ret = "";
                    break;
                case 'U':
                    ret = "";
                    break;
                case 'V':
                    ret = "";
                    break;
                case 'W':
                    ret = "";
                    break;
                case 'X':
                    ret = "";
                    break;
                case 'Y':
                    ret = "";
                    break;
                case 'Z':
                    ret = "";
                    break;
                default:
                    ret = "�޴��ۿ۲�λ";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// ��������(���ݽ�������ʱ�䣬��pName��ͷ�Ľ��������ж�Ҫɱ����Ψһ����
        /// </summary>
        /// <param name="pName">����������eagle</param>
        /// <param name="start">��������ʱ��</param>
        static public void KillProcess(string pName, DateTime start)
        {
            foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcesses())
            {
                if (thisproc.ProcessName.ToLower()==pName.ToLower())
                {
                    try
                    {
                        DateTime d = thisproc.StartTime;
                        TimeSpan ts = start - d;
                        if (ts.TotalSeconds < 60) thisproc.Kill();
                    }
                    catch(Exception ex)
                    {
                        EagleFileIO.LogWrite("BaseFunc.KillProcess:" + ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// �ж�ָ���������Ƿ��Ѿ���װ����Ҫ��TEC����
        /// </summary>
        static public bool FontExist(string fontname)
        {
            InstalledFontCollection insFont = new InstalledFontCollection();
            FontFamily[] families = insFont.Families;
            foreach (FontFamily family in families)
            {
                if (family.Name.ToUpper() == fontname.ToUpper()) return true;
            }
            return false;
        }
        /// <summary>
        /// �Զ���װTEC���壬�����ɹ�����ʾ��װ����
        /// </summary>
        static public void FontTecSetupMethodPromopt()
        {
            if (!FontTecInstall())
            {
                string s = "�г̵�����δ��װ��\r\n";
                s += "��װ��������:\r\n";
                s += "1. ����ϵͳFontsĿ¼��һ��ΪC:\\Windows\\Fonts\r\n";
                s += "2. �ҵ�TEC.TTF�ļ��������϶�������\r\n";
                s += "3. �ٰ������ϵ�TEC.TTF�ļ��ϻص��ϻص�ϵͳFontsĿ¼�£�ϵͳ����ʾ���尲װ���̣�������尲װ\r\n";
                s += "ע��:���Ҳ�����Ӧ��TEC.TTF�ļ��������°�װ�׸񺽿ն�Ʊϵͳ\r\n";
                MessageBox.Show(s);
            }
        }
        /// <summary>
        /// ��װָ����FONTSĿ¼�µ�TEC����
        /// </summary>
        static private bool FontTecInstall()
        {
            string fn = Environment.SystemDirectory;
            fn = egString.trim(fn,'\\') + "\\fonts\\tec.ttf";
            return (Imported.FontResourceAdd(fn) != 0);
        }
        static public string [] CITY_ALLY(string city)
        {
            city = city.ToUpper();
            string[] a = new string[] { "XIY,SIA", "PEK,BJS", "PVG,SHA" };
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i].IndexOf(city) >= 0) return a[i].Split(',');
            }
            return city.Split(',');
        }
        static public List<string> CITYPAIR_ALLY(string citypair)
        {
            citypair = citypair.ToUpper();
            string[] start = CITY_ALLY(citypair.Substring(0, 3));
            string[] end = CITY_ALLY(citypair.Substring(3));

            List<string> ls = new List<string>();
            for (int i = 0; i < start.Length; ++i)
            {
                for (int j = 0; j < end.Length; ++j)
                {
                    ls.Add(start[i] + end[j]);
                }
            }
            return ls;
        }
        /// <summary>
        /// ������
        /// </summary>
        public class Crypt
        {
            /// <summary>
            /// ����MD5ժҪ
            /// </summary>
            /// <param name="original">����Դ</param>
            /// <returns>ժҪ</returns>
            private static byte[] MakeMD5(byte[] original)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyhash = hashmd5.ComputeHash(original);
                hashmd5 = null;
                return keyhash;
            }
            /// <summary>
            /// ʹ�ø�����Կ����
            /// </summary>
            /// <param name="original">����</param>
            /// <param name="key">��Կ</param>
            /// <returns>����</returns>
            public static byte[] Encrypt(byte[] original, byte[] key)
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = MakeMD5(key);
                des.Mode = CipherMode.ECB;

                return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
            }
            public static string Encrypt(string source)
            {
                byte[] key = System.Text.Encoding.ASCII.GetBytes("eagle.claw");
                byte[] original = System.Text.Encoding.ASCII.GetBytes(source);
                byte[] dest = Encrypt(original, key);
                return System.Text.Encoding.ASCII.GetString(dest);
            }
            /// <summary>
            /// ʹ�ø�����Կ��������
            /// </summary>
            /// <param name="encrypted">����</param>
            /// <param name="key">��Կ</param>
            /// <returns>����</returns>
            public static byte[] Decrypt(byte[] encrypted, byte[] key)
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = MakeMD5(key);
                des.Mode = CipherMode.ECB;
                return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
            }
            public static string Decrypt(string source)
            {
                byte[] key = System.Text.Encoding.Unicode.GetBytes("eagle.claw");
                byte[] encrypted = System.Text.Encoding.Unicode.GetBytes(source);
                byte[] dest = Decrypt(encrypted, key);
                return System.Text.Encoding.ASCII.GetString(dest);
            }
            /// <summary>
            /// �����ַ�����
            /// </summary>
            public static class CryptString
            {
                private const string mstr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
                /**/
                /// <summary>
                /// �ַ�������
                /// </summary>
                /// <param name="str">�����ܵ��ַ���</param>
                /// <returns>���ܺ���ַ���</returns>
                public static string EnCode(string str)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        return "";
                    }
                    byte[] buff = Encoding.Default.GetBytes(str);
                    int j, k, m;
                    int len = mstr.Length;
                    StringBuilder sb = new StringBuilder();
                    Random r = new Random();
                    for (int i = 0; i < buff.Length; i++)
                    {
                        j = (byte)r.Next(6);
                        buff[i] = (byte)((int)buff[i] ^ j);
                        k = (int)buff[i] % len;
                        m = (int)buff[i] / len;
                        m = m * 8 + j;
                        sb.Append(mstr.Substring(k, 1) + mstr.Substring(m, 1));
                    }
                    return sb.ToString();
                }
                /// <summary>
                /// �ַ�������
                /// </summary>
                /// <param name="str">�����ܵ��ַ���</param>
                /// <returns>���ܺ���ַ���</returns>
                public static string DeCode(string str)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        return "";
                    }
                    try
                    {
                        int j, k, m, n = 0;
                        int len = mstr.Length;
                        byte[] buff = new byte[str.Length / 2];
                        for (int i = 0; i < str.Length; i += 2)
                        {
                            k = mstr.IndexOf(str[i]);
                            m = mstr.IndexOf(str[i + 1]);
                            j = m / 8;
                            m = m - j * 8;
                            buff[n] = (byte)(j * len + k);
                            buff[n] = (byte)((int)buff[n] ^ m);
                            n++;
                        }
                        return Encoding.Default.GetString(buff);
                    }
                    catch
                    {
                        return "";
                    }
                }
            }
        }
        /// <summary>
        /// ����ָ��Name�Ŀؼ�,���򷵻�NULL,���Դ�Сд
        /// </summary>
        public static Control SearchSubCtrl(Control.ControlCollection parent, string name)
        {
            Control[] ctrl = parent.Find(name, true);
            if (ctrl.Length > 0) return ctrl[0];
            else
            {
                EagleFileIO.LogWrite("BaseFunc.SearchSubCtrl : δ�ҵ��ؼ�" + name);
                return null;
            }
        }
    }
    public class Imported
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern IntPtr GetActiveWindow();
        /// <summary>
        /// ͨ���������Ʊ�����г̵��ŵõ�ʮ�����ƴ�
        /// </summary>
        /// <param name="sn">13λ�ĵ��ӿ�Ʊ��</param>
        /// <param name="en">10λ���г̵���</param>
        /// <param name="output">Ҫ���͵�����һ�����ַ���</param>
        /// <returns></returns>
        [DllImport("TestDll.dll", EntryPoint = "RecieptPrint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern unsafe static string RecieptPrint([MarshalAs(UnmanagedType.LPStr)]string sn, [MarshalAs(UnmanagedType.LPStr)]string en, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        /// <summary>
        /// ͨ���������Ʊ�ţ��г̵��õ������г̵���ʮ�����ƴ�
        /// </summary>
        /// <param name="sn">13λ�ĵ��ӿ�Ʊ��</param>
        /// <param name="en">10λ���г̵���</param>
        /// <param name="userid">";USERID=xPkbi2qSPe8="</param>
        /// <param name="pwd">";PWD=OKK5r2tgag3UwE9nNaTCXw=="</param>
        [DllImport("TestDll.dll", EntryPoint = "RecieptRemove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern unsafe static string RecieptRemove([MarshalAs(UnmanagedType.LPStr)]string sn, [MarshalAs(UnmanagedType.LPStr)]string en, [MarshalAs(UnmanagedType.LPStr)]string userid, [MarshalAs(UnmanagedType.LPStr)]string pwd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        /// <summary>
        /// GDI�����尲װ������0��ʾ��װʧ��
        /// ʹ�ã�SendMessage((IntPtr)0xffff, 0x001d, IntPtr.Zero, IntPtr.Zero);  //֪ͨ�����������е�Ӧ�ó�����������ע����
        /// </summary>
        /// <param name="fontSource">�����ļ���(Ҫ·��)</param>
        [DllImport("gdi32.dll", EntryPoint = "AddFontResource")]
        public static extern int FontResourceAdd([In, MarshalAs(UnmanagedType.LPWStr)]string fontSource);
        /// <summary>
        /// ��ָ��ڷ�����Ϣ
        /// </summary>
        /// <param name="hWnd">0xFFFF�����д��ڷ���</param>
        /// <param name="Msg">��Ϣ����</param>
        /// <param name="wParam">��Ų���</param>
        /// <param name="lParam">��Ų���</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);
        public enum egMsg : int
        {
            /// <summary>
            /// ֪ͨ�������л����ã�����ID����wParam��
            /// </summary>
            SWITCH_CONFIG = 0x1100,
            /// <summary>
            /// �л����ý��,�л��ɹ�lparam=1,����Ϊ0
            /// </summary>
            SWITCH_CONFIG_RESULT = 0x1101,
            /// <summary>
            /// ���׸񱨱�����Ϣ,lParamΪ��ǰ������
            /// </summary>
            EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_CURRENT=0x1103,

            EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_TOTAL=0x1104//������
        }
    }
}