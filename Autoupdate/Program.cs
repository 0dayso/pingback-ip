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
            if (!mutex.WaitOne(5000, false)) //等待5秒, 如果有相同实例运行则给用户提示
            {
                //MessageBox.Show("程序已在运行，如果仍有问题，请检查是否已在系统进程中运行。");
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
