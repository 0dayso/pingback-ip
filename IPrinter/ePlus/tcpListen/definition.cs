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
        public byte ip_verlen; // �汾��4λ��IP�汾��,��ͷ����4λ����4�ֽ�Ϊ��λ����0101��ʾIP���İ�ͷ����20�ֽ�
        [FieldOffset(1)]
        public byte ip_tos; //����������1�ֽڡ�һ��ûʲô�á�ָʾ·������δ�������ݰ���TOS

        [FieldOffset(2)]
        public ushort ip_totallength; // IP���ݰ����ܳ���
        [FieldOffset(4)]
        public ushort ip_id; // ϵͳ��Χ�ڣ�ÿ����һ��IP������ֵ�Զ�����1��ûʲô��
        [FieldOffset(6)]
        public ushort ip_offset; //��Ƭ��־��1������λ��1λ 2������Ƭ��1λ 3�����һ��Ƭ�Σ�1λ 4��ƫ�ƣ�13λ����Ƭ�������IP���ݰ��е�λ��
        [FieldOffset(8)]
        public byte ip_ttl; // Time To Live
        [FieldOffset(9)]
        public byte ip_protocol; // protocol (TCP, UDP etc) ����Э��
        [FieldOffset(10)]
        public ushort ip_checksum; //У��ͣ�2�ֽڡ�IP��ͷ��У��ͣ���������������
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
                //����?���������� �������?�������� ?true
                //?Platform SDK �� �������� 2 ����?������?� ?����?����?������
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
IP���ݰ��ṹ2007-07-26 09:15IP���ݰ��ṹ��
�汾��4λ��IP�汾��
��ͷ����4λ����4�ֽ�Ϊ��λ����0101��ʾIP���İ�ͷ����20�ֽ�
����������1�ֽڡ�һ��ûʲô�á�ָʾ·������δ�������ݰ���TOS
�ܳ���2�ֽڡ�IP���ݰ����ܳ���
��ʶ��2�ֽڡ�ϵͳ��Χ�ڣ�ÿ����һ��IP������ֵ�Զ�����1��ûʲô��
��Ƭ��־��
1������λ��1λ
2������Ƭ��1λ
3�����һ��Ƭ�Σ�1λ
4��ƫ�ƣ�13λ����Ƭ�������IP���ݰ��е�λ��
TTL��1�ֽڡ�ÿ����1��·��������ֵ�Զ���1
����Э�飺1�ֽڡ�IP���ݰ����ص�Э��
У��ͣ�2�ֽڡ�IP��ͷ��У��ͣ��������������ݡ�
Դ��ַ��4�ֽڡ�
Ŀ���ַ��4�ֽڡ�
ѡ����ȿɱ䡣һ�������û�д�����
��䣺���ȿɱ䡣
���أ�

��ΪTTL����·����ʱ��ı䣬����IP��ͷ��У���Ҳ��Ҫ���¼��㡣
 
*/