using System;
using System.Xml;

namespace EagleWebService
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
        public XmlNode AddPara(string p_strParaName, string p_strParaVal)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode nodeNew = doc.CreateNode(XmlNodeType.Element, p_strParaName, "");
            nodeNew.AppendChild(doc.CreateTextNode(p_strParaVal));
            root.AppendChild(nodeNew);
            return nodeNew;
        }
        /// <summary>
        /// 在指定的path后增加结点
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
            catch (Exception ex)
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
    /// C#XML文件操作类 包含常用XML操作信息
    /// by CrazyCoder.Cn
    /// 转载请注明出处
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
        /// 加载Xml文档
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

        #region 取得名称为name的结点的值
        /// <summary>
        /// 取得名称为name的结点的值
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

        #region 创建一个包含version和指定根节点的XmlDocument
        /// <summary>
        /// 创建一个包含version和指定根节点的XmlDocument
        /// </summary>
        /// <param name="rootName"></param>
        public void CreateRoot(string rootName)
        {
            XmlElement xe = xdoc.CreateElement(rootName);
            xdoc.AppendChild(xe);
            root = xe;
        }
        #endregion

        #region 增加一个子结点
        /// <summary>
        /// 在根结点上增加一个子结点
        /// </summary>
        /// <param name="name">名</param>
        /// <param name="_value">值</param>
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

        #region 为一个XmlElement添加子节点，并返回添加的子节点引用
        /// <summary>
        /// 为一个指定的XmlElement添加子节点，并返回添加的子节点引用
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

        #region 为一个XmlElement添加子节点，并返回添加的子节点引用
        /// <summary>
        /// 为一个XmlElement添加子节点，并返回添加的子节点引用
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

        #region 为一个节点添加属性
        /// <summary>
        /// 为一个节点添加属性
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public void AddAttribute(XmlElement xe, string strName, string strValue)
        {
            //判断属性是否存在
            string s = GetXaValue(xe.Attributes, strName);
            //属性已经存在
            if (s != null)
            {
                throw new System.Exception("attribute exists");
            }
            XmlAttribute xa = xdoc.CreateAttribute(strName);
            xa.Value = strValue;
            xe.Attributes.Append(xa);
        }
        #endregion

        #region 为一个节点添加属性，不是系统表
        /// <summary>
        /// 为一个节点添加属性，不是系统表
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="xe"></param>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        protected void AddAttribute(XmlDocument xdoc, XmlElement xe, string strName, string strValue)
        {
            //判断属性是否存在
            string s = GetXaValue(xe.Attributes, strName);
            //属性已经存在
            if (s != null)
            {
                throw new Exception("Error:The attribute '" + strName + "' has been existed!");
            }
            XmlAttribute xa = xdoc.CreateAttribute(strName);
            xa.Value = strValue;
            xe.Attributes.Append(xa);
        }
        #endregion

        #region 通过节点名称找到指定的节点
        /// <summary>
        /// 通过节点名称找到指定的节点
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

        #region 找到指定名称属性的值
        /// <summary>
        /// 找到指定名称属性的值
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

        #region 找到指定名称属性的值
        /// <summary>
        /// 找到指定名称属性的值
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

        #region 为一个节点指定值
        /// <summary>
        /// 为一个节点指定值
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

        #region 为一个属性指定值
        /// <summary>
        /// 为一个属性指定值
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

        #region 寻找具有指定名称和属性/值组合的节点
        /// <summary>
        /// 寻找具有指定名称和属性/值组合的节点
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
