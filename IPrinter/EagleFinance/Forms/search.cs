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
                "[TKTNUMBER] as 票号," +            //2
                "[DATEOFINCOMING] as 入库日期," +  //3
                "[E-TKT-NUMBER] as 电子票号," +    //4
                "[ORIG-DEST] as 始终," +           //5
                "[COLLECTION] as 票价," +          //6
                "[TAXS] as 税," +                   //7
                "[COMM] as 底价点扣," +            //8
                "[PNR]," +                          //9
                "[receiptNumber] as 行程单号," +   //10
                "[DATEOFSALE] as 销售日期," +
                "[TKT-FLAG] as 状态," +
                "[USERACCOUNT] as 用户," +
                "[USERNAME] as 中文名," +
                "[AGENTNAME] as 代理商," +         //15
                "[FLIGHTDATE] as 乘机日期," +
                "[FLIGHTNUMBER] as 航班号," +
                "[FLIGHTBUNK] as 舱位," +
                "[FLIGHTCITY] as 飞抵城市," +
                "[AGENTCOMM] as 代理返点," +       //20
                "[IMPORTCOUNT] as 导入次数" +
                " from etickets where 1=1";
            //selString = "select * from etickets where 1=1";
            string temp = "";
            #region 入库日期，售票日期，十位票号
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
            if (tbOrig.Text.Trim() == "" || tbOrig.Text.Trim() == "多个用逗号隔开")
            {
            }
            if (tbDest.Text.Trim() == "" || tbDest.Text.Trim() == "多个用逗号隔开")
            {
            }
            #region PNR
            if (!(tbPNR.Text.Trim() == "" || tbPNR.Text.Trim() == "多个用逗号隔开"))
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
            #region OFFICE号
            if (!(tbOffice.Text.Trim() == "" || tbOffice.Text.Trim() == "多个用逗号隔开"))
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
            #region 结算号
            if (!(tbAirlineCode.Text.Trim() == "" || tbAirlineCode.Text.Trim() == "多个用逗号隔开"))
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
            #region 用户帐号
            if (!(tbUserAccount.Text.Trim() == "" || tbUserAccount.Text.Trim() == "多个用逗号隔开"))
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
            #region 用户中文名
            if (!(tbUserName.Text.Trim() == "" || tbUserName.Text.Trim() == "多个用逗号隔开"))
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
            #region 代理商名
            if (!(tbAgentName.Text.Trim() == "" || tbAgentName.Text.Trim() == "多个用逗号隔开"))
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
            #region 搜索范围
            switch (cbSearchField.Text)
            {
                case "全部入库票":
                    temp ="";
                    break;
                case "全部已经售出票":
                    temp = " and ([E-TKT-NUMBER]>' ')";
                    break;
                case "售出并已完整":
                    temp = " and ([USERACCOUNT]>' ')";
                    break;
                case "售出但未完整":
                    temp = " and (isnull([USERACCOUNT]))";
                    break;
                default:
                    temp = "";
                    break;
            }
            
            selString += temp;
            #endregion 搜索范围
            #region 客票状态，正常，退票，废票，之间的组合
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
                    throw new Exception("请选择客票状态！");
            }
            selString += temp;
            #endregion
            /*/switch (cbGroupBy.Text)
            //{
            //    case "十位票号":
            //        selString += " group by ALL TKTNUMBER";
            //        break;
            //    case "OFFICE":
            //        selString += " group by OFFICE";
            //        break;
            //    case "入库日期":
            //        selString += " group by DATEOFINCOMING";
            //        break;
            //    case "电子客票号":
            //        selString += " group by [E-TKT-NUMBER]";
            //        break;
            //    case "出发地到达地":
            //        selString += " group by [ORIG-DEST]";
            //        break;
            //    case "票面价":
            //        selString += " group by COLLECTION";
            //        break;
            //    case "PNR":
            //        selString += " group by PNR";
            //        break;
            //    case "销售日期":
            //        selString += " group by DATEOFSALE";
            //        break;
            //    case "状态标志":
            //        selString += " group by [TKT-FLAG]";
            //        break;
            //    case "Eagle帐户名":
            //        selString += " group by USERACCOUNT";
            //        break;
            //    case "Eagle帐户中文名":
            //        selString += " group by USERNAME";
            //        break;
            //    case "Eagle代理商名":
            //        selString += " group by AGENTNAME";
            //        break;
            //    default:
            //        break;
            //}*/
            #region 排序
            switch (cbOrderBy.Text)
            {
                case "十位票号":
                    selString += " order by TKTNUMBER";
                    break;
                case "OFFICE":
                    selString += " order by OFFICE";
                    break;
                case "入库日期":
                    selString += " order by DATEOFINCOMING";
                    break;
                case "电子客票号":
                    selString += " order by [E-TKT-NUMBER]";
                    break;
                case "出发地到达地":
                    selString += " order by [ORIG-DEST]";
                    break;
                case "票面价":
                    selString += " order by COLLECTION";
                    break;
                case "PNR":
                    selString += " order by PNR";
                    break;
                case "销售日期":
                    selString += " order by DATEOFSALE";
                    break;
                case "状态标志":
                    selString += " order by [TKT-FLAG]";
                    break;
                case "Eagle帐户名":
                    selString += " order by USERACCOUNT";
                    break;
                case "Eagle帐户中文名":
                    selString += " order by USERNAME";
                    break;
                case "Eagle代理商名":
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
            if (tbPNR.Text == "多个用逗号隔开") tbPNR.Text = "";
        }

        private void tbOffice_Enter(object sender, EventArgs e)
        {
            if (tbOffice.Text == "多个用逗号隔开") tbOffice.Text = "";
        }

        private void tbAirlineCode_Enter(object sender, EventArgs e)
        {
            if (tbAirlineCode.Text == "多个用逗号隔开") tbAirlineCode.Text = "";
        }

        private void tbUserAccount_Enter(object sender, EventArgs e)
        {
            if (tbUserAccount.Text == "多个用逗号隔开") tbUserAccount.Text = "";
        }

        private void tbUserName_Enter(object sender, EventArgs e)
        {
            if (tbUserName.Text == "多个用逗号隔开") tbUserName.Text = "";
        }

        private void tbAgentName_Enter(object sender, EventArgs e)
        {
            if (tbAgentName.Text == "多个用逗号隔开") tbAgentName.Text = "";
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