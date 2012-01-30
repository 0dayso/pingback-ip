using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace EagleFinance
{
    public partial class search : Form
    {
        public search()
        {
            InitializeComponent();
        }

        private void search_Load(object sender, EventArgs e)
        {
            tbOrig.Enabled = tbDest.Enabled = false;
            cbGroupBy.Text = cbGroupBy.Items[0].ToString();
            cbOrderBy.Text = cbOrderBy.Items[0].ToString();
            cbSearchField.Text = cbSearchField.Items[0].ToString();
        }
        public string selString = "";
        private void btSearch_Click(object sender, EventArgs e)
        {
            selString = "select [ID],[OFFICE]," +
                "[TKTNUMBER] as Ʊ��," +            //2
                "[DATEOFINCOMING] as �������," +  //3
                "[E-TKT-NUMBER] as ����Ʊ��," +    //4
                "[ORIG-DEST] as ʼ��," +           //5
                "[COLLECTION] as Ʊ��," +          //6
                "[TAXS] as ˰," +                   //7
                "[COMM] as �׼۵��," +            //8
                "[PNR]," +                          //9
                "[receiptNumber] as �г̵���," +   //10
                "[DATEOFSALE] as ��������," +
                "[TKT-FLAG] as ״̬," +
                "[USERACCOUNT] as �û�," +
                "[USERNAME] as ������," +
                "[AGENTNAME] as ������," +         //15
                "[FLIGHTDATE] as �˻�����," +
                "[FLIGHTNUMBER] as �����," +
                "[FLIGHTBUNK] as ��λ," +
                "[FLIGHTCITY] as �ɵֳ���," +
                "[AGENTCOMM] as ������," +       //20
                "[IMPORTCOUNT] as �������" +
                " from etickets where 1=1";
            //selString = "select * from etickets where 1=1";
            string temp = "";
            #region ������ڣ���Ʊ���ڣ�ʮλƱ��
            if (cbIncomeDate.Checked)
            {
                temp = string.Format(" and (DATEOFINCOMING>=cdate('{0}') and DATEOFINCOMING<cdate('{1}'))",
                   dtpIncomeBeg.Value.ToShortDateString(),
                   dtpIncomeEnd.Value.AddDays(1).ToShortDateString());
                selString += temp;
            }
            if (cbSaleDate.Checked)
            {
                temp = string.Format(" and (DATEOFSALE>=cdate('{0}') and DATEOFSALE<cdate('{1}'))",
                     dtpSaleBeg.Value.ToShortDateString(),
                     dtpSaleEnd.Value.AddDays(1).ToShortDateString());
                selString += temp;
            }
            if (tbTicketNumberBeg.Text.Length == tbTicketNumberEnd.Text.Length && tbTicketNumberEnd.Text.Length == 10)
            {
                temp = string.Format(" and (TKTNUMBER>='{0}' and TKTNUMBER<='{1}')",
                    tbTicketNumberBeg.Text,
                    tbTicketNumberEnd.Text);
                selString += temp;
            }
            #endregion
            if (tbOrig.Text.Trim() == "" || tbOrig.Text.Trim() == "����ö��Ÿ���")
            {
            }
            if (tbDest.Text.Trim() == "" || tbDest.Text.Trim() == "����ö��Ÿ���")
            {
            }
            #region PNR
            if (!(tbPNR.Text.Trim() == "" || tbPNR.Text.Trim() == "����ö��Ÿ���"))
            {
                string[] pnrs = tbPNR.Text.Trim().Split(',');
                int count=0;
                string pnrTemp = "";
                for (int i = 0; i < pnrs.Length; i++)
                {
                    string tt = pnrs[i].Trim();
                    if (tt.Trim().Length != 5) continue;
                    count ++;
                    pnrTemp += string.Format(" or (PNR='{0}')", tt);
                }
                if (count > 0)
                {
                    temp = " and (1=0 " + pnrTemp + ")";
                    selString += temp;
                }
            }
            #endregion
            #region OFFICE��
            if (!(tbOffice.Text.Trim() == "" || tbOffice.Text.Trim() == "����ö��Ÿ���"))
            {
                string[] pnrs = tbOffice.Text.Trim().Split(',');
                int count = 0;
                string pnrTemp = "";
                for (int i = 0; i < pnrs.Length; i++)
                {
                    string tt = pnrs[i].Trim();
                    if (tt.Trim().Length != 6) continue;
                    count++;
                    pnrTemp += string.Format(" or (OFFICE='{0}')", tt);
                }
                if (count > 0)
                {
                    temp = " and (1=0 " + pnrTemp + ")";
                    selString += temp;
                }
            }
            #endregion
            #region �����
            if (!(tbAirlineCode.Text.Trim() == "" || tbAirlineCode.Text.Trim() == "����ö��Ÿ���"))
            {
                string[] pnrs = tbAirlineCode.Text.Trim().Split(',');
                int count = 0;
                string pnrTemp = "";
                for (int i = 0; i < pnrs.Length; i++)
                {
                    string tt = pnrs[i].Trim();
                    if (tt.Trim().Length != 3) continue;
                    count++;
                    pnrTemp += string.Format(" or ([E-TKT-NUMBER] like '{0}%')", tt);
                }
                if (count > 0)
                {
                    temp = " and (1=0 " + pnrTemp + ")";
                    selString += temp;
                }
            }
            #endregion
            #region �û��ʺ�
            if (!(tbUserAccount.Text.Trim() == "" || tbUserAccount.Text.Trim() == "����ö��Ÿ���"))
            {
                string[] pnrs = tbUserAccount.Text.Trim().Split(',');
                int count = 0;
                string pnrTemp = "";
                for (int i = 0; i < pnrs.Length; i++)
                {
                    string tt = pnrs[i].Trim();
                    //if (tt.Trim().Length != 6) continue;
                    count++;
                    pnrTemp += string.Format(" or (USERACCOUNT='{0}')", tt);
                }
                if (count > 0)
                {
                    temp = " and (1=0 " + pnrTemp + ")";
                    selString += temp;
                }
            }
            #endregion
            #region �û�������
            if (!(tbUserName.Text.Trim() == "" || tbUserName.Text.Trim() == "����ö��Ÿ���"))
            {
                string[] pnrs = tbUserName.Text.Trim().Split(',');
                int count = 0;
                string pnrTemp = "";
                for (int i = 0; i < pnrs.Length; i++)
                {
                    string tt = pnrs[i].Trim();
                    //if (tt.Trim().Length != 6) continue;
                    count++;
                    pnrTemp += string.Format(" or (USERNAME='{0}')", tt);
                }
                if (count > 0)
                {
                    temp = " and (1=0 " + pnrTemp + ")";
                    selString += temp;
                }
            }
            #endregion
            #region ��������
            if (!(tbAgentName.Text.Trim() == "" || tbAgentName.Text.Trim() == "����ö��Ÿ���"))
            {
                string[] pnrs = tbAgentName.Text.Trim().Split(',');
                int count = 0;
                string pnrTemp = "";
                for (int i = 0; i < pnrs.Length; i++)
                {
                    string tt = pnrs[i].Trim();
                    //if (tt.Trim().Length != 6) continue;
                    count++;
                    pnrTemp += string.Format(" or (AGENTNAME='{0}')", tt);
                }
                if (count > 0)
                {
                    temp = " and (1=0 " + pnrTemp + ")";
                    selString += temp;
                }
            }
            #endregion
            #region ������Χ
            switch (cbSearchField.Text)
            {
                case "ȫ�����Ʊ":
                    temp ="";
                    break;
                case "ȫ���Ѿ��۳�Ʊ":
                    temp = " and ([E-TKT-NUMBER]>' ')";
                    break;
                case "�۳���������":
                    temp = " and ([USERACCOUNT]>' ')";
                    break;
                case "�۳���δ����":
                    temp = " and (isnull([USERACCOUNT]))";
                    break;
                default:
                    temp = "";
                    break;
            }
            
            selString += temp;
            #endregion ������Χ
            #region ��Ʊ״̬����������Ʊ����Ʊ��֮������
            int iflag = 0;
            if (checkBox1.Checked)
            {
                iflag += 1;
            }
            if (checkBox2.Checked)
            {
                iflag += 2;
            }
            if (checkBox3.Checked)
            {
                iflag += 4;
            }
            switch (iflag)
            {
                case 0:
                    temp = "";
                    break;
                case 1:
                    temp = " and ([TKT-FLAG]= '1')";
                    break;
                case 2:
                    temp = " and ([TKT-FLAG]= '2')";
                    break;
                case 3:
                    temp = " and ([TKT-FLAG]= '1' or [TKT-FLAG]= '2')";
                    break;
                case 4:
                    temp = " and ([TKT-FLAG]= '4')";
                    break;
                case 5:
                    temp = " and ([TKT-FLAG]= '4' or [TKT-FLAG]= '1')";
                    break;
                case 6:
                    temp = " and ([TKT-FLAG]= '4' or [TKT-FLAG]= '2')";
                    break;
                case 7:
                    temp = "and ([TKT-FLAG]= '4' or [TKT-FLAG]= '2' or [TKT-FLAG]= '1')";
                    break;
                default:
                    throw new Exception("��ѡ���Ʊ״̬��");
            }
            selString += temp;
            #endregion
            /*/switch (cbGroupBy.Text)
            //{
            //    case "ʮλƱ��":
            //        selString += " group by ALL TKTNUMBER";
            //        break;
            //    case "OFFICE":
            //        selString += " group by OFFICE";
            //        break;
            //    case "�������":
            //        selString += " group by DATEOFINCOMING";
            //        break;
            //    case "���ӿ�Ʊ��":
            //        selString += " group by [E-TKT-NUMBER]";
            //        break;
            //    case "�����ص����":
            //        selString += " group by [ORIG-DEST]";
            //        break;
            //    case "Ʊ���":
            //        selString += " group by COLLECTION";
            //        break;
            //    case "PNR":
            //        selString += " group by PNR";
            //        break;
            //    case "��������":
            //        selString += " group by DATEOFSALE";
            //        break;
            //    case "״̬��־":
            //        selString += " group by [TKT-FLAG]";
            //        break;
            //    case "Eagle�ʻ���":
            //        selString += " group by USERACCOUNT";
            //        break;
            //    case "Eagle�ʻ�������":
            //        selString += " group by USERNAME";
            //        break;
            //    case "Eagle��������":
            //        selString += " group by AGENTNAME";
            //        break;
            //    default:
            //        break;
            //}*/
            #region ����
            switch (cbOrderBy.Text)
            {
                case "ʮλƱ��":
                    selString += " order by TKTNUMBER";
                    break;
                case "OFFICE":
                    selString += " order by OFFICE";
                    break;
                case "�������":
                    selString += " order by DATEOFINCOMING";
                    break;
                case "���ӿ�Ʊ��":
                    selString += " order by [E-TKT-NUMBER]";
                    break;
                case "�����ص����":
                    selString += " order by [ORIG-DEST]";
                    break;
                case "Ʊ���":
                    selString += " order by COLLECTION";
                    break;
                case "PNR":
                    selString += " order by PNR";
                    break;
                case "��������":
                    selString += " order by DATEOFSALE";
                    break;
                case "״̬��־":
                    selString += " order by [TKT-FLAG]";
                    break;
                case "Eagle�ʻ���":
                    selString += " order by USERACCOUNT";
                    break;
                case "Eagle�ʻ�������":
                    selString += " order by USERNAME";
                    break;
                case "Eagle��������":
                    selString += " order by AGENTNAME";
                    break;
                default:
                    break;
            }
            #endregion
            this.Close();
        }

        private void tbTicketNumberBeg_TextChanged(object sender, EventArgs e)
        {
            tbTicketNumberEnd.Text = tbTicketNumberBeg.Text;
        }

        private void tbPNR_Enter(object sender, EventArgs e)
        {
            if (tbPNR.Text == "����ö��Ÿ���") tbPNR.Text = "";
        }

        private void tbOffice_Enter(object sender, EventArgs e)
        {
            if (tbOffice.Text == "����ö��Ÿ���") tbOffice.Text = "";
        }

        private void tbAirlineCode_Enter(object sender, EventArgs e)
        {
            if (tbAirlineCode.Text == "����ö��Ÿ���") tbAirlineCode.Text = "";
        }

        private void tbUserAccount_Enter(object sender, EventArgs e)
        {
            if (tbUserAccount.Text == "����ö��Ÿ���") tbUserAccount.Text = "";
        }

        private void tbUserName_Enter(object sender, EventArgs e)
        {
            if (tbUserName.Text == "����ö��Ÿ���") tbUserName.Text = "";
        }

        private void tbAgentName_Enter(object sender, EventArgs e)
        {
            if (tbAgentName.Text == "����ö��Ÿ���") tbAgentName.Text = "";
        }


        void multiselect(TextBox tb, string field)
        {
            string cmdString = string.Format("select distinct " + field + " from etickets");
            OleDbCommand cmd = new OleDbCommand(cmdString, GlobalVar.cn);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);
            string accounts = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string temp = dt.Rows[i][field].ToString().Trim();
                if(temp!="")
                accounts += temp + ",";
            }
            if (accounts.Length > 0) accounts = accounts.Substring(0, accounts.Length - 1);
            MultiSelect ms = new MultiSelect(accounts, tb.Text);
            if (ms.ShowDialog() == DialogResult.OK)
            {
                tb.Text = ms.strResult;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            multiselect(tbUserAccount, "USERACCOUNT");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            multiselect(tbUserName, "USERNAME");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            multiselect(tbAgentName, "AgentName");
        }


    }
}