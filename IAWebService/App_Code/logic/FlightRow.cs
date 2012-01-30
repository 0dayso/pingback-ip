using System;

namespace logic
{
	/// <summary>
	/// FlightRow 的摘要说明。
	/// </summary>
	public class FlightRow
	{
		
		
		protected logic.PolicyInfo pi = null;
		protected string strDiscount = "";
		protected string strSeatName = "";
		protected double dbRetGain = 0;
		protected double dbEtkPrc = 0;
		protected double dbAgentEtkPrc = 0;
		protected string strCabin = "";
		protected string strPoliId = "";
		protected string strFule = "0";
		protected string strBaseFee = "0";


		public FlightRow()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			
		}

		public string m_strFule
		{ 
			get { return strFule; } 
			set { strFule = value; } 
		}

		public string m_strBaseFee
		{ 
			get { return strBaseFee; } 
			set { strBaseFee = value; } 
		}

		public logic.PolicyInfo m_pi 
		{ 
			get { return pi; } 
			set { pi = value; } 
		} 

		public string m_strDiscount
		{ 
			get { return strDiscount; } 
			set { strDiscount = value; } 
		}

		public string m_strSeatName
		{ 
			get { return strSeatName; } 
			set { strSeatName = value; } 
		}

		public double m_dbRetGain
		{ 
			get { return dbRetGain; } 
			set { dbRetGain = value; } 
		}

		public double m_dbEtkPrc
		{ 
			get { return dbEtkPrc; } 
			set { dbEtkPrc = value; } 
		}

		public double m_dbAgentEtkPrc
		{ 
			get { return dbAgentEtkPrc; } 
			set { dbAgentEtkPrc = value; } 
		}

		public string m_strCabin
		{ 
			get { return strCabin; } 
			set { strCabin = value; } 
		}

		
	}
}
