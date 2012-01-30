using System;
using System.Collections;
using logic.mytool;
using gs.DataBase;
using System.Data;

namespace logic
{
	/// <summary>
	/// EgAir 的摘要说明。
	/// </summary>
	public class EgAir
	{
		public EgAir()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private static Hashtable m_htSeats = null;
		/// <summary>
		/// 得到航空公司的舱位情况对照表
		/// </summary>
		/// <param name="p_strAirName"></param>
		/// <returns></returns>
		public Hashtable getCabinDiscount()
		{
			if(m_htSeats == null)
			{
				m_htSeats = new Hashtable();
				Conn cn = null;
				try
				{
					DataSet rsRet = null;
					cn = new Conn();
					string strSql = "";
					strSql = "select * from eg_PolicSeat";
					rsRet = cn.GetDataSet(strSql);
					for(int i=0;i<rsRet.Tables[0].Rows.Count;i++)
					{
						string strKey = rsRet.Tables[0].Rows[i]["vcAirCode"].ToString().Trim() + "-" + rsRet.Tables[0].Rows[i]["vcSeatName"].ToString().Trim();
						m_htSeats.Add(strKey,rsRet.Tables[0].Rows[i]["numDiscount"].ToString().Trim());
					}
				}
				catch(Exception ex)
				{
					gs.util.func.Write("logic.EgAir.getCabinDiscount is err" + ex.Message);
				}
				finally
				{
					cn.close();
				}
			}
			
			return m_htSeats;
		}

		private static Hashtable m_htAirNames = null;

		/// <summary>
		/// 得到航空公司名字和编码对照表HASH
		/// </summary>
		/// <returns></returns>
		public Hashtable getAirNamesHash()
		{
			if(m_htAirNames == null)
			{
				m_htAirNames = new Hashtable();
				DataSet dsAir = util.getDs("select * from eg_PolicyAir");
			
				for(int iAir=0;iAir<dsAir.Tables[0].Rows.Count;iAir++)
				{
					m_htAirNames.Add(dsAir.Tables[0].Rows[iAir]["vcAirCode"].ToString().Trim(),dsAir.Tables[0].Rows[iAir]["vcAirName"].ToString().Trim());
				}
			}

			return m_htAirNames;
		}

	}
}
