using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using EagleString;
using EagleProtocal;
using EagleControls;
using System.Runtime.InteropServices;

namespace EagleExtension
{
    /// <summary>
    /// ��ʼ�������ֵ���ĺ������InitInputArgs
    /// �����Է��ؽ�����ֽ����ϵĴ����ı�����ĺ��������PrimaryHandleResult.cs
    /// </summary>
    public class DataHandler
    {
        public bool BackGroundCommand { get { return m_backgroundcmd; } }
        private bool m_backgroundcmd = false;
        public PromoptType PROMOPTTYPE { get { return m_promoptype; } }
        public bool TYPE350 { get { return m_type350; } }
        /// <summary>
        /// Ҫ�ӵ������е��ı�
        /// </summary>
        public string Text { get { return m_txt; } }
        /// <summary>
        /// �ڱ��ദ������к������͵�����
        /// </summary>
        public byte[] BUFFER { get { return m_sendbyte; } }
        /// <summary>
        /// ָ���л����ú���ı�
        /// </summary>
        public string SPECIFICOFFICE { get { return m_in_specoffice; } set { m_in_specoffice = value; } }

        /// <summary>
        /// ���
        /// </summary>
        public string MONEY { get { return m_in_money; } set { m_in_money = value; } }

        /// <summary>
        /// ����������ı���ע����Text������
        /// </summary>
        public string COMMANDRESULT { get { return m_cmdres; } }
        public string COMMANDRESULT_BACK { get { return m_cmdres_back; } set { m_cmdres_back = ""; } }
        public string COMMANDRESULT99 { get { return m_result99; } }
        /// <summary>
        /// �洢ÿ���յ���2048���ֽ�
        /// </summary>
        byte[] m_buf;
        bool m_type350 = true;
        PromoptType m_promoptype;
        byte[] m_buf99;//�����Զ�pn

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        /// <param name="buf">��SOCKET�յ���BUFFER</param>
        /// <param name="lr">��¼���</param>
        /// <param name="MsgNo">socket��Ϣ��ˮ��</param>
        public void InitInputArgs(byte[] buf, EagleString.LoginInfo lr, uint MsgNo)
        {

            m_buf = new byte[buf.Length];
            m_tempBuffer = new byte[buf.Length];
            buf.CopyTo(m_buf, 0);
            buf.CopyTo(m_tempBuffer, 0);

            m_in_username = lr.b2b.username;
            m_in_lr = lr;
            m_in_MsgNo = MsgNo;

        }
        /// <summary>
        /// Ҫ�л���Ŀ������
        /// </summary>
        string[] m_ipid_dest;
        public void SetDestConfigIpids(string[] ipid)
        {
            m_ipid_dest = ipid;
            m_in_specoffice = m_in_lr.b2b.lr.IpidsText(ipid);
        }
        bool m_passport = false;

        /// <summary>
        /// �������Ҫ����ʾ���ı�,�Լ�Ҫ������ı���ʾ����,û�������
        /// </summary>
        string m_txt = "";
        /// <summary>
        /// ָ�����ı�,��ӦCOMMANDRESULT
        /// </summary>
        string m_cmdres = "";
        /// <summary>
        /// ָ�����ı�,��ӦCOMMANDRESULT_BACK
        /// </summary>
        string m_cmdres_back = "";
        /// <summary>
        /// �������õ�Ҫ���͵�����,û�������
        /// </summary>
        byte[] m_sendbyte;

        string m_in_money;
        string m_in_username;
        EagleString.LoginInfo m_in_lr;
        uint m_in_MsgNo;
        string m_in_specoffice;

        /// <summary>
        /// �յ������Э���־
        /// </summary>
        public enum TypeOfRecv:byte
        {
            Passport = 0,
            IpRegister = 1,
            CommandForeGround=3,
            CommandBackGround=7,
            RemainConnect=4,
            NewPnrOrder=6,
            NewSpecBunkApply=9,
            BunkApplyHandleFinish=10,
            None = 100
        }

        public TypeOfRecv typeOfRecv = new TypeOfRecv();

        public void recvHandle()
        {
            typeOfRecv = TypeOfRecv.None;
            m_txt = "";
            m_sendbyte = null;
            m_promoptype = PromoptType.NoThing;
            if (m_buf[0] == 0x01 && m_buf[1] == 0x10)//Passport Check                   --Finished
            {
                typeOfRecv = TypeOfRecv.Passport;
                HandlePassport();
            }
            else if (m_buf[0] == 0x02 && m_buf[1] == 0x10)//Register IP                 --Finished
            {
                typeOfRecv = TypeOfRecv.IpRegister;
                HandleIpRegister();
            }
            else if (m_buf[0] == 0x03 && m_buf[1] == 0x10)//Foreground Command          --Finished
            {
                typeOfRecv = TypeOfRecv.CommandForeGround;

                m_backgroundcmd = false;
                PACKET_COMMAND_RESULT packet = new PACKET_COMMAND_RESULT();
                packet.FromBytes(m_buf);
                m_totalLengthOfBuffer = packet.header.MsgLength;
                if (m_totalLengthOfBuffer > 2048)
                {
                    HandleContinuePacket();
                }
                else
                {
                    HandleResult_ForeGround(m_buf);
                }
            }
            else if (m_buf[0] == 0x07 && m_buf[1] == 0x10)//Background Command
            {
                typeOfRecv = TypeOfRecv.CommandBackGround;
                m_backgroundcmd = true;
                HandleResult_BackGround();
            }
            else if (m_buf[0] == 0x04 && m_buf[1] == 0x10)//Remain Connect              --Finished
            {
                typeOfRecv = TypeOfRecv.RemainConnect;
                HandleRemainConnect();
            }
            else if (m_buf[0] == 0x06 && m_buf[1] == 0x10)//New Order Promopt           --Finished
            {
                typeOfRecv = TypeOfRecv.NewPnrOrder;
                m_promoptype = PromoptType.NewOrder;
            }
            else if (m_buf[0] == 0x09 && m_buf[1] == 0x10)//New Specify Bunk Apply      --Finished
            {
                typeOfRecv = TypeOfRecv.NewSpecBunkApply;
                m_promoptype = PromoptType.NewApply;
            }
            else if (m_buf[0] == 0x0A && m_buf[1] == 0x10)//Applied Bunk Handled        --Finished
            {
                typeOfRecv = TypeOfRecv.BunkApplyHandleFinish;
                m_promoptype = PromoptType.ApplyHandled;
            }
            else if (m_buf[0] == 0x00 && m_buf[1] == 0x00)//DisConnected                --Finished
            {
                System.Windows.Forms.MessageBox.Show("�������Ͽ�����������!");
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                HandleContinuePacket();
            }
            m_buf = null;
        }
        private void HandlePassport()
        {
            m_passport = false;
            if (m_buf[12] == 0x01 && m_buf[13] == 0x00)//Passed
            {
                m_passport = true;
                m_txt = m_in_username
                    + ": ��ӭʹ��[�׸񺽿ն�Ʊϵͳ2.0]���Ѿ�ͨ��������֤��\r\n>";
                
                m_txt += "�����ʻ����Ϊ����" + m_in_money + "\r\n>";

                if (m_ipid_dest == null) m_ipid_dest = m_in_lr.b2b.lr.IpidsWhenLogin(ref m_in_specoffice).ToArray();

                try
                {
                    EagleFileIO.LogWrite("ͨ����֤,��ע��Ŀ������" + string.Join(",", m_ipid_dest));
                }
                catch
                {
                    EagleFileIO.LogWrite("m_ipid_destΪnull!");
                }

                if (m_ipid_dest == null || m_ipid_dest.Length == 0)
                {
                    m_txt += "��û�п������á���\r\n>";
                }
                else
                {
                    PACKET_IP_REGISTER packet = new PACKET_IP_REGISTER((uint)m_in_MsgNo,
                        (ushort)m_ipid_dest.Length,
                         m_ipid_dest);
                    m_sendbyte = packet.ToBytes();

                }
            }
            else if (m_buf[12] == 0x04 && m_buf[13] == 0x10)//UnPassed
            {
                m_txt = "δ��ͨ�����������֤����\r\n";//Reasons:1,Client and Server not connect the same WebServer 2,Server Error Occer
            }
            else//Error
            {
                m_txt = "�����֤�������󡭡�\r\n>";
            }
        }

        private void HandleIpRegister()
        {
            int lparam = 1;
            if (m_buf[12] == 0x01 && m_buf[13] == 0x00)
            {
                m_txt = "�л�����(" + m_in_specoffice + ")�ɹ�����OK\r\n>";

                m_in_lr.b2b.lr.SwitchConfig(m_ipid_dest);
            }
            else
            {
                lparam = 0;
                m_txt = "�л�����(" + m_in_specoffice + ")ʧ�ܡ���\r\n>";
            }
            Imported.SendMessage(0xFFFF, (int)Imported.egMsg.SWITCH_CONFIG_RESULT, 0, lparam);//�������ù���
        }

        private int m_totalLengthOfBuffer;
        /// <summary>
        /// �����ʱ�洢
        /// </summary>
        private byte[] m_tempBuffer;
        private ArrayList m_lsBuffer = new ArrayList();
        private void HandleContinuePacket()
        {
            if (m_lsBuffer.Count < m_totalLengthOfBuffer)
            {
                ArrayList b = ArrayList.Adapter(m_tempBuffer);
                m_lsBuffer.AddRange(b);
            }
            if (m_lsBuffer.Count >= m_totalLengthOfBuffer)//������else�������ۼӺ�Ҫ�����ж�
            {
                byte[] b = (byte[])m_lsBuffer.ToArray(typeof(byte));
                HandleResult_ForeGround(b);
                m_lsBuffer.Clear();
            }
        }

        string m_result99 = "";
        object o = new object();
        /// <summary>
        /// ժҪ����ȡ��Ҫ��ʾ���ı�����������������ִ��
        /// </summary>
        private string HandleResult_ForeGround(byte[] m_buf)
        {
            lock (o)
            {
                m_type350 = (m_buf[12] < 0x10);

                if (m_buf[0] == 0x07 && m_buf[1] == 0x10) m_backgroundcmd = true;
                else m_backgroundcmd = false;
                string t = "";
                m_result99 = "";
                if (m_type350)
                {
                    m_buf99 = new byte[m_buf.Length - 12];
                    for (int i = 0; i < m_buf99.Length; ++i)
                    {
                        m_buf99[i] = m_buf[i + 12];
                    }
                    List<byte> temp = new List<byte>();
                    //for (int i = 0; i < m_buf99.Length - 4; ++i)
                    {
                        int i = 0;
                        while (i < m_buf99.Length - 4)
                        {
                            if (m_buf99[i] == 0x1E &&
                                m_buf99[i + 1] == 0x1B &&
                                m_buf99[i + 2] == 0x62 &&
                                m_buf99[i + 3] == 0x03)
                            {
                                temp.Add(m_buf99[i]);//����1E
                                string t2 = System.Text.Encoding.Default.GetString(temp.ToArray(), 19, temp.Count - 19);
                                t2 = textOfDisplay(t2);
                                m_result99 += t2 + "\n";

                                temp.Clear();
                                i = i + 4;
                            }
                            else
                            {
                                temp.Add(m_buf99[i]);
                                ++i;
                            }
                        }
                    }
                    t = m_result99;
                    if (t == "")//���:���Ű�һ����Ϣ�ֳ��˶������������һ����1E 1B 62 03 ������û��
                    {
                        t = System.Text.Encoding.Default.GetString(m_buf99, 19, m_buf99.Length - 19);
                    }

                }
                else//�������¿��ܱ���Ϊ��443,Ҫ��1E 1B 62 03 �滻��">"
                {
                    int headlen = 12;
                    t = System.Text.Encoding.Default.GetString(m_buf, headlen, m_buf.Length - headlen);
                }
                t = egString.trim(t);
                t = textOfDisplay(t);
                m_txt = t;
                m_cmdres = t.Replace("\x1C", " ").Replace("\x1D", " ");
                if (m_backgroundcmd)
                {
                    m_cmdres_back = m_cmdres;
                    m_cmdres = "";
                }
                return m_cmdres;
            }
        }
        /// <summary>
        /// �滻1E 1B 62 03
        /// </summary>
        /// <param name="t2"></param>
        /// <returns></returns>
        private string textOfDisplay(string t2)
        {
            t2 = EagleString.egString.cutEtermHead_1B4D(t2);
            return EagleString.egString.trim(
                recv_chinese_USAS2GB(recv_add_newline(t2))
                            .Replace("\x1B\x62\x03", "")
                            .Replace("\x1B\x62", "")
                            .Replace("\x1Bb", "")
                            .Replace("\x1B", "")
                            .Replace("\x1E", ">")
                            );
        }
        private string recv_add_newline(string s)
        {
            try
            {
                string[] a = s.Split(new string[] { "\x0D" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < a.Length; ++i)
                {
                    a[i] = a[i].Replace(" >", "\n>");
                }
                a = string.Join("\n", a).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < a.Length; ++i)
                {
                    string e = a[i];
                    int count = e.Length / 80;
                    int remain = e.Length % 80;
                    if (remain == 0) --count;
                    for (int j = count; j > 0; --j)
                    {
                        e = e.Substring(0, j * 80) + "\r\n" + e.Substring(j * 80);
                    }
                    a[i] = e;
                }
                return string.Join("\n", a);
            }
            catch
            {

                return "***�ָ�>80����ʱ��������\r\n>" + s;
            }
        }

        private string recv_chinese_USAS2GB(string s)
        {
            try
            {
                string flag = string.Format("{0}{1}", (char)'\x1B', (char)'\x0E');
                string flag2 = string.Format("{0}{1}", (char)'\x1B', (char)'\x0F');
                string[] a = s.Split(new string[] { flag }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < a.Length; ++i)
                {
                    string[] b = a[i].Split(new string[] { flag2 }, StringSplitOptions.RemoveEmptyEntries);
                    string cn = b[0];
                    byte[] temp = new byte[cn.Length];
                    for (int j = 0; j < cn.Length/2; ++j)
                    {
                        byte c1 = (byte)cn[j * 2];
                        byte c2 = (byte)cn[j * 2 + 1];
                        if (!(c1 >= 128 || c2 >= 128))
                        {
                            byte u1 = c1;
                            byte u2 = c2;
                            if (u1 >= 0x25 && u1 <= 0x28)
                            {
                                byte t = u1;
                                u1 = u2;
                                u2 = (byte)((int)t + 10);
                            }
                            if (u1 > 0x24)
                                c1 = (byte)((int)u1 - 0x20 + 0xa0);
                            else
                                c1 = (byte)((int)u1 - 0x20 + 14 + 0xa0);
                            c2 = (byte)((int)u2 - 0x20 + 0xa0);
                        }
                        temp[j * 2] = c1;
                        temp[j * 2 + 1] = c2;
                    }
                    a[i] = System.Text.Encoding.Default.GetString(temp) + b[1];

                }
                return string.Join("", a);
            }
            catch
            {
                //return "***����ת�������˴���\r\n>" + s;
                StringBuilder strClaw = new StringBuilder("", 4096);
                GetReturnString(s, s.Length, strClaw);
                s = strClaw.ToString();
                ChineseCodeRecieve(s, strClaw);
                s = strClaw.ToString();
                return s;
            }
        }

        [DllImport("TestDll.dll", EntryPoint = "ChineseCodeRecieve", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern unsafe static string ChineseCodeRecieve([MarshalAs(UnmanagedType.LPStr)]string cmd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);

        [DllImport("TestDll.dll", EntryPoint = "GetReturnString", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern unsafe static string GetReturnString([MarshalAs(UnmanagedType.LPStr)]string cmd, int psize, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);

        /// <summary>
        /// the command like:rt,detr,tpr,tsl
        /// </summary>
        private void HandleResult_BackGround()
        {
            
            PACKET_COMMAND_BACKGROUND_RESULT packet = new PACKET_COMMAND_BACKGROUND_RESULT();
            packet.FromBytes(m_buf);
            m_totalLengthOfBuffer = packet.header.MsgLength;
            if (m_totalLengthOfBuffer > 2048)
            {
                HandleContinuePacket();
            }
            else
            {
                HandleResult_ForeGround(m_buf);
            }
        }

        private void HandleRemainConnect()
        {
            if (m_buf.Length <= 12)
            {
                System.Windows.Forms.MessageBox.Show("�����ʺ��ڱ��ظ���½", "�������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                if (m_buf[12] == 0x00 && m_buf[13] == 0x00)
                {

                }
                else if (m_buf[12] == 0x01 && m_buf[13] == 0x00)
                {
                    m_txt = "��������æ��";
                }
                else if (m_buf[12] == 0x02 && m_buf[13] == 0x00)
                {
                    m_txt = "�񺽴���ָ�ʱ��";
                }
            }
        }
    }
    /// <summary>
    /// ��ʾ��Ϣ���ͣ��¶������������
    /// </summary>
    public enum PromoptType
    {
        /// <summary>
        /// �¶�����ʾ
        /// </summary>
        NewOrder,
        /// <summary>
        /// ��������ʾ
        /// </summary>
        NewApply,
        /// <summary>
        /// ���봦�����
        /// </summary>
        ApplyHandled,
        /// <summary>
        /// ��
        /// </summary>
        NoThing
    }
    /// <summary>
    /// ���������¶���һ���ֶ�η��ص����
    /// </summary>
    public class MultiReturn
    {
        byte[] buffer;

        /// <summary>
        /// buf�����������׸�ͷ
        /// </summary>
        /// <param name="buf"></param>
        public byte[] handleOneReturn(byte[] buf)
        {
            if (buffer == null && PacketFirst(buf) && !PacketHas1E1B6203(buf))
            {
                AddFirstBuffer(buf, true);
            }
            if (buffer != null && !PacketFirst(buf) && !PacketHas1E1B6203(buf))
            {
                AddMiddleBuffer(buf, true);
            }
            if (buffer != null && !PacketFirst(buf) && PacketHas1E1B6203(buf))
            {
                AddLastBuffer(buf, true);
                byte[] ret = new byte[buffer.Length];
                buffer.CopyTo(ret, 0);
                buffer = null;
                return ret;
            }
            return null;
        }
        /// <summary>
        /// buf�����������׸�ͷ
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        bool PacketFirst(byte[] buf)
        {
            ///����350Э���һ���ֽ�Ϊ01
            return (buf[12] < 0x10);
        }
        bool PacketHas1E1B6203(byte[] buf)
        {
            for (int i = 0; i < buf.Length - 4; i++)
            {
                if (buf[i] == 0x1E &&
                                buf[i + 1] == 0x1B &&
                                buf[i + 2] == 0x62 &&
                                buf[i + 3] == 0x03)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// buf��������Э��ͷ
        /// </summary>
        /// <param name="buf"></param>
        void AddFirstBuffer(byte[] buf,bool bufIncludeEagleHead)
        {
            if (!bufIncludeEagleHead)
            {
                buffer = new byte[buf.Length];
                buf.CopyTo(buffer, 0);
            }
            else
            {
                buffer = new byte[buf.Length - 12];
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = buf[i + 12];
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        void AddMiddleBuffer(byte[] buf, bool bufIncludeEagleHead)
        {
            byte[] temp = new byte[buffer.Length + buf.Length - (bufIncludeEagleHead ? 12 : 0)];
            buffer.CopyTo(temp, 0);
            if (bufIncludeEagleHead)
            {
                for (int i = 0; i < buf.Length - 12; i++)
                {
                    temp[i + buffer.Length] = buf[i + 12];
                }
            }
            else
            {
                buf.CopyTo(temp, buffer.Length);
            }
            buffer = new byte[temp.Length];
            temp.CopyTo(buffer, 0);
        }
        void AddLastBuffer(byte[] buf, bool bufIncludeEagleHead)
        {
            AddMiddleBuffer(buf, bufIncludeEagleHead);
        }
    }
}
