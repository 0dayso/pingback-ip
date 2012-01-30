using System;
using System.Data;
using gs.DataBase;
using System.Collections;
using gs.para;
using System.Xml;

namespace logic
{
	/// <summary>
	/// Custer ��ժҪ˵����
	/// </summary>
	public class Custer
	{
		public Custer()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string SaveCust(string p_strXml)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();

				strRet = SaveCust(cn,p_strXml);
			}
			finally
			{
				cn.close();
			}


			return strRet;
		}

		/// <summary>
		/// �����ͻ�����
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		public string SaveCust(Conn cn,string p_strXml)
		{
			NewPara npRet = new NewPara();
			
			string strRet = "fail";
			NewPara npCust = new NewPara(p_strXml);

			string strCustId = logic.mytool.util.getId(cn,"eg_Custer");

			string strAgentId = "";
			string strSqlAg = "select * from eg_user where vcLoginName='" + npCust.FindTextByPath("//eg/vcInpEgUser").Trim() + "'";

			DataSet dsAg = cn.GetDataSet(strSqlAg);

			if(dsAg.Tables[0].Rows.Count > 0)
			{
				strAgentId = dsAg.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
			}
			dsAg.Clear();

			string strErr = "";
			//Conn cn = null;
			try
			{
				//cn = new Conn();

				//cn.beginTrans();
				
				string strIdentCard = npCust.FindTextByPath("//eg/vcIdentCard").Trim();
				string strSql = "select * from eg_Custer where vcIdentCard='" + strIdentCard + "'";

				DataSet ds = cn.GetDataSet(strSql);

				string strCustName = npCust.FindTextByPath("//eg/vcCustName").Trim();
				string strMobile = npCust.FindTextByPath("//eg/vcMobile").Trim();
				
				if(ds.Tables[0].Rows.Count == 0 && strCustName != "" && strMobile != "" && strIdentCard != "")
				{

					Top2 tp = new Top2(cn);                                                                  
			
					tp.AddRow("numCustId","i",strCustId);
					tp.AddRow("vcCustNo","c",npCust.FindTextByPath("//eg/vcCustNo"));
					tp.AddRow("vcCustName","c",strCustName);
					tp.AddRow("vcTel","c",npCust.FindTextByPath("//eg/vcTel"));
					tp.AddRow("vcMobile","c",strMobile);
					tp.AddRow("vcInpEgUser","c",npCust.FindTextByPath("//eg/vcInpEgUser"));
					tp.AddRow("vcIdentCard","c",strIdentCard);
					if(npCust.FindTextByPath("//eg/numIdentType") == "")
					{
						tp.AddRow("numIdentType","i","0");
					}
					else
					{
						tp.AddRow("numIdentType","i",npCust.FindTextByPath("//eg/numIdentType"));
					}
					tp.AddRow("vcUnitName","c",npCust.FindTextByPath("//eg/vcUnitName"));
					tp.AddRow("numAgentId","c",strAgentId);
					if(npCust.FindTextByPath("//eg/numCustType") != "")
					{
						tp.AddRow("numCustType","i",npCust.FindTextByPath("//eg/numCustType"));
					}
					if(npCust.FindTextByPath("//eg/numMemSrc") != "")
					{
						tp.AddRow("numMemSrc","i",npCust.FindTextByPath("//eg/numMemSrc"));
					}
					tp.AddRow("numMemScore","i","0");
					tp.AddRow("dtCustBuildTime","t","getdate()");
					//tp.AddRow("dtLastFee","c","1949-10-1");
					tp.AddRow("vcCustAdr","c",npCust.FindTextByPath("//eg/vcCustAdr"));
					tp.AddRow("vcPostCode","c",npCust.FindTextByPath("//eg/vcPostCode"));
					tp.AddRow("dtBirthDay","c",npCust.FindTextByPath("//eg/dtBirthDay"));
					tp.AddRow("vcCredCardOne","c",npCust.FindTextByPath("//eg/vcCredCardOne"));
					tp.AddRow("vcCredCardTwo","c",npCust.FindTextByPath("//eg/vcCredCardTwo"));
				
						
					if(tp.AddNewRec("eg_Custer"))
					{
						strErr = "�����ɹ�";
						strRet = "succ";
					}
					else
					{
						strErr = "����ʧ��,��������";
						gs.util.func.Write("logic.SaveCust is err ����ʧ��,��������");
					}
				}
				else
				{
					strCustId = ds.Tables[0].Rows[0]["numCustId"].ToString().Trim();
					strRet = "reCust";
				}
				//cn.commit();
			}
			catch(Exception ex)
			{
				//cn.rollback();
				strErr = "�����¿ͻ�ʧ��" + ex.Message;
				gs.util.func.Write("logic.SaveCust is err �����¿ͻ�ʧ��" + ex.Message);
			}
			finally
			{
				//cn.close();
			}

			npRet.AddPara("cm","RetSaveCust");//������Ϣ
			npRet.AddPara("Flag",strRet);
			npRet.AddPara("CustId",strCustId);
			npRet.AddPara("Mes",strErr);

			return npRet.GetXML();
		}

		/// <summary>
		/// �޸Ŀͻ�����
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		public string UpdateCust(string p_strXml)
		{
			NewPara npRet = new NewPara();
			
			string strRet = "fail";
			NewPara npCust = new NewPara(p_strXml);

			string strCustId = npCust.FindTextByPath("//eg/numCustId").Trim();
			//string strAgentId = Right.getAgent(npCust.FindTextByPath("//eg/vcInpEgUser").Trim());

			string strErr = "";
			Conn cn = null;
			try
			{
				cn = new Conn();

				//cn.beginTrans();
				
				string strIdentCard = npCust.FindTextByPath("//eg/vcIdentCard").Trim();
				string strSql = "select * from eg_Custer where vcIdentCard='" + strIdentCard + "' and numCustId<>" + strCustId;

				DataSet ds = cn.GetDataSet(strSql);

				if(ds.Tables[0].Rows.Count == 0)
				{

					Top2 tp = new Top2(cn);                                                                  
			
					//tp.AddRow("numCustId","i",strCustId);
					tp.AddRow("vcCustNo","c",npCust.FindTextByPath("//eg/vcCustNo"));
					tp.AddRow("vcCustName","c",npCust.FindTextByPath("//eg/vcCustName"));
					tp.AddRow("vcTel","c",npCust.FindTextByPath("//eg/vcTel"));
					tp.AddRow("vcMobile","c",npCust.FindTextByPath("//eg/vcMobile"));
					//tp.AddRow("vcInpEgUser","c",npCust.FindTextByPath("//eg/vcInpEgUser"));
					tp.AddRow("vcIdentCard","c",strIdentCard);
					tp.AddRow("numIdentType","i",npCust.FindTextByPath("//eg/numIdentType"));
					tp.AddRow("vcUnitName","c",npCust.FindTextByPath("//eg/vcUnitName"));
					//tp.AddRow("numAgentId","c",strAgentId);
					tp.AddRow("numCustType","i",npCust.FindTextByPath("//eg/numCustType"));
					tp.AddRow("numMemSrc","i",npCust.FindTextByPath("//eg/numMemSrc"));
					//tp.AddRow("numMemScore","i","0");
					tp.AddRow("dtCustBuildTime","t","getdate()");
					//tp.AddRow("dtLastFee","c","1949-10-1");
					tp.AddRow("vcCustAdr","c",npCust.FindTextByPath("//eg/vcCustAdr"));
					tp.AddRow("vcPostCode","c",npCust.FindTextByPath("//eg/vcPostCode"));
					tp.AddRow("dtBirthDay","c",npCust.FindTextByPath("//eg/dtBirthDay"));
					tp.AddRow("vcCredCardOne","c",npCust.FindTextByPath("//eg/vcCredCardOne"));
					tp.AddRow("vcCredCardTwo","c",npCust.FindTextByPath("//eg/vcCredCardTwo"));
				
						
					if(tp.Update("eg_Custer","numCustId",strCustId))
					{
						strErr = "�޸ĳɹ�";
						strRet = "succ";
					}
					else
					{
						strErr = "�޸�ʧ��,��������";
						gs.util.func.Write("logic.UpdateCust is err �޸�ʧ��,��������");
					}
				}
				else
				{
					strRet = "reCust";
				}
				//cn.commit();
			}
			catch(Exception ex)
			{
				//cn.rollback();
				strErr = "�޸Ŀͻ�����ʧ��" + ex.Message;
				gs.util.func.Write("logic.UpdateCust is err �޸Ŀͻ�ʧ��" + ex.Message);
			}
			finally
			{
				cn.close();
			}

			npRet.AddPara("cm","RetUpdateCust");//������Ϣ
			npRet.AddPara("Flag",strRet);
			npRet.AddPara("CustId",strCustId);
			npRet.AddPara("Mes",strErr);

			return npRet.GetXML();
		}

		/// <summary>
		/// ���ӿͻ����Ѽ�¼
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		public string NewCustAirFee(string p_strXml)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();

				strRet = NewCustAirFee(cn,p_strXml);
			}
			finally
			{
				cn.close();
			}


			return strRet;
		}

		public string NewCustAirFee(Conn cn,string p_strXml)
		{
			NewPara npRet = new NewPara();
			
			string strRet = "fail";
			NewPara npCust = new NewPara(p_strXml);

			string strCustId = npCust.FindTextByPath("//eg/numCustId").Trim();

			string strErr = "";
			//Conn cn = null;
			try
			{
				//cn = new Conn();

				//cn.beginTrans();
				
				string strSql = "select * from eg_Custer where numCustId=" + strCustId;

				DataSet ds = cn.GetDataSet(strSql);

				if(ds.Tables[0].Rows.Count > 0)
				{

					Top2 tp = new Top2(cn);                                                                  
			
					tp.AddRow("numCustId","i",strCustId);
					tp.AddRow("vcSrcCityCode","c",npCust.FindTextByPath("//eg/vcSrcCityCode"));
					tp.AddRow("vcDstCityCode","c",npCust.FindTextByPath("//eg/vcDstCityCode"));
					tp.AddRow("dtFlyTime","c",npCust.FindTextByPath("//eg/dtFlyTime"));
					tp.AddRow("vcAirNo","c",npCust.FindTextByPath("//eg/vcAirNo"));
					tp.AddRow("vcAirCorp","c",npCust.FindTextByPath("//eg/vcAirCorp"));
					tp.AddRow("vcCabin","c",npCust.FindTextByPath("//eg/vcCabin"));
					if(npCust.FindTextByPath("//eg/numPrice") != "")
					{
						tp.AddRow("numPrice","d",npCust.FindTextByPath("//eg/numPrice"));
					}
					tp.AddRow("vcDiscount","c",npCust.FindTextByPath("//eg/vcDiscount"));
					if(npCust.FindTextByPath("//eg/numDist") != "")
					{
						tp.AddRow("numDist","i",npCust.FindTextByPath("//eg/numDist"));
					}
					tp.AddRow("vcPnr","c",npCust.FindTextByPath("//eg/vcPnr"));
					tp.AddRow("vcEtkId","c",npCust.FindTextByPath("//eg/vcEtkId"));
					tp.AddRow("dtFeeTime","t","getdate()");
				
						
					if(tp.AddNewRec("eg_CustFeeRec"))
					{
						strErr = "�����ͻ����Ѽ�¼�ɹ�";
						strRet = "succ";
					}
					else
					{
						strErr = "�޸�ʧ��,��������";
						gs.util.func.Write("logic.Custer.NewCustAirFee is err �����ͻ����Ѽ�¼ʧ��,��������");
					}
				}
				else
				{
					strRet = "NoCust";
				}
				//cn.commit();
			}
			catch(Exception ex)
			{
				//cn.rollback();
				strErr = "�����ͻ����Ѽ�¼ʧ��" + ex.Message;
				gs.util.func.Write("logic.Custer.NewCustAirFee is err �������Ѽ�¼ʧ��" + ex.Message);
				throw ex;
			}
			finally
			{
				//cn.close();
			}

			npRet.AddPara("cm","RetNewCustAirFee");//������Ϣ
			npRet.AddPara("Flag",strRet);
			npRet.AddPara("Mes",strErr);

			return npRet.GetXML();
		}

		/// <summary>
		/// ���ݿͻ������¼���ҿͻ�����
		/// </summary>
		/// <param name="p_strMobileNo"></param>
		/// <returns></returns>
		public string getCustInfoByMobile(string p_strMobileNo)
		{
			DataSet ds = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "SELECT dbo.eg_CustIdent.vcIdentType, dbo.eg_CustMemSrc.vcMenSrcDesc, dbo.eg_CustType.vcCustType, dbo.eg_Custer.*,dbo.getAgentName(eg_Custer.numAgentId) as vcNewAgentName " + 
					" FROM dbo.eg_Custer  LEFT OUTER JOIN dbo.eg_CustType ON dbo.eg_Custer.numCustType = dbo.eg_CustType.numCustTypeId LEFT OUTER JOIN dbo.eg_CustMemSrc ON dbo.eg_Custer.numMemSrc = dbo.eg_CustMemSrc.numMemSrcId  LEFT OUTER JOIN dbo.eg_CustIdent ON dbo.eg_Custer.numIdentType = dbo.eg_CustIdent.numIdentType " + 
					" where vcMobile='" + p_strMobileNo + "'"; //
				//gs.util.func.Write(strSql);
				ds = cn.GetDataSet(strSql);

			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.getCustInfoByMobile is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}

			NewPara npRet = new NewPara();

			npRet.AddPara("cm","RetCustInfoByMobile");//������Ϣ

			if(ds.Tables[0].Rows.Count > 0)
			{
				npRet.AddPara("bExistCust","true");
				npRet.AddPara("numCustId",ds.Tables[0].Rows[0]["numCustId"].ToString().Trim());
				npRet.AddPara("vcCustNo",ds.Tables[0].Rows[0]["vcCustNo"].ToString().Trim());
				npRet.AddPara("vcCustName",ds.Tables[0].Rows[0]["vcCustName"].ToString().Trim());
				npRet.AddPara("vcTel",ds.Tables[0].Rows[0]["vcTel"].ToString().Trim());
				npRet.AddPara("vcIdentCard",ds.Tables[0].Rows[0]["vcIdentCard"].ToString().Trim());
				npRet.AddPara("vcIdentType",ds.Tables[0].Rows[0]["vcIdentType"].ToString().Trim());
				npRet.AddPara("vcCustType",ds.Tables[0].Rows[0]["vcCustType"].ToString().Trim());
				npRet.AddPara("vcMenSrcDesc",ds.Tables[0].Rows[0]["vcMenSrcDesc"].ToString().Trim());
				npRet.AddPara("numMemScore",ds.Tables[0].Rows[0]["numMemScore"].ToString().Trim());
				npRet.AddPara("dtCustBuildTime",ds.Tables[0].Rows[0]["dtCustBuildTime"].ToString().Trim());
				npRet.AddPara("dtLastFee",ds.Tables[0].Rows[0]["dtLastFee"].ToString().Trim());
				npRet.AddPara("numAgentId",ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim());
				npRet.AddPara("vcInpEgUser",ds.Tables[0].Rows[0]["vcInpEgUser"].ToString().Trim());
				npRet.AddPara("vcNewAgentName",ds.Tables[0].Rows[0]["vcNewAgentName"].ToString().Trim());
				npRet.AddPara("vcCustAdr",ds.Tables[0].Rows[0]["vcCustAdr"].ToString().Trim());
				npRet.AddPara("dtBirthDay",ds.Tables[0].Rows[0]["dtBirthDay"].ToString().Trim());
				npRet.AddPara("vcCredCardOne",ds.Tables[0].Rows[0]["vcCredCardOne"].ToString().Trim());
			}
			else
			{
				npRet.AddPara("bExistCust","false");
			}

			return npRet.GetXML();
		}


		/// <summary>
		/// �����ͻ������¼
		/// </summary>
		/// <param name="p_strXml"></param>
		/// <returns></returns>
		public string NewCustCallRec(string p_strXml)
		{
			NewPara npRet = new NewPara();
			
			string strRet = "fail";
			NewPara npCust = new NewPara(p_strXml);

			string strCustId = npCust.FindTextByPath("//eg/numCustId").Trim();
			string strMobileNo = npCust.FindTextByPath("//eg/MobileNo").Trim();

			string strErr = "";
			Conn cn = null;
			try
			{
				cn = new Conn();

								
				string strSql = "select * from eg_Custer where numCustId=" + strCustId;

				DataSet ds = cn.GetDataSet(strSql);

				if(ds.Tables[0].Rows.Count > 0)
				{

					string strIns = "insert into eg_CustCallRec(numCustId,vcCallNo) values(" + strCustId + ",'" + strMobileNo + "')";
					cn.Update(strIns);
					strRet = "succ";
				}
				else
				{
					strRet = "NoCust";
				}
				
			}
			catch(Exception ex)
			{
				
				strErr = "�����ͻ������¼ʧ��" + ex.Message;
				gs.util.func.Write("logic.Custer.NewCustCallRec is err �����ͻ�����ʧ��" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}

			npRet.AddPara("cm","RetNewCustCallRec");//������Ϣ
			npRet.AddPara("Flag",strRet);
			npRet.AddPara("Mes",strErr);

			return npRet.GetXML();
		}

		/// <summary>
		/// �õ��ͻ����10�������¼
		/// </summary>
		/// <param name="p_strCustId"></param>
		/// <returns></returns>
		public string GetCustLastCalls(string p_strCustId)
		{
			NewPara npRet = new NewPara();
			string strRet = "";
			string strFeeRet = "";

			Conn cn = null;
			try
			{
				cn = new Conn();

								
				string strSql = "select top 10 * from eg_CustCallRec where numCustId=" + p_strCustId + " order by dtCallTime desc";

				DataSet ds = cn.GetDataSet(strSql);
				
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					strRet += ds.Tables[0].Rows[i]["vcCallNo"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["dtCallTime"].ToString().Trim() + "$";
				}

				if(strRet != "")
					strRet = strRet.Substring(0,strRet.Length-1);
				
				ds.Clear();


				//strSql = "select top 10 * from eg_CustFeeRec where numCustId=" + p_strCustId + " order by numRecId desc";
				strSql = "SELECT top 10 dbo.eg_PolicyAir.vcAirName AS vcAirName,dbo.eg_PolicyCity.vcCityName AS vcSrcCityName,eg_PolicyCity_1.vcCityName AS vcDstCityName, dbo.eg_CustFeeRec.* " +
					" FROM dbo.eg_CustFeeRec LEFT OUTER JOIN  dbo.eg_PolicyAir ON dbo.eg_CustFeeRec.vcAirCorp = dbo.eg_PolicyAir.vcAirCode LEFT OUTER JOIN dbo.eg_PolicyCity eg_PolicyCity_1 ON dbo.eg_CustFeeRec.vcDstCityCode = eg_PolicyCity_1.vcCityCode LEFT OUTER JOIN dbo.eg_PolicyCity ON dbo.eg_CustFeeRec.vcSrcCityCode = dbo.eg_PolicyCity.vcCityCode where numCustId=" + p_strCustId + " order by numRecId desc";
				ds = cn.GetDataSet(strSql);
				
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					strFeeRet += ds.Tables[0].Rows[i]["vcAirName"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["vcSrcCityName"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["vcDstCityName"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["dtFlyTime"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["vcAirNo"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["vcCabin"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["numPrice"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["vcDiscount"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["vcPnr"].ToString().Trim() + "~" + ds.Tables[0].Rows[i]["dtFeeTime"].ToString().Trim() + "$";
				}

				if(strRet != "")
					strFeeRet = strFeeRet.Substring(0,strFeeRet.Length-1);

				ds.Clear();
				
			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Custer.getCustLastCalls is err �õ��ͻ����10�������¼ʧ��" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}

			npRet.AddPara("cm","RetGetCustLastCalls");//������Ϣ
			npRet.AddPara("Content",strRet);
			npRet.AddPara("AirFee",strFeeRet);

			return npRet.GetXML();
		}
	}
}
