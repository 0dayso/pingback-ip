//======================================================================
//
//        Copyright (C) 2008-2018
//        All rights reserved 易格网科技
//
//        filename :KBunkXmlEntity
//        description :
//
//        created by Eric at  2008-12-8 13:53:02
//        ZSCHL@163.com
//        http://Ericch.qyun.net
//
//======================================================================
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;

namespace logic.XmlEntity
{
    /// <summary>
    /// 提交K位申请的XML中的Passenger小节
    /// </summary>
    public class XMLPassenger
    {
        /// <summary>
        /// 
        /// </summary>
        public string name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string passport = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string phone = string.Empty;

    }
    ///// <summary>
    ///// passengers类描述
    ///// </summary>
    //public class passengers
    //{

    //    public passenger passengers = null;
    //}
    /// <summary>
    /// 提交K位申请的XML
    /// </summary>
    public class XMLApplyInfo
    {
        /// <summary>
        /// 0001
        /// </summary>
        public string applyuser = string.Empty;
        /// <summary>
        /// 1
        /// </summary>
        public string kpolicyid = string.Empty;
        /// <summary>
        /// 2008-12-14
        /// </summary>
        public string date = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string new_bunk = string.Empty;
        /// <summary>
        /// 3
        /// </summary>
        public string bunkamount = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string pnr = string.Empty;

        public XMLPassenger []passengerss=null;
    }

}
