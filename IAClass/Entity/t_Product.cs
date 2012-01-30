using System;
namespace IAClass.Entity
{
    /// <summary>
    /// t_Product:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class t_Product
    {
        public t_Product()
        { }
        #region Model
        private int _productid;
        private string _productname;
        private string _productsupplier;
        private int _productduration;
        private string _productcomment;
        private string _printingconfig;
        private string _filterinclude;
        private string _filterexclude;
        private string _filtercomment;
        private DateTime? _timespan = DateTime.Now;
        private bool _enabled = true;
        private bool _isissuingrequired = false;
        private string _ioc_typename;
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
        public string productName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string productSupplier
        {
            set { _productsupplier = value; }
            get { return _productsupplier; }
        }
        /// <summary>
        /// 保险期限
        /// </summary>
        public int productDuration
        {
            set { _productduration = value; }
            get { return _productduration; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string productComment
        {
            set { _productcomment = value; }
            get { return _productcomment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrintingConfig
        {
            set { _printingconfig = value; }
            get { return _printingconfig; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FilterInclude
        {
            set { _filterinclude = value; }
            get { return _filterinclude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FilterExclude
        {
            set { _filterexclude = value; }
            get { return _filterexclude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FilterComment
        {
            set { _filtercomment = value; }
            get { return _filtercomment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Timespan
        {
            set { _timespan = value; }
            get { return _timespan; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsIssuingRequired
        {
            set { _isissuingrequired = value; }
            get { return _isissuingrequired; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IOC_TypeName
        {
            set { _ioc_typename = value; }
            get { return _ioc_typename; }
        }
        #endregion Model

    }
}

