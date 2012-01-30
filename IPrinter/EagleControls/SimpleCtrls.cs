using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace EagleControls
{
    class SimpleCtrls
    {
    }
    public class lvSelectedBunkInEasy : SortListView
    {
        ColumnHeader[] columnHeader;
        bool m_chinese = false;
        public lvSelectedBunkInEasy(bool chinese)
        {
            this.Dock = DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.View = View.Details;
            m_chinese = chinese;
            string [] a;
            if (chinese)
            {
                a = new string[] { "日期", "城市对", "航空公司", "航班号", "折扣" };

            }
            else
            {
                a = new string[] { "日期", "航班号", "舱位", "城市对" };
            }
            columnHeader = new ColumnHeader[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                columnHeader[i] = new ColumnHeader();
                columnHeader[i].Text = a[i];
            }
            this.Columns.AddRange(columnHeader);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        public void Add(DateTime date, string flightno, char bunk, string citypair)
        {
            ListViewItem lvi = new ListViewItem();

            if (!m_chinese)
            {
                lvi.Text = date.ToShortDateString();
                lvi.SubItems.Add(flightno);
                lvi.SubItems.Add(bunk.ToString());
                lvi.SubItems.Add(citypair);
            }
            else
            {
                lvi.Text = date.ToString("yyyyMMdd");
                lvi.SubItems.Add(EagleString.EagleFileIO.CityPairCnName(citypair));
                lvi.SubItems.Add(EagleString.BaseFunc.AirLineCnName(flightno));
                lvi.SubItems.Add(flightno);
                if (!EagleString.egString.LargeThan420(date))
                {
                    lvi.SubItems.Add(EagleString.EagleFileIO.RebateOf(bunk, flightno).ToString());
                }
                else
                {
                    //lvi.SubItems.Add(EagleString.EagleFileIO.RebateOfNew(bunk, flightno).ToString());
                    lvi.SubItems.Add("舱");
                }
            }
            if (this.Items.Count % 2 == 0) lvi.BackColor = System.Drawing.Color.Gray;
            this.Items.Add(lvi);
            this.Sort();
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

    }

    public class lvPassengerInEasy : SortListView
    {
        ColumnHeader[] columnHeader;
        public lvPassengerInEasy()
        {
            this.GridLines = true;
            this.View = View.Details;
            this.FullRowSelect = true;
            this.Dock = DockStyle.Fill;
            string[] a = new string[] { "姓名","证件号码","电话号码"};
            columnHeader = new ColumnHeader[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                columnHeader[i] = new ColumnHeader();
                columnHeader[i].Text = a[i];
            }
            this.Columns.AddRange(columnHeader);
            this.MouseDoubleClick += new MouseEventHandler(lvPassengerInEasy_MouseDoubleClick);
        }

        void lvPassengerInEasy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.SelectedIndices.Count == 1)
                {
                    int index = SelectedIndices[0];
                    this.Items.RemoveAt(index);
                }
            }
            catch
            {
            }
        }
        public void Add(string name, string card, string phone)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = name;
            lvi.SubItems.Add(card);
            lvi.SubItems.Add(phone);
            this.Items.Add(lvi);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        public void Modify(int index, string name, string card, string phone)
        {
            this.Items.RemoveAt(index);
            Add(name, card, phone);
        }

    }

    public class PnrListView : SortListView
    {
        EagleString.RtResultList rt;
        EagleString.SsResultList ss;
        string[] statedPnr = new string[] { "", "", "", "" };
        ColumnHeader[] columnHeader;
        EagleString.LoginInfo m_li;
        public PnrListView(EagleString.LoginInfo li)
        {
            m_li = li;
            this.GridLines = true;
            this.View = View.Details;
            this.FullRowSelect = true;
            this.Dock = DockStyle.Fill;
            string[] a = new string[] { "    产生时间    ", "    提交时间    ", "  PNR  ", "    状态    " };//未提交,已提交,已出票,已过时
            columnHeader = new ColumnHeader[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                columnHeader[i] = new ColumnHeader();
                columnHeader[i].Text = a[i];
            }
            this.Columns.AddRange(columnHeader);
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        public void UpdatePnr()
        {

            EagleWebService.kernalFunc kf = new EagleWebService.kernalFunc(m_li.b2b.webservice);
            for (int i = 0; i < statedPnr.Length; i++)
            {
                kf.GetSubmittedPnrsWith(m_li.b2b.username, i, ref statedPnr[i]);
            }
            rt = EagleString.RtResultList.DeSerializeRtResults();
            if (rt == null) rt = new EagleString.RtResultList();
            ss = EagleString.SsResultList.DeSerializeSsResults();
            if (ss == null) ss = new EagleString.SsResultList();
            {
                //SetListView(statedPnr, rt, ss);//
            }
            {
                SetSS(ss);
                SetRT(rt);
                SetPnrState(statedPnr);
                for (int i = 0; i < Items.Count; i++)
                {
                    if (i % 2 == 1) Items[i].BackColor = Color.LightBlue;
                }
            }

        }
        List<string> ls_PNR_SS = new List<string>();
        private void SetSS(EagleString.SsResultList ss)
        {
            this.Items.Clear();
            ls_PNR_SS.Clear();
            
            for (int i = 0; i < ss.ls.Count; ++i)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ss.ls[i].CreateDate.ToString("yyyyMMdd hhmm");
                lvi.SubItems.Add("");
                lvi.SubItems.Add(ss.ls[i].PNR.ToUpper());
                lvi.SubItems.Add("");
                this.Items.Add(lvi);
                ls_PNR_SS.Add(ss.ls[i].PNR.ToUpper());
            }
        }
        List<string> ls_PNR_RT = new List<string>();
        private void SetRT(EagleString.RtResultList rt)
        {
            ls_PNR_RT.Clear();
            for (int i = 0; i < rt.ls.Count; i++)
            {
                string pnr = rt.ls[i].PNR.ToUpper();
                ls_PNR_RT.Add(pnr);
                int row = ls_PNR_SS.IndexOf(pnr);
                if (row < 0)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "";
                    lvi.SubItems.Add(rt.ls[i].SubmittdDate.ToString("yyyyMMdd hhmm"));
                    lvi.SubItems.Add(pnr);
                    lvi.SubItems.Add("");
                    this.Items.Add(lvi);
                }
                else
                {
                    ListViewItem lvi = this.Items[row];
                    lvi.SubItems[1].Text = rt.ls[i].SubmittdDate.ToString();
                }
            }
        }
        private void SetPnrState(string[] statedPnr)
        {
            string[] s = new string[] { "未处理", "通过", "未通过", "完结" };
            for (int i = 0; i < statedPnr.Length; i++)
            {
                string[] pnrs = statedPnr[i].Split(';');
                for (int j = 0; j < pnrs.Length; j++)
                {
                    string pnr = pnrs[j].ToUpper();
                    if (!EagleString.BaseFunc.PnrValidate(pnr)) continue;
                    int index;
                    index = ls_PNR_SS.IndexOf(pnr);
                    if (index >= 0)
                    {
                        this.Items[index].SubItems[3].Text = s[i];
                        continue;
                    }
                    else
                    {
                        index = ls_PNR_RT.IndexOf(pnr);
                        if (index >= 0)
                        {
                            this.Items[index].SubItems[3].Text = s[i];
                            continue;
                        }
                        else
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = "";
                            lvi.SubItems.Add("");
                            lvi.SubItems.Add(pnr);
                            lvi.SubItems.Add(s[i]);
                            this.Items.Add(lvi);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 只显示本地有并且提交过的PNR列表
        /// </summary>
        /// <param name="statedPnr"></param>
        /// <param name="rt"></param>
        /// <param name="ss"></param>
        private void SetListView(string[] statedPnr, EagleString.RtResultList rt, EagleString.SsResultList ss)
        {
            for (int i = 0; i < rt.ls.Count; i++)
            {
                int j = 0;
                for (j = 0; j < ss.ls.Count; j++)
                {
                    if (rt.ls[i].PNR.ToUpper() == ss.ls[j].PNR.ToUpper())
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = ss.ls[i].CreateDate.ToString();
                        lvi.SubItems.Add(rt.ls[i].SubmittdDate.ToString());
                        lvi.SubItems.Add(rt.ls[i].PNR);
                        SetListViewItemLastSubItem(lvi, statedPnr, rt.ls[i]);
                        this.Items.Add(lvi);
                    }
                }
                if (j == ss.ls.Count)//rt中的PNR在ss中找不到，即不是在易格2.0中产生的PNR,但在2.0中进行了提交
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "无本地数据";
                    lvi.SubItems.Add(rt.ls[i].SubmittdDate.ToString());
                    lvi.SubItems.Add(rt.ls[i].PNR);
                    SetListViewItemLastSubItem(lvi, statedPnr, rt.ls[i]);
                    this.Items.Add(lvi);
                }
            }
        }
        private void SetListViewItemLastSubItem(ListViewItem lvi,string[] statedPnr, EagleString.RtResult rt)
        {
            for (int aa = 0; aa < statedPnr.Length; aa++)
            {
                string[] arr = statedPnr[aa].Split(';');
                int bb = 0;
                for (bb = 0; bb < arr.Length; bb++)
                {
                    if (arr[bb].ToUpper() == rt.PNR.ToUpper())
                    {
                        switch (aa)
                        {
                            case 0:
                                lvi.SubItems.Add("未处理");
                                break;
                            case 1:
                                lvi.SubItems.Add("通过");
                                break;
                            case 2:
                                lvi.SubItems.Add("未通过");
                                break;
                            case 3:
                                lvi.SubItems.Add("完结");
                                break;
                        }
                    }
                }
                if (bb == arr.Length) lvi.SubItems.Add("无服务器数据");//表示在服务器中找不到状态
            }
        }
    }



    public class SortListView : ListView
    {
        int currentCol = -1;
        bool sort;

        public SortListView()
        {
        }
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            base.OnColumnClick(e);
            string Asc = ((char)0x25bc).ToString().PadLeft(4, ' ');
            string Des = ((char)0x25b2).ToString().PadLeft(4, ' ');

            if (sort == false)
            {
                sort = true;
                string oldStr = this.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.Columns[e.Column].Text = oldStr + Des;
            }
            else if (sort == true)
            {
                sort = false;
                string oldStr = this.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.Columns[e.Column].Text = oldStr + Asc;
            }

            ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            this.Sort();
            int rowCount = this.Items.Count;
            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    this.Items[i].UseItemStyleForSubItems = false;
                    this.Items[i].SubItems[currentCol].BackColor = Color.White;

                    if (e.Column != currentCol)
                        this.Columns[currentCol].Text = this.Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                this.Items[i].UseItemStyleForSubItems = false;
                this.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;
                currentCol = e.Column;
            }
        }

        public class ListViewItemComparer : IComparer
        {
            public bool sort_b;
            public SortOrder order = SortOrder.Ascending;

            private int col;

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column, bool sort)
            {
                col = column;
                sort_b = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort_b)
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }
    }
}
