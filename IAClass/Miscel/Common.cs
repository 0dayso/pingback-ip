using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Sockets;
using System.Text;
using IAClass.Entity;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using log4net;

//namespace IAClass
//{
    /// <summary>
    /// Common 的摘要说明
    /// </summary>
    public class Common
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Common));
        /// <summary>
        /// 投保队列名称
        /// </summary>
        public static string Queue_Issuing = ConfigurationManager.AppSettings["Queue.Issuing"];
        /// <summary>
        /// 短信队列名称
        /// </summary>
        public static string Queue_SMS = ConfigurationManager.AppSettings["Queue.SMS"];
        /// <summary>
        /// 退保队列名称
        /// </summary>
        public static string Queue_Withdraw = ConfigurationManager.AppSettings["Queue.Withdraw"];
        /// <summary>
        /// 是否调试模式 true/false
        /// </summary>
        public static readonly bool Debug = bool.Parse(ConfigurationManager.AppSettings["Debug"]);
        /// <summary>
        /// 是否可以重复下单 true/false
        /// </summary>
        public static readonly bool IsDuplicable = bool.Parse(ConfigurationManager.AppSettings["IsDuplicable"]);
        /// <summary>
        /// 最晚投保时间限制：起飞前前x分钟
        /// </summary>
        public static readonly int IssuingDeadline = int.Parse(ConfigurationManager.AppSettings["IssuingDeadline"]);
        /// <summary>
        /// 从何处获取当前时间 NTSC：国际授时中心  Local：本机
        /// </summary>
        public static readonly string ConfigRequestTimeFrom = ConfigurationManager.AppSettings["RequestTimeFrom"];

        public static NBearLite.Database DB = new NBearLite.Database("InsuranceAviation");
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["InsuranceAviation"].ConnectionString;
        public static readonly string BaseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//HttpContext.Current.Server.MapPath("~");
        public static IAClass.MessageQClient MessageQ = new IAClass.MessageQClient(Queue_Issuing, 1);

        public static bool CheckIfSystemFailed(PurchaseResponseEntity response)
        {
            string systemFailInfo = ConfigurationManager.AppSettings["SystemFailInfo"];

            if (!string.IsNullOrEmpty(systemFailInfo))
            {
                //response.Trace.ErrorMsg = "接监管部门通知,系统暂停使用。";
                response.Trace.ErrorMsg = systemFailInfo.ToLower().Replace("[br]", "\n");
                return true;
            }
            else
                return false;
        }

        public static bool IsValidIPv4(string strIP)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(strIP, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ip_ = strIP.Split('.');

                if (ip_.Length == 4 || ip_.Length == 6)
                {
                    if (System.Int32.Parse(ip_[0]) < 256 && System.Int32.Parse(ip_[1]) < 256 & System.Int32.Parse(ip_[2]) < 256 & System.Int32.Parse(ip_[3]) < 256) return true;
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public static bool IsNumeric(string str)
        {
            if(string.IsNullOrEmpty(str))
                return false;
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        public enum DateType
        {
            Day = 1, Week, Month
        }

        /// <summary>
        /// 友好时间显示
        /// </summary>
        /// <param name="dtBase"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeAgo(DateTime dtBase, DateTime dt)
        {
            string str = "";
            TimeSpan ts = new TimeSpan(dtBase.Ticks - dt.Ticks);

            if (ts.TotalMinutes < 15)
            {
                str = "15分钟前";
            }
            else if (ts.TotalMinutes < 30)
            {
                str = "30分钟前";
            }
            else if (ts.TotalMinutes < 60)
            {
                str = "一个小时前";
            }
            else
            {
                str = "更久之前";
            }
            return str;
        }

        /// <summary>
        /// 替换sql语句中的有问题符号
        /// </summary>
        public static string ReplaceBadSQL(string str)
        {
            string str2 = "";
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            string str1 = str;
            string[] strArray = new string[] { "'", "--" };
            StringBuilder builder = new StringBuilder(str1);
            for (int i = 0; i < strArray.Length; i++)
            {
                str2 = builder.Replace(strArray[i], "").ToString();
            }
            return builder.Replace("@@", "@").ToString();
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDatetime()
        {
            if (ConfigRequestTimeFrom.ToUpper() == "NTSC")
                return GetDateTimeFromNTSC();
            else
                return DateTime.Now;
        }

        /// <summary>
        /// 国家授时中心 http://www.ntsc.cas.cn/
        /// </summary>
        /// <returns></returns>
        public static System.DateTime GetDateTimeFromNTSC()
        {
            TcpClient c = new TcpClient();
            c.Connect("www.time.ac.cn", 37); 

            NetworkStream s;
            s = c.GetStream();//读取数据流 

            byte[] buf = new byte[4];
            s.Read(buf, 0, 4);//把数据存到数组中 
            c.Close();

            uint serverSecs;
            //把服务器返回数据转换成1900/1/1   UTC   到现在所经过的秒数 

            //方法1
            //serverSecs = ((uint)buf[0] << 24) + ((uint)buf[1] << 16) + ((uint)buf[2] << 8) + (uint)buf[3];

            //方法2
            int recvInt = BitConverter.ToInt32(buf, 0);//这里将byte数组转换为int类型

            recvInt = System.Net.IPAddress.NetworkToHostOrder(recvInt);//这里转换网络字节序为主机字节序

            serverSecs = (uint)(recvInt);//转换为真正的秒数

            //得到真实的本地时间 
            System.DateTime datetime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            datetime = datetime.AddSeconds(serverSecs).ToLocalTime();

            return datetime;
        }

        /// <summary>
        /// 由身份证号码取得生日和性别
        /// </summary>
        /// <param name="identityCard">身份证号码</param>
        /// <returns>{yyyy-MM-dd, F} 数组</returns>
        public static BirthAndGender GetBirthAndSex(string identityCard)
        {
            string birthday = "";
            string genderStr = "";
            Gender gender;

            if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                genderStr = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                genderStr = identityCard.Substring(12, 3);
            }

            if (int.Parse(genderStr) % 2 == 0)//性别代码为偶数是女性奇数为男性
            {
                gender = Gender.Female;
            }
            else
            {
                gender = Gender.Male;
            }

            BirthAndGender birthAndGender = new BirthAndGender { Birth = DateTime.Parse(birthday), Gender = gender };
            return birthAndGender;
        }

        /// <summary>
        /// 根据给定的起止单证流水号,得到中间所有的连号
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string[] GetArranged(string start, string end)
        {
            if (start == end)
            {
                return new string[] { start };
            }

            string strPipelineNumberStart = Common.InterceptNumber(start);
            string strPipelineNumberEnd = Common.InterceptNumber(end);
            string strPipelinePrefix = start.Replace(strPipelineNumberStart, string.Empty).ToUpper();
            string strPipelinePrefix2 = end.Replace(strPipelineNumberEnd, string.Empty).ToUpper();

            if (strPipelinePrefix != strPipelinePrefix2)//前缀不一致
                throw new Exception("号段前缀（既字母部分）不一致！");

            int numberLenth = strPipelineNumberStart.Length;
            int numberLenth2 = strPipelineNumberEnd.Length;

            if (numberLenth != numberLenth2)//数字部分长度不一致
                throw new Exception("号段数字部分长度不一致！");

            if (numberLenth == 0)
                throw new Exception("请填写号段的数字部分！");

            Int64 intPipelineNumberStart = 0;
            Int64 intPipelineNumberEnd = 0;

            intPipelineNumberStart = Int64.Parse(strPipelineNumberStart);
            intPipelineNumberEnd = Int64.Parse(strPipelineNumberEnd);

            if (intPipelineNumberStart > intPipelineNumberEnd)
                throw new Exception("起始单证号必须大于截止单证号！");

            Int64 count = intPipelineNumberEnd - intPipelineNumberStart + 1;
            if (count > 10000)
                throw new Exception("号段范围太大可能导致服务器停止响应，建议分批次创建，一次创建的号码不要超过一万个！");

            string[] arrange = new string[count];
            int index = 0;

            for (Int64 i = intPipelineNumberStart; i <= intPipelineNumberEnd; i++)
            {
                string number = i.ToString().PadLeft(numberLenth, '0');
                string pipeLineNumber = strPipelinePrefix + number;
                arrange[index++] = pipeLineNumber.ToUpper();
            }

            return arrange;
        }

        /// <summary>
        /// 截取字符串中的数字部分
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string InterceptNumber(string source)
        {
            Regex regex = new Regex(@"(-?\d+)(\.\d+)?");
            Match match = regex.Match(source);

            if (match.Success)
                return match.Value;
            else
                return string.Empty;
        }

        public static void ExportExcelFromGridView(System.Web.UI.WebControls.GridView gv)
        {
            string filename = HttpUtility.UrlEncode("IA" + DateTime.Now.ToString("yyyy-MM-dd-hhmmss") + ".xls", System.Text.Encoding.GetEncoding("utf-8"));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\">");
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

            gv.RenderControl(htmlWrite);
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();
        }

        public static void ExportExcelXMLFromDataSet(DataSet ds)
        {
            DataSet objDataset = ds;


            //Create   the   FileStream   to   write   with.     
            System.IO.FileStream fs = new System.IO.FileStream(
                  "C:\\test.xml", System.IO.FileMode.Create);

            //Create   an   XmlTextWriter   for   the   FileStream.     
            System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(
                  fs, System.Text.Encoding.Unicode);

            //Add   processing   instructions   to   the   beginning   of   the   XML   file,   one     
            //of   which   indicates   a   style   sheet.     
            xtw.WriteProcessingInstruction("xml", "version='1.0'");
            //xtw.WriteProcessingInstruction("xml-stylesheet",
            //   "type='text/xsl'   href='customers.xsl'");     

            //Write   the   XML   from   the   dataset   to   the   file.     
            objDataset.WriteXml(xtw);
            xtw.Close();
            Common.DownLoadFile("c:\\test.xml");
        }

        public static void ExportCSVFromDataSet(DataSet ds)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            string data = string.Empty;

            foreach (DataTable tb in ds.Tables)
            {
                //写出列名 
                foreach (DataColumn column in tb.Columns)
                {
                    data += column.ColumnName + ",";
                }

                sw.WriteLine(data);
                data = string.Empty;

                //写出数据 
                foreach (DataRowView drv in tb.DefaultView)//已排序
                {
                    for (int i = 0; i < tb.Columns.Count; i++)
                    {
                        data += drv[i].ToString() + ",";
                    }

                    sw.WriteLine(data);
                    data = string.Empty;
                }
            }

            sw.Close();

            string filename = DateTime.Now.ToString("yyyy-MM-dd-hhmmss") + ".csv";
            filename = HttpUtility.UrlEncode(filename, System.Text.Encoding.GetEncoding("GB2312"));

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.Write(sw);
            HttpContext.Current.Response.End();
        }

        public static void DownLoadFile(string filePath)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fi.Name, System.Text.Encoding.Default));
            HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
            HttpContext.Current.Response.WriteFile(fi.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static void LogIt(object text)
        {
            //Thread th = new Thread(new ParameterizedThreadStart(LogThread));
            //th.Start(text);
            LogThread(text);
        }

        private static void LogThread(object obj)
        {
            //try
            //{
            //    //发现在多线程中，会导致HttpContext.Current为null值而报错,故把path放到全局变量中
            //    string logPath = Path.Combine(ServerPath, "Log/");
            //    if (!Directory.Exists(logPath))
            //        Directory.CreateDirectory(logPath);

            //    string filename = Path.Combine(logPath, DateTime.Today.ToString("yyyy-MM-dd") + ".log");
            //    string text = DateTime.Now.ToLongTimeString() + Environment.NewLine + obj + Environment.NewLine + Environment.NewLine;
            //    File.AppendAllText(filename, text);
            //}
            //catch { }
            log.Debug(obj);
        }


        /// <summary>
        /// 若字串中有全角则替换成半角
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Full2Half(string text)
        {
            string 全角 = "１２３４５６７８９０ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ，　－（）＿＋＝！＠＃￥％＾＆＊｛｝。＜＞？";
            string 半角 = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ, -()_+=!@#$%^&*{}.<>?";
            for (int i = 0; i < 全角.Length; i++)
            {
                text = text.Replace(全角[i], 半角[i]);
            }
            string ret = text;
            return ret;
        }

        /// <summary>
        /// 获取一个类的实例内所有属性、字段的名称和值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetClassInfo<T>(T t)
        {
            if (t == null)
                throw new Exception("反射失败，对象的实例为空！");

            StringBuilder tStr = new StringBuilder();

            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr.AppendLine(string.Format("{0}:{1},", name, value));
                }
                else//引用类型
                {
                    if (value != null)
                        tStr.AppendLine(GetClassInfo(value));
                    else
                        tStr.AppendLine(string.Format("{0}:null,", name));
                }
            }

            System.Reflection.FieldInfo[] fields = t.GetType().GetFields();
            foreach (System.Reflection.FieldInfo item in fields)
            {
                string name = item.Name;
                object value = item.GetValue(t);
                if (item.FieldType.IsValueType || item.FieldType.Name.StartsWith("String"))
                {
                    tStr.AppendLine(string.Format("{0}:{1},", name, value));
                }
                else
                {
                    if (value != null)
                        tStr.AppendLine(GetClassInfo(value));
                    else
                        tStr.AppendLine(string.Format("{0}:null,", name));
                }
            }

            return tStr.ToString();
            //Type t = typeof(TransportAccidentResponseDto);
            //var ms = t.GetMethods();  //所有方法
            //var fs = t.GetFields();   //所有字段
            //var ps = t.GetProperties();   //所有属性
            //var es = t.GetEvents();     //所有事件
            //var mb = t.GetMembers();     //
        }

        /// <summary>
        /// 序列化一个类的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string XmlSerializer<T>(T t)
        {
            if (t == null)
                throw new Exception("对象的实例为空，无法序列化！");

            XmlSerializer serializer = new XmlSerializer(t.GetType(), string.Empty);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, t, ns);
            return sw.ToString();
        }

        public static string SoapSerialize<T>(T t)
        {
            if (t == null)
                throw new Exception("对象的实例为空，无法序列化！");

            SoapFormatter serializer = new SoapFormatter();

            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, t);
            return Encoding.UTF8.GetString(stream.ToArray());//Encoding.Default 序列化“证件类型”IdentityType类的时候出现乱码
        }
    }
//}