///Version 1.0
///Auther Eric.Chen
///�����ӿ�Ʊ��û�д��г̵����ࡣ
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
        private static string strCurEtkNum="";//��ǰ��Ʊ��
        private static string strCurResult="";//���ص�ǰƱ����Ϣ
        private static ArrayList alPrinted;//�Ѿ���ӡ�����г̵�
        public static ArrayList alEtn;//������
        public static ArrayList alCheckedEtn;//δ�򵥵��б�
        public static int nCheckFlag = 0;//�Ƿ��ڼ��ı�־
        public static int intDlay = 500;//ÿ��ָ���ӳ�ʱ��
        public static int intAmount = 0;//��������
        public static int intCurCount = 0;//��ǰ���е���
        public static bool bImport = false;
        public CheckPrintObject()
        {
            alEtn = new ArrayList();
        }
        /// <summary>
        /// ��ʼ���
        /// </summary>
        public static void StartCheck()
        {
            nCheckFlag = 1;//�ÿ�ʼ����־λ
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
        /// ���ռ�鷵�ص�����
        /// </summary>
        /// <param name="detr"></param>
        public static void ReceiveData(string detr)
        {
            try
            {
                string tem = "";
                if (detr == "")
                {
                    MessageBox.Show(CheckPrintObject.strCurEtkNum + "���г̵���Ϣδ����,���¼���ֶ����.");
                    return;
                }
                int nRcpt = detr.IndexOf("RCPT");
                if (nRcpt != -1)
                {
                    tem = detr.Substring(nRcpt + 4);
                    int nResult = tem.IndexOf("RP");
                    if (nResult == -1)
                    {
                        //δ���
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
        /// ����Excel����
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
                string tablename = schematable.Rows[0][2].ToString().Trim();//ȡ�ù������б������
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
                        alEtn.Add(etk);//����Ʊ��ȡ����,����ArrayList
                    }
                    catch (Exception ee)
                    {
                       // MessageBox.Show(ee.Message);
                        continue;
                    }

                }
                bImport = true;
                intAmount = alEtn.Count;//���������
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
        /// ������
        /// </summary>
        /// <returns></returns>
        public bool WriteResult(int n)//д��־,����N,1��ʾ��дδ���Ʊ��,2��ʾ��д�Ѵ�����г̵�
        {
            string info="";
           
            string path = Application.StartupPath;
            if (!Directory.Exists(path + "\\Log"))
                Directory.CreateDirectory(path + "\\Log");

            //b.�����ļ����Ƿ����
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
                        info = "δ����г̵��ĵ��ӿ�Ʊ��:\r\n";
                        for (int i = 0; i < alCheckedEtn.Count; i++)
                        {
                            info = new StringBuilder().Append(info).Append(alCheckedEtn[i].ToString() + "\r\n").ToString();
                        }
                        info += "��" + alCheckedEtn.Count.ToString() + "������";
                    }
                    else if (n == 2)
                    {
                        info = "���ù����г̵���:\r\n";
                        for (int i = 0; i < alPrinted.Count; i++)
                        {
                            info = new StringBuilder().Append(info).Append(alPrinted[i].ToString() + "\r\n").ToString();
                        }
                        info += "��" + alPrinted.Count.ToString() + "������";
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
                    MessageBox.Show("д��־��������ϵϵͳ����Ա��");
                    return false;
                }
            }
            return true;
        }
    }
}
