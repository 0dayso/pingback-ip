using System;

namespace IAClass.Entity
{
    /// <summary>
    /// t_Case:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class t_Case
    {
        public t_Case()
        { }
        #region Model
        private int _caseid;
        private string _caseno;
        private string _caseowner;
        private string _casesupplier;
        private int _productid;
        private int _caseduration = 1;
        private string _customername;
        private string _customerid;
        private string _customerphone;
        private string _customerflightno;
        private DateTime _customerflightdate;
        private string _beneficiary;
        private DateTime _datetime = DateTime.Now;
        private bool _isissued = false;
        private bool _isprinted = false;
        private bool _issmsent = false;
        private bool _isqueryed = false;
        private DateTime? _queryeddatetime;
        private bool _enabled = true;
        private string _ip;
        private string _iplocation;
        private string _certno;
        private string _reserved;
        private string _printingNo;
        string _caseOwnerDisplay;

        public string caseOwnerDisplay
        {
            set { _caseOwnerDisplay = value; }
            get { return _caseOwnerDisplay; }
        }

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
        public string caseOwner
        {
            set { _caseowner = value; }
            get { return _caseowner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string caseSupplier
        {
            set { _casesupplier = value; }
            get { return _casesupplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int productID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int caseDuration
        {
            set { _caseduration = value; }
            get { return _caseduration; }
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
        public string customerPhone
        {
            set { _customerphone = value; }
            get { return _customerphone; }
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
        public string beneficiary
        {
            set { _beneficiary = value; }
            get { return _beneficiary; }
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
        public bool isIssued
        {
            set { _isissued = value; }
            get { return _isissued; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isPrinted
        {
            set { _isprinted = value; }
            get { return _isprinted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isSMSent
        {
            set { _issmsent = value; }
            get { return _issmsent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isQueryed
        {
            set { _isqueryed = value; }
            get { return _isqueryed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? QueryedDatetime
        {
            set { _queryeddatetime = value; }
            get { return _queryeddatetime; }
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
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IpLocation
        {
            set { _iplocation = value; }
            get { return _iplocation; }
        }
        /// <summary>
        /// 保险公司返回的正式保单号
        /// </summary>
        public string CertNo
        {
            set { _certno = value; }
            get { return _certno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string reserved
        {
            set { _reserved = value; }
            get { return _reserved; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrintingNo
        {
            set { _printingNo = value; }
            get { return _printingNo; }
        }
        #endregion Model

    }
}