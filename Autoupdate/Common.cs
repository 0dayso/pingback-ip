using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AutoUpdate
{
    class Common
    {
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
    }
}
