﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18034
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码由 wsdl 自动生成, Version=4.0.30319.17929。
// 
namespace XiAnQiYi
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback alterableApprovalOperationCompleted;

        private System.Threading.SendOrPostCallback policyCancelOperationCompleted;

        private System.Threading.SendOrPostCallback alterableApproval_LBOperationCompleted;

        private System.Threading.SendOrPostCallback policyCancel_LBOperationCompleted;

        /// <remarks/>
        public Service()
        {
            this.Url = "http://218.30.22.181/Service.asmx";
        }

        /// <remarks/>
        public event alterableApprovalCompletedEventHandler alterableApprovalCompleted;

        /// <remarks/>
        public event policyCancelCompletedEventHandler policyCancelCompleted;

        /// <remarks/>
        public event alterableApproval_LBCompletedEventHandler alterableApproval_LBCompleted;

        /// <remarks/>
        public event policyCancel_LBCompletedEventHandler policyCancel_LBCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/alterableApproval", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string alterableApproval(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string productCode)
        {
            object[] results = this.Invoke("alterableApproval", new object[] {
                        name,
                        pwd,
                        ApplicantBirth,
                        ApplicantCercCode,
                        ApplicantCertType,
                        ApplicantName,
                        ApplicantSex,
                        By1,
                        ClaimantCercCode,
                        ClaimantCertType,
                        ClaimantName,
                        FlightNo,
                        InsurantNexus,
                        InsuredBirth,
                        InsuredCercCode,
                        InsuredCertType,
                        InsuredName,
                        InsuredSex,
                        PhoneNumber,
                        PolicyBeginDate,
                        productCode});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginalterableApproval(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string productCode,
                    System.AsyncCallback callback,
                    object asyncState)
        {
            return this.BeginInvoke("alterableApproval", new object[] {
                        name,
                        pwd,
                        ApplicantBirth,
                        ApplicantCercCode,
                        ApplicantCertType,
                        ApplicantName,
                        ApplicantSex,
                        By1,
                        ClaimantCercCode,
                        ClaimantCertType,
                        ClaimantName,
                        FlightNo,
                        InsurantNexus,
                        InsuredBirth,
                        InsuredCercCode,
                        InsuredCertType,
                        InsuredName,
                        InsuredSex,
                        PhoneNumber,
                        PolicyBeginDate,
                        productCode}, callback, asyncState);
        }

        /// <remarks/>
        public string EndalterableApproval(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void alterableApprovalAsync(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string productCode)
        {
            this.alterableApprovalAsync(name, pwd, ApplicantBirth, ApplicantCercCode, ApplicantCertType, ApplicantName, ApplicantSex, By1, ClaimantCercCode, ClaimantCertType, ClaimantName, FlightNo, InsurantNexus, InsuredBirth, InsuredCercCode, InsuredCertType, InsuredName, InsuredSex, PhoneNumber, PolicyBeginDate, productCode, null);
        }

        /// <remarks/>
        public void alterableApprovalAsync(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string productCode,
                    object userState)
        {
            if ((this.alterableApprovalOperationCompleted == null))
            {
                this.alterableApprovalOperationCompleted = new System.Threading.SendOrPostCallback(this.OnalterableApprovalOperationCompleted);
            }
            this.InvokeAsync("alterableApproval", new object[] {
                        name,
                        pwd,
                        ApplicantBirth,
                        ApplicantCercCode,
                        ApplicantCertType,
                        ApplicantName,
                        ApplicantSex,
                        By1,
                        ClaimantCercCode,
                        ClaimantCertType,
                        ClaimantName,
                        FlightNo,
                        InsurantNexus,
                        InsuredBirth,
                        InsuredCercCode,
                        InsuredCertType,
                        InsuredName,
                        InsuredSex,
                        PhoneNumber,
                        PolicyBeginDate,
                        productCode}, this.alterableApprovalOperationCompleted, userState);
        }

        private void OnalterableApprovalOperationCompleted(object arg)
        {
            if ((this.alterableApprovalCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.alterableApprovalCompleted(this, new alterableApprovalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/policyCancel", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string policyCancel(string name, string pwd, string policyNo)
        {
            object[] results = this.Invoke("policyCancel", new object[] {
                        name,
                        pwd,
                        policyNo});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginpolicyCancel(string name, string pwd, string policyNo, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("policyCancel", new object[] {
                        name,
                        pwd,
                        policyNo}, callback, asyncState);
        }

        /// <remarks/>
        public string EndpolicyCancel(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void policyCancelAsync(string name, string pwd, string policyNo)
        {
            this.policyCancelAsync(name, pwd, policyNo, null);
        }

        /// <remarks/>
        public void policyCancelAsync(string name, string pwd, string policyNo, object userState)
        {
            if ((this.policyCancelOperationCompleted == null))
            {
                this.policyCancelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpolicyCancelOperationCompleted);
            }
            this.InvokeAsync("policyCancel", new object[] {
                        name,
                        pwd,
                        policyNo}, this.policyCancelOperationCompleted, userState);
        }

        private void OnpolicyCancelOperationCompleted(object arg)
        {
            if ((this.policyCancelCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.policyCancelCompleted(this, new policyCancelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/alterableApproval_LB", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string alterableApproval_LB(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string PolicyEndDate,
                    string productCode)
        {
            object[] results = this.Invoke("alterableApproval_LB", new object[] {
                        name,
                        pwd,
                        ApplicantBirth,
                        ApplicantCercCode,
                        ApplicantCertType,
                        ApplicantName,
                        ApplicantSex,
                        By1,
                        ClaimantCercCode,
                        ClaimantCertType,
                        ClaimantName,
                        FlightNo,
                        InsurantNexus,
                        InsuredBirth,
                        InsuredCercCode,
                        InsuredCertType,
                        InsuredName,
                        InsuredSex,
                        PhoneNumber,
                        PolicyBeginDate,
                        PolicyEndDate,
                        productCode});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginalterableApproval_LB(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string PolicyEndDate,
                    string productCode,
                    System.AsyncCallback callback,
                    object asyncState)
        {
            return this.BeginInvoke("alterableApproval_LB", new object[] {
                        name,
                        pwd,
                        ApplicantBirth,
                        ApplicantCercCode,
                        ApplicantCertType,
                        ApplicantName,
                        ApplicantSex,
                        By1,
                        ClaimantCercCode,
                        ClaimantCertType,
                        ClaimantName,
                        FlightNo,
                        InsurantNexus,
                        InsuredBirth,
                        InsuredCercCode,
                        InsuredCertType,
                        InsuredName,
                        InsuredSex,
                        PhoneNumber,
                        PolicyBeginDate,
                        PolicyEndDate,
                        productCode}, callback, asyncState);
        }

        /// <remarks/>
        public string EndalterableApproval_LB(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void alterableApproval_LBAsync(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string PolicyEndDate,
                    string productCode)
        {
            this.alterableApproval_LBAsync(name, pwd, ApplicantBirth, ApplicantCercCode, ApplicantCertType, ApplicantName, ApplicantSex, By1, ClaimantCercCode, ClaimantCertType, ClaimantName, FlightNo, InsurantNexus, InsuredBirth, InsuredCercCode, InsuredCertType, InsuredName, InsuredSex, PhoneNumber, PolicyBeginDate, PolicyEndDate, productCode, null);
        }

        /// <remarks/>
        public void alterableApproval_LBAsync(
                    string name,
                    string pwd,
                    string ApplicantBirth,
                    string ApplicantCercCode,
                    string ApplicantCertType,
                    string ApplicantName,
                    string ApplicantSex,
                    string By1,
                    string ClaimantCercCode,
                    string ClaimantCertType,
                    string ClaimantName,
                    string FlightNo,
                    string InsurantNexus,
                    string InsuredBirth,
                    string InsuredCercCode,
                    string InsuredCertType,
                    string InsuredName,
                    string InsuredSex,
                    string PhoneNumber,
                    string PolicyBeginDate,
                    string PolicyEndDate,
                    string productCode,
                    object userState)
        {
            if ((this.alterableApproval_LBOperationCompleted == null))
            {
                this.alterableApproval_LBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnalterableApproval_LBOperationCompleted);
            }
            this.InvokeAsync("alterableApproval_LB", new object[] {
                        name,
                        pwd,
                        ApplicantBirth,
                        ApplicantCercCode,
                        ApplicantCertType,
                        ApplicantName,
                        ApplicantSex,
                        By1,
                        ClaimantCercCode,
                        ClaimantCertType,
                        ClaimantName,
                        FlightNo,
                        InsurantNexus,
                        InsuredBirth,
                        InsuredCercCode,
                        InsuredCertType,
                        InsuredName,
                        InsuredSex,
                        PhoneNumber,
                        PolicyBeginDate,
                        PolicyEndDate,
                        productCode}, this.alterableApproval_LBOperationCompleted, userState);
        }

        private void OnalterableApproval_LBOperationCompleted(object arg)
        {
            if ((this.alterableApproval_LBCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.alterableApproval_LBCompleted(this, new alterableApproval_LBCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/policyCancel_LB", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string policyCancel_LB(string name, string pwd, string policyNo)
        {
            object[] results = this.Invoke("policyCancel_LB", new object[] {
                        name,
                        pwd,
                        policyNo});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginpolicyCancel_LB(string name, string pwd, string policyNo, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("policyCancel_LB", new object[] {
                        name,
                        pwd,
                        policyNo}, callback, asyncState);
        }

        /// <remarks/>
        public string EndpolicyCancel_LB(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void policyCancel_LBAsync(string name, string pwd, string policyNo)
        {
            this.policyCancel_LBAsync(name, pwd, policyNo, null);
        }

        /// <remarks/>
        public void policyCancel_LBAsync(string name, string pwd, string policyNo, object userState)
        {
            if ((this.policyCancel_LBOperationCompleted == null))
            {
                this.policyCancel_LBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpolicyCancel_LBOperationCompleted);
            }
            this.InvokeAsync("policyCancel_LB", new object[] {
                        name,
                        pwd,
                        policyNo}, this.policyCancel_LBOperationCompleted, userState);
        }

        private void OnpolicyCancel_LBOperationCompleted(object arg)
        {
            if ((this.policyCancel_LBCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.policyCancel_LBCompleted(this, new policyCancel_LBCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    public delegate void alterableApprovalCompletedEventHandler(object sender, alterableApprovalCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class alterableApprovalCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal alterableApprovalCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    public delegate void policyCancelCompletedEventHandler(object sender, policyCancelCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class policyCancelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal policyCancelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    public delegate void alterableApproval_LBCompletedEventHandler(object sender, alterableApproval_LBCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class alterableApproval_LBCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal alterableApproval_LBCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    public delegate void policyCancel_LBCompletedEventHandler(object sender, policyCancel_LBCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class policyCancel_LBCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal policyCancel_LBCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
