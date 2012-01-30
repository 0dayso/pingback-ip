using System;
using System.Xml;
using System.Collections; 

namespace gs.data
{
	/// <summary>
	/// XMLRead 的摘要说明。
	/// </summary>
	public class XMLRead
	{
		private XmlDocument doc = null;
		public XMLRead(string p_strXML)
		{
			doc = new XmlDocument();
			doc.LoadXml(p_strXML);
		}

		/// <summary>
		/// 读出XML中对应的参数
		/// </summary>
		/// <param name="p_strParaName"></param>
		/// <returns></returns>
		public string GetPara(string p_strParaName)
		{
			string strRet = "";
			try
			{
				XmlNode root = doc.DocumentElement;
				XmlNode rec = root.ChildNodes[0];
				
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
				nsmgr.AddNamespace("eid", "urn:samples");
				strRet = root.SelectSingleNode("descendant::rec[@eid=\"" + p_strParaName + "\"]",nsmgr).ChildNodes[0].ChildNodes[0].Value  ;
			}
			catch(Exception ex)
			{
				//strRet = "gs.data.XMLRead.GetPara读数据异常" + e.Message;
				//throw new Exception("gs.data.XMLRead.GetPara读数据异常;" + e.Message); 
				strRet = ex.Message;
				strRet = " ";
			}
			return strRet;
		}

		/// <summary>
		/// 得到参数集合
		/// </summary>
		/// <returns></returns>
		public ArrayList getParas()
		{
			ArrayList ary = new ArrayList();
			XmlNode root = doc.DocumentElement;
			for(int i=0;i<root.ChildNodes.Count;i++)
			{
				XmlNode rec = root.ChildNodes[i];
				string[] strAry = new string[2];
				strAry[0] = rec.Attributes["eid"].Value;
				strAry[1] = rec.ChildNodes[0].ChildNodes[0].Value;
				ary.Add(strAry);
			}
			return ary;
		}
	}
}
