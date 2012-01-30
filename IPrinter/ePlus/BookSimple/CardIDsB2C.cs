using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ePlus.BookSimple
{
    public partial class CardIDsB2C : Form
    {
        public CardIDsB2C()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否有常旅客
        /// </summary>
        bool hasB2CPassengers = false;
        DataTable dtB2CPassengers;

        private void bt_Select_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 1) return;

            {
                if (hasB2CPassengers)
                {
                    string item = listBox1.SelectedItem.ToString();
                    string[] itemArray = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    RichTextBox textBox = GlobalVar.stdRichTB;
                    int CurrentLine = textBox.GetLineFromCharIndex(textBox.SelectionStart);
                    string CurrentLineText = textBox.Lines[CurrentLine].ToLower();

                    if (CurrentLineText.IndexOf("nm") < 0)
                        textBox.AppendText("nm");

                    textBox.AppendText(" ");
                    textBox.AppendText(itemArray[1]);
                }
            }
        }

        private void CardIDs_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 显示常旅客列表
        /// </summary>
        public void LoadPassengers()
        {
            this.listBox1.Items.Clear();

            if (string.IsNullOrEmpty(Options.GlobalVar.B2CCallingXml))
            {
                listBox1.Items.Add("没有检测到客户来电记录！");
                return;
            }

            string strXml = Options.GlobalVar.B2CCallingXml;
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.LoadXml(strXml);
            }
            catch (Exception ee)
            {
                listBox1.Items.Add("读取来电记录失败！" + Environment.NewLine + ee.Message);
                return;
            }

            if (xd.SelectSingleNode("NewCustomer") != null)
            {
                listBox1.Items.Add("当前来电为新客户，没有常旅客。请注意保存该客户信息！");
                return;
            }
            else
            {
                DataEntity.XMLSchema.Customers customer;
                customer = DataEntity.XMLSchema.xml_BaseClass.LoadXml<DataEntity.XMLSchema.Customers>(strXml);

                if (customer.Passengers == null || customer.Passengers.Count == 0)
                {
                    listBox1.Items.Add("该来电客户目前没有常旅客！");
                    return;
                }
                else
                {
                    this.hasB2CPassengers = true;
                    dtB2CPassengers = new DataTable();
                    dtB2CPassengers.Columns.Add("py");
                    dtB2CPassengers.Columns.Add("item");

                    foreach (DataEntity.XMLSchema.Passenger passenger in customer.Passengers)
                    {
                        string item = passenger.fet_ID.PadRight(6, ' ') + passenger.fet_Name + "-" + passenger.fet_CardID1;
                        this.listBox1.Items.Add(item);

                        DataRow dr = dtB2CPassengers.NewRow();
                        dr[0] = mystring.GetSpellInitial(passenger.fet_Name);
                        dr[1] = item;
                        dtB2CPassengers.Rows.Add(dr);
                    }
                }
            }
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            bt_Select_Click(sender, e);
        }

        private void txtNameFilter_TextChanged(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            string py = this.txtNameFilter.Text;
            DataRow[] drs = dtB2CPassengers.Select("py like '" + py + "%'");

            foreach (DataRow dr in drs)
            {
                string item = dr[1].ToString();
                this.listBox1.Items.Add(item);
            }
        }
    }
}