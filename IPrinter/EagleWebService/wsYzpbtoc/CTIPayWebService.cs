﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3053
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码由 wsdl 自动生成, Version=2.0.50727.3038。
// 
namespace EagleWebService
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "CTIPayWebServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class CTIPayWebService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback RefundOperationCompleted;

        private System.Threading.SendOrPostCallback ReversalOperationCompleted;

        /// <remarks/>
        public CTIPayWebService()
        {
            //this.Url = "http://192.168.1.3/CTIPayService/CTIPayWebService.asmx";
            this.Url = "http://221.232.148.250:6000/CTIPayService/CTIPayWebService.asmx";
        }

        /// <remarks/>
        public event RefundCompletedEventHandler RefundCompleted;

        /// <remarks/>
        public event ReversalCompletedEventHandler ReversalCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Refund", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Refund(string orderID, decimal amount)
        {
            object[] results = this.Invoke("Refund", new object[] {
                        orderID,
                        amount});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRefund(string orderID, decimal amount, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Refund", new object[] {
                        orderID,
                        amount}, callback, asyncState);
        }

        /// <remarks/>
        public string EndRefund(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void RefundAsync(string orderID, decimal amount)
        {
            this.RefundAsync(orderID, amount, null);
        }

        /// <remarks/>
        public void RefundAsync(string orderID, decimal amount, object userState)
        {
            if ((this.RefundOperationCompleted == null))
            {
                this.RefundOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRefundOperationCompleted);
            }
            this.InvokeAsync("Refund", new object[] {
                        orderID,
                        amount}, this.RefundOperationCompleted, userState);
        }

        private void OnRefundOperationCompleted(object arg)
        {
            if ((this.RefundCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RefundCompleted(this, new RefundCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Reversal", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Reversal(string orderID)
        {
            object[] results = this.Invoke("Reversal", new object[] {
                        orderID});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReversal(string orderID, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Reversal", new object[] {
                        orderID}, callback, asyncState);
        }

        /// <remarks/>
        public string EndReversal(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ReversalAsync(string orderID)
        {
            this.ReversalAsync(orderID, null);
        }

        /// <remarks/>
        public void ReversalAsync(string orderID, object userState)
        {
            if ((this.ReversalOperationCompleted == null))
            {
                this.ReversalOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReversalOperationCompleted);
            }
            this.InvokeAsync("Reversal", new object[] {
                        orderID}, this.ReversalOperationCompleted, userState);
        }

        private void OnReversalOperationCompleted(object arg)
        {
            if ((this.ReversalCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReversalCompleted(this, new ReversalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void RefundCompletedEventHandler(object sender, RefundCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RefundCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal RefundCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void ReversalCompletedEventHandler(object sender, ReversalCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReversalCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ReversalCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
