﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2300
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// 此源代码由 wsdl, Version=1.1.4322.2300 自动生成。
// 
namespace logic {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="mysmsSoap", Namespace="http://tempuri.org/")]
    public class mysms : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public mysms() {
            this.Url = "http://hbpiao.3322.org:81/SMSWS/mysms.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendSms", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendSms(string p_strPhoneNo, string p_strSmsContent) {
            object[] results = this.Invoke("SendSms", new object[] {
                        p_strPhoneNo,
                        p_strSmsContent});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSendSms(string p_strPhoneNo, string p_strSmsContent, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendSms", new object[] {
                        p_strPhoneNo,
                        p_strSmsContent}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSendSms(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
    }
}
