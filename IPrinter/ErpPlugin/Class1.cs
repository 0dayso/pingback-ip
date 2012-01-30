using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace ErpPlugin
{
    /// <summary>
    /// 机票录入单
    /// </summary>
    public class Class1
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]   //找子窗体   
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("HtmlDoc", EntryPoint = "MyGetHTMLDocInterface")]
        private static extern IHTMLDocument2 MyGetHTMLDocInterface(IntPtr hWnd);

        static public void SetTicketRecordSheet(string psgName, string sail, string pnr, string date1, string hour1, string minute1, string date2, string hour2, string minute2, string carrier1, string flightno1, string bunk1, string carrier2, string flightno2, string bunk2)
        {
            IntPtr ParenthWnd = new IntPtr(0);
            IntPtr ShellWnd = new IntPtr(0);
            IntPtr EndhWnd = new IntPtr(0);
            string lpszParentWindow = "机票单录入 - Microsoft Internet Explorer";
            ParenthWnd = FindWindow("IEFrame", lpszParentWindow);
            if (ParenthWnd == (IntPtr)(0))
            {
                MessageBox.Show("未找到－机票单录入－窗口");
                return;
            }
            ShellWnd = FindWindowEx(ParenthWnd, new IntPtr(0), "Shell DocObject View", null);
            if (ShellWnd == (IntPtr)(0))
            {
                MessageBox.Show("未找到－机票单录入－窗口");
                return;
            }
            EndhWnd = FindWindowEx(ShellWnd, new IntPtr(0), "Internet Explorer_Server", null);
            if (EndhWnd == (IntPtr)(0))
            {
                MessageBox.Show("未找到－机票单录入－窗口");
                return;
            }
            IHTMLDocument2 hDoc = MyGetHTMLDocInterface(EndhWnd);
            if (hDoc == null) return;
            name = psgName;
            s = sail;
            p = pnr;
            d1 = date1;
            d2 = date2;
            h1 = hour1;
            h2 = hour2;
            m1 = minute1;
            m2 = minute2;
            c1 = carrier1;
            c2 = carrier2;
            f1 = flightno1;
            f2 = flightno2;
            b1 = bunk1;
            b2 = bunk2;
            hdoc = hDoc;
            Thread th = new Thread(new ThreadStart (Class1.set机票录入单controls));
            th.Start();
            
        }
        static string name;
        static string s;
        static string p;
        static string d1;
        static string h1;
        static string m1;
        static string c1;
        static string f1;
        static string b1;
        static string d2;
        static string h2;
        static string m2;
        static string c2;
        static string f2;
        static string b2;
        static IHTMLDocument2 hdoc;


        static void set机票录入单controls()
        {
            for (int i = 0; i < hdoc.all.length; i++)
            {
                IHTMLElement elem = (IHTMLElement)hdoc.all.item(i, i);

                if (elem.id == "txtPasname")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = name;
                }
                if (elem.id == "txtRoute")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = s;
                }
                if (elem.id == "txtRecno")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = p;
                }
                if (elem.id == "txtBeg_date1")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = d1;
                }
                if (elem.id == "txtBeg_date1_1")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = h1;
                }
                if (elem.id == "txtBeg_date1_2")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = m1;
                }
                if (elem.id == "txtBeg_date2")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = d2;
                }
                if (elem.id == "txtBeg_date2_1")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = h2;
                }
                if (elem.id == "txtBeg_date2_2")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = m2;
                }
                if (elem.id == "txtAirno")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = c1;
                }
                if (elem.id == "txtFlightno1")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = f1;
                }
                if (elem.id == "txtClassno")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = b1;
                }
                if (elem.id == "txtFlightno2")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = c2;
                }
                if (elem.id == "txtFlightno3")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = f2;
                }
                if (elem.id == "txtFlightno4")
                {
                    IHTMLInputElement inputelem = (IHTMLInputElement)elem;
                    inputelem.value = b2;
                }
            }
        }
    }
}
