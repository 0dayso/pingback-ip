using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace EagleBase
{

    public class DllApi
    {


        [DllImport("kernel32", EntryPoint = "CopyMemory")]
        public static extern void CopyMemory(byte[] Destination, object Source, int Length);
        [DllImport("kernel32")]
        public static extern int GetCurrentThreadId();
        [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(HookType idHook, HOOK.HOOKPROC lpfn, int hmod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, int lParam);



        
    }

    public enum HookType
    {
        WH_KEYBOARD = 2
    }
    public class HOOK
    {
        public delegate int HOOKPROC(int nCode, int wParam, int lParam);
    }
}
