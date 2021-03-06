namespace Leo.RawSockets
{
    using System;
    using System.Runtime.InteropServices;
    using System.Net.Sockets;
    using System.Net;
    using System.Windows.Forms;
    [StructLayout(LayoutKind.Explicit)]
    public struct IpHeader
    {
        [FieldOffset(0)]
        public byte ip_verlen; // 版本：4位。IP版本号,包头长：4位。以4字节为单位，如0101表示IP包的包头长度20字节
        [FieldOffset(1)]
        public byte ip_tos; //服务质量：1字节。一般没什么用。指示路由器如何处理该数据包。TOS

        [FieldOffset(2)]
        public ushort ip_totallength; // IP数据包的总长度
        [FieldOffset(4)]
        public ushort ip_id; // 系统范围内，每发出一个IP包，其值自动增加1。没什么用
        [FieldOffset(6)]
        public ushort ip_offset; //分片标志：1、保留位：1位 2、不分片：1位 3、最后一个片段：1位 4、偏移：13位。此片段在这个IP数据包中的位置
        [FieldOffset(8)]
        public byte ip_ttl; // Time To Live
        [FieldOffset(9)]
        public byte ip_protocol; // protocol (TCP, UDP etc) 负载协议
        [FieldOffset(10)]
        public ushort ip_checksum; //校验和：2字节。IP包头的校验和，不包括负载内容
        [FieldOffset(12)]
        public UInt32 ip_srcaddr; //Source address
        [FieldOffset(16)]
        public UInt32 ip_destaddr;//Destination Address
    }
    public abstract class BaseSocket
    {

        protected int len_receive_buf;
        protected int len_send_buf;
        protected byte[] receive_buf = null;
        protected byte[] send_buf = null;
        protected Socket socket = null;
        public BaseSocket()
        {
            len_receive_buf = 4096;
            len_send_buf = 4096;
            receive_buf = new byte[len_receive_buf];
            send_buf = new byte[len_send_buf];
        }
        public void CreateAndBindSocket(string ip)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), 0));

        }
        public void Shutdown()
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }


        }
        public int LengthReceiveBuf
        {
            get
            {
                return len_receive_buf;
            }
            set
            {
                lock (receive_buf)
                {
                    len_receive_buf = value;
                    receive_buf = new byte[len_receive_buf];
                }
            }
        }
        public int LengthSendBuf
        {
            get
            {
                return len_send_buf;
            }
            set
            {
                lock (send_buf)
                {
                    len_send_buf = value;
                    send_buf = new byte[len_send_buf];
                }
            }
        }
        public bool SetSockoption()
        {
            bool ret_value = true;
            try
            {
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
                //眢骓?篑蜞眍忤螯 怩钿�?镟疣戾蝠 ?true
                //?Platform SDK 铐 玎龛爨弪 2 耠钼?镱铎?� ?玟羼?蜞赕?皲咫嚯
                byte[] IN = new byte[4] { 1, 0, 0, 0 };
                byte[] OUT = new byte[4];
                int SIO_RCVALL = unchecked((int)0x98000001);
                int ret_code = socket.IOControl(SIO_RCVALL, IN, OUT);
                ret_code = OUT[0] + OUT[1] + OUT[2] + OUT[3];
                if (ret_code != 0)
                    ret_value = false;
            }
            catch (SocketException)
            {
                ret_value = false;
            }
            return ret_value;
        }
        public Socket Handle
        {
            get
            {
                return socket;
            }
        }
        ~BaseSocket()
        {
            try
            {
                socket.Close();
            }
            catch
            {
            }
        }
        public abstract void Receive(byte[] buf, int len);
    }
}
/*
IP数据包结构2007-07-26 09:15IP数据包结构：
版本：4位。IP版本号
包头长：4位。以4字节为单位，如0101表示IP包的包头长度20字节
服务质量：1字节。一般没什么用。指示路由器如何处理该数据包。TOS
总长：2字节。IP数据包的总长度
标识：2字节。系统范围内，每发出一个IP包，其值自动增加1。没什么用
分片标志：
1、保留位：1位
2、不分片：1位
3、最后一个片段：1位
4、偏移：13位。此片段在这个IP数据包中的位置
TTL：1字节。每经过1个路由器，其值自动减1
负载协议：1字节。IP数据包负载的协议
校验和：2字节。IP包头的校验和，不包括负载内容。
源地址：4字节。
目标地址：4字节。
选项：长度可变。一般情况下没有此内容
填充：长度可变。
负载：

因为TTL经过路由器时会改变，所以IP包头的校验和也需要重新计算。
 
*/