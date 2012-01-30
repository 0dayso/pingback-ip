using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ePlus
{
    //����Э�鲽�裺1.�޸�SetMsgLenth��2.�޸�ConvertToString
    //��������δ�õ�����
    class ProtocalFunc
    {
        /// <summary>
        /// ָ������
        /// </summary>
        /// <param name="ipstrings">��~�ָ�Ķ��IP��</param>
        /// <returns></returns>
        public static char[] SpecifyCFG(string ipstrings)
        {//ipstringsΪ��~����Ķ��IP��
            string[] ipls = ipstrings.Split('~');
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 2;
            ep.IPAddress = ipls;
            ep.IPCount = (UInt16)ep.IPAddress.Length;
            ep.SetMsgLength();
            char[] sendstring = ep.ConvertToString(out outlength);
            return sendstring;
        }
        /// <summary>
        /// ָ��Passport
        /// </summary>
        /// <returns></returns>
        public static char[] SpecifyPassport(string pp)
        {
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 1;
            ep.MsgBody = pp;
            ep.SetMsgLength();
            char[] sendstring = ep.ConvertToString(out outlength);
            return sendstring;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public static byte[] RemainConnect()
        {
            int it = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgBody = "";
            ep.MsgType = 4;
            ep.SetMsgLength();
            char[] temp = ep.ConvertToString(out it);
            byte[] bTemp = new byte[it];
            for (int i = 0; i < it; i++)
            {
                bTemp[i] = (byte)temp[i];
            }
            return bTemp;
        }
        /// <summary>
        /// ������ͨ��Ʊָ��
        /// </summary>
        /// <param name="cc">ָ��ַ�</param>
        /// <returns></returns>
        public static char[] SendCommenCommand(string cc)
        {
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 3;
            ep.cmdType = 1;
            ep.MsgBody = cc;
            ep.SetMsgLength();
            char[] sendstring1 = ep.ConvertToString(out outlength);
            return sendstring1;
        }
        /// <summary>
        /// ��������һָ��
        /// </summary>
        /// <param name="pc">����һ��ָ�</param>
        /// <returns></returns>
        public static char[] SendPrintCommand(string pc)
        {
            int outlength = 0;
            EagleProtocol ep = new EagleProtocol();
            ep.MsgType = 3;
            ep.cmdType = 2;
            ep.MsgBody = pc;
            ep.SetMsgLength();
            char[] sendstring = ep.ConvertToString(out outlength);
            return sendstring;

        }
    }
    class EagleProtocol
    {
        static public bool b_passport = false;//true��ʱ�򣬷��ͱ��������ź�
        static public int segstep = 0;

        public UInt16 MsgType;//�������Ӧ����
        public UInt16 MsgLength;//��Ϣ�ܳ���(����Ϣͷ����Ϣ��)
        public UInt32 SegNo;//��Ϣ��ˮ��,˳���ۼ�,����Ϊ1,ѭ��ʹ�ã�һ�������Ӧ����Ϣ����ˮ�ű�����ͬ��
        public UInt32 Extend;//��������չ��
        //ͷ��12�ֽ�

        public string MsgBody;
        public UInt16 IPCount;
        public string[] IPAddress;
        public UInt16 cmdType;
        public UInt16 cmdCount;
        public string cmdstring;



        public EagleProtocol()
        {
            Extend = 0;
            IPCount = 0;
            SegNo = (UInt32)(segstep);
            segstep++;
        }
        public void SetMsgLength()
        {
            if (MsgType == 1 || MsgType == 5)
                MsgLength = 44;//(UInt16)(sizeof(UInt16) + sizeof(UInt16) + sizeof(UInt32) + sizeof(UInt32) + 32);
            else if (MsgType == 2)
                MsgLength = (UInt16)(12 + 2 + IPCount * 20);//(UInt16)(sizeof(UInt16) + sizeof(UInt16) + sizeof(UInt32) + sizeof(UInt32) + 32);
            else if (MsgType == 3 || MsgType == 7)
            {
                if (cmdType == 0) MsgLength = (UInt16)(12 + 2 + cmdstring.Length);
                if (cmdType == 1 || cmdType == 3)
                {
                    MsgLength = (UInt16)(12 + 2 + 2 + cmdstring.Length);
                    cmdCount = (UInt16)(cmdstring.Split('~').Length-1);
                }
                if (cmdType == 2)
                {
                    MsgLength = (UInt16)(12+2 + MsgBody.Length / 2);//����һ���в��ɼ��ַ���������ĳ��ȳ�2����
                }
            }
            else if (MsgType == 4)
            {
                MsgLength = 12;
            }
            else if (MsgType == 6)
            {
                MsgLength = (UInt16)(12 + 2 + MsgBody.Length);
            }
            else if (MsgType == 8)
            {
                MsgLength = (UInt16)(12 + 4);
            }
        }

        //[DllImport("TestDll.dll", EntryPoint = "GetSendPacket", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        //public extern unsafe static string GetSendPacket([MarshalAs(UnmanagedType.LPStr)]string cmd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);

        public char[] ConvertToString(out int retLength)
        {
            string ret;
            //Э��ͷת����16���ƣ���22����
            ret = converse(MsgType.ToString("x4") + MsgLength.ToString("x4") + SegNo.ToString("x8") + Extend.ToString("x8"));


            string body="";
            //��Ϣ����Ϊ1,passport��֤
            #region passport if (MsgType == 1)
            if (MsgType == 1)
            {
                body = MsgBody;
                char[] output = new char[12 + 32];
                for (int i = 0; i < 12; i++)
                {
                    //Э��ͷ����output��ǰ12���ֽ�
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                //passport����output�ĺ�32���ֽ�
                if (body.Length < 32)
                {
                    for (int i = 0; i < body.Length; i++)
                    {
                        output[i + 12] = body[i];
                    }
                    for (int i = body.Length; i < output.Length -12 -body.Length; i++)
                    {
                        output[i + 12] = '\0';
                    }
                }
                else
                {
                    body = body.Substring(0, 32);
                    for (int i = 0; i < body.Length; i++)
                    {
                        output[i + 12] = body[i];
                    }
                }
                retLength = output.Length;
                return output;
            }
            #endregion
            //��Ϣ����Ϊ5,passport��֤����������֤
            #region passport if (MsgType == 5)
            if (MsgType == 5)
            {
                body = MsgBody;
                char[] output = new char[12 + 32];
                for (int i = 0; i < 12; i++)
                {
                    //Э��ͷ����output��ǰ12���ֽ�
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                //passport����output�ĺ�32���ֽ�
                if (body.Length < 32)
                {
                    for (int i = 0; i < body.Length; i++)
                    {
                        output[i + 12] = body[i];
                    }
                    for (int i = body.Length; i < output.Length - 12 - body.Length; i++)
                    {
                        output[i + 12] = '\0';
                    }
                }
                else
                {
                    body = body.Substring(0, 32);
                    for (int i = 0; i < body.Length; i++)
                    {
                        output[i + 12] = body[i];
                    }
                }
                retLength = output.Length;
                return output;
            }
#endregion
            #region if (MsgType == 2)//ip information
            if (MsgType == 2)//ip information
            {
                //����֮ǰ��IPAddress���鸳ֵ
                if (IPAddress.Length <= 0) ;//û�и�IPAddressֵ
                else
                {
                    IPCount = (UInt16)IPAddress.Length;
                    //MsgLength = (UInt16)(12 + 2 + IPCount * 20);
                    char[] output = new char[12 + 2 + IPCount * 20];
                    for(int i = 0;i<output.Length;i++)
                    {
                        output[i] = '\0';
                    }
                    //Э��ͷ12�ֽڸ���output��ǰ12���ֽ�
                    for (int i = 0; i < 12; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    ret = converse(IPCount.ToString("x4"));
                    for (int i = 12; i < 14; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring((i-12) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    for (int i = 0; i < (int)(IPCount); i++)
                    {
                        for(int j= 0;j<IPAddress[i].Length;j++)
                        {
                            output[i * 20 + 14 + j] = IPAddress[i][j];
                        }
                    }
                    retLength = MsgLength;
                    return output;
                }
            }
            #endregion
            #region if (MsgType == 3 || MsgType == 7)//cmd��Э��7���ͻ��˺�̨����
            if (MsgType == 3 || MsgType == 7)//cmd
            {
                if (cmdType == 0)
                {
                    char[] output = new char[12 + 2 + MsgBody.Length+1];
                    for (int i = 0; i < 12; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    ret = converse(cmdType.ToString("x4"));
                    for (int i = 12; i < 14; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring((i - 12) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    for (int i = 14; i < output.Length-1; i++)
                    {
                        output[i] = MsgBody[i-14];
                    }
                    retLength = output.Length;
                    output[output.Length - 1] = '\0';
                    return output;
                }
                if (cmdType == 1 || cmdType ==3)
                {

                    char[] output = new char[12 + 2 + 2 + MsgBody.Length +1];
                    
                    for (int i = 0; i < 12; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    ret = converse(cmdType.ToString("x4"));
                    for (int i = 12; i < 14; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring((i - 12) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    ret = converse(cmdCount.ToString("x4"));
                    for (int i = 14; i < 16; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring((i - 14) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    for (int i = 16; i < output.Length-1; i++)
                    {
                        output[i] = MsgBody[i - 16];
                    }
                    char[] tempMsg = MsgBody.ToCharArray();
                    int bodylength = System.Text.Encoding.Default.GetBytes(tempMsg).Length;
                    //�ı�����ֽ��ܳ�
                    retLength = output.Length - MsgBody.Length + bodylength;
                    output[output.Length - 1] = '\0';
                    ret = converse(retLength.ToString("x4"));
                    for (int i = 0; i < 2; i++)
                    {
                        output[i + 2] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    //���س���Ϊ�ַ����ܳ�
                    retLength = output.Length;
                    return output;
                }
                if (cmdType == 2)
                {
                    //MsgBodyΪ��̬��������16���ƴ�
                    //ԭchar[] output = new char[12 + 2 + MsgBody.Length/2];
                    char[] output = new char[12 + 2 + 2 + MsgBody.Length + 1];
                    for (int i = 0; i < 12; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    ret = converse(cmdType.ToString("x4"));
                    for (int i = 12; i < 14; i++)
                    {
                        output[i] = (char)int.Parse(ret.Substring((i - 12) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }

                    for (int i = 16; i < output.Length-1; i++)
                    {
                        int j = i - 16;
                        //ԭoutput[i] = (char)(int.Parse(MsgBody[2 * j].ToString(),System.Globalization.NumberStyles.HexNumber)*16
                        //    + int.Parse(MsgBody[2 * j + 1].ToString(),System.Globalization.NumberStyles.HexNumber));
                        output[i] = MsgBody[j];
                    }
                    retLength = output.Length;
                    ret = converse(retLength.ToString("x4"));
                    for (int i = 0; i < 2; i++)
                    {
                        output[i + 2] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    output[output.Length - 1] = '\0';
                    return output;
                }
            }
            #endregion
            #region if (MsgType == 4)//remain connect
            if (MsgType == 4)//remain connect
            {
                body = "";
                ret = converse(MsgType.ToString("x4") + MsgLength.ToString("x4") + SegNo.ToString("x8") + Extend.ToString("x8"));
                char[] output = new char[12 + body.Length];
                for (int i = 0; i < 12; i++)
                {
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                for (int i = 12; i < output.Length; i++)
                {
                    output[i] = body[i - 12];
                }
                retLength = output.Length;
                return output;
            }
            #endregion
            #region if (MsgType == 6)//���߷��������µĶ���
            if (MsgType == 6)
            {
                //body = "";
                //ret = converse(MsgType.ToString("x4") + MsgLength.ToString("x4") + SegNo.ToString("x8") + Extend.ToString("x8"));
                //char[] output = new char[12 + body.Length];
                //for (int i = 0; i < 12; i++)
                //{
                //    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                //}
                //for (int i = 12; i < output.Length; i++)
                //{
                //    output[i] = body[i - 12];
                //}
                //retLength = output.Length;
                //return output;



                //IPCount = (UInt16)IPAddress.Length;
                ushort psptCount = (UInt16)(MsgBody.Length / 32);
                //MsgLength = (UInt16)(12 + 2 + IPCount * 20);
                char[] output = new char[12 + 2 + psptCount * 32];
                for (int i = 0; i < output.Length; i++)
                {
                    output[i] = '\0';
                }
                //Э��ͷ12�ֽڸ���output��ǰ12���ֽ�
                for (int i = 0; i < 12; i++)
                {
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                ret = converse(psptCount.ToString("x4"));
                for (int i = 12; i < 14; i++)
                {
                    output[i] = (char)int.Parse(ret.Substring((i - 12) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                //for (int i = 0; i < (int)(psptCount); i++)
                {
                    for (int j = 0; j < MsgBody.Length; j++)
                    {
                        output[14 + j] = MsgBody[j];
                    }
                }
                retLength = MsgLength;
                return output;
            }
            #endregion
            if(MsgType == 8)
            {
                ret = converse(MsgType.ToString("x4") + MsgLength.ToString("x4") + SegNo.ToString("x8") + Extend.ToString("x8") +
                    int.Parse(MsgBody).ToString("x8"));
                char[] output = new char[12 + 4];
                for (int i = 0; i < 16; i++)
                {
                    //Э��ͷ����output��ǰ12���ֽ�
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                retLength = output.Length;
                return output;
            }
            retLength = 0;
            return null;


        }

        private string converse(string input)//��������
        {
            if (input.Length % 4 != 0) return null;
            string temp = "";
            for (int i = 0; i < input.Length / 4; i++)
            {
                temp += input[4 * i + 2];
                temp += input[4 * i + 3];
                temp += input[4 * i];
                temp += input[4 * i + 1];
            }
            return temp;

        }
    }
}
