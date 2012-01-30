using System;
using System.Xml;

namespace EagleCTI
{
	/// <summary>
	/// NewPara 的摘要说明。
	/// </summary>
	public class NewPara
	{
		private XmlDocument doc = null;

		public NewPara()
		{
			doc = new XmlDocument();
			string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
			strXML += "<eg>";
			strXML += "</eg>";
			doc.LoadXml(strXML); 
		}
        public NewPara(string rootName, bool NotEg)
        {
            if (NotEg)
            {
                doc = new XmlDocument();
                string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                strXML += "<" + rootName + ">";
                strXML += "</" + rootName + ">";
                doc.LoadXml(strXML);
            }
            else
            {
                doc = new XmlDocument();
                string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                strXML += "<eg>";
                strXML += "</eg>";
                doc.LoadXml(strXML); 

            }
        }
		public NewPara(string p_strXML)
		{
			doc = new XmlDocument();
			doc.LoadXml(p_strXML); 
		}

		public string GetXML()
		{
			return doc.OuterXml;
		}

		/// <summary>
		/// 加入一个新的命令
		/// </summary>
		/// <param name="p_strCmName"></param>
		/// <returns></returns>
		public XmlNode AddPara(string p_strParaName,string p_strParaVal)
		{
			XmlNode root = doc.DocumentElement;
			XmlNode nodeNew = doc.CreateNode(XmlNodeType.Element,p_strParaName,"");
			nodeNew.AppendChild(doc.CreateTextNode(p_strParaVal));
  			root.AppendChild(nodeNew);
			return nodeNew;
		}

		/// <summary>
		/// 按路经找接点,如果没找到则返回NULL
		/// </summary>
		/// <param name="p_strPath"></param>
		/// <returns></returns>
		public XmlNode FindNodeByPath(string p_strPath)
		{
			XmlNode nodeRet = null;
			try
			{
				XmlNode root = doc.DocumentElement;
				nodeRet = root.SelectSingleNode(p_strPath);
			}
			catch(Exception ex)
			{
				System.Console.Write(ex.Message);
			}
			return nodeRet;
		}

		/// <summary>
		/// 按路经找内容,如果没找到则返回""
		/// </summary>
		/// <param name="p_strPath"></param>
		/// <returns></returns>
		public string FindTextByPath(string p_strPath)
		{
			string strRet = "";
			XmlNode nodeRet = null;
			try
			{
				XmlNode root = doc.DocumentElement;
				if(root.SelectSingleNode(p_strPath).ChildNodes.Count > 0)
				{
					if(root.SelectSingleNode(p_strPath).ChildNodes[0] != null)
					{
						nodeRet = root.SelectSingleNode(p_strPath).ChildNodes[0];
						strRet = nodeRet.Value;
					}
				}
			}
			catch(Exception ex)
			{
				System.Console.Write(ex.Message);
			}
			return strRet;
		}
		
		public XmlDocument getRoot()
		{
			return doc;
		}

	}
}
