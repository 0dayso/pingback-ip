using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAMessageQ
{
    public class XMLConfigMQ : IAClass.XMLConfig
    {
        public MessageConfigList MessageConfigList { get; set; }
    }
}
