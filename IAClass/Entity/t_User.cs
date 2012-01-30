using System;
namespace IAClass.Entity
{
    /// <summary>
    /// t_User:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class t_User
    {
        public t_User()
        { }
        #region Model
        private int _userid;
        private string _username;
        private string _password;
        private int _usertype;
        private string _usergroup = "未指定";
        private string _displayname;
        private string _phone;
        private string _address;
        private string _parent;
        private bool _enabled = true;
        private DateTime _datetime = DateTime.Now;
        private int? _offsetx;
        private int? _offsety;
        private long _logincount = 0;
        private DateTime? _lastlogindate;
        private string _lastloginip;
        private DateTime? _lastactiondate;
        private string _lastactionip;
        private string _lastactionlocation;
        private int _countconsumed = 0;
        private int _countwithdrawed = 0;
        /// <summary>
        /// 
        /// </summary>
        public int userID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int usertype
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userGroup
        {
            set { _usergroup = value; }
            get { return _usergroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string displayname
        {
            set { _displayname = value; }
            get { return _displayname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parent
        {
            set { _parent = value; }
            get { return _parent; }
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
        public DateTime datetime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? offsetX
        {
            set { _offsetx = value; }
            get { return _offsetx; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? offsetY
        {
            set { _offsety = value; }
            get { return _offsety; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long loginCount
        {
            set { _logincount = value; }
            get { return _logincount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastLoginDate
        {
            set { _lastlogindate = value; }
            get { return _lastlogindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastLoginIP
        {
            set { _lastloginip = value; }
            get { return _lastloginip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastActionDate
        {
            set { _lastactiondate = value; }
            get { return _lastactiondate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastActionIP
        {
            set { _lastactionip = value; }
            get { return _lastactionip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastActionLocation
        {
            set { _lastactionlocation = value; }
            get { return _lastactionlocation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CountConsumed
        {
            set { _countconsumed = value; }
            get { return _countconsumed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CountWithdrawed
        {
            set { _countwithdrawed = value; }
            get { return _countwithdrawed; }
        }
        #endregion Model

    }
}

