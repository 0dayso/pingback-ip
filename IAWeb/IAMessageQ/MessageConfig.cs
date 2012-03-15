using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IAMessageQ
{
    public class MessageConfig
    {
        /// <summary>
        /// 应用的名称
        /// XmlAttribute 标签:指示该字段将序列成为一个XML属性,例如<MessageConfig AppName="测试" />
        /// XmlElement 标签:指示该字段将序列化成一个XML元素,例如<MessageConfig><AppName>测试</AppName></MessageConfig>
        /// </summary>
        [XmlAttribute]
        public string AppName { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        [XmlAttribute]
        public string QueueName { get; set; }
    }

    public class MessageConfigList : List<MessageConfig>
    {
    }
}
