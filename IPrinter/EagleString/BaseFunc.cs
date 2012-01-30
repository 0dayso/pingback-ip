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
                throw new Exception("生成作废行程单字节码失败！");
            }
        }
        static public string EIstring(string al, char bunk ,bool b2009420)
        {
            string air = egString.left(egString.trim(al, "* "), 2);
            int rebate = EagleFileIO.RebateOf(bunk, air);
            string EI = "";
            int[] sp = new int[] { 100, 85, 40 };
            if (b2009420)//420修改
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
                EI = "不得签转";
            }
            else if (rebate >= sp[2])
            {
                EI = "不得签转更改";
            }
            else
            {
                EI = "不得签转更改退票";
            }
            return EI;
        }
        /// <summary>
        /// The Same In egString Class
        /// </summary>
        /// <param name="code">月三字码</param>
        /// <returns>返回string型数字月份</returns>
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
        /// 返回int型月份
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        static public int MonthInt2(string code)
        {
            return int.Parse(MonthInt(code));
        }
        /// <summary>
        /// 按二字代码取航空公司中文名称,只要在这里去掉对应航空公司，中文版就无法预定！
        /// </summary>
        /// <param name="AirLineCode">航班号或航空公司名(可带星号直接计算)</param>
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
                case "JD":                    return "金鹿航空";
                case "PN":                    return "西部航空";
                case "NS":                    return "东北航空公司";

                case "CA":                    return "国际航空公司";
                case "MU":                    return "东方航空公司";
                case "CZ":                    return "南方航空公司";
                case "SZ":                    return "西南航空公司";
                case "WH":                    return "西北航空公司";
                case "CJ":                    return "北方航空公司";
                case "SH":                    return "上海航空公司";
                case "3Q":                    return "云南航空公司";
                case "F6":                    return "航空股份有限公司";
                case "GP":                    return "通用航空公司";
                case "Z2":                    return "中原航空公司";
                case "MF":                    return "厦门航空公司";
                case "3U":                    return "四川航空公司";
                case "XO":                    return "新疆航空公司";
                case "H4":                    return "海南省航空公司";
                case "X2":                    return "新华航空公司";
                case "ZH":                    return "深圳航空公司";
                case "ZJ":                    return "浙江航空公司";
                case "WU":                    return "武汉航空公司";
                case "GH":                    return "贵州省航空公司";
                case "GW":                    return "长城航空公司";
                case "G8":                    return "长城航空公司";
                case "FJ":                    return "福建航空公司";
                case "SC":                    return "山东航空公司";
                case "3W":                    return "南京航空公司";
                case "2Z":                    return "长安航空公司";
                case "FM":                    return "上海航空公司";
                case "HU":                    return "海南航空公司";
                case "8C":                    return "东星航空公司";
                case "EU":                    return "鹰联航空公司";
                case "KA":                    return "港龙航空公司";
                case "HO":                    return "吉祥航空公司";
                case "8L":                    return "鹏祥航空公司";
                case "9C":                    return "春秋航空公司";
                case "KN":                    return "中联航空公司";
                case "G5":                    return "华夏航空公司";
                case "GS":                    return "华夏航空公司";
                case "BK":                    return "奥凯航空公司";
                case "CN":                    return "大新华航空有限公司";
                case "BR":                    return "长荣航空公司";
                case "CI":                    return "中华航空公司";
                case "CK":                    return "中国货运航空有限公司";
                case "CX":                    return "国泰航空有限公司";
                case "EF":                    return "远东航空运输公司";
                case "JL":                    return "日本航空公司";
                case "NH":                    return "全日空航空公司";
                case "UA":                    return "美联航空公司";
                case "OS":                    return "奥地利航空";
                case "QF":                    return "澳洲航空公司";
                case "3K":                    return "捷星亚洲航空公司";
                case "4L":                    return "AIR ASTANA";
                case "5J":                    return "宿雾太平洋航空公司";
                case "5Q":                    return "KEENAIR CHARTER LTD";
                case "7G":                    return "Michael Kruger AirLines";
                case "AA":                    return "美国航空公司";
                case "AB":                    return "柏林航空公司";
                case "AC":                    return "加拿大航空公司";
                case "AF":                    return "法国航空公司";
                case "AI":                    return "印度航空公司";
                case "AK":                    return "亚洲航空公司";
                case "AM":                    return "墨西哥航空公司";
                case "AN":                    return "澳大利亚安捷航空公司";
                case "AP":                    return "比利时国际空运公司";
                case "AR":                    return "阿根廷航空公司";
                case "AS":                    return "阿拉斯加航空公司";
                case "AY":                    return "芬兰航空公司";
                case "AZ":                    return "意大利航空公司";
                case "B2":                    return "白俄罗斯航空公司";
                case "BA":                    return "英国航空公司";
                case "BD":                    return "英国大陆航空公司";
                case "BG":                    return "孟加拉航空公司";
                case "BI":                    return "汶莱皇家航空";
                case "BL":                    return "越南太平洋航空";
                case "CE":                    return "南非Nationwide航空公司";
                case "CO":                    return "美国大陆航空公司";
                case "D7":                    return "亚航X";
                case "DF":                    return "金鹿公务机有限公司";
                case "DJ":                    return "澳洲维珍航空公司";
                case "DL":                    return "美国达美航空";
                case "DN":                    return "Air Exel Belgique";
                case "DS":                    return "瑞士easyJet Switzerland";
                case "EK":                    return "阿联酋航空";
                case "EN":                    return "多洛米蒂航空公司";
                case "EY":                    return "阿联酋阿提哈德航空公司";
                case "FD":                    return "泰国亚洲航空公司";
                case "FL":                    return "边境航空公司";
                case "FV":                    return "俄罗斯国家航空公司";
                case "G6":                    return "柬埔寨吴哥航空公司";
                case "GA":                    return "印度尼西亚航空";
                default:                      return "";
            }
        }
        /// <summary>
        /// 将Eterm日期格式转换为DateTime,".","+","-"
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
        /// 校验PNR的正确性
        /// </summary>
        /// <param name="code">PNR</param>
        /// <returns>正确为true</returns>
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
        /// 是否完全符合航班号格式:(*)航空公司二字码+数字号 正则表达式\*[A-Za-z0-9]{2}[0-9]{3,4}
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
        /// 校验OFFICE的正确性
        /// </summary>
        static public bool OfficeValidate(string office)
        {
            if (office.Length != 6) return false;
            string s = office.ToUpper();
            string c = s.Substring(0, 3);//城市代码
            string d = s.Substring(3);//数字
            if (EagleFileIO.CityCnName(c) == "") return false;
            int i = 0;
            if (!int.TryParse(d, out i)) return false;
            return true;
        }
        /// <summary>
        /// 校验电子客票号的格式
        /// </summary>
        /// <param name="tktno">13或14位的电子客票号aaabbbbbbbbbb或aaa-bbbbbbbbbb</param>
        /// <param name="tkt13">返回13位的电子客票号</param>
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
        /// 返回对应舱位的中文意义
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
                    ret = "剩余" + ch.ToString() + "个位置！";
                    break;
                case 'A':
                    ret = "剩余10个以上位置！";
                    break;
                case 'B':
                    ret = "";
                    break;
                case 'C':
                    ret = "舱位取消";
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
                    ret = "无票需要向航空公司申请";
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
                    ret = "无票，需要向航空公司申请";
                    break;
                case 'R':
                    ret = "";
                    break;
                case 'S':
                    ret = "无票，需要向航空公司申请";
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
                    ret = "无此折扣舱位";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 结束进程(根据进程启动时间，及pName开头的进程名，判断要杀死的唯一进程
        /// </summary>
        /// <param name="pName">进程名，如eagle</param>
        /// <param name="start">进程启动时间</param>
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
        /// 判断指定的字体是否已经安装。主要是TEC字体
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
        /// 自动安装TEC字体，若不成功则提示安装方法
        /// </summary>
        static public void FontTecSetupMethodPromopt()
        {
            if (!FontTecInstall())
            {
                string s = "行程单字体未安装！\r\n";
                s += "安装步骤如下:\r\n";
                s += "1. 进入系统Fonts目录，一般为C:\\Windows\\Fonts\r\n";
                s += "2. 找到TEC.TTF文件，将其拖动到桌面\r\n";
                s += "3. 再把桌面上的TEC.TTF文件拖回到拖回到系统Fonts目录下，系统会显示字体安装过程，完成字体安装\r\n";
                s += "注意:若找不到对应的TEC.TTF文件，请重新安装易格航空订票系统\r\n";
                MessageBox.Show(s);
            }
        }
        /// <summary>
        /// 安装指定的FONTS目录下的TEC字体
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
        /// 加密类
        /// </summary>
        public class Crypt
        {
            /// <summary>
            /// 生成MD5摘要
            /// </summary>
            /// <param name="original">数据源</param>
            /// <returns>摘要</returns>
            private static byte[] MakeMD5(byte[] original)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyhash = hashmd5.ComputeHash(original);
                hashmd5 = null;
                return keyhash;
            }
            /// <summary>
            /// 使用给定密钥加密
            /// </summary>
            /// <param name="original">明文</param>
            /// <param name="key">密钥</param>
            /// <returns>密文</returns>
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
            /// 使用给定密钥解密数据
            /// </summary>
            /// <param name="encrypted">密文</param>
            /// <param name="key">密钥</param>
            /// <returns>明文</returns>
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
            /// 加密字符串类
            /// </summary>
            public static class CryptString
            {
                private const string mstr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
                /**/
                /// <summary>
                /// 字符串加密
                /// </summary>
                /// <param name="str">待加密的字符串</param>
                /// <returns>加密后的字符串</returns>
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
                /// 字符串解密
                /// </summary>
                /// <param name="str">待解密的字符串</param>
                /// <returns>解密后的字符串</returns>
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
        /// 搜索指定Name的控件,无则返回NULL,忽略大小写
        /// </summary>
        public static Control SearchSubCtrl(Control.ControlCollection parent, string name)
        {
            Control[] ctrl = parent.Find(name, true);
            if (ctrl.Length > 0) return ctrl[0];
            else
            {
                EagleFileIO.LogWrite("BaseFunc.SearchSubCtrl : 未找到控件" + name);
                return null;
            }
        }
    }
    public class Imported
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern IntPtr GetActiveWindow();
        /// <summary>
        /// 通过传入电子票号与行程单号得到十六进制串
        /// </summary>
        /// <param name="sn">13位的电子客票号</param>
        /// <param name="en">10位的行程单号</param>
        /// <param name="output">要发送的三合一类型字符串</param>
        /// <returns></returns>
        [DllImport("TestDll.dll", EntryPoint = "RecieptPrint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern unsafe static string RecieptPrint([MarshalAs(UnmanagedType.LPStr)]string sn, [MarshalAs(UnmanagedType.LPStr)]string en, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        /// <summary>
        /// 通过传入电子票号，行程单得到作废行程单的十六进制串
        /// </summary>
        /// <param name="sn">13位的电子客票号</param>
        /// <param name="en">10位的行程单号</param>
        /// <param name="userid">";USERID=xPkbi2qSPe8="</param>
        /// <param name="pwd">";PWD=OKK5r2tgag3UwE9nNaTCXw=="</param>
        [DllImport("TestDll.dll", EntryPoint = "RecieptRemove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern unsafe static string RecieptRemove([MarshalAs(UnmanagedType.LPStr)]string sn, [MarshalAs(UnmanagedType.LPStr)]string en, [MarshalAs(UnmanagedType.LPStr)]string userid, [MarshalAs(UnmanagedType.LPStr)]string pwd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        /// <summary>
        /// GDI的字体安装，返回0表示安装失败
        /// 使用：SendMessage((IntPtr)0xffff, 0x001d, IntPtr.Zero, IntPtr.Zero);  //通知其它正在运行的应用程序，有新字体注册了
        /// </summary>
        /// <param name="fontSource">字体文件名(要路径)</param>
        [DllImport("gdi32.dll", EntryPoint = "AddFontResource")]
        public static extern int FontResourceAdd([In, MarshalAs(UnmanagedType.LPWStr)]string fontSource);
        /// <summary>
        /// 向指令窗口发送消息
        /// </summary>
        /// <param name="hWnd">0xFFFF向所有窗口发送</param>
        /// <param name="Msg">消息类型</param>
        /// <param name="wParam">存放参数</param>
        /// <param name="lParam">存放参数</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);
        public enum egMsg : int
        {
            /// <summary>
            /// 通知主窗口切换配置，配置ID放在wParam中
            /// </summary>
            SWITCH_CONFIG = 0x1100,
            /// <summary>
            /// 切换配置结果,切换成功lparam=1,否则为0
            /// </summary>
            SWITCH_CONFIG_RESULT = 0x1101,
            /// <summary>
            /// 从易格报表导入消息,lParam为当前导入数
            /// </summary>
            EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_CURRENT=0x1103,

            EXPIRE_TICKET_IMPORT_FROM_EAGLEFINANCE_TOTAL=0x1104//总条数
        }
    }
}