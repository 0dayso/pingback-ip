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
    class YzpbtocFunc
    {
        public YzpbtocFunc(string ipORdns)
        {
            wsAddr = ipORdns;
        }
        private string wsAddr = "";
        private void handle_exception(Exception ex, string wsCmd)
        {
            MessageBox.Show(wsCmd + ":" + ex.Message);
        }

        public void NewTicketsOrder(
            int userid,

            string pnr, 
            string tl, 
            string remark, 
            string names, 
            string phone, 
            string personcount,
            string[] flightno, 
            string[] bunk, 
            string[] date, 
            string[] citypair,
            string fareTkt, 
            string fareReal, 
            string taxBuild, 
            string taxFuel, 
            string usergain, 
            string lirun, 
            string fareTotal,

            ref bool IsSucceed
            )
        {
            string xml1 = "<TicketOrderCode>#��������#</TicketOrderCode><CustomerID>#�ͻ�ID#</CustomerID><DepartmentID>#Ʊ��˾ID#</DepartmentID><ReturnMoneyType>#��ٸ��ʽ#</ReturnMoneyType><CAACOrderCode>#�񺽶�����#</CAACOrderCode><IsAccumulate>#�Ƿ�������#</IsAccumulate><ContactorName>#��ϵ��#</ContactorName><ContactorMobile>#��ϵ���ֻ�#</ContactorMobile><ContactorTel>#��ϵ�˵绰#</ContactorTel><ContactorEmail>#��ϵ�˵����ʼ�#</ContactorEmail><DeliveryTypeID>#��Ʊ��ʽ#</DeliveryTypeID><DeliveryAddr>#��Ʊ��ַ#</DeliveryAddr><DeliveryDeadlineTime>#��Ʊ����ʱ��#</DeliveryDeadlineTime><DeliveryDeparmentID>#��Ʊ����#</DeliveryDeparmentID><PaymentTypeID>#���㷽ʽ#</PaymentTypeID><OperateTypeID>1</OperateTypeID><InsuranceTypeID>0</InsuranceTypeID><OrderType>0</OrderType><Remark>#��ע#</Remark>";

            xml1 = xml1.
                Replace("#��������#", pnr).
                Replace("#�ͻ�ID#", "0000").
                Replace("#Ʊ��˾ID#", "0").
                Replace("#��ٸ��ʽ#", "0").
                Replace("#�񺽶�����#", "0").
                Replace("#�Ƿ�������#", "0").
                Replace("#��ϵ��#", "��ϵ��").
                Replace("#��ϵ���ֻ�#", "13888888888").
                Replace("#��ϵ�˵绰#", "88888888").
                Replace("#��ϵ�˵����ʼ�#", "a@b.c").
                Replace("#��Ʊ��ʽ#", "0").
                Replace("#��Ʊ��ַ#", "ĳ��ĳ·ĳ��").
                Replace("#��Ʊ����ʱ��#", "2008-8-8").
                Replace("#��Ʊ����#", "1").
                Replace("#���㷽ʽ#", "0").
                Replace("#��ע#", remark);

            string xmltemp = "<Airline><AirlineCode>#�����#</AirlineCode><TakeOffDay>#�������#</TakeOffDay><PositionsType>#��λ����#</PositionsType><AirportTax>#����˰#</AirportTax><OilTax>#ȼ��˰#</OilTax><TakeOffTime>#���ʱ��#</TakeOffTime><ArrivedTime>#����ʱ��#</ArrivedTime><TakeOffCity>#��ɳ���#</TakeOffCity><ArrivedCity>#�������#</ArrivedCity><PlanType>#����#</PlanType><StopStation>#StopStation#</StopStation><AirLineCorpID>#���չ�˾ID#</AirLineCorpID><SalePrice>#SalePrice#</SalePrice><AirLineIndex>#AirLineIndex#</AirLineIndex></Airline>";
            string xml2 = "<Airlines>";
            for (int i = 0; i < flightno.Length; i++)
            {
                if (flightno[i].Trim() == "") continue;
                if (i == 0)//ֻ��һ��˰���۸�
                {
                    xml2 += xmltemp.
                        Replace("#�����#", flightno[i]).
                        Replace("#�������#", date[i].Trim().Split(' ')[0]).
                        Replace("#��λ����#", bunk[i]).
                        Replace("#����˰#", taxBuild).
                        Replace("#ȼ��˰#", taxFuel).
                        Replace("#���ʱ��#", "0:00:00").
                        Replace("#����ʱ��#", "0:00:00").
                        Replace("#��ɳ���#", citypair[i].Substring(0, 3)).
                        Replace("#�������#", citypair[i].Substring(3)).
                        Replace("#����#", "").
                        Replace("#StopStation#", "").
                        Replace("#���չ�˾ID#", "0").
                        Replace("#SalePrice#", fareReal).
                        Replace("#AirLineIndex#", "0");
                }
                else
                {
                    xml2 += xmltemp.
                        Replace("#�����#", flightno[i]).
                        Replace("#�������#", date[i]).
                        Replace("#��λ����#", bunk[i]).
                        Replace("#����˰#", "0").
                        Replace("#ȼ��˰#", "0").
                        Replace("#���ʱ��#", "").
                        Replace("#����ʱ��#", "").
                        Replace("#��ɳ���#", citypair[i].Substring(0, 3)).
                        Replace("#�������#", citypair[i].Substring(3)).
                        Replace("#����#", "").Replace("#StopStation#", "").
                        Replace("#���չ�˾ID#", "").
                        Replace("#SalePrice#", "0").
                        Replace("#AirLineIndex#", "");

                }
            }
            xml2 += "</Airlines>";

            xmltemp = "<Ticket><TicketType>#�˿�����#</TicketType><PassengerName>#�˿�����#</PassengerName><CardID>#֤������#</CardID><CardType>#֤������#</CardType><TicketNo>#Ʊ��#</TicketNo><PaymentPrice>#�����#</PaymentPrice><SalePrice>#���ۼ�#</SalePrice><HandBackPrice>#��Ʊ��#</HandBackPrice><InsuranceType>#���ղ�Ʒ����#</InsuranceType><InsuranceBasicPrice>#���յ׼�#</InsuranceBasicPrice><InsuranceSalePrice>#�����ۼ�#</InsuranceSalePrice><IsFreeInsurance>#�Ƿ����ͱ���#</IsFreeInsurance><Status>#Ʊ��״̬#</Status><TicketConfigID>#��������ID#</TicketConfigID><IsBuy>#IsBuy#</IsBuy><IsEspecial>#IsEspecial#</IsEspecial></Ticket>";
            string xml3 = "<Tickets>";
            string[] arrName = names.Split(';');
            for (int i = 0; i < arrName.Length; i++)
            {
                xml3 += xmltemp.
                    Replace("#�˿�����#", arrName[i].IndexOf("(CHD)") > 0 ? "2" : "1").
                    Replace("#�˿�����#", arrName[i].Split('-')[0]).
                    Replace("#֤������#", arrName[i].Split('-')[1]).
                    Replace("#֤������#", "1").
                    Replace("#Ʊ��#", "0").
                    Replace("#�����#", fareReal).
                    Replace("#���ۼ�#", fareTkt).
                    Replace("#��Ʊ��#", "0").
                    Replace("#���ղ�Ʒ����#", "0").
                    Replace("#���յ׼�#", "20").
                    Replace("#�����ۼ�#", "20").
                    Replace("#�Ƿ����ͱ���#", "0").
                    Replace("#Ʊ��״̬#", "0").
                    Replace("#��������ID#", "0").
                    Replace("#IsBuy#", "1").
                    Replace("#IsEspecial#", "0");
            }
            xml3 += "</Tickets>";

            string xml = "<TicketOrder>" + xml1 + xml2 + xml3 + "</TicketOrder>";

            string wsCmd = "NewTicketsOrder";
            try
            {
                wsYzpbtoc tkt = new wsYzpbtoc(wsAddr);
                int ii = tkt.NewTicketsOrder(xml, userid, userid);
                switch (ii)
                {
                    case 1:
                        MessageBox.Show("�������ύ�ɹ�!");
                        IsSucceed = true;
                        return;
                    case -1:
                        throw new Exception("XML������!");
                    case -2:
                        throw new Exception("��������ԱID������!");
                    case -3:
                        throw new Exception("��������ԱID������!");
                    case -4:
                        throw new Exception("���������ݿⷢ������!");

                }
            }
            catch (Exception ex)
            {
                handle_exception(ex, wsCmd);
            }
            IsSucceed = false;
        }
    }
}
