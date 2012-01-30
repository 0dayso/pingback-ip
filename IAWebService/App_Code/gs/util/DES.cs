using System;
using System.Security.Cryptography;

namespace gs.util
{
	/// <summary>
	/// DES ��ժҪ˵����
	/// </summary>
	public class DES
	{
		private SymmetricAlgorithm mCSP;

		public DES()
		{
			mCSP = new TripleDESCryptoServiceProvider();
		}

		/// <summary>
		/// �õ���Կ
		/// </summary>
		/// <returns></returns>
		public string getKey()
		{
			mCSP.GenerateKey();
			return Convert.ToBase64String(mCSP.Key).Trim();
		}

		/// <summary>
		/// �õ�������
		/// </summary>
		/// <returns></returns>
		public string getIV()
		{
			mCSP.GenerateIV();
			return Convert.ToBase64String(mCSP.IV).Trim();
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="p_strKey">��Կ</param>
		/// <param name="p_strIV">������</param>
		/// <returns></returns>
		public string EncryptString(string Value,string p_strKey,string p_strIV)
		{
			ICryptoTransform ct;
			System.IO.MemoryStream ms;
			CryptoStream cs;
			byte[] byt;

			//ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
			ct = mCSP.CreateEncryptor(Convert.FromBase64String(p_strKey.Trim()),Convert.FromBase64String(p_strIV.Trim()) );

			byt = System.Text.Encoding.UTF8.GetBytes(Value);

			ms = new System.IO.MemoryStream();
			cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
			cs.Write(byt, 0, byt.Length);
			cs.FlushFinalBlock();

			cs.Close();

			return Convert.ToBase64String(ms.ToArray());
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="p_strKey"></param>
		/// <param name="p_strIV"></param>
		/// <returns></returns>
		public string DecryptString(string Value,string p_strKey,string p_strIV)
		{
			ICryptoTransform ct;
			System.IO.MemoryStream ms;
			CryptoStream cs;
			byte[] byt;

			//ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
			ct = mCSP.CreateDecryptor(Convert.FromBase64String(p_strKey.Trim()),Convert.FromBase64String(p_strIV.Trim()) );

			byt = Convert.FromBase64String(Value);

			ms = new System.IO.MemoryStream();
			cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
			cs.Write(byt, 0, byt.Length);
			cs.FlushFinalBlock();

			cs.Close();

			return System.Text.Encoding.UTF8.GetString(ms.ToArray());
		}

		public static string getMd5(string p_strVal)
		{
			return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(p_strVal,"MD5");
		}
	}
}
