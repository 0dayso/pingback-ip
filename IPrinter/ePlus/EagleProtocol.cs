using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ePlus
{
    //增加协议步骤：1.修改SetMsgLenth；2.修改ConvertToString
    //本程序中未用到此类
    class ProtocalFunc
    {
        /// <summary>
        /// 指定配置
        /// </summary>
        /// <param name="ipstrings">以~分割的多个IP串</param>
        /// <returns></returns>
        public static char[] SpecifyCFG(string ipstrings)
        {//ipstrings为以~相隔的多个IP串
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
        /// 指定Passport
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
        /// 保持连接
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
        /// 发送普通订票指令
        /// </summary>
        /// <param name="cc">指令串字符</param>
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
        /// 发送三合一指令
        /// </summary>
        /// <param name="pc">三合一的指令串</param>
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
        static public bool b_passport = false;//true的时候，发送保持连接信号
        static public int segstep = 0;

        public UInt16 MsgType;//命令或响应类型
        public UInt16 MsgLength;//消息总长度(含消息头及消息体)
        public UInt32 SegNo;//消息流水号,顺序累加,步长为1,循环使用（一对请求和应答消息的流水号必须相同）
        public UInt32 Extend;//保留，扩展用
        //头共12字节

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
                    MsgLength = (UInt16)(12+2 + MsgBody.Length / 2);//三合一中有不可见字符，处理串后的长度除2即可
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
            //协议头转换成16进制，并22倒置
            ret = converse(MsgType.ToString("x4") + MsgLength.ToString("x4") + SegNo.ToString("x8") + Extend.ToString("x8"));


            string body="";
            //消息类型为1,passport认证
            #region passport if (MsgType == 1)
            if (MsgType == 1)
            {
                body = MsgBody;
                char[] output = new char[12 + 32];
                for (int i = 0; i < 12; i++)
                {
                    //协议头赋给output的前12个字节
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                //passport赋给output的后32个字节
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
            //消息类型为5,passport认证，无条件认证
            #region passport if (MsgType == 5)
            if (MsgType == 5)
            {
                body = MsgBody;
                char[] output = new char[12 + 32];
                for (int i = 0; i < 12; i++)
                {
                    //协议头赋给output的前12个字节
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                //passport赋给output的后32个字节
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
                //发送之前将IPAddress数组赋值
                if (IPAddress.Length <= 0) ;//没有赋IPAddress值
                else
                {
                    IPCount = (UInt16)IPAddress.Length;
                    //MsgLength = (UInt16)(12 + 2 + IPCount * 20);
                    char[] output = new char[12 + 2 + IPCount * 20];
                    for(int i = 0;i<output.Length;i++)
                    {
                        output[i] = '\0';
                    }
                    //协议头12字节赋给output的前12个字节
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
            #region if (MsgType == 3 || MsgType == 7)//cmd及协议7，客户端后台工作
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
                    //改变计算字节总长
                    retLength = output.Length - MsgBody.Length + bodylength;
                    output[output.Length - 1] = '\0';
                    ret = converse(retLength.ToString("x4"));
                    for (int i = 0; i < 2; i++)
                    {
                        output[i + 2] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    //返回长度为字符数总长
                    retLength = output.Length;
                    return output;
                }
                if (cmdType == 2)
                {
                    //MsgBody为动态库出来后的16进制串
                    //原char[] output = new char[12 + 2 + MsgBody.Length/2];
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
                        //原output[i] = (char)(int.Parse(MsgBody[2 * j].ToString(),System.Globalization.NumberStyles.HexNumber)*16
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
            #region if (MsgType == 6)//告诉服务器有新的订单
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
                //协议头12字节赋给output的前12个字节
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
                    //协议头赋给output的前12个字节
                    output[i] = (char)int.Parse(ret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
                retLength = output.Length;
                return output;
            }
            retLength = 0;
            return null;


        }

        private string converse(string input)//两两倒置
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
