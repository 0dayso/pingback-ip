using System;
using gs.para;
using gs.DataBase;
using System.Data;

namespace logic
{
	/// <summary>
	/// eTicket 的摘要说明。
	/// </summary>
	public class eTicket
	{
		public static void LogIt(string username, string pnr, string receiptNumber, string isOffline)
		{
//			if(!IfExist(receiptNumber))//无需判断，重复的行程单号民航不会接受
			{
				Conn cn = new Conn();
				Top2 tp = new Top2(cn);
				tp.AddRow("receiptNumber", "c", receiptNumber);
				tp.AddRow("isOffline", "i", isOffline);
				string strWhere = "UserID = '{0}' and Pnr = '{1}' and OperateTime > '{2}'";

				string date = DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd");
				strWhere = string.Format(strWhere, username, pnr, date);

				tp.Update("eg_eticket", strWhere);
			}
//			else
//				throw new Exception("行程单号 " + receiptNumber.ToString() + " 已经被打印过！");
		}

		public static bool IfExist(string receiptNumber)
		{
			bool bRet = false;
			Conn cn = null;

			try
			{
				cn = new Conn();
				string strSql = "select receiptNumber from eg_eticket where receiptNumber='" + receiptNumber + "'";
				DataSet ds = cn.GetDataSet(strSql);

				if(ds.Tables[0].Rows.Count > 0)
				{
					bRet = true;
				}
			}
			catch(Exception ex)
			{
				gs.util.func.Write("eTicket.IfExist 方法报错：" + ex.Message);
				throw ex;
			}
			finally
			{
				cn.close();
			}

			return bRet;
		}
	}
}
