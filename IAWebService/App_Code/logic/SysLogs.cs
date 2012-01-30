using System;
using System.Data;
using System.Xml;
using gs.para;
using gs.DataBase;

namespace logic
{
	/// <summary>
	/// SysLogs 的摘要说明。
	/// </summary>
	public class SysLogs
	{
	

		public bool AddLogs(string strUser,string strCmd,string strReturnResult)
		{
			Conn cn = null;
			bool bRet = false;
			string strLogs = "";
			try
			{
				cn = new Conn();

				/*Top3 tp = new Top3(cn); 
								
				tp.AddRow("UserID","c",strUser);
				tp.AddRow("Cmd","c",strCmd);
				tp.AddRow("ReturnResult","c",strReturnResult);
						 
							
				if(tp.AddNewRec("eg_operationLogs"))
				{
					bRet = true;
				}*/

				string strTmpCmd = strCmd.Replace("'"," ");
				strTmpCmd = strTmpCmd.Replace("\""," ");
				strTmpCmd = strTmpCmd.Replace(","," ");

				string strTmpResult = strReturnResult.Replace("'"," ");
				strTmpResult = strTmpResult.Replace("\""," ");
				strTmpResult = strTmpResult.Replace(","," ");

				strLogs = "insert into eg_operationlogs(userid,cmd,returnresult) values('" + strUser + "','" + strTmpCmd + "','" + strTmpResult + "')";
				cn.Update(strLogs);
				bRet = true;

			}
			catch(Exception ex)
			{
				gs.util.func.Write("AddLogs is err=" + ex.Message + ":sql=" + strLogs);
			}
			finally
			{
				cn.close();
			}
			return bRet;
		}
	}
}
