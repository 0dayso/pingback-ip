using System;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
namespace EagleProtocal
{
    public class TypeOfCommand
    {
        /// <summary>
        /// ����ָ��
        /// </summary>
        public static UInt16 Single = 0;
        /// <summary>
        /// �������ָ��,��~�ָ�
        /// </summary>
        public static UInt16 Multi = 1;
        /// <summary>
        /// ����һָ��ڴ�ӡ�������г̵�ʱʹ��
        /// </summary>
        public static UInt16 ThreeInOne = 2;
        /// <summary>
        /// ǿ�Ʋ���ѯ��ĳЩָ���ڳ����д�����ʱʹ����ѯ����
        /// </summary>
        public static UInt16 UnTurn = 3;
        /// <summary>
        /// ���������Զ���ҳ�󣬷������������ע�⣺AVָ�Ҫʹ�á�
        /// </summary>
        public static UInt16 AutoPn = 99;
        /// <summary>
        /// �Զ���ҳ�ĺ�ָ̨��
        /// </summary>
        public static UInt16 AutoPnBack = 97;
    }
    public class EagleProtocal
    {
        public static string s_MsgNotFit = "��Ϣ���Ͳ���";
        public static UInt32 MsgNo = 0;
        /// <summary>
        /// �ṹ��תbyte����
        /// </summary>
        /// <param name="structObj">Ҫת���Ľṹ��</param>
        /// <returns>ת�����byte����</returns>
        public static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];
             IntPtr structPtr = Marshal.AllocHGlobal(size);
             Marshal.StructureToPtr(structObj, structPtr, false);
             Marshal.Copy(structPtr, bytes, 0, size);
             Marshal.FreeHGlobal(structPtr);
            return bytes;
         }
        /// <summary>
        /// byte����ת�ṹ��
        /// </summary>
        /// <param name="bytes">byte����</param>
        /// <param name="type">�ṹ������</param>
        /// <returns>ת����Ľṹ��</returns>
        public static object BytesToStuct(byte[] bytes,Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
            {
                return null;
             }
             IntPtr structPtr = Marshal.AllocHGlobal(size);
             Marshal.Copy(bytes,0,structPtr,size);
            object obj = Marshal.PtrToStructure(structPtr, type);
             Marshal.FreeHGlobal(structPtr);
            return obj;
         }
    }
    enum CHECK_RESULT : ushort
    {
        RET_SUCCESS = 0x0001,   //�ɹ�
        RET_FAIL = 0x1001,      //ʧ��
        RET_INVALID = 0x1002,   //��Ч����
        RET_NO_AUTH = 0x1003,   //��Ȩ��
        RET_AUTH_FAIL = 0x1004  //��֤ʧ��
    }
    enum MESSAGE_TYPE : ushort
    {
        CS_PASSPORT_CHECK = 0x0001,
        SC_PASSPORT_CHECK = 0x1001,
        CS_IP_REGISTER = 0x0002,
        SC_IP_REGISTER = 0x1002,
        CS_COMMAND = 0x0003,
        SC_COMMAND = 0x1003,
        CS_CONNECT_REMAIN = 0x0004,
        SC_CONNECT_REMAIN = 0x1004,
        CS_PASSPORT_CHECK_EX = 0x0005,
        CS_PROMOPT_SUBMIT_PNR = 0x0006,
        SC_PROMOPT_SUBMIT_PNR = 0x1006,
        CS_COMMAND_BACKGROUND = 0x0007,
        SC_COMMAND_BACKGROUND = 0x1007,
        CS_IP_LONELY = 0x0008,
        SC_IP_LONELY = 0x1008,
        CS_PROMOPT_NEW_APPLY = 0x0009,
        SC_PROMOPT_NEW_APPLY = 0x1009,
        CS_PROMOPT_FINISH_APPLY = 0x000A,
        SC_PROMOPT_FINISH_APPLY = 0x100A
    }
    /// <summary>
    /// Э��ͷ
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Header
    {
        [FieldOffset(0)]
        public UInt16 MsgType;//��Ϣ����,2�ֽ�
        [FieldOffset(2)]
        public UInt16 MsgLength;//��Ϣ����,2�ֽ�
        [FieldOffset(4)]
        public UInt32 MsgNo;//��ˮ��,4�ֽ�
        [FieldOffset(8)]
        public UInt32 Extend;//��չ,����,4�ֽ�
        public void FromBytes(byte[] b)
        {
            this = (Header)EagleProtocal.BytesToStuct(b, this.GetType());
        }
    }
    /// <summary>
    /// ���ͣ�PASSPORT��֤��
    /// </summary>
    [StructLayout(LayoutKind.Explicit,Pack=1)]
    public struct PACKET_CHECK_PASSPORT
    {
        [FieldOffset(0)]
        Header header;
        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        char[] passport;
        public PACKET_CHECK_PASSPORT(UInt32 MsgNo, string pspt)
        {
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_PASSPORT_CHECK;
            header.MsgLength = 44;//header + passport
            header.MsgNo = MsgNo;
            header.Extend = 0xFFFFFFFF;
            passport = pspt.ToCharArray();
        }
        public byte[] ToBytes()
        {
            return EagleProtocal.StructToBytes(this);
        }
    }
    /// <summary>
    /// ���գ�PASSPORT��֤�����
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct PACKET_CHECK_PASSPORT_RESULT
    {
        [FieldOffset(0)]
        Header header;
        [FieldOffset(12)]
        UInt16 result;
        public void FromBytes(byte[] b)
        {
            this =(PACKET_CHECK_PASSPORT_RESULT) EagleProtocal.BytesToStuct(b, this.GetType());
            if (header.MsgType != (ushort)MESSAGE_TYPE.SC_PASSPORT_CHECK)
                throw new Exception(EagleProtocal.s_MsgNotFit + header.MsgType.ToString());
        }
    }
    /// <summary>
    /// ���ͣ�IPע���
    /// </summary>
    public struct PACKET_IP_REGISTER
    {
        Header header;
        UInt16 ipcount;
        string[] iplist;
        public PACKET_IP_REGISTER(UInt32 MsgNo, UInt16 count, string[] ips)
        {
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_IP_REGISTER;
            header.MsgLength = (UInt16)(12 + 2 + count * 20);
            ipcount = count;
            header.MsgNo = MsgNo;
            header.Extend = 0x0;
            iplist = ips;
        }
        public byte[] ToBytes()
        {
            byte[] ret = new byte[header.MsgLength];
            byte[] b1 = EagleProtocal.StructToBytes(header);
            byte[] b2 = EagleProtocal.StructToBytes(ipcount);
            b1.CopyTo(ret, 0);
            b2.CopyTo(ret, b1.Length);
            int start_0 = b1.Length + b2.Length;
            for (int i = 0; i < ipcount; ++i)
            {
                int start = i * 20 + start_0;
                string ip = iplist[i];
                byte[] b = System.Text.Encoding.Default.GetBytes(ip.ToCharArray());
                for (int j = 0; j < 20; j++)
                {
                    if (j < b.Length) ret[start + j] = b[j];
                    else ret[start + j] = 0;
                }
            }
            return ret;
        }
    }
    /// <summary>
    /// ���գ�IPע������
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct PACKET_IP_REGISTER_RESULT
    {
        [FieldOffset(0)]
        Header header;
        [FieldOffset(12)]
        UInt16 result;
        public void FromBytes(byte[] b)
        {
            this = (PACKET_IP_REGISTER_RESULT)EagleProtocal.BytesToStuct(b, this.GetType());
            if (header.MsgType != (ushort)MESSAGE_TYPE.SC_IP_REGISTER)
                throw new Exception(EagleProtocal.s_MsgNotFit + header.MsgType.ToString());
        }
    }
    /// <summary>
    /// ���ͣ�ָ���(ǰ̨)
    /// </summary>
    public struct PACKET_COMMAND
    {
        Header header;
        UInt16 commandtype;
        UInt16 commandcount;
        string commands;
        public PACKET_COMMAND(UInt32 MsgNo,UInt16 CmdType,string cmds)
        {
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_COMMAND;
            header.MsgLength = (UInt16)(12 + 2 + 2 + System.Text.Encoding.Default.GetBytes(cmds).Length);
            header.MsgNo = MsgNo;
            header.Extend = 0xFFFFFFFF;
            commandtype = CmdType;
            commandcount = (UInt16)cmds.Split('~').Length;
            commands = cmds;
        }
        public byte[] ToBytes()
        {
            byte[] ret = new byte[header.MsgLength];
            byte[] b1 = EagleProtocal.StructToBytes(header);
            byte[] b2 = EagleProtocal.StructToBytes(commandtype);
            byte[] b3 = EagleProtocal.StructToBytes(commandcount);
            byte[] b4 = System.Text.Encoding.Default.GetBytes(commands);
            int start = 0;
            b1.CopyTo(ret, start);
            start += b1.Length;
            b2.CopyTo(ret, start);
            start += b2.Length;
            b3.CopyTo(ret, start);
            start += b3.Length;
            b4.CopyTo(ret, start);
            return ret;
        }
    }
    /// <summary>
    /// ���գ�ָ��ذ�(ǰ̨)
    /// </summary>
    public struct PACKET_COMMAND_RESULT
    {
        public Header header;
        byte[] packet_eterm;
        public void FromBytes(byte[] b)
        {
            byte[] b1 = new byte[12];
            for (int i = 0; i < b1.Length; ++i) 
                b1[i] = b[i];
            header =(Header)EagleProtocal.BytesToStuct(b1, header.GetType());
            if (header.MsgType != (ushort)MESSAGE_TYPE.SC_COMMAND)
                throw new Exception(EagleProtocal.s_MsgNotFit + header.MsgType.ToString());
            packet_eterm = new byte[b.Length - b1.Length];
            for (int i = 0; i < packet_eterm.Length; ++i)
                packet_eterm[i] = b[i + b1.Length];
        }
    }
    /// <summary>
    /// ���ͣ���������
    /// </summary>
    public struct PACKET_CONNECT_REMAIN
    {
        Header header;
        public PACKET_CONNECT_REMAIN(UInt32 MsgNo)
        {
            header.Extend = 0xFFFFFFFF;
            header.MsgLength = 12;
            header.MsgNo = MsgNo;
            header.MsgType = 4;
        }
        public byte[] ToBytes()
        {
            return EagleProtocal.StructToBytes(header);
        }
    }
    /// <summary>
    /// ���գ��������ӽ��
    /// </summary>
    public struct PACKET_CONNECT_REMAIN_RESULT
    {
        Header header;
        public void FromBytes(byte[] b)
        {
            header = (Header)EagleProtocal.BytesToStuct(b, header.GetType());
        }
    }
    /// <summary>
    /// ���ͣ���������л����ã�����ͬSC_PASSPORT_CHECK
    /// </summary>
    [StructLayout(LayoutKind.Explicit,Pack=1)]
    public struct PACKET_SERVER_SWITCH
    {
        [FieldOffset(0)]
        Header header;
        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        char[] passport;
        public PACKET_SERVER_SWITCH(UInt32 MsgNo, string pspt)
        {
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_PASSPORT_CHECK_EX;
            header.MsgLength = 44;//header + passport
            header.MsgNo = MsgNo;
            header.Extend = 0xFFFFFFFF;
            passport = pspt.ToCharArray();
        }
        public byte[] ToBytes()
        {
            return EagleProtocal.StructToBytes(this);
        }
    }
    /// <summary>
    /// ���ͣ���ʾ����PNR�ύ�Ķ���,���վ�һ��Header
    /// </summary>
    public struct PACKET_PROMOPT_SUBMIT_PNR
    {
        Header header;
        UInt16 count;
        string[] passport;
        public PACKET_PROMOPT_SUBMIT_PNR(UInt32 MsgNo, string[] pspt)
        {
            header.Extend = 0xFFFFFFFF;
            header.MsgLength = (UInt16)(12 + 2 + pspt.Length * 32);
            header.MsgNo = MsgNo;
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_PROMOPT_SUBMIT_PNR;
            passport = pspt;
            count = (UInt16)pspt.Length;
        }
        public byte[] ToBytes()
        {
            byte[] ret = new byte[header.MsgLength];
            byte[] b1 = EagleProtocal.StructToBytes(header);
            byte[] b2 = EagleProtocal.StructToBytes(count);
            b1.CopyTo(ret, 0);
            b2.CopyTo(ret, b1.Length);
            for (int i = 0; i < passport.Length; ++i)
            {
                string pspt = passport[i];
                byte[] t = System.Text.Encoding.Default.GetBytes(pspt);
                t.CopyTo(ret, b1.Length + b2.Length + i * 32);
            }
            return ret;
        }
    }
    /// <summary>
    /// ���ͣ�ָ���(��̨)
    /// </summary>
    public struct PACKET_COMMAND_BACKGROUND
    {
        Header header;
        UInt16 commandtype;
        UInt16 commandcount;
        string commands;
        public PACKET_COMMAND_BACKGROUND(UInt32 MsgNo, UInt16 CmdType, string cmds)
        {
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_COMMAND_BACKGROUND;
            header.MsgLength = (UInt16)(12 + 2 + 2 + System.Text.Encoding.Default.GetBytes(cmds).Length);
            header.MsgNo = MsgNo;
            header.Extend = 0xFFFFFFFF;
            commandtype = CmdType;
            commandcount = (UInt16)cmds.Split('~').Length;
            commands = cmds;
        }
        public byte[] ToBytes()
        {
            byte[] ret = new byte[header.MsgLength];
            byte[] b1 = EagleProtocal.StructToBytes(header);
            byte[] b2 = EagleProtocal.StructToBytes(commandtype);
            byte[] b3 = EagleProtocal.StructToBytes(commandcount);
            byte[] b4 = System.Text.Encoding.Default.GetBytes(commands);
            int start = 0;
            b1.CopyTo(ret, start);
            start += b1.Length;
            b2.CopyTo(ret, start);
            start += b2.Length;
            b3.CopyTo(ret, start);
            start += b3.Length;
            b4.CopyTo(ret, start);
            return ret;
        }
    }
    /// <summary>
    /// ���գ�ָ��ذ�(��̨)
    /// </summary>
    public struct PACKET_COMMAND_BACKGROUND_RESULT
    {
        public Header header;
        byte[] packet_eterm;
        public void FromBytes(byte[] b)
        {
            byte[] b1 = new byte[12];
            for (int i = 0; i < b1.Length; ++i)
                b1[i] = b[i];
            header = (Header)EagleProtocal.BytesToStuct(b1, header.GetType());
            if (header.MsgType != (ushort)MESSAGE_TYPE.SC_COMMAND_BACKGROUND)
                throw new Exception(EagleProtocal.s_MsgNotFit + header.MsgType.ToString());
            packet_eterm = new byte[b.Length - b1.Length];
            for (int i = 0; i < packet_eterm.Length; ++i)
                packet_eterm[i] = b[i + b1.Length];
        }
    }
    /// <summary>
    /// ���ͣ�����չ���ͬһ���ṹ��
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct PACKET_LONELY_USE
    {
        [FieldOffset(0)]
        Header header;
        /// <summary>
        /// ����ʱ��ʾipid������ʱ��ʾ���ؽ����0-ʧ�ܣ�1-�ɹ���2-��ռʱ�䵽
        /// </summary>
        [FieldOffset(12)]
        UInt32 var;
        public PACKET_LONELY_USE(UInt32 MsgNo, UInt32 ipid)
        {
            header.Extend = 0xFFFFFFFF;
            header.MsgLength = 16;
            header.MsgNo = MsgNo;
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_IP_LONELY;
            var = ipid;
        }
        public byte[] ToBytes()
        {
            return EagleProtocal.StructToBytes(this);
        }
        public void FromBytes(byte[] b)
        {
            this = (PACKET_LONELY_USE)EagleProtocal.BytesToStuct(b, this.GetType());
        }
    }
    /// <summary>
    /// ���ͣ���Kλ����ʾһ���µ������λ���룬���վ�ֻ��Header
    /// </summary>
    public struct PACKET_PROMOPT_NEW_APPLY
    {
        Header header;
        UInt16 count;
        string[] passport;
        public PACKET_PROMOPT_NEW_APPLY(UInt32 MsgNo, string[] pspt)
        {
            header.Extend = 0xFFFFFFFF;
            header.MsgLength = (UInt16)(12 +2+ pspt.Length * 32);
            header.MsgNo = MsgNo;
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_PROMOPT_NEW_APPLY;
            passport = pspt;
            count = (UInt16)pspt.Length;
        }
        public byte[] ToBytes()
        {
            byte[] ret = new byte[header.MsgLength];
            byte[] b1 = EagleProtocal.StructToBytes(header);
            byte[] b2 = EagleProtocal.StructToBytes(count);
            b1.CopyTo(ret, 0);
            b2.CopyTo(ret, b1.Length);
            for (int i = 0; i < passport.Length; ++i)
            {
                string pspt = passport[i];
                byte[] t = System.Text.Encoding.Default.GetBytes(pspt);
                t.CopyTo(ret, b1.Length + b2.Length + i * 32);
            }
            return ret;
        }
    }
    /// <summary>
    /// ���ͣ�������ϣ��������˷�����ʾ�����վ�ֻ��Header+content
    /// </summary>
    public struct PACKET_PROMOPT_FINISH_APPLAY
    {
        Header header;
        UInt16 count;
        string[] passport;
        string content;
        public PACKET_PROMOPT_FINISH_APPLAY(UInt32 MsgNo, string[] pspt,string text)
        {
            header.Extend = 0xFFFFFFFF;
            header.MsgLength = (UInt16)(12 +2+ pspt.Length * 32 + System.Text.Encoding.Default.GetBytes(text).Length);
            header.MsgNo = MsgNo;
            header.MsgType = (UInt16)MESSAGE_TYPE.CS_PROMOPT_FINISH_APPLY;
            passport = pspt;
            content = text;
            count = (UInt16)pspt.Length;
        }
        public byte[] ToBytes()
        {
            byte[] ret = new byte[header.MsgLength];
            byte[] b1 = EagleProtocal.StructToBytes(header);
            byte[] b2 = EagleProtocal.StructToBytes(count);
            b1.CopyTo(ret,0);
            b2.CopyTo(ret, b1.Length);
            for (int i = 0; i < passport.Length; ++i)
            {
                string pspt = passport[i];
                byte[] t = System.Text.Encoding.Default.GetBytes(pspt);
                t.CopyTo(ret, b1.Length + b2.Length + i * 32);
            }
            byte[] b3 = System.Text.Encoding.Default.GetBytes(content);
            b3.CopyTo(ret, 12 + 2 + passport.Length * 32);
            return ret;
        }
    }
    public struct PACKET_PROMOPT_FINISH_APPLY_RESULT
    {
        Header header;
        public string content;
        public void FromBytes(byte[] b)
        {
            byte[] b1 = new byte[12];
            for (int i = 0; i < b1.Length; ++i) b1[i] = b[i];
            header = (Header)EagleProtocal.BytesToStuct(b1, header.GetType());
            if (header.MsgType != (ushort)MESSAGE_TYPE.SC_PROMOPT_FINISH_APPLY)
                throw new Exception(EagleProtocal.s_MsgNotFit + header.MsgType.ToString());
            content = System.Text.Encoding.Default.GetString(b, b1.Length, b.Length - b1.Length);
        }
    }
}
