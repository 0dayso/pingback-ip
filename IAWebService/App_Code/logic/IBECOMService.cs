//------------------------------------------------------------------------------
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
namespace logic 
{
	using System.Diagnostics;
	using System.Xml.Serialization;
	using System;
	using System.Web.Services.Protocols;
	using System.ComponentModel;
	using System.Web.Services;
    
    
	/// <remarks/>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Web.Services.WebServiceBindingAttribute(Name="IBECOMPortBinding", Namespace="http://com/")]
	public class IBECOMService : System.Web.Services.Protocols.SoapHttpClientProtocol 
	{
        
		/// <remarks/>
		public IBECOMService() 
		{
			//this.Url = "http://219.139.240.93:8080/IBEClient/IBECOM";
			this.Url = gs.util.func.getIbeUrl();
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://com/", ResponseNamespace="http://com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		[return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string getFdTime([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0) 
		{
			object[] results = this.Invoke("getFdTime", new object[] {
																		 arg0});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BegingetFdTime(string arg0, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getFdTime", new object[] {
																  arg0}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EndgetFdTime(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://com/", ResponseNamespace="http://com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		[return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string getAV([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg1, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg2, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg3) 
		{
			object[] results = this.Invoke("getAV", new object[] {
																	 arg0,
																	 arg1,
																	 arg2,
																	 arg3});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BegingetAV(string arg0, string arg1, string arg2, string arg3, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getAV", new object[] {
															  arg0,
															  arg1,
															  arg2,
															  arg3}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EndgetAV(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://com/", ResponseNamespace="http://com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		[return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string MakeOrder([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0) 
		{
			object[] results = this.Invoke("MakeOrder", new object[] {
																		 arg0});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BeginMakeOrder(string arg0, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("MakeOrder", new object[] {
																  arg0}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EndMakeOrder(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://com/", ResponseNamespace="http://com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		[return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string delPnr([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0) 
		{
			object[] results = this.Invoke("delPnr", new object[] {
																	  arg0});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BegindelPnr(string arg0, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("delPnr", new object[] {
															   arg0}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EnddelPnr(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://com/", ResponseNamespace="http://com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		[return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string getFDWS([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg1, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg2) 
		{
			object[] results = this.Invoke("getFDWS", new object[] {
																	   arg0,
																	   arg1,
																	   arg2});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BegingetFDWS(string arg0, string arg1, string arg2, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getFDWS", new object[] {
																arg0,
																arg1,
																arg2}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EndgetFDWS(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://com/", ResponseNamespace="http://com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		[return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string getRt([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0) 
		{
			object[] results = this.Invoke("getRt", new object[] {
																	 arg0});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BegingetRt(string arg0, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getRt", new object[] {
															  arg0}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EndgetRt(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
	}
}
