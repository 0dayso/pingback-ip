﻿using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ip2Location
{
    //辅助类，用于保存IP索引信息   
    public class CZ_INDEX_INFO
    {
        public UInt32 IpSet;
        public UInt32 IpEnd;
        public UInt32 Offset;

        public CZ_INDEX_INFO()
        {
            IpSet = 0;
            IpEnd = 0;
            Offset = 0;
        }
    }

    /// <summary>
    /// 用法
    /// Ip2Location.Ip2Location ip2lo = new Ip2Location.Ip2Location();
    /// location = ip2lo.GetLocation(ip);
    /// </summary>
    public class Ip2Location
    {
        protected static bool IsIpDatabaseInitialized = false;
        //protected FileStream FileStrm;
        protected static MemoryStream MemStream;
        protected static UInt32 Index_Set;
        protected static UInt32 Index_End;
        protected static UInt32 Index_Count;
        protected UInt32 Search_Index_Set;
        protected UInt32 Search_Index_End;
        protected CZ_INDEX_INFO Search_Set;
        protected CZ_INDEX_INFO Search_Mid;
        protected CZ_INDEX_INFO Search_End;

        static Ip2Location()
        {
            SetDbFilePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QQWry.Dat"));
        }

        //使用二分法查找索引区，初始化查找区间
        private void Initialize()
        {
            Search_Index_Set = 0;
            Search_Index_End = Index_Count - 1;
        }

        //关闭文件
        public void Dispose()
        {
            IsIpDatabaseInitialized = false;

            if (MemStream != null)
            {
                MemStream.Close();
                MemStream = null;
            }
        }

        private static void SetDbFilePath(string dbFilePath)
        {
            if (!IsIpDatabaseInitialized)
            {
                FileStream FileStrm = null;

                try
                {
                    FileStrm = new FileStream(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] buffer = new byte[FileStrm.Length];
                    FileStrm.Read(buffer, 0, buffer.Length);
 
                    MemStream = new MemoryStream(buffer, false);
                }
                finally
                {
                    if (FileStrm != null)
                    {
                        FileStrm.Close();
                        FileStrm = null;
                    }

                    IsIpDatabaseInitialized = false;
                }
            }

            //检查文件长度
            if (MemStream.Length < 8)
            {
                MemStream.Close();
                MemStream = null;
                IsIpDatabaseInitialized = false;
                throw new Exception("IP库大小有误，请确认是否使用了正确的 IP 库！");
            }

            //得到第一条索引的绝对偏移和最后一条索引的绝对偏移
            MemStream.Seek(0, SeekOrigin.Begin);
            Index_Set = GetUInt32();
            Index_End = GetUInt32();

            //得到总索引条数
            Index_Count = (Index_End - Index_Set) / 7 + 1;
            IsIpDatabaseInitialized = true;
        }

        public string GetLocation(string IPValue)
        {
            if (!IsIpDatabaseInitialized)
                throw new Exception("IP库尚未初始化！");

            try
            {
                Initialize();

                UInt32 ip = IPToUInt32(IPValue);
                while (true)
                {

                    //首先初始化本轮查找的区间

                    //区间头
                    Search_Set = IndexInfoAtPos(Search_Index_Set);
                    //区间尾
                    Search_End = IndexInfoAtPos(Search_Index_End);

                    //判断IP是否在区间头内
                    if (ip >= Search_Set.IpSet && ip <= Search_Set.IpEnd)
                        return ReadAddressInfoAtOffset(Search_Set.Offset);


                    //判断IP是否在区间尾内
                    if (ip >= Search_End.IpSet && ip <= Search_End.IpEnd)
                        return ReadAddressInfoAtOffset(Search_End.Offset);

                    //计算出区间中点
                    Search_Mid = IndexInfoAtPos((Search_Index_End + Search_Index_Set) / 2);

                    //判断IP是否在中点
                    if (ip >= Search_Mid.IpSet && ip <= Search_Mid.IpEnd)
                        return ReadAddressInfoAtOffset(Search_Mid.Offset);

                    //本轮没有找到，准备下一轮
                    if (ip < Search_Mid.IpSet)
                        //IP比区间中点要小，将区间尾设为现在的中点，将区间缩小1倍。
                        Search_Index_End = (Search_Index_End + Search_Index_Set) / 2;
                    else
                        //IP比区间中点要大，将区间头设为现在的中点，将区间缩小1倍。
                        Search_Index_Set = (Search_Index_End + Search_Index_Set) / 2;
                }
            }
            catch(Exception e)
            {
                Common.LogIt(e.ToString());
                throw;
            }
        }

        private string ReadAddressInfoAtOffset(UInt32 Offset)
        {
            string country = "";
            string area = "";
            UInt32 country_Offset = 0;
            byte Tag = 0;
            //跳过4字节，因这4个字节是该索引的IP区间上限。
            MemStream.Seek(Offset + 4, SeekOrigin.Begin);

            //读取一个字节，得到描述国家信息的“寻址方式”
            Tag = GetTag();

            if (Tag == 0x01)
            {

                //模式0x01，表示接下来的3个字节是表示偏移位置
                MemStream.Seek(GetOffset(), SeekOrigin.Begin);

                //继续检查“寻址方式”
                Tag = GetTag();
                if (Tag == 0x02)
                {
                    //模式0x02，表示接下来的3个字节代表国家信息的偏移位置
                    //先将这个偏移位置保存起来，因为我们要读取它后面的地区信息。
                    country_Offset = GetOffset();
                    //读取地区信息（注：按照Luma的说明，好像没有这么多种可能性，但在测试过程中好像有些情况没有考虑到，
                    //所以写了个ReadArea()来读取。
                    area = ReadArea();
                    //读取国家信息
                    MemStream.Seek(country_Offset, SeekOrigin.Begin);
                    country = ReadString();
                }
                else
                {
                    //这种模式说明接下来就是保存的国家和地区信息了，以'\0'代表结束。
                    MemStream.Seek(-1, SeekOrigin.Current);
                    country = ReadString();
                    area = ReadArea();

                }
            }
            else if (Tag == 0x02)
            {
                //模式0x02，说明国家信息是一个偏移位置
                country_Offset = GetOffset();
                //先读取地区信息
                area = ReadArea();
                //读取国家信息
                MemStream.Seek(country_Offset, SeekOrigin.Begin);
                country = ReadString();
            }
            else
            {
                //这种模式最简单了，直接读取国家和地区就OK了
                MemStream.Seek(-1, SeekOrigin.Current);
                country = ReadString();
                area = ReadArea();

            }
            string Address = country + " " + area;
            return Address;

        }

        private UInt32 GetOffset()
        {
            byte[] TempByte4 = new byte[4];
            TempByte4[0] = (byte)MemStream.ReadByte();
            TempByte4[1] = (byte)MemStream.ReadByte();
            TempByte4[2] = (byte)MemStream.ReadByte();
            TempByte4[3] = 0;
            return BitConverter.ToUInt32(TempByte4, 0);
        }

        protected string ReadArea()
        {
            byte Tag = GetTag();

            if (Tag == 0x01 || Tag == 0x02)
            {
                MemStream.Seek(GetOffset(), SeekOrigin.Begin);
                return ReadString();
            }
            else
            {
                MemStream.Seek(-1, SeekOrigin.Current);
                return ReadString();
            }
        }

        protected string ReadString()
        {
            UInt32 Offset = 0;
            byte[] TempByteArray = new byte[256];
            TempByteArray[Offset] = (byte)MemStream.ReadByte();
            while (TempByteArray[Offset] != 0x00)
            {
                Offset += 1;
                TempByteArray[Offset] = (byte)MemStream.ReadByte();
            }
            return System.Text.Encoding.Default.GetString(TempByteArray).TrimEnd('\0');
        }

        protected byte GetTag()
        {
            return (byte)MemStream.ReadByte();
        }

        protected CZ_INDEX_INFO IndexInfoAtPos(UInt32 Index_Pos)
        {
            CZ_INDEX_INFO Index_Info = new CZ_INDEX_INFO();
            //根据索引编号计算出在文件中在偏移位置
            MemStream.Seek(Index_Set + 7 * Index_Pos, SeekOrigin.Begin);
            Index_Info.IpSet = GetUInt32();
            Index_Info.Offset = GetOffset();
            MemStream.Seek(Index_Info.Offset, SeekOrigin.Begin);
            Index_Info.IpEnd = GetUInt32();

            return Index_Info;
        }

        private UInt32 IPToUInt32(string IpValue)
        {
            string[] IpByte = IpValue.Split('.');
            Int32 nUpperBound = IpByte.GetUpperBound(0);
            if (nUpperBound != 3)
            {
                IpByte = new string[4];
                for (Int32 i = 1; i <= 3 - nUpperBound; i++)
                    IpByte[nUpperBound + i] = "0";
            }

            byte[] TempByte4 = new byte[4];
            for (Int32 i = 0; i <= 3; i++)
            {
                //'如果是.Net 2.0可以支持TryParse。
                //'If Not (Byte.TryParse(IpByte(i), TempByte4(3 - i))) Then
                //'    TempByte4(3 - i) = &H0
                //'End If
                if (IsNumeric(IpByte[i]))
                    TempByte4[3 - i] = (byte)(Convert.ToInt32(IpByte[i]) & 0xff);
            }

            return BitConverter.ToUInt32(TempByte4, 0);
        }

        protected bool IsNumeric(string str)
        {
            if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str, @"^-?\d+$"))
                return true;
            else
                return false;
        }

        protected static UInt32 GetUInt32()
        {
            byte[] TempByte4 = new byte[4];
            MemStream.Read(TempByte4, 0, 4);
            return BitConverter.ToUInt32(TempByte4, 0);
        }
    }
}
