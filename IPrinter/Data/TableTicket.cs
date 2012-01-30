using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus.Data
{
    public partial class TableTicket : Form
    {
        public TableTicket()
        {
            InitializeComponent();
        }
        public TableTicket(ePlus.Data.avDataSet ds,int index)
        {
            InitializeComponent();
            initListWithDs(ds,index);
        }
        public void initListWithDs(ePlus.Data.avDataSet ds,int index)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            try
            {
                lv.Clear();
                if (index >= ds.TableTicket.Count || index < 0) index = ds.TableTicket.Count - 1;
                for (int i = 0; i < ds.TableTicket[index].Columns.Count; i++)
                {
                    lv.Columns.Add(ds.TableTicket[index].Columns[i].ColumnName);
                }
                for (int i = 0; i < ds.TableTicket[index].Rows.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    for (int j = 0; j < ds.TableTicket[index].Columns.Count; j++)
                    {
                        if (j == 0) lvi.Text = ds.TableTicket[index].Rows[i][j].ToString();
                        else
                            lvi.SubItems.Add(ds.TableTicket[index].Rows[i][j].ToString());
                    }
                    lv.Items.Add(lvi);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lv.Columns[0].Width += lv.Columns[1].Width;
                lv.Columns[1].Width = 0;
                lv.Columns[5].Width += lv.Columns[2].Width;
                lv.Columns[2].Width = 0;
                lv.Columns[6].Width += lv.Columns[3].Width;
                lv.Columns[3].Width = 0;
                lv.Columns[7].Width += lv.Columns[4].Width;
                lv.Columns[4].Width = 0;
                lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                lv.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.ColumnContent);
                lv.AutoResizeColumn(6, ColumnHeaderAutoResizeStyle.ColumnContent);
                lv.AutoResizeColumn(7, ColumnHeaderAutoResizeStyle.ColumnContent);
                SpecifyLowestPriceLine();
            }
            catch
            {
            }
        }
        private void TableTicket_Load(object sender, EventArgs e)
        {

        }

        private void TableTicket_SizeChanged(object sender, EventArgs e)
        {
            lv.Size = new Size(lv.Width, this.Height - btnSubmitOrderWithPnr.Height - lblCityPair.Height * 5);
            lblDate.Location = new Point(lv.Location.X, lv.Location.Y + lv.Size.Height);
            lblCityPair.Location = new Point(lblDate.Location.X + lblDate.Width, lblDate.Location.Y);
            lblTimeBeg.Location = new Point(lblCityPair.Location.X + lblCityPair.Width, lblDate.Location.Y);
            lblTimeEnd.Location = new Point(lblTimeBeg.Location.X + lblTimeBeg.Width, lblDate.Location.Y);
            checkBox1.Location = new Point(lblDate.Location.X, lblDate.Location.Y + lblDate.Height);
            btnSubmitOrderWithPnr.Location = new Point(lv.Location .X +lv.Width /2 -btnSubmitOrderWithPnr .Width /2, 
                checkBox1.Location .Y +checkBox1 .Height );
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
        void SpecifyLowestPriceLine()
        {
            int minPrice = 9999;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                int p = 9999; int.TryParse(lv.Items[i].SubItems[6].Text, out p);
                if (p > 0 && minPrice > p) minPrice = p;
            }
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[6].Text == minPrice.ToString())
                {
                    lv.Items[i].BackColor = Color.Black;
                    lv.Items[i].ForeColor = Color.White;
                }
            }
        }

        private void btnSubmitOrderWithPnr_Click(object sender, EventArgs e)
        {

        }
    }
}