using System;
using System.Data;
using System.Xml;
using gs.para;
using gs.DataBase;


namespace logic
{
	/// <summary>
	/// Order 的摘要说明。
	/// </summary>
	public class Order
	{
		public Order()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataSet getTekInfo(string p_strPnr)
		{
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "";
				strSql = "select * from eg_eticket where Pnr='" + p_strPnr + "' and datediff(day,OperateTime,getdate())>=-20 and  datediff(day,OperateTime,getdate())<=20";
				rsRet = cn.GetDataSet(strSql);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Order.getTekInfo is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return rsRet;
		}

		public string SubmitOrder(Conn cn,string p_strXML)
		{//gs.util.func.Write(p_strXML);
			NewPara npRet = new NewPara(p_strXML.Trim());
			string strCmm = npRet.FindTextByPath("//eg/cm").Trim();
			
			string strErr = "";		
			string strOrderId = "";
			string strPnr = "";
			if(strCmm == "SubmitWebOrder")
			{
				strPnr = npRet.FindTextByPath("//eg/pnr").Trim();

				if(strPnr != null && strPnr != "" && strPnr != "fail")
				{
					string strCurUserName = npRet.FindTextByPath("//eg/CurUserName").Trim();
							
					try
					{
						

						strOrderId = logic.mytool.util.getId(cn,"eg_PolOrder");
						string strCurAgentId = "";
						string strSql =	"select dbo.getAgentName(eg_user.numAgentId) as agentdepthname,eg_user.*,eg_agent.vcAgentName from dbo.eg_agent INNER JOIN dbo.eg_user ON dbo.eg_agent.numAgentId = dbo.eg_user.numAgentId where eg_user.vcLoginName='" + strCurUserName + "'";
					
						DataSet ds = cn.GetDataSet(strSql);

						string strAgentDepthName = "";
						string strCurUserTitle = "";
						if(ds.Tables[0].Rows.Count > 0)
						{
							strAgentDepthName = ds.Tables[0].Rows[0]["agentdepthname"].ToString().Trim();
							strCurAgentId = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
							strCurUserTitle =  ds.Tables[0].Rows[0]["vcUserTitle"].ToString().Trim();
						}

						Top2 tp = new Top2(cn);                                                                  
			
						//gs.util.func.Write("txtTotal.Text.Trim()=" + txtTotal.Text.Trim());
						tp.AddRow("numOrderId","i",strOrderId);
						tp.AddRow("vcBeginPort","c",npRet.FindTextByPath("//eg/BeginPort").ToString().Trim());
						tp.AddRow("vcEndPort","c",npRet.FindTextByPath("//eg/EndPort").ToString().Trim());
						tp.AddRow("vcBeginPortName","c",npRet.FindTextByPath("//eg/BeginPortName").ToString().Trim());
						tp.AddRow("vcEndPortName","c",npRet.FindTextByPath("//eg/EndPortName").ToString().Trim());
						tp.AddRow("vcAirCode","c",npRet.FindTextByPath("//eg/AirName").ToString().Trim().Substring(0,2));//航空公司代码
						tp.AddRow("vcFlightNo","c",npRet.FindTextByPath("//eg/FlightNo").ToString().Trim());//航班号
						tp.AddRow("vcStartTime","c",npRet.FindTextByPath("//eg/DepaTime").ToString().Trim());
						tp.AddRow("vcLandTime","c",npRet.FindTextByPath("//eg/LandTime").ToString().Trim());
						tp.AddRow("vcFlightType","c",npRet.FindTextByPath("//eg/AirType").ToString().Trim());
						tp.AddRow("vcRetTkRule","c","");
						tp.AddRow("vcSeatName","c",npRet.FindTextByPath("//eg/SeatName").ToString().Trim());
						tp.AddRow("numTkPrc","d",npRet.FindTextByPath("//eg/EtkPrc").ToString().Trim());
						tp.AddRow("numRealPrc","d",npRet.FindTextByPath("//eg/RealPrc").ToString().Trim());
						tp.AddRow("numBase","d",npRet.FindTextByPath("//eg/BaseFee").ToString().Trim());
						tp.AddRow("numFuel","d",npRet.FindTextByPath("//eg/Fule").ToString().Trim());
						tp.AddRow("numPersCount","i",npRet.FindTextByPath("//eg/PerCounts").ToString().Trim());
						tp.AddRow("numRetGain","d",npRet.FindTextByPath("//eg/RetGain").ToString().Trim());
						tp.AddRow("numAirGain","d",npRet.FindTextByPath("//eg/AirRetGain").ToString().Trim());
						tp.AddRow("numPubGain","d",npRet.FindTextByPath("//eg/PubGain").ToString().Trim());
						tp.AddRow("numPoliId","i",npRet.FindTextByPath("//eg/PoliId").ToString().Trim());
						tp.AddRow("numTotal","d",npRet.FindTextByPath("//eg/Total").ToString().Trim());
						tp.AddRow("vcSaleCode","c",strCurUserName);
						tp.AddRow("vcSaleUserName","c",strCurUserTitle);
						tp.AddRow("vcSaleAgentName","c",strAgentDepthName);
						tp.AddRow("vcSaleAgentCode","c",strCurAgentId);
						tp.AddRow("dtPoliBegin","c",npRet.FindTextByPath("//eg/PoliBegin").ToString().Trim());
						tp.AddRow("dtPoliEnd","c",npRet.FindTextByPath("//eg/PoliEnd").ToString().Trim());
						tp.AddRow("vcProviderAgentName","c",npRet.FindTextByPath("//eg/ProviderAgentName").ToString().Trim());
						tp.AddRow("vcProviderAgentCode","c",npRet.FindTextByPath("//eg/ProviderAgentId").ToString().Trim());
						tp.AddRow("vcSaleContacter","c",npRet.FindTextByPath("//eg/vcSaleContacter").ToString().Trim());
						tp.AddRow("vcSalMobile","c",npRet.FindTextByPath("//eg/vcSalMobile").ToString().Trim());
						tp.AddRow("vcSalTel","c",npRet.FindTextByPath("//eg/vcSalTel").ToString().Trim());
						tp.AddRow("vcSalQq","c",npRet.FindTextByPath("//eg/vcSalQq").ToString().Trim());
						tp.AddRow("vcAddress","c",npRet.FindTextByPath("//eg/vcAddress").ToString().Trim());
						tp.AddRow("vcAirCorpName","c",npRet.FindTextByPath("//eg/AirCorpName").ToString().Trim());
						tp.AddRow("vcDiscount","c",npRet.FindTextByPath("//eg/Discount").ToString().Trim());
						tp.AddRow("dtFlyDate","c",npRet.FindTextByPath("//eg/FlyDate").ToString().Trim());
						tp.AddRow("dtOrderTime","t","getdate()");
						tp.AddRow("iTksState","i","0");
						tp.AddRow("vcPnr","c",strPnr);


						string strProviderAgentCode = npRet.FindTextByPath("//eg/ProviderAgentId").ToString().Trim();
						string strSaleAgentCode = strCurAgentId;
						string strSaleAgentName = strAgentDepthName;
						string strProviderAgentName = npRet.FindTextByPath("//eg/ProviderAgentName").ToString().Trim();
						double dbRetGain = Double.Parse(npRet.FindTextByPath("//eg/RetGain").ToString().Trim());
						double dbPubGain = Double.Parse(npRet.FindTextByPath("//eg/PubGain").ToString().Trim());
						double dbAirGain = Double.Parse(npRet.FindTextByPath("//eg/AirRetGain").ToString().Trim());
						double dbRealTkPrc = Double.Parse(npRet.FindTextByPath("//eg/RealPrc").ToString().Trim());//实收单价
						double dbTkPrc = Double.Parse(npRet.FindTextByPath("//eg/EtkPrc").ToString().Trim());//票面单价
						double dbFule = Double.Parse(npRet.FindTextByPath("//eg/Fule").ToString().Trim());
						double dbBaseFee = Double.Parse(npRet.FindTextByPath("//eg/BaseFee").ToString().Trim());
						int iPerCount = Int32.Parse(npRet.FindTextByPath("//eg/PerCounts").ToString().Trim());

						//Response.Write("strProviderAgentCode=" + strProviderAgentCode + "---" + "strSaleAgentCode=" + strSaleAgentCode);
						if(strProviderAgentCode.Substring(0,8) == strSaleAgentCode.Substring(0,8))
						{//如果销售商定的是自己的票
							double dbSalUserGain = dbTkPrc * dbRetGain /100*iPerCount;//得到用户利润
							double dbAgentGain = dbTkPrc * (dbAirGain-dbRetGain) /100*iPerCount;//得到代理人的利润
							//gs.util.func.Write("numSalAgentGainJe=" + dbAgentGain.ToString().Trim());
							double dbAgentPrc = dbTkPrc*(100-dbAirGain)/100;//得到代理人每张票的单价
							double dbAgentTotal = (dbAgentPrc+dbBaseFee+dbFule)*iPerCount;//得到总价
							tp.AddRow("numSalUserGainJe","d",dbSalUserGain.ToString().Trim());
							tp.AddRow("numSalAgentGainJe","d",dbAgentGain.ToString().Trim());
							tp.AddRow("numAgentPrc","d",dbAgentPrc.ToString().Trim());
							tp.AddRow("numAgentTotal","d",dbAgentTotal.ToString().Trim());

							tp.AddRow("numIsLocalOrder","i","1");
						}
						else
						{//如果定的是别的供应商的票
							double dbSalUserGain = dbTkPrc * dbRetGain /100*iPerCount;//得到用户利润
							double dbAgentGain = dbTkPrc * (dbPubGain-dbRetGain) /100*iPerCount;//得到代理人的利润
							double dbAgentPrc = dbTkPrc*(100-dbPubGain)/100;//得到代理人每张票的单价
							double dbAgentTotal = (dbAgentPrc+dbBaseFee+dbFule)*iPerCount;//得到总价

							double dbProviderPrc = dbTkPrc*(100-dbAirGain)/100 ;//得到供应商单价
							double dbProviderTotal = (dbProviderPrc+dbBaseFee+dbFule)*iPerCount;//得到供应商总价
							double dbProviderGainJe = dbTkPrc*(dbAirGain-dbPubGain)*iPerCount/100;//得到供应商利润


							tp.AddRow("numSalUserGainJe","d",dbSalUserGain.ToString().Trim());
							tp.AddRow("numSalAgentGainJe","d",dbAgentGain.ToString().Trim());
							tp.AddRow("numAgentPrc","d",dbAgentPrc.ToString().Trim());
							tp.AddRow("numAgentTotal","d",dbAgentTotal.ToString().Trim());
							tp.AddRow("numProviderPrc","d",dbProviderPrc.ToString().Trim());
							tp.AddRow("numProviderTotal","d",dbProviderTotal.ToString().Trim());
							tp.AddRow("numProviderGainJe","d",dbProviderGainJe.ToString().Trim());

							tp.AddRow("numIsLocalOrder","i","0");
					
						}

					
						
						if(tp.AddNewRec("eg_PolOrder"))
						{
							XmlNode nodeCusters = npRet.FindNodeByPath("//eg/Custer");
							//gs.util.func.Write("Count=" + nodeCusters.ChildNodes.Count);
							string strPers = "";
							for(int i=0;i<nodeCusters.ChildNodes.Count;i++)
							{
								XmlNode nodeCuster = nodeCusters.ChildNodes[i];

								string strPerName = nodeCuster.ChildNodes[0].ChildNodes[0].Value.ToString().Trim();
								string strIdentCardType = nodeCuster.ChildNodes[1].ChildNodes[0].Value.ToString().Trim();
								string strMobile = nodeCuster.ChildNodes[2].ChildNodes[0].Value.ToString().Trim();
								string strIdentCardNo = nodeCuster.ChildNodes[3].ChildNodes[0].Value.ToString().Trim();
								string chInsurance = nodeCuster.ChildNodes[4].ChildNodes[0].Value.ToString().Trim();

								strPers += (strPerName + ";");
											
								string strIns = "insert into eg_PolOrderView(numOrderId,vcPerName,vcIdentCardType,vcIdentCardNo,vcMobile,bInsurance) values(" + strOrderId + ",'" + strPerName + "','" + strIdentCardType + "','" + strIdentCardNo + "','" + strMobile + "'," + chInsurance + ")";
								//Response.Write(strIns + "<br>");
								cn.Update(strIns);
							}
							
							strErr = "succ$" + strPnr;
						}
						else
						{
							strErr = "预定失败,请检查数据";
						}
						
					}
					catch(Exception ex)
					{
						
						strErr = "预定失败" + ex.Message;
						gs.util.func.Write("SubmitOrder is err =" + ex.Message);
						throw ex;
					}
					finally
					{
						
					}
				}
				else
				{
					strErr = "航信预定失败";
				}
			}
			return strErr;
		}

		/// <summary>
		/// 河南邮政185接口
		/// </summary>
		/// <param name="p_str185"></param>
		/// <returns></returns>
		public string Submit185Order(string p_str185)
		{
			Conn cn = null;
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			string strRet = "";

			try
			{
				string[] ary = p_str185.Split('$');
		
				
				NewPara npSent = new NewPara();
				npSent.AddPara("AirNo",ary[7]);
				npSent.AddPara("ActionCode","LL");
				npSent.AddPara("DepartTime",Policy.getIBEDate2(ary[33]));
				npSent.AddPara("Dst",ary[3]);
				npSent.AddPara("Org",ary[2]);
				npSent.AddPara("SeatName",ary[11]);
				npSent.AddPara("EtkPrc",ary[12]);
				npSent.AddPara("Fule",ary[15]);
				npSent.AddPara("BaseFee",ary[14]);
				npSent.AddPara("TkDate",Policy.getIBEDate(ary[33]));
				npSent.AddPara("PerCount",ary[16]);
			
				XmlDocument rootDoc = npSent.getRoot();
				XmlNode nodeRecord = npSent.AddPara("record","");
			
			
				string strCuster = ary[34];
				string[] aryCust = strCuster.Split('~');

				for(int iTmp=0;iTmp<aryCust.Length;iTmp++)
				{
					string strACust = aryCust[iTmp];
					string[] aryCustView = strACust.Split('*');
					
					string strPerName = aryCustView[0];
					string strIdentCardType = aryCustView[1];
					string strMobile = aryCustView[2];
					string strIdentCardNo = aryCustView[3];
					string chInsurance = aryCustView[4];
					
					XmlNode nodeRec = rootDoc.CreateNode(XmlNodeType.Element,"rec","");

					XmlNode subNode = rootDoc.CreateNode(XmlNodeType.Element,"IdCard","");
					subNode.AppendChild(rootDoc.CreateTextNode(strIdentCardNo));
					nodeRec.AppendChild(subNode);	//把变量值赋进去

					subNode = rootDoc.CreateNode(XmlNodeType.Element,"AirCorp","");
					subNode.AppendChild(rootDoc.CreateTextNode(ary[6]));
					nodeRec.AppendChild(subNode);	//把变量值赋进去

					subNode = rootDoc.CreateNode(XmlNodeType.Element,"IdType","");
					subNode.AppendChild(rootDoc.CreateTextNode("NI"));
					nodeRec.AppendChild(subNode);	//把变量值赋进去

					subNode = rootDoc.CreateNode(XmlNodeType.Element,"CustName","");
					subNode.AppendChild(rootDoc.CreateTextNode(strPerName));
					nodeRec.AppendChild(subNode);	//把变量值赋进去
				
					nodeRecord.AppendChild(nodeRec);
					
				}
		

				//TextBox1.Text = npSent.GetXML();
				//gs.util.func.Write("1111");
				IBECOMService ic = new IBECOMService();
				string strPnr = ic.MakeOrder(npSent.GetXML());
				gs.util.func.Write("pnr="+strPnr);

				np.AddPara("cm","SubmitWebOrder");//
				np.AddPara("pnr",strPnr);
				//np.AddPara("pnr",ary[0]);
				np.AddPara("CurUserName",ary[1]);
				//gs.util.func.Write("CurUserName="+ary[1]);
				np.AddPara("BeginPort",ary[2]);
				np.AddPara("EndPort",ary[3]);
				np.AddPara("BeginPortName",ary[4]);
				np.AddPara("EndPortName",ary[5]);
				np.AddPara("AirName",ary[6]);
				np.AddPara("FlightNo",ary[7]);
				np.AddPara("DepaTime",ary[8]);
				np.AddPara("LandTime",ary[9]);
				np.AddPara("AirType",ary[10]);
				np.AddPara("SeatName",ary[11]);
				np.AddPara("EtkPrc",ary[12]);
				np.AddPara("RealPrc",ary[13]);
				np.AddPara("BaseFee",ary[14]);
				np.AddPara("Fule",ary[15]);
				np.AddPara("PerCounts",ary[16]);
				np.AddPara("RetGain",ary[17]);
				np.AddPara("AirRetGain",ary[18]);
				np.AddPara("PubGain",ary[19]);
				np.AddPara("PoliId",ary[20]);
				np.AddPara("Total",ary[21]);
				//gs.util.func.Write("Total="+ary[21]);
				np.AddPara("PoliBegin",ary[22]);
				np.AddPara("PoliEnd",ary[23]);
				np.AddPara("ProviderAgentName",ary[24]);
				np.AddPara("ProviderAgentId",ary[25]);
				np.AddPara("vcSaleContacter",ary[26]);
				np.AddPara("vcSalMobile",ary[27]);
				np.AddPara("vcSalTel",ary[28]);
				np.AddPara("vcSalQq",ary[29]);
				np.AddPara("vcAddress",ary[30]);
				//gs.util.func.Write("vcAddress="+ary[30]);
				np.AddPara("AirCorpName",ary[31]);
				np.AddPara("Discount",ary[32]);
				//gs.util.func.Write("Discount="+ary[32]);
				np.AddPara("FlyDate",ary[33]);
				//gs.util.func.Write("FlyDate="+ary[33]);
				
				//gs.util.func.Write("2222");
				//string strCuster = ary[34];
				//string[] aryCust = strCuster.Split('~');
				XmlNode nodeCusters = np.AddPara("Custer","");
				gs.util.func.Write("aryCust.Length="+aryCust.Length);
				for(int iTmp=0;iTmp<aryCust.Length;iTmp++)
				{
					string strACust = aryCust[iTmp];
					string[] aryCustView = strACust.Split('*');
					XmlNode subCuster = doc.CreateNode(XmlNodeType.Element,"ACUSTER","");

					XmlNode subNode = null;
					//gs.util.func.Write("aryCustView[0]="+aryCustView[0]);
					subNode = doc.CreateNode(XmlNodeType.Element,"PerName","");
					subNode.AppendChild(doc.CreateTextNode(aryCustView[0]));
					subCuster.AppendChild(subNode);	//把变量值赋进去
					//gs.util.func.Write("aryCustView[1]="+aryCustView[1]);
					subNode = doc.CreateNode(XmlNodeType.Element,"IdentCardType","");
					subNode.AppendChild(doc.CreateTextNode(aryCustView[1]));
					subCuster.AppendChild(subNode);	//把变量值赋进去

					subNode = doc.CreateNode(XmlNodeType.Element,"Mobile","");
					subNode.AppendChild(doc.CreateTextNode(aryCustView[2]));
					subCuster.AppendChild(subNode);	//把变量值赋进去

					subNode = doc.CreateNode(XmlNodeType.Element,"IdentCardNo","");
					subNode.AppendChild(doc.CreateTextNode(aryCustView[3]));
					subCuster.AppendChild(subNode);	//把变量值赋进去

					subNode = doc.CreateNode(XmlNodeType.Element,"Insurance","");
					subNode.AppendChild(doc.CreateTextNode(aryCustView[4]));
					subCuster.AppendChild(subNode);	//把变量值赋进去
					//gs.util.func.Write("aryCustView[4]="+aryCustView[4]);

					nodeCusters.AppendChild(subCuster);
				}

				string strSent = np.GetXML();

				cn = new Conn();
				cn.beginTrans();
				strRet = SubmitOrder(cn,strSent);
				cn.commit();

			}
			catch(Exception ex)
			{
				cn.rollback();
				strRet = "outtkerr";
				gs.util.func.Write("Submit185Order is err=" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		/// <summary>
		/// 得到最新的网站订单消息
		/// </summary>
		/// <returns></returns>
		public string getNewOrderList()
		{//gs.util.func.Write("getNewOrderList come on");
			//NewPara np = new NewPara();
			//np.AddPara("cm","RetWriteLog");

			string strRet = "";
			DataSet rsRet = null;
			Conn cn = null;
			try
			{
				cn = new Conn();
				string strSql = "select *,dtOrderStep=(case eg_PolOrder.iTksState when 1 then convert(varchar,datediff(minute,eg_PolOrder.dtPayTime,getdate())) else '' end ) from eg_PolOrder where (iTksState=1 or iTksState=5 or iTksState=7) and dtPayTime > CONVERT(DATETIME, '" + System.DateTime.Now.AddDays(-1).ToShortDateString().Trim() + " 00:00:00') AND  dtPayTime < CONVERT(DATETIME, '" + System.DateTime.Now.ToShortDateString().Trim() + " 23:59:59')";
				//gs.util.func.Write("getNewOrderList strSql=" + strSql);
				rsRet = cn.GetDataSet(strSql);
				for(int i=0;i<rsRet.Tables[0].Rows.Count;i++)
				{
					string strPnr = rsRet.Tables[0].Rows[i]["vcPnr"].ToString().Trim();
					string strOrderStep = rsRet.Tables[0].Rows[i]["dtOrderStep"].ToString().Trim();
					string strOrderState = rsRet.Tables[0].Rows[i]["iTksState"].ToString().Trim();
					strRet += strPnr + "~";
					strRet += strOrderStep + "~";
					strRet += strOrderState + "~";
					string strProviderAgentCode = rsRet.Tables[0].Rows[i]["vcProviderAgentCode"].ToString().Trim();
					strSql = "select * from eg_user where numAgentId='" + strProviderAgentCode + "' and numRoleId=1";
					DataSet dsUser = cn.GetDataSet(strSql);
					string strUsers = "";
					for(int j=0;j<dsUser.Tables[0].Rows.Count;j++)
					{
						string strUserName = dsUser.Tables[0].Rows[j]["vcLoginName"].ToString().Trim();
						strUsers += strUserName + "<au>";
					}
					dsUser.Clear();
					/*if(strUsers != "")
					{
						strUsers = strUsers.Substring(0,strUsers.Length-1);
					}*/
					strUsers += "admin";
					strRet += strUsers + "!";
				}
				if(strRet != "")
				{
					strRet = strRet.Substring(0,strRet.Length-1);
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("logic.Order.getNewOrderList is err" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}

			return strRet;	
		}


	}
}
