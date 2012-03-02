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
        /// </summary>
        [XmlAttribute]
        public string AppName { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        [XmlAttribute]
        public string QueueName { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [XmlAttribute]
        public string ConnectionString { get; set; }
    }

    public class MessageConfigList : List<MessageConfig>
    {
    }
}
