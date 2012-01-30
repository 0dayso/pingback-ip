using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ePlus
{
    public partial class NewOrderWeb : Form
    {
        public NewOrderWeb()
        {
            InitializeComponent();
        }

        private void NewOrderWeb_Load(object sender, EventArgs e)
        {

        }

        public void Navigate()
        {
            string url_1 = "Ticket/UpdateTicketOrder.aspx?TicketOrderID={0}&CurStepID=1&";
            url_1 = string.Format(url_1, Options.GlobalVar.B2CNewOrderID);

            string url_2 = GlobalVar.WebUrl.ToLower();//    http://{0}/AirLineTicket/default.aspx?strSessionUUID={1}
            int index1 = url_2.LastIndexOf('/');
            int index2 = url_2.LastIndexOf("?");
            string strPage = url_2.Substring(index1 + 1, index2 - index1);//截取出字符串:default.aspx?

            url_1 = url_2.Replace(strPage, url_1);

            webBrowser1.Navigate(url_1);
        }

        private void NewOrderWeb_FormClosing(object sender, FormClosingEventArgs e)
        {
            Options.GlobalVar.B2CNewOrderID = 0;
            this.Visible = false;
            e.Cancel = true;
        }
    }
}