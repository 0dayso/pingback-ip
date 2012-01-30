using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DataEntity.XMLSchema;

namespace YzpBtoC
{
    public partial class SubmitPnrBtoC : Form
    {
        EagleWebService.wsYzpbtoc ws = new EagleWebService.wsYzpbtoc();
        public SubmitPnrBtoC()
        {
            InitializeComponent();
        }
        int peopleCount = 0;
        public SubmitPnrBtoC(YzpBtoC.YZPnewOrder no)
        {
            InitializeComponent();
            //LEFT
            try
            {
                txtPnr.Text = no.lPNR;
                txtCustomID.Text = (no.lCUSTOMID == "" ? "0" : no.lCUSTOMID);
                txtAgentID.Text = (no.lAGENTID == "" ? "0" : no.lAGENTID);
                txtFanyong.Text = txtFanyong.Items[0].ToString();// no.lFANYONG;//返佣方式，默认不返
                txtHXorderNo.Text = no.lPNR;//no.lHXORDERNO;
                cbJiFen.Text = no.lISJIFEN;
                txtTouchName.Text = no.lTOUCHNAME;
                txtTouchMobile.Text = no.lTOUCHMOBILE;
                txtTouchPhone.Text = no.lTOUCHPHONE;
                txtTouchEmail.Text = no.lTOUCHEMAIL;
                //cbSendTicketMethod.Text = cbSendTicketMethod.Items[0].ToString();//no.lSENDMETHOD;
                txtSendAddress.Text = no.lSENDADDRESS;
                try
                {
                    dtpSend.Value = DateTime.Parse(no.lSENDSTOPTIME);
                }
                catch
                {
                    dtpSend.Value = DateTime.Now;
                }
                txtSenderPart.Text = no.lSENDPART;
                txtSenderPart.ReadOnly = true;
                cbJieSuanMethod.Text = no.lJIESUANMETHOD;
                cbOperationType.Text = no.lOPERATIONTYPEID;
                //cbInsuraceTypeID.Text = (no.lINSURANCETYPEID == "" ? cbInsuraceTypeID.Items[0].ToString() : no.lINSURANCETYPEID);
                //cbOrderType.Text = (no.lORDERTYPE == "" ? cbOrderType.Items[0].ToString() : no.lORDERTYPE);
                txtRemark.Text = no.lREMARK;

                //RIGHT TOP
                for (int i = 0; i < no.rFLIGHTINFO.Length; i++)
                {
                    int fix = i * 14 + 13;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rFLIGHTNO;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rFLIGHTDATE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rBUNK + no.rFLIGHTINFO[i].rDisCount;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rTAXBUILD;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rTAXOIL;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rTIMELEAVE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rTIMEARRIVE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rCITYLEAVE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rCITYARRIVE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rPLANETYPE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rSTOPSTATION;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rAIRLINEID;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rSALEPRICE;
                    groupBox2.Controls["textBox" + string.Format("{0}", (fix++))].Text = no.rFLIGHTINFO[i].rAIRLINEINDEX;

                }
                //RIGHT BOTTOM
                dataGridView1.Rows.Clear();
                peopleCount = no.rPASSINFO.Length;
                for (int i = 0; i < peopleCount; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(dataGridView1);
                    int col = 0;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bPASSTYPE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bPASSNAME;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bCARDNO;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bCARDTYPE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bTKTNO;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bPAYPRICE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bSALEPRICE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bHANDBACKPRICE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bINSURANCETYPE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bINSURANCEBASICBPRICE;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bINSURANCESALEPRICE;
                    dr.Cells[col++].Value = (no.rPASSINFO[i].bINSURANCEISFREE == "1" ? true : false);
                    dr.Cells[col++].Value = no.rPASSINFO[i].bINSURANCESTATUS;
                    dr.Cells[col++].Value = no.rPASSINFO[i].bTICKETCONFIGID;
                    dr.Cells[col++].Value = (no.rPASSINFO[i].bISBUY == "1" ? true : false);
                    dr.Cells[col++].Value = (no.rPASSINFO[i].bISESPECIAL == "1" ? true : false);
                    dr.Cells[col++].Value = no.rPASSINFO[i].bBackHandMoney;
                    dr.Cells[col++].Value = "0";// no.rPASSINFO[i].bTaxTotal;
                    dataGridView1.Rows.Add(dr);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("B2C订单提交初始化时发生错误：\n" + ex.Message);
            }
        }
        
        private void SubmitPnrBtoC_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (txtCustomID.Text == "0" )
                {
                    try//取CALLXML中的
                    {
                        string strXml = Options.GlobalVar.B2CCallingXml;
                        DataEntity.XMLSchema.Customers customer = DataEntity.XMLSchema.xml_BaseClass.LoadXml<DataEntity.XMLSchema.Customers>(strXml);

                        txtCustomID.Text = customer.CustomerID + ":" + customer.CustomerName;
                        txtTouchName.Text = customer.CustomerName;
                        //txtTouchPhone.Text = xd1.SelectSingleNode("//Customer/Tel").InnerText.Trim();
                        txtTouchMobile.Text = customer.Mobiles[0];
                        txtTouchEmail.Text = customer.Email;
                    }
                    catch
                    {
                        MessageBox.Show("未检测到来电信息，订单将被关联至默认客户。");
                    }
                }
                //TJDD PNR ADDR *** FEE *
                if (gb.tjddPnr != "")
                {
                    this.txtPnr.Text = gb.tjddPnr;
                    gb.tjddPnr = "";
                }
                if (gb.tjddAddr != "")
                {
                    this.txtSendAddress.Text = gb.tjddAddr;
                    gb.tjddAddr = "";
                }
                if (gb.tjddFee != "")
                {
                    this.txtOtherFee.Text = gb.tjddFee;
                    gb.tjddFee = "";
                }

                Application.DoEvents();

                //保险类型 PNR * 暂时不需要 commentted by chenqj
                //string xml = ws.GetInsuranceType();
                //List<DataEntity.XMLSchema.InsuranceType> insuranceTypeArray =
                //    DataEntity.XMLSchema.xml_BaseClass.LoadArrayOfXml<DataEntity.XMLSchema.InsuranceType>(xml);

                //if (gb.gbYzpInsuranceType == null)
                //{
                //    cbInsuraceTypeID.Items.Clear();
                //    gb.gbYzpInsuranceType = new string[insuranceTypeArray.Count];
                //    int i = 0;

                //    foreach (DataEntity.XMLSchema.InsuranceType type in insuranceTypeArray)
                //    {
                //        gb.gbYzpInsuranceType[i] =
                //            type.InsuranceTypeID + "~";
                //        gb.gbYzpInsuranceType[i] +=
                //            type.InsuranceTypeName + "~";
                //        gb.gbYzpInsuranceType[i] +=
                //            type.BasicPrice + "~";
                //        gb.gbYzpInsuranceType[i] +=
                //            type.SalePrice;

                //        i++;
                //        cbInsuraceTypeID.Items.Add(type.InsuranceTypeID + ":" + type.InsuranceTypeName);
                //    }
                //}

                //cbInsuraceTypeID.Sorted = true;
                //cbInsuraceTypeID.SelectedIndex = 0;

                //if (gb.gbHtPNR_BAOXIAN.Contains(this.txtPnr.Text.ToUpper()))
                //    cbInsuraceTypeID.SelectedIndex = int.Parse((string)gb.gbHtPNR_BAOXIAN[this.txtPnr.Text.ToUpper()]) - 1;

                //业务员ID 暂时不需要 commentted by chenqj
                //string xml = ws.GetClearUsers(int.Parse(this.txtAgentID.Text.Trim().Split(':')[0]));
                //if (xml.ToLower() == "failed") throw new Exception("获取业务员列表(部门)发生错误！[WebService:GetClearUsers]");

                //List<DataEntity.XMLSchema.ClearUser> clearUserArray =
                //    DataEntity.XMLSchema.xml_BaseClass.LoadArrayOfXml<DataEntity.XMLSchema.ClearUser>(xml);
                //cbClearID.Items.Clear();
                //foreach (DataEntity.XMLSchema.ClearUser clearUser in clearUserArray)
                //{
                //    cbClearID.Items.Add(clearUser.UserID + ":" + clearUser.RealName);
                //}

                cbClearID.Items.Insert(0, "-1:无");
                cbClearID.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            if (Options.GlobalVar.B2CIsAutoSubmit)
            {
                this.btOrderAdd_Click(null, null);
                this.Close();
            }
        }

        private void btOrderSearch_Click(object sender, EventArgs e)
        {
            //ws.QueryTicketsOrder(
        }

        private void btOrderAdd_Click(object sender, EventArgs e)
        {
            DataEntity.XMLSchema.TicketOrder order = new DataEntity.XMLSchema.TicketOrder();
            order.TicketOrderCode = txtPnr.Text;//订单编码
            order.CustomerID = txtCustomID.Text.Split (':')[0];//客户ID
            order.DepartmentID = txtAgentID.Text.Split(':')[0];//票务公司ID
            order.ReturnMoneyType = txtFanyong.Text.Split(':')[0];//返俑方式
            order.CAACOrderCode = txtHXorderNo.Text;//民航订单号
            order.IsAccumulate = cbJiFen.Checked ? "1" : "0";//是否参与积分
            order.ContactorName = txtTouchName.Text;//联系人
            order.ContactorMobile = txtTouchMobile.Text;//联系人手机
            order.ContactorTel = txtTouchPhone.Text;//联系人电话
            order.ContactorEmail = txtTouchEmail.Text;//联系人电子邮件
            order.DeliveryTypeID = cbSendTicketMethod.Text.Split(':')[0];//送票方式
            order.DeliveryAddr = txtSendAddress.Text;//送票地址
            order.DeliveryDeadlineTime = dtpSend.Value.ToString();//送票截至时间
            order.DeliveryDeparmentID = txtSenderPart.Text.Split (':')[0];//送票机构
            order.PaymentTypeID = cbJieSuanMethod.Text.Split(':')[0];//结算方式
            order.OperateTypeID = this.cbOperationType.Text.Split(':')[0];//操作类型ID
            order.InsuranceTypeID = this.cbInsuraceTypeID.Text.Split(':')[0];//保险类型ID
            order.OrderType = this.cbOrderType.Text.Split(':')[0];//订单类型
            order.Remark = txtRemark.Text;//备注
            order.LinkPRN = txtPnr2.Text == "多个逗号隔开" ? "" : txtPnr2.Text;//关联PNR
            order.AttachMoney = txtOtherFee.Text;//杂费

//            string xml1 = @"<TicketOrderCode>#订单编码#</TicketOrderCode><CustomerID>#客户ID#</CustomerID><DepartmentID>#票务公司ID#</DepartmentID>
//                <ReturnMoneyType>#返俑方式#</ReturnMoneyType><CAACOrderCode>#民航订单号#</CAACOrderCode><IsAccumulate>#是否参与积分#</IsAccumulate>
//                <ContactorName>#联系人#</ContactorName><ContactorMobile>#联系人手机#</ContactorMobile><ContactorTel>#联系人电话#</ContactorTel>
//                    <ContactorEmail>#联系人电子邮件#</ContactorEmail><DeliveryTypeID>#送票方式#</DeliveryTypeID><DeliveryAddr>#送票地址#</DeliveryAddr>
//                        <DeliveryDeadlineTime>#送票截至时间#</DeliveryDeadlineTime><DeliveryDeparmentID>#送票机构#</DeliveryDeparmentID>
//                            <PaymentTypeID>#结算方式#</PaymentTypeID><OperateTypeID>#操作类型ID#</OperateTypeID>
//                                <InsuranceTypeID>#保险类型ID#</InsuranceTypeID><OrderType>#订单类型#</OrderType><Remark>#备注#</Remark>
//                                    <LinkPRN>#关联PNR#</LinkPRN><AttachMoney>#杂费#</AttachMoney>";

//            string xmltemp = @"<Airline><AirlineCode>#航班号#</AirlineCode><TakeOffDay>#起飞日期#</TakeOffDay><PositionsType>#舱位类型#</PositionsType>
//                <AirportTax>#机场税#</AirportTax><OilTax>#燃油税#</OilTax><TakeOffTime>#起飞时间#</TakeOffTime><ArrivedTime>#到达时间#</ArrivedTime>
//<TakeOffCity>#起飞城市#</TakeOffCity><ArrivedCity>#到达城市#</ArrivedCity><PlanType>#机型#</PlanType><StopStation>#StopStation#</StopStation>
//<AirLineCorpName>#航空公司名称#</AirLineCorpName><SalePrice>#SalePrice#</SalePrice><AirLineIndex>#AirLineIndex#</AirLineIndex>
//<DisCount>#折扣#</DisCount></Airline>";
//            string xml2 = "<Airlines>";

            for (int i = 0; i < 10; i++)
            {
                int no = i*14+13;
                if (groupBox2.Controls["textBox" + no.ToString()].Text.Trim() == "") continue;

                DataEntity.XMLSchema.Airline airline = new DataEntity.XMLSchema.Airline();
                airline.AirlineCode = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//航班号
                airline.TakeOffDay = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//起飞日期
                airline.PositionsType = groupBox2.Controls["textBox" + string.Format("{0}", (no))].Text.Trim()[0].ToString();//舱位类型
                airline.DisCount = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim().Substring(1);//折扣
                airline.AirportTax = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//机场税
                airline.OilTax = "0"; no++;// groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//燃油税
                airline.TakeOffTime = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//起飞时间
                airline.ArrivedTime = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//到达时间
                airline.TakeOffCity = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//起飞城市
                airline.ArrivedCity = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//到达城市
                airline.PlanType = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//机型
                airline.StopStation = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//StopStation
                airline.AirLineCorpName = airline.AirlineCode.Substring(0, 2);//航空公司名称
                airline.SalePrice = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//SalePrice
                airline.AirLineIndex = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//AirLineIndex

                int index = airline.ArrivedTime.LastIndexOf(':');
                if (index > 0)
                    airline.ArrivedTime = airline.ArrivedTime.Substring(0, index);

                index = airline.TakeOffTime.LastIndexOf(':');
                if (index > 0)
                    airline.TakeOffTime = airline.TakeOffTime.Substring(0, index);

                order.Airlines.Add(airline);
            }

//            xml2 += "</Airlines>";

//            xmltemp = @"<Ticket><TicketType>#乘客类型#</TicketType><PassengerName>#乘客姓名#</PassengerName><CardID>#证件号码#</CardID>
//<CardType>#证件类型#</CardType><TicketNo>#票号#</TicketNo><PaymentPrice>#结算价#</PaymentPrice><SalePrice>#销售价#</SalePrice>
//<HandBackPrice>#退票费#</HandBackPrice><InsuranceType>#保险产品类型#</InsuranceType><InsuranceBasicPrice>#保险底价#</InsuranceBasicPrice>
//<InsuranceSalePrice>#保险售价#</InsuranceSalePrice><IsFreeInsurance>#是否赠送保险#</IsFreeInsurance><Status>#票的状态#</Status>
//<TicketConfigID>#政策配置ID#</TicketConfigID><IsBuy>#IsBuy#</IsBuy><IsEspecial>#IsEspecial#</IsEspecial><Backhander>#返利#</Backhander>
//<Tax>#税费#</Tax></Ticket>";
//            string xml3 = "<Tickets>";

            for (int i = 0; i < peopleCount ; i++)
            {
                DataEntity.XMLSchema.Ticket ticket = new DataEntity.XMLSchema.Ticket();

                ticket.TicketType = dataGridView1[0, i].Value.ToString().Split(':')[0];//乘客类型
                ticket.PassengerName = dataGridView1[1, i].Value.ToString();//乘客姓名
                ticket.CardID = dataGridView1[2, i].Value.ToString();//证件号码
                ticket.CardType = dataGridView1[3, i].Value.ToString().Split(':')[0];//证件类型
                ticket.TicketNo = dataGridView1[4, i].Value.ToString();//票号
                ticket.PaymentPrice = dataGridView1[5, i].Value.ToString();//结算价
                ticket.SalePrice = dataGridView1[6, i].Value.ToString();//销售价
                ticket.HandBackPrice = dataGridView1[7, i].Value.ToString();//退票费
                ticket.InsuranceType = dataGridView1[8, i].Value.ToString();//保险产品类型
                ticket.InsuranceBasicPrice = dataGridView1[9, i].Value.ToString();//保险底价
                ticket.InsuranceSalePrice = dataGridView1[10, i].Value.ToString();//保险售价
                ticket.IsFreeInsurance = (bool)dataGridView1[11, i].Value ? "1" : "0";//是否赠送保险
                ticket.Status = dataGridView1[12, i].Value.ToString();//票的状态
                ticket.TicketConfigID = dataGridView1[13, i].Value.ToString();//政策配置ID
                ticket.IsBuy = (bool)dataGridView1[14, i].Value ? "1" : "0";//IsBuy
                ticket.IsEspecial = (bool)dataGridView1[15, i].Value ? "1" : "0";//IsEspecial
                ticket.Backhander = (bool)dataGridView1[11, i].Value ? "0" : dataGridView1[16, i].Value.ToString();//返利
                ticket.Tax = dataGridView1[17, i].Value.ToString();//税费

                order.Tickets.Add(ticket);
            }
            //xml3 += "</Tickets>";
            //string xml = "<TicketOrder>" + xml1 + xml2 + xml3 + "</TicketOrder>";

            try
            {
                int clearid = int.Parse(cbClearID.Text.Split(':')[0]);
                string xml = new TXIO.XML_IO().SaveToText(order);
                int ret = ws.NewTicketsOrder(xml, int.Parse(Options.GlobalVar.B2CUserID), clearid);

                switch (ret)
                {
                    case -1:
                        throw new Exception("XML格式不正确!");
                    case -2:
                        throw new Exception("操作员不存在!");
                    case -3:
                        throw new Exception("业务员不存在!");
                    case -4:
                        throw new Exception("服务器数据库发生错误!");
                    case -5:
                        throw new Exception("PNR 重复（七天内），且该 PNR 对应订单不是废单！");
                    default:
                        Options.GlobalVar.B2CNewOrderID = ret;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提交订单", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbInsuraceTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cbInsuraceTypeID.SelectedIndex;
                for (int i = 0; i < peopleCount; i++)
                {
                    dataGridView1[8, i].Value = gb.gbYzpInsuranceType[index].Split('~')[1];
                    dataGridView1[9, i].Value = gb.gbYzpInsuranceType[index].Split('~')[2];
                    dataGridView1[10, i].Value = gb.gbYzpInsuranceType[index].Split('~')[3];
                }
            }
            catch
            {
            }
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();
        }

        private void cbSendTicketMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbSendTicketMethod.Text[0] == '1' || cbSendTicketMethod.Text[0] == '4')//送票上门,邮寄
                {
                    txtSendAddress.ReadOnly  = false;
                }
                else
                {
                    txtSendAddress.ReadOnly  = true;
                }
            }
            catch
            {
            }
        }

        private void cbSendTicketMethod_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtSenderPart_Enter(object sender, EventArgs e)
        {
            try
            {
                string departType = "0";
                if (cbSendTicketMethod.Text[0] == '2' )//机场取票
                {
                    departType = "0";
                }
                else //if (cbSendTicketMethod.Text[0] == '3' || cbSendTicketMethod.Text[0] == '1')
                {
                    departType = "1";
                }
                //else return;

                string xml = ws.GetAirportOrSalerooms(int.Parse(txtAgentID.Text.Split(':')[0]), int.Parse(departType));
                if (xml.ToLower() == "failed")
                {
                    txtSendAddress.Text = "未找到对应柜台";
                    return;
                }
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xml);
                List<string> arr = new List<string>();
                XmlNode xn;
                if (departType == "0")
                    xn = xd.SelectSingleNode("AirPorts");
                else
                    xn = xd.SelectSingleNode("Salerooms");
                for (int i = 0; i < xn.ChildNodes.Count; i++)
                {
                    arr.Add(xn.ChildNodes[i].SelectSingleNode("DepartmentID").InnerText
                    + ":" + xn.ChildNodes[i].SelectSingleNode("DepartmentName").InnerText);
                }
                if (xn.ChildNodes.Count == 1)
                {
                    txtSenderPart.Text = arr[0];//.Split(':')[0];
                    //txtSendAddress.Text = arr[0].Split(':')[1];//送票地址
                }
                else
                {
                    ListSelect dlg = new ListSelect(arr);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        txtSenderPart.Text = dlg.retString;//dlg.retString.Split(':')[0];
                        //txtSendAddress.Text = dlg.retString.Split(':')[1];//送票地址

                    }
                }
            }
            catch
            {
            }
        }

        private void btSelectSender_Click(object sender, EventArgs e)
        {
            txtSenderPart_Enter(null, null);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtPnr2_Enter(object sender, EventArgs e)
        {
            if (txtPnr2.Text == "多个逗号隔开") txtPnr2.Text = "";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


    }
}