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
                txtFanyong.Text = txtFanyong.Items[0].ToString();// no.lFANYONG;//��Ӷ��ʽ��Ĭ�ϲ���
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
                MessageBox.Show("B2C�����ύ��ʼ��ʱ��������\n" + ex.Message);
            }
        }
        
        private void SubmitPnrBtoC_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (txtCustomID.Text == "0" )
                {
                    try//ȡCALLXML�е�
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
                        MessageBox.Show("δ��⵽������Ϣ����������������Ĭ�Ͽͻ���");
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

                //�������� PNR * ��ʱ����Ҫ commentted by chenqj
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

                //ҵ��ԱID ��ʱ����Ҫ commentted by chenqj
                //string xml = ws.GetClearUsers(int.Parse(this.txtAgentID.Text.Trim().Split(':')[0]));
                //if (xml.ToLower() == "failed") throw new Exception("��ȡҵ��Ա�б�(����)��������[WebService:GetClearUsers]");

                //List<DataEntity.XMLSchema.ClearUser> clearUserArray =
                //    DataEntity.XMLSchema.xml_BaseClass.LoadArrayOfXml<DataEntity.XMLSchema.ClearUser>(xml);
                //cbClearID.Items.Clear();
                //foreach (DataEntity.XMLSchema.ClearUser clearUser in clearUserArray)
                //{
                //    cbClearID.Items.Add(clearUser.UserID + ":" + clearUser.RealName);
                //}

                cbClearID.Items.Insert(0, "-1:��");
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
            order.TicketOrderCode = txtPnr.Text;//��������
            order.CustomerID = txtCustomID.Text.Split (':')[0];//�ͻ�ID
            order.DepartmentID = txtAgentID.Text.Split(':')[0];//Ʊ��˾ID
            order.ReturnMoneyType = txtFanyong.Text.Split(':')[0];//��ٸ��ʽ
            order.CAACOrderCode = txtHXorderNo.Text;//�񺽶�����
            order.IsAccumulate = cbJiFen.Checked ? "1" : "0";//�Ƿ�������
            order.ContactorName = txtTouchName.Text;//��ϵ��
            order.ContactorMobile = txtTouchMobile.Text;//��ϵ���ֻ�
            order.ContactorTel = txtTouchPhone.Text;//��ϵ�˵绰
            order.ContactorEmail = txtTouchEmail.Text;//��ϵ�˵����ʼ�
            order.DeliveryTypeID = cbSendTicketMethod.Text.Split(':')[0];//��Ʊ��ʽ
            order.DeliveryAddr = txtSendAddress.Text;//��Ʊ��ַ
            order.DeliveryDeadlineTime = dtpSend.Value.ToString();//��Ʊ����ʱ��
            order.DeliveryDeparmentID = txtSenderPart.Text.Split (':')[0];//��Ʊ����
            order.PaymentTypeID = cbJieSuanMethod.Text.Split(':')[0];//���㷽ʽ
            order.OperateTypeID = this.cbOperationType.Text.Split(':')[0];//��������ID
            order.InsuranceTypeID = this.cbInsuraceTypeID.Text.Split(':')[0];//��������ID
            order.OrderType = this.cbOrderType.Text.Split(':')[0];//��������
            order.Remark = txtRemark.Text;//��ע
            order.LinkPRN = txtPnr2.Text == "������Ÿ���" ? "" : txtPnr2.Text;//����PNR
            order.AttachMoney = txtOtherFee.Text;//�ӷ�

//            string xml1 = @"<TicketOrderCode>#��������#</TicketOrderCode><CustomerID>#�ͻ�ID#</CustomerID><DepartmentID>#Ʊ��˾ID#</DepartmentID>
//                <ReturnMoneyType>#��ٸ��ʽ#</ReturnMoneyType><CAACOrderCode>#�񺽶�����#</CAACOrderCode><IsAccumulate>#�Ƿ�������#</IsAccumulate>
//                <ContactorName>#��ϵ��#</ContactorName><ContactorMobile>#��ϵ���ֻ�#</ContactorMobile><ContactorTel>#��ϵ�˵绰#</ContactorTel>
//                    <ContactorEmail>#��ϵ�˵����ʼ�#</ContactorEmail><DeliveryTypeID>#��Ʊ��ʽ#</DeliveryTypeID><DeliveryAddr>#��Ʊ��ַ#</DeliveryAddr>
//                        <DeliveryDeadlineTime>#��Ʊ����ʱ��#</DeliveryDeadlineTime><DeliveryDeparmentID>#��Ʊ����#</DeliveryDeparmentID>
//                            <PaymentTypeID>#���㷽ʽ#</PaymentTypeID><OperateTypeID>#��������ID#</OperateTypeID>
//                                <InsuranceTypeID>#��������ID#</InsuranceTypeID><OrderType>#��������#</OrderType><Remark>#��ע#</Remark>
//                                    <LinkPRN>#����PNR#</LinkPRN><AttachMoney>#�ӷ�#</AttachMoney>";

//            string xmltemp = @"<Airline><AirlineCode>#�����#</AirlineCode><TakeOffDay>#�������#</TakeOffDay><PositionsType>#��λ����#</PositionsType>
//                <AirportTax>#����˰#</AirportTax><OilTax>#ȼ��˰#</OilTax><TakeOffTime>#���ʱ��#</TakeOffTime><ArrivedTime>#����ʱ��#</ArrivedTime>
//<TakeOffCity>#��ɳ���#</TakeOffCity><ArrivedCity>#�������#</ArrivedCity><PlanType>#����#</PlanType><StopStation>#StopStation#</StopStation>
//<AirLineCorpName>#���չ�˾����#</AirLineCorpName><SalePrice>#SalePrice#</SalePrice><AirLineIndex>#AirLineIndex#</AirLineIndex>
//<DisCount>#�ۿ�#</DisCount></Airline>";
//            string xml2 = "<Airlines>";

            for (int i = 0; i < 10; i++)
            {
                int no = i*14+13;
                if (groupBox2.Controls["textBox" + no.ToString()].Text.Trim() == "") continue;

                DataEntity.XMLSchema.Airline airline = new DataEntity.XMLSchema.Airline();
                airline.AirlineCode = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//�����
                airline.TakeOffDay = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//�������
                airline.PositionsType = groupBox2.Controls["textBox" + string.Format("{0}", (no))].Text.Trim()[0].ToString();//��λ����
                airline.DisCount = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim().Substring(1);//�ۿ�
                airline.AirportTax = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//����˰
                airline.OilTax = "0"; no++;// groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//ȼ��˰
                airline.TakeOffTime = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//���ʱ��
                airline.ArrivedTime = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//����ʱ��
                airline.TakeOffCity = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//��ɳ���
                airline.ArrivedCity = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//�������
                airline.PlanType = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//����
                airline.StopStation = groupBox2.Controls["textBox" + string.Format("{0}", (no++))].Text.Trim();//StopStation
                airline.AirLineCorpName = airline.AirlineCode.Substring(0, 2);//���չ�˾����
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

//            xmltemp = @"<Ticket><TicketType>#�˿�����#</TicketType><PassengerName>#�˿�����#</PassengerName><CardID>#֤������#</CardID>
//<CardType>#֤������#</CardType><TicketNo>#Ʊ��#</TicketNo><PaymentPrice>#�����#</PaymentPrice><SalePrice>#���ۼ�#</SalePrice>
//<HandBackPrice>#��Ʊ��#</HandBackPrice><InsuranceType>#���ղ�Ʒ����#</InsuranceType><InsuranceBasicPrice>#���յ׼�#</InsuranceBasicPrice>
//<InsuranceSalePrice>#�����ۼ�#</InsuranceSalePrice><IsFreeInsurance>#�Ƿ����ͱ���#</IsFreeInsurance><Status>#Ʊ��״̬#</Status>
//<TicketConfigID>#��������ID#</TicketConfigID><IsBuy>#IsBuy#</IsBuy><IsEspecial>#IsEspecial#</IsEspecial><Backhander>#����#</Backhander>
//<Tax>#˰��#</Tax></Ticket>";
//            string xml3 = "<Tickets>";

            for (int i = 0; i < peopleCount ; i++)
            {
                DataEntity.XMLSchema.Ticket ticket = new DataEntity.XMLSchema.Ticket();

                ticket.TicketType = dataGridView1[0, i].Value.ToString().Split(':')[0];//�˿�����
                ticket.PassengerName = dataGridView1[1, i].Value.ToString();//�˿�����
                ticket.CardID = dataGridView1[2, i].Value.ToString();//֤������
                ticket.CardType = dataGridView1[3, i].Value.ToString().Split(':')[0];//֤������
                ticket.TicketNo = dataGridView1[4, i].Value.ToString();//Ʊ��
                ticket.PaymentPrice = dataGridView1[5, i].Value.ToString();//�����
                ticket.SalePrice = dataGridView1[6, i].Value.ToString();//���ۼ�
                ticket.HandBackPrice = dataGridView1[7, i].Value.ToString();//��Ʊ��
                ticket.InsuranceType = dataGridView1[8, i].Value.ToString();//���ղ�Ʒ����
                ticket.InsuranceBasicPrice = dataGridView1[9, i].Value.ToString();//���յ׼�
                ticket.InsuranceSalePrice = dataGridView1[10, i].Value.ToString();//�����ۼ�
                ticket.IsFreeInsurance = (bool)dataGridView1[11, i].Value ? "1" : "0";//�Ƿ����ͱ���
                ticket.Status = dataGridView1[12, i].Value.ToString();//Ʊ��״̬
                ticket.TicketConfigID = dataGridView1[13, i].Value.ToString();//��������ID
                ticket.IsBuy = (bool)dataGridView1[14, i].Value ? "1" : "0";//IsBuy
                ticket.IsEspecial = (bool)dataGridView1[15, i].Value ? "1" : "0";//IsEspecial
                ticket.Backhander = (bool)dataGridView1[11, i].Value ? "0" : dataGridView1[16, i].Value.ToString();//����
                ticket.Tax = dataGridView1[17, i].Value.ToString();//˰��

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
                        throw new Exception("XML��ʽ����ȷ!");
                    case -2:
                        throw new Exception("����Ա������!");
                    case -3:
                        throw new Exception("ҵ��Ա������!");
                    case -4:
                        throw new Exception("���������ݿⷢ������!");
                    case -5:
                        throw new Exception("PNR �ظ��������ڣ����Ҹ� PNR ��Ӧ�������Ƿϵ���");
                    default:
                        Options.GlobalVar.B2CNewOrderID = ret;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "�ύ����", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                if (cbSendTicketMethod.Text[0] == '1' || cbSendTicketMethod.Text[0] == '4')//��Ʊ����,�ʼ�
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
                if (cbSendTicketMethod.Text[0] == '2' )//����ȡƱ
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
                    txtSendAddress.Text = "δ�ҵ���Ӧ��̨";
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
                    //txtSendAddress.Text = arr[0].Split(':')[1];//��Ʊ��ַ
                }
                else
                {
                    ListSelect dlg = new ListSelect(arr);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        txtSenderPart.Text = dlg.retString;//dlg.retString.Split(':')[0];
                        //txtSendAddress.Text = dlg.retString.Split(':')[1];//��Ʊ��ַ

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
            if (txtPnr2.Text == "������Ÿ���") txtPnr2.Text = "";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


    }
}