using System;
using System.Collections;
using logic.mytool;
using gs.DataBase;
using System.Data;

namespace logic
{
	/// <summary>
	/// EgAir ��ժҪ˵����
	/// </summary>
	public class EgAir
	{
		public EgAir()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private static Hashtable m_htSeats = null;
		/// <summary>
		/// �õ����չ�˾�Ĳ�λ������ձ�
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
		/// �õ����չ�˾���ֺͱ�����ձ�HASH
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
