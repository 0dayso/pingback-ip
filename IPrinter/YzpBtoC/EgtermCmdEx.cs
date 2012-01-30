using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace YzpBtoC
{
    public class EgtermCmdEx
    {
        public string cmdex = "";
        public string retString = "";
        public string errorString = "";
        public string tktime = "";
        public string tkdate = "";
        public string officeno = "";

        public bool bFlag = false;//ָ�������־(�ɹ����)
        public bool bEgExCmd = true;
        //˽�б�����������Ӧ�������
        private string resKHZLLD = "";
        private string resEGYD = "";
        private string resKHZLMC = "";
        EagleWebService.wsYzpbtoc ws;

        public EgtermCmdEx()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isB2C">�Ƿ�b2cģʽ</param>
        public EgtermCmdEx(bool isB2C)
        {
            if(isB2C)
                ws = new EagleWebService.wsYzpbtoc();
        }

        /// <summary>
        /// ���ݵ�ǰ���磬�����ֹ�����绰�����ѯ�ͻ����ÿ��б�
        /// modified by chenqj
        /// </summary>
        private  void KHZLLD()
        {
            string ret = string.Empty;
            XmlDocument xd = new XmlDocument();

            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);

                if (arr.Length > 1)//�ֹ�����绰�����ѯ
                {
                    ret = ws.GetCustomerByPhone(arr[1]);

                    if (ret.ToLower() == "none")
                    {
                        resKHZLLD = "";
                        throw new Exception("�ÿͻ����ϲ����ڣ�");
                    }
                    else if (ret.ToLower() == "failed")
                    {
                        resKHZLLD = "";
                        throw new Exception("��ѯʧ�ܣ�");
                    }
                    else
                        Options.GlobalVar.B2CCallingXml = ret;
                }
                else//1.��CallXml��ȡCustomID
                {
                    if (string.IsNullOrEmpty(Options.GlobalVar.B2CCallingXml))
                    {
                        resKHZLLD = "";
                        throw new Exception("û�м�⵽�ͻ������¼��");
                    }
                    //if (NewCustomer()) return;
                    string strXml = Options.GlobalVar.B2CCallingXml;

                    try
                    {
                        xd.LoadXml(strXml);
                    }
                    catch(Exception ee)
                    {
                        resKHZLLD = "";
                        throw new Exception("��ȡ�����¼ʧ�ܣ�" + Environment.NewLine + ee.Message);
                    }

                    if (xd.SelectSingleNode("NewCustomer") != null)
                    {
                        throw new Exception("��ǰ����Ϊ�¿ͻ������ȱ���ÿͻ���Ϣ��");
                    }
                    else
                    {
                        ret = strXml;
                    }
                }

                //2.����WebService���ع����û�����
                resKHZLLD = ret;
                DataEntity.XMLSchema.Customers customer;
                customer = DataEntity.XMLSchema.xml_BaseClass.LoadXml<DataEntity.XMLSchema.Customers>(ret);

                if (customer.Passengers == null || customer.Passengers.Count == 0)
                    throw new Exception("������ͻ�Ŀǰû�г��ÿͣ�");

                retString = "";
                foreach(DataEntity.XMLSchema.Passenger passenger in customer.Passengers)
                {
                    retString += passenger.fet_ID + " "
                          + passenger.fet_Name + " "
                          + passenger.fet_CardID1 + newline.ToString();
                }
                bFlag = true;
            }
            catch(Exception ee)
            {
                //resKHZLLD =  "";
                retString = ee.Message;
                bFlag = false;
            }
        }
        string[] sp = new string[] { " ", ",", "��", ":", ";" ,"��","��"};
        char newline = '\xD';
        private  void EGYD()
        {
            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length < 2) throw new Exception("EGYD3");                                       //����3
                
                string strCmd = "sd" + arr[1] + string.Format("{0}", arr.Length - 2) + newline.ToString();
                List<string> lsNM = new List<string>();

                XmlDocument xd = new XmlDocument();
                if (resKHZLLD == "") throw new Exception("EGYD4");                                      //����4
                xd.LoadXml(resKHZLLD);
                XmlNode xn = xd.SelectSingleNode("//Customer/Passengers");
                for (int i = 2; i < arr.Length; i++)
                {
                    for (int j = 0; j < xn.ChildNodes.Count; j++)
                    {
                        if (xn.ChildNodes[j].SelectSingleNode("PassengerID").InnerText.Trim() == arr[i].Trim())
                        {
                            lsNM.Add(xn.ChildNodes[j].SelectSingleNode("PassengerName").InnerText  + "-" + xn.ChildNodes[j].SelectSingleNode("CardID").InnerText);
                        }
                    }
                }
                mystring.sortStringListByPinYin(lsNM);
                strCmd += "nm";
                for (int i = 0; i < lsNM.Count; i++)
                {
                    strCmd += "1" + lsNM[i].Split('-')[0];
                }
                strCmd += newline.ToString();
                strCmd += "ct" + xd.SelectSingleNode("//Customer/Mobile").InnerText + xd.SelectSingleNode("//Customer/Mobile").InnerText
                    + newline.ToString();
                strCmd += "tktl" + (tktime == "" ? "2300" : tktime)
                    + "/" + (tkdate == "" ? "." : tkdate) + "/" + (officeno == "" ? "wuh128" : officeno) + newline.ToString();
                for (int i = 0; i < lsNM.Count; i++)
                {
                    string tempstr = lsNM[i].Split('-')[1].Trim();
                    if (tempstr == "") tempstr = "0";
                    strCmd += "SSR FOID YY HK/NI" + tempstr + "/p" + string.Format("{0}", i + 1) + newline.ToString();
                }
                strCmd += "@";
                retString = strCmd;
                resEGYD = strCmd;
                bFlag = true;
            }
            catch(Exception ex)
            {
                retString = resEGYD = "";
                errorString = "�д��󣬴�����Ϊ�� " + ex.Message;
                bFlag = false;
            }
        }
        private void BAOXIAN()
        {
            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length != 3) throw new Exception("5");                                 //����5
                retString = arr[1] + "-" + arr[2];
                if (gb.gbHtPNR_BAOXIAN.Contains(arr[1].ToUpper()))
                    gb.gbHtPNR_BAOXIAN[arr[1].ToUpper()] = arr[2];
                else
                    YzpBtoC.gb.gbHtPNR_BAOXIAN.Add(arr[1].ToUpper(), arr[2]);
                bFlag = true;
                retString = "��PNR:" + arr[1].ToUpper() + "�ı��ո�Ϊ" + arr[2];
            }
            catch(Exception ex)
            {
                bFlag = false;
                errorString = "�д��󣬴�����Ϊ�� " + ex.Message;
            }
        }
        //SubmitPnrBtoC dlg = new SubmitPnrBtoC();
        private void TJDD()
        {//TJDD PGBWG ADDR���人�����·188�� FEE��5
            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                string[] sp2 = new string[] {"TJDD","ADDR","FEE" };
                string[] args = cmdex.ToUpper().Split(sp2, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length < 2) throw new Exception("11");
                gb.tjddPnr = arr[1];
                //string sendAddr = "";
                //string sendFee = "";
                //string temp = "";
                //for (int i = 2; i < arr.Length; i++)
                //{
                //    if (arr[i].ToUpper() == "ADDR")
                //    {
                //        sendFee = temp;
                //        temp = "";
                //    }
                //    else if (arr[i].ToUpper() == "FEE")
                //    {
                //        sendAddr = temp;
                //        temp = "";
                //    }
                //    else
                //    {
                //        temp += arr[i] + " ";
                //    }
                //}
                try
                {
                    gb.tjddAddr = args[1];

                    gb.tjddFee = args[2];
                }
                catch
                {
                }
                
                //dlg.ShowDialog();                     ���ﲻ��ʾ����Ҫ���ú������ύPNR�Ի���Ҳ�������������������
                
                //gb.tjddFee = "";
                //gb.tjddAddr = "";
                //gb.tjddPnr = "";
                retString = cmdex;
                bFlag = true;
            }
            catch (Exception ex)
            {
                bFlag = false;
                errorString = "�д��󣬴�����Ϊ�� " + ex.Message;
            }
        }

        private void KHZLMC()
        {
            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length != 2) throw new Exception("6");                                 //����6

                resKHZLMC = ws.GetCustomerByName(arr[1].Trim());
                if (resKHZLMC.ToLower() == "none" || resKHZLMC.ToLower() == "failed") throw new Exception("�ͻ����ϲ�����!");
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(resKHZLMC);
                XmlNode xn = xd.SelectSingleNode("Customers");
                retString = "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    retString += xn.ChildNodes[i].SelectSingleNode("CustomerID").InnerText.Trim() + " "
                        + xn.ChildNodes[i].SelectSingleNode("CustomerName").InnerText.Trim() + " "
                        + xn.ChildNodes[i].SelectSingleNode("Mobile").InnerText.Trim() + " "
                        + xn.ChildNodes[i].SelectSingleNode("CardID").InnerText.Trim() + newline.ToString();
                }
                bFlag = true;
            }
            catch (Exception ex)
            {
                bFlag = false;
                errorString = "�д��󣬴�����Ϊ�� " + ex.Message;
            }
        }

        private void KHZLDH()
        {
            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length != 2) throw new Exception("8");                                 //����8

                resKHZLLD = ws.GetCustomerByPhone(arr[1].Trim());
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(resKHZLLD);
                XmlNode xn = xd.SelectSingleNode("//Customer/Passengers");
                retString = "";
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    retString += xn.ChildNodes[i].SelectSingleNode("PassengerID").InnerText.Trim() + " "
                          + xn.ChildNodes[i].SelectSingleNode("PassengerName").InnerText.Trim() + " "
                          + xn.ChildNodes[i].SelectSingleNode("CardID").InnerText.Trim() + newline.ToString();
                }
                bFlag = true;
            }
            catch (Exception ex)
            {
                bFlag = false;
                errorString = "�д��󣬴�����Ϊ�� " + ex.Message;
            }
        }

        private void KHWXBH()
        {
            try
            {
                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length < 3) throw new Exception("10");                                   //����10

                bool r = true;// ws.InputCustomerQuery(int.Parse(arr[1]), cmdex.Substring(7 + arr[1].Length + 1));
                if (!r) throw new Exception("10.1");
                bFlag = true;
                retString = "����ɹ���\r";
            }
            catch
            {
                bFlag = false;
                retString = "����ʧ�ܣ�\r";
            }
        }

        public void NM()//������NM��ʱ����Ƿ�NM1��NM
        {
            try
            {
                //����Ӣ�������д��ո�������Ҫ�������ʱ����һ��С���ŰѸ����ְ���������
                //ͬʱΪ���㴦��nm ��չָ�������н��Ὣ�����еĿո�ת�����»��� //added by chenqj
                string cmdexBak = cmdex.Clone().ToString();//����
                int intStart = cmdex.IndexOf('(');
                int intEnd = cmdex.IndexOf(')');
                if (intEnd > intStart)
                {
                    string nameSpecial = cmdex.Substring(intStart + 1, intEnd - intStart - 1);
                    string nameSpecialBak = nameSpecial.Clone().ToString();
                    nameSpecial = nameSpecial.Replace("��", "-");//ȫ�ǵĺ��
                    nameSpecial = nameSpecial.Replace(' ', '_');
                    cmdex = cmdex.Replace(nameSpecialBak, nameSpecial);
                }

                string[] arr = cmdex.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                if (arr[0].ToUpper() != "NM")
                {
                    bFlag = false;
                    return;////////////////////////////////////�������NMָ��ʲô������
                }
                if (arr.Length < 2) throw new Exception("NM3");
                List<string> lsNM = new List<string>();

                if (resKHZLLD == "")
                {
                    //XmlDocument xd = new XmlDocument();

                    //if (string.IsNullOrEmpty(Options.GlobalVar.B2CCallingXml))
                    //{
                    //    throw new Exception("û�м�⵽�ͻ������¼������ʹ�� nm ָ�����չ��ʽ������ʹ�ñ�׼��ʽ��");
                    //}
                    //else
                    {
                        string strXml = Options.GlobalVar.B2CCallingXml;

                        //try
                        //{
                        //    xd.LoadXml(strXml);
                        //}
                        //catch (Exception ee)
                        //{
                        //    throw new Exception("��ȡ�����¼ʧ�ܣ�" + Environment.NewLine + ee.Message);
                        //}

                        //if (xd.SelectSingleNode("NewCustomer") != null)
                        //{
                        //    throw new Exception("��ǰ����Ϊ�¿ͻ����뱣��ÿͻ���Ϣ�������޷�ʹ�� nm ָ�����չ��ʽ��");
                        //}
                        //else
                            resKHZLLD = strXml;
                    }
                }

                DataEntity.XMLSchema.Customers customer = new DataEntity.XMLSchema.Customers();
                if(!string.IsNullOrEmpty(resKHZLLD))
                    customer = DataEntity.XMLSchema.xml_BaseClass.LoadXml<DataEntity.XMLSchema.Customers>(resKHZLLD);

                for (int i = 1; i < arr.Length; i++)
                {
                    string[] nc = arr[i].Split('-');
                    if (nc.Length == 1)//Ϊ���
                    {
                        foreach (DataEntity.XMLSchema.Passenger passenger in customer.Passengers)
                        {
                            if (passenger.fet_ID == arr[i].Trim())
                            {
                                lsNM.Add(passenger.fet_Name + "-" + passenger.fet_CardID1);
                                break;
                            }
                        }
                    }
                    else if (nc.Length == 2)//����-���֤
                    {
                        //lsNM.Add(arr[i]);
                        string name = arr[i].Replace('_', ' ');
                        name = name.Replace("(", string.Empty);
                        name = name.Replace(")", string.Empty);
                        lsNM.Add(name);
                    }
                }

                mystring.sortStringListByPinYin(lsNM);
                Options.GlobalVar.PassengersArray = lsNM.ToArray();

                retString = "nm";
                for (int i = 0; i < lsNM.Count; i++)
                {
                    retString += "1" + lsNM[i].Split('-')[0];
                    
                }
                for (int i = 0; i < lsNM.Count; i++)
                {
                    retString += "\r\nssr foid yy hk/ni" + lsNM[i].Split('-')[1] + string.Format("/p{0}", i + 1);
                }
                bFlag = true;
                cmdex = cmdexBak;//added by chenqj
            }
            catch (Exception ex)
            {
                retString = "";
                bFlag = false;
                errorString = "�д��󣬴�����Ϊ�� " + ex.Message;
            }
        }

        public bool IsEgExCmd()
        {
            cmdex = cmdex.ToUpper();
            bEgExCmd = true;
            if (cmdex.ToUpper().IndexOf("KHZLLD") == 0 || cmdex.ToUpper().IndexOf("KLD") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("KHZLMCXX") == 0 || cmdex.ToUpper().IndexOf("KXX") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("KHZLMC") == 0 || cmdex.ToUpper().IndexOf("KMC") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("KHZLDH") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("KHZLBH") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("KHWXLD") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("KHWXBH") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("TJDD") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("BAOXIAN") == 0)
            {
            }
            else if (cmdex.ToUpper().IndexOf("EGYD") == 0)
            {
            }
            else bEgExCmd = false;
            return bEgExCmd;
        }
        public void ExeCmds(IAsyncResult ar)
        {
            cmdex = cmdex.ToUpper();
            bEgExCmd = true;
            retString = "";
            if (cmdex.ToUpper().IndexOf("KHZLLD") == 0 || cmdex.ToUpper().IndexOf("KLD") == 0)
            {
                KHZLLD();
            }
            else if (cmdex.ToUpper().IndexOf("KHZLMC") == 0 || cmdex.ToUpper().IndexOf("KMC") == 0)
            {
                KHZLMC();
            }
            else if (cmdex.ToUpper().IndexOf("KHZLDH") == 0)
            {
                KHZLDH();
            }
            else if (cmdex.ToUpper().IndexOf("KHWXBH") == 0)
            {
                KHWXBH();
            }
            else if (cmdex.ToUpper().IndexOf("TJDD") == 0)
            {
                TJDD();
            }
            else if (cmdex.ToUpper().IndexOf("BAOXIAN") == 0)
            {
                BAOXIAN();
            }
            else if (cmdex.ToUpper().IndexOf("EGYD") == 0)
            {
                EGYD();
            }
            else bEgExCmd = false;
            //return "";
        }
        public string asyncRun()
        {
            AsyncCallback mycb = new AsyncCallback(ExeCmds);
            mydelegate md = new mydelegate(getResult);
            IAsyncResult ar = md.BeginInvoke(mycb, null);
            ar.AsyncWaitHandle.WaitOne();
            return md.EndInvoke(ar);
        }
        public string getResult()
        {
            return retString;
        }
    }
    public delegate string mydelegate();
}
