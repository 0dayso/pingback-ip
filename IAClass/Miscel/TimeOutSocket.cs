using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IAClass
{
    public class TimeOutSocket
    {
        public static TcpClient Connect(IPEndPoint remoteEndPoint, int timeoutMSec)
        {
            TcpClient tcpclient = new TcpClient();

            IAsyncResult asyncResult = tcpclient.BeginConnect(remoteEndPoint.Address, remoteEndPoint.Port, null, null);

            if (asyncResult.AsyncWaitHandle.WaitOne(timeoutMSec, false))
            {
                try
                {
                    tcpclient.EndConnect(asyncResult);
                    return tcpclient;
                }
                catch
                {
                    tcpclient.Close();
                    tcpclient = null;
                    throw;
                }
            }
            else
            {
                tcpclient.Close();
                tcpclient = null;
                throw new TimeoutException("TimeOut Exception");
            }
        }
    }
}
