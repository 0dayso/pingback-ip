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
        public byte ip_verlen; // °æ±¾£º4Î»¡£IP°æ±¾ºÅ,°üÍ·³¤£º4Î»¡£ÒÔ4×Ö½ÚÎªµ¥Î»£¬Èç0101±íÊ¾IP°üµÄ°üÍ·³¤¶È20×Ö½Ú
        [FieldOffset(1)]
        public byte ip_tos; //·şÎñÖÊÁ¿£º1×Ö½Ú¡£Ò»°ãÃ»Ê²Ã´ÓÃ¡£Ö¸Ê¾Â·ÓÉÆ÷ÈçºÎ´¦Àí¸ÃÊı¾İ°ü¡£TOS

        [FieldOffset(2)]
        public ushort ip_totallength; // IPÊı¾İ°üµÄ×Ü³¤¶È
        [FieldOffset(4)]
        public ushort ip_id; // ÏµÍ³·¶Î§ÄÚ£¬Ã¿·¢³öÒ»¸öIP°ü£¬ÆäÖµ×Ô¶¯Ôö¼Ó1¡£Ã»Ê²Ã´ÓÃ
        [FieldOffset(6)]
        public ushort ip_offset; //·ÖÆ¬±êÖ¾£º1¡¢±£ÁôÎ»£º1Î» 2¡¢²»·ÖÆ¬£º1Î» 3¡¢×îºóÒ»¸öÆ¬¶Î£º1Î» 4¡¢Æ«ÒÆ£º13Î»¡£´ËÆ¬¶ÎÔÚÕâ¸öIPÊı¾İ°üÖĞµÄÎ»ÖÃ
        [FieldOffset(8)]
        public byte ip_ttl; // Time To Live
        [FieldOffset(9)]
        public byte ip_protocol; // protocol (TCP, UDP etc) ¸ºÔØĞ­Òé
        [FieldOffset(10)]
        public ushort ip_checksum; //Ğ£ÑéºÍ£º2×Ö½Ú¡£IP°üÍ·µÄĞ£ÑéºÍ£¬²»°üÀ¨¸ºÔØÄÚÈİ
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
                //íóæí?óñòàíîâèòü âõîäÿùè?ïàğàìåòğ ?true
                //?Platform SDK îí çàíèìàåò 2 ñëîâ?ïîıòîì?ÿ ?çäåñ?òàêæ?ñäåëàë
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
IPÊı¾İ°ü½á¹¹2007-07-26 09:15IPÊı¾İ°ü½á¹¹£º
°æ±¾£º4Î»¡£IP°æ±¾ºÅ
°üÍ·³¤£º4Î»¡£ÒÔ4×Ö½ÚÎªµ¥Î»£¬Èç0101±íÊ¾IP°üµÄ°üÍ·³¤¶È20×Ö½Ú
·şÎñÖÊÁ¿£º1×Ö½Ú¡£Ò»°ãÃ»Ê²Ã´ÓÃ¡£Ö¸Ê¾Â·ÓÉÆ÷ÈçºÎ´¦Àí¸ÃÊı¾İ°ü¡£TOS
×Ü³¤£º2×Ö½Ú¡£IPÊı¾İ°üµÄ×Ü³¤¶È
±êÊ¶£º2×Ö½Ú¡£ÏµÍ³·¶Î§ÄÚ£¬Ã¿·¢³öÒ»¸öIP°ü£¬ÆäÖµ×Ô¶¯Ôö¼Ó1¡£Ã»Ê²Ã´ÓÃ
·ÖÆ¬±êÖ¾£º
1¡¢±£ÁôÎ»£º1Î»
2¡¢²»·ÖÆ¬£º1Î»
3¡¢×îºóÒ»¸öÆ¬¶Î£º1Î»
4¡¢Æ«ÒÆ£º13Î»¡£´ËÆ¬¶ÎÔÚÕâ¸öIPÊı¾İ°üÖĞµÄÎ»ÖÃ
TTL£º1×Ö½Ú¡£Ã¿¾­¹ı1¸öÂ·ÓÉÆ÷£¬ÆäÖµ×Ô¶¯¼õ1
¸ºÔØĞ­Òé£º1×Ö½Ú¡£IPÊı¾İ°ü¸ºÔØµÄĞ­Òé
Ğ£ÑéºÍ£º2×Ö½Ú¡£IP°üÍ·µÄĞ£ÑéºÍ£¬²»°üÀ¨¸ºÔØÄÚÈİ¡£
Ô´µØÖ·£º4×Ö½Ú¡£
Ä¿±êµØÖ·£º4×Ö½Ú¡£
Ñ¡Ïî£º³¤¶È¿É±ä¡£Ò»°ãÇé¿öÏÂÃ»ÓĞ´ËÄÚÈİ
Ìî³ä£º³¤¶È¿É±ä¡£
¸ºÔØ£º

ÒòÎªTTL¾­¹ıÂ·ÓÉÆ÷Ê±»á¸Ä±ä£¬ËùÒÔIP°üÍ·µÄĞ£ÑéºÍÒ²ĞèÒªÖØĞÂ¼ÆËã¡£
 
*/