using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAClass.Entity
{
    /// <summary>
    /// 短信队列实体类
    /// </summary>
    [Serializable]
    public class SMSEntity : MessageEntity
    {
        public string MobilePhone;
        public string Content;
        public string CaseNo;
    }
}
