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
    /// MyTcpIpClient 提供在Net TCP_IP 协议上基于消息的服务端 
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
                        //state.Datastream.Write(state.buffer, 0, state.buffer.Length);//改为
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
                        //throw(new Exception("读入的数据小于1bit"));
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
                        //throw(new Exception("读入的数据小于1bit"));
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
        /// 析构
        /// </summary>
        /// <param name="disposing">不知道</param>
        protected override void Dispose(bool disposing)
        {
            Abort();
        }

        /// <summary>
        /// 引发接收事件
        /// </summary>
        /// <param name="e">数据</param>
        protected virtual void OnInceptServerEvent(InceptServerEventArgs e)
        {
            if (InceptServer != null)
            {
                InceptServer(this, e);
            }
        }
        /// <summary>
        /// 引发错误事件
        /// </summary>
        /// <param name="e">数据</param>
        protected virtual void OnErrorServerEvent(ErrorServerEventArgs e)
        {
            if (ErrorServer != null)
            {
                ErrorServer(this, e);
            }
        }

        /// <summary>
        /// 开始监听访问
        /// </summary>
        public void Listening()
        {
            //StartListening();
            thread = new Thread(new ThreadStart(StartListening));
            thread.Name = "MyTcpIpServer.Listening";
            thread.Start();
        }
        /// <summary>
        /// 异常中止服务
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
        ///构造 
        /// </summary>
        /// <param name="container">父控件</param>
        public MyTcpIpServer(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 构造
        /// </summary>
        public MyTcpIpServer()
        {
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        #region Component Designer generated code
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        /// <summary>
        /// 要连接的服务器IP地址
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
        /// 要连接的服务器所使用的端口
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
        /// 缓冲器大小
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
        /// 连接的活动状态
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
        /// 发送一个流数据
        /// </summary>
        public void Send(Socket ClientSocket, Stream Astream)
        {
            try
            {
                if (ClientSocket.Connected == false)
                {
                    throw (new Exception("没有连接客户端不可以发送信息!"));
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
                    //ClientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), ClientSocket);//改为
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
        /// 发送一个流数据
        /// </summary>
        public void SendWithoutHead(Socket ClientSocket, Stream Astream)
        {
            try
            {
                if (ClientSocket.Connected == false)
                {
                    throw (new Exception("没有连接客户端不可以发送信息!"));
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
        /// 接收到数据事件
        /// </summary>
        public event InceptServerEvent InceptServer;
        /// <summary>
        /// 发生错误事件
        /// </summary>
        public event ErrorServerEvent ErrorServer;
    }
    /// <summary>
    /// 状态对象
    /// </summary>
    public class StateObject
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bufferSize">缓存</param>
        /// <param name="WorkSocket">工作的插座</param>
        public StateObject(int bufferSize, Socket WorkSocket)
        {
            buffer = new byte[bufferSize];
            workSocket = WorkSocket;
        }
        /// <summary>
        /// 缓存
        /// </summary>
        public byte[] buffer = null;
        /// <summary>
        /// 工作插座
        /// </summary>
        public Socket workSocket = null;
        /// <summary>
        /// 数据流
        /// </summary>
        public Stream Datastream = new MemoryStream();
        /// <summary>
        /// 剩余大小
        /// </summary>
        public long residualSize = 0;
        /// <summary>
        /// 数据包大小
        /// </summary>
        public long packSize = 0;
        /// <summary>
        /// 计数器
        /// </summary>
        public int Cortrol = 0;
    }

    /// <summary>
    /// 接收事件
    /// </summary>
    public class InceptServerEventArgs : EventArgs
    {
        private readonly Stream datastream;
        private readonly Socket serverSocket;
        private readonly MyTcpIpServer tcpIpServer;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Astream">数据</param>
        /// <param name="ServerSocket">工作插座</param>
        /// <param name="TcpIpServer">提供服务的TCP/IP对象</param>
        public InceptServerEventArgs(Stream Astream, Socket ServerSocket, MyTcpIpServer TcpIpServer)
        {
            datastream = Astream;
            serverSocket = ServerSocket;
            tcpIpServer = TcpIpServer;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public Stream Astream
        {
            get { return datastream; }
        }
        /// <summary>
        /// 工作插座
        /// </summary>
        public Socket ServerSocket
        {
            get { return serverSocket; }
        }
        /// <summary>
        /// 提供TCP/IP服务的服务器对象.
        /// </summary>
        public MyTcpIpServer TcpIpServer
        {
            get { return tcpIpServer; }
        }
    }
    /// <summary>
    /// 接收数据委托
    /// </summary>
    public delegate void InceptServerEvent(object sender, InceptServerEventArgs e);
    /// <summary>
    /// 错误事件委托
    /// </summary>
    public class ErrorServerEventArgs : EventArgs
    {
        private readonly Exception error;
        private readonly Socket serverSocket;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Error">数据</param>
        /// <param name="ServerSocket">问题插座</param>
        public ErrorServerEventArgs(Exception Error, Socket ServerSocket)
        {
            error = Error;
            serverSocket = ServerSocket;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public Exception Error
        {
            get { return error; }
        }
        /// <summary>
        /// 问题插座
        /// </summary>
        public Socket ServerSocket
        {
            get { return serverSocket; }
        }
    }
    /// <summary>
    ///错误事件委托 
    /// </summary>
    public delegate void ErrorServerEvent(object sender, ErrorServerEventArgs e);
}

