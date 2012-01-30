using System;
using System.Collections;
using System.Data;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// EgCity ��ժҪ˵����
	/// </summary>
	public class EgCity
	{
		public EgCity()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �õ����еĳ���
		/// </summary>
		/// <returns></returns>
		public DataSet getAllCity()
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select * from eg_PolicyCity order by vcCityCode";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.EgCity.getAllCity is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		private static Hashtable m_htCitys = null;

		/// <summary>
		/// �õ����д�����ձ�HASH
		/// </summary>
		/// <returns></returns>
		public Hashtable getCitysHash()
		{
			if(m_htCitys == null)
			{
				m_htCitys = new Hashtable();
				
				DataSet dsCitys = getAllCity();
			
				for(int iAir=0;iAir<dsCitys.Tables[0].Rows.Count;iAir++)
				{
					m_htCitys.Add(dsCitys.Tables[0].Rows[iAir]["vcCityCode"].ToString().Trim(),dsCitys.Tables[0].Rows[iAir]["vcCityName"].ToString().Trim());
				}
			}

			return m_htCitys;
		}

		public string getCityName(Hashtable p_ht,string p_strCityCode)
		{
			return p_ht[p_strCityCode].ToString();
		}

	}
}
