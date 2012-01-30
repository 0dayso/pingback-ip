using System;
using System.Collections.Generic;
using System.Text;

namespace IAClass.Entity
{
    /// <summary>
    /// 异常追踪实体类
    /// </summary>
    [Serializable]
    public struct TraceEntity
    {
        /// <summary>
        /// 错误信息,若为空,则表示操作成功
        /// </summary>
        public string ErrorMsg;
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Detail;
    }

    public enum UserType
    {
        AdminPlus = 0, Admin = 1, Default = 2, Insurer = 99
    }

    public class UserLoginResponse
    {
        public string Username;
        public int UserId;
        public UserType Type;
        public string DisplayName;
        public int OffsetX;
        public int OffsetY;
        public string ParentPath;
        public decimal Balance;
        public int CountWithdrawed;
        public int CountConsumed;
        public TraceEntity Trace;
    }

    /// <summary>
    /// 保险购买的请求参数类
    /// </summary>
    public class PurchaseRequestEntity
    {
        /// <summary>
        /// 保险代码,用于区别险种
        /// </summary>
        public string InsuranceCode;
        /// <summary>
        /// 用户名
        /// </summary>
        public string username;
        /// <summary>
        /// 密码
        /// </summary>
        public string password;
        /// <summary>
        /// 航班起飞日期时间
        /// </summary>
        public DateTime flightDate;
        /// <summary>
        /// 航班号
        /// </summary>
        public string flightNo;
        /// <summary>
        /// 乘客性别
        /// </summary>
        public Gender customerGender;
        /// <summary>
        /// 乘客出生日期
        /// </summary>
        public DateTime customerBirth = DateTime.Parse("2000-1-1");
        /// <summary>
        /// 乘客证件类型
        /// </summary>
        public IdentityType customerIDType;
        /// <summary>
        /// 乘客证件号码
        /// </summary>
        public string customerID;
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string customerName;
        /// <summary>
        /// 乘客手机号码（可选）
        /// </summary>
        public string customerPhone;
        /// <summary>
        /// PNR编码、票号（可选）
        /// </summary>
        public string PNR;
        /// <summary>
        /// 备用字段（可选）
        /// </summary>
        public string Reserved;
    }

    /// <summary>
    /// 保险购买的返回信息类
    /// </summary>
    public class PurchaseResponseEntity
    {
        /// <summary>
        /// 保险公司正式保单号
        /// </summary>
        public string SerialNo;
        /// <summary>
        /// 电子单号
        /// </summary>
        public string CaseNo;
        /// <summary>
        /// 代理商的名称
        /// </summary>
        public string AgentName;
        /// <summary>
        /// 用于真伪查询的电话号码
        /// </summary>
        public string ValidationPhoneNumber;
        /// <summary>
        /// 异常追踪
        /// </summary>
        public TraceEntity Trace;
    }

    /// <summary>
    /// 产品列表接口返回实体类
    /// </summary>
    public class ProductListResponseEntity
    {
        public List<t_Product> ProductList = new List<t_Product>();
        public TraceEntity Trace;
    }

    /// <summary>
    /// 单证实体类
    /// </summary>
    public class PolicyResponseEntity
    {
        //public List<t_Case> PolicyList = new List<t_Case>();
        public t_Case Policy;
        public TraceEntity Trace;
    }

    /// <summary>
    /// 出单列表接口返回实体类
    /// </summary>
    public class PolicyListResponseEntity
    {
        //public List<t_Case> PolicyList = new List<t_Case>();
        public Policy[] PolicyList;
        public TraceEntity Trace;
    }

    public class LogoutRequestEntity
    {
        public string Username;
        public string Password;
        public int OffsetX;
        public int OffsetY;
    }

    /// <summary>
    /// Policy:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Policy
    {
        public Policy()
        { }
        #region Model
        private int _caseid;
        private string _caseno;
        private string _customername;
        private string _customerid;
        private string _customerflightno;
        private DateTime _customerflightdate;
        private DateTime _datetime = DateTime.Now;
        private bool _enabled = true;
        private string _certno;
        private string _productName;

        /// <summary>
        /// 
        /// </summary>
        public int caseID
        {
            set { _caseid = value; }
            get { return _caseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string caseNo
        {
            set { _caseno = value; }
            get { return _caseno; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string customerName
        {
            set { _customername = value; }
            get { return _customername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string customerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string customerFlightNo
        {
            set { _customerflightno = value; }
            get { return _customerflightno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime customerFlightDate
        {
            set { _customerflightdate = value; }
            get { return _customerflightdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime datetime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }

        /// <summary>
        /// 保险公司返回的正式保单号
        /// </summary>
        public string CertNo
        {
            set { _certno = value; }
            get { return _certno; }
        }

        public string ProductName
        {
            set { _productName = value; }
            get { return _productName; }
        }

        #endregion Model

    }
}
