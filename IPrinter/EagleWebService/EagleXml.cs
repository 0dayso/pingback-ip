using System;
using System.Xml;

namespace EagleWebService
{
    /// <summary>
    /// NewPara ��ժҪ˵����
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
        /// ����һ���µ�����
        /// </summary>
        /// <param name="p_strCmName"></param>
        /// <returns></returns>
        public XmlNode AddPara(string p_strParaName, string p_strParaVal)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode nodeNew = doc.CreateNode(XmlNodeType.Element, p_strParaName, "");
            nodeNew.AppendChild(doc.CreateTextNode(p_strParaVal));
            root.AppendChild(nodeNew);
            return nodeNew;
        }
        /// <summary>
        /// ��ָ����path�����ӽ��
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public XmlNode AddPara(string path, string name, string val)
        {
            XmlNode root = doc.DocumentElement;
            root = root.SelectSingleNode(path);
            XmlNode nodeNew = doc.CreateNode(XmlNodeType.Element, name, "");
            nodeNew.AppendChild(doc.CreateTextNode(val));
            root.AppendChild(nodeNew);
            return nodeNew;
        }
        /// <summary>
        /// ��·���ҽӵ�,���û�ҵ��򷵻�NULL
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
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return nodeRet;
        }

        /// <summary>
        /// ��·��������,���û�ҵ��򷵻�""
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
                if (root.SelectSingleNode(p_strPath).ChildNodes.Count > 0)
                {
                    if (root.SelectSingleNode(p_strPath).ChildNodes[0] != null)
                    {
                        nodeRet = root.SelectSingleNode(p_strPath).ChildNodes[0];
                        strRet = nodeRet.Value;
                        if (strRet == null) return nodeRet.OuterXml;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return strRet.Trim();
        }

        public XmlDocument getRoot()
        {
            return doc;
        }

    }

    /// <summary>
    /// C#XML�ļ������� ��������XML������Ϣ
    /// by CrazyCoder.Cn
    /// ת����ע������
    /// </summary>
    public class XmlHandler
    {
        protected XmlDocument xdoc = new XmlDocument();
        public XmlElement root;
        public XmlHandler()
        {

        }

        #region LoadXml
        /// <summary>
        /// ����Xml�ĵ�
        /// </summary>
        /// <param name="xml"></param>
        public void LoadXml(string xml)
        {
            xdoc.LoadXml(xml);
            root = (XmlElement)xdoc.LastChild;
        }
        #endregion

        public void SaveXml(string file)
        {
            xdoc.Save(file);
        }

        #region ȡ������Ϊname�Ľ���ֵ
        /// <summary>
        /// ȡ������Ϊname�Ľ���ֵ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name)
        {
            XmlNode xn = FindXnByName(root.ChildNodes, name);
            if (xn == null) return null;
            return xn.InnerText;
        }
        #endregion

        #region ����һ������version��ָ�����ڵ��XmlDocument
        /// <summary>
        /// ����һ������version��ָ�����ڵ��XmlDocument
        /// </summary>
        /// <param name="rootName"></param>
        public void CreateRoot(string rootName)
        {
            XmlElement xe = xdoc.CreateElement(rootName);
            xdoc.AppendChild(xe);
            root = xe;
        }
        #endregion

        #region ����һ���ӽ��
        /// <summary>
        /// �ڸ����������һ���ӽ��
        /// </summary>
        /// <param name="name">��</param>
        /// <param name="_value">ֵ</param>
        /// <returns></returns>
        public XmlElement AppendChild(string name, string _value)
        {
            return AddChild((XmlElement)root, name, _value);
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return xdoc.OuterXml;
        }
        #endregion

        #region Ϊһ��XmlElement����ӽڵ㣬��������ӵ��ӽڵ�����
        /// <summary>
        /// Ϊһ��ָ����XmlElement����ӽڵ㣬��������ӵ��ӽڵ�����
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="sField"></param>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public XmlElement AddChild(XmlElement xe, string sField, string sValue)
        {
            XmlElement xeTemp = xdoc.CreateElement(sField);
            xeTemp.InnerText = sValue;
            xe.AppendChild(xeTemp);
            return xeTemp;
        }
        #endregion

        #region Ϊһ��XmlElement����ӽڵ㣬��������ӵ��ӽڵ�����
        /// <summary>
        /// Ϊһ��XmlElement����ӽڵ㣬��������ӵ��ӽڵ�����
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="xd"></param>
        /// <param name="sField"></param>
        /// <returns></returns>
        protected XmlElement AddChild(XmlElement xe, XmlDocument xd, string sField)
        {
            XmlElement xeTemp = xd.CreateElement(sField);
            xe.AppendChild(xeTemp);
            return xeTemp;
        }
        #endregion

        #region Ϊһ���ڵ��������
        /// <summary>
        /// Ϊһ���ڵ��������
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public void AddAttribute(XmlElement xe, string strName, string strValue)
        {
            //�ж������Ƿ����
            string s = GetXaValue(xe.Attributes, strName);
            //�����Ѿ�����
            if (s != null)
            {
                throw new System.Exception("attribute exists");
            }
            XmlAttribute xa = xdoc.CreateAttribute(strName);
            xa.Value = strValue;
            xe.Attributes.Append(xa);
        }
        #endregion

        #region Ϊһ���ڵ�������ԣ�����ϵͳ��
        /// <summary>
        /// Ϊһ���ڵ�������ԣ�����ϵͳ��
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="xe"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        protected void AddAttribute(XmlDocument xdoc, XmlElement xe, string strName, string strValue)
        {
            //�ж������Ƿ����
            string s = GetXaValue(xe.Attributes, strName);
            //�����Ѿ�����
            if (s != null)
            {
                throw new Exception("Error:The attribute '" + strName + "' has been existed!");
            }
            XmlAttribute xa = xdoc.CreateAttribute(strName);
            xa.Value = strValue;
            xe.Attributes.Append(xa);
        }
        #endregion

        #region ͨ���ڵ������ҵ�ָ���Ľڵ�
        /// <summary>
        /// ͨ���ڵ������ҵ�ָ���Ľڵ�
        /// </summary>
        /// <param name="xnl"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public XmlNode FindXnByName(XmlNodeList xnl, string strName)
        {
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl.Item(i).LocalName == strName) return xnl.Item(i);
            }
            return null;
        }
        #endregion

        public XmlNode FindXnByNameAndValue(XmlNodeList xnl, string name, string value)
        {
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl[i].Name == name && xnl[i].InnerText.Trim() == value) return xnl[i];
            }
            return null;
        }

        #region �ҵ�ָ���������Ե�ֵ
        /// <summary>
        /// �ҵ�ָ���������Ե�ֵ
        /// </summary>
        /// <param name="xac"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        protected string GetXaValue(XmlAttributeCollection xac, string strName)
        {
            for (int i = 0; i < xac.Count; i++)
            {
                if (xac.Item(i).LocalName == strName) return xac.Item(i).Value;
            }
            return null;
        }
        #endregion

        #region �ҵ�ָ���������Ե�ֵ
        /// <summary>
        /// �ҵ�ָ���������Ե�ֵ
        /// </summary>
        /// <param name="xnl"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        protected string GetXnValue(XmlNodeList xnl, string strName)
        {
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl.Item(i).LocalName == strName) return xnl.Item(i).InnerText;
            }
            return null;
        }
        #endregion

        #region Ϊһ���ڵ�ָ��ֵ
        /// <summary>
        /// Ϊһ���ڵ�ָ��ֵ
        /// </summary>
        /// <param name="xnl"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        protected void SetXnValue(XmlNodeList xnl, string strName, string strValue)
        {
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl.Item(i).LocalName == strName)
                {
                    xnl.Item(i).InnerText = strValue;
                    return;
                }
            }
            return;
        }
        #endregion

        #region Ϊһ������ָ��ֵ
        /// <summary>
        /// Ϊһ������ָ��ֵ
        /// </summary>
        /// <param name="xac"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        protected void SetXaValue(XmlAttributeCollection xac, string strName, string strValue)
        {
            for (int i = 0; i < xac.Count; i++)
            {
                if (xac.Item(i).LocalName == strName)
                {
                    xac.Item(i).Value = strValue;
                    return;
                }
            }
            return;
        }
        #endregion

        #region Ѱ�Ҿ���ָ�����ƺ�����/ֵ��ϵĽڵ�
        /// <summary>
        /// Ѱ�Ҿ���ָ�����ƺ�����/ֵ��ϵĽڵ�
        /// </summary>
        /// <param name="xnl"></param>
        /// <param name="strXaName"></param>
        /// <param name="strXaValue"></param>
        /// <returns></returns>
        public XmlNode FindXnByXa(XmlNodeList xnl, string strXaName, string strXaValue)
        {
            string xa;
            for (int i = 0; i < xnl.Count; i++)
            {
                xa = GetXaValue(xnl.Item(i).Attributes, strXaName);
                if (xa != null)
                {
                    if (xa == strXaValue) return xnl.Item(i);
                }
            }
            return null;
        }
        #endregion
    }


}
