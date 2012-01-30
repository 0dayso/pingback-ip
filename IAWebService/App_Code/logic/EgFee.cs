using System;
using System.Data;
using gs.para;
using logic;
using gs.DataBase;
using System.Xml;

namespace logic
{
	/// <summary>
	/// Fee 的摘要说明。
	/// </summary>
	public class EgFee
	{
		public EgFee()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 给帐户冲直
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <param name="p_strVal"></param>
		/// <param name="p_strOperator"></param>
		/// <returns></returns>
		public bool AddFee(string p_strUserId,string p_strVal,string p_strOperator,string p_strOpType,string p_strRemark,string p_strUrl,string p_strDesc)
		{
			bool bRet = false;

			Conn cn = null;
			try
			{
				cn = new Conn();
				cn.beginTrans();
				/*string strSql = "insert into eg_userFeeRec(numUserId,numOp,dtOpTime,vcUserCode,numVal,vcMark) values(" + p_strUserId + ",0,getdate(),'" + p_strOperator + "'," + p_strVal + ",'冲值')";
				cn.Update(strSql);
				strSql = "update eg_user set numVal=numVal+" + p_strVal + " where numUserId=" + p_strUserId;
				cn.Update(strSql);*/
				AddFee(cn,p_strUserId,p_strVal,p_strOperator,p_strOpType,p_strRemark,p_strUrl,p_strDesc);
				cn.commit();
				bRet = true;

			}
			catch(Exception ex)
			{
				cn.rollback();
				gs.util.func.Write("AddFee is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		public bool AddFee(Conn p_cn,string p_strUserId,string p_strVal,string p_strOperator,string p_strOpType,string p_strRemark,string p_strUrl,string p_strDesc)
		{
			bool bRet = false;

			Conn cn = p_cn;
			try
			{
				string strSql = "update eg_user set numVal=numVal+" + p_strVal + " where numUserId=" + p_strUserId;
				cn.Update(strSql);

				strSql = "select * from eg_user where numUserId=" + p_strUserId;
				DataSet ds = cn.GetDataSet(strSql);
				string strBalance = "0";
				if(ds.Tables[0].Rows.Count > 0)
					strBalance = ds.Tables[0].Rows[0]["numVal"].ToString().Trim();

				strSql = "insert into eg_userFeeRec(numUserId,numOp,dtOpTime,vcUserCode,numVal,vcOpType,vcMark,dbBalance,vcOpenUrl,vcDesc) values(" + p_strUserId + ",0,getdate(),'" + p_strOperator + "'," + p_strVal + ",'" + p_strOpType + "','" + p_strRemark + "'," + strBalance + ",'" + p_strUrl + "','" + p_strDesc + "')";
				cn.Update(strSql);
				bRet = true;
			}
			catch(Exception ex)
			{
				gs.util.func.Write("AddFee is err" + ex.Message);
				throw ex;
			}
			return bRet;
		}

		
		/// <summary>
		/// 减款,主要用来做帐方便
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <param name="p_strVal"></param>
		/// <param name="p_strOperator"></param>
		/// <returns></returns>
		public bool ReduceFee(string p_strUserId,string p_strVal,string p_strOperator,string p_strRemark)
		{
			bool bRet = false;

			Conn cn = null;
			try
			{
				cn = new Conn();
				cn.beginTrans();
				string strSql = "insert into eg_userFeeRec(numUserId,numOp,dtOpTime,vcUserCode,numVal,vcMark) values(" + p_strUserId + ",1,getdate(),'" + p_strOperator + "'," + p_strVal + ",'" + p_strRemark + "')";
				cn.Update(strSql);
				strSql = "update eg_user set numVal=numVal-" + p_strVal + " where numUserId=" + p_strUserId;
				cn.Update(strSql);
				cn.commit();
				bRet = true;

			}
			catch(Exception ex)
			{
				cn.rollback();
				gs.util.func.Write("ReduceFee is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}


		/// <summary>
		/// 消费减款
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <param name="p_strVal"></param>
		/// <param name="p_strOperator"></param>
		/// <returns></returns>
		public bool DecFee(string p_strUserId,string p_strVal,string p_strOperator,string p_strOpType,string p_strRemark,string p_strUrl,string p_strDesc)
		{
			bool bRet = false;

			Conn cn = null;
			try
			{
				cn = new Conn();
				cn.beginTrans();
				/*string strSql = "insert into eg_userFeeRec(numUserId,numOp,dtOpTime,vcUserCode,numVal,vcMark) values(" + p_strUserId + ",1,getdate(),'" + p_strOperator + "'," + p_strVal + ",'消费减款')";
				cn.Update(strSql);
				strSql = "update eg_user set numVal=numVal-" + p_strVal + " where numUserId=" + p_strUserId;
				cn.Update(strSql);*/
				DecFee(cn,p_strUserId,p_strVal,p_strOperator,p_strOpType,p_strRemark,p_strUrl,p_strDesc);
				cn.commit();
				bRet = true;

			}
			catch(Exception ex)
			{
				cn.rollback();
				gs.util.func.Write("ReduceFee is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}

		public bool DecFee(Conn p_cn,string p_strUserId,string p_strVal,string p_strOperator,string p_strOpType,string p_strRemark,string p_strUrl,string p_strDesc)
		{
			bool bRet = false;
//gs.util.func.Write("df====71 ");
			Conn cn = p_cn;
			try
			{
//gs.util.func.Write("df====72 ");
				string strSql = "update eg_user set numVal=numVal-" + p_strVal + " where numUserId=" + p_strUserId;
//gs.util.func.Write("df====73 strSql=" + strSql);
				cn.Update(strSql);
				strSql = "select * from eg_user where numUserId=" + p_strUserId;
//gs.util.func.Write("df====74 strSql=" + strSql);
				DataSet ds = cn.GetDataSet(strSql);

				string strBalance = "0";
				if(ds.Tables[0].Rows.Count > 0)
					strBalance = ds.Tables[0].Rows[0]["numVal"].ToString().Trim();
//gs.util.func.Write("df====75 strBalance=" + strBalance);

				strSql = "insert into eg_userFeeRec(numUserId,numOp,dtOpTime,vcUserCode,numVal,vcOpType,vcMark,dbBalance,vcOpenUrl,vcDesc) values(" + p_strUserId + ",1,getdate(),'" + p_strOperator + "'," + p_strVal + ",'" + p_strOpType + "','" + p_strRemark + "'," + strBalance + ",'" + p_strUrl + "','" + p_strDesc + "')";
//gs.util.func.Write("df====76 strSql=" + strSql);
				cn.Update(strSql);
//gs.util.func.Write("df====77" );
				bRet = true;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("DecFee is err" + ex.Message);
				throw ex;
			}
			return bRet;
		}
		/// <summary>
		/// 得到帐户余额
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <returns></returns>
		public string GetCloseBalance(string p_strUserId)
		{
			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				/*string strSql = "select numVal from eg_user where numUserId=" + p_strUserId;
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strRet = ds.Tables[0].Rows[0]["numVal"].ToString().Trim();

				}*/
				strRet = GetCloseBalance(cn,p_strUserId);
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetCloseBalance is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}
			return strRet;
		}

		public string GetCloseBalance(Conn p_cn,string p_strUserId)
		{
			string strRet = "";
			Conn cn = p_cn;
			try
			{
				string strSql = "select numVal from eg_user where numUserId=" + p_strUserId;
				DataSet ds = cn.GetDataSet(strSql);
				if(ds.Tables[0].Rows.Count > 0)
				{
					strRet = ds.Tables[0].Rows[0]["numVal"].ToString().Trim();
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetCloseBalance is err" + ex.Message);
				throw ex;
			}
			
			return strRet;
		}
		/// <summary>
		/// 用户退票的增款
		/// </summary>
		/// <param name="p_strUserId"></param>
		/// <param name="p_strVal"></param>
		/// <param name="p_strOperator"></param>
		/// <returns></returns>
		public bool BackFee(string p_strUserId,string p_strVal,string p_strOperator)
		{

			bool bRet = false;
			bRet = AddFee(p_strUserId,p_strVal,p_strOperator,"用户退票的增款","system增款到" + Right.getUserNameById(p_strUserId),"","");
			/*Conn cn = null;
			try
			{
				cn = new Conn();
				cn.beginTrans();
				string strSql = "insert into eg_userFeeRec(numUserId,numOp,dtOpTime,vcUserCode,numVal,vcMark) values(" + p_strUserId + ",0,getdate(),'" + p_strOperator + "'," + p_strVal + ",'用户退票')";
				cn.Update(strSql);
				strSql = "update eg_user set numVal=numVal+" + p_strVal + " where numUserId=" + p_strUserId;
				cn.Update(strSql);
				cn.commit();
				bRet = true;

			}
			catch(Exception ex)
			{
				cn.rollback();
				gs.util.func.Write("BackFee is err" + ex.Message);
			}
			finally
			{
				cn.close();
			}*/
			return bRet;
		}

		/// <summary>
		/// 得到为支付的PNR列表
		/// </summary>
		/// <param name="p_strUserCode"></param>
		/// <returns></returns>
		public string GetUnPayPnr(string p_strUserCode)
		{
			NewPara np = new NewPara();
			XmlDocument doc = np.getRoot();
			np.AddPara("cm","RetUnPayPnr");
			string strSql = "";

			string strRet = "";
			Conn cn = null;
			try
			{
				cn = new Conn();
				strSql = "select * from eg_user where vcLoginName='" + p_strUserCode.Trim() + "'";
				DataSet ds = cn.GetDataSet(strSql);
				string strAgentCode = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
				if(strAgentCode.Length > 8)
					strAgentCode = strAgentCode.Substring(0,8);

				strSql = "select eg_eticket.Pnr from dbo.eg_eticket INNER JOIN dbo.eg_user ON ltrim(rtrim(dbo.eg_eticket.UserID)) = ltrim(rtrim(dbo.eg_user.vcLoginName)) where ltrim(rtrim(eg_eticket.DecFeeState))='0' and eg_user.numAgentId like '" + strAgentCode + "%' and eg_eticket.OperateTime > CONVERT(DATETIME, '" + System.DateTime.Now.AddDays(-1).ToShortDateString().Trim() + " 00:00:00') AND  eg_eticket.OperateTime < CONVERT(DATETIME, '" + System.DateTime.Now.ToShortDateString().Trim() + " 23:59:59') ";
				ds = cn.GetDataSet(strSql);
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					strRet += (ds.Tables[0].Rows[i]["Pnr"].ToString().Trim() + ";");
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("GetUnPayPnr is err sql=" + strSql + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}

			np.AddPara("Pnrs",strRet);

			return np.GetXML();
		}
        /// <summary>
        /// 得到电子客票号为空和未支付的PNR列表
        /// </summary>
        /// <param name="p_strUserCode"></param>
        /// <returns></returns>
        public string GetTNumberNullPnr(string p_strUserCode)
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "RetTNumberNullPnr");
            string strSql = "";

            string strRet = "";
            Conn cn = null;
            try
            {
                cn = new Conn();
                strSql = "select * from eg_user where vcLoginName='" + p_strUserCode.Trim() + "'";
                DataSet ds = cn.GetDataSet(strSql);
                string strAgentCode = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
                if (strAgentCode.Length > 8)
                    strAgentCode = strAgentCode.Substring(0, 8);

                strSql = "select eg_eticket.Pnr from dbo.eg_eticket INNER JOIN dbo.eg_user ON ltrim(rtrim(dbo.eg_eticket.UserID)) = ltrim(rtrim(dbo.eg_user.vcLoginName)) where ({ fn LENGTH(eg_eticket.etNumber) } < 14 or eg_eticket.etNumber IS NULL or eg_eticket.etNumber = ''or ltrim(rtrim(eg_eticket.DecFeeState))='0') and eg_user.numAgentId like '" + strAgentCode + "%' and eg_eticket.OperateTime > CONVERT(DATETIME, '" + System.DateTime.Now.AddDays(-1).ToShortDateString().Trim() + " 00:00:00') AND  eg_eticket.OperateTime < CONVERT(DATETIME, '" + System.DateTime.Now.ToShortDateString().Trim() + " 23:59:59') ";
                ds = cn.GetDataSet(strSql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strRet += (ds.Tables[0].Rows[i]["Pnr"].ToString().Trim() + ";");
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("GetTNumberNullPnr is err sql=" + strSql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }

            np.AddPara("Pnrs", strRet);

            return np.GetXML();
        }
        public string GetTNNullPnrByIPID(string ipid)
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "RetTNumberNullPnr");
            string strSql = "";

            string strRet = "";
            Conn cn = null;
            try
            {
                cn = new Conn();
                //strSql = "select * from eg_user where vcLoginName='" + p_strUserCode.Trim() + "'";
                strSql = "select * from  eg_SrvIps where ipid="+ipid.Trim();
                DataSet ds = cn.GetDataSet(strSql);
                string strAgentCode = ds.Tables[0].Rows[0]["numAgentId"].ToString().Trim();
                if (strAgentCode.Length > 8)
                    strAgentCode = strAgentCode.Substring(0, 8);

                strSql = "select eg_eticket.Pnr from dbo.eg_eticket INNER JOIN dbo.eg_user ON ltrim(rtrim(dbo.eg_eticket.UserID)) = ltrim(rtrim(dbo.eg_user.vcLoginName)) where ({ fn LENGTH(eg_eticket.etNumber) } < 14 or eg_eticket.etNumber IS NULL or eg_eticket.etNumber = ''or ltrim(rtrim(eg_eticket.DecFeeState))='0') and eg_user.numAgentId like '" + strAgentCode + "%' and eg_eticket.OperateTime > CONVERT(DATETIME, '" + System.DateTime.Now.AddDays(-1).ToShortDateString().Trim() + " 00:00:00') AND  eg_eticket.OperateTime < CONVERT(DATETIME, '" + System.DateTime.Now.ToShortDateString().Trim() + " 23:59:59') ";
                ds = cn.GetDataSet(strSql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strRet += (ds.Tables[0].Rows[i]["Pnr"].ToString().Trim() + ";");
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("GetTNumberNullPnr is err sql=" + strSql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }

            np.AddPara("Pnrs", strRet);

            return np.GetXML();
        }
	}
}
