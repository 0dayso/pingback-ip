using System;
using System.Collections.Generic;
using System.Text;

namespace EagleWebService
{
    public class RemoteKernal
    {
        static string remote_url = Options.GlobalVar.RemotingUrl;

        static EgRClient.RClient rc = new EgRClient.RClient();

        public static RemoteStatus status = RemoteStatus.UNCONNECT;
        public static string RemoteSend(string value)
        {
            if (string.IsNullOrEmpty(remote_url)) remote_url = "tcp://remoting.cn95161.com:8086/EgRServer";
            string dns = EagleString.egString.Between2String(remote_url, "tcp://", ":");
            try
            {
                if (System.Net.Dns.GetHostAddresses(dns).Length <= 0) throw new Exception("DNS RESOLVE ERROR!");
            }
            catch
            {
                remote_url = "tcp://61.152.93.177:8086/EgRServer";
            }
            string ret = "";
            if (rc.isConnected)
            {
                status = RemoteStatus.CONNECTED;
                try
                {
                    ret = rc.InvokeRemoting(value);
                }
                catch (Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("RemoteSend Exception1:" + ex.Message);
                    status = RemoteStatus.DISCONNECT;
                }
            }
            else
            {
                try
                {
                    rc.strRURL = remote_url;
                    if (!rc.isConnected) throw new Exception("连不上！");
                    return RemoteSend(value);
                }
                catch (Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("RemoteSend Exception2:" + ex.Message);
                    status = RemoteStatus.DISCONNECT;
                }
            }
            
            return ret;
        }
        public static void StartCheck()
        {
            if (EagleString.EagleFileIO.ValueOf("TheRootAgent") == "1") return;
            try
            {
                if (ThreadConnect.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    ThreadConnect = new System.Threading.Thread(new System.Threading.ThreadStart(Thread_Check_Remoting_Server));
                    ThreadConnect.Name = "ThreadRemotingReconnect";
                    ThreadConnect.Start();
                }
            }
            catch
            {
            }
        }
        public static void StopCheck()
        {
            try
            {
                if (ThreadConnect != null && ThreadConnect.ThreadState == System.Threading.ThreadState.Running)
                {
                    ThreadConnect.Abort();
                    ThreadConnect = null;
                }
            }
            catch
            {
            }
        }
        static System.Threading.Thread ThreadConnect;
        static void Thread_Check_Remoting_Server()
        {
            while (true)
            {
                if (status == RemoteStatus.DISCONNECT)
                {
                    try
                    {
                        rc.strRURL = remote_url;
                        if (rc.isConnected)
                        {
                            status = RemoteStatus.CONNECTED;
                        }
                    }
                    catch
                    {
                        
                    }
                }
                System.Threading.Thread.Sleep(10 * 60 * 1000);
            }
        }

        public enum RemoteStatus
        {
            /// <summary>
            /// 未连接
            /// </summary>
            UNCONNECT,
            /// <summary>
            /// 连接中
            /// </summary>
            CONNECTING,
            /// <summary>
            /// 连通
            /// </summary>
            CONNECTED,
            /// <summary>
            /// 断开
            /// </summary>
            DISCONNECT
        }
    }
}
