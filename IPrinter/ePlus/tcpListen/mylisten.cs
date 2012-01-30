
namespace ePlus.tcpListen
{
    using System;
    using System.Windows.Forms;
    using System.Net.Sockets;
    using System.Net;
    using Leon.RawThread;
    using System.Threading;
    class mylisten
    {
        Thread Listener = null;
        RawWorker ClassWorker = new RawWorker();

        public string ipSource = "";
        public int portSource = 0;
        public string ipDest = "";
        public int portDest = 0;

        public void StartWork()
        {
            AbortWork();
            ClassWorker.SelectedIP = ipDest;
            ClassWorker.ipDest = ipDest;
            ClassWorker.ipSource = ipSource;
            ClassWorker.portDest = portDest;
            ClassWorker.portSource = portSource;
            Listener = new Thread(new ThreadStart(ClassWorker.Run));
            Listener.Start();
        }
        public void StopWork()
        {
            AbortWork();
        }
        public void AbortWork()
        {
            if (Listener != null)
            {
                Listener.Abort();
                Listener.Join();
                Listener = null;
            }
        }
    }
}
