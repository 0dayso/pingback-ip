using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Options
{
    public class gbOption
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }

        public class Win32
        {
            [DllImport("Kernel32.dll")]
            public static extern bool SetSystemTime(ref SystemTime sysTime);
            [DllImport("Kernel32.dll")]
            public static extern void GetSystemTime(ref SystemTime sysTime);
        } 
        static public void syncLocalTimeWithServerTime()
        {
            try
            {
                wsIBE.HiWSService ii = new wsIBE.HiWSService();
                DateTime serverDT = DateTime.Parse(ii.wsGetServerTime());
                serverDT = serverDT.AddHours(-8);
                SystemTime st = new SystemTime();
                st.wYear = (ushort)serverDT.Year;
                st.wMonth = (ushort)serverDT.Month;
                st.wDay = (ushort)serverDT.Day;
                st.wHour = (ushort)serverDT.Hour;
                st.wMinute = (ushort)serverDT.Minute;
                st.wSecond = (ushort)serverDT.Second;
                st.wDayOfWeek = (ushort)serverDT.DayOfWeek;
                st.wMiliseconds = (ushort)serverDT.Millisecond;
                Win32.SetSystemTime(ref st);
            }
            catch { }
        }
        static public void isDifferCompareTimeWithServerThan12()
        {
            //try
            //{
            //    wsIBE.HiWSService ii = new wsIBE.HiWSService();
            //    DateTime serverDT = DateTime.Parse(ii.wsGetServerTime());
            //    DateTime localDT = DateTime.Now;
            //    TimeSpan ts = localDT - serverDT;
            //    if (Math.Abs(ts.TotalHours) > 24)
            //    {
            //        if (MessageBox.Show("您的系统时间可能不正确,是否设置为北京时间?其它时间请手动设置系统时间", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            serverDT = serverDT.AddHours(-8);
            //            SystemTime st = new SystemTime();
            //            st.wYear = (ushort)serverDT.Year;
            //            st.wMonth = (ushort)serverDT.Month;
            //            st.wDay = (ushort)serverDT.Day;
            //            st.wHour = (ushort)serverDT.Hour;
            //            st.wMinute = (ushort)serverDT.Minute;
            //            st.wSecond = (ushort)serverDT.Second;
            //            st.wDayOfWeek = (ushort)serverDT.DayOfWeek;
            //            st.wMiliseconds = (ushort)serverDT.Millisecond;
            //            Win32.SetSystemTime(ref st);
            //            MessageBox.Show("恭喜,北京时间设置成功!");
            //        }
            //    }
            //}
            //catch { }
        }
    }
}
