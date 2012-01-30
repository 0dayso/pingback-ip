using System;
using System.Data;
using gs.para;
using logic;
using gs.DataBase;
using System.Xml;
namespace logic
{
    public class CityTicketInfo
    {
        public CityTicketInfo()
        {
            
        }
        public string getAllPolicyCity()//得到所有的城市
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "ret_getAllPolicyCity");
            string strSql = "";
            string strRet = "";
            Conn cn = null;
            try
            {
                cn = new Conn();
                strSql = "SELECT vcCityCode FROM eg_PolicyCity ";
                DataSet ds = cn.GetDataSet(strSql);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strRet += (ds.Tables[0].Rows[i]["vcCityCode"].ToString().Trim() + ";");
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("getAllPolicyCity is err sql=" + strSql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }
            np.AddPara("Citys", strRet);
            return np.GetXML();
        }
        public string updateTicketInfo(string bfrom, string eto, string bunkf, string bunky)//更新
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "ret_updateTicketInfo");
            string strSql = "";
            string strRet = "";
            Conn cn = null;
            try
            {
                cn = new Conn();
                strSql = "UPDATE eg_ticketInfo SET BUNKF = '" + bunkf + "', BUNKY = '" + bunky + "' WHERE (BFROM = '" + bfrom + "') AND (ETO = '" + eto + "')";
                if (cn.Update(strSql))
                {
                    strRet = "true";
                }
                else 
                {
                    strRet = "false";
                }
                
            }
            catch (Exception ex)
            {
                gs.util.func.Write("updateTicketInfo is err sql=" + strSql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }
            np.AddPara("ret", strRet);
            return np.GetXML();
        }
        public string insertTicketInfo(string bfrom, string eto, string bunkf, string bunky)//添加
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "ret_insertTicketInfo");
            string strSql = "";
            string strRet = "";
            Conn cn = null;
            try
            {
                cn = new Conn();
                strSql = "INSERT INTO eg_ticketInfo (BFROM, ETO, BUNKF, BUNKY) VALUES ('" + bfrom + "', '" + eto + "', '" + bunkf + "', '" + bunky + "')";
                if (cn.Update(strSql))
                {
                    strRet = "true";
                }
                else
                {
                    strRet = "false";
                }

            }
            catch (Exception ex)
            {
                gs.util.func.Write("insertTicketInfo is err sql=" + strSql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }
            np.AddPara("ret", strRet);
            return np.GetXML();
        }
        public string getTicketInfo(string bfrom, string eto)//得到
        {
            NewPara np = new NewPara();
            XmlDocument doc = np.getRoot();
            np.AddPara("cm", "ret_getTicketInfo");
            string strSql = "";
            string strRet = "";
            Conn cn = null;
            try
            {
                cn = new Conn();
                strSql = "SELECT * FROM eg_ticketInfo WHERE (BFROM = '" + bfrom + "') AND (ETO = '" + eto + "')";
                DataSet ds = cn.GetDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strRet = "true";
                }
                else
                {
                    strRet = "false";
                }
            }
            catch (Exception ex)
            {
                gs.util.func.Write("getTicketInfo is err sql=" + strSql + ex.Message);
                throw ex;
            }
            finally
            {
                cn.close();
            }
            np.AddPara("ret", strRet);
            return np.GetXML();
        }

    }
}