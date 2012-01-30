using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;


namespace EagleControls
{
    /// <summary>
    /// ���ָ���ݹ��ܣ�ָ�������
    /// </summary>
    public class BlackWindowHotKey
    {
        public Hashtable HOTKEY1 = new Hashtable();
        public Hashtable HOTKEY2 = new Hashtable();
        public Hashtable HOTKEY3 = new Hashtable();
        public BlackWindowHotKey()
        {
            LoadHotKey();
        }
        void LoadHotKey()
        {
            string f = Application.StartupPath + "\\eagle.txt";
            Hashtable ht = EagleString.EagleFileIO.ReadEagleDotTxtFileToHashTable();
            foreach (DictionaryEntry de in ht)
            {
                string key = de.Key.ToString().ToUpper().Trim();
                if (key.IndexOf("HOTKEY1") == 0)
                {
                    HOTKEY1.Add(key.Substring(7), de.Value.ToString());
                }
                else if (key.IndexOf("HOTKEY2") == 0)
                {
                    HOTKEY2.Add(key.Substring(7), de.Value.ToString());
                }
                else if (key.IndexOf("HOTKEY3") == 0)
                {
                    HOTKEY3.Add(key.Substring(7), de.Value.ToString());
                }
            }
        }
        void SaveHotKey()
        {
            //HOTKEY��ͷ����ȫ��ɾ��
            string f = Application.StartupPath + "\\eagle.txt";
            string s = System.IO.File.ReadAllText(f);
            string[] a = s.Split(new string[] { "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            List<string> ls = new List<string>();
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i].IndexOf("HOTKEY") != 0)
                {
                    ls.Add(a[i]);
                }
            }
            System.IO.File.WriteAllLines(f, ls.ToArray(),Encoding.Default);
            
                
            

            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(HOTKEY1, "HOTKEY1");
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(HOTKEY2, "HOTKEY2");
            EagleString.EagleFileIO.WiteHashTableToEagleDotTxt(HOTKEY3, "HOTKEY3");
        }
        public void SetHotKey()
        {
            BlackWindowHotKeySetup dlg = new BlackWindowHotKeySetup(HOTKEY1, HOTKEY2, HOTKEY3);
            dlg.ShowDialog();
            HOTKEY1 = dlg.m_ht1;
            HOTKEY2 = dlg.m_ht2;
            HOTKEY3 = dlg.m_ht3;
            SaveHotKey();
        }
        /// <summary>
        /// �����Ӧ
        /// </summary>
        /// <param name="value"></param>
        public void HandleShortCut(string value)
        {
            byte b = byte.Parse(value);
            FUNCTION_ID fi = (FUNCTION_ID)b;
            throw new Exception(fi.ToString());
        }

    }
    public enum FUNCTION_ID : byte
    {
        ��ӡ�г̵� = 0,             //--mnPrint Finished                 toolbar
        
        ���ú������� = 1,           //--mnWindow          7
        ��������� = 2,             //--mnWindow          5
        ��ʾ����� = 3,             //--mnWindow          4
        ����ȫ���� = 4,             //--mnWindow          2
        ������ԭȫ�� = 5,           //--mnWindow          3
        ��һ��ָ�� = 6,             //--mnOperation 6
        ��һ��ָ�� = 7,             //--mnOperation 7
        �鿴������־ = 8,           //--mnFile Finished 
        �鿴��־ = 9,               //--mnFile Finished 
        �鿴��� = 10,              //--mnFile Finished 
        ���� = 11,                  //--mnWindow           1                     toolbar

        ����_����רҵ�� = 12,            //--
        ����_��̨���� = 13,              //--
        ����_����ƽ̨ = 14,              //--
        �ֶ���Ʊ = 15,              //--mnOperation 2                     toolbar
        һ����Ʊ = 16,              //--mnOperation 1                     toolbar
        �Զ���Q = 17,               //--mnOperation 4
        PNR�����ύ = 18,           //--mnOperation 3                     toolbar
        �޸�B2B�ʺ����� = 19,       //--mnFile Finished
        ������ = 20,                //--mnHelp                             1
        ���±� = 21,                //--mnHelp                             2
        ���� = 22,                  //--mnHelp                             3
        ���תС�� = 23,            //--mnOperation 5
        �л��������� = 24,          //--mnOperation 0     Finished                toolbar
        //25�ճ�,���ظ���ɾ��
        ������ݼ����� = 26,        //--mnWindow            6
        B2C���� = 27,               //--mnHelp                             4
        �鿴��ʹ��ָ�� = 28,        //--mnFile Finished
        �л�IBE������ = 29,         //--mnOperation 8
        
        ����_���� = 30,              //--
        ��ӡ���յ� = 31                //--mnPrint Finished 
    }

}
/*
HOTKEY1CTRL+ALT+A=av h
HOTKEY2CTRL+P=0001
HOTKEY3eg print=0001           0001ָ���ӡ����
*/