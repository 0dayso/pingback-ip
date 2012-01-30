using System;
using System.Xml;
using System.Data;
using gs.para;
using gs.DataBase;
using System.Collections;

namespace logic
{
	/// <summary>
	/// Policy ��ժҪ˵����
	/// </summary>
	public class Policy
	{
		public Policy()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="node"></param>
		/// <param name="p_strAgent">����Ǳ��ش������ֵΪ���ش�����,����Ϊ��</param>
		/// <param name="p_strAir">����һ������</param>
		/// <param name="p_strDate"></param>
		/// <param name="p_strBeginCity"></param>
		/// <param name="p_strEndCity"></param>
		/*public void getPromotXml(Conn cn,XmlDocument rootDoc,XmlNode node,bool p_bSelf,string p_strAgent,string p_strAir,string p_strDate,string p_strBeginCity,string p_strEndCity,string p_strUserLevelId,double p_dbUserDecGain)
		{
			string strAirOrg = p_strAir.Substring(0,2);	//�õ����չ�˾������
			try
			{
				
				//�õ����в�λ(���������λ)������
				string strSql = "SELECT dbo.eg_Policy.*, dbo.eg_PolicyGain.vcSpecSeatName AS vcSeatName,";
				strSql += "dbo.eg_PolicyGain.numRetGain AS numRetGain, ";
				strSql += "dbo.eg_PolicyGain.numGainId AS numGainId, ";
				strSql += "dbo.eg_PolicyGain.numDiscount AS numDiscount, ";
				strSql += "dbo.eg_PolicyGain.numIsPub AS numIsPub";
				strSql += " FROM dbo.eg_Policy INNER JOIN ";
				strSql += "dbo.eg_PolicyGain ON ";
				strSql += "dbo.eg_Policy.numPoliId = dbo.eg_PolicyGain.numPoliId ";
				
				//strSql += " where (eg_Policy.vcDiscounts is not null) and eg_Policy.vcBeginPort='" + p_strBeginCity + "' and ','+eg_Policy.vcEndPorts like '%," + p_strEndCity + ",%' ";
				strSql += " where eg_Policy.vcBeginPort='" + p_strBeginCity + "' and ','+eg_Policy.vcEndPorts like '%," + p_strEndCity + ",%' ";
				strSql += " and convert(datetime,'" + p_strDate + " 00:02:00')>dateadd(hour,1,eg_Policy.dtBegin) and convert(datetime,'" + p_strDate + " 00:02:00')<dateadd(hour,3,eg_Policy.dtEnd)";
				strSql += " and (ltrim(rtrim(eg_Policy.vcAirFlights))='' or ','+eg_Policy.vcAirFlights+',' like '%," + p_strAir + ",%')";
				strSql += " and eg_Policy.vcAirCode='" + strAirOrg + "' and eg_Policy.numIsStart=1 ";

				DataSet dsSelf = null;
				bool bSelf = false;
				if(!p_bSelf)
				{//�����ѯ���ǹ�������
					
					//������ش�����Ҳ��������صķ������ߣ�����ʹ�ô������Լ������ߡ�
					string strSqlSelf = strSql + " and eg_PolicyGain.numIsPub='" + p_strUserLevelId.Trim() + "' and eg_Policy.numAgentId like '" + p_strAgent + "%'";
					strSqlSelf += " order by eg_PolicyGain.vcSpecSeatName,eg_PolicyGain.numDiscount desc,eg_PolicyGain.numRetGain";
					dsSelf = cn.GetDataSet(strSqlSelf);
					//gs.util.func.Write("dsSelf.Tables[0].Rows.Count=" + dsSelf.Tables[0].Rows.Count + " sql=" + strSqlSelf);
					if(dsSelf.Tables[0].Rows.Count > 0)
					{
						bSelf = true;
					}
					else
					{
						//���û�в鵽���������Լ����������������ʹ��ƽ̨��������
						strSql += " and eg_PolicyGain.numIsPub='0'";
					}
				}
				else
				{//���������ɵ����ѯ�������̷�����������Ϣ
					strSql += " and eg_PolicyGain.numIsPub='" + p_strUserLevelId.Trim() + "' and eg_Policy.numAgentId like '" + p_strAgent + "%'";
				}
				strSql += " order by eg_PolicyGain.vcSpecSeatName,eg_PolicyGain.numDiscount desc,eg_PolicyGain.numRetGain";

				DataSet ds = null;
				if(bSelf)
				{//������ش�����Ҳ��������صķ������ߣ�����ʹ�ô������Լ������ߡ�
					ds = dsSelf;
				}
				else
				{
					ds = cn.GetDataSet(strSql);
				}


				//gs.util.func.Write("getPromotXml sql2" + strSql);
				int iSize = ds.Tables[0].Rows.Count;
				//string strLast = "";
				//int iMax = 0;	//�������¼�������
				for(int i=0;i<iSize;i++)
				{
					string strCurDis = ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim();//��¼��ǰ��λ

					string strPrevDis = "";
					if( (i+1) < iSize )
					{
						strPrevDis = ds.Tables[0].Rows[i+1]["vcSeatName"].ToString().Trim();
					}

					if(strCurDis != strPrevDis)//�����һ����¼�ͱ���¼�Ĳ�λ������ͬ������������һ��,�������ͬ����Ϊ�ҵ��Ǳ���λ�з�����ߵĲ�λ
					{
						XmlNode poliNode = rootDoc.CreateNode(XmlNodeType.Element,"APROMOTION","");

						XmlNode subNode = null;

						string strPoliId = ds.Tables[0].Rows[i]["numPoliId"].ToString().Trim();
						//string strTmpDis = ds.Tables[0].Rows[i]["vcSpecSeatName"].ToString().Trim();

						
						DataSet dsTmpGain = cn.GetDataSet("select numRetGain,numIsPub from eg_PolicyGain where numPoliId=" + strPoliId + " and vcSpecSeatName='" + strCurDis + "' and (numIsPub='1' or numIsPub='0')");
						string strAirRetGain = "0";//���չ�˾�ķ���
						string strPubRetGain = "0";//�����˹�������߷���
						int iTmpGainCount = dsTmpGain.Tables[0].Rows.Count;
						if(iTmpGainCount>0)
						{
							for(int iGain=0;iGain<iTmpGainCount;iGain++)
							{
								string strTmpGain = dsTmpGain.Tables[0].Rows[iGain]["numRetGain"].ToString().Trim();
								string strTmpIsPub = dsTmpGain.Tables[0].Rows[iGain]["numIsPub"].ToString().Trim();
								if(strTmpIsPub == "1")
								{
									strAirRetGain = strTmpGain;//�õ����չ�˾�ķ���
								}

								if(strTmpIsPub == "0")
								{
									strPubRetGain = strTmpGain;//�õ������˹�������߷���
								}

							}
						}

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliId","");
						subNode.AppendChild(rootDoc.CreateTextNode(strPoliId));
						poliNode.AppendChild(subNode);	//�ѱ���ֵ����ȥ

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"AirRetGain","");
						subNode.AppendChild(rootDoc.CreateTextNode(strAirRetGain));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"GainId","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["numGainId"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"Discount","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["numDiscount"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						string strRetGain = ds.Tables[0].Rows[i]["numRetGain"].ToString().Trim();
						if(!p_bSelf)
						{//�����ѯ���ǹ�������,��Ҫ�۵�
							if(!bSelf)
							{//������ش�����Ҳ��������ط������������������
								double dbRetGain = Double.Parse(strRetGain) - p_dbUserDecGain;
								strRetGain = dbRetGain.ToString().Trim();
							}
						}

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"RetGain","");
						subNode.AppendChild(rootDoc.CreateTextNode(strRetGain));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"SeatName","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"AgentId","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["numAgentId"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"AgentName","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["vcAgentName"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PubUserTitle","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["vcPubTitle"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"key","");
						subNode.AppendChild(rootDoc.CreateTextNode(p_strAir + "-" + ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PubRetGain","");
						subNode.AppendChild(rootDoc.CreateTextNode(strPubRetGain));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliBegin","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["dtBegin"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliEnd","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["dtEnd"].ToString().Trim()));
						poliNode.AppendChild(subNode);
						
						
						node.AppendChild(poliNode);
					}
					
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("getPromotXml is err" + ex.Message);
				throw ex;
			}
		}

		public string getPromote(string p_strUserCode,string p_strAirs,string p_strDate,string p_strBeginCity,string p_strEndCity)
		{
			string strRet = "";
			string strLocalAgentCode = "";

			Conn cn = null;
			try
			{
				cn = new Conn();

				//���㵱ǰ�û����ڴ��������ڳ���
				string strSqlCity = "SELECT substring(dbo.eg_agent.numAgentId,1,8) as AgentId FROM dbo.eg_agent INNER JOIN dbo.eg_user ON dbo.eg_agent.numAgentId = dbo.eg_user.numAgentId where eg_user.vcLoginName='" + p_strUserCode.Trim() + "'";
				//gs.util.func.Write("strSqlCity="+strSqlCity);
                DataSet rsRet = cn.GetDataSet(strSqlCity);
				string strAgentCode = rsRet.Tables[0].Rows[0]["AgentId"].ToString().Trim();
				rsRet.Clear();

				strSqlCity = "SELECT vcCityCode FROM eg_agent where numAgentId ='" + strAgentCode + "'";
				rsRet = cn.GetDataSet(strSqlCity);
				string strCityCode = rsRet.Tables[0].Rows[0]["vcCityCode"].ToString().Trim();	//�õ��û����ڳ�������
				rsRet.Clear();
				
				//gs.util.func.Write("fukkkkkkkkkk=1");
				//�����û��ȼ�
				string strUserLevel = "SELECT eg_Command.numCmId,eg_Command.vcCmDesc " +
					" FROM dbo.eg_userCm INNER JOIN dbo.eg_user ON dbo.eg_userCm.numUserId = dbo.eg_user.numUserId INNER JOIN  dbo.eg_Command ON dbo.eg_userCm.numCmId = dbo.eg_Command.numCmId " +
					" where  eg_userCm.numCmId like '000100020006____' and eg_userCm.numCmId<>'000100020006' and eg_user.vcLoginName='" + p_strUserCode.Trim() + "' order by eg_Command.vcCmDesc";
//gs.util.func.Write("fukkkkkkkkkk=2" + strUserLevel);
				DataSet dsLevel = cn.GetDataSet(strUserLevel);
				int iLevelCount = dsLevel.Tables[0].Rows.Count;
				
//gs.util.func.Write("fukkkkkkkkkk=3");
				string strUserLevelId = "";
				if(iLevelCount > 0)
				{//gs.util.func.Write("fukkkkkkkkkk=4");
					strUserLevelId = dsLevel.Tables[0].Rows[0]["numCmId"].ToString().Trim();
				}
				else
				{//gs.util.func.Write("fukkkkkkkkkk=5");
					strUserLevel = "SELECT eg_Command.numCmId,eg_Command.vcCmDesc from eg_Command where numCmId like '000100020006____' and numCmId<>'000100020006' order by eg_Command.vcCmDesc desc";
					dsLevel = cn.GetDataSet(strUserLevel);
					strUserLevelId = dsLevel.Tables[0].Rows[0]["numCmId"].ToString().Trim();
				}
//gs.util.func.Write("fukkkkkkkkkk=6");
				//������û����ڵǼǶ�Ӧ�ķ���۵�
				string strGainSql = "select * from eg_AgentGain where numAgentId='" + strAgentCode + "' and numCmId='" + strUserLevelId + "'";
				DataSet dsGain = cn.GetDataSet(strGainSql);
//gs.util.func.Write("fukkkkkkkkkk=7");				
				double dbUserDecGain = 0;
				if(dsGain.Tables[0].Rows.Count > 0)
				{
					dbUserDecGain = Double.Parse(dsGain.Tables[0].Rows[0]["numGain"].ToString());
				}
//gs.util.func.Write("fukkkkkkkkkk=8");				

				bool bSelf = false; //�����Ƿ�����������ڳ��г������ÿ�
				strLocalAgentCode = strAgentCode;
				if(strCityCode == p_strBeginCity.Trim())
				{
					bSelf = true;
				}

				NewPara np = new NewPara();
				XmlDocument doc = np.getRoot();
				np.AddPara("cm","RetPormot");//������Ϣ˵���������������
				XmlNode nodePromot = np.AddPara("Promots","");

				string[] aryAir = p_strAirs.Split(',');
				for(int i=0;i<aryAir.Length;i++)
				{
					string strAir = aryAir[i].Trim();
					//XmlNode subAirNode = doc.CreateNode(XmlNodeType.Element,"AIRPROMOT","");
					//getPromotXml(cn,doc,subAirNode,strLocalAgentCode,strAir,p_strDate,p_strBeginCity,p_strEndCity);			
					//nodePromot.AppendChild(subAirNode);
					getPromotXml(cn,doc,nodePromot,bSelf,strLocalAgentCode,strAir,p_strDate,p_strBeginCity,p_strEndCity,strUserLevelId,dbUserDecGain);			
				}
				strRet = np.GetXML();

			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Policy.getPromote is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}


			
			return strRet;
		}*/


        ///<summary>
        ///֧�����ص���ʱ��Ѷ�����Ϣд�����ݿ�,˵��֧���ɹ�!
        /// </summary>
        public bool CallBackNotify(Conn p_cn,string p_strSubject, string p_strOrderId, string p_strTotalFee, string p_strTradeNo, string p_strSeller, string p_strTime)
        {
            bool bRet = false;
            Conn cn = p_cn;
            try
            {
                string strSql = "update eg_AliPayOrder set vsSubject='" + p_strSubject + "',numTotalFee='" + p_strTotalFee + "',numTradeNo='" + p_strTradeNo + "',vsSeller='" + p_strSeller + "',numOrderState=1,dtOrderTime='" + p_strTime + "' where numOrderId='" + p_strOrderId + "'";
                cn.Update(strSql);
                bRet = true;
            }
            catch (Exception ex)
            {
                gs.util.func.Write("CallBackPay is err" + ex.Message);
                throw ex;
            }
            return bRet;
        }

        ///<summary>
        ///֧�����˿���Ϣд�����ݿ�,˵���˿�ɹ�!
        /// </summary>
        public bool CallBackRefund(Conn p_cn,string p_strTradeNo, string p_strTotalFee, string p_strOrderState, string p_strTime)
        {
            bool bRet = false;
            Conn cn = p_cn;
            try
            {
                string strSql = "Insert Into eg_AlipayRefund (numTradeNo,numTotalFee,numState,dtTime) values ('"+ p_strTradeNo+"','"+p_strTotalFee+"','"+p_strOrderState+"','"+p_strTime +"')";
                cn.Update(strSql);
                bRet = true;
            }
            catch (Exception ex)
            {
                gs.util.func.Write("CallBackPay is err" + ex.Message);
                throw ex;
            }
            return bRet;
        }

		
		/// <summary>
		/// ֧��ƽ̨�ص���ʱ����Ҫ�Ѷ���״̬����Ϊ��֧��,����һ����Ӧ�̳�Ʊ��ʱ������ɷ���
		/// </summary>
		/// <param name="p_strOrderId"></param>
		/// <param name="p_strJe"></param>
		/// <param name="strTxdId">YeePay������ˮ�ţ������˿�ͽⶳ</param>
		/// <param name="p_strBuyAct">֧�����Ĳɹ����˻�</param>
		/// <returns></returns>
		public string CallBackPay(string p_strOrderId,string p_strJe,string strTxdId,string p_strPayType,string p_strBuyAct)
		{
			string strRet = "fail";
			Conn cn = null;
			try
			{
				cn = new Conn();
				cn.beginTrans();
			
				string strSql = "";
				//�޸Ķ���״̬Ϊ��֧��
				strSql = "update eg_PolOrder set iTksState=1,dtPayTime=getdate(),vcPayType='��֧',vcPayId='" + strTxdId + "',vcNetPayCorp='" + p_strPayType + "',buyer_email='" + p_strBuyAct + "' where iTksState=0 and numOrderId=" + p_strOrderId;
				cn.Update(strSql);
			
				cn.commit();

				strRet = "succ";
			}
			catch(Exception ex)
			{
				cn.rollback();
				strRet = "fail";
				gs.util.func.Write("CallBackPay is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		private void SentSms(string p_strMobilePhones,string p_strPnr)
		{
			string[] aryMobile = p_strMobilePhones.Split('-');
			mysms sms = new mysms();
			for(int i=0;i<aryMobile.Length;i++)
			{
				sms.SendSms(aryMobile[i],"�����¶���,pnr:" + p_strPnr);
			}

			//sms.SendSms("13971386971","�����¶���,pnr:" + p_strPnr);
			sms.SendSms("13720367987","�����¶���,pnr:" + p_strPnr);
			sms.SendSms("15802777270","�����¶���,pnr:" + p_strPnr);
		}

		/// <summary>
		/// �����Ÿ�������Ӧ��Ҫ�����
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="p_strOrderId"></param>
		public void SentProviderOrderSms(Conn cn,string p_strOrderId)
		{
			//ȡ����Ӧ�̶���������ֻ�������
			try
			{
				string strSqlMobile = "SELECT eg_agent.vcMobilePhone,eg_PolOrder.vcPnr FROM dbo.eg_agent INNER JOIN dbo.eg_PolOrder ON " +
					" dbo.eg_agent.numAgentId = dbo.eg_PolOrder.vcProviderAgentCode where eg_PolOrder.numOrderId=" + p_strOrderId;
				DataSet dsMo = cn.GetDataSet(strSqlMobile);
				string strChkOrderMobile = dsMo.Tables[0].Rows[0]["vcMobilePhone"].ToString().Trim();
				string strPnr = dsMo.Tables[0].Rows[0]["vcPnr"].ToString().Trim();
				SentSms(strChkOrderMobile,strPnr);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("SentProviderOrderSms is err=" + ex.Message);
				throw ex;
			}

		}
		/// <summary>
		/// �����Ÿ�������Ӧ��Ҫ�����
		/// </summary>
		/// <param name="p_strOrderId"></param>
		public void SentProviderOrderSms(string p_strOrderId)
		{
			Conn cn = null;
			try
			{
				cn = new Conn();
				SentProviderOrderSms(cn,p_strOrderId);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("SentProviderOrderSms2 is err=" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}
		}

		public string getPromote(string p_strUserCode,string p_strAirs,string p_strDate,string p_strBeginCity,string p_strEndCity)
		{
			string strRet = "";
			string strLocalAgentCode = "";

			Conn cn = null;
			try
			{
				cn = new Conn();

				//���㵱ǰ�û����ڴ��������ڳ���
				string strSqlCity = "SELECT substring(dbo.eg_agent.numAgentId,1,8) as AgentId FROM dbo.eg_agent INNER JOIN dbo.eg_user ON dbo.eg_agent.numAgentId = dbo.eg_user.numAgentId where eg_user.vcLoginName='" + p_strUserCode.Trim() + "'";
				//gs.util.func.Write("strSqlCity="+strSqlCity);
				DataSet rsRet = cn.GetDataSet(strSqlCity);
				string strAgentCode = rsRet.Tables[0].Rows[0]["AgentId"].ToString().Trim();
				
				rsRet.Clear();

				strSqlCity = "SELECT * FROM eg_agent where numAgentId ='" + strAgentCode + "'";
				rsRet = cn.GetDataSet(strSqlCity);
				string strCityCode = rsRet.Tables[0].Rows[0]["vcCityCode"].ToString().Trim();	//�õ��û����ڳ�������
				string strSharePolicy = rsRet.Tables[0].Rows[0]["numSharePolicy"].ToString().Trim();	//�������Ƿ�ֻʹ���Լ������ߣ���ʹ�ù�������ƽ̨�����1ʹ�����߹���.0��ʹ��
				string strDeftRetGain = "0";
				if(rsRet.Tables[0].Rows[0]["numDeftRetGain"] != null)
				{
					strDeftRetGain = rsRet.Tables[0].Rows[0]["numDeftRetGain"].ToString().Trim();
				}
				rsRet.Clear();
				rsRet.Dispose();
				
				
				//�����û��ȼ�
				string strUserLevel = "SELECT eg_Command.numCmId,eg_Command.vcCmDesc " +
					" FROM dbo.eg_userCm INNER JOIN dbo.eg_user ON dbo.eg_userCm.numUserId = dbo.eg_user.numUserId INNER JOIN  dbo.eg_Command ON dbo.eg_userCm.numCmId = dbo.eg_Command.numCmId " +
					" where  eg_userCm.numCmId like '000100020006____' and eg_userCm.numCmId<>'000100020006' and eg_user.vcLoginName='" + p_strUserCode.Trim() + "' order by eg_Command.vcCmDesc";

				DataSet dsLevel = cn.GetDataSet(strUserLevel);
				int iLevelCount = dsLevel.Tables[0].Rows.Count;
				
				
				string strUserLevelId = "";
				if(iLevelCount > 0)
				{
					strUserLevelId = dsLevel.Tables[0].Rows[0]["numCmId"].ToString().Trim();
				}
				else
				{
					strUserLevel = "SELECT eg_Command.numCmId,eg_Command.vcCmDesc from eg_Command where numCmId like '000100020006____' and numCmId<>'000100020006' order by eg_Command.vcCmDesc desc";
					dsLevel = cn.GetDataSet(strUserLevel);
					strUserLevelId = dsLevel.Tables[0].Rows[0]["numCmId"].ToString().Trim();
				}

				dsLevel.Clear();
				dsLevel.Dispose();

				strLocalAgentCode = strAgentCode;
				NewPara np = new NewPara();
				XmlDocument doc = np.getRoot();
				XmlDocument rootDoc = doc;
				np.AddPara("cm","RetPormot");//������Ϣ˵���������������
				np.AddPara("RetGain",strDeftRetGain);//Ĭ�Ϸ�������
				XmlNode nodePromot = np.AddPara("Promots","");

				
				string strFlightSql = "";
				string[] aryAir = p_strAirs.Split(',');
				for(int iA=0;iA<aryAir.Length;iA++)
				{
					string strAir = aryAir[iA].Trim();
					strFlightSql +=  " or ','+eg_Policy.vcAirFlights+',' like '%," + strAir + ",%'";
				}

				string strSql = "SELECT dbo.eg_Policy.*, dbo.eg_PolicyGain.vcSpecSeatName AS vcSeatName,";
				strSql += "dbo.eg_PolicyGain.numRetGain AS numRetGain, ";
				strSql += "dbo.eg_PolicyGain.numGainId AS numGainId, ";
				strSql += "dbo.eg_PolicyGain.numDiscount AS numDiscount, ";
				strSql += "dbo.eg_PolicyGain.numIsPub AS numIsPub";
				strSql += " FROM dbo.eg_Policy INNER JOIN dbo.eg_PolicyGain ON dbo.eg_Policy.numPoliId = dbo.eg_PolicyGain.numPoliId INNER JOIN dbo.eg_PoliDstCitys ON dbo.eg_Policy.numPoliId = dbo.eg_PoliDstCitys.numPoliId INNER JOIN dbo.eg_PoliOrgCitys ON dbo.eg_Policy.numPoliId = dbo.eg_PoliOrgCitys.numPoliId ";
				
				strSql += " where eg_PoliOrgCitys.vcCityCode='" + p_strBeginCity + "' and eg_PoliDstCitys.vcDstCityCode='" + p_strEndCity + "' ";
				strSql += " and convert(datetime,'" + p_strDate + " 00:02:00')>eg_Policy.dtBegin and convert(datetime,'" + p_strDate + " 00:02:00')<(eg_Policy.dtEnd+convert(datetime,'23:59:59'))";
				strSql += " and (ltrim(rtrim(eg_Policy.vcAirFlights))='' " + strFlightSql + ")";
				strSql += " and eg_Policy.numIsStart=1";
				strSql += " and eg_PolicyGain.numIsPub='" + strUserLevelId + "' and eg_Policy.numAgentId like '" + strLocalAgentCode + "%'";

				strSql += " order by eg_Policy.vcAirCode,eg_PolicyGain.vcSpecSeatName,eg_PolicyGain.numDiscount desc,eg_PolicyGain.numRetGain";
				//gs.util.func.Write(strSql);
				DataSet ds = cn.GetDataSet(strSql);

				int iSize = 0;

				iSize = ds.Tables[0].Rows.Count;
				
				for(int iAir=0;iAir<aryAir.Length;iAir++)
				{
					string strAir = aryAir[iAir].ToUpper().Trim();
					string strAirCp = strAir.Substring(0,2);
					//gs.util.func.Write("strAirCp=" + strAirCp);

					int iIns = 0;  //��¼Ӧ����Ϊ���ǲ�λ���뵽�����м�¼��ţ������˷
					for(int i=0;i<iSize;i++)
					{
						/*string strCurAirCode = ds.Tables[0].Rows[i]["vcAirCode"].ToString().Trim();//��¼��ǰ���չ�˾
						string strNextAirCode = "";
						if( (i+1) < iSize )
						{
							strNextAirCode = ds.Tables[0].Rows[i+1]["vcAirCode"].ToString().Trim();
						}*/
						string strAirFlights = ds.Tables[0].Rows[i]["vcAirFlights"].ToString().ToUpper().Trim();//���������
						string strAirCorp = ds.Tables[0].Rows[i]["vcAirCode"].ToString().Trim();//���������
						//gs.util.func.Write("strAirCp = " + strAirCp + " strAirCorp=" + strAirCorp);		
						if(strAirCp == strAirCorp)
						{
							//gs.util.func.Write("fuck=" + strAirCorp);							
							string strCurDis = ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim();//��¼��ǰ��λ
							string strPrevDis = "";
							if( (i+1) < iSize )
							{
								strPrevDis = ds.Tables[0].Rows[i+1]["vcSeatName"].ToString().Trim();
							}
													

							//if(strCurDis != strPrevDis || strCurAirCode != strNextAirCode)//�����һ����¼�ͱ���¼�Ĳ�λ������ͬ�к��չ�˾������ͬ������������һ��,�������ͬ����Ϊ�ҵ��Ǳ���λ�з�����ߵĲ�λ
							 
							if(strCurDis != strPrevDis)
							{	

								if(strAirFlights != "")
								{
									string strTmpFlights = ","+strAirFlights+",";
										
									if(strTmpFlights.IndexOf(","+strAir+",") > -1)
									{//��������α�����
										//gs.util.func.Write("strTmpFlights=" + strTmpFlights + " strAir=" + strAir);
										iIns = i;
									}
									else
									{
										if(iIns == 0)	//�����Ϊ�㣬˵���Ѿ��к��ʵ������޸Ĺ�
										{
											iIns = -1;
										}
									}
								}
								else
								{
									iIns = i;
								}

								if(iIns != -1)
								{
													
									XmlNode poliNode = rootDoc.CreateNode(XmlNodeType.Element,"APROMOTION","");
									XmlNode subNode = null;

									string strPoliId = ds.Tables[0].Rows[iIns]["numPoliId"].ToString().Trim();
									string strAirRetGain = "0";//���չ�˾�ķ���
									string strPubRetGain = "0";//�����˹�������߷���
								
									subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliId","");
									subNode.AppendChild(rootDoc.CreateTextNode(strPoliId));
									poliNode.AppendChild(subNode);	//�ѱ���ֵ����ȥ

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"AirRetGain","");
									subNode.AppendChild(rootDoc.CreateTextNode(strAirRetGain));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"GainId","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["numGainId"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"Discount","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["numDiscount"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									string strRetGain = ds.Tables[0].Rows[iIns]["numRetGain"].ToString().Trim();
									subNode = rootDoc.CreateNode(XmlNodeType.Element,"RetGain","");
									subNode.AppendChild(rootDoc.CreateTextNode(strRetGain));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"SeatName","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["vcSeatName"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"AgentId","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["numAgentId"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"AgentName","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["vcAgentName"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"PubUserTitle","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["vcPubTitle"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"key","");
									subNode.AppendChild(rootDoc.CreateTextNode(strAir + "-" + ds.Tables[0].Rows[iIns]["vcSeatName"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"PubRetGain","");
									subNode.AppendChild(rootDoc.CreateTextNode(strPubRetGain));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliBegin","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["dtBegin"].ToString().Trim()));
									poliNode.AppendChild(subNode);

									subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliEnd","");
									subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[iIns]["dtEnd"].ToString().Trim()));
									poliNode.AppendChild(subNode);
																
									nodePromot.AppendChild(poliNode);

									iIns = 0;

								}

								//if(i+1 < iSize)
								//	iIns = i+1;
								
							}
							else
							{//��Ϊ�������һ�������������ˡ�
								if(strAirFlights != "")
								{
									string strTmpFlights = ","+strAirFlights+",";
										
									if(strTmpFlights.IndexOf(","+strAir+",") > -1)
									{//��������α�����
										//gs.util.func.Write("new strTmpFlights=" + strTmpFlights + " strAir=" + strAir);
										iIns = i;
									}
									//else
									//{
									//	iIns = -1;
									//}
								}
								//else
								//{
								//	iIns = i;
								//}
							}
						}
						//else
						//{
						//	if(i+1 < iSize)
						//		iIns = i+1;
						//}

						
					}
				}

				ds.Clear();
				ds.Dispose();

				ds = null;

				strRet = np.GetXML();

			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Policy.getPromote is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}

			return strRet;
		}

		/// <summary>
		/// �õ��û���������Ϣ
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <param name="p_strAirs"></param>
		/// <param name="p_strDate"></param>
		/// <param name="p_strBeginCity"></param>
		/// <param name="p_strEndCity"></param>
		/// <returns></returns>
		public string getPromoteOld(string p_strUserCode,string p_strAirs,string p_strDate,string p_strBeginCity,string p_strEndCity)
		{
			string strRet = "";
			string strLocalAgentCode = "";

			Conn cn = null;
			try
			{
				cn = new Conn();

				//���㵱ǰ�û����ڴ��������ڳ���
				string strSqlCity = "SELECT substring(dbo.eg_agent.numAgentId,1,8) as AgentId FROM dbo.eg_agent INNER JOIN dbo.eg_user ON dbo.eg_agent.numAgentId = dbo.eg_user.numAgentId where eg_user.vcLoginName='" + p_strUserCode.Trim() + "'";
				//gs.util.func.Write("strSqlCity="+strSqlCity);
				DataSet rsRet = cn.GetDataSet(strSqlCity);
				string strAgentCode = rsRet.Tables[0].Rows[0]["AgentId"].ToString().Trim();
				
				rsRet.Clear();

				strSqlCity = "SELECT * FROM eg_agent where numAgentId ='" + strAgentCode + "'";
				rsRet = cn.GetDataSet(strSqlCity);
				string strCityCode = rsRet.Tables[0].Rows[0]["vcCityCode"].ToString().Trim();	//�õ��û����ڳ�������
				string strSharePolicy = rsRet.Tables[0].Rows[0]["numSharePolicy"].ToString().Trim();	//�������Ƿ�ֻʹ���Լ������ߣ���ʹ�ù�������ƽ̨�����1ʹ�����߹���.0��ʹ��
				string strDeftRetGain = "0";
				if(rsRet.Tables[0].Rows[0]["numDeftRetGain"] != null)
				{
					strDeftRetGain = rsRet.Tables[0].Rows[0]["numDeftRetGain"].ToString().Trim();
				}
				rsRet.Clear();
				
				//gs.util.func.Write("fukkkkkkkkkk=1");
				//�����û��ȼ�
				string strUserLevel = "SELECT eg_Command.numCmId,eg_Command.vcCmDesc " +
					" FROM dbo.eg_userCm INNER JOIN dbo.eg_user ON dbo.eg_userCm.numUserId = dbo.eg_user.numUserId INNER JOIN  dbo.eg_Command ON dbo.eg_userCm.numCmId = dbo.eg_Command.numCmId " +
					" where  eg_userCm.numCmId like '000100020006____' and eg_userCm.numCmId<>'000100020006' and eg_user.vcLoginName='" + p_strUserCode.Trim() + "' order by eg_Command.vcCmDesc";
				//gs.util.func.Write("fukkkkkkkkkk=2" + strUserLevel);
				DataSet dsLevel = cn.GetDataSet(strUserLevel);
				int iLevelCount = dsLevel.Tables[0].Rows.Count;
				
				//gs.util.func.Write("fukkkkkkkkkk=3");
				string strUserLevelId = "";
				if(iLevelCount > 0)
				{//gs.util.func.Write("fukkkkkkkkkk=4");
					strUserLevelId = dsLevel.Tables[0].Rows[0]["numCmId"].ToString().Trim();
				}
				else
				{//gs.util.func.Write("fukkkkkkkkkk=5");
					strUserLevel = "SELECT eg_Command.numCmId,eg_Command.vcCmDesc from eg_Command where numCmId like '000100020006____' and numCmId<>'000100020006' order by eg_Command.vcCmDesc desc";
					dsLevel = cn.GetDataSet(strUserLevel);
					strUserLevelId = dsLevel.Tables[0].Rows[0]["numCmId"].ToString().Trim();
				}
				//gs.util.func.Write("fukkkkkkkkkk=6");
			
				//gs.util.func.Write("fukkkkkkkkkk=8");				

				strLocalAgentCode = strAgentCode;
				NewPara np = new NewPara();
				XmlDocument doc = np.getRoot();
				np.AddPara("cm","RetPormot");//������Ϣ˵���������������
				np.AddPara("RetGain",strDeftRetGain);//Ĭ�Ϸ�������
				XmlNode nodePromot = np.AddPara("Promots","");

				string[] aryAir = p_strAirs.Split(',');
				for(int i=0;i<aryAir.Length;i++)
				{
					string strAir = aryAir[i].Trim();
					//��ʹ�ù������ߣ����ѯ�Լ�����������
					//gs.util.func.Write("is exluse");
					getSelfPromotXml(cn,doc,nodePromot,strLocalAgentCode,strAir,p_strDate,p_strBeginCity,p_strEndCity,strUserLevelId);			
				}
				strRet = np.GetXML();

			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Policy.getPromote is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}

			return strRet;
		}


		/// <summary>
		/// ������֮��ѯ�Լ������ߣ���ʹ�ù������ߵ����
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="rootDoc"></param>
		/// <param name="node"></param>
		/// <param name="p_bSelf"></param>
		/// <param name="p_strAgent"></param>
		/// <param name="p_strAir"></param>
		/// <param name="p_strDate"></param>
		/// <param name="p_strBeginCity"></param>
		/// <param name="p_strEndCity"></param>
		/// <param name="p_strUserLevelId"></param>
		/// <param name="p_dbUserDecGain"></param>
		public void getSelfPromotXml(Conn cn,XmlDocument rootDoc,XmlNode node,string p_strAgent,string p_strAir,string p_strDate,string p_strBeginCity,string p_strEndCity,string p_strUserLevelId)
		{
			string strAirOrg = p_strAir.Substring(0,2);	//�õ����չ�˾������
			try
			{
				
				//�õ����в�λ(���������λ)������
				/*string strSql = "SELECT dbo.eg_Policy.*, dbo.eg_PolicyGain.vcSpecSeatName AS vcSeatName,";
				strSql += "dbo.eg_PolicyGain.numRetGain AS numRetGain, ";
				strSql += "dbo.eg_PolicyGain.numGainId AS numGainId, ";
				strSql += "dbo.eg_PolicyGain.numDiscount AS numDiscount, ";
				strSql += "dbo.eg_PolicyGain.numIsPub AS numIsPub";
				strSql += " FROM dbo.eg_Policy INNER JOIN ";
				strSql += "dbo.eg_PolicyGain ON ";
				strSql += "dbo.eg_Policy.numPoliId = dbo.eg_PolicyGain.numPoliId ";
				
				strSql += " where ','+eg_Policy.vcBeginPort like '%," + p_strBeginCity + ",%' and ','+eg_Policy.vcEndPorts like '%," + p_strEndCity + ",%' ";
				strSql += " and convert(datetime,'" + p_strDate + " 00:02:00')>eg_Policy.dtBegin and convert(datetime,'" + p_strDate + " 00:02:00')<(eg_Policy.dtEnd+convert(datetime,'23:59:59'))";
				strSql += " and (ltrim(rtrim(eg_Policy.vcAirFlights))='' or ','+eg_Policy.vcAirFlights+',' like '%," + p_strAir + ",%')";
				strSql += " and eg_Policy.vcAirCode='" + strAirOrg + "' and eg_Policy.numIsStart=1 ";
				strSql += " and eg_PolicyGain.numIsPub='" + p_strUserLevelId.Trim() + "' and eg_Policy.numAgentId like '" + p_strAgent + "%'";
				strSql += " order by eg_PolicyGain.vcSpecSeatName,eg_PolicyGain.numDiscount desc,eg_PolicyGain.numRetGain";*/

				string strSql = "SELECT dbo.eg_Policy.*, dbo.eg_PolicyGain.vcSpecSeatName AS vcSeatName,";
				strSql += "dbo.eg_PolicyGain.numRetGain AS numRetGain, ";
				strSql += "dbo.eg_PolicyGain.numGainId AS numGainId, ";
				strSql += "dbo.eg_PolicyGain.numDiscount AS numDiscount, ";
				strSql += "dbo.eg_PolicyGain.numIsPub AS numIsPub";
				strSql += " FROM dbo.eg_Policy INNER JOIN dbo.eg_PolicyGain ON dbo.eg_Policy.numPoliId = dbo.eg_PolicyGain.numPoliId INNER JOIN dbo.eg_PoliDstCitys ON dbo.eg_Policy.numPoliId = dbo.eg_PoliDstCitys.numPoliId INNER JOIN dbo.eg_PoliOrgCitys ON dbo.eg_Policy.numPoliId = dbo.eg_PoliOrgCitys.numPoliId ";
				
				strSql += " where eg_PoliOrgCitys.vcCityCode='" + p_strBeginCity + "' and eg_PoliDstCitys.vcDstCityCode='" + p_strEndCity + "' ";
				strSql += " and convert(datetime,'" + p_strDate + " 00:02:00')>eg_Policy.dtBegin and convert(datetime,'" + p_strDate + " 00:02:00')<(eg_Policy.dtEnd+convert(datetime,'23:59:59'))";
				strSql += " and (ltrim(rtrim(eg_Policy.vcAirFlights))='' or ','+eg_Policy.vcAirFlights+',' like '%," + p_strAir + ",%')";
				strSql += " and eg_Policy.vcAirCode='" + strAirOrg + "' and eg_Policy.numIsStart=1 ";
				strSql += " and eg_PolicyGain.numIsPub='" + p_strUserLevelId.Trim() + "' and eg_Policy.numAgentId like '" + p_strAgent + "%'";
				strSql += " order by eg_PolicyGain.vcSpecSeatName,eg_PolicyGain.numDiscount desc,eg_PolicyGain.numRetGain";


				DataSet ds = null;
				ds = cn.GetDataSet(strSql);
				
				//gs.util.func.Write("getPromotXml sql2" + strSql);
				int iSize = ds.Tables[0].Rows.Count;
				//string strLast = "";
				//int iMax = 0;	//�������¼�������
				for(int i=0;i<iSize;i++)
				{
					string strCurDis = ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim();//��¼��ǰ��λ

					string strPrevDis = "";
					if( (i+1) < iSize )
					{
						strPrevDis = ds.Tables[0].Rows[i+1]["vcSeatName"].ToString().Trim();
					}

					if(strCurDis != strPrevDis)//�����һ����¼�ͱ���¼�Ĳ�λ������ͬ������������һ��,�������ͬ����Ϊ�ҵ��Ǳ���λ�з�����ߵĲ�λ
					{
						XmlNode poliNode = rootDoc.CreateNode(XmlNodeType.Element,"APROMOTION","");

						XmlNode subNode = null;

						string strPoliId = ds.Tables[0].Rows[i]["numPoliId"].ToString().Trim();
						//string strTmpDis = ds.Tables[0].Rows[i]["vcSpecSeatName"].ToString().Trim();

						
						DataSet dsTmpGain = cn.GetDataSet("select numRetGain,numIsPub from eg_PolicyGain where numPoliId=" + strPoliId + " and vcSpecSeatName='" + strCurDis + "' and (numIsPub='1' or numIsPub='0')");
						string strAirRetGain = "0";//���չ�˾�ķ���
						string strPubRetGain = "0";//�����˹�������߷���
						int iTmpGainCount = dsTmpGain.Tables[0].Rows.Count;
						if(iTmpGainCount>0)
						{
							for(int iGain=0;iGain<iTmpGainCount;iGain++)
							{
								string strTmpGain = dsTmpGain.Tables[0].Rows[iGain]["numRetGain"].ToString().Trim();
								string strTmpIsPub = dsTmpGain.Tables[0].Rows[iGain]["numIsPub"].ToString().Trim();
								if(strTmpIsPub == "1")
								{
									strAirRetGain = strTmpGain;//�õ����չ�˾�ķ���
								}

								if(strTmpIsPub == "0")
								{
									strPubRetGain = strTmpGain;//�õ������˹�������߷���
								}

							}
						}

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliId","");
						subNode.AppendChild(rootDoc.CreateTextNode(strPoliId));
						poliNode.AppendChild(subNode);	//�ѱ���ֵ����ȥ

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"AirRetGain","");
						subNode.AppendChild(rootDoc.CreateTextNode(strAirRetGain));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"GainId","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["numGainId"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"Discount","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["numDiscount"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						string strRetGain = ds.Tables[0].Rows[i]["numRetGain"].ToString().Trim();
						
						subNode = rootDoc.CreateNode(XmlNodeType.Element,"RetGain","");
						subNode.AppendChild(rootDoc.CreateTextNode(strRetGain));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"SeatName","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"AgentId","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["numAgentId"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"AgentName","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["vcAgentName"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PubUserTitle","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["vcPubTitle"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"key","");
						subNode.AppendChild(rootDoc.CreateTextNode(p_strAir + "-" + ds.Tables[0].Rows[i]["vcSeatName"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PubRetGain","");
						subNode.AppendChild(rootDoc.CreateTextNode(strPubRetGain));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliBegin","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["dtBegin"].ToString().Trim()));
						poliNode.AppendChild(subNode);

						subNode = rootDoc.CreateNode(XmlNodeType.Element,"PoliEnd","");
						subNode.AppendChild(rootDoc.CreateTextNode(ds.Tables[0].Rows[i]["dtEnd"].ToString().Trim()));
						poliNode.AppendChild(subNode);
						
						
						node.AppendChild(poliNode);
					}
					
				}

			}
			catch(Exception ex)
			{
				gs.util.func.Write("getPromotXml is err" + ex.Message);
				throw ex;
			}
		}
		
		/// <summary>
		/// ת������ΪIBE��ʶ�������
		/// </summary>
		/// <param name="p_strDate"></param>
		/// <returns></returns>
		public static string getIBEDate(string p_strDate)
		{
			string strRet = "";
			string[] ary = p_strDate.Split('-');

			string strMonth = ary[1].Trim();
			if(strMonth.Length<2)
			{
				strMonth = "0" + strMonth;
			}

			string strDay = ary[2].Trim();
			if(strDay.Length<2)
			{
				strDay = "0" + strDay;
			}
			strRet = ary[0] + strMonth + strDay;
			return strRet;
		}

		/// <summary>
		/// �õ�����1977-01-09���������ڸ�ʽ
		/// </summary>
		/// <param name="p_strDate"></param>
		/// <returns></returns>
		public static string getIBEDate2(string p_strDate)
		{
			string strRet = "";
			string[] ary = p_strDate.Split('-');

			string strMonth = ary[1].Trim();
			if(strMonth.Length<2)
			{
				strMonth = "0" + strMonth;
			}

			string strDay = ary[2].Trim();
			if(strDay.Length<2)
			{
				strDay = "0" + strDay;
			}
			strRet = ary[0] + "-" + strMonth + "-" + strDay;
			return strRet;
		}

		/// <summary>
		/// ��ʼһ�β�ѯ
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <param name="p_strAir"></param>
		/// <param name="p_strDate"></param>
		/// <param name="p_strBeginCity"></param>
		/// <param name="p_strEndCity"></param>
		/// <returns></returns>
		public string getAv(string p_strUserCode,string p_strAirCorp,string p_strDate,string p_strTime,string p_strBeginCity,string p_strEndCity)
		{
			string strRet = "";
			string strErr = "";
			string strUserType = "";
			string strCurAgentId = "";

			XmlNode m_nodeFlight = null;
			Hashtable m_ht = null;

			EgUser eu = new EgUser();
			DataSet dsEu = eu.getUserInfoByUserName(p_strUserCode);
			if(dsEu.Tables[0].Rows.Count > 0)
			{
				strUserType = dsEu.Tables[0].Rows[0]["vcUserType"].ToString().Trim();
				strCurAgentId = dsEu.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
			}

			
			///////
			//������û����ڴ������Ƿ�ʹ�ù�������
			string strRootAgent = strCurAgentId;
			if(strRootAgent.Length > 8)
			{
				strRootAgent = strRootAgent.Substring(0,8);
			}
			//Response.Write("strRootAgent=" + strRootAgent);
			DataSet dsShare = logic.mytool.util.getDs("select * from eg_agent where numAgentId='" + strRootAgent + "'");
			string strShare = dsShare.Tables[0].Rows[0]["numSharePolicy"].ToString().Trim();

			string strDeftAirRetGain = dsShare.Tables[0].Rows[0]["numDeftAirRetGain"].ToString().Trim();
			string strDeftRetGain = dsShare.Tables[0].Rows[0]["numDeftRetGain"].ToString().Trim();
			string strDeftPubUserTitle = dsShare.Tables[0].Rows[0]["vcDeftPubUserTitle"].ToString().Trim();
			string strDeftAgentName = dsShare.Tables[0].Rows[0]["vcAgentName"].ToString().Trim();
			string strDeftAgentId = strRootAgent;
		
			string strFuleJe = "100";
			DataSet dsDistance = logic.mytool.util.getDs("select * from eg_ticketInfo where BFROM='" + p_strBeginCity + "' and ETO='" + p_strEndCity + "'" );
			if(dsDistance.Tables[0].Rows.Count > 0)
			{
				if(Double.Parse( dsDistance.Tables[0].Rows[0]["BUNKF"].ToString() ) < 800)
				{//�������С��800���ȼ�͸����Ѿ���60��
					strFuleJe = "60";
				}
			}
			

			//����û�з������ߵ�Ĭ������
			PolicyInfo m_piDefault = new PolicyInfo();

		
			m_piDefault.strPoliId = "0";	//���߱��
			m_piDefault.strAirRetGain = strDeftAirRetGain ;//���չ�˾����
			m_piDefault.strGainId = "0";
			m_piDefault.strRetGain = strDeftRetGain;
			m_piDefault.strAgentId = strDeftAgentId;	//�����ṩ�̵�ID
			m_piDefault.strAgentName = strDeftAgentName;
			m_piDefault.strPubUserTitle = strDeftPubUserTitle;
			m_piDefault.strPubGain = strDeftRetGain;	//���߹���ķ���
			m_piDefault.strPoliBegin = "2000-1-1";	//���߿�ʼʱ��
			m_piDefault.strPoliEnd = "2050-1-1";	//���߽���ʱ��
		

			//��ʼ��ѯ����
			
			string m_strBeginPort = p_strBeginCity.Trim();
			string m_strEndPort = p_strEndCity.Trim();
			EgCity ec = new EgCity();
			Hashtable htCity = ec.getCitysHash();
			string m_strBeginPortName = ec.getCityName(htCity,m_strBeginPort);
			string m_strEndPortName = ec.getCityName(htCity,m_strEndPort);

			Policy ep = new Policy();

			IBECOMService ibe = new IBECOMService();
			string strAv = "";
			
			string strBeginCity = p_strBeginCity;
			string strEndCity = p_strEndCity;
			string strQryDate = p_strDate;
			string strQryTime = Policy.getIBEDate(strQryDate) + " " + p_strTime + ":00";
			//Response.Write("QryTime=" + strQryTime);
			gs.util.func.Write("��ʼIBE��ѯ" + System.DateTime.Now.ToLongTimeString());
			try
			{
				strAv = ibe.getAV(strBeginCity,strEndCity,strQryTime,p_strAirCorp.Trim());
				//gs.util.func.Write("the xml=" + strAv);
				gs.util.func.Write("����IBE��ѯ���õ����ؽ��" + System.DateTime.Now.ToLongTimeString());
			}
			catch(Exception ex)
			{
				gs.util.func.Write("IBE��ѯ��ʱ" + ex.Message);
			}
			//gs.util.func.Write(strAv);
			//TextBox1.Text = strAv;
			//Response.End();

			XmlNode nodeFlight = null;
			if(strAv == "")
			{
				strErr = "��ѯʧ��!";
				
			}
			else
			{
			
				NewPara np = null;
				
				try
				{
					np = new NewPara(strAv);
					nodeFlight = np.FindNodeByPath("//eg/AFlight");
					if(nodeFlight.ChildNodes.Count == 0)
					{
						strErr = "noflight";
					}
				}
				catch(Exception ex)
				{
					gs.util.func.Write("IBEû�к���" + ex.Message);
					strErr = "noflight";
				}
			}

			if(strErr == "")
			{
				m_nodeFlight = nodeFlight;
			
				try
				{
					//�õ�������Ϣbegin
					string strAirs = "";
					for(int i=0;i<nodeFlight.ChildNodes.Count;i++)
					{//�Ȱ�Ҫ��ѯ�ĺ��������
						XmlNode nodeAir = nodeFlight.ChildNodes[i];
						strAirs += nodeAir.ChildNodes[0].ChildNodes[0].Value + ",";
					}

					if(strAirs != "")
					{
						strAirs = strAirs.Substring(0,strAirs.Length-1);
					}
					else
					{
						strErr = "�޺�������";
					}
				
			
					if(strErr == "")
					{
						///�õ�����
						//WS.egws ws = new WS.egws();
						gs.util.func.Write("��ʼ��ѯ����" + System.DateTime.Now.ToLongTimeString());
						//string strPolicy = ws.getEgSoap(strSent);
						string strPolicy = ep.getPromote(p_strUserCode.Trim(),strAirs,strQryDate,strBeginCity,strEndCity);
						gs.util.func.Write("������ѯ����" + System.DateTime.Now.ToLongTimeString());
						//TextBox1.Text = strPolicy;
						//gs.util.func.Write("strPolicy="+strPolicy);
						//TextBox1.Text = TextBox1.Text + "fuk" + strPolicy;
				
						///�������
						NewPara npPoly = new NewPara(strPolicy);
						XmlNode nodeProms = npPoly.FindNodeByPath("//eg/Promots");

						Hashtable ht = new Hashtable();

						for(int i=0;i<nodeProms.ChildNodes.Count;i++)
						{
							XmlNode nodePolicy = nodeProms.ChildNodes[i];

							string strKey = nodePolicy.ChildNodes[9].ChildNodes[0].Value.ToString().Trim();
							PolicyInfo pi = new PolicyInfo();
							pi.strPoliId = nodePolicy.ChildNodes[0].ChildNodes[0].Value.ToString().Trim();
							pi.strAirRetGain = nodePolicy.ChildNodes[1].ChildNodes[0].Value.ToString().Trim();
							pi.strGainId = nodePolicy.ChildNodes[2].ChildNodes[0].Value.ToString().Trim();
							pi.strDiscount = nodePolicy.ChildNodes[3].ChildNodes[0].Value.ToString().Trim();
							pi.strRetGain = nodePolicy.ChildNodes[4].ChildNodes[0].Value.ToString().Trim();;
							pi.strSeatName = nodePolicy.ChildNodes[5].ChildNodes[0].Value.ToString().Trim();
							pi.strAgentId = nodePolicy.ChildNodes[6].ChildNodes[0].Value.ToString().Trim();
							pi.strAgentName = nodePolicy.ChildNodes[7].ChildNodes[0].Value.ToString().Trim();
							pi.strPubUserTitle = nodePolicy.ChildNodes[8].ChildNodes[0].Value.ToString().Trim();
							pi.strPubGain = nodePolicy.ChildNodes[10].ChildNodes[0].Value.ToString().Trim();
							pi.strPoliBegin = nodePolicy.ChildNodes[11].ChildNodes[0].Value.ToString().Trim();
							pi.strPoliEnd = nodePolicy.ChildNodes[12].ChildNodes[0].Value.ToString().Trim();

							if(!ht.ContainsKey(strKey))
							{
								ht.Add(strKey,pi);
							}

							//Response.Write("PoliId =" + nodePolicy.ChildNodes[0].ChildNodes[0].Value + " AirRetGain =" + nodePolicy.ChildNodes[1].ChildNodes[0].Value + " PubUserTitle=" + pi.strPubUserTitle + "<br>");
						}
						m_ht = ht;
						///�õ�������Ϣend
						///
						//gs.util.func.Write("���߽������" + System.DateTime.Now.ToLongTimeString());

						//����ٲֵļ۸�
						double dbYCabinPrice = Double.Parse(ep.getYCabinPrice(strBeginCity,strEndCity,strQryTime));
						//gs.util.func.Write("Y Price=" + dbYCabinPrice.ToString());
						//�޲���ȫ�ļ۸���Ϣ
						EgAir eaa = new EgAir();
						Hashtable htDis = eaa.getCabinDiscount();//�õ���λ���ձ�

						for(int i=0;i<nodeFlight.ChildNodes.Count;i++)
						{
							XmlNode nodeAir = nodeFlight.ChildNodes[i];
							string strAirName = nodeAir.ChildNodes[0].ChildNodes[0].Value.ToString().Trim();
							string strAirCorpName = strAirName.Substring(0,2);
							//Response.Write("airname =" + strAirName + " begin=" + nodeAir.ChildNodes[1].ChildNodes[0].Value + " end=" + nodeAir.ChildNodes[2].ChildNodes[0].Value + " airstyle=" + nodeAir.ChildNodes[3].ChildNodes[0].Value + "<br>");
							XmlNode nodeSeats = nodeAir.ChildNodes[4];

							for(int j=0;j<nodeSeats.ChildNodes.Count;j++)
							{
								XmlNode nodeSeat = nodeSeats.ChildNodes[j];
								string strDiscount = nodeSeat.ChildNodes[2].ChildNodes[0].Value.ToString().Trim();
								string strSeatName = nodeSeat.ChildNodes[0].ChildNodes[0].Value.ToString().Trim();
											
								if(strDiscount == "~")
								{//�ж��Ƿ���û�������۸�Ĳ�λ,�Ѳ�λ�۸���
									//double dbTkPrc = Double.Parse(nodeSeat.ChildNodes[1].ChildNodes[0].Value.ToString());
									double dbTkPrc = dbYCabinPrice;
									string strDisKey = strAirCorpName + "-" + strSeatName;
									string strNewDiscount = "100";
									if(htDis.ContainsKey(strDisKey))
									{//����ͨ��λ���ձ��Ҹò�λ���ۿ���Ϣ
										strNewDiscount = htDis[strDisKey].ToString();
									}
									else
									{
										if(ht.ContainsKey(strAirName + "-" + strSeatName))
										{//�������ͨ��λ���ձ�����û�ҵ��ͳ����������λ���ձ�����һ�Ѹò�λ���ۿ���Ϣ
											PolicyInfo pi = (PolicyInfo)ht[strAirName + "-" + strSeatName];
											strNewDiscount = pi.strDiscount;
											//gs.util.func.Write("the strNewDiscount=" + strNewDiscount);
										}
										else
										{//�����Ȼû�в鵽�ۿ���Ϣ����Ѳ�λ��־�ɲ�������
											strNewDiscount = "notk";
										}
									}

									if(strNewDiscount != "notk")
									{
										double dbDis = Double.Parse(strNewDiscount);
										dbTkPrc = dbTkPrc*dbDis/100;
										int iTkPrc = (int)dbTkPrc;
										iTkPrc = ((iTkPrc + 5)/10)*10;
										nodeSeat.ChildNodes[1].ChildNodes[0].Value = iTkPrc.ToString(); //�����µ�Ʊ���
										nodeSeat.ChildNodes[2].ChildNodes[0].Value = strNewDiscount+".0";	//�����µ��ۿ�
										//nodeSeat.ChildNodes[3].ChildNodes[0].Value = strSeatName;	//�����µĲ�λ
									}
									else
									{
										nodeSeat.ChildNodes[1].ChildNodes[0].Value = "0"; //�����µ�Ʊ���
										nodeSeat.ChildNodes[2].ChildNodes[0].Value = "-1";	//�����µ��ۿ�
									}
								}
							}

						}
					}

					//gs.util.func.Write("���۸����" + System.DateTime.Now.ToLongTimeString());
				}
				catch(Exception ex)
				{
					gs.util.func.Write("��ѯʧ��,��ѯ����ʧ��" + ex.Message);
					strErr = "��ѯʧ��,��ѯ����ʧ��";

				}
			}

			if(strErr != "noflight")
			{
				if(m_nodeFlight != null && strErr == "")
				{
				
					for(int i=0;i<m_nodeFlight.ChildNodes.Count;i++)
					{
						System.Xml.XmlNode nodeAir = m_nodeFlight.ChildNodes[i];
						string strAirName = nodeAir.ChildNodes[0].ChildNodes[0].Value.ToString().Trim();
						string strAirType = nodeAir.ChildNodes[3].ChildNodes[0].Value.ToString().Trim();
						string strBeginTime = nodeAir.ChildNodes[1].ChildNodes[0].Value.ToString().Trim();
						string strEndTime = nodeAir.ChildNodes[2].ChildNodes[0].Value.ToString().Trim();
						//Response.Write("airname =" + strAirName + " begin=" + nodeAir.ChildNodes[1].ChildNodes[0].Value + " end=" + nodeAir.ChildNodes[2].ChildNodes[0].Value + " airstyle=" + nodeAir.ChildNodes[3].ChildNodes[0].Value + "<br>");
				
						//���Ի����չ�˾����HASH
						EgAir ea = new EgAir();
						string strAirCorpName = "";
						Hashtable m_htAirs = ea.getAirNamesHash();
						if(m_htAirs.ContainsKey(strAirName.Substring(0,2)))
						{
							strAirCorpName = m_htAirs[strAirName.Substring(0,2)].ToString().Trim();
						}

						strRet += strAirCorpName + "~" + strAirName + "~" + strAirType + "~" + strBeginTime + "~" + strEndTime + "$";
				
						System.Xml.XmlNode nodeSeats = nodeAir.ChildNodes[4];
						int iCount = nodeSeats.ChildNodes.Count;
				
						//����ʵ�ռ۰Ѳ�λ����BEGIN
						System.Collections.ArrayList al = new System.Collections.ArrayList();

						FlightRow fw = null;
						for(int j=0;j<iCount;j++)
						{
							fw = new FlightRow();
					
							System.Xml.XmlNode nodeSeat = nodeSeats.ChildNodes[j];
							string strDiscount = nodeSeat.ChildNodes[2].ChildNodes[0].Value.ToString().Trim();
							//gs.util.func.Write("fuck strDiscount=" + strDiscount);
							string strSeatName = nodeSeat.ChildNodes[0].ChildNodes[0].Value;
							fw.m_strSeatName = strSeatName;
							strDiscount = strDiscount.Substring(0,strDiscount.Length-2);
							fw.m_strDiscount = strDiscount;
					
							string strRetGain = "0";
							string strKey = strAirName + "-" + strSeatName;
							string strPoliId = "";
							if(m_ht.ContainsKey(strKey))
							{
								logic.PolicyInfo pi = (logic.PolicyInfo)(m_ht[strKey]);
								fw.m_pi = pi;
								strRetGain = pi.strRetGain;
								strPoliId = pi.strPoliId;
							}
							else
							{//���û���˷�����Ӧ�����ߣ�Ŀǰʹ�����������
						
								fw.m_pi = m_piDefault;
								strRetGain = m_piDefault.strRetGain;
								strPoliId = m_piDefault.strPoliId;
							}
					
							double dbRetGain = Double.Parse(strRetGain);
							fw.m_dbRetGain = dbRetGain;
							double dbEtkPrc = Double.Parse(nodeSeat.ChildNodes[1].ChildNodes[0].Value.ToString());
							fw.m_dbEtkPrc = dbEtkPrc;
							double dbAgentEtkPrc = dbEtkPrc*(100-dbRetGain)/100;
							fw.m_dbAgentEtkPrc = dbAgentEtkPrc;
					
							string strCabin = nodeSeat.ChildNodes[4].ChildNodes[0].Value.ToString().Trim();
							fw.m_strCabin = strCabin;
					
							fw.m_strFule = nodeSeat.ChildNodes[5].ChildNodes[0].Value.ToString().Trim();
							fw.m_strBaseFee = nodeSeat.ChildNodes[6].ChildNodes[0].Value.ToString().Trim();
					
							al.Add(fw);
						}
				
						FlightRow fw2 = new FlightRow();
						ReverserClass reverser = new ReverserClass(fw2.GetType(),"m_dbAgentEtkPrc","ASC");
						al.Sort(reverser);
						//����ʵ�ռ۰Ѳ�λ����END
				
						//�������Ĳ�λչʾ
						foreach(FlightRow item in al)
						{
							//if(item.m_pi != null) 
							//gs.util.func.Write("fuck item.m_strDiscount=" + item.m_strDiscount);
							if(item.m_strDiscount != "") 
							{

								string strCabin = item.m_strCabin;
								String strCabinTitle = strCabin;
								if(strCabin == "A")
								{
									strCabinTitle = ">9";
								}
							
								strRet += item.m_dbAgentEtkPrc + "@"+ item.m_dbEtkPrc + "@"+ item.m_strSeatName + "@"+ item.m_strDiscount + "@"+ item.m_dbRetGain + "@"+ strCabinTitle + "@"+ item.m_pi.strPoliBegin + "@"+ item.m_pi.strPoliEnd + "@"+ item.m_pi.strAgentId + "@"+ item.m_pi.strAgentName + "@"+ strBeginTime + "@"+ strEndTime + "@"+ strCabin + "@"+ strAirType + "@"+ item.m_pi.strPoliId + "@"+ item.m_pi.strAirRetGain + "@"+ strFuleJe + "@"+ item.m_strBaseFee + "@"+ item.m_pi.strPubGain + "@"+ m_strBeginPortName + "@"+ m_strEndPortName + "@"+ m_strBeginPort + "@"+ m_strEndPort + "&"; 

							}
						}
						strRet = strRet.Substring(0,strRet.Length-1);
						strRet += "*";
					}
					strRet = strRet.Substring(0,strRet.Length-1);

				}
				else
				{
					strRet = "avfail";
				}
			}
			else
			{
				strRet = "noflight";
			}
			return strRet;
		}

		private static Hashtable m_htCabinPrice = null;
		public string getYCabinPrice(string p_strBeginPort,string p_strEndPort,string p_strTime)
		{//gs.util.func.Write("p_strBeginPort" + p_strBeginPort + "p_strEndPort=" + p_strEndPort + "p_strTime=" + p_strTime);
			string strRet = "0";
			string strCityKey = p_strBeginPort.Trim() + "-" + p_strEndPort.Trim();

			if(m_htCabinPrice == null)
			{
				m_htCabinPrice = new Hashtable();

				DataSet ds = logic.mytool.util.getDs("select * from eg_ticketInfo");
				int iCount = ds.Tables[0].Rows.Count;
				for(int i=0;i<iCount;i++)
				{
					CabinPriceInfo cpi = new CabinPriceInfo();
					cpi.strBeginEndKey = ds.Tables[0].Rows[i]["BFROM"].ToString().Trim() + "-" + ds.Tables[0].Rows[i]["ETO"].ToString().Trim();
					cpi.strPrice = ds.Tables[0].Rows[i]["BUNKY"].ToString().Trim();
					//gs.util.func.Write("cpi.strBeginEndKey=" + cpi.strBeginEndKey + " cpi.strPrice=" + cpi.strPrice);
					m_htCabinPrice.Add(cpi.strBeginEndKey,cpi);
				}
				gs.util.func.Write("���̱�װ���ڴ�ɹ�");
			}

			
			if(m_htCabinPrice.ContainsKey(strCityKey))
			{//����ں��̱����ҵ����жΣ���ֻ��Ҫ�����ݿ����ҵ�Y�ּ۸�
				strRet = ((CabinPriceInfo)m_htCabinPrice[strCityKey]).strPrice;
				//gs.util.func.Write(strCityKey+"�ļ۸������ݿ��ﱻ�е�=" + strRet);
			}
			else
			{
				
				//ͨ��IBEȥѰ�Ҽ۸�,Ȼ��׷��
				IBECOMService ib = new IBECOMService();
				strRet = ib.getFDWS(p_strBeginPort,p_strEndPort,p_strTime);
				//gs.util.func.Write("strRet=" + strRet);
				CabinPriceInfo cpi2 = new CabinPriceInfo();
				cpi2.strBeginEndKey = strCityKey;
				cpi2.strPrice = strRet;
				m_htCabinPrice.Add(strCityKey,cpi2);

			}
		
			//gs.util.func.Write("getYCabinPrice=" + strRet);
			return strRet;
		}

	}


}
