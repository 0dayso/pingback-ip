using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace ePlus
{
    public class LogForPrintReceipt
    {
        public string info;

        public void WritePrintLog()
        {
            string path = Application.StartupPath;
            if (!Directory.Exists(path + "\\Log"))
                Directory.CreateDirectory(path + "\\Log");

            //b.�����ļ����Ƿ����
            string filename = path + "\\Log\\PrintReceipt" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            bool bwrited = false;
            while (!bwrited)
            {
                try
                {
                    FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write("\r\n");
                    sw.Write(System.DateTime.Now.ToLongTimeString() + info);
                    sw.Close();
                    fs.Close();
                    bwrited = true;
                   
                }
                catch
                {
                    MessageBox.Show("�г̵���ӡ��־��������ϵϵͳ����Ա��");
                   
                }
            }
          
        }
    }
}
