using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ePlus.Model
{
    class Tpr
    {
        //public static Tpr context = null;
        public static bool b������ռ���� = false;
        public static string tprResult = "";
        public static bool running = false;
        public Tpr()
        {
            //context = this;
        }
        static public void run(string tprcmd)
        {
            if (Tpr.running) return;
            Tpr.running = true;
            Tpr.tprResult = "";
            EagleAPI.EagleSendOneCmd("i~" + tprcmd);
        }
        static public void run()
        {
            if (Tpr.running) return;
            Tpr.running = true;
            Tpr.tprResult = "";
            TprCmd tc = new TprCmd();
            if (tc.ShowDialog() == DialogResult.OK)
                EagleAPI.EagleSendOneCmd("i~" + tc.cmd);
        }
        static public void run1(string tslcmd)
        {
            if (Tpr.running) return;
            Tpr.running = true;
            Tpr.tprResult = "";
            TprCmd tc = new TprCmd(tslcmd);
            if (tc.ShowDialog() == DialogResult.OK)
                EagleAPI.EagleSendOneCmd("i~" + tc.cmd);
        }
        public static string retstring
        {
            set
            {
                tprResult += connect_4_Command.AV_String;
                string temp = mystring.trim(connect_4_Command.AV_String);
                temp = mystring.trim(temp, '>');
                temp = mystring.trim(temp);
                if (temp[temp.Length - 1] == '+') EagleAPI.EagleSendOneCmd("pn");
                else
                {
                    Tpr.saveTpr();
                    Tpr.stop();
                }
            }
        }
        public static void stop()
        {
            Tpr.running = false;
        }
        public static bool bSaving = false;
        public static void saveTpr()
        {
            if (bSaving) return;
            try
            {
                bSaving = true;
                string content = tprResult;
                string str0 = "DATE   : ";
                string str1 = "AIRLINE:   ";
                int pos0 = content.IndexOf(str0);
                int pos1 = content.IndexOf(str1);
                if (pos0 < 0 || pos1 < 0 || pos1 <= pos0) throw new Exception("ȡ���������ļ�����");
                else
                {
                    string saleofdate = (content.Substring(pos0 + str0.Length, pos1 - pos0 - str0.Length).Trim());
                    str0 = "OFFICE :";
                    str1 = "IATA NUMBER :";
                    pos0 = content.IndexOf(str0);
                    pos1 = content.IndexOf(str1);
                    string officenumber = "";
                    if (pos0 < 0 || pos1 < 0 || pos1 <= pos0) throw new Exception("ȡ�ϣƣƣɣãźŴ���");
                    else officenumber = (content.Substring(pos0 + str0.Length, pos1 - pos0 - str0.Length).Trim());
                    saleofdate = officenumber + "-" + saleofdate;
                    string dir = Application.StartupPath + "\\tpr";
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    int serial = 0;
                    while (File.Exists(dir + "\\" + saleofdate + serial.ToString("d2") + ".tpr"))
                    {
                        serial++;
                    }
                    FileStream fs = new FileStream(dir + "\\" + saleofdate + serial.ToString("d2") + ".tpr", FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(content);
                    string tmpFn = fs.Name;
                    sw.Close();
                    fs.Close();
                    if (MessageBox.Show("�ļ��Ѿ�����Ϊ:" + tmpFn + "  �Ƿ���Ҫ��?",
                        "ע��", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        == DialogResult.Yes)
                    {
                        EagleAPI.LogRead(tmpFn);
                    }
                }
                b������ռ���� = false;
            }
            catch (Exception ex)
            {
                try
                {
                    GlobalVar.stdRichTB.AppendText(ex.Message);
                }
                catch
                {
                }
            }
            Tpr.running = false;
            bSaving = false;
        }
    }
}
