using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringHelper
{
    public static class MiscelHelper
    {
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

        /// <summary>
        /// 截取字符串中的汉字部分
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string InterceptChinese(string source)
        {
            Regex regex = new Regex(@"[\u4e00-\u9fa5]+");
            Match match = regex.Match(source);

            if (match.Success)
                return match.Value;
            else
                return string.Empty;
        }

        public static bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
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
    }

}
