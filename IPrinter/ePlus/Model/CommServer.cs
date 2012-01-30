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
        //���ӵ��û� 
        System.Collections.Generic.List<User> userList = new List<User>();
        TcpListener listener;
        //ʹ�õı���IP��ַ 
        IPAddress localAddress;

        //��ǰuser
        User userCurrent = null;
        //�����˿� 
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
            //����һ���̼߳����ͻ����������� 
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
                    //�ȴ��û����� 
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //��������ֹͣ�����������˳��˴���ʱAcceptTcpClient()������쳣 
                    //��˿������ô��쳣�˳�ѭ�� 
                    break;
                }
                //ÿ����һ���ͻ�������,�ʹ���һ����Ӧ���߳�ѭ�����ոÿͻ��˷�������Ϣ 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(pts);
                User user = new User(newClient);
                threadReceive.Start(user);
                //if (userList.Count >= 1) userList.Clear();
                
                userList.Add(user);
                userCurrent = user;
                //AddComboBoxitem(user);
                //SetListBox(string.Format("[{0}]����", newClient.Client.RemoteEndPoint));
                //SetListBox(string.Format("��ǰ�����û�����{0}", userList.Count));
            }
        }
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            userCurrent = user;
            TcpClient client = user.client;
            //�Ƿ������˳������߳� 
            bool normalExit = false;
            //���ڿ����Ƿ��˳�ѭ�� 
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    //���������ж����ַ��� 
                    //�˷������Զ��ж��ַ�������ǰ׺�������ݳ���ǰ׺�����ַ��� 
                    receiveString = user.br.ReadString();
                }
                catch
                {
                    //�ײ��׽��ֲ�����ʱ������쳣 
                    //MessageBox.Show("��������ʧ��");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        //���ֹͣ�˼�����ConnectedΪfalse 
                        if (client.Connected == true)
                        {
                            //SetListBox(string.Format("��[{0}]ʧȥ��ϵ������ֹ���ո��û���Ϣ", client.Client.RemoteEndPoint));
                        }
                    }
                    break;
                }
                //SetListBox(string.Format("����[{0}]��{1}", user.client.Client.RemoteEndPoint, receiveString));
                string[] splitString = receiveString.Split(',');
                string sendString = "";
                //MessageBox.Show(user.client.Client.RemoteEndPoint.ToString());
                switch (splitString[0])
                {
                    case "Login":
                        //��ʽ��Login 
                        //sendString = "Hello�����Ƿ����������!";
                        //SendToClient(user, sendString);
                        //GlobalVar.stdRichTB.AppendText("���ز��������½");
                        break;
                    case "Logout":
                        //��ʽ��Logout 
                        //SetListBox(string.Format("[{0}]�˳�", user.client.Client.RemoteEndPoint));
                        normalExit = true;
                        exitWhile = true;
                        break;
                    case "Talk":
                        //��ʽ��Talk,�Ի����� 
                        //SetListBox(string.Format("[{0}]˵��{1}", client.Client.RemoteEndPoint,
                        //receiveString.Substring(splitString[0].Length + 1)));
                        break;
                    default:
                        //SetListBox("ʲô��˼����" + receiveString);
                        //SendToClient(user, "fuck you too");
                        GlobalVar.cwRecvString = receiveString;
                        EagleAPI.CLEARCMDLIST(3);
                        EagleAPI.EagleSendCmd(receiveString,3);
                        break;
                }
            }
            userList.Remove(user);
            client.Close();
            //SetListBox(string.Format("��ǰ�����û�����{0}", userList.Count));
        }
        private void SendToClient(User user, string str)
        {
            //MessageBox.Show("send:" + user.client.Client.RemoteEndPoint);
            try
            {
                //���ַ���д�����������˷������Զ������ַ�������ǰ׺ 
                user.bw.Write(str);
                user.bw.Flush();
                //SetListBox(string.Format("��[{0}]���ͣ�{1}", user.client.Client.RemoteEndPoint, str));
            }
            catch
            {
                //MessageBox.Show(string.Format("��[{0}]������Ϣʧ��", user.client.Client.RemoteEndPoint));
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
            //ͨ��ֹͣ������myListener.AcceptTcpClient()�����쳣�˳������߳� 
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
                    //MessageBox.Show(e.Message+"����ʧ��(���������)");
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
