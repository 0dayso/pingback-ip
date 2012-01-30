using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;


namespace EagleControls
{
    /// <summary>
    /// 快捷指令，快捷功能，指令功能设置
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
            //HOTKEY开头的项全部删掉
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
        /// 快键响应
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
        打印行程单 = 0,             //--mnPrint Finished                 toolbar
        
        设置黑屏界面 = 1,           //--mnWindow          7
        隐藏右面板 = 2,             //--mnWindow          5
        显示右面板 = 3,             //--mnWindow          4
        黑屏全屏化 = 4,             //--mnWindow          2
        黑屏还原全屏 = 5,           //--mnWindow          3
        上一条指令 = 6,             //--mnOperation 6
        下一条指令 = 7,             //--mnOperation 7
        查看今天日志 = 8,           //--mnFile Finished 
        查看日志 = 9,               //--mnFile Finished 
        查看余额 = 10,              //--mnFile Finished 
        清屏 = 11,                  //--mnWindow           1                     toolbar

        界面_中文专业版 = 12,            //--
        界面_后台管理 = 13,              //--
        界面_对帐平台 = 14,              //--
        手动退票 = 15,              //--mnOperation 2                     toolbar
        一键出票 = 16,              //--mnOperation 1                     toolbar
        自动清Q = 17,               //--mnOperation 4
        PNR订单提交 = 18,           //--mnOperation 3                     toolbar
        修改B2B帐号密码 = 19,       //--mnFile Finished
        计算器 = 20,                //--mnHelp                             1
        记事本 = 21,                //--mnHelp                             2
        画板 = 22,                  //--mnHelp                             3
        大编转小编 = 23,            //--mnOperation 5
        切换可用配置 = 24,          //--mnOperation 0     Finished                toolbar
        //25空出,有重复被删除
        黑屏快捷键设置 = 26,        //--mnWindow            6
        B2C设置 = 27,               //--mnHelp                             4
        查看可使用指令 = 28,        //--mnFile Finished
        切换IBE或配置 = 29,         //--mnOperation 8
        
        界面_黑屏 = 30,              //--
        打印保险单 = 31                //--mnPrint Finished 
    }

}
/*
HOTKEY1CTRL+ALT+A=av h
HOTKEY2CTRL+P=0001
HOTKEY3eg print=0001           0001指向打印功能
*/