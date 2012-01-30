using System;
using System.Collections.Generic;
using System.Text;

namespace IAClass.Entity
{
    /// <summary>
    /// 实体类t_Feedback 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class t_Feedback
    {
        public t_Feedback()
        { }
        #region Model
        private long _id;
        private string _subject;
        private string _fromwho;
        private string _fromlocation;
        private string _fromip;
        private string _remark;
        private int? _timer;
        private DateTime _timespan;
        /// <summary>
        /// 
        /// </summary>
        public long id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fromWho
        {
            set { _fromwho = value; }
            get { return _fromwho; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fromLocation
        {
            set { _fromlocation = value; }
            get { return _fromlocation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fromIP
        {
            set { _fromip = value; }
            get { return _fromip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 计时器，时间单位是毫秒
        /// </summary>
        public int? timer
        {
            set { _timer = value; }
            get { return _timer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime timespan
        {
            set { _timespan = value; }
            get { return _timespan; }
        }
        #endregion Model

    }
}
