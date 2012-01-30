using System;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace EagleProtocal
{
    /// <summary>
    /// MyTcpIpClient �ṩ��Net TCP_IP Э���ϻ�����Ϣ�Ŀͻ��� 
    /// </summary>
    public class MyTcpIpClient : System.ComponentModel.Component
    {
        private int bufferSize = 2048;
        private string tcpIpServerIP = "127.0.0.1";
        private int tcpIpServerPort = 11000;
        private Socket ClientSocket = null;
        private ManualResetEvent connectDone = new ManualResetEvent(false);

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));
            }
            finally
            {
                connectDone.Set();
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket handler = null;
            try
            {
                lock (ar)
                {
                    StateObject state = (StateObject)ar.AsyncState;
                    handler = state.workSocket;

                    int bytesRead = handler.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        int ReadPiont = 0;
                        for (int i = bytesRead; i < state.buffer.Length; i++)
                        {
                            state.buffer[i] = 0;
                        }
                        state.Datastream.Position = 0; 
                        state.Datastream.Write(state.buffer, 0, bufferSize);
                        OnInceptEvent(new InceptEventArgs(state.Datastream, handler));
                    }
                    else
                    {
                        throw (new Exception("���������С��1bit"));
                    }
                    if (handler.Connected == true)
                    {
                        handler.BeginReceive(state.buffer, 0, bufferSize, 0,
                         new AsyncCallback(ReceiveCallback), state);
                    }
                }
            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));

            }
        }
        private void ReceiveCallbackOld(IAsyncResult ar)
        {
            Socket handler = null;
            try
            {
                lock (ar)
                {
                    StateObject state = (StateObject)ar.AsyncState;
                    handler = state.workSocket;

                    int bytesRead = handler.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        int ReadPiont = 0;
                        while (ReadPiont < bytesRead)
                        {
                            if (state.Cortrol == 0 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = (bi1 << 24) & 0xff000000;
                                state.packSize = bi1;
                                ReadPiont++;
                                state.Cortrol = 1;
                            }

                            if (state.Cortrol == 1 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = (bi1 << 16) & 0x00ff0000;
                                state.packSize = state.packSize + bi1;
                                ReadPiont++;
                                state.Cortrol = 2;
                            }

                            if (state.Cortrol == 2 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = (bi1 << 8) & 0x0000ff00;
                                state.packSize = state.packSize + bi1;
                                ReadPiont++;
                                state.Cortrol = 3;
                            }

                            if (state.Cortrol == 3 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = bi1 & 0xff;
                                state.packSize = state.packSize + bi1 - 4;
                                ReadPiont++;
                                state.Cortrol = 4;
                            }

                            if (state.Cortrol == 4 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = (bi1 << 24) & 0xff000000;
                                state.residualSize = bi1;
                                ReadPiont++;
                                state.Cortrol = 5;
                                state.packSize -= 1;
                            }

                            if (state.Cortrol == 5 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = (bi1 << 16) & 0x00ff0000;
                                state.residualSize = state.residualSize + bi1;
                                ReadPiont++;
                                state.Cortrol = 6;
                                state.packSize -= 1;
                            }

                            if (state.Cortrol == 6 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = (bi1 << 8) & 0x0000ff00;
                                state.residualSize = state.residualSize + bi1;
                                ReadPiont++;
                                state.Cortrol = 7;
                                state.packSize -= 1;
                            }
                            if (state.Cortrol == 7 && ReadPiont < bytesRead)
                            {
                                long bi1 = state.buffer[ReadPiont];
                                bi1 = bi1 & 0xff;
                                state.residualSize = state.residualSize + bi1;
                                state.Datastream.SetLength(0);
                                state.Datastream.Position = 0;

                                ReadPiont++;
                                state.Cortrol = 8;
                                state.packSize -= 1;
                            }

                            if (state.Cortrol == 8 && ReadPiont < bytesRead)
                            {
                                int bi1 = bytesRead - ReadPiont;
                                int bi2 = (int)(state.residualSize - state.Datastream.Length);
                                if (bi1 >= bi2)
                                {
                                    state.Datastream.Write(state.buffer, ReadPiont, bi2);
                                    ReadPiont += bi2;
                                    OnInceptEvent(new InceptEventArgs(state.Datastream, handler));
                                    state.Cortrol = 9;
                                    state.packSize -= bi2;
                                }
                                else
                                {
                                    state.Datastream.Write(state.buffer, ReadPiont, bi1);
                                    ReadPiont += bi1;
                                    state.packSize -= bi1;
                                }
                            }
                            if (state.Cortrol == 9 && ReadPiont < bytesRead)
                            {
                                int bi1 = bytesRead - ReadPiont;
                                if (bi1 < state.packSize)
                                {
                                    state.packSize = state.packSize - bi1;
                                    ReadPiont += bi1;
                                }
                                else
                                {
                                    state.Cortrol = 0;
                                    ReadPiont += (int)state.packSize;
                                }
                            }
                        }
                    }
                    else
                    {
                        throw (new Exception("���������С��1bit"));
                    }
                    if (handler.Connected == true)
                    {
                        handler.BeginReceive(state.buffer, 0, bufferSize, 0,
                         new AsyncCallback(ReceiveCallback), state);
                    }
                }
            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));

            }
        }
        //public void ReUse()
        //{
        //    ClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        //}

        /// <summary>
        /// ���ӷ�����
        /// </summary>
        public void Conn()
        {
            try
            {
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddress = IPAddress.Parse(tcpIpServerIP);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, tcpIpServerPort);
                connectDone.Reset();
                ClientSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), ClientSocket);
                connectDone.WaitOne();
                StateObject state = new StateObject(bufferSize, ClientSocket);
                ClientSocket.BeginReceive(state.buffer, 0, bufferSize, 0,
                 new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));
            }

        }
        /// <summary>
        /// �Ͽ�����
        /// </summary>
        public void Close()
        {
            try
            {
                if (ClientSocket.Connected == true)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                }
            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));
            }

        }
        private EagleString.LoginInfo m_li;
        
        /// <summary>
        /// ����һ��������
        /// </summary>
        /// <param name="Astream">������</param>
        public void Send(Stream Astream)
        {
            try
            {
                if (ClientSocket == null)
                    ClientSocket = Options.GlobalVar.socketGlobal;

                if (ClientSocket == null || ClientSocket.Connected == false)
                {
                    throw (new Exception("û�����ӷ����������Է�����Ϣ!"));
                }
                Astream.Position = 0;
                byte[] byteData = new byte[bufferSize];
                int bi1 = (int)((Astream.Length + 8) / bufferSize);
                int bi2 = (int)Astream.Length;
                if (((Astream.Length + 8) % bufferSize) > 0)
                {
                    bi1 = bi1 + 1;
                }
                bi1 = bi1 * bufferSize;

                //byteData[0] = System.Convert.ToByte(bi1 >> 24);
                //byteData[1] = System.Convert.ToByte((bi1 & 0x00ff0000) >> 16);
                //byteData[2] = System.Convert.ToByte((bi1 & 0x0000ff00) >> 8);
                //byteData[3] = System.Convert.ToByte((bi1 & 0x000000ff));

                //byteData[4] = System.Convert.ToByte(bi2 >> 24);
                //byteData[5] = System.Convert.ToByte((bi2 & 0x00ff0000) >> 16);
                //byteData[6] = System.Convert.ToByte((bi2 & 0x0000ff00) >> 8);
                //byteData[7] = System.Convert.ToByte((bi2 & 0x000000ff));

                //int n = Astream.Read(byteData, 8, byteData.Length - 8);
                int n = Astream.Read(byteData, 0, byteData.Length);

                while (n > 0)
                {
                    //ClientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), ClientSocket);
                    ClientSocket.BeginSend(byteData, 0, (int)Astream.Length, 0, null, ClientSocket);

                    byteData = new byte[bufferSize];
                    n = Astream.Read(byteData, 0, byteData.Length);
                }
            }
            catch (Exception e)
            {
                OnErrorEvent(new ErrorEventArgs(e));
            }
        }
        public void Send(string str)
        {
            Send(new MemoryStream(System.Text.Encoding.Default.GetBytes(str)));
        }
        public void Send(byte[] b)
        {
            Send(new MemoryStream(b));
        }
        uint msgno = 0;
        public void SendPassport(string pspt)
        {
            EagleString.EagleFileIO.LogWrite("��֤PASSPORT=" + pspt);
            PACKET_CHECK_PASSPORT packet = new PACKET_CHECK_PASSPORT(msgno++, pspt);
            Send(packet.ToBytes());

        }
        public void SendRegisterIPs(string[] ips)
        {
            EagleString.EagleFileIO.LogWrite("ע���������");
            PACKET_IP_REGISTER packet = new PACKET_IP_REGISTER(msgno++, (ushort)ips.Length, ips);
            Send(packet.ToBytes());
        }
        /// <summary>
        /// ����ǰָ̨��(3)
        /// </summary>
        public void SendCommand(string cmd,ushort cmdtype)
        {
            EagleString.EagleFileIO.LogWrite("SEND: " + cmd);
            //EagleString.EagleFileIO.LogWriteAccess(cmd);
            string[] a = cmd.Split('~');
            for (int i = 0; i < a.Length; ++i)//������ָ����Ȩ�޿���
            {
                if (cmdtype == TypeOfCommand.ThreeInOne) break;
                if (!m_li.b2b.lr.AuthorityOfCommand(a[i]))
                {
                    EagleString.EagleFileIO.LogWrite("��ָ��Ȩ��:" + a[i]);
                    System.Windows.Forms.MessageBox.Show("��ָ��Ȩ��:" + a[i]);
                    return;
                }
            }
            if (EagleString.egString.right(cmd.ToLower(), 3) == "/eg")
            {
                cmd = cmd.Substring(0, cmd.Length - 3);
                PACKET_COMMAND p = new PACKET_COMMAND(msgno++, 99, cmd);
                Send(p.ToBytes());
                return;
            }

            if (cmd == "") return;
            
            PACKET_COMMAND packet = new PACKET_COMMAND(msgno++, cmdtype, cmd);
            Send(packet.ToBytes());
        }
        
        /// <summary>
        /// ���ͺ�ָ̨��(7)
        /// </summary>
        public void SendCommandBack(string cmd,ushort cmdtype)
        {
            if (cmd == "") return;

            EagleString.EagleFileIO.LogWrite("SENDBACKGROUND: " + cmd);
            string[] a = cmd.Split('~');
            for (int i = 0; i < a.Length; ++i)//������ָ����Ȩ�޿���
            {
                if (!m_li.b2b.lr.AuthorityOfCommand(a[i]))
                {
                    EagleString.EagleFileIO.LogWrite("ָ��Ȩ��:" + a[i]);
                    return;
                }
            }
            if (EagleString.egString.right(cmd.ToLower(), 3) == "/eg")
            {
                cmd = cmd.Substring(0, cmd.Length - 3);
                PACKET_COMMAND_BACKGROUND p = new PACKET_COMMAND_BACKGROUND(msgno++, TypeOfCommand.AutoPnBack, cmd);
                Send(p.ToBytes());
                return;
            }

            if (cmd == "") return;

            PACKET_COMMAND_BACKGROUND packet = new PACKET_COMMAND_BACKGROUND(msgno++, cmdtype, cmd);
            Send(packet.ToBytes());

        }

        public void SendReceiptPrint(string tktno, string recptno)
        {
            EagleString.EagleFileIO.LogWrite(string.Format("�����г̵���ӡ,{0}->{1}", tktno, recptno));
            StringBuilder sb = new StringBuilder("", 4096);
            EagleString.Imported.RecieptPrint(tktno, recptno, sb);
            SendCommand(sb.ToString(), TypeOfCommand.ThreeInOne);
        }
        public void SendReceiptCancel(string tktno, string recptno)
        {
            EagleString.EagleFileIO.LogWrite(string.Format("�����г̵�����,{0}->{1}", tktno, recptno));
            StringBuilder sb = new StringBuilder("", 4096);
            EagleString.Imported.RecieptRemove(tktno, recptno, ";USERID=xPkbi2qSPe8=", ";PWD=OKK5r2tgag3UwE9nNaTCXw==", sb);
            SendCommand(sb.ToString(), TypeOfCommand.ThreeInOne);
        }
        public void SendNewPnrOrder(string[] passport)
        {
            PACKET_PROMOPT_SUBMIT_PNR packet = new PACKET_PROMOPT_SUBMIT_PNR(msgno++, passport);
            Send(packet.ToBytes());
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="container">���ؼ�</param>
        public MyTcpIpClient(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }
        /// <summary>
        /// ���죬logininfo���ڷ���ʱ�ٴ���Ȩ�޿���
        /// </summary>
        public MyTcpIpClient(EagleString.LoginInfo li)
        {
            InitializeComponent();
            m_li = li;
            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }
        /// <summary>
        /// �����Ѵ��ڵ�SOCKET�Ĺ���
        /// </summary>
        /// <param name="sk"></param>
        public MyTcpIpClient(Socket sk,EagleString.LoginInfo li)
        {
            InitializeComponent();
            ClientSocket = sk;
            m_li = li;
        }
        public MyTcpIpClient()
        {
            InitializeComponent();
            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }
        #region Component Designer generated code
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        /// <summary>
        /// Ҫ���ӵķ�����IP��ַ
        /// </summary>
        public string TcpIpServerIP
        {
            get
            {
                return tcpIpServerIP;
            }
            set
            {
                tcpIpServerIP = value;
            }
        }

        /// <summary>
        /// Ҫ���ӵķ�������ʹ�õĶ˿�
        /// </summary>
        public int TcpIpServerPort
        {
            get
            {
                return tcpIpServerPort;
            }
            set
            {
                tcpIpServerPort = value;
            }
        }

        /// <summary>
        /// ��������С
        /// </summary>
        public int BufferSize
        {
            get
            {
                return bufferSize;
            }
            set
            {
                bufferSize = value;
            }
        }

        /// <summary>
        /// ���ӵĻ״̬
        /// </summary>
        public bool Activ
        {
            get
            {
                if (ClientSocket == null)
                {
                    return false;
                }
                return ClientSocket.Connected;
            }
        }
        /// <summary>
        /// ���յ������������¼�
        /// </summary>
        public event InceptEvent Incept;
        /// <summary>
        /// �������������¼�
        /// </summary>
        /// <param name="e">��������</param>
        protected virtual void OnInceptEvent(InceptEventArgs e)
        {
            if (Incept != null)
            {
                Incept(this, e);
            }
        }
        /// <summary>
        /// ���������������¼�
        /// </summary>
        public event ErrorEvent Error;
        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="e">��������</param>
        protected virtual void OnErrorEvent(ErrorEventArgs e)
        {
            if (Error != null)
            {
                Error(this, e);
            }
        }

    }

    /// <summary>
    /// ���������¼�
    /// </summary>
    public class InceptEventArgs : EventArgs
    {
        private readonly Stream datastream;
        private readonly Socket clientSocket;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Astream">���յ�������</param>
        /// <param name="ClientSocket">���յĲ���</param>
        public InceptEventArgs(Stream Astream, Socket ClientSocket)
        {
            datastream = Astream;
            clientSocket = ClientSocket;
        }
        /// <summary>
        /// ���ܵ�������
        /// </summary>
        public Stream Astream
        {
            get { return datastream; }
        }
        /// <summary>
        /// ���յĲ���
        /// </summary>
        public Socket ClientSocket
        {
            get { return clientSocket; }
        }
    }
    /// <summary>
    /// �������ί��
    /// </summary>
    public delegate void InceptEvent(object sender, InceptEventArgs e);
    /// <summary>
    /// ���¼�
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        private readonly Exception error;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Error">������Ϣ����</param>
        public ErrorEventArgs(Exception Error)
        {
            error = Error;
        }
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public Exception Error
        {
            get { return error; }
        }
    }
    /// <summary>
    /// ����ί��
    /// </summary>
    public delegate void ErrorEvent(object sender, ErrorEventArgs e);
}



