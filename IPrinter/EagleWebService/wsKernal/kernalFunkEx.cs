using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace EagleWebService
{
    public class kernalFunkEx
    {
        /// <summary>
        /// ȡƱ�۾���,�ȱ���,�ٷ�����,���Ҳ����򷵻ض�Ϊ0
        /// </summary>
        /// <param name="citypair">Ҫȡ�ĳ��ж�</param>
        /// <param name="cn">data.mdb������,����Ϊnull</param>
        /// <param name="wsaddr">Web�����ַ</param>
        /// <param name="distance">���صľ���</param>
        /// <param name="price">���صļ۸�</param>
        static public void FC_get_server_local(string citypair,OleDbConnection cn,string wsaddr,ref int distance,ref int price)
        {
            if (cn.State != ConnectionState.Open)
                try
                {

                    string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.mdb;";
                    cn.ConnectionString = ConnStr;
                    cn.Open();
                }
                catch(Exception ex)
                {
                    EagleString.EagleFileIO.LogWrite("FC_get_server_local in kernalFunkEx:" + ex.Message);
                }
            try
            {
                string sFrom = citypair.Substring(0, 3);
                string sTo = citypair.Substring(3);
                OleDbCommand cmd = new OleDbCommand("select * from t_fc where [From]='" + sFrom + "'" + "and [To]='" + sTo + "'", cn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dtTmp = new DataTable();
                adapter.Fill(dtTmp);

                
                if (dtTmp.Rows.Count != 0)
                {
                    distance = (int)float.Parse(dtTmp.Rows[0]["BunkF"].ToString());
                    price = (int)float.Parse(dtTmp.Rows[0]["BunkY"].ToString());
                }
                else//ȡ�������۸�
                {
                    kernalFunc kf = new kernalFunc(wsaddr);
                    int tf, tc, ty;
                    tf = tc = ty = 0;
                    kf.FC_Read(citypair,ref tf,ref tc,ref ty);
                    distance = tf;
                    price = ty;
                }
            }
            catch (Exception ex)
            {
                EagleString.EagleFileIO.LogWrite("FC_get_server_local in kernalFunkEx:" + ex.Message);
                return;
            }
        }
    }
}
