using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ePlus
{
    class Hook
    {
        [DllImport("kernel32")]
        public static extern int GetCurrentThreadId();
        [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(HookType idHook, HOOKPROC lpfn, int hmod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, int lParam);  


        static int hHook = 0;

        public enum HookType
        {
            WH_KEYBOARD = 2
        }
        public delegate int HOOKPROC(int nCode, int wParam, int lParam);

        public void SetHook()
        {
            // set the keyboard hook
            if (hHook == 0)
            {
                //System.Windows.Forms.MessageBox.Show("安装hook");
                hHook = SetWindowsHookEx(HookType.WH_KEYBOARD, new HOOKPROC(this.MyKeyboardProc), 0, GetCurrentThreadId());//安装勾子
                //System.Windows.Forms.MessageBox.Show("hook号为" + hHook.ToString());
                if (hHook == 0) System.Windows.Forms.MessageBox.Show("HOOK FAILED");//若hHook=0，表示安装失败

            }
            else//已经存在勾子
            {
                bool ret = UnhookWindowsHookEx(hHook);//卸掉勾子
                if (ret == false)//失败
                {
                    System.Windows.Forms.MessageBox.Show("UnhookWindowsHookEx Failed");
                    return;
                }
                //成功
                hHook = 0;
                System.Windows.Forms.MessageBox.Show("Set Windows Hook");
                
            }
        }
        public void unSetHook()
        {
            try
            {
                System.Windows.Forms.MessageBox.Show("卸掉hook");
                if (UnhookWindowsHookEx(hHook)) hHook = 0;
            }
            catch
            {
            }
        }
        public int MyKeyboardProc(int nCode, int wParam, int lParam)
        {
            //System.Windows.Forms.MessageBox.Show("调用");
            string temp = nCode.ToString() +"-"+ wParam.ToString() +"-"+ lParam.ToString();
            if (temp == "0-13-1835009" || temp == "0-13--1071906815")//大回车
            {
                GlobalVar.b_NumpadEnter = false;
                System.Windows.Forms.MessageBox.Show("大");
                //return 0;
            }
            if (temp == "0-13-18612225" || temp == "0-13--1055129599")//小回车
            {
                GlobalVar.b_NumpadEnter = true;
                System.Windows.Forms.MessageBox.Show("小");
                //return 0;
                
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam); 
        }
        //对“Eagle!ePlus.Hook+HOOKPROC::Invoke”类型的已垃圾回收委托进行了回调。这可能会导致应用程序崩溃、损坏和数据丢失。向非托管代码传递委托时，托管应用程序必须让这些委托保持活动状态，直到确信不会再次调用它们。

    }
}
