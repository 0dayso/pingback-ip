using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO; 

namespace ePlus.Model
{
    class CommServer
    {
        //连接的用户 
        System.Collections.Generic.List<User> userList = new List<User>();
        TcpListener listener;
        //使用的本机IP地址 
        IPAddress localAddress;

        //当前user
        User userCurrent = null;
        //监听端口 
        private int port = 51888;
        private TcpListener myListener;
        public CommServer()
        {
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = addrIP[0];

        }
        public void Start()
        {
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            //创建一个线程监听客户端连接请求 
            ThreadStart ts = new ThreadStart(ListenClientConnect);
            Thread myThread = new Thread(ts);
            myThread.Start();

        }
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    //等待用户进入 
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //当单击“停止监听”或者退出此窗体时AcceptTcpClient()会产生异常 
                    //因此可以利用此异常退出循环 
                    break;
                }
                //每接受一个客户端连接,就创建一个对应的线程循环接收该客户端发来的信息 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(pts);
                User user = new User(newClient);
                threadReceive.Start(user);
                //if (userList.Count >= 1) userList.Clear();
                
                userList.Add(user);
                userCurrent = user;
                //AddComboBoxitem(user);
                //SetListBox(string.Format("[{0}]进入", newClient.Client.RemoteEndPoint));
                //SetListBox(string.Format("当前连接用户数：{0}", userList.Count));
            }
        }
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            userCurrent = user;
            TcpClient client = user.client;
            //是否正常退出接收线程 
            bool normalExit = false;
            //用于控制是否退出循环 
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    //从网络流中读出字符串 
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串 
                    receiveString = user.br.ReadString();
                }
                catch
                {
                    //底层套接字不存在时会出现异常 
                    //MessageBox.Show("接收数据失败");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        //如果停止了监听，Connected为false 
                        if (client.Connected == true)
                        {
                            //SetListBox(string.Format("与[{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint));
                        }
                    }
                    break;
                }
                //SetListBox(string.Format("来自[{0}]：{1}", user.client.Client.RemoteEndPoint, receiveString));
                string[] splitString = receiveString.Split(',');
                string sendString = "";
                //MessageBox.Show(user.client.Client.RemoteEndPoint.ToString());
                switch (splitString[0])
                {
                    case "Login":
                        //格式：Login 
                        //sendString = "Hello，我是服务器，你好!";
                        //SendToClient(user, sendString);
                        //GlobalVar.stdRichTB.AppendText("本地财务软件登陆");
                        break;
                    case "Logout":
                        //格式：Logout 
                        //SetListBox(string.Format("[{0}]退出", user.client.Client.RemoteEndPoint));
                        normalExit = true;
                        exitWhile = true;
                        break;
                    case "Talk":
                        //格式：Talk,对话内容 
                        //SetListBox(string.Format("[{0}]说：{1}", client.Client.RemoteEndPoint,
                        //receiveString.Substring(splitString[0].Length + 1)));
                        break;
                    default:
                        //SetListBox("什么意思啊：" + receiveString);
                        //SendToClient(user, "fuck you too");
                        GlobalVar.cwRecvString = receiveString;
                        EagleAPI.CLEARCMDLIST(3);
                        EagleAPI.EagleSendCmd(receiveString,3);
                        break;
                }
            }
            userList.Remove(user);
            client.Close();
            //SetListBox(string.Format("当前连接用户数：{0}", userList.Count));
        }
        private void SendToClient(User user, string str)
        {
            //MessageBox.Show("send:" + user.client.Client.RemoteEndPoint);
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀 
                user.bw.Write(str);
                user.bw.Flush();
                //SetListBox(string.Format("向[{0}]发送：{1}", user.client.Client.RemoteEndPoint, str));
            }
            catch
            {
                //MessageBox.Show(string.Format("向[{0}]发送信息失败", user.client.Client.RemoteEndPoint));
            }
        }
        public void Stop()
        {
            for (int i = 0; i < userList.Count; i++)
            {
                //comboBoxReceiver.Items.Remove(userList[i].client.Client.RemoteEndPoint);
                userList[i].br.Close();
                userList[i].bw.Close();
                userList[i].client.Close();
            }
            //通过停止监听让myListener.AcceptTcpClient()产生异常退出监听线程 
            myListener.Stop();
        }
        public void Send(string text)
        {
            //for (int i = 0; i < userList.Count; i++)
            {
                try
                {
                    SendToClient(userCurrent, text);
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message+"发送失败(财务监听！)");
                }
            }
        }
        public void Close()
        {
            if (myListener != null)
            {
                Stop();
            } 
        }
    }
    class User
    {
        public TcpClient client = null;
        public BinaryWriter bw = null;
        public BinaryReader br = null;
        public User(TcpClient c)
        {
            client = new TcpClient();
            client = c;
            br = new BinaryReader(client.GetStream());
            bw = new BinaryWriter(client.GetStream());


        }
    }
}
