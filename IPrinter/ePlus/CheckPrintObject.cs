///Version 1.0
///Auther Eric.Chen
///检查电子客票有没有打行程单的类。
///2008.11.1  14:21
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading;

namespace ePlus
{
    class CheckPrintObject
    {
        private static string strCurEtkNum="";//当前客票号
        private static string strCurResult="";//返回当前票的信息
        private static ArrayList alPrinted;//已经打印过的行程单
        public static ArrayList alEtn;//待检查表
        public static ArrayList alCheckedEtn;//未打单的列表
        public static int nCheckFlag = 0;//是否在检查的标志
        public static int intDlay = 500;//每条指令延迟时间
        public static int intAmount = 0;//总数据数
        public static int intCurCount = 0;//当前进行的数
        public static bool bImport = false;
        public CheckPrintObject()
        {
            alEtn = new ArrayList();
        }
        /// <summary>
        /// 开始检查
        /// </summary>
        public static void StartCheck()
        {
            nCheckFlag = 1;//置开始检查标志位
            alCheckedEtn = new ArrayList();
            alPrinted = new ArrayList();
            for (int i = 0; i < alEtn.Count; i++)
            {
                try
                {
                    strCurEtkNum = alEtn[i].ToString();
                    EagleAPI.EagleSendOneCmd("detr tn/" + CheckPrintObject.strCurEtkNum.Trim() + ",f");
                    intCurCount = i+1;
                    Thread.Sleep(intDlay);
                }
                catch (Exception ea)
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// 接收检查返回的数据
        /// </summary>
        /// <param name="detr"></param>
        public static void ReceiveData(string detr)
        {
            try
            {
                string tem = "";
                if (detr == "")
                {
                    MessageBox.Show(CheckPrintObject.strCurEtkNum + "的行程单信息未返回,请记录后手动检查.");
                    return;
                }
                int nRcpt = detr.IndexOf("RCPT");
                if (nRcpt != -1)
                {
                    tem = detr.Substring(nRcpt + 4);
                    int nResult = tem.IndexOf("RP");
                    if (nResult == -1)
                    {
                        //未打过
                        alCheckedEtn.Add(CheckPrintObject.strCurEtkNum);
                    }
                    else
                    {
                        alPrinted.Add(tem.Substring(nResult+2,10));
                    }
                }
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.Message);
            }
   
        }
        /// <summary>
        /// 导入Excel数据
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool ImportExcel(string filepath)
        {

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filepath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                DataTable schematable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables,null);
                string tablename = schematable.Rows[0][2].ToString().Trim();//取得工作簿中表的名字
                strExcel = "select * from ["+tablename+"]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds);
                conn.Close();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        string etk = ds.Tables[0].Rows[i][6].ToString();
                        if (etk != null) etk = etk.Substring(0, 14);
                        if (etk.IndexOf("-") != -1 && etk.Length >= 4)
                            etk = etk.Remove(3, 1);
                        alEtn.Add(etk);//将客票号取出来,导入ArrayList
                    }
                    catch (Exception ee)
                    {
                       // MessageBox.Show(ee.Message);
                        continue;
                    }

                }
                bImport = true;
                intAmount = alEtn.Count;//导入多少条
            }
            catch (Exception aa)
            {
               
                if (conn.State == ConnectionState.Open)
                    conn.Close(); 
                MessageBox.Show(aa.Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 保存结果
        /// </summary>
        /// <returns></returns>
        public bool WriteResult(int n)//写日志,参数N,1表示是写未打的票号,2表示是写已打过的行程单
        {
            string info="";
           
            string path = Application.StartupPath;
            if (!Directory.Exists(path + "\\Log"))
                Directory.CreateDirectory(path + "\\Log");

            //b.当天文件名是否存在
            string filename="";
            if (n == 1)
            {
                filename = path + "\\Log\\NOPrintEtk" + System.DateTime.Now.ToShortDateString() + ".log";
            }
            else if(n==2)
            {
                filename = path + "\\Log\\PrintedReceeipt" + System.DateTime.Now.ToShortDateString() + ".log";
            }
            bool bwrited = false;
            while (!bwrited)
            {
                try
                {
                    if (n == 1)
                    {
                        info = "未打过行程单的电子客票号:\r\n";
                        for (int i = 0; i < alCheckedEtn.Count; i++)
                        {
                            info = new StringBuilder().Append(info).Append(alCheckedEtn[i].ToString() + "\r\n").ToString();
                        }
                        info += "共" + alCheckedEtn.Count.ToString() + "条数据";
                    }
                    else if (n == 2)
                    {
                        info = "已用过的行程单号:\r\n";
                        for (int i = 0; i < alPrinted.Count; i++)
                        {
                            info = new StringBuilder().Append(info).Append(alPrinted[i].ToString() + "\r\n").ToString();
                        }
                        info += "共" + alPrinted.Count.ToString() + "条数据";
                    }
                    FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write("\r\n");
                    sw.Write(info);
                    sw.Close();
                    fs.Close();
                    bwrited = true;

                }
                catch
                {
                    MessageBox.Show("写日志出错，请联系系统管理员！");
                    return false;
                }
            }
            return true;
        }
    }
}
