﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace AutoUpdate
{
    public enum ISP
    {
        Default, ChinaTelecom, ChinaUnicom
    }

    public enum QueryType
    {
        Eterm, WebService, Zizaibao
    }

    /// <summary>
    /// 配置文件的抽象基类，必须被重载
    /// </summary>
    public abstract class XMLConfig
    {
        public XMLConfig Read()
        {
            string fileName = this.GetType().Name + ".xml";
            return Read(fileName);
        }

        public XMLConfig Read(string fileName)
        {
            XMLConfig data = null;
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                data = serializer.Deserialize(fs) as XMLConfig;
                fs.Close();
            }
            catch (Exception e)
            {
                if (fs != null)
                    fs.Close();
                Common.LogWrite(e.ToString());
                //System.Windows.Forms.MessageBox.Show("读取 " + fileName + " 配置文件失败，将使用默认值。", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                data = Activator.CreateInstance(this.GetType()) as XMLConfig;
            }

            return data;
        }

        public void Save()
        {
            string fileName = this.GetType().Name + ".xml";
            Save(fileName);
        }

        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());

            // serialize the object
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, this);
                fs.Close();
            }
        }
    }

    public class XMLConfigUser : XMLConfig
    {
        string iaCode = "1";
        ISP selectedISP;

        /// <summary>
        /// 最后一次打印的保险代码
        /// </summary>
        [XmlElement]
        public string IACode
        {
            set { iaCode = value; }
            get { return iaCode; }
        }

        /// <summary>
        /// 线路运营商类型
        /// </summary>
        [XmlElement]
        public ISP SelectedISP
        {
            set { selectedISP = value; }
            get { return selectedISP; }
        }
    }

    public class XMLSettingsGlobal : XMLConfig
    {
        string remotingUrl = "tcp://localhost/test";
        string iaWebServiceURL = "http://localhost/Webservice.asmx";
        string iaWebServiceURL1 = "";
        string iaWebServiceURL2 = "";
        string loginWebService = "http://localhost/egws.asmx";
        string loginWebService1 = "";
        string loginWebService2 = "";
        string website = "http://localhost/";
        string website1 = "";
        string website2 = "";
        string loginCredential = "renwox,renwox";
        string iaCode = "1";
        ISP selectedISP;
        QueryType queryType;

        /// <summary>
        /// 最后一次打印的保险代码
        /// </summary>
        [XmlElement]
        public string IACode
        {
            set { iaCode = value; }
            get { return iaCode; }
        }

        /// <summary>
        /// 编码提取方式
        /// </summary>
        [XmlElement]
        public QueryType QueryType
        {
            set { queryType = value; }
            get { return queryType; }
        }

        /// <summary>
        /// 线路运营商类型
        /// </summary>
        [XmlElement]
        public ISP SelectedISP
        {
            set { selectedISP = value; }
            get { return selectedISP; }
        }
        /// <summary>
        /// 登录凭证
        /// </summary>
        [XmlElement]
        public string LoginCredential
        {
            set { loginCredential = value; }
            get { return loginCredential; }
        }

        /// <summary>
        /// 登录接口地址
        /// </summary>
        [XmlElement]
        public string LoginWebService
        {
            set { loginWebService = value; }
            get { return loginWebService; }
        }

        /// <summary>
        /// 登录接口地址1
        /// </summary>
        [XmlElement]
        public string LoginWebService1
        {
            set { loginWebService1 = value; }
            get { return loginWebService1; }
        }

        /// <summary>
        /// 登录接口地址2
        /// </summary>
        [XmlElement]
        public string LoginWebService2
        {
            set { loginWebService2 = value; }
            get { return loginWebService2; }
        }

        /// <summary>
        /// 网站首页地址
        /// </summary>
        [XmlElement]
        public string Website
        {
            set { website = value; }
            get { return website; }
        }

        /// <summary>
        /// 网站首页地址1
        /// </summary>
        [XmlElement]
        public string Website1
        {
            set { website1 = value; }
            get { return website1; }
        }

        /// <summary>
        /// 网站首页地址2
        /// </summary>
        [XmlElement]
        public string Website2
        {
            set { website2 = value; }
            get { return website2; }
        }

        /// <summary>
        /// remoting 地址
        /// </summary>
        [XmlElement]
        public string RemotingUrl
        {
            set { remotingUrl = value; }
            get { return remotingUrl; }
        }

        /// <summary>
        /// 保险Webservice地址
        /// </summary>
        [XmlElement]
        public string IAWebServiceURL
        {
            set { iaWebServiceURL = value; }
            get { return iaWebServiceURL; }
        }

        /// <summary>
        /// 保险Webservice地址1
        /// </summary>
        [XmlElement]
        public string IAWebServiceURL1
        {
            set { iaWebServiceURL1 = value; }
            get { return iaWebServiceURL1; }
        }

        /// <summary>
        /// 保险Webservice地址2
        /// </summary>
        [XmlElement]
        public string IAWebServiceURL2
        {
            set { iaWebServiceURL2 = value; }
            get { return iaWebServiceURL2; }
        }
    }
}
