using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;

namespace EagleString
{
    public class EagleFileIO
    {

        public static int[] TrafficAmountRead()
        {
            int[] ret = new int[] { 0, 0 };

            try
            {
                string file = Application.StartupPath + "\\traf.dat";
                string[] lines = File.ReadAllLines(file);
                int.TryParse(lines[0].Trim(), out ret[0]);
                int.TryParse(lines[1].Trim(), out ret[1]);
            }
            catch
            {
            }
            return ret;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void TrafficAmount(int type,int amount)
        {
            string file = Application.StartupPath + "\\traf.dat";
            int Amount1 = 0;
            int Amount2 = 0;
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                int.TryParse(lines[0].Trim(), out Amount1);
                int.TryParse(lines[1].Trim(), out Amount2);
            }
            if (type == 1)
            {
                Amount1 += amount;
            }
            else if (type == 2)
            {
                Amount2 += amount;
            }
            string[] arr = new[] { Amount1.ToString(), Amount2.ToString() };
            File.WriteAllLines(file, arr);
        }
        /// <summary>
        /// 备份options.xml，若长度为0，则从备份文件中复制
        /// </summary>
        public static void BackupOptionsXml()
        {
            string path = Application.StartupPath + "\\" ;
            string f = "options.xml";
            string f2 = "options.xml.bak";
            
            try
            {
                string text = File.ReadAllText(path + f).Trim();
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(text);
                File.Copy(path + f, path + f2, true);
            }
            catch
            {
                File.Copy(path + f2, path + f, true);
            }
        }
        public static string SignCodeOf(string office)
        {
            try
            {
                office = office.Substring(0, 6).ToUpper();
                if (!EagleString.BaseFunc.OfficeValidate(office)) throw new Exception("");
                string f = Application.StartupPath + "\\si.txt";//WUH128=SI:12345/56789a
                Hashtable ht = ReadEagleDotTxtFileToHashTable("si.txt");
                if (ht.ContainsKey(office)) return ht[office].ToString();
            }
            catch
            {
                
            }
            throw new Exception("未能在si.txt中搜索到SI参数！");
        }
        /// <summary>
        /// 在pnrprice.txt的格式为pnr,价格,日期 : ABCDE,1200,2009-4-4
        /// </summary>
        /// <param name="pnr"></param>
        /// <returns></returns>
        public static int PriceOfPnrInFile(string pnr)
        {
            string f = Application.StartupPath+"\\pp.mp3";
            if (!File.Exists(f)) return -1;
            string[] lines = File.ReadAllLines(f);
            foreach (string l in lines)
            {
                if (l.Trim() == "") continue;
                string[] a = l.Split(',');
                if (a[0].ToUpper() == pnr.ToUpper())
                {
                    TimeSpan ts = new TimeSpan();
                    DateTime dt = DateTime.Parse(a[2]);
                    ts = DateTime.Now - dt;
                    if (ts.Days > 7) continue;
                    return int.Parse(a[1]);
                }
            }
            return -1;
        }

        public static void WritePnrPrice2File(string pnr, int price)
        {
            try
            {
                string f = Application.StartupPath + "\\pp.mp3";
                if (!File.Exists(f))
                {
                    File.Create(f);
                }
                File.AppendAllText(f, "\r\n" + pnr + "," + price.ToString() + "," + DateTime.Now.ToShortDateString());
            }
            catch
            {
                WritePnrPrice2File(pnr, price);
            }
        }

        public static string File_Price = "Price.txt";
        public static void PriceTxtFix()
        {
            Hashtable ht = ReadEagleDotTxtFileToHashTable(File_Price);
            Hashtable ht2 = new Hashtable();
            foreach (DictionaryEntry de in ht)
            {
                string key = de.Key.ToString();
                if (!ht.ContainsKey(key.Substring(3) + key.Substring(0, 3)))
                {
                    ht2.Add((key.Substring(3) + key.Substring(0, 3)), de.Value.ToString());
                }
            }
            WiteHashTableToEagleDotTxt(ht2, "", File_Price);
        }


        public static EagleString.ServerAddr Server
        {
            get
            {
                Hashtable ht = EagleString.EagleFileIO.ReadEagleDotTxtFileToHashTable();
                string code = ht["TheRootAgent"].ToString();
                return (EagleString.ServerAddr)byte.Parse(code);
            }
        }

        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="shortcutPath">LNK文件路径及文件名:c:\My Document\my ShortCut.LNK</param>
        /// <param name="destPath">目标文件所在路径:c:\My Document\(最后含\)</param>
        /// <param name="destFile">目标文件名Eagle2.0.EXE</param>
        public static void CreateShortCut(string shortcutPath, string destPath, string destFile)
        {
            if (!File.Exists(destFile)) return;
            if (File.Exists(shortcutPath)) return;
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = destPath + destFile;
            shortcut.WorkingDirectory = destPath;
            shortcut.WindowStyle = 1;
            shortcut.Description = "易格2.0";
            shortcut.IconLocation = destPath + "eg2.0.ico";
            shortcut.Save();
        }
        public static void CreateShortCut20()
        {
            
            try
            {
                if (ReadEagleDotTxtFileToHashTable()["TheRootAgent"].ToString() != "0") return;
                string shortcutpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                shortcutpath = egString.trim(shortcutpath, '\\') + "\\";
                string destPath = Application.StartupPath + "\\";
                string destFile = "eagle2.0.exe";
                CreateShortCut(shortcutpath+"易格2.0快捷方式.lnk", destPath, destFile);
            }
            catch(Exception ex)
            {
                MessageBox.Show("创建快捷方式失败!" + ex.Message);
            }
        }
        //public static void SerializeRtResults(RtResultList ls)
        //{
        //    using (FileStream fs = new FileStream(Application.StartupPath + "\\SubmittedPnr.txt", FileMode.Create))
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        formatter.Serialize(fs, ls);
        //    }
            
        //}
        //public static RtResultList DeSerializeRtResults()
        //{
        //    try
        //    {
        //        using (FileStream fs = new FileStream(Application.StartupPath + "\\SubmittedPnr.txt", FileMode.Open))
        //        {
        //            BinaryFormatter formatter = new BinaryFormatter();
        //            return (RtResultList)(formatter.Deserialize(fs));
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 从价格文件Price.txt中获取指定城市对的价格。不存在则返回0
        /// </summary>
        public static int PriceOf(string citypair)
        {
            string price_distance = EagleString.EagleFileIO.ValueOf(citypair, EagleString.EagleFileIO.File_Price);
            if (price_distance == "") return 0;
            return Convert.ToInt32(price_distance.Split(',')[0]);
        }
        /// <summary>
        /// 从价格文件Price.txt中获取指定城市对的距离。不存在则返回0
        /// </summary>
        public static int DistanceOf(string citypair)
        {
            string price_distance = EagleString.EagleFileIO.ValueOf(citypair, EagleString.EagleFileIO.File_Price);
            if (price_distance == "") return 0;
            return Convert.ToInt32(price_distance.Split(',')[1]);
        }
        /// <summary>
        /// 读取对应航空公司的舱位序
        /// </summary>
        /// <param name="al">二字代码</param>
        /// <returns></returns>
        public static string ReadBunkSeq(string al)
        {
            
            string tt = "";
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\bunks.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            try
            {
                while (!sr.EndOfStream)
                {
                    string linestring = sr.ReadLine();
                    if (linestring.Substring(0, 2).ToUpper() == al.ToUpper())
                    {
                        tt = linestring.Split(',')[1].Replace('-', ' ');//sz,fcy-habcd-efg
                        break;
                    }
                }
            }
            catch
            {
                tt = "";
            }

            if(tt=="")
            {
                tt = "FCY ";
            }
            return tt;
        }
        public static int[] arrayOfRebate = new int[] { 150, 130, 100, 96, 92, 88, 84, 80, 76, 72, 68, 64, 60, 56, 52, 48, 44 };
        //public static int[] arrayOfRebate = new int[] { 150, 130, 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30 };
        public static int[] arrayOfRebate2 = new int[] { 150, 130, 100, 96, 92, 88, 84, 80, 76, 72, 68, 64, 60, 56, 52, 48, 44 };
        /// <summary>
        /// 返回对应折扣的舱位
        /// </summary>
        /// <param name="rebate"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        public static char BunkOf(int rebate, string al)
        {
            string bunkseq = ReadBunkSeq(al);
            int[] a_rebates = arrayOfRebate;
            for (int i = 0; i < a_rebates.Length; ++i)
            {
                if (rebate == a_rebates[i]) return bunkseq[i];
            }
            return '-';
        }
        public static char BunkOf(int rebate, string al ,bool newrebate)
        {
            string bunkseq = ReadBunkSeq(al);
            int[] a_rebates = arrayOfRebate;
            if (newrebate) a_rebates = arrayOfRebate2;
            for (int i = 0; i < a_rebates.Length; ++i)
            {
                if (rebate == a_rebates[i]) return bunkseq[i];
            }
            return '-';
        }
        /// <summary>
        /// 返回可申请低dec点的所有舱位
        /// </summary>
        /// <param name="bunk"></param>
        /// <param name="al"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static string BunksOf(char bunk, string al, int dec)
        {
            int a = RebateOf(bunk, al);
            if (a > 100) return bunk.ToString();
            string ret = "";
            int b = dec / 5;
            for (int i = 1; i <= b; ++i)
            {
                ret += BunkOf(a - i * 5, al);
            }
            return ret;
        }
        /// <summary>
        /// 返回可申请低dec点的所有舱位(420后)
        /// </summary>
        public static string BunksOfNew(char bunk, string al, int dec)
        {
            int a = RebateOf(bunk, al);
            if (a > 100) return bunk.ToString();
            string ret = "";
            int b = dec / 5;
            for (int i = 1; i <= b; ++i)
            {
                ret += BunkOf(a - i * 5, al);
            }
            return ret;
        }
        /// <summary>
        /// 返回可申请低dec点的所有折扣
        /// </summary>
        /// <param name="bunk"></param>
        /// <param name="al">航班，二字代码：*MU2501,CA1234,CZ</param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static int[] RebatesOf(char bunk, string al, int dec)
        {
            if (dec == 0) return null;
            int a = RebateOf(bunk, al);
            int count = dec / 5;
            int[] ret = new int[count];
            for (int i = 1; i <= count; ++i)
            {
                ret[i-1] = a - i * 5;
            }
            return ret;
        }
        /// <summary>
        /// 返回可申请低dec点的所有折扣(420)
        /// </summary>
        public static int[] RebatesOfNew(char bunk, string al, int dec)
        {
            if (dec == 0) return null;
            int a = RebateOf(bunk, al);
            int count = dec / 4;
            int[] ret = new int[count];
            for (int i = 1; i <= count; ++i)
            {
                ret[i - 1] = a - i * 4;
            }
            return ret;
        }
        /// <summary>
        /// 返回折扣(整数0-150),在舱位序中找不到，按29折计
        /// </summary>
        /// <param name="bunk"></param>
        /// <param name="al">航班，二字代码：*MU2501,CA1234,CZ</param>
        /// <returns></returns>
        public static int RebateOf(char bunk, string al)
        {
            try
            {
                if (al[0] == '*') al = al.Substring(1);
                al = al.Substring(0, 2);
                int[] a_rebates = arrayOfRebate;

                string bunkseq = ReadBunkSeq(al);
                for (int i = 0; i < bunkseq.Length; ++i)
                {
                    if (bunk == bunkseq[i]) return a_rebates[i];
                }
            }
            catch
            {
            }
            return 29;//在舱位序中找不到，按29折计
        }
        /// <summary>
        /// 2009-4-20后调用此函数
        /// </summary>
        public static int RebateOfNew(char bunk, string al)
        {
            try
            {
                if (al[0] == '*') al = al.Substring(1);
                al = al.Substring(0, 2);
                int[] a_rebates = arrayOfRebate2;
                string bunkseq = ReadBunkSeq(al);
                for (int i = 0; i < bunkseq.Length; ++i)
                {
                    if (bunk == bunkseq[i]) return a_rebates[i];
                }
            }
            catch
            {
            }
            return 43;//在舱位序中找不到，按43折计(420)
        }
        /// <summary>
        /// 将Eagle.txt配置读入到一个HashTable中
        /// </summary>
        /// <returns></returns>
        public static Hashtable ReadEagleDotTxtFileToHashTable()
        {
            Hashtable ht = new Hashtable();
            string txt = File.ReadAllText(Application.StartupPath + "\\eagle.txt", Encoding.Default);
            string[] lines = txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; ++i)
            {
                try
                {
                    string l = lines[i].Trim();
                    if (l == "") continue;
                    if (l.ToLower()[0] > 'z' || l.ToLower()[0] < 'a') continue;
                    if (l.IndexOf("=") < 0) continue;
                    string left = l.Split('=')[0].Trim();
                    string right = l.Split('=')[1].Trim();
                    ht.Add(left, right);
                }
                catch(Exception ex)
                {
                    LogWrite("EagleFileIO.ReadEagleDotTxtFileToHashTable : " + ex.Message);
                }
            }
            return ht;
        }
        /// <summary>
        /// 将应用程序目录下的指定文件的配置读入到Hashtable中,filename不包含路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Hashtable ReadEagleDotTxtFileToHashTable(string filename)
        {
            Hashtable ht = new Hashtable();
            string txt = File.ReadAllText(Application.StartupPath + "\\"+filename, Encoding.Default);
            string[] lines = txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; ++i)
            {
                try
                {
                    string l = lines[i].Trim();
                    if (l == "") continue;
                    if (l.ToLower()[0] > 'z' || l.ToLower()[0] < 'a') continue;
                    if (l.IndexOf("=") < 0) continue;
                    string left = l.Split('=')[0].Trim().ToUpper();
                    string right = l.Split('=')[1].Trim();
                    ht.Add(left, right);
                }
                catch (Exception ex)
                {
                    LogWrite("EagleFileIO.ReadEagleDotTxtFileToHashTable : " + ex.Message);
                }
            }
            return ht;
        }
        /// <summary>
        /// 将hash内容更新到Eagle.txt中，并保留原有注释
        /// </summary>
        /// <param name="hash"></param>
        public static void WiteHashTableToEagleDotTxt(Hashtable hash)
        {
            WiteHashTableToEagleDotTxt(hash, "");
        }
        public static void WiteHashTableToEagleDotTxt(Hashtable hash,string keyhead)
        {
            string file = Application.StartupPath + "\\eagle.txt";
            Hashtable ht = ReadEagleDotTxtFileToHashTable();
            string txt = File.ReadAllText(file, Encoding.Default);

            
            foreach (DictionaryEntry de in hash)
            {
                string key = keyhead + de.Key.ToString();
                if (ht.ContainsKey(key))
                {
                    txt = txt.Replace(key + "=" + ht[key], key + "=" + de.Value);
                }
                else
                {
                    txt += Environment.NewLine + key + "=" + de.Value;
                }
            }
            File.WriteAllText(file, txt, Encoding.Default);
        }
        public static void WiteHashTableToEagleDotTxt(Hashtable hash, string keyhead,string filename)
        {
            string file = Application.StartupPath + "\\" + filename;
            if (!File.Exists(file)) File.CreateText(file);
            Hashtable ht = ReadEagleDotTxtFileToHashTable(filename);
            string txt = File.ReadAllText(file, Encoding.Default);


            foreach (DictionaryEntry de in hash)
            {
                string key = keyhead + de.Key.ToString();
                if (ht.ContainsKey(key))
                {
                    txt = txt.Replace(key + "=" + ht[key], key + "=" + de.Value);
                }
                else
                {
                    txt += Environment.NewLine + key + "=" + de.Value;
                }
            }
            File.WriteAllText(file, txt, Encoding.Default);
        }
        /// <summary>
        /// 取eagle.txt对应left的值
        /// </summary>
        /// <param name="left">=号左边的串</param>
        /// <returns></returns>
        public static string ValueOf(string left)
        {
            return egString.Null2S(ReadEagleDotTxtFileToHashTable()[left]);
        }
        public static string ValueOf(string left,string filename)
        {
            return egString.Null2S(ReadEagleDotTxtFileToHashTable(filename)[left]);
        }
        /// <summary>
        /// 将内容写入当日指令日志文档log\yyyy-MM-dd.log
        /// </summary>
        /// <param name="text">要写入的内容</param>
        static public void LogWrite(string text)
        {
            //a.Log目录是否存在
            string path = Application.StartupPath;
            if (!Directory.Exists(path + "\\Log"))
                Directory.CreateDirectory(path + "\\Log");

            //b.当天文件名是否存在
            string filename = path + "\\Log\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string content = "\r\n" + "[" + System.DateTime.Now.ToLongTimeString() + "] " + text;

            bool bwrited = false;
            while (!bwrited)
            {
                try
                {
                    File.AppendAllText(filename, content);
                    bwrited = true;
                }
                catch
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        static public void LogWriteAccess(string send)
        {
            string recv = "";
            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\EagleLog.mdb;";
            OleDbConnection cn = new OleDbConnection();
            try
            {
                try
                {
                    cn.ConnectionString = ConnStr;
                    cn.Open();
                }
                catch
                {
                    return;//系统环境无法打开Access库，直接跳过
                }

                string cmString = "insert into LocalLog (STRINGSEND,STRINGRECV) Values ('{0}','{1}')";
                cmString = string.Format(cmString, send, recv);
                OleDbCommand cmd = new OleDbCommand(cmString, cn);
                cmd.CommandText = cmString;
                if (cmd.ExecuteNonQuery() != 1) throw new Exception("写日志时，保存本地数据失败");


                cn.Close();
            }
            catch
            {
                MessageBox.Show("LogWriteAccess : 写本地数据库日志失败");
            }
        }

        public static void RunProgram(string program, string arg)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = program;
                proc.StartInfo.Arguments = arg;
                proc.Start();
                if (!System.IO.File.Exists(program))
                    throw new Exception("未找到程序" + program);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 用记事本打开文件
        /// </summary>
        /// <param name="filename">文件名</param>
        static public void LogRead(string filename)
        {
            string prog = Environment.SystemDirectory + "\\notepad.exe";
            if (!System.IO.File.Exists(prog))
                throw new Exception("未找到记事本程序！");
            EagleFileIO.RunProgram(prog, filename);
        }
        /// <summary>
        /// 按type排列方式取城市列表CityCode.txt
        /// </summary>
        /// <param name="type">0:三字码,1:中文,2:拼音</param>
        /// <param name="inside">是否显示国内城市</param>
        /// <param name="outside">是否显示国外城市</param>
        /// <returns></returns>
        static public List<string> WhiteWindowCity(int type, bool inside, bool outside)
        {
            List<string> ls = new List<string>();
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\CityCode.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            string temp = reader.ReadLine();
            List<string> lstemp = new List<string>();
            while (temp != null)
            {
                if (temp.Split(',').Length < 4)
                {
                    temp = reader.ReadLine();
                    continue;
                }
                lstemp.Add(temp);
                temp = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
            for (int i = 0; i < lstemp.Count; ++i)
            {
                string[] a = lstemp[i].Split(',');
                string strAdd = "";
                if (type == 0)
                {
                    strAdd = a[2] + a[1];
                }
                else if (type == 1)
                {
                    strAdd = a[1] + a[2];
                }
                else if (type == 2)
                {
                    strAdd = a[0][0].ToString() + a[1] + a[2];
                }
                if (inside && (a[3].Trim() == "0")) ls.Add(strAdd);
                if (outside && (a[3].Trim() == "1")) ls.Add(strAdd);

            }
            ls.Sort();
            return ls;
        }
        /// <summary>
        /// 取对应三字码的中文城市名，没有则返回""
        /// </summary>
        /// <param name="citycode">三字代码</param>
        /// <returns></returns>
        public static string CityCnName(string citycode)
        {
            string code = citycode.Trim().ToUpper();
            if (code.Length != 3) return null;
            FileStream fs = new FileStream(Application.StartupPath + "\\citycode.txt", FileMode.Open, FileAccess.Read);//txt
            StreamReader reader = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
            string temp = reader.ReadLine();
            while (temp != null)
            {
                try
                {
                    string[] a = temp.Split(',');
                    if (a[2].Trim() == code)//txt
                    {
                        reader.Close();
                        fs.Close();
                        return a[1];//txt
                    }
                }
                catch
                {
                }
                temp = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
            return "";
        }
        /// <summary>
        /// 返回 武汉-上海
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public static string CityPairCnName(string cp)
        {
            if (cp.Length != 6) return "";
            return CityCnName(cp.Substring(0, 3)) + "-" + CityCnName(cp.Substring(3));
        }
        /// <summary>
        /// 重命名文件(移动文件,目标文件已存在，将被删除)
        /// </summary>
        /// <param name="pathFrom">源文件</param>
        /// <param name="pathTo">目标文件</param>
        public static void Rename(string pathFrom, string pathTo)
        {
            try
            {
                if (File.Exists(pathTo)) File.Delete(pathTo);
                File.Move(pathFrom, pathTo);
            }
            catch(Exception ex)
            {
                EagleFileIO.LogWrite("EagleFileIO.Rename : " + ex.Message);
            }
        }

        /// <summary>
        /// 获取对应机型的机场建设税
        /// </summary>
        /// <param name="airtype">机型</param>
        /// <returns></returns>
        public static int TaxOfBuildBy(string airtype)
        {
            try
            {
                string taxFile = Application.StartupPath + "\\tax.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(taxFile);
                if (airtype == "") return 0;
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("local").SelectSingleNode("build");
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    string value = xn.ChildNodes[i].InnerText;
                    if (value.IndexOf(airtype) >= 0)
                    {
                        string r = xn.ChildNodes[i].Name.Substring(3);
                        //if (r == "10") MessageBox.Show(airtype);
                        return Convert.ToInt32(r);
                    }
                }
            }
            catch(Exception ex)
            {
                LogWrite("EagleFileIO.TaxOfBuildBy :" +ex.Message);
            }
            return 50;
        }
        /// <summary>
        /// 按距离取燃油税(若距离为0，返回0)
        /// </summary>
        /// <param name="distance">距离</param>
        /// <returns></returns>
        public static int TaxOfFuelBy(int distance)
        {
            if (distance == 0) return 0;
            try
            {
                int d = distance;
                string taxFile = Application.StartupPath + "\\tax.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(taxFile);
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("local").SelectSingleNode("fuel");
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    string scope = xn.ChildNodes[i].InnerText;
                    double a = double.Parse(scope.Split('-')[0]);
                    double b = double.Parse(scope.Split('-')[1]);
                    if (d >= a && d <= b) return Convert.ToInt32(xn.ChildNodes[i].Name.Substring(3));
                }
            }
            catch(Exception ex)
            {
                LogWrite("EagleFileIO.TaxOfFuelBy :" +ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 取退改签规定
        /// </summary>
        /// <param name="air">航班号(可带星号)或航空公司二字代码</param>
        /// <param name="bunk"></param>
        /// <returns></returns>
        public static string TGQ_RULE(string air, string bunk)
        {
            try
            {
                string code = egString.trim(air, " *");
                string al = code.Substring(0, 2);
                string taxFile = Application.StartupPath + "\\tax.xml";
                XmlDocument xd = new XmlDocument();
                xd.Load(taxFile);
                if (al != "CZ") bunk = "ALL";
                string ret = "";
                XmlNode xn = xd.SelectSingleNode("eg").SelectSingleNode("TGQ").SelectSingleNode("_" + al).SelectSingleNode(bunk);
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    switch (xn.ChildNodes[i].Name)
                    {
                        case "T":
                            ret += "\r\n退票规定：";
                            break;
                        case "G":
                            ret += "\r\n变更规定：";
                            break;
                        case "Q":
                            ret += "\r\n订座规定：";
                            break;
                        default:
                            ret += "\r\n其它规定";
                            break;
                    }
                    ret += xn.ChildNodes[i].InnerText;
                }
                return ret;
            }
            catch(Exception ex)
            {
                //LogWrite("EagleFileIO.TGQ_RULE :" + air + bunk + ex.Message);
                return "";
            }
        }
        public static int EtdzPrinterNumber(string office)
        {
            if (string.IsNullOrEmpty(office)) return 0;
            try
            {
                office = office.ToUpper();
                string txt = File.ReadAllText(Application.StartupPath + "\\printerno.txt", Encoding.Default).ToUpper();
                string[] a = txt.Split('~');
                for (int i = 0; i < a.Length; ++i)
                {
                    if (a[i].IndexOf(office) >= 0)
                    {
                        return int.Parse(a[i].Split(':')[1].Trim());
                    }
                }
            }
            catch
            {
                
            }
            return 0;
        }

        public class Hosts
        {
            static public void AddEagleDNS()
            {
                try
                {
                    string file = System.Environment.SystemDirectory + "\\drivers\\etc\\hosts";
                    string nl = "\r\n";
                    string content = File.ReadAllText(file);
                    content += nl + "221.123.65.26 " + "www.eg66.com";
                    content += nl + "221.123.65.26 " + "yinge.eg66.com";
                    content += nl + "221.123.65.26 " + "download.eg66.com";
                    content += nl + "221.122.60.171 " + "wangtong.eg66.com";
                    content += nl + "221.122.60.171 " + "downwangtong.eg66.com";
                    File.WriteAllText(file, content, Encoding.Default);
                }
                catch (Exception ex)
                {
                    LogWrite("Hosts.AddEagleDNS : " + ex.Message);
                }
            }
            static public void DelEagleDNS()
            {
                try
                {
                    string file = System.Environment.SystemDirectory + "\\drivers\\etc\\hosts";
                    string nl = "\r\n";
                    string content = File.ReadAllText(file);
                    string[] a = content.Split(new string[] { nl }, StringSplitOptions.RemoveEmptyEntries);
                    content = "";
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i].Contains("eg66.com")) continue;
                        else content += nl + a[i];
                    }
                    File.WriteAllText(file, content, Encoding.Default);
                }
                catch (Exception ex)
                {
                    LogWrite("Hosts.DelEagleDNS : " + ex.Message);
                }
            }
        }
    }
}
