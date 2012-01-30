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
            string xml1 = "<TicketOrderCode>#订单编码#</TicketOrderCode><CustomerID>#客户ID#</CustomerID><DepartmentID>#票务公司ID#</DepartmentID><ReturnMoneyType>#返俑方式#</ReturnMoneyType><CAACOrderCode>#民航订单号#</CAACOrderCode><IsAccumulate>#是否参与积分#</IsAccumulate><ContactorName>#联系人#</ContactorName><ContactorMobile>#联系人手机#</ContactorMobile><ContactorTel>#联系人电话#</ContactorTel><ContactorEmail>#联系人电子邮件#</ContactorEmail><DeliveryTypeID>#送票方式#</DeliveryTypeID><DeliveryAddr>#送票地址#</DeliveryAddr><DeliveryDeadlineTime>#送票截至时间#</DeliveryDeadlineTime><DeliveryDeparmentID>#送票机构#</DeliveryDeparmentID><PaymentTypeID>#结算方式#</PaymentTypeID><OperateTypeID>1</OperateTypeID><InsuranceTypeID>0</InsuranceTypeID><OrderType>0</OrderType><Remark>#备注#</Remark>";

            xml1 = xml1.
                Replace("#订单编码#", pnr).
                Replace("#客户ID#", "0000").
                Replace("#票务公司ID#", "0").
                Replace("#返俑方式#", "0").
                Replace("#民航订单号#", "0").
                Replace("#是否参与积分#", "0").
                Replace("#联系人#", "联系人").
                Replace("#联系人手机#", "13888888888").
                Replace("#联系人电话#", "88888888").
                Replace("#联系人电子邮件#", "a@b.c").
                Replace("#送票方式#", "0").
                Replace("#送票地址#", "某市某路某号").
                Replace("#送票截至时间#", "2008-8-8").
                Replace("#送票机构#", "1").
                Replace("#结算方式#", "0").
                Replace("#备注#", remark);

            string xmltemp = "<Airline><AirlineCode>#航班号#</AirlineCode><TakeOffDay>#起飞日期#</TakeOffDay><PositionsType>#舱位类型#</PositionsType><AirportTax>#机场税#</AirportTax><OilTax>#燃油税#</OilTax><TakeOffTime>#起飞时间#</TakeOffTime><ArrivedTime>#到达时间#</ArrivedTime><TakeOffCity>#起飞城市#</TakeOffCity><ArrivedCity>#到达城市#</ArrivedCity><PlanType>#机型#</PlanType><StopStation>#StopStation#</StopStation><AirLineCorpID>#航空公司ID#</AirLineCorpID><SalePrice>#SalePrice#</SalePrice><AirLineIndex>#AirLineIndex#</AirLineIndex></Airline>";
            string xml2 = "<Airlines>";
            for (int i = 0; i < flightno.Length; i++)
            {
                if (flightno[i].Trim() == "") continue;
                if (i == 0)//只算一次税及价格
                {
                    xml2 += xmltemp.
                        Replace("#航班号#", flightno[i]).
                        Replace("#起飞日期#", date[i].Trim().Split(' ')[0]).
                        Replace("#舱位类型#", bunk[i]).
                        Replace("#机场税#", taxBuild).
                        Replace("#燃油税#", taxFuel).
                        Replace("#起飞时间#", "0:00:00").
                        Replace("#到达时间#", "0:00:00").
                        Replace("#起飞城市#", citypair[i].Substring(0, 3)).
                        Replace("#到达城市#", citypair[i].Substring(3)).
                        Replace("#机型#", "").
                        Replace("#StopStation#", "").
                        Replace("#航空公司ID#", "0").
                        Replace("#SalePrice#", fareReal).
                        Replace("#AirLineIndex#", "0");
                }
                else
                {
                    xml2 += xmltemp.
                        Replace("#航班号#", flightno[i]).
                        Replace("#起飞日期#", date[i]).
                        Replace("#舱位类型#", bunk[i]).
                        Replace("#机场税#", "0").
                        Replace("#燃油税#", "0").
                        Replace("#起飞时间#", "").
                        Replace("#到达时间#", "").
                        Replace("#起飞城市#", citypair[i].Substring(0, 3)).
                        Replace("#到达城市#", citypair[i].Substring(3)).
                        Replace("#机型#", "").Replace("#StopStation#", "").
                        Replace("#航空公司ID#", "").
                        Replace("#SalePrice#", "0").
                        Replace("#AirLineIndex#", "");

                }
            }
            xml2 += "</Airlines>";

            xmltemp = "<Ticket><TicketType>#乘客类型#</TicketType><PassengerName>#乘客姓名#</PassengerName><CardID>#证件号码#</CardID><CardType>#证件类型#</CardType><TicketNo>#票号#</TicketNo><PaymentPrice>#结算价#</PaymentPrice><SalePrice>#销售价#</SalePrice><HandBackPrice>#退票费#</HandBackPrice><InsuranceType>#保险产品类型#</InsuranceType><InsuranceBasicPrice>#保险底价#</InsuranceBasicPrice><InsuranceSalePrice>#保险售价#</InsuranceSalePrice><IsFreeInsurance>#是否赠送保险#</IsFreeInsurance><Status>#票的状态#</Status><TicketConfigID>#政策配置ID#</TicketConfigID><IsBuy>#IsBuy#</IsBuy><IsEspecial>#IsEspecial#</IsEspecial></Ticket>";
            string xml3 = "<Tickets>";
            string[] arrName = names.Split(';');
            for (int i = 0; i < arrName.Length; i++)
            {
                xml3 += xmltemp.
                    Replace("#乘客类型#", arrName[i].IndexOf("(CHD)") > 0 ? "2" : "1").
                    Replace("#乘客姓名#", arrName[i].Split('-')[0]).
                    Replace("#证件号码#", arrName[i].Split('-')[1]).
                    Replace("#证件类型#", "1").
                    Replace("#票号#", "0").
                    Replace("#结算价#", fareReal).
                    Replace("#销售价#", fareTkt).
                    Replace("#退票费#", "0").
                    Replace("#保险产品类型#", "0").
                    Replace("#保险底价#", "20").
                    Replace("#保险售价#", "20").
                    Replace("#是否赠送保险#", "0").
                    Replace("#票的状态#", "0").
                    Replace("#政策配置ID#", "0").
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
                        MessageBox.Show("订单已提交成功!");
                        IsSucceed = true;
                        return;
                    case -1:
                        throw new Exception("XML串错误!");
                    case -2:
                        throw new Exception("参数操作员ID不存在!");
                    case -3:
                        throw new Exception("参数结算员ID不存在!");
                    case -4:
                        throw new Exception("服务器数据库发生错误!");

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
