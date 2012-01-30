using System;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MyKJ
{
    /// <summary>
    /// MyTcpIpClient �ṩ��Net TCP_IP Э���ϻ�����Ϣ�ķ���� 
    /// </summary>
    public class MyTcpIpServer : System.ComponentModel.Component
    {
        private int bufferSize = 2048;
        private string tcpIpServerIP = "";
        private int tcpIpServerPort = 11000;
        private Socket listener = null;
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private Thread thread = null;

        private void StartListening()
        {
            try
            {
                listener = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream, ProtocolType.Tcp);

                IPAddress ipAddress;
                if (tcpIpServerIP.Trim() == "")
                {
                    ipAddress = IPAddress.Any;
                }
                else
                {
                    ipAddress = IPAddress.Parse(tcpIpServerIP);
                }
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, tcpIpServerPort);

                listener.Bind(localEndPoint);
                listener.Listen(10);
                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, listener));
            }
        }

        private void ReadCallback(IAsyncResult ar)
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
                        //state.Datastream.Write(state.buffer, 0, state.buffer.Length);//��Ϊ
                        for (int i = bytesRead; i < state.buffer.Length; i++)
                        {
                            state.buffer[i] = 0;
                        }
                        state.Datastream.Position = 0; state.Datastream.Write(state.buffer, 0, bufferSize);
                        OnInceptServerEvent(new InceptServerEventArgs(state.Datastream, state.workSocket, this));
                        if (handler.Connected == true)
                        {
                            handler.BeginReceive(state.buffer, 0, bufferSize, 0,
                             new AsyncCallback(ReadCallback), state);
                        }
                    }
                    else
                    {
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        //throw(new Exception("���������С��1bit"));
                    }
                }
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, handler));

            }
        }
        private void ReadCallbackOld(IAsyncResult ar)
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
                                    OnInceptServerEvent(new InceptServerEventArgs(state.Datastream, state.workSocket, this));
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
                        if (handler.Connected == true)
                        {
                            handler.BeginReceive(state.buffer, 0, bufferSize, 0,
                             new AsyncCallback(ReadCallback), state);
                        }
                    }
                    else
                    {
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        //throw(new Exception("���������С��1bit"));
                    }
                }
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, handler));

            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, client));
            }
            finally
            {
                sendDone.Set();
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket handler = null;
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                handler = listener.EndAccept(ar);
                StateObject state = new StateObject(bufferSize, handler);
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, bufferSize, 0,
                 new AsyncCallback(ReadCallback), state);
                //System.Windows.Forms.MessageBox.Show(handler.RemoteEndPoint.ToString());
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, handler));
            }
            finally
            {
                allDone.Set();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="disposing">��֪��</param>
        protected override void Dispose(bool disposing)
        {
            Abort();
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="e">����</param>
        protected virtual void OnInceptServerEvent(InceptServerEventArgs e)
        {
            if (InceptServer != null)
            {
                InceptServer(this, e);
            }
        }
        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="e">����</param>
        protected virtual void OnErrorServerEvent(ErrorServerEventArgs e)
        {
            if (ErrorServer != null)
            {
                ErrorServer(this, e);
            }
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public void Listening()
        {
            //StartListening();
            thread = new Thread(new ThreadStart(StartListening));
            thread.Name = "MyTcpIpServer.Listening";
            thread.Start();
        }
        /// <summary>
        /// �쳣��ֹ����
        /// </summary>
        public void Abort()
        {
            if (thread != null)
            {
                thread.Abort();
                listener.Close();
            }
        }

        /// <summary>
        ///���� 
        /// </summary>
        /// <param name="container">���ؼ�</param>
        public MyTcpIpServer(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }

        /// <summary>
        /// ����
        /// </summary>
        public MyTcpIpServer()
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
                return listener.Connected;
            }
            //set
            //{
            // activ=value;
            //}
        }

        /// <summary>
        /// ����һ��������
        /// </summary>
        public void Send(Socket ClientSocket, Stream Astream)
        {
            try
            {
                if (ClientSocket.Connected == false)
                {
                    throw (new Exception("û�����ӿͻ��˲����Է�����Ϣ!"));
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

                int n = Astream.Read(byteData, 0, byteData.Length);

                while (n > 0)
                {
                    //ClientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), ClientSocket);//��Ϊ
                    ClientSocket.BeginSend(byteData, 0, (int)Astream.Length, 0, new AsyncCallback(SendCallback), ClientSocket);
                    sendDone.WaitOne();
                    byteData = new byte[bufferSize];
                    n = Astream.Read(byteData, 0, byteData.Length);
                }
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, ClientSocket));
            }
        }
        /// <summary>
        /// ����һ��������
        /// </summary>
        public void SendWithoutHead(Socket ClientSocket, Stream Astream)
        {
            try
            {
                if (ClientSocket.Connected == false)
                {
                    throw (new Exception("û�����ӿͻ��˲����Է�����Ϣ!"));
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

                int n = Astream.Read(byteData, 0, byteData.Length);

                while (n > 0)
                {
                    ClientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), ClientSocket);
                    sendDone.WaitOne();
                    byteData = new byte[bufferSize];
                    n = Astream.Read(byteData, 0, byteData.Length);
                }
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, ClientSocket));
            }
        }
        /// <summary>
        /// ���յ������¼�
        /// </summary>
        public event InceptServerEvent InceptServer;
        /// <summary>
        /// ���������¼�
        /// </summary>
        public event ErrorServerEvent ErrorServer;
    }
    /// <summary>
    /// ״̬����
    /// </summary>
    public class StateObject
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="bufferSize">����</param>
        /// <param name="WorkSocket">�����Ĳ���</param>
        public StateObject(int bufferSize, Socket WorkSocket)
        {
            buffer = new byte[bufferSize];
            workSocket = WorkSocket;
        }
        /// <summary>
        /// ����
        /// </summary>
        public byte[] buffer = null;
        /// <summary>
        /// ��������
        /// </summary>
        public Socket workSocket = null;
        /// <summary>
        /// ������
        /// </summary>
        public Stream Datastream = new MemoryStream();
        /// <summary>
        /// ʣ���С
        /// </summary>
        public long residualSize = 0;
        /// <summary>
        /// ���ݰ���С
        /// </summary>
        public long packSize = 0;
        /// <summary>
        /// ������
        /// </summary>
        public int Cortrol = 0;
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    public class InceptServerEventArgs : EventArgs
    {
        private readonly Stream datastream;
        private readonly Socket serverSocket;
        private readonly MyTcpIpServer tcpIpServer;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Astream">����</param>
        /// <param name="ServerSocket">��������</param>
        /// <param name="TcpIpServer">�ṩ�����TCP/IP����</param>
        public InceptServerEventArgs(Stream Astream, Socket ServerSocket, MyTcpIpServer TcpIpServer)
        {
            datastream = Astream;
            serverSocket = ServerSocket;
            tcpIpServer = TcpIpServer;
        }
        /// <summary>
        /// ����
        /// </summary>
        public Stream Astream
        {
            get { return datastream; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public Socket ServerSocket
        {
            get { return serverSocket; }
        }
        /// <summary>
        /// �ṩTCP/IP����ķ���������.
        /// </summary>
        public MyTcpIpServer TcpIpServer
        {
            get { return tcpIpServer; }
        }
    }
    /// <summary>
    /// ��������ί��
    /// </summary>
    public delegate void InceptServerEvent(object sender, InceptServerEventArgs e);
    /// <summary>
    /// �����¼�ί��
    /// </summary>
    public class ErrorServerEventArgs : EventArgs
    {
        private readonly Exception error;
        private readonly Socket serverSocket;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Error">����</param>
        /// <param name="ServerSocket">�������</param>
        public ErrorServerEventArgs(Exception Error, Socket ServerSocket)
        {
            error = Error;
            serverSocket = ServerSocket;
        }
        /// <summary>
        /// ����
        /// </summary>
        public Exception Error
        {
            get { return error; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public Socket ServerSocket
        {
            get { return serverSocket; }
        }
    }
    /// <summary>
    ///�����¼�ί�� 
    /// </summary>
    public delegate void ErrorServerEvent(object sender, ErrorServerEventArgs e);
}

