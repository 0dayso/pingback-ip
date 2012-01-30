using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace EagleBase
{
    class egbase
    {
        public egbase()
        {
            INIT_KEYVALUE();
        }
        /// <summary>
        /// KeyCode/KeyString
        /// </summary>
        static public Hashtable KEYVALUE = new Hashtable();

        private void INIT_KEYVALUE()
        {
            for (int i = 65; i <= 90; ++i)
            {
                KEYVALUE.Add(i, Convert.ToChar(i).ToString());
            }
            for (int i = 48; i <= 57; ++i)
            {
                KEYVALUE.Add(i, Convert.ToChar(i).ToString());
            }
            for (int i = 96; i < 105; ++i)
            {
                KEYVALUE.Add(i, "NUM" + Convert.ToString(i - 96));
            }
            KEYVALUE.Add(106, "NUM*");
            KEYVALUE.Add(107, "NUM+");
            KEYVALUE.Add(108, "NUM ENTER");
            KEYVALUE.Add(109, "NUM-");
            KEYVALUE.Add(110, "NUM.");
            KEYVALUE.Add(111, "NUM/");
            for (int i = 1; i <= 12; ++i)
            {
                KEYVALUE.Add(i + 111, "F" + i.ToString());
            }
            KEYVALUE.Add(8, "BACKSPACE");
            KEYVALUE.Add(9, "TAB");
            KEYVALUE.Add(12, "CLEAR");
            KEYVALUE.Add(13, "ENTER");
            KEYVALUE.Add(16, "SHIFT");
            KEYVALUE.Add(17, "CTRL");
            KEYVALUE.Add(18, "ALT");
            KEYVALUE.Add(20, "CAPSLOCK");
            KEYVALUE.Add(27, "ESC");
            KEYVALUE.Add(32, "SAPCEBAR");
            KEYVALUE.Add(33, "PAGEUP");
            KEYVALUE.Add(34, "PAGEDOWN");
            KEYVALUE.Add(35, "END");
            KEYVALUE.Add(36, "HOME");
            KEYVALUE.Add(37, "LEFTARROW");
            KEYVALUE.Add(38, "UPARROW");
            KEYVALUE.Add(39, "RIGHTARROW");
            KEYVALUE.Add(40, "DOWNARROW");
            KEYVALUE.Add(45, "INSERT");
            KEYVALUE.Add(46, "DELETE");
            KEYVALUE.Add(186, ";");
            KEYVALUE.Add(187, "=");
            KEYVALUE.Add(188, ",");
            KEYVALUE.Add(189, "-");
            KEYVALUE.Add(190, ".");
            KEYVALUE.Add(191, "/");
            KEYVALUE.Add(192, "~");//`
            KEYVALUE.Add(219, "[");
            KEYVALUE.Add(220, "\\");
            KEYVALUE.Add(221, "]");
            KEYVALUE.Add(222, "'");
        }
    }

}
