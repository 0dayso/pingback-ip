using System;

namespace logic
{
	/// <summary>
	/// PolicyInfo 的摘要说明。
	/// </summary>
	public class PolicyInfo
	{
		public PolicyInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string strPoliId = "";	//政策编号
		public string strAirRetGain = "";//航空公司返点
		public string strGainId = "";
		public string strDiscount = "";
		public string strRetGain = "";
		public string strSeatName = "";
		public string strAgentId = "";	//政策提供商的ID
		public string strAgentName = "";
		public string strPubUserTitle = "";
		public string strPubGain = "";	//政策共享的返点
		public string strPoliBegin = "";	//政策开始时间
		public string strPoliEnd = "";	//政策结束时间
		public string strSecRate = "";  //二级分销商的返点
		public string strSecAgentCode = ""; //二级分销商的代码
		public string strSecGainJe = ""; //二级分销商赚取的利润
	}
}
