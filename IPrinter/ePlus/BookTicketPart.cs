using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ePlus
{
    public partial class BookTicket
    {
        public static DateTime DateSearch;
        public void _420Init()
        {
            for (int i = 10; i < this.lv_查询结果.Columns.Count; i++)
            {
                lv_查询结果.Columns[i].Text = string.Format("{0}",100-(i-9)*4);
            }
        }

        private void wf_init()
        {
#if RWY
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblNotice.Visible = false;
            this.pnlFixBunk.Visible = false;
            this.pnlFlowBunk.Visible = false;
            this.pnlGroup.Visible = false;
            this.label4.Visible = false;
            this.label5.Visible = false;
#endif
        }

        public void initNkgControls()
        {
            this.AutoScroll = false;
            //pnlNkg .Visible = true;
            lv_查询结果.Visible = false;
            btOther.Visible = false;//修改密码
            button9.Visible = false;//后台管理
            lblNotice.Visible = false;//滚动条
            checkBox1.Visible = false;//直飞
            checkBox2.Visible = false;//政策
            button10.Visible = false;//设置

            DataTable dt = new DataTable();
            dt.Columns .Add (  "姓名1");//         
            dt.Columns.Add("证件号码1");//
            dt.Columns.Add("类型1");//
            dt.Columns.Add("姓名2");//         
            dt.Columns.Add("证件号码2");//
            dt.Columns.Add("类型2");//
            dt.Columns.Add("姓名3");//         
            dt.Columns.Add("证件号码3");//
            dt.Columns.Add("类型3");//
            //dgAddPassenger.DataSource = dt;
            //dgAddPassenger.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        void dgFlightInfo_AddSelectBox()
        {
            //if (dgFlightInfo.Columns["选择航班"] != null) return;
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell();
            col.Name = "选择航班";
            col.CellTemplate = cell;
            cell.Value = false;
            //dgFlightInfo.Columns.Add(col);
            //dgFlightInfo.AutoResizeColumns();
        }
        void dgFlightInfo_DoubleClick()
        {
            //显示所有舱位
            EasyPrice.tt.Show();
        }

        //private void dgFlightInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    int col = e.ColumnIndex;
            
        //    if (dgFlightInfo.Columns[col].Name == "选择航班")
        //    {
        //        int row = e.RowIndex;
        //        if ((bool)dgFlightInfo[col, row].Value)//选中,则加入
        //        {
        //            if (dgSelectedFight.ColumnCount == 0)//首先增加列名
        //            {
        //                for (int i = 0; i < dgFlightInfo.Columns.Count; i++)
        //                {
        //                    DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
        //                    newCol.Name = dgFlightInfo.Columns[i].Name;
        //                    dgSelectedFight.Columns.Add(newCol);
        //                }
        //                {
        //                    DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
        //                    newCol.Name = "日期";
        //                    dgSelectedFight.Columns.Add(newCol);
        //                }
        //            }
        //            try
        //            {

        //                DataGridViewRow dr = new DataGridViewRow();
        //                dr.CreateCells(dgFlightInfo);
        //                dr.Cells.Add(new DataGridViewTextBoxCell());
        //                int i = 0;
        //                for (i = 0; i < dr.Cells.Count-1; i++)
        //                {
        //                    dr.Cells[i].Value = dgFlightInfo.Rows[row].Cells[i].Value;
        //                }
        //                dr.Cells[i].Value = dtp_机票日期.Value.ToShortDateString();
        //                dgSelectedFight.Rows.Add(dr);
        //                dgSelectedFight.AutoResizeColumns();
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        else//否则,删除
        //        {
        //            for (int i = 0; i < dgSelectedFight.Rows.Count; i++)
        //            {
        //                if (dgSelectedFight[0, i].Value == dgFlightInfo[col, row].Value)
        //                {
        //                    dgSelectedFight.Rows.RemoveAt(i);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
        //private void dgSelectedFight_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    int col = e.ColumnIndex;

        //    if (dgSelectedFight.Columns[col].Name == "选择航班")
        //    {
        //        int row = e.RowIndex;
        //        if ((bool)dgSelectedFight[col, row].Value) ;
        //        else
        //        {
        //            dgSelectedFight.Rows.RemoveAt(e.RowIndex);
        //        }
        //        dgSelectedFight.AutoResizeColumns();
        //    }
        //}
        //private void dgAddPassenger_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    dgAddPassenger.AutoResizeColumns();
        //}

        //private void dgFlightInfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    pnlTT.Controls.Clear();
        //    pnlTT.Controls.Add(EasyPrice.tt);
        //    EasyPrice.tt.Show();
        //}
        /*private void btBookAndCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dgSelectedFight.Rows.Count < 2) return;
                //if (dgAddPassenger.Rows.Count < 2) return;

                b_bookticket_提取 = false;
                b_bookticket_fd = false;
                b_BookTicketAv = false;
                b_book = true;

                List<string> ls = new List<string>();
                for (int i = 0; i < dgAddPassenger.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        string nm = dgAddPassenger.Rows[i].Cells[0 + j * 3].Value.ToString().Trim();
                        if (nm != "")
                        {
                            string card = dgAddPassenger.Rows[i].Cells[1 + j * 3].Value.ToString().Trim();
                            if (card == "") card = "0";
                            ls.Add(nm + ";" + card);
                        }
                    }
                }
                char newline = '\xD';
                string ss = "";//ssCZ2565/C/20FEB/KHNCAN/NN1
                DateTime firstdate = new DateTime();//第一航段的时间，
                string firsttime = "";
                for (int i = 0; i < dgSelectedFight.Rows.Count - 1; i++)
                {
                    string fn = dgSelectedFight.Rows[i].Cells[0].Value.ToString().Split('-')[0];//取航班号
                    string bk = dgSelectedFight.Rows[i].Cells[0].Value.ToString().Split('-')[1];//取舱位
                    DateTime date = DateTime.Parse(dgSelectedFight.Rows[i].Cells[9].Value.ToString());//日期
                    string time = dgSelectedFight.Rows[i].Cells[2].Value.ToString();
                    if (i == 0)
                    {
                        firstdate = date;
                        firsttime = time;
                    }
                    else if (firstdate > date)
                    {
                        firstdate = date;
                        firsttime = time;
                    }
                    else if (firstdate == date)
                    {
                        if (firsttime.CompareTo(time) > 0) firsttime = time;
                    }
                    string dt = date.Day.ToString("d2") + EagleAPI.GetMonthCode(date.Month);
                    string ft = dgSelectedFight.Rows[i].Cells[1].Value.ToString();
                    string no = "LL" + ls.Count.ToString();
                    string sp = "/";
                    ss += "ss" + fn + sp + bk + sp + dt + sp + ft + sp + no + newline.ToString();
                }
                mystring.sortStringListByPinYin(ls);
                string name = "NM";
                for (int i = 0; i < ls.Count; i++)
                {
                    name += "1" + ls[i].Split(';')[0];
                }
                name += newline.ToString();
                string ct = "ct95161" + newline.ToString();
                DateTime d = DateTime.Parse(firstdate.ToShortDateString() + " " + firsttime.Insert(2, ":") + ":00");
                d = d.AddHours(-3);
                string tk =
                    "tktl" + d.ToString("hhmm") + "/" + d.Day.ToString("d2") + EagleAPI.GetMonthCode(d.Month) + "/WUH128" + newline.ToString();
                string ssr = "";
                for (int i = 0; i < ls.Count; i++)
                {
                    int j = i + 1;
                    string psptno = ls[i].Substring(ls[i].IndexOf(";") + 1);
                    if (psptno.Split('/').Length == 1)
                    {
                        ssr += "SSR FOID YY HK/NI" + psptno + "/p" + j.ToString() + newline.ToString();
                    }
                    else
                    {
                        ssr += "SSR PSPT YY HK1/" + psptno + "/p" + j.ToString() + newline.ToString();
                    }
                }
                string cmd = ss + name + ct + tk + ssr + "@";
                EagleAPI.CLEARCMDLIST(3);
                EagleAPI.EagleSendCmd(cmd, 3);
                tb_电脑号.Text = GlobalVar.WaitString;
            }
            catch
            {
            }
        }
        */
    }
}
