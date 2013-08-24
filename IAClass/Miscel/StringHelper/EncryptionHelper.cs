using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace StringHelper
{
    public static class EncryptionHelper
    {
        //密码算法|密钥|初始向量(自个随便写)
        static RijndaelManaged _cryptoProvider;
        static readonly byte[] key = { 12, 34, 46, 39, 43, 56, 34, 13, 43, 56, 78, 30, 56, 37, 15, 99 };
        static readonly byte[] IV = { 34, 53, 63, 63, 78, 99, 45, 2, 69, 34, 68, 62, 4, 126, 78, 90 };

        //constructor  -- INIT cyptoProvider
        static EncryptionHelper()
        {
            _cryptoProvider = new RijndaelManaged();
            _cryptoProvider.Mode = CipherMode.CBC;
            _cryptoProvider.Padding = PaddingMode.PKCS7;
        }

        /// <summary>
        /// 使用Rijndael算法加密字符串
        /// </summary>
        /// <param name="unencrpytedString"></param>
        /// <returns></returns>
        public static string Encrypt(string unencrpytedString)
        {
            //byte[] bytIn = ASCIIEncoding.ASCII.GetBytes(unencrpytedString);这个是针对英文滴。
            byte[] bytIn = Encoding.UTF8.GetBytes(unencrpytedString);//用UTF8才是中文加解密。
            MemoryStream ms = new MemoryStream();//存放加密后byte[]滴stream
            //用于实现加解密滴类CS
            CryptoStream cs = new CryptoStream(ms, _cryptoProvider.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            //CLOSE STREAM
            cs.Clear();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// 使用Rijndael算法解密字符串
        /// </summary>
        /// <param name="encrptytedString"></param>
        /// <returns></returns>
        public static string Decrpyt(string encrptytedString)
        {
            byte[] bytIn = Convert.FromBase64String(encrptytedString);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            CryptoStream cs = new CryptoStream(ms, _cryptoProvider.CreateDecryptor(key, IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="password">原始字符串</param>
        /// <returns>返回字符串数组 { hash, salt }</returns>
        public static string[] EncryptWithSalt(string password)
        {
            // random salt 
            string salt = Guid.NewGuid().ToString();

            // random salt 
            // you can also use RNGCryptoServiceProvider class            
            //System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider(); 
            //byte[] saltBytes = new byte[36]; 
            //rng.GetBytes(saltBytes); 
            //string salt = Convert.ToBase64String(saltBytes); 

            string hashString = EncryptWithSalt(password, salt);

            return new string[] { hashString, salt };
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="password">原始字符串</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        public static string EncryptWithSalt(string password, string salt)
        {
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);
            return hashString;
        }
    }

}
