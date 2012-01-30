using System;
using System.Xml;

namespace gs.data
{
	/// <summary>
	/// XmlPara ��ժҪ˵����
	/// </summary>
	public class XmlPara
	{
		private XmlDocument doc = null;
 
		public XmlPara()
		{
			doc = new XmlDocument();
			string strXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
			strXML += "<NewDataSet xmlns:eid=\"urn:samples\">";
			strXML += "</NewDataSet>";
			doc.LoadXml(strXML); 
		}

		public XmlPara(string p_strXML)
		{
			doc = new XmlDocument();
			doc.LoadXml(p_strXML); 
		}

		public void AddPara(string p_strParaName,string p_strVal)
		{
			/*string[] ary = new string[2];
			ary[0] = p_strParaName;
			ary[1] = p_strVal;
			m_ary.Add(ary); */

			XmlNode root = doc.DocumentElement;
			XmlNode rec = root.ChildNodes[0];
			
			XmlAttribute atr = doc.CreateAttribute("eid");	//����REC������
			atr.Value = p_strParaName;
			XmlNode nodeNew = doc.CreateNode(XmlNodeType.Element,"rec","");
			nodeNew.Attributes.Append(atr);		//���������ӵ�REC��
  
			XmlNode subNodeNew = doc.CreateNode(XmlNodeType.Element,"paravalue","");
			subNodeNew.AppendChild(doc.CreateTextNode(p_strVal));	//�ѱ���ֵ����ȥ
			nodeNew.AppendChild(subNodeNew);	//���½ڵ����ӵ�REC��
			root.AppendChild(nodeNew);			//����һ���µ�REC

		}

		public string GetXML()
		{
			return doc.OuterXml;
		}
	}
}
