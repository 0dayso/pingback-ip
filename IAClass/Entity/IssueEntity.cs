using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IAClass.Entity
{
    /// <summary>
    /// 投保信息实体类
    /// </summary>
    [Serializable]
    public class IssueEntity : MessageEntity
    {
        public string Name;
        public string ID;
        public IdentityType IDType;
        public Gender Gender;
        public DateTime Birthday;
        public DateTime EffectiveDate;
        public DateTime ExpiryDate;
        public string PhoneNumber;
        public string FlightNo;
        public string HolderName = "刘德华";
        public IdentityType HolderIDType = IdentityType.其他证件;
        public string HolderID = "387846396091";
        public DateTime HolderBirth = DateTime.Parse("1982-4-12");
        public Gender HolderGender;
        public string SMSContent;
        /// <summary>
        /// 可延迟投保（追溯）
        /// </summary>
        public bool IsLazyIssue;
        /// <summary>
        /// 数据库事务
        /// 其中[XmlIgnore]用于XmlSerializer，而[NonSerialized]用于SoapFormatter
        /// </summary>
        [XmlIgnore]
        [NonSerialized]
        public System.Data.Common.DbCommand DbCommand;
        /// <summary>
        /// 当前出单记录的数据库ID
        /// </summary>
        public string CaseId;
        /// <summary>
        /// 本系统电子单号
        /// </summary>
        public string CaseNo;
        /// <summary>
        /// 当前投保所使用的接口编号
        /// </summary>
        public int InterfaceId;
    }

    /// <summary>
    /// 投保结果
    /// </summary>
    [Serializable]
    public struct IssuingResultEntity
    {
        /// <summary>
        /// 保险公司的正式保单号
        /// </summary>
        public string PolicyNo;
        /// <summary>
        /// 第三方平台/中间商/的保单/订单号
        /// </summary>
        public string AgentOrderNo;
        /// <summary>
        /// 承保公司名称
        /// </summary>
        public string Insurer;
        /// <summary>
        /// 保险金额
        /// </summary>
        public string AmountInsured;
        /// <summary>
        /// 承保公司查询网址
        /// </summary>
        public string Website;
        /// <summary>
        /// 查询服务电话
        /// </summary>
        public string CustomerService;
        public TraceEntity Trace;
    }

    [Serializable]
    public class WithdrawEntity : MessageEntity
    {
        /// <summary>
        /// 正式保单号
        /// </summary>
        public string PolicyNo;
        /// <summary>
        /// 本系统电子单号
        /// </summary>
        public string CaseNo;
    }
}
