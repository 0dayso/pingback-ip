using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace AutoUpdate
{
    static class Program
    {
        static Mutex mutex = new Mutex(false, "f880a127-9d21-4907-aa83-6041dc0faa4a");

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Common.LogWrite("Starting AutoUpdate...");

            if (!mutex.WaitOne(5000, false)) //等待5秒, 如果有相同实例运行则给用户提示
            {
                Common.LogWrite("程序已存在运行中的实例。");
                return;
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new fmMain());
            }
            finally { mutex.ReleaseMutex(); }
        }
    }
}
